

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WsdlOperationParam.cs
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
    public class WsdlOperationParam : WsdlItem 
    {
        private enuWsdlParamType m_paramType;
        private string m_message;
        
        public WsdlOperationParam(enuWsdlParamType paramType)
        {
            this.m_paramType = paramType;
            this.Name = null;
        }
        public override string TagName
        {
            get
            {
                return "wsdl:"+m_paramType.ToString().ToLower ();
            }
        }
        protected internal override void LoadProperties(CoreXmlElement c)
        {
            base.LoadProperties(c);
            c["message"] = "tns:" + this.message;
        }
        /// <summary>
        /// get or set the message id
        /// </summary>
        public string message
        {
            get { return m_message; }
            set
            {
                if (m_message != value)
                {
                    m_message = value;
                }
            }
        }
    }
}
