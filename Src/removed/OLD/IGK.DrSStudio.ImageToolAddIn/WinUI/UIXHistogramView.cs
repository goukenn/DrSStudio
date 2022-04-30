

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: UIXHistogramView.cs
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
file:UIXHistogramView.cs
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
﻿
/* 
-------------------------------------------------------------
This file is part of iGK-DEV-DrawingStudio
-------------------------------------------------------------
-------------------------------------------------------------
-------------------------------------------------------------
view license file in Documentation folder to get more info
Copyright (c) 2008-2010 
Author  : Charles A.D. BONDJE DOUE 
mail : bondje.doue@hotmail.com
-------------------------------------------------------------
-------------------------------------------------------------
*/
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio.Drawing2D.WinUI
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    public partial class UIXHistogramView :         
        IGKXToolConfigControlBase 
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.c_cmb = new IGK.DrSStudio.WinUI.IGKXComboBox();
            this.c_pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lb_colorMode = new IGK.DrSStudio.WinUI.IGKXLabel();
            ((System.ComponentModel.ISupportInitialize)(this.c_pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.c_cmb.FormattingEnabled = true;
            this.c_cmb.Location = new System.Drawing.Point(173, 12);
            this.c_cmb.Name = "comboBox1";
            this.c_cmb.Size = new System.Drawing.Size(134, 21);
            this.c_cmb.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.c_pictureBox1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.c_pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.c_pictureBox1.Location = new System.Drawing.Point(3, 38);
            this.c_pictureBox1.Name = "pictureBox1";
            this.c_pictureBox1.Size = new System.Drawing.Size(304, 270);
            this.c_pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.c_pictureBox1.TabIndex = 1;
            this.c_pictureBox1.TabStop = false;
            // 
            // lb_colorMode
            // 
            this.lb_colorMode.AutoSize = true;
            this.lb_colorMode.CaptionKey = "lb.ColorMode.Title";
            this.lb_colorMode.Location = new System.Drawing.Point(3, 12);
            this.lb_colorMode.Name = "lb_colorMode";
            this.lb_colorMode.Size = new System.Drawing.Size(158, 13);
            this.lb_colorMode.TabIndex = 2;
            // 
            // HistogramViewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;            
            this.Controls.Add(this.lb_colorMode);
            this.Controls.Add(this.c_pictureBox1);
            this.Controls.Add(this.c_cmb);
            this.MinimumSize = new System.Drawing.Size(310, 0);
            this.Name = "HistogramViewControl";
            this.Size = new System.Drawing.Size(310, 334);
            ((System.ComponentModel.ISupportInitialize)(this.c_pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
        private IGKXComboBox c_cmb;
        private System.Windows.Forms.PictureBox c_pictureBox1;
        private IGKXLabel lb_colorMode;
        enuRGBMode m_Mode;
        public event EventHandler ModeChanged;
        public enuRGBMode Mode
        {
            get
            {
                return this.m_Mode;
            }
            private set
            {
                this.m_Mode = value;
                OnModeChanged(EventArgs.Empty);
            }
        }
        private void OnModeChanged(EventArgs eventArgs)
        {
            if (this.ModeChanged != null)
                this.ModeChanged(this, eventArgs);
        }
        public UIXHistogramView():base(Tools.HistogramViewManagerTool .Instance )
        {
            InitializeComponent();
            InitControl();
            this.c_pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            this.m_Mode = enuRGBMode.RGB;
            this.c_cmb.DropDownStyle = ComboBoxStyle.DropDownList;
            this.c_cmb.SelectedItem = enuRGBMode.RGB;
        }
        private void InitControl()
        {
            this.AutoScroll = true;
            foreach (enuRGBMode r in Enum.GetValues(typeof(enuRGBMode)))
            {
                this.c_cmb.Items.Add(r);
            };
            this.c_cmb.SelectedItem = Mode; ;
            this.c_cmb.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);
            this.c_cmb.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.c_cmb.SelectedItem != null)
            {
                this.Mode = (enuRGBMode)this.c_cmb.SelectedItem;
            }
        }
        internal void SetHistogram(Bitmap bmp)
        {
            if (this.c_pictureBox1.Image != null)
            {
                this.c_pictureBox1.Image.Dispose();
            }
             this.c_pictureBox1.Image = bmp;
        }
    }
}

