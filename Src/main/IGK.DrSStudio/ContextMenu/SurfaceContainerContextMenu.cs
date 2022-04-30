

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: SurfaceContainerContextMenu.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.WinUI;
using IGK.ICore.ContextMenu;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.ContextMenu
{
    public abstract class SurfaceContainerContextMenu : CoreApplicationContextMenu
    {
        protected override bool IsVisible()
        {
            WinCoreLayoutManagerBase l = Workbench.GetLayoutManager() as WinCoreLayoutManagerBase;
            return (this.CurrentSurface != null) && this.OwnerContext.SourceControl == l.SurfaceContainer;
        }
    }
}
