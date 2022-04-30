

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CloseSolution.cs
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
    /*
     * 
     * REMOVE ALL SOLUTION FROM THIS PROJECT
     * 
     * 
     * */
    [DrSStudioMenu("File.CloseSolution", 0x150)]
    class CloseSolutionMenu : CoreApplicationMenu 
    {
        public new ICoreSolutionManagerWorkbench Workbench
        {
            get
            {
                return base.Workbench as ICoreSolutionManagerWorkbench;
            }
        }
        protected override bool PerformAction()
        {
            var v_bench = this.Workbench;
            var s = v_bench.Solution;
            var fb = v_bench as ICoreSurfaceManagerWorkbench;
            if ((s == null) || (fb ==null))
                return false;
            var e = fb.Surfaces.ToArray();
            foreach (var item in e)
            {
                if (item == null)
                    continue;
                var g = item as ICoreWorkingProjectSolutionSurface;
                if ((g != null) && (g.Solution == s))
                {
                    fb.Surfaces.Remove(g);
                }
            }
            v_bench.Solution.Close();
            v_bench.Solution = null;
            return base.PerformAction();
        }
        protected override void RegisterBenchEvent(ICoreSystemWorkbench workbench)
        {
            base.RegisterBenchEvent(workbench);
            ICoreSolutionManagerWorkbench v = workbench as ICoreSolutionManagerWorkbench;
            if (v!=null)
            v.SolutionChanged += Workbench_SolutionChanged;
        }
        protected override void UnregisterBenchEvent(ICoreSystemWorkbench workbench)
        {
            ICoreSolutionManagerWorkbench v = workbench as ICoreSolutionManagerWorkbench;
            if (v != null)
            v.SolutionChanged -= Workbench_SolutionChanged;
            base.UnregisterBenchEvent(workbench);
        }
        void Workbench_SolutionChanged(object sender, EventArgs e)
        {
            this.SetupEnableAndVisibility();
        }
        protected override bool IsVisible()
        {
            return this.CurrentSolution != null;
        }

        protected override bool IsEnabled()
        {
            return this.CurrentSolution != null;
        }
        /// <summary>
        /// get or close the solution
        /// </summary>
        public ICoreWorkingProjectSolution  CurrentSolution { get {
            return this.Workbench.Solution;
        } }
    }
}
