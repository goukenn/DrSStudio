using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.WinUI.Dispatch
{
    public interface ICoreDispatcherEventArgs
    {
        bool StopPropagation { get;set; }
    }
}
