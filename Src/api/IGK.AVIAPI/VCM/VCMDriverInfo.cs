

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: VCMDriverInfo.cs
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
file:VCMDriverInfo.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices; 
namespace IGK.AVIApi.VCM
{
    using IGK.ICore;using IGK.AVIApi.AVI;
    using System.IO;
    /// <summary>
    /// represent the Video Controller Driver Info
    /// </summary>
    public class VCMDriverInfo
    {
        VCMInfo m_info;
        IntPtr m_hopen;
        public string FCCType { get { return this.m_info.FCCType; } }
        public string TypeHandler { get { return this.m_info.FCCHandler; } }
        /// <summary>
        /// get the driver name 
        /// </summary>
        public string Name { get { return this.m_info.Name; } }
        /// <summary>
        /// Get the description
        /// </summary>
        public string Description { get { return this.m_info.Description ; } }
        /// <summary>
        /// get the driver
        /// </summary>
        public string Driver { get { return this.m_info.Driver; } }
        internal VCMDriverInfo(VCMInfo info)
        {
            this.m_info = info;
        }
        public override string ToString()
        {
            return string.Format(this.Name + " [{0}] ", string.IsNullOrEmpty(this.Description) ? "NoDescription" : this.Description);// +" ]";
        }
        /// <summary>
        /// get if the driver is currently opened
        /// </summary>
        public bool IsOpen {
            get {
                return (this.m_hopen != IntPtr.Zero);
            }
        }
        public bool Open(enuVCMLocateFlags flag)
        {
            if (this.m_hopen == IntPtr.Zero)
            {
                IntPtr hic = VCMApi.ICOpen(this.m_info.fccType, this.m_info.fccHandler, flag);
                if (hic != IntPtr.Zero)
                {
                    this.m_hopen = hic;
                    return true;
                }
            }
            return false;
        }
        public void Close()
        {
            if (this.m_hopen != IntPtr.Zero)
            {
                int i = VCMApi.ICClose(this.m_hopen);
                if (i == 0)
                {
                    this.m_hopen = IntPtr.Zero;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Can't close the video driver handle");
                }
            }
        }
        public bool CanConfigure {
            get {
                if (this.m_hopen!= IntPtr.Zero )
                    return VCMApi.ICQueryConfigure(this.m_hopen);
                return false;
            }
        }
        public void ShowAbout(IntPtr hwnd)
        {
            VCMApi.ICAbout(this.m_hopen, hwnd);
        }
        public bool Configure(IntPtr hwnd)
        {
            return VCMApi.ICConfigure(this.m_hopen, hwnd);
        }
        public AVIFile.VideoStream Compress(
            AVI.AVIFile.VideoStream vidStream, 
            AVISaveProgressionCallBack PROC)
        {
            if (vidStream == null)
                return null ;
            this.Open(enuVCMLocateFlags.Compress);
            AVIApi.AVICOMPRESSOPTIONS_CLASS rf = new AVIApi.AVICOMPRESSOPTIONS_CLASS();
            rf.fccType = (uint)this.m_info.fccType;
            rf.fccHandler = (uint)this.m_info.fccHandler;
            if (vidStream.Compression == 0)
            {//compress using the option class
                AVIFile.VideoStream v_vid =  vidStream.Compress(rf);                
                return v_vid;
            }
            //try to decompress
            AVIFile vf = vidStream.Decompress(0, -1);
            if (vf == null)
            {
                string tempfile =  TempFile(System.IO.Path.GetTempFileName() , ".avi");
                AVIFile[] v_tvf = AVIFile.VideoStream.DecompressTo(tempfile, vidStream, 0, 
                    vidStream.Length,
                    500000000, PROC);
                if (v_tvf != null)
                {
                   AVIFile .VideoStream vid =  vf.GetVideoStream();
                }
                System.IO.File.Delete(tempfile);
            }
            return null;
        }

        
         private static string TempFile(string tempFile, string extension)
        {
            string f= tempFile + extension;
            if (File.Exists(tempFile))
            {
                File.Move(tempFile, tempFile + extension);
                return f;
            }
            return tempFile;
        }
        
