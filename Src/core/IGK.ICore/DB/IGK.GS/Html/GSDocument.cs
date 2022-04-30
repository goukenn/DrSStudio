using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.WinUI.Configuration;
using IGK.ICore.Settings;






﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.GS.Html
{
    /// <summary>
    /// represent the rendered document to render options on document
    /// </summary>
    public class GSDocument : IHtmlItem
    {

        private HtmlBody m_body;
        private HtmlHead m_header;
        private HtmlDocType m_docType;
        private HtmlItem m_title;
        private HtmlScriptManager m_scriptManager;
        private HtmlControllerManager m_controllerManager;

        /// <summary>
        /// get the document body
        /// </summary>
        public HtmlBody Body { get { return this.m_body; } }
        /// <summary>
        /// get the script manager
        /// </summary>
        public HtmlScriptManager ScriptManager { get { return this.m_scriptManager;  } }
        /// <summary>
        /// get the controller manager
        /// </summary>
        public HtmlControllerManager ControllerManager { get { return this.m_controllerManager; } }

        public HtmlItem addScript() {
            HtmlItem c = HtmlItem.CreateWebNode("script");
            c["type"] = "text/javascript";
            c["language"] = "javascript";
            this.m_header.add(c);
            return c;
        }
        public GSDocument()
        {
            this.m_body = new HtmlBody();
            this.m_header = new HtmlHead();
            this.m_docType = new HtmlDocType();
            this.m_scriptManager = new HtmlScriptManager(this);
            this.m_controllerManager = new HtmlControllerManager (this);
            this.m_title = HtmlItem.CreateWebNode("title");
            this.m_header.add(this.m_title);
            this.m_docType.Content = "html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\"";
            this.addStyle(Uri.UriSchemeFile + "://D://temp/main.css");
            this.addScript();
            this.m_controllerManager.ViewController();
        }

        private HtmlItem addStyle(string uri)
        {
            var lnk = HtmlItem.CreateWebNode("link");
            lnk["type"]="text/css";
            lnk["rel"]="stylesheet";
            lnk["href"] = uri;
            this.m_header.add(lnk);
            return lnk;
        }
    
        public string Render(IHtmlItemRenderOptions option)
        {
            StringBuilder sb = new StringBuilder();
           // sb.Append(this.m_docType.Render(option));
            sb.Append("<html>");
            sb.Append(m_header.Render(option));
            sb.Append(m_body.Render(option));
            sb.Append("</html>");
            return sb.ToString();
        }
        /// <summary>
        /// get or set the title of the document
        /// </summary>
        public string Title { get { return this.m_title.Content; } set { this.m_title.Content = value; } }
    }
}
