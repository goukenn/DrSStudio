

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PMMCHKInfo.cs
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
file:PMMCHKInfo.cs
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
    /// represent the MMIO chunck info
    /// </summary>
    public class PMMCHKInfo
    {
        internal MMIOApi.MMIOChunkINFO m_info;
        internal static PMMCHKInfo Create(MMIOApi .MMIOChunkINFO info)
        {
            PMMCHKInfo v = new PMMCHKInfo();
            v.m_info = info;
            return v;
        }
        public int CKId { get { return this.m_info.ckid; } }
        public int Size { get { return this.m_info.cksize; } }
        public int DataOffset { get { return this.m_info.dwDataOffset; } }
        public int DataType { get { return this.m_info.fccType; } }
        public enuMMIOCKInfoflag Flag { get { return (enuMMIOCKInfoflag)this.m_info.dwFlags; } }
        public string CIdName { get { return MMIOApi.getChunckString(this.m_info.ckid); } }
    }
}

