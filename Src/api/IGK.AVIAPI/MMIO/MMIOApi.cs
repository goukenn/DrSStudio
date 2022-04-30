

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: MMIOApi.cs
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
file:MMIOApi.cs
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
using System.Runtime.InteropServices ;
namespace IGK.AVIApi.MMIO
{
    /// <summary>
    /// represent a mm io proc andler
    /// </summary>
    /// <param name="lpmmioinfo"></param>
    /// <param name="uMsg"></param>
    /// <param name="lParam1"></param>
    /// <param name="lParam2"></param>
    /// <returns></returns>
    internal  delegate int MMIOProcHandler(ref MMIOApi.MMIOINFO  lpmmioinfo,enuMMIOMessage  uMsg,IntPtr  lParam1,IntPtr  lParam2);
    
    /// <summary>
    /// represent the api
    /// </summary>
    internal static class MMIOApi
    {
        #region constant
        internal const int WAVE_FORMAT_PCM = 1;
        internal const int  MMIOERR_BASE       =         256;
        internal const int  MMIOERR_FILENOTFOUND        =(MMIOERR_BASE + 1);  /* file not found */
        internal const int  MMIOERR_OUTOFMEMORY         =(MMIOERR_BASE + 2);  /* out of memory */
        internal const int  MMIOERR_CANNOTOPEN          =(MMIOERR_BASE + 3);  /* cannot open */
        internal const int  MMIOERR_CANNOTCLOSE         =(MMIOERR_BASE + 4);  /* cannot close */
        internal const int  MMIOERR_CANNOTREAD          =(MMIOERR_BASE + 5);  /* cannot read */
        internal const int  MMIOERR_CANNOTWRITE         =(MMIOERR_BASE + 6);  /* cannot write */
        internal const int  MMIOERR_CANNOTSEEK          =(MMIOERR_BASE + 7);  /* cannot seek */
        internal const int  MMIOERR_CANNOTEXPAND        =(MMIOERR_BASE + 8);  /* cannot expand file */
        internal const int  MMIOERR_CHUNKNOTFOUND       =(MMIOERR_BASE + 9);  /* chunk not found */
        internal const int  MMIOERR_UNBUFFERED          =(MMIOERR_BASE + 10); /*  */
        internal const int  MMIOERR_PATHNOTFOUND        =(MMIOERR_BASE + 11); /* path incorrect */
        internal const int  MMIOERR_ACCESSDENIED        =(MMIOERR_BASE + 12); /* file was protected */
        internal const int  MMIOERR_SHARINGVIOLATION    =(MMIOERR_BASE + 13); /* file in use */
        internal const int  MMIOERR_NETWORKERROR        =(MMIOERR_BASE + 14); /* network not responding */
        internal const int  MMIOERR_TOOMANYOPENFILES    =(MMIOERR_BASE + 15); /* no more file handles  */
        internal const int  MMIOERR_INVALIDFILE         =(MMIOERR_BASE + 16); /* default error file error */
        /* bit field masks */
        internal const int MMIO_RWMODE = 0x00000003;      /* open file for reading/writing/both */
        internal const int MMIO_SHAREMODE = 0x00000070;      /* file sharing mode number */
        /* constants for dwFlags field of MMIOINFO */
        internal const int MMIO_CREATE = 0x00001000;      /* create new file (or truncate file) */
        internal const int MMIO_PARSE = 0x00000100;      /* parse new file returning path */
        internal const int MMIO_DELETE = 0x00000200;      /* create new file (or truncate file) */
        internal const int MMIO_EXIST = 0x00004000;      /* checks for existence of file */
        internal const int MMIO_ALLOCBUF = 0x00010000;      /* mmioOpen() should allocate a buffer */
        internal const int MMIO_GETTEMP = 0x00020000;      /* mmioOpen() should retrieve temp name */
        internal const int MMIO_DIRTY = 0x10000000;      /* I/O buffer is dirty */
        /* read/write mode numbers (bit field MMIO_RWMODE) */
        internal const int MMIO_READ = 0x00000000;      /* open file for reading only */
        internal const int MMIO_WRITE = 0x00000001;      /* open file for writing only */
        internal const int MMIO_READWRITE = 0x00000002;      /* open file for reading and writing */
        /* share mode numbers (bit field MMIO_SHAREMODE) */
        internal const int MMIO_COMPAT = 0x00000000;      /* compatibility mode */
        internal const int MMIO_EXCLUSIVE = 0x00000010;      /* exclusive-access mode */
        internal const int MMIO_DENYWRITE = 0x00000020;      /* deny writing to other processes */
        internal const int MMIO_DENYREAD = 0x00000030;      /* deny reading to other processes */
        internal const int MMIO_DENYNONE = 0x00000040;      /* deny nothing to other processes */
        /* various MMIO flags */
        internal const int MMIO_FHOPEN = 0x0010;  /* mmioClose: keep file handle open */
        internal const int MMIO_EMPTYBUF = 0x0010;  /* mmioFlush: empty the I/O buffer */
        internal const int MMIO_TOUPPER = 0x0010;  /* mmioStringToFOURCC: to u-case */
        internal const int MMIO_INSTALLPROC = 0x00010000;  /* mmioInstallIOProc: install MMIOProc */
        internal const int MMIO_GLOBALPROC = 0x10000000;  /* mmioInstallIOProc: install globally */
        internal const int MMIO_REMOVEPROC = 0x00020000;  /* mmioInstallIOProc: remove MMIOProc */
        internal const int MMIO_UNICODEPROC = 0x01000000;  /* mmioInstallIOProc: Unicode MMIOProc */
        internal const int MMIO_FINDPROC = 0x00040000;  /* mmioInstallIOProc: find an MMIOProc */
        internal const int MMIO_FINDCHUNK = 0x0010;  /* mmioDescend: find a chunk by ID */
        internal const int MMIO_FINDRIFF = 0x0020;  /* mmioDescend: find a LIST chunk */
        internal const int MMIO_FINDLIST = 0x0040;  /* mmioDescend: find a RIFF chunk */
        internal const int MMIO_CREATERIFF = 0x0020;  /* mmioCreateChunk: make a LIST chunk */
        internal const int MMIO_CREATELIST = 0x0040;  /* mmioCreateChunk: make a RIFF chunk */
        /* message numbers for MMIOPROC I/O procedure functions */
        internal const int MMIOM_READ = MMIO_READ;     /* read */
        internal const int MMIOM_WRITE = MMIO_WRITE;     /* write */
        internal const int MMIOM_SEEK = 2;     /* seek to a new position in file */
        internal const int MMIOM_OPEN = 3;     /* open file */
        internal const int MMIOM_CLOSE = 4;     /* close file */
        internal const int MMIOM_WRITEFLUSH = 5;      /* write and flush */
        internal const int MMIOM_RENAME = 6;    /* rename specified file */
        internal const int MMIOM_USER = 0x8000;   /* beginning of user-defined messages */
        /* standard four character codes */
        internal static readonly int FOURCC_WAVE = mmioFOURCC('W', 'A', 'V', 'E');
        internal static readonly int FOURCC_fmt = mmioFOURCC('f', 'm', 't', ' ');
        internal static readonly int FOURCC_data = mmioFOURCC('d', 'a', 't', 'a');
        internal static readonly int FOURCC_RIFF = mmioFOURCC('R', 'I', 'F', 'F');
        internal static readonly int FOURCC_LIST = mmioFOURCC('L', 'I', 'S', 'T');
        /* four character codes used to identify standard built-in I/O procedures */
        internal static readonly int FOURCC_DOS = mmioFOURCC('D', 'O', 'S', ' ');
        internal static readonly int FOURCC_MEM = mmioFOURCC('M', 'E', 'M', ' ');
        /* flags for mmioSeek() */
        internal const int SEEK_SET = 0;               /* seek to an absolute position */
        internal const int SEEK_CUR = 1;               /* seek relative to current position */
        internal const int SEEK_END = 2;               /* seek relative to end of file */
        /* other constants */
        internal const int MMIO_DEFAULTBUFFER = 8192;/* default buffer size */
        /* MMIO macros */
        #endregion
        internal static int mmioFOURCC(char ch0, char ch1, char ch2, char ch3)
        {
            return ((int)(byte)(ch0) | ((int)(byte)(ch1) << 8) |
                       ((int)(byte)(ch2) << 16) | ((int)(byte)(ch3) << 24));
        }
        internal static int getChunck(char[] tab)
        {
            int r = 0;
            for (int i = tab.Length - 1; i >= 0; --i)
            {
                r += tab[i];
                if (i > 0)
#pragma warning disable IDE0054 // Use compound assignment
                    r = r << 8;
#pragma warning restore IDE0054 // Use compound assignment
            }
            return r;
        }
        internal static string getChunckString(int t)
        {
            string str = "" + (char)(t & 0xFF) +
                            (char)((t & 0xFF00) >> 8) +
                            (char)((t & 0xFF0000) >> 16) +
                            (char)((t & 0xFF000000) >> 24);
            return str;
        }
        #region "Function"
/* MMIO function prototypes */
        [DllImport ("winmm.dll", CallingConvention= CallingConvention.StdCall )]
        internal static extern int mmioInstallIOProc(
            int fccIOProc,
            MMIOProcHandler pIOProc,
            int dwFlags);
        /// <summary>
        /// intall mmio procedure
        /// </summary>
        /// <param name="fccIOProc"></param>
        /// <param name="pIOProc"></param>
        /// <param name="dwFlags"></param>
        /// <returns></returns>
        [DllImport("winmm.dll", CallingConvention= CallingConvention.StdCall )]
        internal static extern int mmioInstallIOProc(
            int fccIOProc,
            IntPtr pIOProc,
            int dwFlags);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pszFileName">128 bytes string file name</param>
        /// <param name="pmmioinfo"></param>
        /// <param name="dwOpen"></param>
        /// <returns></returns>
        [DllImport ("winmm.dll")]
        internal static extern IntPtr mmioOpen(string pszFileName,
            ref MMIOINFO pmmioinfo,  
            int dwOpen);
       [DllImport ("winmm.dll", CallingConvention= CallingConvention.StdCall )]
        internal static extern int mmioRename(string pszFileName, 
    string pszNewFileName, 
    IntPtr pmmioinfo,
    int fdwRename);
 [DllImport ("winmm.dll", CallingConvention= CallingConvention.StdCall )] internal static extern int  mmioClose( IntPtr  hmmio,  uint fuClose);
 [DllImport("winmm.dll", CallingConvention= CallingConvention.StdCall  )] internal static extern int mmioRead(int hmmio, byte[] pch, int cch);
 [DllImport("winmm.dll", CallingConvention = CallingConvention.StdCall)]
 internal static extern int mmioRead(IntPtr hmmio, IntPtr pch, long cch);
 [DllImport ("winmm.dll", CallingConvention= CallingConvention.StdCall )] internal static extern int mmioWrite( IntPtr hmmio, byte[]  pch,  long cch);
 [DllImport("winmm.dll", CallingConvention= CallingConvention.StdCall )] internal static extern int mmioWrite(IntPtr hmmio, char[] pch, long cch);
 [DllImport("winmm.dll", CallingConvention= CallingConvention.StdCall )]  internal static extern int mmioWrite(IntPtr hmmio, IntPtr pch, long cch);
 [DllImport("winmm.dll",  CallingConvention = CallingConvention.StdCall )]
 internal static extern int mmioSeek(int hmmio, int Offset, int iOrigin);
 [DllImport ("winmm.dll")] internal static extern int  mmioGetInfo( IntPtr hmmio, ref MMIOINFO  pmmioinfo,  uint fuInfo);
 [DllImport ("winmm.dll", CallingConvention= CallingConvention.StdCall )] internal static extern int  mmioSetInfo( IntPtr hmmio,  
     ref MMIOINFO pmmioinfo,  
     uint fuInfo);
 [DllImport ("winmm.dll", CallingConvention= CallingConvention.StdCall )] internal static extern int  mmioSetBuffer( IntPtr hmmio, 
     IntPtr  pchBuffer,  
     long cchBuffer,
     uint fuBuffer);
 [DllImport ("winmm.dll", CallingConvention= CallingConvention.StdCall )] internal static extern int  mmioFlush( IntPtr hmmio,  uint fuFlush);
 [DllImport ("winmm.dll", CallingConvention= CallingConvention.StdCall )] internal static extern int  mmioAdvance( 
     IntPtr hmmio,  
     ref MMIOINFO pmmioinfo,  
     uint fuAdvance);
[DllImport ("winmm.dll", CallingConvention= CallingConvention.StdCall )] internal static extern IntPtr  mmioSendMessage( 
    IntPtr hmmio,  
    uint uMsg,
    IntPtr  lParam1, 
    IntPtr lParam2);
 [DllImport ("winmm.dll", CallingConvention= CallingConvention.StdCall )] internal static extern int  mmioDescend( IntPtr hmmio,
     ref MMIOChunkINFO pmmcki,
     ref MMIOChunkINFO pmmckiParent,  
     uint fuDescend);
 [DllImport("winmm.dll", CallingConvention= CallingConvention.StdCall )]
 internal static extern int mmioDescend(IntPtr hmmio,
     out MMIOChunkINFO pmmcki,
     IntPtr  pmmckiParent,
     uint fuDescend);
 [DllImport ("winmm.dll", CallingConvention= CallingConvention.StdCall )] 
        internal static extern int  mmioAscend( int hmmio, ref  MMIOChunkINFO pmmcki,  uint fuAscend);
 [DllImport ("winmm.dll", CallingConvention= CallingConvention.StdCall )] internal static extern int  mmioCreateChunk(IntPtr hmmio, ref  MMIOChunkINFO pmmcki,  uint fuCreate);
        #endregion
        [StructLayout (LayoutKind.Sequential )]
        internal struct MMIOChunkINFO
        {
             internal int ckid;
             internal int cksize;
             internal int fccType;
             internal int dwDataOffset;
             internal int dwFlags;
            public static MMIOChunkINFO Empty;
            static MMIOChunkINFO() {
                Empty = new MMIOChunkINFO();
            }
             public bool IsEmpty {
                 get {
                     return this.Equals(Empty);
                 }
             }
             public string FCCTypeName
             {
                 get{
                     return NativeAPI.mmoiToString(this.fccType);
                 }
             }
             public string CidName
             {
                 get
                 {
                     return NativeAPI.mmoiToString(this.ckid );
                 }
             }
        }
        [StructLayout(LayoutKind.Sequential)]
        internal struct MMIOINFO
        { 
            internal int       dwFlags; 
            internal int     fccIOProc; 
            internal IntPtr  pIOProc; 
            internal uint      wErrorRet; 
            internal IntPtr      hTask;
            internal long cchBuffer; 
            internal IntPtr      pchBuffer; 
            internal IntPtr      pchNext; 
            internal IntPtr      pchEndRead; 
            internal IntPtr      pchEndWrite; 
            internal long lBufOffset; 
            internal long lDiskOffset; 
            [MarshalAs (UnmanagedType .ByValArray , SizeConst = 4)]
            internal int[]      adwInfo; 
            internal int       dwReserved1; 
            internal int       dwReserved2; 
            internal IntPtr      hmmio;
            public string FCCProcName {
                get {
                    return NativeAPI.mmoiToString(this.fccIOProc);
                }
            }
        }
        [StructLayout(LayoutKind.Sequential)]
        internal struct PCMWAVEFORMAT
        {
            //  WAVEFORMAT wf; 
            internal short wFormatTag;
            internal short nChannels;
            internal int nSamplesPerSec;
            internal int nAvgBytesPerSec;
            internal short nBlockAlign;
            internal short wBitsPerSample;
        }
    }
}

