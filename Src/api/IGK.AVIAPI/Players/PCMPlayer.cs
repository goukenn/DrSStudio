

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PCMPlayer.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore;using IGK.AVIApi.MCI;
using IGK.AVIApi.MMIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IGK.AVIApi.Players
{
    /*
     * 
     * 
     * play pcm data. in synchronous wait.
     * 
     * 
     * */
    public class PCMPlayer
    {
        private Stream m_stream;//stream to read
        private string m_DeviceType;
        private IntPtr m_mmioHandle;
        private bool m_opened;
        private string m_name;
        private MMIOProcHandler m_ProcHandler;
        private string m_regFunctionType;
        /// <summary>
        /// get the lengthn of the stream
        /// </summary>
        public long Length
        {
            get { return this.m_stream.Length; }
        }
        /// <summary>
        /// get the stream position
        /// </summary>
        public long Position
        {
            get
            {
                return this.m_stream.Position;
            }
            internal set
            {
                this.m_stream.Position = value;
            }
        }
        public string DeviceType { get { return this.m_DeviceType; } }
        private PCMPlayer()
        {

        }

        /// <summary>
        /// create a player from wave file
        /// </summary>
        /// <param name="wavefilename"></param>
        /// <param name="id"></param>
        /// <param name="regName"></param>
        /// <returns></returns>
        public static PCMPlayer CreatePlayer(string wavefilename, string id, string regName)
        {
            return null;
        }
        /// <summary>
        /// create a player
        /// </summary>
        /// <param name="pcmData">pcm data</param>
        /// <param name="id"></param>
        /// <param name="regName">exeample WAVE</param>
        /// <returns></returns>
        public static PCMPlayer CreatePlayer(byte[] pcmData, string id, string regName)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("id");
            }
            if ((string.IsNullOrEmpty(regName) || regName.Length != 4))
                throw new ArgumentException("regName");


            //create a memory input file
            PCMPlayer v_s = new PCMPlayer();
            v_s.m_stream = new MemoryStream();
            v_s.m_name = id;
            v_s.m_regFunctionType = regName;
            v_s.m_DeviceType = "pcmdata";
            MMIOProcHandler proc = new MMIOProcHandler(v_s.IOStreamProc);
            MMIOApi.MMIOINFO v_info = new MMIOApi.MMIOINFO();
            v_info.pIOProc = System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(proc);

            //register and opent stream
            IntPtr v_inf = MMIOApi.mmioOpen(null,
                ref v_info,
                (int)(((int)enuMMIOMode.CreateNew) | (int)(enuMMIOAccess.ReadWrite)));
            if (v_inf != IntPtr.Zero)
            {
                v_s.m_mmioHandle = v_inf;
                v_s.AddWaveChunck();
                v_s.AddPCMData(pcmData);
                v_s.Flush();
                MMIOManager.Close(v_inf, true);
            }
            else
                return null;
            v_s.Position = 0;
            v_s.Open("waveaudio");
            return v_s;
            
        }
        /// <summary>
        /// flush the content
        /// </summary>
        private void Flush()
        {
            if (this.m_mmioHandle != IntPtr.Zero)
            {
                MMIOApi.MMIOChunkINFO t = new MMIOApi.MMIOChunkINFO();
                MMIOManager.Ascend(this.m_mmioHandle, ref t);
            }
        }
        /// <summary>
        /// add a wave chunk to this mci
        /// </summary>
        private void AddWaveChunck()
        {
            MMIOManager.CreateWaveChunck(this.m_mmioHandle, 4,2,16,  44100,      176400);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">mci alias name</param>
        /// <param name="type">requested type of the proc</param>
        private void Open(string type)
        {
            this.m_ProcHandler = new MMIOProcHandler(this.IOStreamProc);
            MMIOManager.InstallIoProc(this.m_regFunctionType, this.m_ProcHandler);
            string q = string.Format("open file.{0}+okm type {1} alias {2} ",
                this.m_regFunctionType.Trim(),
                type,
                this.m_name);
            MCIManager.SendString(q);
        }
        /// <summary>
        /// open the current device
        /// </summary>
        public void Open()
        {
            this.Open(this.DeviceType);
        }
        /// <summary>
        /// io proc to handle operation
        /// </summary>
        /// <param name="lpmmioinfo"></param>
        /// <param name="uMsg"></param>
        /// <param name="lParam1"></param>
        /// <param name="lParam2"></param>
        /// <returns></returns>
        private int IOStreamProc(ref MMIOApi.MMIOINFO lpmmioinfo, enuMMIOMessage msg, IntPtr lParam1, IntPtr lParam2)
        {
            MMIOApi.MMIOINFO v_info = (MMIOApi.MMIOINFO)lpmmioinfo;
            enuMMIOOpenFlag op = (enuMMIOOpenFlag)v_info.dwFlags;
#if DEBUG
            System.Diagnostics.Debug.WriteLine(" msg : " + msg);
#endif
            switch (msg)
            {
                case enuMMIOMessage.Open:
                    if (this.m_opened)
                        return 0;
                    string fname = lParam1 == IntPtr.Zero ?
                        string.Empty :
                     Marshal.PtrToStringAnsi(lParam1);
                    string[] f = fname != null ? fname.Split('.') : null;
                    if (this.m_mmioHandle.ToInt32() != v_info.adwInfo[1])
                        this.m_mmioHandle = new IntPtr(v_info.adwInfo[1]);
                    v_info.lDiskOffset = 0;
                    lpmmioinfo = v_info;
                    this.m_opened = true;
                    if (op == enuMMIOOpenFlag.Parse)
                    {
                        return 0;
                    }
                    return 0;
                case enuMMIOMessage.Close:
                    this.m_stream.Flush();
                    this.m_stream.Seek(0, SeekOrigin.Begin);
                    v_info.pchEndRead = IntPtr.Zero;
                    v_info.pchEndWrite = IntPtr.Zero;
                    lpmmioinfo = v_info;
                    this.m_opened = false;
                    this.m_mmioHandle = IntPtr.Zero;
                    return 0;
                case enuMMIOMessage.Read:
                    ///MMIOApi.MMIOINFO v_cinfo = MMIOManager.GetInfo(v_info.hmmio);
                    int isize = lParam2.ToInt32();
                    //data already read
                    int v_position = (int)(this.Length - (this.Length - (v_info.lDiskOffset)));
                    //
                    int v_adatatoread = v_position + isize;
                    if (v_adatatoread > this.Length)
                    {
                        isize = (int)(this.Length - v_adatatoread);
                    }
                    if (isize < 0)
                        return 0;
                    byte[] d = new byte[isize];
                    m_stream.Read(d, 0, isize);
                    Marshal.Copy(d, 0, lParam1, isize);
                    v_info.lDiskOffset += isize;
                    v_info.pchEndRead = new IntPtr(v_info.lDiskOffset);
                    v_info.pchEndWrite = new IntPtr(v_info.lDiskOffset);
                    lpmmioinfo = v_info;
#if DEBUG
                    System.Diagnostics.Debug.WriteLine(this.Position +"/"+this.Length );
#endif
                    return isize;
                case enuMMIOMessage.Seek:
                    switch ((enuMMIOSeek)lParam2.ToInt32())
                    {
                        case enuMMIOSeek.Origin:
                            v_info.lDiskOffset = lParam1.ToInt32();
                            break;
                        case enuMMIOSeek.End:
                            v_info.lDiskOffset = this.Length - lParam1.ToInt32();
                            break;
                        case enuMMIOSeek.Current:
                            v_info.lDiskOffset += lParam1.ToInt32();
                            break;
                        default:
                            break;
                    }
                    this.m_stream.Position = v_info.lDiskOffset;
                    v_info.pchEndRead = new IntPtr(v_info.lDiskOffset);
                    lpmmioinfo = v_info;
                    return (int)v_info.lDiskOffset;
                case enuMMIOMessage.Write:
                    byte[] data = new byte[lParam2.ToInt32()];
                    Marshal.Copy(lParam1, data, 0, data.Length);
                    m_stream.Write(data, 0, data.Length);
                    v_info.lDiskOffset += data.Length;
                    v_info.pchEndRead = new IntPtr(v_info.lDiskOffset);
                    v_info.pchEndWrite = new IntPtr(v_info.lDiskOffset);
                    lpmmioinfo = v_info;
                    return data.Length;                   
                case enuMMIOMessage.WriteFlush:
                    this.m_stream.Flush();
                    break;
                default:
                    break;
            }
            return -1;
        }

        public void Pause()
        {
            MCIManager.Pause(this.m_name, false, false);
        }
        public void Play()
        {
            MCIManager.Play(this.m_name);
        }
        public void Stop()
        {
            MCIManager.Stop(this.m_name, false, false);
        }
        public void AddPCMData(byte[] pcmData)
        {
            if (pcmData == null)
                return;
            MMIOApi.MMIOChunkINFO chunck = new MMIOApi.MMIOChunkINFO();
            MMIOManager.AddData(this.m_mmioHandle, pcmData, ref chunck);
            MMIOManager.Ascend(this.m_mmioHandle, ref chunck);
            //MMIOApi.MMIOINFO v_info = MMIOManager.GetInfo(this.m_mmioHandle);
            //int i =    MMIOManager.Write(this.m_mmioHandle, pcmData, pcmData.Length);
            //update size
            BinaryWriter binW = new BinaryWriter(this.m_stream);
            binW.Seek(4, SeekOrigin.Begin);
            binW.Write((int)this.Length - 8);
            binW.Seek(40, SeekOrigin.Begin);
            binW.Write((int)this.Length - 40);
            binW.Flush();
            binW.Seek(0, SeekOrigin.End);
        }
    }
}
