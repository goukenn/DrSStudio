

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebXmlWriter.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:WebXmlWriter.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IGK.ICore.Xml ;
namespace IGK.DrSStudio.WebAddIn
{
    class WebXmlWriter : CoreXmlWriter , IXmlOptions 
    {
        static List<string> m_mustCloseTag = new List<string>();
        static WebXmlWriter()
        {
            m_mustCloseTag.Add("html");
            m_mustCloseTag.Add("head");
            m_mustCloseTag.Add("body");
            m_mustCloseTag.Add("div");
            m_mustCloseTag.Add("form");
            m_mustCloseTag.Add("script");
            m_mustCloseTag.Add("noscript");
        }
        public void WriteDocType()
        {
            this.WriteString("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">");
            if (this.Indent)
                this.WriteString("\n");
        }
        public WebXmlWriter(StringBuilder sb, System.Xml.XmlWriterSettings setting):base(sb, setting )
        {            
        }
        public new static WebXmlWriter Create(StringBuilder sb, System.Xml.XmlWriterSettings setting)
        {
            return new WebXmlWriter(sb, setting);
        }
        public override bool MustCloseTag(string tagName)
        {
            return m_mustCloseTag.Contains(tagName.ToLower());
        }
    }
}

