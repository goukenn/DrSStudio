

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: UIPreviewDocument.cs
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
file:UIPreviewDocument.cs
*/
using IGK.ICore;using IGK.DrSStudio.WinUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D.WinUI
{
    class UIPreviewDocument : XUserControl
    {
        private XPanel xPanel2;
        private IGKXLabel xLabel2;
        private XTextBox c_height;
        private XTextBox c_width;
        private IGKXLabel xLabel1;
        private IGKXButton c_btn_preview;
        private System.Windows.Forms.PictureBox pictureBox1;
        private IGKXButton xButton1;
        private XPanel xPanel1;
        public int PreviewWidth { get; set; }
        public int PreviewHeight { get; set; }
        public ICore2DDrawingDocument CurrentDocument { get; set; }
        public UIPreviewDocument()
        {
            this.InitializeComponent();
        }
        private void InitializeComponent()
        {
            this.xPanel1 = new IGK.DrSStudio.WinUI.XPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.xPanel2 = new IGK.DrSStudio.WinUI.XPanel();
            this.xButton1 = new IGK.DrSStudio.WinUI.IGKXButton();
            this.c_btn_preview = new IGK.DrSStudio.WinUI.IGKXButton();
            this.xLabel2 = new IGK.DrSStudio.WinUI.IGKXLabel();
            this.c_height = new IGK.DrSStudio.WinUI.XTextBox();
            this.c_width = new IGK.DrSStudio.WinUI.XTextBox();
            this.xLabel1 = new IGK.DrSStudio.WinUI.IGKXLabel();
            this.xPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.xPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // xPanel1
            // 
            this.xPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.xPanel1.Controls.Add(this.pictureBox1);
            this.xPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xPanel1.Location = new System.Drawing.Point(0, 47);
            this.xPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.xPanel1.Name = "xPanel1";
            this.xPanel1.Size = new System.Drawing.Size(585, 274);
            this.xPanel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(581, 270);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // xPanel2
            // 
            this.xPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.xPanel2.Controls.Add(this.xButton1);
            this.xPanel2.Controls.Add(this.c_btn_preview);
            this.xPanel2.Controls.Add(this.xLabel2);
            this.xPanel2.Controls.Add(this.c_height);
            this.xPanel2.Controls.Add(this.c_width);
            this.xPanel2.Controls.Add(this.xLabel1);
            this.xPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.xPanel2.Location = new System.Drawing.Point(0, 0);
            this.xPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.xPanel2.Name = "xPanel2";
            this.xPanel2.Size = new System.Drawing.Size(585, 47);
            this.xPanel2.TabIndex = 0;
            // 
            // xButton1
            // 
            this.xButton1.CaptionKey = "btn.configure.caption";
            this.xButton1.Checked = false;
            this.xButton1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.xButton1.Location = new System.Drawing.Point(494, 10);
            this.xButton1.Name = "xButton1";
            this.xButton1.ShowButtonImage = false;
            this.xButton1.Size = new System.Drawing.Size(86, 23);
            this.xButton1.State = IGK.DrSStudio.WinUI.enuButtonState.Normal;
            this.xButton1.TabIndex = 5;
            this.xButton1.Click += new System.EventHandler(this.xButton1_Click);
            // 
            // c_btn_preview
            // 
            this.c_btn_preview.CaptionKey = "btn.preview.caption";
            this.c_btn_preview.Checked = false;
            this.c_btn_preview.DialogResult = System.Windows.Forms.DialogResult.None;
            this.c_btn_preview.Location = new System.Drawing.Point(401, 10);
            this.c_btn_preview.Name = "c_btn_preview";
            this.c_btn_preview.ShowButtonImage = false;
            this.c_btn_preview.Size = new System.Drawing.Size(87, 23);
            this.c_btn_preview.State = IGK.DrSStudio.WinUI.enuButtonState.Normal;
            this.c_btn_preview.TabIndex = 4;
            this.c_btn_preview.Click += new System.EventHandler(this.c_btn_preview_Click);
            // 
            // xLabel2
            // 
            this.xLabel2.AutoSize = true;
            this.xLabel2.CaptionKey = "lb.height.caption";
            this.xLabel2.Location = new System.Drawing.Point(198, 20);
            this.xLabel2.Name = "xLabel2";
            this.xLabel2.Size = new System.Drawing.Size(85, 13);
            this.xLabel2.TabIndex = 3;
            // 
            // c_height
            // 
            this.c_height.Location = new System.Drawing.Point(294, 13);
            this.c_height.Name = "c_height";
            this.c_height.Size = new System.Drawing.Size(100, 20);
            this.c_height.TabIndex = 2;
            this.c_height.TextChanged += new System.EventHandler(this.c_height_TextChanged);
            // 
            // c_width
            // 
            this.c_width.Location = new System.Drawing.Point(92, 13);
            this.c_width.Name = "c_width";
            this.c_width.Size = new System.Drawing.Size(100, 20);
            this.c_width.TabIndex = 1;
            this.c_width.TextChanged += new System.EventHandler(this.c_text_TextChanged);
            // 
            // xLabel1
            // 
            this.xLabel1.AutoSize = true;
            this.xLabel1.CaptionKey = "lb.width.caption";
            this.xLabel1.Location = new System.Drawing.Point(5, 20);
            this.xLabel1.Name = "xLabel1";
            this.xLabel1.Size = new System.Drawing.Size(81, 13);
            this.xLabel1.TabIndex = 0;
            // 
            // UIPreviewDocument
            // 
            this.Controls.Add(this.xPanel1);
            this.Controls.Add(this.xPanel2);
            this.Name = "UIPreviewDocument";
            this.Size = new System.Drawing.Size(585, 321);
            this.xPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.xPanel2.ResumeLayout(false);
            this.xPanel2.PerformLayout();
            this.ResumeLayout(false);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.c_height.Text = this.PreviewHeight.ToString ();
            this.c_width.Text = this.PreviewWidth.ToString ();
            PreviewDocument();
        }
        private void PreviewDocument()
        {
            if (this.pictureBox1.Image != null)
                this.pictureBox1.Image.Dispose();
            if ((this.CurrentDocument == null) || (this.PreviewWidth <= 0) || (this.PreviewHeight <= 0))
                return;
            Bitmap bmp = new Bitmap(this.PreviewWidth, this.PreviewHeight);
            Graphics g = Graphics.FromImage(bmp);
            this.CurrentDocument.Draw(g, new Rectanglei(0, 0, bmp.Width, bmp.Height));
            g.Flush();
            g.Dispose();
            this.pictureBox1.Image = bmp;
        }
        private void c_btn_preview_Click(object sender, EventArgs e)
        {
            this.PreviewDocument();
        }
        private void c_text_TextChanged(object sender, EventArgs e)
        {
            this.PreviewWidth = (int)Math.Ceiling (((CoreUnit)this.c_width.Text).GetValue(enuUnitType.px));
        }
        private void c_height_TextChanged(object sender, EventArgs e)
        {
            this.PreviewHeight = (int)Math.Ceiling(((CoreUnit)this.c_height.Text).GetValue(enuUnitType.px));
        }
        private void xButton1_Click(object sender, EventArgs e)
        {
            if (this.CurrentDocument != null)
            {
                Workbench.ConfigureWorkingObject(this.CurrentDocument);
                this.PreviewDocument();
            }
        }
    }
}

