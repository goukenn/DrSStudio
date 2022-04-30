using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.CommandWindow.Actions
{
    public abstract class CWActionBase : CoreActionBase
    {
        protected ICoreCommandWindowContext CommandContext
        {
            get
            {
                return base.GetActionContext(CWConstant.COMMAND_WINDOW_EXEC_CONTEXT)
                    as ICoreCommandWindowContext;
            }
        }
    }
}
