

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: HelpControlGUI.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.DrSStudio.Net.Settings;
using IGK.DrSStudio.WinUI;
using IGK.ICore;
using IGK.ICore.Web;
using IGK.ICore.IO;
using IGK.ICore.WinUI;
using IGK.ICore.Xml;
using IGK.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGK.ICore.WinCore.WinUI.Controls;

namespace IGK.DrSStudio.HelpManagerAddIn.WinUI
{
    using IGK.ICore.Web;

    public class HelpControlGUI :
        IGKXUserControl,
        IIGKWebBrowserControl
    {
        private System.Windows.Forms.WebBrowser c_web_document;
        private IGKXPanel igkxPanel1;
        private IGKXPanel c_pan_top;
        private IGKXPanel c_pan_left;
        private IGKXSplitter igkxSplitter1;
        private IGKXPanel c_pan_bottom;
        private TreeView treeView1;

        private CoreXmlWebDocument m_webDocument;

        private PhpServer php_helpServer;

        public void RefreshUrit()
        {
            this.c_web_document.Refresh(WebBrowserRefreshOption.Completely);
        }
        public HelpControlGUI()
        {
            this.Load += _Load;
            this.InitializeComponent();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.php_helpServer != null)
                {
                    this.php_helpServer.StopServer();
                    this.php_helpServer = null;
                }
            }
            base.Dispose(disposing);
        }

        private void _Load(object sender, EventArgs e)
        {

            /*
             * 
             * 
             * 
             * 
             * Attension l'uri Root doit être a la racine du site
             * 
             * */

            //int port = HelpConstant.PORT;

            ////start a new server locally
            //php_helpServer = new PhpServer();
            //php_helpServer.DocumentRoot = PathUtils.GetPath("%startup%/Help");
            //PathUtils.CreateDir(php_helpServer.DocumentRoot);
            //php_helpServer.WebContext = true;
            //php_helpServer.TargetSDKFolder = PhpServerSetting.Instance.TargetPath;
            //php_helpServer.Port = port;
            //php_helpServer.StartServer();


            //this.c_web_document.DocumentCompleted += _DocumentCompleted;
            //this.c_web_document.ProgressChanged += _ProgressChanged;
            //this.c_web_document.Navigating += _Navigating;
            //this.c_web_document.Navigated += _Navigated;

            //this.c_web_document.ObjectForScripting = new HelpScritping(this);

            //Uri uri = new Uri(Uri.UriSchemeHttp + "://localhost:" + port+"/Configs");
            //this.c_web_document.Url = uri;
            if (m_webDocument == null)
            {
                m_webDocument = CoreXmlWebDocument.CreateICoreDocument();
                CoreWebExtensions.InitDocument(m_webDocument, this.Workbench as ICoreWorkbenchDocumentInitializer);
              
                //m_webDocument.InitDocument();
            }
            this.ViewInitPage();

            this.IGKApplyBrowserDocumentText(this.m_webDocument);            
            this.c_web_document.WaitToComplete();

        }

        private void ViewInitPage()
        {
            var body = m_webDocument.Body;
            body.ClearChilds();


            var d  = body.Add("div");
            d["class"] = "igk-help-title";
            d.Content = "title";


            d = body.Add("div");
            d["class"] = "igk-help-content";
            d.Content = "content";

            d = body.Add("div");
            d["class"] = "igk-help-footer";
            d.Content = CoreConstant.COPYRIGHT;

        }

        

        private void InitializeComponent()
        {
            this.c_web_document = new System.Windows.Forms.WebBrowser();
            this.igkxPanel1 = new IGKXPanel();
            this.c_pan_top = new IGKXPanel();
            this.c_pan_left = new IGKXPanel();
            this.igkxSplitter1 = new IGKXSplitter();
            this.c_pan_bottom = new IGKXPanel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.c_pan_left.SuspendLayout();
            this.SuspendLayout();
            // 
            // c_web_document
            // 
            this.c_web_document.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_web_document.Location = new System.Drawing.Point(214, 64);
            this.c_web_document.MinimumSize = new System.Drawing.Size(20, 20);
            this.c_web_document.Name = "c_web_document";
            this.c_web_document.Size = new System.Drawing.Size(448, 353);
            this.c_web_document.TabIndex = 0;
            // 
            // igkxPanel1
            // 
            this.igkxPanel1.CaptionKey = null;
            this.igkxPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.igkxPanel1.Location = new System.Drawing.Point(0, 0);
            this.igkxPanel1.Name = "igkxPanel1";
            this.igkxPanel1.Size = new System.Drawing.Size(200, 100);
            this.igkxPanel1.TabIndex = 0;
            // 
            // c_pan_top
            // 
            this.c_pan_top.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.c_pan_top.CaptionKey = null;
            this.c_pan_top.Dock = System.Windows.Forms.DockStyle.Top;
            this.c_pan_top.Location = new System.Drawing.Point(0, 0);
            this.c_pan_top.Name = "c_pan_top";
            this.c_pan_top.Size = new System.Drawing.Size(662, 64);
            this.c_pan_top.TabIndex = 1;
            // 
            // c_pan_left
            // 
            this.c_pan_left.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.c_pan_left.CaptionKey = null;
            this.c_pan_left.Controls.Add(this.treeView1);
            this.c_pan_left.Dock = System.Windows.Forms.DockStyle.Left;
            this.c_pan_left.Location = new System.Drawing.Point(0, 64);
            this.c_pan_left.Name = "c_pan_left";
            this.c_pan_left.Size = new System.Drawing.Size(211, 353);
            this.c_pan_left.TabIndex = 2;
            // 
            // igkxSplitter1
            // 
            this.igkxSplitter1.Location = new System.Drawing.Point(211, 64);
            this.igkxSplitter1.Name = "igkxSplitter1";
            this.igkxSplitter1.Size = new System.Drawing.Size(3, 353);
            this.igkxSplitter1.TabIndex = 0;
            this.igkxSplitter1.TabStop = false;
            // 
            // c_pan_bottom
            // 
            this.c_pan_bottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.c_pan_bottom.CaptionKey = null;
            this.c_pan_bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.c_pan_bottom.Location = new System.Drawing.Point(0, 417);
            this.c_pan_bottom.Name = "c_pan_bottom";
            this.c_pan_bottom.Size = new System.Drawing.Size(662, 48);
            this.c_pan_bottom.TabIndex = 3;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(209, 351);
            this.treeView1.TabIndex = 0;
            // 
            // HelpControlGUI
            // 
            this.Controls.Add(this.c_web_document);
            this.Controls.Add(this.igkxSplitter1);
            this.Controls.Add(this.c_pan_left);
            this.Controls.Add(this.c_pan_top);
            this.Controls.Add(this.c_pan_bottom);
            this.MinimumSize = new System.Drawing.Size(662, 465);
            this.Name = "HelpControlGUI";
            this.Size = new System.Drawing.Size(662, 465);
            this.c_pan_left.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void _DocumentCompleted(object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void _ProgressChanged(object sender, System.Windows.Forms.WebBrowserProgressChangedEventArgs e)
        {

        }

        private void _Navigating(object sender, System.Windows.Forms.WebBrowserNavigatingEventArgs e)
        {

        }
        void _Navigated(object sender, System.Windows.Forms.WebBrowserNavigatedEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
        }

        public string DocumentText
        {
            get
            {
                return this.c_web_document.DocumentText;
            }
            set
            {
                this.c_web_document.DocumentText = value;
            }
        }

        public bool IsBodyDefined
        {
            get { 
                return ((this.c_web_document.Document !=null) && 
                    (this.c_web_document.Document.Body !=null));
            }
        }

        public void setBodyInnerHtml(string text)
        {
            this.c_web_document.Document.Body.InnerHtml = text;
        }


        public void InvokeScript(string p1, string[] p2)
        {
            this.c_web_document.Document.InvokeScript(p1, p2);
        }
    }
}
