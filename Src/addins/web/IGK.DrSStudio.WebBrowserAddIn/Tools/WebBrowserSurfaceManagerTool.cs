

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebBrowserSurfaceManager.cs
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
file:WebBrowserSurfaceManager.cs
*/
using IGK.DrSStudio.WebBrowserAddIn.WinUI;
using IGK.ICore.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.WebBrowserAddIn.Tools
{
    /// <summary>
    /// represent a tool manager base
    /// </summary>
    [CoreTools("Tool.WebBrowserSurfaceManager")]
    class WebBrowserSurfaceManagerTool : CoreToolBase
    {
        private static WebBrowserSurfaceManagerTool sm_instance;
        private WebKitBrowserSurface m_surface;
        public static WebBrowserSurfaceManagerTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static WebBrowserSurfaceManagerTool()
        {
            sm_instance = new WebBrowserSurfaceManagerTool();
        }
        private WebBrowserSurfaceManagerTool()
        {
        }
        protected override void RegisterBenchEvent(ICoreSystemWorkbench workbench)
        {
            base.RegisterBenchEvent(workbench);
        }
        protected override void UnregisterBenchEvent(ICoreSystemWorkbench workbench)
        {
            base.UnregisterBenchEvent(workbench);
        }
        public WebKitBrowserSurface GetSurface()
        {
            if( (this.m_surface == null)||(this.m_surface.IsDisposed ))
            {
                this.m_surface = new WebKitBrowserSurface();
                this.m_surface.Workbench = this.Workbench;
                this.m_surface.Disposed += m_surface_Disposed;
            }
            return this.m_surface;
        }
        void m_surface_Disposed(object sender, EventArgs e)
        {
            this.m_surface = null;
        }
        protected override void GenerateHostedControl()
        {
            this.HostedControl = null;
        }
    }
}

