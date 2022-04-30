

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreXMLAttributeAttribute.cs
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
file:CoreXMLAttributeAttribute.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
namespace IGK.ICore.Codec
{
    /// <summary>
    /// represent the default attribute elements
    /// </summary>
    [AttributeUsage (AttributeTargets.Property , AllowMultiple=false)]
    public class CoreXMLAttributeAttribute:
        CoreXMLSerializerAttribute 
    {
        bool m_isAttribute;
        private bool m_IsResource;
        /// <summary>
        /// set if this attribute is ref id resources
        /// </summary>
        public bool IsResource
        {
            get { return m_IsResource; }
            set { m_IsResource = value; }
        }
        public bool IsAttribute {
            get { return this.m_isAttribute; }            
        }
        public CoreXMLAttributeAttribute() {
            this.m_isAttribute = true;
            this.m_IsResource = false;
        }
        public CoreXMLAttributeAttribute(bool isAttribute)
        {
            this.m_isAttribute = isAttribute;
            this.m_IsResource = false;
        }
        public static CoreXMLAttributeAttribute GetCustomAttribute(System.Reflection.PropertyInfo pr)
        {
            object[] o = pr.GetCustomAttributes(typeof(CoreXMLAttributeAttribute), false);
            if (o.Length == 1)
                return o[0] as CoreXMLAttributeAttribute;
            CoreXMLAttributeAttribute attrib = null;
            Type t = pr.DeclaringType;
            Type[] interfaces = t.GetInterfaces();
            System.Reflection.PropertyInfo prInfo = null;
            for (int i = 0; i < interfaces.Length; i++)
            {
                prInfo = interfaces[i].GetProperty(pr.Name);
                if (prInfo != null)
                {
                    object[] obj = prInfo.GetCustomAttributes(typeof(CoreXMLAttributeAttribute), false);
                    if (obj.Length == 1)
                    {
                        return obj[0] as CoreXMLAttributeAttribute;
                    }
                }
            }
            return attrib;
        }
        //public new static bool IsDefined( PropertyInfo pr)
        //{
        //    return false;
        //}
        /// <summary>
        /// determine a a object  contanct a property
        /// </summary>
        /// <param name="targetObject"></param>
        /// <param name="pr"></param>
        /// <returns></returns>
        public static bool IsDefined(object targetObject, PropertyInfo pr)
        {
            return CoreAttribute.IsDefined(targetObject, pr, typeof(CoreXMLAttributeAttribute));
        }
    }
}

