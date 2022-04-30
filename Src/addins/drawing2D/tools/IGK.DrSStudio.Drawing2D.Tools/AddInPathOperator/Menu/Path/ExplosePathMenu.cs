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
     [CoreMenu("Path.ExplosePathMenu", 12)]
    /// <summary>
    /// represent explose path menu
    /// </summary>
    class ExplosePathMenu : PathElementMenuBase
    {
        protected override bool PerformAction()
        {
            var p = this.PathElement.GetPath();
            Vector2f[] pts = null;
            byte[] t = null;
            if (p.GetAllDefinition(out pts, out t)) {

                List<IGK.ICore.Drawing2D.PathElement> c = new List<IGK.ICore.Drawing2D.PathElement>();
                //find next marquer
                int i = 1;
                int n = 0;
                int l = 0;
                
                byte e = (byte)enuGdiGraphicPathType.EndPoint;
                while (i < pts.Length) {

                    if (((t[i] & e) == e) || (i == (pts.Length -1)))
                    {
                        l = i - n +1;
                        
                        var v_def = new byte[l];
                        var v_pts = new Vector2f[l];
                        //copy def
                        Array.Copy(pts, n, v_pts, 0,l);
                        Array.Copy(t, n, v_def, 0, l);
                        var se = IGK.ICore.Drawing2D.PathElement.CreateElement(v_pts, v_def);
                        se.FillBrush.Copy(this.PathElement.FillBrush);
                        se.StrokeBrush.Copy(this.PathElement.StrokeBrush);
                        c.Add (se);
                        n = i+1;
                    }
                    i++;
                }
                if (c.Count >0){
                    c.Reverse();
                    this.CurrentSurface.CurrentLayer.Elements.AddRange(c.ToArray ());
                }
            }


                
            return base.PerformAction();
        }
    }
}
