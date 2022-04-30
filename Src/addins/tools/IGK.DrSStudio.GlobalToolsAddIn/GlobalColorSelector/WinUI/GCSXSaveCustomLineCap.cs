

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GCSXSaveCustomLineCap.cs
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
file:XSaveCustomLineCap.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Drawing2D;
using IGK.ICore.WinUI;
using IGK.ICore.WinCore.WinUI.Controls;
namespace IGK.DrSStudio.WinUI
{
    sealed class GCSXSaveCustomLineCap : UIXConfigControlBase 
    {
        private string m_CapName;
        private enuLineCap m_DefaultLineCap;
        private IGKXLabel xLabel1;
        private IGKXTextBox xTextBox1;
        private IGKXLabel xLabel2;
        private IGKXRuleLabel xRuleLabel1;
        private IGKXLabel xLabel3;
        private IGKXNumericTextBox xNumericTextBox1;
        private IGKXComboBox xComboBox1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.ComponentModel.IContainer components;
        private IGKXButton btn_cancel;
        private IGKXButton btn_ok;
        private IGKXRuleLabel xRuleLabel2;
        private float m_CapInset;
        public float CapInset
        {
            get { return m_CapInset; }
            set
            {
                if (m_CapInset != value)
                {
                    m_CapInset = value;
                }
            }
        }
        public enuLineCap DefaultLineCap
        {
            get { return m_DefaultLineCap; }
            set
            {
                if (m_DefaultLineCap != value)
                {
                    m_DefaultLineCap = value;
                }
            }
        }
        public string CapName
        {
            get { return m_CapName; }
            set
            {
                if (m_CapName != value)
                {
                    m_CapName = value;
                }
            }
        }
        public GCSXSaveCustomLineCap()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.xLabel1 = new IGKXLabel();
            this.xTextBox1 = new IGKXTextBox();
            this.xLabel2 = new IGKXLabel();
            this.xRuleLabel1 = new IGKXRuleLabel();
            this.xLabel3 = new IGKXLabel();
            this.xNumericTextBox1 = new IGKXNumericTextBox();
            this.xComboBox1 = new IGKXComboBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.xRuleLabel2 = new IGKXRuleLabel();
            this.btn_ok = new IGKXButton();
            this.btn_cancel = new IGKXButton();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // xLabel1
            // 
            this.xLabel1.AutoSize = true;
            this.xLabel1.CaptionKey = "lb.Name.caption";
            this.xLabel1.Location = new System.Drawing.Point(9, 25);
            this.xLabel1.Name = "xLabel1";
            this.xLabel1.Size = new System.Drawing.Size(84, 13);
            this.xLabel1.TabIndex = 0;
            // 
            // xTextBox1
            // 
            this.xTextBox1.Location = new System.Drawing.Point(117, 18);
            this.xTextBox1.Name = "xTextBox1";
            this.xTextBox1.Size = new System.Drawing.Size(179, 20);
            this.xTextBox1.TabIndex = 1;
            this.xTextBox1.Text = "CapName";
            this.xTextBox1.TextChanged += new System.EventHandler(this.xTextBox1_TextChanged);
            // 
            // xLabel2
            // 
            this.xLabel2.AutoSize = true;
            this.xLabel2.CaptionKey = "lb.baseInset.caption";
            this.xLabel2.Location = new System.Drawing.Point(9, 126);
            this.xLabel2.Name = "xLabel2";
            this.xLabel2.Size = new System.Drawing.Size(102, 13);
            this.xLabel2.TabIndex = 2;
            // 
            // xRuleLabel1
            // 
            this.xRuleLabel1.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel1.CaptionKey = null;
            this.xRuleLabel1.Location = new System.Drawing.Point(3, 57);
            this.xRuleLabel1.Name = "xRuleLabel1";
            this.xRuleLabel1.Size = new System.Drawing.Size(327, 24);
            this.xRuleLabel1.TabIndex = 4;
            // 
            // xLabel3
            // 
            this.xLabel3.AutoSize = true;
            this.xLabel3.CaptionKey = "lb.DefaultCap.caption";
            this.xLabel3.Location = new System.Drawing.Point(9, 101);
            this.xLabel3.Name = "xLabel3";
            this.xLabel3.Size = new System.Drawing.Size(109, 13);
            this.xLabel3.TabIndex = 5;
            // 
            // xNumericTextBox1
            // 
            this.xNumericTextBox1.Location = new System.Drawing.Point(117, 126);
            this.xNumericTextBox1.MaxLength = 10;
            this.xNumericTextBox1.Name = "xNumericTextBox1";
            this.xNumericTextBox1.Size = new System.Drawing.Size(179, 20);
            this.xNumericTextBox1.TabIndex = 7;
            this.xNumericTextBox1.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.xNumericTextBox1.ValueChanged += new System.EventHandler(this.xNumericTextBox1_ValueChanged);
            // 
            // xComboBox1
            // 
            this.xComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.xComboBox1.FormattingEnabled = true;
            this.xComboBox1.Location = new System.Drawing.Point(117, 93);
            this.xComboBox1.Name = "xComboBox1";
            this.xComboBox1.Size = new System.Drawing.Size(179, 21);
            this.xComboBox1.TabIndex = 8;
            this.xComboBox1.SelectedIndexChanged += new System.EventHandler(this.xComboBox1_SelectedIndexChanged);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // xRuleLabel2
            // 
            this.xRuleLabel2.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel2.CaptionKey = null;
            this.xRuleLabel2.Location = new System.Drawing.Point(3, 160);
            this.xRuleLabel2.Name = "xRuleLabel2";
            this.xRuleLabel2.Size = new System.Drawing.Size(327, 24);
            this.xRuleLabel2.TabIndex = 9;
            // 
            // btn_ok
            // 
            this.btn_ok.CaptionKey = "btn.ok.caption";
            this.btn_ok.Checked = false;
            this.btn_ok.DialogResult = enuDialogResult.OK;
            this.btn_ok.DocumentUsedAsBrush = false;
            this.btn_ok.Location = new System.Drawing.Point(220, 171);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(76, 32);
            this.btn_ok.State = enuButtonState.Normal;
            this.btn_ok.TabIndex = 10;
            // 
            // btn_cancel
            // 
            this.btn_cancel.CaptionKey = "btn.cancel.caption";
            this.btn_cancel.Checked = false;
            this.btn_cancel.DialogResult = enuDialogResult.Cancel;
            this.btn_cancel.DocumentUsedAsBrush = false;
            this.btn_cancel.Location = new System.Drawing.Point(138, 171);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(76, 32);
            this.btn_cancel.State = enuButtonState.Normal;
            this.btn_cancel.TabIndex = 11;
            // 
            // XSaveCustomLineCap
            // 
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.xRuleLabel2);
            this.Controls.Add(this.xComboBox1);
            this.Controls.Add(this.xNumericTextBox1);
            this.Controls.Add(this.xLabel3);
            this.Controls.Add(this.xRuleLabel1);
            this.Controls.Add(this.xLabel2);
            this.Controls.Add(this.xTextBox1);
            this.Controls.Add(this.xLabel1);
            this.Name = "XSaveCustomLineCap";
            this.Size = new System.Drawing.Size(333, 222);
            this.Load += new System.EventHandler(this._Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private void _Load(object sender, EventArgs e)
        {
            foreach (LineCap item in Enum.GetValues (typeof (LineCap )))
            {
                if (item != LineCap.Custom )
                    this.xComboBox1.Items.Add (item );
            }
            this.xComboBox1.SelectedItem = LineCap.Flat ;
            this.AcceptButton = this.btn_ok;
            this.CancelButton = this.btn_cancel;
        }
        private void xTextBox1_TextChanged(object sender, EventArgs e)
        {
            ICoreLineCap v_dp = CorePenLineCap.GetLineCap(xTextBox1.Text);
            if (string.IsNullOrEmpty(xTextBox1.Text) || (v_dp.LineCap == enuLineCap.Custom ))
            {
                this.errorProvider1.SetError(xTextBox1, "value is null or allready registred");
                this.m_CapName = null;
            }
            else
            {
                this.errorProvider1.SetError(xTextBox1, null);
                this.m_CapName = this.xTextBox1.Text;
            }
        }
        private void xComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.m_DefaultLineCap = (enuLineCap)
                this.xComboBox1.SelectedItem;
        }
        private void xNumericTextBox1_ValueChanged(object sender, EventArgs e)
        {
            this.m_CapInset = (float ) this.xNumericTextBox1.Value;
        }
    }
}

