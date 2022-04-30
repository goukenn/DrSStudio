

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WsdlSoapBinding.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Wsdl
{
    public class WsdlSoapBinding : WsdlItem 
    {
    //    <soap:binding transport="http://schemas.xmlsoap.org/soap/http" />
    //<wsdl:operation name="HelloWorld">
    //  <soap:operation soapAction="http://tempuri.org/HelloWorld" style="document" />
    //  <wsdl:input>
    //    <soap:body use="literal" />
    //  </wsdl:input>
    //  <wsdl:output>
    //    <soap:body use="literal" />
    //  </wsdl:output>
    //</wsdl:operation>
        public override string TagName
        {
            get
            {
                return "soap:binding";
            }
        }

        public WsdlSoapBinding()
        {
           // this.m_operation = new WsdlSoapOperation(this);
        }

        //public class WsdlSoapOperation : WsdlItem
        //{
        //    private WsdlSoapBinding m_owner;

        //    public WsdlSoapOperation(WsdlSoapBinding wsdlSoapBinding)
        //    {                
        //        this.m_owner = wsdlSoapBinding;
        //    }
        //    public override string TagName
        //    {
        //        get
        //        {
        //            return "soap:operation";
        //        }
        //    }
        //    protected internal override void LoadProperties(CoreXmlElement c)
        //    {
        //        this.Name = null;
        //        base.LoadProperties(c);
        //        //c["soapAction"] = this.m_owner.soapAction;
        //        //c["style"] = this.m_owner.style.ToString();
        //        //var d = c.Add ("input");
        //        //d.Add ("soap:body")["use"]="literal";
        //        //d = c.Add ();
        //        //d.Add ("soap:body")["use"]="literal";
        //    }
        //}
        
        
        
        //private WsdlSoapOperation m_operation;
        //[WsdlElement ()]
        ///// <summary>
        ///// get the soap operation
        ///// </summary>
        //public WsdlSoapOperation operation
        //{
        //    get { return m_operation; }
        //}
        

        [WsdlAttribute()]
        public string transport
        {
            get { return "http://schemas.xmlsoap.org/soap/http"; }
        }

        public string soapAction { get; set; }

        public enuWsdlStyle style { get; set; }
    }
}
