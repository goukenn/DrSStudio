

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AVIStream.cs
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
file:AVIStream.cs
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
namespace IGK.AVIApi.AVI
{
    public abstract class AVIStream : IDisposable
    {
        private IntPtr m_handle;
        public IntPtr Handle { get { return this.m_handle; } protected set { this.m_handle = value; } }
        public AVITimeInfo GetTimeInfoFromSample(long sample)
        {
            long i= AVIApi.AVIStreamSampleToTime(this.Handle, (int)sample );
            if (i == -1)
                return new AVITimeInfo ();
            return AVITimeInfo.GetTime(i);
        }
        /// <summary>
        /// get format ptr
        /// </summary>
        /// <returns></returns>
        internal IntPtr GetFormatPtr()
        {
            return GetFormatPtr(0);
        }
        internal IntPtr GetFormatPtr(int i)
        {
            return GetFormatPtr(this.Handle, i);
        }
      

        internal static IntPtr GetFormatPtr(IntPtr stream, int i)
        {
            int v_formatSize = AVIStream.GetSizeOfFormat(stream, i);
            if (v_formatSize > 0)
            {
                IntPtr v_alloc = Marshal.AllocCoTaskMem(v_formatSize);
                enuAviError error = (enuAviError)AVIApi.AVIStreamReadFormat(stream, i, v_alloc, ref v_formatSize);
                if (error != enuAviError.NoError)
                    return IntPtr.Zero;
                return v_alloc;
            }
            return IntPtr.Zero;
        }
        /// <summary>
        /// free format ptr
        /// </summary>
        /// <param name="handle"></param>
        internal static void FreeFormatPtr(IntPtr handle)
        {
            Marshal.FreeCoTaskMem(handle);
        }
        public long GetTimeFromSample(long millisecond)
        {
            long i = AVIApi.AVIStreamTimeToSample(this.Handle, (int )millisecond + 1);
            return i;
        }
        public long GetTimeFromSample(AVITimeInfo info)
        {
            return GetTimeFromSample((long)info.TimeSpan.TotalMilliseconds);
        }
        /// <summary>
        /// read a data
        /// </summary>
        /// <param name="stream">data stream to read</param>
        /// <param name="start">start</param>
        /// <param name="sample">samplee</param>
        /// <param name="data">data length</param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static int ReadData(AVIStream stream, int start, int sample,
            byte[] data,
            int length)
        {
            int plByte = 0;
            int plSample = 0;
            int v_result = AVIApi.AVIStreamRead(stream.Handle,
                start,
                sample,
                data,
                length,
                ref plByte,
                ref plSample);
            if (v_result != AVIApi.NOERROR )
            {//error
                return -1;
            }
            return plByte;
        }
        public static int ReadData(AVIStream stream, int start, int sample,
           byte[] data,
           int length,
            ref int plSample)
        {
            int plByte = 0;
            enuAviError r = (enuAviError)AVIApi.AVIStreamRead(stream.Handle,
                start,
                sample,
                data,
                length,
                ref plByte,
                ref plSample);
            if (r != enuAviError.NoError)
            {//error
                return -1;
            }
            return plByte;
        }
        public static  bool ExtractStreamToDataFile(AVIStream stream, string filename)
        {
            if (stream == null)
                return false;
            AVIStreamInfoStruct info= AVIStream.GetStreamInfo(stream.Handle);
            Byte[] data = new Byte[
            (info.dwSuggestedBufferSize > 0) ? info.dwSuggestedBufferSize*1024 : 4096];
            BinaryWriter binw = new BinaryWriter(File.Open(filename, FileMode.Create));
            int count = 0;
            int sample = 0;
            int rsample = 0;
            try
            {
                //AVIApi.AVIStreamBeginStreaming(stream.Handle, 0, info.dwLength, 1000);
                while ((count = ReadData(stream, sample, 1, data, data.Length, ref rsample)) > 0)
                {
                    binw.Write(data, 0, count);
                    sample += rsample;
                }
                //AVIApi.AVIStreamEndStreaming(stream.Handle );
            }
            catch
            {
            }
            finally
            {
                if (binw != null)
                {
                    binw.Flush();
                    binw.Close();
                }
            }
            return (sample > 0);
        }
    
