

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuGLClear.cs
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
file:enuGLClear.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2011
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.GLLib
{
    /// <summary>
    /// 
    /// </summary>
    [Flags()]
    public enum enuGLClear : uint
    {
        ColorBuffer = GL.GL_COLOR_BUFFER_BIT,
        DepthBuffer = GL.GL_DEPTH_BUFFER_BIT,
        AccumBuffer = GL.GL_ACCUM_BUFFER_BIT,
        StencilBuffer = GL.GL_STENCIL_BUFFER_BIT,
        AllBuffer = ColorBuffer | DepthBuffer | AccumBuffer | StencilBuffer
    }
}

