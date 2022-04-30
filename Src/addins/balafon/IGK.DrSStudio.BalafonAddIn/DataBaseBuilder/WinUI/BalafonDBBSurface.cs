
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IGK.DrSStudio.Balafon.DataBaseBuilder.WinUI
{
    using IGK.DrSStudio.Balafon.DataBaseBuilder.XML;
    using IGK.ICore;
    using IGK.ICore.GraphicModels;
    using IGK.ICore.WinCore.WinUI.Controls;
    using IGK.ICore.WinUI;
    using IGK.ICore.Xml;
    using IGK.ICore.Web;
    using IGK.ICore.Resources;
    using System.IO;

    class BalafonDBBSurface : IGKXWinCoreWorkingSurface,
        ICoreWebScriptListener,
        ICoreWebReloadDocumentListener
    {
        private BalafonDBBSchemaDocument m_Document;
        private IGKXWebBrowserControl m_WebBrowser;

        /// <summary>
        /// used to save as a migration files
        /// </summary>
        public bool Migration { get; set; }
        

        public BalafonDBBSchemaDocument Document
        {
            get { return m_Document; }
            set
            {
                if (m_Document != value)
                {
                    m_Document = value;
                    OnDocumentChanged(EventArgs.Empty);
                }
            }
        }
       
        public event EventHandler DocumentChanged;
        private BalafonDBBScriptViewerProfile m_scriptObject;

        public CoreXmlWebDocument WebDocument
        {
            get
            {
                return this.m_scriptObject.Document;
            }
        }
        protected virtual void OnDocumentChanged(EventArgs e)
        {
            DocumentChanged?.Invoke(this, e);
        }

        /// <summary>
        /// .ctr
        /// </summary>
        public BalafonDBBSurface()
        {
            this.Paint += _Paint;
            this.m_WebBrowser = new IGKXWebBrowserControl
            {
                AllowNavigation = true,
                Dock = DockStyle.Fill
            };
            this.Controls.Add(this.m_WebBrowser);            
            this.Load += _Load;            
            this.m_WebBrowser.SetReloadDocumentListener(this);
            this.DocumentChanged += _e_DocumentChanged;

            this.CreateControl();
        }
        internal void RefreshView() {
            //this.Reload(this.m_scriptObject.Document);
            if (this.m_WebBrowser.Host is WebBrowser i)
            {

                if (i.ReadyState == WebBrowserReadyState.Loading)
                {
                    i.Stop();
                    Application.DoEvents();
                }
                i.Refresh(WebBrowserRefreshOption.Completely);
                this.ReloadDocument(this.m_scriptObject.Document, true);
            }
            else {
                var opts = new CoreXmlWebOptions() {
                    InlineData = true
                };
                
                this.m_WebBrowser.SetDocumentString(this.m_scriptObject.Document.RenderXML(opts));
            }
        }
        void _e_DocumentChanged(object sender, EventArgs e)
        {
            if (this.Document != null)
            {
                this.m_scriptObject = new BalafonDBBScriptViewerProfile(this.Document, this)
                {
                    Document = CoreXmlWebDocument.CreateICoreDocument()
                };
                this.m_scriptObject.Document.InitDocument();
           

                this.m_WebBrowser.ObjectForScripting = this.m_scriptObject;
                this.m_scriptObject.SetScriptListener(this);
                this.m_scriptObject.SetReloadListener(this);

                this.View(this.m_scriptObject.Document);
                

            }
            else {
                this.m_WebBrowser.ObjectForScripting = null;
                this.m_scriptObject = null;
            }
        }

        private void _Load(object sender, EventArgs e)
        {
            if (this.m_scriptObject != null)
            {
                this.m_scriptObject.Document.Body.Clear();
                this.View(this.m_scriptObject.Document);
            }
        }

        void _Paint(object sender, CorePaintEventArgs e)
        {
            RenderChemaView(e.Graphics);
        }

        private void RenderChemaView(ICoreGraphics coreGraphics)
        {
            coreGraphics.Clear(Colorf.FromFloat(0.8f));
        }
        //public void Reload()
        //{
        //    if (this.m_WebBrowser.IsDisposed)
        //        return;
        //    this.m_scriptObject.Document.Body.Clear();
        //    this.View(this.m_scriptObject.Document);
            
        //}

        private void View(CoreXmlWebDocument coreXmlWebDocument)
        {
            string s = null;

#if DEBUG
            string file = CoreConstant.DRS_SRC+ @"\addins\web\IGK.DrSStudio.BalafonAddIn\DataBaseBuilder\Resources\balafon_primary.html";
            if (File.Exists(file))
                s = File.ReadAllText(file);
            else 
                s = CoreResources.GetResourceString(
                  string.Format(BalafonDBBConstant.RES_DATA_1, BalafonDBBConstant.MAIN_RESFILE),
                   GetType().Assembly);
#else
         s =    CoreResources.GetResourceString(
                  string.Format(BalafonDBBConstant.RES_DATA_1, BalafonDBBConstant.MAIN_RESFILE),
                   GetType().Assembly);
#endif
            //coreXmlWebDocument.Head.add ('title')
            if (s == null)
            {
                coreXmlWebDocument.Body.addDiv().Content = "ResFileNotFound";
            }
            else
            {

                coreXmlWebDocument.Body.LoadString(
                    CoreWebUtils.EvalWebStringExpression(s,
                    this.m_scriptObject));
            }
        }

        public object  InvokeScript(string script)
        {
               return  this.m_WebBrowser.InvokeScript ("eval", script);//.WebControl.Document.InvokeScript(script);
         
        }

        public void ReloadDocument(CoreXmlWebDocument document, bool attach)
        {
            document.Body.Clear ();
            document.Body.AppendScriptSource(
               CoreResources.GetResourceString("databasebuilder/resources/balafon.js", BalafonDBBConstant.BLFADDIN_FOLDER, GetType().Assembly )
               );
           this.View(document);
           if (attach) {
               this.m_WebBrowser.AttachDocument(document);
           }
        }

        public BalafonDBBSchemaDataDefinition CurrentTable
        {
            get {
            return this.m_scriptObject.DocumentSchema.GetTableDefinition (this.m_scriptObject.TableName );
        } }

        internal void SaveAs(string fileName)
        {
            try
            {

                if ((this.Document != null) && !string.IsNullOrEmpty(fileName))
                {
                    CoreXmlElement migrate = null;

                    int i = Math.Max(1, ++this.Document.Version);
                    this.Document["date"] = DateTime.Now.ToString("yyyy-MM-dd");
                    this.Document["app"] = this.CompanyName;
                    this.Document["author"] = "C.A.D BONDJE DOUE";
                    this.Document["version"] = i;
                    this.Document["migration"] = this.Migration;
                    if (this.Migration){

                        migrate = this.Document.add("Migrations");

                    }

                    File.WriteAllText(fileName, this.Document.RenderXML(null));
                    migrate?.Parent.Remove(migrate);
                    this.Document.Version++;

                }
            }
            catch (Exception ex) {
                MessageBox.Show("Failed to store file {0}".R(ex.Message), "Error".R(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
