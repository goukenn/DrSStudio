

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuGLConvolutionType.cs
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
file:enuGLConvolutionType.cs
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
namespace IGK.OGLGame.Graphics.Imaging
{
    public enum enuGLConvolutionType : uint
    {
        Alpha = IGK.GLLib.GL.GL_ALPHA ,
        Luminance = IGK.GLLib.GL.GL_LUMINANCE ,
        LuminanceAlpah = IGK.GLLib.GL.GL_LUMINANCE_ALPHA ,
        Intensity = IGK.GLLib.GL.GL_INTENSITY ,
        RGB =  IGK.GLLib.GL.GL_RGB,
        RGBA = IGK.GLLib.GL.GL_RGBA 
    }
}

