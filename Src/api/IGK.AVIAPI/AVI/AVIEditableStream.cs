

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AVIEditableStream.cs
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
file:AVIEditableStream.cs
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
using System.Drawing;
namespace IGK.AVIApi.AVI
{
    public class AVIEditableStream :
        AVIFrameStream,
        IDisposable,
        IAVIGetFrame
    {

        private AVIStreamInfoStruct __getInfo(IntPtr handle)
        {
            AVIStreamInfoStruct psi = new AVIStreamInfoStruct();
            if (this.Handle == IntPtr.Zero)
                return psi;
            int r = AVIApi.AVIStreamInfo(
                handle,
                ref psi,
                Marshal.SizeOf(psi));
            return psi;
        }
        private AVIStreamInfoStruct __getInfo() {

            return this.__getInfo(this.Handle);
        }
        public string Encoding {
            get {
                return this.__getInfo().FCCHandlerName;
            }
        }
        public string StreamType
        {
            get
            {
                return this.__getInfo().FCCTypeName;
            }
        }
        public int  Scale {
            get
            {
                return this.__getInfo().dwScale;
            }
        }
        public int  Rate
        {
            get
            {
                return this.__getInfo().dwRate;
            }
        }
        public int Quality
        {
            get
            {
                return this.__getInfo().dwQuality;
            }
        }
        public Rectangle Display
        {
            get
            {
                return this.__getInfo().rcFrame;
            }
        }
        public long Length {
            get {
                return (AVIApi.AVIStreamLength(this.Handle));
            }
        }
        /// <summary>
        /// private ctr.
        /// </summary>
        private AVIEditableStream()
        { 
        }
        /// <summary>
        /// create a new editable stream
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static AVIEditableStream Create(AVIStream stream)
        {
            return CreateFromHwnd(stream.Handle);
        }
        public static AVIEditableStream CreateFromHwnd(IntPtr stream)
        {
            IntPtr v_ref = IntPtr.Zero;
            enuAviError v_result = (enuAviError)AVIApi.CreateEditableStream(ref v_ref, stream);
            //enuAviOpenError v_result = AVIApi.CreateEditableStream(ref v_ref, IntPtr.Zero);
            if (v_result != enuAviError.NoError)
                throw new Exception("Can't create an editable stream");
            AVIEditableStream v_stream = new AVIEditableStream();            
            v_stream.Handle = v_ref;
            //v_stream.SetStreamInfo(AVIStream.GetStreamInfo(stream));
            //v_stream.Paste(stream, 0,(Int32) AVIStream.GetStreamLength(stream));
            return v_stream;
        }
        /// <summary>
        /// create an empty editable stream
        /// </summary>
        /// <returns></returns>
        public static AVIEditableStream CreateEmpty()
        {
            IntPtr v_ref = IntPtr.Zero;
            enuAviError v_result =(enuAviError) AVIApi.CreateEditableStream(ref v_ref,IntPtr.Zero );
            if (v_result != enuAviError.NoError)
                throw new Exception("Can't create an editable stream");
            AVIEditableStream v_stream = new AVIEditableStream();
            v_stream.Handle = v_ref;
            return v_stream;
        }
        /// <summary>
        /// create an empty editable stream
        /// </summary>
        /// <returns></returns>
        public static AVIEditableStream CreateEmpty(AVIStream stream)
        {
            IntPtr v_ref = IntPtr.Zero;
            enuAviError v_result = (enuAviError)AVIApi.CreateEditableStream(ref v_ref, IntPtr.Zero);//stream.Handle );
            if (v_result != enuAviError.NoError)
                throw new Exception("Can't create an editable stream");
            AVIEditableStream v_stream = new AVIEditableStream();            
            v_stream.Handle = v_ref;
            v_stream.SetStreamInfo(AVIStream.GetStreamInfo(stream.Handle));
           AVIEditableStream e =   v_stream.Cut(0, (int)v_stream.Length);
            if (e!=null)
                e.Dispose();
            return v_stream;
        }
        /// <summary>
        /// create an empty editable stream
        /// </summary>
        /// <returns></returns>
        public static AVIEditableStream CreateEditableWaveStream()
        {
            //create a dummy AVIFile
            string tempfile = System.IO.Path.GetTempFileName()+".wav";
            AVIFile f = AVIFile.CreateFile(tempfile );
            f.AddEmptyWave();
            AVIFile .AudioStream aud =  f.GetAudioStream();
            AVIEditableStream e = AVIEditableStream.Create(aud);
            AVIStreamInfoStruct info = AVIStream.GetStreamInfo(e.Handle);
            aud.Dispose();
            f.Close();
            try
            {
                System.IO.File.Delete(tempfile);
            }
            catch { 

            }
            return e;      
        }
        /// <summary>
        /// copy the current editable stream an return a new instance
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public AVIEditableStream Copy(int start, int end)
        {
            AVIEditableStream v_s = null;
            IntPtr h = IntPtr.Zero;
            enuAviError  i = (enuAviError)AVIApi.EditStreamCopy(this.Handle, ref start, ref end, ref h);
            if (i != 0)
                throw new Exception("can't copy");
            v_s = new AVIEditableStream();
            v_s.Handle = h;
            return v_s;
        }
        /// <summary>
        /// copy the editable stream . a create a new editable stream
        /// </summary>
        /// <param name="source"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static AVIEditableStream Copy(AVIStream source, int start, int end)
        {
            AVIEditableStream v_s = null;
            IntPtr h = IntPtr.Zero;
            enuAviError  i = (enuAviError)AVIApi.EditStreamCopy(source.Handle, ref start, ref end, ref h);
            if (i != 0)
                throw new Exception("can't copy");
            
            v_s = new AVIEditableStream();
            v_s.Handle = h;
       //     AVIStreamInfoStruct psi = new AVIStreamInfoStruct ();
       // int r = AVIApi.AVIStreamInfo(
       //     source.Handle,
       //     ref psi,
       //     Marshal.SizeOf(psi));

       //bool k =  v_s.SetStreamInfo(psi);
            return v_s;
        }
        /// <summary>
        /// Cut the editable stream an return a new intance
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public AVIEditableStream  Cut(int start, int length)        
        {
            if (length < 0)
                length = (int)this.Length;
            AVIEditableStream v_s = null;
            IntPtr h = IntPtr.Zero;
            enuAviError  i = (enuAviError)AVIApi.EditStreamCut(this.Handle, ref start, ref length, ref h);
            if (i != 0)
                throw new Exception("can't copy");
            v_s = new AVIEditableStream();
            v_s.Handle = h;
            //AVIStreamInfoStruct psi = new AVIStreamInfoStruct();
            //int r = AVIApi.AVIStreamInfo(
            //    this.Handle,
            //    ref psi,
            //    Marshal.SizeOf(psi));
            //AVIStreamInfoStruct psi2 = new AVIStreamInfoStruct();
            //r = AVIApi.AVIStreamInfo(
            //    h,
            //    ref psi2,
            //    Marshal.SizeOf(psi2));

            //bool k = v_s.SetStreamInfo(psi);
            //var ki = AVIApi.AVIStreamRelease(this.Handle);
            return v_s;
        }
        public void Empty() {
            if (this.Length > 0)
            {
                AVIEditableStream m = this.Cut(0, (int)this.Length);
                m.Dispose();
            }
        }
        /// <summary>
        /// append stream into this stream 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public bool Paste(IntPtr  stream, int start, int end)
        {
            int v_s = (int)AVIStream.GetStreamLength (this.Handle );
            int v_l = end-start;
            enuAviError  i = (enuAviError)AVIApi.EditStreamPaste(
                this.Handle,
                ref v_s,
                ref v_l,
                stream,
                start,
                end);
            return (i == 0);
        }
        /// <summary>
        /// paste the stream
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="at"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public bool Paste(AVIStream stream, int at, int start,int end)
        {
            int v_s = at;
            int v_l = 0;
            enuAviError  i = (enuAviError)AVIApi.EditStreamPaste(
                this.Handle,
                ref v_s,
                ref v_l,
                stream.Handle ,
                start,
                end);
            return (i == 0);
        }
        /// <summary>
        /// paste a source stream on the current editable
        /// </summary>
        /// <param name="source">stream source</param>
        /// <param name="from">start position in stream</param>
        /// <param name="to">end position</param>
        /// <returns></returns>
        public bool Paste(AVIStream source, int from ,int to)
        {
            if ((source==null)|| (this.Handle == IntPtr.Zero))
                return false;

            int v_s = (int)AVIStream.GetStreamLength(this.Handle);
            int v_ds = (int)AVIStream.GetStreamLength(source.Handle);

            int v_l = 0;// to - from;
            //v_s = 0;
            enuAviError  i = (enuAviError)AVIApi.EditStreamPaste(
                this.Handle,
                ref v_s,
                ref v_l,
                source.Handle,
                from,
                to);
            return (i == enuAviError.NoError);
        }
        /// <summary>
        /// set stream info
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        internal bool SetStreamInfo(AVIStreamInfoStruct info)
        {
            enuAviError  i = (enuAviError)AVIApi.EditStreamSetInfo(this.Handle, ref info, Marshal.SizeOf(info));
            return (i ==  enuAviError.NoError );
        }
        /// <summary>
        /// export directy to file. no compression used
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool ExportToFile(string filename, AVISaveProgressionCallBack progressionCallBack)
        {
            AVIFile f = AVIFile.CreateFileFromStreams(this.Handle );
            if (f != null)
            {
                bool v = f.SaveTo(filename, progressionCallBack);
                f.Dispose();
                return v;
            }
            return false;
        }
        /// <summary>
        /// export editable stream to
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="progressionCallBack"></param>
        /// <param name="isCompressed"></param>
        /// <returns></returns>
        public bool ExportToFile(string filename,AVISaveProgressionCallBack progressionCallBack, bool isCompressed)
        {
            if (!isCompressed)
            {
                return ExportToFile(filename, null);
            }
            enuAviError i = enuAviError.NoError;
            AVIApi.AVICOMPRESSOPTIONS_CLASS c = new AVIApi.AVICOMPRESSOPTIONS_CLASS();
            AVIStreamInfoStruct st = AVIStream.GetStreamInfo (this.Handle );
            c.fccType = (uint) st.fccType;
            c.fccHandler = (uint)st.fccHandler;
            IntPtr v_h = this.Handle;
            bool v = AVIApi.AVISaveOptions(IntPtr.Zero,
                AVIApi.ICMF_CHOOSE_DATARATE |
                AVIApi.ICMF_CHOOSE_KEYFRAME |
                AVIApi.ICMF_CHOOSE_PREVIEW,
                1,
                ref v_h,
                ref c);
            AVIApi.AVICOMPRESSOPTIONS[] v_t = new  AVIApi.AVICOMPRESSOPTIONS []{c.ToStruct()};
            i = (enuAviError)AVIApi.AVISaveV(filename, 0, progressionCallBack, 1,new IntPtr[]{v_h}, ref v_t);
            AVIApi.AVISaveOptionsFree(1, ref c);
            return (i ==  enuAviError .NoError );
        }
        public override void Dispose()
        {
            int  i = AVIApi.AVIStreamRelease(this.Handle);
            if (i > 0)
            {
                System.Diagnostics.Debug.WriteLine("[EditableStream] - StreamInUsed " + i);
            }
            else
            {
                this.Handle = IntPtr.Zero;
            }
        }
        /// <summary>
        /// create editatable stream from a video stream
        /// </summary>
        /// <param name="vid"></param>
        /// <returns></returns>
        public static AVIEditableStream CreateFrom(AVIFile.VideoStream vid)
        {
            if (vid == null)
                return null;
            IntPtr v_ref = IntPtr.Zero;
            enuAviError v_result = (enuAviError)AVIApi.CreateEditableStream(ref v_ref, vid.Handle);
            if (v_result != enuAviError.NoError)
                throw new Exception("Can't create an editable stream");
            AVIEditableStream v_stream = new AVIEditableStream();
            v_stream.Handle = v_ref;
            AVIStreamInfoStruct psi = v_stream.__getInfo();
            //int r = AVIApi.AVIStreamInfo(
            //    vid.Handle,
            //    ref psi,
            //    Marshal.SizeOf(psi));

            //bool k = v_stream.SetStreamInfo(psi);
            //int h = AVIApi.AVIStreamRelease(vid.Handle);
            return v_stream;
        }
        /// </summary>
        /// <param name="vid"></param>
        /// <returns></returns>
        public static AVIEditableStream CreateFrom(AVIFile.AudioStream audio)
        {
            if (audio == null)
                return null;
            IntPtr v_ref = IntPtr.Zero;
            enuAviError v_result = (enuAviError)AVIApi.CreateEditableStream(ref v_ref, audio.Handle);
            if (v_result != enuAviError.NoError)
                throw new Exception("Can't create an editable stream");
            AVIEditableStream v_stream = new AVIEditableStream();
            v_stream.Handle = v_ref;
            return v_stream;
        }
        /// <summary>
        /// set the description name
        /// </summary>
        /// <param name="p"></param>
        public void SetName(string name)
        {
            IntPtr h  = Marshal.StringToCoTaskMemAnsi(name);
            AVIApi.EditStreamSetName(this.Handle , ref h);
            Marshal.FreeCoTaskMem(h);
        }
        public void ExportToFile(string p)
        {
            AVIFile c = AVIFile.Open(p, enuAviAccess.Create);
            c.CopyStream(this.Handle);
            c.Close();
            //AVIApi.AVICOMPRESSOPTIONS[] options = null;//knew AVIApi.AVICOMPRESSOPTIONS[1];
            ////options[0] = ((AVIApi.AVICOMPRESSOPTIONS_CLASS)AVIStream.ChooseCodec(IntPtr.Zero, this.Handle)).ToStruct ();
            //enuAviError   v_r = AVIApi.AVISaveV(p, 0, null, 1, new IntPtr[] { this.Handle },ref options );
        }
        /// <summary>
        /// write binary data to editable stream
        /// </summary>
        /// <param name="b"></param>
        public void WriteData(byte[] b)
        {
            IntPtr alloc  =Marshal.AllocCoTaskMem (b.Length );
            Marshal.Copy(b, 0, alloc, b.Length);
            int sample = 0;
            int bytes = 0;
            enuAviError r =(enuAviError) AVIApi.AVIStreamWrite(this.Handle, 0, -1, alloc, b.Length, enuAviWriteMode.None , ref sample, ref bytes);
            Marshal.FreeCoTaskMem(alloc);
        }
    }
}

