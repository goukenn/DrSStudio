

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuGLError.cs
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
file:enuGLError.cs
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
    public enum enuGLError : uint
    {
        NoError = 0,
        InvalidEnumeration = GL.GL_INVALID_ENUM ,
        InvalidOperation = GL.GL_INVALID_OPERATION ,
        InvalidValue  = GL.GL_INVALID_VALUE ,
        StackOverflow = GL. GL_STACK_OVERFLOW ,
        StackUnderFlow = GL.GL_STACK_UNDERFLOW,
        OutOfMemory = GL.GL_OUT_OF_MEMORY ,
        TableTooLarge = GL.GL_TABLE_TOO_LARGE,       
        NotImplement
    }
}

