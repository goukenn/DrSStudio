
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace IGK.DrSStudio.WinUI
{
    using IGK.ICore.Xml;
    using IGK.ICore.Web;
    using IGK.ICore.WinCore.Web;
    using IGK.ICore.WinCore.WinUI.Controls;
    using IGK.ICore.Resources;
    using IGK.ICore;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinCore.WinUI;
    using IGK.ICore.IO;

    public class DrSstudioNotifyBoxForm : XFormDialog,
        ICoreDialogForm,
        ICoreWebReloadDocumentListener,
        ICoreWebScriptListener        
    {
        private IGKXWebBrowserControl c_webBrowser1;
        private readonly CoreXmlWebDocument m_document;
        const int WS_EX_TOOLWINDOW = 0x00000080;
        private readonly DrSStudioNotifyBoxScriptProfile m_script;

        protected override CreateParams CreateParams
        {
            get
            {
                var p = base.CreateParams;
                p.ExStyle |= WS_EX_TOOLWINDOW;                
                return p;
            }
        }
        public DrSstudioNotifyBoxForm(){
            this.InitializeComponent();

            this.c_webBrowser1.DocumentComplete += _DocumentCompleted;
            this.m_document = CoreXmlWebDocument.CreateICoreDocument();
            this.m_document.InitDocument();
            this.m_document.AddLink(PathUtils.GetPath("%startup%/sdk/Styles/drs.css"));

            this.Bounds = new System.Drawing.Rectangle(0, 0, 100, 100);
            this.m_script = new DrSStudioNotifyBoxScriptProfile
            {
                Dialog = this
            };
            this.m_script.SetReloadListener(this);
            this.m_script.SetScriptListener(this);

            this.c_webBrowser1.ObjectForScripting = this.m_script;

       

            this.m_document.ForWebBrowserDocument = true;
            this.FormClosed += _FormClosed;
            this.CreateControl();
        }
     

        private void InitializeComponent()
        {
            this.c_webBrowser1 = new IGKXWebBrowserControl();
            this.SuspendLayout();
            // 
            // c_webBrowser1
            // 
            this.c_webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.c_webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.c_webBrowser1.Name = "c_webBrowser1";
            this.c_webBrowser1.TabIndex = 0;
            // 
            // DrSstudioNotifyBoxForm
            // 
            this.ClientSize = new System.Drawing.Size(628, 261);
            this.Controls.Add(this.c_webBrowser1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DrSstudioNotifyBoxForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.Load +=this._Load;
            this.Shown += this._Shown;
            this.FormClosed += _FormClosed;
            this.ResumeLayout(false);

        }

        void _FormClosed(object sender, FormClosedEventArgs e)
        {
            this.c_webBrowser1.SetReloadDocumentListener(null);
        }

        private void _Shown(object sender, EventArgs e)
        {
            
        }

        private void setupBound(Vector2i V)
        {
            var f = this.ParentForm ?? CoreSystem.Instance.MainForm as Form ;

            var g = Screen.FromRectangle(f.Bounds);

            var r = g.WorkingArea;
            var m = Math.Min(r.Height, V.Y);
            this.Bounds = new System.Drawing.Rectangle(r.X, (r.Height - m) / 2,
                r.Width, m);   
        }

        private void _Load(object sender, EventArgs e)
        {
            this._initView();
            this.c_webBrowser1.AttachDocument(m_document);
            //this.c_webBrowser1.CreateControl();
            //this.c_webBrowser1.WebControl.DocumentText = "Sample";//DocumentComplete 
            //WebBrowser wb = new WebBrowser();
            //wb.DocumentText = "Sample";
            //this.Controls.Clear();
            //wb.Dock = DockStyle.Fill;
            //this.Controls.Add(wb);
            //this.Location = new System.Drawing.Point (0,0);// PointToClient.E
           // this._DocumentCompleted(this, e);
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private void _initView()
        {
            this.m_document.Body.ClearChilds();
            var s = CoreResources.GetResourceString("resources/notify_box.js");
            if (string.IsNullOrEmpty (s) == false )
            this.m_document.Body.AppendScriptSource(s         );
            this.m_document.Body.setClass("igk-drs-notifybox");
            var b = this.m_document.Body.addDiv("notifybox")
             .setAttribute("class", "igk-notify-box");
            b.addDiv().setClass("igk-title-4").Content = Title;
            b.addDiv().setClass("igk-panel").LoadString(this.Message);
        }
      
        void _DocumentCompleted(object sender, EventArgs e)
        {
            object size =   this.c_webBrowser1.InvokeScript("getBoxSize();");
            if (size != null)
            {
                var V = Vector2i.ConvertFromString(size.ToString());
                this.setupBound(V);
            }
        }

        public string Message { get; set; }



        public void ReloadDocument(CoreXmlWebDocument document, bool attach)
        {
            _initView();
            if (attach) {
                this.c_webBrowser1.AttachDocument(this.m_document);
            }
        }


        public void Reload()
        {
            this.ReloadDocument(this.m_document, true);            
        }

        public object InvokeScript(string script)
        {
            return this.c_webBrowser1.InvokeScript(script);
        }
    }

    
}
