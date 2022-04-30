using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.WinUI
{
    /// <summary>
    /// represent a file manager workbench
    /// </summary>
    public interface ICoreFileManagerWorkbench : ICoreWorkbench 
    {
        //------------------------------------
        // EVENTS
        //------------------------------------
        event EventHandler<CoreWorkingFileOpenEventArgs> FileOpened;
        /// <summary>
        /// open a list of file
        /// </summary>
        /// <param name="files"></param>
        new void OpenFile(params string[] files);
        /// <summary>
        /// check if the filename is actually opened by the drs 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        bool IsFileOpened(string filename);
    }
}
