

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebSolutionMenuBase.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Web.Menu
{
    public class WebSolutionMenuBase : CoreApplicationMenu
    {
        /// <summary>
        /// get or set the web solution
        /// </summary>
        public WebSolutionSolution CurrentSolution
        {
            get {
                ICoreSolutionManagerWorkbench r = base.Workbench as ICoreSolutionManagerWorkbench;
                if (r == null)
                    return null;
                return r.Solution  as WebSolutionSolution ; }
            
        }
        protected override bool IsVisible()
        {
            return (this.CurrentSolution != null);
        }
        protected override bool IsEnabled()
        {
            return this.IsVisible();
        }
     
        protected override void RegisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            base.RegisterBenchEvent(Workbench);
            (Workbench as ICoreSolutionManagerWorkbench).SolutionChanged += Workbench_SolutionChanged;
            if (Workbench is ICoreWorkingSurfaceHandler s )
            s.CurrentSurfaceChanged += Workbench_CurrentSurfaceChanged;
        }

        private void Workbench_CurrentSurfaceChanged(object sender, CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
        }

        private void Workbench_SolutionChanged(object sender, EventArgs e)
        {
         
        }

      
        protected override void UnregisterBenchEvent(ICoreSystemWorkbench Workbench)
        {

            if (Workbench is ICoreWorkingSurfaceHandler s)
                s.CurrentSurfaceChanged -= Workbench_CurrentSurfaceChanged;
            (Workbench as ICoreSolutionManagerWorkbench).SolutionChanged -= Workbench_SolutionChanged;
            base.UnregisterBenchEvent(Workbench);
        }
        
        void Workbench_SolutionChanged(object sender, CoreWorkingElementChangedEventArgs<ICoreWorkingProjectSolution> e)
        {
            this.SetupEnableAndVisibility();
        }
    }
}
