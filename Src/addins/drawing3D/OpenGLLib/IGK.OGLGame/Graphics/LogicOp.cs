

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: LogicOp.cs
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
file:LogicOp.cs
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
    public enum LogicOp : uint
    {
        Clear = GL.GL_CLEAR,
        Set =GL.GL_SET,
        Copy = GL.GL_COPY,
        CopyInverted = GL.GL_COPY_INVERTED,
        NoOP = GL.GL_NOOP,
        Invert = GL.GL_INVERT,
        And = GL.GL_AND,
        NAnd = GL.GL_NAND,
        Or = GL.GL_OR,
        NOr = GL.GL_NOR,
        XOr = GL.GL_XOR,
        Equiv = GL.GL_EQUIV, 
        AndReversed =GL.GL_AND_REVERSE, 
        AndInverted =GL.GL_AND_INVERTED,
        OrReverse = GL.GL_OR_REVERSE,
        OrInverted = GL.GL_OR_INVERTED
    }
}

