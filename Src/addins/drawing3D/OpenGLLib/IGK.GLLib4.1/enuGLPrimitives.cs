

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuGLPrimitives.cs
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
file:enuGLPrimitives.cs
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
namespace IGK.GLLib
{
///<summary>
///represent gl primitives mode
///</summary>
  public enum enuGLPrimitives : uint
    {
        Points = GL.GL_POINTS,
        Polygon = GL.GL_POLYGON ,
        Lines = GL.GL_LINES,
        LineLoop = GL.GL_LINE_LOOP ,
        LineStrip = GL.GL_LINE_STRIP ,
        Triangles = GL.GL_TRIANGLES ,
        TrianglesFan = GL.GL_TRIANGLE_FAN ,
        TrianglesStrip = GL.GL_TRIANGLE_STRIP ,
        Quads  = GL.GL_QUADS ,
        QuadStrip  = GL.GL_QUAD_STRIP
    }
}

