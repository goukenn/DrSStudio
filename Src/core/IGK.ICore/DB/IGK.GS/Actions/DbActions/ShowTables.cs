using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.GS.Actions.DbActions
{
    [GSAction ("db.show.tables")]
    class ShowTables : GSActionBase 
    {
        protected override bool PerformAction()
        {
            string[] tb = GSDataContext.GetTables();
            if (Environment.UserInteractive)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("---------------------------------------------");
                sb.AppendLine("Tables");
                sb.AppendLine("---------------------------------------------");
                for (int i = 0; i < tb.Length; i++)
                {
                    if (i > 0)
                        sb.AppendLine();
                    sb.Append(tb[i]);
                }
                GSLog.CWriteLine(sb.ToString());
            }
            this.Response = tb;
            return true;
        }
    }
}
