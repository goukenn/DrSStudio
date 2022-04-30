using IGK.DrSStudio.WinCore.Actions;
using IGK.ICore.ContextMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D.AddInLayerOutlineExplorer.ContextMenu
{
    [CoreContextMenu("LayerExplorer.DesignElement", 0x010)]
    class DesignElementLayerExplorerContextMenu : LayerExplorerContextMenuBase
    {
        protected override bool PerformAction()
        {
            Workbench.CallAction(WinCoreActions.EDIT_DESIGN_WITH_MECANISM);
            return false;
        }
    }
}
