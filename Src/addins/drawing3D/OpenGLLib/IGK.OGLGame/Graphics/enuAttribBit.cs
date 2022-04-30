

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuAttribBit.cs
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
file:enuAttribBit.cs
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
    [Flags ()]
    public enum  enuAttribBit : uint 
    {
        AccumBuffer = GL.GL_ACCUM_BUFFER_BIT ,
        ColorBuffer = GL.GL_COLOR_BUFFER_BIT ,
        Current = GL.GL_CURRENT_BIT ,
        DepthBuffer = GL.GL_DEPTH_BUFFER_BIT ,
        Enable = GL.GL_ENABLE_BIT ,
        Eval = GL.GL_EVAL_BIT ,
        Fog = GL.GL_FOG_BIT ,
        Hint = GL.GL_HINT_BIT ,
        Lightingg = GL.GL_LIGHTING_BIT ,
        Line = GL.GL_LINE_BIT ,
        List = GL.GL_LIST_BIT ,
        Multisample = GL.GL_MULTISAMPLE_BIT ,
        PixelMode = GL.GL_PIXEL_MODE_BIT ,
        Point = GL.GL_POINT_BIT,
        Polygon = GL.GL_POLYGON_BIT ,
        PolygonStipple = GL.GL_POLYGON_STIPPLE_BIT ,
        Scissor = GL.GL_SCISSOR_BIT ,
        Stencil  = GL.GL_STENCIL_BUFFER_BIT ,
        Texture = GL.GL_TEXTURE_BIT ,
        Transform = GL.GL_TRANSFORM_BIT ,
        Viewport = GL.GL_VIEWPORT_BIT ,
        All = GL.GL_ALL_ATTRIB_BITS ,
    }
}

