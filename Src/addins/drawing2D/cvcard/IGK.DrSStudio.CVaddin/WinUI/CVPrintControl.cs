

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CVPrintControl.cs
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
file:CVPrintControl.cs
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
﻿
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
﻿using System;
﻿using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
namespace IGK.DrSStudio.WinUI
{
    
using IGK.ICore.WinUI;
    using IGK.ICore;
    using IGK.ICore.Codec;
 using IGK.ICore.Drawing2D; using IGK.DrSStudio.Drawing2D;
    using IGK.ICore.Menu;
    using IGK.ICore.Tools;
    using IGK.DrSStudio.Printing;
    using IGK.ICore.WinCore.WinUI.Controls;
    public class CVPrintControl : UIXConfigControlBase
    {
        PrinterSettings m_PrinterSetting;
        PrintDocument m_printDocument;
        private IGKXRuleLabel xRuleLabel1;
        private IGKXPanel xPanel1;
        private IGKXCheckBox xCheckBox1;
        private PrintPreviewControl printPrevControl;
        private IGKXCheckBox chb_drawgrid;
        private IGKXLabel xLabel1;
        private IGKXComboBox cmb_paper;
        private IGKXRuleLabel xRuleLabel2;
        private IGKXButton btn_preference;
        CVDocument m_document;
        public CVDocument DocumentToPrint
        {
            get
            {
                return this.m_document;
            }
        }
        public CVPrintControl(CVDocument document)
        {
            if (document == null)
                throw new CoreException("document");
            this.m_document = document;
            InitializeComponent();
        }
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
            if (disposing)
            {
                if (this.m_printDocument != null)
                {
                    this.m_printDocument.Dispose();
                    this.m_printDocument = null;
                }
            }
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
            this.bt_print = new IGKXButton();
            this.label1 = new IGKXLabel();
            this.cmb_printer = new System.Windows.Forms.ComboBox();
            this.bt_cancel = new IGKXButton();
            this.xRuleLabel1 = new IGKXRuleLabel();
            this.xPanel1 = new IGKXPanel();
            this.xCheckBox1 = new IGKXCheckBox();
            this.printPrevControl = new System.Windows.Forms.PrintPreviewControl();
            this.chb_drawgrid = new IGKXCheckBox();
            this.xLabel1 = new IGKXLabel();
            this.cmb_paper = new IGKXComboBox();
            this.xRuleLabel2 = new IGKXRuleLabel();
            this.btn_preference = new IGKXButton();
            this.xPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bt_print
            // 
            this.bt_print.CaptionKey = CoreConstant.BTN_OK;
            this.bt_print.DialogResult = enuDialogResult.OK;
            this.bt_print.Location = new System.Drawing.Point(187, 17);
            this.bt_print.Name = "bt_print";
            this.bt_print.Size = new System.Drawing.Size(75, 26);
            this.bt_print.TabIndex = 0;
            this.bt_print.Click += new System.EventHandler(this.bt_print_Click);
            // 
            // label1
            // 
            this.label1.CaptionKey = "lb.printer.caption";
            this.label1.Location = new System.Drawing.Point(17, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 21);
            this.label1.TabIndex = 1;
            // 
            // cmb_printer
            // 
            this.cmb_printer.FormattingEnabled = true;
            this.cmb_printer.Location = new System.Drawing.Point(138, 31);
            this.cmb_printer.Name = "cmb_printer";
            this.cmb_printer.Size = new System.Drawing.Size(206, 21);
            this.cmb_printer.TabIndex = 11;
            this.cmb_printer.Text = "choose your Printer";
            // 
            // bt_cancel
            // 
            this.bt_cancel.CaptionKey = CoreConstant.BTN_CANCEL;
            this.bt_cancel.DialogResult = enuDialogResult.Cancel;
            this.bt_cancel.Location = new System.Drawing.Point(268, 17);
            this.bt_cancel.Name = "bt_cancel";
            this.bt_cancel.Size = new System.Drawing.Size(75, 26);
            this.bt_cancel.TabIndex = 14;
            // 
            // xRuleLabel1
            // 
            this.xRuleLabel1.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel1.CaptionKey = null;
            this.xRuleLabel1.Location = new System.Drawing.Point(17, 21);
            this.xRuleLabel1.Name = "xRuleLabel1";
            this.xRuleLabel1.Size = new System.Drawing.Size(324, 23);
            this.xRuleLabel1.TabIndex = 16;
            this.xRuleLabel1.TabStop = false;
            // 
            // xPanel1
            // 
            this.xPanel1.BackColor = System.Drawing.Color.Gray;
            this.xPanel1.Controls.Add(this.bt_print);
            this.xPanel1.Controls.Add(this.bt_cancel);
            this.xPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xPanel1.ForeColor = System.Drawing.Color.White;
            this.xPanel1.Location = new System.Drawing.Point(0, 448);
            this.xPanel1.Name = "xPanel1";
            this.xPanel1.Size = new System.Drawing.Size(351, 46);
            this.xPanel1.TabIndex = 17;
            // 
            // xCheckBox1
            // 
            this.xCheckBox1.AutoSize = true;
            this.xCheckBox1.CaptionKey = "chb.allowVerticalSpace.caption";
            this.xCheckBox1.Location = new System.Drawing.Point(138, 128);
            this.xCheckBox1.Name = "xCheckBox1";
            this.xCheckBox1.Size = new System.Drawing.Size(175, 17);
            this.xCheckBox1.TabIndex = 18;
            this.xCheckBox1.CheckedChanged += new System.EventHandler(this.xCheckBox1_CheckedChanged);
            // 
            // printPrevControl
            // 
            this.printPrevControl.Location = new System.Drawing.Point(17, 243);
            this.printPrevControl.Name = "printPrevControl";
            this.printPrevControl.Size = new System.Drawing.Size(324, 199);
            this.printPrevControl.TabIndex = 19;
            // 
            // chb_drawgrid
            // 
            this.chb_drawgrid.AutoSize = true;
            this.chb_drawgrid.CaptionKey = "chb.drawCVgrid.caption";
            this.chb_drawgrid.Location = new System.Drawing.Point(138, 151);
            this.chb_drawgrid.Name = "chb_drawgrid";
            this.chb_drawgrid.Size = new System.Drawing.Size(139, 17);
            this.chb_drawgrid.TabIndex = 20;
            this.chb_drawgrid.CheckedChanged += new System.EventHandler(this.chb_drawgrid_CheckedChanged);
            // 
            // xLabel1
            // 
            this.xLabel1.CaptionKey = "lb.paper.caption";
            this.xLabel1.Location = new System.Drawing.Point(17, 58);
            this.xLabel1.Name = "xLabel1";
            this.xLabel1.Size = new System.Drawing.Size(115, 21);
            this.xLabel1.TabIndex = 21;
            // 
            // cmb_paper
            // 
            this.cmb_paper.FormattingEnabled = true;
            this.cmb_paper.Location = new System.Drawing.Point(138, 58);
            this.cmb_paper.Name = "cmb_paper";
            this.cmb_paper.Size = new System.Drawing.Size(206, 21);
            this.cmb_paper.TabIndex = 22;
            this.cmb_paper.Text = "choose paper";
            // 
            // xRuleLabel2
            // 
            this.xRuleLabel2.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel2.CaptionKey = null;
            this.xRuleLabel2.Location = new System.Drawing.Point(17, 214);
            this.xRuleLabel2.Name = "xRuleLabel2";
            this.xRuleLabel2.Size = new System.Drawing.Size(324, 23);
            this.xRuleLabel2.TabIndex = 23;
            this.xRuleLabel2.TabStop = false;
            // 
            // btn_preference
            // 
            this.btn_preference.CaptionKey = "btn.preference.caption";
            this.btn_preference.DialogResult = enuDialogResult.OK;
            this.btn_preference.Location = new System.Drawing.Point(138, 85);
            this.btn_preference.Name = "btn_preference";
            this.btn_preference.Size = new System.Drawing.Size(75, 26);
            this.btn_preference.TabIndex = 24;
            this.btn_preference.Click += new System.EventHandler(this.btn_preference_Click);
            // 
            // CVPrintControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_preference);
            this.Controls.Add(this.xRuleLabel2);
            this.Controls.Add(this.xLabel1);
            this.Controls.Add(this.cmb_paper);
            this.Controls.Add(this.chb_drawgrid);
            this.Controls.Add(this.printPrevControl);
            this.Controls.Add(this.xCheckBox1);
            this.Controls.Add(this.xPanel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmb_printer);
            this.Controls.Add(this.xRuleLabel1);
            this.Name = "CVPrintControl";
            this.Size = new System.Drawing.Size(351, 494);
            this.Load += new System.EventHandler(this.PrintControlDialog_Load);
            this.xPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
        private IGKXButton bt_print;
        private IGKXLabel label1;
        private System.Windows.Forms.ComboBox cmb_printer;
        private IGKXButton bt_cancel;
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
        }
        private void PrintControlDialog_Load(object sender, EventArgs e)
        {
            this.m_PrinterSetting = new PrinterSettings();
            this.m_printDocument = new PrintDocument();
            this.cmb_printer.Items.Clear();
            foreach (string st in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                this.cmb_printer.Items.Add(st);
            }
            this.cmb_printer.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);
            this.cmb_paper.SelectedIndexChanged += new EventHandler(cmb_paper_SelectedIndexChanged);
            this.cmb_printer.Text = this.m_printDocument.PrinterSettings.PrinterName;
            this.printPrevControl.Document = this.m_printDocument;
            this.xCheckBox1.Checked = CoreCV.AllowVerticalSpace;
            this.chb_drawgrid.Checked = CoreCV.DrawGridLine;
            this.cmb_paper.Text = this.m_printDocument.PrinterSettings.DefaultPageSettings.PaperSize.PaperName;
            CoreCV.PrintDocument(this.m_printDocument, DocumentToPrint, true);
            UpdatePaperSize();
        }
        private void UpdatePaperSize()
        {
            this.cmb_paper.Items.Clear();
            foreach (PaperSize s in this.m_printDocument.PrinterSettings.PaperSizes)
            {
                this.cmb_paper.Items.Add(s.PaperName);
            }
            this.cmb_paper.SelectedItem = this.m_printDocument.DefaultPageSettings.PaperSize.PaperName;
        }
        void cmb_paper_SelectedIndexChanged(object sender, EventArgs e)
        {
            //paper type changed
            if (this.cmb_paper.SelectedIndex >= 0)
            {
                this.printPrevControl.Document.DefaultPageSettings.PaperSize =
                    this.m_printDocument.PrinterSettings.PaperSizes[this.cmb_paper.SelectedIndex];
            }
            if (this.printPrevControl.Document != null)
            {
                this.printPrevControl.InvalidatePreview();
            }
        }
        void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmb_printer.SelectedItem == null)
                return;
            this.m_PrinterSetting.PrinterName = this.cmb_printer.SelectedItem.ToString();
            if (this.printPrevControl.Document != null)
            {
                this.printPrevControl.Document.PrinterSettings = this.m_PrinterSetting;
                this.printPrevControl.InvalidatePreview();
            }
        }
        private void xCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            CoreCV.AllowVerticalSpace = this.xCheckBox1.Checked;
            this.printPrevControl.InvalidatePreview();
        }
        private void chb_drawgrid_CheckedChanged(object sender, EventArgs e)
        {
            CoreCV.DrawGridLine = this.chb_drawgrid.Checked;
            this.printPrevControl.InvalidatePreview();
        }
        private void bt_print_Click(object sender, EventArgs e)
        {
            this.printPrevControl.Document.Print();
        }
        private void btn_preference_Click(object sender, EventArgs e)
        {
            Form frm = this.FindForm();
            WinCorePrintControl.ShowPrintSettingDialog (
            CoreSystem.Instance.MainForm.Handle ,
                this.printPrevControl.Document.PrinterSettings);
            if (frm != null)
                frm.DialogResult = DialogResult.None;
            this.printPrevControl.InvalidatePreview();
        }
    }
}

