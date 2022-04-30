using IGK.ICore.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.WinUI
{
    public static class CoreWorkbenchUtility
    {
         /// <summary>
        /// initialize the tools to the workbench
        /// </summary>
        public static void InitTools(ICoreWorbenchToolListener bench)
        {
            foreach (KeyValuePair<string, ICoreTool> a in CoreSystem.GetTools())
            {
                if (a.Value is CoreToolBase t)
                {
                    t.Workbench = bench as ICoreWorkbench;
                }
            }
        }        


        
    }
}
