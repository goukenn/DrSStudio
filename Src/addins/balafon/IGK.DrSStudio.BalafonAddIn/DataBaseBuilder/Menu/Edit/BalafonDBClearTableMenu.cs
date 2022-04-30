using IGK.ICore;
using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Balafon.DataBaseBuilder.Menu.Edit
{
    [CoreMenu("Edit.ClearDataTable", 0x101, 
        ImageKey=CoreImageKeys.MENU_DELETE_GKDS)]
    sealed class BalafonDBClearTableMenu : BalafonDBBMenuBase
    {
        protected override bool PerformAction()
        {
            this.CurrentSurface.Document.ClearTable();
            return base.PerformAction();
        }
        protected override bool IsEnabled()
        {
            return base.IsEnabled() && (this.CurrentSurface.Document !=null);
        }
    }
}
