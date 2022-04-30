

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebBrowserSurface.cs
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
file:WebBrowserSurface.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGK.ICore.WinCore.WinUI.Controls;
namespace IGK.DrSStudio.WebBrowserAddIn.WinUI
{
    [CoreSurface("WebBrowserSurface")]
    class WebBrowserSurface : IGKXWinCoreWorkingSurface, 
        ICoreWorkingSurface ,
        IWebBrowserSurface
    {
        private WebBrowser m_webBrowser;
        private XWebNavigationBar c_navbar;
        public WebBrowserSurface()
        {
            this.InitializeComponent();
            this.c_navbar.WebBrowser = this;
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.m_webBrowser = new WebBrowser();
            this.c_navbar = new XWebNavigationBar();
            this.c_navbar.Dock = DockStyle.Top;
            this.m_webBrowser.Dock = DockStyle.Fill;
            // 
            // WebBrowserSurface
            // 
            this.Controls.Add(this.m_webBrowser);
            this.Controls.Add(this.c_navbar);
            this.Name = "WebBrowserSurface";
            this.Size = new System.Drawing.Size(604, 260);
            this.ResumeLayout(false);
        }
        public void Reload()
        {
            this.m_webBrowser.Invoke ((MethodInvoker)this.m_webBrowser.Refresh);
        }
    }
}

