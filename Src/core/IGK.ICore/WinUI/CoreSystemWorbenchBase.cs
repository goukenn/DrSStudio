using IGK.ICore.Tools.ActionRegister;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.WinUI
{
    public abstract class CoreSystemWorbenchBase : ICoreSystemWorkbench, ICoreActionRegisterWorkbench
    {
        public virtual ICoreWorkingSurface CurrentSurface => null;

        public virtual void CallAction(string cmd)
        {
            
        }

        public virtual void Init(CoreSystem appInstance)
        {
            
        }

        public virtual void RegisterMessageFilter(ICoreMessageFilter filter) {
        }
        public virtual void UnregisterMessageFilter(ICoreMessageFilter filter) {
        }
    }
}
