using IGK.ICore;
using IGK.ICore.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.GS.Actions.SystemActions
{
    [GSAction("actions.list", Description="list all present action")]
    public class actionsListAction : GSActionBase 
    {
        protected override bool PerformAction()
        {            
            if (CoreUtils.IsInConsoleMode ())
            {
                int i = 0;
                StringBuilder sb = new StringBuilder();
                System.Text.RegularExpressions.Regex rg = new System.Text.RegularExpressions.Regex("");
                if (this.Param != null) {
                    rg = new System.Text.RegularExpressions.Regex((string)this.Param,
                        System.Text.RegularExpressions.RegexOptions.IgnoreCase );
                }
                int vc = 0;
                foreach (ICoreAction c in CoreSystem.GetActions())
                {
                    if (string .IsNullOrEmpty (c.Id) || !rg.IsMatch(c.Id))
                        continue;

                    if (i > 3)
                    {
                        i = 0;
                        sb.AppendLine();
                    }
                    sb.Append (c.Id + "\t");
                    i++;
                    vc++;
                }
                if (vc == 0)
                {
                    GSLog.CWriteLine("-----------------   No actions founds ----------------------");
                }
                else
                {
                    GSLog.CWriteLine("count : " + vc);
                    GSLog.CWriteLine(sb.ToString());
                    
                }

            }            
            return false;
        }
      
    }
}
