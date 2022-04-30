

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _MenuViewSolutionExplorer.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:_MenuViewSolutionExplorer.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Tools.SolutionExplorer.Menu
{
    [IGK.DrSStudio.Menu .CoreMenu ("View.SolutionExplorer", 40, ImageKey="Menu_SolutionEx")]
    class _MenuViewSolutionExplorer : IGK.DrSStudio.Menu.CoreMenuViewToolBase 
    {
        public new IGK.DrSStudio.WinUI.ICoreWorkingProjectManagerSurface CurrentSurface {
            get {
                return base.CurrentSurface as IGK.DrSStudio.WinUI.ICoreWorkingProjectManagerSurface;
            }
        }
        public _MenuViewSolutionExplorer():base(SolutionExplorer.CoreSolutionExplorerTool.Instance)
        {
        }
        protected override bool IsVisible()
        {
            return (this.CurrentSurface != null);
        }
        protected override bool IsEnabled()
        {
            return IsVisible ();
        }
    }
}

