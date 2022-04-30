

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuGLCreationFlag.cs
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
file:enuGLCreationFlag.cs
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
namespace IGK.OGLGame
{
    [Flags ()]
    public enum enuGLCreationFlag : uint
    {
        None = 0,
        DoubleBuffer = 1,
        Stereo = 2,
        DrawToWindow = 4,
        DrawToBitmap = 8,
        SupportGdi = 16,
        SupportOpenGL = 32,
        WinOpenGLDoubleBuffer = 37,
        GenericFormat = 64,
        NeedPalette = 128,
        NeedSystemPalette = 256,
        SwapExchange = 512,
        SwapCopy = 1024,
        SwapLayerBuffers = 2048,
        GenericAccelerated = 4096,
        SupportDirectDraw = 8192,
        Direct2DAccelerated = 16384,
        SupportComposition = 32768,
        PFD_DEPTH_DONTCARE = 536870912,
        PFD_DOUBLEBUFFER_DONTCARE = 1073741824,
        PFD_STEREO_DONTCARE = 2147483648,
    }
}

