

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ACMManager.cs
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
file:ACMManager.cs
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
using System.IO;
namespace IGK.AVIApi.ACM
{
    using AVI;
    using MMSYSTEM;
    using IGK.ICore;using IGK.AVIApi.Native;
    public delegate void acmStreamConvertCallback(
  IntPtr has,    
  uint  uMsg,         
  int dwInstance,  
  IntPtr lParam1,    
  IntPtr lParam2     
);
    /// <summary>
    /// represent a audio controller manager class
    /// </summary>
    public static class ACMManager
    {
        public static int MaxSizeFormat { get { return GetMectricsi(IntPtr.Zero, enuACMMetrics.MaxSizeFormat); } }
        public static int MaxSizeFilter { get { return GetMectricsi(IntPtr.Zero, enuACMMetrics.MaxSizeFilter); } }
        public static int CountLocalDrivers { get { return GetMectricsi(IntPtr.Zero, enuACMMetrics.CountLocalDrivers); } }
        public static int CountLocalFilters { get { return GetMectricsi(IntPtr.Zero, enuACMMetrics.CountLocalFilters); } }
        public static int CountLocalCodecs { get { return GetMectricsi(IntPtr.Zero, enuACMMetrics.CountLocalCodecs); } }
        public static int CountGlobalCodecs { get { return GetMectricsi(IntPtr.Zero, enuACMMetrics.CountCodecs); } }
        public static int CountGlobalDrivers { get { return GetMectricsi(IntPtr.Zero, enuACMMetrics.CountFilters); } }
        public static int CountGlobalHardware { get { return GetMectricsi(IntPtr.Zero, enuACMMetrics.CountHardWare ); } }
        public static int CountLocalConverters { get { return GetMectricsi (IntPtr .Zero , enuACMMetrics.CountLocalConverters );} }
        /// <summary>
        /// Get system installer mp3 Audio driver info
        /// </summary>
        /// <returns></returns>
        public static ACMDriverInfo[] GetMP3Driver()
        {
            ACMDriverInfo[] v_d = GetDrivers(enuACMDriverFlag.All ) ;
            List<ACMDriverInfo> v_l = new List<ACMDriverInfo>();
            for (int i = 0; i < v_d.Length; i++)
            {
                ACMFormatTagInfo[] tag = v_d[i].GetFormatTags();
                if (tag == null) continue;
                for (int j = 0; j < tag.Length; j++)
                {
                    if (tag[j].FormatTag == enuACMWaveFormat.MPEGLAYER3)
                    {
                        v_l.Add(v_d[i]);
                    }
                }
            }
            return v_l.ToArray();
        }
        /// <summary>
        /// retreive all audio's installed driver 
        /// </summary>
        /// <param name="driverflag">driver</param>
        /// <returns>array of acm driver info</returns>
        public static ACMDriverInfo[] GetDrivers(enuACMDriverFlag driverflag)
        {
            ACMManagerDriverEnum v_enum = new ACMManagerDriverEnum();
            ACMDriverEnumCallBack d = new ACMDriverEnumCallBack(v_enum.Enum);
            ACMApi.acmDriverEnum(Marshal.GetFunctionPointerForDelegate(d),
                IntPtr.Zero, (int)driverflag); 
            return v_enum.ToArray();
        }
        /// <summary>
        /// retrieves all format of the driver
        /// </summary>
        /// <param name="driverH">driver handle</param>
        /// <param name="driverflag"></param>
        /// <returns></returns>
        public static ACMFormatInfo[] GetDriverFormats(
            ACMDriverInfo driverOpenH,
            enuACMEnumFormatType supportType)
        {
            ACMManagerFormatEnum v_enum = new ACMManagerFormatEnum(driverOpenH);
             ACMFormatEnumCallback d = new ACMFormatEnumCallback(v_enum.Enum);
             ACMApi.ACMFORMATDETAILS v_details = new ACMApi.ACMFORMATDETAILS();
             v_details.cbStruct  = Marshal.SizeOf(v_details);
             WAVEFORMATEX t = new WAVEFORMATEX();
             t.cbSize = (short)ACMInfo.MaxSizeFormat;
             t.wFormatTag = MMSYSTEM.mmSystemAPI.WAVE_FORMAT_UNKNOWN;
             IntPtr vAlloc = Marshal.AllocHGlobal (t.cbSize);
             Marshal.StructureToPtr(t, vAlloc, true);
             v_details.pwfx = vAlloc;
             v_details.cbwfx = (short)t.cbSize;
             v_details.dwFormatTag = MMSYSTEM.mmSystemAPI.WAVE_FORMAT_UNKNOWN;// MMSYSTEM.mmSystemAPI.WAVE_FORMAT_ADPCM;
             try
             {
                 IntPtr s_null = IntPtr .Zero ;
                 enuACMResult v_r = ACMApi.acmFormatEnum(
                     driverOpenH.OpenHandle  ,
                     ref v_details,
                     Marshal.GetFunctionPointerForDelegate(d),
                     out s_null,
                     (int)supportType
                      );
                 if (v_r != enuACMResult.NoError)
                 {
                     throw ThrowError(v_r, "acmFormatEnum");
                 }
             }
             catch (Exception ex)
             {
                 System.Diagnostics.Debug.WriteLine(ex.Message);
             }
            Marshal.FreeHGlobal (vAlloc);
            return v_enum.ToArray();
        }
        /// <summary>
        /// get the driver format tag
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="supportType"></param>
        /// <returns></returns>
        public static ACMFormatTagInfo[] GetDriverFormatTags(ACMDriverInfo driver)
        {
            ACMManagerFormatTagEnum v_enum = new ACMManagerFormatTagEnum(driver);
            ACMFormatTagEnumCallback d = new ACMFormatTagEnumCallback(v_enum.Enum);
            ACMApi.ACMFORMATTAGDETAILS v_details = new ACMApi.ACMFORMATTAGDETAILS();
            v_details.cbStruct = Marshal.SizeOf(v_details);
            try
            {            
                IntPtr s_null = IntPtr.Zero;
                enuACMResult v_r = ACMApi.acmFormatTagEnum(
                    driver.OpenHandle,
                    ref v_details,
                    Marshal.GetFunctionPointerForDelegate(d),
                    out s_null,
                    (int)0
                     );
                if (v_r != enuACMResult.NoError)
                {
                    throw ThrowError(v_r, "acmFormatTagEnum");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return v_enum.ToArray();
        }
        public static ACMFilterInfo[] GetDriverFilters(ACMDriverInfo driver)
        {
            ACMManagerFilterEnum v_enum = new ACMManagerFilterEnum(driver);
            ACMFilterEnumCallback d = new ACMFilterEnumCallback(v_enum.Enum);
            ACMApi.ACMFILTERDETAILS  v_details = new ACMApi.ACMFILTERDETAILS ();
            v_details.cbStruct = Marshal.SizeOf(v_details);
            //create and init wave filter
            ACMApi.WAVEFILTER v_waveFilter = new ACMApi.WAVEFILTER ();
            //init the size
            v_waveFilter.dwFilterTag = 0;// mmSystemAPI.WAVE_FILTER_VOLUME;
            v_waveFilter.dwReserved = new int[5];
            v_waveFilter.cbStruct = Marshal.SizeOf(v_waveFilter);
            IntPtr v_alloc = Marshal.AllocHGlobal (v_waveFilter .cbStruct );
            Marshal.StructureToPtr (v_waveFilter , v_alloc ,true );
            v_details.pwfltr = v_alloc;
            v_details.cbwfltr = v_waveFilter .cbStruct ;
            v_details.dwFilterTag = 0;// mmSystemAPI.WAVE_FILTER_VOLUME;
            try
            {
                IntPtr s_null = IntPtr.Zero;
                enuACMResult v_r = ACMApi.acmFilterEnum (
                    driver.OpenHandle,
                    ref v_details,
                    Marshal.GetFunctionPointerForDelegate(d),
                    out s_null,
                    (int)0//ACMApi.ACM_FILTERENUMF_DWFILTERTAG
                     );
                if (v_r != enuACMResult.NoError)
                {
                    throw ThrowError(v_r, "acmFilterEnum");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            Marshal.FreeHGlobal (v_alloc );
            return v_enum.ToArray(); ;
        }
        /// <summary>
        /// get the filters tags
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static ACMFilterTagInfo[] GetDriverFilterTags(ACMDriverInfo driver)
        {
            ACMManagerFilterTagEnum v_enum = new ACMManagerFilterTagEnum(driver);
            ACMFilterTagEnumCallback d = new ACMFilterTagEnumCallback(v_enum.Enum);
            ACMApi.ACMFILTERTAGDETAILS v_details = new ACMApi.ACMFILTERTAGDETAILS();
            v_details.cbStruct = Marshal.SizeOf(v_details);
            try
            {
                IntPtr s_null = IntPtr.Zero;
                enuACMResult v_r = ACMApi.acmFilterTagEnum(
                    driver.OpenHandle,
                    ref v_details,
                    Marshal.GetFunctionPointerForDelegate(d),
                    out s_null,
                    (int)0
                     );
                if (v_r != enuACMResult.NoError)
                {
                    throw ThrowError(v_r, "acmFilterTagEnum");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return v_enum.ToArray(); ;
        }
        public static Exception ThrowError(enuACMResult result, string message)
        {
            return new Exception (string.Format ("{0} : {1}",result,message));
        }
        abstract class ACMManagerEnum<T>
        {
            ACMDriverInfo m_driver;
            List<T> m_infos = new List<T>();
            public ACMDriverInfo  Driver
            {
                get { return m_driver; }
            }
            public ACMManagerEnum(ACMDriverInfo driver)
            {
                this.m_driver = driver;
            }
            protected void Add(T item)
            {
                m_infos.Add(item);
            }
            internal T[] ToArray()
            {
                return m_infos.ToArray();
            }
        }
        sealed class ACMManagerDriverEnum
        {
            List<ACMDriverInfo> m_drivers = new List<ACMDriverInfo>();
            internal bool  Enum(IntPtr h, IntPtr appflag, int i)
            {
                ACMDriverInfo v_info = ACMDriverInfo.CreateFromHandle(h,i);
                if (v_info != null)
                {
                    m_drivers.Add(v_info);
                }
                return true;
            }
internal ACMDriverInfo[] ToArray()
{
    return this.m_drivers.ToArray();
}}
        sealed class ACMManagerFormatEnum : ACMManagerEnum<ACMFormatInfo>
        { 
            public ACMManagerFormatEnum(ACMDriverInfo driver):base(driver )
            {                
            }
            internal bool Enum(   IntPtr deviceHandle,
               ref ACMApi.ACMFORMATDETAILS pafd, 
               ref IntPtr dwInstance, 
               int fdwSupport  )
            {
                this.Add (ACMFormatInfo .CreateInfo (this.Driver, pafd));                
                return true ;
            }
        }
        sealed class ACMManagerFormatTagEnum : ACMManagerEnum<ACMFormatTagInfo>
        {
            public ACMManagerFormatTagEnum(ACMDriverInfo driver):base(driver )
            {           
            }
            internal bool Enum( 
                IntPtr deviceHandle,
               ref ACMApi.ACMFORMATTAGDETAILS pafd,
               ref IntPtr dwInstance,
               int fdwSupport)
            {
                this.Add(ACMFormatTagInfo.Create (Driver, pafd));
                return true;
            }
        }
        sealed class ACMManagerFilterEnum : ACMManagerEnum<ACMFilterInfo>
        {
            public ACMManagerFilterEnum(ACMDriverInfo driver)
                : base(driver)
            {
            }
            internal bool Enum(IntPtr deviceHandle,
               ref ACMApi.ACMFILTERDETAILS pafd,
               ref IntPtr dwInstance,
               int fdwSupport)
            {
                this.Add(
                    ACMFilterInfo.Create(this.Driver, pafd)
                    );
                return true;
            }
        }
        sealed class ACMManagerFilterTagEnum : ACMManagerEnum<ACMFilterTagInfo>
        {
            public ACMManagerFilterTagEnum(ACMDriverInfo driver)
                : base(driver)
            {
            }
            internal bool Enum(
                IntPtr deviceHandle,
               ref ACMApi.ACMFILTERTAGDETAILS pafd,
               ref IntPtr dwInstance,
               int fdwSupport)
            {
                this.Add(ACMFilterTagInfo.Create(Driver, pafd));
                return true;
            }
        }
        internal static long GetMectricsL(enuACMMetrics enuACMMetrics)
        {
            return GetMectricsL(IntPtr.Zero, enuACMMetrics);
        }
        internal static long GetMectricsL(IntPtr oHandle, enuACMMetrics enuACMMetrics)
        {
            IntPtr t = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(long)));            
            GetACMMetric(oHandle, enuACMMetrics, t);            
            long[] i = new long[1];
            Marshal.Copy(t, i, 0, 1);
            Marshal.FreeCoTaskMem(t);
            return i[0];
        }
        internal static int GetMectricsi(IntPtr oHandle, enuACMMetrics enuACMMetrics)
        {
            IntPtr t = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(int)));
            GetACMMetric(oHandle, enuACMMetrics, t);
            int[] i  = new int[1];
            Marshal.Copy (t,i,0,1);
            Marshal.FreeCoTaskMem(t);
            return i[0];
        }
        private static void GetACMMetric(IntPtr oHandle, enuACMMetrics metrics, IntPtr output)
        {
            enuACMResult r = ACMApi.acmMetrics(oHandle, (int)metrics , output);
            if (r != enuACMResult.NoError)
                throw ThrowError(r, "acmMetrics");
        }
        internal static ACMApi.LPACMDRIVERDETAILS GetDriverDetails(IntPtr h)
        {
            ACMApi.LPACMDRIVERDETAILS v_driverDetails= new ACMApi.LPACMDRIVERDETAILS();
            v_driverDetails.cbStruct = (uint)Marshal.SizeOf(v_driverDetails);
            ACMApi.acmDriverDetails(h, ref v_driverDetails, 0);
            //ACMApi.acmDriverDetailsW(h, ref v_driverDetails, 0);
            return v_driverDetails;
        }
        public static void CloseStream(IntPtr v_hmp3Stream)
        {
            enuACMResult v_r = ACMApi.acmStreamClose(
                v_hmp3Stream,
                0);         
            if (v_r != enuACMResult.NoError)
            {
                throw ThrowError(v_r, "acmStreamClose");
            }
        }
        /// <summary>
        /// Choose audio encoder PCM Format
        /// </summary>
        /// <param name="returnType">Format Type: e.i:  typeof(MPEGLAYER3WAVEFORMAT) </param>
        /// <returns>the format choosed</returns>
        public static object ChoosePCMFormat(string title,  Type returnType)
        {
            ACMApi.ACMFORMATCHOOSE pfmtc = new ACMApi.ACMFORMATCHOOSE();
            pfmtc.cbStruct = Marshal.SizeOf(pfmtc);
            pfmtc.cbwfx = 50;// formatSize;
            pfmtc.pwfx = Marshal.AllocCoTaskMem(pfmtc.cbwfx);
            pfmtc.pszTitle = Marshal.StringToCoTaskMemAnsi (title);
            WAVEFORMATEX cx = new WAVEFORMATEX();
            cx.cbSize = (short)Marshal.SizeOf(cx);
            cx.wFormatTag = (short)AVIApi.WAVE_FORMAT_PCM;
            cx.wBitsPerSample = 16;
            pfmtc .pwfxEnum = Marshal.AllocCoTaskMem (cx.cbSize );

            Marshal.StructureToPtr(cx, pfmtc.pwfxEnum, false);
            pfmtc.fdwEnum =(int) (ACMApi.ACM_FORMATENUMF_WFORMATTAG | ACMApi.ACM_FORMATSUGGESTF_WBITSPERSAMPLE );
            
            enuACMResult ri = ACMApi.acmFormatChoose(ref pfmtc);
            object v_selectedFormat = null;
            if ((ri == enuACMResult.NoError) && (returnType !=null) )
            {
                //chose your pixel format
                v_selectedFormat = pfmtc.pwfx.ConvertTo(returnType);
            }            
            Marshal.FreeCoTaskMem(pfmtc.pwfx);
            return v_selectedFormat;
        }
        public static IntPtr OpenStream(ACMDriverInfo driver, 
            ref IntPtr sourceFormat, 
            ref IntPtr destinationformat, 
            IntPtr intPtr, 
            IntPtr intPtr_5, 
            IntPtr intPtr_6, 
            enuACMOpenMode enuAcmOpenMode)
        {
            IntPtr hv = IntPtr.Zero;
            enuACMResult v_r = ACMApi.acmStreamOpen(
                ref hv,
                driver.OpenHandle,
                sourceFormat,
                destinationformat,
                IntPtr.Zero,
                IntPtr.Zero,
                IntPtr.Zero ,
                0);
            if (v_r != enuACMResult.NoError)
            {
                throw ThrowError(v_r, "acmStreamOpen");
            }
            return hv;
        }
        /// <summary>
        /// convert stream
        /// </summary>
        /// <param name="driver">used driver</param>
        /// <param name="inStream">in stream</param>
        /// <param name="outStream">out stream</param>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="suggest"></param>
        /// <returns></returns>
        public static bool Convert(
             ACMDriverInfo driver,
            Stream inStream,
            Stream outStream,
           IWAVEFORMATEX  source,
           IWAVEFORMATEX destination,
            enuACMSuggest suggest
            )
        {
            bool isoopen  = driver.Open();
            IntPtr hsrc = (source!=null)?source.Handle: IntPtr.Zero ;
            IntPtr hdest = (destination != null) ? destination.Handle : Marshal.AllocCoTaskMem(ACMManager.MaxSizeFormat);
            enuACMResult r = ACMApi.acmFormatSuggest(driver.OpenHandle,
   hsrc, hdest, ACMManager.MaxSizeFormat, (int)suggest);//(int)enuACMSuggest.WFormat); 

            //r = ACMApi.acmFormatSuggest(driver.OpenHandle,
            //   source.Handle, destination.Handle, ACMManager.MaxSizeFormat, (int)enuACMSuggest.WFormat); 
            if (r != enuACMResult.NoError)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("{0} : error", r));
                return false;
            }
            int count = 0;
            IntPtr v_zconversion = IntPtr.Zero;
            r = ACMApi.acmStreamOpen(ref v_zconversion,               // open an ACM conversion stream
driver.OpenHandle,                       // open handle
hsrc,//source hanle
hdest, //destination handle
IntPtr.Zero,                       // with no filter
IntPtr.Zero,                          // or async callbacks
IntPtr.Zero,                          // (and no data for the callback)
0                           // and no flags
);
            if (v_zconversion == IntPtr.Zero)
                return false;
            int sourceSize = ACMApi.MP3_BLOCK_SIZE;
            int destSize = 4096;
            r = ACMApi.acmStreamSize(
                 v_zconversion,
                 sourceSize, //ACMApi.MP3_BLOCK_SIZE,
                 ref destSize,
                 ACMApi.ACM_STREAMSIZEF_SOURCE);
            //destSize = Math.Min (4096, destSize );
            ACMApi.ACMSTREAMHEADER v_header = new ACMApi.ACMSTREAMHEADER();
            v_header.cbStruct = Marshal.SizeOf(v_header);
            v_header.pbSrc = Marshal.AllocCoTaskMem(sourceSize);
            v_header.cbSrcLength = sourceSize;
            v_header.pbDst = Marshal.AllocCoTaskMem(destSize);
            v_header.cbDstLength = destSize;
            Byte[] v_tab = new byte[sourceSize];
            Byte[] v_tout = new byte[destSize];
            //set header to query
            //ACMApi.ACMSTREAMHEADER c = new ACMApi.ACMSTREAMHEADER();
            r = ACMApi.acmStreamPrepareHeader(
                 v_zconversion,//.ToInt32 (),
                ref v_header,
                0);
            if (r != enuACMResult.NoError)
            {
                throw new Exception(r.ToString());
            }
            BinaryReader binR = new BinaryReader(inStream);
            BinaryWriter binW = new BinaryWriter(outStream);
            System.Diagnostics.Debug.WriteLine("Converting Data...");
#if DEBUG
            long rdata = 0;
#else 
            int rdata = 0;
#endif

            //get thet start point of pcm data in mp3 file

            enuACMResult v_result = enuACMResult.NoError;
            while (true)
            {
                count = binR.Read(v_tab, 0, sourceSize);
                Marshal.Copy(v_tab, 0, v_header.pbSrc, count);
                v_result = ACMApi.acmStreamConvert(v_zconversion,
                    ref v_header,
                     enuACMConvertType.BlockAlign);
                Marshal.Copy(v_header.pbDst, v_tout, 0, v_header.cbDstLength);
                if (v_header.cbDstLengthUsed > 0)
                {
                    binW.Write(v_tout, 0, v_header.cbDstLengthUsed);//. v_header .cbDstLengthUsed );
                    rdata += v_header.cbDstLengthUsed;
                }
#if DEBUG
                

                System.Diagnostics.Debug.Write(string.Format("{0}\xd", rdata));
#endif
                if (count != sourceSize)
                    break;
            }
            //unprepare header
            r = ACMApi.acmStreamUnprepareHeader(
              v_zconversion,
              ref v_header,
              0);
            //free resource 
            ACMManager.CloseStream(v_zconversion);
            binW.Flush();
            return true;
        }
 /// <summary>
 /// 
 /// </summary>
 /// <param name="driver">driver must be opened</param>
 /// <param name="sourcefile"></param>
 /// <param name="outfile"></param>
 /// <param name="source"></param>
 /// <param name="destination"></param>
 /// <param name="suggest"></param>
 /// <returns></returns>
        public static bool Convert(
            ACMDriverInfo driver,
            string sourcefile, 
            string outfile,
            IWAVEFORMATEX   source,
            IWAVEFORMATEX destination,
            enuACMSuggest suggest)
        {           
            enuACMResult r =  ACMApi.acmFormatSuggest(
                driver.OpenHandle,
                (source ==null) ? IntPtr.Zero : source.Handle ,
                (destination == null)? IntPtr.Zero :destination.Handle ,
                (destination ==null)? MaxSizeFormat : destination.FormatSize ,
                (int)suggest 
                );
            if (r != enuACMResult.NoError)
            {
                System.Diagnostics.Debug.WriteLine (string.Format ("{0} : error", r));
                return false;
            }
            int count = 0;
            IntPtr z = IntPtr.Zero;
            r = ACMApi.acmStreamOpen(ref z,               // open an ACM conversion stream
driver.OpenHandle,                       // querying all ACM drivers
source.Handle ,
destination.Handle , // target
IntPtr.Zero,                       // with no filter
IntPtr.Zero,                          // or async callbacks
IntPtr.Zero,                          // (and no data for the callback)
0                           // and no flags
);
            int sourceSize = ACMApi.MP3_BLOCK_SIZE;
            int destSize = 4096;
            r = ACMApi.acmStreamSize(
                 z,
                 sourceSize, //ACMApi.MP3_BLOCK_SIZE,
                 ref destSize,
                 ACMApi.ACM_STREAMSIZEF_SOURCE);
            ACMApi.ACMSTREAMHEADER v_header = new ACMApi.ACMSTREAMHEADER();
            v_header.cbStruct = Marshal.SizeOf(v_header);
            v_header.pbSrc = Marshal.AllocCoTaskMem(sourceSize );
            v_header.cbSrcLength = sourceSize;
            v_header.pbDst = Marshal.AllocCoTaskMem(destSize);
            v_header.cbDstLength = destSize;
            Byte[] v_tab = new byte[sourceSize ];
            Byte[] v_tout = new byte[destSize ];
            //set header to query
            r = ACMApi.acmStreamPrepareHeader(
                z,
                ref v_header,
                0);
            BinaryReader binR = new BinaryReader(File.Open(sourcefile, FileMode.Open));
            BinaryWriter binW = new BinaryWriter(File.Create(outfile));
            while (true)
            {
                count = binR.Read(v_tab, 0, sourceSize );
                if (count != sourceSize)
                    break;
                Marshal.Copy(v_tab, 0, v_header.pbSrc, count);
                ACMApi.acmStreamConvert(z,
                    ref v_header,
                     enuACMConvertType.BlockAlign);
                Marshal.Copy(v_header.pbDst, v_tout, 0, v_header.cbDstLength);
                binW.Write(v_tout, 0, v_header.cbDstLengthUsed);//. v_header .cbDstLengthUsed );
            }
            //unprepare header
            r = ACMApi.acmStreamUnprepareHeader (
              z,
              ref v_header,
              0);
            //free resource 
            ACMManager.CloseStream(z);
            binR.Close();
            binW.Flush();
            binW.Close();
            return true;
        }


     
        
