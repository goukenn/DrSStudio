

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreSettingAttribute.cs
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
file:CoreSettingAttribute.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
namespace IGK.ICore.Settings
{
    /// <summary>
    /// application on class that will be consider as setting
    /// </summary>
    public abstract class CoreAttributeSettingBase : CoreAttribute
    {
        private string m_Name;
        private string m_ImageKey;
        private int m_Index;
        /// <summary>
        /// get or set the group index
        /// </summary>
        public abstract int GroupIndex
        {
            get;
        }
        /// <summary>
        /// get or set the group index
        /// </summary>
        public abstract string GroupName
        {
            get;
        }
        /// <summary>
        /// represent the representative image keys off this setting attribute
        /// </summary>
        public string ImageKey
        {
            get { return m_ImageKey; }
            set
            {
                if (m_ImageKey != value)
                {
                    m_ImageKey = value;
                }
            }
        }
        /// <summary>
        /// get or set the index
        /// </summary>
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
        /// <summary>
        /// get or set the name of this property
        /// </summary>
        public string Name
        {
            get { return m_Name; }
            set { this.m_Name = value ; }
        }
        public CoreAttributeSettingBase()//(string Name)
        {
            this.m_Index = 0;
        }
        public CoreAttributeSettingBase(string name)
        {
            this.m_Name = name;
            this.m_Index = 0;
        }
        public override string ToString()
        {
            return this.Name;
        }
        public static bool IsSettingClass(Type t)
        {
            if (t == null) return false;
            if (t.IsSubclassOf(typeof(CoreSettingBase)))
            {
                object[] obj = t.GetCustomAttributes(MethodInfo.GetCurrentMethod().DeclaringType,
                false);
                return (obj.Length == 1);
            }
            return false;
        }
        internal static CoreAttributeSettingBase GetAttribute(Type type)
        {
            try
            {
                object[] obj = type.GetCustomAttributes(typeof(CoreAttributeSettingBase), false);
                if (obj.Length == 1)
                    return (CoreAttributeSettingBase)obj[0];
            }
            catch {
                CoreLog.WriteDebug("CoreSettingTypeError : " + type.FullName );
            }
            return null;
        }
    }
}

