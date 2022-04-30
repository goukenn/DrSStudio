

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: MMIOStream.cs
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
file:MMIOStream.cs
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
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace IGK.AVIApi.MMIO
{
    /// <summary>
    /// represent the MMIO Stream
    /// </summary>
    public class MMIOStream : IDisposable 
    {
        private IntPtr m_mmIOHandle;
        MMIOApi.MMIOINFO m_info;
        private MMIOApi.MMIOChunkINFO m_topChunk;
        private MMIOApi.MMIOChunkINFO  m_currentChunk;
        private bool m_Closed;
        private string m_filename;
        private Stream m_stream;
        private enuMMIOStreamType m_streamType;
        private bool m_opened=false;
        /// <summary>
        /// get the stream type of the mmio stream
        /// </summary>
        public enuMMIOStreamType StreamType {get{return m_streamType;}}
        public string FileName { get { return this.m_filename; } }
        public IntPtr Handle { get { return this.m_mmIOHandle; } }
        public long Length { get {
            if (this.StreamType == enuMMIOStreamType.MemoryStream && (this.m_stream != null))
            {
                return this.m_stream.Length;
            }
            if (System.IO.File.Exists (this.FileName ))
            {
                return new System.IO.FileInfo(this.FileName ).Length ;
            }
            return m_info .pchEndWrite .ToInt64 ();
        }
        }
        public bool CanRead {
            get {
                return (((enuMMIOOpenFlag)this.m_info.dwFlags) & enuMMIOOpenFlag.Read) == enuMMIOOpenFlag.Read;                
            }
        }
        public bool CanWrite {
            get {
                return 
                    ((this.OpenFlag & enuMMIOOpenFlag.ReadWrite )== enuMMIOOpenFlag.ReadWrite) ||
                    (this.OpenFlag & enuMMIOOpenFlag.Write) == enuMMIOOpenFlag.Write;
            }
        }
        public enuMMIOOpenFlag OpenFlag {
            get {
                return(enuMMIOOpenFlag) this.m_info.dwFlags;
            }
        }
        public long BuffOffset {
            get {
                return this.m_info.lBufOffset;
            }
        }
        internal static MMIOStream Create(IntPtr handle,string filename)
        {
            if (handle == IntPtr.Zero)
                return null;
            MMIOStream v = new MMIOStream();
            v.m_mmIOHandle = handle;
            v.m_info = MMIOManager.GetInfo(handle);
            v.m_filename = filename;
            v.m_streamType = enuMMIOStreamType.File;
            return v;
        }
        internal static MMIOStream Create(IntPtr handle, enuMMIOStreamType streamType)
        {
            if (handle == IntPtr.Zero)
                return null;
            MMIOStream v = new MMIOStream();
            v.m_mmIOHandle = handle;
            v.m_info = MMIOManager.GetInfo(handle);
            v.m_filename = null;
            v.m_streamType = streamType;
            return v;
        }
        /// <summary>
        /// .ctr private
        /// </summary>
        private MMIOStream()
        { 
        }
        /// <summary>
        /// create a new MMIOStream from memory stream
        /// </summary>
        /// <returns></returns>
        public static MMIOStream Create()
        {
            MMIOStream v_s = new MMIOStream();
            MMIOProcHandler proc = new MMIOProcHandler(v_s.IOStreamProc);
            MMIOApi.MMIOINFO v_info = new MMIOApi.MMIOINFO();            
            v_info.pIOProc = System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(proc );          
            IntPtr inf = MMIOApi.mmioOpen(null, 
                ref v_info,
                (int)(((int)enuMMIOMode.CreateNew) | (int)(enuMMIOAccess.ReadWrite  )));            
            if (inf != IntPtr.Zero)
            {
                v_s.m_streamType = enuMMIOStreamType.MemoryStream;
                v_s.m_mmIOHandle = inf;
                v_s.m_info = MMIOManager.GetInfo(inf);                
                return v_s;
            }
            return null;
        }
        /// <summary>
        /// prepresent the base io proc message handler. to write data
        /// </summary>
        /// <param name="lpmmioinfo"></param>
        /// <param name="msg"></param>
        /// <param name="lParam1"></param>
        /// <param name="lParam2"></param>
        /// <returns></returns>
        internal int IOStreamProc(
               ref  MMIOApi.MMIOINFO lpmmioinfo,
             enuMMIOMessage msg,
             IntPtr lParam1,
             IntPtr lParam2)
        {
            MMIOApi.MMIOINFO v_info = lpmmioinfo;
            switch (msg)
            {
                case enuMMIOMessage.Open:
                    if (this.m_opened)
                        return 0;
                    this.m_stream = new MemoryStream();
                    return 0;
                case enuMMIOMessage.Close:
                    if (this.m_stream != null)
                    {
                        this.m_stream.Flush();
                        this.m_stream.Dispose();
                        this.m_stream = null;
                    }
                    return 0;
                case enuMMIOMessage.Read:
                    break;
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
                    v_info.pchEndWrite = new IntPtr (this.m_stream.Length);
                    lpmmioinfo = v_info;
                    return (int)v_info.lDiskOffset;                    
                case enuMMIOMessage.Write:
                    //write data to memory stream
                    byte[] data = new byte[lParam2.ToInt32()];
                    Marshal.Copy(lParam1, data, 0, data.Length);
                    m_stream.Write(data, 0, data.Length);
                    v_info.lDiskOffset += data.Length;
                    v_info.pchEndRead = new IntPtr(v_info.lDiskOffset);
                    v_info.pchEndWrite= new IntPtr(v_info.lDiskOffset);
                    lpmmioinfo = v_info;
                    return data.Length;                    
                case enuMMIOMessage.WriteFlush:
                    m_stream.Flush();
                    return 0;
                default:
                    break;
            }
            return 0;
        }
#region IDisposable Members
        public void  Dispose()
        {
            if (!this.m_Closed)
            {
                try
                {
                    //update the top handle
                    if (this.CanWrite)
                    {
                        if (!m_currentChunk.IsEmpty)
                            MMIOManager.Ascend(this.Handle, ref m_currentChunk);
                        if (!m_topChunk.IsEmpty)
                            MMIOManager.Ascend(this.Handle, ref m_topChunk);
                    }
                }
                catch
                {
                }
                finally {
                    MMIOManager.Close(this.m_mmIOHandle, true);
                    this.m_Closed = true;
                    if (this.m_stream != null)
                    {
                        this.m_stream.Dispose();
                    }
                    this.m_Closed = true;
                }
            }
        }
#endregion
        public bool AddRiffChunck()
        {
            MMIOManager.AddChunk (this, MMIOApi .FOURCC_RIFF, enuMMIOCreateChunkParam.Riff);
            GetInfo();
            return true;
        }
        /// <summary>
        /// add wave chunk
        /// </summary>
        /// <param name="blockCount"></param>
        /// <param name="channel"></param>
        /// <param name="bits"></param>
        /// <param name="frequency"></param>
        /// <param name="average"></param>
        /// <returns></returns>
        public bool AddWaveChunck(
            int blockCount,//4
            int channel ,//2
            int bits,//16
            int frequency , //44100
            int average  //176400
        )
        {
            try
            {
                MMIOManager.CreateWaveChunck(this,
                    blockCount ,
                    channel,
                    bits,
                    frequency,
                    average );
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
        /// <summary>
        /// add wave with default chunk
        /// </summary>
        /// <returns></returns>
        public bool AddWaveChunck()
        {
           return this.AddWaveChunck (
                 4,
            2,
            16,
            44100,
            176400);
        }
        public bool ReadWaveChunck()
        {
            try
            {
               return  MMIOManager.ReadWaveChunck(this.m_mmIOHandle);
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// close the stream
        /// </summary>
        public void Close()
        {
            this.Dispose();
        }
        /// <summary>
        /// Add PCM Data From File
        /// </summary>
        /// <param name="pcmFile">pcm saveing file</param>
        public void AddPCMDataFromFile(string pcmFile)
        {
            byte[] t = System.IO.File.ReadAllBytes(pcmFile);
            MMIOManager.AddData(this, t);
        }
        public long Position {
            get {
                return MMIOManager.GetInfo(this.Handle).pchEndRead.ToInt64 () ;
            }
        }
        /// <summary>
        /// add pcm data file
        /// </summary>
        /// <param name="t"></param>
        public void AddPCMData(byte[] t)
        {
            MMIOManager.AddData(this, t);
            GetInfo();
        }
        private void GetInfo()
        {
            m_info = MMIOManager.GetInfo(this.Handle);
        }
        /// <summary>
        /// flush data and update the chunck info
        /// </summary>
        public void Flush()
        {
            MMIOManager.Flush(this.Handle);
            this.GetInfo();
        }
        internal void SetTopChunck(MMIOApi.MMIOChunkINFO p)
        {
            this.m_topChunk = p;
        }
        internal void SetCurrentChunck(MMIOApi.MMIOChunkINFO p)
        {
            this.m_currentChunk  = p;
            //update top chunck
            MMIOManager.Ascend(this.Handle, ref p);// this.m_topChunk);
        }
        public void SaveTo(string p)
        {
            byte[] t = null;
            if (this.StreamType == enuMMIOStreamType.MemoryStream)
            {
                BinaryReader binR = new BinaryReader(this.m_stream);
                t = new byte[m_stream.Length];
                binR.BaseStream.Seek(0, SeekOrigin.Begin);
                binR.Read(t, 0, t.Length);
                File.WriteAllBytes(p, t );
                return;
            }
            MMIOManager.Seek(this, 0, enuMMIOSeek.Origin);
            t = new byte[this.Length];
            MMIOManager.Read(this.Handle, t, t.Length);
            File.WriteAllBytes(p,t);
        }
        public int Seek(int position, enuMMIOSeek seek)
        {            
                long i =  MMIOApi.mmioSeek(this.Handle.ToInt32() , position, (int)seek );
               if (i == -1)
                   throw new Exception("mmioSeekError");
               this.GetInfo();
               return (int)i;
        }
        /// <summary>
        /// create a mmio stream from file
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public static MMIOStream CreateFromFile(string filename,enuMMIOAccess desiredAccess )
        {
            MMIOApi.MMIOINFO v_info = new MMIOApi.MMIOINFO();
            IntPtr inf = MMIOApi.mmioOpen(filename, ref v_info,
                ((int)desiredAccess));
            MMIOStream v_s = null;
            if (inf != IntPtr.Zero)
            {
                v_s = MMIOStream.Create(inf, filename);
            }
            return v_s;
        }
        public static MMIOStream CreateNewFile(string filename, enuMMIOAccess desiredAccess)
        {
            MMIOApi.MMIOINFO v_info = new MMIOApi.MMIOINFO();
            IntPtr inf = MMIOApi.mmioOpen(filename, ref v_info,
                (int)(((int)enuMMIOMode.CreateNew) | (int)desiredAccess));
            MMIOStream v_s = null;
            if (inf != IntPtr.Zero)
            {
                v_s = MMIOStream.Create(inf, filename);
            }
            return v_s;
        }
        public static MMIOStream OpenFile(string filename, enuMMIOAccess access)
        {
            MMIOApi.MMIOINFO v_info = new MMIOApi.MMIOINFO();
            IntPtr inf = MMIOApi.mmioOpen(filename, ref v_info,
                (int)access);
            MMIOStream v_s = null;
            if (inf != IntPtr.Zero)
            {
                v_s = MMIOStream.Create(inf, filename);
            }
            return v_s;
        }
        /// <summary>
        /// create a new mmio stream from PCM Data.
        /// </summary>
        /// <param name="sm_Data">PCM Data read from file or get width MMIOManager.GetPCMData method</param>
        /// <returns></returns>
        public static MMIOStream CreateFromPCM(byte[] sm_Data)
        {
            MMIOStream v_sm = Create();
            //v_sm.AddWaveChunck();
            MMIOManager.CreateWaveChunck(
                v_sm.Handle,
                4,
                2,
                16,
                44100,
                176400);
            v_sm.AddPCMData(sm_Data);
            return v_sm;
        }
        public void Ascend()
        {
            MMIOApi.MMIOChunkINFO info = this.m_currentChunk;
            MMIOManager.Ascend(this.Handle, ref info);
            this.m_topChunk = info;
            this.m_currentChunk = info;
        }
        /// <summary>
        /// get current chunck size
        /// </summary>
        public int ChunckSize {
            get {
                return this.m_currentChunk.cksize;
            }
        }
        /// <summary>
        /// get current chunkc offset
        /// </summary>
        public int ChunckOffset
        {
            get
            {
                return this.m_currentChunk.dwDataOffset;
            }
        }
        /// <summary>
        /// get current chunck display name
        /// </summary>
        public string ChunckName {
            get {
                return this.m_currentChunk.FCCTypeName;
            }
        }
        /// <summary>
        /// get current chunk display id
        /// </summary>
        public string ChunckIdName {
            get
            {
                return this.m_currentChunk.CidName;
            }
        }
        /// <summary>
        /// get current chunck id
        /// </summary>
        public int ChunckId
        {
            get
            {
                return this.m_currentChunk .ckid ;
            }
        }
        public bool Descend(string fourName, enuMMIOFindMode mode)
        {
            try
            {
                MMIOApi.MMIOChunkINFO info = new MMIOApi.MMIOChunkINFO();
                if (!string.IsNullOrEmpty(fourName))
                {
                    switch (mode)
                    {
                        case enuMMIOFindMode.FindList:
                        case enuMMIOFindMode .FindRiff :
                            info.fccType = MMIOApi.getChunck(fourName.ToCharArray());
                            break;
                        case enuMMIOFindMode.FindChunk:
                            info.ckid = MMIOApi.getChunck(fourName.ToCharArray());
                            break;
                    }
                }
                    enuMMIOError e = (enuMMIOError)MMIO.MMIOApi.mmioDescend(this.Handle,
                out info,
                IntPtr.Zero ,//ref m_currentChunk,
                (uint)mode
               );
                    if (e == enuMMIOError.NoError)
                    {
                        //MMIOManager.Descend(this.Handle, out info, mode);
                        this.m_topChunk = info;
                        this.m_currentChunk = info;
                    }
                return (e == enuMMIOError.NoError );
            }
            catch
            {
                return false;
            }
        }
        public bool DescendCurrent(enuMMIOFindMode mode, string fourName)
        {
            try
            {
                MMIOApi.MMIOChunkINFO info = new MMIOApi.MMIOChunkINFO();
                if (!string.IsNullOrEmpty (fourName ))
                    info.ckid = MMIOApi.getChunck(fourName.ToCharArray());
                enuMMIOError e = (enuMMIOError)MMIO.MMIOApi.mmioDescend(this.Handle,
                ref info,
                ref m_currentChunk,
                (uint)mode
               );
                if (e == enuMMIOError.NoError)
                {
                    if (info.cksize < m_currentChunk.cksize)
                    {
                        m_topChunk = m_currentChunk;
                        m_currentChunk = info;
                        return true;
                    }
                    else {
                       int i =  MMIO.MMIOApi.mmioAscend(this.Handle.ToInt32(), ref m_currentChunk, 0);
                    }
                }
                //MMIOManager.Descend(this.Handle, out info, out m_currentChunk, mode);               
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return false;
        }
        public bool Descend(enuMMIOFindMode mode)
        {
            return Descend(null, mode);
        }
        /// <summary>
        /// advance the info
        /// </summary>
        public bool Advance()
        {
            try{
            MMIOManager.Advance(this.Handle, ref this.m_info);
            return (m_info.pchNext!= m_info.pchEndRead);
            }
            catch{
                return false ;
            }
        }
        /// <summary>
        /// read one bite
        /// </summary>
        public void Read()
        {
            byte[] t = new byte[1];
            int r = MMIOManager.Read(this.Handle, new byte[0], 1);
        }
        /// <summary>
        /// read data
        /// </summary>
        /// <param name="length">size of data to read</param>
        /// <returns></returns>
        public byte[] Read(int length)
        {
            byte[] t = new byte[length];
            int r = MMIOManager.Read(this.Handle,t, t.Length);
            return t;
        }
        /// <summary>
        /// read read 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        internal int Read(byte[] d, int length)
        {
            int r = MMIOManager.Read(this.Handle, d, length);
            return r;
        }
        internal void Write(byte[] d,  int length)
        {
            MMIOManager.Write(this.Handle, d, length );
        }
        /// <summary>
        /// read the chunk at current location
        /// </summary>
        /// <returns></returns>
        public PMMCHKInfo ReadChunck()
        {
            PMMCHKInfo v_info = new PMMCHKInfo();
            byte[] t = Read(Marshal.SizeOf(v_info.m_info ));
            if (t != null)
            {
                IntPtr v_alloc = Marshal.AllocCoTaskMem(t.Length);
                Marshal.Copy(t, 0, v_alloc, t.Length);
                v_info.m_info  = (MMIOApi.MMIOChunkINFO )Marshal.PtrToStructure(v_alloc, v_info.m_info.GetType());
                Marshal.FreeCoTaskMem(v_alloc);
            }
            return v_info;
        }
    }
}

