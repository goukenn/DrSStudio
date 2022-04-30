

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreDependencyObject.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:CoreDependencyObject.cs
*/
using IGK.ICore;using IGK.ICore.WorkingObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.Dependency;
using System.Reflection;
using IGK.ICore.JSon;
using IGK.ICore.ComponentModel;
using System.Collections;

namespace IGK.ICore.Dependency
{
    /// <summary>
    /// Represent a default dependency object
    /// </summary>
    public class CoreDependencyObject : CoreWorkingObjectBase
    {
        private static Dictionary<string, CoreDependencyInfo> sm_infos;//global dependency info
        private static Dictionary<CoreDependencyObject, CoreDependencyValue> sm_values;
        private static Dictionary<Type, CoreDependencyObject> sm_dependency_key;
        
        static CoreDependencyObject() {
            //load dependency object properties
      sm_dependency_key = new Dictionary<Type, CoreDependencyObject>();
            sm_infos = new Dictionary<string, CoreDependencyInfo>();
            sm_values = new Dictionary<CoreDependencyObject, CoreDependencyValue>();
            var v_dom = AppDomain.CurrentDomain;
            v_dom.AssemblyLoad += (o,e) =>
            {
                
                __loadInfo(e.LoadedAssembly);
            };
            foreach (Assembly item in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    __loadInfo(item);
                }
                catch(Exception ex) {
                    CoreLog.WriteLine(ex.Message);
                }
            }
            if (CoreApplicationManager.Application != null)
                CoreApplicationManager.Application.ApplicationExit += _ApplicationExit;            
        }

        public static CoreDependencyProperty GetRegisterProperty(string name)
        {
            return CoreDependencyProperty.GetProperty(name);         
        }
        public static CoreDependencyProperty GetRegisterProperty(CoreDependencyInfo propertyInfo)
        {
            if (propertyInfo == null) return null;
            return CoreDependencyProperty.GetProperty(propertyInfo.DeclaringType+"::"+propertyInfo.Name);
        }
        static void _ApplicationExit(object sender, EventArgs e)
        {
            
        }

        private static void __loadInfo(Assembly item)
        {
            var h = typeof(CoreDependencyNameAttribute);
            foreach (Type t in item.GetTypes ())
	        {
                if (t.IsAbstract )continue;
                if (CoreDependencyNameAttribute.IsDefined(t, h))
                {
                    CoreDependencyNameAttribute v_h =
                        t.GetCustomAttribute(h) as CoreDependencyNameAttribute;

                    if (sm_infos.ContainsKey(v_h.Name))
                    {
                        CoreLog.WriteLine("Dependency already loaded from ..." + sm_infos[v_h.Name].DeclaringType.FullName);
                    }
                    else {
                        var v_c = CoreDependencyInfo.Create(
                            v_h.Name,
                             enuDependencyType.Container ,
                             t
                            );
                        sm_infos.Add (v_h.Name , v_c);
                        //load childs 
                        __loadInfoProperties(v_c);
                    }
                }
	        }
    
        }

        private static void __loadInfoProperties(CoreDependencyInfo v_c)
        {
            string key = string.Empty;
            foreach (var s in v_c.DeclaringType.GetFields( BindingFlags.Static | BindingFlags.Public))
            {
                if (s.FieldType.IsAssignableFrom (typeof(CoreDependencyProperty)))
                {
                    CoreDependencyProperty dp = s.GetValue(null) as CoreDependencyProperty;
                    var g = CoreDependencyInfo.Create(
                        dp.Name, 
                        enuDependencyType.Property,
                        v_c.DeclaringType);
                    g.Parent = v_c;
                    sm_infos.Add(g.GetFullName(), g);
                }
            }
        }
        /// <summary>
        /// get the dependency object property
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        public virtual object GetValue(CoreDependencyProperty prop)
        {
            //property value chain
            if (sm_values.ContainsKey(this))
                return sm_values[this].GetValue(prop);
            return prop.Metadata!=null? prop.Metadata.DefaultValue : null;            
        }
        /// <summary>
        /// get the current value
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public object GetValue(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;            
            CoreDependencyProperty p = GetRegisterProperty(GetType().FullName + "::" + name);
            if (p != null)
            {
                return GetValue(this, p);
            }
            else { //not a current property value
                if (sm_infos.ContainsKey(name))
                {
                    var e = sm_infos[name];
                    p = GetRegisterProperty(e.DeclaringType + "::" + e.Name);
                    return GetValue(this, p);
                }
            }
            return null;
        }
        /// <summary>
        /// get the value of the target dependency 
        /// </summary>
        /// <param name="source">source of the dependency. exemple Margin </param>
        /// <param name="target">target that the dependy object is apply to . exemple Rectangle</param>
        /// <returns></returns>
        public static object GetValue(CoreDependencyObject source, CoreDependencyProperty target)
        {
            if (sm_values.ContainsKey(source))
            return sm_values[source].GetValue(target);
            

            return null;
        }

        /// <summary>
        /// Set Dependen property 
        /// </summary>
        /// <param name="prop">properties</param>
        /// <param name="value">value of the property </param>
        public void SetValue(CoreDependencyProperty prop, object value)
        {
            SetValue(this, prop, value);
        }
        /// <summary>
        /// set global dependey value
        /// </summary>
        /// <param name="coreDependencyObject"></param>
        /// <param name="prop"></param>
        /// <param name="value"></param>
        private static void SetValue(CoreDependencyObject coreDependencyObject, CoreDependencyProperty prop, object value)
        {
            if (sm_values.ContainsKey(coreDependencyObject))
                sm_values[coreDependencyObject].SetProperty(prop, value, enuDependencyValueType.Default);
            else { 
                var p = new CoreDependencyValue ();
                p.SetProperty(prop, value , enuDependencyValueType.Default );
                sm_values.Add(coreDependencyObject, p);
            }
        }
        /// <summary>
        /// get the stored dependency info
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<CoreDependencyInfo> GetDependencyInfos()
        {
            //load assembly from type
            return sm_infos.Values;
        }
        /// <summary>
        /// return dependency info that will imply
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal static CoreDependencyInfo GetObjectByKey(string name)
        {
            if (sm_infos.ContainsKey(name))
            {
                return sm_infos[name];
            }
            return null;
        }

        /// <summary>
        /// get the objet that will be used to reference a specific dependency objet
        /// </summary>
        /// <param name="type">type of core dependety</param>
        /// <returns></returns>
        internal static CoreDependencyObject GetDenpendency(Type type)
        {
            if (type ==null)return null;
                if (sm_dependency_key.ContainsKey(type))
                    return sm_dependency_key[type];


                if (!type.IsAbstract && type.IsSubclassOf(typeof(CoreDependencyObject)))
                { 
                    var dp = type.Assembly.CreateInstance (type.FullName) as CoreDependencyObject;
                    sm_dependency_key.Add(type, dp);
                    return dp;
                }
                return null;
        }

        internal static IEnumerable GetDenpendencyValues(CoreDependencyObject s)
        {
            if (sm_values.ContainsKey(s))
                return sm_values[s].GetValues( enuDependencyValueType.Default);
            return null;
        }

        internal static CoreDependencyInfo GetDependencyInfo(CoreDependencyProperty property)
        {
            var t = property.DeclaringType;
            string k = property.DeclaringType+":"+property.Name ;
             CoreDependencyNameAttribute v_h =
                       t.GetCustomAttribute(typeof (CoreDependencyNameAttribute)) as CoreDependencyNameAttribute;
            return sm_infos [ v_h.Name + "." + property.Name];            
        }
    }
}

