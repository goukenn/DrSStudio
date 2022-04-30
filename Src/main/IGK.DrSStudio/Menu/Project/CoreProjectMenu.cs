using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Menu.Project
{

    [DrSStudioMenu("Project", 3)]
    class CoreProjectMenu : CoreApplicationMenu
    {
        public new ICoreWorkingProjectSolutionSurface CurrentSurface {
            get {
            return base.CurrentSurface as ICoreWorkingProjectSolutionSurface; }
        }

        protected override bool IsVisible()
        {
            return (this.CurrentSurface !=null);//base.IsVisible();
        }

    }
}
