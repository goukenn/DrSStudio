using IGK.DrSStudio.FileBrowser.WinUI;
using IGK.ICore;
using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.FileBrowser.Menu.Tools
{
    [CoreMenu("Tools.FileBrowser", 0x500)]
    class FBToolsMenu : CoreApplicationMenu 
    {
        protected override bool PerformAction(){
            FBSurface srv = FBTool.Instance.GetSurface();
            this.Workbench.AddSurface(srv, true);
            return false;
        }
    }
}
