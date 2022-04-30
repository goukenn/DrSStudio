using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.WinUI
{
    public interface ICoreFilterActionWorkbench : ICoreSystemWorkbench
    {
        object FilteredAction{get;set;}
    }
}
