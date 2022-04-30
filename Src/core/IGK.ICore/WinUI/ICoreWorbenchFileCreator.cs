namespace IGK.ICore.WinUI
{
    public interface ICoreWorbenchFileCreator : ICoreSystemWorkbench
    {
        /// <summary>
        /// create a new files
        /// </summary>
        bool CreateNewFile();
    }
}