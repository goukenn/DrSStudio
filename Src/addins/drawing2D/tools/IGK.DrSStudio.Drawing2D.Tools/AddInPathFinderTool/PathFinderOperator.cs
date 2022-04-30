using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.WinCore;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace IGK.DrSStudio.Drawing2D.AddInPathFinderTool
{
    /// <summary>
    /// represent a path finder operator
    /// </summary>
    class PathFinderOperator
    {
        List<GraphicsPath> m_path;

        public void Add(GraphicsPath path) {
            this.m_path.Add(path);
        }
        public void Clear() {
            this.m_path.Clear();
        }
        public void RemvoeAt(int index) {
            this.m_path.RemoveAt(index);
        }



        ///<summary>
        ///public .ctr
        ///</summary>
        public PathFinderOperator()
        {
            this.m_path = new List<GraphicsPath>();
        }

        public CoreGraphicsPath Unite() {
            using (GraphicsPath g = new GraphicsPath())
            {
                foreach (var item in this.m_path)
                {
                    g.AddPath(item, false);
                }
               WinCoreGraphicsPathUtils.GetOutlinePath(g, out byte[] t, out Vector2f[] pts );
                CoreGraphicsPath h = new CoreGraphicsPath();
                h.AddDefinition(pts, t);
                return h;// new CoreGraphicsPath() new GraphicsPath( pts.CoreConvertTo<PointF[]>(), t, FillMode.Winding);

            }
        }
    }
}
