using IGK.DrSStudio.Drawing2D.Menu;
using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D.AddInPathOperator.Menu.Path
{
    [CoreMenu("Path.InvertMatrix", 0xa1)]
    class InvertMatrixMenu : PathElementMenuBase
    {
        protected override bool PerformAction()
        {
            var l = this.PathElement;
            var g = l.GetMatrix();
            if (!g.IsIdentity && g.IsInvertible) {
                g.Invert();
                l.Transform(g);
                this.CurrentSurface.Invalidate();
            }

            return base.PerformAction();    
        }
    }
}
