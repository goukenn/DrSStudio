

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CircleTextEditor.Designer.cs
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
file:CircleTextEditor.Designer.cs
*/
/* 
-------------------------------------------------------------
This file is part of iGK-DEV-DrawingStudio
-------------------------------------------------------------
-------------------------------------------------------------
-------------------------------------------------------------
view license file in Documentation folder to get more info
Copyright (c) 2008-2010 
Author  : Charles A.D. BONDJE DOUE 
mail : bondje.doue@hotmail.com
-------------------------------------------------------------
-------------------------------------------------------------
*/
namespace IGK.DrSStudio.Drawing2D.WinUI
{
    partial class CircleTextEditor
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txb_text = new System.Windows.Forms.TextBox();
            this.xRuleLabel1 = new IGK.DrSStudio.WinUI.IGKXRuleLabel();
            this.xRuleLabel2 = new IGK.DrSStudio.WinUI.IGKXRuleLabel();
            this.c_num_PortionAngle = new System.Windows.Forms.NumericUpDown();
            this.xRuleLabel3 = new IGK.DrSStudio.WinUI.IGKXRuleLabel();
            this.c_cmb_orientation = new IGK.DrSStudio.WinUI.IGKXComboBox();
            this.c_numoffsetAngle = new System.Windows.Forms.NumericUpDown();
            this.xRuleLabel4 = new IGK.DrSStudio.WinUI.IGKXRuleLabel();
            this.c_num_orientationAngle = new System.Windows.Forms.NumericUpDown();
            this.xRuleLabel5 = new IGK.DrSStudio.WinUI.IGKXRuleLabel();
            this.xRuleLabel6 = new IGK.DrSStudio.WinUI.IGKXRuleLabel();
            this.xLabel1 = new IGK.DrSStudio.WinUI.IGKXLabel();
            this.xLabel2 = new IGK.DrSStudio.WinUI.IGKXLabel();
            this.c_xnumtxb_scalex = new IGK.DrSStudio.WinUI.IGKXNumericTextBox();
            this.c_xnumtxb_scaley = new IGK.DrSStudio.WinUI.IGKXNumericTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.c_num_PortionAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c_numoffsetAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c_num_orientationAngle)).BeginInit();
            this.SuspendLayout();
            // 
            // txb_text
            // 
            this.txb_text.Location = new System.Drawing.Point(6, 270);
            this.txb_text.Name = "txb_text";
            this.txb_text.Size = new System.Drawing.Size(341, 20);
            this.txb_text.TabIndex = 0;
            // 
            // xRuleLabel1
            // 
            this.xRuleLabel1.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.xRuleLabel1.CaptionKey = "lb.text.caption";
            this.xRuleLabel1.Location = new System.Drawing.Point(6, 243);
            this.xRuleLabel1.Name = "xRuleLabel1";
            this.xRuleLabel1.Size = new System.Drawing.Size(341, 21);
            this.xRuleLabel1.TabIndex = 1;
            this.xRuleLabel1.TabStop = false;
            // 
            // xRuleLabel2
            // 
            this.xRuleLabel2.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.xRuleLabel2.CaptionKey = "lb.PortionAngle.caption";
            this.xRuleLabel2.Location = new System.Drawing.Point(6, 6);
            this.xRuleLabel2.Name = "xRuleLabel2";
            this.xRuleLabel2.Size = new System.Drawing.Size(341, 21);
            this.xRuleLabel2.TabIndex = 2;
            this.xRuleLabel2.TabStop = false;
            // 
            // c_num_PortionAngle
            // 
            this.c_num_PortionAngle.Location = new System.Drawing.Point(6, 24);
            this.c_num_PortionAngle.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.c_num_PortionAngle.Name = "c_num_PortionAngle";
            this.c_num_PortionAngle.Size = new System.Drawing.Size(149, 20);
            this.c_num_PortionAngle.TabIndex = 3;
            // 
            // xRuleLabel3
            // 
            this.xRuleLabel3.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.xRuleLabel3.CaptionKey = "lb.orientation.caption";
            this.xRuleLabel3.Location = new System.Drawing.Point(6, 144);
            this.xRuleLabel3.Name = "xRuleLabel3";
            this.xRuleLabel3.Size = new System.Drawing.Size(341, 21);
            this.xRuleLabel3.TabIndex = 4;
            this.xRuleLabel3.TabStop = false;
            // 
            // c_cmb_orientation
            // 
            this.c_cmb_orientation.FormattingEnabled = true;
            this.c_cmb_orientation.Location = new System.Drawing.Point(6, 166);
            this.c_cmb_orientation.Name = "c_cmb_orientation";
            this.c_cmb_orientation.Size = new System.Drawing.Size(149, 21);
            this.c_cmb_orientation.TabIndex = 5;
            // 
            // c_numoffsetAngle
            // 
            this.c_numoffsetAngle.Location = new System.Drawing.Point(6, 68);
            this.c_numoffsetAngle.Maximum = new decimal(new int[] {
            359,
            0,
            0,
            0});
            this.c_numoffsetAngle.Name = "c_numoffsetAngle";
            this.c_numoffsetAngle.Size = new System.Drawing.Size(149, 20);
            this.c_numoffsetAngle.TabIndex = 7;
            // 
            // xRuleLabel4
            // 
            this.xRuleLabel4.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.xRuleLabel4.CaptionKey = "lb.OffsetAngle.caption";
            this.xRuleLabel4.Location = new System.Drawing.Point(6, 50);
            this.xRuleLabel4.Name = "xRuleLabel4";
            this.xRuleLabel4.Size = new System.Drawing.Size(341, 21);
            this.xRuleLabel4.TabIndex = 6;
            this.xRuleLabel4.TabStop = false;
            // 
            // c_num_orientationAngle
            // 
            this.c_num_orientationAngle.Location = new System.Drawing.Point(6, 112);
            this.c_num_orientationAngle.Maximum = new decimal(new int[] {
            359,
            0,
            0,
            0});
            this.c_num_orientationAngle.Name = "c_num_orientationAngle";
            this.c_num_orientationAngle.Size = new System.Drawing.Size(149, 20);
            this.c_num_orientationAngle.TabIndex = 9;
            // 
            // xRuleLabel5
            // 
            this.xRuleLabel5.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.xRuleLabel5.CaptionKey = "lb.OrientationAngle.caption";
            this.xRuleLabel5.Location = new System.Drawing.Point(6, 94);
            this.xRuleLabel5.Name = "xRuleLabel5";
            this.xRuleLabel5.Size = new System.Drawing.Size(341, 21);
            this.xRuleLabel5.TabIndex = 8;
            this.xRuleLabel5.TabStop = false;
            // 
            // xRuleLabel6
            // 
            this.xRuleLabel6.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.xRuleLabel6.CaptionKey = "lb.scale.caption";
            this.xRuleLabel6.Location = new System.Drawing.Point(6, 193);
            this.xRuleLabel6.Name = "xRuleLabel6";
            this.xRuleLabel6.Size = new System.Drawing.Size(341, 21);
            this.xRuleLabel6.TabIndex = 10;
            this.xRuleLabel6.TabStop = false;
            // 
            // xLabel1
            // 
            this.xLabel1.CaptionKey = "x:";
            this.xLabel1.Location = new System.Drawing.Point(6, 217);
            this.xLabel1.Name = "xLabel1";
            this.xLabel1.Size = new System.Drawing.Size(32, 23);
            this.xLabel1.TabIndex = 11;
            // 
            // xLabel2
            // 
            this.xLabel2.CaptionKey = "y:";
            this.xLabel2.Location = new System.Drawing.Point(123, 214);
            this.xLabel2.Name = "xLabel2";
            this.xLabel2.Size = new System.Drawing.Size(32, 23);
            this.xLabel2.TabIndex = 12;
            // 
            // c_xnumtxb_scalex
            // 
            this.c_xnumtxb_scalex.Location = new System.Drawing.Point(44, 217);
            this.c_xnumtxb_scalex.Name = "c_xnumtxb_scalex";
            this.c_xnumtxb_scalex.Size = new System.Drawing.Size(61, 20);
            this.c_xnumtxb_scalex.TabIndex = 13;
            this.c_xnumtxb_scalex.Text = "1";
            this.c_xnumtxb_scalex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.c_xnumtxb_scalex.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // c_xnumtxb_scaley
            // 
            this.c_xnumtxb_scaley.Location = new System.Drawing.Point(161, 217);
            this.c_xnumtxb_scaley.Name = "c_xnumtxb_scaley";
            this.c_xnumtxb_scaley.Size = new System.Drawing.Size(61, 20);
            this.c_xnumtxb_scaley.TabIndex = 14;
            this.c_xnumtxb_scaley.Text = "1";
            this.c_xnumtxb_scaley.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.c_xnumtxb_scaley.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // CircleTextEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.c_xnumtxb_scaley);
            this.Controls.Add(this.c_xnumtxb_scalex);
            this.Controls.Add(this.xLabel2);
            this.Controls.Add(this.xLabel1);
            this.Controls.Add(this.xRuleLabel6);
            this.Controls.Add(this.c_num_orientationAngle);
            this.Controls.Add(this.xRuleLabel5);
            this.Controls.Add(this.c_numoffsetAngle);
            this.Controls.Add(this.xRuleLabel4);
            this.Controls.Add(this.c_cmb_orientation);
            this.Controls.Add(this.xRuleLabel3);
            this.Controls.Add(this.c_num_PortionAngle);
            this.Controls.Add(this.xRuleLabel2);
            this.Controls.Add(this.xRuleLabel1);
            this.Controls.Add(this.txb_text);
            this.Name = "CircleTextEditor";
            this.Size = new System.Drawing.Size(353, 303);
            ((System.ComponentModel.ISupportInitialize)(this.c_num_PortionAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c_numoffsetAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c_num_orientationAngle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
        private System.Windows.Forms.TextBox txb_text;
        private IGK.DrSStudio.WinUI.IGKXRuleLabel xRuleLabel1;
        private IGK.DrSStudio.WinUI.IGKXRuleLabel xRuleLabel2;
        private System.Windows.Forms.NumericUpDown c_num_PortionAngle;
        private IGK.DrSStudio.WinUI.IGKXRuleLabel xRuleLabel3;
        private IGK.DrSStudio.WinUI.IGKXComboBox c_cmb_orientation;
        private System.Windows.Forms.NumericUpDown c_numoffsetAngle;
        private IGK.DrSStudio.WinUI.IGKXRuleLabel xRuleLabel4;
        private System.Windows.Forms.NumericUpDown c_num_orientationAngle;
        private IGK.DrSStudio.WinUI.IGKXRuleLabel xRuleLabel5;
        private IGK.DrSStudio.WinUI.IGKXRuleLabel xRuleLabel6;
        private IGK.DrSStudio.WinUI.IGKXLabel xLabel1;
        private IGK.DrSStudio.WinUI.IGKXLabel xLabel2;
        private IGK.DrSStudio.WinUI.IGKXNumericTextBox c_xnumtxb_scalex;
        private IGK.DrSStudio.WinUI.IGKXNumericTextBox c_xnumtxb_scaley;
    }
}

