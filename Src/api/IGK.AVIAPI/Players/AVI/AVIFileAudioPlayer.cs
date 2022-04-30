

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AVIFileAudioPlayer.cs
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
file:AVIFileAudioPlayer.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection;
using System.IO;
namespace IGK.AVIApi.Players
{
    using IGK.ICore;using IGK.AVIApi.AVI;
    using IGK.AVIApi.MCI;
    using IGK.AVIApi.MMIO;
    using IGK.AVIApi.Players.AVI;
    /// <summary>
    /// represent an avi audio player
    /// </summary>
    public class AVIFileAudioPlayer : AVIPlayerBase,
        IAVIPlayer, IDisposable
    {

        private int mmio_Type = AVIFilePlayerConstant.MMIO_AVIFILE;
        private IntPtr m_mmioHandle;
        private AVIFile.AudioStream m_audio;
        private MMIOStream m_memStream;
        private int m_offsetSample;
        private int m_offsetByte;
        private Byte[] m_defaultWAVHeaderInfo;
        //samples
        private long m_position;
        /// <summary>
        /// get the total size 
        /// </summary>
        public int Length { get { return this.m_audio.Length + this.m_defaultWAVHeaderInfo.Length; } }
        /// <summary>
        /// get or set the current position
        /// </summary>
        public long Position { get { return this.m_position; } }
        /// <summary>
        /// get the mmiostream
        /// </summary>
        private MMIOStream MemStream { get { return this.m_memStream; } }
        //long audioLegnth;
        private AVIFileAudioPlayer()
        {
            m_memStream = null;
            //wave header info
            m_defaultWAVHeaderInfo = new byte[]{
               82,73,70,70,36,0,0,0,87,65,86,69,102,109,116,32,16,0,0,0,1,0,2,0,68,172,0,0,16,177,2,0,4,0,16,0,100,97,116,97,4,0,0,0
            };
        }
        /// <summary>
        /// Create an audio file player
        /// </summary>
        /// <param name="name">name used for identify the audio device</param>
        /// <param name="audio">avi stream audio</param>
        /// <returns></returns>
        public static AVIFileAudioPlayer CreateAudioPlayer(string name, AVIFile.AudioStream audio)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }
            if (audio == null)
                throw new ArgumentNullException("audio");
            AVIFileAudioPlayer v = new AVIFileAudioPlayer();
            v.m_audio = audio;
            IntPtr h = MMIOManager.OpenMMIO(new MMIOProcHandler(v.IOProc), enuMMIOMode.Read);
            if (h != IntPtr.Zero)
            {
                v.Name = name;
                v.BaseStream = audio;
                v.m_mmioHandle = h;
                v.UpdateInfo();
                v.Open("waveaudio");
                //v.Open("mpgeg");
                return v;
            }
            return null;
        }
        
        public static AVIFileAudioPlayer CreateAudioPlayer(string name, MMIOStream stream)
        {
            //AVIFileAudioPlayer v = new AVIFileAudioPlayer();
            
            //IntPtr h = MMIOManager.OpenMMIO(new MMIOProcHandler(v.IOMMIOProc), enuMMIOMode.CreateNew);
            //if (h != IntPtr.Zero)
            //{
            //    v.Name = name;
            //    v.m_memStream = stream;
            //    v.m_mmioHandle = h;
            //    v.mmio_Type = MMIO_STREAM;
            //    if (stream.Descend("WAVE", enuMMIOFindMode.FindChunk))
            //    {
            //        stream.Seek(8, enuMMIOSeek.Origin);
            //        v.UpdateInfo();
            //        v.Open("waveaudio");
            //    }
            //    return v;
            //}
            return null;
        }
        public string GetLength()
        {
            return MCIManager.GetString(string.Format("status {0} length", this.Name), 255);
        }
        public string GetPosition()
        {
            return MCIManager.GetString(string.Format("status {0} position", this.Name), 255);
        }
        void UpdateInfo()
        {
            switch (mmio_Type)
            {
                case AVIFilePlayerConstant.MMIO_AVIFILE:
                    using (MemoryStream mem = new MemoryStream())
                    {
                        mem.Write(this.m_defaultWAVHeaderInfo, 0, this.m_defaultWAVHeaderInfo.Length);
                        mem.Flush();
                        long v_length = this.m_audio.Length * this.m_audio.SampleSize;
                        BinaryWriter binW = new BinaryWriter(mem);
                        binW.Seek(0, SeekOrigin.Begin);
                        binW.Write(MMIOApi.FOURCC_WAVE);
                        binW.Seek(4, SeekOrigin.Begin);
                        binW.Write((int)v_length - 8);
                        binW.Seek(40, SeekOrigin.Begin);
                        binW.Write((int)v_length - 40);
                        binW.Flush();
                        binW.Seek(0, SeekOrigin.Begin);
                        Byte[] t = new byte[this.m_defaultWAVHeaderInfo.Length];
                        mem.Read(t, 0, t.Length);
                        this.m_defaultWAVHeaderInfo = t;
                    }
                    break;
                case AVIFilePlayerConstant.MMIO_STREAM:
                    //get wave from memory
                    Byte[] d = m_memStream.Read(44);
                    this.m_defaultWAVHeaderInfo = d;
                    break;
            }
        }
        [StructLayout(LayoutKind.Sequential)]
        internal struct CMMIOChunkINFO
        {
            internal int ckid;
            internal int cksize;
            string CIdName { get { return NativeAPI.mmoiToString(this.ckid); } }
        }
        private int IOProc(ref MMIOApi.MMIOINFO info, enuMMIOMessage msg, IntPtr lParam1, IntPtr lParam2)
        {
            MMIOApi.MMIOINFO v_info = (MMIOApi.MMIOINFO)info;
            System.Diagnostics.Debug.WriteLine("IOProc : " + msg);
            switch (msg)
            {
                case enuMMIOMessage.Open:
                    string s = Marshal.PtrToStringAnsi(lParam1); // get name of device
                    this.m_offsetSample = 0;
                    this.m_offsetByte = 0;
                    this.m_position = 0;
                    System.Diagnostics.Debug.WriteLine("IOProc : Open file [" +s+"]");
                    v_info.lDiskOffset = 0;
                    
                    info = v_info;
                    return 0;
                case enuMMIOMessage.Close:
                    this.m_offsetSample = 0;
                    this.m_offsetByte = 0;
                    this.m_position = 0;
                    break;
                case enuMMIOMessage.Read:
                    int isize = lParam2.ToInt32();
                    byte[] d = null;
                    int rBytes = 0;
                    int rFrames = 0;
                    long v_length = 0;
                    int v_position = 0;





                    //CMMIOChunkINFO v_d = new CMMIOChunkINFO();
                    ////for mp3  reading !!!!!
                    //if (isize == 8)
                    //{              
                    //    //read chunk
                    //    //d = new byte[8];
                    //    Marshal.Copy(this.m_defaultWAVHeaderInfo, 0, lParam1, isize);

                    //    //v_d = (CMMIOChunkINFO)Marshal.PtrToStructure(lParam1, v_d.GetType());
                    //    //    return -1;// isize;
                    //    //correction
                    //    return isize;
                    //}
                    //data already read
                    //get the number of 
                    switch (this.mmio_Type)
                    {
                        case AVIFilePlayerConstant.MMIO_AVIFILE:
                            v_length = this.m_audio.Size;//.Length * this.m_audio.SampleSize;
                            v_position = (int)(v_length - (v_length - (v_info.lDiskOffset)));
                            if (v_position < this.m_defaultWAVHeaderInfo.Length)
                            {
                                //read header info
                                Marshal.Copy(this.m_defaultWAVHeaderInfo, v_position, lParam1, isize);
                                v_position += isize;
                            }
                            else
                            {
                                //
                                int v_adatatoread = v_position - this.m_defaultWAVHeaderInfo.Length + isize;
                                if (v_adatatoread > v_length)
                                {
                                    isize = (int)(v_length - v_adatatoread);
                                }
                                if (isize < 0)
                                    return 0;
                                d = new byte[isize];
                                AVIApi.AVIStreamRead(this.m_audio.Handle,
                                    this.m_offsetSample,
                                    -1,
                                    d,
                                    d.Length,
                                    ref rBytes,
                                    ref rFrames);
                                if (rBytes != isize)
                                {
                                    if (rBytes == 0)
                                    {
                                        return -1;
                                    }
                                }
                                Marshal.Copy(d, 0, lParam1, rBytes);
                                this.m_offsetSample += rFrames;
                                this.m_offsetByte += rBytes;
                                this.m_position += rFrames;
                                isize = rBytes;
                                System.Diagnostics.Debug.WriteLine("Position : " + this.m_position+"/"+ this.m_audio.Length);
                            }
                            break;
                        case AVIFilePlayerConstant. MMIO_STREAM:
                            v_length = this.m_memStream.Length;//.Length * this.m_audio.SampleSize;
                            v_position = (int)(v_length - (v_length - (v_info.lDiskOffset)));
                            if (v_position < this.m_defaultWAVHeaderInfo.Length)
                            {
                                Marshal.Copy(this.m_defaultWAVHeaderInfo, v_position, lParam1, isize);
                            }
                            else
                            {
                            }
                            break;
                    }
                    v_info.lDiskOffset += isize;
                    v_info.pchEndRead = new IntPtr(v_info.lDiskOffset);
                    v_info.pchEndWrite = new IntPtr(v_info.lDiskOffset);
                    info = v_info;
                    return isize;
                case enuMMIOMessage.Seek:
                    int v_pos = lParam1.ToInt32();
                    switch ((enuMMIOSeek)lParam2.ToInt32())
                    {
                        case enuMMIOSeek.Origin:
                            v_info.lDiskOffset = v_pos;
                            break;
                        case enuMMIOSeek.End:
                            v_info.lDiskOffset = this.m_audio.Size - v_pos;
                            break;
                        case enuMMIOSeek.Current:
                            v_info.lDiskOffset += v_pos;
                            break;
                        default:
                            break;
                    }
                    // this.m_stream.Position = v_info.lDiskOffset;
                    v_pos = (int)v_info.lDiskOffset;
                    v_info.pchEndRead = new IntPtr(v_pos);
                    m_offsetSample = (int)((v_pos - m_defaultWAVHeaderInfo.Length) / this.m_audio.SampleSize);
                    m_offsetByte = (int)(m_offsetSample * this.m_audio.SampleSize);
                    this.m_position = m_offsetSample;
                    info = v_info;
                    return (int)v_info.lDiskOffset;
                case enuMMIOMessage.Write:
                    break;
                case enuMMIOMessage.WriteFlush:
                    break;
                default:
                    break;
            }
            return 0;
        }
        //private int IOMMIOProc(ref MMIOApi.MMIOINFO info, enuMMIOMessage msg, IntPtr lParam1, IntPtr lParam2)
        //{
        //    System.Diagnostics.Debug.WriteLine("IOMMIOProc : " + msg);
        //    switch (msg)
        //    {
        //        case enuMMIOMessage.Open:
        //            this.m_position = 0;
        //            break;
        //        case enuMMIOMessage.Close:
        //            this.m_position = 0;
        //            break;
        //        case enuMMIOMessage.Read:
        //            break;
        //        case enuMMIOMessage.Seek:
        //            break;
        //        case enuMMIOMessage.Write:
        //            break;
        //        case enuMMIOMessage.WriteFlush:
        //            break;
        //        default:
        //            break;
        //    }
        //    return 0;
        //}
        public void Open(string type)
        {
           // MMIOProcHandler ProcHandler = null;
            const string TNAME = "WAVE";
            //if (this.m_mmioHandle == IntPtr.Zero)
            //{
                //ProcHandler = new MMIOProcHandler(this.IOProc);
                //bool installed = MMIOManager.InstallIoProc(TNAME, ProcHandler);
            //}
            //else {
            //    bool installed = MMIOManager.InstallIoProc(TNAME, this.m_mmioHandle);
            //}
            
            
            string q = string.Format("open f.{0}+ type {1} alias {2}",
                TNAME,
                type,
                this.Name);
            MCIManager.SendString(q);
        }
        public override void Stop()
        {
            const string TNAME = "WAVE";
            MCIManager.Stop(this.Name, false, false);
            MMIOManager.RemoveIoProc(TNAME);
        }
        public void Seek(int frame)
        {
            MCIManager.SendString(
                string.Format("seek {0} to {1}", this.Name, frame));// .Seek(frame);
        }
        public override void Close()
        {
            this.Stop();
            try
            {
                MMIO.MMIOManager.Close(this.m_mmioHandle, true);
            }
            catch
            {
            }
            finally
            {
                this.m_mmioHandle = IntPtr.Zero;
            }
        }
        public override void Pause()
        {
            MCIManager.Pause(this.Name, false, false);
        }
        void IDisposable.Dispose()
        {
            this.Close();
        }
    }
}

