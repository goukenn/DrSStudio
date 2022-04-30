

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuStencilOp.cs
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
file:enuStencilOp.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.OGLGame.Graphics
{
    
using IGK.ICore;using IGK.GLLib;
    /// <summary>
    /// reprensent the stencil function 
    /// </summary>
    public enum  enuStencilOp : uint
    {
        Keep = GL.GL_KEEP ,
        Zero = GL.GL_ZERO ,
        Replace = GL.GL_REPLACE ,
        Increment = GL.GL_INCR ,
        Decrement = GL.GL_DECR ,
        IncWrap = GL.GL_INCR_WRAP ,
        DecWrap = GL.GL_DECR_WRAP ,        
        Invert = GL.GL_INVERT 
    }
}

