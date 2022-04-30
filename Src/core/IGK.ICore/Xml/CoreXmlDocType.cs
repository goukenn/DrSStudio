

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XmlDocType.cs
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
file:XmlDocType.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Xml
{
    class CoreXmlDocType : CoreXmlElement
    {
        private string m_Value;
        public override  string Value
        {
            get { return m_Value; }
            //set
            //{
            //    if (m_Value != value)
            //    {
            //        m_Value = value;
            //    }
            //}
        }
        public CoreXmlDocType()
        {
            this.m_Value = "PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\"";
        }
        public override string RenderXML(IXmlOptions option)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append ("<!DOCTYPE html "+this.Value+ ">");
            return sb.ToString();
        }
    }
}

