

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: UIXAdjustGrayLevel.cs
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
file:UIXAdjustGrayLevel.cs
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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IGK.ICore.WinCore.WinUI.Controls;
namespace IGK.DrSStudio.Drawing2D.WinUI
{
    public partial class UIXAdjustGrayLevel : UIXImageSurfaceConfigBase
    {
        public UIXAdjustGrayLevel()
        {
            InitializeComponent();
        }
           private IGKXDualColorTrackBar bar_red;
        private IGKXDualColorTrackBar bar_green;
        private IGKXDualColorTrackBar bar_blue;
        private IGKXLabel lb_scale;
        private IGKXLabel lb_luminance;
        private IGKXLabel lb_saturation;
        private IGKXLabel xLabel1;
        private IGKXLabel xLabel2;
        private IGKXLabel xLabel3;
        private IGKXButton btn_ok;
        private IGKXRuleLabel xRuleLabel1;
        private IGKXButton btn_cancel;
        public event EventHandler PropertyChanged;
        public float RedAdjust { get { return (bar_red.Value /100.0f); } }
        public float GreenAdjust{ get { return (bar_green.Value/100.0f); } }
        public float BlueAdjust { get { return (bar_blue.Value/100.0f); } }
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (this.Parent is Form)
            {
                Form v_frm = this.Parent as Form;
                v_frm.AcceptButton = this.btn_ok;
                v_frm.CancelButton = this.btn_cancel;
            }
        }
        private void InitializeComponent()
        {
            this.bar_red = new IGKXDualColorTrackBar();
            this.bar_green = new IGKXDualColorTrackBar();
            this.bar_blue = new IGKXDualColorTrackBar();
            this.lb_scale = new IGKXLabel();
            this.lb_luminance = new IGKXLabel();
            this.lb_saturation = new IGKXLabel();
            this.xLabel1 = new IGKXLabel();
            this.xLabel2 = new IGKXLabel();
            this.xLabel3 = new IGKXLabel();
            this.btn_ok = new IGKXButton();
            this.btn_cancel = new IGKXButton();
            this.xRuleLabel1 = new IGKXRuleLabel();
            this.SuspendLayout();
            // 
            // bar_red
            // 
            this.bar_red.EndColor = Colorf.Red;
            this.bar_red.Location = new System.Drawing.Point(77, 12);
            this.bar_red.MaxValue = 255F;
            this.bar_red.MinValue = 0F;
            this.bar_red.Name = "bar_red";
            this.bar_red.Size = new System.Drawing.Size(220, 10);
            this.bar_red.StartColor = Colorf.Black;
            this.bar_red.Step = 1F;
            this.bar_red.TabIndex = 28;
            this.bar_red.Value = 127F;
            this.bar_red.ValueChanged += new System.EventHandler(this._ValueChanged);
            // 
            // bar_green
            // 
            this.bar_green.EndColor = Colorf.Lime;
            this.bar_green.Location = new System.Drawing.Point(77, 39);
            this.bar_green.MaxValue = 255F;
            this.bar_green.MinValue = 0F;
            this.bar_green.Name = "bar_green";
            this.bar_green.Size = new System.Drawing.Size(220, 10);
            this.bar_green.StartColor = Colorf.Black;
            this.bar_green.Step = 1F;
            this.bar_green.TabIndex = 29;
            this.bar_green.Value = 127F;
            this.bar_green.ValueChanged += new System.EventHandler(this._ValueChanged);
            // 
            // bar_blue
            // 
            this.bar_blue.EndColor = Colorf.Blue;
            this.bar_blue.Location = new System.Drawing.Point(77, 66);
            this.bar_blue.MaxValue = 255F;
            this.bar_blue.MinValue = 0F;
            this.bar_blue.Name = "bar_blue";
            this.bar_blue.Size = new System.Drawing.Size(220, 10);
            this.bar_blue.StartColor = Colorf.Black;
            this.bar_blue.Step = 1F;
            this.bar_blue.TabIndex = 30;
            this.bar_blue.Value = 127F;
            this.bar_blue.ValueChanged += new System.EventHandler(this._ValueChanged);
            // 
            // lb_scale
            // 
            this.lb_scale.Location = new System.Drawing.Point(303, 12);
            this.lb_scale.Name = "lb_scale";
            this.lb_scale.Size = new System.Drawing.Size(34, 13);
            this.lb_scale.TabIndex = 32;
            this.lb_scale.CaptionKey  = "127";
            
            // 
            // lb_luminance
            // 
            this.lb_luminance.Location = new System.Drawing.Point(303, 36);
            this.lb_luminance.Name = "lb_luminance";
            this.lb_luminance.Size = new System.Drawing.Size(34, 13);
            this.lb_luminance.TabIndex = 33;
            this.lb_luminance.CaptionKey = "127";
            
            // 
            // lb_saturation
            // 
            this.lb_saturation.Location = new System.Drawing.Point(303, 63);
            this.lb_saturation.Name = "lb_saturation";
            this.lb_saturation.Size = new System.Drawing.Size(34, 13);
            this.lb_saturation.TabIndex = 34;
            this.lb_saturation.CaptionKey ="127";
            
            // 
            // xLabel1
            // 
            this.xLabel1.CaptionKey = "lb.r.caption";
            this.xLabel1.Location = new System.Drawing.Point(3, 12);
            this.xLabel1.Name = "xLabel1";
            this.xLabel1.Size = new System.Drawing.Size(68, 13);
            this.xLabel1.TabIndex = 36;
            // 
            // xLabel2
            // 
            this.xLabel2.CaptionKey = "lb.G.caption";
            this.xLabel2.Location = new System.Drawing.Point(3, 39);
            this.xLabel2.Name = "xLabel2";
            this.xLabel2.Size = new System.Drawing.Size(68, 13);
            this.xLabel2.TabIndex = 37;
            // 
            // xLabel3
            // 
            this.xLabel3.CaptionKey = "lb.B.caption";
            this.xLabel3.Location = new System.Drawing.Point(3, 66);
            this.xLabel3.Name = "xLabel3";
            this.xLabel3.Size = new System.Drawing.Size(68, 13);
            this.xLabel3.TabIndex = 38;
            // 
            // btn_ok
            // 
            this.btn_ok.CaptionKey = CoreConstant.BTN_OK;
            this.btn_ok.Checked = true;
            this.btn_ok.DialogResult = enuDialogResult.OK;
            this.btn_ok.Location = new System.Drawing.Point(180, 116);
            this.btn_ok.Name = CoreConstant.BTN_OK;
            this.btn_ok.Size = new System.Drawing.Size(75, 23);
            this.btn_ok.TabIndex = 40;
            // 
            // btn_cancel
            // 
            this.btn_cancel.CaptionKey = CoreConstant.BTN_CANCEL;
            this.btn_cancel.DialogResult = enuDialogResult.Cancel;
            this.btn_cancel.Location = new System.Drawing.Point(261, 116);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 41;
            // 
            // xRuleLabel1
            // 
            this.xRuleLabel1.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel1.Location = new System.Drawing.Point(3, 98);
            this.xRuleLabel1.Name = "xRuleLabel1";
            this.xRuleLabel1.Size = new System.Drawing.Size(334, 12);
            this.xRuleLabel1.TabIndex = 42;
            this.xRuleLabel1.TabStop = false;
            // 
            // UIXAdjust_GrayTolerance
            // 
            this.Controls.Add(this.xRuleLabel1);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.xLabel3);
            this.Controls.Add(this.xLabel2);
            this.Controls.Add(this.xLabel1);
            this.Controls.Add(this.lb_scale);
            this.Controls.Add(this.lb_luminance);
            this.Controls.Add(this.lb_saturation);
            this.Controls.Add(this.bar_red);
            this.Controls.Add(this.bar_green);
            this.Controls.Add(this.bar_blue);
            
            this.Name = "UIXAdjust_GrayTolerance";
            this.Size = new System.Drawing.Size(346, 152);
            this.ResumeLayout(false);
        }
        private void _ValueChanged(object sender, EventArgs e)
        {
            this.lb_scale.Text = string.Format("{0:##0.00}", bar_red.Value/255.0f);
            this.lb_luminance.Text = string.Format("{0:##0.00}", bar_green.Value/255.0f);
            this.lb_saturation.Text = string.Format("{0:##0.00}", bar_blue.Value/255.0f);            
            OnPropertyChanged(EventArgs.Empty);
        }
        private void OnPropertyChanged(EventArgs eventArgs)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, eventArgs);
        }
    }
}

