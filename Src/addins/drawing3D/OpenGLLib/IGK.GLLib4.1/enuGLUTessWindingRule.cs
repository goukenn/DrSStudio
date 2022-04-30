

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuGLUTessWindingRule.cs
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
file:enuGLUTessWindingRule.cs
*/
/*
 * enuGLUTessWindingRule.cs
 * 
 * Author: C.A.D. BONDJE DOUE
 * copyright : IGKDEV
 * */
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.GLLib
{
    public enum enuGLUTessWindingRule : uint
    {
        ODD = GLU.GLU_TESS_WINDING_ODD ,
        NonZero = GLU.GLU_TESS_WINDING_NONZERO ,
        Negative = GLU.GLU_TESS_WINDING_NEGATIVE,
        Positive = GLU.GLU_TESS_WINDING_POSITIVE,
        ABS_Geq_Two = GLU.GLU_TESS_WINDING_ABS_GEQ_TWO
    }
}

