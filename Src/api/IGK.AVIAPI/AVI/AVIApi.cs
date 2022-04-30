

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AVIApi.cs
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
file:AVIApi.cs
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
/* 
-------------------------------------------------------------
This file is part of iGK-DEV-DrawingStudio
-------------------------------------------------------------
-------------------------------------------------------------
-------------------------------------------------------------
view license file in Documentation folder to get more info
Copyright (c) 2008-2010 
Author  : Charles A.D. BONDJE DOUE 
mail : bondje.doue@hotmail.com
-------------------------------------------------------------
-------------------------------------------------------------
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace IGK.AVIApi.AVI
{
    using IGK.ICore;using IGK.AVIApi.Native;
    
    public delegate long AVISaveProgressionCallBack(int percent);

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AVIStreamInfoStruct
    {
        internal int fccType;
        internal int fccHandler;
        internal int dwFlags;        /* Contains AVITF_* flags */
        internal int dwCaps;
        internal short wPriority;
        internal short wLanguage;
        internal int dwScale;
        internal int dwRate; /* dwRate / dwScale == samples/second */
        internal int dwStart;
        internal int dwLength; /* In units above... */
        internal int dwInitialFrames;
        internal int dwSuggestedBufferSize;
        internal int dwQuality;
        internal int dwSampleSize;
        internal Rectangle rcFrame;
        internal int dwEditCount;
        internal int dwFormatChangeCount;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        internal string szName;
        public string FCCHandlerName { get { return NativeAPI.mmoiToString(this.fccHandler); } }
        public string FCCTypeName { get { return NativeAPI.mmoiToString(this.fccType); } }
    }
    /// <summary>
    /// represent the avi api
    /// </summary>
    internal static class AVIApi
    {
        internal const string LIBNAME = "avifil32.dll";
        internal static int AVIGETFRAMEF_BESTDISPLAYFMT = 1;
        public static readonly int streamTypeANY = 0;
        public static readonly int streamTypeVideo = NativeAPI.mmoiCount('v', 'i', 'd', 's');
        public static readonly int streamTypeAudio = NativeAPI.mmoiCount('a', 'u', 'd', 's');
        public static readonly int streamtypeMIDI = NativeAPI.mmoiCount('m', 'i', 'd', 's');
        public static readonly int streamtypeTEXT = NativeAPI.mmoiCount('t', 'x', 't', 's');
        public static readonly int streamtypeCAUD = NativeAPI.mmoiCount('C', 'A', 'U', 'D');
        public static readonly int streamtypeCVID = NativeAPI.mmoiCount('C', 'V', 'I', 'D');
        public static readonly int streamtypeDIVX = NativeAPI.mmoiCount('D', 'I', 'V', 'X');
        public static readonly int streamtypeXVID = NativeAPI.mmoiCount('X', 'V', 'I', 'D');
        public const int SEVERITY_ERROR = 1;
        public const int FACILITY_ITF = 4;
        public const int FACILITY_WIN32 =7;
        public const int WAVE_FORMAT_PCM = 1;
        //public static string mmoiToString(int v)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append ((char)(v & 255));
        //    sb.Append((char)((v>>8) & 255));
        //    sb.Append((char)((v >> 16) & 255));
        //    sb.Append((char)((v >> 24) & 255));
        //    return sb.ToString();
        //}
        //public static int mmoiCount(char ch0, char ch1, char ch2, char ch3)
        //{
        //    return ((int)ch0 | ((int)ch1 << 8) | ((int)ch2 << 16) | ((int)ch3 << 24));
        //}
        //public static long MAKE_CODE(int sev, int fac, int code) {
        //    return     (long)((sev<<31) | (fac<<16) | (code));
        //}
        //public static long MAKE_AVIERR(int code){
        //    return MAKE_CODE(SEVERITY_ERROR, FACILITY_ITF, code);
        //}
        //public readonly static long AVIERR_UNSUPPORTED      = MAKE_AVIERR(101);
        //public readonly static long AVIERR_BADFORMAT        = MAKE_AVIERR(102);
        //public readonly static long AVIERR_MEMORY           = MAKE_AVIERR(103);
        //public readonly static long AVIERR_INTERNAL         = MAKE_AVIERR(104);
        //public readonly static long AVIERR_BADFLAGS         = MAKE_AVIERR(105);
        //public readonly static long AVIERR_BADPARAM         = MAKE_AVIERR(106);
        //public readonly static long AVIERR_BADSIZE          = MAKE_AVIERR(107);
        //public readonly static long AVIERR_BADHANDLE        = MAKE_AVIERR(108);
        //public readonly static long AVIERR_FILEREAD         = MAKE_AVIERR(109);
        //public readonly static long AVIERR_FILEWRITE        = MAKE_AVIERR(110);
        //public readonly static long AVIERR_FILEOPEN         = MAKE_AVIERR(111);
        //public readonly static long AVIERR_COMPRESSOR       = MAKE_AVIERR(112);
        //public readonly static long AVIERR_NOCOMPRESSOR     = MAKE_AVIERR(113);
        //public readonly static long AVIERR_READONLY		= MAKE_AVIERR(114);
        //public readonly static long AVIERR_NODATA		= MAKE_AVIERR(115);
        //public readonly static long AVIERR_BUFFERTOOSMALL	= MAKE_AVIERR(116);
        //public readonly static long AVIERR_CANTCOMPRESS	= MAKE_AVIERR(117);
        //public readonly static long AVIERR_USERABORT        = MAKE_AVIERR(198);
        //public readonly static long AVIERR_ERROR            = MAKE_AVIERR(199);
        /* constants for the biCompression field */
        internal const int BI_RGB = 0;
        internal const int BI_RLE8 = 1;
        internal const int BI_RLE4 = 2;
        internal const int BI_BITFIELDS = 3;
        internal const int BI_JPEG = 4;
        internal const int BI_PNG = 5;
        internal const int OF_READ = 0x00000000;
        internal const int OF_WRITE = 0x00000001;
        internal const int OF_READWRITE = 0x00000002;
        internal const int OF_SHARE_COMPAT = 0x00000000;
        internal const int OF_SHARE_EXCLUSIVE = 0x00000010;
        internal const int OF_SHARE_DENY_WRITE = 0x00000020;
        internal const int OF_SHARE_DENY_READ = 0x00000030;
        internal const int OF_SHARE_DENY_NONE = 0x00000040;
        internal const int OF_PARSE = 0x00000100;
        internal const int OF_DELETE = 0x00000200;
        internal const int OF_VERIFY = 0x00000400;
        internal const int OF_CANCEL = 0x00000800;
        internal const int OF_CREATE = 0x00001000;
        internal const int OF_PROMPT = 0x00002000;
        internal const int OF_EXIST = 0x00004000;
        internal const int OF_REOPEN = 0x00008000;
        /*ERROR*/
        //2147500037
        internal const uint _MAKE_AVIERR =  unchecked ( ((uint)(int)(SEVERITY_ERROR << 31) | (uint)(FACILITY_ITF << 16)|0x4000 ));
        internal const uint AVIERR_UNSUPPORTED = (uint)(_MAKE_AVIERR  +(101));
        internal const uint AVIERR_BADFORMAT = AVIERR_UNSUPPORTED + 1;
        internal const uint  AVIERR_MEMORY           =(uint)_MAKE_AVIERR +(103);
        internal const uint  AVIERR_INTERNAL         =(uint)_MAKE_AVIERR +(104);
        internal const uint  AVIERR_BADFLAGS         =(uint)_MAKE_AVIERR +(105);
        internal const uint  AVIERR_BADPARAM         =(uint)_MAKE_AVIERR +(106);
        internal const uint  AVIERR_BADSIZE          =(uint)_MAKE_AVIERR +(107);
        internal const uint  AVIERR_BADHANDLE        =(uint)_MAKE_AVIERR +(108);
        internal const uint  AVIERR_FILEREAD         =(uint)_MAKE_AVIERR +(109);
        internal const uint  AVIERR_FILEWRITE        =(uint)_MAKE_AVIERR +(110);
        internal const uint  AVIERR_FILEOPEN         =(uint)_MAKE_AVIERR +(111);
        internal const uint  AVIERR_COMPRESSOR       =(uint)_MAKE_AVIERR +(112);
        internal const uint  AVIERR_NOCOMPRESSOR     =(uint)_MAKE_AVIERR +(113);
        internal const uint  AVIERR_READONLY		=(uint)_MAKE_AVIERR +(114);
        internal const uint  AVIERR_NODATA		=(uint)_MAKE_AVIERR +(115);
        internal const uint  AVIERR_BUFFERTOOSMALL	=(uint)_MAKE_AVIERR +(116);
        internal const uint  AVIERR_CANTCOMPRESS	=(uint)_MAKE_AVIERR +(117);
        internal const uint  AVIERR_USERABORT        =(uint)_MAKE_AVIERR +(198);
        internal const uint  AVIERR_ERROR            =(uint)_MAKE_AVIERR +(199);
        internal const uint EFILE_NOTFOUNT = unchecked(((uint)(SEVERITY_ERROR << 31) | (uint)(FACILITY_WIN32 << 16) | 2));
        internal const uint ERROR_LABEL_TOO_LONG = 154;
        // Flags for dwCaps
        internal const int AVIFILECAPS_CANREAD = 0x00000001;
        internal const int AVIFILECAPS_CANWRITE = 0x00000002;
        internal const int AVIFILECAPS_ALLKEYFRAMES = 0x00000010;
        internal const int AVIFILECAPS_NOCOMPRESSION = 0x00000020;
        //dialog function
        public const UInt32 ICMF_CHOOSE_NONE = 0x0000;
        public const UInt32 ICMF_CHOOSE_KEYFRAME = 0x0001;	// show KeyFrame Every box
        public const UInt32 ICMF_CHOOSE_DATARATE = 0x0002;	// show DataRate box
        public const UInt32 ICMF_CHOOSE_PREVIEW = 0x0004;	// allow expanded preview dialog
        /* Flags for AVI file index */
        public  const int AVIIF_LIST	=0x00000001;
        public  const int AVIIF_TWOCC	=0x00000002;
        public  const int AVIIF_KEYFRAME	=0x00000010;
        public  const int NOERROR =0x0000;
        #region method declarations
        //Initialize the AVI library
        [DllImport(LIBNAME)]
        public static extern void AVIFileInit();
        [DllImport(LIBNAME)]
        public static extern int AVIClearClipboard();
        [DllImport(LIBNAME)]
        public static extern int AVIPutFileOnClipboard();
        [DllImport(LIBNAME)]
        internal extern static IntPtr AVIStreamBeginStreaming(IntPtr  pvidfile, int start, int length, int speed);
        [DllImport(LIBNAME)]
        internal extern static IntPtr AVIStreamEndStreaming(IntPtr  pfile);
        [DllImport(LIBNAME)]
        public static extern int AVIStreamCreate(ref IntPtr pavistream,
            int dummy1,
            int dummy2,
            ref IntPtr classused);
        //Get the start position of a stream
        [DllImport(LIBNAME)]
        public static extern int AVIStreamStart(IntPtr pavistream);
        public static int AVIStreamEnd(IntPtr pavistream) {
            return (int)(AVIStreamStart(pavistream) + AVIStreamLength(pavistream));
        }
        //Get the length of a stream in frames
        [DllImport(LIBNAME, PreserveSig = true)]
        public static extern int AVIStreamLength(int pavi);
        //Get information about an open stream
        [DllImport(LIBNAME)]
        public static extern int AVIStreamInfo(
            IntPtr pAVIStream,
            ref AVIStreamInfoStruct psi,
            int lSize);
        //Get a pointer to a GETFRAME object (returns 0 on error)
        [DllImport(LIBNAME)]
        public static extern IntPtr  AVIStreamGetFrameOpen(
            IntPtr pAVIStream,
            ref BITMAPINFOHEADER bih);
        //Get a pointer to a packed DIB (returns 0 on error)
        [DllImport(LIBNAME)]
        public static extern int AVIStreamGetFrame(
            int pGetFrameObj,
            int lPos);
        //Create a new stream in an open AVI file
        [DllImport(LIBNAME)]
        public static extern int AVIFileCreateStream(
            IntPtr pfile,
            out IntPtr ppavi,
            ref AVIStreamInfoStruct ptr_streaminfo);
        //Create an editable stream
        [DllImport(LIBNAME)]
        public static extern int CreateEditableStream(
            ref IntPtr ppsEditable,
            IntPtr psSource
        );
        //Cut samples from an editable stream
        [DllImport(LIBNAME)]
        public static extern int EditStreamCut(
            IntPtr pStream,
            ref Int32 plStart,
            ref Int32 plLength,
            ref IntPtr ppResult
        );
        //Copy a part of an editable stream
        [DllImport(LIBNAME)]
        public static extern int EditStreamCopy(
            IntPtr pStream,
            ref Int32 plStart,
            ref Int32 plLength,
            ref IntPtr ppResult
        );
        //Paste an editable stream into another editable stream
        [DllImport(LIBNAME)]
        public static extern int EditStreamPaste(
            IntPtr pStream,
            ref int   plPos,
            ref int plLength,
            IntPtr pstream,
            Int32 lStart,
            Int32 lLength
        );
        //Change a stream's header values
        [DllImport(LIBNAME)]
        public static extern int EditStreamSetInfo(
            IntPtr pStream,
            ref AVIStreamInfoStruct lpInfo,
            Int32 cbInfo
        );
        [DllImport(LIBNAME)]
        internal static extern  void EditStreamSetName(IntPtr intPtr, ref IntPtr p);
        [DllImport(LIBNAME)]
        public static extern int AVIMakeFileFromStreams(
            ref IntPtr ppfile,
            int nStreams,
            ref IntPtr papStreams
        );
        [DllImport(LIBNAME)]
        public static extern int AVIMakeFileFromStreams(
            ref IntPtr ppfile,
            int nStreams,
            IntPtr[] papStreams
        );
        //Set the format for a new stream
        [DllImport(LIBNAME)]
        public static extern int AVIStreamSetFormat(
            IntPtr aviStream, Int32 lPos,
            ref BITMAPINFOHEADER lpFormat, Int32 cbFormat);
        [DllImport(LIBNAME)]
        public static extern int AVIStreamSetFormat(
            IntPtr aviStream, Int32 lPos,
            ref WAVEFORMATEX lpFormat, Int32 cbFormat);
        [DllImport(LIBNAME)]
        public static extern int AVIStreamSetFormat(
            IntPtr pStream, Int32 lPos,
            IntPtr lpFormat, Int32 cbFormat);
        //[DllImport(LIBNAME)]
        //public static extern int AVIStreamSetFormat(
        //    IntPtr pStream, Int32 lPos,
        //    AVIStreamInfoStruct lpFormat, 
        //    Int32 cbFormat);
        //Set the format for a new stream
        [DllImport(LIBNAME)]
        public static extern int AVIStreamSetFormat(
            IntPtr aviStream,
            Int32 lPos,
            ref IWAVEFORMAT lpFormat,
            Int32 cbFormat);
        //Read the format for a stream
        [DllImport(LIBNAME)]
        public static extern int AVIStreamReadFormat(
            IntPtr aviStream, Int32 lPos,
            ref BITMAPINFOHEADER lpFormat,
            ref Int32 cbFormat
            );
        [DllImport(LIBNAME)]
        public static extern int AVIStreamReadFormat(
            IntPtr aviStream, Int32 lPos,
            IntPtr lpFormat, ref Int32 cbFormat
            );      
        //Read the format for a stream
        [DllImport(LIBNAME)]
        public static extern int AVIStreamReadFormat(
            IntPtr aviStream, Int32 lPos,
            ref WAVEFORMATEX lpFormat, ref Int32 cbFormat
            );
        [DllImport(LIBNAME)]
        public static extern int AVIStreamReadData(
            IntPtr aviStream, 
            int lkid,
            IntPtr buffer, 
            ref Int32 cbLength
            );
        //Write a sample to a stream
        [DllImport(LIBNAME)]
        public static extern uint  AVIStreamWrite(
            IntPtr aviStream, Int32 lStart, Int32 lSamples,
            IntPtr lpBuffer, Int32 cbBuffer,
            enuAviWriteMode dwFlags,
            Int32 dummy1,
            Int32 dummy2);
        [DllImport(LIBNAME)]
        public static extern uint  AVIStreamWrite(
            IntPtr aviStream, Int32 lStart, Int32 lSamples,
            IntPtr lpBuffer, Int32 cbBuffer,
            enuAviWriteMode dwFlags,
            ref int sampWriten,
            ref int sampByteWritten);
        [DllImport(LIBNAME)]
        internal static extern long AVIStreamWrite(
            IntPtr aviStream, Int32 lStart, Int32 lSamples,
            byte[] lpBuffer, Int32 cbBuffer,
            enuAviWriteMode dwFlags,
            ref int sampWriten,
            ref int sampByteWritten);
        //Release the GETFRAME object
        [DllImport(LIBNAME)]
        public static extern int AVIStreamGetFrameClose(
            int pGetFrameObj);
        //Release an open AVI stream
        [DllImport(LIBNAME)]
        public static extern int AVIStreamRelease(IntPtr aviStream);
        //Release an open AVI file
        [DllImport(LIBNAME)]
        public static extern int AVIFileRelease(int pfile);
        //Close the AVI library
        [DllImport(LIBNAME)]
        public static extern void AVIFileExit();
        [DllImport(LIBNAME)]
        public static extern int AVIMakeCompressedStream(
            out IntPtr ppsCompressed, IntPtr aviStream,
            ref AVICOMPRESSOPTIONS ao, int dummy);
        [DllImport(LIBNAME)]
        public static extern bool AVISaveOptions(
            IntPtr hwnd,
            UInt32 uiFlags,
            Int32 nStreams,
            ref IntPtr ppavi,
            ref AVICOMPRESSOPTIONS_CLASS plpOptions
            );
        [DllImport(LIBNAME)]
        public static extern int AVISaveOptionsFree(
            int nStreams,
            ref AVICOMPRESSOPTIONS_CLASS plpOptions
            );
        [DllImport(LIBNAME)]
        public static extern int AVIFileInfo(
            int pfile,
            ref AVIFileInfoStruct pfi,
            int lSize);
        [DllImport("winmm.dll", EntryPoint = "mmioStringToFOURCCA")]
        public static extern int mmioStringToFOURCC(String sz, int uFlags);
        [DllImport(LIBNAME)]
        public static extern int AVIStreamRead(
            IntPtr pavi,
            Int32 lStart,
            Int32 lSamples,
            IntPtr lpBuffer,
            Int32 cbBuffer,
            ref int plBytes,
            ref int plSamples
            );
        [DllImport(LIBNAME)]
        public static extern int AVIStreamRead(
            IntPtr pavi,
            Int32 lStart,
            Int32 lSamples,
            byte[] lpBuffer,
            int cbBuffer,
            ref int plBytes,
            ref int plSamples
            );
        //[DllImport(LIBNAME)]
        //public static extern enuAviOpenError AVISaveV(
        //    String szFile,
        //    Int16 empty,
        //    AVISaveProgressionCallBack lpfnCallback,
        //    Int16 nStreams,
        //    ref IntPtr ppavi,
        //    ref AVICOMPRESSOPTIONS_CLASS plpOptions
        //    );
        [DllImport(LIBNAME)]
        public static extern int AVISaveV(
            String szFile,
            Int16 empty,
            AVISaveProgressionCallBack lpfnCallback,
            Int16 nStreams,
            IntPtr[] ppavi,
            AVICOMPRESSOPTIONS_CLASS[] plpOptions
            );
        [DllImport(LIBNAME)]
        public static extern int AVISaveV(
            String szFile,
            int empty,
            AVISaveProgressionCallBack lpfnCallback,
            int nStreams,
            IntPtr[] ppavi,
            ref AVICOMPRESSOPTIONS[] plpOptions
            );
        [DllImport(LIBNAME)]
        public static extern int AVISaveV(
            String szFile,
            int empty,
            AVISaveProgressionCallBack lpfnCallback,
            int nStreams,
            IntPtr[] ppavi,
            IntPtr  plpOptions
            );
        [DllImport(LIBNAME)]
        public static extern int AVISave(
            String szFile,
            Int16 empty,
            AVISaveProgressionCallBack lpfnCallback,
            Int16 nStreams,
            IntPtr ppavi,
            ref AVICOMPRESSOPTIONS_CLASS plpOptions
            );
        #endregion method declarations
        [DllImport(LIBNAME)]
        public static extern int AVIFileOpen(out IntPtr pavi, string filename, int mode, IntPtr access);
        [DllImport(LIBNAME, CallingConvention= CallingConvention.StdCall)]
        public static extern int AVIFileInfo(IntPtr pfile, ref AVIFileInfoStruct avifileInfo, int size);
        [DllImport(LIBNAME)]
        internal static extern int AVIGetFromClipboard(
                ref IntPtr  lppf
        );
         [DllImport(LIBNAME)]
        internal static extern int AVIPutFileOnClipboard(
            IntPtr pf
        );
        [DllImport(LIBNAME)]
        public static extern int 
            AVIFileGetStream(
            IntPtr pfile, 
            out IntPtr ppavi, 
            int type, 
            int lparam);        
        //Get a stream from an open AVI file
        [DllImport(LIBNAME)]
        public static extern long AVIFileStreamLength(IntPtr pfile);
        [DllImport(LIBNAME)]
        public static extern int AVIFileRelease(IntPtr pavi);
        [DllImport(LIBNAME)]
        public static extern long AVIStreamLength(IntPtr pavi);
        [DllImport(LIBNAME)]
        public static extern int AVIStreamOpenFromFile(out IntPtr pavi, string filename, IntPtr streamtype, int mode, IntPtr access);
        [DllImport(LIBNAME)]
        public static extern IntPtr AVIStreamGetFrame(IntPtr hfile, int position); //return handler to Bitmap infor
        [DllImport(LIBNAME)]
        public static extern IntPtr AVIStreamGetFrameOpen(
                            IntPtr pavi,
                            IntPtr /*LPBITMAPINFOHEADER*/ lpbiWanted
        );
        [DllImport(LIBNAME)]
        public static extern IntPtr AVIStreamGetFrameClose(
                            IntPtr lpf
        );
        [DllImport(LIBNAME)]
        public static extern long AVIStreamSampleToTime(IntPtr pavi, int length);
        [DllImport(LIBNAME)]
        public static extern long AVIStreamTimeToSample(IntPtr pavi, int timeMiliseconds);
        [DllImport(LIBNAME)]
        public static extern int AVIStreamRead(
  IntPtr  pavi,
  long lStart,
  long lSamples,
  byte[] lpBuffer,
  long cbBuffer,
  IntPtr plBytes,
  IntPtr plSamples);
       [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct AVIFileInfoStruct
        {
            internal int dwMaxBytesPerSec;
            internal int dwFlags;
            internal int dwCaps;
            internal int dwStreams;
            internal int dwSuggestedBufferSize;
            internal int dwWidth;
            internal int dwHeight;
            internal int dwScale;
            internal int dwRate;
            internal int dwLength;
            internal int dwEditCount;
            [MarshalAs(UnmanagedType.ByValTStr , SizeConst = 64)]
            internal string szFileType;
        }
        /// <summary>AviSaveV needs a pointer to a pointer to an AVICOMPRESSOPTIONS structure</summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class AVICOMPRESSOPTIONS_CLASS
        {
            public UInt32 fccType;
            public UInt32 fccHandler;
            public UInt32 dwKeyFrameEvery;  // only used with AVICOMRPESSF_KEYFRAMES
            public UInt32 dwQuality;
            public UInt32 dwBytesPerSecond; // only used with AVICOMPRESSF_DATARATE
            public UInt32 dwFlags;
//#if WINDOWS_64
//            public Int32 lpFormat;
//            public Int32 lpFormat2;
//#else
            public IntPtr lpFormat;
//#endif
            public UInt32 cbFormat;
//#if WINDOWS_64
//            public Int32 lpParms;
//            public Int32 lpParms2;
//#else
            public IntPtr lpParms;
//#endif
            public UInt32 cbParams;
            public UInt32 dwInterleaveEvery;
            public AVICOMPRESSOPTIONS ToStruct()
            {
                AVICOMPRESSOPTIONS returnVar = new AVICOMPRESSOPTIONS();
                returnVar.fccType = this.fccType;
                returnVar.fccHandler = this.fccHandler;
                returnVar.dwKeyFrameEvery = this.dwKeyFrameEvery;
                returnVar.dwQuality = this.dwQuality;
                returnVar.dwBytesPerSecond = this.dwBytesPerSecond;
                returnVar.dwFlags = this.dwFlags;
                returnVar.lpFormat = this.lpFormat;
                returnVar.cbFormat = this.cbFormat;
                returnVar.lpParms = this.lpParms;
                returnVar.cbParms = this.cbParams;
                returnVar.dwInterleaveEvery = this.dwInterleaveEvery;
                return returnVar;
            }
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct AVICOMPRESSOPTIONS
        {
            public UInt32 fccType;
            public UInt32 fccHandler;
            public UInt32 dwKeyFrameEvery;  // only used with AVICOMRPESSF_KEYFRAMES
            public UInt32 dwQuality;
            public UInt32 dwBytesPerSecond; // only used with AVICOMPRESSF_DATARATE
            public UInt32 dwFlags;
//#if WINDOWS_64
//            public Int32 lpFormat;
//            public Int32 lpFormat2;
//#else
            public IntPtr lpFormat;
//#endif
            public UInt32 cbFormat;
//#if  WINDOWS_64
//            public Int32 lpParms;
//            public Int32 lpParms2;
//#else
            public IntPtr lpParms;
//#endif
            public UInt32 cbParms;
            public UInt32 dwInterleaveEvery;
        }
    }
}

