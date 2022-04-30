

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GdiStrokePropertySelector.cs
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
file:GdiStrokeProperty.cs
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
using System; using IGK.ICore.WinCore;
using IGK.ICore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing ;
using System.Drawing.Drawing2D;
namespace IGK.DrSStudio.WinUI
{
    using IGK.ICore.Drawing2D;
    using IGK.ICore.WinUI.Configuration;
    using IGK.ICore.Resources;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinCore.WinUI.Controls;
    /// <summary>
    /// represent control to configure GDI Stroke properties
    /// </summary>
    sealed class GdiStrokePropertySelector :
        GCSXBrushConfigureBase         
    {
        private IGKXLabel lb_width;
        private IGKXComboBox cmb_penWidth;
        private IGKXNumericTextBox c_numTextMitter;
        private IGKXComboBox cmb_startCap;
        private IGKXComboBox cmb_endCap;
        private IGKXComboBox cmb_dashCap;
        private IGKXRuleLabel xRuleLabel2;
        private IGKXButton bt_joinBevel;
        private IGKXButton bt_joinMitter;
        private IGKXButton bt_joinRound;
        private IGKXRuleLabel xRuleLabel3;
        private IGKXButton btn_alignInset;
        private IGKXButton btn_alignCenter;
        private IGKXRuleLabel xRuleLabel1;
        private IGKXComboBox cmb_dashStyle;
        private bool m_configuring;
        enuPenAlignment m_PenAlignment;
        enuLineJoin m_lineJoin;
        float  m_penwidth;
        float m_mitterLimit;
        private ICoreLineCap m_startCap;
        private ICoreLineCap m_endCap;
        private IGKXLabel xLabel1;
        private IGKXLabel xLabel2;
        private IGKXLabel xLabel3;
        private IGKXLabel xLabel4;
        private IGKXButton btn_editStartCap;
        private IGKXButton btn_editEndCap;
        private IGKXButton bt_mitterClipped;
        private IGKXButton c_btn_editStyle;
        private ICorePen m_editedPen;
        public enuPenAlignment PenAlignment { get { return this.m_PenAlignment; } }
        public ICoreLineCap StartLineCap { get { return this.m_startCap;} }
        public ICoreLineCap EndLineCap { get { return this.m_endCap; } }
        public enuLineJoin LineJoin { get { return this.m_lineJoin; } }
        public float PenWidth { get { return this.m_penwidth; } }
        public float MitterLimit { get { return this.m_mitterLimit; } }
        public ICoreLineStyle DashStyle { get { return (ICoreLineStyle  ) this.cmb_dashStyle.SelectedItem; } }
        public enuDashCap DashCap { get { return (enuDashCap)this.cmb_dashCap.SelectedItem; } }

        public new ICorePen BrushToConfigure {
            get {
                return base.BrushToConfigure as ICorePen;
            }
            set {
                base.BrushToConfigure = value;
            }
        }
        protected override void OnBrushToConfigureChanged(EventArgs eventArgs)
        {
            base.OnBrushToConfigureChanged(eventArgs);
            this.Enabled = (this.BrushToConfigure != null);
        }
        protected override void ConfigFromBrush(ICoreBrush value)
        {
            if ((value !=null) && (m_editedPen == value))
            {
                return;
            }
            if ((value is ICorePen) == false)
            {
                this.m_penwidth = 1.0f;
                this.m_editedPen = null;
                return;
            }
            else {
                this.m_configuring = true;
                ICorePen pen = value as ICorePen;
                if (value is CorePen)
                {
                    CorePen cPen = value as CorePen;
                    this.cmb_startCap.SelectedItem = cPen.StartCap;
                    this.cmb_endCap.SelectedItem = cPen.EndCap;    
                }
                else {
                    this.cmb_startCap.SelectedItem = pen.StartCap;
                    this.cmb_endCap.SelectedItem = pen.EndCap;
                }
                this.m_penwidth = pen.Width;
                this.cmb_penWidth.SelectedItem = this.m_penwidth.ToString();
                this.cmb_penWidth.Text = ((CoreUnit)this.m_penwidth.ToString()).ToString();                
                this.c_numTextMitter.Value = (decimal)pen.MiterLimit;
                this.cmb_dashCap.SelectedItem = pen.DashCap;
                this.cmb_dashStyle.SelectedItem = pen.DashStyle;
                //init button
                bt_joinBevel.Checked = false;
                bt_joinMitter.Checked = false;
                bt_joinRound.Checked = false;
                bt_mitterClipped.Checked = false;
                switch (pen.LineJoin)
                {
                    case enuLineJoin.Bevel:
                        bt_joinBevel .Checked = true;
                        break;
                    case enuLineJoin.Miter:
                        bt_joinMitter.Checked = true;
                        break;
                    case enuLineJoin.MiterClipped:
                        bt_mitterClipped.Checked = true;
                        break;
                    case enuLineJoin.Round:
                        bt_joinRound.Checked = true;
                        break;
                    default:
                        break;
                }
                bool v = (pen.Alignment == enuPenAlignment.Inset);
                this.btn_alignInset.Checked = v;
                this.btn_alignCenter.Checked = !v;
                this.m_editedPen = pen;
                this.m_configuring = false;
            }
        }
        public GdiStrokePropertySelector()
        {
            this.InitializeComponent();
            this.InitControl();
            this.Dock = DockStyle.Fill;
            this.AutoScroll = true;
        }
        private void InitControl()
        {
            this.m_configuring = true;
            this.btn_alignCenter.Tag = enuPenAlignment.Center;
            this.btn_alignInset.Tag = enuPenAlignment.Inset;
            //init Combobox
            foreach (enuDashCap item in Enum.GetValues (typeof (enuDashCap )))
            {   
                this.cmb_dashCap .Items.Add (item);
            }
            this.cmb_dashCap.SelectedItem = enuDashCap.Round;
            this.cmb_dashCap.DrawMode = DrawMode.OwnerDrawFixed;
            this.cmb_dashCap.DrawItem += new DrawItemEventHandler(cmb_dashCap_DrawItem);
            
            CorePenDashStyle[] v_t = CorePenDashStyle.LoadStyles();
            this.cmb_dashStyle.DataSource = v_t;
            this.cmb_dashStyle.DisplayMember = "DisplayName";
            this.cmb_dashStyle.SelectedItem = global::System.Drawing.Drawing2D.DashStyle.Solid;
            this.cmb_dashStyle.DrawMode = DrawMode.OwnerDrawFixed;
            this.cmb_dashStyle.DrawItem += new DrawItemEventHandler(cmb_dashStyle_DrawItem);
            CorePenLineCap[] t = CorePenLineCap.LoadAnchor();
            this.cmb_startCap.DataSource = t;
            this.cmb_startCap.DisplayMember = "DisplayName";
            this.cmb_endCap.DataSource = t.Clone () as CorePenLineCap[];
            this.cmb_endCap.DisplayMember = "DisplayName";
            /*foreach (enuLineCap item in Enum.GetValues (typeof(enuLineCap)))
	        {                        
                this.cmb_startCap .Items .Add (item);
                this.cmb_endCap .Items.Add (item );
	        }*/
            this.cmb_startCap.SelectedItem = enuLineCap.Round;
            this.cmb_endCap.SelectedItem = enuLineCap.Round;
            this.cmb_startCap.DrawMode = DrawMode.OwnerDrawFixed;
            this.cmb_endCap.DrawMode = DrawMode.OwnerDrawFixed;
            this.cmb_startCap.DrawItem += new DrawItemEventHandler(cmb_startCap_DrawItem);
            this.cmb_endCap.DrawItem += new DrawItemEventHandler(cmb_endCap_DrawItem);
            this.m_PenAlignment = enuPenAlignment.Center;
            this.m_lineJoin = enuLineJoin.Round;
            this.bt_joinRound.Checked = true;
            this.cmb_penWidth.SelectedItem = "1";
            this.bt_joinMitter.Tag = enuLineJoin.Miter;
            this.bt_joinRound.Tag = enuLineJoin.Round;
            this.bt_joinBevel.Tag = enuLineJoin.Bevel;
            this.bt_mitterClipped.Tag = enuLineJoin.MiterClipped;
            this.btn_alignCenter.Tag = enuPenAlignment.Center;
            this.btn_alignInset.Tag = enuPenAlignment.Inset;
            this.m_configuring = false;
        }
        #region "DashItems"
        void cmb_dashStyle_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (e.Index == -1)
                return;
            ICoreLineStyle cl = (ICoreLineStyle)this.cmb_dashStyle.Items[e.Index];
            Rectangle rc = e.Bounds;
            rc.Inflate(-1, -1);
            rc.Width = 16;
            int x = rc.X + 3;
            int y = rc.Y + rc.Height / 2;
            int w = rc.X + rc.Width - 3;
            Pen p = new Pen(Color.Black);
            //cl..SetStyle(p);
            p.Width = 3;
            e.Graphics.DrawLine(p, x, y, w, y);
            p.Dispose();
            ControlPaint.DrawBorder(e.Graphics,
                rc, Color.Black, ButtonBorderStyle.Solid);
            //draw text
            e.Graphics.DrawString(cl.DisplayName , e.Font, 
                WinCoreBrushRegister.GetBrush(e.ForeColor), 
                rc.X + rc.Width + 3, rc.Y);
            if (e.Index == this.cmb_dashStyle.SelectedIndex)
                e.DrawFocusRectangle();
        }
        void cmb_endCap_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1)
                return;
            e.DrawBackground();
            ICoreLineCap cl = (ICoreLineCap)this.cmb_endCap.Items[e.Index];
            Color v_cl = e.ForeColor;
            Rectangle rc = e.Bounds;
            rc.Inflate(-1, -1);
            rc.Width = 16;
            int x = rc.X + 3;
            int y = rc.Y + rc.Height / 2;
            int w = rc.X + rc.Width - 3;
            Pen p = new Pen(v_cl);
            //if (cl.LineCap == enuLineCap.Custom)
            //{
            //    CustomLineCap lp = cl.GetCustomCap();
            //    lp.WidthScale = (2 / 32.0f) * (1 / 3.0f);
            //    p.CustomEndCap = lp;                
            //}
            //else
            p.EndCap =(LineCap ) cl.LineCap ;
            p.Width = 3;
            e.Graphics.DrawLine(p, x, y, w, y);
            p.Dispose();
            ControlPaint.DrawBorder(e.Graphics,
                rc, Color.Black, ButtonBorderStyle.Solid);
            //draw text
            e.Graphics.DrawString(cl.ToString(), e.Font, 
                WinCoreBrushRegister.GetBrush(e.ForeColor), rc.X + rc.Width + 3, rc.Y);
            if (e.Index == this.cmb_dashStyle.SelectedIndex)
                e.DrawFocusRectangle();
        }
        void cmb_startCap_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1)
                return;
            Color v_cl = e.ForeColor;
            e.DrawBackground();
            ICoreLineCap cl = (ICoreLineCap)this.cmb_endCap.Items[e.Index];
            Rectangle rc = e.Bounds;
            rc.Inflate(-1, -1);
            rc.Width = 16;
            int x = rc.X + 3;
            int y = rc.Y + rc.Height / 2;
            int w = rc.X + rc.Width - 3;
            Pen p = new Pen(v_cl);
            //if (cl.LineCap == enuLineCap.Custom)
            //{
            //    float v_w = cl.PathElement.GetBound().Width;
            //    CustomLineCap lp = cl.GetCustomCap();
            //    lp.WidthScale = (2/32.0f) * (1 / 3.0f);
            //    p.CustomStartCap = lp;
            //}
            //else 
                p.StartCap =(LineCap ) cl.LineCap ;
            p.Width = 3;
            e.Graphics.DrawLine(p, x, y, w, y);
            p.Dispose();
            ControlPaint.DrawBorder(e.Graphics,
                rc, Color.Black, ButtonBorderStyle.Solid);
            //draw text
            e.Graphics.DrawString(cl.ToString(), e.Font, WinCoreBrushRegister.GetBrush(e.ForeColor), rc.X + rc.Width + 3, rc.Y);
            if (e.Index == this.cmb_dashStyle.SelectedIndex)
                e.DrawFocusRectangle();
        }
        void cmb_dashCap_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1)
                return;
            e.DrawBackground();  
            enuDashCap cl = (enuDashCap)this.cmb_dashCap.Items[e.Index];
            Color v_cl = e.ForeColor;
            Rectangle rc = e.Bounds;
            rc.Inflate(-1, -1);
            rc.Width = 16;
            int x = rc.X + 3;
            int y = rc.Y + rc.Height / 2;
            int w = rc.X + rc.Width - 3;
            Pen p = new Pen(v_cl);
            p.DashCap =(DashCap ) cl;
            p.DashStyle = global::System.Drawing.Drawing2D.DashStyle.Dot;
            p.Width = 3;
            List<PointF> pts = new List<PointF>();
            for (int i = 0; i < w; i += 3)
            {
                pts.Add(new PointF(x + i, (float)(y + 3 * Math.Sin((i / 3) * Math.PI / 4))));
            }
            GraphicsState s = e.Graphics.Save();
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            if (pts.Count >= 2)
                e.Graphics.DrawCurve(p, pts.ToArray(), 0);
            p.Dispose();
            e.Graphics.Restore(s);
            ControlPaint.DrawBorder(e.Graphics,
                rc, Color.Black, ButtonBorderStyle.Solid);
            //draw text
            e.Graphics.DrawString(cl.ToString(), e.Font, WinCoreBrushRegister.GetBrush(e.ForeColor), rc.X + rc.Width + 3, rc.Y);
            if (e.Index == this.cmb_dashCap.SelectedIndex)
                e.DrawFocusRectangle();
        }
        #endregion
        private void InitializeComponent()
        {
            this.lb_width = new IGKXLabel();
            this.cmb_penWidth = new IGKXComboBox();
            this.xRuleLabel1 = new IGKXRuleLabel();
            this.c_numTextMitter = new IGKXNumericTextBox();
            this.cmb_startCap = new IGKXComboBox();
            this.cmb_endCap = new IGKXComboBox();
            this.cmb_dashCap = new IGKXComboBox();
            this.xRuleLabel2 = new IGKXRuleLabel();
            this.bt_joinBevel = new IGKXButton();
            this.bt_joinMitter = new IGKXButton();
            this.bt_joinRound = new IGKXButton();
            this.xRuleLabel3 = new IGKXRuleLabel();
            this.btn_alignInset = new IGKXButton();
            this.btn_alignCenter = new IGKXButton();
            this.cmb_dashStyle = new IGKXComboBox();
            this.xLabel1 = new IGKXLabel();
            this.xLabel2 = new IGKXLabel();
            this.xLabel3 = new IGKXLabel();
            this.xLabel4 = new IGKXLabel();
            this.bt_mitterClipped = new IGKXButton();
            this.btn_editStartCap = new IGKXButton();
            this.btn_editEndCap = new IGKXButton();
            this.c_btn_editStyle = new IGKXButton();
            this.SuspendLayout();
            // 
            // lb_width
            // 
            this.lb_width.BackColor = System.Drawing.Color.Transparent;
            this.lb_width.CaptionKey = "lb.width.caption";
            this.lb_width.Location = new System.Drawing.Point(22, 16);
            this.lb_width.Name = "lb_width";
            this.lb_width.Size = new System.Drawing.Size(88, 23);
            this.lb_width.TabIndex = 0;
            this.lb_width.TabStop = false;
            // 
            // cmb_penWidth
            // 
            this.cmb_penWidth.FormattingEnabled = true;
            this.cmb_penWidth.Items.AddRange(new object[] {
            "1",
            "2",
            "5",
            "8",
            "12",
            "14",
            "20",
            "25",
            "30",
            "50",
            "100"});
            this.cmb_penWidth.Location = new System.Drawing.Point(116, 16);
            this.cmb_penWidth.Name = "cmb_penWidth";
            this.cmb_penWidth.Size = new System.Drawing.Size(121, 21);
            this.cmb_penWidth.TabIndex = 1;
            this.cmb_penWidth.SelectedIndexChanged += new System.EventHandler(this.xComboBox1_SelectedIndexChanged);
            this.cmb_penWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmb_penWidth_KeyPress);
            this.cmb_penWidth.Validating += new System.ComponentModel.CancelEventHandler(this.xComboBox1_Validating);
            this.cmb_penWidth.Validated += new System.EventHandler(this.xComboBox1_Validated);
            // 
            // xRuleLabel1
            // 
            this.xRuleLabel1.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xRuleLabel1.BackColor = System.Drawing.Color.Transparent;
            this.xRuleLabel1.CaptionKey = "lb.styleandaspect.caption";
            this.xRuleLabel1.Location = new System.Drawing.Point(3, 40);
            this.xRuleLabel1.Name = "xRuleLabel1";
            this.xRuleLabel1.Size = new System.Drawing.Size(520, 14);
            this.xRuleLabel1.TabIndex = 2;
            this.xRuleLabel1.TabStop = false;
            // 
            // c_numTextMitter
            // 
            this.c_numTextMitter.AllowDecimalValue = true;
            this.c_numTextMitter.Location = new System.Drawing.Point(119, 187);
            this.c_numTextMitter.MaxLength = 10;
            this.c_numTextMitter.Name = "c_numTextMitter";
            this.c_numTextMitter.Size = new System.Drawing.Size(121, 20);
            this.c_numTextMitter.TabIndex = 3;
            this.c_numTextMitter.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.c_numTextMitter.ValueChanged += new System.EventHandler(this.xNumericTextBox1_ValueChanged);
            // 
            // cmb_startCap
            // 
            this.cmb_startCap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_startCap.FormattingEnabled = true;
            this.cmb_startCap.Location = new System.Drawing.Point(116, 108);
            this.cmb_startCap.Name = "cmb_startCap";
            this.cmb_startCap.Size = new System.Drawing.Size(121, 21);
            this.cmb_startCap.TabIndex = 4;
            this.cmb_startCap.SelectedValueChanged += new System.EventHandler(this.cmb_SelectedValueChanged);
            // 
            // cmb_endCap
            // 
            this.cmb_endCap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_endCap.FormattingEnabled = true;
            this.cmb_endCap.Location = new System.Drawing.Point(116, 135);
            this.cmb_endCap.Name = "cmb_endCap";
            this.cmb_endCap.Size = new System.Drawing.Size(121, 21);
            this.cmb_endCap.TabIndex = 5;
            this.cmb_endCap.SelectedValueChanged += new System.EventHandler(this.cmb_SelectedValueChanged);
            // 
            // cmb_dashCap
            // 
            this.cmb_dashCap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_dashCap.FormattingEnabled = true;
            this.cmb_dashCap.Location = new System.Drawing.Point(116, 84);
            this.cmb_dashCap.Name = "cmb_dashCap";
            this.cmb_dashCap.Size = new System.Drawing.Size(121, 21);
            this.cmb_dashCap.TabIndex = 6;
            this.cmb_dashCap.SelectedValueChanged += new System.EventHandler(this.cmb_SelectedValueChanged);
            // 
            // xRuleLabel2
            // 
            this.xRuleLabel2.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xRuleLabel2.BackColor = System.Drawing.Color.Transparent;
            this.xRuleLabel2.CaptionKey = "lb.strokeJoint.caption";
            this.xRuleLabel2.Location = new System.Drawing.Point(6, 170);
            this.xRuleLabel2.Name = "xRuleLabel2";
            this.xRuleLabel2.Size = new System.Drawing.Size(517, 14);
            this.xRuleLabel2.TabIndex = 7;
            this.xRuleLabel2.TabStop = false;
            // 
            // bt_jointBevel
            // 
            this.bt_joinBevel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.bt_joinBevel.CaptionKey = null;
            this.bt_joinBevel.Checked = false;
            this.bt_joinBevel.DialogResult = enuDialogResult.None;
            this.bt_joinBevel.Location = new System.Drawing.Point(94, 213);
            this.bt_joinBevel.Name = "bt_jointBevel";
            this.bt_joinBevel.ShowButtonImage = false;
            this.bt_joinBevel.Size = new System.Drawing.Size(32, 32);
            this.bt_joinBevel.State = enuButtonState.Normal;
            this.bt_joinBevel.TabIndex = 8;
            this.bt_joinBevel.Click += new System.EventHandler(this.lineJoin_Click);
            // 
            // bt_jointMitter
            // 
            this.bt_joinMitter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.bt_joinMitter.CaptionKey = null;
            this.bt_joinMitter.Checked = false;
            this.bt_joinMitter.DialogResult = enuDialogResult.None;
            this.bt_joinMitter.Location = new System.Drawing.Point(132, 213);
            this.bt_joinMitter.Name = "bt_jointMitter";
            this.bt_joinMitter.ShowButtonImage = false;
            this.bt_joinMitter.Size = new System.Drawing.Size(32, 32);
            this.bt_joinMitter.State = enuButtonState.Normal;
            this.bt_joinMitter.TabIndex = 9;
            this.bt_joinMitter.Click += new System.EventHandler(this.lineJoin_Click);
            // 
            // bt_jointRound
            // 
            this.bt_joinRound.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.bt_joinRound.CaptionKey = null;
            this.bt_joinRound.Checked = false;
            this.bt_joinRound.DialogResult = enuDialogResult.None;
            this.bt_joinRound.Location = new System.Drawing.Point(170, 213);
            this.bt_joinRound.Name = "bt_jointRound";
            this.bt_joinRound.ShowButtonImage = false;
            this.bt_joinRound.Size = new System.Drawing.Size(32, 32);
            this.bt_joinRound.State = enuButtonState.Normal;
            this.bt_joinRound.TabIndex = 10;
            this.bt_joinRound.Click += new System.EventHandler(this.lineJoin_Click);
            // 
            // xRuleLabel3
            // 
            this.xRuleLabel3.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xRuleLabel3.BackColor = System.Drawing.Color.Transparent;
            this.xRuleLabel3.CaptionKey = "lb.PenAlignment.caption";
            this.xRuleLabel3.Location = new System.Drawing.Point(9, 248);
            this.xRuleLabel3.Name = "xRuleLabel3";
            this.xRuleLabel3.Size = new System.Drawing.Size(514, 14);
            this.xRuleLabel3.TabIndex = 11;
            this.xRuleLabel3.TabStop = false;
            // 
            // btn_alignInset
            // 
            this.btn_alignInset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_alignInset.CaptionKey = null;
            this.btn_alignInset.Checked = false;
            this.btn_alignInset.DialogResult = enuDialogResult.None;
            this.btn_alignInset.Location = new System.Drawing.Point(208, 265);
            this.btn_alignInset.Name = "btn_alignInset";
            this.btn_alignInset.ShowButtonImage = false;
            this.btn_alignInset.Size = new System.Drawing.Size(32, 32);
            this.btn_alignInset.State = enuButtonState.Normal;
            this.btn_alignInset.TabIndex = 13;
            this.btn_alignInset.Click += new System.EventHandler(this.btn_align_Click);
            // 
            // btn_alignCenter
            // 
            this.btn_alignCenter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_alignCenter.CaptionKey = null;
            this.btn_alignCenter.Checked = false;
            this.btn_alignCenter.DialogResult = enuDialogResult.None;
            this.btn_alignCenter.Location = new System.Drawing.Point(170, 265);
            this.btn_alignCenter.Name = "btn_alignCenter";
            this.btn_alignCenter.ShowButtonImage = false;
            this.btn_alignCenter.Size = new System.Drawing.Size(32, 32);
            this.btn_alignCenter.State = enuButtonState.Normal;
            this.btn_alignCenter.TabIndex = 12;
            this.btn_alignCenter.Click += new System.EventHandler(this.btn_align_Click);
            // 
            // cmb_dashStyle
            // 
            this.cmb_dashStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_dashStyle.FormattingEnabled = true;
            this.cmb_dashStyle.Location = new System.Drawing.Point(116, 57);
            this.cmb_dashStyle.Name = "cmb_dashStyle";
            this.cmb_dashStyle.Size = new System.Drawing.Size(121, 21);
            this.cmb_dashStyle.TabIndex = 14;
            this.cmb_dashStyle.SelectedValueChanged += new System.EventHandler(this.cmb_SelectedValueChanged);
            // 
            // xLabel1
            // 
            this.xLabel1.BackColor = System.Drawing.Color.Transparent;
            this.xLabel1.CaptionKey = "lb.startCap.caption";
            this.xLabel1.Location = new System.Drawing.Point(22, 108);
            this.xLabel1.Name = "xLabel1";
            this.xLabel1.Size = new System.Drawing.Size(88, 23);
            this.xLabel1.TabIndex = 15;
            this.xLabel1.TabStop = false;
            // 
            // xLabel2
            // 
            this.xLabel2.BackColor = System.Drawing.Color.Transparent;
            this.xLabel2.CaptionKey = "lb.endCap.caption";
            this.xLabel2.Location = new System.Drawing.Point(22, 135);
            this.xLabel2.Name = "xLabel2";
            this.xLabel2.Size = new System.Drawing.Size(88, 23);
            this.xLabel2.TabIndex = 16;
            this.xLabel2.TabStop = false;
            // 
            // xLabel3
            // 
            this.xLabel3.BackColor = System.Drawing.Color.Transparent;
            this.xLabel3.CaptionKey = "lb.styleCap.caption";
            this.xLabel3.Location = new System.Drawing.Point(22, 85);
            this.xLabel3.Name = "xLabel3";
            this.xLabel3.Size = new System.Drawing.Size(88, 23);
            this.xLabel3.TabIndex = 17;
            this.xLabel3.TabStop = false;
            // 
            // xLabel4
            // 
            this.xLabel4.BackColor = System.Drawing.Color.Transparent;
            this.xLabel4.CaptionKey = "lb.style.caption";
            this.xLabel4.Location = new System.Drawing.Point(22, 57);
            this.xLabel4.Name = "xLabel4";
            this.xLabel4.Size = new System.Drawing.Size(88, 23);
            this.xLabel4.TabIndex = 18;
            this.xLabel4.TabStop = false;
            // 
            // bt_mitterClipped
            // 
            this.bt_mitterClipped.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.bt_mitterClipped.CaptionKey = null;
            this.bt_mitterClipped.Checked = false;
            this.bt_mitterClipped.DialogResult = enuDialogResult.None;
            this.bt_mitterClipped.Location = new System.Drawing.Point(208, 213);
            this.bt_mitterClipped.Name = "bt_mitterClipped";
            this.bt_mitterClipped.ShowButtonImage = false;
            this.bt_mitterClipped.Size = new System.Drawing.Size(32, 32);
            this.bt_mitterClipped.State = enuButtonState.Normal;
            this.bt_mitterClipped.TabIndex = 19;
            this.bt_mitterClipped.Click += new System.EventHandler(this.lineJoin_Click);
            // 
            // btn_editStartCap
            // 
            this.btn_editStartCap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_editStartCap.CaptionKey = "btn.Browse.caption";
            this.btn_editStartCap.Checked = false;
            this.btn_editStartCap.DialogResult = enuDialogResult.None;
            this.btn_editStartCap.Location = new System.Drawing.Point(243, 108);
            this.btn_editStartCap.Name = "btn_editStartCap";
            this.btn_editStartCap.ShowButtonImage = false;
            this.btn_editStartCap.Size = new System.Drawing.Size(34, 21);
            this.btn_editStartCap.State = enuButtonState.Normal;
            this.btn_editStartCap.TabIndex = 20;
            this.btn_editStartCap.Click += new System.EventHandler(this.btn_editStart_Click);
            // 
            // btn_editEndCap
            // 
            this.btn_editEndCap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_editEndCap.CaptionKey = "btn.Browse.caption";
            this.btn_editEndCap.Checked = false;
            this.btn_editEndCap.DialogResult = enuDialogResult.None;
            this.btn_editEndCap.Location = new System.Drawing.Point(243, 135);
            this.btn_editEndCap.Name = "btn_editEndCap";
            this.btn_editEndCap.ShowButtonImage = false;
            this.btn_editEndCap.Size = new System.Drawing.Size(34, 21);
            this.btn_editEndCap.State = enuButtonState.Normal;
            this.btn_editEndCap.TabIndex = 21;
            this.btn_editEndCap.Click += new System.EventHandler(this.btn_editEndCap_Click);
            // 
            // c_btn_editStyle
            // 
            this.c_btn_editStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.c_btn_editStyle.CaptionKey = "lb.StropProperty.Edit";
            this.c_btn_editStyle.Checked = false;
            this.c_btn_editStyle.DialogResult = enuDialogResult.None;
            this.c_btn_editStyle.Location = new System.Drawing.Point(243, 57);
            this.c_btn_editStyle.Name = "c_btn_editStyle";
            this.c_btn_editStyle.ShowButtonImage = false;
            this.c_btn_editStyle.Size = new System.Drawing.Size(34, 21);
            this.c_btn_editStyle.State = enuButtonState.Normal;
            this.c_btn_editStyle.TabIndex = 22;
            this.c_btn_editStyle.Click += new System.EventHandler(this.c_btn_editStyle_Click);
            // 
            // GdiStrokePropertySelector
            // 
            this.Controls.Add(this.c_btn_editStyle);
            this.Controls.Add(this.btn_editEndCap);
            this.Controls.Add(this.btn_editStartCap);
            this.Controls.Add(this.bt_mitterClipped);
            this.Controls.Add(this.xLabel4);
            this.Controls.Add(this.xLabel3);
            this.Controls.Add(this.xLabel2);
            this.Controls.Add(this.xLabel1);
            this.Controls.Add(this.cmb_dashStyle);
            this.Controls.Add(this.btn_alignInset);
            this.Controls.Add(this.btn_alignCenter);
            this.Controls.Add(this.xRuleLabel3);
            this.Controls.Add(this.bt_joinRound);
            this.Controls.Add(this.bt_joinMitter);
            this.Controls.Add(this.bt_joinBevel);
            this.Controls.Add(this.xRuleLabel2);
            this.Controls.Add(this.cmb_dashCap);
            this.Controls.Add(this.cmb_endCap);
            this.Controls.Add(this.cmb_startCap);
            this.Controls.Add(this.c_numTextMitter);
            this.Controls.Add(this.xRuleLabel1);
            this.Controls.Add(this.cmb_penWidth);
            this.Controls.Add(this.lb_width);
            this.Name = "GdiStrokePropertySelector";
            this.Size = new System.Drawing.Size(526, 481);
            this.Load += new System.EventHandler(this._Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private void btn_align_Click(object sender, EventArgs e)
        {
            if (this.m_configuring) return;
            IGKXButton btn = sender as IGKXButton;
            this.m_PenAlignment = (enuPenAlignment )btn.Tag ;
            this.btn_alignCenter.Checked = false;
            this.btn_alignInset.Checked = false;
            btn.Checked = true;
            ConfigureBrush();
        }
        private void cmb_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.m_configuring) 
                return;
            this.m_startCap = (ICoreLineCap)this.cmb_startCap.SelectedItem;
            this.m_endCap = (ICoreLineCap)this.cmb_endCap.SelectedItem;
            ConfigureBrush();
        }
        protected override void ConfigureBrush()
        {
            ICorePen vp = this.BrushToConfigure  as ICorePen;
            if (vp == null) return;
            if (vp is CorePen)
            {
                (vp as CorePen).SetStrokeProperty(
                    this.PenWidth,
                    this.PenAlignment,
                    this.DashStyle,
                    this.StartLineCap,
                    this.EndLineCap,
                    this.DashCap,
                    this.LineJoin,
                    this.MitterLimit);
            }
            else {
                vp .SetStrokeProperty(
                    this.PenWidth,
                    this.PenAlignment,
                    this.DashStyle.Style ,
                    this.StartLineCap.LineCap ,
                    this.EndLineCap.LineCap ,
                    this.DashCap,
                    this.LineJoin,
                    this.MitterLimit);
            }
        }
        private void lineJoin_Click(object sender, EventArgs e)
        {
            if (this.m_configuring)
                return;
            IGKXButton v_btn = sender as IGKXButton;
            m_lineJoin = (enuLineJoin)(v_btn).Tag;
            IGKXButton[] v_t = new IGKXButton[]{
                bt_joinBevel ,
                bt_joinMitter ,
                bt_joinRound ,
                bt_mitterClipped };
            for (int i = 0; i < v_t .Length; i++)
            {
                v_t[i].Checked = false;
            }
            //uncheck of
            v_btn.Checked = true;
            ConfigureBrush ();
        }
        private void xComboBox1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CoreUnit v_width = this.cmb_penWidth.Text;
            if (v_width != null)
            {
                float f = ((ICoreUnitPixel)v_width).Value;
                e.Cancel = (f <= 0);
            }
            else 
                e.Cancel = true;
        }
        private void xComboBox1_Validated(object sender, EventArgs e)
        {
            if (this.m_configuring)
                return;
            float f =((ICoreUnitPixel ) ((CoreUnit)this.cmb_penWidth .Text)).Value;
            this.m_penwidth = f;
            ConfigureBrush();
        }
        private void xComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_configuring)
                return;
            this.m_penwidth = float.Parse(cmb_penWidth.Text);
            ConfigureBrush();
        }
        private void xNumericTextBox1_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_configuring)
                return;
            this.m_mitterLimit = (float)c_numTextMitter.Value;
            ConfigureBrush();
        }
        private void _Load(object sender, EventArgs e)
        {
            this.btn_alignCenter.AutoButtonSize = false ;
            this.btn_alignCenter.ButtonDocument
                = CoreButtonDocument.CreateFromRes(CoreImageKeys.BTN_PENALIGNCENTER_GKDS);
            this.btn_alignCenter.Size = new Size (32,32);

            this.btn_alignInset.AutoButtonSize = false ;
            this.btn_alignInset.ButtonDocument
               = CoreButtonDocument.CreateFromRes(CoreImageKeys.BTN_PENALIGNINSET_GKDS);
            this.btn_alignInset.Size = new Size(32, 32);

            this.bt_mitterClipped .ButtonDocument
                  = CoreButtonDocument.CreateFromRes(CoreImageKeys.BTN_MITTERCLIPPED_GKDS);
            this.bt_joinRound .ButtonDocument
                  = CoreButtonDocument.CreateFromRes(CoreImageKeys.BTN_PENMITTER_GKDS);
            this.bt_joinBevel .ButtonDocument
                = CoreButtonDocument.CreateFromRes(CoreImageKeys.BTN_PENMITTERBEVEL_GKDS);;
            this.bt_joinMitter .ButtonDocument
                = CoreButtonDocument.CreateFromRes(CoreImageKeys.BTN_JOIN_MITTER_GKDS);

            CorePenLineCap.CorePenLineCapAdded += new CorePenLineCapEventHandler(CorePenLineCap_CorePenLineCapAdded);

            this.Enabled = false;
        }
        void CorePenLineCap_CorePenLineCapAdded(object sender, CorePenLineCapEventArgs e)
        {
            CorePenLineCap[] t = CorePenLineCap.LoadAnchor();
            this.cmb_endCap.DataSource  = t;
            this.cmb_startCap.DataSource = t.Clone () as CorePenLineCap[];
            //this.cmb_endCap.Items.Add(e.PenLineCap);
            //this.cmb_startCap.Items.Add(e.PenLineCap);
        }
        #region IColorSelector Members
        public override enuBrushType BrushType
        {
            get
            {
                if (this.BrushToConfigure !=null)
                    return this.BrushToConfigure.BrushType;
                return enuBrushType.Solid;
            }
        }
        public override  void SetColor(Colorf color)
        {
            //do nothing
            if (this.BrushToConfigure !=null)
            {
                if (this.BrushToConfigure.BrushType == enuBrushType.Solid)
                {
                  this.BrushToConfigure.SetSolidColor(color);
                }
                else{
                    //configure brush setting

                }
            }
        }
        #endregion
        private void cmb_penWidth_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch ((Keys)e.KeyChar)            
            { 
                case Keys.Enter :                    
                    this.Validate();
                    e.Handled = true;
                    break;
            }
        }
        private void btn_editStart_Click(object sender, EventArgs e)
        {
            ICoreLineCap p = this.StartLineCap;
            if (p.LineCap == enuLineCap.Custom )
            {
                PenCapConfigObject v_config = new PenCapConfigObject(p);
                if (CoreSystem.Instance.Workbench.ConfigureWorkingObject(v_config,
                    "title.editstartpenlinecap".R(), false, Size2i.Empty) == enuDialogResult.OK)
                {
                    this.m_startCap = v_config.GetLineCap();
                    ConfigureBrush();
                }
            }
        }
        private void btn_editEndCap_Click(object sender, EventArgs e)
        {
            ICoreLineCap p = this.EndLineCap;
            if (p.LineCap == enuLineCap.Custom)
            {
                PenCapConfigObject v_config = new PenCapConfigObject(p);
                if (CoreSystem.Instance.Workbench.ConfigureWorkingObject(
                    v_config,
                    "titel.editendlinecap".R(),
                    false, Size2i.Empty) == enuDialogResult.OK)
                {
                    this.m_endCap = v_config.GetLineCap();
                    ConfigureBrush();
                }
            }
        }
        private void c_btn_editStyle_Click(object sender, EventArgs e)
        {
            if (this.DashStyle.Style == enuDashStyle.Custom)
            {
                CoreSystem.Instance.Workbench.
                    ConfigureWorkingObject(this.DashStyle, "title.editdaskstyle".R(), false, Size2i.Empty);
                (this.DashStyle as CorePenDashStyle).Save();
                this.ConfigureBrush();
            }
            else { 
                CorePenDashStyle style = (CorePenDashStyle)CorePenDashStyle.GetLineStyle ("New",
                    new CoreUnit[]{ "1px", "10px"});
                if (CoreSystem.Instance.Workbench.
                    ConfigureWorkingObject(style, "title.editpendashstyle".R(), false, Size2i.Empty) == enuDialogResult.OK)
                {
                    if (CorePenDashStyle.AddStyle(style))
                    {
                        style.Save();
                        CorePenDashStyle.Reload();
                        //reload style
                        this.cmb_dashStyle.DataSource = CorePenDashStyle.LoadStyles();
                    }
                }
            }
        }
    }
    sealed class PenCapConfigObject : ICoreWorkingConfigurableObject
    {
        ICoreLineCap m_lCap;
        private float m_WidthScale;
        private enuLineCap m_CustomCap;
        private float m_BaseInset;
        public PenCapConfigObject(ICoreLineCap lCap)
        {
            m_lCap = lCap;
            this.m_BaseInset = lCap.BaseInset;
            this.m_WidthScale = lCap.WidthScale;
        }
        public ICoreLineCap GetLineCap()
        {
            return CorePenLineCap.GetLineCap(string.Format ( m_lCap.DisplayName +" {0} {1} {2}",
                    m_lCap.CustomCap,
                    this.m_BaseInset,
                    this.m_WidthScale));
        }
        
        public enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            ICoreParameterConfigCollections param = parameters;
            var group = param.AddGroup(CoreConstant.PARAM_DEFINITION);
            group.AddItem("WidthScale", "lb.WidthScale.caption", this.m_WidthScale , enuParameterType.SingleNumber, ValueChanged);
            //param.AddEnumItem("CustomCap", "lb.CustomCap.caption", ValueChanged);
            group.AddItem("BaseInset", "lb.Offset.caption", this.m_BaseInset, enuParameterType.SingleNumber, ValueChanged);
            return param;
        }
        void ValueChanged(Object sender, CoreParameterChangedEventArgs e)
        {
            switch (e.Item.Name.ToLower())
            {
                case "widthscale":
                    if (e.Value != null)
                        this.m_WidthScale = Convert.ToSingle(e.Value);
                    break;
                case "customcap":
                    if (Enum.IsDefined(typeof(enuLineCap), e.Value.ToString()))
                    {
                        this.m_CustomCap = (enuLineCap)Enum.Parse(typeof(enuLineCap), e.Value.ToString());
                    }
                    break;
                case "baseinset":
                    this.m_BaseInset = Convert.ToSingle(e.Value);
                    break;
            }
        }
        public  ICoreControl GetConfigControl()
        {
            return null;
        }
        
        
        public string Id
        {
            get { return this.m_lCap.DisplayName; }
        }
    }
    }

