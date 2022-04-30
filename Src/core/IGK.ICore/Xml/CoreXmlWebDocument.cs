using IGK.ICore.IO;
using IGK.ICore.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Xml
{
    /// <summary>
    /// represent a xml web document
    /// </summary>
    public class CoreXmlWebDocument : CoreXmlWebElement 
    {

        const string DOCTYPE = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">";

        private CoreXmlElement m_head;
        private CoreXmlWebBodyElement m_body;
        private XmlWebDocumentLinkManager m_linkManager;
        private XmlWebDocumentScriptManager m_scriptManager;
        private bool m_ForWebBrowserDocument;

        /// <summary>
        /// indicate that the document if for WebBrowser document
        /// </summary>
        public bool ForWebBrowserDocument
        {
            get { return m_ForWebBrowserDocument; }
            set
            {
                if (m_ForWebBrowserDocument != value)
                {
                    m_ForWebBrowserDocument = value;
                }
            }
        }
        /// <summary>
        /// get the body content
        /// </summary>
        public CoreXmlWebBodyElement Body { get { return m_body; } }
        /// <summary>
        /// get the head document
        /// </summary>
        public CoreXmlElement Head { get { return m_head; } }

        /// <summary>
        /// 
        /// </summary>
        abstract class XmlWebDocumentItemBase : CoreXmlElement 
        {
            private CoreXmlWebDocument m_document;
            protected CoreXmlWebDocument Document {
                get {
                    return this.m_document;
                }
            }
            public XmlWebDocumentItemBase(CoreXmlWebDocument document)
            {
                this.m_document = document;
            }

        }
        class XmlWebDocumentScriptManager : XmlWebDocumentItemBase 
        {
            private Dictionary<string, CoreXmlElement> m_scripts;
            public XmlWebDocumentScriptManager(CoreXmlWebDocument document):base(document )
            {
                this.m_scripts = new Dictionary<string, CoreXmlElement>();
            }
            public override string RenderXML(IXmlOptions option)
            {
                if (m_scripts.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var item in m_scripts )
                    {
                        sb.Append(item.Value.RenderXML(option));
                    }
                    return sb.ToString();
                }
                return string.Empty;
            }

            internal CoreXmlElement  AddScript(string scriptFile)
            {

                if (string.IsNullOrEmpty(scriptFile))
                    return null;

                if (this.m_scripts.ContainsKey(scriptFile))
                    return this.m_scripts[scriptFile];

                var lnk = CoreXmlWebElement.CreateXmlNode("script") as CoreXmlWebScriptElement;
                this.m_scripts.Add(scriptFile, lnk);
                lnk["src"] = new CoreXmlUriLink(scriptFile);
                lnk["type"] = "text/javascript";
                lnk["language"] = "javascript";
                lnk.Document = this.Document; 
                return lnk;
            }
            public bool RemoveScript(string file)
            {
                if (this.m_scripts.ContainsKey(file))
                {
                   // CoreXmlWebScriptElement h = 
                   if (this.m_scripts[file] as CoreXmlWebScriptElement is CoreXmlWebScriptElement h)                   
                        h.Document = null;
                    this.m_scripts.Remove(file);
                    return true;
                }
                return false ;
            }
        }
        class XmlWebDocumentLinkManager : XmlWebDocumentItemBase
        {
            private Dictionary<string, CoreXmlElement> m_links;
            
            public XmlWebDocumentLinkManager(CoreXmlWebDocument xmlWebDocument):base(xmlWebDocument )
            {
                this.m_links = new Dictionary<string, CoreXmlElement>();
            }
            public override string RenderXML(IXmlOptions option)
            {
                if (m_links.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var item in m_links)
                    {
                        sb.Append(item.Value.RenderXML(option));
                    }
                    return sb.ToString();
                }
                return string.Empty;
            }
            public override string RenderInnerHTML(IXmlOptions option)
            {
                return null;
            }

            internal  CoreXmlElement AddLink(string linkFile, string type="text/css", string rel="stylesheet")
            {
                if (string.IsNullOrEmpty (linkFile ))
                    return null;

                if (    this.m_links.ContainsKey(linkFile))
                    return this.m_links[linkFile];

                var lnk = CoreXmlWebElement.CreateXmlNode("link");
                this.m_links.Add(linkFile, lnk);
                lnk["href"] = new CoreXmlUriLink(linkFile);
                lnk["rel"] = rel;
                lnk["type"] = type;
                return lnk;

            }
        }
        public CoreXmlWebDocument():base()
        {
            m_head = CoreXmlWebElement.CreateXmlNode("head");
            m_body = new CoreXmlWebBodyElement(this);//.CreateXmlNode("body");
            this.m_scriptManager =  new XmlWebDocumentScriptManager(this);
            this.m_linkManager = new XmlWebDocumentLinkManager(this);

            m_head.AddChild(this.m_linkManager);
            m_head.AddChild(this.m_scriptManager);
            //init child element

        }
        public void AddScript(string scriptFile)
        {
            this.m_scriptManager.AddScript(scriptFile);
        }
        public void AddLink(string linkFile)
        {
            this.m_linkManager.AddLink(linkFile);
        }

        public override string RenderXML(IXmlOptions option)
        {
            if (option == null)
                option = CreateXmlOptions();
            StringBuilder sb = new StringBuilder();
            CoreXmlElement c = CoreXmlElement.CreateXmlNode("html");
            c["lang"] = CoreApplicationSetting.Instance.Lang;
            c.AddChild(m_head);
            c.AddChild(m_body);

            sb.AppendLine(DOCTYPE);
            sb.Append(c.RenderXML(option));
            c.ClearChilds();
            return sb.ToString();
        }

       
        public override string RenderInnerHTML(IXmlOptions option)
        {
            return string.Empty;
        }

        public static CoreXmlWebDocument CreateICoreDocument()
        {
            CoreXmlWebDocument doc = new CoreXmlWebDocument();

#if DEBUG
            string _script = PathUtils.GetPath(@"D:\wwwroot\igkdev\Lib\igk\Scripts\igk.js");
            if (File.Exists(_script))
                doc.AddScript(PathUtils.GetPath(_script)); //  @"D:\wamp\www\igkdev\Lib\igk\Scripts\igk.js"));
            else {
                CoreLog.WriteLine("Global Balafon script not found :");
                doc.AddScript(PathUtils.GetPath("%startup%/Sdk/Scripts/igk.js"));
            }
#else
            doc.AddScript(PathUtils.GetPath("%startup%/Sdk/Scripts/igk.js"));
#endif
            doc.Head.Add("meta").SetAttribute("http-equiv", "Content-Type").SetAttribute("content", "text/html; charset=utf-8");

            doc.AddLink(PathUtils.GetPath("%startup%/Sdk/Styles/icore.css"));
            doc.Body["class"] = "no-overflow";
            doc.ForWebBrowserDocument = Environment.OSVersion.Version < CoreOSVersions.Windows8;
            return doc;
        }
    }
}
