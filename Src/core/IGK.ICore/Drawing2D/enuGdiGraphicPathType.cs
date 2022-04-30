

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuGdiGraphicPathType.cs
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
file:enuGdiGraphicPathType.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Drawing2D
{
    [Flags()]
    /// <summary>
    /// represent studied gdi path TYPES
    /// </summary>
    public enum enuGdiGraphicPathType : byte
    {
        StartFigure = 0,
        LinePoint = 1, //line control moint
        ControlPoint = 3,//bezier control point
        Mask = 0x7,//masking. not used
        Marker= 0x20, //marker for path iteration
        EndPoint = 0x80 // end point
    }
}

