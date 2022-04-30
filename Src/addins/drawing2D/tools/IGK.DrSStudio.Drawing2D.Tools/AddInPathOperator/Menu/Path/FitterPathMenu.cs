using IGK.DrSStudio.Drawing2D.Menu;
using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.Resources;
using IGK.ICore.Services;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D.AddInPathOperator.Menu.Path
{

    [CoreMenu("Path.FitterPathMenu", 12)]
    class FitterPathMenu : PathElementMenuBase
    {
        protected override bool PerformAction()
        {

            if (CoreSystemServices.GetServiceByName("PathFitterServices") is ICorePathService service)
            {
                service.Apply(this.PathElement);
                this.CurrentSurface.Invalidate();
            }
            else
            {
                CoreMessageBox.NotifyMessage("Error",
                   CoreResources.GetResourceString("sys://no_resources_found_1"));
            }
            return false;
        }
    }
}
