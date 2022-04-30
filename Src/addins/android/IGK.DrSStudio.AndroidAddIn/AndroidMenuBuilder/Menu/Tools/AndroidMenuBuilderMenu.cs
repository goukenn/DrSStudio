
using IGK.ICore;using IGK.DrSStudio.Android.AndroidMenuBuilder.WinUI;
using IGK.DrSStudio.Android.Menu;
using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.AndroidMenuBuilder.Menu
{
    [CoreMenu("Tools.Android.AndroidBuildMenuEditor", 0x11)]
    class AndroidMenuBuilderMenu : AndroidMenuBase
    {
        private AndroidMenuBuilderSurface c_currentSurface;

        protected override bool PerformAction()
        {
            AndroidMenuBuilderSurface s = this.c_currentSurface;
            if ((s == null) || s.IsDisposed)
            {
                s = new AndroidMenuBuilderSurface();
                Workbench.AddSurface(s, true);
                this.c_currentSurface = s;
            }
            else
            {
                this.Workbench.SetCurrentSurface( s);
            }
            return base.PerformAction();
        }
    }
}
