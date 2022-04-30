

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebSolutionWizzardUserControl.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
﻿using IGK.DrSStudio.Web.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGK.ICore.WinCore.WinUI.Controls;

namespace IGK.DrSStudio.Web.WinUI
{
    class WebSolutionWizzardUserControl : IGKXUserControl
    {
        private IGKXPanel igkxPanel1;
        private IGKXTextBox c_outPutFolder;
        private IGKXTextBox c_projectname;
        private IGKXButton igkxButton1;
        private IGKXRuleLabel igkxRuleLabel1;
        private IGKXLabel igkxLabel2;
        private IGKXButton igkxButton2;
        private IGKXLabel igkxLabel1;
        private IGKXTextBox c_txb_companyPrefix;
        private IGKXLabel igkxLabel3;
        private Templates.WebSolutionConfigType webSolutionConfigType;

        public string CompanyPrefix {
            get {
                return this.c_txb_companyPrefix.Text;
            }
        }
        public WebSolutionWizzardUserControl()
        {
            this.InitializeComponent();
        }

        public WebSolutionWizzardUserControl(WebSolutionConfigType webSolutionConfigType)
        {
            this.InitializeComponent();
            this.webSolutionConfigType = webSolutionConfigType;
            this.c_outPutFolder.Text = webSolutionConfigType.OutputFolder;
            this.c_projectname.Text = webSolutionConfigType.Name;
            this.igkxButton1.DialogResult = enuDialogResult.None;
            this.igkxButton2.DialogResult = enuDialogResult.None;
        }

