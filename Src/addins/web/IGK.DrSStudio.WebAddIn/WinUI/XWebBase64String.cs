

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XWebBase64String.cs
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
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:XWebBase64String.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IGK.ICore.WinCore.WinUI.Controls;
namespace IGK.DrSStudio.WebAddIn.WinUI
{
    class XWebBase64String : UIXConfigControlBase
    {
        private IGKXRuleLabel xRuleLabel1;
        private IGKXRadioButton rd_mode1;
        private IGKXRadioButton rd_mode2;
        private IGKXRuleLabel xRuleLabel2;
        private System.Windows.Forms.TextBox textBox1;
        private IGKXButton xButton2;
        private IGKXButton xButton1;
        public event EventHandler ModeChanged;
        public XWebBase64String()
        {
            this.InitializeComponent();
            this.m_Mode = 1;
        }
        public override void LoadDisplayText()
        {
            base.LoadDisplayText();
            this.rd_mode1.Text = "".R();
        }
        public string Value
        {
            get { return this.textBox1.Text ; }
            set
            {
                if (Value != value)
                {
                    this.textBox1.Text = value;
                }
            }
        }
        private int m_Mode;
        public int Mode
        {
            get { return m_Mode; }
            set
            {
                if (m_Mode != value)
                {
                    m_Mode = value;
                }
            }
        }
        private void InitializeComponent()
        {
            this.xRuleLabel1 = new IGKXRuleLabel();
            this.rd_mode1 = new IGKXRadioButton();
            this.rd_mode2 = new IGKXRadioButton();
            this.xRuleLabel2 = new IGKXRuleLabel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.xButton1 = new IGKXButton();
            this.xButton2 = new IGKXButton();
            this.SuspendLayout();
            // 
            // xRuleLabel1
            // 
            this.xRuleLabel1.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel1.CaptionKey = "lb.mode.caption";
            this.xRuleLabel1.Location = new System.Drawing.Point(0, 9);
            this.xRuleLabel1.Name = "xRuleLabel1";
            this.xRuleLabel1.Size = new System.Drawing.Size(265, 23);
            this.xRuleLabel1.TabIndex = 0;
            // 
            // xRadioButton1
            // 
            this.rd_mode1.AutoSize = true;
            this.rd_mode1.Checked = true;
            this.rd_mode1.Location = new System.Drawing.Point(31, 35);
            this.rd_mode1.Name = "xRadioButton1";
            this.rd_mode1.Size = new System.Drawing.Size(92, 17);
            this.rd_mode1.TabIndex = 1;
            this.rd_mode1.TabStop = true;
            this.rd_mode1.CheckedChanged += new System.EventHandler(this.xRadioButton1_CheckedChanged);
            // 
            // xRadioButton2
            // 
            this.rd_mode2.AutoSize = true;
            this.rd_mode2.Location = new System.Drawing.Point(129, 35);            
            this.rd_mode2.Size = new System.Drawing.Size(94, 17);
            this.rd_mode2.TabIndex = 2;
            this.rd_mode2.CheckedChanged += new System.EventHandler(this.xRadioButton2_CheckedChanged);
            // 
            // xRuleLabel2
            // 
            this.xRuleLabel2.Alignment = System.Drawing.StringAlignment.Near;            
            this.xRuleLabel2.Location = new System.Drawing.Point(3, 55);
            this.xRuleLabel2.Name = "xRuleLabel2";
            this.xRuleLabel2.Size = new System.Drawing.Size(265, 23);
            this.xRuleLabel2.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(31, 81);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(227, 67);
            this.textBox1.TabIndex = 4;
            // 
            // xButton1
            // 
            this.xButton1.BackColor = System.Drawing.Color.Transparent;
            this.xButton1.ButtonDocument = null;
            this.xButton1.CaptionKey = CoreConstant.BTN_OK;
            this.xButton1.Checked = false;
            this.xButton1.DialogResult = enuDialogResult.OK;
            this.xButton1.DocumentUsedAsBrush = false;
            this.xButton1.Location = new System.Drawing.Point(183, 169);
            this.xButton1.Name = "xButton1";
            this.xButton1.Size = new System.Drawing.Size(75, 23);
            this.xButton1.State = enuButtonState.Normal;
            this.xButton1.TabIndex = 5;
            // 
            // xButton2
            // 
            this.xButton2.BackColor = System.Drawing.Color.Transparent;
            this.xButton2.ButtonDocument = null;
            this.xButton2.CaptionKey = "btn.copyToClipboard.caption";
            this.xButton2.Checked = false;
            this.xButton2.DialogResult = enuDialogResult.OK;
            this.xButton2.DocumentUsedAsBrush = false;
            this.xButton2.Location = new System.Drawing.Point(52, 169);            
            this.xButton2.Size = new System.Drawing.Size(125, 23);
            this.xButton2.State = enuButtonState.Normal;
            this.xButton2.TabIndex = 6;
            this.xButton2.Click += new System.EventHandler(this.xButton2_Click);
            // 
            // XWebBase64String
            // 
            this.Controls.Add(this.xButton2);
            this.Controls.Add(this.xButton1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.xRuleLabel2);
            this.Controls.Add(this.rd_mode2);
            this.Controls.Add(this.rd_mode1);
            this.Controls.Add(this.xRuleLabel1);
            this.Name = "XWebBase64String";
            this.Size = new System.Drawing.Size(268, 207);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private void xRadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_mode2.Checked)
            {
                this.m_Mode = 0;
                rd_mode1.Checked = false;
                OnModeChanged(EventArgs.Empty);
            }
        }
        private void OnModeChanged(EventArgs eventArgs)
        {
            if (this.ModeChanged != null)
                this.ModeChanged(this, eventArgs);
        }
        private void xRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_mode1.Checked)
            {
                this.m_Mode = 1;
                rd_mode2.Checked = false;
                OnModeChanged(EventArgs.Empty);
            }
        }
        private void xButton2_Click(object sender, EventArgs e)
        {
            //copy text to the clipboard
            //--------------------------
            System.Windows.Forms.Clipboard.SetText (this.textBox1.Text );            
        }
    }
}

