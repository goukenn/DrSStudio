

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: MCIWaveInApi.cs
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
file:MCIWaveInApi.cs
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
using System.IO;
using System.Runtime.InteropServices;
namespace IGK.AVIApi.MCI
{
    using MMIO;
    using AVI;
    using MMSYSTEM;
    using IGK.ICore;using IGK.AVIApi.Native;
    /// <summary>
    /// represent a MCI wave call back functions
    /// </summary>
    /// <param name="hwi"></param>
    /// <param name="uMsg"></param>
    /// <param name="dwInstance"></param>
    /// <param name="dwParam1"></param>
    /// <param name="dwParam2"></param>
    public delegate void MCIWaveInCallback(
  IntPtr hwi,
  uint uMsg,
  IntPtr dwInstance,
  IntPtr dwParam1,
  IntPtr dwParam2
);
    /// <summary>
    /// used to manage int wave appi
    /// </summary>
    internal static class MCIWaveInApi
    {
        internal const int WIM_OPEN = 958;
        internal const int WIM_DATA = 960;
        internal const int WIM_CLOSE = 959;
        [StructLayout(LayoutKind.Sequential)]
        internal struct WAVEINCAPS
        {
            internal ushort wMid;
            internal ushort wPid;
            internal MMVERSION vDriverVersion;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = mmSystemAPI.MAXPNAMELEN)]
            internal string szPname;
            internal enuMCIWaveInFormat dwFormats;
            internal ushort wChannels;
            internal ushort wReserved1;
        }
        #region functions
        [DllImport("winmm.dll")]
        internal extern static int waveInAddBuffer(
            IntPtr hwi,//WaveIN HANDLE
            ref WAVEHDR pwh, //WAVEHRD
            int cbwh  //size of WAVEHDR
            );
        [DllImport("winmm.dll")]
        internal extern static int waveInAddBuffer(
            IntPtr hwi,//WaveIN HANDLE
            IntPtr pwh, //WAVEHRD
            int cbwh  //size of WAVEHDR
            );
        [DllImport("winmm.dll")]
        internal extern static int waveInClose(IntPtr hwi);
        [DllImport("winmm.dll")]
        internal extern static int waveInGetDevCaps(
             IntPtr uDeviceID,//device id or device handle
  ref WAVEINCAPS pwic, //wave caps
  int cbwic      //size of waveincaps
            );
        [DllImport("winmm.dll")]
        internal extern static int waveInGetErrorText(
            int errorCode,
            IntPtr buffer,
            int buffSize
            );
        [DllImport("winmm.dll")]
        internal extern static int waveInGetID(IntPtr whwi, out int id);
        [DllImport("winmm.dll")]
        internal extern static int waveInGetNumDevs();
        [Obsolete()]
        [DllImport("winmm.dll")]
        internal extern static void waveInGetPosition();
        [DllImport("winmm.dll")]
        internal extern static int waveInMessage(
            IntPtr whwi,
            uint msg,
            IntPtr lParam1,
            IntPtr lParam2
            );
        [DllImport("winmm.dll")]
        internal extern static int waveInOpen(ref IntPtr phwi,
  uint uDeviceID,
  ref WAVEFORMATEX pwfx,
  IntPtr dwCallback,
  IntPtr dwCallbackInstance,
  int fdwOpen);
        [DllImport("winmm.dll")]
        internal extern static int waveInPrepareHeader(
                         IntPtr uDeviceID,//device id or device handle
  ref WAVEHDR pwic, //wave caps
  int cbwic      //size of waveincaps
            );
        [DllImport("winmm.dll")]
        internal extern static int waveInPrepareHeader(
                         IntPtr uDeviceID,//device id or device handle
  IntPtr  pwic, //wave caps
  int cbwic      //size of waveincaps
            );
        [DllImport("winmm.dll")]
        internal extern static int waveInReset(IntPtr hdevice);
        [DllImport("winmm.dll")]
        internal extern static int waveInStart(IntPtr hdevice);
        [DllImport("winmm.dll")]
        internal extern static int waveInStop(IntPtr hdevice);
        [DllImport("winmm.dll")]
        internal extern static int waveInUnprepareHeader(
                                    IntPtr uDeviceID,//device id or device handle
  ref WAVEHDR pwic, //wave caps
  int cbwic      //size of waveincaps
            );
        [DllImport("winmm.dll")]
        internal extern static int waveInUnprepareHeader(
                                    IntPtr uDeviceID,//device id or device handle
  IntPtr  pwic, //wave caps
  int cbwic      //size of waveincaps
            );
        #endregion
    }
}

