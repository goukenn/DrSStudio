

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: SplineSegment.cs
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
file:SplineSegment.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.Drawing2D.Segments
{
    public class SplineSegment : PointSegment
    {
        private Vector2f[] m_Points;
        private float m_Tension;
        private bool m_Closed;
        public bool Closed
        {
            get { return m_Closed; }
            set
            {
                if (m_Closed != value)
                {
                    m_Closed = value;
                }
            }
        }
        public float Tension
        {
            get { return m_Tension; }
            set
            {
                if (m_Tension != value)
                {
                    m_Tension = value;
                }
            }
        }
        public Vector2f[] Points
        {
            get { return m_Points; }
            set
            {
                if (m_Points != value)
                {
                    m_Points = value;
                }
            }
        }
        public SplineSegment(Vector2f[] points, float tension, bool closed)
        {
            if (points == null)
                throw new ArgumentNullException("points");
            this.m_Points = points ;
            this.m_Tension = tension ;
            this.m_Closed = closed;
            this.BuildPath();
        }
        private void BuildPath()
        {
            var ctangents = GetCurveTangents(1, this.Points, this.Points.Length, this.Tension, !this.Closed);
            appendCurve(this.Points, ctangents, 0, this.Points.Length, this.Closed);
            if (Closed)
                close_figure();
        }
    }
}

