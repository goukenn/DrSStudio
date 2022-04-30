

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidWizardGUI.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.IO;
using IGK.ICore.WinUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinCore.WinUI.Controls;

namespace IGK.DrSStudio.Android.WinUI
{
    public class AndroidWizardGUI : IGKXUserControl
    {
        private AndroidProject m_project;
        private string m_ProjectName;
        private string m_ProjectLocation;
        private IGKXPanel c_pan_content;
        private IGKXButton c_btn_next;
        private IGKXButton c_btn_previous;
        private IGKXButton c_btn_finish;        
        private IGKXPanel c_pan_navigation;
        private WizardPage m_WizarPage;

        public WizardPage WizardPageControl
        {
            get { return m_WizarPage; }
            set
            {
                if (m_WizarPage != value)
                {
                    m_WizarPage = value;
                    OnWizardPageChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler WizardPageChanged;
        ///<summary>
        ///raise the WizardPageChanged 
        ///</summary>
        protected virtual void OnWizardPageChanged(EventArgs e)
        {
            if (WizardPageChanged != null)
                WizardPageChanged(this, e);
        }


        public string ProjectLocation
        {
            get { return m_ProjectLocation; }
            set
            {
                if (m_ProjectLocation != value)
                {
                    m_ProjectLocation = value;
                }
            }
        }
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

        
        /// <summary>
        /// get the android project
        /// </summary>
        public AndroidProject Project { get { return this.m_project; } }
        public AndroidWizardGUI()
        {
            this.InitializeComponent();
            this.WizardPageChanged += _WizardPageChanged;
            this.Load += _Load;
            
        }

        private void _Load(object sender, EventArgs e)
        {
            this.WizardPageControl = new WizardPage() { 
                HostControl = new AndroidAppPage(this) 
            };
        }

        void _WizardPageChanged(object sender, EventArgs e)
        {
            this.c_pan_content.SuspendLayout();
            this.c_pan_content.Controls.Clear();
            this.WizardPageControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_pan_content.Controls.Add(this.WizardPageControl);
            this.c_pan_content.ResumeLayout();
            this.UpdateButtons();
        }

        private void UpdateButtons()
        {
            this.c_btn_previous.Enabled = this.WizardPageControl.CanGoToNext();
            this.c_btn_next.Enabled = this.WizardPageControl.CanGoToPrevious();
            this.c_btn_finish.Enabled = true;
        }
        public void Finish()
        {
            if (string.IsNullOrEmpty(this.ProjectLocation) ||
                string.IsNullOrEmpty(this.ProjectName))
                return;
            string dir = System.IO.Path.Combine(this.ProjectLocation, this.ProjectName);
            if (PathUtils.CreateDir(dir))
            {
                this.m_project = AndroidProject.CreateProject(
                    this.ProjectName,
                    dir,
                    this.PackageName,
                    this.MainActivity,
                    this.AndroidPlatformTarget,
                    this.AndroidMinTarget );
                if (this.m_project != null)
                {
                    this.FindForm().DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }

        }

        private void InitializeComponent()
        {
            this.c_pan_content = new IGKXPanel();
            this.c_pan_navigation = new IGKXPanel();
            this.c_btn_previous = new IGKXButton();
            this.c_btn_next = new IGKXButton();
            this.c_btn_finish = new IGKXButton();
            this.c_pan_navigation.SuspendLayout();
            this.SuspendLayout();
            // 
            // c_pan_content
            // 
            this.c_pan_content.AutoScroll = true;
            this.c_pan_content.CaptionKey = null;
            this.c_pan_content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_pan_content.Location = new System.Drawing.Point(0, 0);
            this.c_pan_content.Name = "c_pan_content";
            this.c_pan_content.Size = new System.Drawing.Size(591, 259);
            this.c_pan_content.TabIndex = 0;
            // 
            // c_pan_navigation
            // 
            this.c_pan_navigation.CaptionKey = null;
            this.c_pan_navigation.Controls.Add(this.c_btn_previous);
            this.c_pan_navigation.Controls.Add(this.c_btn_next);
            this.c_pan_navigation.Controls.Add(this.c_btn_finish);
            this.c_pan_navigation.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.c_pan_navigation.Location = new System.Drawing.Point(0, 259);
            this.c_pan_navigation.Name = "c_pan_navigation";
            this.c_pan_navigation.Size = new System.Drawing.Size(591, 42);
            this.c_pan_navigation.TabIndex = 0;
            // 
            // c_btn_previous
            // 
            this.c_btn_previous.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.c_btn_previous.CaptionKey = "btn.previous";
            this.c_btn_previous.Checked = false;
            this.c_btn_previous.DialogResult = enuDialogResult.None;
            this.c_btn_previous.Location = new System.Drawing.Point(222, 6);
            this.c_btn_previous.Name = "c_btn_previous";
            this.c_btn_previous.ShowButtonImage = false;
            this.c_btn_previous.Size = new System.Drawing.Size(115, 32);
            this.c_btn_previous.State = enuButtonState.Normal;
            this.c_btn_previous.TabIndex = 2;
            // 
            // c_btn_next
            // 
            this.c_btn_next.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.c_btn_next.CaptionKey = "btn.next";
            this.c_btn_next.Checked = false;
            this.c_btn_next.DialogResult = enuDialogResult.None;
            this.c_btn_next.Location = new System.Drawing.Point(343, 6);
            this.c_btn_next.Name = "c_btn_next";
            this.c_btn_next.ShowButtonImage = false;
            this.c_btn_next.Size = new System.Drawing.Size(115, 32);
            this.c_btn_next.State = enuButtonState.Normal;
            this.c_btn_next.TabIndex = 1;
            this.c_btn_next.Click += new System.EventHandler(this.c_btn_next_Click);
            // 
            // c_btn_finish
            // 
            this.c_btn_finish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.c_btn_finish.CaptionKey = "btn.finish";
            this.c_btn_finish.Checked = false;
            this.c_btn_finish.DialogResult = enuDialogResult.None;
            this.c_btn_finish.Location = new System.Drawing.Point(464, 6);
            this.c_btn_finish.Name = "c_btn_finish";
            this.c_btn_finish.ShowButtonImage = false;
            this.c_btn_finish.Size = new System.Drawing.Size(115, 32);
            this.c_btn_finish.State = enuButtonState.Normal;
            this.c_btn_finish.TabIndex = 0;
            this.c_btn_finish.Click += new System.EventHandler(this.c_btn_finish_Click);
            // 
            // AndroidWizardGUI
            // 
            this.Controls.Add(this.c_pan_content);
            this.Controls.Add(this.c_pan_navigation);
            this.Name = "AndroidWizardGUI";
            this.Size = new System.Drawing.Size(591, 301);
            this.c_pan_navigation.ResumeLayout(false);
            this.ResumeLayout(false);

        }

      

        private void c_btn_finish_Click(object sender, EventArgs e)
        {
            this.Finish();
        }
        /// <summary>
        /// represent a wizad page
        /// </summary>
        public class WizardPage : IGKXUserControl 
        {
            public IGKXUserControl HostControl { get; set; }
            public WizardPage Previous { get; set; }
            public WizardPage Next { get; set; }

            public WizardPage()
            {
                this.Load += _Load;
            }

            void _Load(object sender, EventArgs e)
            {
                if (this.HostControl != null)
                {
                    this.HostControl.Dock = System.Windows.Forms.DockStyle.Fill;
                    this.Controls.Add(this.HostControl);
                }
            }
            public virtual bool CanGoToNext()
            {
                return (this.Next != null);
            }
            public virtual bool CanGoToPrevious()
            {
                return (this.Previous != null);
            }
        }
        
        class WizardPageCollection : IEnumerable 
        {
            List<WizardPage> m_pages;
            public WizardPageCollection(){ 
 
                this.m_pages = new List<WizardPage> ();
}
            public IEnumerator GetEnumerator()
            {
                return m_pages.GetEnumerator ();
            }
        }

        private void c_btn_next_Click(object sender, EventArgs e)
        {
            if (this.WizardPageControl.CanGoToNext())
            {
                this.WizardPageControl = this.WizardPageControl.Next;
            }
        }

        public AndroidTargetInfo AndroidMinTarget { get; set; }
        public string MainActivity { get; set; }
        /// <summary>
        /// get or set the platform target
        /// </summary>
        public AndroidTargetInfo AndroidPlatformTarget { get; set; }

        public string PackageName { get; set; }
    }
}
