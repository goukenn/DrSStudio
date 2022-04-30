

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: BlendFactor.cs
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
file:BlendFactor.cs
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
/* 
-------------------------------------------------------------
This file is part of iGK-DEV-DrawingStudio
-------------------------------------------------------------
-------------------------------------------------------------
-------------------------------------------------------------
view license file in Documentation folder to get more info
Copyright (c) 2008-2010 
Author  : Charles A.D. BONDJE DOUE 
mail : bondje.doue@hotmail.com
-------------------------------------------------------------
-------------------------------------------------------------
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.OGLGame.Graphics
{
    
using IGK.ICore;using IGK.GLLib;
    public enum BlendFactor : uint
    {
        Zero = GL.GL_ZERO ,
        One = GL.GL_ONE ,
        SourceColor = GL.GL_SRC_COLOR ,
        OneMinusSourceColor = GL.GL_ONE_MINUS_SRC_COLOR,
        DestinationColor = GL.GL_DST_COLOR,
        OneMinusDestinationColor = GL.GL_ONE_MINUS_DST_COLOR,
        SourceAlpha = GL.GL_SRC_ALPHA,
        OneMinusSourceAlpha = GL.GL_ONE_MINUS_SRC_ALPHA,
        DestinationAlpha = GL.GL_DST_ALPHA,
        OneMinusDestinationAlpha = GL.GL_ONE_MINUS_DST_ALPHA,
        //constant
        ConstantColor = GL.GL_CONSTANT_COLOR,
        OneMinusConstantColor = GL.GL_ONE_MINUS_CONSTANT_COLOR,
        ConstantAlpha = GL.GL_CONSTANT_ALPHA,
        OneMinusConstantAlpha = GL.GL_ONE_MINUS_CONSTANT_ALPHA,
        //add
        SourceAlphaSaturate  = GL.GL_SRC_ALPHA_SATURATE 
     }
}

