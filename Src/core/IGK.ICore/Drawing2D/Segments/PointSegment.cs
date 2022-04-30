

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PointSegment.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore;using IGK.ICore.GraphicModels;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:PointSegment.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Drawing2D.Segments
{
    public class PointSegment : CorePathSegmentBase 
    {
        List<Vector2f> m_points;
        List<Byte> m_bytes;
        private bool  start_new_fig = true ;
        public int PointCount { get { return m_points.Count; } }
        public int PointByteCount { get { return m_bytes.Count; } }

        public void Reset() {
            m_bytes.Clear();
            m_points.Clear();
        }
        public override void SetPoint(int index, int def)
        {
            if (index < 0)
                this.m_bytes[this.m_bytes.Count + index] = (byte)def;
            else
                this.m_bytes[index] = (byte)def;
        }
        public override void Transform(Matrix matrix)
        {
            Vector2f[] m_vector2f = CoreMathOperation.TransformVector2fPoint(matrix, m_points.ToArray());
            if (m_vector2f != null)
            {
                m_points.Clear();
                m_points.AddRange(m_vector2f);
            }
        }
        public override void Invert()
        {
            m_points.Reverse();
        }
        public PointSegment()
        {
            this.m_points = new List<Vector2f>();
            this.m_bytes = new List<byte>();
        }
        public override enuSegmentType SegmentType
        {
            get { return enuSegmentType.Points; }
        }
      
        public override Vector2f[] GetPathPoints()
        {
            return this.m_points.ToArray();
        }
        public override byte[] GetPathTypes()
        {
            return this.m_bytes.ToArray();
        }
        protected Vector2f[] getCurvePoint(Vector2f[] points, Vector2f[] tangents, int startindex, int Length, bool closed)

        {
            List<Vector2f> pt = new List<Vector2f>();
            enuGdiGraphicPathType ptype = ((closed) || (points.Length == 0)) ? enuGdiGraphicPathType.StartFigure : enuGdiGraphicPathType.LinePoint;
            int i;
            pt.Add(new Vector2f(points[startindex].X, points[startindex].Y));
            for (i = startindex; i < startindex + Length - 1; i++)
            {
                int j = i + 1;
                float x1 = points[i].X + tangents[i].X;
                float y1 = points[i].Y + tangents[i].Y;
                float x2 = points[j].X - tangents[j].X;
                float y2 = points[j].Y - tangents[j].Y;
                float x3 = points[j].X;
                float y3 = points[j].Y;
                pt.Add(new Vector2f(x1, y1));
                pt.Add(new Vector2f(x2, y2));
                pt.Add(new Vector2f(x3, y3));
            }
            /* complete (close) the curve using the first point */
            if (closed)
            {
                float x1 = points[i].X + tangents[i].X;
                float y1 = points[i].Y + tangents[i].Y;
                float x2 = points[0].X - tangents[0].X;
                float y2 = points[0].Y - tangents[0].Y;
                float x3 = points[0].X;
                float y3 = points[0].Y;
                pt.Add (new Vector2f (x1, y1));
                pt.Add (new Vector2f (x2, y2));
                pt.Add (new Vector2f (x3, y3));
                close_figure();
            }
            return pt.ToArray();
        }
        protected void appendCurve(Vector2f[] points,Vector2f[] tangents, int startindex, int Length, bool closed)
        {
            enuGdiGraphicPathType ptype = ((closed) ||  (points.Length == 0)) ? enuGdiGraphicPathType.StartFigure : enuGdiGraphicPathType.LinePoint;
            int i;
            append(points[startindex].X, points[startindex].Y , ptype, true);
            for (i = startindex; i < startindex + Length-1; i++)
            {
                int j = i + 1;
                float x1 = points[i].X + tangents[i].X;
                float y1 = points[i].Y + tangents[i].Y;
                float x2 = points[j].X - tangents[j].X;
                float y2 = points[j].Y - tangents[j].Y;
                float x3 = points[j].X;
                float y3 = points[j].Y;
                append_bezier(x1, y1, x2, y2, x3, y3);
            }
            /* complete (close) the curve using the first point */
            if (closed)
            {
                float x1 = points[i].X + tangents[i].X;
                float y1 = points[i].Y + tangents[i].Y;
                float x2 = points[0].X - tangents[0].X;
                float y2 = points[0].Y - tangents[0].Y;
                float x3 = points[0].X;
                float y3 = points[0].Y;
                append_bezier(x1, y1, x2, y2, x3, y3);
                close_figure();
            }
        }
        protected void append(float x, float y, enuGdiGraphicPathType pathtype, bool compressed)
        {
            if (this.m_points == null)
                return;
            byte t = (byte)pathtype;
            Vector2f pt = Vector2f.Zero;
            /* in some case we're allowed to compress identical points */
            if (compressed && (this.m_points.Count > 0))
            {
                /* points (X, Y) must be identical */
                Vector2f lastPoint = this.m_points[this.m_points.Count - 1];
                if ( (Math.Round(Math.Abs(lastPoint.X - x))==0) &&
                    (Math.Round(Math.Abs(lastPoint.Y - y))==0))
                {
                    /* types need not be identical but must handle closed subpaths */
                    enuGdiGraphicPathType last_type = (enuGdiGraphicPathType)this.m_bytes[this.m_bytes.Count - 1];
                    if ((last_type & enuGdiGraphicPathType.EndPoint) != enuGdiGraphicPathType.EndPoint)
                        return;
                }
            }
            if (start_new_fig)
                t = (byte)enuGdiGraphicPathType.StartFigure;
            /* if we closed a subpath, then start new figure and append */
            else if (this.m_points.Count > 0)
            {
                pathtype = (enuGdiGraphicPathType)this.m_bytes[this.m_bytes.Count - 1];
                if ((pathtype & enuGdiGraphicPathType.EndPoint) != 0)
                    t = (byte)enuGdiGraphicPathType.StartFigure;
            }
            pt.X = x;
            pt.Y = y;
            this.m_points.Add(pt);
            this.m_bytes.Add(t);
            start_new_fig = false;
            /*
            Vector2f pt = new Vector2f(x, y);
            if (compressed)
            {
                if (this.m_points.Count > 0)
                {
                    if (this.m_points[this.m_points.Count - 1] == pt)
                        return;
                }
            }
            if (pathtype == enuGdiGraphicPathType.StartFigure)
            {
                this.m_points.Add(pt);
                this.m_bytes.Add((byte)pathtype);
            }
            else {
                this.m_points.Add(pt);
                this.m_bytes.Add((byte)enuGdiGraphicPathType.ControlPoint);
                //this.m_points.Add(pt);
                //this.m_bytes.Add((byte)enuGdiGraphicPathType.ControlPoint);
                //this.m_points.Add(pt);
                //this.m_bytes.Add((byte)enuGdiGraphicPathType.ControlPoint );
            }*/
        }
        protected void append_bezier(
            float x1, float y1,
            float x2, float y2,
            float x3, float y3)
        {
            append(x1, y1, enuGdiGraphicPathType.ControlPoint, false);
            append(x2, y2, enuGdiGraphicPathType.ControlPoint, false);
            append(x3, y3, enuGdiGraphicPathType.ControlPoint, false);
        }
        protected void append_arcs(float x, float y, float width, float height, float startAngle, float sweepAngle)
        {
            int i;
            float drawn = 0;
            int increment;
            float endAngle;
            bool enough = false;
            if (Math.Abs(sweepAngle) >= 360)
            {
                append_ellipse(x, y, width, height);
                //GdipAddPathEllipse (path, x, y, width, height);
                return;
            }
            endAngle = startAngle + sweepAngle;
            increment = (endAngle < startAngle) ? -90 : 90;
            /* i is the number of sub-arcs drawn, each sub-arc can be at most 90 degrees.*/
            /* there can be no more then 4 subarcs, ie. 90 + 90 + 90 + (something less than 90) */
            for (i = 0; i < 4; i++)
            {
                float current = startAngle + drawn;
                float additional;
                if (enough)
                    return;
                additional = endAngle - current; /* otherwise, add the remainder */
                if (Math.Abs(additional) > 90)
                {
                    additional = increment;
                }
                else
                {
                    /* a near zero value will introduce bad artefact in the drawing (#78999) */
                    if (gdip_near_zero(additional))
                        return;
                    enough = true;
                }
                append_arc((i == 0), /* only move to the starting pt in the 1st iteration */
                         x, y, width, height, /* bounding rectangle */
                         current, current + additional);
                drawn += additional;
            }
        }
        private void append_ellipse(float x, float y, float width, float height)
        {
            float rx = width / 2.0f;
            float ry = height / 2.0f;
            float cx = x + rx;
            float cy = y + ry;
            const float C1 = 0.552285f;
            //const float C2 = 0.552285f;
            /* origin */
            append(cx + rx, cy,
                enuGdiGraphicPathType.StartFigure, false);
            /* quadrant I */
            append_bezier(
                           cx + rx,
                           cy - C1 * ry,
                           cx + C1 * rx,
                           cy - ry,
                           cx,
                           cy - ry);
            /* quadrant II */
            append_bezier(
                           cx - C1 * rx, cy - ry,
                           cx - rx, cy - C1 * ry,
                           cx - rx, cy);
            /* quadrant III */
            append_bezier(
                           cx - rx, cy + C1 * ry,
                           cx - C1 * rx, cy + ry,
                           cx, cy + ry);
            /* quadrant IV */
            append_bezier(
                           cx + C1 * rx, cy + ry,
                           cx + rx, cy + C1 * ry,
                           cx + rx, cy);
            /* close the path */
            close_figure();
        }
        protected static bool gdip_near_zero(float value)
        {
            return ((value >= -0.0001f) && (value <= 0.0001f));
        }
        protected void  append_arc (bool start, float x, float y, float width, float height, float startAngle, float endAngle)
{
        const float _2PI = (float)(Math.PI * 2);
        const float M_PI = 3.14159265358979323846f;
        float delta, bcp;
        double sin_alpha, sin_beta, cos_alpha, cos_beta;
        float rx = width / 2;
        float ry = height / 2;
        /* center */
        float cx = x + rx;
        float cy = y + ry;
        /* angles in radians */
        float alpha = (float)(startAngle * Math.PI / 180.0f);
        float beta = (float)(endAngle * Math.PI / 180.0f);
        /* adjust angles for ellipses */
        alpha = (float)Math.Atan2(rx * Math.Sin(alpha), ry * Math.Cos(alpha));
        beta = (float)Math.Atan2 (rx * Math.Sin (beta), ry * Math.Cos (beta));
        if (Math.Abs(beta - alpha) > M_PI){
            if (beta > alpha)
                beta -= _2PI;
            else
                alpha -= _2PI;
        }
        delta = beta - alpha;
        //// http://www.stillhq.com/ctpfaq/2001/comp.text.pdf-faq-2001-04.txt (section 2.13)
        bcp =(float)( 4.0 / 3 * (1 - Math.Cos (delta / 2)) / Math.Sin (delta / 2));
        sin_alpha = Math.Sin (alpha);
        sin_beta = Math.Sin (beta);
        cos_alpha = Math.Cos (alpha);
        cos_beta = Math.Cos (beta);
        /* move to the starting point if we're not continuing a curve */
        if (start) {
                /* starting point */
                float sx = (float)(cx + rx * cos_alpha);
                float  sy = (float)(cy + ry * sin_alpha);
                append ( sx, sy, enuGdiGraphicPathType.LinePoint , false );
        }
        append_bezier (
               (float )( cx + rx * (cos_alpha - bcp * sin_alpha)),
               (float )( cy + ry * (sin_alpha + bcp * cos_alpha)),
                (float )(cx + rx * (cos_beta + bcp * sin_beta)),
                (float )(cy + ry * (sin_beta - bcp * cos_beta)),
                (float )(cx + rx * cos_beta),
                (float )(cy + ry * sin_beta));
}
        protected void close_figure()
        {
            if (this.m_points.Count > 0)
            {
                this.m_bytes[this.m_bytes.Count - 1] |= (byte)enuGdiGraphicPathType.EndPoint;
            }
        }
        internal static Vector2f [] GetCurveTangents(int terms, Vector2f[] points, int count, float tension, bool open)
        {
            float coefficient = tension /3f;
            Vector2f[] tangents = new Vector2f[count];
            if (count <= 2)
                return tangents;
            for (int i = 0; i < count; i++)
            {
                int r = i + 1;
                int s = i - 1;
                if (r >= count)
                {
                    if (!open)
                    r = 0;// count - 1;
                    else
                    r = count - 1;
                }
                if (open)
                {
                    if (s < 0)
                        s = 0;
                }
                else
                {
                    if (s < 0)
                        s += count;
                }
                tangents[i].X += (coefficient * (points[r].X - points[s].X));
                tangents[i].Y += (coefficient * (points[r].Y - points[s].Y));
            }
            return tangents;
        }

        public override void CloseFigure()
        {
            this.close_figure();
        }


                  public override bool IsClosed
        {
            get {
                return (this.m_bytes  == null) ||

               (this.m_bytes[this.m_bytes.Count - 1] & (byte)enuGdiGraphicPathType.EndPoint) == (byte)enuGdiGraphicPathType.EndPoint; 
            }
        }
            
    }
}

