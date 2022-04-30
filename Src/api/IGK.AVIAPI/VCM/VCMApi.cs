

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: VCMApi.cs
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
file:VCMApi.cs
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
namespace IGK.AVIApi.VCM
{
    /// <summary>
    /// represent the require default Video Compression Manager
    /// </summary>
    internal static  class VCMApi
    {
        public readonly static int VIDC = NativeAPI.mmoiCount('v', 'i', 'd', 'c');
        public readonly static int AUDC = NativeAPI.mmoiCount('a', 'u', 'd', 'c');
        #region "CONSTANT"
        internal const int NULL = 0;
        internal const int ICMODE_COMPRESS = 1;
        internal const int ICMODE_DECOMPRESS = 2;
        internal const int ICMODE_FASTDECOMPRESS = 3;
        internal const int ICMODE_QUERY = 4;
        internal const int ICMODE_FASTCOMPRESS = 5;
        internal const int ICMF_CONFIGURE_QUERY = 0x00000001;
        internal const int ICMF_ABOUT_QUERY = 0x00000001;
        internal const int ICERR_OK = 0;
        internal const int ICERR_DONTDRAW = 1;
        internal const int ICERR_NEWPALETTE = 2;
        internal const int ICERR_GOTOKEYFRAME = 3;
        internal const int ICERR_STOPDRAWING = 4;
        internal const int ICERR_UNSUPPORTED = -1;
        internal const int ICERR_BADFORMAT = -2;
        internal const int ICERR_MEMORY = -3;
        internal const int ICERR_INTERNAL = -4;
        internal const int ICERR_BADFLAGS = -5;
        internal const int ICERR_BADPARAM = -6;
        internal const int ICERR_BADSIZE = -7;
        internal const int ICERR_BADHANDLE = -8;
        internal const int ICERR_CANTUPDATE = -9;
        internal const int ICERR_ABORT = -10;
        internal const int ICERR_ERROR = -100;
        internal const int ICERR_BADBITDEPTH = -200;
        internal const int ICERR_BADIMAGESIZE = -201;
        internal const int ICERR_CUSTOM = -400;
        /* Flags for AVI file index */
        internal const int AVIIF_LIST = 0x00000001;
        internal const int AVIIF_TWOCC = 0x00000002;
        internal const int AVIIF_KEYFRAME = 0x00000010;
        /* quality flags */
        internal const int ICQUALITY_LOW = 0;
        internal const int ICQUALITY_HIGH = 10000;
        internal const int ICQUALITY_DEFAULT = -1;
        /************************************************************************
        ************************************************************************/
        internal const int DRV_USER = 0x4000;
        internal const int ICM_USER = (DRV_USER + 0x0000);
        internal const int ICM_RESERVED = ICM_RESERVED_LOW;
        internal const int ICM_RESERVED_LOW = (DRV_USER + 0x1000);
        internal const int ICM_RESERVED_HIGH = (DRV_USER + 0x2000);
        /************************************************************************
            messages.
        ************************************************************************/
        internal const int ICM_GETSTATE = (ICM_RESERVED + 0);   // Get compressor state
        internal const int ICM_SETSTATE = (ICM_RESERVED + 1);  // Set compressor state
        internal const int ICM_GETINFO = (ICM_RESERVED + 2);  // Query info about the compressor
        internal const int ICM_CONFIGURE = (ICM_RESERVED + 10);   // show the configure dialog
        internal const int ICM_ABOUT = (ICM_RESERVED + 11);  // show the about box
        internal const int ICM_GETERRORTEXT = (ICM_RESERVED + 12); // get error text TBD ;Internal
        internal const int ICM_GETFORMATNAME = (ICM_RESERVED + 20);// get a name for a format ;Internal
        internal const int ICM_ENUMFORMATS = (ICM_RESERVED + 21);// cycle through formats ;Internal
        internal const int ICM_GETDEFAULTQUALITY = (ICM_RESERVED + 30);   // get the default value for quality
        internal const int ICM_GETQUALITY = (ICM_RESERVED + 31);  // get the current value for quality
        internal const int ICM_SETQUALITY = (ICM_RESERVED + 32); // set the default value for quality
        internal const int ICM_SET = (ICM_RESERVED + 40);	// Tell the driver something
        internal const int ICM_GET = (ICM_RESERVED + 41);	// Ask the driver something
        /************************************************************************
            ICM specific messages.
        ************************************************************************/
        internal const int ICM_COMPRESS_GET_FORMAT = (ICM_USER + 4);    // get compress format or size
        internal const int ICM_COMPRESS_GET_SIZE = (ICM_USER + 5);    // get output size
        internal const int ICM_COMPRESS_QUERY = (ICM_USER + 6);    // query support for compress
        internal const int ICM_COMPRESS_BEGIN = (ICM_USER + 7);    // begin a series of compress calls.
        internal const int ICM_COMPRESS = (ICM_USER + 8);    // compress a frame
        internal const int ICM_COMPRESS_END = (ICM_USER + 9);    // end of a series of compress calls.
        internal const int ICM_DECOMPRESS_GET_FORMAT = (ICM_USER + 10);   // get decompress format or size
        internal const int ICM_DECOMPRESS_QUERY = (ICM_USER + 11);   // query support for dempress
        internal const int ICM_DECOMPRESS_BEGIN = (ICM_USER + 12);   // start a series of decompress calls
        internal const int ICM_DECOMPRESS = (ICM_USER + 13);   // decompress a frame
        internal const int ICM_DECOMPRESS_END = (ICM_USER + 14);   // end a series of decompress calls
        internal const int ICM_DECOMPRESS_SET_PALETTE = (ICM_USER + 29);   // fill in the DIB color table
        internal const int ICM_DECOMPRESS_GET_PALETTE = (ICM_USER + 30);   // fill in the DIB color table
        internal const int ICM_DRAW_QUERY = (ICM_USER + 31);   // query support for dempress
        internal const int ICM_DRAW_BEGIN = (ICM_USER + 15);   // start a series of draw calls
        internal const int ICM_DRAW_GET_PALETTE = (ICM_USER + 16);   // get the palette needed for drawing
        internal const int ICM_DRAW_UPDATE = (ICM_USER + 17);   // update screen with current frame ;Internal
        internal const int ICM_DRAW_START = (ICM_USER + 18);   // start decompress clock
        internal const int ICM_DRAW_STOP = (ICM_USER + 19);   // stop decompress clock
        internal const int ICM_DRAW_BITS = (ICM_USER + 20);   // decompress a frame to screen ;Internal
        internal const int ICM_DRAW_END = (ICM_USER + 21);   // end a series of draw calls
        internal const int ICM_DRAW_GETTIME = (ICM_USER + 32);   // get value of decompress clock
        internal const int ICM_DRAW = (ICM_USER + 33);   // generalized "render" message
        internal const int ICM_DRAW_WINDOW = (ICM_USER + 34);   // drawing window has moved or hidden
        internal const int ICM_DRAW_SETTIME = (ICM_USER + 35);   // set correct value for decompress clock
        internal const int ICM_DRAW_REALIZE = (ICM_USER + 36);   // realize palette for drawing
        internal const int ICM_DRAW_FLUSH = (ICM_USER + 37);   // clear out buffered frames
        internal const int ICM_DRAW_RENDERBUFFER = (ICM_USER + 38);   // draw undrawn things in queue
        internal const int ICM_DRAW_START_PLAY = (ICM_USER + 39);   // start of a play
        internal const int ICM_DRAW_STOP_PLAY = (ICM_USER + 40);   // end of a play
        internal const int ICM_DRAW_SUGGESTFORMAT = (ICM_USER + 50);   // Like ICGetDisplayFormat
        internal const int ICM_DRAW_CHANGEPALETTE = (ICM_USER + 51);   // for animating palette
        internal const int ICM_DRAW_IDLE = (ICM_USER + 52);   // send each frame time ;Internal
        internal const int ICM_GETBUFFERSWANTED = (ICM_USER + 41);   // ask about prebuffering
        internal const int ICM_GETDEFAULTKEYFRAMERATE = (ICM_USER + 42);   // get the default value for key frames
        internal const int ICM_DECOMPRESSEX_BEGIN = (ICM_USER + 60);   // start a series of decompress calls
        internal const int ICM_DECOMPRESSEX_QUERY = (ICM_USER + 61);   // start a series of decompress calls
        internal const int ICM_DECOMPRESSEX = (ICM_USER + 62);   // decompress a frame
        internal const int ICM_DECOMPRESSEX_END = (ICM_USER + 63);   // end a series of decompress calls
        internal const int ICM_COMPRESS_FRAMES_INFO = (ICM_USER + 70);   // tell about compress to come
        internal const int ICM_COMPRESS_FRAMES = (ICM_USER + 71);   // compress a bunch of frames ;Internal
        internal const int ICM_SET_STATUS_PROC = (ICM_USER + 72);   // set status callback
        // Constants for ICM_SET:
        public readonly static int ICM_FRAMERATE = NativeAPI.mmoiCount('F', 'r', 'm', 'R');
        public readonly static int ICM_KEYFRAMERATE = NativeAPI.mmoiCount('K', 'e', 'y', 'R');
        #endregion
        //[DllImport("Msvfw32.dll")]
        //public  static extern ICDecompressOpen(uint fccType, uint fccHandler, 
        //    IntPtr lpbiIn,out IntPtr lpbiOut);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fccType"></param>
        /// <param name="fccHandler">LPBITMAPINFOHEADER</param>
        /// <param name="lpbiIn">LPBITMAPINFOHEADER</param>
        /// <param name="lpbiOut"></param>
        /// <param name="wFlags"></param>
        /// <returns></returns>
        [DllImport("Msvfw32.dll")]
        internal static  extern IntPtr ICLocate(int fccType, int fccHandler, 
            IntPtr lpbiIn, 
            IntPtr lpbiOut, 
            enuVCMLocateFlags wFlags);
        [DllImport("Msvfw32.dll")]
        internal static  extern IntPtr ICLocate(int fccType, int fccHandler,
            ref BITMAPINFOHEADER lpbiIn,
            ref BITMAPINFOHEADER lpbiOut,
            enuVCMLocateFlags wFlags);
        [DllImport("Msvfw32.dll")]
        internal static  extern IntPtr ICLocate(
            int fccType, 
            int fccHandler,
                   LPBITMAPINFOHEADER lpbiIn,
                   LPBITMAPINFOHEADER lpbiOut,
            enuVCMLocateFlags wFlags);
        [DllImport("Msvfw32.dll")]
        internal static  extern bool ICInfo(int fccType, int index, ref VCMInfo info);
        [DllImport("Msvfw32.dll")]
        internal static  extern IntPtr ICOpen(int fccType, int fccHandler, enuVCMLocateFlags flag);
        [DllImport("Msvfw32.dll")]
        internal static  extern int ICClose(IntPtr hic);
        [DllImport("Msvfw32.dll")]
        internal static  extern int ICGetInfo(IntPtr hvc, ref VCMInfo info, int size);
        [DllImport("Msvfw32.dll")]
        internal static  extern int ICGetInfo(IntPtr hvc, IntPtr info, int size);
        [DllImport("Msvfw32.dll")]
        internal static  extern bool ICInstall(int fccType, int fccHandler, IntPtr lParam, IntPtr szDesc, int wFlags);
        internal static  bool ICQueryAbout(IntPtr hic)
        {
            enuVCMError h = ICSendMessage(hic, ICM_ABOUT, -1, ICMF_ABOUT_QUERY);// == ICERR_OK;
            return (h == enuVCMError.NoError);
        }
        internal static  bool ICAbout(IntPtr hIC, IntPtr hwndApp)
        {
            enuVCMError h = ICSendMessage(hIC, ICM_ABOUT, hwndApp.ToInt32(), 0);
            return (h == enuVCMError.NoError);
        }
        internal static  bool ICQueryConfigure(IntPtr hic)
        {
            return ICSendMessage(hic, ICM_CONFIGURE, -1, ICMF_CONFIGURE_QUERY) == enuVCMError.NoError;
        }
        internal static  bool ICConfigure(IntPtr hic, IntPtr hwnd)
        {
            return ICSendMessage(hic, ICM_CONFIGURE, hwnd.ToInt32(), 0) == enuVCMError.NoError;
        }
        [DllImport("Msvfw32.dll")]
        internal static  extern enuVCMError ICSendMessage(IntPtr hic, uint Msg, ref int dw1, int dw2);
        [DllImport("Msvfw32.dll")]
        internal static  extern enuVCMError ICSendMessage(IntPtr hic, uint Msg, int dw1, int dw2);
        [DllImport("Msvfw32.dll")]
        internal static extern enuVCMError ICSendMessage(IntPtr hic, uint Msg, IntPtr dw1, IntPtr dw2);
        [DllImport("Msvfw32.dll")]
        internal static extern enuVCMError ICSendMessage(IntPtr hic, uint Msg, IntPtr dw1, int dw2);
        [DllImport("Msvfw32.dll")]
        internal static extern enuVCMError ICSendMessage(IntPtr hic, uint Msg, LPBITMAPINFOHEADER dw1, LPBITMAPINFOHEADER dw2);
        [DllImport("Msvfw32.dll")]
        internal static extern enuVCMError ICSendMessage(IntPtr hic, uint Msg, LPBITMAPINFOHEADER dw1, ref IntPtr dw2);
        /************************************************************************
            decompression functions
        ************************************************************************/
        /*
         *  ICDecompress()
         *
         *  decompress a single frame
         *
         */
        public const int ICDECOMPRESS_HURRYUP = 0x40000000 << 1;// don't draw just buffer (hurry up!)
        [DllImport("Msvfw32.dll")]
        internal static  extern enuVCMError
ICDecompress(
    IntPtr hic,
    int dwFlags,    // flags (from AVI index...)
    LPBITMAPINFOHEADER lpbiFormat, // BITMAPINFO of compressed data
            // biSizeImage has the chunk size
    IntPtr lpData,     // data
    LPBITMAPINFOHEADER lpbi,       // DIB to decompress to
    byte[] lpBits
    );
        internal static  IntPtr ICDecompressOpen(
   int fccType,
   int fccHandler,
   IntPtr lpbiIn,
   IntPtr lpbiOut
 ) {
             IntPtr h = ICLocate(fccType, fccHandler,  lpbiIn,  lpbiOut, 
                 enuVCMLocateFlags.DeCompress);
            return h;
     }
        /// <summary>
        /// get the decompressor 
        /// </summary>
        /// <param name="fccType"></param>
        /// <param name="fccHandler"></param>
        /// <param name="lpbiIn"></param>
        /// <returns></returns>
        internal static  IntPtr ICDecompressOpen(
int fccType,
int fccHandler,
IntPtr lpbiIn
)
        {
            IntPtr h = ICLocate(fccType, fccHandler, lpbiIn, (IntPtr)0,
                enuVCMLocateFlags.DeCompress);
            return h;
        }
        internal static IntPtr ICDecompressOpen(
int fccType,
int fccHandler,
BITMAPINFOHEADER lpbiIn
)
        {
            BITMAPINFOHEADER outb = new BITMAPINFOHEADER();
            IntPtr h = ICLocate(fccType, fccHandler, ref lpbiIn, ref outb ,
                enuVCMLocateFlags.DeCompress);
            return h;
        }
        /*
         *  ICDecompressBegin()
         *
         *  start compression from a source format (lpbiInput) to a dest
         *  format (lpbiOutput) is supported.
         *
         */
        internal static  void ICDecompressBegin(IntPtr hic, IntPtr lpbiInput, IntPtr lpbiOutput)
        {
            ICSendMessage(hic, ICM_DECOMPRESS_BEGIN, lpbiInput, lpbiOutput);
        }
        internal static  bool ICDecompressBegin(IntPtr hic, LPBITMAPINFOHEADER lpbiInput, LPBITMAPINFOHEADER lpbiOutput)
        {
            enuVCMError i = ICSendMessage(hic, ICM_DECOMPRESS_BEGIN, lpbiInput, lpbiOutput);
            return (i == enuVCMError.NoError );
        }
        /*
         *  ICDecompressQuery()
         *
         *  determines if compression from a source format (lpbiInput) to a dest
         *  format (lpbiOutput) is supported.
         *
         */
        internal static  void ICDecompressQuery(IntPtr hic, int lpbiInput, int lpbiOutput)
        {
            ICSendMessage(hic, ICM_DECOMPRESS_QUERY, lpbiInput, lpbiOutput);
        }
        /*
         *  ICDecompressGetFormat()
         *
         *  get the output format, (format of un-compressed data)
         *  if lpbiOutput is NULL return the size in bytes needed for format.
         *
         */
        internal static  int ICDecompressGetFormat(IntPtr hic, LPBITMAPINFOHEADER lpbiInput,
            LPBITMAPINFOHEADER lpbiOutput)
        {
           return (int) ICSendMessage(hic, ICM_DECOMPRESS_GET_FORMAT, lpbiInput, lpbiOutput);
        }
        internal static  int ICDecompressGetFormat(IntPtr hic, LPBITMAPINFOHEADER lpbiInput,
          ref IntPtr lpbiOutput)
        {
            return (int)ICSendMessage(hic, ICM_DECOMPRESS_GET_FORMAT, lpbiInput, ref lpbiOutput);
        }
        internal static  int ICDecompressGetFormatSize(IntPtr hic, LPBITMAPINFOHEADER  lpbi)
        {
            return ICDecompressGetFormat(hic, lpbi, null);
        }
        /*
         *  ICDecompressGetPalette()
         *
         *  get the output palette
         *
         */
        internal static  void ICDecompressGetPalette(IntPtr hic, int lpbiInput, int lpbiOutput)
        {
            ICSendMessage(hic, ICM_DECOMPRESS_GET_PALETTE, lpbiInput, lpbiOutput);
        }
        internal static  void ICDecompressSetPalette(IntPtr hic, int lpbiPalette)
        {
            ICSendMessage(hic, ICM_DECOMPRESS_SET_PALETTE, lpbiPalette, 0);
        }
        internal static  void ICDecompressEnd(IntPtr hic)
        {
            ICSendMessage(hic, ICM_DECOMPRESS_END, 0, 0);
        }
        /************************************************************************
            get/set state macros
        ************************************************************************/
        internal static  int ICGetState(IntPtr hic, int pv, int cb)
        {
            return (int)ICSendMessage(hic, ICM_GETSTATE, pv, cb);
        }
        internal static  int ICGetState(IntPtr hic, IntPtr  pv, int cb)
        {
            return (int)ICSendMessage(hic, ICM_GETSTATE, pv, cb);
        }
        internal static  void ICSetState(IntPtr hic, IntPtr pv, int cb)
        {
            ICSendMessage(hic, ICM_SETSTATE, pv, cb);
        }
        internal static  int   ICGetStateSize(IntPtr hic)
        {
            return ICGetState(hic, NULL, 0);
        }
        /************************************************************************
    get value macros
************************************************************************/
        internal static  int ICGetDefaultQuality(IntPtr hic)
        {
            IntPtr dwIcValue = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(int)));
            ICSendMessage(hic, ICM_GETDEFAULTQUALITY, dwIcValue, (IntPtr)Marshal.SizeOf(typeof(int)));
            int i = Marshal.ReadInt32(dwIcValue);
            Marshal.FreeCoTaskMem(dwIcValue);
            return i;
        }
        internal static  int ICGetDefaultKeyFrameRate(IntPtr hic)
        {
            IntPtr dwIcValue = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(int)));
            ICSendMessage(hic, ICM_GETDEFAULTKEYFRAMERATE, dwIcValue, (IntPtr)Marshal.SizeOf(typeof(int)));
            int i = Marshal.ReadInt32(dwIcValue);
            Marshal.FreeCoTaskMem(dwIcValue);
            return i;
        }
        /************************************************************************
    compression functions
************************************************************************/
/*
 *  ICCompress()
 *
 *  compress a single frame
 *
 */
                [DllImport("Msvfw32.dll")]
        internal static extern enuVCMError
