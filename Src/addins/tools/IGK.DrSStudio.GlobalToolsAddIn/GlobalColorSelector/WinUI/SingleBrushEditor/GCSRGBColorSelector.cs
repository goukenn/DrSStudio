

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GCSRGBColorSelector.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:RGB_ColorSelector.cs
*/
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinCore.WinUI.Controls;
namespace IGK.DrSStudio
{
    /// <summary>
    /// represent a rgb color picker
    /// </summary>
    class GCSRGBColorSelector : GCSingleColorPickerBase, IGCSSingleColorPicker
    {
        private IGKXLabel c_lb_a;
        private IGKXLabel c_lb_l;
        private IGKXLabel c_lb_b;
        private IGKXLabel c_lb_g;
        private IGKXLabel c_lb_r;
        private IGKXTextBox c_txb_a;
        private IGKXTextBox c_txb_l;
        private IGKXNumericTextBox c_txb_blue;
        private IGKXNumericTextBox c_txb_green;
        private IGKXNumericTextBox c_txb_red;
        private IGKXDualColorTrackBar c_alpha;
        private IGKXDualColorTrackBar c_light;
        private IGKXDualColorTrackBar c_blue;
        private IGKXDualColorTrackBar c_green;
        private IGKXDualColorTrackBar c_red;
        private IGKXCircleColorSelector c_circleColorSelector;
        private IGKXRuleLabel c_delimiter1;
        private bool m_ConfigFlag;
        private Colorf m_baseColor;
       


