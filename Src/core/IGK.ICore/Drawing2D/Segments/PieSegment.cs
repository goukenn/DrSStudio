

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PieSegment.cs
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
file:PieSegment.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Drawing2D.Segments
{
    using IGK.ICore;using IGK.ICore.GraphicModels;

    public class PieSegment : PointSegment
    {
        protected Vector2f m_center;
        protected Vector2f m_radius;
        protected float m_startAngle;
        protected float m_sweepAngle;

        public Vector2f Center { get { return this.m_center; } }
        public Vector2f Radius { get { return this.m_radius; } }
        public float StartAngle { get { return this.m_startAngle; } }
        public float SweepAngle { get { return this.m_sweepAngle; } }

        public override enuSegmentType SegmentType
        {
            get { return enuSegmentType.Pie; }
        }
        public PieSegment(Vector2f  center, Vector2f radius, float startAngle , float sweepAngle)
            :base()
        {
            this.m_center = center;
            this.m_radius = radius;
            this.m_startAngle = startAngle;
            this.m_sweepAngle = sweepAngle;
            BuildPath();
        }
        protected virtual   void BuildPath()
        {
            float sin_alpha, cos_alpha;
            float rx = m_radius.X;
            float ry = m_radius.Y;


            if ((rx == 0) || (ry == 0))
            {
                return;
            }
            /* center */
            float x = this.m_center.X - m_radius.X;
            float y = this.m_center.Y - m_radius.Y;
            float cx = this.m_center.X;
            float cy = this.m_center.Y;
            float width = this.m_radius.X * 2.0f;
            float height = this.m_radius.Y * 2.0f;
            /* angles in radians */
            float alpha = (float)(m_startAngle * Math.PI / 180.0f);
            /* adjust angle for ellipses */
            alpha =(float) Math.Atan2(rx * Math.Sin(alpha), ry * Math.Cos(alpha));
            sin_alpha =(float) Math.Sin(alpha);
            cos_alpha = (float)Math.Cos(alpha);
            /* move to center */
            append( cx, cy, enuGdiGraphicPathType.StartFigure, false);
            /* draw pie edge */
            if (Math.Abs (m_sweepAngle) < 360)
                append(cx + rx * cos_alpha, cy + ry * sin_alpha, enuGdiGraphicPathType.LinePoint , false );
            /* draw the arcs */
            append_arcs( x, y, width, height, m_startAngle, m_sweepAngle);
            /* draw pie edge */
            if (Math.Abs(m_sweepAngle) < 360)
                append( cx, cy, enuGdiGraphicPathType.LinePoint , false );
        }
    }
}

