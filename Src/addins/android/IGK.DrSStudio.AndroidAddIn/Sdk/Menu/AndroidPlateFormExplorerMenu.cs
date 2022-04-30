
using IGK.ICore;using IGK.DrSStudio.Android.WinUI;
using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.DrSStudio.Android.Sdk.WinUI;

namespace IGK.DrSStudio.Android.Sdk.Menu.Tools
{
    [CoreMenu("Tools.Android.ExploreAndroidPlatForms" , 0x010)]
    class AndroidPlateFormExplorerMenu : CoreApplicationMenu
    {
        private AndroidSdkManagerSurface c_surface;
        protected override bool PerformAction()
        {
            AndroidSdkManagerSurface c = this.c_surface;
            if ((c == null) || c.IsDisposed)
            {
                c = new AndroidSdkManagerSurface();
                this.c_surface = c;
                this.Workbench.AddSurface(c, true);
            }
            else {
                this.Workbench.SetCurrentSurface(c);
            }
            return false;
        }
    }
}
