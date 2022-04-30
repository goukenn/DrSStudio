
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.CssPropertiesBuilder.WinUI
{

    using IGK.ICore;
    using IGK.ICore.Web;
    using IGK.ICore.WinCore.WinUI.Controls;
    using IGK.ICore.WinUI;
    using IGK.ICore.Xml;
    using IGK.ICore.WinCore.Web;
    using IGK.ICore.IO;
    using IGK.ICore.Resources;
    using System.IO;
    using IGK.DrSStudio;

    [CoreSurface("{21F7BBCE-52FF-4B5B-BEF2-0DB6CE1CB507}")]
    public class CssEditorSurface :
        IGKXWinCoreWorkingSurface,
        ICoreWorkingSurface,
         ICoreWebReloadDocumentListener,
        ICoreWebScriptListener
        
    {
        private IGKXWebBrowserControl c_webBrowser;
        private CoreXmlWebDocument m_document;
        private CSSBuilderScriptViewerProfile m_script;
        private string m_View;
        public string FileName { get; set; }
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
        protected virtual void OnViewChanged(EventArgs e)
        {
            ViewChanged?.Invoke(this, e);
        }

        public event EventHandler ViewChanged;
        public WebBrowser WebControl {
            get {
                return c_webBrowser.Host as WebBrowser;
            }
        }
        public CssEditorSurface()
        {
            this.InitializeComponent();
            this.InitSurface();    
        }

        private void InitSurface()
        {
            this.m_View = "css_builder_main.html";
            this.Load += _Load;
            this.ViewChanged += _ViewChanged;
        }

        private void _Load(object sender, EventArgs e)
        {
            string f = this.FileName;
            if (string.IsNullOrEmpty(f))
                this.Title = "Css Builder".R();
            else 
                this.Title = "title.balafoncss.builder".R(Path.GetFileName(f));
            InitDocument();
        }

        private void _ViewChanged(object sender, EventArgs e)
        {
            this.ReloadDocument(this.m_document, false );
        }

        private void InitDocument()
        {
            m_document = CoreXmlWebDocument.CreateICoreDocument();
            m_document.InitDocument();
            

            var d = new CSSBuilderScriptViewerProfile();
            //d.SetReloadListener(this);
            d.SetScriptListener(this);
            this.m_script = d;
            this.c_webBrowser.ObjectForScripting = d;
            this.c_webBrowser.SetReloadDocumentListener (this);
            d.Document = m_document;


            ReloadDocument(m_document, true );
            this.c_webBrowser.SetReloadDocumentListener(this);

        }

        public object InvokeScript(string script)
        {
          
                return this.c_webBrowser.InvokeScript("eval", new string[] { script });
           
        }

        public void ReloadDocument(CoreXmlWebDocument document, bool attach)
        {
            document.Body.Clear();

#if DEBUG

            string c = "";
            string file = string.Format(CoreConstant.DRS_SRC+@"\addins\web\IGK.CssPropertiesBuilder\Resources\{0}", this.View);
            if (File.Exists(file))
                c = System.IO.File.ReadAllText(file);
            else
                c = CoreResources.GetResourceString(string.Format(CSSBuilderConstant.RES_FORMAT_1, this.View));
                  

            document.Body.LoadString(
                CoreWebUtils.EvalWebStringExpression(
                c, this.m_script)
            );
#else 
               document.Body.LoadString(
                CoreWebUtils.EvalWebStringExpression(
                CoreResources.GetResourceString (string.Format (CSSBuilderConstant.RES_FORMAT_1 , this.View)),
                this.m_script)
            );
#endif
            if (attach && this.c_webBrowser.Host is WebBrowser i)
            document.AttachToWebbrowser(i, true);

#if DEBUG            
            IGK.ICore.IO.CoreFileUtils.WriteTo(CoreConstant.DebugTempFolder+@"\cssbuilder.doc.html", document.RenderXML(null));
#endif
        }
        public void Reload()
        {
            this.ReloadDocument(this.m_document, false );
        }

        private void InitializeComponent()
        {
            this.c_webBrowser = new IGK.ICore.WinCore.WinUI.Controls.IGKXWebBrowserControl();
            this.SuspendLayout();
            // 
            // c_webBrowser
            // 
            this.c_webBrowser.CaptionKey = null;
            this.c_webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_webBrowser.Location = new System.Drawing.Point(0, 0);
            this.c_webBrowser.Name = "c_webBrowser";
            this.c_webBrowser.Size = new System.Drawing.Size(510, 253);
            this.c_webBrowser.TabIndex = 0;
            // 
            // CssEditorSurface
            // 
            this.Controls.Add(this.c_webBrowser);
            this.Name = "CssEditorSurface";
            this.Size = new System.Drawing.Size(510, 253);
            this.ResumeLayout(false);

        }
    }
}