        internal bool BeginDecompress(LPBITMAPINFOHEADER inBitmap, LPBITMAPINFOHEADER outBitmap)
        {
            return VCMApi.ICDecompressBegin(this.m_hopen, inBitmap, outBitmap);
        }
        /// <summary>
        /// save the current video stream
        /// </summary>
        /// <param name="vidStream">Video stream to save using this video driver</param>
        /// <param name="filename">target filename</param>
        /// <returns></returns>
        public bool CompressTo(AVIFile.VideoStream vidStream, string filename, AVISaveProgressionCallBack PROC)
        {
            if (vidStream == null)
                return false;
            this.Open(enuVCMLocateFlags.Compress);
//            AVIApi.AVICOMPRESSOPTIONS_CLASS ropts = AVIStreamUtils.ChooseCodec(vidStream.Handle, IntPtr.Zero, enuDialogFlag.All);
            AVIApi.AVICOMPRESSOPTIONS_CLASS rf = new AVIApi.AVICOMPRESSOPTIONS_CLASS();
            rf.fccType = (uint)this.m_info.fccType;
            rf.fccHandler = (uint)this.m_info.fccHandler;
            rf.dwQuality = 0; //this.GetQuality();
            rf.dwKeyFrameEvery = 0;// this.GetKeyFrameRate();
            rf.dwInterleaveEvery = 0;// (uint)size;
            AVIApi.AVICOMPRESSOPTIONS[] v_t = new AVIApi.AVICOMPRESSOPTIONS[] { rf.ToStruct() };
            enuAviError e = (enuAviError)AVIApi.AVISaveV(filename, 0, PROC, 1, new IntPtr[] { vidStream.Handle },
               ref v_t ) ;
            this.Close();
            return (e == enuAviError.NoError) ;
        }
        private uint GetKeyFrameRate()
        {
            int i = 0;
            VCMApi.ICSendMessage(this.m_hopen, VCMApi.ICM_GETDEFAULTKEYFRAMERATE, ref i, 0);
            return (uint)i;
        }
        private uint GetQuality()
        {
            int i = 0;        
            VCMApi.ICSendMessage(this.m_hopen, VCMApi.ICM_GETQUALITY, ref i, 0);
            return (uint)i;
        }
        public bool DecompressTo(string filename, AVIFile.VideoStream vid, AVISaveProgressionCallBack PROC)
        {
            if (vid  == null)
                return false;
            AVIApi.AVICOMPRESSOPTIONS_CLASS rf = new AVIApi.AVICOMPRESSOPTIONS_CLASS();
            rf.fccType = (uint)this.m_info.fccType;
            rf.fccHandler = (uint)this.m_info.fccHandler;
            AVIStreamInfoStruct info = vid.GetStreamInfo();
            AVIEditableStream edit = AVIEditableStream.CreateFrom (vid);//.CreateEmpty();
            AVIEditableStream edit2 = AVIEditableStream .CreateEmpty();
            info.dwQuality = 0;
            info.dwRate = 23;
            info.dwScale = 1;
            info.fccHandler =  (int)rf.fccHandler ;
            info.fccType = (int)rf.fccType;
            edit2.SetStreamInfo(info);
            AVIFile c = AVIFile.CreateInMemory();
            c.AddStream(edit2);
            c.Save(filename);          
            return true;// vid.Save(filename, rf, PROC);  
        }
        IntPtr v_state;
        public void SaveState()
        {
            int size = VCMApi.ICGetStateSize(this.m_hopen);
            IntPtr alloc = Marshal.AllocCoTaskMem(size);
            int err = VCMApi.ICGetState(this.m_hopen, alloc, size);
            this.v_state = alloc;
        }
        public void RestoreState()
        {
            int size = VCMApi.ICGetStateSize(this.m_hopen);            
            VCMApi.ICSetState(this.m_hopen, this.v_state , size);
        }
    }
}

