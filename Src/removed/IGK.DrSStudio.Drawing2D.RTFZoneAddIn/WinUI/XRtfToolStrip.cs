

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XRtfToolStrip.cs
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
file:XRtfToolStrip.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
namespace IGK.DrSStudio.Drawing2D.WinUI
{
    class XRtfToolStrip : ToolStrip
    {
        public event EventHandler FontDefinitionChanged;
        private string m_FontName;
        private CoreUnit m_FontSize;
        private ToolStripLabel toolStripLabel1;
        private ToolStripComboBox c_fontName;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton c_alignl;
        private ToolStripButton c_alignc;
        private ToolStripButton c_alignr;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton c_color;
        private FontStyle m_FontStyle;
        public FontStyle FontStyle
        {
            get { return m_FontStyle; }
            set
            {
                if (m_FontStyle != value)
                {
                    m_FontStyle = value;
                }
            }
        }
        public CoreUnit  FontSize
        {
            get { return m_FontSize; }
        }
        public string FontName
        {
            get { return m_FontName; }
            set
            {
                if (m_FontName != value)
                {
                    m_FontName = value;
                }
            }
        }
        protected virtual void OnFontDefinitionChanged(EventArgs e)
        {
            if (this.FontDefinitionChanged != null)
                    this.FontDefinitionChanged(this, e);    
        }
        public XRtfToolStrip()
        {
            this.InitializeComponent();
            this.InitToolSetting();
            this.m_FontSize = "0px";
        }
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XRtfToolStrip));
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.c_fontName = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.c_alignl = new System.Windows.Forms.ToolStripButton();
            this.c_alignc = new System.Windows.Forms.ToolStripButton();
            this.c_alignr = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.c_color = new System.Windows.Forms.ToolStripButton();
            this.SuspendLayout();
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(86, 15);
            this.toolStripLabel1.Text = "toolStripLabel1";
            // 
            // c_fontName
            // 
            this.c_fontName.Name = "c_fontName";
            this.c_fontName.Size = new System.Drawing.Size(121, 23);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 6);
            // 
            // c_alignl
            // 
            this.c_alignl.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.c_alignl.Image = ((System.Drawing.Image)(resources.GetObject("c_alignl.Image")));
            this.c_alignl.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c_alignl.Name = "c_alignl";
            this.c_alignl.Size = new System.Drawing.Size(23, 20);
            this.c_alignl.Text = "toolStripButton1";
            // 
            // c_alignc
            // 
            this.c_alignc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.c_alignc.Image = ((System.Drawing.Image)(resources.GetObject("c_alignc.Image")));
            this.c_alignc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c_alignc.Name = "c_alignc";
            this.c_alignc.Size = new System.Drawing.Size(23, 20);
            this.c_alignc.Text = "toolStripButton2";
            // 
            // c_alignr
            // 
            this.c_alignr.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.c_alignr.Image = ((System.Drawing.Image)(resources.GetObject("c_alignr.Image")));
            this.c_alignr.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c_alignr.Name = "c_alignr";
            this.c_alignr.Size = new System.Drawing.Size(23, 20);
            this.c_alignr.Text = "toolStripButton3";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 6);
            // 
            // c_color
            // 
            this.c_color.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.c_color.Image = ((System.Drawing.Image)(resources.GetObject("c_color.Image")));
            this.c_color.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c_color.Name = "c_color";
            this.c_color.Size = new System.Drawing.Size(23, 20);
            this.c_color.Text = "toolStripButton4";
            // 
            // XRtfToolStrip
            // 
            this.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.c_fontName,
            this.toolStripSeparator1,
            this.c_alignl,
            this.c_alignc,
            this.c_alignr,
            this.toolStripSeparator2,
            this.c_color});
            this.ResumeLayout(false);
        }
        private void InitToolSetting()
        {
            IGK.DrSStudio.Settings.ICoreSetting setting;
            setting = IGK.DrSStudio.CoreSystem.Instance.Settings["FontProperties"];
            //if (setting != null)
            //{
            //    this.FontComboBox.SelectedText = setting["DefaultFontName"].Value as string;
            //    this.SizeComboBox.Items.AddRange((setting["DefaultFontSizes"].Value as string[]));
            //    this.SizeComboBox.Text = setting["DefaultFontSize"].Value as string;
            //}
        }
    }
}

