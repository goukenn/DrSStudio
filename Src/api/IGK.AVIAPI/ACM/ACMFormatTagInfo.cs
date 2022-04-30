

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ACMFormatTagInfo.cs
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
file:ACMFormatTagInfo.cs
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
    public class ACMFormatTagInfo
    {
        private ACMDriverInfo m_driver;
        private ACMApi.ACMFORMATTAGDETAILS m_tagdetails;
        internal static ACMFormatTagInfo Create(ACMDriverInfo driver,
            ACMApi.ACMFORMATTAGDETAILS formattagdetails)
        {
            ACMFormatTagInfo v_m = new ACMFormatTagInfo();
            v_m.m_driver = driver;
            v_m.m_tagdetails = formattagdetails;
            return v_m; 
        }
        private ACMFormatTagInfo()
        {
        }
        public string DisplayName { get { return this.m_tagdetails.szFormatTag; } }
        public enuACMWaveFormat FormatTag {get{return (enuACMWaveFormat)this.m_tagdetails .dwFormatTag ;}}
        public int Index { get { return this.m_tagdetails.dwFormatTagIndex; } }
        public int FormatSize { get { return this.m_tagdetails.cbFormatSize; } }
        public int StandardFormats { get { return this.m_tagdetails.cStandardFormats; } }
        public enuACMSupportFlag Support { get { return (enuACMSupportFlag)this.m_tagdetails.fdwSupport; } }
        public override string ToString()
        {
            return this.DisplayName + "("+this.Support +")";
        }
    }
}

