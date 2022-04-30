

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuSegmentType.cs
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
file:enuSegmentType.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.GraphicModels
{
    /// <summary>
    /// represent a graphics segment type
    /// </summary>
    public enum enuSegmentType
    {
        /// <summary>
        /// represent a line segment. 2 point
        /// </summary>
        Line,
        /// <summary>
        /// represent a Points segment minumum of 2 points
        /// </summary>
        Points,
        /// <summary>
        /// represent a polygon segment
        /// </summary>
        Polygon,
        /// <summary>
        /// represent a besier segment
        /// </summary>
        Bezier,
        /// <summary>
        /// represent aRectangleisegment
        /// </summary>
        Rectangle,
        /// <summary>
        /// represent a arc segment
        /// </summary>
        Arc,
        /// <summary>
        /// represent a ellipse/Circle segment
        /// </summary>
        Ellipse,
        /// <summary>
        /// represent a path string segment
        /// </summary>
        String,
        /// <summary>
        /// represent a end segement marker
        /// </summary>
        End,
        /// <summary>
        /// represent a pie segment
        /// </summary>
        Pie,
        /// <summary>
        /// represent a custom segment type
        /// </summary>
        Custom,
        /// <summary>
        /// represent a graphics path
        /// </summary>
        GraphicsPath,
        /// <summary>
        /// represent a text path element
        /// </summary>
        TextPath
    }
}

