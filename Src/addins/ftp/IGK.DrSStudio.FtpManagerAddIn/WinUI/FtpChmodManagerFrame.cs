

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: FtpChmodManagerFrame.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:FtpChmodManagerFrame.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.FtpManagerAddIn.WinUI
{
    
using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinCore.WinUI.Controls;
    class FtpChmodManagerFrame : IGKXUserControl
    {
        private IGKXCheckBox c_uRead;
        private IGKXCheckBox c_uWrite;
        private IGKXCheckBox c_uExecute;
        private IGKXCheckBox c_gExecute;
        private IGKXCheckBox c_gWrite;
        private IGKXCheckBox c_gRead;
        private IGKXRuleLabel xRuleLabel1;
        private IGKXRuleLabel xRuleLabel2;
        private IGKXRuleLabel xRuleLabel3;
        private IGKXCheckBox c_oExecute;
        private IGKXCheckBox c_oWrite;
        private IGKXCheckBox c_oRead;
        private IGKXRuleLabel xRuleLabel4;
        private IGKXButton xButton1;
        public FtpChmodManagerFrame()
        {
            this.InitializeComponent();
        }
        private void InitializeComponent()
        {
            this.c_uRead = new IGKXCheckBox();
            this.c_uWrite = new IGKXCheckBox();
            this.c_uExecute = new IGKXCheckBox();
            this.c_gExecute = new IGKXCheckBox();
            this.c_gWrite = new IGKXCheckBox();
            this.c_gRead = new IGKXCheckBox();
            this.xRuleLabel1 = new IGKXRuleLabel();
            this.xRuleLabel2 = new IGKXRuleLabel();
            this.xRuleLabel3 = new IGKXRuleLabel();
            this.c_oExecute = new IGKXCheckBox();
            this.c_oWrite = new IGKXCheckBox();
            this.c_oRead = new IGKXCheckBox();
            this.xRuleLabel4 = new IGKXRuleLabel();
            this.xButton1 = new IGKXButton();
            this.SuspendLayout();
            // 
            // c_uRead
            // 
            this.c_uRead.AutoSize = true;
            this.c_uRead.CaptionKey = "Read";
            this.c_uRead.Checked = true;
            this.c_uRead.CheckState = enuCheckState.Checked;
            this.c_uRead.Location = new System.Drawing.Point(53, 45);
            this.c_uRead.Name = "c_uRead";
            this.c_uRead.Size = new System.Drawing.Size(52, 17);
            this.c_uRead.TabIndex = 3;
            this.c_uRead.CheckedChanged += new System.EventHandler(this.c_u_CheckedChanged);
            // 
            // c_uWrite
            // 
            this.c_uWrite.AutoSize = true;
            this.c_uWrite.CaptionKey = "Write";
            this.c_uWrite.Location = new System.Drawing.Point(53, 68);
            this.c_uWrite.Name = "c_uWrite";
            this.c_uWrite.Size = new System.Drawing.Size(51, 17);
            this.c_uWrite.TabIndex = 4;
            this.c_uWrite.CheckedChanged += new System.EventHandler(this.c_u_CheckedChanged);
            // 
            // c_uExecute
            // 
            this.c_uExecute.AutoSize = true;
            this.c_uExecute.CaptionKey = "Execution";
            this.c_uExecute.Location = new System.Drawing.Point(53, 91);
            this.c_uExecute.Name = "c_uExecute";
            this.c_uExecute.Size = new System.Drawing.Size(73, 17);
            this.c_uExecute.TabIndex = 5;
            this.c_uExecute.CheckedChanged += new System.EventHandler(this.c_u_CheckedChanged);
            // 
            // c_gExecute
            // 
            this.c_gExecute.AutoSize = true;
            this.c_gExecute.CaptionKey = "Execution";
            this.c_gExecute.Location = new System.Drawing.Point(53, 177);
            this.c_gExecute.Name = "c_gExecute";
            this.c_gExecute.Size = new System.Drawing.Size(73, 17);
            this.c_gExecute.TabIndex = 8;
            this.c_gExecute.CheckedChanged += new System.EventHandler(this.c_g_CheckedChanged);
            // 
            // c_gWrite
            // 
            this.c_gWrite.AutoSize = true;
            this.c_gWrite.CaptionKey = "Write";
            this.c_gWrite.Location = new System.Drawing.Point(53, 154);
            this.c_gWrite.Name = "c_gWrite";
            this.c_gWrite.Size = new System.Drawing.Size(51, 17);
            this.c_gWrite.TabIndex = 7;
            this.c_gWrite.CheckedChanged += new System.EventHandler(this.c_g_CheckedChanged);
            // 
            // c_gRead
            // 
            this.c_gRead.AutoSize = true;
            this.c_gRead.CaptionKey = "Read";
            this.c_gRead.Checked = true;
            this.c_gRead.CheckState = enuCheckState.Checked;
            this.c_gRead.Location = new System.Drawing.Point(53, 131);
            this.c_gRead.Name = "c_gRead";
            this.c_gRead.Size = new System.Drawing.Size(52, 17);
            this.c_gRead.TabIndex = 6;
            this.c_gRead.CheckedChanged += new System.EventHandler(this.c_g_CheckedChanged);
            // 
            // xRuleLabel1
            // 
            this.xRuleLabel1.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xRuleLabel1.CaptionKey = "lb.userMod.caption";
            this.xRuleLabel1.Location = new System.Drawing.Point(0, 18);
            this.xRuleLabel1.Name = "xRuleLabel1";
            this.xRuleLabel1.Size = new System.Drawing.Size(330, 24);
            this.xRuleLabel1.TabIndex = 9;
            // 
            // xRuleLabel2
            // 
            this.xRuleLabel2.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xRuleLabel2.CaptionKey = "lb.groupMod.caption";
            this.xRuleLabel2.Location = new System.Drawing.Point(3, 111);
            this.xRuleLabel2.Name = "xRuleLabel2";
            this.xRuleLabel2.Size = new System.Drawing.Size(327, 24);
            this.xRuleLabel2.TabIndex = 10;
            // 
            // xRuleLabel3
            // 
            this.xRuleLabel3.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xRuleLabel3.CaptionKey = "lb.otherMod.caption";
            this.xRuleLabel3.Location = new System.Drawing.Point(6, 197);
            this.xRuleLabel3.Name = "xRuleLabel3";
            this.xRuleLabel3.Size = new System.Drawing.Size(324, 24);
            this.xRuleLabel3.TabIndex = 11;
            // 
            // c_oExecute
            // 
            this.c_oExecute.AutoSize = true;
            this.c_oExecute.CaptionKey = "Execution";
            this.c_oExecute.Location = new System.Drawing.Point(52, 260);
            this.c_oExecute.Name = "c_oExecute";
            this.c_oExecute.Size = new System.Drawing.Size(73, 17);
            this.c_oExecute.TabIndex = 14;
            this.c_oExecute.CheckedChanged += new System.EventHandler(this.c_oCheckedChanged);
            // 
            // c_oWrite
            // 
            this.c_oWrite.AutoSize = true;
            this.c_oWrite.CaptionKey = "Write";
            this.c_oWrite.Location = new System.Drawing.Point(52, 237);
            this.c_oWrite.Name = "c_oWrite";
            this.c_oWrite.Size = new System.Drawing.Size(51, 17);
            this.c_oWrite.TabIndex = 13;
            this.c_oWrite.CheckedChanged += new System.EventHandler(this.c_oCheckedChanged);
            // 
            // c_oRead
            // 
            this.c_oRead.AutoSize = true;
            this.c_oRead.CaptionKey = "Read";
            this.c_oRead.Checked = true;
            this.c_oRead.CheckState = enuCheckState.Checked;
            this.c_oRead.Location = new System.Drawing.Point(52, 214);
            this.c_oRead.Name = "c_oRead";
            this.c_oRead.Size = new System.Drawing.Size(52, 17);
            this.c_oRead.TabIndex = 12;
            this.c_oRead.CheckedChanged += new System.EventHandler(this.c_oCheckedChanged);
            // 
            // xRuleLabel4
            // 
            this.xRuleLabel4.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xRuleLabel4.CaptionKey = "";
            this.xRuleLabel4.Location = new System.Drawing.Point(3, 280);
            this.xRuleLabel4.Name = "xRuleLabel4";
            this.xRuleLabel4.Size = new System.Drawing.Size(324, 24);
            this.xRuleLabel4.TabIndex = 15;
            this.xRuleLabel4.Click += new System.EventHandler(this.xRuleLabel4_Click);
            // 
            // xButton1
            // 
            this.xButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xButton1.ButtonImageSize = new Size2i(32, 32);
            this.xButton1.CaptionKey = "btn.Apply.caption";
            this.xButton1.Checked = false;
            this.xButton1.DialogResult = enuDialogResult.OK;
            this.xButton1.Location = new System.Drawing.Point(222, 289);
            this.xButton1.Name = "xButton1";
            this.xButton1.ShowButtonImage = false;
            this.xButton1.Size = new System.Drawing.Size(105, 35);
            this.xButton1.State = enuButtonState.Normal;
            this.xButton1.TabIndex = 16;
            // 
            // FtpChmodManager
            // 
            this.Controls.Add(this.xButton1);
            this.Controls.Add(this.xRuleLabel4);
            this.Controls.Add(this.c_oExecute);
            this.Controls.Add(this.c_oWrite);
            this.Controls.Add(this.c_oRead);
            this.Controls.Add(this.xRuleLabel3);
            this.Controls.Add(this.xRuleLabel1);
            this.Controls.Add(this.c_gExecute);
            this.Controls.Add(this.c_gWrite);
            this.Controls.Add(this.c_gRead);
            this.Controls.Add(this.c_uExecute);
            this.Controls.Add(this.c_uWrite);
            this.Controls.Add(this.c_uRead);
            this.Controls.Add(this.xRuleLabel2);
            this.Name = "FtpChmodManager";
            this.Size = new System.Drawing.Size(333, 327);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private int m_OtherAuthorization;
        private int m_UserAuthorization;
        private int m_GroupAuthorization;
        public int GroupAuthorization
        {
            get { return m_GroupAuthorization; }
            set
            {
                if (m_GroupAuthorization != value)
                {
                    m_GroupAuthorization = value;
                }
            }
        }
        public int UserAuthorization
        {
            get { return m_UserAuthorization; }
            set
            {
                if (m_UserAuthorization != value)
                {
                    m_UserAuthorization = value;
                }
            }
        }
        public int OtherAuthorization
        {
            get { return m_OtherAuthorization; }
            set
            {
                if (m_OtherAuthorization != value)
                {
                    m_OtherAuthorization = value;
                }
            }
        }
        private void c_oCheckedChanged(object sender, EventArgs e)
        {
            this.OtherAuthorization = GetAutoriation(c_oRead.Checked,
                c_oWrite.Checked,
                c_oExecute.Checked);
        }
        private void c_g_CheckedChanged(object sender, EventArgs e)
        {
            this.GroupAuthorization = GetAutoriation(c_gRead.Checked,
               c_gWrite.Checked,
               c_gExecute.Checked);
        }
        private void c_u_CheckedChanged(object sender, EventArgs e)
        {
            this.UserAuthorization = GetAutoriation(c_uRead.Checked,
              c_uWrite.Checked,
              c_uExecute.Checked);
        }
        public string AutorizationToString() {
            return string.Format("0{0}{1}{2}", this.UserAuthorization, this.GroupAuthorization, this.OtherAuthorization);
        }
        private int GetAutoriation(bool read, bool write, bool execute)
        {
            int r = 0;
            r |= (read ) ? 1: 0;
#pragma warning disable IDE0054 // Use compound assignment
            r = r<<1;
#pragma warning restore IDE0054 // Use compound assignment
            r |= (write) ? 1 : 0;
#pragma warning disable IDE0054 // Use compound assignment
            r = r << 1;
#pragma warning restore IDE0054 // Use compound assignment
            r |= (execute) ? 1 : 0;
            return r;
        }
        private void xRuleLabel4_Click(object sender, EventArgs e)
        {
            c_u_CheckedChanged(this, e);
            c_g_CheckedChanged (this, e);
            c_oCheckedChanged(this, e);
        }
    }
}

