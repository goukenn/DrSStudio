

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: VideoItem.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:VideoItem.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.AudioVideo
{
    using IGK.ICore;using IGK.DrSStudio.AudioVideo;
    using IGK.AudioVideo.AVI;
    public class VideoItem : IVideoItem 
    {
        AVIEditableStream  m_editStream;
        private bool m_compressed;
        private string m_fccHandlerName;
        private AVIStreamInfoStruct GetStreamInfo()
        {
            return AVIStream.GetStreamInfo(this.m_editStream.Handle);
        }
        private static bool IsValidFccType(AVIStreamInfoStruct info, string fccHandlerName)
        {
            return (info.FCCHandlerName == fccHandlerName);
        }
        public static VideoItem Open(string fileName)
        {
            AVIFile f = AVIFile.Open(fileName);
            {
                if (f != null)
                {
                    AVIFile.VideoStream v_vid = f.GetVideoStream();
                    {
                        if (v_vid != null)
                        {
                            return CreateFromAVIStream(v_vid.Handle );
                        }
                    }
                }
            }
            return null;
        }
        public static VideoItem CreateFromAVIStream(IntPtr handle)
        {
            if (handle == IntPtr.Zero)
                return null;
            AVIStreamInfoStruct c = AVIStream.GetStreamInfo (handle);
            if (c.FCCTypeName == "vids")
            {
                AVIEditableStream v_stream = AVIEditableStream.CreateFromHwnd(handle);
                if (v_stream != null)
                {
                    VideoItem v_i = new VideoItem();
                    v_i.m_editStream = v_stream;
                    v_i.Init();
                    return v_i;
                }
            }
            return null;
        }
        public static VideoItem CreateCopy(VideoItem vidItem)
        {
            return CreateFrom(vidItem.m_editStream);
        }
        public static VideoItem CreateFrom(AVIEditableStream editableStream)
        {
            AVIEditableStream v_stream =   AVIEditableStream.CreateFromHwnd(editableStream.Handle);
            AVIStreamInfoStruct c = AVIStream.GetStreamInfo(v_stream.Handle);
            if (v_stream != null)
            {
                VideoItem v_i = new VideoItem();
                v_i.m_editStream = v_stream;
                v_i.Init();
                return v_i;
            }
            return null;
        }
        private void Init()
        {
            string s = this.GetStreamInfo().FCCHandlerName;
            this.m_compressed = !string.IsNullOrEmpty(s);
            this.m_fccHandlerName = s;
        }
        private VideoItem()
        { 
        }
        #region IVideoItem Members
        public long Length
        {
            get { return this.m_editStream.Length; }
        }
        #endregion
        #region IDisposable Members
        public void Dispose()
        {
            this.m_editStream.Dispose();
        }
        #endregion
        public override string ToString()
        {
            return "VideoItem ["+this.Length+"]";
        }
        public void SaveTo(string p)
        {
            this.m_editStream.ExportToFile(p);
        }
        public bool Decompress(string filename, int from, int length)
        {
            if (this.m_editStream.BeginGetFrame())
            {
                AVIStreamInfoStruct v_struct = GetStreamInfo ();
                AVIFile f = AVIFile.Open(filename, enuAviAccess.Create | enuAviAccess.ReadWrite);
                AVIFile.VideoStream vid = AVIFile.VideoStream.CreateFrom(this.m_editStream.Handle );
                AVIFile .VideoStream tvid = f.AddNewVideoStream(vid.Rate , vid.Scale, 0, vid.Width, vid.Height);
                AVIEditableStream v_edit = AVIEditableStream.Create(tvid);
                v_edit.Cut(0,(int) v_edit.Length).Dispose ();
                v_edit.BeginGetFrame();
                int i = 0;
                while ( i < length   )
                {
                    v_edit.AddFrame(i, 1, this.m_editStream.GetFrame(from + i));
                    i++;
                }
                v_edit.EndGetFrame();
                f.Dispose();
              //  AVIFile.VideoStream.DecompressTo (filename, vid, from, length);
                //AVIFile v_f = AVIFile.Open(filename, enuAviAccess.Create | enuAviAccess.ReadWrite);
                this.m_editStream.EndGetFrame();
            }
            return false;
        }
        /// <summary>
        /// cut this video items . cut drop the unselected intervall
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length"></param>
        public void  Cut(int start, int length)
        {            
            AVIEditableStream r =  this.m_editStream.Cut(start, length);
            if (!IsValidFccType(GetStreamInfo(), this.m_fccHandlerName))
            {
                this.m_editStream.Dispose();
                this.m_editStream = r;
            }
            else {
                r.Dispose();
            }
        }
        public void Copy(int start, int length)
        {
            AVIEditableStream r = this.m_editStream.Copy(start, length);
            this.m_editStream.Dispose();
            this.m_editStream = r;
        }
        public void Clear()
        {
            this.Cut(0, (int)this.Length );
        }
        public void Paste(VideoItem i, int from, int length)
        {
            this.m_editStream.Paste(i.m_editStream, from, length);
        }
        /// <summary>
        /// set the name of this video item
        /// </summary>
        /// <param name="p"></param>
        public void SetName(string p)
        {
            this.m_editStream.SetName(p);
        }
    }
}

