using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.DB
{
    using IGK.ICore;
    using IGK.ICore.JSon;

    public abstract class CoreDataInitializerBase : ICoreDataTableInitializer
    {       

        private CoreDataAdapterBase m_Adapter;
        private Type m_InitType;
        /// <summary>
        /// get type to initiliazer
        /// </summary>
        public Type InitType
        {
            get { return m_InitType; }
        }
        /// <summary>
        /// get data adapter
        /// </summary>
        public CoreDataAdapterBase Adapter
        {
            get { return m_Adapter; }
        }
        public void Initialize(Type type, CoreDataAdapterBase adapter)
        {
            this.m_InitType = type;
            this.m_Adapter = adapter;
            var t = CoreDBManager.CreateInterfaceInstance(type, null);
            MethodInfo.GetCurrentMethod().Visit(this, new object[] { t });
        }

        //init interface

       // public interface Itbbaobabtv_extra_info_types : ICoreDataTable
        public void LoadData(ICoreDataTable o, string jsonData) {

            CoreJSon c = new CoreJSon();
            var d = CoreJSonReader.Load(jsonData);
            Type t = o.GetType();

            var tr = GetPropertiesSources(o.GetSourceTableInterface());

            
            foreach (var p in tr) //t.GetProperties())
            {
                if (d.ContainsKey(p.Name)) continue;

                var iss = p.GetCustomAttribute(typeof(CoreDataTableFieldAttribute)) as CoreDataTableFieldAttribute;


                if ((iss?.Binding & enuCoreDataField.IsNotNull) == enuCoreDataField.IsNotNull) {
                    d.Add(p.Name, 
                        iss.Default ??  Adapter.GetDefaultType(p.PropertyType));
                }
                //p.SetValue(o, null);
            }


            CoreDBManager.Insert(t, d, Adapter);
            ////reset data;
            //foreach (var p in t.GetProperties()) {
            //    p.SetValue(o, null);
            //}

            //foreach (KeyValuePair<string, object> item in d)
            //{
            //    t.GetProperty(item.Key).SetValue(o, item.Value);
            //}

            //o.Insert(Adapter);
        }

        private static PropertyInfo[] GetPropertiesSources(Type type)
        {
            Dictionary<string, PropertyInfo> cp = new Dictionary<string, PropertyInfo>();

            Type[] gg = type.FindInterfaces(new TypeFilter((o, g) => {
                return (o == typeof(ICoreDataTable)) ||
                (o.ImpletementInterface(typeof(ICoreDataTable)));
            }), null
                  );

            int i = 0;


            while (type != null)
            {
                foreach (var c in type.GetProperties()) {
                    if (cp.ContainsKey(c.Name))
                        continue;

                    cp.Add(c.Name, c);

                }

                if (i < gg.Length)
                    type = gg[i];
                else
                    type = null;
                i++;                
            }
            return cp.Values.ToArray();
        }
    }
}
