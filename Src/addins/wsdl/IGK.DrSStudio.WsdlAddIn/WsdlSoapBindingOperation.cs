

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WsdlSoapBindingOperation.cs
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
using System.Threading.Tasks;

namespace IGK.DrSStudio.Wsdl
{
    public class WsdlSoapBindingOperation : WsdlBindingOperation
    {
        private class WsdlSoapOperationTag : WsdlItem 
        {
            private WsdlSoapBindingOperation m_owner;

            public WsdlSoapOperationTag(WsdlSoapBindingOperation wsdlSoapBindingOperation)
            {                
                this.m_owner = wsdlSoapBindingOperation;
            }
            public override string TagName
            {
                get
                {
                    return "soap:operation";
                }
            }
            
            protected internal override void LoadProperties(CoreXmlElement c)
            {
                base.LoadProperties(c);

            
            }
            [WsdlAttribute ()]
            public string soapAction {
                get {
                    return m_owner.soapAction;
                }
            }
            [WsdlAttribute()]
            public enuWsdlStyle  style
            {
                get
                {
                    return m_owner.style ;
                }
            }

        }
        private WsdlSoapOperationTag m_opTag;

        public WsdlSoapBindingOperation()
        {
            this.m_opTag = new WsdlSoapOperationTag(this);
        }
        public override string TagName
        {
            get
            {
                return "operation";
            }
        }
        protected internal override void LoadProperties(CoreXmlElement c)
        {
            base.LoadProperties(c);
            c.AddChild(this.m_opTag.GetNode());
            //c["soapAction"] = this.m_owner.soapAction;
            //c["style"] = this.m_owner.style.ToString();
            var d = c.Add("wsdl:input");
            d.Add("soap:body")["use"] = "literal";
            d = c.Add("wsdl:output");
            d.Add("soap:body")["use"] = "literal";
        }
    }
}
