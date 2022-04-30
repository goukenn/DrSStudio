using IGK.DrSStudio.Drawing2D.Menu;
using IGK.ICore;
using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D.AddInPathOperator.Menu.Path
{
    [CoreMenu("Path.InvertPathElement", 11)]
    class InvertPathElementMenu : PathElementMenuBase
    {
        protected override bool PerformAction()
        {
            if (PathElement == null)
                return false;
            var p = PathElement.GetPath();

            p.Invert();
            p.GetAllDefinition(out Vector2f[] pts, out byte[] types);
            
            PathElement.SetDefinition(pts, types);
            this.CurrentSurface.RefreshScene();
            return true ;
        }
    }
}
