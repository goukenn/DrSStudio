

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreDependencyProperty.cs
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
file:CoreDependencyProperty.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Dependency
{
    /// <summary>
    /// represent a core DependencyProperty 
    /// </summary>
    public class CoreDependencyProperty
    {
        static Dictionary<string, CoreDependencyProperty> sm_properties;
        private CoreDependencyPropertyMetadata m_metaData;
        private Type m_type;
        private string m_name;
        private Type m_declaringType;

        public override string ToString()
        {
            return this.GetType().Name + ":" + this.Name;
        }
        public static CoreDependencyProperty[] GetProperties(Type t)
        {
            List<CoreDependencyProperty> v_list = new List<CoreDependencyProperty>();
            if (t.IsClass && !t.IsAbstract && t.IsAssignableFrom(typeof(CoreDependencyObject)))            
            {
                foreach (var item in t.GetFields(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public ))
                {
                    CoreDependencyProperty p = item.GetValue(null) as CoreDependencyProperty;
                    if (p != null)
                    {
                        v_list.Add(p);
                    }
                }
            }
            return v_list.ToArray();
        }
    
        public CoreDependencyPropertyMetadata Metadata
        {
            get { return m_metaData; }
        }
        static CoreDependencyProperty()
        {
            sm_properties = new Dictionary<string, CoreDependencyProperty>();
        }
        /// <summary>
        /// .ctr core dependency property
        /// </summary>
        private CoreDependencyProperty()
        {
        }
        public static CoreDependencyProperty GetProperty(string fullName)
        {
            if (sm_properties.ContainsKey(fullName))
                return sm_properties[fullName];
            return null;
        }
        /// <summary>
        /// register a dependcy properties key
        /// </summary>
        /// <param name="name"></param>
        /// <param name="typeofproperty"></param>
        /// <param name="declaringType"></param>
        /// <param name="metadata"></param>
        /// <returns></returns>
        public static CoreDependencyProperty Register(string name, Type typeofproperty, 
            Type declaringType,
            CoreDependencyPropertyMetadata metadata)
        {
            if (typeofproperty == null)
                throw new CoreException(enuExceptionType.ArgumentIsNull, "typeofproperty");
            if (declaringType == null)
                throw new CoreException(enuExceptionType.ArgumentIsNull, "declaringType");

            CoreDependencyProperty prop = null;
            string v_key = declaringType.FullName + "::" + name;
            if (!sm_properties.ContainsKey(v_key))
            { 
               CoreDependencyProperty d =  new CoreDependencyProperty ();
               d.m_metaData = metadata;
               d.m_type = typeofproperty;
               d.m_name = name;
               d.m_declaringType = declaringType;
               sm_properties.Add(v_key, d);
               return d;
            }
            return prop;
        }
      

        public string Name { get { return this.m_name;  } }
        public Type PropertyType { get{return this.m_type ;} }
        public Type DeclaringType { get { return this.m_declaringType; } }

        /// <summary>
        /// retreive the full name of this property
        /// </summary>
        /// <returns></returns>
        public string GetFullName() {
             return CoreDependencyObject.GetDependencyInfo(this).GetFullName();            
        }

        
    }
}

