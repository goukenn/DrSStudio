

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _CloseAllButNotThecurrentSurface.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:_CloseAllButNotThecurrentSurface.cs
*/
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.DrSStudio.Menu.File
{
    [DrSStudioMenu("File.CloseAllButNotCurrentSurface", Int16.MaxValue - 200)]
    class _CloseAllButNotThecurrentSurfaceMenu :  CoreApplicationMenu 
    {
        protected override bool PerformAction()
        {
             var fb = this.Workbench  as ICoreSurfaceManagerWorkbench;
             if (fb != null)
             {
                 if (fb.Surfaces.Count > 1)
                 {
                     ICoreWorkingSurface s = this.CurrentSurface;
                     ICoreWorkingSurface[] surfaces = fb.Surfaces.ToArray();
                     foreach (ICoreWorkingSurface item in surfaces)
                     {
                         if (item == s) continue;
                         fb.Surfaces.Remove(item);
                     }
              
                 }
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
            CheckVailability();
        }
        void workbench_SurfaceAdded(object o, CoreItemEventArgs<ICoreWorkingSurface> e)
        {
            CheckVailability();
        }
        private void CheckVailability()
        {
             var fb = this.Workbench as ICoreSurfaceManagerWorkbench;
             if (fb != null)
             {
                 this.Enabled = fb.Surfaces.Count > 1;
             }
             else
                 this.Enabled = false;
        }
    }
}

