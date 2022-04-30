

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: BlendFactorEXT.cs
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
file:BlendFactorEXT.cs
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

using IGK.ICore;using IGK.GLLib;
namespace IGK.OGLGame.Graphics
{
    public enum BlendFactorEXT : uint
    {
        ConstantColor = GL.GL_CONSTANT_COLOR_EXT,
        OneMinusConstColor = GL.GL_ONE_MINUS_CONSTANT_COLOR_EXT,
        ConstantAlpha = GL.GL_CONSTANT_ALPHA_EXT,
        OnMinusConstAlpha = GL.GL_ONE_MINUS_CONSTANT_ALPHA_EXT
    }
}

