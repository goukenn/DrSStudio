

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: TextureWrapMethod.cs
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
file:TextureWrapMethod.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.OGLGame.Graphics
{
    
using IGK.ICore;using IGK.GLLib;
    public enum TextureWrapMethod :uint
    {
        Repeat = GL.GL_REPEAT ,
        MirroredRepeat = GL.GL_MIRRORED_REPEAT ,
        Clamp = GL.GL_CLAMP ,
        ClampToEdge = GL.GL_CLAMP_TO_EDGE,
        ClampToBorder  = GL.GL_CLAMP_TO_BORDER 
    }
    public enum TextureMagFilter : uint
    {
        Nearest = GL.GL_NEAREST ,
        Linear = GL.GL_LINEAR ,
        //other are used for min filter
        NearestMipmapNearest = GL.GL_NEAREST_MIPMAP_NEAREST,
        NearestMipmapLinear = GL.GL_NEAREST_MIPMAP_LINEAR ,
        LinearMipmapNearest= GL.GL_LINEAR_MIPMAP_NEAREST ,
        LinearMipmapLinear = GL.GL_LINEAR_MIPMAP_LINEAR
    }
}

