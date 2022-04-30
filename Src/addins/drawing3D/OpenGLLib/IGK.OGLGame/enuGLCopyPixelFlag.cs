

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuGLCopyPixelFlag.cs
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
file:enuGLCopyPixelFlag.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.OGLGame
{
    
using IGK.ICore;using IGK.GLLib ;
    /// <summary>
    /// represent a GL Stencil
    /// </summary>
    public enum enuGLCopyPixelFlag : uint
    {
        Color = GL.GL_COLOR ,
        Depth = GL.GL_DEPTH ,
        Stencil  = GL.GL_STENCIL 
    }
}

