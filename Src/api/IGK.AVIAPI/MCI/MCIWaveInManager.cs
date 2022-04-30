

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: MCIWaveInManager.cs
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
file:MCIWaveInManager.cs
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
namespace IGK.AVIApi.MCI
{
    using MMSYSTEM ;
    using AVI;
    using IGK.ICore;using IGK.AVIApi.Native;
    /// <summary>
    /// represent the public wave in manager. used for recording
    /// </summary>
    public static class MCIWaveInManager
    {
        internal static readonly int HEADER_SIZE = Marshal.SizeOf(typeof(WAVEHDR));
        /// <summary>
        /// return the number of wave in device
        /// </summary>
        public static int Devices { get { return MCIWaveInApi.waveInGetNumDevs(); } }
        internal static MCIWaveInApi.WAVEINCAPS GetDeviceCaps(uint id)
        {
            MCIWaveInApi.WAVEINCAPS v_caps = new MCIWaveInApi.WAVEINCAPS ();
            int error = MCIWaveInApi .waveInGetDevCaps (new  IntPtr (id), 
                ref v_caps ,
                Marshal.SizeOf (v_caps ));
            return v_caps;
        }
        public static void DeviceCaps(uint id) { GetDeviceCaps(id); }
        public static int GetWaveInId(IntPtr wIn)
        {
            int i = 0;
            Handle(MCIWaveInApi.waveInGetID(wIn, out  i), "waveInGetID");
            return i;
        }
        internal static 
            void Handle(int error, string message)
        {
            if (error == 0) return;
            string text = string.Empty ;
            IntPtr z = Marshal.AllocCoTaskMem (mmSystemAPI  .MAXERRORLENGTH);
            MCIWaveInApi .waveInGetErrorText (error, z, mmSystemAPI  . MAXERRORLENGTH);
            text = Marshal.PtrToStringAnsi (z);
            Marshal.FreeCoTaskMem (z);
            throw new Exception(string.Format ("{0} : {1}\n{2}" ,error , message, text));
        }
        public static MCIWaveInStream Open(int deviceId, int blockAlign, int bitPSample, int channel, int nsamplePerSec)
        {
            return MCIWaveInStream.Create (deviceId, blockAlign  , bitPSample , channel , nsamplePerSec );
        }
        /// <summary>
        /// Open a MCI wave in stream
        /// </summary>
        /// <param name="deviceId">device id for record</param>
        /// <param name="avg">average 44100</param>
        /// <param name="bitPSample">number of bit per sample : 8 or 16</param>
        /// <param name="channel">channel : 1 or 2</param>
        /// <param name="nsamplePerSec">number of sample per sec : 176400</param>
        /// <param name="callBack">the callback function</param>
        /// <returns></returns>
        public static MCIWaveInStream Open(int deviceId, int avg, int bitPSample, int channel, int nsamplePerSec, MCIWaveInCallback callBack)
        {
            IntPtr ht = IntPtr.Zero;
            IntPtr function = (callBack != null) ? Marshal.GetFunctionPointerForDelegate(callBack) : IntPtr.Zero;
            enuMCIWaveOutOpenFlag v_flag = (callBack != null) ? enuMCIWaveOutOpenFlag.Function | enuMCIWaveOutOpenFlag.FormatDirect : 
                enuMCIWaveOutOpenFlag.WaveMapped | enuMCIWaveOutOpenFlag.FormatDirect | enuMCIWaveOutOpenFlag.CallBackNull ;
            WAVEFORMATEX f = new WAVEFORMATEX();
            f.wFormatTag = 1;
            f.nAvgBytesPerSec = avg;// 44100;
            f.wBitsPerSample = (short)bitPSample;// 16;
            f.cbSize = 0;// (short)Marshal.SizeOf(f);
            f.nChannels = (short)channel;// 2;// 1 for mono - 2 for stereo
            f.nBlockAlign = 4;
            f.nSamplesPerSec = nsamplePerSec;// 176400;
            Handle(MCIWaveInApi.waveInOpen(
                ref ht,
                (uint)(new IntPtr(deviceId).ToInt32()),
                ref f,
                function ,
                IntPtr.Zero,
                (int)(v_flag  )), "waveInOpen");
            MCIWaveInStream c = MCIWaveInStream.Create(ht);
            //if (callBack !=null)
            //    callBack += c.ProcHandle;
            return c;
        }
        public static MCIWaveInStream Open(int deviceId)
        {
            IntPtr ht = IntPtr.Zero ;
            WAVEFORMATEX f = new WAVEFORMATEX ();
            f.wFormatTag = 1;
            f.nAvgBytesPerSec = 44100;
            f.wBitsPerSample = 16;
            f.cbSize = (short)Marshal.SizeOf(f);
            f.nChannels = 2;// 1 for mono - 2 for stereo
            f.nBlockAlign = 4;
            f.nSamplesPerSec = 176400;
            Handle (MCIWaveInApi .waveInOpen (
                ref ht,
                (uint)( new IntPtr (deviceId).ToInt32()),
                ref f,
                IntPtr.Zero , 
                IntPtr .Zero ,
                (int) enuMCIWaveInOpenFlag .WaveMapped ) , "waveInOpen");
            return MCIWaveInStream.Create (ht);
        }
        public static bool Start(IntPtr hwaveIn)
        {
            Handle(MCIWaveInApi.waveInStart(hwaveIn),
                "waveInStart");
            return true;
        }
        public static bool Stop(IntPtr hwaveIn)
        {
            Handle(MCIWaveInApi.waveInStop(hwaveIn),
                "waveInStop");
            return true;
        }
        internal static void Close(IntPtr hwaveIn)
        {
            Handle(MCIWaveInApi.waveInClose(hwaveIn), "waveInClose");
        }
        /// <summary>
        /// reset the hwaveIn
        /// </summary>
        /// <param name="intPtr"></param>
        internal static void Reset(IntPtr hwaveIn)
        {
            Handle(MCIWaveInApi.waveInReset(hwaveIn), "waveInReset");
        }
        internal static void PrepareHeader(IntPtr hwaveIn, ref IntPtr preparedHeader, int bufferSize)
        {
            WAVEHDR hdr = new WAVEHDR ();
            hdr.lpData = Marshal.AllocCoTaskMem(bufferSize );
            hdr.dwBufferLength = bufferSize ;
            hdr.dwFlags = 0;
            IntPtr v_alloc = Marshal.AllocCoTaskMem(Marshal.SizeOf(hdr));
            Marshal.StructureToPtr(hdr, v_alloc, true);
            Handle(MCIWaveInApi.waveInPrepareHeader(hwaveIn,
                v_alloc,
                Marshal.SizeOf(hdr)), "waveInPrepareHeader");            
            preparedHeader = v_alloc;
            ;
        }
        internal static void AddBuffer(IntPtr hwaveIn, IntPtr preparedHeader)
        {
            Handle(MCIWaveInApi.waveInAddBuffer(hwaveIn,
                preparedHeader,
                HEADER_SIZE ), "waveInAddBuffer");
        }
        internal static void UnPrepareHeader(IntPtr hwaveIn, IntPtr  preparedHeader)
        {
           Handle ( MCIWaveInApi.waveInUnprepareHeader(hwaveIn,
                preparedHeader,
                HEADER_SIZE ), "waveInUnprepareHader");
        }
    }
}

