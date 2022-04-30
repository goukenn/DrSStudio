using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Balafon.DataBaseBuilder.Menu.Edit
{
    [CoreMenu("Edit.AddDataTable", 0x100, SeparatorBefore=true)]
    sealed class BalafonDBAddTableMenu : BalafonDBBMenuBase
    {
        protected override bool PerformAction()
        {
            string f = IGK.ICore.WinUI.Common.CoreCommonDialogUtility.PickFileName(this.Workbench);
            if (string.IsNullOrEmpty(f) == false)
            {
                this.CurrentSurface.Document.AddTable(f);
            }
            return base.PerformAction();
        }
    }
}
