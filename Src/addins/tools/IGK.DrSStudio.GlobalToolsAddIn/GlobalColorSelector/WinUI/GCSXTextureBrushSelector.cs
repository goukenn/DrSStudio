

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GCSXTextureBrushSelector.cs
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
file:TextureBrushSelector.cs
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
using System; using IGK.ICore.WinCore;
using IGK.ICore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D ;
using System.IO; 
namespace IGK.DrSStudio.WinUI
{
    using IGK.ICore.Drawing2D;
    using IGK.ICore.Resources;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinCore.WinUI.Controls;
    /// <summary>
    /// represent the texture brush selector
    /// </summary>
   sealed  class TextureBrushSelector :
           GCSXBrushConfigureBase 
    {
        private IGKXButton btn_browser;
        private IGKXRuleLabel xRuleLabel1;
        private IGKXPictureBox pictureBox1;
        private IGKXRuleLabel xRuleLabel2;
        private IGKXComboBox cmb_wrapMode;
        private IGKXCheckBox chb_autosize;
        private IGKXTextBox txb_rectangle;
        private ICoreBitmap m_bmp;
        private IGKXRuleLabel xRuleLabel3;
        private Rectanglei m_requireRectangle;
        private IGKXCheckBox c_chb_autotransform;
        private bool m_configure;
        private string m_FileName;
       /// <summary>
       /// Get the filename
       /// </summary>
        public string FileName
        {
            get { return m_FileName; }
            private set
            {
                if (m_FileName != value)
                {
                    m_FileName = value;
                }
            }
        }
        public ICoreBitmap Bitmap { get{return m_bmp ;}}
        public Rectanglei TextureRequireRectangle { get { return this.m_requireRectangle; } }
        public enuWrapMode WrapMode { get { return (enuWrapMode)this.cmb_wrapMode.SelectedItem; } }
        public bool TextureAutoSize { get { return this.chb_autosize.Checked; } }
        public bool TextureAutoTransform { get { return this.c_chb_autotransform.Checked; } }
        public override enuBrushType BrushType
        {
            get
            {
                return enuBrushType.Texture;
            }
        }
        protected override void ConfigFromBrush(ICoreBrush brush)
        {
            this.Enabled = (brush != null);
            if ((brush == null)||(brush.BrushType != enuBrushType.Texture ))
                return;
            this.m_configure = true;
            if (this.m_bmp != brush.Bitmap)
            {
                this.m_bmp = brush.Bitmap;
                this.pictureBox1.Image = brush.Bitmap;
            }
            this.chb_autosize.Checked = brush.AutoSize;
            this.c_chb_autotransform.Checked = brush.TextureAutoTranform;
            this.cmb_wrapMode.SelectedItem = brush.WrapMode;
            this.txb_rectangle.Text = string.Format("{0:0} {1:0} {2:0} {3:0}",
         brush.Bounds.X,
         brush.Bounds.Y,
         Math.Max (1,brush.Bounds.Width),
         Math.Max (1, brush.Bounds.Height));
            this.m_configure = false;
        }
        public override void SetColor(Colorf color)
        {
            //no action
        }
        public TextureBrushSelector()
        {            
            this.InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.AutoScroll = true;
        }
        private void InitializeComponent()
        {
            this.btn_browser = new IGKXButton();
            this.xRuleLabel1 = new IGKXRuleLabel();
            this.pictureBox1 = new IGKXPictureBox();
            this.xRuleLabel2 = new IGKXRuleLabel();
            this.cmb_wrapMode = new IGKXComboBox();
            this.chb_autosize = new IGKXCheckBox();
            this.txb_rectangle = new IGKXTextBox();
            this.xRuleLabel3 = new IGKXRuleLabel();
            this.c_chb_autotransform = new IGKXCheckBox();
            this.SuspendLayout();
            // 
            // btn_browser
            // 
            this.btn_browser.ButtonImageSize = new Size2i(32, 32);
            this.btn_browser.CaptionKey = "btn.Browse.caption";
            this.btn_browser.Checked = false;
            this.btn_browser.DialogResult = enuDialogResult.None;
            this.btn_browser.Location = new System.Drawing.Point(65, 16);
            this.btn_browser.Name = "btn_browser";
            this.btn_browser.ShowButtonImage = false;
            this.btn_browser.Size = new System.Drawing.Size(75, 23);
            this.btn_browser.State = enuButtonState.Normal;
            this.btn_browser.TabIndex = 0;
            this.btn_browser.Click += new System.EventHandler(this.btn_browser_Click);
            // 
            // xRuleLabel1
            // 
            this.xRuleLabel1.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.xRuleLabel1.CaptionKey = "lb.wrapMode.caption";
            this.xRuleLabel1.Location = new System.Drawing.Point(3, 42);
            this.xRuleLabel1.Name = "xRuleLabel1";
            this.xRuleLabel1.Size = new System.Drawing.Size(246, 23);
            this.xRuleLabel1.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(65, 110);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(170, 129);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // xRuleLabel2
            // 
            this.xRuleLabel2.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.xRuleLabel2.CaptionKey = "lb.preview.caption";
            this.xRuleLabel2.Location = new System.Drawing.Point(3, 84);
            this.xRuleLabel2.Name = "xRuleLabel2";
            this.xRuleLabel2.Size = new System.Drawing.Size(246, 23);
            this.xRuleLabel2.TabIndex = 3;
            // 
            // cmb_wrapMode
            // 
            this.cmb_wrapMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_wrapMode.FormattingEnabled = true;
            this.cmb_wrapMode.Location = new System.Drawing.Point(65, 60);
            this.cmb_wrapMode.Name = "cmb_wrapMode";
            this.cmb_wrapMode.Size = new System.Drawing.Size(170, 21);
            this.cmb_wrapMode.TabIndex = 4;
            this.cmb_wrapMode.SelectedIndexChanged += new System.EventHandler(this.cmb_wrapMode_SelectedIndexChanged);
            // 
            // chb_autosize
            // 
            this.chb_autosize.AutoSize = true;
            this.chb_autosize.CaptionKey = "chb.autosize.caption";
            this.chb_autosize.Checked = true;
            this.chb_autosize.CheckState = enuCheckState.Checked;
            this.chb_autosize.Location = new System.Drawing.Point(65, 259);
            this.chb_autosize.Name = "chb_autosize";
            this.chb_autosize.Size = new System.Drawing.Size(124, 17);
            this.chb_autosize.TabIndex = 5;
            
            this.chb_autosize.CheckedChanged += new System.EventHandler(this.chb_autosize_CheckedChanged);
            // 
            // txb_rectangle
            // 
            this.txb_rectangle.Enabled = false;
            this.txb_rectangle.Location = new System.Drawing.Point(65, 282);
            this.txb_rectangle.Name = "txb_rectangle";
            this.txb_rectangle.Size = new System.Drawing.Size(170, 20);
            this.txb_rectangle.TabIndex = 6;
            this.txb_rectangle.Text = "0 0 0 0";
            this.txb_rectangle.TextChanged += new System.EventHandler(this.txb_rectangle_TextChanged);
            // 
            // xRuleLabel3
            // 
            this.xRuleLabel3.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.xRuleLabel3.CaptionKey = "lb.texturesize.caption";
            this.xRuleLabel3.Location = new System.Drawing.Point(3, 242);
            this.xRuleLabel3.Name = "xRuleLabel3";
            this.xRuleLabel3.Size = new System.Drawing.Size(246, 23);
            this.xRuleLabel3.TabIndex = 7;
            // 
            // c_chb_autotransform
            // 
            this.c_chb_autotransform.AutoSize = true;
            this.c_chb_autotransform.CaptionKey = "chb.autotransform.caption";
            this.c_chb_autotransform.Checked = true;
            this.c_chb_autotransform.CheckState = enuCheckState.Checked;
            this.c_chb_autotransform.Location = new System.Drawing.Point(65, 308);
            this.c_chb_autotransform.Name = "c_chb_autotransform";
            this.c_chb_autotransform.Size = new System.Drawing.Size(149, 17);
            this.c_chb_autotransform.TabIndex = 8;

            this.c_chb_autotransform.CheckedChanged += new System.EventHandler(this.c_chb_autotransform_CheckedChanged);
            // 
            // TextureBrushSelector
            // 
            this.Controls.Add(this.c_chb_autotransform);
            this.Controls.Add(this.txb_rectangle);
            this.Controls.Add(this.chb_autosize);
            this.Controls.Add(this.cmb_wrapMode);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.xRuleLabel1);
            this.Controls.Add(this.btn_browser);
            this.Controls.Add(this.xRuleLabel2);
            this.Controls.Add(this.xRuleLabel3);
            this.Name = "TextureBrushSelector";
            this.Size = new System.Drawing.Size(252, 347);
            this.Load += new System.EventHandler(this.TextureBrushSelector_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private void btn_browser_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Pictures | *.jpg;*.jpeg; *.gif; *.tiff; *.bmp;*.png; *.data;";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        switch (System.IO.Path.GetExtension(ofd.FileName).ToLower())
                        {
                            case ".data":
                                string data = System.IO.File.ReadAllText(ofd.FileName);
                                  Byte[] v_tab = Convert.FromBase64String(data);
                    System.IO.MemoryStream mem = new System.IO.MemoryStream();
                    mem.Write(v_tab, 0, v_tab.Length);
                    mem.Seek(0, System.IO.SeekOrigin.Begin);
                    ICoreBitmap v_bmp = null;
                                try
                                {
                                    v_bmp = CoreResources.GetBitmapResourcesFromFileStream(mem);
                                }
                                catch
                                {
                                    CoreLog.WriteDebug("Bitmap failed from memory stream");
                                }
                                finally
                                {
                                  mem.Dispose();
                                }
                                if (v_bmp != null)
                                {
                                    this.m_bmp =v_bmp;
                                    ConfigureBrush();
                                    this.pictureBox1.Image = m_bmp;
                                    //unregister text changed
                                    this.txb_rectangle.TextChanged -= txb_rectangle_TextChanged;
                                    this.txb_rectangle.Text =
                                        string.Format("{0} {1} {2} {3}",
                                        0, 0, v_bmp.Width, v_bmp.Height);
                                    this.m_requireRectangle = new Rectanglei(0, 0, v_bmp.Width, v_bmp.Height);
                                    //register text changed
                                    this.txb_rectangle.TextChanged += txb_rectangle_TextChanged;
                                }
                                break;
                            default :
                                ICoreBitmap bmp = CoreResources.GetBitmapResourcesFromFile(ofd.FileName);
                                this.m_bmp = bmp;
                                this.m_FileName = ofd.FileName;
                                ConfigureBrush();
                                this.pictureBox1.Image = bmp;
                                
                                //unregister text changed
                                this.txb_rectangle.TextChanged -= txb_rectangle_TextChanged;
                                this.txb_rectangle.Text =
                                    string.Format("{0} {1} {2} {3}",
                                    0, 0, bmp.Width, bmp.Height);
                                this.m_requireRectangle = new Rectanglei(0, 0, bmp.Width, bmp.Height);
                                //register text changed
                                this.txb_rectangle.TextChanged += txb_rectangle_TextChanged;
                                break;
                        }
                    }
                    catch { 
                    }
                }
            }
        }
        protected override void ConfigureBrush()
        {

            if ((this.ColorOwner == null) || ((this.ColorOwner.BrushSupport & enuBrushSupport.Texture) != enuBrushSupport.Texture))
                return;

            ICoreBrush br = this.BrushToConfigure;
            if ((br == null) || (this.Bitmap == null) || (this.Bitmap.PixelFormat == enuPixelFormat.Undefined ))
            {
                return;
            }
            if (!this.TextureAutoSize &&
                this.TextureRequireRectangle.IsEmpty)
            {
                this.m_requireRectangle = new Rectanglei(0, 0, this.Bitmap.Width, this.Bitmap.Height);
            }
            ICoreWorkingResourcesContainerSurface v_rsContainer = this.CurrentSurface as
                ICoreWorkingResourcesContainerSurface;
            if ((v_rsContainer != null) && (v_rsContainer.Resources !=null) && (File.Exists (this.FileName )))
            {
                //manage resources element
                ICoreTextureResource v_Text = null;
                string name = Path.GetFileNameWithoutExtension(this.FileName);
                if (v_rsContainer.Resources.Contains(name))
                {
                    v_Text = v_rsContainer.Resources.GetResourceById(name) as ICoreTextureResource;
                }
                else
                {
                    v_Text =ResourceFileManager.CreateTextureRes (
                        v_rsContainer,
                    Path.GetFullPath(this.FileName),
                    name,
                    this.Bitmap)
                  ;
                    v_rsContainer.Resources.Register(v_Text);
                }
                if (v_Text != null)
                {
                    br.SetTextureResource(v_Text,
                        this.WrapMode,
                        this.TextureAutoSize,
                        this.TextureRequireRectangle,
                        this.TextureAutoTransform);
                }
            }
            else {
                br.SetTextureBrush(
                    this.Bitmap,
                    this.WrapMode,
                    this.TextureAutoSize,
                    this.TextureRequireRectangle,
                    this.TextureAutoTransform);
            }
        }
        private void TextureBrushSelector_Load(object sender, EventArgs e)
        {
            InitControl();            
        }
        private void InitControl()
        {
            foreach (enuWrapMode item in Enum.GetValues(typeof(enuWrapMode)))
            {
                this.cmb_wrapMode.Items.Add(item);
            }
            this.cmb_wrapMode.SelectedItem = enuWrapMode.Tile;
        }
        private void txb_rectangle_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txb_rectangle.Text))
                return;
            string[] v = this.txb_rectangle.Text.Split(' ');
            if (v.Length == 4)
            {
                int x, y, w, h;
                if (int.TryParse(v[0], out x) &&
                    int.TryParse(v[1], out y) &&
                    int.TryParse(v[2], out w) &&
                    int.TryParse(v[3], out h))
                {
                    this.m_requireRectangle = new Rectanglei(x, y, w, h);
                    ConfigureBrush();
                }
            }
        }
        private void chb_autosize_CheckedChanged(object sender, EventArgs e)
        {
            this.txb_rectangle.Enabled = !(this.chb_autosize.Checked);
            if (this.m_configure)
                return;
            ConfigureBrush();
        }
        private void cmb_wrapMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_configure) 
                return;
            this.m_configure = true;
            ConfigureBrush();
            this.m_configure = false;
        }
        private void c_chb_autotransform_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_configure)
                return;
            ConfigureBrush();
        }
    }
}

