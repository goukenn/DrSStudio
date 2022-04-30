

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: MMIOManager.cs
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
file:MMIOManager.cs
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
using System.IO;
namespace IGK.AVIApi.MMIO
{
    /// <summary>
    /// Represent the MMIOManager 
    /// </summary>
    public static class MMIOManager
    {       
        /// <summary>
        /// create a memory stream
        /// </summary>
        /// <param name="procId"></param>
        /// <param name="access"></param>
        /// <returns></returns>
        public static MMIOStream CreateMemory(string procId, enuMMIOAccess access)
        {
            if ((string.IsNullOrEmpty (procId ) || procId.Length !=4))
                return null;
            MMIOApi.MMIOINFO v_info = new MMIOApi.MMIOINFO();
            v_info.fccIOProc = MMIOApi.getChunck(procId.ToCharArray());
            v_info.pIOProc = IntPtr.Zero;
            IntPtr inf = MMIOApi.mmioOpen(null, ref v_info,
                (int)(((int)enuMMIOMode.CreateNew) | (int)access));
            //reserver 4096 byte
            Byte[] t = new byte[4096];
            int i = MMIOManager.Write(inf, t, 4096);
            MMIOStream v_s = null;
            if (inf != IntPtr.Zero)
            {
                v_s = MMIOStream.Create(inf, null);
            }
            return v_s;
        }

        /// <summary>
        /// create mmio in memory
        /// </summary>
        /// <param name="d"></param>
        /// <param name="access"></param>
        /// <returns></returns>
        public static MMIOStream CreateMemory(MulticastDelegate d, enuMMIOAccess access)
        {
            MMIOApi.MMIOINFO v_info = new MMIOApi.MMIOINFO();
            if (d != null)
                v_info.pIOProc = Marshal.GetFunctionPointerForDelegate(d);
            else
                v_info.fccIOProc = MMIOApi.getChunck("MEM ".ToCharArray());
            IntPtr inf = MMIOApi.mmioOpen(null, ref v_info,
                (int)(((int)enuMMIOMode.CreateNew) | (int)access));
            MMIOStream v_s = null;
            if (inf != IntPtr.Zero)
            {
                v_s = MMIOStream.Create(inf, enuMMIOStreamType.CustomIoProc);
            }
            return v_s;
        }

