

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XAudioBuilderSurface.cs
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
file:XAudioBuilderSurface.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.IO;
using System.Drawing;
namespace IGK.DrSStudio.AudioBuilder.WinUI
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.AudioVideo;
    using IGK.AudioVideo.ACM;
    using IGK.AudioVideo.AVI;
    [CoreSurface("AudioBuilder")]
    public class XAudioBuilderSurface : CoreWorkingSurface ,
        ICoreWorkingSurface,
        IAudioBuilderSurface,
        ICoreWorkingFilemanagerSurface,
        ICoreWorkingConfigElementSurface
    {
        private string m_ProjectName;
        private IAudioBuilderTrackCollections m_audioTracks;
        //CONTROLS
        private XTrackItem c_CurrentTrack;
        private TimeLineCursor c_timeLineCursor;
        private XTimeLineEditor c_editor;
        private int m_Position;
        private int m_duration;
        private bool m_isPlaying;
        private int m_Scale;
        [DefaultValue(1000)]
        /// <summary>
        /// get the milisecond scale of this surface
        /// </summary>
        public int TimeScale
        {
            get { return m_Scale; }
            set
            {
                if ((m_Scale != value) && (value > 0))
                {
                    m_Scale = value;
                    OnScaleChanged(EventArgs.Empty);
                }
            }
        }
        private void OnScaleChanged(EventArgs eventArgs)
        {
            if (this.ScaleChanged != null)
                this.ScaleChanged(this, eventArgs);
        }
        //EVENTS 
        public event EventHandler ScaleChanged;
        public event EventHandler PositionChanged;
        [Category("MixerTrack")]
        [Description("<res></res>Get or set the audio track name")]
        /// <summary>
        /// get or set the cursor position
        /// </summary>
        public int Position
        {
            get { return m_Position; }
            set
            {
                if (m_Position != value)
                {
                    m_Position = value;
                    OnPositionChanged(EventArgs.Empty);
                }
            }
        }
        [Category("MixerTrack")]
        [Description("<res></res>Get or set project audio duration")]
        public int Duration
        {
            get
            {
                return m_duration;
            }
            set
            {
                m_duration = value;
            }
        }
        protected virtual void OnPositionChanged(EventArgs eventArgs)
        {
            if (PositionChanged != null)
                this.PositionChanged(this, eventArgs);
        }
        private enuTrackMode m_AudioMode;
        public enuTrackMode AudioMode
        {
            get { return m_AudioMode; }
            set
            {
                if (m_AudioMode != value)
                {
                    m_AudioMode = value;
                }
            }
        }
        public event TrackEventHandler TrackRemoved;
        public event TrackEventHandler TrackAdded;
        #region ICoreWorkingObjectPropertyEvent Members
        public event CoreWorkingObjectPropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs e)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, e);
        }
        #endregion
        ///<summary>
        ///raise the TrackAdded 
        ///</summary>
        protected virtual void OnTrackAdded(TrackEventArgs e)
        {
            if (TrackAdded != null)
                TrackAdded(this, e);
        }
        ///<summary>
        ///raise the TrackRemoved 
        ///</summary>
        protected virtual void OnTrackRemoved(TrackEventArgs e)
        {
            if (TrackRemoved != null)
                TrackRemoved(this, e);
        }
        public XTrackItem CurrentTrack
        {
            get { return c_CurrentTrack; }
            set
            {
                if (c_CurrentTrack != value)
                {
                    c_CurrentTrack = value;
                    OnCurrentTrackChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler CurrentTrackChanged;
        ///<summary>
        ///raise the CurrentTrackChanged 
        ///</summary>
        protected virtual void OnCurrentTrackChanged(EventArgs e)
        {
            if (CurrentTrackChanged != null)
                CurrentTrackChanged(this, e);
        }
        /// <summary>
        /// get or set the project name
        /// </summary>
        public string ProjectName
        {
            get { return m_ProjectName; }
            set
            {
                if (m_ProjectName != value)
                {
                    m_ProjectName = value;
                }
            }
        }
        public XAudioBuilderSurface()
        {
            this.m_Scale = 1000;
            this.m_audioTracks = CreateTrackCollection();
            this.c_editor = new XTimeLineEditor(this);
            this.c_timeLineCursor = new TimeLineCursor(this);
            this.c_editor.Dock = DockStyle.Top;
            this.SuspendLayout();
            this.Controls.Add(this.c_editor);
            this.ResumeLayout();
            this.CurrentTrackChanged += new EventHandler(XAudioBuilderSurface_CurrentTrackChanged);
            this.Paint += new PaintEventHandler(_Paint);
            this.AllowDrop = true;
            this.Click += new EventHandler(XAudioBuilderSurface_Click);
        }
        void XAudioBuilderSurface_Click(object sender, EventArgs e)
        {
            this.CurrentTrack = null;
        }
        void _Paint(object sender, PaintEventArgs e)
        {
            Rectangle v_rc = this.ClientRectangle;
            int y = v_rc.Y + AudioConstant.TIMELINE_HEIGHT;
            while (y < v_rc.Bottom)
            {
                y += AudioConstant.TRACK_ITEM_HEIGHT;
                e.Graphics.DrawLine(CoreBrushRegister.GetPen(Colorf.DarkGray), 0, y, v_rc.Right, y);
            }
        }
        void XAudioBuilderSurface_CurrentTrackChanged(object sender, EventArgs e)
        {
            this.ElementToConfigure = this.CurrentTrack;
        }
        public void OpenFile(string filename)
        {
            string ext = Path.GetExtension(filename).ToLower();
            XProgressForm frm = null;
            switch (ext)
            {
                default:
                case ".avi":
                case ".wav":
                    AVIFile f = AVIFile.Open(filename);
                    AudioSequenceBase seq = null;
                    if (f != null)
                    {//
                        AVIFile.AudioStream stream = f.GetAudioStream();
                        if (stream != null)
                        {
                            //single file available
                            frm = new XProgressForm();
                            using (IGK.DrSStudio.WinUI.ICoreDialogForm ctr = Workbench.CreateNewDialog(frm))
                            {
                                ctr.Owner = Workbench.MainForm as System.Windows.Forms.Form;
                                ctr.StartPosition = FormStartPosition.CenterParent;
                                CallBackProgress c = new CallBackProgress(frm);
                                frm.CancelClick += new EventHandler(c.CancelClick);
                                string v_filename = System.IO.Path.GetTempFileName() + ".wav";
                                ctr.Show();
                                stream.ExportToWaveFile(v_filename, c.Progess, false, IntPtr.Zero);
                                ctr.Close();
                                DialogResult dl = ctr.DialogResult;
                                if (dl != DialogResult.Cancel)
                                {
                                    seq = new AudioWavFileSequence(v_filename);
                                    this.AddSequence(seq);
                                }
                            }
                        }
                    }
                    break;
                case ".mp3":
                    //single file available
                    break;
            }
        }
        internal void InsertAudioTrack(string filename)
        {
            string ext = Path.GetExtension(filename).ToLower();
            AudioWavFileSequence seq = new AudioWavFileSequence(FileName);
            XTrackItem track = new XTrackItem(CreateAudioTrack(), this);
            track.Sequences.Add(seq);
        }
        public XTrackItem AddNewTrack()
        {
            XTrackItem track = new XTrackItem(CreateAudioTrack(), this);
            return track;
        }
        /// <summary>
        /// add sequence to current track
        /// </summary>
        /// <param name="seq"></param>
        private void AddSequence(AudioSequenceBase seq)
        {
            XTrackItem track = this.CurrentTrack;
            if (track == null)
            {
                //if (this.Tracks.Count == 0)
                //{
                track = AddNewTrack();
                // }
                //else {
                //    track = (XTrackItem)this.Tracks[0];
                //}
                this.CurrentTrack = track;
            }
            track.Sequences.Add(seq);
        }
        protected virtual AudioTrackBase CreateAudioTrack()
        {
            return new AudioTrack();
        }
        public virtual IAudioBuilderTrackCollections CreateTrackCollection()
        {
            return new AudioFileCollections(this);
        }
        /// <summary>
        /// rerpresent the audio file 
        /// </summary>
        public class AudioFileCollections : IAudioBuilderTrackCollections
        {
            List<IAudioTrack> m_audioTracks;
            XAudioBuilderSurface m_audioBuilder;
            internal XAudioBuilderSurface Owner { get { return this.m_audioBuilder; } }
            /// <summary>
            /// Get the audio track
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            public IAudioTrack this[int index] { get { return m_audioTracks[index]; } }
            public AudioFileCollections(XAudioBuilderSurface builder)
            {
                this.m_audioBuilder = builder;
                this.m_audioTracks = new List<IAudioTrack>();
            }
            public override string ToString()
            {
                return string.Format("Tracks [0]", this.Count);
            }
            #region ICoreCountEnumerable Members
            public int Count
            {
                get { return this.m_audioTracks.Count; }
            }
            #endregion
            #region IEnumerable Members
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_audioTracks.GetEnumerator();
            }
            #endregion
            #region IAudioBuilderTrackCollections Members
            public void AddTrack(IAudioTrack track)
            {
                if ((track == null) || (m_audioTracks.Contains(track)) || (Count >= AudioBuilderInfo.MaxAvailableSystemTrack))
                    return;
                this.m_audioTracks.Add(track);
            }
            public void RemoveTrack(IAudioTrack track)
            {
                if (m_audioTracks.Contains(track))
                {
                    this.m_audioTracks.Remove(track);
                    track.Dispose();
                }
            }
            #endregion
            #region IAudioBuilderTrackCollections Members
            public int IndexOf(IAudioTrack xTrackItem)
            {
                return this.m_audioTracks.IndexOf(xTrackItem);
            }
            #endregion
        }
        #region ICoreWorkingSurface Members
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        [System.ComponentModel.Browsable(false)]
        public override string SurfaceEnvironment
        {
            get { return AudioConstant.ENVIRONMENT; }
        }
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        [System.ComponentModel.Browsable(false)]
        public override string DisplayName
        {
            get { return AudioConstant.DISPLAY_NAME; }
        }
        #endregion
        [Category("MixerTrack")]
        [Description("get the track name collection")]
        [TypeConverter(typeof(TrackConverter))]
        [Editor(typeof(TrackEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public IAudioBuilderTrackCollections Tracks
        {
            get { return this.m_audioTracks; }
        }
        public event EventHandler ProjectNameChanged;
        ///<summary>
        ///raise the ProjectNameChanged 
        ///</summary>
        protected virtual void OnProjectNameChanged(EventArgs e)
        {
            if (ProjectNameChanged != null)
                ProjectNameChanged(this, e);
        }
        #region ICoreWorkingFilemanagerSurface Members
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        [System.ComponentModel.Browsable(false)]
        public string FileName
        {
            get { return this.DisplayName; }
        }
        public virtual void RenameTo(string newfilename)
        {
        }
#pragma warning disable
        public event EventHandler FileNameChanged;
        #endregion
        #region ICoreWorkingRecordableSurface Members
        public bool NeedToSave
        {
            get { return true; }
        }
        public event EventHandler NeedToSaveChanged;
        public void Save()
        {
            SaveAs(this.FileName  );
        }
        public void SaveAs(string filename)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "audio output| *.wav";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    SaveAllTrackTo(sfd.FileName, true);
                }
            }
        }
        private void SaveAllTrackTo(string filename, bool mergin)
        {
            AVIEditableStream edit = null;
            AVIEditableStream v_e = null;
            foreach (IAudioTrack track in this.Tracks)
            {
                v_e = track.GetEditableStream();
                if (edit == null)
                    edit = AVIEditableStream.Create(v_e);
                else
                    edit.Paste(v_e,
                        0,
                        (int)v_e.Length);
                v_e.Dispose();
            }
            AVIFile f = AVIFile.CreateFileFromStreams(edit.Handle);
            f.SaveTo(filename, null);
            f.Dispose();
            edit.Dispose();
        }
        #endregion
        internal void Play()
        {
            if (this.m_ElementToConfigure != null)
                this.m_ElementToConfigure.Play();
            else
            {
            }
        }
        internal void Pause()
        {
            if (this.ElementToConfigure != null)
                this.m_ElementToConfigure.Stop();
        }
        #region ICoreWorkingConfigElementSurface Members
        public ITrack m_ElementToConfigure;
        public ICoreWorkingObject ElementToConfigure
        {
            get
            {
                return m_ElementToConfigure;
            }
            set
            {
                ITrack c = value as ITrack;
                if (m_ElementToConfigure != c)
                {
                    m_ElementToConfigure = c;
                    OnElementToConfigureChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler ElementToConfigureChanged;
        #endregion
        protected virtual void OnElementToConfigureChanged(EventArgs e)
        {
            if (this.ElementToConfigureChanged != null)
                this.ElementToConfigureChanged(this, e);
        }
        class CallBackProgress
        {
            internal readonly XProgressForm m_frm;
            bool m_isCancel;
            public CallBackProgress(XProgressForm frm)
            {
                this.m_frm = frm;
            }
            internal void CancelClick(Object sender, EventArgs e)
            {
                this.m_isCancel = true;
            }
            internal long Progess(int i)
            {
                this.m_frm.Value = i;
                Application.DoEvents();
                if (this.m_isCancel)
                    return -1;
                return 0;
            }
        }
        class TrackConverter : TypeConverter
        {
            public TrackConverter()
            {
            }
            public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
            {
                return base.GetCreateInstanceSupported(context);
            }
        }
        class TrackEditor : System.Drawing.Design.UITypeEditor
        {
            public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
            {
                return System.Drawing.Design.UITypeEditorEditStyle.Modal;
            }
            public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
            {
                AudioFileCollections f = value as AudioFileCollections;
                if (f.Owner != null)
                {
                    using (OpenFileDialog ofd = new OpenFileDialog())
                    {
                        if (ofd.ShowDialog() == DialogResult.OK)
                        {
                            try
                            {
                                f.Owner.OpenFile(ofd.FileName);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.StackTrace);
                            }
                        }
                    }
                }
                return value;
            }
        }
        internal void ExportMix()
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                }
            }
        }
        /// <summary>
        /// stop the current playing
        /// </summary>
        internal void Stop()
        {
            if (m_isPlaying)
            {
                this.m_isPlaying = false;
            }
        }
        /// <summary>
        /// configure audio setting
        /// </summary>
        internal void Configure()
        {
        }
        public string GetDefaultFilter()
        {
            return "Audio Files | *.wav";
        }
        public ICoreSaveAsInfo GetSaveAsInfo()
        {
            return new CoreSaveAsInfo("Save Video File",
                this.GetDefaultFilter(),
                this.FileName);
        }
    }
}