ICCompress(
    IntPtr hic,
    int dwFlags,        // flags
    BITMAPINFOHEADER  lpbiOutput,     // output format
    IntPtr lpData,         // output data
    BITMAPINFOHEADER  lpbiInput,      // format of frame to compress
    IntPtr lpBits,         // frame data to compress
    ref int lpckid,         // ckid for data in AVI file
    ref int lpdwFlags,      // flags in the AVI index.
    long lFrameNum,      // frame number of seq.
    int dwFrameSize,    // reqested size in bytes. (if non zero)
    int dwQuality,      // quality within one frame
    BITMAPINFOHEADER  lpbiPrev,       // format of previous frame
    IntPtr              lpPrev          // previous frame
    );
/*
 *  ICCompressBegin()
 *
 *  start compression from a source format (lpbiInput) to a dest
 *  format (lpbiOuput) is supported.
 *
 */
internal static  void ICCompressBegin(IntPtr hic, int lpbiInput, int lpbiOutput) {
    ICSendMessage(hic, ICM_COMPRESS_BEGIN, lpbiInput, lpbiOutput);
}
/*
 *  ICCompressQuery()
 *
 *  determines if compression from a source format (lpbiInput) to a dest
 *  format (lpbiOuput) is supported.
 *
 */
internal static  void ICCompressQuery(IntPtr hic, int lpbiInput, int lpbiOutput) {
    ICSendMessage(hic, ICM_COMPRESS_QUERY, lpbiInput, lpbiOutput);
    }
