

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PathSegment.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:PathSegment.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Drawing2D.Segments
{
    /// <summary>
    /// represent a single path segment
    /// </summary>
    public class PathSegment : PointSegment 
    {
        public PathSegment()
        {
        }
        public override void CloseFigure()
        {
            this.close_figure();
        }
        public void AddArc(Rectanglef rectanglef, float startAngle, float sweepAngle)
        {
            this.append_arcs(rectanglef.X, rectanglef.Y, rectanglef.Width, rectanglef.Height,
                startAngle, sweepAngle);
        }
        public void AddArc(Vector2f center, Vector2f radius, float startAngle, float sweepAngle, bool closed)
        {

            var g = new ArcSegment(center, radius, startAngle, sweepAngle,false );
            //var s = g.GetPathPoints();
            //var m = g.GetPathTypes();
            //for (int i = 1; i < s.Length; i++)
            //{
            //    this.append(s[i].X, s[i].Y, (enuGdiGraphicPathType)m[i], (i == 0));
            //}

            this.Append(g.GetPathPoints (), g.GetPathTypes ());
            //this.append_arcs(rectanglef.X, rectanglef.Y, rectanglef.Width, rectanglef.Height,
            //    startAngle, sweepAngle);
        }
        public  void Append(Vector2f[] points, byte[] types)
        {
            for (int i = 0; i < points.Length; i++)
            {
                this.append(points[i].X, points[i].Y, (enuGdiGraphicPathType)types[i], (i == 0));
            }
        }
        public void AddLine(Vector2f def1, Vector2f def2)
        {
            this.append(def1.X, def1.Y, enuGdiGraphicPathType.LinePoint, true);
            this.append(def2.X, def2.Y, enuGdiGraphicPathType.LinePoint, true);
        }
        public void AddLines(Vector2f[] tab)
        {
            if (this.IsEmpty)
            {
                for (int i = 0; i < tab.Length; i++)
                {
                    this.append(tab[i].X, tab[i].Y, (i == 0) ?
                        enuGdiGraphicPathType.StartFigure :
                        enuGdiGraphicPathType.LinePoint, false);
                }
            }
            else {
                for (int i = 0; i < tab.Length; i++)
                {
                    this.append(tab[i].X, tab[i].Y,
                        enuGdiGraphicPathType.LinePoint, false);
                }
            }
        }

        internal void AddArc(Rectanglef v_rc, float startAngle, float sweepAngle , bool closed)
        {
            this.AddArc (v_rc.Center,
                new Vector2f(v_rc.Width / 2.0f, v_rc.Height / 2.0f)
                , startAngle, sweepAngle, closed);
        }

        public void AddBezier(float outX, float outY, float inX, float inY, float curX, float curY)
        {
            this.append_bezier(outX, outY, inX, inY, curX, curY);
        }

        /// <summary>
        /// Line to 
        /// </summary>
        /// <param name="vector2f"></param>
        public  void LineTo(Vector2f vector2f)
        {
            //if (this.PointCount > 0)
            this.LineTo(vector2f.X, vector2f.Y);
            //else {
            //    this.append(vector2f.X, vector2f.Y, enuGdiGraphicPathType.StartFigure, false);
            //}
        }
        public void LineTo(float x, float y) {
            this.append(x, y, (this.PointCount > 0) ? enuGdiGraphicPathType.LinePoint : enuGdiGraphicPathType.StartFigure,
               false);
        }
        public void MoveTo(Vector2f point) {
            if (PointCount == 0)
            {
                this.append(point.X, point.Y, enuGdiGraphicPathType.StartFigure,
                       false);
            }
            else {
                throw new NotImplementedException();
            }
        }
        
    }
}

