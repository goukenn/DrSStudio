using IGK.ICore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.DB
{
    /// <summary>
    /// Represent the default database Table 
    /// </summary>
    public class CoreDataBaseTable : MarshalByRefObject, ICoreDataValue
    {
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            ICoreDataTable b = this as ICoreDataTable;
            ICoreDataTable c = obj as ICoreDataTable;

            if ((b != null) && (c != null) && (b.GetType () == c.GetType()))
            {
                return b.clId == c.clId;
            }


            return base.Equals(obj);
        }
        
        public override string ToString()
        {
            return base.ToString();
        }
        public override System.Runtime.Remoting.ObjRef CreateObjRef(Type requestedType)
        {
            return base.CreateObjRef(requestedType);
        }

        public object GetValue(string name) {
            var p = GetType().GetProperty(name);
            if (p != null)
                return p.GetValue(this, null);
            return null;
        }
        public T GetValue<T>(string name, T basic = default (T))
        {
            var obj = this.GetValue (name );
            if  (obj == null)
                return basic ;
            if (obj is T)
                return (T)obj;
            return CoreExtensions.GetValue<T>(obj.ToString(), basic);           
            
        }
    }
}
