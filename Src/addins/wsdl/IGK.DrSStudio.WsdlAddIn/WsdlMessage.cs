

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WsdlMessage.cs
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
    /// represent a wsdlMessage
    /// </summary>
    public class WsdlMessage : WsdlItem 
    {
        private WsdlPart m_name;

        public WsdlPart Part
        {
            get { return m_name; }           
        }
        public override string TagName
        {
            get
            {
                return WsdlConstant.MESSAGE_TAG_NAME;
            }
        }
        public WsdlMessage()
        {
            this.m_name = new WsdlPart();
        }

        protected internal override void LoadProperties(CoreXmlElement c)
        {
            base.LoadProperties(c);
            c.AddChild(this.Part.GetNode());
        }
    }
}
