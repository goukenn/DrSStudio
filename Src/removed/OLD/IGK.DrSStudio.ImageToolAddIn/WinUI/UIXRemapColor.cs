

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: UIXRemapColor.cs
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
file:UIXRemapColor.cs
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
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IGK.ICore;using IGK.DrSStudio.WinUI;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace IGK.DrSStudio.Drawing2D.WinUI
{
    using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio;
    using IGK.DrSStudio.Codec;
    using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio.Menu;
    using IGK.DrSStudio.Tools;
    /// <summary>
    /// represent control to remap color
    /// </summary>
    public class UIXRemapColor : XAcceptOrCancelControl 
    {
        private Colorf m_startColor;
        private Colorf m_endColor;
        private IGKXRuleLabel xRuleLabel1;
        private IGKXButton btn_cancel;
        private IGKXButton btn_ok;
        private IGKXGroupBox grb_before;
        private System.Windows.Forms.PictureBox picB_before;
        private System.Windows.Forms.PictureBox picB_After;
        private IGKXGroupBox xGroupBox1;
        private IGKXLabel xLabel5;
        private IGKXDualColorTrackBar xDualColorTrackBar1;
        private ImageElement m_image;
        private int factor;
        private System.Windows.Forms.Label label1;
        private UIXCoreColorPicker uixColorPicker1;
        private UIXCoreColorPicker uixColorPicker2;
        private IGKXButton btn_apply;
        //get the real old image
        private Bitmap m_oldlimage;
        //properties
        public Colorf StartColor
        {
            get
            {
                return this.m_startColor;
            }
            protected internal set
            {
                this.uixColorPicker1.SelectedColor = value;
            }
        }
        public Colorf EndColor
        {
            get
            {
                return this.m_endColor;
            }
            protected internal set
            {
                this.uixColorPicker2.SelectedColor = value;
            }
        }
        public ImageElement  Image
        {
            get
            {
                return this.m_image;
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.m_oldlimage != null)
                {
                    this.m_oldlimage.Dispose();
                    this.m_oldlimage = null;
                }
            }
            base.Dispose(disposing);
        }
        public UIXRemapColor(ImageElement image)
        {
            this.m_image = image;
            this.InitializeComponent();
            this.btn_apply.DialogResult = enuDialogResult.None;
            this.StartColor = Colorf.White;
            this.EndColor = Colorf.White;
            this.InitControl();
        }
        private void InitControl()
        {
            this.factor = 1;
            this.m_oldlimage = this.m_image.Bitmap.Clone() as Bitmap;
            BuildBeforePicture();
            this.PreviewBuildImage();
        }
        private void BuildBeforePicture()
        {
            Bitmap v_bmp = new Bitmap(this.picB_before.Width,
              this.picB_before.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(v_bmp);
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.SmoothingMode = SmoothingMode.None;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.CompositingMode = CompositingMode.SourceOver;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.DrawImage(this.m_oldlimage, new Rectangle(Point.Empty, v_bmp.Size));
            g.Flush();
            g.Dispose();
            this.picB_before.Image = v_bmp;
        }
        void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PreviewBuildImage();
        }
        static Bitmap ApplyProperty(Bitmap picbmp, Colorf oldColor, Colorf newColor, float factor)
        {
            if (picbmp == null)
                throw new CoreException(enuExceptionType.ArgumentIsNull, "picbmp");
            if (factor == 1)
            {
                ColorMap cl = new ColorMap();
                cl.OldColor = oldColor.CoreConvertTo<Color>();
                cl.NewColor = newColor.CoreConvertTo<Color>();
                int w = picbmp.Width;
                int h = picbmp.Height;
                Bitmap bmp = new Bitmap(w, h);
                Rectangle rc = new Rectangle(0, 0, w, h);
                Graphics g = Graphics.FromImage(bmp);
                ImageAttributes attr = new ImageAttributes();
                attr.SetRemapTable(new ColorMap[] { cl });
                g.DrawImage(picbmp, rc, 0, 0, w, h, GraphicsUnit.Pixel, attr);
                g.Flush();
                g.Dispose();
                return bmp;
            }
            else
            {
                return BuilImageMultiple(picbmp, oldColor, newColor, factor);
            }
        }
        static Bitmap BuilImageMultiple(Bitmap picbmp, Colorf oldColor, Colorf nColor, float factor)
        {
            if (picbmp == null)
                throw new CoreException(enuExceptionType.ArgumentIsNull, "picbmp");
            Bitmap bmp = picbmp.Clone() as Bitmap;
            int w = bmp.Width;
            int h = bmp.Height;
            int r, g, b = 0;
            int offset = 0;
            Rectangle rc = new Rectangle(0, 0, w, h);
            BitmapData bmpData = bmp.LockBits(rc, ImageLockMode.ReadWrite, PixelFormat.Format32bppPArgb);
            byte[] data = new byte[bmpData.Stride * h];
            CoreColorHandle.HSV cl_hsv = CoreColorHandle.RGBtoHSV(oldColor);
            CoreColorHandle.HSV cl_hsv2;
            Marshal.Copy(bmpData.Scan0, data, 0, data.Length);
            float angle = (cl_hsv.Hue * 360 / 255.0f);
            float angle2 = angle;
            float step = (factor * 360 / 100.0f);
            float tolA_ = angle + step;
            float tolB_ = angle - step;
            //convert to value
            int top = (int)(tolA_ * 255 / 360.0f);
            int bottom = (int)(tolB_ * 255 / 360.0f);
            for (int j = 0; j < h; ++j)
            {
                for (int i = 0; i < w; ++i)
                {
                    offset = j * bmpData.Stride + i * 4;
                    r = data[offset];
                    g = data[offset + 1];
                    b = data[offset + 2];
                    cl_hsv2 = CoreColorHandle.RGBtoHSV(Color.FromArgb(r, g, b));
                    angle2 = (cl_hsv2.Hue * 360 / 255.0f);
                    if (Math.Abs(angle2 - angle) <= step)
                    {
                        data[offset] = nColor.R;
                        data[offset + 1] = nColor.G;
                        data[offset + 2] = nColor.B;
                    }
                }
            }
            Marshal.Copy(data, 0, bmpData.Scan0, data.Length);
            bmp.UnlockBits(bmpData);
            return bmp;
        }
        private void InitializeComponent()
        {
            this.xRuleLabel1 = new IGK.DrSStudio.WinUI.IGKXRuleLabel();
            this.btn_cancel = new IGK.DrSStudio.WinUI.IGKXButton();
            this.btn_ok = new IGK.DrSStudio.WinUI.IGKXButton();
            this.grb_before = new IGK.DrSStudio.WinUI.IGKXGroupBox();
            this.uixColorPicker1 = new IGK.DrSStudio.WinUI.UIXCoreColorPicker();
            this.picB_before = new System.Windows.Forms.PictureBox();
            this.xGroupBox1 = new IGK.DrSStudio.WinUI.IGKXGroupBox();
            this.uixColorPicker2 = new IGK.DrSStudio.WinUI.UIXCoreColorPicker();
            this.picB_After = new System.Windows.Forms.PictureBox();
            this.xLabel5 = new IGK.DrSStudio.WinUI.IGKXLabel();
            this.xDualColorTrackBar1 = new IGK.DrSStudio.WinUI.IGKXDualColorTrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_apply = new IGK.DrSStudio.WinUI.IGKXButton();
            this.grb_before.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picB_before)).BeginInit();
            this.xGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picB_After)).BeginInit();
            this.SuspendLayout();
            // 
            // xRuleLabel1
            // 
            this.xRuleLabel1.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            
            this.xRuleLabel1.CaptionKey = null;
            this.xRuleLabel1.Location = new System.Drawing.Point(9, 447);
            this.xRuleLabel1.Name = "xRuleLabel1";
            this.xRuleLabel1.Size = new System.Drawing.Size(650, 10);
            this.xRuleLabel1.TabIndex = 0;
            this.xRuleLabel1.Click += new System.EventHandler(this.xRuleLabel1_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_cancel.CaptionKey = CoreConstant.BTN_CANCEL;
            this.btn_cancel.DialogResult = enuDialogResult.Cancel;
            this.btn_cancel.Location = new System.Drawing.Point(581, 462);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 1;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_ok
            // 
            this.btn_ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ok.CaptionKey = CoreConstant.BTN_OK;
            this.btn_ok.DialogResult = enuDialogResult.OK;
            this.btn_ok.Location = new System.Drawing.Point(494, 462);
            this.btn_ok.Name = CoreConstant.BTN_OK;
            this.btn_ok.Size = new System.Drawing.Size(75, 23);
            this.btn_ok.TabIndex = 2;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // grb_before
            //                         
            this.grb_before.Controls.Add(this.uixColorPicker1);
            this.grb_before.Controls.Add(this.picB_before);
            this.grb_before.Location = new System.Drawing.Point(9, 0);
            this.grb_before.Name = "grb_before";
            this.grb_before.Size = new System.Drawing.Size(322, 413);
            this.grb_before.TabIndex = 3;
            this.grb_before.TabStop = false;
            this.grb_before.Text = "ImageGdi.before.caption";
            // 
            // uixColorPicker1
            // 
            this.uixColorPicker1.BackColor = System.Drawing.SystemColors.Control;
            this.uixColorPicker1.CaptionKey = null;
            this.uixColorPicker1.Location = new System.Drawing.Point(6, 19);
            this.uixColorPicker1.MinimumSize = new System.Drawing.Size(0, 116);
            this.uixColorPicker1.Name = "uixColorPicker1";
            this.uixColorPicker1.Size = new System.Drawing.Size(310, 116);
            this.uixColorPicker1.TabIndex = 1;
            this.uixColorPicker1.SelectedColorChanged += new System.EventHandler(this.uixColorPicker1_ColorChanged);
            // 
            // picB_before
            // 
            this.picB_before.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picB_before.Location = new System.Drawing.Point(6, 141);
            this.picB_before.Name = "picB_before";
            this.picB_before.Size = new System.Drawing.Size(310, 265);
            this.picB_before.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picB_before.TabIndex = 0;
            this.picB_before.TabStop = false;
            this.picB_before.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picB_before_MouseClick);
            // 
            // xGroupBox1
            //             
            this.xGroupBox1.Controls.Add(this.uixColorPicker2);
            this.xGroupBox1.Controls.Add(this.picB_After);
            this.xGroupBox1.Location = new System.Drawing.Point(337, 0);
            this.xGroupBox1.Name = "xGroupBox1";
            this.xGroupBox1.Size = new System.Drawing.Size(322, 413);
            this.xGroupBox1.TabIndex = 4;
            this.xGroupBox1.TabStop = false;
            this.xGroupBox1.Text = "ImageGdi.after.caption";
            // 
            // uixColorPicker2
            // 
            this.uixColorPicker2.BackColor = System.Drawing.SystemColors.Control;
            this.uixColorPicker2.Location = new System.Drawing.Point(6, 19);
            this.uixColorPicker2.MinimumSize = new System.Drawing.Size(0, 116);
            this.uixColorPicker2.Name = "uixColorPicker2";
            this.uixColorPicker2.Size = new System.Drawing.Size(310, 116);
            this.uixColorPicker2.TabIndex = 2;
            this.uixColorPicker2.SelectedColorChanged += new System.EventHandler(this.uixColorPicker2_ColorChanged);
            // 
            // picB_After
            // 
            this.picB_After.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picB_After.Location = new System.Drawing.Point(6, 141);
            this.picB_After.Name = "picB_After";
            this.picB_After.Size = new System.Drawing.Size(310, 265);
            this.picB_After.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picB_After.TabIndex = 0;
            this.picB_After.TabStop = false;
            // 
            // xLabel5
            // 
            this.xLabel5.AutoSize = true;
            this.xLabel5.CaptionKey = "lb.factor.caption";
            this.xLabel5.Location = new System.Drawing.Point(9, 426);
            this.xLabel5.Name = "xLabel5";
            this.xLabel5.Size = new System.Drawing.Size(83, 13);
            this.xLabel5.TabIndex = 13;
            // 
            // xDualColorTrackBar1
            // 
            
            this.xDualColorTrackBar1.EndColor = Colorf.White;
            this.xDualColorTrackBar1.Location = new System.Drawing.Point(120, 419);
            this.xDualColorTrackBar1.MaxValue = 100F;
            this.xDualColorTrackBar1.MinValue = 1F;
            this.xDualColorTrackBar1.Name = "xDualColorTrackBar1";
            this.xDualColorTrackBar1.Size = new System.Drawing.Size(205, 20);
            this.xDualColorTrackBar1.StartColor = Colorf.White;
            this.xDualColorTrackBar1.Step = 1F;
            this.xDualColorTrackBar1.TabIndex = 14;
            this.xDualColorTrackBar1.Value = 1F;
            this.xDualColorTrackBar1.ValueChanged += new System.EventHandler(this.xDualColorTrackBar1_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(340, 426);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "1";
            // 
            // btn_apply
            // 
            this.btn_apply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_apply.CaptionKey = "btn.apply.caption";
            this.btn_apply.DialogResult = enuDialogResult.OK;
            this.btn_apply.Location = new System.Drawing.Point(413, 462);
            this.btn_apply.Name = "btn_apply";
            this.btn_apply.Size = new System.Drawing.Size(75, 23);
            this.btn_apply.TabIndex = 16;
            this.btn_apply.Click += new System.EventHandler(this.btn_apply_Click);
            // 
            // UIXImageRemapColorAttribute
            // 
            this.CaptionKey = "UIXImageRemapColor";
            this.Controls.Add(this.btn_apply);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.xDualColorTrackBar1);
            this.Controls.Add(this.xLabel5);
            this.Controls.Add(this.xGroupBox1);
            this.Controls.Add(this.grb_before);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.xRuleLabel1);
            this.Name = "UIXImageRemapColorAttribute";
            this.Size = new System.Drawing.Size(662, 488);
            this.grb_before.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picB_before)).EndInit();
            this.xGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picB_After)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private void btn_ok_Click(object sender, EventArgs e)
        {
            this.m_image.SetBitmap (ApplyProperty(this.m_oldlimage, this.StartColor, this.EndColor, this.factor), false);
            //free resource
            FreeResources();
        }
        private void FreeResources()
        {
            this.picB_After.Image.Dispose();
            this.picB_After.Image = null;
            this.picB_before.Image.Dispose();
            this.picB_before.Image = null;
            if (this.m_oldlimage != null)
                this.m_oldlimage.Dispose();
        }
        void PreviewBuildImage()
        {
            if (this.picB_before.Image != null)
                this.picB_After.Image = ApplyProperty(picB_before.Image as Bitmap, this.StartColor, this.EndColor, this.factor);
        }
        private void xDualColorTrackBar1_ValueChanged(object sender, EventArgs e)
        {
            this.factor = (int)xDualColorTrackBar1.Value;
            this.label1.Text = string.Format("{0:#0}", this.factor);
            this.PreviewBuildImage();
        }
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            //restore old bitmap
        }
        private void xRuleLabel1_Click(object sender, EventArgs e)
        {
        }
        private void uixColorPicker1_ColorChanged(object sender, EventArgs e)
        {
            this.m_startColor = uixColorPicker1.SelectedColor;
            this.PreviewBuildImage();
        }
        private void uixColorPicker2_ColorChanged(object sender, EventArgs e)
        {
            this.m_endColor = uixColorPicker2.SelectedColor;
            this.PreviewBuildImage();
        }
        private void picB_before_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    //-----------------------------------------------------------------------
                    //pick color
                    //-----------------------------------------------------------------------
                    this.uixColorPicker1.SelectedColor = GetColor(e.Location);
                    break;
            }
        }
        private Color GetColor(Point point)
        {
            Color cl = Color.Empty;
            int w = this.picB_before.Image.Width;
            int h = this.picB_before.Image.Height;
            float zoomx = this.picB_before.Width / (float)w;
            float zoomy = this.picB_before.Height / (float)h;
            zoomx = Math.Min(zoomx, zoomy);
            zoomy = zoomx;
            //get image location
            Vector2f v_imgstartlocation = new Vector2f((this.picB_before.Width - w * zoomx) / 2,
                (this.picB_before.Height - h * zoomy) / 2);
            Vector2f v_loc = new Vector2f(
              (point.X - v_imgstartlocation.X) / zoomx,
              (point.Y - v_imgstartlocation.Y) / zoomy);
            int v_w = (int)v_loc.X;
            int v_h = (int)v_loc.Y;
            if ((v_w >= 0) && (v_w < w) && (v_h >= 0) && (v_h < h))
                cl = (this.picB_before.Image as Bitmap).GetPixel((int)v_w, (int)v_h);
            return cl;
        }
        private void btn_apply_Click(object sender, EventArgs e)
        {
            this.m_image.SetBitmap(ApplyProperty(this.m_oldlimage, this.StartColor, this.EndColor, this.factor),false );
            this.m_image.Invalidate(true);
            this.FreeResources();
            //free resources
            //init 
            this.m_oldlimage = this.m_image.Bitmap.Clone() as Bitmap;
            this.BuildBeforePicture();
            this.PreviewBuildImage();
        }
    }
}

