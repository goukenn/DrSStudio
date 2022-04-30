

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuMMIOError.cs
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
file:enuMMIOError.cs
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
namespace IGK.AVIApi.MMIO
{
    /// <summary>
    /// represent the mmio errro code
    /// </summary>
    public enum enuMMIOError
    {
        NoError = 0,
        FileNotFound = MMIOApi.MMIOERR_FILENOTFOUND,
        OutOfMemory = MMIOApi.MMIOERR_OUTOFMEMORY,
        CanNotOpen = MMIOApi.MMIOERR_CANNOTOPEN,
        CanNotClose = MMIOApi.MMIOERR_CANNOTCLOSE,
        CanNotRead = MMIOApi.MMIOERR_CANNOTREAD,
        CanNotWrite = MMIOApi.MMIOERR_CANNOTWRITE,
        CanNotSeek = MMIOApi.MMIOERR_CANNOTSEEK,
        CanNotExpand = MMIOApi.MMIOERR_CANNOTEXPAND,
        ChunkNotFound = MMIOApi.MMIOERR_CHUNKNOTFOUND,
        UnBuffered = MMIOApi.MMIOERR_UNBUFFERED,
        PathNotFound = MMIOApi.MMIOERR_PATHNOTFOUND,
        AccessDenied = MMIOApi.MMIOERR_ACCESSDENIED,
        SharingViolation = MMIOApi.MMIOERR_SHARINGVIOLATION,
        NetWorkError = MMIOApi.MMIOERR_NETWORKERROR,
        TooManyOpenFiles = MMIOApi.MMIOERR_TOOMANYOPENFILES,
        InvalidFile = MMIOApi.MMIOERR_INVALIDFILE
    }
}

