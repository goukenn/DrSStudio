

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: LineSegment.cs
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
file:LineSegment.cs
*/
using IGK.ICore;using IGK.ICore.GraphicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.Drawing2D.Segments
{
    /// <summary>
    /// represent a line segment
    /// </summary>
    public sealed class LineSegment : CorePathSegmentBase
    {
        private Vector2f m_startPoint;
        private Vector2f m_endPoint;
        private bool m_closed;
        private bool m_startFigure;
        public override void Transform(Matrix matrix)
        {
            m_startPoint = CoreMathOperation.TransformVector2fPoint(matrix, m_startPoint)[0];
            m_endPoint = CoreMathOperation.TransformVector2fPoint(matrix, m_endPoint)[0];
        }
        public override void SetPoint(int index, int def)
        {
            
        }
        public override void Invert()
        {
            Vector2f p = m_startPoint;
            m_startPoint = m_endPoint;
            m_endPoint = p;
            this.m_closed = false;
        }
        public LineSegment(Vector2f startPoint, Vector2f endPoint ,bool startFigure)
        {
            this.m_startFigure = startFigure;
            this.m_startPoint = startPoint;
            this.m_endPoint = endPoint;
        }
        public override enuSegmentType SegmentType
        {
            get {
                return enuSegmentType.Line;
            }
        }
        public Vector2f StartPoint {
            get { return this.m_startPoint; }
            set { this.m_startPoint = value; }
        }
        public Vector2f EndPoint
        {
            get { return this.m_endPoint ; }
            set { this.m_endPoint  = value; }
        }
      
        public override Vector2f[] GetPathPoints()
        {
            return new Vector2f[]{
                this.m_startPoint ,
                this.m_endPoint };
        }
        public override byte[] GetPathTypes()
        {
            return new byte[]{
               (byte)( this.m_startFigure? enuGdiGraphicPathType.StartFigure : enuGdiGraphicPathType.LinePoint  ),
               (byte)(enuGdiGraphicPathType.LinePoint |(this.m_closed ?enuGdiGraphicPathType.EndPoint :enuGdiGraphicPathType.LinePoint))
            };
        }
        public override void CloseFigure()
        {
            this.m_closed = true;
        }

        public override bool IsClosed
        {
            get { return this.m_closed; }
        }
    }
}

