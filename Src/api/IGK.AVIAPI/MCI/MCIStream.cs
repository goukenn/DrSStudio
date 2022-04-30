

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: MCIStream.cs
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
file:MCIStream.cs
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
using System.Runtime.InteropServices ;
namespace IGK.AVIApi.MCI
{
    using MMIO;
    /// <summary>    
    /// represent the mci stream manager. How a stream to get data to play
    /// </summary>
    public class MCIStream : IDisposable 
    {
        //handle stream
        private Stream m_stream;
        private IntPtr m_mmioHandle;
        //opened or not
        private bool m_opened;
        private bool m_closed;
        private bool m_disposed=false;
        private string m_regFunctionType;
        private string m_name;
        private MMIOProcHandler m_ProcHandler;
        private string m_DeviceType;
        public string DeviceType
        {
            get { return m_DeviceType; }
        }
        /// <summary>
        /// get the name
        /// </summary>
        public string Name { get { return this.m_name; } }


        /// <summary>
        /// .ctr
        /// </summary>
        /// <param name="stream">input stream. generally memory stream</param>
        /// <param name="name">name that identify the mci stream</param>
        /// <param name="regFunctionType">registreated function type.exemple : "WAVE"  max of 4 length, </param>
        private MCIStream(Stream stream,string name, string regFunctionType)
        {
            if (string.IsNullOrEmpty(regFunctionType) || (regFunctionType.Length != 4))
            {
                throw new ArgumentException($"{nameof(regFunctionType )}");
            }
            this.m_name = name;
            this.m_stream = stream ;
            this.m_regFunctionType = regFunctionType;
        }
        /// <summary>
        /// create new MCI Stream from PCM Data
        /// </summary>
        /// <param name="pcmData"></param>
        /// <param name="name">name that will identify the element</param>
        /// <param name="regFunctionType">the registererd function type name</param>
        /// <returns></returns>
        public static MCIStream CreateFromPCMData(byte[] pcmData, string name, string regFunctionType)
        {
            //create a memory input file
            MCIStream v_s = new MCIStream(new MemoryStream(), name, regFunctionType);
            v_s.m_DeviceType = "waveaudio";
            MMIOProcHandler proc = new MMIOProcHandler(v_s.IOStreamProc );
            MMIOApi.MMIOINFO v_info = new MMIOApi.MMIOINFO();
            v_info.pIOProc = System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(proc);
            IntPtr inf = MMIOApi.mmioOpen(null,
                ref v_info,
                (int)(((int)enuMMIOMode.CreateNew) | (int)(enuMMIOAccess.ReadWrite)));
            if (inf != IntPtr.Zero)
            {  
                v_s.m_mmioHandle = inf ;                
                v_s.AddWaveChunck();
                v_s.AddPCMData(pcmData);                
                v_s.Flush();
                MMIOManager .Close (inf, true);
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
        public void Flush()
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
           MMIOManager.CreateWaveChunck(this.m_mmioHandle ,    4,
     2,
     16,
     44100,
     176400 );            
        }
        public static MCIStream CreateNew(
            string type,
            string name,
            string regFunctionType)
        {
            MCIStream sm = new MCIStream(new MemoryStream(), name, regFunctionType);            
            sm.OpenNew(type);
            return sm;
        }
        /// <summary>
        /// create a mcistream from memoy stream
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="regFunctionType"></param>
        /// <returns></returns>
        public static MCIStream CreateFromStream(
            Stream stream, 
            string type,
            string name,
            string regFunctionType)
        {
            MCIStream sm = new MCIStream(stream, name, regFunctionType);
            sm.Open(type);
            return sm ;
        }
        ////public static MCIStream CreateNewFromStream(
        ////    Stream stream, 
        ////    string type,
        ////    string name,
        ////    string regFunctionType)
        ////{
        ////    MCIStream sm = new MCIStream(stream, name, regFunctionType);
        ////    sm.OpenNew(type);
        ////    return sm;
        ////}
        /// <summary>
        /// Open a new stream file type
        /// </summary>
        /// <param name="type"></param>
        private void OpenNew(string typeName)
        {
           this.m_ProcHandler = new MMIOProcHandler(this.IOStreamProc);
           MMIOManager.InstallIoProc(this.m_regFunctionType , this.m_ProcHandler);
           string v_file = string.Format("stream.{0}+{1}",
               this.m_regFunctionType.Trim(),
               this.m_name);
           MMIOStream mem = MMIOStream .CreateNewFile (v_file, enuMMIOAccess.ReadWrite);
          // good one
          // mem.AddWaveChunck(1, 1, 8, 11025, 11025);
           mem.AddWaveChunck();//4, 2, 16, 44100, 176400);
           mem.AddPCMData(new byte[0]);
           mem.Close();
           MCIManager.SendString(string.Format("open {0} type {1} alias {2} ",
               v_file ,
               typeName,
               this.m_name
               ));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">mci alias name</param>
        /// <param name="type">requested type of the proc</param>
        private void Open(string type)
        {
            this.m_ProcHandler = new MMIOProcHandler (this.IOStreamProc );
            MMIOManager.InstallIoProc(this.m_regFunctionType, this.m_ProcHandler );
            string q = string.Format("open file.{0}+okm type {1} alias {2} ",
                this.m_regFunctionType.Trim(),
                type,
                this.m_name);
            MCIManager.SendString(q);
        }
        public void Open()
        {
            this.Open(this.DeviceType );
        }
        public void Close()
        {
            if (!this.m_closed)
            {
                MCIManager.SendString(string.Format("close {0}", this.m_name));
                MMIOManager.RemoveIoProc(this.m_regFunctionType);
                this.m_closed = true;
            }
        }
        /// <summary>
        /// get the lengthn of the stream
        /// </summary>
        public long Length {
            get { return this.m_stream.Length; }
        }
        /// <summary>
        /// get the stream position
        /// </summary>
        public long Position {
            get {
                return this.m_stream.Position;
            }
            internal set {
                this.m_stream.Position = value;
            }
        }
        #region IDisposable Members
        public void Dispose()
        {
            if (!this.m_disposed && (this.m_stream != null))
            {
                this.Close();
                this.m_stream.Dispose();
                this.m_stream = null;
            }
        }
        #endregion
        private int IOStreamProc(
            ref MMIOApi.MMIOINFO lpmmioinfo,
            enuMMIOMessage msg,
            IntPtr lParam1,
            IntPtr lParam2)
        {
            MMIOApi.MMIOINFO v_info = (MMIOApi.MMIOINFO)lpmmioinfo;
            enuMMIOOpenFlag op = (enuMMIOOpenFlag)v_info.dwFlags;
            System.Diagnostics.Debug.WriteLine(" msg : "+ msg);          
            switch (msg)
            {
                case enuMMIOMessage.Open:
                    if (this.m_opened )
                        return 0;
                    string fname =lParam1 == IntPtr.Zero ?
                        string.Empty:
                     Marshal.PtrToStringAnsi(lParam1);
                    string[] f = fname!=null ?fname.Split ('.'): null;
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
                    int v_position =(int) (this.Length  - (this.Length - (v_info.lDiskOffset)));
                    //
                    int v_adatatoread = v_position + isize;
                    if (v_adatatoread > this.Length )
                    {
                        isize = (int)(this.Length - v_adatatoread);
                    }
                    if (isize < 0)
                        return 0;
                    byte[] d = new byte[isize];
                    m_stream .Read (d, 0, isize );
                    Marshal.Copy(d, 0, lParam1, isize);
                    v_info.lDiskOffset += isize;
                    v_info.pchEndRead = new IntPtr(v_info.lDiskOffset);
                    v_info.pchEndWrite = new IntPtr(v_info.lDiskOffset);
                    lpmmioinfo = v_info;
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
                    this.m_stream .Position = v_info.lDiskOffset ;
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
                    //Byte[] v_tab = new Byte[lParam2.ToInt32()];
                    //Marshal.Copy(lParam1, v_tab, 0, v_tab.Length);                    
                    //this.m_stream .Write(v_tab, 0, v_tab.Length);
                    ////update write
                    //v_info.lDiskOffset = this.m_stream.Position;
                    //v_info.pchEndRead = new IntPtr(v_info.lDiskOffset);
                    //v_info.pchEndWrite = v_info.pchEndRead;
                    //lpmmioinfo = v_info;
                    //return v_tab.Length;                    
                case enuMMIOMessage.WriteFlush:
                    this.m_stream.Flush();
                    break;                          
                default:
                    break;
            }
            return -1;
        }
        public void SaveTo(string file)
        {
            this.m_stream.Seek(0, SeekOrigin.Begin);
            byte[] t =new byte[this.m_stream.Length ];
            this.m_stream.Read(t, 0, t.Length);
            File.WriteAllBytes(file, t);
        }
        public void Record(bool overwrite)
        {
            MCIManager.Record(this.m_name, overwrite , false, false);
        }
        //public void Capture()
        //{
        //    MCIManager.Capture(this.m_name, false, false);
        //}
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
            MCIManager.Stop(this.m_name ,false, false);
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

