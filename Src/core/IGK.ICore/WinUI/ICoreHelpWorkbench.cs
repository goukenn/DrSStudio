using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.WinUI
{
    public interface ICoreHelpWorkbench : ICoreWorkbench 
    {
        void SetHelpMessageListener(ICoreWorkbenchHelpMessageListener helpMessageListener);

        void OnHelpMessage(string message);
    }
}
