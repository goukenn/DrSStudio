using System;

namespace IGK.ICore.WinUI
{
    /// <summary>
    /// represent a environment workbench handler
    /// </summary>
    public interface ICoreWorkbenchEnvironmentHandler
    {
        ICoreWorkbenchEnvironment Environment { get;set;}

        event EventHandler<CoreWorkingElementChangedEventArgs<ICoreWorkbenchEnvironment>> EnvironmentChanged;
    }
}