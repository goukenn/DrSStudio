

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: MCIWaveInStream.cs
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
file:MCIWaveInStream.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
namespace IGK.AVIApi.MCI
{
    using IGK.ICore;using IGK.AVIApi.ACM;
    using IGK.AVIApi.AVI;
    using IGK.AVIApi.Native;
    public delegate void MCIWaveInProcData(byte[] buffer, int length);
    /// <summary>
    /// represent a mci wave in stream. with 2 buffer
    /// </summary>
    public class MCIWaveInStream : IDisposable 
    {
        private IntPtr m_Handle;       
        //buffer
        internal IntPtr m_buffer1;
        internal IntPtr m_buffer2;
        internal IntPtr m_cbuffer;
        private readonly int BUFFSIZE = Marshal.SizeOf(typeof(WAVEHDR));
        public MCIWaveInProcData ProcessData;
        public bool SwapBuffer()
        {
            if (m_cbuffer == m_buffer1)
                m_cbuffer = m_buffer2;
            else
                m_cbuffer = m_buffer1;
            MCIWaveInManager.Handle (MCIWaveInApi.waveInAddBuffer(this.Handle, m_cbuffer, BUFFSIZE), "waveInAddBuffer");
            return true;
        }
        public IntPtr Handle
        {
            get { return m_Handle; }
        }
        private  MCIWaveInStream()
        {
        }
        internal static MCIWaveInStream Create(IntPtr handle)
        {
            MCIWaveInStream stream = new MCIWaveInStream();
            stream.m_Handle = handle;
            return stream;
        }
        public void Start()
        {
            this.m_isplaying = true;
            MCIWaveInManager.Start(this.Handle);
        }
        public void Close()
        {
            if (this.m_isplaying)
                this.Stop();
            MCIWaveInManager.UnPrepareHeader(this.Handle, this.m_buffer1);
            MCIWaveInManager.UnPrepareHeader(this.Handle, this.m_buffer2);
            MCIWaveInManager.Close(this.Handle);                        
            this.m_Handle = IntPtr.Zero;
            this.m_cbuffer = IntPtr.Zero;
            this.m_buffer1 = IntPtr.Zero;
            this.m_buffer2 = IntPtr.Zero;
        }
        public void Stop()
        {
            this.m_isplaying = false;
            MCIWaveInManager.Stop(this.Handle);
        }
        #region IDisposable Members
        public void Dispose()
        {
            if (this.m_Handle != IntPtr.Zero)                
                this.Close();
        }
        #endregion
       // internal void ProcHandle(IntPtr hwi,         uint uMsg,         IntPtr dwInstance,
       //  IntPtr dwParam1,
       //  IntPtr dwParam2
       //)
       // {
       //     Console.WriteLine("Message");
       // }
        /// <summary>
        /// reset the stream and send buffer message
        /// </summary>
        public void Reset()
        {
            MCIWaveInManager.Reset(this.Handle);
        }
        /// <summary>
        /// create a new ave in stream
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="avg"></param>
        /// <param name="bitPSample"></param>
        /// <param name="channel"></param>
        /// <param name="nsamplePerSec"></param>
        /// <returns></returns>
        internal static MCIWaveInStream Create(int deviceId, 
            int nBlockAlign, 
            int bitPSample, 
            int channel, 
            int nsamplePerSec)
        {
            MCIWaveInStream v_out = new MCIWaveInStream();
            IntPtr ht = IntPtr.Zero;
            Delegate callBack = new MCIWaveInCallback(v_out.CallBackProc);
            IntPtr function = (callBack != null) ? Marshal.GetFunctionPointerForDelegate(callBack) : IntPtr.Zero;
            enuMCIWaveOutOpenFlag v_flag = (callBack != null) ? enuMCIWaveOutOpenFlag.Function | enuMCIWaveOutOpenFlag.FormatDirect :
                enuMCIWaveOutOpenFlag.WaveMapped | enuMCIWaveOutOpenFlag.FormatDirect | enuMCIWaveOutOpenFlag.CallBackNull;
            WAVEFORMATEX f = new WAVEFORMATEX();
            f.wFormatTag = AVIApi .WAVE_FORMAT_PCM;
            f.wBitsPerSample = (short)bitPSample;// 16;
            f.cbSize = 0;// (short)Marshal.SizeOf(f);
            f.nChannels = (short)channel;// 2;// 1 for mono - 2 for stereo
            f.nBlockAlign = (short)nBlockAlign;
            f.nSamplesPerSec = nsamplePerSec;// 176400;
            f.nAvgBytesPerSec = nsamplePerSec * nBlockAlign;// avg;// 44100;
            MCIWaveInManager.Handle(MCIWaveInApi.waveInOpen(
                ref ht,
                (uint)(new IntPtr(deviceId).ToInt32()),
                ref f,
                function,
                IntPtr.Zero,
                (int)(v_flag)), "waveInOpen");
            v_out.m_Handle = ht;
            MCIWaveInManager.PrepareHeader(ht, ref v_out.m_buffer1, f.nAvgBytesPerSec);
            MCIWaveInManager.PrepareHeader(ht, ref v_out.m_buffer2, f.nAvgBytesPerSec);
            v_out.SwapBuffer();
            return v_out ;
        }
        static int iii;
        byte[] m_data;
        bool m_isplaying;
        private void CallBackProc( IntPtr hwi,
          uint uMsg,
          IntPtr dwInstance,
          IntPtr dwParam1,
          IntPtr dwParam2
        )
        {
            iii++;
            Console.WriteLine(iii +" >>>Message : " + (enuMCIWaveInMSG) uMsg );
            switch ((enuMCIWaveInMSG)uMsg)
            { 
                case enuMCIWaveInMSG.Open :
                    break;
                case enuMCIWaveInMSG .Data :
                    if (dwParam1 == IntPtr.Zero)
                        return;
                    WAVEHDR d = (WAVEHDR)Marshal.PtrToStructure(dwParam1, typeof(WAVEHDR));
                    if (d.dwBytesRecorded > 0)
                    {
                        if (m_data == null)
                            m_data = new byte[d.dwBufferLength];
                        Console.WriteLine("recorded = " + d.dwBytesRecorded);
                        Byte[] data = m_data;// new byte[d.dwBytesRecorded];
                    //    if (data.Length > 0)
                    //    {
                            Marshal.Copy(d.lpData, data, 0, d.dwBytesRecorded );
                            if (ProcessData != null)
                                ProcessData(data, d.dwBytesRecorded );
                    //    }
                    }
                    if (m_isplaying )
                    this.SwapBuffer();
                    break;
                case enuMCIWaveInMSG.Close :
                    this.m_isplaying = false;
                    break;
            }
        }
    }
}

