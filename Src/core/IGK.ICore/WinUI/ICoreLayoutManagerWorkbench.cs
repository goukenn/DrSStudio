using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.WinUI
{
    /// <summary>
    /// represent is workbench that will host a layout manager
    /// </summary>
    public interface ICoreLayoutManagerWorkbench : ICoreSystemWorkbench
    {
        /// <summary>
        /// get the layout manager
        /// </summary>
        ICoreWorkbenchLayoutManager LayoutManager { get; }
        IXCoreContextMenuItemContainer CreateContextMenuItem();
        IXCoreMenuItemContainer CreateMenuItem();
    }
}
