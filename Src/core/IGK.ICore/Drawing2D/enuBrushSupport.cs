

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuBrushSupport.cs
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
file:enuBrushSupport.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Drawing2D
{
    [Flags ()]
    public enum enuBrushSupport
    {
        //no brush support
        None = 0,
        /// <summary>
        /// stroke brush
        /// </summary>
        Stroke= 1,        
        /// <summary>
        /// element support fill brush edition property
        /// </summary>
        Fill = 2,
        /// <summary>
        /// element allow solid color
        /// </summary>
        Solid = 4,
        /// <summary>
        /// element support Hatch
        /// </summary>
        Hatch = 8,
        /// <summary>
        /// element allow linear gradient setting for the brush
        /// </summary>
        LinearGradient = 16,
        /// <summary>
        /// element allow path gradient setting for the current brush
        /// </summary>
        PathGradient = 32,
        /// <summary>
        /// element allow texture setting for the current current brush
        /// </summary>
        Texture = 64,
        /// <summary>
        /// element allow gdi setting property for current brush
        /// </summary>
        GdiStroke = 128,
        /// <summary>
        /// specification of all property
        /// </summary>
        All = Stroke | Fill| Solid| Hatch | LinearGradient|PathGradient|Texture|GdiStroke ,
        StrokeOnly = Stroke | Solid|Hatch | LinearGradient|PathGradient|Texture|GdiStroke ,
        FillOnly =    Fill| Solid| Hatch | LinearGradient|PathGradient|Texture
    }
}

