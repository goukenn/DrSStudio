

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: UIXCoreColorPicker.cs
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
file:UIXCoreColorPicker.cs
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
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.WinCore.WinUI.Controls;
using IGK.ICore.WinUI;
namespace IGK.ICore.WinUI
{
    [DefaultEvent("SelectedColorChanged")]
    public class UIXCoreColorPicker : 
        UIXConfigControlBase
    {
       private Colorf m_color;
        private Colorf m_baseColor;
        private float  m_baseAlpha;
        private bool m_configuring;
        public event EventHandler SelectedColorChanged;
        public Colorf SelectedColor
        {
            get
            {
                return this.m_color;
            }
            set
            {
                if (this.m_color != value)
                {
                    this.m_color = value;
                    if (!this.m_configuring)
                    {
                        this.m_configuring = true;
                        this.c_CircleColorSelector.Color = value;
                        this.SetColorInfo();
                        this.m_configuring = false;
                        
                    }
                    OnSelectedColorChanged(EventArgs.Empty);
                }
            }
        }
        private void OnSelectedColorChanged(EventArgs eventArgs)
        {
            if (this.SelectedColorChanged != null)
            {
                this.SelectedColorChanged(this, eventArgs);
            }
        }
        public UIXCoreColorPicker()
            : base()
        {
            this.InitializeComponent();
            this.m_configuring = true;
            this.m_baseAlpha = 255;
            this.c_a.Value = 255;
            this.m_configuring = false;
        }
        private void InitializeComponent()
        {
            this.lb_l = new IGKXLabel();
            this.lb_r = new IGKXLabel();
            this.lb_a = new IGKXLabel();
            this.lb_g = new IGKXLabel();
            this.lb_b = new IGKXLabel();
            this.xLabel5 = new IGKXLabel();
            this.lb_a_caption = new IGKXLabel();
            this.xLabel3 = new IGKXLabel();
            this.xLabel2 = new IGKXLabel();
            this.xLabel1 = new IGKXLabel();
            this.c_lum = new IGKXDualColorTrackBar();
            this.c_a = new IGKXDualColorTrackBar();
            this.c_r = new IGKXDualColorTrackBar();
            this.c_g = new IGKXDualColorTrackBar();
            this.c_b = new IGKXDualColorTrackBar();
            this.xRuleLabel1 = new IGKXRuleLabel();
            this.c_CircleColorSelector = new IGKXCircleColorSelector();
            this.SuspendLayout();
            // 
            // lb_l
            // 
            this.lb_l.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_l.CaptionKey = "50%";
            this.lb_l.Location = new System.Drawing.Point(352, 119);
            this.lb_l.Name = "lb_l";
            this.lb_l.Size = new System.Drawing.Size(34, 13);
            this.lb_l.TabIndex = 14;
            // 
            // lb_r
            // 
            this.lb_r.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_r.CaptionKey = "255";
            this.lb_r.Location = new System.Drawing.Point(352, 38);
            this.lb_r.Name = "lb_r";
            this.lb_r.Size = new System.Drawing.Size(34, 13);
            this.lb_r.TabIndex = 2;
            // 
            // lb_a
            // 
            this.lb_a.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_a.CaptionKey = "100%";
            this.lb_a.Location = new System.Drawing.Point(352, 102);
            this.lb_a.Name = "lb_a";
            this.lb_a.Size = new System.Drawing.Size(34, 13);
            this.lb_a.TabIndex = 11;
            // 
            // lb_g
            // 
            this.lb_g.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_g.CaptionKey = "255";
            this.lb_g.Location = new System.Drawing.Point(352, 55);
            this.lb_g.Name = "lb_g";
            this.lb_g.Size = new System.Drawing.Size(34, 13);
            this.lb_g.TabIndex = 5;
            // 
            // lb_b
            // 
            this.lb_b.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_b.CaptionKey = "255";
            this.lb_b.Location = new System.Drawing.Point(352, 70);
            this.lb_b.Name = "lb_b";
            this.lb_b.Size = new System.Drawing.Size(34, 13);
            this.lb_b.TabIndex = 8;
            // 
            // xLabel5
            // 
            this.xLabel5.CaptionKey = "lb.L.caption";
            this.xLabel5.Location = new System.Drawing.Point(140, 116);
            this.xLabel5.Name = "xLabel5";
            this.xLabel5.Size = new System.Drawing.Size(26, 16);
            this.xLabel5.TabIndex = 12;
            // 
            // lb_a_caption
            // 
            this.lb_a_caption.CaptionKey = "lb.A.caption";
            this.lb_a_caption.Location = new System.Drawing.Point(140, 100);
            this.lb_a_caption.Name = "lb_a_caption";
            this.lb_a_caption.Size = new System.Drawing.Size(26, 16);
            this.lb_a_caption.TabIndex = 9;
            // 
            // xLabel3
            // 
            this.xLabel3.CaptionKey = "lb.B.caption";
            this.xLabel3.Location = new System.Drawing.Point(140, 70);
            this.xLabel3.Name = "xLabel3";
            this.xLabel3.Size = new System.Drawing.Size(26, 16);
            this.xLabel3.TabIndex = 6;
            // 
            // xLabel2
            // 
            this.xLabel2.CaptionKey = "lb.G.caption";
            this.xLabel2.Location = new System.Drawing.Point(140, 54);
            this.xLabel2.Name = "xLabel2";
            this.xLabel2.Size = new System.Drawing.Size(26, 16);
            this.xLabel2.TabIndex = 3;
            // 
            // xLabel1
            // 
            this.xLabel1.CaptionKey = "lb.R.caption";
            this.xLabel1.Location = new System.Drawing.Point(140, 37);
            this.xLabel1.Name = "xLabel1";
            this.xLabel1.Size = new System.Drawing.Size(26, 16);
            this.xLabel1.TabIndex = 0;
            // 
            // c_lum
            // 
            this.c_lum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c_lum.Cursor = System.Windows.Forms.Cursors.Hand;
            this.c_lum.EndColor = Colorf.White;
            this.c_lum.Location = new System.Drawing.Point(172, 119);
            this.c_lum.MaxValue = 255F;
            this.c_lum.MinValue = -255F;
            this.c_lum.Name = "c_lum";
            this.c_lum.Size = new System.Drawing.Size(174, 10);
            this.c_lum.StartColor = Colorf.Black;
            this.c_lum.Step = 1F;
            this.c_lum.TabIndex = 13;
            this.c_lum.Value = 0F;
            this.c_lum.ValueChanged += new System.EventHandler(this._lumChanged);
            // 
            // c_a
            // 
            this.c_a.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c_a.Cursor = System.Windows.Forms.Cursors.Hand;
            this.c_a.EndColor = Colorf.Black;
            this.c_a.Location = new System.Drawing.Point(172, 103);
            this.c_a.MaxValue = 255F;
            this.c_a.MinValue = 0F;
            this.c_a.Name = "c_a";
            this.c_a.Size = new System.Drawing.Size(174, 10);
            this.c_a.StartColor = Colorf.Transparent;
            this.c_a.Step = 1F;
            this.c_a.TabIndex = 10;
            this.c_a.Value = 0F;
            this.c_a.ValueChanged += new System.EventHandler(this._aChanged);
            // 
            // c_r
            // 
            this.c_r.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c_r.Cursor = System.Windows.Forms.Cursors.Hand;
            this.c_r.EndColor = Colorf.Red;
            this.c_r.Location = new System.Drawing.Point(172, 39);
            this.c_r.MaxValue = 255F;
            this.c_r.MinValue = 0F;
            this.c_r.Name = "c_r";
            this.c_r.Size = new System.Drawing.Size(174, 10);
            this.c_r.StartColor = Colorf.Black;
            this.c_r.Step = 1F;
            this.c_r.TabIndex = 1;
            this.c_r.Value = 255F;
            this.c_r.ValueChanged += new System.EventHandler(this._ValueChanged);
            // 
            // c_g
            // 
            this.c_g.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c_g.Cursor = System.Windows.Forms.Cursors.Hand;
            this.c_g.EndColor = Colorf.Lime;
            this.c_g.Location = new System.Drawing.Point(172, 55);
            this.c_g.MaxValue = 255F;
            this.c_g.MinValue = 0F;
            this.c_g.Name = "c_g";
            this.c_g.Size = new System.Drawing.Size(174, 10);
            this.c_g.StartColor = Colorf.Black;
            this.c_g.Step = 1F;
            this.c_g.TabIndex = 4;
            this.c_g.Value = 255F;
            this.c_g.ValueChanged += new System.EventHandler(this._ValueChanged);
            // 
            // c_b
            // 
            this.c_b.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c_b.Cursor = System.Windows.Forms.Cursors.Hand;
            this.c_b.EndColor = Colorf.Blue;
            this.c_b.Location = new System.Drawing.Point(172, 71);
            this.c_b.MaxValue = 255F;
            this.c_b.MinValue = 0F;
            this.c_b.Name = "c_b";
            this.c_b.Size = new System.Drawing.Size(174, 10);
            this.c_b.StartColor = Colorf.Black;
            this.c_b.Step = 1F;
            this.c_b.TabIndex = 7;
            this.c_b.Value = 255F;
            this.c_b.ValueChanged += new System.EventHandler(this._ValueChanged);
            // 
            // xRuleLabel1
            // 
            this.xRuleLabel1.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xRuleLabel1.CaptionKey = null;
            this.xRuleLabel1.Location = new System.Drawing.Point(140, 87);
            this.xRuleLabel1.Name = "xRuleLabel1";
            this.xRuleLabel1.Size = new System.Drawing.Size(246, 10);
            this.xRuleLabel1.TabIndex = 16;
            // 
            // c_CircleColorSelector
            // 
            this.c_CircleColorSelector.Color = Colorf.FromFloat(0F, 0F, 0F, 0F);
            this.c_CircleColorSelector.Location = new System.Drawing.Point(3, 14);
            this.c_CircleColorSelector.Name = "c_CircleColorSelector";
            this.c_CircleColorSelector.Size = new System.Drawing.Size(143, 136);
            this.c_CircleColorSelector.TabIndex = 15;
            this.c_CircleColorSelector.ColorChanged += new System.EventHandler(this.c_CircleColorSelector_ColorChanged);
            // 
            // UIXCoreColorPicker
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.lb_l);
            this.Controls.Add(this.lb_r);
            this.Controls.Add(this.lb_a);
            this.Controls.Add(this.lb_g);
            this.Controls.Add(this.lb_b);
            this.Controls.Add(this.xLabel5);
            this.Controls.Add(this.lb_a_caption);
            this.Controls.Add(this.xLabel3);
            this.Controls.Add(this.xLabel2);
            this.Controls.Add(this.xLabel1);
            this.Controls.Add(this.c_lum);
            this.Controls.Add(this.c_a);
            this.Controls.Add(this.c_r);
            this.Controls.Add(this.c_g);
            this.Controls.Add(this.c_b);
            this.Controls.Add(this.xRuleLabel1);
            this.Controls.Add(this.c_CircleColorSelector);
            this.MinimumSize = new System.Drawing.Size(0, 116);
            this.Name = "UIXCoreColorPicker";
            this.Size = new System.Drawing.Size(379, 165);
            this.ResumeLayout(false);
        }
        /// <summary>
        /// set color information
        /// </summary>
        private void SetColorInfo()
        {
            if (!this.m_configuring) return;
            c_r.Value = SelectedColor.R.TrimByte ();
            c_g.Value = SelectedColor.G.TrimByte ();
            c_b.Value = SelectedColor.B.TrimByte ();
            c_a.Value = SelectedColor.A.TrimByte ();
            c_lum.Value = GetLuminosityValue(this.SelectedColor);
            c_a.StartColor = this.SelectedColor;
            c_a.EndColor = this.SelectedColor;
            m_baseAlpha = (this.SelectedColor.A * 255).TrimByte ();
            m_baseColor = this.SelectedColor;
            this.SetText();
        }
        private void _ValueChanged(object sender, EventArgs e)
        {
            if (this.m_configuring)
                return;
            this.m_configuring = true;
            int r;
            int g;
            int b;
            int a;
            r = (int)this.c_r.Value;
            g = (int)this.c_g.Value;
            b = (int)this.c_b.Value;
            a = (int)this.c_a.Value;
            Colorf cl = Colorf.FromIntArgb(a, r, g, b);
            this.m_color = cl;
            this.m_baseColor = cl;
            this.SetText();
            c_CircleColorSelector.Color = cl;
            c_a.EndColor = cl;
            c_a.StartColor = cl;
            c_lum.Value = (int)GetLuminosityValue(cl);
            this.OnSelectedColorChanged(EventArgs.Empty);
            this.m_configuring = false;
        }
        int GetLuminosityValue(Colorf cl)
        {
            return (int)(c_lum.MinValue + (Colorf.GetLuminosity(cl) * (c_lum.MaxValue - c_lum.MinValue) / 100.0f));
        }
        void SetText()
        {
            float r = SelectedColor.R;
            float g = SelectedColor.G;
            float b = SelectedColor.B;
            float a = SelectedColor.A;
            this.lb_r.Text = string.Format("{0:##0.###}",(int) (r *255));// _r.Value);
            this.lb_g.Text = string.Format("{0:##0.###}", (int)(g * 255));// _g.Value);
            this.lb_b.Text = string.Format("{0:##0.###}", (int)(b *255));// _b.Value);
            this.lb_a.Text = string.Format("{0:##0.###}%", 100.0f * a);
            this.lb_l.Text = string.Format("{0:##0}%", Colorf.GetLuminosity(this.SelectedColor));
        }
        private void _aChanged(object sender, EventArgs e)
        {
            if (this.m_configuring) return;
            m_configuring = true;
            Colorf cl = this.SelectedColor;
            m_baseAlpha = (int)c_a.Value;
            this.m_color = Colorf.FromFloat (m_baseAlpha/255.0f, Colorf.FromFloat(1.0f, cl.R, cl.G, cl.B));
            this.lb_a.Text = string.Format("{0:##0}%", 100.0f * c_a.Value / 255.0f);
            this.c_a.EndColor = cl;
            this.c_a.StartColor = cl;
            this.OnSelectedColorChanged(EventArgs.Empty);
            m_configuring = false;
        }
        private void _lumChanged(object sender, EventArgs e)
        {
            //this.lb_a.Text = string.Format("{0:000}", _lum.Value);
            //calculat luminance
            if (this.m_configuring)
                return;
            this.m_configuring = true;
            Colorf cl = this.m_baseColor;
            float  r = cl.R;
            float g = cl.G;
            float b = cl.B;
            float a = cl.A;
            int lum = (int)c_lum.Value;
            int tol = lum;// (int)((-lum + (r + g + b)) / 3.0f);
#pragma warning disable IDE0054 // Use compound assignment
            r = ((r = (r + tol)) < 0) ? 0 : (r > 255) ? 255 : r;
#pragma warning restore IDE0054 // Use compound assignment
#pragma warning disable IDE0054 // Use compound assignment
            g = ((g = (g + tol)) < 0) ? 0 : (g > 255) ? 255 : g;
#pragma warning restore IDE0054 // Use compound assignment
#pragma warning disable IDE0054 // Use compound assignment
            b = ((b = (b + tol)) < 0) ? 0 : (b > 255) ? 255 : b;
#pragma warning restore IDE0054 // Use compound assignment
            //r = pcl.R;
            //g = pcl.G;
            //b = pcl.B;
            cl = Colorf.FromIntArgb((int)this.m_baseAlpha,(int) r,(int) g,(int) b);
            this.c_CircleColorSelector.Color = cl;
            this.m_color = cl;
            //setup color set
            this.c_a.EndColor = cl;
            this.c_a.StartColor = cl;
            //set value
            this.c_b.Value = (int)(cl.B * 255);
            this.c_r.Value = (int)(cl.R * 255);
            this.c_g.Value = (int)(cl.G * 255);
            this.c_a.Value =(int) (cl.A * 255);
            SetText();
            this.OnSelectedColorChanged(EventArgs.Empty);
            this.m_configuring = false;
        }
        private void c_CircleColorSelector_ColorChanged(object sender, EventArgs e)
        {
            if (this.m_configuring)
                return;
            m_configuring = true;
            Colorf cl = c_CircleColorSelector.Color;
            Color v_sysColor = cl.CoreConvertTo<Color>();
            m_baseColor = Colorf.FromFloat (1.0f, cl);
            c_r.Value = v_sysColor.R;
            c_g.Value = v_sysColor.G;
            c_b.Value = v_sysColor.B;
            c_lum.Value = this.GetLuminosityValue(cl);
            this.m_color = Colorf.FromFloat(this.m_baseAlpha/255.0f, m_baseColor);
            c_a.StartColor = m_baseColor;
            c_a.EndColor = m_baseColor;
            SetText();
            this.OnSelectedColorChanged(EventArgs.Empty);
            m_configuring = false;
        }
        private IGKXCircleColorSelector c_CircleColorSelector;
        private IGKXDualColorTrackBar c_lum;
        private IGKXDualColorTrackBar c_a;
        private IGKXDualColorTrackBar c_r;
        private IGKXDualColorTrackBar c_g;
        private IGKXDualColorTrackBar c_b;
        private IGKXRuleLabel xRuleLabel1;
        private IGKXLabel xLabel1;
        private IGKXLabel xLabel2;
        private IGKXLabel xLabel3;
        private IGKXLabel lb_a_caption;
        private IGKXLabel xLabel5;
        private IGKXLabel lb_l;
        private IGKXLabel lb_r;
        private IGKXLabel lb_a;
        private IGKXLabel lb_g;
        private IGKXLabel lb_b;
    }
}

