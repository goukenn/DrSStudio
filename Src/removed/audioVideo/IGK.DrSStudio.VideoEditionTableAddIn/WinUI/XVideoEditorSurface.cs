

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XVideoEditorSurface.cs
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
file:XVideoEditorSurface.cs
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio.VideoEditionTableAddIn.WinUI
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.AudioVideo;
    using IGK.AudioVideo.AVI ;
    /// <summary>
    /// represent the video editor surface
    /// </summary>
    [IGK.DrSStudio.CoreSurface (VideoConstant .VIDEOEDITOR_SURFACENAME )]
    public partial class XVideoEditorSurface : 
        CoreWorkingSurface, 
        IVideoEditorSurface ,
        ICoreWorkingRecordableSurface 
    {
        private IVideoImportedFile  m_CurrentFile;
        /// <summary>
        /// get or set the current file
        /// </summary>
        public IVideoImportedFile  CurrentFile
        {
            get { return m_CurrentFile; }
            set
            {
                if (m_CurrentFile != value)
                {
                    m_CurrentFile = value;
                    OnCurrentImportedFileChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler CurrentImpotedFileChanged;
        /// <summary>
        /// raise the current file impoted event
        /// </summary>
        /// <param name="eventArgs"></param>
        protected virtual void OnCurrentImportedFileChanged(EventArgs eventArgs)
        {
            if (this.CurrentImpotedFileChanged != null)
            {
                this.CurrentImpotedFileChanged(this, eventArgs);    
                    }
        }
        public XVideoEditorSurface()
        {
            InitializeComponent();
            this.c_videoPlayer.Surface = this;
            this.c_timeLineManager.Surface = this;            
            this.FileRemove += new ImportedFileEventHandler(XVideoEditorSurface_FileRemove);
            this.FileImported += new ImportedFileEventHandler(XVideoEditorSurface_FileImported);
            ///register video time line
            this.c_timeLineManager.Controls.Add(new XVideoTimeLine());
            this.c_timeLineManager.Controls.Add(new XVideoSoundTimeLine());
            this.c_timeLineManager.Controls.Add(new XVideoDocumentTimeLine());
        }
        void XVideoEditorSurface_FileImported(object sender, ImportedFileEventArgs e)
        {
            string v_file = System.IO.Path.GetFullPath(e.File.FileName);
            ListViewItem v_item = new ListViewItem (System.IO.Path.GetFileName (v_file));
            v_item.Name = v_file.ToLower() ;
            v_item.Tag = e.File;
            if (e.File.HasVideo)
            {
                using (AVIFile.VideoStream vid = e.File.File.GetVideoStream())
                {
                    Bitmap bmp = Utils.Utils.GetPicture(vid, vid.Length /100);
                    if (bmp != null)
                    {
                        string f = Guid.NewGuid().ToString();
                        this.c_imageList1.Images.Add(f, new Bitmap(bmp, new Size(64, 64)));
                        bmp.Dispose();
                        v_item.ImageKey = f;
                    }
                }
            }
            this.c_listView1 .Items .Add (v_item );
        }
        void XVideoEditorSurface_FileRemove(object sender, ImportedFileEventArgs e)
        {//remove items
            ListViewItem v_c = null;
            foreach (ListViewItem c in this.c_listView1.Items)
	{
                if (c.Tag == e.File ){
                    v_c = c; break ;
                }
	}
            if (v_c != null)
            {
                this.c_listView1.Items.Remove(v_c);
                this.c_imageList1.Images.RemoveByKey(v_c.ImageKey);
            }
        }
        void _Load(object sender, EventArgs e)
        {
        }
        #region IVideoEditorSurface Members
        IVideoProject m_VideoProject;
        public IVideoProject VideoProject
        {
            get
            {
                return m_VideoProject;
            }
            set
            {
                if (m_VideoProject != value)
                {
                    m_VideoProject = value;
                    m_VideoProject.FileImported += new ImportedFileEventHandler(m_VideoProject_FileImported);
                    m_VideoProject.FileRemove += new ImportedFileEventHandler(m_VideoProject_FileRemove);
                }
            }
        }
        void m_VideoProject_FileRemove(object sender, ImportedFileEventArgs e)
        {
            OnFileRemove(e);
        }
        void m_VideoProject_FileImported(object sender, ImportedFileEventArgs e)
        {
            OnFileImported(e);
        }
        public event ImportedFileEventHandler FileImported;
        public event ImportedFileEventHandler FileRemove;
        internal protected virtual void OnFileImported(ImportedFileEventArgs e)
        {
            if (this.FileImported != null)
                this.FileImported(this, e);
        }
        internal protected virtual void OnFileRemove(ImportedFileEventArgs e)
        {
            if (this.FileRemove != null)
                this.FileRemove(this, e);
        }
        public bool SaveProject(string filename)
        {
            try
            {
                this.VideoProject.SaveProject(filename);
            }
            catch {
                return false;
            }
            return true;
        }
        public virtual bool BuildVideoProject(string filename)
        {
            //VideoProjectBuilder v_proj = new VideoProjectBuilder();
            //if (this.Workbench.ConfigureWorkingObject(v_proj) == DialogResult.OK)
            //{
            //    return v_proj.Build(filename);
            //}
            return false;
        }
        #endregion
        #region ICoreWorkingSurface Members
        public override string DisplayName
        {
            get {
                if (VideoProject != null)
                {
                    return VideoProject.ProjectName;
                }
                return VideoConstant.EMPTY_PRJ_NAME;
            }
        }
        public override  string SurfaceEnvironment
        {
            get { return VideoConstant.VIDEO_ENVIRONMENT; }
        }
        #endregion
        #region ICoreWorkingObjectPropertyEvent Members
        public event CoreWorkingObjectPropertyChangedEventHandler PropertyChanged;
        #endregion
        void OnPropertyChanged(EventArgs e)
        {
            if (this.PropertyChanged !=null)
            {
                this.PropertyChanged.Invoke(this, e as CoreWorkingObjectPropertyChangedEventArgs);
            }
        }
        #region IVideoEditor Members
        public virtual bool Import(string filename)
        {
            string s= System.IO.Path.GetFullPath(filename).ToLower();
            if ((System.IO.File.Exists(s) == false)||(this.c_listView1.Items.ContainsKey (s)))
                return false;
            IVideoImportedFile v_items = VideoImportedFile.Create(filename);            
            if (v_items != null)
            {
                this.ImportedFiles.Add(v_items);
                return true;
            }
            return false;
        }
        #endregion
        public Bitmap GetPlayerDisplayedImage() { return this.c_videoPlayer.GetDisplayedBitmap(); }
        #region IVideoEditorSurface Members
        /// <summary>
        /// Get the imported file
        /// </summary>
        public IVideoImportedFileCollections ImportedFiles
        {
            get {
                if (VideoProject !=null)
                    return this.VideoProject.ImportedFiles;
                return null;
            }
        }
        #endregion
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (c_listView1.SelectedItems.Count == 1)
            {
                ListViewItem item = c_listView1.SelectedItems[0];
                this.CurrentFile = item.Tag as IVideoImportedFile;
            }
            else
                this.CurrentFile = null;
        }
        #region ICoreWorkingRecordableSurface Members
        bool m_NeedToSave;
        public bool NeedToSave
        {
            get { return m_NeedToSave; }
            private set {
                if (m_NeedToSave != value)
                {
                    m_NeedToSave = value;
                    OnNeedToSave(EventArgs.Empty);
                }
            }
        }
        private void OnNeedToSave(EventArgs eventArgs)
        {
            if (this.NeedToSaveChanged != null)
                this.NeedToSaveChanged(this, eventArgs);
        }
        public event EventHandler NeedToSaveChanged;
        public void Save()
        {
            SaveAs(this.CurrentFile.FileName);
        }
        public void SaveAs(string filename)
        {
            this.VideoProject.SaveProject(filename);
        }
        #endregion
        private void toolTip1_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
            e.DrawText(TextFormatFlags.WordEllipsis | TextFormatFlags.NoPadding);
        }
        private void xVideoPlayer1_Paint(object sender, PaintEventArgs e)
        {
        }
        internal void ClearSounds()
        {
        }
        internal void ClearVideos()
        {
        }
        public ICoreSaveAsInfo GetSaveAsInfo()
        {
            return new CoreSaveAsInfo("Save Video Project",
                "Video files | *.avi; *.mpeg; *.mp4",
                this.CurrentFile.FileName);
        }
    }
}

