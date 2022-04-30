

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XVideoPlayer.cs
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
file:XVideoPlayer.cs
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
namespace IGK.DrSStudio.VideoEditionTableAddIn.WinUI
{
    using IGK.ICore;using IGK.AudioVideo.AVI ;
    using IGK.AudioVideo.ACM;
    using IGK.AudioVideo.MMIO;
    using IGK.AudioVideo.MCI; //reprensent the mci for playing audio
    public partial class XVideoPlayer : IGK.DrSStudio.WinUI.XPanel, IVideoPlayer 
    {
        private bool m_canPlayAudio;
        private Thread m_playerThread;
        private XVideoEditorSurface  m_Surface;
        private PictureBox c_PictureBox;
        private XVideoTabManager c_VideoTabManager;
        private bool m_IsPlaying;
        private AVIFile.VideoStream m_vidStream;
        private int m_framePos;
        private string m_tempAudioFile;
        private string m_audioId; //id of the audio file
        /// <summary>
        /// get the displayed image
        /// </summary>
        /// <returns></returns>
        public System.Drawing.Bitmap GetDisplayedBitmap()
        {
            if (this.c_PictureBox.Image != null)
            {
                return this.c_PictureBox.Image as Bitmap ;
            }
            return null;
        }
        public bool IsPlaying
        {
            get { return m_IsPlaying; }
        }
        /// <summary>
        /// get the picture box
        /// </summary>
        public PictureBox PictureBox
        {
            get { return c_PictureBox; }
        }
        public XVideoEditorSurface  Surface
        {
            get { return m_Surface; }
            set
            {
                if (m_Surface != value)
                {
                    m_Surface = value;
                    RegisterEvent();
                }
            }
        }
        private void RegisterEvent()
        {
            this.Surface.CurrentImpotedFileChanged += new EventHandler(Surface_CurrentImpotedFileChanged);
        }
        void Surface_CurrentImpotedFileChanged(object sender, EventArgs e)
        {
            if (this.IsPlaying)
                this.Stop();
            if ((this.Surface.CurrentFile != null) && (this.Surface.CurrentFile.HasVideo))
            {
                AVIFile.VideoStream vid = this.Surface.CurrentFile.File.GetVideoStream();
                if (vid.BeginStreaming(0, vid.Length, (int)enuVideoPlaySpeed.Normal))
                {
                    if (vid.BeginGetFrame())
                    {
                        //Get 
                        this.c_PictureBox.Image = vid.GetFrame(vid.Length / 10);
                        vid.EndGetFrame();
                    }
                    vid.EndStreaming();
                }
                vid.Dispose();
            }
            else {
                this.c_PictureBox.Image = null;
            }
        }
        public XVideoPlayer()
        {
            InitializeComponent();
            c_PictureBox = new PictureBox();
            c_PictureBox.BackColor = Color.Black;
            c_PictureBox.Dock = DockStyle.Fill;
            c_PictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            m_IsPlaying = false;
            c_VideoTabManager = new XVideoTabManager();
            c_VideoTabManager.Player = this;
            this.Controls.Add(c_VideoTabManager);
            this.Controls.Add(c_PictureBox);
            this.SizeChanged += new EventHandler(XVideoPlayer_SizeChanged);
        }
        void XVideoPlayer_SizeChanged(object sender, EventArgs e)
        {
            this.c_VideoTabManager.Bounds = new Rectangle(0, this.Height - this.c_VideoTabManager.Height,
                this.Width,
                this.c_VideoTabManager.Height);
        }
        #region IVideoPlayer Members
        public void Play()
        {
            if (this.DesignMode || (this.Surface.CurrentFile == null))
                return;
            if (this.IsPlaying)
            {
                this.Pause();
            }
            else { 
                //
                this.m_vidStream = this.Surface.CurrentFile.File.GetVideoStream();
                this.m_canPlayAudio = this.Surface.CurrentFile.HasAudio;
                if (this.Surface.CurrentFile.HasAudio)
                {
                    this.m_tempAudioFile = this.Surface.CurrentFile.TempWaveFileName;
                }
                else
                    this.m_tempAudioFile = null;
                m_playerThread = new Thread(RunVideo);
                m_millisecondsPerFrame = (int)(1000 /this.m_vidStream.FramePerSecond );
                this.m_IsPlaying = true;
                m_playerThread.IsBackground = false;
                m_playerThread.Start();
                //play the audio wave stream with mci
                if (this.m_canPlayAudio)
                {
                    this.m_audioId = Guid.NewGuid().ToString();
                    MCIManager.Open(this.m_tempAudioFile, this.m_audioId , "waveaudio", false, false);
                    MCIManager.Play(this.m_audioId);
                }
            }
        }
        int m_millisecondsPerFrame;
        Bitmap m_currentBitmap;
        void RunVideo()
        {
            try
            {
                AVIFile.VideoStream vid = m_vidStream;
                MethodInvoker m = () => { this.c_PictureBox.Image = this.m_currentBitmap; this.c_PictureBox.Refresh(); };
                if (vid.BeginStreaming(0, vid.Length, (int)enuVideoPlaySpeed.Normal))
                {
                    System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
                    if (vid.BeginGetFrame())
                    {
                        watch.Start();
                        int start = m_framePos;
                        for (int i = start ; i < vid.Length;)
                        {
                            i = start  + (int)vid.GetTimeFromSample(watch.ElapsedMilliseconds);
                            m_currentBitmap = vid.GetFrame(i);
                            this.m_framePos = i;
                            this.c_PictureBox.Invoke(m);
                            Thread.Sleep(m_millisecondsPerFrame);
                        }
                        watch.Stop();
                        vid.EndGetFrame();
                    }
                    vid.EndStreaming();
                }
            }
            catch { 
            }
        }
        public void Pause()
        {
            if (this.IsPlaying)
            {
                this.m_playerThread.Abort();
                this.m_playerThread.Join();
                this.m_vidStream.EndGetFrame();
                this.m_vidStream.EndStreaming();
                this.m_vidStream.Dispose();
                this.m_framePos = 0;
                this.DisposeAudio();
                this.m_IsPlaying = false;
            }
        }
        private void DisposeAudio()
        {
            if (this.m_canPlayAudio)
            {
                MCIManager.Stop(this.m_audioId, false, false);
                MCIManager.Close(this.m_audioId);
            }
        }
        public void Stop()
        {
            this.Pause();
            this.m_framePos = 0;
        }
        #endregion
    }
}

