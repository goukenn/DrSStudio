using IGK.ICore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.Drawing2D;
using IGK.ICore.PathFitter;
using IGK.ICore.Drawing2D.Segments;

namespace IGK.ICore.FitterService
{
    [CoreService("PathFitterServices")]
    class FitterPathService : CoreServices, ICorePathService
    {
        public void Apply(PathElement element)
        {
            var g = element.GetPath().GetAllDefinition();
            CorePathSegment sg = new CorePathSegment();
            for (int i = 0; i < g.Points.Length; i++)
            {
                sg.AddPoint(g.Points[i].X,
                    g.Points[i].Y);
            }

            var tb = new IGK.ICore.PathFitter.CorePathFitter(sg).fit(10);
            List<PenToolSegment> v_sg = new List<PenToolSegment>();
            for (int i = 0; i < tb.Length; i++)
            {
                var v_s  = tb[i];
                var pt = new Vector2f(v_s.point.X, v_s.point.Y);
                v_sg.Add(new PenToolSegment() {
                    Point =new Vector2f (v_s.point.X, v_s.point.Y),
                    HandleIn = pt + new Vector2f(v_s.handleOut.X,v_s.handleOut.Y),
                    HandleOut =pt+ new Vector2f(v_s.handleIn.X,v_s.handleIn.Y)
                });
            }
            //convert segment to path defenition
            var path = GetCorePath(v_sg);
            element.SetDefinition(path);
            path.Dispose();
        }

        private CoreGraphicsPath GetCorePath(IEnumerable<ICorePenToolSegment> tb)
        {
            CoreGraphicsPath pt = new CoreGraphicsPath();
            bool first = false;
            PathSegment vp = new PathSegment();

            float curX = 0, curY = 0,
            prevX = 0, prevY = 0,
            inX, inY,
            outX = 0, outY = 0;
            Vector2f handle;
            foreach (var item in tb)
            {
                curX = item.Point.X;
                curY = item.Point.Y;
                if (!first)
                {
                    first = true;
                    vp.LineTo(item.Point);

                }
                else
                {
                    handle = item.Point - item.HandleIn;
                    inX = curX + handle.X;
                    inY = curY + handle.Y;

                    if (inX == curX && inY == curY
                        && outX == prevX && outY == prevY)
                    {
                        vp.LineTo(curX, curY);
                    }
                    else
                    {
                        vp.AddBezier(outX, outY, inX, inY, curX, curY);
                    }
                }
                prevX = curX;
                prevY = curY;

                handle = item.Point - item.HandleOut;
                outX = prevX + handle.X;
                outY = prevY + handle.Y;
            }

            pt.AddSegment(vp);
            return pt;
        }
    }
}