        public GCSRGBColorSelector()
        {
            this.InitializeComponent();
            this.ColorChanged += _ColorChanged;
        }
        //init color changed
        void _ColorChanged(object sender, EventArgs e)
        {
            //
            if (m_ConfigFlag) return;
            m_ConfigFlag = true ;
            LoadColorString();
            m_ConfigFlag = false;
        }
        private void InitializeComponent()
        {
            this.c_lb_a = new IGK.ICore.WinCore.WinUI.Controls.IGKXLabel();
            this.c_lb_l = new IGK.ICore.WinCore.WinUI.Controls.IGKXLabel();
            this.c_lb_b = new IGK.ICore.WinCore.WinUI.Controls.IGKXLabel();
            this.c_lb_g = new IGK.ICore.WinCore.WinUI.Controls.IGKXLabel();
            this.c_lb_r = new IGK.ICore.WinCore.WinUI.Controls.IGKXLabel();
            this.c_txb_a = new IGK.ICore.WinUI.IGKXTextBox();
            this.c_txb_l = new IGK.ICore.WinUI.IGKXTextBox();
            this.c_txb_blue = new IGK.ICore.WinUI.IGKXNumericTextBox();
            this.c_txb_green = new IGK.ICore.WinUI.IGKXNumericTextBox();
            this.c_txb_red = new IGK.ICore.WinUI.IGKXNumericTextBox();
            this.c_alpha = new IGK.ICore.WinUI.IGKXDualColorTrackBar();
            this.c_light = new IGK.ICore.WinUI.IGKXDualColorTrackBar();
            this.c_blue = new IGK.ICore.WinUI.IGKXDualColorTrackBar();
            this.c_green = new IGK.ICore.WinUI.IGKXDualColorTrackBar();
            this.c_red = new IGK.ICore.WinUI.IGKXDualColorTrackBar();
            this.c_circleColorSelector = new IGK.ICore.WinUI.IGKXCircleColorSelector();
            this.c_delimiter1 = new IGK.ICore.WinUI.IGKXRuleLabel();
            this.SuspendLayout();
            // 
            // c_lb_a
            // 
            this.c_lb_a.CaptionKey = "lb.cl.A";
            this.c_lb_a.HorizontalAlignment = IGK.ICore.Drawing2D.enuStringAlignment.Near;
            this.c_lb_a.Location = new System.Drawing.Point(136, 110);
            this.c_lb_a.Name = "c_lb_a";
            this.c_lb_a.Size = new System.Drawing.Size(35, 14);
            this.c_lb_a.TabIndex = 46;
            this.c_lb_a.TabStop = false;
            this.c_lb_a.VerticalAlignment = IGK.ICore.Drawing2D.enuStringAlignment.Near;
            // 
            // c_lb_l
            // 
            this.c_lb_l.CaptionKey = "lb.cl.L";
            this.c_lb_l.HorizontalAlignment = IGK.ICore.Drawing2D.enuStringAlignment.Near;
            this.c_lb_l.Location = new System.Drawing.Point(136, 90);
            this.c_lb_l.Name = "c_lb_l";
            this.c_lb_l.Size = new System.Drawing.Size(34, 14);
            this.c_lb_l.TabIndex = 45;
            this.c_lb_l.TabStop = false;
            this.c_lb_l.VerticalAlignment = IGK.ICore.Drawing2D.enuStringAlignment.Near;
            // 
            // c_lb_b
            // 
            this.c_lb_b.CaptionKey = "lb.cl.B";
            this.c_lb_b.HorizontalAlignment = IGK.ICore.Drawing2D.enuStringAlignment.Near;
            this.c_lb_b.Location = new System.Drawing.Point(136, 52);
            this.c_lb_b.Name = "c_lb_b";
            this.c_lb_b.Size = new System.Drawing.Size(35, 14);
            this.c_lb_b.TabIndex = 44;
            this.c_lb_b.TabStop = false;
            this.c_lb_b.VerticalAlignment = IGK.ICore.Drawing2D.enuStringAlignment.Near;
            // 
            // c_lb_g
            // 
            this.c_lb_g.CaptionKey = "lb.cl.G";
            this.c_lb_g.HorizontalAlignment = IGK.ICore.Drawing2D.enuStringAlignment.Near;
            this.c_lb_g.Location = new System.Drawing.Point(136, 33);
            this.c_lb_g.Name = "c_lb_g";
            this.c_lb_g.Size = new System.Drawing.Size(36, 14);
            this.c_lb_g.TabIndex = 43;
            this.c_lb_g.TabStop = false;
            this.c_lb_g.VerticalAlignment = IGK.ICore.Drawing2D.enuStringAlignment.Near;
            // 
            // c_lb_r
            // 
            this.c_lb_r.CaptionKey = "lb.cl.R";
            this.c_lb_r.HorizontalAlignment = IGK.ICore.Drawing2D.enuStringAlignment.Near;
            this.c_lb_r.Location = new System.Drawing.Point(136, 14);
            this.c_lb_r.Name = "c_lb_r";
            this.c_lb_r.Size = new System.Drawing.Size(36, 14);
            this.c_lb_r.TabIndex = 42;
            this.c_lb_r.TabStop = false;
            this.c_lb_r.VerticalAlignment = IGK.ICore.Drawing2D.enuStringAlignment.Near;
            // 
            // c_txb_a
            // 
            this.c_txb_a.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.c_txb_a.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.c_txb_a.Location = new System.Drawing.Point(327, 109);
            this.c_txb_a.Name = "c_txb_a";
            this.c_txb_a.Size = new System.Drawing.Size(43, 20);
            this.c_txb_a.TabIndex = 10;
            this.c_txb_a.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.c_txb_a.TipText = null;
            // 
            // c_txb_l
            // 
            this.c_txb_l.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.c_txb_l.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.c_txb_l.Location = new System.Drawing.Point(327, 83);
            this.c_txb_l.Name = "c_txb_l";
            this.c_txb_l.Size = new System.Drawing.Size(43, 20);
            this.c_txb_l.TabIndex = 8;
            this.c_txb_l.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.c_txb_l.TipText = null;
            this.c_txb_l.TextChanged += new System.EventHandler(this.lb_l_TextChanged);
            // 
            // c_txb_blue
            // 
            this.c_txb_blue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.c_txb_blue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.c_txb_blue.Location = new System.Drawing.Point(327, 52);
            this.c_txb_blue.MaxLength = 10;
            this.c_txb_blue.Name = "c_txb_blue";
            this.c_txb_blue.Size = new System.Drawing.Size(43, 20);
            this.c_txb_blue.TabIndex = 6;
            this.c_txb_blue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.c_txb_blue.TipText = null;
            this.c_txb_blue.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.c_txb_blue.ValueChanged += new System.EventHandler(this.v_ValueChanged);
            // 
            // c_txb_green
            // 
            this.c_txb_green.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.c_txb_green.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.c_txb_green.Location = new System.Drawing.Point(327, 29);
            this.c_txb_green.MaxLength = 10;
            this.c_txb_green.Name = "c_txb_green";
            this.c_txb_green.Size = new System.Drawing.Size(43, 20);
            this.c_txb_green.TabIndex = 4;
            this.c_txb_green.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.c_txb_green.TipText = null;
            this.c_txb_green.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.c_txb_green.ValueChanged += new System.EventHandler(this.v_ValueChanged);
            // 
            // c_txb_red
            // 
            this.c_txb_red.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.c_txb_red.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.c_txb_red.Location = new System.Drawing.Point(327, 6);
            this.c_txb_red.MaxLength = 10;
            this.c_txb_red.Name = "c_txb_red";
            this.c_txb_red.Size = new System.Drawing.Size(43, 20);
            this.c_txb_red.TabIndex = 2;
            this.c_txb_red.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.c_txb_red.TipText = null;
            this.c_txb_red.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.c_txb_red.ValueChanged += new System.EventHandler(this.v_ValueChanged);
            // 
            // c_alpha
            // 
            this.c_alpha.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c_alpha.CaptionKey = null;
            this.c_alpha.Cursor = System.Windows.Forms.Cursors.Hand;
            this.c_alpha.DrawDash = true;
            this.c_alpha.Location = new System.Drawing.Point(158, 109);
            this.c_alpha.MaxValue = 255F;
            this.c_alpha.MinValue = 0F;
            this.c_alpha.Name = "c_alpha";
            this.c_alpha.Size = new System.Drawing.Size(163, 20);
            this.c_alpha.Step = 1F;
            this.c_alpha.TabIndex = 9;
            this.c_alpha.Value = 255F;
            // 
            // c_light
            // 
            this.c_light.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c_light.CaptionKey = null;
            this.c_light.Cursor = System.Windows.Forms.Cursors.Hand;
            this.c_light.DrawDash = false;
            this.c_light.Location = new System.Drawing.Point(158, 83);
            this.c_light.MaxValue = 255F;
            this.c_light.MinValue = -255F;
            this.c_light.Name = "c_light";
            this.c_light.Size = new System.Drawing.Size(163, 19);
            this.c_light.Step = 1F;
            this.c_light.TabIndex = 7;
            this.c_light.Value = 0F;
            // 
            // c_blue
            // 
            this.c_blue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c_blue.CaptionKey = null;
            this.c_blue.Cursor = System.Windows.Forms.Cursors.Hand;
            this.c_blue.DrawDash = false;
            this.c_blue.Location = new System.Drawing.Point(158, 52);
            this.c_blue.MaxValue = 255F;
            this.c_blue.MinValue = 0F;
            this.c_blue.Name = "c_blue";
            this.c_blue.Size = new System.Drawing.Size(163, 16);
            this.c_blue.Step = 1F;
            this.c_blue.TabIndex = 5;
            this.c_blue.Value = 255F;
            // 
            // c_green
            // 
            this.c_green.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c_green.CaptionKey = null;
            this.c_green.Cursor = System.Windows.Forms.Cursors.Hand;
            this.c_green.DrawDash = false;
            this.c_green.Location = new System.Drawing.Point(158, 31);
            this.c_green.MaxValue = 255F;
            this.c_green.MinValue = 0F;
            this.c_green.Name = "c_green";
            this.c_green.Size = new System.Drawing.Size(163, 16);
            this.c_green.Step = 1F;
            this.c_green.TabIndex = 3;
            this.c_green.Value = 255F;
            // 
            // c_red
            // 
            this.c_red.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c_red.CaptionKey = null;
            this.c_red.Cursor = System.Windows.Forms.Cursors.Hand;
            this.c_red.DrawDash = false;
            this.c_red.Location = new System.Drawing.Point(158, 10);
            this.c_red.MaxValue = 255F;
            this.c_red.MinValue = 0F;
            this.c_red.Name = "c_red";
            this.c_red.Size = new System.Drawing.Size(163, 16);
            this.c_red.Step = 1F;
            this.c_red.TabIndex = 1;
            this.c_red.Value = 255F;
            // 
            // c_circleColorSelector
            // 
            this.c_circleColorSelector.CaptionKey = null;
            this.c_circleColorSelector.Location = new System.Drawing.Point(3, 3);
            this.c_circleColorSelector.Name = "c_circleColorSelector";
            this.c_circleColorSelector.Size = new System.Drawing.Size(127, 134);
            this.c_circleColorSelector.TabIndex = 0;
            // 
            // c_delimiter1
            // 
            this.c_delimiter1.Alignment = System.Drawing.StringAlignment.Near;
            this.c_delimiter1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c_delimiter1.CaptionKey = null;
            this.c_delimiter1.HorizontalAlignment = IGK.ICore.Drawing2D.enuStringAlignment.Near;
            this.c_delimiter1.Location = new System.Drawing.Point(132, 76);
            this.c_delimiter1.Name = "c_delimiter1";
            this.c_delimiter1.Size = new System.Drawing.Size(244, 13);
            this.c_delimiter1.TabIndex = 33;
            this.c_delimiter1.TabStop = false;
            this.c_delimiter1.VerticalAlignment = IGK.ICore.Drawing2D.enuStringAlignment.Near;
            // 
            // GCSRGBColorSelector
            // 
            this.Controls.Add(this.c_lb_a);
            this.Controls.Add(this.c_lb_l);
            this.Controls.Add(this.c_lb_b);
            this.Controls.Add(this.c_lb_g);
            this.Controls.Add(this.c_lb_r);
            this.Controls.Add(this.c_txb_a);
            this.Controls.Add(this.c_txb_l);
            this.Controls.Add(this.c_txb_blue);
            this.Controls.Add(this.c_txb_green);
            this.Controls.Add(this.c_txb_red);
            this.Controls.Add(this.c_alpha);
            this.Controls.Add(this.c_light);
            this.Controls.Add(this.c_blue);
            this.Controls.Add(this.c_green);
            this.Controls.Add(this.c_red);
            this.Controls.Add(this.c_circleColorSelector);
            this.Controls.Add(this.c_delimiter1);
            this.Name = "GCSRGBColorSelector";
            this.Size = new System.Drawing.Size(379, 142);
            this.Load += new System.EventHandler(this._Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private void LoadColorString()
        {
            
            if (!this.m_ConfigFlag)
            {
                return;
            }

            System.Drawing.Color rb = this.Color.ToGdiColor();

            int i = this.c_txb_a.SelectionStart;
           this.c_txb_a.Text = string.Format("{0:##0}%", 100.0f * Color.A);
           if (this.c_txb_a.Text.Length == 2)
           {
               this.c_txb_a.SelectionStart = 1;
           }
           else
           {
               if (i != 0 && ((this.c_txb_a.Text.Length - 1) >= i))
               {
                   this.c_txb_a.SelectionStart = i;
               }
           }
            //luminausity
           i = this.c_txb_l.SelectionStart;
           float v_lum = Colorf.GetLuminosity(Color);
                this.c_txb_l.Text = string.Format("{0:##0}%", v_lum);// Colorf.GetLuminosity(Color));
                if (this.c_txb_l.Text.Length == 2)
                {
                    this.c_txb_l.SelectionStart = 1;
                }
                else
                {
                    if (i != 0 && ((this.c_txb_l.Text.Length - 1) >= i))
                    {
                        this.c_txb_l.SelectionStart = i;
                    }
                }
            this.c_txb_red.Text = string.Format("{0:##0}", rb.R);
            this.c_txb_green.Text = string.Format("{0:##0}", rb.G);
            this.c_txb_blue.Text = string.Format("{0:##0}", rb.B);
        }
        private void GetColorFromCircleColor()
        {
            bool isControl = (System.Windows.Forms.Control.ModifierKeys & System.Windows.Forms.Keys.Control) == System.Windows.Forms.Keys.Control;
            this.Color = Colorf.FromFloat(
                isControl ? 1.0F : this.c_alpha.Value / 255.0F,
                c_circleColorSelector.Color
                );
            this.m_baseColor = Colorf.FromFloat(
                1.0f,
                Color);
        }
        private void LoadColorValue()
        {
            if (!this.m_ConfigFlag)
            {
                float v_lum = GetLuminosityValue(this.Color);
                this.c_light.Value = v_lum;
            }
            System.Drawing.Color rb = this.Color.ToGdiColor();
            this.c_red.Value = rb.R;
            this.c_green.Value = rb.G;
            this.c_blue.Value = rb.B;
            this.c_alpha.Value = rb.A;
            this.c_alpha.StartColor = this.Color;
            this.c_alpha.EndColor = this.Color;
        }
        private int GetLuminosityValue(Colorf cl)
        {
            return (int)(c_light.MinValue + (Colorf.GetLuminosity(cl) * (c_light.MaxValue - c_light.MinValue) / 100.0f));
        }
        private void _Load(object sender, EventArgs e)
        {
            c_red.EndColor = Colorf.Red;
            c_blue.EndColor = Colorf.Blue;
            c_green.EndColor = Colorf.Lime;

            //luminausity is a special behaviour
            c_txb_a.TextChanged += manual_colorChanged;
            c_txb_red.TextChanged += manual_colorChanged;
            c_txb_green.TextChanged += manual_colorChanged;
            c_txb_blue.TextChanged += manual_colorChanged;
            
            c_circleColorSelector.ColorChanged += this.circle_ColorChanged;
            c_light.ValueChanged += c_l_ValueChanged;
            c_green.ValueChanged += c_ValueChanged;
            c_red.ValueChanged += c_ValueChanged;
            c_blue.ValueChanged += c_ValueChanged;
            c_alpha.ValueChanged += c_a_ValueChanged;
        }
        private void c_a_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_ConfigFlag) return;
            this.m_ConfigFlag = true;
            this.m_baseColor = Colorf.FromFloat(1.0F, this.Color);
            this.Color = Colorf.FromFloat(this.c_alpha.Value/255.0F,
                this.m_baseColor);
            this.m_ConfigFlag = false;
        }
        private void circle_ColorChanged(object sender, EventArgs e)
        {
            if (this.m_ConfigFlag)
                return;
            this.m_ConfigFlag = true;
            this.GetColorFromCircleColor();
            this.m_ConfigFlag = false;
        }
        void manual_colorChanged(object sender, EventArgs e)
        {
            //update color changed . for manual event
            if (m_ConfigFlag)
                return;
            m_ConfigFlag = true;
            float v_olda = this.Color.A;
            float r = (byte)(c_txb_red.Value < 0 ? 0 : c_txb_red.Value > 255 ? 255 : c_txb_red.Value);
            float g = (byte)(c_txb_green.Value < 0 ? 0 : c_txb_green.Value > 255 ? 255 : c_txb_green.Value);
            float b = (byte)(c_txb_blue.Value < 0 ? 0 : c_txb_blue.Value > 255 ? 255 : c_txb_blue.Value);
            float a = 0.0f;
            float f = 0.0f;
            string s = this.c_txb_a.Text.Replace("%", "");
            if (float.TryParse(s, out f))
            {
                if (f > 100)
                    f = 100;
                else
                    if (f < 0)
                        f = 0;
                a = f / 100.0f;
                int i = this.c_txb_a.SelectionStart;

            }
            else {
                a = 0.0f;
            }

            Colorf cl = Colorf.FromFloat (
                a,
                r/255.0f,
                g/255.0f,
                b/255.0f
                );
            this.Color = cl;
            m_ConfigFlag = false;
          
        }
        protected  override void EditFromProperty()
        {
            if (this.m_ConfigFlag) {
                LoadColorValue();
                LoadColorString();
                return;
            }
            this.m_ConfigFlag = true;
            this.m_baseColor = Colorf.FromFloat(
                1.0f, this.Color);
            this.c_circleColorSelector.Color = this.m_baseColor;
            LoadColorValue();
            LoadColorString();
            this.m_ConfigFlag = false;
        }
        private void GetColorFromTrackBar()
        {
            this.Color = Colorf.FromByteArgb(
                (byte)this.c_alpha.Value,
                (byte)this.c_red.Value,
                (byte)this.c_green.Value,
                (byte)this.c_blue.Value);
            this.m_baseColor = Colorf.FromFloat(1.0f,
                Color);
            this.c_circleColorSelector.Color = this.m_baseColor;
            //LoadColorString();
        }
        private void c_ValueChanged(object sender, EventArgs e)
        {
            if (m_ConfigFlag)
                return;
            this.m_ConfigFlag = true;
            GetColorFromTrackBar();         
            this.m_ConfigFlag = false;
        }
        private void c_l_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_ConfigFlag) return;
            this.m_ConfigFlag = true;
            this.__setLuminausity((int)c_light.Value);
            this.m_ConfigFlag = false;
        }
        private void __setLuminausity(float lum)
        {
            //save base color
            Colorf bck = this.m_baseColor;
            //color operation
            Colorf cl = this.m_baseColor;
            float r = cl.R;
            float g = cl.G;
            float b = cl.B;
            float a = cl.A;
            float tol = lum/255.0F;// (int)((-lum + (r + g + b)) / 3.0f);
#pragma warning disable IDE0054 // Use compound assignment
            r = ((r = (r + tol)) < 0) ? 0 : (r > 1.0f) ? 1.0f : r;
#pragma warning restore IDE0054 // Use compound assignment
#pragma warning disable IDE0054 // Use compound assignment
            g = ((g = (g + tol)) < 0) ? 0 : (g > 1.0f) ? 1.0f : g;
#pragma warning restore IDE0054 // Use compound assignment
#pragma warning disable IDE0054 // Use compound assignment
            b = ((b = (b + tol)) < 0) ? 0 : (b > 1.0f) ? 1.0f : b;
#pragma warning restore IDE0054 // Use compound assignment
            //r = pcl.R;
            //g = pcl.G;
            //b = pcl.B;
            float  v_baseA = this.c_alpha.Value/255.0f;
            cl = Colorf.FromFloat (v_baseA, r, g, b);
            this.Color = cl;
            this.c_circleColorSelector.Color = cl;
            this.m_baseColor = bck;
            this.c_light.Value = lum;
        }
        private void lb_l_TextChanged(object sender, EventArgs e)
        {
            if (this.m_ConfigFlag)
                return;
            m_ConfigFlag = true;
            int i = 0;
            string s = this.c_txb_l.Text.Replace("%", "");
            if (int.TryParse(s, out i))
            {
                if (i > 100)
                    i = 100;
                else if (i < 0)
                    i = 0;
                int r = c_txb_l.SelectionStart;
                __setLuminausity((int)Math.Round (-255 + (510 * (i /100.0f))));
                c_txb_l.SelectionStart = r;
            }
            m_ConfigFlag = false;
        }
        private void lb_a_TextChanged(object sender, EventArgs e)
        {
           
        }
        private void v_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_ConfigFlag)
                return;
            m_ConfigFlag = true;
            m_ConfigFlag = false;
        }
    }
}

