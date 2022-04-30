

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: MCIWaveOutManager.cs
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
file:MCIWaveOutManager.cs
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
    using MMSYSTEM;
    using AVI;
    using IGK.ICore;using IGK.AVIApi.Native;
    /// <summary>
    /// represent the MCI Wave out manager
    /// </summary>
    public static class MCIWaveOutManager
    {
        /// <summary>
        /// return thet number of in wave device installed
        /// </summary>
        public static int Devices { get { return MCIWaveOutApi.waveOutGetNumDevs(); } }
        /// <summary>
        /// create a wave out with default setting
        /// </summary>
        /// <returns></returns>
        public static MCIWaveOutStream CreateWaveOut()
        {
            return CreateWaveOut(mmSystemAPI.WAVE_MAPPER,
                 enuMCIWaveOutOpenFlag.CallBackNull, IntPtr.Zero , IntPtr.Zero );
        }
        /// <summary>
        /// create a wave output stream
        /// </summary>
        /// <param name="devId">device id</param>
        /// <param name="flag">type flag</param>
        /// <returns></returns>
        public static MCIWaveOutStream CreateWaveOut(uint devId, enuMCIWaveOutOpenFlag flag, IntPtr callBack, IntPtr callBackInstance)        
        {
            IntPtr hIn = IntPtr.Zero;
            WAVEFORMATEX v_format = new WAVEFORMATEX();
            v_format.nBlockAlign = 4;
            v_format.nChannels = 2;
            v_format.cbSize =(short) Marshal.SizeOf(v_format);
            v_format.wBitsPerSample = 16;
            v_format.nSamplesPerSec = 44100;
            v_format.nAvgBytesPerSec = 176400;
            v_format.wFormatTag = AVIApi.WAVE_FORMAT_PCM;// 1;
            Handle(MCIWaveOutApi.waveOutOpen(
                ref hIn,
                devId  ,
                ref v_format,
                callBack,
                callBackInstance ,
                (int)flag),"waveOutOpen");
            if (hIn != IntPtr.Zero)
            {
                return MCIWaveOutStream.CreateFromHandle(hIn, v_format );
            }
            return null;
        }
        /// <summary>
        /// create a wave output
        /// </summary>
        /// <param name="devId"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static MCIWaveOutStream CreateWaveOut(uint devId, enuMCIWaveOutOpenFlag flag,
            short blockAlign, short channel, short bitsPerSample, int samplePersecond,
            IntPtr callBack,
            IntPtr callBackInstance)
        {
            IntPtr hIn = IntPtr.Zero;
            WAVEFORMATEX v_format = new WAVEFORMATEX();
            v_format.nBlockAlign = blockAlign ;
            v_format.nChannels = channel ;
            v_format.cbSize = (short)Marshal.SizeOf(v_format);
            v_format.wBitsPerSample = bitsPerSample ;
            v_format.nSamplesPerSec = samplePersecond;//44100;
            v_format.nAvgBytesPerSec = samplePersecond * blockAlign; // avgPerSec;// 176400;
            v_format.wFormatTag = AVIApi.WAVE_FORMAT_PCM;// 1;
            Handle(MCIWaveOutApi.waveOutOpen(
                ref hIn,
                devId,
                ref v_format,
                callBack ,
                callBackInstance ,
                (int)flag), "waveOutOpen");
            if (hIn != IntPtr.Zero)
            {
                return MCIWaveOutStream.CreateFromHandle(hIn , v_format );
            }
            return null;
        }
        internal static void Handle(int error, string message)
        {
          if (error == 0) return;
            string text = string.Empty ;
            IntPtr z = Marshal.AllocCoTaskMem (mmSystemAPI  .MAXERRORLENGTH);
            MCIWaveInApi .waveInGetErrorText (error, z, mmSystemAPI  . MAXERRORLENGTH);
            text = Marshal.PtrToStringAnsi (z);
            Marshal.FreeCoTaskMem (z);
            throw new Exception(string.Format ("{0} : {1}\n{2}" ,error , message, text));
    }
        public static void Close(MCIWaveOutStream OutStream)
        {
            Handle (MCIWaveOutApi.waveOutClose (OutStream.Handle),"waveOutClose");
        }
        internal static MCIWaveOutApi.WAVEOUTCAPS GetDevCaps(MCIWaveOutStream OutStream)
        {
            MCIWaveOutApi.WAVEOUTCAPS casp = new MCIWaveOutApi.WAVEOUTCAPS();
            Handle(MCIWaveOutApi.waveOutGetDevCaps(OutStream.Handle, ref casp, Marshal.SizeOf(casp)), "waveOutGetDevCaps");
            return casp;
        }
        internal static uint GetID(MCIWaveOutStream OutStream)
        {
            uint i = 0;
            Handle(MCIWaveOutApi.waveOutGetID(OutStream.Handle, ref i), "waveOutGetID");
            return i;
        }
        internal static uint GetVolume(MCIWaveOutStream OutStream)
        {
            uint i = 0;
            Handle(MCIWaveOutApi.waveOutGetVolume(OutStream.Handle, ref i), "waveOutGetVolume");
            return i;
        }
        internal static uint GetPitch(MCIWaveOutStream OutStream)
        {
            uint i = 0;
            Handle(MCIWaveOutApi.waveOutGetPitch(OutStream.Handle, ref i), "waveOutGetPitch");
            return i;
        }
        internal static void SetVolume(MCIWaveOutStream OutStream, uint t)
        {            
            Handle(MCIWaveOutApi.waveOutSetVolume(OutStream.Handle, t), "waveOutSetVolume");            
        }
        internal static void SetPitch(MCIWaveOutStream OutStream, uint t)
        {
            Handle(MCIWaveOutApi.waveOutSetPitch(OutStream.Handle, t), "waveOutSetPitch");
        }
        internal static int GetPosition(MCIWaveOutStream mCIWaveOutStream)
        {
            MMTIME t = new MMTIME ();
            t.wType = MCIWaveOutApi.TIME_MS;
            int size = Marshal.SizeOf(t.GetType ());
            IntPtr alloc = Marshal.AllocCoTaskMem(size);
            Marshal.StructureToPtr(t, alloc, true);
            MCIWaveOutApi.waveOutGetPosition(mCIWaveOutStream.Handle,
                alloc ,
                size
                );
            t = (MMTIME) Marshal.PtrToStructure(alloc, t.GetType ());
            Marshal.FreeCoTaskMem(alloc);
            return t.ms;
        }
    }
}

