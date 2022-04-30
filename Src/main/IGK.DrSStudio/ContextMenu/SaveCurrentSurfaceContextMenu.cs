

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: SaveCurrentSurfaceContextMenu.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.ContextMenu
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.WinUI;

    [DrSStudioContextMenuAttribute("File.SaveCurrentSurface", 0x100, ShortCut=enuKeys.Control | enuKeys.S )]
    sealed class SaveCurrentSurfaceContextMenu : SurfaceContainerContextMenu 
    {
        public new ICoreWorkingFilemanagerSurface CurrentSurface {
            get {
                return base.CurrentSurface as ICoreWorkingFilemanagerSurface;
            }
        }
        protected override bool IsEnabled()
        {
            return base.IsEnabled() && (this.CurrentSurface !=null);
        }
        protected override bool PerformAction()
        {

            this.CurrentSurface.Save();
            return false;
        }
    }
}
