

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WsdlPrimitiveType.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿
using IGK.ICore;using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Wsdl
{
    public class WsdlPrimitiveType : WsdlType
    {
        private string m_typeName;
        private CoreXmlAttributeValue m_paramName;
        
        public string typeName
        {
            get { return m_typeName; }
            set
            {
                if (m_typeName != value)
                {
                    m_typeName = value;
                }
            }
        }
        protected internal override void LoadProperties(CoreXmlElement c)
        {
            base.LoadProperties(c);
            if (!string.IsNullOrEmpty(this.typeName))
            {
                var h = c.Add("s:complexType");
                h = h.Add("s:sequence").Add ("s:element");

                //c.Add("s:complexType");
                h["minOccurs"] = 0;
                h["maxOccurs"] =(string)( this.OutBounded ? "unbounded" : "1");
                h["type"] = "s:" + typeName;
                h["name"] = m_paramName;
            }
            
        }

        internal static WsdlPrimitiveType Create(Type type, string paramName)
        {
            if (string.IsNullOrEmpty(paramName))
                return null;
            string n = WsdlUtility.GetPrimitiveType(type)?? null;
            if (n != null)
            {
                WsdlPrimitiveType p = new WsdlPrimitiveType();
                p.typeName = n;
                p.m_paramName = paramName;
                return p;
            }
            return null;
        }

        public bool OutBounded { get; set; }
    }
}
