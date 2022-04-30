using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.WinUI.Common
{
    public interface ICoreWorkbenchDialogFactory : ICoreSystemWorkbench
    {
        /// <summary>
        /// show dialog in working
        /// </summary>
        /// <param name="control"></param>
        enuDialogResult ShowDialog(string title, ICoreControl control);

        ICoreDialogForm CreateNewDialog();
        ICoreDialogForm CreateNewDialog(ICoreControl baseControl);
        IXCoreSaveDialog CreateNewSaveDialog();
        IXCoreOpenDialog CreateOpenFileDialog();
        IXCoreFontDialog CreateFontDialog();
        IXCoreColorDialog CreateColorDialog();
        IXCoreWaitDialog CreateWaitDialog();
        IXCoreJobDialog CreateJobDialog();

        /// <summary>
        /// create a new commond dialog
        /// </summary>
        /// <param name="name">common dialog name</param>
        /// <returns>return the common dialog</returns>
        T CreateCommonDialog<T>(string name) where T : IXCommonDialog;
        /// <summary>
        /// create a common dialog from the type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T CreateCommonDialog<T>() where T : IXCommonDialog;
    }
}
