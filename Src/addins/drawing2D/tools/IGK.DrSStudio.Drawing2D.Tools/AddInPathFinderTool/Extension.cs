using IGK.ICore;
using IGK.ICore.GraphicModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D.AddInPathFinderTool
{
    public static class Extension
    {
        public static GraphicsPath ToGdiPGraphicsPath(this ICoreGraphicsPath o) {

            o.GetAllDefinition(out Vector2f[] points, out byte[] types);
            GraphicsPath m = new GraphicsPath(points.CoreConvertTo<PointF[]>(), types);
            return m;
        }
    }
}
