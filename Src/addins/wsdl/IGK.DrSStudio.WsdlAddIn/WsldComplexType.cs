

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WsldComplexType.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Wsdl
{
    [WsdlXSDElement()]
    /// <summary>
    /// represent a complex type
    /// </summary>
    public class WsldComplexType : WsdlType 
    {
        List<WsldComplexTypeDefinition> m_definitions;

        class WsldComplexTypeDefinition : WsdlElement 
        {
            public WsldComplexTypeDefinition()
            {

            }
            public override string TagName
            {
                get
                {
                    return "s:element";
                }
            }
            
            public string type { get; set; }
            private bool m_IsArray;
            /// <summary>
            /// get or set if this is array
            /// </summary>
            public bool OutBounded
            {
                get { return m_IsArray; }
                set
                {
                    if (m_IsArray != value)
                    {
                        m_IsArray = value;
                    }
                }
            }

            protected internal override void LoadProperties(CoreXmlElement c)
            {
                base.LoadProperties(c);
                c["type"]="s:"+this.type;
                if (OutBounded)
                {
                    c["minOccurs"] = "0";
                    c["minMaxOccurs"] = WsdlConstant.UNBOUNDED;
                }
            }
        }
        
        public WsldComplexType()
        {
            this.m_definitions = new List<WsldComplexTypeDefinition>();
        }
        
        public override string TagName
        {
            get
            {
                return "s:complexType";
            }
        }

        protected internal override void LoadProperties(CoreXmlElement c)
        {
            base.LoadProperties(c);
            var t = c.Add("s:sequence");
            foreach (WsldComplexTypeDefinition def in this.m_definitions)
            {
                t.AddChild(def.GetNode());
            }
        }
        /// <summary>
        /// add definition
        /// </summary>
        /// <param name="p"></param>
        /// <param name="type"></param>
        public void AddDef(string p, Type type)
        {
            string t = WsdlUtility.GetPrimitiveType (type);
            if (!string.IsNullOrEmpty(t))
            {
                this.m_definitions.Add(new WsldComplexTypeDefinition() { Name=p, type = t });
            }
        }

    }
}
