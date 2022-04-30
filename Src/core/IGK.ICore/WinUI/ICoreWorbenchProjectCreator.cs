namespace IGK.ICore.WinUI
{
    public interface ICoreWorbenchProjectCreator : ICoreSystemWorkbench
    {
        /// <summary>
        /// create a new project
        /// </summary>
        bool CreateNewProject();
    }
}