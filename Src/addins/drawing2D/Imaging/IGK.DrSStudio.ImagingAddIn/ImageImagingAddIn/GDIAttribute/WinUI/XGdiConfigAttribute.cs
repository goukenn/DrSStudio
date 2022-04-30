

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XGdiConfigAttribute.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
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
file:XGdiConfigAttribute.cs
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
using System.Drawing ;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using IGK.ICore.WinCore.WinUI.Controls;
namespace IGK.DrSStudio.ImageAddIn.GDIAttribute.WinUI
{
    /// <summary>
    /// represent gdi config attributes
    /// </summary>
    class XGdiConfigAttribute : UIXConfigControlBase 
    {
        private System.Windows.Forms.PictureBox pictureBox1;
        private IGKXPanel xPanel1;
        private IGKXButton xButton2;
        private IGKXButton xButton1;
        private IGKXComboBox cmb_int1;
        private IGKXComboBox cmb_int2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private IGKXLabel xLabel1;
        private IGKXLabel xLabel2;
        private IGKXLabel xLabel3;
        private IGKXComboBox cmb_factor;
        private ImageElement m_element;
        public XGdiConfigAttribute(ImageElement element)
        {
            InitializeComponent();
            this.m_element = element;
        }
        Bitmap m_oldBitmap;
        private int m_factor;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.InitControl();
        }
        private void InitControl()
        {
            this.m_factor = 1;
            foreach (System.Drawing.Drawing2D.InterpolationMode i in Enum.GetValues(typeof(System.Drawing.Drawing2D.InterpolationMode)))
            {
                if (i == System.Drawing.Drawing2D.InterpolationMode.Invalid) 
                    continue;
                this.cmb_int1.Items.Add(i);
                this.cmb_int2.Items.Add(i);
            }
            //foreach (System.Drawing.Drawing2D.CompositingQuality i in Enum.GetValues(typeof(System.Drawing.Drawing2D.CompositingQuality)))
            //{
            //    if (i == System.Drawing.Drawing2D.CompositingQuality.Invalid) continue;
            //    this.cmb_qual.Items.Add(i);
            //    this.cmb_qual2.Items.Add(i);
            //}
            //this.cmb_qual.SelectedIndex = 0;
            //this.cmb_qual.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);
            this.cmb_int1.SelectedIndex = 0;
            this.cmb_int1.SelectedIndexChanged += new EventHandler(ComboBox1_SelectedIndexChanged);
            //this.cmb_qual2.SelectedIndex = 0;
            //this.cmb_qual2.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);
            this.cmb_int2.SelectedIndex = 0;
            this.cmb_int2.SelectedIndexChanged += new EventHandler(ComboBox1_SelectedIndexChanged);
            this.m_oldBitmap  = this.m_element.Bitmap.Clone() as Bitmap;
            this.pictureBox1.Image = this.m_oldBitmap;
            this.BuildImage();
        }
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.xPanel1 = new IGKXPanel();
            this.xButton2 = new IGKXButton();
            this.xButton1 = new IGKXButton();
            this.cmb_int1 = new IGKXComboBox();
            this.cmb_int2 = new IGKXComboBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.xLabel1 = new IGKXLabel();
            this.xLabel2 = new IGKXLabel();
            this.xLabel3 = new IGKXLabel();
            this.cmb_factor = new IGKXComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.xPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(214, 167);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // xPanel1
            // 
            this.xPanel1.CaptionKey = null;
            this.xPanel1.Controls.Add(this.xButton2);
            this.xPanel1.Controls.Add(this.xButton1);
            this.xPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xPanel1.Location = new System.Drawing.Point(0, 264);
            this.xPanel1.Name = "xPanel1";
            this.xPanel1.Size = new System.Drawing.Size(446, 36);
            this.xPanel1.TabIndex = 2;
            // 
            // xButton2
            // 
            this.xButton2.BackColor = System.Drawing.Color.Transparent;
            this.xButton2.ButtonDocument = null;
            this.xButton2.CaptionKey = CoreConstant.BTN_OK;
            this.xButton2.Checked = false;
            this.xButton2.DialogResult = enuDialogResult.OK;
            this.xButton2.DocumentUsedAsBrush = false;
            this.xButton2.Location = new System.Drawing.Point(281, 10);
            this.xButton2.Name = "xButton2";
            this.xButton2.Size = new System.Drawing.Size(75, 23);
            this.xButton2.State = enuButtonState.Normal;
            this.xButton2.TabIndex = 3;
            this.xButton2.Click += new System.EventHandler(this.Btn_ok_click);
            // 
            // xButton1
            // 
            this.xButton1.BackColor = System.Drawing.Color.Transparent;
            this.xButton1.ButtonDocument = null;
            this.xButton1.CaptionKey = CoreConstant.BTN_CANCEL;
            this.xButton1.Checked = false;
            this.xButton1.DialogResult = enuDialogResult.Cancel;
            this.xButton1.DocumentUsedAsBrush = false;
            this.xButton1.Location = new System.Drawing.Point(362, 10);
            this.xButton1.Name = "xButton1";
            this.xButton1.Size = new System.Drawing.Size(75, 23);
            this.xButton1.State = enuButtonState.Normal;
            this.xButton1.TabIndex = 0;
            this.xButton1.Click += new System.EventHandler(this.XButton1_Click);
            // 
            // cmb_int1
            // 
            this.cmb_int1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_int1.FormattingEnabled = true;
            this.cmb_int1.Location = new System.Drawing.Point(96, 176);
            this.cmb_int1.Name = "cmb_int1";
            this.cmb_int1.Size = new System.Drawing.Size(121, 21);
            this.cmb_int1.TabIndex = 3;
            // 
            // cmb_int2
            // 
            this.cmb_int2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_int2.FormattingEnabled = true;
            this.cmb_int2.Location = new System.Drawing.Point(316, 176);
            this.cmb_int2.Name = "cmb_int2";
            this.cmb_int2.Size = new System.Drawing.Size(121, 21);
            this.cmb_int2.TabIndex = 4;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(229, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(214, 167);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // xLabel1
            // 
            this.xLabel1.CaptionKey = "lb.interpolationmode.caption";
            this.xLabel1.Location = new System.Drawing.Point(3, 176);
            this.xLabel1.Name = "xLabel1";
            this.xLabel1.Size = new System.Drawing.Size(90, 23);
            this.xLabel1.TabIndex = 6;
            // 
            // xLabel2
            // 
            this.xLabel2.CaptionKey = "lb.interpolationmode.caption";
            this.xLabel2.Location = new System.Drawing.Point(223, 176);
            this.xLabel2.Name = "xLabel2";
            this.xLabel2.Size = new System.Drawing.Size(90, 23);
            this.xLabel2.TabIndex = 7;
            // 
            // xLabel3
            // 
            this.xLabel3.CaptionKey = "lb.factor.caption";
            this.xLabel3.Location = new System.Drawing.Point(3, 209);
            this.xLabel3.Name = "xLabel3";
            this.xLabel3.Size = new System.Drawing.Size(90, 23);
            this.xLabel3.TabIndex = 8;
            // 
            // cmb_factor
            // 
            this.cmb_factor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_factor.FormattingEnabled = true;
            this.cmb_factor.Items.AddRange(new object[] {
            "1x",
            "2x",
            "4x",
            "8x",
            "10x",
            "16x",
            "20x",
            "30x",
            "50x",
            "80x",
            "100x"});
            this.cmb_factor.Location = new System.Drawing.Point(96, 203);
            this.cmb_factor.SelectedIndex = 0;
            this.cmb_factor.Name = "cmb_factor";
            this.cmb_factor.Size = new System.Drawing.Size(121, 21);
            this.cmb_factor.TabIndex = 9;
            this.cmb_factor.SelectedIndexChanged += new System.EventHandler(this.XComboBox1_SelectedIndexChanged);
            // 
            // XGdiConfigAttribute
            // 
            this.Controls.Add(this.cmb_factor);
            this.Controls.Add(this.xLabel3);
            this.Controls.Add(this.xLabel2);
            this.Controls.Add(this.xLabel1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.cmb_int2);
            this.Controls.Add(this.xPanel1);
            this.Controls.Add(this.cmb_int1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "XGdiConfigAttribute";
            this.Size = new System.Drawing.Size(446, 300);
            
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.xPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
        }
        InterpolationMode mode;
        InterpolationMode mode2;
        void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            mode = (System.Drawing.Drawing2D.InterpolationMode)this.cmb_int1.SelectedItem;        
            mode2 = (System.Drawing.Drawing2D.InterpolationMode)this.cmb_int2.SelectedItem;
            this.BuildImage();
        }
        private void BuildImage()
        {
            if (this.m_element  == null) return;
            if (this.pictureBox2.Image != null)
                this.pictureBox2.Image.Dispose();
            //change the factor
            int w = (int)(this.pictureBox1.Image.Width / (float)m_factor);
            int h = (int)(this.pictureBox1.Image.Height / (float)m_factor);
            if ((w == 0) || (h == 0))
                return;
            Bitmap bmp = new Bitmap(w, h);
            Bitmap outbmp = new Bitmap(this.pictureBox1.Image.Width, this.pictureBox1.Image.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.InterpolationMode = mode;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.DrawImage(this.pictureBox1.Image, new Rectangle(0, 0, bmp.Width, bmp.Height),
            new Rectangle(0, 0, bmp.Width * m_factor, bmp.Height * m_factor), GraphicsUnit.Pixel
                );
            g.Flush();
            g.Dispose();
            g = Graphics.FromImage(outbmp);
            //set property after property
            g.InterpolationMode = mode2;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.DrawImage(bmp, new Rectangle(Point.Empty, outbmp.Size));
            g.Flush();
            g.Dispose();
            this.pictureBox2.Image = outbmp;
        }
        private void XComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmb_factor.SelectedItem == null)
                return;
            string txt = this.cmb_factor.SelectedItem.ToString();
            m_factor = int.Parse(txt.ToUpper().Split('X')[0]); ;
            this.BuildImage();
        }
        private void Btn_ok_click(object sender, EventArgs e)
        {
            this.m_element.SetBitmap((this.pictureBox2.Image.Clone() as Bitmap).ToCoreBitmap(), false);
            this.pictureBox2.Image.Dispose();
            this.pictureBox2.Image = null;
            this.m_oldBitmap.Dispose();
            this.pictureBox1.Image.Dispose();
        }
        private void XButton1_Click(object sender, EventArgs e)
        {
            //reatore old bitmap
            this.m_element.SetBitmap((this.m_oldBitmap.Clone() as Bitmap).ToCoreBitmap(), true);
            this.m_oldBitmap.Dispose();
        }
    }
}

