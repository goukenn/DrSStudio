

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuVCMError.cs
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
file:enuVCMError.cs
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
using System.Linq;
using System.Text;
namespace IGK.AVIApi.VCM
{
    public enum  enuVCMError : int
    {
        NoError = VCMApi .ICERR_OK ,
DontDraw = VCMApi . ICERR_DONTDRAW     ,
NewPalette = VCMApi . ICERR_NEWPALETTE   ,
GoToKeyFrame = VCMApi . ICERR_GOTOKEYFRAME,
StopDrawing = VCMApi . ICERR_STOPDRAWING ,
Unsupported = VCMApi . ICERR_UNSUPPORTED  ,
BadFormat = VCMApi . ICERR_BADFORMAT    ,
Memory = VCMApi . ICERR_MEMORY       ,
Internal = VCMApi . ICERR_INTERNAL     ,
BadFlags = VCMApi . ICERR_BADFLAGS     ,
BadParam = VCMApi . ICERR_BADPARAM     ,
BadSize = VCMApi . ICERR_BADSIZE      ,
BadHandle = VCMApi . ICERR_BADHANDLE    ,
CantUpdate = VCMApi . ICERR_CANTUPDATE   ,
Abort = VCMApi . ICERR_ABORT	     , 
Error = VCMApi . ICERR_ERROR        ,
BadBitDepth = VCMApi . ICERR_BADBITDEPTH  ,
BadImageSize = VCMApi . ICERR_BADIMAGESIZE ,
Custom = VCMApi . ICERR_CUSTOM       
    }
}

