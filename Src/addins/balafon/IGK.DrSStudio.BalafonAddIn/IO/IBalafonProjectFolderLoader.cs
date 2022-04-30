namespace IGK.DrSStudio.Balafon.IO
{
    internal interface IBalafonProjectFolderLoader
    {
        void LoadFolder(string rootFolder, string dir, IBalafonProject pg);
    }
}