

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GraphicsPrimitives.cs
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
file:GraphicsPrimitives.cs
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
    /// <summary>
    /// represent a draw primitive
    /// </summary>
    public enum enuGraphicsPrimitives :uint
    {
        Points = GL.GL_POINTS ,
        Polygon = GL.GL_POLYGON ,
        Lines = GL.GL_LINES ,
        LineStrip = GL.GL_LINE_STRIP,
        LineLoop = GL.GL_LINE_LOOP,
        Triangles = GL.GL_TRIANGLES,
        TriangleFan = GL.GL_TRIANGLE_FAN ,
        TriangleStrip = GL.GL_TRIANGLE_STRIP ,
        Quads =  GL.GL_QUADS,
        QuadStrip = GL.GL_QUAD_STRIP
    }
}

