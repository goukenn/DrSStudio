

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuAviAccess.cs
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
file:enuAviAccess.cs
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
namespace IGK.AVIApi.AVI
{
    [Flags()]
    public enum enuAviAccess 
    {
        Read  =AVIApi.OF_READ ,
        Write = AVIApi.OF_WRITE ,
        ReadWrite = AVIApi.OF_READWRITE ,
        Create =AVIApi.OF_CREATE ,
        ShareDenyWrite = AVIApi.OF_SHARE_DENY_WRITE ,
        ShareDenyRead = AVIApi.OF_SHARE_DENY_READ ,
        Share = AVIApi.OF_SHARE_DENY_NONE ,
        ShareCompat = AVIApi.OF_SHARE_COMPAT ,
        ShareExclusive  = AVIApi.OF_SHARE_EXCLUSIVE 
    }
}

