

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: VideoFile.cs
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
file:VideoFile.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IGK.ICore;using IGK.AudioVideo.AVI;
namespace IGK.DrSStudio
{
    public class VideoFile : IVideoFile 
    {
        private VideoFile()
        {
            this.m_defaulBitmap = null;
        }
        public TimeSpan Duration { get {
            if (this.m_vidHwnd != null)
            {
                return new TimeSpan(0, 0, 0, (int)
                (this.m_vidHwnd.Length / this.m_vidHwnd.FramePerSecond)
                , 0);
            }
            else {
                return TimeSpan.Zero;
            }
        } }
        public int Compression { get { 
            if (this.m_vidHwnd !=null)
                return this.m_vidHwnd.Compression;
            return 0;
        } }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder ();
            if (this.HasVideo )
            sb.AppendLine (string.Format("{0}x{1}\tfps:{2},\tDuration:{3}",
                this.Width, this.Height,
                this.Fps,
                this.Duration ));
            if (this.HasAudio)
            {
                sb.AppendLine(string.Format("{0} {1},\tDuration:{2}",                    
                    this.m_audHwnd.TypeName ,
                    this.m_audHwnd.Length,
                   new TimeSpan(0,0,0,
                    AVIStream .GetMilliSecond(
                   this.m_AudHandle ,
                   this.m_audHwnd.Length)/1000, 0)));
            }
            return sb.ToString();
        }
        public static VideoFile CreateFromFile(string filename)
        {
            AVIFile f = AVIFile.CreateFromFile (filename, enuAviAccess.Read  );
            if (f!=null)
            {
                VideoFile v_f = new VideoFile();
                AVIFile.VideoStream v_vid = f.GetVideoStream();
                AVIFile.AudioStream v_aud = f.GetAudioStream();
                v_f.m_FileName = filename;
                v_f.m_aviF = f;
                if (v_vid != null)
                {
                    v_f.m_Width = v_vid.Width;
                    v_f.m_Height = v_vid.Height;
                    v_f.m_VidHandle = v_vid.Handle;                    
                    v_f.m_vidHwnd = v_vid;
                    v_f.m_Fps = f.RatePerSecond;
                }
                if (v_aud != null)
                {
                    v_f.m_AudHandle = v_aud.Handle;
                    v_f.m_audHwnd = v_aud;
                }                
                return v_f;
            }
            return null;
        }
        #region IVideoFile Members
        AVIFile m_aviF;
        AVIFile.VideoStream m_vidHwnd;
        AVIFile.AudioStream m_audHwnd;
        private string m_FileName;
        private IntPtr m_VidHandle;
        private IntPtr m_AudHandle;
        private int m_Width;
        private int m_Height;
        private float m_Fps;
        public float Fps
        {
            get { return m_Fps; }
        }
        public int Height
        {
            get { return m_Height; }
        }
        public int Width
        {
            get { return m_Width; }
        }
        public IntPtr VidHandle
        {
            get { return m_VidHandle; }
        }
        public string FileName
        {
            get { return m_FileName; }
        }
        private System.Drawing.Bitmap m_defaulBitmap;
        public System.Drawing.Bitmap GetBitmap()
        {
            return m_defaulBitmap;
        }
        #endregion
        #region IDisposable Members
        public void Dispose()
        {
            if (this.m_defaulBitmap!=null)
                this.m_defaulBitmap.Dispose();
            if (this.m_vidHwnd !=null)
            this.m_vidHwnd.Dispose();
            if (this.m_AudHandle != IntPtr.Zero)
                AVIStream.CloseStream(this.m_AudHandle);
            this.m_aviF.Dispose();
        }
        #endregion
        #region IVideoFile Members
        public IntPtr AudHandle
        {
            get { return m_AudHandle; }
        }
        public bool HasAudio
        {
            get { return m_AudHandle != IntPtr.Zero ; }
        }
        public bool HasVideo
        {
            get {return m_VidHandle != IntPtr.Zero; }
        }
        #endregion
    }
}

