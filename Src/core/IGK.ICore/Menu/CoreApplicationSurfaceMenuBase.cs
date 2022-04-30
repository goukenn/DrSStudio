using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Menu
{
    /// <summary>
    /// reprensent a surfacne workingbench menu item
    /// </summary>
    public abstract class CoreApplicationSurfaceMenuBase : CoreApplicationMenu
    {

        protected override void RegisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            base.RegisterBenchEvent(Workbench);

            if (Workbench is ICoreWorkingSurfaceHandler s )
                    s.CurrentSurfaceChanged += _CurrentSurfaceChanged;
        }
        protected override void UnregisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            if (Workbench is ICoreWorkingSurfaceHandler s)
                s.CurrentSurfaceChanged -= _CurrentSurfaceChanged;
            base.UnregisterBenchEvent(Workbench);
        }

        private void _CurrentSurfaceChanged(object sender, CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            OnCurrentSurfaceChanged(e);
        }

        protected virtual void OnCurrentSurfaceChanged(CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            
        }
    }
}
