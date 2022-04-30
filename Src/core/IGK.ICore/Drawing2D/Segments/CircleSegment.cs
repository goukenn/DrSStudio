

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CircleSegment.cs
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
file:CircleSegment.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Drawing2D.Segments
{
    /// <summary>
    /// represent a circle segment base
    /// </summary>
    public class CircleSegment : PointSegment 
    {
        private Vector2f center;
        private Vector2f radius;
        public CircleSegment(Vector2f center, Vector2f radius)
        {            
            this.center = center;
            this.radius = radius;
            BuildPath();
        }
        private void BuildPath()
        {
            
            float rx = Math.Abs (this.radius.X);
            float ry = Math.Abs (this.radius.Y);
            float cx = this.center.X;
            float cy = this.center.Y;
            float C1 = 0.552285f;
            //const float C1 = 0.447715f;
            //const float C2 = 0.552285f;
            /* origin */
            append(cx + rx, cy, 
                enuGdiGraphicPathType.StartFigure, false);


            //cy - (C1 * ry) =X;
             // C1 =    (155.2285 - 100) / 100;
            // C1*100 + 100 = 155.2285
            ///* quadrant I */
            append_bezier(
                           cx + rx,//x1
                           cy  + C1 * ry, //y1
                           cx + C1 * rx, //x2
                           cy + ry , //y2
                           cx, //x3
                           cy + ry //y3
                           );
            ///* quadrant II */
            append_bezier(
                           cx - C1 * rx, 
                           cy + ry,
                           cx - rx, 
                           cy + C1 * ry,
                           cx - rx, 
                           cy);
            ///* quadrant III */
            append_bezier(
                           cx - rx, 
                           cy - C1 * ry,
                           cx - C1 * rx, 
                           cy - ry,
                           cx, 
                           cy - ry);
            /* quadrant IV */
            append_bezier(
                           cx + C1 * rx, 
                           cy - ry,
                           cx + rx, 
                           cy - C1 * ry,
                           cx + rx, 
                           cy);
            /* close the path */
            close_figure();
            //GdipClosePathFigure(path);
        }
        public override enuSegmentType SegmentType
        {
            get { return enuSegmentType.Ellipse; }
        }
        /// <summary>
        /// get the center
        /// </summary>
        public Vector2f Center { get { return this.center; } }
        /// <summary>
        /// Get thet radius
        /// </summary>
        public Vector2f Radius { get { return this.radius; } }
      
    }
}

