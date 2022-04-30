using IGK.ICore.Menu;
using IGK.ICore.WinUI.Common;
using System;

namespace IGK.DrSStudio.Balafon.DataBaseBuilder.Menu
{
    [CoreMenu(BalafonDBBConstant.MENU_TOOLS_BALAFON + ".DataSchema.ExportToPhpInterface", 0X13)]
    class BalafonDBBExportToPhpInterfaceMenu : BalafonDBBMenuBase
    {   
            protected override bool PerformAction()
            {
                var s = CoreCommonDialogUtility.PickFolder(
                    this.Workbench,
                    null,
                    Environment.CurrentDirectory);
                if (s != null)
                {
                    BalafonUtility.ExportSchemaToPhpInterfaceFile(s, null);
                    return true;
                }   
                return false;
            }
        
    }
}
