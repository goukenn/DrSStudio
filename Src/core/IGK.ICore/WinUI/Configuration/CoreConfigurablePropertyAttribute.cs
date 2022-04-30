

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreConfigurablePropertyAttribute.cs
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
file:CoreConfigurablePropertyAttribute.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.WinUI.Configuration
{
    /// <summary>
    /// represent a configurable property . in core System
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property , AllowMultiple = false  , Inherited = false  ) ]
    public class CoreConfigurablePropertyAttribute : CoreAttribute 
    {
        private bool m_IsConfigurable;
        private string m_AttribName;
        private string m_CaptionKey;
        private int m_Index;
        private string m_Group;
      
        public int Index
        {
            get { return m_Index; }
            set
            {
                if (m_Index != value)
                {
                    m_Index = value;
                }
            }
        }
        public string CaptionKey
        {
            get { return m_CaptionKey; }
        }
        public string AttribName
        {
            get { return m_AttribName; }
        }
        public CoreConfigurablePropertyAttribute(string name, string captionkey):this()
        {
            this.m_AttribName = name;
            this.m_CaptionKey = captionkey;
            this.m_Index = -1;
            this.m_Group = "Default";
        }
        public CoreConfigurablePropertyAttribute()
        {
            this.m_AttribName = null;
            this.m_CaptionKey = null;
            this.m_IsConfigurable = true;
            this.m_Group = CoreConstant.DEFAULT_CONFIGURATION_GROUP;
            this.m_Group = "Default";
            this.m_Index = -1;
        }
        /// <summary>
        /// get or set the group
        /// </summary>
        public string Group
        {
            get { return m_Group; }
            set
            {
                if (m_Group != value)
                {
                    m_Group = value;
                }
            }
        }
        public bool IsConfigurable
        {
            get { return m_IsConfigurable; }
            set
            {
                if (m_IsConfigurable != value)
                {
                    m_IsConfigurable = value;
                }
            }
        }


        /// <summary>
        /// get or set the default value type of the property on configuration. note the type must implement ICoreConfigurableEnumProperty
        /// </summary>
        public Type DefaultValues { get; set; }
        public CoreConfigurablePropertyAttribute(bool configurable):this()
        {
            this.m_IsConfigurable = configurable;
        }
        public static PropertyInfo[] ConfigurableProperties(Type type)
        {
            List<PropertyInfo> v_l = new List<PropertyInfo>();
            foreach (PropertyInfo p in type.GetProperties())
            {
                CoreConfigurablePropertyAttribute v_p = GetCustomAttribute(p);
                if (v_p != null)
                {
                    v_l.Add(p);
                }
            }
            return v_l.ToArray();
        }
        public  static CoreConfigurablePropertyAttribute GetCustomAttribute(System.Reflection.PropertyInfo prInfo)
        {
            object[] o = prInfo.GetCustomAttributes(typeof(CoreConfigurablePropertyAttribute), false);
            if (o.Length == 1)
                return o[0] as CoreConfigurablePropertyAttribute;
            CoreConfigurablePropertyAttribute attrib = null;
            Type t = prInfo.DeclaringType;
            Type[] interfaces = t.GetInterfaces();
            System.Reflection.PropertyInfo v_prInfo = null;
            for (int i = 0; i < interfaces.Length; i++)
            {
                v_prInfo = interfaces[i].GetProperty(prInfo.Name);
                if (v_prInfo != null)
                {
                    object[] obj = v_prInfo.GetCustomAttributes(typeof(CoreConfigurablePropertyAttribute), false);
                    if (obj.Length == 1)
                    {
                        return obj[0] as CoreConfigurablePropertyAttribute;
                    }
                }
            }
            return attrib;
        }
    }
}

