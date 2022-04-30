

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: BlendEquation.cs
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
file:BlendEquation.cs
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
    public enum BlendEquation : uint
    {
        Add = GL.GL_FUNC_ADD ,
        Subtract = GL.GL_FUNC_SUBTRACT ,
        ReverseSubtract = GL.GL_FUNC_REVERSE_SUBTRACT ,
        Min = GL.GL_MIN ,
        Max = GL.GL_MAX,
        ////extention blend
        //[System.ComponentModel.Browsable (false )]
        //[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        //AddEXT = GL.GL_FUNC_ADD_EXT,
        //[System.ComponentModel.Browsable(false)]
        //[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        //SubtractEXT = GL.GL_FUNC_SUBTRACT_EXT ,
        //[System.ComponentModel.Browsable(false)]
        //[System.ComponentModel.EditorBrowsable( System.ComponentModel.EditorBrowsableState.Never )]
        //ReverseSubtractEXT = GL.GL_FUNC_REVERSE_SUBTRACT_EXT,
        //[System.ComponentModel.Browsable(false)]
        //[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        //MinEXT = GL.GL_MIN_EXT ,
        //[System.ComponentModel.Browsable(false)]
        //[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        //MaxEXT = GL.GL_MAX_EXT
    }
}

