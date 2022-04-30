using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.GS.WinUI
{
    using IGK.ICore.WinUI;

    public interface IGSWorkbench : ICoreWorkbench
    {
        /// <summary>
        /// show presentation as dialog
        /// </summary>
        /// <param name="presentationName"></param>
        /// <param name="asDialog"></param>
        void ShowPresentation(string presentationName, bool asDialog);

        void SetMainForm(IXForm  mainForm);

        void LoadMenu(ICoreMenuHostControl   menu);
    }
}
