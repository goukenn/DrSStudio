

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuACMResult.cs
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
file:enuACMResult.cs
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
namespace IGK.AVIApi.ACM
{
    using MMSYSTEM;
    public enum enuACMResult
    {
        NoError = 0,
        InvalidHandle = mmSystemAPI .MMSYSERR_INVALHANDLE,
        InvalidFlag = mmSystemAPI.MMSYSERR_INVALFLAG ,
        NotEnabled  = mmSystemAPI.MMSYSERR_NOTENABLED,
        NoDriver = mmSystemAPI.MMSYSERR_NODRIVER,
        NotPossible = ACMApi.ACMERR_NOTPOSSIBLE ,
        Buzy = ACMApi.ACMERR_BUSY ,
        Cancelled = ACMApi.ACMERR_CANCELED ,
        InvalidParam = NoError + 11,
    }
}

