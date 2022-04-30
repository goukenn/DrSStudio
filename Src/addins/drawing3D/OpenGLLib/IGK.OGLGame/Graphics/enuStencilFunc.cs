

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuStencilFunc.cs
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
file:enuStencilFunc.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.OGLGame.Graphics
{
    
using IGK.ICore;using IGK.GLLib ;
    /// <summary>
    /// reprensent the stencil function 
    /// </summary>
    public enum enuStencilFunc : uint
    {
        Never = GL.GL_NEVER ,
        Always = GL.GL_ALWAYS ,
        Less = GL.GL_LESS ,
        LEqual = GL.GL_LEQUAL ,
        Equal = GL.GL_EQUAL ,
        GEqual = GL.GL_GEQUAL ,
        Greater = GL.GL_GREATER ,
        NotEqual = GL.GL_NOTEQUAL 
    }
}

