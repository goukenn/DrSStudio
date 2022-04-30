

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuAviError.cs
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
file:enuAviError.cs
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
    [Flags ()]
    public enum enuAviError: uint
    {
        NoError =  0,
     //   Unknow = 0x8000FFFF,
       ClassNotReg  =(uint)0x80040154,
       UnSupported = AVIApi.       AVIERR_UNSUPPORTED ,
          BadFormat = AVIApi. AVIERR_BADFORMAT      ,
          Memory = AVIApi. AVIERR_MEMORY ,
          Internal = AVIApi. AVIERR_INTERNAL,
          BadFlags = AVIApi. AVIERR_BADFLAGS ,
          BadParam = AVIApi. AVIERR_BADPARAM  ,
          BadSize = AVIApi. AVIERR_BADSIZE    ,
          BadHandle = AVIApi. AVIERR_BADHANDLE  ,
          FileRead = AVIApi. AVIERR_FILEREAD   ,
          FileWrite  = AVIApi. AVIERR_FILEWRITE   ,
          FileOpen = AVIApi. AVIERR_FILEOPEN     ,
          Compressor = AVIApi. AVIERR_COMPRESSOR    ,
          NoCompressor = AVIApi. AVIERR_NOCOMPRESSOR  ,
          ReadOnly = AVIApi. AVIERR_READONLY	,
          NoData = AVIApi. AVIERR_NODATA		,
          BufferTooSmall = AVIApi. AVIERR_BUFFERTOOSMALL	,
          CantComporess = AVIApi. AVIERR_CANTCOMPRESS,
          UserAbort = AVIApi. AVIERR_USERABORT   ,
          Error = AVIApi. AVIERR_ERROR       ,
          FileIsReadOnly = AVIApi.AVIERR_READONLY,
        DFD = 2147762285
    } 
}

