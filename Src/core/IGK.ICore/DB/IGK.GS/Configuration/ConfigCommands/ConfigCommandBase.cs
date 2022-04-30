using IGK.ICore.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.GS.Configuration.ConfigCommands
{
    public abstract class ConfigCommandBase : CoreActionBase, IGSConfigCommand 
    {
        protected override bool PerformAction()
        {
            return false;
        }
    }
}