        public static bool SetStreamFormat(AVIStream stream, IntPtr format , int size)
        {
            return SetStreamFormat(stream.Handle, format, size);
        }
        public static bool SetStreamFormat(IntPtr stream, IntPtr format, int size)
        {
            return AVIApi.AVIStreamSetFormat(
                stream,
                0,
                format,
                size) == 0;
        }
        /// <summary>
        /// return the format size of this video stream
        /// </summary>
        /// <returns></returns>
        public int GetFormatSize()
        {
            int v_size = 0;
            AVIApi.AVIStreamReadFormat(Handle, 0, IntPtr.Zero, ref v_size);
            return v_size;
        }
        /// <summary>
        /// when overrided must free the allocated Handle
        /// </summary>
        public virtual void Dispose() { 
        }
        public static AVIStreamInfoStruct GetStreamInfo(IntPtr pavistream)
        {
            AVIStreamInfoStruct psi = new AVIStreamInfoStruct();
            AVIApi.AVIStreamInfo(pavistream, ref psi, Marshal.SizeOf(psi));
            return psi;
        }
        public static int GetSizeOfFormat(AVIStream stream)
        {
            return GetSizeOfFormat(stream.Handle);
        }
        public static int GetSizeOfFormat(IntPtr pstream)
        {
            return GetSizeOfFormat(pstream, 0);
        }
        public static int GetSizeOfFormat(IntPtr pstream, int pos)
        {
            int v_s = 0;
            int e = AVIApi.AVIStreamReadFormat(pstream, pos, IntPtr.Zero, ref v_s);
            if (e !=  AVIApi.NOERROR )
                return -1;
            return v_s;
        }
        public static long GetDataSize(IntPtr pstream , int type)
        {
            int v_s = 0;
            int v_result = AVIApi.AVIStreamReadData(pstream, type, IntPtr.Zero, ref v_s);
            return v_s;
        }
        public static long GetStreamLength(IntPtr pstream)
        {
            if (pstream == IntPtr.Zero)
                return -1;
            long l = 0;
            l = AVIApi.AVIStreamLength(pstream);
            return l;
        }
        public static IntPtr GetStreamFormat(IntPtr pstream)
        {
            int v_s = GetSizeOfFormat(pstream);
            if (v_s == -1)
                return IntPtr.Zero;
            IntPtr v_alloc = Marshal.AllocCoTaskMem(v_s);
            AVIApi.AVIStreamReadFormat(pstream, 0, v_alloc, ref v_s);
            return v_alloc;
        }
        public static object GetStreamFormat(IntPtr pstream, Type type)
        {
            int v_s = Marshal.SizeOf(type);
            IntPtr v_alloc = Marshal.AllocCoTaskMem(v_s);
            AVIApi.AVIStreamReadFormat(pstream, 0, v_alloc, ref v_s);
            Object o = Marshal.PtrToStructure(v_alloc, type);
            Marshal.FreeCoTaskMem(v_alloc);
            return o;
        }
        //public  static AVIStream CreateMemoryStream()
        //{
        //    return AVIMemoryStream.CreateStream();
        //}
        internal  static bool SaveV(string filename, short p, AVISaveProgressionCallBack progressCallBack, short nbrstream, IntPtr[] Handle, ref AVIApi.AVICOMPRESSOPTIONS[] rf)
        {            
            enuAviError  i = (enuAviError)AVIApi.AVISaveV(filename, p, progressCallBack, nbrstream,
                Handle ,
               ref rf);
            return i == enuAviError.NoError;
        }
        public static void AviReadInfo(IntPtr stream, int start, int samples, ref int rbytes, ref int rsamples)
        {
            AVIApi.AVIStreamRead (stream, start, samples, null,0, ref rbytes, ref  rsamples);
        }
        public static void CloseStream(IntPtr intPtr)
        {
            if (intPtr != IntPtr .Zero )
                AVIApi.AVIStreamRelease(intPtr);
        }
        /// <summary>
        /// get mili second of matching sample
        /// </summary>
        /// <param name="intPtr"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static int GetMilliSecond(IntPtr intPtr, int p)
        {
            long  i = AVIApi.AVIStreamSampleToTime(intPtr,
                p);
            return (int)i;
        }
        /// <summary>
        /// choose the codec
        /// </summary>
        /// <param name="hwnd">Parent handle of dialog window</param>
        /// <param name="streamHandle">Stream handle</param>
        /// <returns>codec option returned</returns>
        public static object  ChooseCodec(IntPtr hwnd, 
            IntPtr streamHandle)
        {
            if (streamHandle == null)
                return null;
            int v_type = GetStreamInfo(streamHandle).fccType ;
            int v_handle = GetStreamInfo(streamHandle).fccHandler  ;
                AVIApi.AVICOMPRESSOPTIONS_CLASS rf = new AVIApi.AVICOMPRESSOPTIONS_CLASS();
                rf.fccType  = (uint)v_type;
                rf.fccHandler = (uint)v_handle;// AVIApi.streamtypeCAUD;
                IntPtr d = streamHandle;
                if (AVIApi.AVISaveOptions(hwnd,
                    AVIApi.ICMF_CHOOSE_DATARATE |
                    AVIApi.ICMF_CHOOSE_PREVIEW, 1,
                    ref d,
                    ref rf))                
                    return rf;            
                else
                    return null;
        }
        public static bool FreeChooseCodec(object codecOption)
        {
            AVIApi.AVICOMPRESSOPTIONS_CLASS rf = codecOption as AVIApi.AVICOMPRESSOPTIONS_CLASS;
            if (rf == null)
                return false;
            //return rf;
            long h = AVIApi.AVISaveOptionsFree(1, ref rf);
            return (h == 0);
        }
        /// <summary>
        /// compress audio steam. and save ite to a avi file
        /// </summary>
        /// <param name="p"></param>
        /// <param name="v_aud"></param>
        /// <param name="p_3"></param>
        public static void Compress(string p, 
            AVIFile.AudioStream aud, 
            bool choosecodec)
        {
            IntPtr hout = IntPtr.Zero ;
            AVIApi.AVICOMPRESSOPTIONS rf = new AVIApi.AVICOMPRESSOPTIONS();
            object codec = null;
            if (choosecodec )
            {
                codec = ChooseCodec(IntPtr .Zero , aud.Handle);
                rf = (codec as AVIApi .AVICOMPRESSOPTIONS_CLASS ).ToStruct ();
            }
            int h = AVIApi.AVIMakeCompressedStream(out hout,
                aud.Handle ,
                ref rf, 0 );
            if (codec != null )
            {
                FreeChooseCodec (rf );
            }
            if (h == 0)
            {
                AVIFile.AudioStream  audStream = new AVIFile.AudioStream(hout);
                AVIStream.ExtactTo(p, 
                    audStream.Handle );
                audStream.Dispose();
            }
        }
        internal static void HandleError(int r, string method)
        {
            if (r == AVIApi .NOERROR)
                return;
            throw new Exception(string.Format("{0} : {1}", r, method));
        }
        internal static void HandleError(enuAviError r, string method)
        {
            if (r == enuAviError.NoError)
                return;
            throw new Exception(string.Format("{0} : {1}", r, method));
        }
        public static bool ExtactTo(string filename, IntPtr hstream)
        {
          AVIStreamInfoStruct info = GetStreamInfo (hstream );
          if (info.dwLength == 0)
              return false;
          BinaryWriter binw = null;
                //copy data
                int v_size = info.dwLength  * info.dwSampleSize ;
                IntPtr v_alloc = IntPtr.Zero;
                int bytes = 0;
                int samples = 0;
                int v_result = AVIApi.NOERROR;
                int start =0;
                binw = new BinaryWriter(File.Create(filename));
                if (v_size < 4096)
                {
                    v_alloc = Marshal.AllocCoTaskMem(v_size);
                    v_result = AVIApi.AVIStreamRead(hstream,
                        start, 
                        -1 , 
                        v_alloc, 
                        v_size, ref bytes, ref samples);
                    HandleError(v_result, "AVIStreamRead");
                    Byte[] t = new byte[bytes];
                    Marshal.Copy(v_alloc, t, 0, t.Length);
                    binw.Write(t, 0, bytes);
                }
                else {
                    int bufferSize = (info.dwSampleSize == 1)?
                        info.dwRate :
                    v_size;
                    v_alloc = Marshal.AllocCoTaskMem(bufferSize);
                    int max_sample = info.dwLength / info.dwSampleSize;
                    while (max_sample  > start)
                    {
                        v_result = AVIApi.AVIStreamRead(hstream,
                        start, 
                        -1,
                        v_alloc, 
                        bufferSize ,
                        ref bytes, 
                        ref samples);
                        if ((enuAviError)v_result == enuAviError.FileRead)
                            break;
                        HandleError(v_result, "AVIStreamRead");
                        Byte[] t = new byte[bufferSize];
                        Marshal.Copy(v_alloc, t, 0, bytes);
                        binw.Write(t, 0, t.Length );
                        HandleError(v_result, "AVIStreamWrite");
                        v_size -= bufferSize ;
                        start += samples;                      
                    }
                }
                Marshal.FreeCoTaskMem(v_alloc);
                binw.Flush();
                binw.Close();
                return true;
        }
        public static AVIFile.AudioStream GetAudioStream(IntPtr intPtr)
        {
            if (intPtr != IntPtr.Zero) 
            return new AVIFile.AudioStream(intPtr);
            return null;
        }
        /// <summary>
        /// write data to stream
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool WriteData(AVIFile.AudioStream stream,enuAviWriteMode mode, int nsample, byte[] data)
        {
            //IntPtr alloc = Marshal.AllocCoTaskMem(data.Length);
            //Marshal.Copy(data, 0, alloc, data.Length);
            int sample = 0;
            int bytes = 0;
            enuAviError r = (enuAviError)AVIApi.AVIStreamWrite(stream.Handle, stream.Length,
                nsample , data , data.Length, mode, ref sample, ref bytes);
            stream.GetStreamInfo();
            //Marshal.FreeCoTaskMem(alloc);
            return (r == enuAviError.NoError);
        }
        internal static bool SetStreamFormat(AVIStream stream, IWAVEFORMAT format, int position)
        {
            return AVIApi.AVIStreamSetFormat(stream.Handle, position, ref format, Marshal.SizeOf(format))
                == AVIApi.NOERROR;
        }
        public static void SaveTo(string file, AVIStream v_caud, object obj)
        {
            AVIApi.AVICOMPRESSOPTIONS_CLASS c = obj as AVIApi.AVICOMPRESSOPTIONS_CLASS;
            AVIApi.AVICOMPRESSOPTIONS[] t = new AVIApi.AVICOMPRESSOPTIONS []{c.ToStruct()};
            bool i = AVIStream.SaveV(file, 0, null, 1, new IntPtr[] { v_caud.Handle }, ref t);
        }

     
    }
}

