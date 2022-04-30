using IGK.ICore.Menu;
using IGK.ICore.WinUI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Balafon.DataBaseBuilder.Menu
{
    [CoreMenu(BalafonDBBConstant.MENU_TOOLS_BALAFON + ".ExportToCSharpInterface", 0x10)]
    public class BalafonDBBExportToInterfaceSchemaMenu : BalafonDBBMenuBase
    {
        protected override bool PerformAction()
        {
            var s = CoreCommonDialogUtility.PickFolder(
                   this.Workbench,
                   null,
                   Environment.CurrentDirectory);
            if (s != null)
            {
                BalafonUtility.ExportSchemaToCSInterfaceFile(s, this.CurrentSurface.Document , null);
                return true;
            }
            return base.PerformAction();
        }
    }
}
