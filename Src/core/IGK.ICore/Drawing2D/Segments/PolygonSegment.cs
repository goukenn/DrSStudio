

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PolygonSegment.cs
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
file:PolygonSegment.cs
*/
using IGK.ICore;using IGK.ICore.GraphicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.Drawing2D.Segments
{
    class PolygonSegment : PointSegment
    {
        private Vector2f[] m_points;
        private float m_Tension;
        private bool m_withTension;
        private bool m_add_ClosePoint;

        public float Tension { get { return this.m_Tension; } }
        public Vector2f[] Points { get { return m_points; } }
        public bool WithTension { get { return this.m_withTension; } }
        public bool AddClosedPoint { get { return this.m_add_ClosePoint; } }


        public PolygonSegment(Vector2f[] points):this(points, false )
        {
   
        }
        public PolygonSegment(Vector2f[] points, bool addClosedPoint)
        {
                   if ((points == null) || (points.Length < 2))
                throw new CoreException("points is null");
            this.m_points = points;
            this.m_withTension = false;
            this.m_add_ClosePoint = addClosedPoint ;
            this.BuildPath();
        }
        
        public PolygonSegment(Vector2f[] points, float Tension)
        {
            if ((points == null) || (points.Length < 2))
                throw new ArgumentException($"{nameof(points)}");
            this.m_Tension = Tension;
            this.m_points = points;
            this.m_withTension = true;
            this.m_add_ClosePoint = false;
            this.BuildPath();
        }
        private void BuildPath()
        {
            if (this.m_withTension)
            {                
                //for gdi compatibility
                var tangents = GetCurveTangents(1, this.m_points, this.m_points.Length, this.m_Tension, false);
                appendCurve(this.m_points, tangents, 0, this.m_points.Length - 1, true );
            }
            else
            {
                append(m_points[0].X, m_points[0].Y, enuGdiGraphicPathType.StartFigure, true);
                for (int i = 1; i < m_points.Length; i++)
                {
                    append(m_points[i].X , m_points[i].Y, enuGdiGraphicPathType.LinePoint, false  );
                }
                // Add a line from the last point back to the first point if
                // they're not the same
                var last = m_points[m_points.Length - 1];
                
                if ((m_points[0] != last) && m_add_ClosePoint )
                {
                    append(m_points[0].X, m_points[0].Y, enuGdiGraphicPathType.LinePoint, true);
                }
                /* close the path */
                close_figure();
            }
        }
        public override enuSegmentType SegmentType
        {
            get { return enuSegmentType.Polygon; }
        }
     
        public override Vector2f[] GetPathPoints()
        {
            return base.GetPathPoints();
        }
        public override byte[] GetPathTypes()
        {
            return base.GetPathTypes();
        }
    }
}

