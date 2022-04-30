
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.CssPropertiesBuilder.WinUI
{
    using IGK.CssPropertiesBuilder.WinUI;
    using IGK.ICore;
    using IGK.ICore.IO;
    using IGK.ICore.Resources;
    using IGK.ICore.Web;
    using IGK.ICore.WinCore.Web;
    using IGK.ICore.WinCore.WinUI.Controls;
    using IGK.ICore.Xml;
    using System.IO;

    public partial class MainForm : Form,
        ICoreWebReloadDocumentListener,
        ICoreWebScriptListener
        
    {
        private CoreXmlWebDocument m_document;
        private CSSBuilderScriptViewerProfile m_script;
        private string m_View;
        /// <summary>
        /// get or set the view
        /// </summary>
        public string View
        {
            get { return m_View; }
            set
            {
                if (m_View != value)
                {
                    m_View = value;
                    OnViewChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler ViewChanged;

        protected virtual void OnViewChanged(EventArgs e)
        {            
           ViewChanged?.Invoke(this, e);         
        }


        public MainForm()
        {
            InitializeComponent();
            this.m_View = "css_builder_main.html";
            this.Load += _Load;
            this.ViewChanged += _ViewChanged;
        }

        void _ViewChanged(object sender, EventArgs e)
        {
            this.ReloadDocument(this.m_document,false );
        }

        private void _Load(object sender, EventArgs e)
        {
            InitDocument();
        }

        private void InitDocument()
        {
            m_document = CoreXmlWebDocument.CreateICoreDocument();
            m_document.AddScript(PathUtils.GetPath("%startup%/Sdk/Scripts/drs.js"));
            m_document.AddLink(PathUtils.GetPath("%startup%/Sdk/bootstrap/css/bootstrap.min.css"));

            var d = new CSSBuilderScriptViewerProfile();
            d.SetReloadListener(this);
            d.SetScriptListener(this);
            d.Document = m_document;

            this.m_script = d;
            this.c_webBrowser.ObjectForScripting = d;


            ReloadDocument(m_document, false);
            this.c_webBrowser.AttachDocument(m_document);

            //ReloadDocument(m_document,true );
          //  this.c_webBrowser.AttachDocument(m_document);

            this.c_webBrowser.SetReloadDocumentListener(this);

        }

        public object  InvokeScript(string script)
        {
            return this.c_webBrowser.InvokeScript(
                "eval", new string[] { script }
            );
        }

        public void ReloadDocument(CoreXmlWebDocument document, bool attach)
        {
            document.Body.Clear();

            string c = "";
#if DEBUG

            string file = string.Format(CoreConstant.DRS_SRC + @"\addins\web\IGK.CssPropertiesBuilder\Resources\{0}", this.View);
            if (File.Exists(file))
                c = CoreFileUtils.ReadAllFile(file);
            else
            {
                c = CoreResources.GetResourceString(string.Format(CSSBuilderConstant.RES_FORMAT_1, this.View));
                
            }
            document.Body.LoadString(
                CoreWebUtils.EvalWebStringExpression(c,
                this.m_script)
            );
#else 
               document.Body.LoadString(
                CoreWebUtils.EvalWebStringExpression(
                CoreResources.GetResourceString (string.Format (CSSBuilderConstant.RES_FORMAT_1 , this.View)),
                this.m_script)
            );
#endif
            if (attach  && this.c_webBrowser.Host is WebBrowser i)
            document.AttachToWebbrowser(i, true);

#if DEBUG
            File.WriteAllText(CoreConstant.DebugTempFolder +@"\cssbuilder.doc.html", document.RenderXML(null));
#endif


        }



        public void Reload()
        {
            this.ReloadDocument(this.m_document, true );
        }
    }
}
