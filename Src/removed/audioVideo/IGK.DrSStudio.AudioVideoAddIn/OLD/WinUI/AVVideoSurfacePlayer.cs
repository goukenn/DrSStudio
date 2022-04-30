

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AVVideoSurfacePlayer.cs
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
file:AVVideoSurfacePlayer.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.AudioVideo.WinUI
{
    using IGK.ICore;using IGK.AudioVideo.AVI;
    using IGK.AudioVideo;
    using IGK.AudioVideo.Players;
    [CoreSurfaceAttribute("VideoSurfacePlayer")]
    public class AVVideoSurfacePlayer : XVideoSurfaceBase
    {
        private VideoPlayer m_VideoPlayer;
        public VideoPlayer VideoPlayer
        {
            get { return m_VideoPlayer; }
        }
        public override bool NeedToSave
        {
            get
            {
                return false;
            }
            set
            {
                base.NeedToSave = value;
            }
        }
        public override long VideoLength
        {
            get
            {
                return VideoPlayer.Length;
            }
        }
        public override enuVideoSurfaceType VideoSurfaceType
        {
            get { return enuVideoSurfaceType.VideoPlayer; }
        }
        public AVVideoSurfacePlayer(VideoPlayer vidplayer, string fromfilename)
        {
            if (vidplayer == null)
            {
                throw new CoreException(enuExceptionType.ArgumentIsNull, "vidplayer");
            }
            this.m_VideoPlayer = vidplayer;
            this.m_VideoPlayer.Stretch();
            this.FileName = fromfilename;
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            m_VideoPlayer.SetWindowHandle(this.Handle);
        }
        protected override IVideoDefaultAction CreateVideoAction()
        {
            return new VideoPlayerAction(this);
        }
        public class VideoPlayerAction : VideoPlayerActionBase, IVideoDefaultAction
        {
            public new AVVideoSurfacePlayer CurrentSurface
            {
                get
                {
                    return base.CurrentSurface as AVVideoSurfacePlayer;
                }
            }
            public VideoPlayerAction(XVideoSurfaceBase videoSurface)
                : base(videoSurface)
            {
                RegisterSurfaceEvent();
            }
            private void RegisterSurfaceEvent()
            {
                this.CurrentSurface.SizeChanged += new EventHandler(videoSurface_SizeChanged);
            }
            private void UnRegisterSurfaceEvent()
            {
                this.CurrentSurface.SizeChanged -= new EventHandler(videoSurface_SizeChanged);
            }
            void videoSurface_SizeChanged(object sender, EventArgs e)
            {
                if (this.CurrentSurface.VideoPlayer != null)
                    this.CurrentSurface.VideoPlayer.Stretch();
            }
            public override void Play()
            {
                if (this.CurrentSurface.VideoPlayer != null)
                {
                    this.CurrentSurface.VideoPlayer.Play();
                    this.m_playing = true;
                }
            }
            public override void Pause()
            {
                if (this.CurrentSurface.VideoPlayer != null)
                {
                    this.CurrentSurface.VideoPlayer.Pause();
                    this.m_playing = false;
                }
            }
            public override void Stop()
            {
                if (this.CurrentSurface.VideoPlayer != null)
                {
                    this.CurrentSurface.VideoPlayer.Stop();
                    this.CurrentSurface.VideoPlayer.SetPosition(0);
                    this.CurrentSurface.Invalidate();
                }
            }
            public override void Return()
            {
                if (this.CurrentSurface.VideoPlayer != null)
                    this.CurrentSurface.VideoPlayer.SetPosition(0);
            }
            public override void JumpTo(TimeSpan time)
            {
                if (this.CurrentSurface.VideoPlayer != null)
                    this.CurrentSurface.VideoPlayer.JumpTo(time);
            }
            public override void Dispose()
            {
                if (this.CurrentSurface.VideoPlayer != null)
                {
                    this.CurrentSurface.VideoPlayer.Stop();
                    this.CurrentSurface.VideoPlayer.Close();
                }
            }
        }
        /// <summary>
        /// save the player
        /// </summary>
        /// <param name="filename"></param>
        public override void SaveTo(string filename)
        {
            if (this.VideoPlayer.CanSave)
            {
                this.VideoPlayer.SaveTo(filename);
            }
        }
        public override void CaptureImageTo(string filaname)
        {
            this.VideoPlayer.CaptureTo(filaname);
        }
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.FillRectangle(CoreBrushRegister.GetBrush(Colorf.Red), new System.Drawing.Rectangle(0, 0, 100, this.Height));
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Stop();
            }
            base.Dispose(disposing);
        }
    }
}

