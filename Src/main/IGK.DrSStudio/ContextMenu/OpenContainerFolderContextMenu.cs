

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: OpenContainerFolderContextMenu.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.ContextMenu
{
    using IGK.ICore.Menu;
    using IGK.ICore.ContextMenu;
    using System.IO;
    using System.Diagnostics;
    using IGK.ICore.IO;
    [CoreContextMenuAttribute ("File.OpenContainerFolder", 0x200)]
    class OpenContainerFolderContextMenu : SurfaceContainerContextMenu  
    {
        protected override bool IsEnabled()
        {
            return this.IsVisible();
        }
        
        public new ICoreWorkingFilemanagerSurface CurrentSurface {
            get {
                return base.CurrentSurface as ICoreWorkingFilemanagerSurface;
            }
        }
        protected override bool PerformAction()
        {
            ICoreWorkingFilemanagerSurface s = this.CurrentSurface;
            if ((s!=null) && (File.Exists(s.FileName)))
            {
                Process.Start(PathUtils.GetDirectoryName(s.FileName));
                return true;
            }
            return false;
        }
    }
}
