

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuGLQuadricNormalStyle.cs
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
file:enuGLQuadricNormalStyle.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.GLLib
{
    public enum  enuGLQuadricNormalStyle : uint
    {
        None = GLU.GLU_NONE ,
        Line = GLU.GLU_FLAT ,
        Smooth = GLU.GLU_SMOOTH 
    }
}

