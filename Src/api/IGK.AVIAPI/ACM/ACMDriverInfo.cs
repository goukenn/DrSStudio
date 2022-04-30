

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ACMDriverInfo.cs
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
file:ACMDriverInfo.cs
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
using System.Runtime.InteropServices;
namespace IGK.AVIApi.ACM
{
    /// <summary>
    /// represent an acm driver info
    /// </summary>
    public class ACMDriverInfo
    {
        private IntPtr  m_Handle;
        private IntPtr m_oHandle;
        private long m_MaxSizeFormat;
        private ACMApi.LPACMDRIVERDETAILS m_driverDetails;
        private enuACMSupportFlag m_Support;
        private int m_index;
        private static Version m_Version;
        public static Version Version
        {
            get { return m_Version; }
        }
        static ACMDriverInfo() {
            m_Version =  ACMManager.GetACMVersion();
        }
        public bool IsOpen {
            get { return this.m_oHandle != IntPtr.Zero; }
        }
        /// <summary>
        /// get the index of the driver
        /// </summary>
        public int Index { get { return this.m_index; } }
        public enuACMSupportFlag Support
        {
            get { return m_Support; }
        }
        public long MaxSizeFormat
        {
            get { return m_MaxSizeFormat; }
        }
        public IntPtr OpenHandle
        {
            get { return m_oHandle; }           
        }
        public IntPtr  DriverHandle
        {
            get { return m_Handle; }
        }
        public static ACMDriverInfo CreateFromHandle(IntPtr handle, int index )
        {
            ACMApi.LPACMDRIVERDETAILS v_details = ACMManager.GetDriverDetails(handle);
            if (v_details.fccType == 0)
                return null;
            ACMDriverInfo v_acmDrvInf = new ACMDriverInfo();
            v_acmDrvInf.m_Handle = handle;      
            v_acmDrvInf.m_driverDetails = v_details;
            v_acmDrvInf.m_Support = (enuACMSupportFlag)ACMManager.GetMectricsi(
                handle , enuACMMetrics.DriverSupport);
            v_acmDrvInf.m_index = index ;
            ACMFormatTagInfo[] tag =  v_acmDrvInf.GetFormatTags();
            return v_acmDrvInf;
        }
        public bool Open()
        {
            if (this.IsOpen)
                return true;
            IntPtr v_oHandle = new IntPtr();
            enuACMResult v_r = ACMApi.acmDriverOpen(ref v_oHandle, this.DriverHandle , 0);
            if (v_r != enuACMResult.NoError)
            {
                switch (v_r)
                {
                    case enuACMResult.NotEnabled :
                        return false;
                }
                throw ACMManager.ThrowError(v_r, "acmDriverOpen");
            }
            this.m_oHandle = v_oHandle;
            this.m_MaxSizeFormat = ACMManager.GetMectricsi(v_oHandle, enuACMMetrics.MaxSizeFormat);
            return true;
        }
        public void Close()
        {
            if (this.m_oHandle != IntPtr.Zero)
            {
                enuACMResult v_r = ACMApi.acmDriverClose(this.m_oHandle, 0);
                if (v_r != enuACMResult.NoError) 
                    throw ACMManager.ThrowError(v_r, "acmDriverClose");
                this.m_oHandle = IntPtr.Zero ;
            }
        }
        public string ShortName { get { return this.m_driverDetails.szShortName; } }
        public string LongName { get { return this.m_driverDetails.szLongName; } }
        public string CopyRight { get { return this.m_driverDetails.szCopyright; } }
        public string Features { get { return this.m_driverDetails.szFeatures; } }
        public string Licensing { get { return this.m_driverDetails.szLicensing; } }
        public override string ToString()
        {
            return string.Format ("ACMDriverInfo [{0}]", 
                string.IsNullOrEmpty (this.ShortName)? "NoName": this.ShortName );
        }
        /// <summary>
        /// convert to stream to another unit this ACMDriver
        /// </summary>
        public bool ConvertTo(string filename, 
            string outfile,
            IWAVEFORMATEX input,
            IWAVEFORMATEX output,  
            enuACMSuggest suggest)
        {
               bool r = ACMManager.Convert(
                    this,
                    filename , 
                    outfile,
                    input ,
                    output ,
                    suggest);
            return r;
        }
        /// <summary>
        /// convert to stream to another unit this ACMDriver
        /// </summary>
        public bool ConvertTo(
            System.IO.Stream inStream,
            System.IO.Stream outStream,
            IWAVEFORMATEX   input,
            IWAVEFORMATEX output,
            enuACMSuggest suggest)
        {
            bool r = ACMManager.Convert(
                 this,
                 inStream ,
                 outStream ,
                 input,
                 output,
                 suggest);
            return r;
        }
        /// <summary>
        /// get all supported format 
        /// </summary>
        /// <returns></returns>
        public ACMFormatInfo[]  GetFormats()
        {
            if (this.Open())
            {
                ACMFormatInfo[] t = ACMManager.GetDriverFormats(this,
                     enuACMEnumFormatType.None);
                this.Close();
                return t;
            }
            return null;
        }
        /// <summary>
        /// get the format tag of the drivers
        /// </summary>
        /// <returns></returns>
        public ACMFormatTagInfo[] GetFormatTags()
        {
            if (this.Open())
            {
                ACMFormatTagInfo[] t = ACMManager.GetDriverFormatTags(
                    this);
                this.Close();
                return t;
            }
            return null;
        }
        /// <summary>
        /// retreive the tag format. 
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public ACMFormatTagInfo GetFormatTag(enuACMWaveFormat tag)
        {
            ACMFormatTagInfo[] tagformats = this.GetFormatTags();
            foreach (ACMFormatTagInfo item in tagformats)
            {
                if (item.FormatTag == tag)
                    return item;
            }
            return null;
        }
        public ACMFilterInfo[]  GetFilters()
        {
            return ACMManager.GetDriverFilters(this);
        }
        public ACMFilterTagInfo[] GetFilterTag()
        {
            return ACMManager.GetDriverFilterTags(this);
        }
    }
}

