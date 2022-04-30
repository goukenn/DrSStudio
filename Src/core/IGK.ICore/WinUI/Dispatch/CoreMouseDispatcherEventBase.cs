using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.WinUI.Dispatch
{
    public abstract class CoreMouseDispatcherEventBase : CoreDispatcherEvent 
    {
        public override bool CanProcess(ICoreWorkingObject obj, params object[] arguments)
        {
            return base.CanProcess(obj, arguments);
        }
    }
}