        internal static Version GetACMVersion()
        {
            int d = ACMApi.acmGetVersion();
            int major = (d>>24) & 0xFF;
            int minor = (d & 0x00FF0000) >> 16;
            int build = (d & 0x0000FFFF);
            Version v = new Version(major, minor ,build );
            return v;
        }
        /// <summary>
        /// get the driver
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static ACMDriverInfo FindDriver(string p, enuACMSupportFlag support)
        {
            ACMDriverInfo[] c = GetDrivers(enuACMDriverFlag.All);
            foreach (ACMDriverInfo  item in c)
            {
                if (item.Support == support)
                {
                        return item;
                }
            }
            return null;
        }
        /// <summary>
        /// get a suggested format
        /// </summary>
        /// <param name="sourceFormat"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public static enuACMResult Suggest(IWAVEFORMAT sourceFormat, ACMFormatInfo destination)
        {
            IntPtr source = IntPtr.Zero;
            IntPtr dest = IntPtr.Zero;
            WAVEFORMATEX v_destformat = 
             (WAVEFORMATEX)destination.GetFormatEXInfo(typeof(WAVEFORMATEX));
            WAVEFORMATEX v_srcformat = v_destformat ;
            v_srcformat.cbSize = (short)sourceFormat.Size;
            v_srcformat.nAvgBytesPerSec = 0;// sourceFormat.AvgBytesPerSec;
            v_srcformat.nChannels = (short)sourceFormat.Channel;
            v_srcformat.wFormatTag = (short)destination.Format;
            v_srcformat.nSamplesPerSec = v_destformat.nSamplesPerSec;
            v_srcformat.nBlockAlign = 1;
            v_srcformat.wBitsPerSample = 0;
            PWAVEFORMAT v_srcfrm = new PWAVEFORMAT(v_srcformat);
            PWAVEFORMAT v_destfrm = new PWAVEFORMAT(v_destformat);
            enuACMResult g = ACMApi.acmFormatSuggest(IntPtr.Zero,
                 v_srcfrm .Handle ,
                v_destfrm.Handle ,
                50,
                (int) enuACMSuggest .WFormat  );
            return g;
        }
        public static IWAVEFORMATEX CreateFormat(enuACMWaveFormat format,
            int channel,//1 for mono 2 for stereo
            int SamplePerSecond,//44100 for exemple
            int AvgByteBerSecond,
            int blockAlign,//1 for mp3 
            int BitsPerSample // 16 or 8
            )
        {
            IWAVEFORMATEX ex = null;
            WAVEFORMATEX c = new WAVEFORMATEX();
            c.nBlockAlign =(short) blockAlign;
            c.cbSize = (short)Marshal.SizeOf(c);// MaxSizeFormat;
            c.nChannels = (short)channel;
            c.wBitsPerSample = (short)BitsPerSample;
            c.nAvgBytesPerSec = AvgByteBerSecond;
            c.wFormatTag = (short)format;
            ex = new PWAVEFORMAT(c);
            return ex;
        }
        public static IWAVEFORMATEX CreateFormat(ACMFormatInfo format)
        {
            WAVEFORMATEX d = format.GetFormatEXInfo<WAVEFORMATEX>();
            PWAVEFORMAT t = new PWAVEFORMAT(d);
            return t;
        }
    }
}

