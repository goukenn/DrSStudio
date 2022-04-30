

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreSettingDefaultValueAttribute.cs
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
file:CoreSettingDefaultValueAttribute.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using IGK.ICore;using IGK.ICore.ComponentModel;
namespace IGK.ICore.Settings
{
    [AttributeUsage (AttributeTargets.Property )]
    public class CoreSettingDefaultValueAttribute : CoreAttribute 
    {
        private object m_value;
        private Type m_type;
        private enuSettingConfig m_ConfigType;
        /// <summary>
        /// get or set the default type configuration
        /// </summary>
        public enuSettingConfig ConfigType
        {
            get { return m_ConfigType; }
            set
            {
                if (m_ConfigType != value)
                {
                    m_ConfigType = value;
                }
            }
        }
        /// <summary>
        /// get the default value object
        /// </summary>
        public object Value { get { return m_value; } protected set { } }
        /// <summary>
        /// get the requested type 
        /// </summary>
        public Type Type { get { return m_type; } }
        public CoreSettingDefaultValueAttribute()
        {
            m_value = null;
        }
        public CoreSettingDefaultValueAttribute(object value)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            this.m_value = value;
            this.m_type = value.GetType();
        }
        public CoreSettingDefaultValueAttribute(object value, Type type)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            this.m_value = value;
            this.m_type = type;
        }
        /// <summary>
        /// retreive the real object default value
        /// </summary>
        /// <returns></returns>
        internal object GetValue()
        {
            if (m_value == null)
                return null;
            if (this.m_type == typeof(Colorf))
            {
                return Colorf.FromFloat((int)m_value);
            }
            TypeConverter conv = CoreTypeDescriptor.GetConverter(this.m_type);
            return conv.ConvertFromString(m_value.ToString());
        }
    }
}

