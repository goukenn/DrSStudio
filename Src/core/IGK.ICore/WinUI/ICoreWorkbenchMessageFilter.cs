using IGK.ICore.Menu;

namespace IGK.ICore.WinUI
{
    public interface ICoreWorkbenchMessageFilter : ICoreSystemWorkbench
    {
        /// <summary>
        /// get if the menu action is filtered
        /// </summary>
        bool MenuActionMessageFiltering { get; }
        /// <summary>
        /// get or set the filtered action container
        /// </summary>
        ICoreMenuMessageShortcutContainer FilteredAction { get; set; }
    }
}