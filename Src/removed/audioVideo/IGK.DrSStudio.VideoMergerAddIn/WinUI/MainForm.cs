

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: MainForm.cs
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
file:MainForm.cs
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio.WinUI
{
    public partial class MainForm : Form, IMergeForm
    {
        VideoFileProject m_project;
        XVideoFileControl c_vidcontrol;
        public MainForm()
        {
            InitializeComponent();
            m_project = new VideoFileProject();
            c_vidcontrol = new XVideoFileControl();
            this.panel2.Controls.Add(c_vidcontrol);
            m_project.ItemAdded += m_project_ItemAdded;
            m_project.ItemRemoved += m_project_ItemRemoved;
        }
        void m_project_ItemRemoved(object sender, VideoItemEventArgs<IVideoFile> e)
        {
        }
        void m_project_ItemAdded(object sender, VideoItemEventArgs<IVideoFile> e)
        {
            c_vidcontrol.Items.Add(e.Item as VideoFile);
            c_vidcontrol.Refresh();
        }
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        MergerProgression progress;
        private void generate_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Video Files | *.avi; | Audio File | *.wav; *.mp3| All Files| *.*";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;
                    progress = new MergerProgression(sfd.FileName,
                        this, 
                        m_project);
                    progress.BeginGenerate();                    
                    this.Cursor = Cursors.Default;
                }
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.LoadTitle();
        }
        private void addVideoFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Video Files | *.avi; | Audio File | *.wav; *.mp3;| All Files| *.*";
                ofd.Multiselect = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    for (int i = 0; i < ofd .FileNames.Length; i++)
                    {
                        VideoFile vid = VideoFile.CreateFromFile(ofd.FileNames[i]);
                        if (vid == null)
                            return;
                        if ((this.m_project.VideoFiles.Count == 0) ||
                        this.m_project.CanAdd(vid))
                            this.m_project.VideoFiles.Add(vid);
                    }
                }
            }
        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.m_project.VideoFiles.Clear();
            this.c_vidcontrol.Items.Clear();
        }
        #region IMergeForm Members
        public void LoadTitle()
        {
            this.Text = CoreSystem.GetString("IGK.MERGEVID_TITLE");
        }
        #endregion
        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //edit audio Codec
            IGK.AudioVideo.AVI.AVIFile f = null;// IGK.AudioVideo.AVI.AVIFile.CreateFileFromStreams(null);
            f = IGK.AudioVideo.AVI.AVIFile.Open("tmp.avi", IGK.AudioVideo.AVI.enuAviAccess.Create | IGK.AudioVideo.AVI.enuAviAccess.ReadWrite);
            if (f.AddEmptyWave())
            {
                IGK.AudioVideo.AVI.AVIFile.AudioStream aud = f.GetAudioStream();
                //IGK.AudioVideo.AVI.AVIEditableStream es =
                //    IGK.AudioVideo.AVI.AVIEditableStream.CreateEditableWaveStream();
                if (this.m_project.AudioCodec == null)
                    IGK.AudioVideo.AVI.AVIStream.FreeChooseCodec(this.m_project.AudioCodec);
                this.m_project .AudioCodec = IGK.AudioVideo.AVI.AVIStream.ChooseCodec(this.Handle ,aud.Handle);
                IGK.AudioVideo.AVI.AVIStream.FreeChooseCodec(this.m_project.AudioCodec );
                //es.Dispose();
                aud.Dispose();
            }
            f.Dispose();
            System.IO.File.Delete("tmp.avi");
        }
        private void videoPrefrencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //edit audio Codec
            try
            {
                IGK.AudioVideo.AVI.AVIFile f = null;// IGK.AudioVideo.AVI.AVIFile.CreateFileFromStreams(null);
                f = IGK.AudioVideo.AVI.AVIFile.Open("tmp.avi", 
                    IGK.AudioVideo.AVI.enuAviAccess.Create | IGK.AudioVideo.AVI.enuAviAccess.ReadWrite);
                if (f != null)
                {
                    IGK.AudioVideo.AVI.AVIFile.VideoStream vid = f.AddVideoStream(24, 1, 0, IGK.AudioVideo.AVI.enuAVIVideoFormat.Format24, 100, 100);
                    if (vid != null)
                    {
                        //IGK.AudioVideo.AVI.AVIEditableStream es =
                        //    IGK.AudioVideo.AVI.AVIEditableStream.CreateEditableWaveStream();
                        if (this.m_project.VideoCodec == null)
                            IGK.AudioVideo.AVI.AVIStream.FreeChooseCodec(this.m_project.VideoCodec);
                        this.m_project.VideoCodec = IGK.AudioVideo.AVI.AVIStream.ChooseCodec(this.Handle, vid.Handle);
                        IGK.AudioVideo.AVI.AVIStream.FreeChooseCodec(this.m_project.VideoCodec);
                        vid.Dispose();
                    }
                }
                f.Dispose();
                System.IO.File.Delete("tmp.avi");
            }
            catch (Exception Exception)
            {
                CoreLog.WriteDebug(Exception.Message);
            }
        }
    }
}

