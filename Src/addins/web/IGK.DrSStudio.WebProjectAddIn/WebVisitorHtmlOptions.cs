

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebVisitorHtmlOptions.cs
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
file:WebVisitorHtmlOptions.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.WebProjectAddIn
{
    using IGK.ICore.Xml;
    class WebVisitorHtmlOptions : CoreXmlSettingOptions
    {
        public WebVisitorHtmlOptions()
        {
            this.Indent = true;
            this.Context = "xml";
        }

        static List<string> m_closeTag;
        static WebVisitorHtmlOptions() {
            m_closeTag = new List<string>();
            m_closeTag.Add("div");
            m_closeTag.Add("script");
            m_closeTag.Add("style");
            m_closeTag.Add("form");
            m_closeTag.Add("head");
            m_closeTag.Add("body");
        }
        public override bool MustCloseTag(string tagName)
        {
            if (string.IsNullOrEmpty(tagName))
                return false;
            return m_closeTag.Contains(tagName.ToLower());
        }
    }
}

