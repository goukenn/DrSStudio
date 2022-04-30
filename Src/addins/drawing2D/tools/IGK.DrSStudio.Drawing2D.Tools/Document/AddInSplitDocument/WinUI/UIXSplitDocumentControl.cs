

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: UIXSplitDocumentControl.cs
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
using IGK.DrSStudio.Drawing2D.Menu;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:UIXSplitterControl.cs
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
using System; using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
namespace IGK.DrSStudio.Drawing2D.WinUI
{
    using IGK.ICore.GraphicModels;
    using IGK.ICore.WinCore.WinUI.Controls;
    /// <summary>
    /// represent control to split image
    /// </summary>
    class UIXSplitDocumentControl : 
        UIXConfigControlBase ,
        ISplitDocument 
    {
        int m_row;
        int m_col;
        private IGKXLabel xLabel1;
        private IGKXRuleLabel xRuleLabel1;
        private IGKXButton btn_cancel;
        private IGKXButton btn_ok;
        private IGKXLabel xLabel2;
        private IGKXTextBox txb_row;
        private IGKXTextBox txb_col;
        private IGKXButton btn_browse;
        private IGKXTextBox txb_folder;
        private IGKXRuleLabel lb_outdir;
        private bool m_configure;
        private IGKXLabel lb_codec;
        private IGKXComboBox cmb_codec;
        private ICore2DDrawingDocument  m_document;
        private IGKXRuleLabel xRuleLabel2;
        private IGKXCheckBox chb_webfile;
        private ImageFormat m_imageFormat;
        private ICore2DDrawingSurface  m_surface;
        private string SelectedFolder {
            get {
                return txb_folder.Text;
            }
        }
        public int Row {
            get { return m_row; }
            set { m_row = value; }
        }
        public int Col
        {
            get { return m_col; }
            set { m_col = value; }
        }
        internal UIXSplitDocumentControl(ICore2DDrawingDocument document) 
        {
            this.m_document = document;
            InitializeComponent();
            InitControl();
            this.CaptionKey = "Menu.Tools.SplitDocumentImage";
        }
        private void InitControl()
        {
            this.m_configure = true;
            this.txb_folder.Text = Environment.CurrentDirectory;
            this.AcceptButton = btn_ok;
            this.CancelButton = btn_cancel;
            Row = 2;
            Col = 2;
            this.txb_row.Text = Row.ToString();
            this.txb_col.Text = Col.ToString();
            this.cmb_codec.Items.Add(".bmp");
            this.cmb_codec.Items.Add(".png");
            this.cmb_codec.Items.Add(".jpg");
            this.cmb_codec.SelectedItem = ".bmp";
            this.m_imageFormat = ImageFormat.Bmp;
            this.btn_ok.CaptionKey = CoreConstant.BTN_OK;
            this.btn_cancel.CaptionKey = CoreConstant.BTN_CANCEL;
            this.m_configure = false;
            m_surface = this.m_document.Parent as ICore2DDrawingSurface ;
            if (m_surface == null)
            {
                
                    this.m_surface = CoreSystem.GetWorkbench().CurrentSurface  as ICore2DDrawingSurface ;
            }
            if (m_surface != null)
            {
                m_surface.Paint +=m_surface_Paint;
                m_surface.Invalidate();
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                if (m_surface != null)
                {
                    m_surface.Paint -= m_surface_Paint;
                    m_surface.Invalidate();
                }
            }
            base.Dispose(disposing);
        }
        void m_surface_Paint(object sender, CorePaintEventArgs e)
        {
            PaintGrid(e.Graphics);
        }
        private void InitializeComponent()
        {
            this.xLabel1 = new IGKXLabel();
            this.xRuleLabel1 = new IGKXRuleLabel();
            this.btn_cancel = new IGKXButton();
            this.btn_ok = new IGKXButton();
            this.xLabel2 = new IGKXLabel();
            this.txb_row = new IGKXTextBox();
            this.txb_col = new IGKXTextBox();
            this.btn_browse = new IGKXButton();
            this.txb_folder = new IGKXTextBox();
            this.lb_outdir = new IGKXRuleLabel();
            this.lb_codec = new IGKXLabel();
            this.cmb_codec = new IGKXComboBox();
            this.xRuleLabel2 = new IGKXRuleLabel();
            this.chb_webfile = new IGKXCheckBox();
            this.SuspendLayout();
            // 
            // xLabel1
            // 
            this.xLabel1.AutoSize = true;
            this.xLabel1.BackColor = System.Drawing.Color.Transparent;
            this.xLabel1.CaptionKey = "lb.row.caption";
            this.xLabel1.Location = new System.Drawing.Point(3, 12);
            this.xLabel1.Name = "xLabel1";
            this.xLabel1.Size = new System.Drawing.Size(73, 13);
            this.xLabel1.TabIndex = 0;
            // 
            // xRuleLabel1
            // 
            this.xRuleLabel1.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.xRuleLabel1.CaptionKey = null;
            this.xRuleLabel1.Location = new System.Drawing.Point(3, 206);
            this.xRuleLabel1.Name = "xRuleLabel1";
            this.xRuleLabel1.Size = new System.Drawing.Size(294, 10);
            this.xRuleLabel1.TabIndex = 1;
            this.xRuleLabel1.TabStop = false;
            // 
            // btn_cancel
            // 
            this.btn_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));            
            this.btn_cancel.CaptionKey = CoreConstant.BTN_CANCEL;
            this.btn_cancel.DialogResult = enuDialogResult.Cancel;
            this.btn_cancel.Location = new System.Drawing.Point(222, 222);            
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 2;
            // 
            // btn_ok
            // 
            this.btn_ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ok.CaptionKey = CoreConstant.BTN_OK;
            this.btn_ok.DialogResult = enuDialogResult.OK;
            this.btn_ok.Location = new System.Drawing.Point(141, 222);
            this.btn_ok.Name = CoreConstant.BTN_OK;
            this.btn_ok.Size = new System.Drawing.Size(75, 23);
            this.btn_ok.TabIndex = 3;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // xLabel2
            // 
            this.xLabel2.AutoSize = true;            
            this.xLabel2.CaptionKey = "lb.col.caption";
            this.xLabel2.Location = new System.Drawing.Point(3, 35);
            this.xLabel2.Name = "xLabel2";
            this.xLabel2.Size = new System.Drawing.Size(70, 13);
            this.xLabel2.TabIndex = 4;
            // 
            // txb_row
            //             
            this.txb_row.Location = new System.Drawing.Point(82, 5);
            this.txb_row.Name = "txb_row";
            this.txb_row.Size = new System.Drawing.Size(82, 20);
            this.txb_row.TabIndex = 5;
            this.txb_row.Text = "2";
            this.txb_row.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txb_row.TextChanged += new System.EventHandler(this.txb_col_TextChanged);
            // 
            // txb_col
            // 
            this.txb_col.Location = new System.Drawing.Point(82, 31);
            this.txb_col.Name = "txb_col";
            this.txb_col.Size = new System.Drawing.Size(82, 20);
            this.txb_col.TabIndex = 6;
            this.txb_col.Text = "2";
            this.txb_col.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txb_col.TextChanged += new System.EventHandler(this.txb_col_TextChanged);
            // 
            // btn_browse
            // 
            this.btn_browse.BackColor = System.Drawing.Color.Transparent;
            this.btn_browse.CaptionKey = CoreConstant.BTN_BROWSE;
            this.btn_browse.DialogResult = enuDialogResult.OK;
            this.btn_browse.Location = new System.Drawing.Point(263, 78);
            this.btn_browse.Name = "btn_browse";
            this.btn_browse.Size = new System.Drawing.Size(34, 23);
            this.btn_browse.TabIndex = 7;
            this.btn_browse.Click += new System.EventHandler(this.btn_browse_Click);
            // 
            // txb_folder
            // 
            this.txb_folder.Location = new System.Drawing.Point(6, 81);
            this.txb_folder.Name = "txb_folder";
            this.txb_folder.ReadOnly = true;
            this.txb_folder.Size = new System.Drawing.Size(251, 20);
            this.txb_folder.TabIndex = 8;
            // 
            // lb_outdir
            // 
            this.lb_outdir.Alignment = System.Drawing.StringAlignment.Near;
            this.lb_outdir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_outdir.BackColor = System.Drawing.Color.Transparent;            
            this.lb_outdir.CaptionKey = "lb.dir.caption";
            this.lb_outdir.Location = new System.Drawing.Point(3, 62);
            this.lb_outdir.Name = "lb_outdir";
            this.lb_outdir.Size = new System.Drawing.Size(294, 13);
            this.lb_outdir.TabIndex = 9;
            this.lb_outdir.TabStop = false;
            // 
            // lb_codec
            // 
            this.lb_codec.CaptionKey = "lb.codec.caption";
            this.lb_codec.Location = new System.Drawing.Point(6, 120);
            this.lb_codec.Name = "lb_codec";
            this.lb_codec.Size = new System.Drawing.Size(70, 13);
            this.lb_codec.TabIndex = 10;
            // 
            // cmb_codec
            // 
            this.cmb_codec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_codec.FormattingEnabled = true;
            this.cmb_codec.Location = new System.Drawing.Point(82, 112);
            this.cmb_codec.Name = "cmb_codec";
            this.cmb_codec.Size = new System.Drawing.Size(82, 21);
            this.cmb_codec.TabIndex = 11;
            this.cmb_codec.SelectedIndexChanged += new System.EventHandler(this.cmb_codec_SelectedIndexChanged);
            // 
            // xRuleLabel2
            // 
            this.xRuleLabel2.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.xRuleLabel2.BackColor = System.Drawing.Color.Transparent;            
            this.xRuleLabel2.CaptionKey = "";
            this.xRuleLabel2.Location = new System.Drawing.Point(3, 139);
            this.xRuleLabel2.Name = "xRuleLabel2";
            this.xRuleLabel2.Size = new System.Drawing.Size(294, 13);
            this.xRuleLabel2.TabIndex = 12;
            this.xRuleLabel2.TabStop = false;
            // 
            // chb_webfile
            // 
            this.chb_webfile.AutoSize = true;
            this.chb_webfile.CaptionKey = "chb.web.file.caption";
            this.chb_webfile.Location = new System.Drawing.Point(6, 158);
            this.chb_webfile.Name = "chb_webfile";
            this.chb_webfile.Size = new System.Drawing.Size(121, 17);
            this.chb_webfile.TabIndex = 13;
            // 
            // UIXSplitterControl
            // 
            this.Controls.Add(this.chb_webfile);
            this.Controls.Add(this.xRuleLabel2);
            this.Controls.Add(this.cmb_codec);
            this.Controls.Add(this.lb_codec);
            this.Controls.Add(this.lb_outdir);
            this.Controls.Add(this.txb_folder);
            this.Controls.Add(this.btn_browse);
            this.Controls.Add(this.txb_col);
            this.Controls.Add(this.txb_row);
            this.Controls.Add(this.xLabel2);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.xRuleLabel1);
            this.Controls.Add(this.xLabel1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "UIXSplitterControl";
            this.Size = new System.Drawing.Size(300, 249);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private void txb_col_TextChanged(object sender, EventArgs e)
        {
            if (m_configure )return ;
            int r = 0;
            int c = 0;
            if (int.TryParse (txb_row .Text ,out r) && (int.TryParse (txb_col.Text , out c)))
            {
                if ((r > 0)&&(c > 0))
                {
                this.Row = r;
                this.Col = c;
                this.btn_ok.Enabled = true ;
                }
                else 
                    this.btn_ok.Enabled = false ;
            }
            else {
                this.btn_ok .Enabled = false ;
            }
            this.m_surface.Invalidate();
        }
        private void btn_ok_Click(object sender, EventArgs e)
        {
            Split();
        }
        public bool WebFile {
            get {
                return this.chb_webfile.Checked;
            }
        }
        private void Split()
        {
            if (Col < 1) return;
            if (Row < 1) return;
            Bitmap bmp = WinCoreBitmapOperation.GetGdiBitmap(m_document, CoreScreen.DpiX, CoreScreen.DpiY);
            int w = bmp.Width / Col;
            int h = bmp.Height / Row;
            if ((w <= 0)||(h <=0))
            {
                CoreMessageBox.Show ("Impossible de d?couper le graphique taille reqise non valide");
                bmp.Dispose ();
                return ;
            }
            Bitmap outbmp = null;
            int count = 0;
            string v_extension = null;
            if (m_imageFormat.Guid ==ImageFormat.Bmp.Guid)
            {
                v_extension = ".bmp";
            }
            else if (m_imageFormat.Guid ==ImageFormat.Png.Guid)
                v_extension = ".png";
            else 
                v_extension = ".jpg";
            string fname = this.SelectedFolder +"\\out";
            fname = fname.Replace('%', '_');
            Graphics g = null;
            for (int i = 0; i < Row; i++) {
                for (int j = 0; j < Col; j++)
                {
                    outbmp = new Bitmap(w, h);
                    g = Graphics.FromImage(outbmp);
                    g.DrawImage(bmp,
                        new Rectangle(Point.Empty, outbmp.Size),
                        new Rectangle(new Point(j * w, i * h), outbmp.Size),
                        GraphicsUnit.Pixel);
                    outbmp.Save(
                       this.SelectedFolder + "\\" + Path.GetFileNameWithoutExtension(fname) + "_" + count + v_extension,
                        m_imageFormat);
                    outbmp.Dispose();
                    count++;
                }
            }
            bmp.Dispose();
            if (WebFile)
            {
                FileStream fs = File.Create(fname + ".html");
                StreamWriter sw = new StreamWriter(fs);
                count = 0;
                sw.WriteLine("<table cellspacing=\"0\" cellpadding=\"0\" >");
                for (int i = 0; i < Row; i++)
                {
                    sw.WriteLine("<tr>");
                    for (int j = 0; j < Col; j++)
                    {
                        sw.Write("<td>");
                        sw.Write("<img src=\"" + Path.GetFileNameWithoutExtension(fname) + "_" + count + v_extension + "\" width=\""+w+"px\" height=\""+h+"px\" />");
                        sw.WriteLine("</td>");
                        count++;
                    }
                    sw.WriteLine("</tr>");
                }
                sw.WriteLine("</table>");
                sw.Flush();
                fs.Close();
            }
        }
        private void cmb_codec_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_configure) return;
            switch (cmb_codec.SelectedItem.ToString())
            {
                case ".bmp": this.m_imageFormat = ImageFormat.Bmp; break;
                case ".png": this.m_imageFormat = ImageFormat.Png; break;
                case ".jpg": this.m_imageFormat = ImageFormat.Jpeg; break;                    
            }
        }
        private void btn_browse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = this.SelectedFolder;
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    this.txb_folder.Text = fbd.SelectedPath;
                }
            }
            ParentForm.DialogResult = DialogResult.None;
        }
        void PaintGrid(ICoreGraphics g)
        {
            if (Col < 1) return;
            if (Row < 1) return;
            Rectanglef rc = this.m_surface.GetScreenBound (m_document.Bounds);
            int W = (int)rc.Width;
            int H = (int)rc.Height ;
            int w = W  / Col;
            int h = H  / Row;
            if (w <= 1) return;
            if (h <= 1) return;
            Pen p = WinCoreBrushRegister.GetPen(Colorf.DarkGray);
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            object   s  = g.Save();
            g.TranslateTransform(rc.X, rc.Y, enuMatrixOrder.Append);
            for (int i = 0; i < this.Row; i++)
            {
                g.DrawLine(p, 0, h + i * h, W, h + i * h);
            }
            for (int j = 0; j < this.Col; j++)
            {
                g.DrawLine(p, w + w * j, 0, w + w * j, H);
            }
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            g.Restore(s);
        }
        #region ISplitDocument Members
        public Bitmap[] SplitDocument(ICore2DDrawingDocument document, int row, int col)
        {
            Bitmap[] tab = new Bitmap[col * row];
            Bitmap bmp = WinCoreBitmapOperation.GetGdiBitmap(document, CoreScreen.DpiX , CoreScreen.DpiY );
            Bitmap outbmp = null;
            Graphics g = null;
            int w = bmp.Width / Col;
            int h = bmp.Height / Row;
            int count = 0;
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    outbmp = new Bitmap(w, h);
                    g = Graphics.FromImage(outbmp);
                    g.DrawImage(bmp,
                        new Rectangle(Point.Empty, outbmp.Size),
                        new Rectangle(new Point(j * w, i * h), outbmp.Size),
                        GraphicsUnit.Pixel);
                    outbmp.Dispose();
                    tab[i] = outbmp;
                    count++;
                }
            }
            bmp.Dispose();
            return tab;
        }
        #endregion
    }
}

