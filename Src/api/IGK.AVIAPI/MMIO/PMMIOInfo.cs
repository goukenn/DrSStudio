

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PMMIOInfo.cs
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
file:PMMIOInfo.cs
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
    public class PMMIOInfo
    {
        MMIOApi.MMIOINFO m_info;
        private PMMIOInfo()
        {
        }
        internal static PMMIOInfo Create(MMIOApi .MMIOINFO info)
        {
            PMMIOInfo v = new PMMIOInfo();
            v.m_info = info;
            return v;
        }
        public enuMMIOInfoFlags Flag {
            get { return (enuMMIOInfoFlags) this.m_info .dwFlags ;}
        }
        public int FourIOProc { get { return this.m_info.fccIOProc; } }
        public IntPtr IOProcHandle { get { return this.m_info.pIOProc; } }
        public long BufferSize { get { return this.m_info.cchBuffer; } }
        public IntPtr BufferHandle { get { return this.m_info.pchBuffer; } }
        public IntPtr NextHandle { get { return this.m_info.pchNext; } }
        public IntPtr EndWriteHandle { get { return this.m_info.pchEndWrite; } }
        public IntPtr EndReadHandle { get { return this.m_info.pchEndRead ; } }        
        public IntPtr IOHandle { get { return this.m_info.hmmio; } }
        public long Offset { get { return this.m_info.lDiskOffset ; } }
    }
}

