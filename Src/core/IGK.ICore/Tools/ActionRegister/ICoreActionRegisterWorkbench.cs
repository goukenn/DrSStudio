using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Tools.ActionRegister
{
    public interface ICoreActionRegisterWorkbench : ICoreSystemWorkbench 
    {
        void RegisterMessageFilter(ICoreMessageFilter filter);
        void UnregisterMessageFilter(ICoreMessageFilter filter);
    }
}
