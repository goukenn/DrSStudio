

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XConfigureItemProperty.cs
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
file:XConfigureItemProperty.cs
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
using System.Drawing;
namespace IGK.DrSStudio.WinUI
{
    using IGK.ICore;using IGK.DrSStudio.WinUI.Configuration;
    /// <summary>
    /// represent the default configure item property
    /// </summary>
    public sealed class XConfigureItemProperty :
        UIXConfigControlBase
    {
        const int DOWN_HEIGHT = 48;
        private XPanel c_downpanel;
        private IGKXButton btn_ok;
        private IGKXButton btn_cancel;
        private IGKXRuleLabel xRuleLabel1;
        private XPanel c_toppanel;
        private ICoreWorkingConfigurableObject m_Element;
        private bool m_CanCancel;
        public bool AllowOkButton{
            get {
                return this.btn_ok.Visible;
            }
            set {
                this.btn_ok.Enabled = value;
                this.btn_ok.Visible = value;
            }
        }
        /// <summary>
        /// get or set if the properties list can bee cancelled
        /// </summary>
        public bool CanCancel
        {
            get { return m_CanCancel; }
            set
            {
                if (m_CanCancel != value)
                {
                    m_CanCancel = value;
                    this.btn_cancel.Visible = value;
                    this.btn_cancel.Enabled = value;
                }
                if (!value)
                    this.btn_ok.Bounds = this.btn_cancel.Bounds;
                else
                    this.btn_ok.Bounds = new Rectangle(this.btn_cancel.Left - this.btn_ok.Width - 5,
                this.btn_ok.Top, this.btn_ok.Width, this.btn_ok.Height);
            }
        }
        /// <summary>
        /// get the top container panel
        /// </summary>
        public System.Windows.Forms.Control TopPanel {
            get { return c_toppanel; }
        }
        /// <summary>
        /// get the top container panel
        /// </summary>
        public System.Windows.Forms.Control DownPanel
        {
            get { return c_downpanel ; }
        }
        public ICoreWorkingConfigurableObject Element
        {
            get { return m_Element; }
            set
            {
                if (m_Element != value)
                {
                    m_Element = value;
                    OnElementChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler ElementChanged;
        private void OnElementChanged(EventArgs eventArgs)
        {
            if (this.ElementChanged != null)
                this.ElementChanged(this, eventArgs);
        }
        public XConfigureItemProperty()
        {
            this.InitializeComponent();
            this.btn_cancel.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_ok.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.CanCancel = false;
            this.TopPanel.ControlAdded += new System.Windows.Forms.ControlEventHandler(TopPanel_ControlAdded);
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            //InitBound();
        }
        private void InitBound()
        {
            Size v_f = this.TopPanel.PreferredSize;
            this.TopPanel.Size = new Size(this.Width, v_f.Height);
            this.DownPanel.Location = new Point(0, v_f.Height);
            //maintain the height
            this.DownPanel.Size = new Size(this.Width, this.DownPanel.Size.Height);
        }
        void TopPanel_ControlAdded(object sender, System.Windows.Forms.ControlEventArgs e)
        {
            //update the minimun size
            this.TopPanel.Size = this.TopPanel.PreferredSize;
            this.TopPanel.MinimumSize = e.Control.Size;
            Size v_f = this.TopPanel.PreferredSize;
            this.DownPanel.Location = new Point(0, v_f.Height);
            this.DownPanel.Size = new Size(this.Width, DOWN_HEIGHT);
            this.MinimumSize = new Size(this.TopPanel.PreferredSize.Width, v_f.Height + DOWN_HEIGHT + 4);
        }
        private void InitializeComponent()
        {
            this.c_downpanel = new IGK.DrSStudio.WinUI.XPanel();
            this.btn_ok = new IGK.DrSStudio.WinUI.IGKXButton();
            this.btn_cancel = new IGK.DrSStudio.WinUI.IGKXButton();
            this.xRuleLabel1 = new IGK.DrSStudio.WinUI.IGKXRuleLabel();
            this.c_toppanel = new IGK.DrSStudio.WinUI.XPanel();
            this.c_downpanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // c_downpanel
            // 
            this.c_downpanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));                
            this.c_downpanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.c_downpanel.Location = new System.Drawing.Point(0, 362);
            this.c_downpanel.Margin = new System.Windows.Forms.Padding(0);
            this.c_downpanel.Name = "c_downpanel";
            this.c_downpanel.Padding = new System.Windows.Forms.Padding(0);
            this.c_downpanel.Size = new System.Drawing.Size(380, DOWN_HEIGHT);
            this.c_downpanel.MinimumSize = new System.Drawing.Size(0, DOWN_HEIGHT);
            this.c_downpanel.Controls.Add(this.btn_ok);
            this.c_downpanel.Controls.Add(this.btn_cancel);
            this.c_downpanel.Controls.Add(this.xRuleLabel1);
            this.c_downpanel.TabIndex = 0;
            // 
            // btn_ok
            // 
            this.btn_ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ok.CaptionKey = "btn.ok.caption";
            this.btn_ok.Checked = false;
            this.btn_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_ok.DocumentUsedAsBrush = false;
            this.btn_ok.Location = new System.Drawing.Point(221, 8);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(75, 33);
            this.btn_ok.State = IGK.DrSStudio.WinUI.enuButtonState.Normal;
            this.btn_ok.TabIndex = 2;
            // 
            // btn_cancel
            // 
            this.btn_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_cancel.CaptionKey = "btn.cancel.caption";
            this.btn_cancel.Checked = false;
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.DocumentUsedAsBrush = false;
            this.btn_cancel.Location = new System.Drawing.Point(302, 8);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 33);
            this.btn_cancel.State = IGK.DrSStudio.WinUI.enuButtonState.Normal;
            this.btn_cancel.TabIndex = 1;
            // 
            // xRuleLabel1
            // 
            this.xRuleLabel1.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.xRuleLabel1.CaptionKey = null;
            this.xRuleLabel1.Location = new System.Drawing.Point(3, 0);
            this.xRuleLabel1.Name = "xRuleLabel1";
            this.xRuleLabel1.Size = new System.Drawing.Size(377, 23);
            this.xRuleLabel1.TabIndex = 0;
            // 
            // c_toppanel
            // 
            this.c_toppanel.AutoScroll = true;
            this.c_toppanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.c_toppanel.CaptionKey = null;
            this.c_toppanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_toppanel.Location = new System.Drawing.Point(0, 0);
            this.c_toppanel.Margin = new System.Windows.Forms.Padding(0);
            this.c_toppanel.Name = "c_toppanel";
            this.c_toppanel.Padding = new System.Windows.Forms.Padding(3);
            this.c_toppanel.Size = new System.Drawing.Size(380, 362);
            this.c_toppanel.TabIndex = 1;
            // 
            // XConfigureItemProperty
            // 
            this.AcceptButton = this.btn_ok;
            this.CancelButton = this.btn_cancel;
            this.Controls.Add(this.c_toppanel);
            this.Controls.Add(this.c_downpanel);
            this.MinimumSize = new System.Drawing.Size(340, 420);
            this.Name = "XConfigureItemProperty";
            this.Size = new System.Drawing.Size(380, 420);
            this.Load += new System.EventHandler(this._Load);
            this.c_downpanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }
        private void _Load(object sender, EventArgs e)
        {
            this.AcceptButton = this.btn_ok;
            this.CancelButton = this.btn_cancel;
            if (!this.AllowOkButton && !this.CanCancel)
            {
                this.c_downpanel.Visible = false;
            }
            else
                this.c_downpanel.Visible = true;
        }
    }
}

