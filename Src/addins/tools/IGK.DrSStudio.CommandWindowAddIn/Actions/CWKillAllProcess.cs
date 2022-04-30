using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.CommandWindow.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.CommandWindow.Actions
{
    [CWAction("cw.killallprocess", Description = "kill all started process")]
    class CWKillAllProcess : CWActionBase 
    {
        protected override bool PerformAction()
        {
            CommandWindowTool.Instance.KillAllProcess();
            return true;
        }
    }
}
