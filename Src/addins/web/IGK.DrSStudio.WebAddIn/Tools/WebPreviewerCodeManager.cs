

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebPreviewerCodeManager.cs
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
file:WebPreviewerCodeManager.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WebAddIn.Tools
{
    using IGK.ICore.Tools;
    using IGK.DrSStudio.WebAddIn.WinUI;
    [CoreTools("Tool.HTMLCodePreviewer", Description="html code previewer control")]
    class WebPreviewerCodeManager : WebToolBase
    {
        private static WebPreviewerCodeManager sm_instance;
        XWebCodePreviewer m_previewer;
        /// <summary>
        /// get the code previewer
        /// </summary>
        public XWebCodePreviewer HtmlPreviewSurface {
            get {
                return this.m_previewer;
            }
        }
        private WebPreviewerCodeManager()
        {
        }
        public static WebPreviewerCodeManager Instance
        {
            get
            {
                return sm_instance;
            }
        }
        internal void InitSurface(string title)
        {
            if (this.m_previewer == null)
            {
                this.m_previewer = new XWebCodePreviewer();
                this.Workbench.AddSurface(this.m_previewer,true );
            }
            else{
                this.Workbench.SetCurrentSurface( this.m_previewer);
            }
            this.m_previewer.SetTitle(title);
        }
        static WebPreviewerCodeManager()
        {
            sm_instance = new WebPreviewerCodeManager();
        }
        protected override void RegisterBenchEvent(ICoreSystemWorkbench workbench)
        {
            base.RegisterBenchEvent(workbench);
            if (workbench is ICoreSurfaceManagerWorkbench fb)
                fb.SurfaceRemoved += workbench_SurfaceRemoved;

        }
        protected override void UnregisterBenchEvent(ICoreSystemWorkbench workbench)
        {
            if (workbench is ICoreSurfaceManagerWorkbench fb)
                fb.SurfaceRemoved -= workbench_SurfaceRemoved;
            base.UnregisterBenchEvent(Workbench);
        }

        void workbench_SurfaceRemoved(object sender, CoreItemEventArgs<ICoreWorkingSurface> e)
        {            
            if (e.Item == this.m_previewer)
            {
                this.m_previewer = null;
            }
        }
    }
}

