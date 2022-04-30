

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ACMFilterInfo.cs
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
file:ACMFilterInfo.cs
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
    /// <summary>
    /// represent the filter information
    /// </summary>
    public sealed class ACMFilterInfo
    {
        ACMDriverInfo m_driverInfo;
        ACMApi.ACMFILTERDETAILS m_filterDetails;
        private ACMFilterInfo()
        {
        }
        internal static ACMFilterInfo Create(ACMDriverInfo driver, 
            ACMApi.ACMFILTERDETAILS details)
        {
            ACMFilterInfo v_i = new ACMFilterInfo();
            v_i.m_driverInfo = driver;
            v_i.m_filterDetails = details;
            return v_i;
        }
        public string DisplayName { get { return this.m_filterDetails.szFilter ; } }
        public override string ToString()
        {
            return this.DisplayName;
        }
    }
}

