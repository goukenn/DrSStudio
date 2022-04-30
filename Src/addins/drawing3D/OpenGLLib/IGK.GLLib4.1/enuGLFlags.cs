

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuGLFlags.cs
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
file:enuGLFlags.cs
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
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igk-dev.be
App : DrSStudio
powered by IGK - DEV 2008-2011
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.GLLib
{
    [Flags()]
    public enum enuGLFlags : uint
    {    //PFD flags
        DoubleBuffer = 0x00000001,
        Stereo = 0x00000002,        
        DrawToWindow = 0x00000004,
        DrawToBitmap = 0x00000008,
        SupportGdi = 0x00000010,
        SupportOpenGL = 0x00000020,
        GenericFormat =   0x00000040,        
        NeedPalette =          0x00000080,
        NeedSystemPalette =   0x00000100,
        /* PIXELFORMATDESCRIPTOR flags */
        SwapExchange         = 0x00000200,
        SwapCopy   = 0x00000400,
        SwapLayerBuffers     = 0x00000800,
        GenericAccelerated    =0x00001000,
        SupportDirectDraw    = 0x00002000,
        Direct2DAccelerated   = 0x00004000,
        SupportComposition =    0x00008000,
        /* PIXELFORMATDESCRIPTOR flags for use in ChoosePixelFormat only */
        PFD_DEPTH_DONTCARE          =0x20000000,
        PFD_DOUBLEBUFFER_DONTCARE   =0x40000000,
        PFD_STEREO_DONTCARE         =0x80000000,
        WinOpenGLDoubleBuffer = DoubleBuffer |  SupportOpenGL | DrawToWindow 
    }
}

