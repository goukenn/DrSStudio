using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.WinUI
{
    public interface ICoreActionManagerWorkbench : ICoreWorkbench 
    {
        event EventHandler ActionPerformed; //raise when an action is performed
    }
}
