

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AVIFile.cs
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
file:AVIFile.cs
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
/* 
-------------------------------------------------------------
This file is part of iGK-DEV-DrawingStudio
-------------------------------------------------------------
-------------------------------------------------------------
-------------------------------------------------------------
view license file in Documentation folder to get more info
Copyright (c) 2008-2010 
Author  : Charles A.D. BONDJE DOUE 
mail : bondje.doue@hotmail.com
-------------------------------------------------------------
-------------------------------------------------------------
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Drawing2D ;
using System.Drawing.Imaging;
//###################################################################""""""
//CAD .BONDJE DOUE
//###################################################################""""""
namespace IGK.AVIApi.AVI
{
    using IGK.ICore;using IGK.AVIApi.Native;
    using IGK.ICore.IO;
    /// <summary>
    /// manage and open Avi and Wav file.
    /// </summary>
    public class AVIFile: IDisposable 
    {
        private IntPtr m_pFile;   //handler to avi file        
        private AVIApi.AVIFileInfoStruct m_fileInfo;
        public const int NO_COMPRESSION = 0;
        private static int sm_FileCounter;
      
        /// <summary>
        /// return the number au audio stream in the file
        /// </summary>
        public int GetNumberOfAudioStreams()
        {
                AVIStreamInfoStruct v_streamInfo = new AVIStreamInfoStruct();
                IntPtr ppavi;
                int size = Marshal.SizeOf(v_streamInfo);
                int au = 0;
                for (int i = 0; i < NumberOfStream; i++)
                {
                    if ((enuAviError)AVIApi.AVIFileGetStream(this.Handle, out ppavi, 0, i) == enuAviError.NoError)
                    {
                        AVIApi.AVIStreamInfo(ppavi, ref v_streamInfo, size);
                        if (v_streamInfo.fccType == AVIApi.streamTypeAudio  )
                        {
                            au++;
                        }
                    }
                }
                return au;
        }
        internal IntPtr[] GetStreams()
        {
            AVIStreamInfoStruct v_streamInfo = new AVIStreamInfoStruct();
            IntPtr ppavi;
            int size = Marshal.SizeOf(v_streamInfo);
            IntPtr[] v_c = new IntPtr[NumberOfStream];
            int t = 0;
            for (int i = 0; i < NumberOfStream; i++)
            {
                if ((enuAviError)AVIApi.AVIFileGetStream(this.Handle, out ppavi, 0, i) == enuAviError.NoError)
                {
                    v_c[t] = ppavi;
                    t++;
                }
            }
            return  v_c;
        }
        public int GetNumberOfVideoStreams()
        {
                AVIStreamInfoStruct v_streamInfo = new AVIStreamInfoStruct();
                IntPtr ppavi;
                int size = Marshal.SizeOf(v_streamInfo);
                int au = 0;
                for (int i = 0; i < NumberOfStream; i++)
                {
                    if (AVIApi.AVIFileGetStream(this.Handle, out ppavi, 0, i) == AVIApi.NOERROR)
                    {
                        AVIApi.AVIStreamInfo(ppavi, ref v_streamInfo, size);
                        if (v_streamInfo.fccType == AVIApi.streamTypeVideo )
                        {
                            au++;
                        }
                    }
                }
                return au;
        }
        /// <summary>
        /// get the handle of the avi file
        /// </summary>
        public IntPtr Handle {
            get {
                return m_pFile;
            }
        }
        /// <summary>
        /// get the length of this avi file
        /// </summary>
        public long Length {
            get { return this.m_fileInfo .dwLength; }
        }
        private AVIFile()
        {
            sm_FileCounter++;
        }
        ~AVIFile()
        {
            try
            {
                this.Close();
            }
            catch { 
            }
        }
        public void Close()
        {
            //close file
            if (this.m_pFile != IntPtr.Zero)
            {
                try
                {
                    int  i = AVIApi.AVIFileRelease(this.m_pFile);
                    if (i == AVIApi.NOERROR )
                    {
                        sm_FileCounter--;
                        this.m_pFile = IntPtr.Zero;
                        this.m_fileInfo = new AVIApi.AVIFileInfoStruct();
                        if (sm_FileCounter == 0)
                        {
                            AVIApi.AVIFileExit();
                        }
                    }
                }
                catch { 

                }
            }
        }
        /// <summary>
        /// create a new AVIFile at the specified location
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static AVIFile CreateFile(string filename)
        {
            return CreateFromFile(filename, enuAviAccess.Create |                 
                enuAviAccess .ReadWrite  );
        }
        /// <summary>
        /// create from file
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="access"></param>
        /// <returns></returns>
        public static AVIFile CreateFromFile(string filename, enuAviAccess access)
        {
            return Open(filename, access);
        }
        /// <summary>
        /// create avi file in memory
        /// </summary>
        /// <returns></returns>
        public static AVIFile CreateInMemory()
        {
            AVIApi.AVIFileInit();
            IntPtr alloc = IntPtr.Zero;
            IntPtr g = IntPtr.Zero;
            enuAviError o = enuAviError.NoError;
            AVIEditableStream editStream = AVIEditableStream.CreateEmpty();
            editStream.SetStreamInfo(AVIFile.GetUncompressedVideoInfo(enuAVIVideoFormat.Format32, 1, 24, 400, 300));
            editStream.SetName("IGK_AVI_My_Sample");
            //open file
            o = (enuAviError)AVIApi.AVIMakeFileFromStreams(ref alloc, 1, new IntPtr[] { editStream.Handle });
            if (o == enuAviError.NoError)
            {
                AVIFile m = new AVIFile();
                m.m_pFile = alloc;
                m.GetFileInfo();
                return m;
            }
            return null;
        }
        /// <summary>
        /// open file with desired access.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="access"></param>
        /// <returns></returns>
        /// <remarks>filename must be a valid file with system supported extension like .avi or .wav</remarks>
        public static AVIFile Open(string filename, enuAviAccess access)
        {
            AVIApi.AVIFileInit();
            IntPtr alloc = IntPtr.Zero;
            //open file
            enuAviError o =(enuAviError) AVIApi.AVIFileOpen(out alloc, filename, (int)access, IntPtr.Zero );
            if (o == enuAviError.NoError)
            {
                AVIFile m = new AVIFile();
                m.m_pFile = alloc;                
                m.GetFileInfo ();
                return m;
            }
            return null;
        }
        /// <summary>
        /// open a file video
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static AVIFile Open(string filename)
        {
            return Open(filename, enuAviAccess.Read | enuAviAccess.Write);
        }
        /// <summary>
        /// save the current AVIFile stream a another file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool Save(string filename)
        {
            if (File.Exists(filename))
                File.Delete(filename);
            AVIFile c = Open(filename, enuAviAccess.Create | enuAviAccess.Write);
            if (c == null)
                return false;
            int i = this.NumberOfStream;
            for (int t = 0; t < i; t++)
            {
                IntPtr stream = this.GetStream(i);
                c.AddStream(stream);
            }
            AVIFile.AudioStream v_a = this.GetAudioStream ();
            AVIFile.VideoStream v_v = this.GetVideoStream ();
            if (v_v != null)
            {
                c.AddVideoStream(v_v, enuAviWriteMode.None);
                v_v.Dispose();
            }
            if (v_a != null)
            {
                c.AddStream(v_a);
                v_a.Dispose();
            }
            c.Close();       
            return true;
        }
        private IntPtr GetStream(int i)
        {
            IntPtr ppavi = IntPtr.Zero ;
            enuAviError e = (enuAviError)AVIApi.AVIFileGetStream(
                this.Handle,
                out ppavi,
                0,
                i);
            return ppavi;
        }
        /// <summary>
        /// add stream
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public bool AddStream(IntPtr stream)
        {
            bool v_result = false;
            if (stream == IntPtr.Zero)
                return v_result;
            //using (AVIEditableStream v_s = AVIEditableStream.CreateFromHwnd(stream))
            //{
            //    if (v_s != null)
            //    {
            
                    //int mode = 0;
                    IntPtr allocOld = IntPtr.Zero;// Marshal.AllocCoTaskMem(v_bufferSize);
                    enuAviError i = 0;
                    AVIStreamInfoStruct v_streamInfo = AVIStream.GetStreamInfo(stream);
                    IntPtr v_outStream = IntPtr.Zero;

                    //create new stream 
                    i = (enuAviError)AVIApi.AVIFileCreateStream(this.m_pFile, out v_outStream, ref v_streamInfo);
                    if (v_outStream == IntPtr.Zero)
                        return false;
                    IntPtr v_rh = AVIStream.GetFormatPtr(stream,0);
                    int v_formatSize = AVIStream.GetSizeOfFormat(stream);
                    //set format
                    if (!AVIStream.SetStreamFormat(v_outStream, v_rh, v_formatSize))
                    {
                        AVIApi.AVIStreamRelease(v_outStream);
                        AVIStream.FreeFormatPtr(v_rh);
                        return false;
                    }
                    AVIStream.FreeFormatPtr(v_rh);

                    v_result = AVIStreamUtils.CopyStream(stream, v_outStream, v_streamInfo);
                   
                    AVIApi.AVIStreamRelease(v_outStream);
                    //v_div.Dispose();
                    Marshal.FreeCoTaskMem(allocOld);
                //}
            //}
            return v_result;
        }
        public bool SaveTo(string filename,
            AVISaveProgressionCallBack callback)
        {
            int i = NumberOfStream;
            IntPtr [] s = new IntPtr[i];
            AVIApi.AVICOMPRESSOPTIONS[] t = new AVIApi.AVICOMPRESSOPTIONS[i];
            IntPtr ppavi = IntPtr.Zero;
            AVIApi.AVICOMPRESSOPTIONS_CLASS opts = null;
            AVIStreamInfoStruct v_info;
            for (int n = 0; n < NumberOfStream; n++)
            {
                if ((enuAviError)AVIApi.AVIFileGetStream(this.Handle, out ppavi, 0, n) == enuAviError.NoError)
                {
                    s[n] = ppavi;
                    v_info = AVIStream.GetStreamInfo(ppavi);
                    v_info.szName = Path.GetFileName(filename);                    
                    opts = new AVIApi.AVICOMPRESSOPTIONS_CLASS();
                    opts.fccType = (uint)v_info.fccType;
                    opts.fccHandler = (uint)v_info.fccHandler;
                    t[n] = opts.ToStruct();
                }
            }
            int r = AVIApi.AVISaveV(filename,
                0, 
                callback,
                1, 
                s, 
                ref t);
            //free handle
            for (int n = 0; n < s.Length; n++)
            {
                AVIApi.AVIStreamRelease(s[n]);
            }
            return (r == 0);
        }
        public bool SaveTo(string filename,
           AVISaveProgressionCallBack callback,
            object audioCodec,
            object videoCodec)
        {
            int i = NumberOfStream;
            IntPtr[] s = new IntPtr[i];
            AVIApi.AVICOMPRESSOPTIONS[] t = new AVIApi.AVICOMPRESSOPTIONS[i];
            IntPtr ppavi = IntPtr.Zero;
            int v_type = 0;
            for (int n = 0; n < NumberOfStream; n++)
            {
                if ((enuAviError)AVIApi.AVIFileGetStream(this.Handle, out ppavi, 0, n) == enuAviError.NoError)
                {
                    s[n] = ppavi;
                    v_type = AVIStream.GetStreamInfo(ppavi).fccType;
                    if (v_type == AVIApi.streamTypeAudio)                        
                    {
                        if (audioCodec  is AVIApi.AVICOMPRESSOPTIONS_CLASS)
                            t[n] = (audioCodec as AVIApi.AVICOMPRESSOPTIONS_CLASS).ToStruct();
                    }
                    else if (v_type == AVIApi.streamTypeVideo)
                    {
                        if (videoCodec is AVIApi.AVICOMPRESSOPTIONS_CLASS)
                            t[n] = (videoCodec as AVIApi.AVICOMPRESSOPTIONS_CLASS).ToStruct();
                    }
                    else
                        t[n] = new AVIApi.AVICOMPRESSOPTIONS();
                }
            }
            int r = (int)AVIApi.AVISaveV(filename,
                0,
                callback,
                (short)i,
                s,
                ref t);
            //free handle
            for (int n = 0; n < s.Length; n++)
            {
                AVIApi.AVIStreamRelease(s[n]);
            }
            return (r == 0);
        }
        public AVIStream[] GetAllStreams()
        {
            List<AVIStream> v_list = new List<AVIStream>();
            int v_li = NumberOfStream;
            IntPtr ppavi;
            AVIStreamInfoStruct v_streamInfo = new AVIStreamInfoStruct();
            int size = Marshal.SizeOf(v_streamInfo);
            for (int i = 0; i < NumberOfStream ; i++)
            {
                if (AVIApi.AVIFileGetStream(this.Handle, out ppavi, 0, i) == AVIApi.NOERROR)
                {
                    AVIApi.AVIStreamInfo(ppavi, ref v_streamInfo, size);
                    if (v_streamInfo.fccType == AVIApi.streamTypeVideo)
                    {
                        v_list.Add (new VideoStream (ppavi,this));
                    }
                    else if (v_streamInfo.fccType == AVIApi.streamTypeAudio)
                    {
                        v_list.Add (new AudioStream (ppavi));
                    }
                    else if (v_streamInfo.fccType == AVIApi.streamtypeTEXT)
                    {
                        int v_size = AVIStream.GetSizeOfFormat (ppavi);
                        IntPtr h = Marshal.AllocCoTaskMem (v_size);
                        v_list.Add(new AVITextStream(ppavi, h));
                    }
                }
            }
            return v_list.ToArray ();
        }
        public VideoStream GetVideoStream()
        {
            IntPtr v_videoStream = IntPtr.Zero ;
            int o = AVIApi.AVIFileGetStream(
                this.m_pFile, 
                out v_videoStream, 
                AVIApi.streamTypeVideo,
            0);
            if (o != AVIApi.NOERROR)
                return null;
            AVIStreamInfoStruct faviStream = new AVIStreamInfoStruct();
            int size = Marshal.SizeOf(faviStream);
            AVIApi.AVIStreamInfo(v_videoStream, ref faviStream, size);
            VideoStream vid = new VideoStream(v_videoStream, this);
            return vid;
        }
        public AudioStream GetAudioStream()
        {
            if (this.m_pFile == IntPtr.Zero)
                return null;
            IntPtr v_audioStream;
            int o = AVIApi.AVIFileGetStream(
                this.m_pFile, 
                out v_audioStream, 
                AVIApi.streamTypeAudio, 
                0);
            if (o != AVIApi.NOERROR )
                return null;
            AVIStreamInfoStruct faviStream = new AVIStreamInfoStruct();
            int size = Marshal.SizeOf(faviStream);
            AVIApi.AVIStreamInfo(v_audioStream, ref faviStream, size);
            AudioStream vid = new AudioStream(v_audioStream);
            return vid;
        }
        public enuAviFileCap FileCap
        {
            get
            {
                this.GetFileInfo();
                return (enuAviFileCap)this.m_fileInfo.dwCaps;
            }
        }
        /// <summary>
        /// get the default width
        /// </summary>
        public int Width
        {
            get
            {
                this.GetFileInfo();
                return this.m_fileInfo.dwWidth;
            }
        }
        /// <summary>
        /// get the default height 
        /// </summary>
        public int Height
        {
            get
            {
                this.GetFileInfo();
                return this.m_fileInfo.dwHeight;
            }
        }
        public int Rate
        {
            get
            {
                this.GetFileInfo();
                return this.m_fileInfo.dwRate;
            }
        }
        public int Scale
        {
            get
            {
                this.GetFileInfo();
                return this.m_fileInfo.dwScale;
            }
        }
        /// <summary>
        /// get thet rate per  second of the file
        /// </summary>
        public int RatePerSecond
        {
            get
            {                
                if (Scale != 0)
                {
                    return this.m_fileInfo.dwRate / Scale;
                }
                return -1;
            }
        }
        /// <summary>
        /// get the number of stream in the avi file
        /// </summary>
        public int NumberOfStream
        {
            get
            {
                this.GetFileInfo();
                return this.m_fileInfo.dwStreams;
            }
        }
        public string FileType
        {
            get
            {
                this.GetFileInfo();
                return  this.m_fileInfo.szFileType;
            }
        }
        #region IDisposable Members
        public void Dispose()
        {
            this.Close();
        }
        #endregion
        /// <summary>
        /// represent the avi video stream class
        /// </summary>
        public class VideoStream : AVIFrameStream, IAVIGetFrame
        {
            private AVIStreamInfoStruct m_info;
            private AVIFile m_File;
            public int End {
                get
                {
                    return this.m_info.dwStart + m_info.dwLength;
                }
            }
            public override void Dispose()
            {
                int i = AVIApi.NOERROR;
                if (this.Handle != IntPtr.Zero)
                {
                    i = AVIApi.AVIStreamRelease(this.Handle);
                    if (i != AVIApi.NOERROR)
                    {
                        CoreLog.WriteDebug ("Stream Handle("+i+") still in use");                        
                    }
                    else
                    {
                        this.Handle = IntPtr.Zero;
                    }
                }
            }
            /// <summary>
            /// ctr. with video  stream
            /// </summary>
            /// <param name="videoStream"></param>
            /// <param name="info"></param>
            internal VideoStream(IntPtr videoStream, AVIFile mfile)
            {
                this.m_File = mfile ;
                this.Handle = videoStream;
                this.GetStreamInfo();
            }
            private VideoStream(IntPtr videoStream)
            {
                this.m_File = null;
                this.Handle = videoStream;
                this.GetStreamInfo();
            }
            public static VideoStream CreateFrom(IntPtr vidStream)
            {
                AVIStreamInfoStruct  v_s =  AVIStream.GetStreamInfo(vidStream);
                if (v_s.FCCTypeName == "vids")
                {
                    return new VideoStream(vidStream );
                }
                return null;
            }
            public long GetLongLength()
            {
                //long g = AVIApi.AVIStreamLength(this.Handle);
                int s = 0;
                int k = 0;
                long T = 0;
                int tt = 0;
                //for (int i = 0; i < this.Length; i++)
                //{
                //    tt = AVIApi.AVIStreamRead(this.Handle, i, 1, null, 0, ref s, ref k);
                //    T += s;
                //}
                tt = AVIApi.AVIStreamRead(this.Handle,0, 1, null, 0, ref s, ref k);
                if (tt == 0)
                {
                    T = s * this.Length;
                }
                return T;
            }
            /// <summary>
            /// get the number of frame in the video stream
            /// </summary>
            public int Frames
            {
                get
                {
                    return m_info.dwLength;
                }
            }
            public int BitCount {
                get {
                    return this.GetHeaderInfo ().biBitCount;
                }
            }
            public int Compression {
                get {
                    return this.GetHeaderInfo().biCompression;
                }
            }
            public string CompressionName
            {
                get
                {
                    if (Compression == 0)
                        return "NoCompression";
                    return NativeAPI.mmoiToString(this.Compression );
                }
            }
            /// <summary>
            /// get the start of the stream
            /// </summary>
            public int StreamStart { 
                get
                {
                    return AVIApi.AVIStreamStart(this.Handle);
                }
            }
            /// <summary>
            /// get the length of the stream
            /// </summary>
            public int Length {
                get { 
                    return (int)AVIApi.AVIStreamLength(this.Handle);
                }
            }
            public enuAviFileCap FileCap
            {
                get
                {
                    return (enuAviFileCap)this.m_info.dwFlags;
                }
            }
            /// <summary>
            /// get or set the rate of the document
            /// </summary>
            public int Rate {
                get {
                    return this.m_info.dwRate;
                }
            }
            public float FramePerSecond {
                get {
                    if (this.Scale !=0)
                    return this.Rate / this.Scale;
                    return -1;
                }
            }
            public int Width {
                get {
                    return GetHeaderInfo().biWidth;
                }
            }
            public int Height {
                get {
                    return GetHeaderInfo().biHeight;
                }
            }
            /// <summary>
            /// get the scale of the document
            /// </summary>
            public int Scale { 
                get{
                    return this.m_info.dwScale;
                }
            }
            public string DisplayName
            {
                get
                {
                    string str = "";
                    for (int i = 0; i < this.m_info.szName.Length; i++)
                    {
                        if (this.m_info.szName[i] != '\0')
                            str += this.m_info.szName[i];
                    }
                    return str;
                }
            }
            /// <summary>
            /// get entiere frame data
            /// </summary>
            /// <param name="frame"></param>
            /// <param name="d"></param>
            /// <param name="offset"></param>
            /// <param name="count"></param>
            private void GetFrameData(int frame, byte[] d, int offset, int count)
            {
                if (m_frameOpen == IntPtr.Zero)
                    return;
                IntPtr h = AVIApi.AVIStreamGetFrame(m_frameOpen, frame);
                BITMAPINFOHEADER bmp = (BITMAPINFOHEADER)Marshal.PtrToStructure(h, typeof(BITMAPINFOHEADER));
                IntPtr h2 = new IntPtr(h.ToInt32() + bmp.biSize);
                Marshal.Copy(h2, d, offset, count);
            }
            /// <summary>
            /// decompress the current video file and return a new AVI file
            /// </summary>
            /// <param name="vid">video to decompress</param>
            /// <param name="StartPos">start position</param>
            /// <param name="EndPos">end position</param>
            /// <returns>return new avi file or null if failed. try to used DecompressTo if you own the video driver</returns>
            public static AVIFile Decompress(VideoStream vid, int StartPos, int EndPos)
            {
                if ((vid == null)||(vid.Compression == 0))
                    return null;
                if (vid.BeginGetFrame() == false)
                {//can't decompress
                    return null;
                }
                AVIFile v_filec = null;
                AVIEditableStream edit = AVIEditableStream.CreateEmpty();
                AVIStreamInfoStruct  d = vid.GetStreamInfo ();
                int r = d.dwScale;
                d.dwScale = 1;
#pragma warning disable IDE0054 // Use compound assignment
                d.dwRate = (d.dwRate / r);
#pragma warning restore IDE0054 // Use compound assignment
                d.dwQuality = 0;
                d.dwLength = 0;
                edit.SetStreamInfo(d);
                try
                {
                    if (edit.Paste(vid, StartPos, EndPos))
                    {
                        IntPtr valloc = IntPtr.Zero;
                        enuAviError e = (enuAviError)AVIApi.AVIMakeFileFromStreams(ref valloc, 1, new IntPtr[] { edit.Handle });
                        v_filec = new AVIFile();
                        v_filec.m_pFile = valloc;
                        v_filec.GetFileInfo();
                        return v_filec;
                    }
                }
                catch
                {
                }
                finally
                {
                    edit.Dispose();
                    vid.EndGetFrame();
                }
                return null;
            }
            /// <summary>
            /// decompress the current file
            /// </summary>
            /// <param name="from"></param>
            /// <param name="to"></param>
            /// <returns></returns>
            public AVIFile Decompress(int from, int to)
            {
                return Decompress(this, from, to);
            }
            /// <summary>
            /// decrompress avi video file. XVID Cause some problem need to be used carefully
            /// </summary>
            /// <param name="filename">file name where to decompress</param>
            /// <param name="vid">video stream to decompress</param>
            /// <param name="StartPos">frame start position</param>
            /// <param name="EndPos">fream end position</param>
            /// <param name="maxlength">0 for non mutiparted </param>
            /// <returns>return a avi decompressed file or null. if not null yum mot cloase the handle to save</returns>
            public static AVIFile[]   DecompressTo(string filename, 
                VideoStream vid, 
                int StartPos, 
                int EndPos,
                long maxlength,
                AVISaveProgressionCallBack PROC)
            {
                long v_result = 0;
                int size = 0;                
                int frameLength = 1;
                int i = StartPos;
                int pos = 0;
                int v_samples = 0;
                int v_bytes = 0;
                long v_length =0;
                bool v_ismultiparted = maxlength > 0;
                bool v_start = true;
                List<AVIFile> v_files = new List<AVIFile>();
                int v_duration = EndPos - StartPos;
                string v_dir = PathUtils.GetDirectoryName(filename);
                string v_file = Path.GetFileNameWithoutExtension(filename);
                string v_ext = Path.GetExtension(filename);
                AVIFile v_filec = null;
                System.Windows.Forms.MethodInvoker v_dcreateNewFile =
                    delegate() {
                        v_filec = AVIFile.CreateFile(
                            v_ismultiparted ?
                            string.Format("{0}\\{1}_{2}{3}",v_dir , v_file, v_files.Count, v_ext) :
                            filename
                            );
                    };
                v_dcreateNewFile();
                if (v_filec == null)
                    return null;
                v_files.Add(v_filec);
                Bitmap bmp = null;
                AVIFile.VideoStream c = null;
                if (vid.BeginGetFrame()==false)
                {
                    return null;
                }
                if (vid.Compression != 0)
                {
                    //decompressed video file
                    while (i <= EndPos)
                    {
                        if (v_start)
                        {
                            bmp = vid.GetFrame(i);
                            if (bmp == null)
                                return null;
                            //24, 1, 0, bmp
                            c = v_filec.AddNewVideoStream(vid.FramePerSecond , 1 , 0, bmp);
                            v_start = false;                       
                        }
                        else
                        {
                            IntPtr h = AVIApi.AVIStreamGetFrame(vid.m_frameOpen, i);
                            if (h != IntPtr.Zero)
                            {
                                BITMAPINFOHEADER v_bmpHeader = (BITMAPINFOHEADER)Marshal.PtrToStructure(h, typeof(BITMAPINFOHEADER));
                                //  int offset = 0;
                                size = v_bmpHeader.biSizeImage;
                                if (size == 0)
                                    continue;
                                v_result = AVIApi.AVIStreamWrite(
                                          c.Handle,
                                          pos,
                                          frameLength,
                                          new IntPtr(h.ToInt32() + 40),
                                          size,
                                          enuAviWriteMode.KeyFrame ,
                                          ref v_samples,
                                          ref v_bytes);
                                if (v_length == 0)
                                    v_length = v_bytes * 2;
                                else 
                                    v_length += v_bytes;
                            }
                        }
                        i++;
                        pos++;
                        if (PROC != null) PROC((i - StartPos) / v_duration);
                        if (v_ismultiparted && (v_length > maxlength))
                        {
                            c.EndGetFrame();
                            c.Dispose();
                            v_filec.Close();
                            pos = 0;
                            v_start = true;
                            v_length = 0;
                            v_dcreateNewFile();                            
                            if (v_filec == null)
                                return null;
                            v_files.Add(v_filec);
                        }
                    }
                    if (!v_start)
                    {
                        c.EndGetFrame();
                        c.Dispose();
                    }
                }
                vid.EndGetFrame ();
                return v_files.ToArray ();
            }
            /// <summary>
            /// Decompress a video stream to file.
            /// </summary>
            /// <param name="filename">file name</param>
            /// <param name="vid">video stream</param>
            /// <param name="from">start sample</param>
            /// <param name="to">end sample</param>
            /// <returns>true if decompressed otherwise false</returns>
            public static bool  DecompressTo(
                   string filename,
                   VideoStream vid,
                   int from,
                   int to)
            {
                Bitmap bmp = null;                
                if ((vid==null) || (vid.Compression == 0) || (vid.BeginGetFrame() == false))
                {
                    return false;
                }
                int i = from ;
                AVIFile v_filec = AVIFile .Open (filename , enuAviAccess.Create );
                AVIFile.VideoStream c = null;
                int size = 0;
                long v_result = 0;
                bool v_start = true;
                int v_bytes = 0;
                int v_length = 0;
                int v_samples = 0;
                int pos = 1;
                int frameLength = 1;
                int v_vidLength = vid.Length;
                while ((i <= to)&&(i<= v_vidLength))
                    {
                        if (v_start)
                        {
                            bmp = vid.GetFrame(i);
                            if (bmp == null)
                                break;
                            //24, 1, 0, bmp
                            c = v_filec.AddNewVideoStream(vid.FramePerSecond, 1, 0, bmp);
                            v_start = false;
                        }
                        else
                        {
                            IntPtr h = AVIApi.AVIStreamGetFrame(vid.m_frameOpen, i);
                            if (h != IntPtr.Zero)
                            {
                                BITMAPINFOHEADER v_bmpHeader = (BITMAPINFOHEADER)Marshal.PtrToStructure(h, typeof(BITMAPINFOHEADER));
                                //  int offset = 0;
                                size = v_bmpHeader.biSizeImage;
                                if (size == 0)
                                    continue;
                                v_result = AVIApi.AVIStreamWrite(
                                          c.Handle,
                                          pos,
                                          frameLength,
                                          new IntPtr(h.ToInt32() + 40),
                                          size,
                                          enuAviWriteMode.KeyFrame,
                                          ref v_samples,
                                          ref v_bytes);
                                pos++;
                                if (v_length == 0)
                                    v_length = v_bytes * 2;
                                else
                                    v_length += v_bytes;
                            }
                        }
                        i++;
                    }
                    if (!v_start)
                    {
                        c.EndGetFrame();
                        c.Dispose();
                    }
                    v_filec.Dispose();
                vid.EndGetFrame();
                return true ;
            }
            public override void AddFrame(int pos, int frameLength, Bitmap bmp)
            {
                AddFrame(pos, frameLength, bmp, enuAviWriteMode.None);
            }
            /// <summary>
            /// Add a single bitmap Frame to an uncompressed video stream
            /// </summary>
            /// <param name="pos">position of the new frame</param>
            /// <param name="frameLength">Length of current frame</param>
            /// <param name="bmp">Bitmap to add</param>
            public   void AddFrame(int pos, int frameLength, Bitmap bmp, enuAviWriteMode mode )
            {
                int format = this.BitCount;
                Bitmap v_bmp = bmp.Clone() as Bitmap;
             {
                 if (this.Compression == 0)
                 {
                     switch (format)
                     {
                         case 24:
                             if (bmp.PixelFormat != PixelFormat.Format24bppRgb)
                             {
                                 //convert to format 
                                 Bitmap tbmp = new Bitmap(v_bmp.Width, v_bmp.Height, PixelFormat.Format24bppRgb);
                                 Graphics g = Graphics.FromImage(tbmp);                  
                                 g.Flush();
                                 v_bmp.Dispose();
                                 v_bmp = tbmp;
                             }
                             break;
                         default:
                             if (v_bmp.PixelFormat != PixelFormat.Format32bppArgb)
                             {
                                 //convert to int 32 byte
                                 Bitmap tbmp = new Bitmap(v_bmp.Width, v_bmp.Height, PixelFormat.Format32bppArgb);
                                 Graphics g = Graphics.FromImage(tbmp);
                                 g.Flush();
                                 v_bmp.Dispose();
                                 v_bmp = tbmp;
                             }
                             break;
                     }
                     
                 }
                 //rotatee image to render
                if (v_bmp.PixelFormat != PixelFormat.Undefined )
                  v_bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);

                BitmapData bmpDat = v_bmp.LockBits(
                    new Rectangle(
                    0, 0, v_bmp.Width, v_bmp.Height),
                    ImageLockMode.ReadOnly,v_bmp.PixelFormat);
                int size = bmpDat.Stride * bmpDat.Height;
                if (this.m_info.dwSuggestedBufferSize == 0)
                    this.m_info .dwSuggestedBufferSize  = size;
                int v_frames = 0;
                int v_bytes = 0;
                uint v_result = 0;
                v_result = AVIApi.AVIStreamWrite(
                    this.Handle,
                    pos,
                    frameLength,
                    bmpDat.Scan0,
                    size ,
                    mode,
                    ref v_frames,
                    ref v_bytes);
                if (v_result != 0)
                {
                    throw new Exception("Exception in VideoStreamWrite: " + ((enuAviError ) v_result).ToString());
                }
                v_bmp.UnlockBits(bmpDat);
            }
             v_bmp.Dispose();
                this.GetStreamInfo();  
            }
          
            public bool IsCompressed {
                get {
                    return this.Compression != 0;
                }
            }
            private static bool  SetEditableStreamInfo(IntPtr editablestream, AVIStreamInfoStruct info)
            {
                return (AVIApi.EditStreamSetInfo(editablestream, ref info, Marshal.SizeOf(info)) == 0);
            }
            private void CopyStream(IntPtr pvidstream, IntPtr source, int v_start, int v_pos)
            {
                int v_index = 0;
                int v_endindex = this.Frames;
                int v_rbytes = 0;
                int v_rsamples = 0;
                enuAviError i = 0;
                BITMAPINFOHEADER v_h1 = this.GetHeaderInfo();
                int v_bmpsize = (((((v_h1.biBitCount == 0) ? 32 : v_h1.biBitCount) * v_h1.biWidth + 31) & ~31) / 8) * v_h1.biHeight;           
                IntPtr allocOld = Marshal.AllocCoTaskMem(v_bmpsize);
                while ((AVIApi.AVIStreamRead(source, v_index, 1, allocOld, v_bmpsize, ref v_rbytes, ref v_rsamples) == 0)
        && (v_rbytes > 0))
                {
                    i = (enuAviError)AVIApi.AVIStreamWrite(pvidstream, v_index, 1, allocOld, v_rbytes,
                        enuAviWriteMode .None ,
                        0,
                        0);
                    v_index++;
                }
                Marshal.FreeCoTaskMem(allocOld);
            }
            public void Append(VideoStream videoStream)
            {
                Append(videoStream, enuAviWriteMode.None);
            }
            public void Append(VideoStream videoStream, enuAviWriteMode mode)
            {
                if (videoStream == null)
                    throw new ArgumentNullException("videoStream");
                BITMAPINFOHEADER v_h1 = this.GetHeaderInfo ();
                BITMAPINFOHEADER v_h2 =  videoStream.GetHeaderInfo ();
                if (!v_h1.Equals (v_h2))
                    throw new Exception("videoStream format does't match");
                int v_bmpsize = (((((v_h1.biBitCount == 0) ? 32 : v_h1.biBitCount) * v_h1.biWidth + 31) & ~31) / 8) * v_h1.biHeight;
                //maximun image size
                IntPtr allocOld = Marshal.AllocCoTaskMem(v_bmpsize );
                int v_index =0;
                int v_endindex = this.Frames;
                int v_rbytes = 0;
                int v_rsamples = 0;
                enuAviError i = 0;
                while ((AVIApi.AVIStreamRead(videoStream.Handle, v_index, 1, allocOld, v_bmpsize , ref v_rbytes, ref v_rsamples) == 0)
              && (v_rbytes > 0))
                {
                    i = (enuAviError)AVIApi.AVIStreamWrite(this.Handle, v_endindex + v_index, 1, allocOld, v_rbytes,
                        mode,
                        0,
                        0);
                    v_index++;
#if DEBUG
                    System.Diagnostics.Debug.WriteLine("Append Data "+v_index);
#endif
                }
                Marshal.FreeCoTaskMem(allocOld);
            }

            /// <summary>
            /// please close the stream after calling this method. because all reading writing process lead to a base image
            /// description.
            /// </summary>
            /// <param name="fileData"></param>
            public void WriteAllData(string fileData)
            {
                IntPtr allocOld = IntPtr.Zero;
                int v_rbytes = 0;
                int v_rsamples = 0;

                //int s = AVIStream.GetStreamInfo(this.Handle).dwSuggestedBufferSize;
   
                //int kr = AVIApi.AVIStreamRead(this.Handle, 0, 1, allocOld, s,
                //    ref v_rbytes, ref v_rsamples);
                //Marshal.FreeCoTaskMem(allocOld);

                //this.Save("", null);

              //  AVIApi.AVIStreamRead(this.Handle, v_index, 1, allocOld, v_bmpsize, ref v_rbytes, ref v_rsamples)
                BITMAPINFOHEADER v_h1 = this.GetHeaderInfo ();
                int v_bmpsize = (((((v_h1.biBitCount == 0) ? 32 : v_h1.biBitCount) * v_h1.biWidth + 31) & ~31) / 8) * v_h1.biHeight;
                AVIStreamInfoStruct v_streamInfo = AVIStream.GetStreamInfo(this.Handle);
                
                v_bmpsize = Math.Max(v_streamInfo.dwSuggestedBufferSize, v_bmpsize);
                int v_length = v_streamInfo.dwLength;

               allocOld = Marshal.AllocCoTaskMem(v_bmpsize );
                Byte[] v_data = new byte[v_bmpsize];

                int v_index =0;
                int v_endindex = this.Frames;
                //int v_rbytes = 0;
                //int v_rsamples = 0;
              
                BinaryWriter binw = new BinaryWriter (File.Open (fileData , FileMode.Append));
                int r = 0;
                while ( (v_index < v_length) && ( (r = AVIApi.AVIStreamRead(this.Handle, v_index, 1, allocOld, v_bmpsize, ref v_rbytes, ref v_rsamples)) == 0)
              && (v_rbytes > 0))
                {
                  
                    Marshal.Copy(allocOld, v_data, 0, v_rbytes);
                    binw.Write (v_data, 0, v_rbytes );
#if DEBUG
                    System.Diagnostics.Debug.WriteLine("WriteAllData()->" + v_rbytes);
#endif
                    v_index++;
                }
                Marshal.FreeCoTaskMem(allocOld);
                binw.BaseStream.SetLength(0);
                binw.Flush ();
                binw.Close ();
            }
          
            /// <summary>
            /// Save this stream to file.
            /// </summary>
            /// <param name="fileName">avi video files. </param>
            /// <param name="callback">call back progression. </param>
            /// <returns></returns>
            /// <remarks>
            /// For video file compression. Create a AVI Stream File and this video file to it
            /// </remarks>
            public bool Save(string fileName, AVISaveProgressionCallBack callback)
            {
                if (this.IsCompressed)
                {
                    //int s = AVIStream.GetStreamInfo(this.Handle).dwSuggestedBufferSize;
                    //IntPtr allocOld = Marshal.AllocCoTaskMem(s);
                    //int v_rbytes = 0;
                    //int v_rsamples = 0;
                    //int kr =  AVIApi.AVIStreamRead(this.Handle, 0, 1, allocOld, s, 
                    //    ref v_rbytes, ref v_rsamples);
                    //Marshal.FreeCoTaskMem(allocOld);


                    AVIFile f = AVIFile.CreateFile(fileName);
                    bool r = f.AddStream(this);
                    f.Close();
                    return r;
                }
                 
                return Save(IntPtr.Zero, fileName, false, enuDialogFlag.None, callback);
            }
            /// <summary>
            /// save this 
            /// </summary>
            /// <param name="hwnd"></param>
            /// <param name="fileName"></param>
            /// <param name="choosecodec"></param>
            /// <param name="flag"></param>
            /// <param name="callback"></param>
            /// <returns></returns>
            public bool Save(IntPtr hwnd , string fileName, bool choosecodec, enuDialogFlag flag, AVISaveProgressionCallBack callback)
            {
                AVIApi.AVICOMPRESSOPTIONS_CLASS opts =null;
                if (choosecodec)
                {
                    opts = AVIStreamUtils.ChooseVideoCodec(this.Handle, hwnd, flag);
                }
                else {
                    opts = new AVIApi.AVICOMPRESSOPTIONS_CLASS();
                    //maintain only the fccType if not compressed
                    opts.fccType = (uint)this.m_info.fccType;
                    if (this.IsCompressed)
                        opts.fccHandler = (uint)this.m_info.fccHandler;
                }
                if (opts == null)
                    return false;
                IntPtr d = this.Handle;
                AVIApi.AVICOMPRESSOPTIONS[] v_codecs = new AVIApi.AVICOMPRESSOPTIONS[] { opts.ToStruct() };
                enuAviError  i = (enuAviError)AVIApi.AVISaveV(fileName, 0, callback, 1, new IntPtr[]{ d},
                 ref v_codecs );                
                return (i == enuAviError.NoError);                
            }
            /// <summary>
            /// release the 
            /// </summary>
            public void Release()
            {
                AVIApi.AVIStreamRelease(this.Handle);
            }
            internal AVIStreamInfoStruct GetStreamInfo()
            {
                this.m_info =  GetStreamInfo(this.Handle);
                return this.m_info;
            }
            internal BITMAPINFOHEADER GetHeaderInfo()
            {
                BITMAPINFOHEADER t = new BITMAPINFOHEADER();
                IntPtr ptr = GetStreamFormat(this.Handle);
                if (ptr != IntPtr.Zero) { 
                t = (BITMAPINFOHEADER)Marshal.PtrToStructure(ptr, typeof(BITMAPINFOHEADER));
                Marshal.FreeCoTaskMem(ptr);
                
                }
                return t;
            }
              //public AviVideoStream Decompress()            
            //{
            //    if (this.Compression == AVIApi.streamtypeDIVX)
            //    { 
            //    }
            //    string ComTitle = AVIApi.mmoiToString (this.Compression);
            //    bool cr = BeginStreaming(0, 1000, 1000);
            //    System.Drawing.Bitmap bmp = this.GetFrame(0);
            //    EndStreaming();
            //    BeginGetFrame();
            //    EndGetFrame();
            //    AVIApi.AVICOMPRESSOPTIONS_CLASS c = ChooseCodec();
            //    //AVIApi.AVICOMPRESSOPTIONS c = ChooseCodec();
            //    return null;
            //}
            public static VideoStream Compress(IntPtr hwnd, VideoStream stream, bool chooseCodec, enuDialogFlag flag)
            {
                 AVIApi.AVICOMPRESSOPTIONS ao = new AVIApi.AVICOMPRESSOPTIONS ();
                if (chooseCodec)
                {
                    AVIApi.AVICOMPRESSOPTIONS_CLASS ct = AVIStreamUtils. ChooseVideoCodec(stream.Handle, hwnd, flag);
                    if (ct !=null)
                    ao = ct.ToStruct();
                    else 
                    return null;
                }
                IntPtr hCompress = IntPtr.Zero;
                int i = AVIApi.AVIMakeCompressedStream(out hCompress, stream.Handle, ref ao, 0);
               if (i == 0)
               {
                   //create a compresse stream
                   VideoStream vid = new VideoStream(hCompress,stream.m_File );
                   return vid;
               }
               return null;
            }
            /// <summary>
            /// compress the current video stream and return a new video stream
            /// </summary>
            /// <param name="opts"></param>
            /// <returns></returns>
            internal  VideoStream Compress(AVIApi.AVICOMPRESSOPTIONS_CLASS opts)
            {
                if (this.Compression != 0)
                    return null;
                IntPtr hCompress = IntPtr.Zero;
                AVIApi.AVICOMPRESSOPTIONS ao = opts.ToStruct();
                int i = AVIApi.AVIMakeCompressedStream(
                    out hCompress,
                    this.Handle , 
                    ref ao,0);
                if (i == 0)
                {
                    //create a compresse stream
                    VideoStream vid = new VideoStream(hCompress);//, stream.m_File);
                    return vid;
                }
                return null;
            }
            /// <summary>
            /// Begin streaming
            /// </summary>
            /// <param name="start">start</param>
            /// <param name="end">end</param>
            /// <param name="speed">1000 for normal speed</param>
            /// <returns></returns>
            public bool BeginStreaming(int start, int end, int speed)
            {
                IntPtr h = AVIApi.AVIStreamBeginStreaming(this.Handle, start, end, speed);
                return (h == IntPtr.Zero);
            }
            /// <summary>
            /// export video to another file
            /// </summary>
            /// <param name="hwnd">parent window for codec chooser</param>
            /// <param name="fileName">out file name</param>
            /// <returns></returns>
            public bool ExportTo(IntPtr hwnd, string fileName)
            {
                AVIApi.AVICOMPRESSOPTIONS_CLASS v_opts = new AVIApi.AVICOMPRESSOPTIONS_CLASS();
                v_opts.fccType = (uint)AVIApi.streamTypeVideo;
                //v_opts.lpParms = IntPtr.Zero;
                //v_opts.lpFormat = IntPtr.Zero;
                IntPtr aviStream = this.Handle;
                AVIApi.AVISaveOptions(hwnd,
                    AVIApi.ICMF_CHOOSE_KEYFRAME |
                    AVIApi .ICMF_CHOOSE_PREVIEW |
                    AVIApi.ICMF_CHOOSE_DATARATE,
                    1,
                    ref aviStream, 
                    ref v_opts);
                AVIApi.AVISaveOptionsFree(1, ref v_opts);
                AVIApi.AVICOMPRESSOPTIONS[] v_t = new AVIApi.AVICOMPRESSOPTIONS[] { v_opts.ToStruct() };
                int e = AVIApi.AVISaveV(fileName,
                    0, null,
                    1, new IntPtr []{ aviStream},
                    ref v_t );
                return (e == AVIApi.NOERROR);
            }
            //public bool BeginStreaming(int start, int Length)
            //{
            //    //1000 for normatl speed
            //   return (IntPtr .Zero == (enuAviError)AVIApi.AVIStreamBeginStreaming(this.Handle, start, Length, 1000));
            //}
            public void EndStreaming()
            {
                AVIApi.AVIStreamEndStreaming(this.Handle);
            }
            /// <summary>
            /// get video stream from handle
            /// </summary>
            /// <param name="v_vhedit"></param>
            /// <returns></returns>
            internal static VideoStream FromHandle(IntPtr  v_vhedit)
            {
                if (v_vhedit != IntPtr.Zero )
                {
                    AVIStreamInfoStruct v_info =  AVIStream.GetStreamInfo(v_vhedit);
                    if (v_info.fccHandler != 0)
                    {
                        VideoStream vid = new VideoStream(v_vhedit);
                        return vid;
                    }
                }
                return null;
            }
            public static bool SetStreamFormat(enuAVIVideoFormat pixformat, IntPtr v_videoStream, int width, int height)
            {
                int format = GetPixelFormat(pixformat);
                int frameSize = (((width * format) + 7) / 8) * height;//d.Stride * cbmp.Height;
                BITMAPINFOHEADER bi = new BITMAPINFOHEADER();
                bi.biSize = (short)Marshal.SizeOf(bi);
                bi.biWidth = width;
                bi.biHeight = height;
                bi.biPlanes = 1;
                bi.biBitCount = (byte)format;
                bi.biSizeImage = frameSize;
                bi.biClrUsed = 0;
                bi.biCompression = 0;
                int i = AVIApi.AVIStreamSetFormat(v_videoStream, 0, ref bi, bi.biSize);
                return (i == AVIApi.NOERROR);
            }

            /// <summary>
            /// compress the current video stream
            /// </summary>
            /// <param name="hwnd">owner window handle</param>
            /// <param name="tag">allowed flag</param>
            /// <param name="chooseCodec">choose codec</param>
            /// <returns>new video stream</returns>            
            public VideoStream Compress(IntPtr hwnd, 
                enuDialogFlag tag , 
                bool chooseCodec, AVIVideoCompressionOption compression=null)
            {
                AVIApi.AVICOMPRESSOPTIONS_CLASS ct = null;
                AVIApi.AVICOMPRESSOPTIONS ao = new AVIApi.AVICOMPRESSOPTIONS();
                if (chooseCodec)
                {
                    ct = AVIStreamUtils. ChooseVideoCodec(this.Handle, hwnd, tag);
                    if (ct!=null)
                        ao = ct.ToStruct();
                    else
                        return null;
                }
                IntPtr hCompress = IntPtr.Zero;
                if (compression != null)
                    compression.ao = ao;
                try
                {
                    int i = AVIApi.AVIMakeCompressedStream(out hCompress, 
                        this.Handle, ref ao, 0);
                    if (i == 0)
                    {
                        VideoStream vid = new VideoStream(hCompress);
                        return vid;
                    }
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
                return null;
            }
            /// <summary>
            /// Get a compression option from system
            /// </summary>
            /// <param name="hwnd">window handle</param>
            /// <param name="tag">flag allowed</param>
            /// <param name="compression">objet that will receive a compression tag</param>
            /// <returns></returns>
            public bool GetCompressionOption(IntPtr hwnd,enuDialogFlag tag , AVIVideoCompressionOption compression)
            {
                if (compression == null)
                    return false;
                AVIApi.AVICOMPRESSOPTIONS_CLASS ct = null;
                AVIApi.AVICOMPRESSOPTIONS ao = new AVIApi.AVICOMPRESSOPTIONS();
                
                    ct = AVIStreamUtils.ChooseVideoCodec(this.Handle, hwnd, tag);
                    if (ct != null)
                        ao = ct.ToStruct();
                    else
                        return false;
                    compression.ao = ao;
                    return true;
            }
            public VideoStream  Compress(AVIVideoCompressionOption option)
            {
                if (option==null)
                    return null;
                IntPtr hCompress = IntPtr.Zero;
                try
                {
                    var ao = option.ao;
                    int i = AVIApi.AVIMakeCompressedStream(
                        out hCompress, 
                        this.Handle, ref ao, 0);
                    if (i == 0)
                    {
                        VideoStream vid = new VideoStream(hCompress);
                        return vid;
                    }
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
                return null;
            }

        }
        /// <summary>
        /// represent thet audio strea from avi file
        /// </summary>
        public class AudioStream : AVIStream
        {
            private AVIStreamInfoStruct m_info;
            private long m_position = 0;
            private string m_displayName;
            public int End {
                get {
                    return this.m_info.dwStart + m_info.dwLength;
                }
            }
            public override void Dispose()
            {
                if (this.Handle != IntPtr.Zero)
                {
                    enuAviError  i = (enuAviError)AVIApi.AVIStreamRelease(this.Handle);
                    this.Handle = IntPtr.Zero;
                }
            }
            /// <summary>
            /// get the length of the audio in number of sample
            /// </summary>
            public int Length
            {
                get
                {
                    return m_info.dwLength;
                }
            }
            /// <summary>
            /// get the size of the audio file in octect
            /// </summary>
            public long Size {
                get {
                    return this.Length * SampleSize;
                }
            }
            /// <summary>
            /// get the type of the audio
            /// </summary>
            public int Type { 
                get{
                    return m_info.fccType;
                }
            }
            public string TypeName {
                get {
                    return NativeAPI.mmoiToString(this.Type );
                }
            }
            /// <summary>
            /// get the audio sample size
            /// </summary>
            public int SampleSize {
                get {
                    return m_info.dwSampleSize;
                }
            }
            internal AudioStream(IntPtr audioStream)
            {
                this.Handle = audioStream;
                this.GetStreamInfo();
                string str = "";
                long size = AVIApi.AVIStreamLength(audioStream);
                //write display name
                for (int i = 0; i < this.m_info.szName.Length; i++)
                {
                    if (this.m_info.szName[i] != '\0')
                        str += this.m_info.szName[i];
                    else
                        break;
                }
                this.m_displayName = str;
            }
            public enuAviFileCap FileCap
            {
                get
                {
                    return (enuAviFileCap)this.m_info.dwFlags;
                }
            }
            public string DisplayName
            {
                get
                {
                    return m_displayName;
                }
            }
            public long Position
            {
                get
                {
                    return this.m_position;
                }
            }

            /// <summary>
            /// la class de codec doit �tre enregistrer avant de pouvoir sauvegarder. le format
            /// </summary>
            /// <param name="filename"></param>
            /// <param name="chooseCodec"></param>
            /// <param name="progressCallBack"></param>
            /// <param name="hwnd"></param>
            /// <returns></returns>
            public bool Save(string filename, bool chooseCodec, AVISaveProgressionCallBack progressCallBack, IntPtr hwnd)
            {
                AVIApi.AVICOMPRESSOPTIONS_CLASS opts = null;
                if (chooseCodec)
                {
                    //AVIFile r =  AVIFile.CreateFile(CoreConstant.DEBUG_TEMP_FOLDER+"\\test.avi");
                    //var th = r.AddNewVideoStream(30, 1, 0, 300, 300);

                    //IntPtr aviStream = th.Handle;
                    opts = new AVIApi.AVICOMPRESSOPTIONS_CLASS();
                    //AVIApi.AVISaveOptions(hwnd,
                    //    AVIApi.ICMF_CHOOSE_KEYFRAME |
                    //    AVIApi.ICMF_CHOOSE_PREVIEW |
                    //    AVIApi.ICMF_CHOOSE_DATARATE,
                    //    1,
                    //    ref aviStream,
                    //    ref opts);

                    opts = this.ChooseCodec(hwnd );

                    //MPEGLAYER3WAVEFORMAT kformat = MPEGLAYER3WAVEFORMAT.GetFromHandle(opts.lpFormat);
                    //if (opts.cbFormat > 0)
                    //{
                    //    enuAviError ii = AVIApi.AVISaveV(filename, 0, progressCallBack, 1, new IntPtr[] { this.Handle },
                    //        opts.lpFormat);
                    //}
                }
                else
                {
                    opts = new AVIApi.AVICOMPRESSOPTIONS_CLASS();
                    opts.fccType = (UInt32)AVIApi.streamTypeAudio;
                    opts.fccHandler = (UInt32)AVIApi.streamtypeCAUD;
                }
                AVIApi.AVICOMPRESSOPTIONS[] v_t = new AVIApi.AVICOMPRESSOPTIONS[] { opts.ToStruct() };

               // MMIO.MMIOManager.InstallIoProc("mp3 ", BuildCall);

                enuAviError  i = (enuAviError)AVIApi.AVISaveV(filename,0 , progressCallBack, 1, new IntPtr[]{ this.Handle },
                  //
                  ref v_t );
                long h = AVIApi.AVISaveOptionsFree(1, ref opts);
                return (i == enuAviError.NoError);
            }
            /// <summary>
            /// internal chosse codec
            /// </summary>
            /// <returns></returns>
            internal AVIApi.AVICOMPRESSOPTIONS_CLASS ChooseCodec(IntPtr hwnd)
            {
                AVIApi.AVICOMPRESSOPTIONS_CLASS opts = new AVIApi.AVICOMPRESSOPTIONS_CLASS();
                IntPtr d = this.Handle;
                AVIApi.AVISaveOptions(hwnd ,
                    AVIApi.ICMF_CHOOSE_DATARATE |
                    AVIApi.ICMF_CHOOSE_PREVIEW, 1,
                    ref d,
                    ref opts);
                return opts;
            }
            /// <summary>Export the audio file to wav file format</summary>
            /// <param name="fileName">Name of the new file</param>
            /// <param name="progressCallBack">Progression Handler </param>
            /// <remarks>the filenmane must have extension .wav to match the requirement</remarks>
            public bool ExportToWaveFile(string fileName, AVISaveProgressionCallBack progressCallBack, bool chooseCodec, IntPtr hwnd)
            {
                AVIApi.AVICOMPRESSOPTIONS_CLASS opts = new AVIApi.AVICOMPRESSOPTIONS_CLASS();
                if (chooseCodec)
                {
                    opts = ChooseCodec(hwnd );
                    if (opts == null)
                        return false;
                }
                else
                {
                    opts.fccType = (UInt32)AVIApi.streamTypeAudio;
                    opts.fccHandler = (UInt32)AVIApi.streamtypeCAUD;
                    opts.dwKeyFrameEvery = 0;
                    opts.dwQuality = 0;
                    opts.dwFlags = 0;
                    opts.dwBytesPerSecond = 0;
#if !WINDOWS_64
                opts.lpFormat = new IntPtr(0);
                opts.lpParms = new IntPtr(0);
#endif
                    opts.cbFormat = 0;
                    opts.cbParams = 0;
                    opts.dwInterleaveEvery = 0;
                }
                AVIApi.AVICOMPRESSOPTIONS[] v_tc = new AVIApi.AVICOMPRESSOPTIONS[] { opts.ToStruct() };
                int i = AVIApi.NOERROR;
                try
                {
                    i = AVIApi.AVISaveV(fileName, 0, progressCallBack, 1, new IntPtr[] { this.Handle },
                        ref v_tc);
                }
                catch
                {
                    i = -1;
                }
                finally { 
                }
                return (i == AVIApi.NOERROR);
            }
            internal  AVIStreamInfoStruct GetStreamInfo()
            {
                this.m_info = GetStreamInfo(this.Handle);
                return m_info;
            }
            internal  WAVEFORMATEX GetFormat()
            {
                int v_formatSize = AVIStream.GetSizeOfFormat(this.Handle);
                WAVEFORMATEX v_f = new WAVEFORMATEX();                
                enuAviError error = (enuAviError)AVIApi.AVIStreamReadFormat(this.Handle, 0, ref v_f, ref v_formatSize);                
                return v_f;
            }
            /// <summary>
            /// append audio file
            /// </summary>
            /// <param name="vid"></param>
            public bool Append(AudioStream vid)
            {
                int fsize1 = this.GetFormatSize();
                int fsize2 = vid.GetFormatSize();
                WAVEFORMATEX v_t =  vid.GetFormat();
                WAVEFORMATEX v_i = this.GetFormat();
                if (!v_t.Equals (v_i))
                {
                    return false;
                }
                this.GetStreamInfo();
                long s = (int)GetStreamLength(vid.Handle);
                int v_size = vid.Length * vid.SampleSize ;
                IntPtr v_waveData = Marshal.AllocHGlobal (v_size);
                int v_nbytes = 0;
                int v_nsample = 0;
                enuAviError  i = (enuAviError)AVIApi.AVIStreamRead(vid.Handle, 0, vid.Length, v_waveData,
                    v_size,
                    ref v_nbytes,
                    ref v_nsample);
                i = (enuAviError)AVIApi.AVIStreamWrite(this.Handle,
                    this.End,
                    vid.Length ,
                    v_waveData,
                    v_nbytes,
                     enuAviWriteMode.None ,
                     0,
                     0);
                Marshal.FreeHGlobal(v_waveData);
                this.GetStreamInfo();
                return true;
            }
        }
       
        
        private void HandleError(enuAviError r,string method)
        {
            if (r == enuAviError.NoError)
                return;
            throw new Exception(string.Format("{0} : {1}", r, method));
        }
        private void GetFileInfo()
        {
            AVIApi.AVIFileInfo(this.m_pFile, 
                ref this.m_fileInfo,
                Marshal.SizeOf(this.m_fileInfo));
        }
        /// <summary>
        /// add a new video stream
        /// </summary>
        /// <param name="video"></param>
        /// <returns></returns>
        public bool AddVideoStream(VideoStream video)
        {
            return this.AddVideoStream (video, enuAviWriteMode.None);
        }
        /// <summary>
        /// add a avi stream to the file 
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public bool AddStream(AVIStream v1)
        {
            return AddStream(v1.Handle);
        }
        /// <summary>
        /// copy the avi stream
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public bool CopyStream(AVIStream  stream)
        {
            if (stream == null)
                return false;
            return CopyStream(stream.Handle);
        }
        /// <summary>
        /// copy stream
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        internal bool CopyStream(IntPtr stream)
        {
          IntPtr v_hStream = IntPtr.Zero;
          int v_formatSize = 0;
          enuAviError i = enuAviError.NoError;
          int v_rbytes = 0;
          int v_rsamples = 0;
          int v_index = 0;
            //check read format
          AVIApi.AVIStreamReadFormat(stream, 0, IntPtr.Zero, ref v_formatSize);
          if (v_formatSize == 0)
              return false;
            //check if can read
          i = (enuAviError)AVIApi.AVIStreamRead(stream,
              v_index, -1, IntPtr .Zero , 0 , 
              ref v_rbytes, 
              ref v_rsamples);
          if (i != enuAviError.NoError)
              return false;
          IntPtr v_hformat = Marshal.AllocCoTaskMem(v_formatSize);
            //read format
          AVIApi.AVIStreamReadFormat(stream, 0, v_hformat, ref v_formatSize);  
          AVIStreamInfoStruct v_streamInfo = new AVIStreamInfoStruct();
          //get stream info struct
          AVIApi.AVIStreamInfo(stream, ref v_streamInfo, Marshal.SizeOf(v_streamInfo));
          v_streamInfo.dwFlags =                
              (short)(enuAviFileCap.CanRead | enuAviFileCap.CanWrite);
          i = (enuAviError)AVIApi.AVIFileCreateStream(this.m_pFile, out v_hStream, ref v_streamInfo);
          if (i != enuAviError.NoError)
          {
              Marshal.FreeCoTaskMem(v_hformat);
              return false;
          }
          i = (enuAviError)AVIApi.AVIStreamSetFormat(v_hStream, 0, v_hformat, v_formatSize);
          Marshal.FreeCoTaskMem(v_hformat);
          //create new stream 
          //read all data 
          IntPtr allocOld = Marshal.AllocCoTaskMem(int.MaxValue  );
          //AVIStream.AviReadInfo(stream, 0, -1, ref v_rbytes, ref v_rsamples);
          IntPtr rh = AVIApi.AVIStreamBeginStreaming(stream, 0, -1, 1000);
          while ((AVIApi.AVIStreamRead(stream,
              v_index, -1, allocOld, int.MaxValue  , 
              ref v_rbytes, 
              ref v_rsamples) == 0)
              && (v_rbytes > 0))
          {
              i = (enuAviError)
                  AVIApi.AVIStreamWrite(
                  v_hStream, 
                  v_index,
                  v_rsamples, 
                  allocOld,
                  v_rbytes,
                    enuAviWriteMode.KeyFrame ,
                    0,
                    0);
              v_index+=v_rsamples;
          }
          rh = AVIApi.AVIStreamEndStreaming(stream);
          AVIApi.AVIStreamRelease(v_hStream);
          Marshal.FreeCoTaskMem(allocOld);
          return (v_index > 0);
        }
        /// <summary>
        /// copy the video stream to this element
        /// </summary>
        /// <param name="v1"></param>
        public bool AddVideoStream(VideoStream v1,  enuAviWriteMode mode)
        {
            //if (v1.Compression != 0)
            //{
            //    this.AddVideoStream(1000, 1, 0, enuAVIVideoFormat.Format32, v1.Width, v1.Height);
            //    return false;
            //}
            BITMAPINFOHEADER bi = new BITMAPINFOHEADER ();
            int size = v1.GetFormatSize();
            if (size > Marshal.SizeOf(bi))
                return false;
            size = Marshal.SizeOf(bi);
            AVIApi.AVIStreamReadFormat(v1.Handle , 0, ref bi,ref  size); // Read format 
            //if (bi.biCompression !=AVIApi.BI_RGB) // Wrong compression format? 
            //    return;
            AVIStreamInfoStruct v_streamInfo = new AVIStreamInfoStruct ();
            IntPtr videoStream = IntPtr.Zero;
            //get stream info struct
            AVIApi.AVIStreamInfo(v1.Handle, ref v_streamInfo, Marshal.SizeOf (v_streamInfo));
            //create new stream 
            enuAviError i = (enuAviError)AVIApi.AVIFileCreateStream(this.m_pFile, out videoStream, ref v_streamInfo);
            if (videoStream == IntPtr.Zero)
                return false;
            //set stream format
            i = (enuAviError)AVIApi.AVIStreamSetFormat(videoStream, 0, ref bi, Marshal.SizeOf(bi));
           bi.biSizeImage = (((((bi.biBitCount == 0) ? 32 : bi.biBitCount) * bi.biWidth + 31) & ~31) / 8) * bi.biHeight;
            //maximun image size
           IntPtr allocOld = Marshal.AllocCoTaskMem(bi.biSizeImage);
            //read all data 
            int v_rbytes = 0;
            int v_rsamples = 0;
            int v_index = 0;
            while ((AVIApi.AVIStreamRead(v1.Handle, v_index, 1, allocOld, bi.biSizeImage , ref v_rbytes, ref v_rsamples) == 0)
                && (v_rbytes > 0))
            {
                i = (enuAviError)AVIApi.AVIStreamWrite(videoStream, v_index, 1, allocOld, v_rbytes,
                    mode,
                    0,
                    0);
                v_index++;
            }
            AVIApi.AVIStreamRelease(videoStream);
            Marshal.FreeCoTaskMem(allocOld);
            return true;
        }
        /// <summary>
        /// copy the video stream to this element
        /// </summary>
        /// <param name="v1"></param>
        public bool AddReverseVideoStream(VideoStream v1, enuAviWriteMode mode)
        {
            BITMAPINFOHEADER bi = new BITMAPINFOHEADER();
            int size = v1.GetFormatSize();
            if (size > Marshal.SizeOf(bi))
                return false;
            size = Marshal.SizeOf(bi);
            AVIApi.AVIStreamReadFormat(v1.Handle, 0, ref bi, ref  size); // Read format 
            AVIStreamInfoStruct v_streamInfo = new AVIStreamInfoStruct();
            IntPtr videoStream = IntPtr.Zero;
            //get stream info struct
            AVIApi.AVIStreamInfo(v1.Handle, ref v_streamInfo, Marshal.SizeOf(v_streamInfo));
            //create new stream 
            enuAviError i = (enuAviError)AVIApi.AVIFileCreateStream(this.m_pFile, out videoStream, ref v_streamInfo);
            //set stream format
            i = (enuAviError)AVIApi.AVIStreamSetFormat(videoStream, 0, ref bi, Marshal.SizeOf(bi));
            bi.biSizeImage = (((((bi.biBitCount == 0) ? 32 : bi.biBitCount) * bi.biWidth + 31) & ~31) / 8) * bi.biHeight;
            //maximun image size
            IntPtr allocOld = Marshal.AllocCoTaskMem(bi.biSizeImage);
            //read all data 
            int v_rbytes = 0;
            int v_rsamples = 0;
            int v_index = 0;
            int v_leng = v1.Frames-1;
            while ((AVIApi.AVIStreamRead(v1.Handle,v_leng - v_index, 1, allocOld, bi.biSizeImage, ref v_rbytes, ref v_rsamples) == 0)
                && (v_rbytes > 0))
            {
                AVIApi.AVIStreamWrite(videoStream, v_index, 1, allocOld, v_rbytes,
                    mode,
                    0,
                    0);
                v_index++;
            }
            AVIApi.AVIStreamRelease(videoStream);
            Marshal.FreeCoTaskMem(allocOld);
            return true;
        }
        public VideoStream AddNewCompressedVideoStream(
                 int framerate,
                 int scale,
                 int compression,
                 int width,
                 int height,
             bool chooseCodec
            )
        {
            AVIStreamInfoStruct info = AVIFile.GetUncompressedVideoInfo(
        enuAVIVideoFormat.Format32 ,
        scale, (int)framerate, width, height);//
            IntPtr v_videoStream = IntPtr.Zero;
            IntPtr v_compStream = IntPtr.Zero;
            enuAviError i = (enuAviError)AVIApi.AVIFileCreateStream(this.m_pFile, out v_videoStream, ref info);
            if (i != (int)enuAviError.NoError)
                return null;
           AVIApi.AVICOMPRESSOPTIONS ao = new AVIApi.AVICOMPRESSOPTIONS ();
            if (chooseCodec)
            {
                if (!AVIStreamUtils.ChooseCodec(v_videoStream,IntPtr.Zero, enuDialogFlag.All, ref ao))
                    return null;
            }
            else{
            }
            IntPtr hCompress = IntPtr.Zero;
            try
            {
                int c = AVIApi.AVIMakeCompressedStream(out hCompress, v_videoStream, ref ao, 0);
                if (c == 0)
                {
                    //create a compresse stream
                    VideoStream vid = new VideoStream(hCompress, this);
                    return vid;
                }
            }
            catch { 
            }
           return null;
            //AVIFile.VideoStream.Compress 
            //AVIApi .AVIMakeCompressedStream (out v_compStream, v_videoStream , 
            //return null;
        }
        public VideoStream AddNewVideoStream(
            float framerate,
            int scale,
            int compression,
            int width, int height)
        {
              VideoStream v =null;
            using (Bitmap bmp = new Bitmap(width, height))
            {
              v = AddNewVideoStream(framerate, scale, compression, bmp);
            }
            return v;
        }
        /// <summary>
        /// Create and add new video stream to the video file
        /// </summary>
        /// <param name="framerate">Framerate of your video file. Number of frame per second.default must ve 24 fps</param>
        /// <param name="scale">scale of your video. affect the framerate . default must be 1</param>
        /// <param name="compression"></param>
        /// <param name="cbmp"></param>
        /// <returns></returns>
        public VideoStream AddNewVideoStream(
            float framerate,
            int scale,
            int compression,
            Bitmap cbmp)
        {
            enuAVIVideoFormat v_format =  (enuAVIVideoFormat)cbmp.PixelFormat;
            int format = GetPixelFormat(v_format);
            int frameSize = (((cbmp.Width  * format) + 7) / 8) *cbmp.Height ;//d.Stride * cbmp.Height;
 
            AVIStreamInfoStruct info = AVIFile.GetUncompressedVideoInfo (
                (enuAVIVideoFormat)cbmp.PixelFormat,
                scale,(int)framerate , cbmp.Width ,cbmp .Height );
            IntPtr v_videoStream = IntPtr.Zero;
            enuAviError i = (enuAviError)AVIApi.AVIFileCreateStream(this.m_pFile, out v_videoStream, ref info);

            if (i != (int)enuAviError.NoError)
            {
                System.Diagnostics.Debug.WriteLine("AVIFILE: Can't create video stream : " + i);
                return null;
            }
            //set stream info 
            if (!VideoStream.SetStreamFormat(v_format, 
                v_videoStream,cbmp.Width, cbmp.Height))
                return null;
            VideoStream vid = new VideoStream(v_videoStream,this);            
            vid.BeginGetFrame();           
            vid.AddFrame(0,1, cbmp);
            vid.EndGetFrame();
            return vid;
        }

        /// <summary>
        /// add an empty video video stream
        /// </summary>
        /// <param name="framerate">frame rate</param>
        /// <param name="scale">scale factor . 1</param>
        /// <param name="compression">no compression = 0</param>
        /// <param name="videoFormat">video format</param>
        /// <param name="width">width</param>
        /// <param name="height">height</param>
        /// <returns></returns>
        public VideoStream AddNewVideoStream(
           float framerate,
           int scale,
           int compression,
           enuAVIVideoFormat videoFormat,
            int width,
            int height)
        {
            int format = GetPixelFormat(videoFormat);
            int frameSize = (((width * format) + 7) / 8) * height;//d.Stride * height;

            AVIStreamInfoStruct info = AVIFile.GetUncompressedVideoInfo(
                videoFormat,
                scale, (int)framerate, width, height);
            IntPtr v_videoStream = IntPtr.Zero;
            enuAviError i = (enuAviError)AVIApi.AVIFileCreateStream(this.m_pFile, out v_videoStream, ref info);

            if (i != (int)enuAviError.NoError)
            {
                System.Diagnostics.Debug.WriteLine("AVIFILE: Can't create video stream : " + i);
                return null;
            }
            //set stream info 
            if (!VideoStream.SetStreamFormat(videoFormat,
                v_videoStream, width, height))
                return null;
            VideoStream vid = new VideoStream(v_videoStream, this);
            return vid;
        }
        internal IntPtr AddStream(AVIStreamInfoStruct streamInfo)
        {
            IntPtr v_hStream = IntPtr.Zero;
            enuAviError i = (enuAviError)AVIApi.AVIFileCreateStream(this.m_pFile, 
                out v_hStream, 
                ref streamInfo);
            return v_hStream;
        }
        public AVIFile.VideoStream  AddVideoStream(int framerate, int scale, 
            int compression,
            enuAVIVideoFormat pixformat,
            int width, int height)
        {
            int format = GetPixelFormat(pixformat);
            int frameSize = (((width * format ) +7)/8)  * height ;
            AVIStreamInfoStruct info = AVIFile.GetUncompressedVideoInfo(
                pixformat ,
                scale , framerate, width,height 
                );
            IntPtr v_videoStream = IntPtr.Zero;
            enuAviError i = (enuAviError)AVIApi.AVIFileCreateStream(this.m_pFile, out v_videoStream, ref info);
            if (i != (int)enuAviError.NoError)
                return null;
            //set stream info 
            if (VideoStream.SetStreamFormat(pixformat, v_videoStream, width, height) == false)
            {
                return null;
            }
            VideoStream vid = new VideoStream(v_videoStream, this);
            vid.AddFrame(0, 1, new Bitmap(width, height));
            return vid;
        }
        /// <summary>
        /// get the uncompresse video stream info
        /// </summary>
        /// <param name="scale">video scale</param>
        /// <param name="framerate">frame rate</param>
        /// <param name="width">the width</param>
        /// <param name="height">the height</param>
        /// <returns></returns>
        internal static AVIStreamInfoStruct GetUncompressedVideoInfo(
            enuAVIVideoFormat pixformat,
            int scale, int framerate, int width, int height)
        {
            int format = GetPixelFormat(pixformat);
            int frameSize = (((width * format) + 7) / 8) * height;
            AVIStreamInfoStruct info = 
            new AVIStreamInfoStruct();
            info.fccType = AVIApi.streamTypeVideo;// Avi.mmioStringToFOURCC("vids", 0);
            info.fccHandler = AVIApi.streamtypeCVID;// Avi.mmioStringToFOURCC("CVID", 0);
            info.dwFlags = 0;
            info.dwCaps = 0;
            info.wPriority = 0;
            info.wLanguage = 0;
            info.dwScale = (int)scale;
            info.dwRate = (int)framerate; // Frames per Second
            info.dwStart = 0;
            info.dwLength = 0;
            info.dwInitialFrames = 0;
            info.dwSuggestedBufferSize = frameSize; //height_ * stride_;
            info.dwQuality = -1;        //default
            info.dwSampleSize = 0;
            info.rcFrame = new Rectangle(Point.Empty, new Size(width, height));
            info.dwEditCount = 0;
            info.dwFormatChangeCount = 0;
            //info.szName = new StringBuilder (64).ToString();
            return info ;
        }
        public static int GetPixelFormat(enuAVIVideoFormat pixelFormat)
        {
            int i = 0;
            string str = pixelFormat.ToString();
            if (str.StartsWith("Format"))
            {
                string v = str.Substring(6, 2);
                if (char.IsNumber(v[1]))
                    i = Int32.Parse(v);
                else
                    i = Int32.Parse(v[0].ToString());
            }
            return i;
        }
        /// <summary>
        /// Add New Empty stream 
        /// </summary>
        /// <param name="streamInfo"></param>
        /// <param name="format"></param>
        /// <param name="sample"></param>
        /// <returns></returns>
        internal AudioStream  AddAudioStream(
            AVIStreamInfoStruct streamInfo,
            WAVEFORMATEX format,
            int sample)
        {
            AudioStream stream = CreateAudio(streamInfo, format);
            int v_len = stream.SampleSize * sample;
            byte[] tab = new byte [v_len];
            IntPtr v_data = Marshal.AllocHGlobal(v_len);
            //copy silence
            Marshal.Copy(tab, 0, v_data, tab.Length);
            enuAviError i = (enuAviError)AVIApi.AVIStreamWrite(stream.Handle,
                stream.End, 
                sample,
                v_data,
                v_len,
                 enuAviWriteMode.None,
                 0,
                 0);
            Marshal.FreeHGlobal(v_data);
            stream.GetStreamInfo();
            return stream;
        }

        //don't work well. because copying a of a suite.
        //marked obselete
        //public bool AddAudioStream(AudioStream audioStream)
        //{
        //    AudioStream s = this.CreateAudio(audioStream.GetStreamInfo(),
        //        audioStream.GetFormat());
        //    if (s != null)
        //    {
        //        //copy data
        //        int v_size = audioStream.Length * audioStream.SampleSize;
        //        IntPtr v_alloc = IntPtr.Zero;
        //        int bytes = 0;
        //        int samples = 0;
        //        enuAviError r = enuAviError.NoError;
        //        enuAviWriteMode v_writeMode = enuAviWriteMode.None;
        //        int start = 0;
        //        if (v_size < 4096)
        //        {
        //            v_alloc = Marshal.AllocCoTaskMem(v_size);
        //            r = AVIApi.AVIStreamRead(audioStream.Handle, start, audioStream.Length, v_alloc, v_size, ref bytes, ref samples);
        //            HandleError(r, "AVIStreamRead");
        //            r = (enuAviError)AVIApi.AVIStreamWrite(s.Handle, start, samples, v_alloc, bytes, enuAviWriteMode.None,
        //                0, 0);
        //            HandleError(r, "AVIStreamWrite");
        //        }
        //        else
        //        {
        //            int bufferSize = v_size;
        //            v_alloc = Marshal.AllocCoTaskMem(bufferSize);
        //            int maxsample = audioStream.Length / audioStream.SampleSize;
        //            while ((v_size > 0) && ((maxsample - start) > 0))
        //            {
        //                r = AVIApi.AVIStreamRead(
        //                    audioStream.Handle,
        //                    start,
        //                    Math.Min(maxsample - start, 4096),//audioStream.SampleSize , 
        //                    v_alloc, bufferSize,
        //                    ref bytes,
        //                    ref samples);
        //                if (r == enuAviError.FileRead)
        //                    break;
        //                HandleError(r, "AVIStreamRead");
        //                r = (enuAviError)AVIApi.AVIStreamWrite(s.Handle, start,
        //                    samples,
        //                    v_alloc,
        //                    bytes,
        //                    v_writeMode,
        //                    0, 0);
        //                HandleError(r, "AVIStreamWrite");
        //                v_size -= bytes;// bufferSize;
        //                start += samples;
        //                if (v_writeMode == enuAviWriteMode.None)
        //                {
        //                    v_writeMode = enuAviWriteMode.KeyFrame;
        //                }
        //            }
        //        }
        //        Marshal.FreeCoTaskMem(v_alloc);
        //        s.Dispose();
        //        return true;
        //    }
        //    return false;
        //}
        
     
        
        private AudioStream CreateAudio(AVIStreamInfoStruct streamInfo,
            WAVEFORMATEX format)
        {
            IntPtr v_aud = IntPtr.Zero;
            AVIStreamInfoStruct info = streamInfo;
            info.fccType = AVIApi.streamTypeAudio;
            info.dwLength = 0;
            enuAviError result = (enuAviError)AVIApi.AVIFileCreateStream(this.Handle, out v_aud, ref info);
            if (result != enuAviError.NoError)
                return null;
            //set the format
            AVIApi.AVIStreamSetFormat(
                v_aud,
                0,
                ref format,
                Marshal.SizeOf(format));
            AudioStream stream = new AudioStream(v_aud);
            return stream;
        }
        /// <summary>
        /// add audio thream form file
        /// </summary>
        /// <param name="p"></param>
        public bool AddAudioStream(string filename)
        {
            if (!System.IO.File.Exists (filename))
                return false;
            AVIFile f = AVIFile.Open(filename);
            if (f == null)
                return false;
            AVIFile.AudioStream aud = f.GetAudioStream();
            if (aud == null)
            {
                f.Close();
                return false;
            }
            this.AddStream(aud);
            aud.Dispose();
            f.Close();
            return false;
        }
        /// <summary>
        /// add a empty wav track
        /// </summary>
        public bool AddEmptyWave()
        {
            AVIStreamInfoStruct info = new AVIStreamInfoStruct();
            info.fccType = AVIApi.streamTypeAudio;
            info.fccHandler = 0;
            info.dwRate = 44100*4;
            info.dwQuality = 0;
            info.dwScale = 4;
            info.dwSampleSize = 4;
            IntPtr hppavi = IntPtr.Zero;
            enuAviError i = (enuAviError)AVIApi.AVIFileCreateStream(this.Handle,
                out hppavi,
                ref info);
            if (hppavi == IntPtr.Zero )
                return false;
            AudioStream au=new AudioStream (hppavi);
            WAVEFORMATEX format =  au.GetFormat ();
            format.cbSize = 0;// (short)Marshal.SizeOf(format);
            format.wFormatTag = AVIApi.WAVE_FORMAT_PCM ;
            format.nBlockAlign = 4;
            format.nSamplesPerSec = 44100;
            format.nAvgBytesPerSec = 44100 * 4;
            format.nChannels = 2;
            format.wBitsPerSample = 16;
            i = (enuAviError)AVIApi.AVIStreamSetFormat(hppavi, 0, ref format, Marshal.SizeOf(format));
            au.Dispose();
            return true;
        }
        public static AVIFile CreateFileFromStreams(string filename, AVIFrameStream[] streams)
        {
            AVIFile f = AVIFile.CreateFile(filename);
            //IntPtr[] intPtr = new IntPtr[stream.Length];
            for (int i = 0; i < streams.Length; i++)
            {
                f.AddStream(streams[i]);
            }
            return f;
        }
        public static AVIFile CreateFileFromStreams(params AVIFrameStream[] stream)
        {
            IntPtr[] intPtr = new IntPtr[stream.Length];
            for (int i = 0; i < intPtr.Length; i++)
            {
                intPtr[i] = stream[i].Handle;
            }
            return CreateFileFromStreams(intPtr);
        }
        public static AVIFile CreateFileFromStreams(params IntPtr[] streams)
        {
            IntPtr h = IntPtr.Zero ;
            try
            {
                enuAviError i = enuAviError.NoError;
                    if (streams == null)
                        i = (enuAviError)AVIApi.AVIMakeFileFromStreams(ref h, 0, streams);
                    else
                        i = (enuAviError)AVIApi.AVIMakeFileFromStreams(ref h, streams.Length, streams);
                if (i == enuAviError.NoError)
                {
                    AVIFile v_out = new AVIFile();
                    v_out.m_pFile = h;
                    v_out.GetFileInfo();
                    return v_out;
                }
            }
            catch { 
            }
            return null;
        }
        public static void ExtrackWaveAudio(string sourcefile, string destination, AVISaveProgressionCallBack callback)
        {
            AVIFile f = AVIFile.Open(sourcefile);
            if (f == null)
                return;
            AVIFile.AudioStream  a = f.GetAudioStream();
            if (a != null)
            {
                a.ExportToWaveFile(destination, callback, false , IntPtr.Zero  );
                a.Dispose();
            }
            f.Close();
            f.Dispose();
        }
        public static void PutToClipboard(AVIFile f)
        {
          int i =  (int)AVIApi.AVIPutFileOnClipboard(f.Handle);
        }
        public static void ClearClipboar()
        {
            AVIApi.AVIClearClipboard();
        }
        public static AVIFile GetFromClipboard()
        {
            IntPtr v_h = IntPtr.Zero;
            int i = (int)AVIApi.AVIGetFromClipboard(ref v_h);
            if (i == 0)
            {
                AVIFile f = new AVIFile();
                f.m_pFile  = v_h;
                f.GetFileInfo();
                return f;
            }
            return null;
        }
        /// <summary>
        /// add data stream
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool AddAudioStream(IWAVEFORMAT waveformat, byte[] data)
        {
            IntPtr alloc = Marshal.AllocCoTaskMem(Marshal.SizeOf (waveformat));
            Marshal.StructureToPtr(waveformat, alloc, true);
            WAVEFORMATEX format = new WAVEFORMATEX ();
            format = (WAVEFORMATEX)Marshal.PtrToStructure(alloc, format.GetType());
                Marshal.FreeCoTaskMem (alloc );
                alloc = IntPtr.Zero ;
            format.cbSize = (short)Marshal.SizeOf(format);
           //format.wFormatTag = AVIApi.WAVE_FORMAT_PCM;
            //format.nChannels = 2;
            //format.nBlockAlign = 1;
            //format.nSamplesPerSec = 0;
            IntPtr v_aud = IntPtr.Zero;
            AVIStreamInfoStruct info = new AVIStreamInfoStruct ();
            info.fccType = AVIApi.streamTypeAudio;
            //info.dwLength = 50000;
            info.fccHandler = 0;
            info.dwRate = format.nSamplesPerSec ;//. 48000;// 44100 * 4;
            info.dwQuality = -1;
            info.dwScale = format.nBlockAlign;// 1;// 1152;//1
            info.dwSampleSize = format.nBlockAlign  ;// 1152;//4;
            info.dwInitialFrames = 1;
            info.dwLength  = 0;
            info.dwSuggestedBufferSize = 0;//
            enuAviError result = (enuAviError)AVIApi.AVIFileCreateStream(this.Handle, out v_aud, ref info);
            if (result != enuAviError.NoError)
                return false;
            //set the format
            enuAviError frm = (enuAviError)AVIApi.AVIStreamSetFormat(v_aud, 0, ref format, Marshal.SizeOf(format));
            //get an audio stream and write data to it
            AudioStream stream = new AudioStream(v_aud);
            bool v_wdata = AVIStream.WriteData(stream, enuAviWriteMode.List , 233032, data);
            stream.Dispose();
            return v_wdata ;            
        }
        /// <summary>
        /// add audio info
        /// </summary>
        /// <param name="aVIStreamInfoStruct"></param>
        /// <param name="data"></param>
        internal bool AddAudioStream(IWAVEFORMAT waveformat, AVIStreamInfoStruct streamInfo, byte[] data)
        {
            IntPtr alloc = Marshal.AllocCoTaskMem(Marshal.SizeOf(waveformat));
            Marshal.StructureToPtr(waveformat, alloc, true);
            WAVEFORMATEX format = new WAVEFORMATEX();
            format = (WAVEFORMATEX)Marshal.PtrToStructure(alloc, format.GetType());
            //free allocated memory
            Marshal.FreeCoTaskMem(alloc);
            alloc = IntPtr.Zero;
            IntPtr v_aud = IntPtr.Zero;
            AVIStreamInfoStruct v_info = streamInfo ;
            int length = v_info.dwLength;
            v_info.szName = null;// IntPtr.Zero;//clear the name
            v_info.dwLength = 0;
            enuAviError result = (enuAviError)AVIApi.AVIFileCreateStream(this.Handle, out v_aud, ref v_info);
            if (result != enuAviError.NoError)
                return false;
            //set the format
            result = (enuAviError)AVIApi.AVIStreamSetFormat(v_aud, 0, ref format, format.cbSize);// Marshal.SizeOf(format));
           if (result != enuAviError.NoError)
               return false;
            //get an audio stream and write data to it
           using (AudioStream stream = new AudioStream(v_aud))
           {
               if (stream != null)
               {
                   bool v_wdata = AVIStream.WriteData(stream, enuAviWriteMode.KeyFrame , length, data);
                   return v_wdata;
               }
           }
           return false;
        }
        /// <summary>
        /// add audio info
        /// </summary>
        /// <param name="aVIStreamInfoStruct"></param>
        /// <param name="data"></param>
        internal bool AddAudioStream(IntPtr waveformat, 
            int formatSize, 
            AVIStreamInfoStruct streamInfo, 
            byte[] data,
            MPEGLAYER3WAVEFORMAT[] v_formats)
        {
            AVIFile.AudioStream audStream =null;
            if (AddAudioStream(out audStream, waveformat, formatSize, streamInfo) == false)
                return false;
            bool v_wdata = false;
            using (AudioStream stream = audStream)
            {
                Byte[] v_cdata = new Byte[v_formats[0].nBlockSize];
                int v_offset = 0;
                for (int i = 0; i < v_formats .Length ; i++)
                {
                    if ((v_offset + v_cdata.Length) < data.Length)
                    {
                        Array.Copy(data, v_offset, v_cdata, 0, v_cdata.Length);
                        v_wdata = AVIStream.WriteData(stream, enuAviWriteMode.None, 1, v_cdata);
                        //v_result = file.AddAudioStream(
                        //v_streamformat,
                        //v_formatSize,
                        //v_streamInfo,
                        //v_cdata,
                        //v_formats);
                    }
                    else
                    {
                        break;
                    }
                    v_offset += v_cdata.Length;
                }
                //v_wdata = AVIStream.WriteData(stream, enuAviWriteMode.None, length , data);
            }
            return v_wdata;
        }
        internal bool AddAudioStream(out AVIFile.AudioStream aud, IntPtr waveformat, int formatSize, AVIStreamInfoStruct streamInfo)
        {
            aud = null;
            IntPtr v_haud = IntPtr.Zero;
            AVIStreamInfoStruct v_info = streamInfo;
            int length = v_info.dwLength;
            v_info.dwLength = 0;
            v_info.szName = null;// IntPtr.Zero;//clear the name
            enuAviError result = (enuAviError)AVIApi.AVIFileCreateStream(this.Handle, out v_haud, ref v_info);
            if (result != enuAviError.NoError)
                return false;
            //set the format            
            enuAviError frm = (enuAviError)AVIApi.AVIStreamSetFormat(v_haud, 0, waveformat, formatSize);// Marshal.SizeOf(format));
            if (v_haud != IntPtr.Zero)
            {
                aud = new AudioStream(v_haud);
                return true;
            }
            return false;
        }
    }
}

