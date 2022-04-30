using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Xml
{
    public class CoreXmlWebBodyElement : CoreXmlWebElement
    {
        private CoreXmlElement m_bodyScript;
        private XmlWebBodyScriptManager m_scriptManager;
        private CoreXmlWebDocument coreXmlWebDocument;
        public CoreXmlElement BodyScript
        {
            get
            {
                return m_bodyScript;
            }
        }
        public  CoreXmlWebBodyElement()
            : base("body")
        {
            m_scriptManager = new XmlWebBodyScriptManager(this);
            CoreXmlElement r = CoreXmlElement.CreateXmlNode("script");
            r["type"] = "text/javascript";
            r["language"] = "javascript";
            r.Content = "(function(){if (window.igk)window.igk.init_document(); })();";
            this.m_bodyScript = r;
        }

        public CoreXmlWebBodyElement(CoreXmlWebDocument coreXmlWebDocument)
            : this()
        {
            this.coreXmlWebDocument = coreXmlWebDocument;
        }
        public override string RenderXML(IXmlOptions option)
        {
            this.AddChild(this.m_scriptManager);
            this.AddChild(this.m_bodyScript);
            string o = base.RenderXML(option);
            this.RemoveChild(this.m_bodyScript);
            this.RemoveChild(this.m_scriptManager);
            return o;
        }
        public override string RenderInnerHTML(IXmlOptions option)
        {
            if (option == null)
            {
                option = CreateXmlOptions();
            }
            StringBuilder sb = new StringBuilder(base.RenderInnerHTML(option));

            //sb.Append(this.m_scriptManager.Render(option));
            //sb.Append(this.m_bodyScript.Render(option));
            return sb.ToString();
        }
        /// <summary>
        /// append script
        /// </summary>
        /// <param name="scriptfile">script file name</param>
        public void AppendScript(string scriptfile)
        {
            this.m_scriptManager.AddScript(scriptfile);
        }
        public void AppendScriptSource(string script) {
            if (string.IsNullOrEmpty(script))
                return;
            this.m_scriptManager.AddScriptSource ( script);
        }
        public void AppendScriptSource(string key, string script)
        {
            if (string.IsNullOrEmpty(script))
                return;
            this.m_scriptManager.AddScriptSource(key, script);
        }
        public override void Clear()
        {
            base.Clear();
            this.m_scriptManager.RemoveScriptSource();
        }
        public class XmlWebBodyScriptManager : CoreXmlElement
        {
            private Dictionary<string, CoreXmlElement> m_scripts;
            private CoreXmlWebBodyElement m_body;
            private int m_sources;
            public XmlWebBodyScriptManager(CoreXmlWebBodyElement body)
                : base()
            {
                this.m_body = body;
                this.m_scripts = new Dictionary<string, CoreXmlElement>();
            }
            internal  CoreXmlElement  AddScriptSource(string script ){
                return AddScriptSource("__sourceScript_" + m_sources, script );
            }

            public CoreXmlElement AddScriptSource(string key, string script) {
                var c = this.Add("script");
                c.Content = script;
                if (m_scripts.ContainsKey(key))
                {
                    this.Remove (m_scripts[key]);                    
                    m_scripts[key] = c;
                }
                else
                    m_scripts.Add(key, c);
                m_sources++;
                return c;
            }
            public override string RenderXML(IXmlOptions option)
            {
                if (m_scripts.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var item in m_scripts)
                    {
                        sb.Append(item.Value.RenderXML(option));
                    }
                    return sb.ToString();
                }
                return string.Empty;
            }

            internal CoreXmlElement AddScript(string scriptFile)
            {

                if (string.IsNullOrEmpty(scriptFile))
                    return null;

                if (this.m_scripts.ContainsKey(scriptFile))
                    return this.m_scripts[scriptFile];

                var lnk = CoreXmlElement.CreateXmlNode("script") as CoreXmlWebScriptElement;
                this.m_scripts.Add(scriptFile, lnk);
                lnk["src"] = new CoreXmlUriLink(scriptFile);
                lnk["type"] = "text/javascript";
                lnk["language"] = "javascript";
                lnk.Document = this.m_body.coreXmlWebDocument;
                return lnk;
            }
            public bool RemoveScript(string file)
            {
                if (this.m_scripts.ContainsKey(file))
                {
                    CoreXmlWebScriptElement h = this.m_scripts[file] as CoreXmlWebScriptElement;
                    if (h != null)
                        h.Document = null;
                    this.m_scripts.Remove(file);
                    return true;
                }
                return false;
            }

            internal void RemoveScriptSource()
            {
                List<string> k = new List<string> (m_scripts.Keys);
                foreach (var item in k)
                {
                    if (item.StartsWith("__sourceScript_"))
                    {
                        this.m_scripts.Remove(item);
                    }
                }
            }
        }
    }
}
