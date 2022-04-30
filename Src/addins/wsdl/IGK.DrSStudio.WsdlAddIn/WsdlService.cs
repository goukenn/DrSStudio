

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WsdlService.cs
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
    public class WsdlService : WsdlItem, IWsdlTagItem
    {
        private WsdlBinding m_Binding;
        private enuWsdlOperationType m_ServiceType;
        private string m_Documentation;
        /// <summary>
        /// get or set the service docmentations
        /// </summary>
        public string Documentation
        {
            get { return m_Documentation; }
            set
            {
                if (m_Documentation != value)
                {
                    m_Documentation = value;
                }
            }
        }
        public enuWsdlOperationType ServiceType
        {
            get { return m_ServiceType; }
            set
            {
                if (m_ServiceType != value)
                {
                    m_ServiceType = value;
                }
            }
        }
        public WsdlBinding Binding
        {
            get { return m_Binding; }
            set
            {
                if (m_Binding != value)
                {
                    m_Binding = value;
                }
            }
        }
        public WsdlService(string name):base(name)
        {
        }
        protected internal override void LoadProperties(CoreXmlElement c)
        {
            if (this.Binding != null)                
            c["binding"] = "tns:"+this.Binding.Name;

            if (!string.IsNullOrEmpty(this.Documentation))
            {
                c.Add(WsdlConstant.DEFINITION_TAG_NAME).Content = this.Documentation;
            }
        }
        [WsdlAttribute ()]
        public Uri Uri { get; set; }
    }
}
