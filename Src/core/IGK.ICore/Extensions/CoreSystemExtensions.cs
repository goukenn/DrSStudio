using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore
{
    public static class CoreSystemExtensions
    {
        public static enuDialogResult ConfigureWorkingObject(this ICoreSystemWorkbench bench, ICoreWorkingConfigurableObject item, string tilen, bool allowCancel, Size2i defaultSize) {
            if (bench is ICoreWorkbenchWorkingObjectConfigurator m)
            {
                return m.ConfigureWorkingObject(item, tilen, allowCancel, defaultSize);// false, false, 
            }
            else if (bench is ICoreWorkbench s) {
                // s.C

            }
            return enuDialogResult.None;
        }
    }
}
