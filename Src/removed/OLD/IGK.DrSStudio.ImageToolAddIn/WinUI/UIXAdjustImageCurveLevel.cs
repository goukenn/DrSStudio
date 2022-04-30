

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: UIXAdjustImageCurveLevel.cs
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
file:UIXAdjustImageCurveLevel.cs
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
using System.Windows.Forms ;
using System.Drawing ;
namespace IGK.DrSStudio.Drawing2D.WinUI
{
    using IGK.ICore;using IGK.DrSStudio.WinUI ;
    using System.Drawing.Drawing2D;
    class UIXAdjustImageCurveLevel: 
        UIXConfigControlBase     
    {
        private CoreJobProgressEventHandler m_jobProgess;
        readonly Bitmap bmp = new Bitmap(256, 256);
        private IGKXButton btn_Cancel;
        private IGKXButton btn_ok;
        private IGKXRuleLabel xRuleLabel1;
        private System.Windows.Forms.PictureBox pcb_histogram;
        private int m_demand;
        List<Vector2f> m_splinepoint;
        int m_index;
        WinCoreBitmapData m_oldBuildData;
        //Dictionary<int, int> m_dic;
        private IGKXButton btn_reset;
        private IGKXCheckBox chb_red;
        private IGKXCheckBox chb_g;
        private IGKXCheckBox chb_b;
        private bool m_drawLineDef;
        private IGKXComboBox cmb_ColorMode;
        private enuRGBMode m_colorMode;
        private IGKXButton btn_red;
        private IGKXButton btn_green;
        private IGKXButton btn_blue;
        private ImageElement m_imageElement;
        Dictionary<int, Snippet> m_snippets;
        public Bitmap OlDBitmap { get { return this.m_oldBuildData.ToBitmap(); } }
        public UIXAdjustImageCurveLevel(ImageElement image, CoreJobProgressEventHandler progress)
            : base()
        {
            if (image == null)
                throw new CoreException(enuExceptionType.ArgumentIsNull, "image");
            switch (image.Bitmap.PixelFormat)
            {
                case enuPixelFormat.Format32bppArgb:
                    break;
                default:
                    throw new CoreException(enuExceptionType.ArgumentNotValid, "image");
            }
            this.InitializeComponent();
            this.m_snippets = new Dictionary<int, Snippet>();
            this.m_jobProgess = progress;
            this.m_imageElement = image;
            this.pcb_histogram.Image = bmp;
            this.DrawBitHistogram();
            m_oldBuildData = WinCoreBitmapData.FromBitmap(image.Bitmap);
            m_splinepoint = new List<Vector2f>();
            m_splinepoint.Add(new Vector2f(0, 255));
            m_splinepoint.Add(new Vector2f(255, 0));
            m_index = -1;
            foreach (enuRGBMode mode in Enum.GetValues(typeof(enuRGBMode)))
            {
                this.cmb_ColorMode.Items.Add(mode);
            }
            this.m_colorMode = enuRGBMode.RGB;
            this.cmb_ColorMode.SelectedItem = enuRGBMode.RGB;
            CheckCheckBoxVisibility();
            this.cmb_ColorMode.SelectedIndexChanged += new EventHandler(cmb_ColorMode_SelectedIndexChanged);
            this.PropertyChanged += new EventHandler(_PropertyChanged);
            this.CancelButton = this.btn_Cancel;
            this.AcceptButton = this.btn_ok;
        }
        void _PropertyChanged(object sender, EventArgs e)
        {
            this.Apply(false);
        }
        void OnPropertyChanged(EventArgs e)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, e);
            }
        }
        public event EventHandler PropertyChanged;
        public enuRGBMode ColorMode
        {
            get
            {
                return this.m_colorMode;
            }
            set
            {
                if (this.m_colorMode != value)
                {
                    this.m_colorMode = value;
                    CheckCheckBoxVisibility();
                    OnPropertyChanged(EventArgs.Empty);
                }
            }
        }
        void cmb_ColorMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ColorMode = (enuRGBMode)this.cmb_ColorMode.SelectedItem;
        }
        void CheckCheckBoxVisibility()
        {
            bool v = (this.ColorMode == enuRGBMode.RGB);
            this.chb_b.Visible = v;
            this.chb_g.Visible = v;
            this.chb_red.Visible = v;
        }
        void DrawBitHistogram()
        {
            Graphics g = Graphics.FromImage(bmp);
            //draw 
            g.Clear(Color.DarkGray);
            //draw grid
            Rectangle rc = new Rectangle(0, 0, 255, 255);
            int w = rc.Width / 5;
            int h = rc.Height / 5;
            for (int i = 0; i < rc.Width; i += w)
            {
                //vertical 
                g.DrawLine(Pens.LightGray, i, 0, i, rc.Height);
                //horizontal
                g.DrawLine(Pens.LightGray, 0, i, rc.Width, i);
            }
            g.DrawRectangle(Pens.LightGray, rc);
            //draw midle point
            Pen p = CoreBrushRegister.GetPen<Pen>(Colorf.White);
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            g.DrawLine(p, 0, rc.Height, rc.Width, 0);
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            //g
            g.Flush();
            g.Dispose();
        }    
        bool IsRed
        {
            get
            {
                return this.chb_red.Checked;
            }
        }
        bool IsBlue
        {
            get
            {
                return this.chb_b.Checked;
            }
        }
        bool IsGreen
        {
            get
            {
                return this.chb_g.Checked;
            }
        }
        public int GetFunctionPoint(int x)
        {
            if (this.m_splinepoint.Count == 2)
                return x;
            else
            {
                return x;
            }
        }
        protected void Apply(bool temp)
        {
            OnJobProgress(0);
            WinCoreBitmapData vd = this.m_oldBuildData.Clone();
            int offset = 0;
            GraphicsPath v_path = new GraphicsPath();
            v_path.AddCurve(
                this.m_splinepoint.ToArray().CoreConvertTo<PointF[]>()
                );
            v_path.Flatten(new System.Drawing.Drawing2D.Matrix(), 0.002f);
            PointF[] v_tbpt = v_path.PathPoints;
            v_path.Dispose();
            Point[] yPoint = new Point[256];
            int count = 0;
            int y = 0;
            for (int x = 0; x < yPoint.Length; x++)
            {
                for (int k = count; k < v_tbpt.Length; k++)
                {
                    if (v_tbpt[k].X >= x)
                    {
                        y = (int)Math.Ceiling(255 - v_tbpt[k].Y);
                        y = (y < 0) ? 0 : (y > 255) ? 255 : y;
                        yPoint[x] = new Point(1 + x, y);
                        count = k;
                        break;
                    }
                }
                //yPoint[x] = new Point(1 + x,(int)( 255 - v_tbpt[x].Y ));// (1 + GetFunctionPoint (x)));                
            }
            float r = 0.0f;
            float g = 0.0f;
            float b = 0.0f;
            float v_gray = 255.0f;
            switch (this.m_colorMode)
            {
                case enuRGBMode.RGB:
                    {
                        for (int j = 0; j < vd.Height; j++)
                        {
                            for (int i = 0; i < vd.Width; i++)
                            {
                                offset = (int)((j * vd.Stride) + (i * 4));
                                if (IsBlue)
                                {//blue
                                    b = (255.0f * ((vd.Data[offset] / 255.0f) * (yPoint[vd.Data[offset]].Y / (float)yPoint[vd.Data[offset]].X)));
                                    vd.Data[offset] = (byte)((b < 0) ? 0 : ((b > 255.0f) ? 255 : b));
                                }
                                if (IsGreen)
                                {
                                    g = (255.0f * ((vd.Data[offset + 1] / 255.0f) * (yPoint[vd.Data[offset + 1]].Y / (float)yPoint[vd.Data[offset + 1]].X)));
                                    vd.Data[offset + 1] = (byte)((g < 0) ? 0 : ((g > 255.0f) ? 255 : g));
                                }
                                if (IsRed)
                                {
                                    r = (255.0f * ((vd.Data[offset + 2] / 255.0f) * (yPoint[vd.Data[offset + 2]].Y / (float)yPoint[vd.Data[offset + 2]].X)));
                                    vd.Data[offset + 2] = (byte)((r < 0) ? 0 : ((r > 255.0f) ? 255 : r));
                                }
                                OnJobProgress(j / (float)vd.Height);
                            }                            
                        }
                    }
                    break;
                case enuRGBMode.Gray:
                    byte v_value = 0;
                    for (int j = 0; j < vd.Height; j++)
                    {
                        for (int i = 0; i < vd.Width; i++)
                        {
                            offset = (int)((j * vd.Stride) + (i * 4));
                            b = (255.0f * ((vd.Data[offset] / 255.0f) * (yPoint[vd.Data[offset]].Y / (float)yPoint[vd.Data[offset]].X)));
                            g = (255.0f * ((vd.Data[offset + 1] / 255.0f) * (yPoint[vd.Data[offset + 1]].Y / (float)yPoint[vd.Data[offset + 1]].X)));
                            r = (255.0f * ((vd.Data[offset + 2] / 255.0f) * (yPoint[vd.Data[offset + 2]].Y / (float)yPoint[vd.Data[offset + 2]].X)));
                            v_gray = (r + g + b) / 3.0f;
                            v_value = (byte)((v_gray > 255) ? 255 : (v_gray < 0) ? 0 : v_gray);
                            vd.Data[offset] = v_value;
                            vd.Data[offset + 1] = v_value;
                            vd.Data[offset + 2] = v_value;
                        }
                       OnJobProgress (j / (float)vd.Height);
                    }
                    break;
            }            
            this.m_imageElement.SetBitmap(vd.ToBitmap(), temp );
            this.Invalidate();
        }
        void OnJobProgress(float percent)
        {
            if (this.m_jobProgess  != null)
            {
                this.m_jobProgess.BeginInvoke(percent, null, null);
            }
        }
        private void InitializeComponent()
        {
            this.pcb_histogram = new System.Windows.Forms.PictureBox();
            this.btn_Cancel = new IGK.DrSStudio.WinUI.IGKXButton();
            this.btn_ok = new IGK.DrSStudio.WinUI.IGKXButton();
            this.xRuleLabel1 = new IGK.DrSStudio.WinUI.IGKXRuleLabel();
            this.btn_reset = new IGK.DrSStudio.WinUI.IGKXButton();
            this.chb_red = new IGK.DrSStudio.WinUI.IGKXCheckBox();
            this.chb_g = new IGK.DrSStudio.WinUI.IGKXCheckBox();
            this.chb_b = new IGK.DrSStudio.WinUI.IGKXCheckBox();
            this.cmb_ColorMode = new IGK.DrSStudio.WinUI.IGKXComboBox();
            this.btn_red = new IGK.DrSStudio.WinUI.IGKXButton();
            this.btn_green = new IGK.DrSStudio.WinUI.IGKXButton();
            this.btn_blue = new IGK.DrSStudio.WinUI.IGKXButton();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_histogram)).BeginInit();
            this.SuspendLayout();
            // 
            // pcb_histogram
            // 
            this.pcb_histogram.Location = new System.Drawing.Point(67, 47);
            this.pcb_histogram.Name = "pcb_histogram";
            this.pcb_histogram.Size = new System.Drawing.Size(256, 256);
            this.pcb_histogram.TabIndex = 0;
            this.pcb_histogram.TabStop = false;
            this.pcb_histogram.MouseLeave += new System.EventHandler(this.pcb_histogram_MouseLeave);
            this.pcb_histogram.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pcb_histogram_MouseMove);
            this.pcb_histogram.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pcb_histogram_MouseDown);
            this.pcb_histogram.Paint += new System.Windows.Forms.PaintEventHandler(this.pcb_histogram_Paint);
            this.pcb_histogram.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pcb_histogram_MouseUp);
            this.pcb_histogram.MouseEnter += new System.EventHandler(this.pcb_histogram_MouseEnter);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.CaptionKey = CoreConstant.BTN_CANCEL;
            this.btn_Cancel.DialogResult = enuDialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(237, 377);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(86, 32);
            this.btn_Cancel.TabIndex = 1;
            // 
            // btn_ok
            // 
            this.btn_ok.CaptionKey = CoreConstant.BTN_OK;
            this.btn_ok.DialogResult = enuDialogResult.OK;
            this.btn_ok.Location = new System.Drawing.Point(145, 377);
            this.btn_ok.Name = CoreConstant.BTN_OK;
            this.btn_ok.Size = new System.Drawing.Size(86, 32);
            this.btn_ok.TabIndex = 2;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // xRuleLabel1
            // 
            this.xRuleLabel1.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel1.CaptionKey = null;
            this.xRuleLabel1.Location = new System.Drawing.Point(3, 348);
            this.xRuleLabel1.Name = "xRuleLabel1";
            this.xRuleLabel1.Size = new System.Drawing.Size(320, 10);
            this.xRuleLabel1.TabIndex = 3;
            this.xRuleLabel1.TabStop = false;
            // 
            // btn_reset
            // 
            this.btn_reset.CaptionKey = "btn.reset.caption";
            this.btn_reset.DialogResult = enuDialogResult.OK;
            this.btn_reset.Location = new System.Drawing.Point(53, 377);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(86, 32);
            this.btn_reset.TabIndex = 4;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // chb_red
            // 
            this.chb_red.CaptionKey = "chb.red.Title";
            this.chb_red.Checked = true;
            this.chb_red.CheckState = enuCheckState.Checked;
            this.chb_red.Location = new System.Drawing.Point(67, 309);
            this.chb_red.Name = "chb_red";
            this.chb_red.Size = new System.Drawing.Size(104, 24);
            this.chb_red.TabIndex = 5;
            this.chb_red.CheckedChanged += new System.EventHandler(this.chb_b_CheckedChanged);
            // 
            // chb_g
            // 
            this.chb_g.CaptionKey = "chb.green.Title";
            this.chb_g.Checked = true;
            this.chb_g.CheckState = enuCheckState.Checked;
            this.chb_g.Location = new System.Drawing.Point(145, 309);
            this.chb_g.Name = "chb_g";
            this.chb_g.Size = new System.Drawing.Size(104, 24);
            this.chb_g.TabIndex = 6;
            this.chb_g.CheckedChanged += new System.EventHandler(this.chb_b_CheckedChanged);
            // 
            // chb_b
            // 
            this.chb_b.CaptionKey = "chb.blue.Title";
            this.chb_b.Checked = true;
            this.chb_b.CheckState = enuCheckState.Checked;
            this.chb_b.Location = new System.Drawing.Point(219, 309);
            this.chb_b.Name = "chb_b";
            this.chb_b.Size = new System.Drawing.Size(104, 24);
            this.chb_b.TabIndex = 7;
            this.chb_b.CheckedChanged += new System.EventHandler(this.chb_b_CheckedChanged);
            // 
            // cmb_ColorMode
            // 
            this.cmb_ColorMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_ColorMode.FormattingEnabled = true;
            this.cmb_ColorMode.Location = new System.Drawing.Point(67, 10);
            this.cmb_ColorMode.Name = "cmb_ColorMode";
            this.cmb_ColorMode.Size = new System.Drawing.Size(256, 21);
            this.cmb_ColorMode.TabIndex = 8;
            // 
            // btn_red
            // 
            this.btn_red.CaptionKey = null;
            this.btn_red.Enabled = false;
            this.btn_red.Location = new System.Drawing.Point(21, 47);
            this.btn_red.Name = "btn_red";
            this.btn_red.Size = new System.Drawing.Size(25, 23);
            this.btn_red.TabIndex = 9;
            this.btn_red.Visible = false;
            // 
            // btn_green
            // 
            this.btn_green.CaptionKey = null;
            this.btn_green.Enabled = false;
            this.btn_green.Location = new System.Drawing.Point(21, 76);
            this.btn_green.Name = "btn_green";
            this.btn_green.Size = new System.Drawing.Size(25, 23);
            this.btn_green.TabIndex = 10;
            this.btn_green.Visible = false;
            // 
            // btn_blue
            // 
            this.btn_blue.CaptionKey = null;
            this.btn_blue.Enabled = false;
            this.btn_blue.Location = new System.Drawing.Point(21, 105);
            this.btn_blue.Name = "btn_blue";
            this.btn_blue.Size = new System.Drawing.Size(25, 23);
            this.btn_blue.TabIndex = 11;
            this.btn_blue.Visible = false;
            // 
            // UIXAdjust_ColorHistogram
            // 
            this.Controls.Add(this.btn_blue);
            this.Controls.Add(this.btn_green);
            this.Controls.Add(this.btn_red);
            this.Controls.Add(this.cmb_ColorMode);
            this.Controls.Add(this.chb_b);
            this.Controls.Add(this.chb_g);
            this.Controls.Add(this.chb_red);
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.xRuleLabel1);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.pcb_histogram);
            this.Name = "UIXAdjust_ColorHistogram";
            this.Size = new System.Drawing.Size(343, 430);
            ((System.ComponentModel.ISupportInitialize)(this.pcb_histogram)).EndInit();
            this.ResumeLayout(false);
        }
        private void pcb_histogram_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (this.m_demand <= 0)
                    {
                        this.InsertPoint(e.Location);
                    }
                    break;
            }
        }
        private void pcb_histogram_MouseMove(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    int x = 0;
                    int y = 0;
                    y = (e.Y < 0) ? 0 : (e.Y > 255) ? 255 : e.Y;
                    x = (e.X < 0) ? 0 : (e.X > 255) ? 255 : e.X;
                    if (this.m_demand <= 0)
                    {
                        if (m_index != -1)
                        {
                            if ((x > this.m_splinepoint[m_index - 1].X) &&
                                (x < this.m_splinepoint[m_index + 1].X))
                            {
                                this.m_splinepoint[m_index] =
                                    new Vector2f(x, y);
                                this.pcb_histogram.Invalidate();
                                this.OnPropertyChanged(EventArgs.Empty);
                            }
                        }
                    }
                    else
                    {
                        if (this.m_snippets.Count == 0)
                            return;
                        int i = this.m_snippets[this.m_demand].m_index;
                        if ((x > this.m_splinepoint[i - 1].X) &&
                            (x < this.m_splinepoint[i + 1].X))
                        {
                            this.m_splinepoint[i] =
                                 new Vector2f(x, y);
                            this.pcb_histogram.Invalidate();
                            this.OnPropertyChanged(EventArgs.Empty);
                        }
                    }
                    break;
                default:
                    this.pcb_histogram.Invalidate();
                    break;
            }
        }
        private void pcb_histogram_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    break;
                case MouseButtons.Right:
                    if (this.m_demand > 0)
                    {
                        this.Remove(this.m_demand);
                    }
                    break;
            }
            m_index = -1;
        }
        void InsertPoint(Point pts)
        {
            int index = -1;
            Vector2f[] v_pt = m_splinepoint.ToArray();
            for (int i = 0; i < v_pt.Length - 1; i++)
            {
                if ((pts.X > v_pt[i].X) && (pts.X <= v_pt[i + 1].X))
                {
                    index = i + 1;
                }
            }
            if (index != -1)
            {
                //this.Reset();
                m_splinepoint.Insert(index, pts);
                this.m_index = index;
                new Snippet(this, m_splinepoint.Count - 2);// index);
                //    this.Reset();
            }
        }
        void Remove(int demand)
        {
            if (this.m_snippets.ContainsKey(demand))
            {
                Dictionary<int, Snippet>.Enumerator e = this.m_snippets.GetEnumerator();
                while (e.MoveNext())
                {
                    e.Current.Value.Dispose();
                }
                this.m_snippets.Clear();
                //build snippet
                this.m_splinepoint.Remove(this.m_splinepoint[demand]);
                //generate snippet
                for (int i = 1; i < this.m_splinepoint.Count - 1; i++)
                {
                    new Snippet(this, i);
                }
                this.pcb_histogram.Invalidate();
                this.m_index = -1;
                this.m_demand = -1;
                OnPropertyChanged(EventArgs.Empty);
            }
        }
        void Reset()
        {
            Dictionary<int, Snippet>.Enumerator e = this.m_snippets.GetEnumerator();
            while (e.MoveNext())
            {
                e.Current.Value.Dispose();
            }
            this.m_snippets.Clear();
            this.m_splinepoint.Clear();
            this.m_index = -1;
            this.m_demand = 0;
            this.m_splinepoint.Add(new Vector2f(0, 255));
            this.m_splinepoint.Add(new Vector2f(255, 0));
            this.pcb_histogram.Invalidate();
        }
        private void pcb_histogram_Paint(object sender, PaintEventArgs e)
        {
            //paint line element
            System.Drawing.Drawing2D.GraphicsState st = e.Graphics.Save();
            Point p = pcb_histogram.PointToClient(Control.MousePosition);
            if (this.m_drawLineDef)
            {
                Pen v_pen = CoreBrushRegister.GetPen<Pen>(Colorf.Black);
                v_pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                e.Graphics.DrawLine(v_pen, 0, p.Y, pcb_histogram.Width, p.Y);
                e.Graphics.DrawLine(v_pen, p.X, 0, p.X, pcb_histogram.Height);
                v_pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            }
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.DrawCurve(Pens.Red, this.m_splinepoint.ToArray());
            //System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            //path.AddCurve(this.m_splinepoint.ToArray());
            //path.Flatten();
            //path.Int
            e.Graphics.Restore(st);
        }
        class Snippet : IDisposable
        {
            private UIXAdjustImageCurveLevel m_histo;
            internal int m_index;
            private bool m_visible;
            private int m_inflate;
            private int m_size = 0; //for inflate
            public bool Animated
            {
                get
                {
                    return this.m_timer.Enabled;
                }
                set
                {
                    this.m_timer.Enabled = value;
                }
            }
            Timer m_timer;
            internal Snippet(UIXAdjustImageCurveLevel histo, int index)
            {
                m_timer = new Timer();
                m_timer.Enabled = false;
                m_timer.Interval = 100;
                m_timer.Tick += new EventHandler(m_timer_Tick);
                m_histo = histo;
                m_index = index;
                m_inflate = 3;
                this.m_histo.m_snippets.Add(index, this);
                this.m_visible = true;
                m_histo.pcb_histogram.Paint += new PaintEventHandler(pcb_histogram_Paint);
                m_histo.pcb_histogram.MouseMove += new MouseEventHandler(pcb_histogram_MouseMove);
            }
            void m_timer_Tick(object sender, EventArgs e)
            {
                this.Invalidate(false);
                m_size++;
                if (m_size > 5)
                    m_size = 0;
                this.Invalidate(true);
            }
            void Invalidate(bool visible)
            {
                this.m_visible = visible;
                Rectanglef rc = new Rectanglef(this.m_histo.m_splinepoint[m_index], Size2f.Empty);
                rc.Inflate(m_inflate + m_size + 2, m_inflate + m_size + 2);
                this.m_histo.pcb_histogram.Invalidate(
                    new Rectangle((int)rc.X, (int)rc.Y, (int)rc.Width, (int)rc.Height));
            }
            void pcb_histogram_MouseMove(object sender, MouseEventArgs e)
            {
                if (!this.m_visible)
                    return;
                switch (e.Button)
                {
                    case MouseButtons.None:
                        if (this.Contains(e.Location))
                        {
                            this.m_histo.m_demand = this.m_index;
                            this.Animated = true;
                        }
                        else
                        {
                            if (this.m_histo.m_demand == this.m_index)
                            {
                                this.m_histo.m_demand = -1;
                            }
                            this.Animated = false;
                            this.Invalidate(false);
                            this.m_size = 0;
                            this.Invalidate(true);
                        }
                        break;
                }
            }
            private bool Contains(Vector2f  point)
            {
                Rectanglef rc = new Rectanglef(this.m_histo.m_splinepoint[m_index], Size2f.Empty);
                rc.Inflate(m_inflate, m_inflate);
                return rc.Contains(point);
            }
            void pcb_histogram_Paint(object sender, PaintEventArgs e)
            {
                if (this.m_visible == false)
                    return;
                // this.Dispose();
                Rectanglef rc = new Rectanglef(m_histo.m_splinepoint[m_index], Size2f.Empty);
                rc.Inflate(m_inflate + m_size, m_inflate + m_size);
                e.Graphics.DrawEllipse(Pens.Blue, rc.X , rc.Y, rc.Width, rc.Height );
            }
            internal void Hide()
            {
                this.m_visible = false;
                this.Animated = false;
            }
            internal void Show()
            {
                this.m_visible = true;
            }
            #region IDisposable Members
            public void Dispose()
            {
                if (this.m_timer != null)
                {
                    this.m_timer.Tick -= this.m_timer_Tick;
                    this.m_timer.Enabled = false;
                    this.m_timer.Dispose();
                    this.m_timer = null;
                }
                //unregister event
                m_histo.pcb_histogram.Paint -= new PaintEventHandler(pcb_histogram_Paint);
                m_histo.pcb_histogram.MouseMove -= new MouseEventHandler(pcb_histogram_MouseMove);
            }
            #endregion
        }
        private void btn_ok_Click(object sender, EventArgs e)
        {
            this.Apply(false);
        }
        private void btn_reset_Click(object sender, EventArgs e)
        {
            this.Reset();
            this.FindForm().DialogResult = DialogResult.None;
            this.OnPropertyChanged(EventArgs.Empty);
        }
        private void chb_b_CheckedChanged(object sender, EventArgs e)
        {
            this.OnPropertyChanged(EventArgs.Empty);
        }
        private void pcb_histogram_MouseEnter(object sender, EventArgs e)
        {
            this.m_drawLineDef = true;
            this.pcb_histogram.Invalidate();
        }
        private void pcb_histogram_MouseLeave(object sender, EventArgs e)
        {
            this.m_drawLineDef = false;
            this.pcb_histogram.Invalidate();
        }
    }
}

