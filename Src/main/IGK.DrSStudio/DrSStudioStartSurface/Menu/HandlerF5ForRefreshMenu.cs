using IGK.DrSStudio.WinUI;
using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.DrSStudioStartSurface.Menu
{
    [CoreMenu ("HandleStartMenuF5", -1, IsVisible = false , Shortcut = enuKeys.F5)]
    class HandlerF5ForRefreshMenu : CoreApplicationSurfaceMenuBase
    {
        protected override bool PerformAction()
        {
            if (this.CurrentSurface is ICoreWorkingReloadViewSurface s)
            {
                s.Reload();
                return true;
            }
            return false;
        }
        protected override bool IsEnabled()
        {
            return this.CurrentSurface is ICoreWorkingReloadViewSurface;
        }
        protected override bool IsVisible()
        {
            return base.IsVisible();
        }
       
        protected override  void OnCurrentSurfaceChanged(CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            this.SetupEnableAndVisibility();
        }
        
    }
}
