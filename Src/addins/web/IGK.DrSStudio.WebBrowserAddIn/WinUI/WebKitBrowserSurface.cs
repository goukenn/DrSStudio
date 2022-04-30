

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebKitBrowserSurface.cs
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
file:WebKitBrowserSurface.cs
*/
using IGK.DrSStudio.WebBrowserAddIn.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebKit;
using IGK.ICore.IO;
using IGK.ICore.WinCore.WinUI.Controls;
namespace IGK.DrSStudio.WebBrowserAddIn.WinUI
{
    [CoreSurface("WebKitBrowserSurface", EnvironmentName = WebBrowserConstant.SURFACE_ENVIRONMENT)]
    /// <summary>
    /// represent a working surface. browser surface
    /// </summary>
    class WebKitBrowserSurface : IGKXWinCoreWorkingSurface, ICoreWorkingSurface,
        IWebBrowserSurface
    {
       // WebBrowser c_browser;
        private ICoreSystemWorkbench m_wbench;
        private WebKitBrowser c_browser;
        private XWebNavigationBar c_navbar;


        static WebKitBrowserSurface()
        {
           bool l =  Native.Kernel32.AddDllDirectory (Path.GetFullPath ( PathUtils.GetDirectoryName (System.Reflection.Assembly.GetExecutingAssembly ().Location )  + "/WebKit"));
            AppDomain.CurrentDomain.AssemblyResolve += (object obj, ResolveEventArgs  args) =>
            {
                string dir = PathUtils.GetDirectoryName (System.Reflection.Assembly.GetExecutingAssembly ().Location )  + "/WebKit";
                string f = dir + "/" + args.Name.Split(',')[0] + ".dll";
                if (System.IO.File.Exists(f))
                {
                    return System.Reflection.Assembly.LoadFile(f);
                }
                return null;
            };
        }
       // GeckoWebBrowser c_browser;
        public WebKitBrowserSurface()
        {
            this.InitializeComponent();
            this.c_navbar.WebBrowser = this;
        }
        private void InitializeComponent()
        {
            this.c_browser = new WebKit.WebKitBrowser();
            this.c_navbar = new IGK.DrSStudio.WebBrowserAddIn.WinUI.XWebNavigationBar();
            this.SuspendLayout();
            // 
            // c_browser
            // 
            this.c_browser.BackColor = System.Drawing.Color.White;
            this.c_browser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_browser.Location = new System.Drawing.Point(0, 24);
            this.c_browser.Name = "c_browser";
            this.c_browser.Size = new System.Drawing.Size(553, 241);
            this.c_browser.TabIndex = 0;
            this.c_browser.Url = null;
            // 
            // c_navbar
            // 
            this.c_navbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.c_navbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.c_navbar.Location = new System.Drawing.Point(0, 0);
            this.c_navbar.Margin = new System.Windows.Forms.Padding(0);
            this.c_navbar.Name = "c_navbar";
            this.c_navbar.TabIndex = 1;
            // 
            // WebKitBrowserSurface
            // 
            this.Controls.Add(this.c_browser);
            this.Controls.Add(this.c_navbar);
            this.Name = "WebKitBrowserSurface";
            this.Size = new System.Drawing.Size(553, 265);
            this.ResumeLayout(false);
        }
        public bool OpenFile(string filename)
        {
            if (File.Exists(filename))
            {
                string file = "file://" + filename;
                c_browser.Url = new Uri(file);
                //c_browser.Navigate (file);
                WebBrowserFileChangeTool.Instance.RegisterChanged(this, filename);
                return true;
            }
            return false;
        }
        /// <summary>
        /// open uri
        /// </summary>
        /// <param name="uri"></param>
        public void OpenUri(string uri)
        {
            try
            {
                if (Uri.IsWellFormedUriString(uri, UriKind.Absolute ))
                {
                    c_browser.Url = new Uri(uri);
                    WebBrowserFileChangeTool.Instance.Unregister();
                }
            }
            catch {
                CoreLog.WriteLine("Not A Valid Uri");
            }
        }
        /// <summary>
        /// reload the current document
        /// </summary>
        public void Reload()
        {
           //this.c_browser.Reload();
            this.c_browser.Invoke((MethodInvoker)this.c_browser.Reload );
            this.Title = this.c_browser.DocumentTitle;
        }
  
        public new ICoreSystemWorkbench Workbench
        {
            get { return this.m_wbench; }
            internal set {
                this.m_wbench = value;
            }
        }
    }
}

