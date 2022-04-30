

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuACMMetrics.cs
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
file:enuACMMetrics.cs
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
    public enum enuACMMetrics
    {
        CountDrivers = ACMApi.        ACM_METRIC_COUNT_DRIVERS ,
        CountCodecs = ACMApi. ACM_METRIC_COUNT_CODECS ,
        CountConverters = ACMApi. ACM_METRIC_COUNT_CONVERTERS ,
        CountFilters = ACMApi. ACM_METRIC_COUNT_FILTERS ,
        CountDisabled = ACMApi. ACM_METRIC_COUNT_DISABLED ,
        CountHardWare = ACMApi. ACM_METRIC_COUNT_HARDWARE ,
        CountLocalDrivers = ACMApi. ACM_METRIC_COUNT_LOCAL_DRIVERS ,
        CountLocalCodecs = ACMApi. ACM_METRIC_COUNT_LOCAL_CODECS ,
        CountLocalConverters = ACMApi. ACM_METRIC_COUNT_LOCAL_CONVERTERS ,
        CountLocalFilters = ACMApi. ACM_METRIC_COUNT_LOCAL_FILTERS ,
        CountLocalDisabled = ACMApi. ACM_METRIC_COUNT_LOCAL_DISABLED ,
        HardWareWaveInput = ACMApi. ACM_METRIC_HARDWARE_WAVE_INPUT ,
        HardWareWaveOutPut = ACMApi. ACM_METRIC_HARDWARE_WAVE_OUTPUT ,
        MaxSizeFormat = ACMApi. ACM_METRIC_MAX_SIZE_FORMAT ,
        MaxSizeFilter = ACMApi. ACM_METRIC_MAX_SIZE_FILTER ,
        DriverSupport = ACMApi. ACM_METRIC_DRIVER_SUPPORT ,
        DriverPriority = ACMApi. ACM_METRIC_DRIVER_PRIORITY ,
    }
}