/*
 *  ICCompressGetFormat()
 *
 *  get the output format, (format of compressed data)
 *  if lpbiOutput is NULL return the size in bytes needed for format.
 *
 */
internal static  int ICCompressGetFormat(IntPtr hic, int lpbiInput, int lpbiOutput) 
{
    return (int)ICSendMessage(hic, ICM_COMPRESS_GET_FORMAT, lpbiInput, lpbiOutput);
}
internal static  int ICCompressGetFormatSize(IntPtr hic, int lpbi) {
    return    (int )ICCompressGetFormat(hic, lpbi, NULL);
}
/*
 *  ICCompressSize()
 *
 *  return the maximal size of a compressed frame
 *
 */
internal static  int ICCompressGetSize(IntPtr hic, int  lpbiInput,int lpbiOutput) {
    return (int)ICSendMessage(hic, ICM_COMPRESS_GET_SIZE, lpbiInput, lpbiOutput);
}
internal static  void ICCompressEnd(IntPtr hic) {
    ICSendMessage(hic, ICM_COMPRESS_END, 0, 0);
}
/*
 *  ICDrawQuery()
 *
 *  determines if the compressor is willing to render the specified format.
 *
 */
public  static bool ICDrawQuery(IntPtr hic, IntPtr lpbiInput) 
{
   return  ICSendMessage(hic, ICM_DRAW_QUERY, lpbiInput, IntPtr .Zero ) == enuVCMError.NoError ;
}
public  static void ICDrawChangePalette(IntPtr hic, IntPtr  lpbiInput) {
    ICSendMessage(hic, ICM_DRAW_CHANGEPALETTE, lpbiInput, IntPtr.Zero);
}
public  static void ICGetBuffersWanted(IntPtr hic,IntPtr lpdwBuffers) {
    ICSendMessage(hic, ICM_GETBUFFERSWANTED, lpdwBuffers, IntPtr.Zero );
}
public  static void ICDrawEnd(IntPtr hic) {
    ICSendMessage( hic, ICM_DRAW_END, 0, 0);}
