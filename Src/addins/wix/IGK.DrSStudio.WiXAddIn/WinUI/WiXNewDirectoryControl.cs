

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WiXNewDirectoryControl.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
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
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:WiXNewDirectoryControl.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinCore.WinUI.Controls;
namespace IGK.DrSStudio.WiXAddIn.WinUI
{
    class WiXNewDirectoryControl : IGKXUserControl
    {
        private IGKXTextBox c_dir;
        private IGKXRuleLabel xRuleLabel1;
        private IGKXButton xButton1;
        private IGKXLabel xLabel1;
        public string NewDirectory
        {
            get { return c_dir.Text; }
        }
        public WiXNewDirectoryControl()
        {
            this.InitializeComponent();
        }
        private void InitializeComponent()
        {
            this.xLabel1 = new IGKXLabel();
            this.c_dir = new IGKXTextBox();
            this.xRuleLabel1 = new IGKXRuleLabel();
            this.xButton1 = new IGKXButton();
            this.SuspendLayout();
            // 
            // xLabel1
            // 
            this.xLabel1.CaptionKey = "wix.lb.directoryname";
            this.xLabel1.Location = new System.Drawing.Point(16, 32);
            this.xLabel1.Name = "xLabel1";
            this.xLabel1.Size = new System.Drawing.Size(127, 36);
            this.xLabel1.TabIndex = 0;
            // 
            // c_dir
            // 
            this.c_dir.Location = new System.Drawing.Point(173, 34);
            this.c_dir.Name = "c_dir";
            this.c_dir.Size = new System.Drawing.Size(185, 20);
            this.c_dir.TabIndex = 1;
            this.c_dir.Text = "newDir";
            // 
            // xRuleLabel1
            // 
            this.xRuleLabel1.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel1.CaptionKey = null;
            this.xRuleLabel1.Location = new System.Drawing.Point(3, 78);
            this.xRuleLabel1.Name = "xRuleLabel1";
            this.xRuleLabel1.Size = new System.Drawing.Size(389, 11);
            this.xRuleLabel1.TabIndex = 2;
            // 
            // xButton1
            // 
            this.xButton1.CaptionKey = "btn.ok.caption";
            this.xButton1.Checked = false;
            this.xButton1.Location = new System.Drawing.Point(305, 92);
            this.xButton1.Name = "xButton1";
            this.xButton1.ShowButtonImage = false;
            this.xButton1.Size = new System.Drawing.Size(75, 23);
            this.xButton1.State = enuButtonState.Normal;
            this.xButton1.TabIndex = 3;
            this.xButton1.DialogResult = enuDialogResult.OK;
            // 
            // WiXNewDirectoryControl
            // 
            this.Controls.Add(this.xButton1);
            this.Controls.Add(this.xRuleLabel1);
            this.Controls.Add(this.c_dir);
            this.Controls.Add(this.xLabel1);
            this.Name = "WiXNewDirectoryControl";
            this.Size = new System.Drawing.Size(395, 126);
            this.Load += new System.EventHandler(this.WiXNewDirectoryControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private void WiXNewDirectoryControl_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.Form frm = this.FindForm();
            if (frm != null)
            {
                frm.AcceptButton = this.xButton1;
            }
        }
    }
}

