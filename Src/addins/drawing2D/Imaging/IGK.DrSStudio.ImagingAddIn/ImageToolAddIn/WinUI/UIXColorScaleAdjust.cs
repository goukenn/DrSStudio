

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: UIXColorScaleAdjust.cs
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
file:UIXColorScaleAdjust.cs
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
using IGK.ICore.WinCore.WinUI.Controls;
namespace IGK.DrSStudio.Drawing2D.WinUI
{
    /// <summary>
    /// represent the class to adjust black/white "seuil" image property
    /// </summary>
    class UIXColorScaleAdjust : UIXConfigControlBase
    {
        private IGKXDualColorTrackBar bar_red;
        private IGKXDualColorTrackBar bar_green;
        private IGKXDualColorTrackBar bar_blue;
        private IGKXLabel lb_r;
        private IGKXLabel lb_g;
        private IGKXLabel lb_b;
        private IGKXLabel xlb_r;
        private IGKXLabel xlb_g;
        private IGKXLabel xlb_b;
        private IGKXButton btn_ok;
        private IGKXLabel xlb_a;
        private IGKXLabel lb_a;
        private IGKXDualColorTrackBar bar_alpha;
        private IGKXRuleLabel xRuleLabel1;
        private IGKXCheckBox chb_globalalpha;
        private IGKXRuleLabel xRuleLabel2;
        private IGKXButton btn_cancel;
        public bool GlobalAlpha
        {
            get
            {
                return chb_globalalpha.Checked;
            }
        }
        public event EventHandler PropertyChanged;
        public float RedAdjust { get { return (float)(bar_red.Value / 100.0f); } }
        public float GreenAdjust { get { return (float)(bar_green.Value / 100.0f); } }
        public float BlueAdjust { get { return (float)(bar_blue.Value / 100.0f); } }
        public float AlphaAdjust { get { return (float)(bar_alpha.Value / 100.0f); } }
        protected virtual void OnPropertyChanged(System.EventArgs e)
{
if (PropertyChanged !=null)
PropertyChanged(this, e);
}
        //.for initialisation
        internal UIXColorScaleAdjust()
        {
            this.InitializeComponent();
        }
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (this.Parent is System.Windows.Forms.Form)
            {
                System.Windows.Forms.Form frm = this.Parent as System.Windows.Forms.Form;
                frm.AcceptButton = this.btn_ok;
                frm.CancelButton = this.btn_cancel;
            }
        }
        private void InitializeComponent()
        {
            this.bar_red = new IGKXDualColorTrackBar();
            this.bar_green = new IGKXDualColorTrackBar();
            this.bar_blue = new IGKXDualColorTrackBar();
            this.lb_r = new IGKXLabel();
            this.lb_g = new IGKXLabel();
            this.lb_b = new IGKXLabel();
            this.xlb_r = new IGKXLabel();
            this.xlb_g = new IGKXLabel();
            this.xlb_b = new IGKXLabel();
            this.btn_ok = new IGKXButton();
            this.btn_cancel = new IGKXButton();
            this.xlb_a = new IGKXLabel();
            this.lb_a = new IGKXLabel();
            this.bar_alpha = new IGKXDualColorTrackBar();
            this.xRuleLabel1 = new IGKXRuleLabel();
            this.chb_globalalpha = new IGKXCheckBox();
            this.xRuleLabel2 = new IGKXRuleLabel();
            this.SuspendLayout();
            // 
            // bar_red
            // 
            this.bar_red.EndColor = Colorf.Red;
            this.bar_red.Location = new System.Drawing.Point(77, 12);
            this.bar_red.MaxValue = 200F;
            this.bar_red.MinValue = -200F;
            this.bar_red.Name = "bar_red";
            this.bar_red.Size = new System.Drawing.Size(220, 10);
            this.bar_red.StartColor = Colorf.Black;
            this.bar_red.Step = 1F;
            this.bar_red.TabIndex = 28;
            this.bar_red.Value = 100F;
            this.bar_red.ValueChanged += new System.EventHandler(this._ValueChanged);
            // 
            // bar_green
            // 
            this.bar_green.EndColor = Colorf.Lime;
            this.bar_green.Location = new System.Drawing.Point(77, 39);
            this.bar_green.MaxValue = 200F;
            this.bar_green.MinValue = -200F;
            this.bar_green.Name = "bar_green";
            this.bar_green.Size = new System.Drawing.Size(220, 10);
            this.bar_green.StartColor = Colorf.Black;
            this.bar_green.Step = 1F;
            this.bar_green.TabIndex = 29;
            this.bar_green.Value = 100F;
            this.bar_green.ValueChanged += new System.EventHandler(this._ValueChanged);
            // 
            // bar_blue
            // 
            this.bar_blue.EndColor = Colorf.Blue;
            this.bar_blue.Location = new System.Drawing.Point(77, 66);
            this.bar_blue.MaxValue = 200F;
            this.bar_blue.MinValue = -200F;
            this.bar_blue.Name = "bar_blue";
            this.bar_blue.Size = new System.Drawing.Size(220, 10);
            this.bar_blue.StartColor = Colorf.Black;
            this.bar_blue.Step = 1F;
            this.bar_blue.TabIndex = 30;
            this.bar_blue.Value = 100F;
            this.bar_blue.ValueChanged += new System.EventHandler(this._ValueChanged);
            // 
            // lb_r
            // 
            this.lb_r.CaptionKey = "1,0";
            this.lb_r.Location = new System.Drawing.Point(303, 12);
            this.lb_r.Name = "lb_r";
            this.lb_r.Size = new System.Drawing.Size(34, 13);
            this.lb_r.TabIndex = 32;
            
            // 
            // lb_g
            // 
            this.lb_g.CaptionKey = "1,0";
            this.lb_g.Location = new System.Drawing.Point(303, 36);
            this.lb_g.Name = "lb_g";
            this.lb_g.Size = new System.Drawing.Size(34, 13);
            this.lb_g.TabIndex = 33;
            
            // 
            // lb_b
            // 
            this.lb_b.CaptionKey = "1,0";
            this.lb_b.Location = new System.Drawing.Point(303, 63);
            this.lb_b.Name = "lb_b";
            this.lb_b.Size = new System.Drawing.Size(34, 13);
            this.lb_b.TabIndex = 34;
            
            // 
            // xlb_r
            // 
            this.xlb_r.CaptionKey = "lb.R.caption";
            this.xlb_r.Location = new System.Drawing.Point(7, 12);
            this.xlb_r.Name = "xlb_r";
            this.xlb_r.Size = new System.Drawing.Size(64, 13);
            this.xlb_r.TabIndex = 36;
            // 
            // xlb_g
            // 
            this.xlb_g.CaptionKey = "lb.g.caption";
            this.xlb_g.Location = new System.Drawing.Point(7, 39);
            this.xlb_g.Name = "xlb_g";
            this.xlb_g.Size = new System.Drawing.Size(64, 13);
            this.xlb_g.TabIndex = 37;
            // 
            // xlb_b
            // 
            this.xlb_b.CaptionKey = "lb.b.caption";
            this.xlb_b.Location = new System.Drawing.Point(7, 66);
            this.xlb_b.Name = "xlb_b";
            this.xlb_b.Size = new System.Drawing.Size(64, 13);
            this.xlb_b.TabIndex = 38;
            // 
            // btn_ok
            // 
            this.btn_ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ok.CaptionKey = CoreConstant.BTN_OK;
            this.btn_ok.Checked = true;
            this.btn_ok.DialogResult = enuDialogResult.OK;
            this.btn_ok.Location = new System.Drawing.Point(180, 158);
            this.btn_ok.Name = CoreConstant.BTN_OK;
            this.btn_ok.Size = new System.Drawing.Size(75, 23);
            this.btn_ok.TabIndex = 40;
            // 
            // btn_cancel
            // 
            this.btn_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_cancel.CaptionKey = CoreConstant.BTN_CANCEL;
            this.btn_cancel.DialogResult = enuDialogResult.Cancel;
            this.btn_cancel.Location = new System.Drawing.Point(261, 158);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 41;
            // 
            // xlb_a
            // 
            this.xlb_a.CaptionKey = "lb.a.caption";
            this.xlb_a.Location = new System.Drawing.Point(3, 127);
            this.xlb_a.Name = "xlb_a";
            this.xlb_a.Size = new System.Drawing.Size(68, 13);
            this.xlb_a.TabIndex = 45;
            // 
            // lb_a
            // 
            this.lb_a.CaptionKey = "1,0";
            this.lb_a.Location = new System.Drawing.Point(303, 124);
            this.lb_a.Name = "lb_a";
            this.lb_a.Size = new System.Drawing.Size(34, 13);
            this.lb_a.TabIndex = 44;
            
            // 
            // bar_alpha
            // 
            this.bar_alpha.EndColor = Colorf.White;
            this.bar_alpha.Location = new System.Drawing.Point(77, 127);
            this.bar_alpha.MaxValue = 100F;
            this.bar_alpha.MinValue = 0F;
            this.bar_alpha.Name = "bar_alpha";
            this.bar_alpha.Size = new System.Drawing.Size(220, 10);
            this.bar_alpha.StartColor = Colorf.Black;
            this.bar_alpha.Step = 1F;
            this.bar_alpha.TabIndex = 43;
            this.bar_alpha.Value = 100F;
            this.bar_alpha.ValueChanged += new System.EventHandler(this._ValueChanged);
            // 
            // xRuleLabel1
            // 
            this.xRuleLabel1.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel1.Location = new System.Drawing.Point(7, 93);
            this.xRuleLabel1.Name = "xRuleLabel1";
            this.xRuleLabel1.Size = new System.Drawing.Size(329, 10);
            this.xRuleLabel1.TabIndex = 46;
            this.xRuleLabel1.TabStop = false;
            // 
            // chb_globalalpha
            // 
            this.chb_globalalpha.AutoSize = true;
            this.chb_globalalpha.CaptionKey = "chb_globalalpha.caption";
            this.chb_globalalpha.Location = new System.Drawing.Point(77, 107);
            this.chb_globalalpha.Name = "chb_globalalpha";
            this.chb_globalalpha.Size = new System.Drawing.Size(142, 17);
            this.chb_globalalpha.TabIndex = 47;
            this.chb_globalalpha.CheckedChanged += new System.EventHandler(this.chb_globalalpha_CheckedChanged);
            // 
            // xRuleLabel2
            // 
            this.xRuleLabel2.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel2.Location = new System.Drawing.Point(7, 143);
            this.xRuleLabel2.Name = "xRuleLabel2";
            this.xRuleLabel2.Size = new System.Drawing.Size(336, 10);
            this.xRuleLabel2.TabIndex = 48;
            this.xRuleLabel2.TabStop = false;
            // 
            // UIXAdjust_ColorScale
            // 
            this.Controls.Add(this.xRuleLabel2);
            this.Controls.Add(this.chb_globalalpha);
            this.Controls.Add(this.xRuleLabel1);
            this.Controls.Add(this.xlb_a);
            this.Controls.Add(this.lb_a);
            this.Controls.Add(this.bar_alpha);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.xlb_b);
            this.Controls.Add(this.xlb_g);
            this.Controls.Add(this.xlb_r);
            this.Controls.Add(this.lb_r);
            this.Controls.Add(this.lb_g);
            this.Controls.Add(this.lb_b);
            this.Controls.Add(this.bar_red);
            this.Controls.Add(this.bar_green);
            this.Controls.Add(this.bar_blue);
            
            this.Name = "UIXAdjust_ColorScale";
            this.Size = new System.Drawing.Size(346, 193);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private void _ValueChanged(object sender, EventArgs e)
        {
            this.lb_r.Text = string.Format("{0:##0.0}", bar_red.Value / 100.0f);
            this.lb_g.Text = string.Format("{0:##0.0}", bar_green.Value / 100.0F);
            this.lb_b.Text = string.Format("{0:##0.0}", bar_blue.Value / 100.0F);
            this.lb_a.Text = string.Format("{0:##0.0}", bar_alpha.Value / 100.0F);
            OnPropertyChanged(EventArgs.Empty);
        }
        private void chb_globalalpha_CheckedChanged(object sender, EventArgs e)
        {
            OnPropertyChanged(EventArgs.Empty);
        }
    }
}

