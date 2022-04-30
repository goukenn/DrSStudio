

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CloseAllButCurrentSurfaceContextMenu.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.ContextMenu;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.ContextMenu
{
    [DrSStudioContextMenuAttribute("File.CloseAllButCurrentSurface", 0x205)]
    class CloseAllButCurrentSurfaceContextMenu : SurfaceContainerContextMenu 
    {
        public new ICoreSurfaceManagerWorkbench Workbench {
            get {
                return base.Workbench as ICoreSurfaceManagerWorkbench;
            }
        }
        protected override bool PerformAction()
        {
            var c = this.CurrentSurface;
            var fb = this.Workbench;
              if (fb != null)
              {
                  var tab = fb.Surfaces.ToArray();
                  foreach (ICoreWorkingSurface item in tab)
                  {
                      if (item == c)
                          continue;
                      fb.Surfaces.Remove(item);
                  }
              }
            return false;
        }
    }
}
