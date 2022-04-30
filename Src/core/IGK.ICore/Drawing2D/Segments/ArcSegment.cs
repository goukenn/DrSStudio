

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ArcSegment.cs
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
file:ArcSegment.cs
*/
using IGK.ICore;using IGK.ICore.GraphicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Drawing2D.Segments
{
    /// <summary>
    /// represent arc segment
    /// </summary>
    public class ArcSegment : PointSegment
    {
        protected bool m_closed;
        protected Vector2f m_center;
        protected Vector2f m_radius;
        protected float m_startAngle;
        protected float m_sweepAngle;


        public bool Closed { get { return m_closed; } }
        public Vector2f Center { get { return this.m_center; } }
        public Vector2f Radius { get { return this.m_radius; } }
        public float StartAngle { get { return this.m_startAngle; } }
        public float SweepAngle { get { return this.m_sweepAngle; } }

        public ArcSegment(Vector2f center, Vector2f radius, float startAngle, float sweepAngle, bool closed):
            base()
        {
            this.m_closed = closed;
            this.m_center = center;
            this.m_radius = radius;
            this.m_startAngle = startAngle;
            this.m_sweepAngle = sweepAngle;
            BuildPath();
        }
        protected  void BuildPath()
        {
            Rectanglef rc = new Rectanglef(
                (float)(m_center.X - Math.Abs (m_radius.X )),
                (float)(m_center.Y - Math.Abs (m_radius.Y )),
                (float)(Math.Abs (2* m_radius.X )),
                (float)(Math.Abs (2* m_radius.Y ))
                );
            if (rc.IsEmpty )
            {
                append(rc.X, rc.Y, enuGdiGraphicPathType.StartFigure, false);
                append(rc.X + rc.Width , rc.Y + rc.Height , enuGdiGraphicPathType.LinePoint , false);
                close_figure();
            }
            else
            {
                this.append_arcs(
                    rc.X,
                    rc.Y,
                    rc.Width,
                    rc.Height,
                    this.m_startAngle,
                    this.m_sweepAngle);
               if (this.m_closed )
                    close_figure();
            }
        }
        public override enuSegmentType SegmentType
        {
            get { return enuSegmentType.Arc; }
        }
    }
}

