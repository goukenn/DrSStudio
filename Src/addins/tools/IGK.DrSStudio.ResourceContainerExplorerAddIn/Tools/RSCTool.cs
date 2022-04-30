using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.WinUI;
using IGK.ICore.Tools;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Tools
{
    [CoreTools("RSCTools", Description = "control resources in a surface")]
    class RSCTool : CoreToolBase
    {
        private static RSCTool sm_instance;
        private RSCTool()
        {
        }

        public static RSCTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static RSCTool()
        {
            sm_instance = new RSCTool();
        }
        new RSCView HostedControl {
            get {
                return base.HostedControl as RSCView;
            }
            set {
                base.HostedControl = value;
            }
        }
        protected override void GenerateHostedControl()
        {
            this.HostedControl = new RSCView(this);
            this.HostedControl.CaptionKey = "title.RSCTool";
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

        void Workbench_CurrentSurfaceChanged(object sender, ICore.CoreWorkingElementChangedEventArgs<ICore.WinUI.ICoreWorkingSurface> e)
        {
            this.UnregisterSurfaceEvent(e.OldElement as ICoreWorkingResourcesContainerSurface);
            this.RegisterSurfaceEvent(e.NewElement as ICoreWorkingResourcesContainerSurface);
        }

        private void UnregisterSurfaceEvent(ICoreWorkingResourcesContainerSurface surface)
        {
            if (surface == null) return;
        }

        private void RegisterSurfaceEvent(ICoreWorkingResourcesContainerSurface surface)
        {
            if (surface == null)
            {
                this.HostedControl.Resources = null;
                return;
            }

            this.HostedControl.Resources = surface.Resources;
        }
    }
}
