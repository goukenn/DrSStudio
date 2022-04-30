using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.AndroidResourceEditor
{
    using IGK.DrSStudio.Android.WinUI;
    using IGK.ICore;
#if DEBUG
    [CoreMenu("File.New.Android.Resources", 0x105)]
    class ARENewAndroidResourceMenu : CoreApplicationMenu
    {
        protected override bool PerformAction()
        {
            this.Workbench.AddSurface(new AndroidResourceEditorSurface(), true);
            return false;
        }
    }
#endif
}
