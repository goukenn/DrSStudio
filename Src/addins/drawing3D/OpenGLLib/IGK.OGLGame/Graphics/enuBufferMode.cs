

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuBufferMode.cs
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
file:enuBufferMode.cs
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

using IGK.ICore;using IGK.GLLib;
namespace IGK.OGLGame.Graphics
{
    [Flags ()]
    public enum enuBufferMode : uint
    {
        None = GL.GL_NONE,
        FrontLeft = GL.GL_FRONT_LEFT, 
        FrontRight = GL.GL_FRONT_RIGHT, 
        BackLeft = GL.GL_BACK_LEFT, 
        BackRight = GL.GL_BACK_RIGHT, 
        Front = GL.GL_FRONT,
        Back = GL.GL_BACK, 
        Left = GL.GL_LEFT, 
        Right = GL.GL_RIGHT, 
        FrontAndBack = GL.GL_FRONT_AND_BACK,
        Aux = GL.GL_AUX0
    }
}

