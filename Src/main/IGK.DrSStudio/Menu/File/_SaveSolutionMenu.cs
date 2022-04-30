

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _SaveSolution.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Menu.File
{
    [DrSStudioMenu("File.SaveSolution", CoreConstant.SAVE_MENU_INDEX + 3)]
    class _SaveSolutionMenu: CoreApplicationMenu 
    {
        new ICoreWorkingProjectSolutionSurface CurrentSurface
        {
            get
            {
                return base.CurrentSurface as ICoreWorkingProjectSolutionSurface;
            }
        }
        protected override bool IsEnabled()
        {
           var b = (Workbench as ICoreSolutionManagerWorkbench);
            
            return (b!=null) && (b.Solution != null);
        }
        protected override bool IsVisible()
        {
            return this.CurrentSurface != null;
        }
        protected override bool PerformAction()
        {
            if (this.CurrentSurface != null)
            {
                this.CurrentSurface.Solution.Save();
            }
            return false;
        }
      
        protected override void RegisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            base.RegisterBenchEvent(Workbench);
            if (Workbench is ICoreWorkingSurfaceHandler s)
                s.CurrentSurfaceChanged += Workbench_CurrentSurfaceChanged;
        }
        protected override void UnregisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
             if (Workbench is ICoreWorkingSurfaceHandler s)
                s.CurrentSurfaceChanged -= Workbench_CurrentSurfaceChanged;
            base.UnregisterBenchEvent(Workbench);
        }

        void Workbench_CurrentSurfaceChanged(object sender, CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            this.SetupEnableAndVisibility();
        }
    }
}
