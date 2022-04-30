

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GCSXLinearBrushSelector.cs
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
file:LinearBrushSelector.cs
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
using System.Drawing;
using System.Drawing.Drawing2D;
namespace IGK.DrSStudio.WinUI
{
    using IGK.ICore.Drawing2D;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinCore.WinUI.Controls;
    /// <summary>
    /// represent a linear brush selector
    /// </summary>
    sealed class GCSXLinearBrushSelector : GCSXBrushConfigureBase 
    {
        private PickRectangleMecanism.Mecanism m_pickRectangleM;
        private bool m_configuring;
        private IGKXGradientTrackBar c_gradientBar;
        private IGKXGroupBox c_grbx_Mode;
        private IGKXRadioButton rd_dual;
        private IGKXRadioButton rd_multi;
        private IGKXRadioButton rd_blend;
        private IGKXComboBox cmb_wrapMode;
        private IGKXRuleLabel xRuleLabel4;
        private IGKXDualColorTrackBar c_trackscale;
        private IGKXDualColorTrackBar c_trackfocus;
        private IGKXComboBox cmb_linearOperation;
        private IGKXRuleLabel xRuleLabel2;
        private IGKXLabel lb_trackfocus;
        private IGKXLabel lb_trackscale;
        private IGKXButton btn_reset;
        private IGKXButton btn_pickRectangle;
        private IGKXLabel xLabel3;
        private IGKXAngleSelector c_AngleSelector;
        private System.ComponentModel.IContainer components;
        private IGKXNumericTextBox c_numAngleV;
        private IGKXNumericTextBox c_posFactor;
        private IGKXCheckBox chb_autosize;
        private IGKXTextBox c_txbrectangle;
        private IGKXCheckBox chb_gammaCorrection;
        private enuLinearMode m_linearMode;
        private Rectanglef m_linRectangle;
        public float Angle { get { return (float)this.c_numAngleV.Value; } }
        public enuLinearMode LinearMode { get { return this.m_linearMode; } }
        public enuWrapMode WrapMode { get {
            if (this.cmb_wrapMode .SelectedItem != null)               
                return (enuWrapMode)this.cmb_wrapMode.SelectedItem; 
            return enuWrapMode .Tile ;
        } }
        public bool LinearAutoSize { get {return chb_autosize .Checked ; } }
        public bool GammaCorrection { get { return chb_gammaCorrection.Checked;} }
        public float LinearFocus { get { return (float)this.c_trackfocus.Value/100.0f; } }
        public float LinearScale { get { return (float)this.c_trackscale.Value / 100.0f; } }
        public Rectanglef LinearRectangle { get { return this.m_linRectangle; } }
        public enuLinearOperator LinearOperator { get { return (enuLinearOperator)this.cmb_linearOperation.SelectedItem; 
        } }
        public override enuBrushType BrushType
        {
            get
            {
                return enuBrushType.LinearGradient;
            }
        }
        protected override void ConfigFromBrush(ICoreBrush value)
        {
            if ((value == null) || (value.BrushType != enuBrushType.LinearGradient))
                return;
            m_configuring = true;
            //get angle
            this.c_numAngleV.Value = (decimal)value.Angle;
            this.c_AngleSelector.Angle = (float)value.Angle;
            this.c_trackfocus.Value = value.Focus * 100.0f;
            this.c_trackscale.Value = value.Scale * 100.0f;
            this.SetScaleText();
            this.SetFocusText();
            this.c_gradientBar.ResetBar();
            this.m_linearMode = value.LinearMode;
            switch (value.LinearMode)
            {
                case enuLinearMode.Dual:
                    this.rd_dual.Checked = true;
                    this.c_gradientBar.GradientMode = enuGradientBarMode.DualColor;
                    this.c_gradientBar.Configure(value.Factors, value.Positions);
                    this.c_gradientBar.SetColor(0, value.Colors[0]);
                    this.c_gradientBar.SetColor(1, value.Colors[1]);
                    break;
                case enuLinearMode.DualBlend:
                    this.rd_blend.Checked = true;
                    this.c_gradientBar.GradientMode = enuGradientBarMode.DualBlendColor ;
                    this.c_gradientBar.Configure(value.Factors, value.Positions);
                    this.c_gradientBar.SetColor(0, value.Colors[0]);
                    this.c_gradientBar.SetColor(1, value.Colors[1]);
                    break;
                case enuLinearMode.MultiColor:
                    this.rd_multi.Checked = true;
                    this.c_gradientBar.GradientMode = enuGradientBarMode.MultiColor;
                    this.c_gradientBar.Configure(value.Factors, value.Positions);
                    //set color
                    for (int i = 0; i < value.Colors.Length; i++)
                    {
                        this.c_gradientBar.SetColor(i, value.Colors[i]);
                    }
                    break;
                default:
                    break;
            }
            this.c_gradientBar.SelectedColorIndex = 0;
            //set color
            this.m_linRectangle = value.Bounds;
            this.SetRectangleText();
            this.cmb_linearOperation.SelectedItem = value.LinearOperator;
            this.cmb_wrapMode.SelectedItem = value.WrapMode;
            this.chb_autosize.Checked = value.AutoSize;
            m_configuring = false;
            IsFocusScaleEnabled();
            IsPosFactorEnabled();
            IsConfigureWrap();
        }
        public override void SetColor(Colorf color)
        {
            if (this.c_gradientBar.SelectedColorIndex != -1)
            {
                this.c_gradientBar.SelectedColor = color;
            }
            ConfigureBrush();
        }
        protected override void ConfigureBrush()
        {
            if ((this.ColorOwner == null) || ((this.ColorOwner .BrushSupport &  enuBrushSupport.LinearGradient ) != enuBrushSupport.LinearGradient ))
                return;
            if ((this.BrushToConfigure == null)||(this.m_configuring )) 
                return;
            this.BrushToConfigure.SetLinearBrush(
                this.c_gradientBar.Colors,
                this.c_gradientBar.Factors,
                this.c_gradientBar.Positions,
                this.Angle,
                this.LinearMode,
                this.WrapMode,
                this.GammaCorrection,
                this.LinearOperator,
                this.LinearFocus,
                this.LinearScale,
                this.LinearAutoSize,
                Rectanglef.Round (this.LinearRectangle));
            base.ConfigureBrush();
        }
        public GCSXLinearBrushSelector()
        {
            this.InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.AutoScroll = true;

            this.CaptionKey = "LinearGradient";
            this.c_numAngleV.Validating += new System.ComponentModel.CancelEventHandler(c_numAngleV_Validating);
        }
        void c_numAngleV_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Text.RegularExpressions.Regex rg = new System.Text.RegularExpressions.Regex (@"(-)*[0-9]+((\.)[0-9]+)*");
            if (rg.IsMatch(c_numAngleV.Text))
            {
                e.Cancel = false;
            }
            else
                e.Cancel = true;
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
            this.components = new System.ComponentModel.Container();
            this.c_gradientBar = new IGKXGradientTrackBar();
            this.c_grbx_Mode = new IGKXGroupBox();
            this.rd_multi = new IGKXRadioButton();
            this.rd_blend = new IGKXRadioButton();
            this.rd_dual = new IGKXRadioButton();
            this.cmb_wrapMode = new IGKXComboBox();
            this.xRuleLabel4 = new IGKXRuleLabel();
            this.c_trackscale = new IGKXDualColorTrackBar();
            this.c_trackfocus = new IGKXDualColorTrackBar();
            this.cmb_linearOperation = new IGKXComboBox();
            this.xRuleLabel2 = new IGKXRuleLabel();
            this.lb_trackfocus = new IGKXLabel();
            this.lb_trackscale = new IGKXLabel();
            this.btn_reset = new IGKXButton();
            this.btn_pickRectangle = new IGKXButton();
            this.xLabel3 = new IGKXLabel();
            this.c_AngleSelector = new IGKXAngleSelector(this.components);
            this.c_numAngleV = new IGKXNumericTextBox();
            this.c_posFactor = new IGKXNumericTextBox();
            this.chb_autosize = new IGKXCheckBox();
            this.c_txbrectangle = new IGKXTextBox();
            this.chb_gammaCorrection = new IGKXCheckBox();
            this.c_grbx_Mode.SuspendLayout();
            this.SuspendLayout();
            // 
            // c_gradientBar
            // 
            this.c_gradientBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c_gradientBar.CaptionKey = null;
            this.c_gradientBar.GradientMode = IGK.DrSStudio.WinUI.enuGradientBarMode.DualColor;
            this.c_gradientBar.Location = new System.Drawing.Point(3, 54);
            this.c_gradientBar.Name = "c_gradientBar";
            this.c_gradientBar.SelectedColor = Colorf.FromFloat(1F, 0F, 0F, 0F);
            this.c_gradientBar.SelectedColorIndex = 0;
            this.c_gradientBar.Size = new System.Drawing.Size(348, 35);
            this.c_gradientBar.TabIndex = 0;
            this.c_gradientBar.UnboundInnerColor = true;
            this.c_gradientBar.GradientModeChanged += new System.EventHandler(this.c_gradientBar_GradientModeChanged);
            this.c_gradientBar.GradientCursorChanged += new System.EventHandler(this.c_gradientBar_GradientCursorChanged);
            this.c_gradientBar.SelectedColorIndexChanged += new System.EventHandler(this.c_gradientBar_SelectedColorIndexChanged);
            // 
            // c_grbx_Mode
            // 
            this.c_grbx_Mode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c_grbx_Mode.CaptionKey = "grb.mode";
            this.c_grbx_Mode.Controls.Add(this.rd_multi);
            this.c_grbx_Mode.Controls.Add(this.rd_blend);
            this.c_grbx_Mode.Controls.Add(this.rd_dual);
            this.c_grbx_Mode.Location = new System.Drawing.Point(3, 3);
            this.c_grbx_Mode.Name = "c_grbx_Mode";
            this.c_grbx_Mode.Size = new System.Drawing.Size(348, 45);
            this.c_grbx_Mode.TabIndex = 1;
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
            this.rd_multi.Click += new System.EventHandler(this.rd_multi_Click);
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
            // cmb_wrapMode
            // 
            this.cmb_wrapMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_wrapMode.Enabled = false;
            this.cmb_wrapMode.FormattingEnabled = true;
            this.cmb_wrapMode.Location = new System.Drawing.Point(29, 302);
            this.cmb_wrapMode.Name = "cmb_wrapMode";
            this.cmb_wrapMode.Size = new System.Drawing.Size(168, 21);
            this.cmb_wrapMode.TabIndex = 14;
            this.cmb_wrapMode.SelectedIndexChanged += new System.EventHandler(this.cmb_wrapMode_SelectedIndexChanged);
            // 
            // xRuleLabel4
            // 
            this.xRuleLabel4.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xRuleLabel4.CaptionKey = "lb.wrapMode.caption";
            this.xRuleLabel4.HorizontalAlignment = enuStringAlignment.Near;
            this.xRuleLabel4.Location = new System.Drawing.Point(3, 259);
            this.xRuleLabel4.Name = "xRuleLabel4";
            this.xRuleLabel4.Size = new System.Drawing.Size(351, 23);
            this.xRuleLabel4.TabIndex = 13;
            this.xRuleLabel4.TabStop = false;
            this.xRuleLabel4.VerticalAlignment = enuStringAlignment.Near;
            // 
            // c_trackscale
            // 
            this.c_trackscale.BorderColor = Colorf.FromFloat(1F, 1F, 1F, 1F);
            this.c_trackscale.CaptionKey = null;
            this.c_trackscale.Cursor = System.Windows.Forms.Cursors.Hand;
            this.c_trackscale.Enabled = false;
            this.c_trackscale.EndColor = Colorf.FromFloat(1F, 1F, 1F, 1F);
            this.c_trackscale.Location = new System.Drawing.Point(29, 245);
            this.c_trackscale.MaxValue = 100F;
            this.c_trackscale.MinValue = 0F;
            this.c_trackscale.Name = "c_trackscale";
            this.c_trackscale.Size = new System.Drawing.Size(174, 11);
            this.c_trackscale.StartColor = Colorf.FromFloat(1F, 0F, 0F, 0F);
            this.c_trackscale.Step = 1F;
            this.c_trackscale.TabIndex = 20;
            this.c_trackscale.Value = 100F;
            this.c_trackscale.ValueChanged += new System.EventHandler(this.c_trackscale_ValueChanged);
            // 
            // c_trackfocus
            // 
            this.c_trackfocus.BorderColor = Colorf.FromFloat(1F, 1F, 1F, 1F);
            this.c_trackfocus.CaptionKey = null;
            this.c_trackfocus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.c_trackfocus.Enabled = false;
            this.c_trackfocus.EndColor = Colorf.FromFloat(1F, 1F, 1F, 1F);
            this.c_trackfocus.Location = new System.Drawing.Point(29, 219);
            this.c_trackfocus.MaxValue = 100F;
            this.c_trackfocus.MinValue = 0F;
            this.c_trackfocus.Name = "c_trackfocus";
            this.c_trackfocus.Size = new System.Drawing.Size(174, 11);
            this.c_trackfocus.StartColor = Colorf.FromFloat(1F, 0F, 0F, 0F);
            this.c_trackfocus.Step = 1F;
            this.c_trackfocus.TabIndex = 19;
            this.c_trackfocus.Value = 100F;
            this.c_trackfocus.ValueChanged += new System.EventHandler(this.c_trackfocus_ValueChanged);
            // 
            // cmb_linearOperation
            // 
            this.cmb_linearOperation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmb_linearOperation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_linearOperation.FormattingEnabled = true;
            this.cmb_linearOperation.Location = new System.Drawing.Point(29, 192);
            this.cmb_linearOperation.Name = "cmb_linearOperation";
            this.cmb_linearOperation.Size = new System.Drawing.Size(322, 21);
            this.cmb_linearOperation.TabIndex = 18;
            this.cmb_linearOperation.SelectedIndexChanged += new System.EventHandler(this.cmb_linearOperation_SelectedIndexChanged);
            // 
            // xRuleLabel2
            // 
            this.xRuleLabel2.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xRuleLabel2.CaptionKey = "lb.blendOperation.caption";
            this.xRuleLabel2.HorizontalAlignment = enuStringAlignment.Near;
            this.xRuleLabel2.Location = new System.Drawing.Point(0, 171);
            this.xRuleLabel2.Name = "xRuleLabel2";
            this.xRuleLabel2.Size = new System.Drawing.Size(351, 23);
            this.xRuleLabel2.TabIndex = 17;
            this.xRuleLabel2.TabStop = false;
            this.xRuleLabel2.VerticalAlignment = enuStringAlignment.Near;
            // 
            // lb_trackfocus
            // 
            this.lb_trackfocus.CaptionKey = null;
            this.lb_trackfocus.HorizontalAlignment = enuStringAlignment.Near;
            this.lb_trackfocus.Location = new System.Drawing.Point(212, 219);
            this.lb_trackfocus.Name = "lb_trackfocus";
            this.lb_trackfocus.Size = new System.Drawing.Size(20, 14);
            this.lb_trackfocus.TabIndex = 21;
            this.lb_trackfocus.TabStop = false;
            this.lb_trackfocus.VerticalAlignment = enuStringAlignment.Near;
            // 
            // lb_trackscale
            // 
            this.lb_trackscale.CaptionKey = null;
            this.lb_trackscale.HorizontalAlignment = enuStringAlignment.Near;
            this.lb_trackscale.Location = new System.Drawing.Point(212, 245);
            this.lb_trackscale.Name = "lb_trackscale";
            this.lb_trackscale.Size = new System.Drawing.Size(20, 14);
            this.lb_trackscale.TabIndex = 22;
            this.lb_trackscale.TabStop = false;
            this.lb_trackscale.VerticalAlignment = enuStringAlignment.Near;
            // 
            // btn_reset
            // 
            this.btn_reset.CaptionKey = "btn.reset";
            this.btn_reset.Checked = false;
            this.btn_reset.DialogResult = enuDialogResult.None;
            this.btn_reset.Location = new System.Drawing.Point(29, 329);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.ShowButtonImage = false;
            this.btn_reset.Size = new System.Drawing.Size(43, 23);
            this.btn_reset.State = enuButtonState.Normal;
            this.btn_reset.TabIndex = 23;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // btn_pickRectangle
            // 
            this.btn_pickRectangle.CaptionKey = "btn.pickrectangle";
            this.btn_pickRectangle.Checked = false;
            this.btn_pickRectangle.DialogResult = enuDialogResult.None;
            this.btn_pickRectangle.Location = new System.Drawing.Point(78, 329);
            this.btn_pickRectangle.Name = "btn_pickRectangle";
            this.btn_pickRectangle.ShowButtonImage = false;
            this.btn_pickRectangle.Size = new System.Drawing.Size(43, 23);
            this.btn_pickRectangle.State = enuButtonState.Normal;
            this.btn_pickRectangle.TabIndex = 24;
            this.btn_pickRectangle.Click += new System.EventHandler(this.btn_pickRectangle_Click);
            // 
            // xLabel3
            // 
            this.xLabel3.CaptionKey = "lb.factor.caption";
            this.xLabel3.HorizontalAlignment = enuStringAlignment.Near;
            this.xLabel3.Location = new System.Drawing.Point(124, 111);
            this.xLabel3.Name = "xLabel3";
            this.xLabel3.Size = new System.Drawing.Size(84, 14);
            this.xLabel3.TabIndex = 25;
            this.xLabel3.TabStop = false;
            this.xLabel3.VerticalAlignment = enuStringAlignment.Near;
            // 
            // c_AngleSelector
            // 
            this.c_AngleSelector.Angle = 0F;
            this.c_AngleSelector.CaptionKey = null;
            this.c_AngleSelector.Location = new System.Drawing.Point(3, 95);
            this.c_AngleSelector.Name = "c_AngleSelector";
            this.c_AngleSelector.Size = new System.Drawing.Size(50, 50);
            this.c_AngleSelector.TabIndex = 26;
            this.c_AngleSelector.AngleChanged += new System.EventHandler(this.xAngleSelector1_AngleChanged);
            // 
            // c_numAngleV
            // 
            this.c_numAngleV.AllowDecimalValue = true;
            this.c_numAngleV.AllowNegativeValue = true;
            this.c_numAngleV.Location = new System.Drawing.Point(60, 111);
            this.c_numAngleV.MaxLength = 10;
            this.c_numAngleV.Name = "c_numAngleV";
            this.c_numAngleV.Size = new System.Drawing.Size(48, 20);
            this.c_numAngleV.TabIndex = 27;
            this.c_numAngleV.TipText = null;
            this.c_numAngleV.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.c_numAngleV.ValueChanged += new System.EventHandler(this.c_numAngleV_ValueChanged);
            // 
            // c_posFactor
            // 
            this.c_posFactor.AllowDecimalValue = true;
            this.c_posFactor.Enabled = false;
            this.c_posFactor.Location = new System.Drawing.Point(254, 114);
            this.c_posFactor.MaxLength = 10;
            this.c_posFactor.Name = "c_posFactor";
            this.c_posFactor.Size = new System.Drawing.Size(48, 20);
            this.c_posFactor.TabIndex = 28;
            this.c_posFactor.TipText = null;
            this.c_posFactor.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.c_posFactor.ValueChanged += new System.EventHandler(this.c_posFactor_ValueChanged);
            // 
            // chb_autosize
            // 
            this.chb_autosize.CaptionKey = "chb.autosize.caption";
            this.chb_autosize.Checked = true;
            this.chb_autosize.CheckState = enuCheckState.Checked;
            this.chb_autosize.Location = new System.Drawing.Point(29, 279);
            this.chb_autosize.Name = "chb_autosize";
            this.chb_autosize.Size = new System.Drawing.Size(124, 17);
            this.chb_autosize.TabIndex = 29;
            this.chb_autosize.CheckedChanged += new System.EventHandler(this.chb_autosize_CheckedChanged);
            // 
            // c_txbrectangle
            // 
            this.c_txbrectangle.Location = new System.Drawing.Point(127, 332);
            this.c_txbrectangle.Name = "c_txbrectangle";
            this.c_txbrectangle.Size = new System.Drawing.Size(100, 20);
            this.c_txbrectangle.TabIndex = 30;
            this.c_txbrectangle.Text = "0 0 0 0";
            this.c_txbrectangle.TipText = null;
            this.c_txbrectangle.TextChanged += new System.EventHandler(this.c_txbrectangle_TextChanged);
            // 
            // chb_gammaCorrection
            // 
            this.chb_gammaCorrection.CaptionKey = "chb.gammaCorrection.caption";
            this.chb_gammaCorrection.Checked = true;
            this.chb_gammaCorrection.CheckState = enuCheckState.Checked;
            this.chb_gammaCorrection.Location = new System.Drawing.Point(32, 151);
            this.chb_gammaCorrection.Name = "chb_gammaCorrection";
            this.chb_gammaCorrection.Size = new System.Drawing.Size(167, 17);
            this.chb_gammaCorrection.TabIndex = 31;
            this.chb_gammaCorrection.CheckedChanged += new System.EventHandler(this.chb_gammaCorrection_CheckedChanged);
            // 
            // GCSXLinearBrushSelector
            // 
            this.Controls.Add(this.chb_gammaCorrection);
            this.Controls.Add(this.c_txbrectangle);
            this.Controls.Add(this.chb_autosize);
            this.Controls.Add(this.c_posFactor);
            this.Controls.Add(this.c_numAngleV);
            this.Controls.Add(this.xLabel3);
            this.Controls.Add(this.lb_trackscale);
            this.Controls.Add(this.lb_trackfocus);
            this.Controls.Add(this.c_AngleSelector);
            this.Controls.Add(this.btn_pickRectangle);
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.c_trackfocus);
            this.Controls.Add(this.c_trackscale);
            this.Controls.Add(this.cmb_linearOperation);
            this.Controls.Add(this.xRuleLabel2);
            this.Controls.Add(this.cmb_wrapMode);
            this.Controls.Add(this.xRuleLabel4);
            this.Controls.Add(this.c_grbx_Mode);
            this.Controls.Add(this.c_gradientBar);
            this.Name = "GCSXLinearBrushSelector";
            this.Size = new System.Drawing.Size(366, 276);
            this.Load += new System.EventHandler(this.LinearBrushSelector_Load);
            this.c_grbx_Mode.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private void LinearBrushSelector_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
            this.InitControl();
        }
        private void InitControl()
        {
            this.m_configuring = true;
            foreach (enuWrapMode mode in Enum.GetValues(typeof(enuWrapMode)))
            {
                this.cmb_wrapMode.Items.Add(mode);
            }
            foreach (enuLinearOperator op in Enum.GetValues (typeof(enuLinearOperator)))
            {
                this.cmb_linearOperation.Items.Add(op);
            }
            this.cmb_linearOperation.SelectedItem = enuLinearOperator.None;
            this.cmb_wrapMode.SelectedItem = enuWrapMode.Tile;
            this.m_configuring = false;
            if (this.BrushToConfigure != null)
            {
                this.ConfigFromBrush(this.BrushToConfigure);
            }
            IsFocusScaleEnabled();
            IsPosFactorEnabled();
            IsConfigureWrap();
        }
        private void c_trackfocus_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_configuring) return;
            SetFocusText();
            this.ConfigureBrush();
        }
        private void SetFocusText()
        {
            this.lb_trackfocus.CaptionKey = string.Format("{0:0.0}", c_trackfocus.Value / 100.0f);
        }
        private void c_trackscale_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_configuring) return;
            SetScaleText();
            this.ConfigureBrush();
        }
        private void SetScaleText()
        {
            this.lb_trackscale.CaptionKey = string.Format("{0:0.0}", c_trackscale.Value / 100.0f);
        }
        private void xAngleSelector1_AngleChanged(object sender, EventArgs e)
        {
            if (this.m_configuring) return;
            this.m_configuring = true;
            this.c_numAngleV.Value = (decimal)this.c_AngleSelector.Angle;
            this.m_configuring = false;
            ConfigureBrush();
        }
        private void rd_dual_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_configuring) return;
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
        private void c_gradientBar_GradientModeChanged(object sender, EventArgs e)
        {
            IsPosFactorEnabled();
        }
        private void IsPosFactorEnabled()
        {//check wheter pos factor is enabled
            this.c_posFactor.Enabled =
               (c_gradientBar.GradientMode == enuGradientBarMode.DualBlendColor)
               &&
               (c_gradientBar.SelectedColorIndex != -1)
               ;
        }
        private void c_posFactor_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_configuring) return;
            this.m_configuring = true;
            this.c_gradientBar.SetFactor (
                (float) c_posFactor.Value);
            this.m_configuring = false;
            ConfigureBrush();
        }
        private void c_gradientBar_SelectedColorIndexChanged(object sender, EventArgs e)
        {
            if (this.m_configuring)
                return;
            IsPosFactorEnabled();
            if (this.c_gradientBar.SelectedColorIndex != -1)
            {
                this.m_configuring = true;
                c_posFactor.Value =(decimal ) this.c_gradientBar.Factors[
                this.c_gradientBar.SelectedColorIndex
                ];
                switch (this.LinearMode)
                {
                    case enuLinearMode.Dual :
                    case enuLinearMode.MultiColor :
                        this.ColorOwner.SetColor(this.c_gradientBar.SelectedColor);
                        break;
                    case enuLinearMode.DualBlend :
                        if ((c_gradientBar.SelectedColorIndex == 0) ||
                            (c_gradientBar.SelectedColorIndex == this.c_gradientBar.Colors.Length - 1))
                            this.ColorOwner.SetColor(this.c_gradientBar.SelectedColor);
                        break;
                }
                this.m_configuring = false;
            }
        }
        private void btn_reset_Click(object sender, EventArgs e)
        {
            //init to default
            this.c_gradientBar.ResetBar();
            ConfigureBrush(); 
        }
        private void cmb_linearOperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.m_configuring)
            {
                IsFocusScaleEnabled();
                ConfigureBrush();
            }
        }
        private void IsFocusScaleEnabled()
        {
            if (cmb_linearOperation.SelectedItem == null)
            {
                c_trackfocus.Enabled = false ;
                c_trackscale.Enabled = false ;
                return;
            }
            enuLinearOperator t = (enuLinearOperator)cmb_linearOperation.SelectedItem;
            bool v = (t != enuLinearOperator.None);
            c_trackfocus.Enabled = v;
            c_trackscale.Enabled = v;
        }
        private void c_numAngleV_ValueChanged(object sender, EventArgs e)
        {
            if (!this.m_configuring)
            {
                this.m_configuring = true;
                this.c_AngleSelector.Angle = (float)this.c_numAngleV.Value;
                this.m_configuring = false;
                ConfigureBrush();
            }
        }
        private void c_gradientBar_GradientCursorChanged(object sender, EventArgs e)
        {
            if (!this.m_configuring)
            {                
                ConfigureBrush();                
            }
        }
        private void chb_autosize_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.m_configuring)
            {
                this.m_configuring = true;
                IsConfigureWrap();                
                this.m_configuring = false;
                ConfigureBrush();
            }
        }
        private void IsConfigureWrap()
        {
            this.cmb_wrapMode.Enabled = !this.LinearAutoSize;
            this.c_txbrectangle.Enabled = !this.LinearAutoSize ;
            this.btn_pickRectangle.Enabled = !this.LinearAutoSize;
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
        private void chb_gammaCorrection_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_configuring) return;
            ConfigureBrush();
        }
        private void cmb_wrapMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_configuring) return;
            ConfigureBrush();
        }
        private void btn_pickRectangle_Click(object sender, EventArgs e)
        {
            //start picking rectangle
            if ((m_pickRectangleM == null) && (this.CurrentSurface is ICoreWorkingToolManagerSurface))
            {
                ICoreWorkingToolManagerSurface v_surface = this.CurrentSurface as ICoreWorkingToolManagerSurface;
                if (!v_surface.Mecanism.IsFreezed)
                {
                    m_pickRectangleM = new PickRectangleMecanism.Mecanism(v_surface.Mecanism);
                    m_pickRectangleM.SelectionChanged += new EventHandler(m_pickRectangleM_SelectionChanged);
                    m_pickRectangleM.SelectionComplete += new EventHandler(m_pickRectangleM_SelectionComplete);
                    m_pickRectangleM.SelectionAbort += new EventHandler(m_pickRectangleM_SelectionAbort);

                    if (this.CurrentSurface is ICore.Drawing2D.WinUI.IGKD2DDrawingSurfaceBase s_scene) {
                        s_scene.Scene.Frames.Add(m_pickRectangleM);
                    }
                    
                    
                }
            }
#if DEBUG
            else{
                CoreLog.WriteLine ("Can't pick rectangle");
            }
#endif
        }
        void m_pickRectangleM_SelectionAbort(object sender, EventArgs e)
        {
            _done();
          
        }

        private void _done()
        {
            m_pickRectangleM.SelectionChanged -= new EventHandler(m_pickRectangleM_SelectionChanged);
            m_pickRectangleM.SelectionComplete -= new EventHandler(m_pickRectangleM_SelectionComplete);
            m_pickRectangleM.SelectionAbort -= new EventHandler(m_pickRectangleM_SelectionAbort);
            if (this.CurrentSurface is ICore.Drawing2D.WinUI.IGKD2DDrawingSurfaceBase s_scene)
            {
                s_scene.Scene.Frames.Remove(m_pickRectangleM);
            }
            m_pickRectangleM.Dispose();
            this.m_pickRectangleM = null;
        }

        void m_pickRectangleM_SelectionComplete(object sender, EventArgs e)
        {
            _done();
        }
        void m_pickRectangleM_SelectionChanged(object sender, EventArgs e)
        {
            if (this.m_configuring) return;
            this.m_configuring = true;
            this.m_linRectangle = this.m_pickRectangleM.SelectedRectangle;
            this.SetRectangleText();
            this.m_configuring = false;
            this.ConfigureBrush();
        }
        private void SetRectangleText()
        {
            this.c_txbrectangle.Text = string.Format("{0:0} {1:0} {2:0} {3:0}",
                m_linRectangle.X,
                m_linRectangle.Y,
                m_linRectangle.Width,
                m_linRectangle.Height);
        }

        private void rd_multi_Click(object sender, EventArgs e)
        {

        }
    }
}

