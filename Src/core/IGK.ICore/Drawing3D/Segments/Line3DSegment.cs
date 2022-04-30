

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Line3DSegment.cs
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
file:Line3DSegment.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.Drawing3D.Segments
{

    /// <summary>
    /// for drawing 3D segment
    /// </summary>
    public sealed class Line3DSegment : Core3DPathSegmentBase
    {
        private Vector3f startPoint;
        private Vector3f endPoint;

        public override void SetPoint(int index, int def)
        {
            
        }
        public Line3DSegment(Vector3f startPoint, Vector3f endPoint)
        {
            this.startPoint = startPoint;
            this.endPoint = endPoint;
        }
        public override Vector3f[] Get3DPoints()
        {
            return new Vector3f[]{
            this.startPoint,
            this.endPoint };
        }
        public override enuSegmentType SegmentType
        {
            get { return enuSegmentType.Line; }
        }
   
        public override Vector2f[] GetPathPoints()
        {
            return null;
        }
        public override byte[] GetPathTypes()
        {
            return null;
        }
        public override void Transform(Matrix matrix)
        {
            
        }
        public override void Invert()
        {
            //do nothing
        }

        public override void CloseFigure()
        {
            //do nothing
        }

        public override bool IsClosed
        {
            get { return false; }
        }
    }
}

