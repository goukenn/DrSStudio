

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: MCIWaveOutStream.cs
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
file:MCIWaveOutStream.cs
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
    using IGK.ICore;using IGK.AVIApi.AVI;
    using IGK.AVIApi.Native;
    /// <summary>
    /// used to play wave out in output
    /// </summary>
    public class MCIWaveOutStream : IDisposable 
    {
        IntPtr m_handle;
        IntPtr m_outBuffer;
        IntPtr m_preparedHeader;
        private bool m_closed;
        private MCIWaveOutApi.WAVEOUTCAPS m_caps;
        private WAVEFORMATEX m_format;
        private bool m_IsPlaying;
        private bool m_Loop;
        private int m_LoopTimes;
        public int LoopTimes
        {
            get { return m_LoopTimes; }
            set
            {
                if (m_LoopTimes != value)
                {
                    m_LoopTimes = value;
                }
            }
        }
        /// <summary>
        /// get or set the loop
        /// </summary>
        public bool Loop
        {
            get { return m_Loop; }
            set
            {
                if (m_Loop != value)
                {
                    m_Loop = value;
                }
            }
        }
        public int AvgBytePerSecond { get { return this.m_format.nAvgBytesPerSec; } }
        public int SamplePerSecond { get { return this.m_format.nSamplesPerSec; } }
        public int BlockAlign { get { return this.m_format.nBlockAlign; } }
        public int Bits { get { return this.m_format.wBitsPerSample; } }
        /// <summary>
        /// get if this is playing
        /// </summary>
        public bool IsPlaying
        {
            get { return m_IsPlaying; }
        }
        public IntPtr Handle { get { return this.m_handle; } }
        public bool SupportVolume { get { return (Support & enuMCIWaveOutSupport.Volume ) == enuMCIWaveOutSupport .Volume ; } }
        public bool SupportLRVolume { get { return (Support & enuMCIWaveOutSupport.LRVolume ) == enuMCIWaveOutSupport.LRVolume ; } }
        public enuMCIWaveInFormat Format { get { return (enuMCIWaveInFormat)m_caps.dwFormats; } }
        public string DisplayName { get { return m_caps.szPname; } }
        public Version Version { get { return new Version(m_caps.vDriverVersion .wHight, m_caps.vDriverVersion.wLow); } }
        public int Channel { get { return this.m_caps.wChannels; } }
        public enuMCIWaveOutSupport Support { get { return (enuMCIWaveOutSupport)this.m_caps.dwSupport; } }
        public uint ID { get { return MCIWaveOutManager.GetID(this); } }
        public int Volume{get{
            float t  = (MCIWaveOutManager.GetVolume(this) / (float)0xFFFFFFFF);
            t *= 100;
            return (int)Math.Ceiling(t);// (int)(MCIWaveOutManager.GetVolume(this) / (float)0xFFFFFFFF) * 100;
        }
            set { 
                if ((value >=0)&&(value <=100))
                {
                    uint t = (uint)(value * 0xFFFF / 100);
                    MCIWaveOutManager.SetVolume(this, t<<16 | t);
                }
            }
        }
        public int Position {
            get {
                return MCIWaveOutManager.GetPosition(this);
            }
        }
        public int Pitch
        {
            get
            {
                if ((this.Support & enuMCIWaveOutSupport.Pitch) == enuMCIWaveOutSupport.Pitch)
                {
                    float t = (MCIWaveOutManager.GetPitch(this) / (float)0xFFFFFFFF);
                    t *= 100;
                    return (int)Math.Ceiling(t);// (int)(MCIWaveOutManager.GetVolume(this) / (float)0xFFFFFFFF) * 100;
                }
                return 0;
            }
            set
            {
                if ((this.Support & enuMCIWaveOutSupport.Pitch) == enuMCIWaveOutSupport.Pitch)
                {
                    if ((value >= 0) && (value <= 100))
                    {
                        uint t = (uint)(value * 0xFFFF / 100);
                        MCIWaveOutManager.SetPitch(this, t << 16 | t);
                    }
                }
            }
        }
        private MCIWaveOutStream()
        { 
        }
        ~MCIWaveOutStream()
        {
            this.Close();
        }
        internal static MCIWaveOutStream CreateFromHandle(IntPtr handle, WAVEFORMATEX format)
        {
            MCIWaveOutStream stream = new MCIWaveOutStream();
            stream.m_handle  = handle;
            stream.m_caps  = MCIWaveOutManager.GetDevCaps(stream );
            stream.m_format = format;
            return stream;
        }
        public void Close()
        {
            if (!this.m_closed)
            {               
                MCIWaveOutApi.waveOutReset(this.Handle);
                MCIWaveOutManager.Close(this);
                this.m_closed = true;
            }
        }
        #region IDisposable Members
        public void Dispose()
        {
            this.Close();
            if (this.m_outBuffer != IntPtr.Zero)
            {
                Marshal.FreeCoTaskMem(this.m_outBuffer);
            }
        }
        #endregion
        /// <summary>
        /// play the item
        /// </summary>
        /// <param name="pcmBlock"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        public void Play(byte[] pcmBlock, int index, int length)
        {
            if ((this.m_IsPlaying) || (pcmBlock  == null))
                return;
            this.m_IsPlaying = true;
            WAVEHDR d = new WAVEHDR();
            IntPtr v_alloc = Marshal.AllocCoTaskMem(length);
            Marshal.Copy(pcmBlock, index , v_alloc, length );
            d.dwBufferLength = length;
            d.lpData = v_alloc;
            if (this.Loop)
            {
                d.dwFlags = enuMCIWaveInFlag.BeginLoop | enuMCIWaveInFlag.EndLoop;
                d.dwLoops = this.LoopTimes;
            }
            int size = Marshal.SizeOf(d);
            IntPtr v_header = Marshal.AllocCoTaskMem(size);
            Marshal.StructureToPtr(d, v_header, true);
            MCIWaveOutManager.Handle(MCIWaveOutApi.waveOutPrepareHeader(this.Handle, v_header, (int)size), "waveOutPrepareHeader");
            MCIWaveOutManager.Handle(MCIWaveOutApi.waveOutWrite(this.Handle, v_header, d.dwBufferLength), "waveOutWrite");
            this.m_outBuffer = v_alloc;
            this.m_preparedHeader = v_header;
        }
        /// <summary>
        /// play wave PCM data
        /// </summary>
        /// <param name="block"></param>
        public void Play(byte[] block)
        {
            Play(block, 0, block.Length);
            //if ((this.m_IsPlaying)|| (block ==null))
            //    return;
            //this.m_IsPlaying = true;
            //WAVEHDR d = new WAVEHDR();
            //IntPtr v_alloc = Marshal .AllocCoTaskMem (block.Length );
            //Marshal.Copy(block, 0, v_alloc, block.Length);
            //d.dwBufferLength = block.Length;
            //d.lpData = v_alloc;
            //int size = Marshal.SizeOf (d);
            //IntPtr v_header = Marshal.AllocCoTaskMem(size);
            //Marshal.StructureToPtr (d, v_header , true );
            //MCIWaveOutManager .Handle( MCIWaveOutApi.waveOutPrepareHeader(this.Handle, v_header, (int)size),"waveOutPrepareHeader");
            //MCIWaveOutManager.Handle(MCIWaveOutApi.waveOutWrite(this.Handle, v_header , d.dwBufferLength),"waveOutWrite");
            //this.m_outBuffer  = v_alloc;
            //this.m_preparedHeader = v_header;
        }
        /// <summary>
        /// stop playing
        /// </summary>
        public void Stop()
        {
            int e = MCIWaveOutApi.waveOutReset(this.m_handle);
            int size = Marshal.SizeOf(typeof(WAVEHDR));
            if (this.m_preparedHeader != IntPtr.Zero)
            {
                MCIWaveOutManager.Handle(MCIWaveOutApi.waveOutUnprepareHeader(this.Handle,
                    this.m_preparedHeader,//.m_outBuffer,
                    size), "waveOutUnprepareHeader");
                Marshal.FreeCoTaskMem(this.m_preparedHeader);
                this.m_preparedHeader = IntPtr.Zero;
            }
            this.m_IsPlaying = false;
        }
        /// <summary>
        /// pause the play back
        /// </summary>
        public void Pause() {
            MCIWaveOutManager.Handle(MCIWaveOutApi.waveOutPause(this.Handle),
                "waveOutPause");
        }
        /// <summary>
        /// restart the playback when paused
        /// </summary>
        public void Restart() {
            MCIWaveOutManager.Handle(MCIWaveOutApi.waveOutRestart(this.Handle), "waveOutRestart");
        }
    }
}

