

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreSolutionExplorerTool.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:CoreSolutionExplorerTool.cs
*/
using IGK.ICore;using IGK.DrSStudio.Tool.SolutionExplorer.WinUI;
using IGK.DrSStudio.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Tools.SolutionExplorer
{
    [CoreTools ("Tool.SolutionExplorer",ImageKey="Menu_SolutionEx")]
    public sealed  class CoreSolutionExplorerTool : CoreToolBase 
    {
        private static CoreSolutionExplorerTool sm_instance;
        private CoreSolutionExplorerTool():base()
        {
        }
        public static CoreSolutionExplorerTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static CoreSolutionExplorerTool()
        {
            sm_instance = new CoreSolutionExplorerTool();
        }
        public new UIXSolutionExplorer HostedControl {
            get {
                return base.HostedControl as UIXSolutionExplorer;
            }
            set {
                base.HostedControl = value;
            }
        }
        protected override void GenerateHostedControl()
        {
            this.HostedControl = new UIXSolutionExplorer();
            this.HostedControl.Enabled = false;
        }
        protected override void RegisterBenchEvent(IGK.DrSStudio.WinUI.ICoreWorkbench workbench)
        {
            base.RegisterBenchEvent(workbench);
            workbench.CurrentSurfaceChanged += workbench_CurrentSurfaceChanged;
        }
        protected override void UnregisterBenchEvent(IGK.DrSStudio.WinUI.ICoreWorkbench workbench)
        {
            workbench.CurrentSurfaceChanged -= workbench_CurrentSurfaceChanged;
            base.UnregisterBenchEvent(workbench);
        }

        private void workbench_CurrentSurfaceChanged(object sender, CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            UpdateHost();
        }
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            UpdateHost();
        }
        private void UpdateHost()
        {
            if (this.Visible && (this.CurrentSurface != null))
            {
                this.HostedControl.LoadSolution(this.CurrentSurface.ProjectInfo);
                this.HostedControl.Enabled = true;
            }
            else
            {
                this.HostedControl.LoadSolution(null);
                this.HostedControl.Enabled = false;
            }
        }
        public new IGK.DrSStudio.WinUI.ICoreWorkingProjectManagerSurface CurrentSurface 
        {
            get {
                return base.CurrentSurface as IGK.DrSStudio.WinUI.ICoreWorkingProjectManagerSurface;
            }
        }
    }
}

