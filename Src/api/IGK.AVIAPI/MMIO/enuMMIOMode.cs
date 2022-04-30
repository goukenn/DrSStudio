

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuMMIOMode.cs
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
file:enuMMIOMode.cs
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
    [Flags()]
    public enum enuMMIOMode
    {
        CreateNew = enuMMIOOpenFlag .Create ,
        AllocBuff = MMIOApi.MMIO_ALLOCBUF,
        Compat = MMIOApi.MMIO_COMPAT,
        Create = MMIOApi.MMIO_CREATE,
        Delete = MMIOApi.MMIO_DELETE,
        DenyNone = MMIOApi.MMIO_DENYNONE,
        DenyRead = MMIOApi.MMIO_DENYREAD,
        DenyWrite = MMIOApi.MMIO_DENYWRITE,
        Exclusive = MMIOApi.MMIO_EXCLUSIVE,
        Exists = MMIOApi.MMIO_EXIST,
        GetTemp = MMIOApi.MMIO_GETTEMP,
        Parse = MMIOApi.MMIO_PARSE,
        Read = MMIOApi.MMIO_READ,
        ReadWrite = MMIOApi.MMIO_READWRITE,
        Write = MMIOApi.MMIO_WRITE
    }
}

