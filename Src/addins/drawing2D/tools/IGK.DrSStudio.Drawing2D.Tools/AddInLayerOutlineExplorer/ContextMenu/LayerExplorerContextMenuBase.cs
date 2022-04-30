using IGK.ICore.ContextMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D.AddInLayerOutlineExplorer.ContextMenu
{
    abstract class LayerExplorerContextMenuBase : CoreContextMenuBase
    {
        protected override void InitContextMenu()
        {
            base.InitContextMenu();
            this.IsRootMenu = true;
        }
        protected override bool IsVisible()
        {
            var r = this.OwnerContext.SourceControl;
            if (r == LayerExplorerTool.Instance.HostedControl)
                return true;
            return false;
        }
    }
}
