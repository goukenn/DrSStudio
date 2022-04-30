

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ACMInfo.cs
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
file:ACMInfo.cs
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
    public static class ACMInfo
    {
        readonly static IntPtr Null = IntPtr.Zero;
        /// <summary>
        /// get the acm version
        /// </summary>
        public static string  Version
        {
            get { int i = ACMApi .acmGetVersion(); 
                return string.Format ("{0}.{1}.{2}",
                    (i & 0xff000000)>>24,
                    (i & 0xff0000)>>16,
                    i & 0xffff
                    );
            }
        }
        /// <summary>
        /// get number of codecs
        /// </summary>
        public static int CodecsCount {
            get {
                return ACMManager.GetMectricsi(Null,
                  enuACMMetrics.CountCodecs );
            }
        }
        /// <summary>
        /// drivers count
        /// </summary>
        public static int DriversCount
        {
            get
            {
                return ACMManager.GetMectricsi(Null,
                  enuACMMetrics.CountDrivers);
            }
        }
        public static int HardWaresCount
        {
            get
            {
                return ACMManager.GetMectricsi(Null,
                  enuACMMetrics.CountHardWare );
            }
        }
        public static int FiltersCount
        {
            get
            {
                return ACMManager.GetMectricsi(Null,
                  enuACMMetrics.CountFilters);
            }
        }
        public static int LocalDriversCount
        {
            get
            {
                return ACMManager.GetMectricsi(Null,
                  enuACMMetrics.CountLocalDrivers );
            }
        }
        public static int LocalFiltersCount
        {
            get
            {
                return ACMManager.GetMectricsi(Null,
                  enuACMMetrics.CountLocalFilters);
            }
        }
        public static int MaxSizeFormat
        {
            get
            {
                return ACMManager.GetMectricsi(Null,
                  enuACMMetrics.MaxSizeFormat );
            }
        }
        //public static long MaxSizeFilter
        //{
        //    get
        //    {
        //        return ACMManager.GetMectricsi(Null,
        //          enuACMMetrics.MaxSizeFilter );
        //    }
        //}
        /// <summary>
        /// get number of converters
        /// </summary>
        public static int ConvertersCount
        {
            get
            {
                return ACMManager.GetMectricsi(Null,
                    enuACMMetrics.CountConverters );
            }
        }
        /// <summary>
        /// get the number of local converters
        /// </summary>
        public static int LocalConverters
        {
            get
            {
                return ACMManager.GetMectricsi(Null,
                    enuACMMetrics.CountLocalConverters);
            }
        }
        /// <summary>
        /// get ne numbers of local codec
        /// </summary>
        public static int LocalCodecs
        {
            get
            {
                return ACMManager.GetMectricsi(Null,
                    enuACMMetrics.CountLocalCodecs );
            }
        }
        /// <summary>
        /// get ne numbers of local codec
        /// </summary>
        public static int DisabledDriverCount
        {
            get
            {
                return ACMManager.GetMectricsi(Null,
                    enuACMMetrics.CountDisabled);
            }
        }
        public static int GetHardwareWaveOutput(ACMDriverInfo driver)
        {
            if (driver.Support == enuACMSupportFlag.Hardware )
            {
            return ACMManager.GetMectricsi(
                driver.DriverHandle  , enuACMMetrics.HardWareWaveOutPut);
                }
            return 0;
        }
        public static int GetHardwareWaveInput(ACMDriverInfo driver)
        {
            if (driver.Support  == enuACMSupportFlag.Hardware )
            {
            return ACMManager.GetMectricsi(
                driver.DriverHandle , enuACMMetrics.HardWareWaveInput );
           }
            return 0;
        }
    }
}

