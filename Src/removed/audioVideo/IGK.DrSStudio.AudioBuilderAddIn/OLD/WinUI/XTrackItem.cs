

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XTrackItem.cs
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
file:XTrackItem.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
namespace IGK.DrSStudio.AudioBuilder.WinUI
{
    /// <summary>
    /// public represent the track control
    /// </summary>
    public class XTrackItem : IGK.DrSStudio.WinUI .XControl , IAudioTrack ,
        ITrack 
    {
        private XAudioBuilderSurface m_surface;
        private readonly AudioTrackBase m_track;
        public XAudioBuilderSurface Surface
        {
            get { return m_surface; }            
        }
        public XTrackItem(AudioTrackBase track, XAudioBuilderSurface surface)
        {
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.m_track = track;
            this.m_surface = surface;
            this.Surface.Tracks.AddTrack(this);
            this.Surface.Controls.Add(this);
            this.m_surface.SizeChanged += new EventHandler(m_surface_SizeChanged);
            this.Surface.TrackRemoved += new TrackEventHandler(_TrackRemoved);
            this.Surface.TrackAdded += new TrackEventHandler(_TrackAdded);
            this.Click += new EventHandler(_Click);
            this.Paint += new PaintEventHandler(_Paint);
            this.SetupBound();
        }
        void _Paint(object sender, PaintEventArgs e)
        {
            using (System.Drawing.Drawing2D.LinearGradientBrush br = new System.Drawing.Drawing2D.LinearGradientBrush(
              this.ClientRectangle,
              XAudioRenderer.AudioItemStartColor,
              XAudioRenderer.AudioItemEndColor,
              90.0f))
            {
                CoreControlPaint.FillRoundRect(e.Graphics,
                    br, this.ClientRectangle, 2);
            }
            CoreControlPaint.DrawRoundRect(e.Graphics,
                Colorf.DarkBlue,
                1,
                 System.Drawing.Drawing2D.DashStyle.Solid,
                this.ClientRectangle,
                2);
            Rectangle rc = this.ClientRectangle;
            rc.Inflate(-2, -2);
            RenderSequence(e.Graphics, rc);
        }
        private void RenderSequence(Graphics graphics, Rectangle rectangle)
        {
        }
        void _Click(object sender, EventArgs e)
        {
            this.m_surface.CurrentTrack = this;
        }
        void _TrackAdded(object sender, TrackEventArgs e)
        {
        }
        void _TrackRemoved(object sender, TrackEventArgs e)
        {
            if (e.Tracks == this)
            {
                this.Surface.Controls.Remove(this);
                this.Dispose();
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.m_track.Dispose();
            }
            base.Dispose(disposing);
        }
        void m_surface_SizeChanged(object sender, EventArgs e)
        {
            SetupBound();
        }
        private void SetupBound()
        {
            //upadte the size control
            int y = 0;
            int w = 0;
            y = AudioConstant.TIMELINE_HEIGHT  +  this.Index * AudioConstant.TRACK_ITEM_HEIGHT ;
            w = Math.Max (this.Surface.Width , this.m_track.Duration.Seconds * 10);
            this.Bounds = new Rectangle(0, y, w, AudioConstant.TRACK_ITEM_HEIGHT);
        }
        /// <summary>
        /// get the current track index
        /// </summary>
        public int Index {
            get {
                if (this.Surface != null)
                    return this.Surface.Tracks.IndexOf(this);
                return -1;
            }
        }
        #region IAudioTrack Members
        public TimeSpan Duration
        {
            get {return  this.m_track .Duration; }
        }
        public void Play()
        {
            this.m_track.Play();
        }
        public void Stop()
        {
            this.m_track.Stop();
        }
        public void SaveTo(string filename)
        {
            this.m_track.SaveTo(filename);
        }
        public void Export()
        {
            this.m_track.Export();
        }
        public void Edit()
        {
            this.m_track.Edit();
        }
        public void CopyAll()
        {
            this.m_track.CopyAll();
        }
        public void Copy(TimeSpan From)
        {
            this.m_track.Copy(From);
        }
        public void Copy(TimeSpan From, TimeSpan To)
        {
            this.m_track.Copy(From, To);
        }
        public IAudioSequenceCollections Sequences
        {
            get { return this.m_track.Sequences; }
        }
        #endregion
        #region IAudioTrack Members
        public IGK.AudioVideo.AVI.AVIEditableStream GetEditableStream()
        {
            return this.m_track.GetEditableStream();
        }
        #endregion
        #region ITrack Members
        public void Seek(int position)
        {
            throw new NotImplementedException();
        }
        public void Next()
        {
            throw new NotImplementedException();
        }
        public void Previous()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

