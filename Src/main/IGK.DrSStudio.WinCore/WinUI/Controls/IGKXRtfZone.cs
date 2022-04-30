

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXRtfZone.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.WinUI;
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
    /// <summary>
    /// represent a base rtf zone
    /// </summary>
    public class IGKXRtfZone : IGKXUserControl,  ICoreRtfControl
    {
        private IGKXToolStrip xToolStrip1;
        private ToolStripButton c_btn_bold;
        private ToolStripButton c_btn_underline;
        private ToolStripButton c_btn_italic;
        private ToolStripButton c_btn_selectFont;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        RichTextBox  c_rtf;
        
        protected override System.Drawing.Size DefaultSize
        {
            get
            {
                return new System.Drawing.Size(300, 200);
            }
        }


        public IGKXRtfZone()
        {
            this.InitializeComponent();
            //this.SizeChanged += IGKXRtfZone_SizeChanged;
            //this.c_rtf.Bounds = this.ClientRectangle;
            this.c_rtf.TextChanged += c_rtf_TextChanged;
          
            
        }

        void c_rtf_TextChanged(object sender, EventArgs e)
        {
            OnTextChanged(EventArgs.Empty);
        }

        void IGKXRtfZone_SizeChanged(object sender, EventArgs e)
        {
            this.c_rtf.Bounds = this.ClientRectangle;
        }

        public string Rtf
        {
            get
            {
                return this.c_rtf.Rtf;
            }
            set
            {
                this.c_rtf.Rtf  = value;
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IGKXRtfZone));
            this.xToolStrip1 = new IGKXToolStrip();
            this.c_btn_bold = new System.Windows.Forms.ToolStripButton();
            this.c_btn_underline = new System.Windows.Forms.ToolStripButton();
            this.c_btn_italic = new System.Windows.Forms.ToolStripButton();
            this.c_rtf = new System.Windows.Forms.RichTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.c_btn_selectFont = new System.Windows.Forms.ToolStripButton();
            this.xToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // xToolStrip1
            // 
            this.xToolStrip1.BackgroundDocument = null;
            this.xToolStrip1.CaptionKey = null;
            this.xToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.c_btn_selectFont,
            this.toolStripSeparator1,
            this.c_btn_bold,
            this.c_btn_underline,
            this.c_btn_italic,
            this.toolStripSeparator2});
            this.xToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.xToolStrip1.Name = "xToolStrip1";
            this.xToolStrip1.Size = new System.Drawing.Size(356, 25);
            this.xToolStrip1.TabIndex = 0;
            this.xToolStrip1.Text = "xToolStrip1";
            // 
            // c_btn_bold
            // 
            this.c_btn_bold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.c_btn_bold.Image = ((System.Drawing.Image)(resources.GetObject("c_btn_bold.Image")));
            this.c_btn_bold.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c_btn_bold.Name = "c_btn_bold";
            this.c_btn_bold.Size = new System.Drawing.Size(23, 22);
            this.c_btn_bold.Text = "toolStripButton1";
            this.c_btn_bold.Click += new System.EventHandler(this.c_btn_bold_Click);
            // 
            // c_btn_underline
            // 
            this.c_btn_underline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.c_btn_underline.Image = ((System.Drawing.Image)(resources.GetObject("c_btn_underline.Image")));
            this.c_btn_underline.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c_btn_underline.Name = "c_btn_underline";
            this.c_btn_underline.Size = new System.Drawing.Size(23, 22);
            this.c_btn_underline.Text = "toolStripButton2";
            this.c_btn_underline.Click += new System.EventHandler(this.c_btn_underline_Click);
            // 
            // c_btn_italic
            // 
            this.c_btn_italic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.c_btn_italic.Image = ((System.Drawing.Image)(resources.GetObject("c_btn_italic.Image")));
            this.c_btn_italic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c_btn_italic.Name = "c_btn_italic";
            this.c_btn_italic.Size = new System.Drawing.Size(23, 22);
            this.c_btn_italic.Text = "toolStripButton3";
            this.c_btn_italic.Click += new System.EventHandler(this.c_btn_italic_Click);
            // 
            // c_rtf
            // 
            this.c_rtf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_rtf.Location = new System.Drawing.Point(0, 25);
            this.c_rtf.Name = "c_rtf";
            this.c_rtf.Size = new System.Drawing.Size(356, 212);
            this.c_rtf.TabIndex = 0;
            this.c_rtf.Text = "";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // c_btn_selectFont
            // 
            this.c_btn_selectFont.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.c_btn_selectFont.Image = ((System.Drawing.Image)(resources.GetObject("c_btn_selectFont.Image")));
            this.c_btn_selectFont.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c_btn_selectFont.Name = "c_btn_selectFont";
            this.c_btn_selectFont.Size = new System.Drawing.Size(23, 22);
            this.c_btn_selectFont.Text = "toolStripButton1";
            this.c_btn_selectFont.Click += new System.EventHandler(this.c_btn_selectFont_Click);
            // 
            // IGKXRtfZone
            // 
            this.Controls.Add(this.c_rtf);
            this.Controls.Add(this.xToolStrip1);
            this.Name = "IGKXRtfZone";
            this.Size = new System.Drawing.Size(356, 237);
            this.xToolStrip1.ResumeLayout(false);
            this.xToolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void c_btn_bold_Click(object sender, EventArgs e)
        {
            ChangeStyle(FontStyle.Bold);

        }
        private void c_btn_underline_Click(object sender, EventArgs e)
        {
            ChangeStyle(FontStyle.Underline );
        }
        private void c_btn_italic_Click(object sender, EventArgs e)
        {
            ChangeStyle(FontStyle.Italic );
        }
        private void ChangeStyle(FontStyle fontStyle)
        {

            if (this.c_rtf.SelectionLength > 0)
            {
                var f = this.c_rtf.SelectionFont;
                try
                {
                    this.c_rtf.SelectionFont = new System.Drawing.Font(
                        f, System.Drawing.FontStyle.Bold);
                }
                catch
                {
                }
            }
        }

        private void c_btn_selectFont_Click(object sender, EventArgs e)
        {
            var q = CoreSystem.GetWorkbench();
            if (q != null)
            {
                using (var r = q.CreateFontDialog())
                {
                    if (r.ShowDialog() == enuDialogResult.OK)
                    { 
                        this.ChangeFont(r.Font) ;
                         
                    }
                }
            }
        }

        private void ChangeFont(ICoreFont coreFont)
        {
            if (this.c_rtf.SelectionLength > 0)
            {
                var f = this.c_rtf.SelectionFont;
                try
                {
                    this.c_rtf.SelectionFont = coreFont.ToGdiFont();
                }
                catch
                {
                }
            }
            else
                this.c_rtf.Font = coreFont.ToGdiFont();
        }

       

      
    }
}
