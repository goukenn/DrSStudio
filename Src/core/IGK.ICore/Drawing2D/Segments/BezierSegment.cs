

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: BezierSegment.cs
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
file:BezierSegment.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.Drawing2D.Segments
{
    /// <summary>
    /// segement of 4 points
    /// </summary>
    class BezierSegment : PointSegment 
    {
        private Vector2f start;
        private Vector2f Control1;
        private Vector2f Control2;
        private Vector2f endPoint;
        public BezierSegment(Vector2f start, Vector2f Control1, Vector2f Control2, Vector2f endPoint)
        {
            this.start = start;
            this.Control1 = Control1;
            this.Control2 = Control2;
            this.endPoint = endPoint;
            this.BuildPath();
        }
        private void BuildPath()
        {
            this.append(this.start.X, this.start.Y, enuGdiGraphicPathType.StartFigure, false);
            this.append_bezier(
                   this.Control1.X, this.Control1.Y,
                   this.Control2.X, this.Control2.Y,
                   this.endPoint.X, this.endPoint.Y);
        }
        public override void CloseFigure()
        {
            this.close_figure();
        }
    }
}

