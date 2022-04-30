using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.WinCore.Caret
{
    public interface IWinCoreCaretManager : 
        IDisposable , ICoreCaret
    {

        IntPtr ControlHandle { get; }
    }
}
