

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: UIXAdjustHSLValue.cs
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
file:UIXAdjustHSLValue.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms ;
using System.IO;
namespace IGK.DrSStudio.Drawing2D.WinUI
{
    using IGK.ICore;using IGK.DrSStudio.WinUI ;
    class UIXAdjustHSLValue : XAcceptOrCancelControl 
    {
        private IGKXBulletIntervalTrackBar c_g;
        private IGKXBulletIntervalTrackBar c_b;
        private IGKXLabel xLabel1;
        private IGKXLabel xLabel2;
        private IGKXLabel xLabel3;
        private IGKXLabel lb_r;
        private IGKXLabel lb_g;
        private IGKXLabel lb_b;
        private IGKXRuleLabel xRuleLabel1;
        private IGKXButton btn_ok;
        private IGKXButton btn_cancel;
        private IGKXCheckBox xCheckBox1;
        private IGKXComboBox c_cmb_mode;
        private XHueTrackbackInterval xHueTrackbackInterval1;
        private IGKXLabel xLabel4;
        public event EventHandler PropertyChanged;
        public WinCoreBulletInterval IntervalHue { get { return this.xHueTrackbackInterval1.Interval ; } }
        public WinCoreBulletInterval IntervalSaturation { get { return this.c_g.Interval; } }
        public WinCoreBulletInterval IntervalLuminosity { get { return this.c_b.Interval; } }
        public bool Exclude { get { return this.xCheckBox1.Checked; } }
        protected virtual void OnPropertyChanged(System.EventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
        public UIXAdjustHSLValue()
        {
            this.InitializeComponent();
            this.AcceptButton = this.btn_ok;
            this.CancelButton = this.btn_cancel;
            this.lb_r.Text = string.Format("{0}-{1}",
               this.xHueTrackbackInterval1.Interval.Min,
               this.xHueTrackbackInterval1.Interval.Max);
            this.lb_g.Text = string.Format("{0}-{1}",
                this.c_g.Interval.Min,
                this.c_g.Interval.Max);
            this.lb_b.Text = string.Format("{0}-{1}",
                this.c_b.Interval.Min,
                this.c_b.Interval.Max);
        }
        private void InitializeComponent()
        {
            this.c_g = new IGKXBulletIntervalTrackBar();
            this.c_b = new IGKXBulletIntervalTrackBar();
            this.xLabel1 = new IGK.DrSStudio.WinUI.IGKXLabel();
            this.xLabel2 = new IGK.DrSStudio.WinUI.IGKXLabel();
            this.xLabel3 = new IGK.DrSStudio.WinUI.IGKXLabel();
            this.lb_r = new IGK.DrSStudio.WinUI.IGKXLabel();
            this.lb_g = new IGK.DrSStudio.WinUI.IGKXLabel();
            this.lb_b = new IGK.DrSStudio.WinUI.IGKXLabel();
            this.xRuleLabel1 = new IGK.DrSStudio.WinUI.IGKXRuleLabel();
            this.btn_ok = new IGK.DrSStudio.WinUI.IGKXButton();
            this.btn_cancel = new IGK.DrSStudio.WinUI.IGKXButton();
            this.xCheckBox1 = new IGK.DrSStudio.WinUI.IGKXCheckBox();
            this.c_cmb_mode = new IGK.DrSStudio.WinUI.IGKXComboBox();
            this.xLabel4 = new IGK.DrSStudio.WinUI.IGKXLabel();
            this.xHueTrackbackInterval1 = new XHueTrackbackInterval();
            this.SuspendLayout();
            // 
            // c_g
            // 
            this.c_g.Location = new System.Drawing.Point(84, 56);
            this.c_g.MaxValue = 100;
            this.c_g.MinValue = 0;
            this.c_g.Name = "c_g";
            this.c_g.Size = new System.Drawing.Size(171, 24);
            this.c_g.TabIndex = 1;
            this.c_g.IntervalChanged += new System.EventHandler(this.xBulletIntervalTrackBar2_IntervalChanged);
            this.c_g.Interval = new BulletInterval(0, 100);
            // 
            // c_b
            // 
            this.c_b.Location = new System.Drawing.Point(84, 86);
            this.c_b.MaxValue = 100;
            this.c_b.MinValue = 0;
            this.c_b.Name = "c_b";
            this.c_b.Size = new System.Drawing.Size(171, 24);
            this.c_b.TabIndex = 2;
            this.c_b.IntervalChanged += new System.EventHandler(this.xBulletIntervalTrackBar2_IntervalChanged);
            this.c_b.Interval = new WinCoreBulletInterval(0, 100);
            // 
            // xLabel1
            // 
            this.xLabel1.CaptionKey = "lb.Hue.Title";
            this.xLabel1.Location = new System.Drawing.Point(0, 26);
            this.xLabel1.Name = "xLabel1";
            this.xLabel1.Size = new System.Drawing.Size(78, 16);
            this.xLabel1.TabIndex = 3;
            // 
            // xLabel2
            // 
            this.xLabel2.CaptionKey = "lb.Saturation.Title";
            this.xLabel2.Location = new System.Drawing.Point(0, 56);
            this.xLabel2.Name = "xLabel2";
            this.xLabel2.Size = new System.Drawing.Size(78, 16);
            this.xLabel2.TabIndex = 4;
            // 
            // xLabel3
            // 
            this.xLabel3.CaptionKey = "lb.Lum.Title";
            this.xLabel3.Location = new System.Drawing.Point(0, 86);
            this.xLabel3.Name = "xLabel3";
            this.xLabel3.Size = new System.Drawing.Size(78, 16);
            this.xLabel3.TabIndex = 5;
            // 
            // lb_r
            // 
            this.lb_r.CaptionKey = "0-0";
            this.lb_r.Location = new System.Drawing.Point(261, 26);
            this.lb_r.Name = "lb_r";
            this.lb_r.Size = new System.Drawing.Size(62, 16);
            this.lb_r.TabIndex = 6;
            // 
            // lb_g
            // 
            this.lb_g.CaptionKey = "0-0";
            this.lb_g.Location = new System.Drawing.Point(261, 56);
            this.lb_g.Name = "lb_g";
            this.lb_g.Size = new System.Drawing.Size(62, 16);
            this.lb_g.TabIndex = 7;
            // 
            // lb_b
            // 
            this.lb_b.CaptionKey = "0-0";
            this.lb_b.Location = new System.Drawing.Point(261, 86);
            this.lb_b.Name = "lb_b";
            this.lb_b.Size = new System.Drawing.Size(62, 16);
            this.lb_b.TabIndex = 8;
            // 
            // xRuleLabel1
            // 
            this.xRuleLabel1.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xRuleLabel1.CaptionKey = null;
            this.xRuleLabel1.Location = new System.Drawing.Point(0, 248);
            this.xRuleLabel1.Name = "xRuleLabel1";
            this.xRuleLabel1.Size = new System.Drawing.Size(326, 10);
            this.xRuleLabel1.TabIndex = 9;
            // 
            // btn_ok
            // 
            this.btn_ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ok.ButtonImageSize = new Size2i(32, 32);
            this.btn_ok.CaptionKey = "btn.ok.caption";
            this.btn_ok.Checked = false;
            this.btn_ok.DialogResult = enuDialogResult.OK;
            this.btn_ok.Location = new System.Drawing.Point(158, 275);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.ShowButtonImage = false;
            this.btn_ok.Size = new System.Drawing.Size(75, 23);
            this.btn_ok.State = IGK.DrSStudio.WinUI.enuButtonState.Normal;
            this.btn_ok.TabIndex = 10;
            // 
            // btn_cancel
            // 
            this.btn_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_cancel.ButtonImageSize = new Size2i(32, 32);
            this.btn_cancel.CaptionKey = "btn.cancel.caption";
            this.btn_cancel.Checked = false;
            this.btn_cancel.DialogResult = enuDialogResult.Cancel;
            this.btn_cancel.Location = new System.Drawing.Point(239, 275);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.ShowButtonImage = false;
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.State = IGK.DrSStudio.WinUI.enuButtonState.Normal;
            this.btn_cancel.TabIndex = 11;
            // 
            // xCheckBox1
            // 
            this.xCheckBox1.AutoSize = true;
            this.xCheckBox1.CaptionKey = "chb.transparency.Exclude.caption";
            this.xCheckBox1.Location = new System.Drawing.Point(84, 116);
            this.xCheckBox1.Name = "xCheckBox1";
            this.xCheckBox1.Size = new System.Drawing.Size(187, 17);
            this.xCheckBox1.TabIndex = 12;
            this.xCheckBox1.CheckedChanged += new System.EventHandler(this.xCheckBox1_CheckedChanged);
            // 
            // c_cmb_mode
            // 
            this.c_cmb_mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.c_cmb_mode.FormattingEnabled = true;
            this.c_cmb_mode.Location = new System.Drawing.Point(84, 138);
            this.c_cmb_mode.Name = "c_cmb_mode";
            this.c_cmb_mode.Size = new System.Drawing.Size(171, 21);
            this.c_cmb_mode.TabIndex = 15;
            // 
            // xLabel4
            // 
            this.xLabel4.CaptionKey = "lb.Mode.Title";
            this.xLabel4.Location = new System.Drawing.Point(0, 143);
            this.xLabel4.Name = "xLabel4";
            this.xLabel4.Size = new System.Drawing.Size(78, 16);
            this.xLabel4.TabIndex = 16;
            // 
            // xHueTrackbackInterval1
            // 
            this.xHueTrackbackInterval1.Location = new System.Drawing.Point(84, 18);
            this.xHueTrackbackInterval1.MaxValue = 360;
            this.xHueTrackbackInterval1.MinValue = 0;
            this.xHueTrackbackInterval1.Name = "xHueTrackbackInterval1";
            this.xHueTrackbackInterval1.Size = new System.Drawing.Size(171, 32);
            this.xHueTrackbackInterval1.TabIndex = 17;
            this.xHueTrackbackInterval1.IntervalChanged += new System.EventHandler(this.xBulletIntervalTrackBar2_IntervalChanged);
            // 
            // UIXAdjustHSLValue
            // 
            this.Controls.Add(this.xHueTrackbackInterval1);
            this.Controls.Add(this.xLabel4);
            this.Controls.Add(this.c_cmb_mode);
            this.Controls.Add(this.xCheckBox1);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.xRuleLabel1);
            this.Controls.Add(this.lb_b);
            this.Controls.Add(this.lb_g);
            this.Controls.Add(this.lb_r);
            this.Controls.Add(this.xLabel3);
            this.Controls.Add(this.xLabel2);
            this.Controls.Add(this.xLabel1);
            this.Controls.Add(this.c_b);
            this.Controls.Add(this.c_g);
            this.Name = "UIXAdjustHSLValue";
            this.Size = new System.Drawing.Size(329, 304);
            this.Load += new System.EventHandler(this.UIXSetTransparencyColorKeyInterval_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private void xBulletIntervalTrackBar2_IntervalChanged(object sender, EventArgs e)
        {
            this.lb_r.Text = string.Format("{0}-{1}",
                this.xHueTrackbackInterval1 .Interval.Min ,
                this.xHueTrackbackInterval1.Interval.Max);
            this.lb_g.Text = string.Format("{0}-{1}",
                this.c_g.Interval.Min,
                this.c_g.Interval.Max);
            this.lb_b.Text = string.Format("{0}-{1}",
                this.c_b.Interval.Min,
                this.c_b.Interval.Max);
            this.OnPropertyChanged(EventArgs.Empty);
        }
        private void xCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.OnPropertyChanged(EventArgs.Empty);
        }
        private void UIXSetTransparencyColorKeyInterval_Load(object sender, EventArgs e)
        {
            this.c_cmb_mode.DataSource = Enum.GetValues (typeof(enuCallBackType));
            if (this.c_cmb_mode.Items.Count > 0)
                this.c_cmb_mode.SelectedItem = enuCallBackType.GrayScale;
            this.c_cmb_mode.SelectedIndexChanged += c_cmb_mode_SelectedIndexChanged;
        }
        /// <summary>
        /// get the call back type
        /// </summary>
        public enuCallBackType CallBackType {
            get {
                if (this.InvokeRequired)
                {
                    enuCallBackType t = enuCallBackType.GrayScale ;
                    MethodInvoker meth =(MethodInvoker) delegate()
                    {
                        t = this.CallBackType;
                    };
                    this.Invoke(meth);
                    return t;
                }
                else
                    return (enuCallBackType)this.c_cmb_mode.SelectedItem;
            }
        }
        void c_cmb_mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.OnPropertyChanged(EventArgs.Empty);
        }
    }
}

