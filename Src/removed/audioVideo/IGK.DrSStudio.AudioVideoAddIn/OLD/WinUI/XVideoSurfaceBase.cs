

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XVideoSurfaceBase.cs
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
file:XVideoSurfaceBase.cs
*/
///* 
//-------------------------------------------------------------------
//Company: IGK-DEV
//Author : C.A.D. BONDJE DOUE
//SITE : http://www.igkdev.be
//Application : DrSStudio
//powered by IGK - DEV &copy; 2008-2012
//THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
//FOR MORE INFORMATION ABOUT THE LICENSE
//------------------------------------------------------------------- 
//*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace IGK.DrSStudio.AudioVideo.WinUI
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.AudioVideo;
    using IGK.AudioVideo.AVI;
    /// <summary>
    /// represent the video display surface
    /// </summary>
    public class XVideoSurfaceBase :
        CoreWorkingSurface ,
        ICoreWorkingSurface,
        ICoreWorkingFilemanagerSurface,
        IVideoDefaultAction,
        IVideoSurface
    {
        private IVideoDefaultAction m_VideoSurfaceAction;
        private string m_fileName;
        private bool m_needToSave;
        private AVMecanism m_mecanism;
        private Timer m_timer;
        private long m_currentPosition;
        // private VideoPlayer m_VideoPlayer;
        // private enuVideoSurfaceType m_VideoSurfaceType;
        /// <summary>
        /// get the video action
        /// </summary>
        public IVideoDefaultAction VideoSurfaceAction
        {
            get
            {
                if (this.m_VideoSurfaceAction == null)
                {
                    this.m_VideoSurfaceAction = CreateVideoAction();
                    if (this.m_VideoSurfaceAction == null)
                        throw new CoreException("Video Action createion Instance is null");
                }
                return this.m_VideoSurfaceAction;
            }
        }
        public virtual enuVideoSurfaceType VideoSurfaceType { get { return enuVideoSurfaceType.VideoPlayer; } }
        protected virtual IVideoDefaultAction CreateVideoAction()
        {
            return null;
        }
        protected Timer Timer
        {
            get
            {
                return this.m_timer;
            }
        }
        #region ICoreWorkingObjectPropertyEvent Members
        public event CoreWorkingObjectPropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs e)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, e);
        }
        #endregion
        #region "EVENTS
        public event EventHandler CurrentPositionChanged;
        #endregion
        public void Record()
        {
        }
        public XVideoSurfaceBase()
        {
            this.m_timer = new Timer();
            this.m_timer.Tick += GetTimerTickHandle();// new EventHandler(_Tick);
            this.m_timer.Enabled = false;
            this.m_timer.Interval = 20; //default interface
            this.Dock = DockStyle.Fill;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, false);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.m_VideoSurfaceAction = this.CreateVideoAction();
            this.m_mecanism = new AVMecanism(this);
        }
        protected virtual EventHandler GetTimerTickHandle()
        {
            return null;
        }
        protected override void Dispose(bool disposing)
        {
            if (this.m_timer != null)
            {
                this.m_timer.Dispose();
                this.m_timer = null;
                this.m_mecanism.Dispose();
                this.m_mecanism = null;
            }
            base.Dispose(disposing);
        }
        public string FileName
        {
            get { return this.m_fileName; }
            set
            {
                this.m_fileName = value;
                OnFileNameChanged(EventArgs.Empty);
            }
        }
        public virtual void RenameTo(string newfilename)
        {
            if (string.IsNullOrEmpty(newfilename))
                return;
            string p = this.FileName;
            System.IO.File.Delete(p);
            this.FileName = newfilename;
            this.Save();
        }
        private void OnFileNameChanged(EventArgs eventArgs)
        {
            if (FileNameChanged != null)
            {
                FileNameChanged(this, eventArgs);
            }
        }
        public event EventHandler FileNameChanged;
        public virtual bool NeedToSave
        {
            get { return m_needToSave; }
            set { m_needToSave = value; OnNeedToSaveChanged(EventArgs.Empty); }
        }
        protected virtual void OnNeedToSaveChanged(System.EventArgs e)
        {
            if (NeedToSaveChanged != null)
                NeedToSaveChanged(this, e);
        }
        public event EventHandler NeedToSaveChanged;
        public void Save()
        {
            this.SaveAs(this.FileName );
        }
        public void SaveAs(string filename)
        {
            this.SaveTo(filename);
        }
        #region ICoreWorkingSurface Members
        public override string SurfaceEnvironment
        {
            get { return "AVI-EDITING"; }
        }
        public override string DisplayName
        {
            get { return System.IO.Path.GetFileName(this.FileName); }
        }
        public void SetParam(ICoreInitializatorParam p)
        {
        }
        #endregion
        public long CurrentPosition
        {
            get
            {
                return this.m_currentPosition;
            }
            set
            {
                if (this.m_currentPosition != value)
                {
                    this.m_currentPosition = value;
                    this.Invalidate();
                    OnCurrentPositionChanged(EventArgs.Empty);
                }
            }
        }
        private void OnCurrentPositionChanged(EventArgs eventArgs)
        {
            if (this.CurrentPositionChanged != null)
            {
                this.CurrentPositionChanged(this, eventArgs);
            }
        }
        public virtual long VideoLength
        {
            get { return 0; }
        }
        public bool Playing
        {
            get
            {
                if (m_VideoSurfaceAction == null)
                    return false;
                return m_VideoSurfaceAction.Playing;
            }
        }
        /// <summary>
        /// play the curent audio video file
        /// </summary>
        public void Play()
        {
            if (!this.Playing)
            {
                this.m_VideoSurfaceAction.Play();
            }
            else
            {
                this.Pause();
            }
        }
        public void Pause()
        {
            if (this.Playing)
            {
                this.VideoSurfaceAction.Pause();
            }
        }
        public void Stop()
        {
            this.VideoSurfaceAction.Stop();
            this.Timer.Enabled = false;
        }
        public void Return()
        {
            //play rewing
            this.VideoSurfaceAction.Return();
        }
        public void JumpTo(TimeSpan time)
        {
            this.VideoSurfaceAction.JumpTo(time);
        }
        public abstract class VideoPlayerActionBase : IVideoDefaultAction
        {
            XVideoSurfaceBase videoSurface;
            protected bool m_playing;
            public XVideoSurfaceBase CurrentSurface
            {
                get { return videoSurface; }
            }
            public abstract void Dispose();
            protected VideoPlayerActionBase(XVideoSurfaceBase surface)
                : base()
            {
                this.videoSurface = surface;
            }
            #region IVideoDefaultAction Members
            public bool Playing
            {
                get { return this.m_playing; }
            }
            public abstract void Play();
            public abstract void Pause();
            public abstract void Stop();
            public abstract void Return();
            public abstract void JumpTo(TimeSpan time);
            #endregion
        }
        public virtual void SaveTo(string filename) { }
        public virtual void CaptureImageTo(string filaname) { }
        public DrSStudio.Codec.ICoreProject ProjectInfo
        {
            get { return null; }
        }
        public Resources.ICoreResourceContainer Resources
        {
            get { return null; }
        }
        public string GetDefaultFilter()
        {
            return "Video Files | *.avi";
        }
        public ICoreSaveAsInfo GetSaveAsInfo()
        {
            return new CoreSaveAsInfo("Save Video File",
                this.GetDefaultFilter(),
                this.FileName);
        }
    }
}

