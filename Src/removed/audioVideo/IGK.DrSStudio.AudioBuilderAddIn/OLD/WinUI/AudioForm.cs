

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AudioForm.cs
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
file:AudioForm.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel ;
using System.IO;
//[assembly: System.Security.Permissions .FileIOPermission (System.Security.Permissions.SecurityAction.RequestMinimum, Unrestricted = true )]
namespace IGK.DrSStudio.AudioBuilder.WinUI
{
    public class AudioForm : 
        IGK.DrSStudio.WinUI.XMainForm ,
        ICoreIdentifier
    {
        XAudioBuilderSurface  c_surface;
        /// <summary>
        /// get the current surface
        /// </summary>
        public XAudioBuilderSurface  CurrentSurface {
            get {
                return this.c_surface;
            }
        }
        public AudioForm()
        {
            this.InitializeComponent();
            this.InitControl();
        }
        private void InitControl()
        {
            this.Text = this.c_surface.DisplayName;
            this.m_Workbench = new AudioWorkbench (this);
        }
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportMixToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProjectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.volumeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.c_toolStripLabel_trackInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.c_toolStripLabel_CurrentTrack = new System.Windows.Forms.ToolStripStatusLabel();
            this.c_toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.c_surface = new XAudioBuilderSurface();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.actionToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(639, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem,
            this.exportMixToToolStripMenuItem,
            this.saveProjectsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.importToolStripMenuItem.Text = "&Import";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // exportMixToToolStripMenuItem
            // 
            this.exportMixToToolStripMenuItem.Name = "exportMixToToolStripMenuItem";
            this.exportMixToToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.exportMixToToolStripMenuItem.Text = "Export Mix To ...";
            this.exportMixToToolStripMenuItem.Click += new System.EventHandler(this.exportMixToToolStripMenuItem_Click);
            // 
            // saveProjectsToolStripMenuItem
            // 
            this.saveProjectsToolStripMenuItem.Name = "saveProjectsToolStripMenuItem";
            this.saveProjectsToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.saveProjectsToolStripMenuItem.Text = "Save Projects";
            this.saveProjectsToolStripMenuItem.Click += new System.EventHandler(this.saveProjectsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(155, 6);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.quitToolStripMenuItem.Text = "&Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // actionToolStripMenuItem
            // 
            this.actionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.toolStripMenuItem2,
            this.volumeToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.actionToolStripMenuItem.Name = "actionToolStripMenuItem";
            this.actionToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.actionToolStripMenuItem.Text = "&Action";
            this.actionToolStripMenuItem.Click += new System.EventHandler(this.actionToolStripMenuItem_Click);
            // 
            // playToolStripMenuItem
            // 
            this.playToolStripMenuItem.Name = "playToolStripMenuItem";
            this.playToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.playToolStripMenuItem.Text = "&Play";
            this.playToolStripMenuItem.Click += new System.EventHandler(this.playToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.stopToolStripMenuItem.Text = "&Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(113, 6);
            // 
            // volumeToolStripMenuItem
            // 
            this.volumeToolStripMenuItem.Name = "volumeToolStripMenuItem";
            this.volumeToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.volumeToolStripMenuItem.Text = "&Volume";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.optionsToolStripMenuItem.Text = "&Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.c_toolStripLabel_trackInfo,
            this.c_toolStripLabel_CurrentTrack,
            this.c_toolStripProgressBar,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 358);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(639, 22);
            this.statusStrip1.TabIndex = 2;
            // 
            // c_toolStripLabel_trackInfo
            // 
            this.c_toolStripLabel_trackInfo.Name = "c_toolStripLabel_trackInfo";
            this.c_toolStripLabel_trackInfo.Size = new System.Drawing.Size(0, 17);
            // 
            // c_toolStripLabel_CurrentTrack
            // 
            this.c_toolStripLabel_CurrentTrack.Name = "c_toolStripLabel_CurrentTrack";
            this.c_toolStripLabel_CurrentTrack.Size = new System.Drawing.Size(0, 17);
            // 
            // c_toolStripProgressBar
            // 
            this.c_toolStripProgressBar.Name = "c_toolStripProgressBar";
            this.c_toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            this.c_toolStripProgressBar.Visible = false;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(624, 17);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // c_surface
            // 
            this.c_surface.AllowDrop = true;
            this.c_surface.AudioMode =enuTrackMode.Merge;
            this.c_surface.CurrentTrack = null;
            this.c_surface.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_surface.Duration = 0;
            this.c_surface.ElementToConfigure = null;
            this.c_surface.Location = new System.Drawing.Point(0, 24);
            this.c_surface.Name = "c_surface";
            this.c_surface.Position = 0;
            this.c_surface.ProjectName = null;
            this.c_surface.TimeScale = 30000;
            this.c_surface.Size = new System.Drawing.Size(639, 334);
            this.c_surface.TabIndex = 0;
            this.c_surface.CurrentTrackChanged += new System.EventHandler(this.c_surface_CurrentTrackChanged);
            this.c_surface.TrackAdded += new TrackEventHandler(this.c_surface_TrackAdded);
            // 
            // AudioForm
            // 
            this.ClientSize = new System.Drawing.Size(639, 380);
            this.Controls.Add(this.c_surface);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "AudioForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Audio Builder MIX - v1.0";
            this.Load += new System.EventHandler(this.AudioForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem quitToolStripMenuItem;
        private ToolStripMenuItem importToolStripMenuItem;
        private ToolStripMenuItem exportMixToToolStripMenuItem;
        private ToolStripMenuItem saveProjectsToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem actionToolStripMenuItem;
        private ToolStripMenuItem playToolStripMenuItem;
        private ToolStripMenuItem stopToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem volumeToolStripMenuItem;        
        private ToolStripMenuItem optionsToolStripMenuItem;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel c_toolStripLabel_trackInfo;
        private ToolStripStatusLabel c_toolStripLabel_CurrentTrack;
        private ToolStripProgressBar c_toolStripProgressBar;
        private ToolStripStatusLabel toolStripStatusLabel1;
        #region ICoreMainForm Members
        IGK.DrSStudio.WinUI.ICoreWorkbench m_Workbench;
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
        }
        public string Title
        {
            get
            {
                return this.Text;
            }
            set
            {
                this.Text = value;
            }
        }
        #endregion
        #region ICoreIdentifier Members
        string ICoreIdentifier.Id
        {
            get { return this.Name ;}
        }
        #endregion
        private void AudioForm_Load(object sender, EventArgs e)
        {
            this.Text = AudioConstant.MAINFORM_TITLE;
            this.UpdateTrackInfo();
        }
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void saveProjectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IGK.DrSStudio.WinUI.ICoreWorkingProjectManagerSurface v_s = (this.c_surface as IGK.DrSStudio.WinUI.ICoreWorkingProjectManagerSurface);
            if (v_s != null)
            { 
                 using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(ofd.FileName);
                    IGK.DrSStudio.Codec.CoreXMLSerializer seri =
                        IGK.DrSStudio.Codec.CoreXMLSerializer.Create(writer);
                    v_s.ProjectInfo.Serialize(seri );
                    writer.Flush();
                    seri.Close();
                    writer.Close();
                }
            }
            }
        }
        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    this.c_surface.OpenFile(ofd.FileName);
                }
            }
        }
        private void exportMixToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.c_surface.ExportMix();
        }
        private void playToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.c_surface.Play();
        }
        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.c_surface.Stop();
        }
        private void actionToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.c_surface.Configure();
        }
        private void c_surface_TrackAdded(object sender, TrackEventArgs e)
        {
            UpdateTrackInfo();
        }
        private void UpdateTrackInfo()
        {
            this.c_toolStripLabel_trackInfo.Text = string.Format("Tracks:[{0}]", this.c_surface.Tracks.Count);
            this.c_toolStripLabel_CurrentTrack .Text = string.Format ("CurrentTrack : {0}",
                (c_surface.CurrentTrack == null)?"null":
                c_surface.CurrentTrack.Index.ToString() );
        }
        private void c_surface_CurrentTrackChanged(object sender, EventArgs e)
        {
            UpdateTrackInfo();
        }
    }
}

