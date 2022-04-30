
using IGK.DrSStudio.Drawing3D.FileBrowser.Tools;
using IGK.ICore;
using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing3D.FileBrowser.Menu.Window
{
    [CoreMenu(CoreConstant.MENUI_WINDOW + ".FBViewFileBrowser", 0x0500)]
    public class FBViewFileBrowserMenu : FBMenuBase 
    {

        protected override bool PerformAction()
        {
            FBViewControlTool.Instance.ShowFileBrowser();
            return false;
        }
    }
}
