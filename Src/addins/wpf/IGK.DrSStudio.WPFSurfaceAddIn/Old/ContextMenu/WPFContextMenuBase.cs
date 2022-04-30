

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WPFContextMenuBase.cs
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
file:WPFContextMenuBase.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WPFSurfaceAddIn.ContextMenu
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.ContextMenu;
    using IGK.DrSStudio.WPFSurfaceAddIn.WinUI;
    public abstract class WPFContextMenuBase : CoreContextMenuBase 
    {
        public WPFContextMenuBase()
        {
            this.IsRootMenu = true ;
        }
        protected override void RegisterBenchEvent(IGK.DrSStudio.WinUI.ICoreWorkbench workbench)
        {
            base.RegisterBenchEvent(workbench);
        }
        protected override void UnregisterBenchEvent(IGK.DrSStudio.WinUI.ICoreWorkbench workbench)
        {
            base.UnregisterBenchEvent(workbench);
        }
        public new WPFHostSurface CurrentSurface
        {
            get { return base.CurrentSurface as WPFHostSurface; }
        }
        protected override bool IsEnabled()
        {
            return (this.CurrentSurface != null);
        }
        protected override bool IsVisible()
        {
            return (this.CurrentSurface != null);
        }
    }
}

