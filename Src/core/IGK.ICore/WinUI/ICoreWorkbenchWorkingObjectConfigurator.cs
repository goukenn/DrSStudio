using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.WinUI
{
    public interface ICoreWorkbenchWorkingObjectConfigurator
    {
        enuDialogResult ConfigureWorkingObject(ICoreWorkingConfigurableObject item, string title, bool allowCanel, Size2i defaultSize);
    }
}
