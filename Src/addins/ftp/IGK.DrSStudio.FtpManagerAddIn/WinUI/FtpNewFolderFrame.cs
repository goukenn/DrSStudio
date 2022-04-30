

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: FtpNewFolderFrame.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.DrSStudio.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:FtpNewFolderFrame.cs
*/
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinCore.WinUI.Controls;
namespace IGK.DrSStudio.FtpManagerAddIn.WinUI
{
    /// <summary>
    /// class de new folder
    /// </summary>
    sealed class FtpNewFolderFrame :  IGKXUserControl
    {
        private IGKXTextBox c_txb_name;
        private IGKXLabel c_lb_name;
        private IGKXButton xButton1;
        private IGKXRuleLabel xRuleLabel1;
        /// <summary>
        /// get or set the directory name
        /// </summary>
        public string DirectoryName
        {
            get { return this.c_txb_name.Text; }
            set
            {
                this.c_txb_name.Text = value ;
            }
        }
        public FtpNewFolderFrame()
        {
            this.InitializeComponent();
        }
        private void InitializeComponent()
        {
            this.c_txb_name = new IGKXTextBox();
            this.c_lb_name = new IGKXLabel();
            this.xRuleLabel1 = new IGKXRuleLabel();
            this.xButton1 = new IGKXButton();
            this.SuspendLayout();
            // 
            // c_txb_name
            // 
            this.c_txb_name.Location = new System.Drawing.Point(194, 14);
            this.c_txb_name.Name = "c_txb_name";
            this.c_txb_name.Size = new System.Drawing.Size(152, 20);
            this.c_txb_name.TabIndex = 14;
            this.c_txb_name.Text = "NewFolder";
            // 
            // c_lb_name
            // 
            this.c_lb_name.CaptionKey = "lb.port.Caption";
            this.c_lb_name.Location = new System.Drawing.Point(16, 14);
            this.c_lb_name.Name = "c_lb_name";
            this.c_lb_name.Size = new System.Drawing.Size(131, 21);
            this.c_lb_name.TabIndex = 13;
            // 
            // xRuleLabel1
            // 
            this.xRuleLabel1.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel1.CaptionKey = null;
            this.xRuleLabel1.Location = new System.Drawing.Point(3, 48);
            this.xRuleLabel1.Name = "xRuleLabel1";
            this.xRuleLabel1.Size = new System.Drawing.Size(343, 20);
            this.xRuleLabel1.TabIndex = 12;
            // 
            // xButton1
            // 
            this.xButton1.ButtonImageSize = new Size2i(32, 32);
            this.xButton1.CaptionKey = null;
            this.xButton1.Checked = false;
            this.xButton1.DialogResult = enuDialogResult.OK;
            this.xButton1.Location = new System.Drawing.Point(200, 57);
            this.xButton1.Name = "xButton1";
            this.xButton1.ShowButtonImage = false;
            this.xButton1.Size = new System.Drawing.Size(150, 32);
            this.xButton1.State = enuButtonState.Normal;
            this.xButton1.TabIndex = 15;
            this.xButton1.Click += new System.EventHandler(this.xButton1_Click);
            // 
            // FtpNewFolderFrame
            // 
            this.Controls.Add(this.xButton1);
            this.Controls.Add(this.c_txb_name);
            this.Controls.Add(this.c_lb_name);
            this.Controls.Add(this.xRuleLabel1);
            this.Name = "FtpNewFolderFrame";
            this.Size = new System.Drawing.Size(353, 102);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private void xButton1_Click(object sender, EventArgs e)
        {
        }
    }
}

