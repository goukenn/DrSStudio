using IGK.DRSStudio.BalafonDesigner.WinUI;
using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DRSStudio.BalafonDesigner.Menu
{
    /// <summary>
    /// represent the base menu of balafon designer
    /// </summary>
    internal abstract class BalafonViewDesignerMenuBase : CoreApplicationMenu
    {
        public new BalafonViewDesignerSurface CurrentSurface => Workbench.CurrentSurface as BalafonViewDesignerSurface;


        protected override void InitMenu()
        {
            base.InitMenu();
        }
        protected override bool IsEnabled()
        {
            return CurrentSurface != null;
        }

        protected override bool IsVisible()
        {
            return CurrentSurface != null;
        }
    }
}
