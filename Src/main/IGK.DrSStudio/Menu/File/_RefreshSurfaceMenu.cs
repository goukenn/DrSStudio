using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;

namespace IGK.DrSStudio.Menu.File
{
    [CoreMenu("RefreshSurface", -10, IsVisible =false, Shortcut = enuKeys.F5)]
    class _RefreshSurfaceMenu : CoreApplicationMenu
    {
        public new ICoreWorkingReloadViewSurface CurrentSurface => base.CurrentSurface as ICoreWorkingReloadViewSurface;
        protected override bool PerformAction()
        {
            this.CurrentSurface.Reload();
            return true;
        }
        protected override bool IsVisible()=> false;
        protected override bool IsEnabled()=> (this.CurrentSurface != null);

        protected override void RegisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            base.RegisterBenchEvent(Workbench);
            if (Workbench is ICoreWorkingSurfaceHandler s)
                s.CurrentSurfaceChanged += Workbench_CurrentSurfaceChanged;
        }
        protected override void UnregisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            base.RegisterBenchEvent(Workbench);
            if (Workbench is ICoreWorkingSurfaceHandler s)
                s.CurrentSurfaceChanged -= Workbench_CurrentSurfaceChanged;
        }
        private void Workbench_CurrentSurfaceChanged(object sender, CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            this.SetupEnableAndVisibility();
        }
    }
}
