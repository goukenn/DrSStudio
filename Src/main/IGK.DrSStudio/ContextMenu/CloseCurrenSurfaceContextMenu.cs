

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CloseCurrenSurfaceContextMenu.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.ContextMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinUI;

namespace IGK.DrSStudio.ContextMenu
{
    [DrSStudioContextMenuAttribute("File.CloseCurrentSurface", 0x201)]
    sealed class CloseCurrenSurfaceContextMenu : SurfaceContainerContextMenu 
    {
        public new ICoreSurfaceManagerWorkbench Workbench
        {
            get
            {
                return base.Workbench as ICoreSurfaceManagerWorkbench;
            }
        }
        protected override bool IsEnabled()
        {
            return base.IsEnabled() && (this.Workbench !=null) && (this.CurrentSurface !=null);
        }
        protected override bool PerformAction()
        {
            var c = this.CurrentSurface;
            var fb = this.Workbench;
            if ((c!=null ) && (fb!=null)){
                fb.Surfaces.Remove(c);
            }
            return false;
        }
    }
}
