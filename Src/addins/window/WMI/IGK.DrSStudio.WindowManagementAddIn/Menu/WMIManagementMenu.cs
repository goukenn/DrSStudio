using IGK.ICore.Menu;
using IGK.Management.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.Management.Menu
{
    [CoreMenu ("File.New.Management", 0x300, SeparatorBefore = true)]
    class WMIManagementMenu : CoreApplicationMenu{
        protected override bool PerformAction()
        {
            WMITool.Instance.ShowSurface();
            return false;
        }
    }
}
