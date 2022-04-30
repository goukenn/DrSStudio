

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XResourcesAddStringGUI.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.WinUI;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinCore.WinUI.Controls;

namespace IGK.DrSStudio.ResourcesManager.WinUI
{
    class XResourcesAddStringGUI : IGKXUserControl
    {
        private IGKXTextBox c_tbx_key;
        private IGKXTextBox c_txb_value;
        private IGKXRuleLabel igkxRuleLabel1;
        private IGKXLabel igkxLabel1;
        private IGKXLabel igkxLabel2;
        private IGKXButton igkxButton1;
    
        public XResourcesAddStringGUI()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.c_tbx_key = new IGKXTextBox();
            this.c_txb_value = new IGKXTextBox();
            this.igkxRuleLabel1 = new IGKXRuleLabel();
            this.igkxLabel1 = new IGKXLabel();
            this.igkxLabel2 = new IGKXLabel();
            this.igkxButton1 = new IGKXButton();
            this.SuspendLayout();
            // 
            // c_tbx_key
            // 
            this.c_tbx_key.Location = new System.Drawing.Point(118, 20);
            this.c_tbx_key.Name = "c_tbx_key";
            this.c_tbx_key.Size = new System.Drawing.Size(205, 20);
            this.c_tbx_key.TabIndex = 0;
            // 
            // c_txb_value
            // 
            this.c_txb_value.Location = new System.Drawing.Point(118, 55);
            this.c_txb_value.Multiline = true;
            this.c_txb_value.Name = "c_txb_value";
            this.c_txb_value.Size = new System.Drawing.Size(205, 90);
            this.c_txb_value.TabIndex = 1;
            // 
            // igkxRuleLabel1
            // 
            this.igkxRuleLabel1.Alignment = System.Drawing.StringAlignment.Near;
            this.igkxRuleLabel1.CaptionKey = null;
            this.igkxRuleLabel1.Location = new System.Drawing.Point(3, 151);
            this.igkxRuleLabel1.Name = "igkxRuleLabel1";
            this.igkxRuleLabel1.Size = new System.Drawing.Size(364, 10);
            this.igkxRuleLabel1.TabIndex = 2;
            this.igkxRuleLabel1.TabStop = false;
            // 
            // igkxLabel1
            // 
            this.igkxLabel1.CaptionKey = "lb.Keys";
            this.igkxLabel1.Location = new System.Drawing.Point(3, 20);
            this.igkxLabel1.Name = "igkxLabel1";
            this.igkxLabel1.Size = new System.Drawing.Size(109, 20);
            this.igkxLabel1.TabIndex = 3;
            this.igkxLabel1.TabStop = false;
            // 
            // igkxLabel2
            // 
            this.igkxLabel2.CaptionKey = "lb.Values";
            this.igkxLabel2.Location = new System.Drawing.Point(3, 55);
            this.igkxLabel2.Name = "igkxLabel2";
            this.igkxLabel2.Size = new System.Drawing.Size(51, 14);
            this.igkxLabel2.TabIndex = 4;
            this.igkxLabel2.TabStop = false;
            // 
            // igkxButton1
            // 
            this.igkxButton1.CaptionKey = "btn.add";
            this.igkxButton1.Checked = false;
            this.igkxButton1.DialogResult = enuDialogResult.OK;
            this.igkxButton1.Location = new System.Drawing.Point(248, 174);
            this.igkxButton1.Name = "igkxButton1";
            this.igkxButton1.ShowButtonImage = false;
            this.igkxButton1.Size = new System.Drawing.Size(74, 30);
            this.igkxButton1.State = enuButtonState.Normal;
            this.igkxButton1.TabIndex = 5;
            this.igkxButton1.Click += new System.EventHandler(this.igkxButton1_Click);
            // 
            // XResourcesAddStringGUI
            // 
            this.Controls.Add(this.igkxButton1);
            this.Controls.Add(this.igkxLabel2);
            this.Controls.Add(this.igkxLabel1);
            this.Controls.Add(this.igkxRuleLabel1);
            this.Controls.Add(this.c_txb_value);
            this.Controls.Add(this.c_tbx_key);
            this.Name = "XResourcesAddStringGUI";
            this.Size = new System.Drawing.Size(370, 218);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        public string Key { get { return this.c_tbx_key.Text; } }
        public string Value{ get { return this.c_txb_value.Text ; } }
        private void igkxButton1_Click(object sender, EventArgs e)
        {
            this.FindForm().DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
