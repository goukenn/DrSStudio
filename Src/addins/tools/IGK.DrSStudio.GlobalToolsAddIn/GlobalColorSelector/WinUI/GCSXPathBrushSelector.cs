

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GCSXPathBrushSelector.cs
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
file:PathBrushSelector.cs
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
using System.Drawing;
using System.Drawing.Drawing2D;
namespace IGK.DrSStudio.WinUI
{
    using IGK.ICore.Drawing2D ;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinCore.WinUI.Controls;
    /// <summary>
    /// represent a linear brush selector
    /// </summary>
    public sealed class GCSXPathBrushSelector : GCSXBrushConfigureBase
    {
        private PickRectangleMecanism.Mecanism  m_pickRectangleM;
        private PickCenterMecanism.Mecanism m_pickCenter;
        private bool m_configuring;
        private bool m_EditCenter;
        private IGKXGradientTrackBar c_gradientBar;
        private IGKXRuleLabel xRuleLabel1;
        private IGKXSimpleColorView c_centerColor;
        private IGKXRuleLabel xRuleLabel2;
        private IGKXDualColorTrackBar c_trackx;
        private IGKXDualColorTrackBar c_tracky;
        private IGKXRuleLabel xRuleLabel3;
        private IGKXComboBox cmb_wrapMode;
        private IGKXComboBox cmb_linearOperation;
        private IGKXRuleLabel xRuleLabel4;
        private IGKXDualColorTrackBar c_trackscale;
        private IGKXDualColorTrackBar c_trackfocus;
        private IGKXLabel lb_trackscale;
        private IGKXLabel lb_trackfocus;
        private IGKXLabel lb_fx;
        private IGKXLabel lb_fy;
        private IGKXNumericTextBox c_posFactor;
        private IGKXLabel xLabel5;
        private IGKXRuleLabel xRuleLabel5;
        private IGKXComboBox cmb_pathBrushStyle;
        private IGKXButton btn_reset;
        private IGKXButton btn_pickRectangle;
        private IGKXTextBox c_txbrectangle;
        private IGKXLabel xLabel6;
        private IGKXLabel xLabel7;
        private enuLinearMode m_linearMode;
        private IGKXCheckBox chb_autosize;
        private IGKXCheckBox chb_isAutoCenter;
        private IGKXButton btn_pickCenter;
        private Rectanglef m_linRectangle;
        private IGKXCheckBox chb_oneColorPerVertex;
        private IGKXCheckBox c_unboundInnerColor;
        private IGKXGroupBox c_grbx_Mode;
        private IGKXRadioButton rd_multi;
        private IGKXRadioButton rd_blend;
        private IGKXRadioButton rd_dual;
    