        /// <summary>
        /// create a mi install andler
        /// </summary>
        /// <param name="proc"></param>
        /// <param name="additional"></param>
        /// <returns></returns>
        internal static IntPtr OpenMMIO(MMIOProcHandler proc, enuMMIOMode additional)
        {                 
            MMIOApi.MMIOINFO v_info = new MMIOApi.MMIOINFO();
            v_info.pIOProc = System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(proc);
            IntPtr inf = MMIOApi.mmioOpen(null,
                ref v_info,
                (int)((int)( (int)enuMMIOMode.CreateNew | (int)enuMMIOAccess.ReadWrite|  (int)additional )));
            return inf;
        }

        
        /// <summary>
        /// open mmio file 
        /// </summary>
        /// <param name="filename">file name according to spécification</param>
        /// <param name="access">desired access</param>
        /// <returns></returns>
        public static MMIOStream OpenFile(string filename, enuMMIOAccess access)
        {
            return MMIOStream.OpenFile(filename, access);
        }
        internal static void Close(IntPtr intPtr, bool closeFileHandle)
        {
            //try
            //{
            MMIOApi.MMIOINFO t = GetInfo(intPtr);
            if( (((enuMMIOOpenFlag)t.dwFlags) & enuMMIOOpenFlag.Write) == enuMMIOOpenFlag.Write)
            {
                Flush(intPtr);              
            }
            Handle((enuMMIOError)MMIOApi.mmioClose(intPtr,
                (uint)(closeFileHandle ? 0: MMIOApi .MMIO_FHOPEN)),
                    "mmioClose");
        }
        /// <summary>
        /// internal handle errors
        /// </summary>
        /// <param name="enuMMIOError"></param>
        /// <param name="p"></param>
        internal static void Handle(enuMMIOError enuMMIOError, string p)
        {
            if (enuMMIOError == enuMMIOError.NoError)
                return;
            throw new Exception (string.Format ("{0} : {1}", enuMMIOError , p));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="intPtr"></param>
        /// <param name="blockCount"></param>
        /// <param name="channel"></param>
        /// <param name="bits"></param>
        /// <param name="frequency"></param>
        /// <param name="average"></param>
        /// <returns>return the current chunck</returns>
        internal static MMIOApi.MMIOChunkINFO CreateWaveChunck(
            IntPtr intPtr,
            int blockCount,//4
            int channel,//2
            int bits,//16
            int frequency, //44100
            int average  //176400
            )
        {
            MMIOApi.MMIOChunkINFO chunck = new MMIOApi.MMIOChunkINFO();
            MMIOApi.MMIOChunkINFO p = new MMIOApi.MMIOChunkINFO();
            //create a wave chunk
            chunck.ckid = 0;// MMIOApi.FOURCC_RIFF;
            chunck.fccType = MMIOApi.FOURCC_WAVE;
            CreateChunck(intPtr, ref chunck, enuMMIOCreateChunkParam.Riff);
            Ascend(intPtr, ref  chunck);
            //save parent chunck
            p = chunck;
            // create fmt chuck sub id
            chunck = new MMIOApi.MMIOChunkINFO();
            chunck.ckid = MMIOApi.FOURCC_fmt;
            chunck.fccType = MMIOApi.FOURCC_WAVE;
            Handle((enuMMIOError)MMIOApi.mmioCreateChunk(intPtr,
                ref chunck,
                (uint)enuMMIOCreateChunkParam.Data), "mmioCreateChunk");
            //write pcm header info
            MMIOApi.PCMWAVEFORMAT pcm = new MMIOApi.PCMWAVEFORMAT();
            pcm.wFormatTag = MMIOApi.WAVE_FORMAT_PCM;
            pcm.nAvgBytesPerSec = average;
            pcm.nChannels = (short)channel; //stereo
            pcm.nBlockAlign = (short)blockCount;// block aling
            pcm.wBitsPerSample = (short)bits;//bit per sample 16,8,
            pcm.nSamplesPerSec = frequency;//
            IntPtr v_h = CreateHandle(pcm);
            int i = (int)MMIOApi.mmioWrite(intPtr,
                v_h,
                Marshal.SizeOf(pcm));
            CloseHandle(v_h);
            if (i == -1)
                Handle((enuMMIOError)(-1), "mmioWrite");
            //go to top
            //update the size of the container data
            Ascend(intPtr, ref chunck);
            //update the size of the parent data
            Ascend(intPtr, ref p);
           return p;
        }
        /// <summary>
        /// Create and new wave chunck to MmioStream
        /// </summary>
        /// <param name="stream">the stream</param>
        /// <param name="blockCount">default is 4</param>
        /// <param name="channel">channel default is 2</param>
        /// <param name="bits">bits default is 16</param>
        /// <param name="frequency">frequecny default is 44100</param>
        /// <param name="average">average default is 176400</param>
        public static void CreateWaveChunck(
            MMIOStream stream,
            int blockCount,//4
            int channel ,//2
            int bits,//16
            int frequency , //44100
            int average  //176400
            )
        {            
            //function changed thet top chunck
            CreateWaveChunck(
                stream.Handle,
                blockCount,
                channel,
                bits,
                frequency, 
                average);
        }
        internal static void AddData(MMIOStream stream, byte[] dataByte)
        {
            MMIOApi.MMIOChunkINFO chunck = new MMIOApi.MMIOChunkINFO ();
            AddData(stream.Handle , dataByte ,ref  chunck );           
            //stream.SetCurrentChunck(chunck);
           //Flush(intPtr);           
        }
        internal static void AddData(IntPtr intPtr, byte[] dataByte ,ref MMIOApi.MMIOChunkINFO ch)
        {            
            MMIOApi.MMIOINFO info = GetInfo(intPtr);
            MMIOApi.MMIOChunkINFO chunck = new MMIOApi.MMIOChunkINFO();
            chunck.ckid = MMIOApi.FOURCC_data;
            chunck.fccType = MMIOApi.FOURCC_WAVE;
            chunck.cksize = dataByte.Length;
            Handle((enuMMIOError)MMIOApi.mmioCreateChunk(intPtr,
                   ref chunck,
                   0), "mmioCreateChunk");
            int i = Write(intPtr, dataByte, dataByte.Length);
            //not if you nkown the  size
            Ascend(intPtr, ref chunck);
        }
        public static void Flush(IntPtr h)
        {
            Handle((enuMMIOError)MMIOApi.mmioFlush (h,                           
                           0), "mmioFlush");
        }
        internal static void CloseHandle(IntPtr h)
        {
            Marshal.FreeHGlobal(h);
        }
        internal  static IntPtr CreateHandle(MMIOApi.PCMWAVEFORMAT pcm)
        {
            IntPtr h = Marshal.AllocHGlobal (Marshal .SizeOf (pcm ));
            Marshal.StructureToPtr(pcm, h, true);
            return h;
        }
        internal static MMIOApi.MMIOINFO GetInfo(IntPtr intPtr)
        {
            MMIOApi.MMIOINFO v_info = new MMIOApi.MMIOINFO();
            Handle((enuMMIOError)MMIOApi.mmioGetInfo (intPtr,                
             ref v_info ,
             0), "mmioGetInfo");
            return v_info;
        }
        internal static void SetInfo(IntPtr mmIoHandle, MMIOApi.MMIOINFO info)
        {
            Handle((enuMMIOError)MMIOApi.mmioSetInfo(mmIoHandle,
                      ref info,
                      0), "mmioSetInfo");
        }
        internal static void Descend(IntPtr intPtr,
            ref MMIOApi.MMIOChunkINFO child,
            out MMIOApi.MMIOChunkINFO parent,
            enuMMIOFindMode mode)
        {
            parent = new MMIOApi.MMIOChunkINFO();
            Handle((enuMMIOError)MMIO.MMIOApi.mmioDescend(intPtr,
                ref child ,
                ref parent,
                (uint)mode
               ), "mmioDescend");
        }
        internal static void Ascend(IntPtr intPtr, ref MMIOApi .MMIOChunkINFO  chunck)
        {
            Handle((enuMMIOError)MMIOApi.mmioAscend(intPtr.ToInt32(),
                ref chunck,
                0), "mmioAscend");
        }
        internal static void Advance(IntPtr intPtr, ref MMIOApi.MMIOINFO chunck)
        {
            Handle((enuMMIOError)MMIOApi.mmioAdvance (intPtr,
                ref chunck,
                0), "mmioAdvance");
        }
        internal static void Descend(IntPtr intPtr,
    out MMIOApi.MMIOChunkINFO child,
    enuMMIOFindMode mode)
        {            
            Handle((enuMMIOError)MMIO.MMIOApi.mmioDescend(intPtr,
                out child,
                IntPtr.Zero ,
                (uint)mode
               ), "mmioDescend");
        }
        internal static int Read(IntPtr intPtr, byte[] data, int size)
        {
            int i = MMIOApi.mmioRead(
                intPtr.ToInt32(), data, size);
            return i;
        }
        /// <summary>
        /// write data to io
        /// </summary>
        /// <param name="intPtr"></param>
        /// <param name="data"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public  static int Write(MMIOStream  intPtr, byte[] data, int size)
        {
            int i = (int)MMIOApi.mmioWrite (
                intPtr.Handle , 
                data, 
                size);
            return i;
        }
        public static int Write(IntPtr  intPtr, byte[] data, int size)
        {
            int i = (int)MMIOApi.mmioWrite(
                intPtr, 
                data, 
                size);
            return i;
        }
        /// <summary>
        /// write data to mmio handle
        /// </summary>
        /// <param name="intPtr"></param>
        /// <param name="data"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static int Write(MMIOStream  intPtr, char[] data, int size)
        {
            int i = (int)MMIOApi.mmioWrite(
                intPtr.Handle , data, size);
            return i;
        }
        internal static int Read(IntPtr intPtr, IntPtr data, int size)
        {
            int i = (int)MMIOApi.mmioRead(
                intPtr, data, (long)size);
            return i;
        }
        internal static bool ReadWaveChunck(IntPtr intPtr)
        {
            MMIOApi.MMIOChunkINFO chunck = new MMIOApi.MMIOChunkINFO();
            chunck.fccType =  MMIOApi.FOURCC_WAVE;
            Descend(intPtr, out chunck, enuMMIOFindMode.FindRiff);
            chunck = new MMIOApi.MMIOChunkINFO();
            chunck.fccType = MMIOApi.FOURCC_fmt;
            Descend(intPtr, out chunck, enuMMIOFindMode.Current);
            //ReadWAV FORMAT
            MMIOApi.PCMWAVEFORMAT pcm = new MMIOApi.PCMWAVEFORMAT();
            IntPtr h = CreateHandle (pcm);
            int r = Read(intPtr, h, Marshal.SizeOf(pcm));
            pcm = (MMIOApi.PCMWAVEFORMAT)Marshal.PtrToStructure(h, pcm.GetType()) ;
            CloseHandle(h);
            if (pcm.wFormatTag == MMIOApi.WAVE_FORMAT_PCM)
            {
                Ascend(intPtr, ref chunck);
                return true;
            }
            return false;
        }
        /// <summary>
        /// get embed pcm data from wave file
        /// </summary>
        /// <param name="waveFile"></param>
        /// <returns></returns>
        public static byte[] GetPCMData(string waveFile)
        {
            MMIOStream vm = OpenFile(waveFile, enuMMIOAccess.Read);
            if (vm == null)
                return null;
            if (vm.ReadWaveChunck())
            {
                IntPtr intPtr = vm.Handle;
                byte[] data = null;
                MMIOApi.MMIOChunkINFO chunck = new MMIOApi.MMIOChunkINFO();
                chunck.fccType = MMIOApi.FOURCC_data;
                //go to data chunk
                Descend(vm.Handle, out chunck, enuMMIOFindMode.FindChunk);
                //
                data = new byte[chunck.cksize];
                //
                int r = Read(intPtr, data, data.Length);
                vm.Close();
                return data;
            }
            vm.Close();
            return null;
        }
        /// <summary>
        /// copy file to anotherr
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="targetFile"></param>
        public  static void Copy(string sourceFile, string targetFile)
        {
            MMIOStream source = MMIOStream.OpenFile(sourceFile, enuMMIOAccess.Read );
            MMIOStream dest = MMIOStream.CreateNewFile  (targetFile , enuMMIOAccess .Write );
            //MMIOApi .MMIOChunkINFO chunk = new MMIOApi.MMIOChunkINFO ();
            try
            {
                byte[] d = new  byte [4096];
                int len  =(int) source.Length ;
                int i = 0;
                int h = 0;
                while (i < len)
                {
                    h = source.Read(d, d.Length );
                    if (h>0){
                        dest.Write(d, h);
                        i+=h;
                    }
                    else break ;
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                source.Close();
                dest.Close();
            }
        }
        internal static bool AddRiffChunk(IntPtr intPtr)
        {
           char [] t = new char[]{'R','I','F','F' };
           //MMIOApi.MMIOChunkINFO chunck = new MMIOApi.MMIOChunkINFO();
           //chunck.fccType = MMIOApi.FOURCC_WAVE ;
           MMIOApi.mmioWrite(intPtr, t, 4);
           //Handle((enuMMIOError)MMIOApi.mmioCreateChunk(intPtr,
           //       ref chunck,
           //       0), "mmioCreateChunk");
            return true;
        }
        /// <summary>
        /// create and add new chunk to the current position
        /// </summary>
        /// <param name="intPtr"></param>
        /// <param name="chunck"></param>
        /// <param name="param"></param>
        internal static void CreateChunck(
            IntPtr intPtr, ref MMIOApi .MMIOChunkINFO chunck,
            enuMMIOCreateChunkParam param)
        {
            Handle((enuMMIOError)MMIOApi.mmioCreateChunk(intPtr,
                   ref chunck,
                   (uint)param), "mmioCreateChunk");
        }
        internal static void AddChunk(MMIOStream v_m, int type, enuMMIOCreateChunkParam param)
        {
            MMIOApi.MMIOChunkINFO chunck = new MMIOApi.MMIOChunkINFO();
            chunck.fccType = type;            
            CreateChunck(v_m.Handle, ref chunck, param );
        }
        internal static bool InstallIoProc(string procId, IntPtr  proc)
        {
            int i = MMIOApi.mmioInstallIOProc(
    MMIOApi.getChunck(procId.ToUpper().ToCharArray()),
    proc,
    (int)(enuMMIOInstallFlag.InstallProc));
            return (i != 0);
        }
        internal static bool RemoveIoProc(string procId, IntPtr proc)
        {
            int i = MMIOApi.mmioInstallIOProc(
    MMIOApi.getChunck(procId.ToUpper().ToCharArray()),
    proc,
    (int)(enuMMIOInstallFlag.RemoveProc));
            return (i != 0);
        }

        /// <summary>
        /// install a custom IO Proc handler
        /// </summary>
        /// <param name="procId">string of 4 length exemple: "AVI " or "WAVE" </param>
        /// <param name="proc"></param>
        /// <remarks>used in combination with MCI you must specified the File.EXT+ABC where EXT is the procID</remarks>
        /// <returns></returns>
        public static bool InstallIoProc(string procId, MMIOMsgHandler proc)
        {
            if (proc == null)
                return false;
            MessageHANDLER v_mh = new MessageHANDLER(proc );
            int i = MMIOApi.mmioInstallIOProc(
                MMIOApi.getChunck(procId.ToUpper().ToCharArray()),
                Marshal .GetFunctionPointerForDelegate (v_mh .del ),
                (int)(enuMMIOInstallFlag.InstallProc));
            return (i != 0);
        }
        /// <summary>
        /// register io procedure
        /// </summary>
        /// <param name="procId">4 length proc id</param>
        /// <param name="proc"></param>
        /// <param name="flag"></param>
        internal static bool InstallIoProc(string procId, MMIOProcHandler proc)
        {
            int i = MMIOApi.mmioInstallIOProc(
                MMIOApi.getChunck(procId.ToUpper().ToCharArray()),
                proc,
                (int)(enuMMIOInstallFlag.InstallProc));
            return (i != 0);
        }
        internal sealed class MessageHANDLER
        {
            internal MMIOMsgHandler hmessage;
            internal MMIOProcHandler del;
            internal MMIOApi.MMIOINFO lpmmioinfo;
            public MessageHANDLER(MMIOMsgHandler s)
            {
                this.hmessage = s;
                this.del = new MMIOProcHandler (this.WndProc );
            }
            internal int WndProc(ref MMIOApi.MMIOINFO lpmmioinfo,
enuMMIOMessage uMsg,
IntPtr lParam1,
IntPtr lParam2)
            {
                this.lpmmioinfo = lpmmioinfo;
               int  r = hmessage(new MMIOMsgEventArgs (this, uMsg, lParam1 , lParam2 ));
                lpmmioinfo = this.lpmmioinfo;
                return r;
            }
            internal void SetEndWrite(IntPtr offset)
            {
                this.lpmmioinfo.pchEndWrite = offset;
            }
            internal void SetEndRead(IntPtr offset)
            {
                this.lpmmioinfo.pchEndRead  = offset;
            }
        }
        /// <summary>
        /// Remove te installed IP PRORC
        /// </summary>
        /// <param name="procId">4 length proc id</param>
        /// <param name="proc"></param>
        /// <param name="flag"></param>
        public static bool RemoveIoProc(string procId)
        {
            int i = MMIOApi.mmioInstallIOProc(
                MMIOApi.getChunck(procId.ToUpper().ToCharArray()),
                null,
                (int)enuMMIOInstallFlag.RemoveProc);
            return (i != 0);
        }
        internal static void Seek(MMIOStream stream, int p, enuMMIOSeek enuMMIOSeek)
        {
            //Handle((enuMMIOError)MMIOApi.mmioSeek(stream.Handle, p, (int)enuMMIOSeek),
            //    "mmioSeek");
        }
    }
}

