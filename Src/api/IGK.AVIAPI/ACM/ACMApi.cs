

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ACMApi.cs
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
file:ACMApi.cs
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
using System.Reflection;
using System.Runtime.InteropServices ;
namespace IGK.AVIApi.ACM
{
    using AVI;
    using IGK.ICore;using IGK.AVIApi.Native;
    public delegate bool ACMDriverEnumCallBack(
    IntPtr deviceHandle,
    IntPtr appDefValue, int i
    );
    internal delegate bool ACMFormatEnumCallback(
   IntPtr deviceHandle,
   ref ACMApi.ACMFORMATDETAILS pafd, 
   ref IntPtr dwInstance, 
   int  fdwSupport  
);
    internal delegate bool ACMFormatTagEnumCallback(
    IntPtr has,
    ref ACMApi.ACMFORMATTAGDETAILS formatDetails,
    ref IntPtr lParam1,
    int lParam2
  );
    internal delegate bool ACMFilterEnumCallback(
  IntPtr has,
  ref ACMApi.ACMFILTERDETAILS formatDetails,
  ref IntPtr lParam1,
  int lParam2
);
    internal delegate bool ACMFilterTagEnumCallback(
  IntPtr has,
  ref ACMApi.ACMFILTERTAGDETAILS formatDetails,
  ref IntPtr lParam1,
  int lParam2
);
//    internal delegate void acmStreamConvertCallback(
//  IntPtr has,
//  uint uMsg,
//  uint dwInstance,
//  IntPtr lParam1,
//  IntPtr lParam2
//);
    public static class ACMApi
    {
        internal const int MP3_BLOCK_SIZE = 522;
        internal const int MP3_STD_CODE_DELAY = 1393;
        internal const int MP3_STD_CODE_LAME_DELAY = 16;
        internal const int ACM_DRIVERENUMF_NOLOCAL = 0x40000000;
        internal const int ACM_DRIVERENUMF_DISABLED = 0x40000000 << 1;
        #region Method
        #region Enumeration
        [DllImport("Msacm32.dll")]
        internal static extern void acmDriverEnum(IntPtr callBack,IntPtr instance, int flag);
        [DllImport("Msacm32.dll")]
        internal static extern enuACMResult acmFormatEnum(
    IntPtr had,
    ref ACMFORMATDETAILS pafd,
    IntPtr fnCallback,
    out IntPtr dwInstance,
    int fdwEnum
  );
        [DllImport("Msacm32.dll")]
        internal static extern enuACMResult acmFormatTagEnum(
  IntPtr  had,
  ref ACMFORMATTAGDETAILS paftd,
  IntPtr  fnCallback,
  out IntPtr  dwInstance,
  int fdwEnum
);
        [DllImport("msacm32.dll")]
        internal static extern enuACMResult acmFormatChoose(
  ref ACMFORMATCHOOSE pfmtc
);
        [DllImport("msacm32.dll")]
        internal static extern enuACMResult acmStreamSize
        (
            IntPtr streamHwnd,
            int bufferExpected,
            ref int buffSize,
            int flag);
        internal const int ACM_FILTERENUMF_DWFILTERTAG = 0x00010000;
        [DllImport("Msacm32.dll")]
        internal static extern enuACMResult acmFilterEnum(
  IntPtr had,
  ref ACMFILTERDETAILS pafd,
  IntPtr  fnCallback,
  out IntPtr  dwInstance,
  int fdwEnum
);
        [DllImport("Msacm32.dll")]
        internal static extern enuACMResult acmFilterTagEnum(
  IntPtr had,
  ref ACMFILTERTAGDETAILS pafd,
  IntPtr fnCallback,
  out IntPtr dwInstance,
  int fdwEnum
);
        #endregion
        #endregion
        internal const int ACMERR_BASE = (512);
        internal const int ACMERR_NOTPOSSIBLE = (ACMERR_BASE + 0);
        internal const int ACMERR_BUSY = (ACMERR_BASE + 1);
        internal const int ACMERR_UNPREPARED = (ACMERR_BASE + 2);
        internal const int ACMERR_CANCELED = (ACMERR_BASE + 3);
        //support flag
        internal const long ACMDRIVERDETAILS_SUPPORTF_CODEC = 0x00000001L;
        internal const long ACMDRIVERDETAILS_SUPPORTF_CONVERTER = 0x00000002L;
        internal const long ACMDRIVERDETAILS_SUPPORTF_FILTER = 0x00000004L;
        internal const long ACMDRIVERDETAILS_SUPPORTF_HARDWARE = 0x00000008L;
        internal const long ACMDRIVERDETAILS_SUPPORTF_ASYNC = 0x00000010L;
        internal const long ACMDRIVERDETAILS_SUPPORTF_LOCAL = 0x40000000L;
        internal const long ACMDRIVERDETAILS_SUPPORTF_DISABLED = 0x80000000L;
        //format enum
        internal const long ACM_FORMATENUMF_WFORMATTAG       =0x00010000L;
        internal const long ACM_FORMATENUMF_NCHANNELS        =0x00020000L;
        internal const long ACM_FORMATENUMF_NSAMPLESPERSEC   =0x00040000L;
        internal const long ACM_FORMATENUMF_WBITSPERSAMPLE   =0x00080000L;
        internal const long ACM_FORMATENUMF_CONVERT = 0x00100000L;
        internal const long ACM_FORMATENUMF_SUGGEST          =0x00200000L;
        internal const long ACM_FORMATENUMF_HARDWARE         =0x00400000L;
        internal const long ACM_FORMATENUMF_INPUT            =0x00800000L;
        internal const long ACM_FORMATENUMF_OUTPUT           =0x01000000L;
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - ;
        //
        //  acmMetrics()
        //
        //
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - ;
        [DllImport("msacm32.dll")]
        internal static extern enuACMResult acmMetrics
        (
            IntPtr hao,
            int uMetric,
            IntPtr pMetric
        );
        [DllImport("msacm32.dll")]
        internal static extern enuACMResult acmMetrics
        (
            IntPtr hao,
            int uMetric,
            ref int pMetric
        );
        internal const int ACM_METRIC_COUNT_DRIVERS = 1;
        internal const int ACM_METRIC_COUNT_CODECS = 2;
        internal const int ACM_METRIC_COUNT_CONVERTERS = 3;
        internal const int ACM_METRIC_COUNT_FILTERS = 4;
        internal const int ACM_METRIC_COUNT_DISABLED = 5;
        internal const int ACM_METRIC_COUNT_HARDWARE = 6;
        internal const int ACM_METRIC_COUNT_LOCAL_DRIVERS = 20;
        internal const int ACM_METRIC_COUNT_LOCAL_CODECS = 21;
        internal const int ACM_METRIC_COUNT_LOCAL_CONVERTERS = 22;
        internal const int ACM_METRIC_COUNT_LOCAL_FILTERS = 23;
        internal const int ACM_METRIC_COUNT_LOCAL_DISABLED = 24;
        internal const int ACM_METRIC_HARDWARE_WAVE_INPUT = 30;
        internal const int ACM_METRIC_HARDWARE_WAVE_OUTPUT = 31;
        internal const int ACM_METRIC_MAX_SIZE_FORMAT = 50;
        internal const int ACM_METRIC_MAX_SIZE_FILTER = 51;
        internal const int ACM_METRIC_DRIVER_SUPPORT = 100;
        internal const int ACM_METRIC_DRIVER_PRIORITY = 101;
        [DllImport("msacm32.dll")]
        internal  static extern enuACMResult acmFormatSuggest(
  IntPtr had,
  ref WAVEFORMATEX pwfxSrc,    // source format to convert
  ref WAVEFORMATEX pwfxDst,    // required destination format           
  int cbwfxDst,
  enuACMSuggest fdwSuggest
);
        [DllImport("msacm32.dll")]
        public static extern enuACMResult acmFormatSuggest(
  IntPtr had,
  ref object  pwfxSrc,    // source format to convert
  ref object  pwfxDst,    // required destination format           
  int cbwfxDst,
  enuACMSuggest fdwSuggest
);
        internal const int ACM_FORMATSUGGESTF_WFORMATTAG = 0x00010000;
        internal const int ACM_FORMATSUGGESTF_NCHANNELS = 0x00020000;
        internal const int ACM_FORMATSUGGESTF_NSAMPLESPERSEC = 0x00040000;
        internal const int ACM_FORMATSUGGESTF_WBITSPERSAMPLE = 0x00080000;
        internal const int ACM_FORMATSUGGESTF_TYPEMASK = 0x00FF0000;
        [DllImport("msacm32.dll")]
        internal static extern enuACMResult acmStreamOpen
        (
            ref IntPtr phas,       // pointer to stream handle
            IntPtr had,        // optional driver handle
            ref WAVEFORMATEX pwfxSrc,    // source format to convert
            ref WAVEFORMATEX pwfxDst,    // required destination format
            IntPtr pwfltr,     // optional filter
            IntPtr dwCallback, // callback
            IntPtr dwInstance, // callback instance data
            enuACMOpenMode fdwOpen     // ACM_STREAMOPENF_* and CALLBACK_*
        );
        [DllImport("msacm32.dll")]
        internal static extern enuACMResult acmStreamOpen
        (
            ref IntPtr phas,       // pointer to stream handle
            IntPtr had,        // optional driver handle
            IntPtr pwfxSrc,    // source format to convert
            IntPtr pwfxDst,    // required destination format
            IntPtr pwfltr,     // optional filter
            IntPtr dwCallback, // callback
            IntPtr dwInstance, // callback instance data
            enuACMOpenMode fdwOpen     // ACM_STREAMOPENF_* and CALLBACK_*
        );
        internal const int ACM_STREAMOPEN_NOFLAG = 0x00000000;
        internal const int ACM_STREAMOPENF_QUERY = 0x00000001;
        internal const int ACM_STREAMOPENF_ASYNC = 0x00000002;
        internal const int ACM_STREAMOPENF_NONREALTIME = 0x00000004;
        internal const int ACM_STREAMSIZEF_SOURCE = 0x00000000;
        internal const int ACM_STREAMSIZEF_DESTINATION = 0x00000001;
        internal const int ACM_STREAMSIZEF_QUERYMASK = 0x0000000F;
        internal const int ACM_STREAMCONVERTF_BLOCKALIGN = 0x00000004;
        internal const int ACM_STREAMCONVERTF_START = 0x00000010;
        internal const int ACM_STREAMCONVERTF_END = 0x00000020;
        [DllImport("Msacm32.dll")]
        internal static extern enuACMResult acmStreamOpen(
          IntPtr phas,//handle that will receive the stream handle
          IntPtr had,
          ref WAVEFORMATEX pwfxSrc,
          ref WAVEFORMATEX pwfxDst,
          ref LPWAVEFILTER pwfltr,
          IntPtr dwCallback,
          IntPtr dwInstance,
          int fdwOpen
        );
        /// <summary>
        /// prepare header
        /// </summary>
        /// <param name="has"></param>
        /// <param name="fdwClose">must close</param>
        /// <returns></returns>
        [DllImport("Msacm32.dll")]
        internal static extern enuACMResult acmStreamClose(
            IntPtr has,
            int fdwClose//close must be zero
);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="has"></param>
        /// <param name="pash"></param>
        /// <param name="fdwPrepare">reserved must be 0</param>
        /// <returns></returns>
        [DllImport("msacm32.dll")]
        internal static extern enuACMResult acmStreamPrepareHeader
   (
       int has,
       ref ACMSTREAMHEADER pash,
       int fdwPrepare
   );
        [DllImport("msacm32.dll")]
        public static extern enuACMResult acmStreamPrepareHeader
   (
       IntPtr has,
       ref IntPtr pash,
       int fdwPrepare
   );
        [DllImport("msacm32.dll")]
        public static extern enuACMResult acmStreamPrepareHeader
   (
       IntPtr has,
       ref ACMSTREAMHEADER pash,
       int fdwPrepare
   );
        [DllImport("msacm32.dll")]
        internal static extern enuACMResult acmFormatSuggest(
  IntPtr had,
  IntPtr pwfxSrc,    // source format to convert
  IntPtr pwfxDst,    // required destination format           
  int cbwfxDst,
  int fdwSuggest
);
        /// <summary>
        /// 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="has"></param>
        /// <param name="pash"></param>
        /// <param name="fdwPrepare">reserved must be 0</param>
        /// <returns></returns>
        [DllImport("msacm32.dll")]
        public static extern enuACMResult acmStreamUnprepareHeader
   (
       IntPtr has,
       ref IntPtr pash,
       int fdwPrepare
   );
        [DllImport("msacm32.dll")]
        internal static extern enuACMResult acmStreamSize
        (
            IntPtr has,
            ref int cbInput,
            ref int pdwOutputBytes,
            enuACMSizeFlag fdwSize
        );
        [DllImport("msacm32.dll")]
        public static extern enuACMResult acmStreamReset
       (
           IntPtr has,
           int fdwReset
       );
        [DllImport("msacm32.dll")]
        public static extern enuACMResult acmStreamConvert
   (
       IntPtr has,
       ref ACMApi.ACMSTREAMHEADER pash,
       enuACMConvertType fdwConvert
   );
        [DllImport("msacm32.dll")]
        public static extern enuACMResult acmStreamConvert
   (
       IntPtr has,
       ref IntPtr pash,
       enuACMConvertType fdwConvert
   );
        [DllImport("msacm32.dll")]
        internal extern static int acmGetVersion();
        [DllImport("msacm32.dll")]
        internal extern static enuACMResult acmDriverOpen(ref IntPtr v_oHandle, IntPtr driverHwnd, int reserved);
        [DllImport("msacm32.dll")]
        internal extern static enuACMResult acmDriverClose(IntPtr driverHwnd, int reserved);
        [DllImport("msacm32.dll")]
        internal extern static enuACMResult acmDriverDetails(
                    IntPtr hadid,
                    ref LPACMDRIVERDETAILS padd,
                    int fdwDetails
);
        [DllImport("msacm32.dll")]
        internal extern static enuACMResult acmDriverDetailsW                  (IntPtr hadid,
                    ref LPACMDRIVERDETAILS padd,
                    int fdwDetails
);
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct ACMSTREAMHEADER
        {
            public int cbStruct;
            public enuACMPreparedStatus fdwStatus;
            public IntPtr dwUser;
            public IntPtr pbSrc;
            public int cbSrcLength;
            public int cbSrcLengthUsed;
            public IntPtr dwSrcUser;
            public IntPtr pbDst;
            public int cbDstLength;
            public int cbDstLengthUsed;
            public IntPtr dwDstUser;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public uint[] dwReservedDriver;
        }
        //[StructLayout(LayoutKind.Sequential, Pack = 1)]
        //public struct PACMSTREAMHEADER
        //{
        //    public int cbStruct;
        //    public enuAcmPreparedStatus fdwStatus;
        //    public IntPtr dwUser;
        //    public byte[] pbSrc;
        //    public int cbSrcLength;
        //    public int cbSrcLengthUsed;
        //    public IntPtr dwSrcUser;
        //    public byte[] pbDst;
        //    public int cbDstLength;
        //    public int cbDstLengthUsed;
        //    public IntPtr dwDstUser;
        //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        //    public uint[] dwReservedDriver;
        //}
        [StructLayout(LayoutKind.Sequential)]        
        public struct LPACMDRIVERDETAILS
        {
            internal uint cbStruct;
            internal uint fccType;
            internal uint fccComp;
            internal short wMid;
            internal short wPid;
            internal int vdwACM;
            internal int vdwDriver;
            internal int fdwSupport;
            internal int cFormatTags;
            internal int cFilterTags;
            //internal IntPtr hicon;
            //internal int hicon;
#if WINDOWS_64
            internal int hicon1;
#endif
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ACMDRIVERDETAILS_SHORTNAME_CHARS)]
            internal string szShortName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ACMDRIVERDETAILS_LONGNAME_CHARS)]
            internal string szLongName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ACMDRIVERDETAILS_COPYRIGHT_CHARS)]
            internal string szCopyright;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ACMDRIVERDETAILS_LICENSING_CHARS)]
            internal string szLicensing;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ACMDRIVERDETAILS_FEATURES_CHARS)]
            internal string szFeatures;
        }
        internal const int ACMFORMATDETAILS_FORMAT_CHARS = 128;
        [StructLayout(LayoutKind.Sequential)]
        internal struct ACMFORMATDETAILS
        {
            internal int cbStruct;
            internal int dwFormatIndex;
            internal int dwFormatTag;
            internal int fdwSupport;
            internal IntPtr pwfx; //WAVEFORMATEX
            internal int cbwfx;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ACMFORMATDETAILS_FORMAT_CHARS)]
            internal string szFormat;
        }
        internal const int ACMFORMATTAGDETAILS_FORMATTAG_CHARS = 48;
        [StructLayout(LayoutKind.Sequential)]
        internal struct ACMFORMATTAGDETAILS
        { 
            internal int cbStruct; 
            internal int dwFormatTagIndex; 
            internal int dwFormatTag; 
            internal int cbFormatSize; 
            internal int fdwSupport; 
            internal int cStandardFormats; 
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ACMFORMATTAGDETAILS_FORMATTAG_CHARS)]    
            internal string szFormatTag; 
        }
        internal const int ACMFILTERDETAILS_FILTER_CHARS   =128;
        [StructLayout(LayoutKind.Sequential)]
        internal struct ACMFILTERDETAILS
        {
            internal int cbStruct;
            internal int dwFilterIndex;
            internal int dwFilterTag;
            internal int fdwSupport;
            internal IntPtr pwfltr;
            internal int cbwfltr;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ACMFILTERDETAILS_FILTER_CHARS)]
            internal string szFilter;
        }
        internal const int ACMFILTERTAGDETAILS_FILTERTAG_CHARS =48;
        [StructLayout(LayoutKind.Sequential)]
        internal struct ACMFILTERTAGDETAILS
        {
            internal int cbStruct;
            internal int dwFilterTagIndex;
            internal int dwFilterTag;
            internal int cbFilterSize;
            internal int fdwSupport;
            internal int cStandardFilters;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ACMFILTERTAGDETAILS_FILTERTAG_CHARS)]
            internal string szFilterTag;
        }
        [StructLayout(LayoutKind.Sequential)]
            internal struct WAVEFILTER { 
            internal int  cbStruct; 
            internal int  dwFilterTag; 
            internal int  fdwFilter;
            [MarshalAs(UnmanagedType.ByValArray , SizeConst = 5)]   
            internal int[]  dwReserved; 
        }
        //class LPWAVEFILTER {
        //    internal WAVEFILTER waveFilter;
        //}
        internal const int ACMSTREAMHEADER_STATUSF_DONE = 0x00010000;
        internal const int ACMSTREAMHEADER_STATUSF_PREPARED = 0x00020000;
        internal const int ACMSTREAMHEADER_STATUSF_INQUEUE = 0x00100000;
        internal const int ACMDRIVERDETAILS_SHORTNAME_CHARS = 32;
        internal const int ACMDRIVERDETAILS_LONGNAME_CHARS = 128;
        internal const int ACMDRIVERDETAILS_COPYRIGHT_CHARS = 80;
        internal const int ACMDRIVERDETAILS_LICENSING_CHARS = 128;
        internal const int ACMDRIVERDETAILS_FEATURES_CHARS = 512;
        /*mp3 constant */
        internal const int MPEGLAYER3_WFX_EXTRA_BYTES = 12;
        internal const int MPEGLAYER3_ID_UNKNOWN            =0;
        internal const int MPEGLAYER3_ID_MPEG               =1;
        internal const int MPEGLAYER3_ID_CONSTANTFRAMESIZE  =2;
        internal const int MPEGLAYER3_FLAG_PADDING_ISO      =0x00000000;
        internal const int MPEGLAYER3_FLAG_PADDING_ON       =0x00000001;
        internal const int MPEGLAYER3_FLAG_PADDING_OFF      =0x00000002;
        [DllImport("msacm32.dll")]
        internal static extern enuACMResult acmStreamUnprepareHeader(
       IntPtr has,
       ref ACMApi.ACMSTREAMHEADER pash,
       int fdwPrepare);
        [StructLayout(LayoutKind.Sequential , Pack = 1, Size = 32 + ACMFORMATTAGDETAILS_FORMATTAG_CHARS + ACMFORMATDETAILS_FORMAT_CHARS + 60)]
        internal struct ACMFORMATCHOOSE
        {
            //[FieldOffset (0)]
            internal int cbStruct;
            //[FieldOffset(4)]
            internal int fdwStyle;
            //[FieldOffset(8)]
            internal IntPtr hwndOwner;
            //[FieldOffset(16)]
            internal IntPtr pwfx;
            //[FieldOffset(24)]
            internal int cbwfx;
            //[FieldOffset(28)]
            internal IntPtr pszTitle;
            //[FieldOffset(32)]
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ACMFORMATTAGDETAILS_FORMATTAG_CHARS)]
            internal string szFormatTag;
            //[FieldOffset(32 + ACMFORMATTAGDETAILS_FORMATTAG_CHARS)]
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ACMFORMATDETAILS_FORMAT_CHARS)]
            internal string szFormat;
            //[FieldOffset(32 + ACMFORMATTAGDETAILS_FORMATTAG_CHARS + ACMFORMATDETAILS_FORMAT_CHARS)]
            internal IntPtr pszName;
            //[FieldOffset(32 + ACMFORMATTAGDETAILS_FORMATTAG_CHARS + ACMFORMATDETAILS_FORMAT_CHARS+8)]
            internal int cchName;
            //[FieldOffset(32 + ACMFORMATTAGDETAILS_FORMATTAG_CHARS + ACMFORMATDETAILS_FORMAT_CHARS+16)]
            internal int fdwEnum;
            //[FieldOffset(32 + ACMFORMATTAGDETAILS_FORMATTAG_CHARS + ACMFORMATDETAILS_FORMAT_CHARS+20)]
            internal IntPtr pwfxEnum;
            //[FieldOffset(32 + ACMFORMATTAGDETAILS_FORMATTAG_CHARS + ACMFORMATDETAILS_FORMAT_CHARS + 28)]
            internal IntPtr hInstance;
            //[FieldOffset(32 + ACMFORMATTAGDETAILS_FORMATTAG_CHARS + ACMFORMATDETAILS_FORMAT_CHARS + 36)]
            internal IntPtr pszTemplateName;
            //[FieldOffset(32 + ACMFORMATTAGDETAILS_FORMATTAG_CHARS + ACMFORMATDETAILS_FORMAT_CHARS + 44)]
            internal IntPtr lCustData;
            //[FieldOffset(32 + ACMFORMATTAGDETAILS_FORMATTAG_CHARS + ACMFORMATDETAILS_FORMAT_CHARS + 52)]
            internal IntPtr pfnHook;
        } ;
        //[StructLayout(LayoutKind.Sequential, Pack = 1, Size = 32 + ACMFORMATTAGDETAILS_FORMATTAG_CHARS + ACMFORMATDETAILS_FORMAT_CHARS + 60)]
        //internal struct ACMFORMATCHOOSE
        //{
        //    [FieldOffset(0)]
        //    internal int cbStruct;
        //    [FieldOffset(4)]
        //    internal int fdwStyle;
        //    [FieldOffset(8)]
        //    internal IntPtr hwndOwner;
        //    [FieldOffset(16)]
        //    internal IntPtr pwfx;
        //    [FieldOffset(24)]
        //    internal int cbwfx;
        //    [FieldOffset(28)]
        //    internal IntPtr pszTitle;
        //    [FieldOffset(32)]
        //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ACMFORMATTAGDETAILS_FORMATTAG_CHARS)]
        //    internal string szFormatTag;
        //    [FieldOffset(32 + ACMFORMATTAGDETAILS_FORMATTAG_CHARS)]
        //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ACMFORMATDETAILS_FORMAT_CHARS)]
        //    internal string szFormat;
        //    [FieldOffset(32 + ACMFORMATTAGDETAILS_FORMATTAG_CHARS + ACMFORMATDETAILS_FORMAT_CHARS)]
        //    internal IntPtr pszName;
        //    [FieldOffset(32 + ACMFORMATTAGDETAILS_FORMATTAG_CHARS + ACMFORMATDETAILS_FORMAT_CHARS + 8)]
        //    internal int cchName;
        //    [FieldOffset(32 + ACMFORMATTAGDETAILS_FORMATTAG_CHARS + ACMFORMATDETAILS_FORMAT_CHARS + 16)]
        //    internal int fdwEnum;
        //    [FieldOffset(32 + ACMFORMATTAGDETAILS_FORMATTAG_CHARS + ACMFORMATDETAILS_FORMAT_CHARS + 20)]
        //    internal IntPtr pwfxEnum;
        //    [FieldOffset(32 + ACMFORMATTAGDETAILS_FORMATTAG_CHARS + ACMFORMATDETAILS_FORMAT_CHARS + 28)]
        //    internal IntPtr hInstance;
        //    [FieldOffset(32 + ACMFORMATTAGDETAILS_FORMATTAG_CHARS + ACMFORMATDETAILS_FORMAT_CHARS + 36)]
        //    internal IntPtr pszTemplateName;
        //    [FieldOffset(32 + ACMFORMATTAGDETAILS_FORMATTAG_CHARS + ACMFORMATDETAILS_FORMAT_CHARS + 44)]
        //    internal IntPtr lCustData;
        //    [FieldOffset(32 + ACMFORMATTAGDETAILS_FORMATTAG_CHARS + ACMFORMATDETAILS_FORMAT_CHARS + 52)]
        //    internal IntPtr pfnHook;
        //} ; 
    }
}

