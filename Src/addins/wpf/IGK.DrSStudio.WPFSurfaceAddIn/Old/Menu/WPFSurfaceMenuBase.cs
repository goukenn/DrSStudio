

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WPFSurfaceMenuBase.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:WPFSurfaceMenuBase.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WPFSurfaceAddIn.Menu
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.WPFSurfaceAddIn.WinUI;
    /// <summary>
    /// represent a surface menu base
    /// </summary>
    public abstract class WPFSurfaceMenuBase : WPFMenuBase 
    {
        protected override void RegisterBenchEvent(IGK.DrSStudio.WinUI.ICoreWorkbench workbench)
        {
            base.RegisterBenchEvent(workbench);
            workbench.CurrentSurfaceChanged += new CoreWorkingSurfaceChangedEventHandler(workbench_CurrentSurfaceChanged);
        }
        protected override void UnregisterBenchEvent(IGK.DrSStudio.WinUI.ICoreWorkbench workbench)
        {
            workbench.CurrentSurfaceChanged -= new CoreWorkingSurfaceChangedEventHandler(workbench_CurrentSurfaceChanged);
            base.UnregisterBenchEvent(workbench);
        }
        void workbench_CurrentSurfaceChanged(object o, CoreWorkingSurfaceChangedEventArgs e)
        {
            WPFHostSurface v_s = (e.OldSurface as WPFHostSurface);
            if (v_s != null) UnRegisterSurfaceEvent(v_s);
            v_s = (e.NewSurface as WPFHostSurface);
            if (v_s != null) RegisterSurfaceEvent(v_s);
        }
        protected virtual void UnRegisterSurfaceEvent(WPFHostSurface surface)
        {
        }
        protected virtual void RegisterSurfaceEvent(WPFHostSurface surface)
        {
        }
    }
}

