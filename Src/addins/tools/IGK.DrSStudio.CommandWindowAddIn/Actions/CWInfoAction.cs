using IGK.ICore.WinCore;
using IGK.ICore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.CommandWindow.Actions
{
    [CWAction ("cw.info", Description="show command line info")]
    class CWInfoAction : CWActionBase 
    {
        protected override bool PerformAction()
        {
            if (this.IsInContext(CWConstant.COMMAND_WINDOW_EXEC_CONTEXT))
            {
                CommandContext.WriteLine("this is a command window tools information",
                    Colorf.Indigo);
                return true;
            }
            return false;
        }
    }
}
