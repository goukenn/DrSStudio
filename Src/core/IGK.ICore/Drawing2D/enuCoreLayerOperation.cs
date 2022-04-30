

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuCoreLayerOperation.cs
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
file:enuCoreLayerOperation.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Drawing2D
{
    /// <summary>
    /// reprensent a core layer operation
    /// </summary>
    public enum enuCoreLayerOperation
    {
        None = 0,
        Negate = 1, //negate the layer
        Add = 2,
        Replace = 3, //replace layer with old value
        Xor = 4,    //xor layer value
        SubSrc = 5,
        SubDest = 6,
        //considering alpha componanent of the layer
        AddMask = 7,
        ReplaceMask = 8,
        XorMask = 9,
        SubsrcMask = 10,
        SubdestMask = 11
    }
}

