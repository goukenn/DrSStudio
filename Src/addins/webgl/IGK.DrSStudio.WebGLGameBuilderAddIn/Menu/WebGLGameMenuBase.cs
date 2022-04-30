using IGK.DrSStudio.WebGLEngine.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinUI;
using IGK.ICore;

namespace IGK.DrSStudio.WebGLEngine.Menu
{
    public abstract class WebGLGameMenuBase : IGK.ICore.Menu.CoreApplicationMenu
    {
        public new WebGLDesignSurface CurrentSurface
        {
            get
            {
                return base.CurrentSurface as WebGLDesignSurface;
            }
        }

        protected override bool IsVisible()
        {
            return (this.CurrentSurface != null);// base.IsVisible();
        }
        protected override bool IsEnabled()
        {
            return (this.CurrentSurface != null); //base.IsEnabled();
        }
        protected override void RegisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            base.RegisterBenchEvent(Workbench);
            if (Workbench is ICoreWorkingSurfaceHandler s)
                s.CurrentSurfaceChanged += _surfaceChanged;
        }
        protected override void UnregisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            base.UnregisterBenchEvent(Workbench);
            if (Workbench is ICoreWorkingSurfaceHandler s)
                s.CurrentSurfaceChanged -= _surfaceChanged;
        }

        private void _surfaceChanged(object sender, CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            OnCurrentSurfaceChanged(e);
        }

        protected virtual void OnCurrentSurfaceChanged(CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            this.SetupEnableAndVisibility();
        }
    }
}
