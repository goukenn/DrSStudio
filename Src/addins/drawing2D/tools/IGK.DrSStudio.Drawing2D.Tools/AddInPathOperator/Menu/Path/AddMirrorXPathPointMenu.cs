using IGK.DrSStudio.Drawing2D.Menu;
using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D.Menu
{

    /// <summary>
    /// menu to add mirror x point
    /// </summary>
    [CoreMenu("Path.AddMirrorX", 10)]
    class AddMirrorXPathPointMenu : PathElementMenuBase
    {
        protected override bool PerformAction()
        {
            float x = this.CurrentSurface.CurrentDocument.Width / 2.0f;
            var p = PathElement.GetPath();

            byte[] types = null;
            Vector2f[] pts = null;
            p.GetAllDefinition(out pts, out types);            
            List<Vector2f> tr = new List<Vector2f>();
            List<Byte> tb = new List<byte>();
            tr.AddRange(pts);
            tb.AddRange(types);

            for (int i = pts.Length - 1; i >= 0; i--)
            {
                var v_pts = tr[i];
                var dx = Math.Abs(v_pts.X - x);
                if (v_pts.X > x) {
                    v_pts.X -= (2 * dx);
                }
                else 
                    v_pts.Y += (2*dx);
                tr[i] = v_pts;
            }
            PathElement.SetDefinition(tr.ToArray(), tb.ToArray());
            this.CurrentSurface.RefreshScene();
            return base.PerformAction(); 
        }
    }
}
