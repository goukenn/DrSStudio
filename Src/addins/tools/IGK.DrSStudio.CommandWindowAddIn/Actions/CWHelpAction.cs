using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.CommandWindow.Actions
{
    [CWAction("cw.help")]    
    class CWHelpAction : CWActionBase 
    {
        protected override bool PerformAction()
        {
            if (this.IsInContext (CWConstant.COMMAND_WINDOW_EXEC_CONTEXT))
            {
                ICoreCommandWindowContext c = base.GetActionContext(CWConstant.COMMAND_WINDOW_EXEC_CONTEXT)
                     as ICoreCommandWindowContext;
                if (c != null)
                {
                    c.CWriteLine("help or ?");
                }
            }
            return false;
        }
        
    }
}