        public override enuBrushType BrushType
        {
            get
            {
                return enuBrushType.PathGradient;
            }
        }
        private Vector2f m_PathCenter;
        /// <summary>
        /// get the path center
        /// </summary>
        public Vector2f PathCenter
        {
            get { return m_PathCenter; }           
        }
        public enuLinearMode LinearMode { get { return this.m_linearMode; } }
        public enuWrapMode WrapMode { get {
            if (this.cmb_wrapMode.SelectedItem == null)
                return enuWrapMode.Tile;
            return (enuWrapMode)this.cmb_wrapMode.SelectedItem; } }
        public bool LinearAutoSize { get { return this.chb_autosize.Checked; } }
        public float LinearFocus { get { return (float)this.c_trackfocus.Value / 100.0f; } }
        public float LinearScale { get { return (float)this.c_trackscale.Value / 100.0f; } }
        public Rectanglef LinearRectangle { get { return this.m_linRectangle; } }
        public enuLinearOperator LinearOperator { get { 
            
            return (enuLinearOperator)(this.cmb_linearOperation.SelectedItem ?? enuLinearOperator.None); 
        } }
        public enuPathBrushMode PathBrushMode { get {
            var e = this.cmb_pathBrushStyle.SelectedItem;
            if (e!=null)
                return (enuPathBrushMode)e;
            return enuPathBrushMode.Path;
        } }
        public Vector2f PathFocusScale { get { return new Vector2f(
            c_trackx .Value /100.0f,
            c_tracky .Value /100.0f
            ); } }
        public bool OneColorPerVertex { get { return this.chb_oneColorPerVertex.Checked; } }
        public override void SetColor(Colorf color)
        {
            if (this.m_EditCenter)
            {
                this.c_centerColor.Color = color;
            }
            else
            {
                if (this.c_gradientBar.SelectedColorIndex != -1)
                {
                    this.c_gradientBar.SelectedColor = color;
                }
                else if (this.m_EditCenter)
                    this.c_centerColor.Color = color;
            }
            ConfigureBrush();
        }
        protected override  void ConfigFromBrush(ICoreBrush brush)
        {
            if ((brush == null)|| (m_configuring ) || (brush .BrushType !=  enuBrushType .PathGradient ))
            return;
            m_configuring = true;
            this.c_trackx.Value = brush.PathFocusScale.X * 100.0f;
            this.c_tracky.Value = brush.PathFocusScale.Y * 100.0f;
            this.c_trackfocus.Value = brush.Focus * 100.0f;
            this.c_trackscale.Value = brush.Scale * 100.0f;
            this.SetScaleTextValue();
            this.SetFocusTextValue();
            this.c_gradientBar.ResetBar();
            this.chb_oneColorPerVertex.Checked = brush.OneColorPerVertex;
           switch (brush.LinearMode)
            {
                case enuLinearMode.Dual:
                    this.c_gradientBar.GradientMode = enuGradientBarMode.DualColor;
                    this.rd_dual.Checked = true;
                    this.c_centerColor.Color = brush.Colors[0];
                    this.c_gradientBar.Configure(new float[] { 0, 0 }, new float[] { 0, 1});       
                    this.c_gradientBar.SetColor(0, brush.Colors[0]);
                    this.c_gradientBar.SetColor(1, brush.Colors[1]);
                    break;
                case enuLinearMode.DualBlend:
                    this.rd_blend.Checked = true;
                    this.c_gradientBar.GradientMode = enuGradientBarMode.DualBlendColor;
                    this.c_centerColor.Color = brush.Colors[0];
                    this.c_gradientBar.Configure(brush.Factors, brush.Positions);                          
                    this.c_gradientBar.SetColor(0, brush.Colors[0]);
                    this.c_gradientBar.SetColor(brush.Positions.Length , brush.Colors[1]);
                    break;
                case enuLinearMode.MultiColor:
                    this.rd_multi.Checked = true;
                    this.c_gradientBar.GradientMode = enuGradientBarMode.MultiColor;
                    this.c_centerColor.Color = brush.Colors[0];
                    this.c_gradientBar.Configure(brush.Factors, brush.Positions);          
                    for (int i = 1; i < brush.Colors.Length; i++)
                    {
                        this.c_gradientBar.SetColor(i-1, brush.Colors[i]);
                    }
                    break;
                default:
                    break;
            }
            //set color
           this.m_linRectangle = brush.Bounds;
           this.m_linearMode = brush.LinearMode;
           this.SetRectangleText();
            this.cmb_linearOperation.SelectedItem = brush.LinearOperator;
            this.cmb_wrapMode.SelectedItem = brush.WrapMode;
            this.cmb_pathBrushStyle.SelectedItem = brush.PathBrushMode;
            this.m_PathCenter = brush.PathCenter;
            this.chb_isAutoCenter.Checked = brush.AutoCenter;
            this.chb_autosize.Checked = brush.AutoSize;
            m_configuring = false;
            IsFocusScaleEnabled();
            IsPosFactorEnabled();
            IsConfigureWrap();
        }
        private void SetRectangleText()
        {
            this.c_txbrectangle.Text = string.Format("{0:0} {1:0} {2:0} {3:0}",
                m_linRectangle.X,
                m_linRectangle.Y,
                m_linRectangle.Width,
                m_linRectangle.Height);
        }
        protected override void ConfigureBrush()
        {
            if ((this.ColorOwner == null) || ((this.ColorOwner.BrushSupport & enuBrushSupport.PathGradient) != enuBrushSupport.PathGradient))
                return;

            if (this.BrushToConfigure == null || this.m_configuring ) return;
            Colorf[] colors = null;
            Colorf[] tColors = c_gradientBar.Colors;
            switch (c_gradientBar .GradientMode)
            {
                case enuGradientBarMode.DualColor:                                        
                case enuGradientBarMode.DualBlendColor:
                    colors = c_gradientBar.Colors;
                    break;
                case enuGradientBarMode.MultiColor:
                    colors = new Colorf[1 + tColors.Length];
                    Array.Copy(tColors,0, colors, 1,tColors.Length);
                    colors[0] = c_centerColor.Color;
                    break;
                default:
                    break;
            }
            this.BrushToConfigure.SetPathBrush(
                colors,
                c_gradientBar.Factors,
                c_gradientBar.Positions,
                this.m_linearMode,
                this.WrapMode,
                this.PathFocusScale ,
                this.PathBrushMode,
                this.LinearOperator,
                this.LinearFocus,
                this.LinearScale,
                this.OneColorPerVertex ,
                this.LinearAutoSize,
                Rectanglef.Round ( this.LinearRectangle),
                this.chb_isAutoCenter .Checked ,
                this.m_PathCenter 
                );
           base. ConfigureBrush();
        }
        public GCSXPathBrushSelector()
        {
            this.InitializeComponent();
            this.Load += _Load;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AutoScroll = true;
        }
        public override void LoadDisplayText()
        {
            base.LoadDisplayText();
            this.rd_blend.Text = "blendmode".R();
            this.rd_multi.Text = "multimode".R();
            this.rd_dual.Text = "dualmode".R();
        }
        private void InitializeComponent()
        {
            this.c_gradientBar = new IGKXGradientTrackBar();
            this.xRuleLabel1 = new IGKXRuleLabel();
            this.c_centerColor = new IGKXSimpleColorView();
            this.xRuleLabel2 = new IGKXRuleLabel();
            this.c_trackx = new IGKXDualColorTrackBar();
            this.c_tracky = new IGKXDualColorTrackBar();
            this.xRuleLabel3 = new IGKXRuleLabel();
            this.cmb_wrapMode = new IGKXComboBox();
            this.cmb_linearOperation = new IGKXComboBox();
            this.xRuleLabel4 = new IGKXRuleLabel();
            this.c_trackscale = new IGKXDualColorTrackBar();
            this.c_trackfocus = new IGKXDualColorTrackBar();
            this.lb_trackscale = new IGKXLabel();
            this.lb_trackfocus = new IGKXLabel();
            this.lb_fx = new IGKXLabel();
            this.lb_fy = new IGKXLabel();
            this.c_posFactor = new IGKXNumericTextBox();
            this.xLabel5 = new IGKXLabel();
            this.xRuleLabel5 = new IGKXRuleLabel();
            this.cmb_pathBrushStyle = new IGKXComboBox();
            this.btn_reset = new IGKXButton();
            this.btn_pickRectangle = new IGKXButton();
            this.c_txbrectangle = new IGKXTextBox();
            this.xLabel6 = new IGKXLabel();
            this.xLabel7 = new IGKXLabel();
            this.chb_autosize = new IGKXCheckBox();
            this.chb_isAutoCenter = new IGKXCheckBox();
            this.btn_pickCenter = new IGKXButton();
            this.chb_oneColorPerVertex = new IGKXCheckBox();
            this.c_unboundInnerColor = new IGKXCheckBox();
            this.c_grbx_Mode = new IGKXGroupBox();
            this.rd_multi = new IGKXRadioButton();
            this.rd_blend = new IGKXRadioButton();
            this.rd_dual = new IGKXRadioButton();
            this.c_grbx_Mode.SuspendLayout();
            this.SuspendLayout();
            // 
            // c_gradientBar
            // 
            this.c_gradientBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c_gradientBar.CaptionKey = null;
            this.c_gradientBar.GradientMode = IGK.DrSStudio.WinUI.enuGradientBarMode.DualColor;
            this.c_gradientBar.Location = new System.Drawing.Point(17, 59);
            this.c_gradientBar.Name = "c_gradientBar";
            this.c_gradientBar.SelectedColor = Colorf.FromFloat(1F, 0F, 0F, 0F);
            this.c_gradientBar.SelectedColorIndex = 0;
            this.c_gradientBar.Size = new System.Drawing.Size(313, 35);
            this.c_gradientBar.TabIndex = 2;
            this.c_gradientBar.UnboundInnerColor = false;
            this.c_gradientBar.GradientModeChanged += new System.EventHandler(this.c_gradientBar_GradientModeChanged);
            this.c_gradientBar.GradientCursorChanged += new System.EventHandler(this.c_gradientBar_GradientCursorChanged);
            this.c_gradientBar.SelectedColorIndexChanged += new System.EventHandler(this.c_gradientBar_SelectedColorIndexChanged);
            // 
            // xRuleLabel1
            // 
            this.xRuleLabel1.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xRuleLabel1.CaptionKey = "lb.centerColor.caption";
            this.xRuleLabel1.HorizontalAlignment = enuStringAlignment.Near;
            this.xRuleLabel1.Location = new System.Drawing.Point(8, 151);
            this.xRuleLabel1.Name = "xRuleLabel1";
            this.xRuleLabel1.Size = new System.Drawing.Size(322, 23);
            this.xRuleLabel1.TabIndex = 4;
            this.xRuleLabel1.TabStop = false;
            this.xRuleLabel1.VerticalAlignment = enuStringAlignment.Near;
            // 
            // c_centerColor
            // 
            this.c_centerColor.CaptionKey = null;
            this.c_centerColor.Color = Colorf.FromFloat(0F, 0F, 0F, 0F);
            this.c_centerColor.Location = new System.Drawing.Point(34, 170);
            this.c_centerColor.MinimumSize = new System.Drawing.Size(16, 16);
            this.c_centerColor.Name = "c_centerColor";
            this.c_centerColor.Selected = false;
            this.c_centerColor.Size = new System.Drawing.Size(57, 23);
            this.c_centerColor.TabIndex = 5;
            this.c_centerColor.Click += new System.EventHandler(this.c_centerColor_Click);
            // 
            // xRuleLabel2
            // 
            this.xRuleLabel2.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xRuleLabel2.CaptionKey = "lb.FocusScale.caption";
            this.xRuleLabel2.HorizontalAlignment = enuStringAlignment.Near;
            this.xRuleLabel2.Location = new System.Drawing.Point(8, 193);
            this.xRuleLabel2.Name = "xRuleLabel2";
            this.xRuleLabel2.Size = new System.Drawing.Size(322, 23);
            this.xRuleLabel2.TabIndex = 6;
            this.xRuleLabel2.TabStop = false;
            this.xRuleLabel2.VerticalAlignment = enuStringAlignment.Near;
            // 
            // c_trackx
            // 
            this.c_trackx.BorderColor = Colorf.FromFloat(1F, 1F, 1F, 1F);
            this.c_trackx.CaptionKey = null;
            this.c_trackx.Cursor = System.Windows.Forms.Cursors.Hand;
            this.c_trackx.EndColor = Colorf.FromFloat(1F, 1F, 1F, 1F);
            this.c_trackx.Location = new System.Drawing.Point(31, 217);
            this.c_trackx.MaxValue = 100F;
            this.c_trackx.MinValue = 0F;
            this.c_trackx.Name = "c_trackx";
            this.c_trackx.Size = new System.Drawing.Size(174, 11);
            this.c_trackx.StartColor = Colorf.FromFloat(1F, 0F, 0F, 0F);
            this.c_trackx.Step = 1F;
            this.c_trackx.TabIndex = 7;
            this.c_trackx.Value = 255F;
            this.c_trackx.ValueChanged += new System.EventHandler(this.c_trackx_ValueChanged);
            // 
            // c_tracky
            // 
            this.c_tracky.BorderColor = Colorf.FromFloat(1F, 1F, 1F, 1F);
            this.c_tracky.CaptionKey = null;
            this.c_tracky.Cursor = System.Windows.Forms.Cursors.Hand;
            this.c_tracky.EndColor = Colorf.FromFloat(1F, 1F, 1F, 1F);
            this.c_tracky.Location = new System.Drawing.Point(31, 243);
            this.c_tracky.MaxValue = 100F;
            this.c_tracky.MinValue = 0F;
            this.c_tracky.Name = "c_tracky";
            this.c_tracky.Size = new System.Drawing.Size(174, 11);
            this.c_tracky.StartColor = Colorf.FromFloat(1F, 0F, 0F, 0F);
            this.c_tracky.Step = 1F;
            this.c_tracky.TabIndex = 8;
            this.c_tracky.Value = 255F;
            this.c_tracky.ValueChanged += new System.EventHandler(this.c_tracky_ValueChanged);
            // 
            // xRuleLabel3
            // 
            this.xRuleLabel3.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xRuleLabel3.CaptionKey = "lb.wrapMode.caption";
            this.xRuleLabel3.HorizontalAlignment = enuStringAlignment.Near;
            this.xRuleLabel3.Location = new System.Drawing.Point(5, 394);
            this.xRuleLabel3.Name = "xRuleLabel3";
            this.xRuleLabel3.Size = new System.Drawing.Size(325, 23);
            this.xRuleLabel3.TabIndex = 9;
            this.xRuleLabel3.TabStop = false;
            this.xRuleLabel3.VerticalAlignment = enuStringAlignment.Near;
            // 
            // cmb_wrapMode
            // 
            this.cmb_wrapMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_wrapMode.FormattingEnabled = true;
            this.cmb_wrapMode.Location = new System.Drawing.Point(31, 417);
            this.cmb_wrapMode.Name = "cmb_wrapMode";
            this.cmb_wrapMode.Size = new System.Drawing.Size(291, 21);
            this.cmb_wrapMode.TabIndex = 10;
            this.cmb_wrapMode.SelectedIndexChanged += new System.EventHandler(this.cmb_wrapMode_SelectedIndexChanged);
            // 
            // cmb_linearOperation
            // 
            this.cmb_linearOperation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_linearOperation.FormattingEnabled = true;
            this.cmb_linearOperation.Location = new System.Drawing.Point(31, 282);
            this.cmb_linearOperation.Name = "cmb_linearOperation";
            this.cmb_linearOperation.Size = new System.Drawing.Size(291, 21);
            this.cmb_linearOperation.TabIndex = 12;
            this.cmb_linearOperation.SelectedIndexChanged += new System.EventHandler(this.cmb_linearOperation_SelectedIndexChanged);
            // 
            // xRuleLabel4
            // 
            this.xRuleLabel4.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xRuleLabel4.CaptionKey = "lb.blendOperation.caption";
            this.xRuleLabel4.HorizontalAlignment = enuStringAlignment.Near;
            this.xRuleLabel4.Location = new System.Drawing.Point(8, 258);
            this.xRuleLabel4.Name = "xRuleLabel4";
            this.xRuleLabel4.Size = new System.Drawing.Size(322, 23);
            this.xRuleLabel4.TabIndex = 11;
            this.xRuleLabel4.TabStop = false;
            this.xRuleLabel4.VerticalAlignment = enuStringAlignment.Near;
            // 
            // c_trackscale
            // 
            this.c_trackscale.BorderColor = Colorf.FromFloat(1F, 1F, 1F, 1F);
            this.c_trackscale.CaptionKey = null;
            this.c_trackscale.Cursor = System.Windows.Forms.Cursors.Hand;
            this.c_trackscale.EndColor = Colorf.FromFloat(1F, 1F, 1F, 1F);
            this.c_trackscale.Location = new System.Drawing.Point(31, 325);
            this.c_trackscale.MaxValue = 100F;
            this.c_trackscale.MinValue = 0F;
            this.c_trackscale.Name = "c_trackscale";
            this.c_trackscale.Size = new System.Drawing.Size(174, 11);
            this.c_trackscale.StartColor = Colorf.FromFloat(1F, 0F, 0F, 0F);
            this.c_trackscale.Step = 1F;
            this.c_trackscale.TabIndex = 14;
            this.c_trackscale.Value = 100F;
            this.c_trackscale.ValueChanged += new System.EventHandler(this.c_trackscale_ValueChanged);
            // 
            // c_trackfocus
            // 
            this.c_trackfocus.BorderColor = Colorf.FromFloat(1F, 1F, 1F, 1F);
            this.c_trackfocus.CaptionKey = null;
            this.c_trackfocus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.c_trackfocus.EndColor = Colorf.FromFloat(1F, 1F, 1F, 1F);
            this.c_trackfocus.Location = new System.Drawing.Point(31, 308);
            this.c_trackfocus.MaxValue = 100F;
            this.c_trackfocus.MinValue = 0F;
            this.c_trackfocus.Name = "c_trackfocus";
            this.c_trackfocus.Size = new System.Drawing.Size(174, 11);
            this.c_trackfocus.StartColor = Colorf.FromFloat(1F, 0F, 0F, 0F);
            this.c_trackfocus.Step = 1F;
            this.c_trackfocus.TabIndex = 13;
            this.c_trackfocus.Value = 100F;
            this.c_trackfocus.ValueChanged += new System.EventHandler(this.c_trackfocus_ValueChanged);
            // 
            // lb_trackscale
            // 
            this.lb_trackscale.CaptionKey = "1.0";
            this.lb_trackscale.HorizontalAlignment = enuStringAlignment.Near;
            this.lb_trackscale.Location = new System.Drawing.Point(211, 325);
            this.lb_trackscale.Name = "lb_trackscale";
            this.lb_trackscale.Size = new System.Drawing.Size(20, 14);
            this.lb_trackscale.TabIndex = 15;
            this.lb_trackscale.TabStop = false;
            this.lb_trackscale.VerticalAlignment = enuStringAlignment.Near;
            // 
            // lb_trackfocus
            // 
            this.lb_trackfocus.CaptionKey = "1.0";
            this.lb_trackfocus.HorizontalAlignment = enuStringAlignment.Near;
            this.lb_trackfocus.Location = new System.Drawing.Point(211, 305);
            this.lb_trackfocus.Name = "lb_trackfocus";
            this.lb_trackfocus.Size = new System.Drawing.Size(20, 14);
            this.lb_trackfocus.TabIndex = 16;
            this.lb_trackfocus.TabStop = false;
            this.lb_trackfocus.VerticalAlignment = enuStringAlignment.Near;
            // 
            // lb_fx
            // 
            this.lb_fx.CaptionKey = "1.0";
            this.lb_fx.HorizontalAlignment = enuStringAlignment.Near;
            this.lb_fx.Location = new System.Drawing.Point(211, 214);
            this.lb_fx.Name = "lb_fx";
            this.lb_fx.Size = new System.Drawing.Size(20, 14);
            this.lb_fx.TabIndex = 17;
            this.lb_fx.TabStop = false;
            this.lb_fx.VerticalAlignment = enuStringAlignment.Near;
            // 
            // lb_fy
            // 
            this.lb_fy.CaptionKey = "1.0";
            this.lb_fy.HorizontalAlignment = enuStringAlignment.Near;
            this.lb_fy.Location = new System.Drawing.Point(211, 240);
            this.lb_fy.Name = "lb_fy";
            this.lb_fy.Size = new System.Drawing.Size(20, 14);
            this.lb_fy.TabIndex = 18;
            this.lb_fy.TabStop = false;
            this.lb_fy.VerticalAlignment = enuStringAlignment.Near;
            // 
            // c_posFactor
            // 
            this.c_posFactor.AllowDecimalValue = true;
            this.c_posFactor.Enabled = false;
            this.c_posFactor.Location = new System.Drawing.Point(256, 87);
            this.c_posFactor.MaxLength = 10;
            this.c_posFactor.Name = "c_posFactor";
            this.c_posFactor.Size = new System.Drawing.Size(48, 20);
            this.c_posFactor.TabIndex = 30;
            this.c_posFactor.TipText = null;
            this.c_posFactor.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.c_posFactor.ValueChanged += new System.EventHandler(this.c_posFactor_ValueChanged);
            // 
            // xLabel5
            // 
            this.xLabel5.CaptionKey = "lb.factor.caption";
            this.xLabel5.HorizontalAlignment = enuStringAlignment.Near;
            this.xLabel5.Location = new System.Drawing.Point(189, 87);
            this.xLabel5.Name = "xLabel5";
            this.xLabel5.Size = new System.Drawing.Size(84, 14);
            this.xLabel5.TabIndex = 29;
            this.xLabel5.TabStop = false;
            this.xLabel5.VerticalAlignment = enuStringAlignment.Near;
            // 
            // xRuleLabel5
            // 
            this.xRuleLabel5.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xRuleLabel5.CaptionKey = "lb.pathBrushStyle.caption";
            this.xRuleLabel5.HorizontalAlignment = enuStringAlignment.Near;
            this.xRuleLabel5.Location = new System.Drawing.Point(8, 344);
            this.xRuleLabel5.Name = "xRuleLabel5";
            this.xRuleLabel5.Size = new System.Drawing.Size(322, 23);
            this.xRuleLabel5.TabIndex = 31;
            this.xRuleLabel5.TabStop = false;
            this.xRuleLabel5.VerticalAlignment = enuStringAlignment.Near;
            // 
            // cmb_pathBrushStyle
            // 
            this.cmb_pathBrushStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_pathBrushStyle.FormattingEnabled = true;
            this.cmb_pathBrushStyle.Location = new System.Drawing.Point(31, 366);
            this.cmb_pathBrushStyle.Name = "cmb_pathBrushStyle";
            this.cmb_pathBrushStyle.Size = new System.Drawing.Size(291, 21);
            this.cmb_pathBrushStyle.TabIndex = 32;
            this.cmb_pathBrushStyle.SelectedIndexChanged += new System.EventHandler(this.cmb_pathBrushStyle_SelectedIndexChanged);
            // 
            // btn_reset
            // 
            this.btn_reset.CaptionKey = "btn.reset";
            this.btn_reset.Checked = false;
            this.btn_reset.DialogResult = enuDialogResult.None;
            this.btn_reset.Location = new System.Drawing.Point(31, 470);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.ShowButtonImage = false;
            this.btn_reset.Size = new System.Drawing.Size(44, 21);
            this.btn_reset.State = enuButtonState.Normal;
            this.btn_reset.TabIndex = 33;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // btn_pickRectangle
            // 
            this.btn_pickRectangle.CaptionKey = "btn.pickRectangle";
            this.btn_pickRectangle.Checked = false;
            this.btn_pickRectangle.DialogResult = enuDialogResult.None;
            this.btn_pickRectangle.Location = new System.Drawing.Point(81, 469);
            this.btn_pickRectangle.Name = "btn_pickRectangle";
            this.btn_pickRectangle.ShowButtonImage = false;
            this.btn_pickRectangle.Size = new System.Drawing.Size(42, 21);
            this.btn_pickRectangle.State = enuButtonState.Normal;
            this.btn_pickRectangle.TabIndex = 34;
            this.btn_pickRectangle.Click += new System.EventHandler(this.btn_pickRectangle_Click);
            // 
            // c_txbrectangle
            // 
            this.c_txbrectangle.Enabled = false;
            this.c_txbrectangle.Location = new System.Drawing.Point(129, 470);
            this.c_txbrectangle.Name = "c_txbrectangle";
            this.c_txbrectangle.Size = new System.Drawing.Size(70, 20);
            this.c_txbrectangle.TabIndex = 35;
            this.c_txbrectangle.Text = "0 0 0 0";
            this.c_txbrectangle.TipText = null;
            this.c_txbrectangle.TextChanged += new System.EventHandler(this.c_txbrectangle_TextChanged);
            // 
            // xLabel6
            // 
            this.xLabel6.CaptionKey = "lb.vector.X";
            this.xLabel6.HorizontalAlignment = enuStringAlignment.Near;
            this.xLabel6.Location = new System.Drawing.Point(8, 217);
            this.xLabel6.Name = "xLabel6";
            this.xLabel6.Size = new System.Drawing.Size(12, 14);
            this.xLabel6.TabIndex = 36;
            this.xLabel6.TabStop = false;
            this.xLabel6.VerticalAlignment = enuStringAlignment.Near;
            // 
            // xLabel7
            // 
            this.xLabel7.CaptionKey = "lb.vector.Y";
            this.xLabel7.HorizontalAlignment = enuStringAlignment.Near;
            this.xLabel7.Location = new System.Drawing.Point(8, 240);
            this.xLabel7.Name = "xLabel7";
            this.xLabel7.Size = new System.Drawing.Size(12, 14);
            this.xLabel7.TabIndex = 37;
            this.xLabel7.TabStop = false;
            this.xLabel7.VerticalAlignment = enuStringAlignment.Near;
            // 
            // chb_autosize
            // 
            this.chb_autosize.CaptionKey = "chb.autosize.caption";
            this.chb_autosize.Checked = true;
            this.chb_autosize.CheckState = enuCheckState.Checked;
            this.chb_autosize.Location = new System.Drawing.Point(31, 448);
            this.chb_autosize.Name = "chb_autosize";
            this.chb_autosize.Size = new System.Drawing.Size(124, 17);
            this.chb_autosize.TabIndex = 38;
            this.chb_autosize.CheckedChanged += new System.EventHandler(this.chb_autosize_CheckedChanged);
            // 
            // chb_isAutoCenter
            // 
            this.chb_isAutoCenter.CaptionKey = "chb.autoCenter.caption";
            this.chb_isAutoCenter.Checked = true;
            this.chb_isAutoCenter.CheckState = enuCheckState.Checked;
            this.chb_isAutoCenter.Location = new System.Drawing.Point(31, 499);
            this.chb_isAutoCenter.Name = "chb_isAutoCenter";
            this.chb_isAutoCenter.Size = new System.Drawing.Size(137, 17);
            this.chb_isAutoCenter.TabIndex = 39;
            this.chb_isAutoCenter.CheckedChanged += new System.EventHandler(this.chb_isAutoCenter_CheckedChanged);
            // 
            // btn_pickCenter
            // 
            this.btn_pickCenter.CaptionKey = "btn.pickcenter";
            this.btn_pickCenter.Checked = false;
            this.btn_pickCenter.DialogResult = enuDialogResult.None;
            this.btn_pickCenter.Enabled = false;
            this.btn_pickCenter.Location = new System.Drawing.Point(34, 522);
            this.btn_pickCenter.Name = "btn_pickCenter";
            this.btn_pickCenter.ShowButtonImage = false;
            this.btn_pickCenter.Size = new System.Drawing.Size(42, 21);
            this.btn_pickCenter.State = enuButtonState.Normal;
            this.btn_pickCenter.TabIndex = 40;
            this.btn_pickCenter.Click += new System.EventHandler(this.btn_pickCenter_Click);
            // 
            // chb_oneColorPerVertex
            // 
            this.chb_oneColorPerVertex.CaptionKey = "chb.oneCPV.caption";
            this.chb_oneColorPerVertex.Checked = true;
            this.chb_oneColorPerVertex.CheckState = enuCheckState.Checked;
            this.chb_oneColorPerVertex.Location = new System.Drawing.Point(23, 101);
            this.chb_oneColorPerVertex.Name = "chb_oneColorPerVertex";
            this.chb_oneColorPerVertex.Size = new System.Drawing.Size(124, 17);
            this.chb_oneColorPerVertex.TabIndex = 41;
            this.chb_oneColorPerVertex.CheckedChanged += new System.EventHandler(this.chb_oneColorPerVertex_CheckedChanged);
            // 
            // c_unboundInnerColor
            // 
            this.c_unboundInnerColor.CaptionKey = "chb.pathBrushSelector.unboundInnerColor";
            this.c_unboundInnerColor.Checked = false;
            this.c_unboundInnerColor.CheckState = enuCheckState.Checked;
            this.c_unboundInnerColor.Location = new System.Drawing.Point(23, 131);
            this.c_unboundInnerColor.Name = "c_unboundInnerColor";
            this.c_unboundInnerColor.Size = new System.Drawing.Size(227, 17);
            this.c_unboundInnerColor.TabIndex = 42;
            this.c_unboundInnerColor.CheckedChanged += new System.EventHandler(this.unbound_CheckedChanged);
            // 
            // c_grbx_Mode
            // 
            this.c_grbx_Mode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c_grbx_Mode.CaptionKey = "grb.mode";
            this.c_grbx_Mode.Controls.Add(this.rd_multi);
            this.c_grbx_Mode.Controls.Add(this.rd_blend);
            this.c_grbx_Mode.Controls.Add(this.rd_dual);
            this.c_grbx_Mode.Location = new System.Drawing.Point(8, 3);
            this.c_grbx_Mode.Name = "c_grbx_Mode";
            this.c_grbx_Mode.Size = new System.Drawing.Size(322, 45);
            this.c_grbx_Mode.TabIndex = 43;
            this.c_grbx_Mode.TabStop = false;
            // 
            // rd_multi
            // 
            this.rd_multi.Location = new System.Drawing.Point(218, 7);
            this.rd_multi.Name = "rd_multi";
            this.rd_multi.Size = new System.Drawing.Size(100, 23);
            this.rd_multi.TabIndex = 2;
            this.rd_multi.Tag = "3";
            this.rd_multi.CheckedChanged += new System.EventHandler(this.rd_dual_CheckedChanged);
            // 
            // rd_blend
            // 
            this.rd_blend.Location = new System.Drawing.Point(112, 7);
            this.rd_blend.Name = "rd_blend";
            this.rd_blend.Size = new System.Drawing.Size(100, 23);
            this.rd_blend.TabIndex = 2;
            this.rd_blend.Tag = "2";
            this.rd_blend.CheckedChanged += new System.EventHandler(this.rd_dual_CheckedChanged);
            // 
            // rd_dual
            // 
            this.rd_dual.Checked = true;
            this.rd_dual.Location = new System.Drawing.Point(6, 7);
            this.rd_dual.Name = "rd_dual";
            this.rd_dual.Size = new System.Drawing.Size(100, 23);
            this.rd_dual.TabIndex = 0;
            this.rd_dual.TabStop = true;
            this.rd_dual.Tag = "1";
            this.rd_dual.CheckedChanged += new System.EventHandler(this.rd_dual_CheckedChanged);
            // 
            // GCSXPathBrushSelector
            // 
            this.Controls.Add(this.c_grbx_Mode);
            this.Controls.Add(this.c_unboundInnerColor);
            this.Controls.Add(this.chb_oneColorPerVertex);
            this.Controls.Add(this.btn_pickCenter);
            this.Controls.Add(this.chb_isAutoCenter);
            this.Controls.Add(this.chb_autosize);
            this.Controls.Add(this.c_txbrectangle);
            this.Controls.Add(this.btn_pickRectangle);
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.cmb_pathBrushStyle);
            this.Controls.Add(this.xRuleLabel5);
            this.Controls.Add(this.c_posFactor);
            this.Controls.Add(this.xLabel5);
            this.Controls.Add(this.lb_fy);
            this.Controls.Add(this.lb_fx);
            this.Controls.Add(this.lb_trackfocus);
            this.Controls.Add(this.lb_trackscale);
            this.Controls.Add(this.c_trackscale);
            this.Controls.Add(this.c_trackfocus);
            this.Controls.Add(this.cmb_linearOperation);
            this.Controls.Add(this.xRuleLabel4);
            this.Controls.Add(this.cmb_wrapMode);
            this.Controls.Add(this.xRuleLabel3);
            this.Controls.Add(this.xRuleLabel2);
            this.Controls.Add(this.c_tracky);
            this.Controls.Add(this.c_trackx);
            this.Controls.Add(this.c_centerColor);
            this.Controls.Add(this.xLabel7);
            this.Controls.Add(this.xLabel6);
            this.Controls.Add(this.c_gradientBar);
            this.Controls.Add(this.xRuleLabel1);
            this.Name = "GCSXPathBrushSelector";
            this.Size = new System.Drawing.Size(335, 342);
            this.c_grbx_Mode.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private void _Load(object sender, EventArgs e)
        {
            InitControl();
        }
        private void InitControl()
        {
            this.m_configuring = true;
            foreach (enuWrapMode  mode in Enum.GetValues (typeof (enuWrapMode)))
            {
                this.cmb_wrapMode.Items.Add(mode);
            }
            this.cmb_wrapMode.SelectedItem = enuWrapMode.Tile;
            foreach (enuLinearOperator op in Enum.GetValues(typeof(enuLinearOperator)))
            {
                this.cmb_linearOperation.Items.Add(op);
            }
            this.cmb_linearOperation.SelectedItem = enuLinearOperator.None;
            foreach (enuPathBrushMode item in Enum.GetValues (typeof (enuPathBrushMode )))
            {
                this.cmb_pathBrushStyle .Items.Add (item );
            }
            this.cmb_pathBrushStyle.SelectedItem = enuPathBrushMode.Path ;
            this.c_trackx.Value = 0;
            this.c_tracky.Value = 0;
            this.m_configuring = false;
            IsFocusScaleEnabled();
            IsPosFactorEnabled();
            IsConfigureWrap();
        }
        private void c_posFactor_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_configuring) return;
            this.m_configuring = true;
            this.c_gradientBar.SetFactor(
                (float)c_posFactor.Value);
            this.m_configuring = false;
            ConfigureBrush();
        }
        private void c_gradientBar_SelectedColorIndexChanged(object sender, EventArgs e)
        {
            IsPosFactorEnabled();
            if (this.c_gradientBar.SelectedColorIndex != -1)
            {
                this.m_configuring = true;
                this.m_EditCenter = false;
                c_posFactor.Value = (decimal)this.c_gradientBar.Factors[
                this.c_gradientBar.SelectedColorIndex
                ];
                switch (this.LinearMode)
                {
                    case enuLinearMode.Dual:
                    case enuLinearMode.MultiColor:
                        this.ColorOwner.SetColor(this.c_gradientBar.SelectedColor);
                        break;
                    case enuLinearMode.DualBlend:
                        if ((c_gradientBar.SelectedColorIndex == 0) ||
                            (c_gradientBar.SelectedColorIndex == this.c_gradientBar.Colors.Length - 1))
                            this.ColorOwner.SetColor(this.c_gradientBar.SelectedColor);
                        break;
                }
                this.m_configuring = false;
            }
        }
        private void c_gradientBar_GradientModeChanged(object sender, EventArgs e)
        {
            IsPosFactorEnabled();
        }
        private void IsPosFactorEnabled()
        {
            this.c_posFactor.Enabled =
               (c_gradientBar.GradientMode == enuGradientBarMode.DualBlendColor)
               &&
               (c_gradientBar.SelectedColorIndex != -1)
               ;
        }
        private void rd_dual_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_configuring) 
                return;
            IGKXRadioButton c = (sender as IGKXRadioButton);
            if (c.Checked)
            {
                int i = Convert.ToInt32(c.Tag);
                switch (i)
                {
                    case 1:
                        this.m_linearMode = enuLinearMode.Dual;
                        this.c_gradientBar.GradientMode = enuGradientBarMode.DualColor; break;
                    case 2:
                        this.m_linearMode = enuLinearMode.DualBlend;
                        this.c_gradientBar.GradientMode = enuGradientBarMode.DualBlendColor; break;
                    case 3:
                        this.m_linearMode = enuLinearMode.MultiColor;
                        this.c_gradientBar.GradientMode = enuGradientBarMode.MultiColor; break;
                }
                ConfigureBrush();
            }
        }
        private void cmb_linearOperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            IsFocusScaleEnabled();
            if (this.m_configuring) return;
            ConfigureBrush();
        }
        private void IsFocusScaleEnabled()
        {
            if (this.cmb_linearOperation.SelectedItem == null)
                return;
            enuLinearOperator t = (enuLinearOperator)cmb_linearOperation.SelectedItem;
            bool v = (t != enuLinearOperator.None);
            c_trackfocus.Enabled = v;
            c_trackscale.Enabled = v;
        }
        private void c_trackfocus_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_configuring) return;
            this.SetFocusTextValue();
            this.ConfigureBrush();
        }
        private void c_trackscale_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_configuring) return;
            this.SetScaleTextValue();
            this.ConfigureBrush();
        }
        private void SetScaleTextValue()
        {
            this.lb_trackscale.CaptionKey = string.Format("{0:0.0}", c_trackscale.Value / 100.0f);
        }
        private void SetFocusTextValue()
        {
            this.lb_trackfocus.CaptionKey = string.Format("{0:0.0}", c_trackfocus.Value / 100.0f);
        }
        private void btn_pickRectangle_Click(object sender, EventArgs e)
        {
            //start picking rectangle
            if (m_pickRectangleM == null)
            {
                ICoreWorkingToolManagerSurface v_surface = this.CurrentSurface as ICoreWorkingToolManagerSurface;
                if (!v_surface.Mecanism.IsFreezed)
                {//mecanism already freeze
                    m_pickRectangleM = new PickRectangleMecanism.Mecanism(v_surface.Mecanism);
                    m_pickRectangleM.SelectionChanged += new EventHandler(m_pickRectangleM_SelectionChanged);
                    m_pickRectangleM.SelectionComplete += new EventHandler(m_pickRectangleM_SelectionComplete);
                    m_pickRectangleM.SelectionAbort += new EventHandler(m_pickRectangleM_SelectionAbort);
                }
            }
        }
        void m_pickRectangleM_SelectionAbort(object sender, EventArgs e)
        {
            m_pickRectangleM.SelectionChanged -= new EventHandler(m_pickRectangleM_SelectionChanged);
            m_pickRectangleM.SelectionComplete -= new EventHandler(m_pickRectangleM_SelectionComplete);
            m_pickRectangleM.SelectionAbort -= new EventHandler(m_pickRectangleM_SelectionAbort);
            m_pickRectangleM.Dispose();
            this.m_pickRectangleM = null;            
        }
        void m_pickRectangleM_SelectionComplete(object sender, EventArgs e)
        {
            m_pickRectangleM.SelectionChanged -= new EventHandler(m_pickRectangleM_SelectionChanged);
            m_pickRectangleM.SelectionComplete -= new EventHandler(m_pickRectangleM_SelectionComplete);
            m_pickRectangleM.SelectionAbort -= new EventHandler(m_pickRectangleM_SelectionAbort);
            this.m_pickRectangleM_SelectionChanged(m_pickRectangleM, EventArgs.Empty);
            this.m_pickRectangleM = null;
        }
        void m_pickRectangleM_SelectionChanged(object sender, EventArgs e)
        {
            if (this.m_configuring) return;
            this.m_configuring = true ;
            this.m_linRectangle = this.m_pickRectangleM.SelectedRectangle;
            this.SetRectangleText();
            this.m_configuring = false;
            this.ConfigureBrush();
        }
        private void btn_reset_Click(object sender, EventArgs e)
        {
            this.c_gradientBar.ResetBar();
            ConfigureBrush();
        }
        private void c_tracky_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_configuring) return;
            this.lb_fy.Text = string.Format("{0:0.0}", c_tracky.Value/100.0f);
            ConfigureBrush();
        }
        private void c_trackx_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_configuring) return;
            this.lb_fx.Text = string.Format("{0:0.0}", c_trackx.Value/100.0f);
            ConfigureBrush();
        }
        private void cmb_pathBrushStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_configuring) return;
            ConfigureBrush();            
        }
        private void c_gradientBar_GradientCursorChanged(object sender, EventArgs e)
        {
            if (this.m_configuring) return;
            ConfigureBrush();   
        }
        private void chb_autosize_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_configuring) return;            
            IsConfigureWrap();
            ConfigureBrush(); 
        }
        private void IsConfigureWrap()
        {
            this.cmb_wrapMode.Enabled = true;// !this.LinearAutoSize;
            this.btn_pickRectangle.Enabled = !this.LinearAutoSize;
            this.c_txbrectangle.Enabled = !this.LinearAutoSize;
        }
        private void c_txbrectangle_TextChanged(object sender, EventArgs e)
        {
            if (this.m_configuring) return;
            string[] vt = this.c_txbrectangle.Text.Split(' ');
            float x, y, w, h;
            if ((vt.Length == 4) &&
                float.TryParse(vt[0], out x) &&
            float.TryParse(vt[1], out y) &&
            float.TryParse(vt[2], out w) &&
            float.TryParse(vt[3], out h))
            {
                this.m_linRectangle = new Rectanglei(
                    (int)x,
                    (int)y,
                    (int)w,
                    (int)h);
                ConfigureBrush();
            }
        }
        private void cmb_wrapMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.m_configuring)
                ConfigureBrush();
        }
        private void c_centerColor_Click(object sender, EventArgs e)
        {
            if (!this.m_EditCenter)
            {
                switch (this.c_gradientBar.GradientMode)
                {
                    case enuGradientBarMode.DualColor:
                        break;
                    case enuGradientBarMode.DualBlendColor:                        
                    case enuGradientBarMode.MultiColor:
                    default :
                        this.m_EditCenter = true;
                        this.c_gradientBar.SelectedColorIndex = -1;
                    break;
                }
            }
        }
        private void chb_isAutoCenter_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_configuring) return;
            this.btn_pickCenter.Enabled = !this.chb_isAutoCenter.Checked;
            ConfigureBrush();
        }
        private void chb_oneColorPerVertex_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_configuring)
                return;
            this.ConfigureBrush();
        }
        private void btn_pickCenter_Click(object sender, EventArgs e)
        {
            if (this.m_pickCenter == null)
            {                
                    ICoreWorkingToolManagerSurface v_surface = this.CurrentSurface as ICoreWorkingToolManagerSurface;
                    if (!v_surface.Mecanism.IsFreezed)
                    {//mecanism already freeze
                        m_pickCenter = new PickCenterMecanism.Mecanism(v_surface.Mecanism);
                        m_pickCenter.SelectionChanged += new EventHandler(m_pickCenter_SelectionChanged);
                        m_pickCenter.SelectionComplete += new EventHandler(m_pickCenter_SelectionComplete);
                        m_pickCenter.SelectionAbort += new EventHandler(m_pickCenter_SelectionAbort);
                    }
            }
        }
        void m_pickCenter_SelectionAbort(object sender, EventArgs e)
        {
            m_pickCenter.SelectionChanged -= new EventHandler(m_pickCenter_SelectionChanged);
            m_pickCenter.SelectionComplete -= new EventHandler(m_pickCenter_SelectionComplete);
            m_pickCenter.SelectionAbort -= new EventHandler(m_pickCenter_SelectionAbort);
            m_pickCenter.Dispose();
            this.m_pickCenter = null;
        }
        void m_pickCenter_SelectionComplete(object sender, EventArgs e)
        {
            m_pickCenter.SelectionChanged -= new EventHandler(m_pickCenter_SelectionChanged);
            m_pickCenter.SelectionComplete -= new EventHandler(m_pickCenter_SelectionComplete);
            m_pickCenter.SelectionAbort -= new EventHandler(m_pickCenter_SelectionAbort);
            this.m_pickCenter_SelectionChanged(m_pickCenter, EventArgs.Empty);
            this.m_pickCenter = null;
        }
        void m_pickCenter_SelectionChanged(object sender, EventArgs e)
        {
            if (this.m_configuring) return;
            this.m_configuring = true;
            this.m_PathCenter = this.m_pickCenter.SelectedCenter ;            
            this.m_configuring = false;
            this.BrushToConfigure.PathCenter = this.m_PathCenter ;
        }
        private void unbound_CheckedChanged(object sender, EventArgs e)
        {
            this.c_gradientBar.UnboundInnerColor  = this.c_unboundInnerColor.Checked;
        }
    }
}

