using IGK.DrSStudio.Drawing2D.Menu;
using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D.AddInPathOperator.Menu.Path
{
    [CoreMenu(PathConstants.PATHMENU +".AddReversedPath", 12)]
    class AddReversedPathMenu : PathElementMenuBase
    {
        protected override bool PerformAction()
        {
            var v_path = this.PathElement.GetPath();

            CorePathUtils.ReversePath(v_path, out Vector2f[] pts, out byte[] types);

         

            this.PathElement.AddDefinition(pts, types);
            this.CurrentSurface.Invalidate();

            return base.PerformAction();
        }
    }
}