public  static void ICDrawStart(IntPtr hic) {
    ICSendMessage(hic, ICM_DRAW_START, 0, 0);}
public  static void ICDrawStartPlay(IntPtr hic,int lFrom, int lTo) {
    ICSendMessage(hic, ICM_DRAW_START_PLAY, lFrom, lTo);}
public  static void ICDrawStop(IntPtr hic) {
    ICSendMessage(hic, ICM_DRAW_STOP, 0, 0);
}
public  static void ICDrawStopPlay(IntPtr hic) {
    ICSendMessage(hic, ICM_DRAW_STOP_PLAY, 0, 0);
}
public  static int ICDrawGetTime(IntPtr hic, int lplTime) {
    return (int)    ICSendMessage(hic, ICM_DRAW_GETTIME, lplTime, 0);
}
public  static void ICDrawSetTime(IntPtr hic, int lTime) 
{
    ICSendMessage(hic, ICM_DRAW_SETTIME,lTime, 0);}
public  static void ICDrawRealize(IntPtr hic, IntPtr hdc, IntPtr fBackground) {
    ICSendMessage(hic, ICM_DRAW_REALIZE, hdc,fBackground);}
public  static void ICDrawFlush(IntPtr hic) {
    ICSendMessage(hic, ICM_DRAW_FLUSH, 0, 0);
}
public  static void ICDrawRenderBuffer( IntPtr hic) {
    ICSendMessage(hic, ICM_DRAW_RENDERBUFFER, 0, 0);
}
/************************************************************************
    Status callback functions
************************************************************************/
/*
 *  ICSetStatusProc()
 *
 *  Set the status callback function
 *
 */
// ICMessage is not supported on NT
internal static  enuVCMError ICSetStatusProc(
            IntPtr hic,
            int dwFlags,
            IntPtr lParam,
            ICSETSTATUSPROCHandler fcfunc )
{
    ICSETSTATUSPROC ic;
    ic.dwFlags = dwFlags;
    ic.lParam = lParam;
    ic.Status = Marshal.GetFunctionPointerForDelegate(fcfunc);
    int v_size= Marshal.SizeOf (ic );
    IntPtr alloc = Marshal.AllocCoTaskMem (v_size );
    Marshal.StructureToPtr(ic, alloc, true);
    // note that ICM swaps round the length and pointer
    // length in lparam2, pointer in lparam1
    enuVCMError  r = ICSendMessage(hic, ICM_SET_STATUS_PROC, alloc, (IntPtr)v_size);
    Marshal.FreeCoTaskMem(alloc);
    return r;
}
        [StructLayout (LayoutKind.Sequential )]
        internal struct ICDECOMPRESS
        {
          int               dwFlags;
          LPBITMAPINFOHEADER lpbiInput;
          IntPtr lpInput;
          LPBITMAPINFOHEADER lpbiOutput;
          IntPtr             lpOutput;
          int              ckid;
        };
    }
}

