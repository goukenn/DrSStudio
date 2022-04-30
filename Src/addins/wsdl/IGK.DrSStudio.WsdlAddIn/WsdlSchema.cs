

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WsdlSchema.cs
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
    /// <summary>
    /// represent a wsdl xsd schema
    /// </summary>
    public class WsdlSchema : WsdlElement 
    {
        List<WsdlType> m_types;

        public WsdlSchema()
        {
            this.m_types = new List<WsdlType>();
        }
        public override string TagName
        {
            get
            {
                return "s:schema";
            }
        }
        private enuWsdlFormDefault m_elementFormDefault;

        [WsdlAttribute ()]
        public enuWsdlFormDefault elementFormDefault
        {
            get { return m_elementFormDefault; }
            set
            {
                if (m_elementFormDefault != value)
                {
                    m_elementFormDefault = value;
                }
            }
        }
        private string m_targetNameSpace;
        [WsdlAttribute()]
        public string targetNamespace
        {
            get { return m_targetNameSpace; }
            set
            {
                if (m_targetNameSpace != value)
                {
                    m_targetNameSpace = value;
                }
            }
        }
        public int Count { get { return this.m_types.Count; } }

        protected internal override void LoadProperties(CoreXmlElement c)
        {
            base.LoadProperties(c);
            foreach (WsdlType item in this.m_types)
            {
                c.AddChild(item.GetNode());
            }
        }
        internal void AddType(WsdlType c)
        {
            this.m_types.Add(c);            
        }
    }
}
