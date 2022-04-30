namespace IGK.ICore.WinUI
{
    /// <summary>
    /// represent a Environment handler
    /// </summary>
    public interface ICoreWorkbenchEnvironment
    {
        ICoreSystemWorkbench Workbench { get;}
        bool OpenFile(string filename);
    }
}