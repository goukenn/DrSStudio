

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreXMLElementAttribute.cs
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
file:CoreXMLElementAttribute.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Text;
namespace IGK.ICore.Codec
{
    /// <summary>
    /// represent the default attribute elements
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CoreXMLElementAttribute : CoreXMLSerializerAttribute 
    {
        private bool m_isElement;
        public bool IsElement { get { return this.m_isElement; } set { this.m_isElement = value; } }
        /// <summary>
        /// .ctr
        /// </summary>
        public CoreXMLElementAttribute()
        {
            this.m_isElement = true;
        }
        /// <summary>
        /// .ctr
        /// </summary>
        /// <param name="IsElement"></param>
        public CoreXMLElementAttribute(bool IsElement)
        {
            this.m_isElement = IsElement;
        }
        public static CoreXMLElementAttribute GetCustomAttribute(System.Reflection.PropertyInfo pr)
        {
            object[] o = pr.GetCustomAttributes(typeof(CoreXMLElementAttribute), false);
            if (o.Length == 1)
                return o[0] as CoreXMLElementAttribute;
            CoreXMLElementAttribute attrib = null;
            Type t = pr.DeclaringType;
            Type[] interfaces = t.GetInterfaces();
            System.Reflection.PropertyInfo prInfo = null;
            for (int i = 0; i < interfaces.Length; i++)
            {
                prInfo = interfaces[i].GetProperty(pr.Name);
                if (prInfo != null)
                {
                    object[] obj = prInfo.GetCustomAttributes(typeof(CoreXMLElementAttribute), false);
                    if (obj.Length == 1)
                    {
                        return obj[0] as CoreXMLElementAttribute;
                    }
                }
            }
            return attrib;
        }
        public static bool IsCoreXMLElementAttribute(PropertyInfo prInfo)
        {
            return CoreXMLElementAttribute.GetCustomAttribute(prInfo) != null;
        }
    }
}

