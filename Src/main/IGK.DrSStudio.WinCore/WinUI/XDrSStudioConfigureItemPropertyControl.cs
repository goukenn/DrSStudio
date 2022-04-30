

/*
IGKDEV @ 2008-2016
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
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:XConfigureItemProperty.cs
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGK.ICore.WinCore.WinUI.Controls;
namespace IGK.DrSStudio.WinUI
{
    [CoreRegistrableControl(Name="XConfigureItemPropertyControl")]
    public sealed class XDrSStudioConfigureItemPropertyControl : XAcceptOrCancelControl     
    {
        public const int DOWN_HEIGHT = 64;
        
        private IGKXButton btn_ok;
        private IGKXButton btn_cancel;
        private IGKXRuleLabel c_optionrules;
        private IGKXPanel c_contentpanel;
        private IGKXPanel c_optionspanel;
        private ICoreWorkingConfigurableObject m_Element;
     
        public bool AllowOkButton
        {
            get
            {
                return this.btn_ok.Visible;
            }
            set
            {
                this.btn_ok.Enabled = value;
                this.btn_ok.Visible = value;
            }
        }
    
        /// <summary>
        /// get the top container panel
        /// </summary>
        public Control ContentPanel
        {
            get { return c_contentpanel; }
        }
        /// <summary>
        /// get the top container panel
        /// </summary>
        public Control DownPanel
        {
            get { return c_optionspanel; }
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
        public XDrSStudioConfigureItemPropertyControl()
        {
            this.InitializeComponent();
            this.btn_cancel.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_ok.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;            
        }

        private void InitializeComponent()
        {
            this.c_optionspanel = new IGK.ICore.WinCore.WinUI.Controls.IGKXPanel();
            this.btn_ok = new IGK.ICore.WinCore.WinUI.Controls.IGKXButton();
            this.btn_cancel = new IGK.ICore.WinCore.WinUI.Controls.IGKXButton();
            this.c_optionrules = new IGK.ICore.WinUI.IGKXRuleLabel();
            this.c_contentpanel = new IGK.ICore.WinCore.WinUI.Controls.IGKXPanel();
            this.c_optionspanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // c_optionspanel
            // 
            this.c_optionspanel.CaptionKey = null;
            this.c_optionspanel.Controls.Add(this.btn_ok);
            this.c_optionspanel.Controls.Add(this.btn_cancel);
            this.c_optionspanel.Controls.Add(this.c_optionrules);
            this.c_optionspanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.c_optionspanel.Location = new System.Drawing.Point(0, 150);
            this.c_optionspanel.Margin = new System.Windows.Forms.Padding(0);
            this.c_optionspanel.Name = "c_optionspanel";
            this.c_optionspanel.Size = new System.Drawing.Size(400, 64);
            this.c_optionspanel.TabIndex = 1;
            // 
            // btn_ok
            // 
            this.btn_ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ok.CaptionKey = "btn.ok.caption";
            this.btn_ok.Checked = false;
            this.btn_ok.DialogResult = IGK.ICore.WinUI.enuDialogResult.OK;
            this.btn_ok.Location = new System.Drawing.Point(294, 19);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.ShowButtonImage = false;
            this.btn_ok.Size = new System.Drawing.Size(100, 33);
            this.btn_ok.State = IGK.ICore.WinUI.enuButtonState.Normal;
            this.btn_ok.TabIndex = 0;
            // 
            // btn_cancel
            // 
            this.btn_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_cancel.CaptionKey = "btn.cancel.caption";
            this.btn_cancel.Checked = false;
            this.btn_cancel.DialogResult = IGK.ICore.WinUI.enuDialogResult.Cancel;
            this.btn_cancel.Location = new System.Drawing.Point(186, 19);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.ShowButtonImage = false;
            this.btn_cancel.Size = new System.Drawing.Size(102, 33);
            this.btn_cancel.State = IGK.ICore.WinUI.enuButtonState.Normal;
            this.btn_cancel.TabIndex = 1;
            // 
            // c_optionrules
            // 
            this.c_optionrules.Alignment = System.Drawing.StringAlignment.Near;
            this.c_optionrules.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c_optionrules.CaptionKey = null;
            this.c_optionrules.HorizontalAlignment = IGK.ICore.Drawing2D.enuStringAlignment.Near;
            this.c_optionrules.Location = new System.Drawing.Point(3, 3);
            this.c_optionrules.Name = "c_optionrules";
            this.c_optionrules.Size = new System.Drawing.Size(391, 10);
            this.c_optionrules.TabIndex = 0;
            this.c_optionrules.TabStop = false;
            this.c_optionrules.VerticalAlignment = IGK.ICore.Drawing2D.enuStringAlignment.Near;
            // 
            // c_contentpanel
            // 
            this.c_contentpanel.AutoScroll = true;
            this.c_contentpanel.CaptionKey = null;
            this.c_contentpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_contentpanel.Location = new System.Drawing.Point(0, 0);
            this.c_contentpanel.Margin = new System.Windows.Forms.Padding(0);
            this.c_contentpanel.Name = "c_contentpanel";
            this.c_contentpanel.Size = new System.Drawing.Size(400, 150);
            this.c_contentpanel.TabIndex = 0;
            // 
            // XDrSStudioConfigureItemPropertyControl
            // 
            this.Controls.Add(this.c_contentpanel);
            this.Controls.Add(this.c_optionspanel);
            this.Name = "XDrSStudioConfigureItemPropertyControl";
            this.Size = new System.Drawing.Size(400, 214);
            this.Load += new System.EventHandler(this._Load);
            this.c_optionspanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        private void _Load(object sender, EventArgs e)
        {
            this.AcceptButton = this.btn_ok;
            this.CancelButton = this.btn_cancel;
            this.c_optionspanel.Size = new System.Drawing.Size(this.Width, DOWN_HEIGHT);
            if (!this.AllowOkButton)
            {
                this.c_optionspanel.Visible = false;
            }
            else
                this.c_optionspanel.Visible = true;
        }
    }
}

