

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XVideoSurfaceEditor.cs
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
file:XVideoSurfaceEditor.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
namespace IGK.DrSStudio.AudioVideo.WinUI
{
    using IGK.ICore;using IGK.AudioVideo;
    using IGK.AudioVideo.AVI;
    using IGK.DrSStudio.WinUI;
    /// <summary>
    /// reprenset a video Monter interface
    /// </summary>
    [IGK.DrSStudio.CoreSurface("VideoBuilderSurface")]
    public class XVideoSurfaceEditor : XVideoSurfaceBase
    {
        private string m_tempAudioFile;
        private AVIFile m_AviFile;
        private AVIFile.VideoStream m_videoStream;//Current video stream
        private AVIFile.AudioStream m_audioStream;
        private IGK.AudioVideo.MCI.MCIWaveManager wavManager;
        private int currentTick;
        private int lastTick;
        private Graphics m_ghandle;
        private AVIEditableStream m_edit;
        public XVideoSurfaceEditor()
            : base()
        {
        }
        public XVideoSurfaceEditor(AVIFile file, string filename)
            : base()
        {
            if (file == null)
                throw new CoreException(enuExceptionType.ArgumentIsNull, "file");
            this.m_AviFile = file;
            this.FileName = filename;
            this.m_videoStream = file.GetVideoStream();
            this.m_audioStream = file.GetAudioStream();
            this.m_edit = AVIEditableStream.Create(this.m_videoStream);
        }
        /// <summary>
        /// get the avi file
        /// </summary>
        public AVIFile AviFile
        {
            get { return m_AviFile; }
            private set
            {
                if (m_AviFile != value)
                {
                    m_AviFile = value;
                    m_videoStream = AviFile.GetVideoStream();
                    m_audioStream = AviFile.GetAudioStream();
                }
            }
        }
        public override enuVideoSurfaceType VideoSurfaceType
        {
            get { return enuVideoSurfaceType.VideoEditor; }
        }
        protected override IVideoDefaultAction CreateVideoAction()
        {
            return new VideoEditorAction(this);
        }
        //protected override IVideoDefaultAction CreateVideoAction()
        //{
        //    return new VideoEditorAction(this);
        //}
        protected override EventHandler GetTimerTickHandle()
        {
            return new EventHandler(_Tick);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            RenderScene(e.Graphics);
            base.OnPaint(e);
        }
        private long GetFrameUpdate()
        {
            int c = System.Environment.TickCount;
            if (lastTick == 0)
            {
                lastTick = c;
                return 1;
            }
            int r = (int)25 * (c - lastTick) / 1000;
            if (r > 0)
                lastTick = c;
            return r;
        }
        void _Tick(object sender, EventArgs e)
        {
            if (this.CurrentPosition <= this.m_videoStream.Length - 1)
            {
                this.CurrentPosition += GetFrameUpdate();
            }
            else
            {
                this.Stop();
            }
            if (this.m_ghandle != null)
                this.RenderScene(this.m_ghandle);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Stop();
                if (this.AviFile != null)
                    this.AviFile.Dispose();
                this.VideoSurfaceAction.Dispose();
            }
            base.Dispose(disposing);
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            m_ghandle = this.CreateGraphics();
        }
        protected override void OnHandleDestroyed(EventArgs e)
        {
            m_ghandle.Dispose();
            base.OnHandleDestroyed(e);
        }
        void RenderScene(Graphics g)
        {
            g.Clear(Color.Black);
            if (this.m_videoStream != null)
            {
                if (this.Playing)
                {
                    using (Bitmap bmp = m_videoStream.GetFrame((int)this.CurrentPosition))
                    {
                        if (bmp == null)
                        {
                            Stop();
                            return;
                        }
                        g.DrawImage(bmp,
                            CoreMathOperation.Align(
                             new RectangleF(PointF.Empty, bmp.Size),
                            this.ClientRectangle,
                             IGK.DrSStudio.Drawing2D.enuCore2DAlignElement.Center));
                    }
                }
                else
                {
                    if (this.m_videoStream.BeginGetFrame())
                    {
                        using (Bitmap bmp = m_videoStream.GetFrame((int)this.CurrentPosition))
                        {
                            if (bmp != null)
                            {
                                g.DrawImage(bmp,
                                    this.ClientRectangle);
                            }
                        }
                        this.m_videoStream.EndGetFrame();
                    }
                }
            }
        }
        public class VideoEditorAction : VideoPlayerActionBase
        {
            public new XVideoSurfaceEditor CurrentSurface
            {
                get
                {
                    return base.CurrentSurface as XVideoSurfaceEditor;
                }
            }
            public VideoEditorAction(XVideoSurfaceEditor surface)
                : base(surface)
            {
            }
            public override void Dispose()
            {
                //dispose
                if (System.IO.File.Exists(this.CurrentSurface.m_tempAudioFile))
                {
                    System.IO.File.Delete(this.CurrentSurface.m_tempAudioFile);
                }
                this.CurrentSurface.AviFile.Dispose();
            }
            public override void Play()
            {
                if (string.IsNullOrEmpty(this.CurrentSurface.m_tempAudioFile))
                {
                    this.CurrentSurface.m_tempAudioFile = System.IO.Path.GetFullPath("music.wav");
                    if (this.CurrentSurface.AviFile != null)
                    {
                        AVIFile.AudioStream aud = this.CurrentSurface.AviFile.GetAudioStream();
                        if (aud != null)
                        {
                            this.CurrentSurface.AviFile.GetAudioStream().ExportToWaveFile(
                                this.CurrentSurface.m_tempAudioFile, null, false, IntPtr.Zero)
                                ;
                            string vn = "surface_" + this.GetHashCode();
                            IGK.AudioVideo.MCI.MCIManager.OpenWaveAudio(vn, this.CurrentSurface.m_tempAudioFile);
                            this.CurrentSurface.wavManager = IGK.AudioVideo.MCI.MCIWaveManager.Create(vn);
                        }
                    }
                }
                if ((this.CurrentSurface.m_videoStream != null) && this.CurrentSurface.m_videoStream.BeginGetFrame())
                {
                    this.CurrentSurface.currentTick = Environment.TickCount;
                    this.CurrentSurface.lastTick = this.CurrentSurface.currentTick;
                    this.CurrentSurface.Timer.Interval = 20;// (int)(1000 / m_videoStream.FramePerSecond);
                    if (this.CurrentSurface.wavManager != null)
                    {
                        this.CurrentSurface.Timer.Enabled = true;
                        this.CurrentSurface.wavManager.Play();
                    }
                    else
                    {
                        this.CurrentSurface.Timer.Enabled = false;
                    }
                }
                else
                    CoreMessageBox.Instance.ShowError("can't read video file");
            }
            public override void Pause()
            {
                if (this.Playing)
                {
                    this.CurrentSurface.wavManager.Pause();
                    this.CurrentSurface.Timer.Enabled = false;
                    this.CurrentSurface.m_videoStream.EndGetFrame();
                }
            }
            public override void Stop()
            {
                if (this.CurrentSurface.wavManager != null)
                {
                    this.CurrentSurface.wavManager.Stop();
                }
                if (this.Playing)
                {
                    this.CurrentSurface.wavManager.Stop(false, false);
                    this.CurrentSurface.Timer.Enabled = false;
                    this.CurrentSurface.m_videoStream.EndGetFrame();
                }
                if (this.CurrentSurface.CurrentPosition != 0)
                {
                    this.CurrentSurface.CurrentPosition = 0;
                    this.CurrentSurface.Invalidate();
                }
            }
            public override void Return()
            {
            }
            public override void JumpTo(TimeSpan time)
            {
            }
        }
        /// <summary>
        /// save a copy. to
        /// </summary>
        /// <param name="filename"></param>
        public override void SaveTo(string filename)
        {
            this.AviFile.SaveTo(filename, null);
        }
        public override long VideoLength
        {
            get { return m_videoStream.Length; }
        }
        public override void CaptureImageTo(string filename)
        {
            if (this.m_videoStream == null)
                return;
            this.m_videoStream.BeginGetFrame();
            using (System.Drawing.Bitmap bmp = this.m_videoStream.GetFrame((int)this.CurrentPosition))
            {
                if (bmp != null)
                {
                    System.Drawing.Bitmap cb = new Bitmap(bmp.Width, bmp.Height);
                    Graphics g = Graphics.FromImage(cb);
                    g.DrawImage(bmp, Point.Empty);
                    g.Dispose();
                    cb.Save(filename,
                    System.Drawing.Imaging.ImageFormat.Jpeg);
                    cb.Dispose();
                }
            }
            this.m_videoStream.EndGetFrame();
        }
        internal bool ImportFile(string p)
        {
            AVIFile f = AVIFile.Open(p);
            if (f == null)
                return false;
            AVIEditableStream edit = null;
            AVIFile.VideoStream c = f.GetVideoStream();
            AVIFile.AudioStream a = f.GetAudioStream();
            //build video
            edit = AVIEditableStream.Create(c);
            this.m_edit.Paste(edit, 0, c.Length);
            edit.Dispose();
            c.Dispose();
            //build audio
            f.Dispose();
            AVIFile s = AVIFile.CreateFileFromStreams(m_edit.Handle);
            s.SaveTo("d:\\mmmmmm.avi", null);
            s.Dispose();
            return true;
        }
        internal void DeleteAll()
        {
            this.m_videoStream.Clear();
        }
    }
}