        private void InitializeComponent()
        {
            this.igkxPanel1 = new IGKXPanel();
            this.c_txb_companyPrefix = new IGKXTextBox();
            this.igkxLabel3 = new IGKXLabel();
            this.igkxButton2 = new IGKXButton();
            this.c_outPutFolder = new IGKXTextBox();
            this.c_projectname = new IGKXTextBox();
            this.igkxButton1 = new IGKXButton();
            this.igkxRuleLabel1 = new IGKXRuleLabel();
            this.igkxLabel2 = new IGKXLabel();
            this.igkxLabel1 = new IGKXLabel();
            this.igkxPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // igkxPanel1
            // 
            this.igkxPanel1.CaptionKey = null;
            this.igkxPanel1.Controls.Add(this.c_txb_companyPrefix);
            this.igkxPanel1.Controls.Add(this.igkxLabel3);
            this.igkxPanel1.Controls.Add(this.igkxButton2);
            this.igkxPanel1.Controls.Add(this.c_outPutFolder);
            this.igkxPanel1.Controls.Add(this.c_projectname);
            this.igkxPanel1.Controls.Add(this.igkxButton1);
            this.igkxPanel1.Controls.Add(this.igkxRuleLabel1);
            this.igkxPanel1.Controls.Add(this.igkxLabel2);
            this.igkxPanel1.Controls.Add(this.igkxLabel1);
            this.igkxPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.igkxPanel1.Location = new System.Drawing.Point(0, 0);
            this.igkxPanel1.Name = "igkxPanel1";
            this.igkxPanel1.Size = new System.Drawing.Size(358, 283);
            this.igkxPanel1.TabIndex = 0;
            // 
            // c_txb_companyPrefix
            // 
            this.c_txb_companyPrefix.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c_txb_companyPrefix.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c_txb_companyPrefix.Location = new System.Drawing.Point(106, 42);
            this.c_txb_companyPrefix.Name = "c_txb_companyPrefix";
            this.c_txb_companyPrefix.Size = new System.Drawing.Size(187, 21);
            this.c_txb_companyPrefix.TabIndex = 8;
            this.c_txb_companyPrefix.Text = "IGK";
            this.c_txb_companyPrefix.TipText = null;
            // 
            // igkxLabel3
            // 
            this.igkxLabel3.CaptionKey = "lb.companyPrefix";
            this.igkxLabel3.HorizontalAlignment = enuStringAlignment.Near;
            this.igkxLabel3.Location = new System.Drawing.Point(9, 42);
            this.igkxLabel3.Name = "igkxLabel3";
            this.igkxLabel3.Size = new System.Drawing.Size(91, 21);
            this.igkxLabel3.TabIndex = 7;
            this.igkxLabel3.TabStop = false;
            this.igkxLabel3.VerticalAlignment = enuStringAlignment.Near;
            // 
            // igkxButton2
            // 
            this.igkxButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.igkxButton2.CaptionKey = "...";
            this.igkxButton2.Checked = false;
            this.igkxButton2.DialogResult = enuDialogResult.None;
            this.igkxButton2.Location = new System.Drawing.Point(299, 69);
            this.igkxButton2.Name = "igkxButton2";
            this.igkxButton2.ShowButtonImage = false;
            this.igkxButton2.Size = new System.Drawing.Size(53, 21);
            this.igkxButton2.State = enuButtonState.Normal;
            this.igkxButton2.TabIndex = 6;
            this.igkxButton2.Click += new System.EventHandler(this.igkxButton2_Click);
            // 
            // c_outPutFolder
            // 
            this.c_outPutFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c_outPutFolder.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c_outPutFolder.Location = new System.Drawing.Point(106, 69);
            this.c_outPutFolder.Name = "c_outPutFolder";
            this.c_outPutFolder.Size = new System.Drawing.Size(187, 21);
            this.c_outPutFolder.TabIndex = 5;
            this.c_outPutFolder.Text = "d:\\\\temp\\\\webFolder";
            this.c_outPutFolder.TipText = null;
            // 
            // c_projectname
            // 
            this.c_projectname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c_projectname.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c_projectname.Location = new System.Drawing.Point(106, 15);
            this.c_projectname.Name = "c_projectname";
            this.c_projectname.Size = new System.Drawing.Size(187, 21);
            this.c_projectname.TabIndex = 4;
            this.c_projectname.Text = "ProjectName";
            this.c_projectname.TipText = null;
            // 
            // igkxButton1
            // 
            this.igkxButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.igkxButton1.CaptionKey = "btn.ok";
            this.igkxButton1.Checked = false;
            this.igkxButton1.DialogResult = enuDialogResult.OK;
            this.igkxButton1.Location = new System.Drawing.Point(277, 248);
            this.igkxButton1.Name = "igkxButton1";
            this.igkxButton1.ShowButtonImage = false;
            this.igkxButton1.Size = new System.Drawing.Size(78, 32);
            this.igkxButton1.State = enuButtonState.Normal;
            this.igkxButton1.TabIndex = 3;
            this.igkxButton1.Click += new System.EventHandler(this.igkxButton1_Click);
            // 
            // igkxRuleLabel1
            // 
            this.igkxRuleLabel1.Alignment = System.Drawing.StringAlignment.Near;
            this.igkxRuleLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.igkxRuleLabel1.CaptionKey = null;
            this.igkxRuleLabel1.HorizontalAlignment = enuStringAlignment.Near;
            this.igkxRuleLabel1.Location = new System.Drawing.Point(0, 237);
            this.igkxRuleLabel1.Name = "igkxRuleLabel1";
            this.igkxRuleLabel1.Size = new System.Drawing.Size(358, 23);
            this.igkxRuleLabel1.TabIndex = 2;
            this.igkxRuleLabel1.TabStop = false;
            this.igkxRuleLabel1.VerticalAlignment = enuStringAlignment.Near;
            // 
            // igkxLabel2
            // 
            this.igkxLabel2.CaptionKey = "lb.Name";
            this.igkxLabel2.HorizontalAlignment =enuStringAlignment.Near;
            this.igkxLabel2.Location = new System.Drawing.Point(9, 15);
            this.igkxLabel2.Name = "igkxLabel2";
            this.igkxLabel2.Size = new System.Drawing.Size(91, 21);
            this.igkxLabel2.TabIndex = 1;
            this.igkxLabel2.TabStop = false;
            this.igkxLabel2.VerticalAlignment = enuStringAlignment.Near;
            // 
            // igkxLabel1
            // 
            this.igkxLabel1.CaptionKey = "lb.Folder";
            this.igkxLabel1.HorizontalAlignment = enuStringAlignment.Near;
            this.igkxLabel1.Location = new System.Drawing.Point(9, 69);
            this.igkxLabel1.Name = "igkxLabel1";
            this.igkxLabel1.Size = new System.Drawing.Size(91, 21);
            this.igkxLabel1.TabIndex = 0;
            this.igkxLabel1.TabStop = false;
            this.igkxLabel1.VerticalAlignment = enuStringAlignment.Near;
            // 
            // WebSolutionWizzardUserControl
            // 
            this.Controls.Add(this.igkxPanel1);
            this.Name = "WebSolutionWizzardUserControl";
            this.Size = new System.Drawing.Size(358, 283);
            this.igkxPanel1.ResumeLayout(false);
            this.igkxPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        private void igkxButton2_Click(object sender, EventArgs e)
        {
            //pick folder from system
            using (FolderBrowserDialog fdb = new FolderBrowserDialog())
            {                
                if (fdb.ShowDialog() == DialogResult.OK)
                {
                    this.c_outPutFolder.Text = fdb.SelectedPath; 
                }
            }
        }

        private void igkxButton1_Click(object sender, EventArgs e)
        {
            Form frm = this.FindForm();
            if (frm != null)
            {
                frm.Hide();
                frm.DialogResult = DialogResult.OK;
            }
            Application.DoEvents();
            WebSolutionSurface sf = null;
            var bench = CoreSystem.GetWorkbench < ICoreApplicationWorkbench>();
            if (bench != null)
            {
                Control v_ctr = bench. MainForm as Control;
                v_ctr.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                WebSolutionSolution solution =
                    WebSolutionSolution.CreateSolution(
                    this.c_projectname.Text,
                    this.c_outPutFolder.Text);

                if (solution != null)
                {
                    sf = new WebSolutionSurface();
                    sf.Solution = solution;
                    bench.AddSurface(sf, true );
                    bench.CurrentSurface = sf;
                }
              v_ctr.Cursor = System.Windows.Forms.Cursors.Default;
            }

        }
    }
}
