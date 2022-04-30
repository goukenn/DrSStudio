

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: RectangleSegment.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore;
using IGK.ICore.GraphicModels;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:RectangleSegment.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.Drawing2D.Segments
{
    public class RectangleSegment : CorePathSegmentBase
    {
        private Vector2f[]  m_rectanglePts;

        public override void SetPoint(int index, int def)
        {
            
        }
        public Vector2f[]  RectanglePoint{
            get { return m_rectanglePts; }
        }
        public override void Invert()
        {
            List<Vector2f> p = new List<Vector2f>();
            p.AddRange(m_rectanglePts);
            m_rectanglePts = p.ToArray();
        }
        public override enuSegmentType SegmentType
        {
            get { return enuSegmentType.Rectangle; }
        }
        public override void Transform(Matrix matrix)
        {
            this.m_rectanglePts = CoreMathOperation.TransformVector2fPoint(matrix, m_rectanglePts.ToArray());
        }
        public RectangleSegment(Rectanglef rectangle)
        {
            this.m_rectanglePts = CoreMathOperation.GetPoints(rectangle);
        }
        public override Vector2f[] GetPathPoints()
        {
            return this.m_rectanglePts;
        }
        public override byte[] GetPathTypes()
        {
            return new byte[] { 
                (byte) enuGdiGraphicPathType.StartFigure ,
                (byte) enuGdiGraphicPathType.LinePoint  ,
                (byte) enuGdiGraphicPathType.LinePoint ,
                (byte) (enuGdiGraphicPathType.LinePoint  | enuGdiGraphicPathType.EndPoint )
            };
        }
        public override void CloseFigure()
        {
        }

       

        public override bool IsClosed
        {
            get { return true; }
        }
    }
}

