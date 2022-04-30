

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: MCIWaveOutApi.cs
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
file:MCIWaveOutApi.cs
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
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
namespace IGK.AVIApi.MCI
{
    using AVI;
    using MMSYSTEM;
    using IGK.ICore;using IGK.AVIApi.Native;
    internal static class MCIWaveOutApi
    {
        internal const int TIME_MS = mmSystemAPI.TIME_MS;
        internal const int TIME_SAMPLES = mmSystemAPI.TIME_SAMPLES;
        internal const int TIME_BYTES = mmSystemAPI.TIME_BYTES ;// 0x0004;  /* current byte offset */
        internal const int TIME_SMPTE = mmSystemAPI.TIME_SMPTE ;// 0x0008;  /* SMPTE time */
        internal const int TIME_MIDI = mmSystemAPI.TIME_MIDI ;// 0x0010;  /* MIDI time */
        internal const int TIME_TICKS =mmSystemAPI.TIME_TICKS ;// 0x0020;  /* Ticks within MIDI stream */
        [StructLayout (LayoutKind.Sequential )]
        internal struct WAVEOUTCAPS{
            internal short wMid;
            internal short wPid;
            internal MMVERSION vDriverVersion;
            [MarshalAs (UnmanagedType .ByValTStr , SizeConst = mmSystemAPI .MAXPNAMELEN)]
            internal string szPname;
            internal int dwFormats;
            internal short wChannels;
            internal short wReserved1;
            internal int  dwSupport; 
} 
        [DllImport("winmm.dll")]
        internal static extern int waveOutBreakLoop(IntPtr outHwd);
        [DllImport("winmm.dll")]
        internal static extern int waveOutClose(IntPtr outHwd);
        [DllImport("winmm.dll")]
        internal static extern int waveOutGetDevCaps(IntPtr uDeviceID,//device id or device handle
  ref WAVEOUTCAPS pwic, //wave caps
  int cbwic      //size of waveincaps
            );
 [DllImport("winmm.dll")]
        internal static extern int waveOutGetErrorText (   int errorCode,
            IntPtr buffer,
            int buffSize
            );
 [DllImport("winmm.dll")]
 internal static extern int waveOutGetID(IntPtr hwnd, ref uint LpuId);
 [DllImport("winmm.dll")]
 internal static extern int waveOutGetNumDevs();
 [DllImport("winmm.dll")]
 internal static extern int waveOutGetPitch(IntPtr inHwnd, ref uint value);
 [DllImport("winmm.dll")]
 internal static extern int waveOutGetPlaybackRate(IntPtr inHwnd, ref int value);
 [DllImport("winmm.dll")]
 internal static extern int waveOutGetPosition(IntPtr hwo,  
  ref  MMTIME  pmmt, 
  int cbmmt  );
 [DllImport("winmm.dll")]
 internal static extern int waveOutGetPosition(IntPtr hwo,
  IntPtr  pmmt,
  int cbmmt);
 [DllImport("winmm.dll")]
 internal static extern int waveOutGetVolume(IntPtr outHwnd, ref uint value);
 [DllImport("winmm.dll")]
 internal static extern int waveOutMessage(IntPtr outHwnd,
  enuWaveOutMessage uMsg,
  IntPtr  LParam1,
     IntPtr LParam2  
);
 [DllImport("winmm.dll")]
 internal static extern int waveOutOpen(ref IntPtr phwi,
  uint uDeviceID,
  ref WAVEFORMATEX pwfx,
  IntPtr dwCallback,
  IntPtr dwCallbackInstance,
  int fdwOpen);
 [DllImport("winmm.dll")]
 internal static extern int waveOutPause(IntPtr outHwnd);
 //[DllImport("winmm.dll")]
 //internal static extern int waveOutPrepareHeader(IntPtr uDeviceID,//device id or device handle
 // ref WAVEHDR pwic, //wave caps
 // int cbwic      //size of waveincaps
 //    );
 [DllImport("winmm.dll")]
 internal static extern int waveOutPrepareHeader(IntPtr uDeviceID,//device id or device handle
  IntPtr pwic, //wave caps
  int cbwic      //size of waveincaps
     );
//void CALLBACK waveOutProc(
//  HWAVEOUT hwo,      
//  UINT uMsg,         
//  DWORD_PTR dwInstance,  
//  DWORD_PTR dwParam1,    
//  DWORD_PTR dwParam2     
//);
         [DllImport("winmm.dll")]
        internal static extern int waveOutReset(IntPtr outHwd);
  [DllImport("winmm.dll")]
         internal static extern int waveOutRestart(IntPtr outHwd);
  [DllImport("winmm.dll")]
  internal static extern int waveOutSetPitch(IntPtr outHwd, uint value);
  [DllImport("winmm.dll")]
  internal static extern int waveOutSetPlaybackRate(IntPtr outHwd, int value);
  [DllImport("winmm.dll")]
  internal static extern int waveOutSetVolume(IntPtr outHwnd, uint value);
 //[DllImport("winmm.dll")]
 // internal static extern int waveOutUnprepareHeader(IntPtr uDeviceID,//device id or device handle
 // ref WAVEHDR pwic, //wave caps
 // int cbwic      //size of waveincaps
 //    );
 [DllImport("winmm.dll")]
 internal static extern int waveOutUnprepareHeader(IntPtr uDeviceID,//device id or device handle
 IntPtr pwic, //wave caps
 int cbwic      //size of waveincaps
    );
 [DllImport("winmm.dll")]
  internal static extern int waveOutWrite(IntPtr uDeviceID,//device id or device handle
  ref WAVEHDR pwic, //wave caps
  int cbwic      //size of waveincaps
     );
    [DllImport("winmm.dll")]
  internal static extern int waveOutWrite(IntPtr uDeviceID,//device id or device handle
  IntPtr pwic, //wave caps
  int cbwic      //size of waveincaps
     );
    }
}

