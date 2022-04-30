

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _SaveAllSufaces.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:_SaveAllSufaces.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Menu.File
{
    using IGK.ICore.WinCore;
using IGK.ICore;
    using IGK.ICore.Menu;
    using IGK.ICore.WinUI;
        [DrSStudioMenu("File.SaveAll", CoreConstant.SAVE_MENU_INDEX +2,
        Shortcut = enuKeys.Control | enuKeys.Shift  | enuKeys.S,        
        ImageKey = "Menu_SaveAll")]
    class _SaveAllSufaces : CoreApplicationMenu 
    {
            protected override bool PerformAction()
            {
                  var fb = Workbench as ICoreSurfaceManagerWorkbench;
                  if (fb != null)
                  {
                      foreach (ICoreWorkingSurface item in fb.Surfaces)
                      {
                          if ((item == null) || !(item is ICoreWorkingRecordableSurface)) continue;
                          (item as ICoreWorkingRecordableSurface).Save();
                      }
                      return true;
                  }
                return base.PerformAction();
            }
            protected override bool IsEnabled()
            {
                  var fb = this.Workbench as ICoreSurfaceManagerWorkbench;
                  if (fb != null)
                  {
                      return (fb.Surfaces.Count > 0);
                  }
                  return false;
            }
            protected override void RegisterBenchEvent(ICoreSystemWorkbench workbench)
            {
                base.RegisterBenchEvent(workbench);
                   var fb = workbench as ICoreSurfaceManagerWorkbench;
                   if (fb != null)
                   {
                       fb.SurfaceAdded += workbench_SurfaceAdded;
                       fb.SurfaceRemoved += workbench_SurfaceRemoved;
                   }
            }
            protected override void UnregisterBenchEvent(ICoreSystemWorkbench workbench)
            {
                  var fb = workbench as ICoreSurfaceManagerWorkbench;
                  if (fb != null)
                  {
                      fb.SurfaceAdded -= workbench_SurfaceAdded;
                      fb.SurfaceRemoved -= workbench_SurfaceRemoved;
                  }
                base.UnregisterBenchEvent(workbench);
            }
            void workbench_SurfaceRemoved(object o, CoreItemEventArgs<ICoreWorkingSurface> e)
            {
                this.SetupEnableAndVisibility();
            }
            void workbench_SurfaceAdded(object o, CoreItemEventArgs<ICoreWorkingSurface> e)
            {
                this.SetupEnableAndVisibility();
            }
    }
}

