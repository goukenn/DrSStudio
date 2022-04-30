using IGK.DrSStudio.Balafon.DataBaseBuilder.WinUI;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Balafon.DataBaseBuilder.Menu
{
    public class BalafonDBBMenuBase : CoreApplicationMenu
    {
        public new BalafonDBBEditorSurface CurrentSurface {
            get {
                return base.CurrentSurface as BalafonDBBEditorSurface;
            }
        }
        protected override bool IsEnabled()
        {
            return this.CurrentSurface != null;
        }
        protected override bool IsVisible()
        {
            return this.CurrentSurface != null;
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
            this.SetupEnableAndVisibility();
        }
    }
}
