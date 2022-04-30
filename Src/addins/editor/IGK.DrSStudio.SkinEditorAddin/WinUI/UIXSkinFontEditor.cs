using IGK.DrSStudio.WinUI;
using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.Resources;
using IGK.ICore.WinCore;
using IGK.ICore.WinCore.WinUI.Controls;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.SkinEditorAddin.WinUI
{

    public class UIXSkinFontEditor : IGKXUserControl
    {

        
        private IGKXRuleLabel c_lb_size;
        private IGKXComboBox cmb_font;
        private System.Windows.Forms.TextBox c_txt_size;
        private IGKXRuleLabel c_alignment;
        private IGKXRuleLabel c_valignement;
        private IGKXButton c_btn_hnear;
        private IGKXButton c_btn_hcenterigkxButton2;
        private IGKXButton c_btn_hfar;
        private IGKXButton c_btn_vnear;
        private IGKXButton c_btn_vcenter;
        private IGKXButton c_btn_vfar;
        private IGKXRuleLabel c_lb_font;


        private CoreFont m_Font;
        private IGKXButton c_btn_underline;
        private IGKXButton c_btn_italic;
        private IGKXButton c_btn_bold;
        private IGKXRuleLabel c_lb_style;
        private ICore2DDrawingDocument sm_FontDocument;
        private bool m_configuring;
        private enuFontStyle m_fontStyle;
        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        [Browsable (false )]
        /// <summary>
        /// Get or set the font
        /// </summary>
        public CoreFont CoreFont
        {
            get { return m_Font; }
            set
            {
                if (m_Font != value)
                {
                    m_Font = value;
                    OnCoreFontChanged(EventArgs.Empty);
                }
            }
        }

        private void OnCoreFontChanged(EventArgs eventArgs)
        {
            this.m_configuring = true;
            if (this.CoreFont != null)
            {
                this.cmb_font.SelectedItem = this.CoreFont.FontName;
                this.c_txt_size.Text  = string.Format ("{0}", this.CoreFont.FontSize);
                this.UpdateFontCap();
            }
            this.m_configuring = false;
        }
        
        public UIXSkinFontEditor()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.c_lb_font = new IGKXRuleLabel();
            this.c_lb_size = new IGKXRuleLabel();
            this.cmb_font = new IGKXComboBox();
            this.c_txt_size = new System.Windows.Forms.TextBox();
            this.c_alignment = new IGKXRuleLabel();
            this.c_valignement = new IGKXRuleLabel();
            this.c_btn_hnear = new IGKXButton();
            this.c_btn_hcenterigkxButton2 = new IGKXButton();
            this.c_btn_hfar = new IGKXButton();
            this.c_btn_vnear = new IGKXButton();
            this.c_btn_vcenter = new IGKXButton();
            this.c_btn_vfar = new IGKXButton();
            this.c_btn_underline = new IGKXButton();
            this.c_btn_italic = new IGKXButton();
            this.c_btn_bold = new IGKXButton();
            this.c_lb_style = new IGKXRuleLabel();
            this.SuspendLayout();
            // 
            // c_lb_font
            // 
            this.c_lb_font.Alignment = System.Drawing.StringAlignment.Near;
            this.c_lb_font.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c_lb_font.CaptionKey = "lb.font.defintion";
            this.c_lb_font.HorizontalAlignment = IGK.ICore.Drawing2D.enuStringAlignment.Near;
            this.c_lb_font.Location = new System.Drawing.Point(3, 3);
            this.c_lb_font.Name = "c_lb_font";
            this.c_lb_font.Size = new System.Drawing.Size(339, 26);
            this.c_lb_font.TabIndex = 0;
            this.c_lb_font.TabStop = false;
            this.c_lb_font.VerticalAlignment = IGK.ICore.Drawing2D.enuStringAlignment.Near;
            // 
            // c_lb_size
            // 
            this.c_lb_size.Alignment = System.Drawing.StringAlignment.Near;
            this.c_lb_size.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c_lb_size.CaptionKey = "lb.size.defintion";
            this.c_lb_size.HorizontalAlignment = IGK.ICore.Drawing2D.enuStringAlignment.Near;
            this.c_lb_size.Location = new System.Drawing.Point(3, 66);
            this.c_lb_size.Name = "c_lb_size";
            this.c_lb_size.Size = new System.Drawing.Size(339, 26);
            this.c_lb_size.TabIndex = 1;
            this.c_lb_size.TabStop = false;
            this.c_lb_size.VerticalAlignment = IGK.ICore.Drawing2D.enuStringAlignment.Near;
            // 
            // cmb_font
            // 
            this.cmb_font.FormattingEnabled = true;
            this.cmb_font.Location = new System.Drawing.Point(41, 35);
            this.cmb_font.Name = "cmb_font";
            this.cmb_font.Size = new System.Drawing.Size(301, 21);
            this.cmb_font.TabIndex = 2;
            // 
            // c_txt_size
            // 
            this.c_txt_size.Location = new System.Drawing.Point(41, 98);
            this.c_txt_size.Name = "c_txt_size";
            this.c_txt_size.Size = new System.Drawing.Size(301, 20);
            this.c_txt_size.TabIndex = 3;
            // 
            // c_alignment
            // 
            this.c_alignment.Alignment = System.Drawing.StringAlignment.Near;
            this.c_alignment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c_alignment.CaptionKey = "lb.halignment";
            this.c_alignment.HorizontalAlignment = IGK.ICore.Drawing2D.enuStringAlignment.Near;
            this.c_alignment.Location = new System.Drawing.Point(6, 200);
            this.c_alignment.Name = "c_alignment";
            this.c_alignment.Size = new System.Drawing.Size(339, 26);
            this.c_alignment.TabIndex = 4;
            this.c_alignment.TabStop = false;
            this.c_alignment.VerticalAlignment = IGK.ICore.Drawing2D.enuStringAlignment.Near;
            // 
            // c_valignement
            // 
            this.c_valignement.Alignment = System.Drawing.StringAlignment.Near;
            this.c_valignement.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c_valignement.CaptionKey = "lb.valignment";
            this.c_valignement.HorizontalAlignment = IGK.ICore.Drawing2D.enuStringAlignment.Near;
            this.c_valignement.Location = new System.Drawing.Point(3, 265);
            this.c_valignement.Name = "c_valignement";
            this.c_valignement.Size = new System.Drawing.Size(339, 26);
            this.c_valignement.TabIndex = 5;
            this.c_valignement.TabStop = false;
            this.c_valignement.VerticalAlignment = IGK.ICore.Drawing2D.enuStringAlignment.Near;
            // 
            // c_btn_hnear
            // 
            this.c_btn_hnear.CaptionKey = "near";
            this.c_btn_hnear.Checked = false;
            this.c_btn_hnear.Location = new System.Drawing.Point(38, 232);
            this.c_btn_hnear.Name = "c_btn_hnear";
            this.c_btn_hnear.ShowButtonImage = false;
            this.c_btn_hnear.Size = new System.Drawing.Size(45, 33);
            this.c_btn_hnear.State = IGK.ICore.WinUI.enuButtonState.Normal;
            this.c_btn_hnear.TabIndex = 6;
            this.c_btn_hnear.Click += new System.EventHandler(this._hClick);
            // 
            // c_btn_hcenterigkxButton2
            // 
            this.c_btn_hcenterigkxButton2.CaptionKey = "center";
            this.c_btn_hcenterigkxButton2.Checked = false;
            this.c_btn_hcenterigkxButton2.Location = new System.Drawing.Point(89, 232);
            this.c_btn_hcenterigkxButton2.Name = "c_btn_hcenter";
            this.c_btn_hcenterigkxButton2.ShowButtonImage = false;
            this.c_btn_hcenterigkxButton2.Size = new System.Drawing.Size(45, 33);
            this.c_btn_hcenterigkxButton2.State = IGK.ICore.WinUI.enuButtonState.Normal;
            this.c_btn_hcenterigkxButton2.TabIndex = 7;
            this.c_btn_hcenterigkxButton2.Click += new System.EventHandler(this._hClick);
            // 
            // c_btn_hfar
            // 
            this.c_btn_hfar.CaptionKey = "far";
            this.c_btn_hfar.Checked = false;
            this.c_btn_hfar.Location = new System.Drawing.Point(140, 232);
            this.c_btn_hfar.Name = "c_btn_hfar";
            this.c_btn_hfar.ShowButtonImage = false;
            this.c_btn_hfar.Size = new System.Drawing.Size(45, 33);
            this.c_btn_hfar.State = IGK.ICore.WinUI.enuButtonState.Normal;
            this.c_btn_hfar.TabIndex = 8;
            this.c_btn_hfar.Click += new System.EventHandler(this._hClick);
            // 
            // c_btn_vnear
            // 
            this.c_btn_vnear.CaptionKey = "near";
            this.c_btn_vnear.Checked = false;
            this.c_btn_vnear.Location = new System.Drawing.Point(38, 297);
            this.c_btn_vnear.Name = "c_btn_vnear";
            this.c_btn_vnear.ShowButtonImage = false;
            this.c_btn_vnear.Size = new System.Drawing.Size(45, 33);
            this.c_btn_vnear.State = IGK.ICore.WinUI.enuButtonState.Normal;
            this.c_btn_vnear.TabIndex = 9;
            this.c_btn_vnear.Click += new System.EventHandler(this._vClick);
            // 
            // c_btn_vcenter
            // 
            this.c_btn_vcenter.CaptionKey = "center";
            this.c_btn_vcenter.Checked = false;
            this.c_btn_vcenter.Location = new System.Drawing.Point(89, 297);
            this.c_btn_vcenter.Name = "c_btn_vcenter";
            this.c_btn_vcenter.ShowButtonImage = false;
            this.c_btn_vcenter.Size = new System.Drawing.Size(45, 33);
            this.c_btn_vcenter.State = IGK.ICore.WinUI.enuButtonState.Normal;
            this.c_btn_vcenter.TabIndex = 10;
            this.c_btn_vcenter.Click += new System.EventHandler(this._vClick);
            // 
            // c_btn_vfar
            // 
            this.c_btn_vfar.CaptionKey = "far";
            this.c_btn_vfar.Checked = false;
            this.c_btn_vfar.Location = new System.Drawing.Point(140, 297);
            this.c_btn_vfar.Name = "c_btn_vfar";
            this.c_btn_vfar.ShowButtonImage = false;
            this.c_btn_vfar.Size = new System.Drawing.Size(45, 33);
            this.c_btn_vfar.State = IGK.ICore.WinUI.enuButtonState.Normal;
            this.c_btn_vfar.TabIndex = 11;
            this.c_btn_vfar.Click += new System.EventHandler(this._vClick);
            // 
            // c_btn_underline
            // 
            this.c_btn_underline.CaptionKey = "underline";
            this.c_btn_underline.Checked = false;
            this.c_btn_underline.Location = new System.Drawing.Point(140, 156);
            this.c_btn_underline.Name = "c_btn_underline";
            this.c_btn_underline.ShowButtonImage = false;
            this.c_btn_underline.Size = new System.Drawing.Size(45, 33);
            this.c_btn_underline.State = IGK.ICore.WinUI.enuButtonState.Normal;
            this.c_btn_underline.TabIndex = 15;
            // 
            // c_btn_italic
            // 
            this.c_btn_italic.CaptionKey = "italic";
            this.c_btn_italic.Checked = false;
            this.c_btn_italic.Location = new System.Drawing.Point(89, 156);
            this.c_btn_italic.Name = "c_btn_italic";
            this.c_btn_italic.ShowButtonImage = false;
            this.c_btn_italic.Size = new System.Drawing.Size(45, 33);
            this.c_btn_italic.State = IGK.ICore.WinUI.enuButtonState.Normal;
            this.c_btn_italic.TabIndex = 14;
            // 
            // c_btn_bold
            // 
            this.c_btn_bold.CaptionKey = "bold";
            this.c_btn_bold.Checked = false;
            this.c_btn_bold.Location = new System.Drawing.Point(38, 156);
            this.c_btn_bold.Name = "c_btn_bold";
            this.c_btn_bold.ShowButtonImage = false;
            this.c_btn_bold.Size = new System.Drawing.Size(45, 33);
            this.c_btn_bold.State = IGK.ICore.WinUI.enuButtonState.Normal;
            this.c_btn_bold.TabIndex = 13;
            // 
            // c_lb_style
            // 
            this.c_lb_style.Alignment = System.Drawing.StringAlignment.Near;
            this.c_lb_style.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c_lb_style.CaptionKey = "lb.style";
            this.c_lb_style.HorizontalAlignment = IGK.ICore.Drawing2D.enuStringAlignment.Near;
            this.c_lb_style.Location = new System.Drawing.Point(3, 129);
            this.c_lb_style.Name = "c_lb_style";
            this.c_lb_style.Size = new System.Drawing.Size(339, 26);
            this.c_lb_style.TabIndex = 12;
            this.c_lb_style.TabStop = false;
            this.c_lb_style.VerticalAlignment = IGK.ICore.Drawing2D.enuStringAlignment.Near;
            // 
            // UIXSkinFontEditor
            // 
            this.Controls.Add(this.c_btn_underline);
            this.Controls.Add(this.c_btn_italic);
            this.Controls.Add(this.c_btn_bold);
            this.Controls.Add(this.c_lb_style);
            this.Controls.Add(this.c_btn_vfar);
            this.Controls.Add(this.c_btn_vcenter);
            this.Controls.Add(this.c_btn_vnear);
            this.Controls.Add(this.c_btn_hfar);
            this.Controls.Add(this.c_btn_hcenterigkxButton2);
            this.Controls.Add(this.c_btn_hnear);
            this.Controls.Add(this.c_valignement);
            this.Controls.Add(this.c_alignment);
            this.Controls.Add(this.c_txt_size);
            this.Controls.Add(this.cmb_font);
            this.Controls.Add(this.c_lb_size);
            this.Controls.Add(this.c_lb_font);
            this.Name = "UIXSkinFontEditor";
            this.Size = new System.Drawing.Size(345, 363);
            this.Load += new System.EventHandler(this._Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void _vClick(object sender, EventArgs e)
        {
            var b = sender as IGKXButton;
            if (b != null)
            {
                string n = b.Name.Substring("c_btn_v".Length);
                this.VerticalAlignment = this.GetAlignment(n);
            }
        }

        private void _hClick(object sender, EventArgs e)
        {
            var b = sender as IGKXButton;
            string n = b.Name.Substring("c_btn_h".Length);
            this.HorizontalAlignment = this.GetAlignment(n);
           
        }

        private enuStringAlignment GetAlignment(string n)
        {
            switch (n)
            {
                case "near":
                    return enuStringAlignment.Near;
                    
                case "center":
                    return enuStringAlignment.Center;
                    
                case "far":
                    return enuStringAlignment.Far ;
                    
            }
            return  enuStringAlignment.Near;
        }

        private void _Load(object sender, EventArgs e)
        {
            sm_FontDocument = CoreResources.GetDocument (CoreConstant.IMG_POLICE);
            //build combobox
            this.cmb_font.DrawMode = DrawMode.OwnerDrawFixed;
            this.cmb_font.DrawItem += _DrawItemFont;
            this.cmb_font.Items.Clear();
            this.cmb_font.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmb_font.BeginUpdate();
            string[] v_t = CoreFont.GetInstalledFamilies();
            Array.Sort(v_t);
            foreach (string i in v_t)
            {
                this.cmb_font.Items.Add(i);
            }
            this.cmb_font.SelectedIndex = 0;
            this.cmb_font.EndUpdate();
            this.cmb_font.SelectedIndexChanged += font_PropChanged;
            this.cmb_font.AutoSize = false;
            this.cmb_font.Width = 180;
            this.c_txt_size.TextChanged += _textChanged;

            this.c_btn_bold.CheckedChanged += v_checkedChanged;
            this.c_btn_bold.Click += _styleClick;
            this.c_btn_bold.Tag = enuFontStyle.Bold;

            this.c_btn_italic.CheckedChanged += v_checkedChanged;
            this.c_btn_italic.Click += _styleClick;
            this.c_btn_italic.Tag = enuFontStyle.Italic;

            this.c_btn_underline.CheckedChanged += v_checkedChanged;
            this.c_btn_underline.Click += _styleClick;
            this.c_btn_underline.Tag = enuFontStyle.Underline;
         //   this.btn_strikeout.CheckedChanged += v_checkedChanged;
        }

        void _styleClick(object sender, EventArgs e)
        {
            IGKXButton btn = sender as IGKXButton;
            if (btn != null)
                btn.Checked = !btn.Checked;
        }

        private void v_checkedChanged(object sender, EventArgs e)
        {
            if (m_configuring) 
                return;
            m_configuring = true;
            var btn = sender as IGKXButton;
            enuFontStyle t = (enuFontStyle)btn.Tag;
            enuFontStyle v_fontStyle = CoreFont.FontStyle;
            FontFamily ft = (FontFamily)CoreFont.GetFonts(this.FontName).FontFamily;
            if (btn.Checked)
            {
                t = v_fontStyle | t;
                if (ft.IsStyleAvailable((FontStyle)t))
                {
                    v_fontStyle = t;
                }
            }
            else
            {
                if ((v_fontStyle & t) == t)
                {
                    t = (int)v_fontStyle - t;
                    if (!ft.IsStyleAvailable((FontStyle)t))
                    {
                        btn.Checked = true;
                        this.m_configuring = false;
                        ft.Dispose();
                        return;
                    }
                    else
                    {
                        v_fontStyle = t;
                    }
                }
            }
            this.m_configuring = false;
            this.m_fontStyle = v_fontStyle;
            __defFont();
        }

        private void _textChanged(object sender, EventArgs e)
        {
            __defFont();
        }

        private void __defFont()
        {

            if (this.m_configuring)
                return;
            _updateFontDefintion();
            UpdateFontCap();

        }
        /// <summary>
        /// update font capabality
        /// </summary>
        private void UpdateFontCap()
        {
            System.Drawing.FontFamily ft = CoreFont.GetFonts(this.FontName).FontFamily
                as System.Drawing.FontFamily;
            this.c_btn_bold.Enabled = ft.IsStyleAvailable(System.Drawing.FontStyle.Bold);
            this.c_btn_italic.Enabled = ft.IsStyleAvailable(System.Drawing.FontStyle.Italic);
           // this.c_btn_strikeout.Enabled = ft.IsStyleAvailable(System.Drawing.FontStyle.Strikeout);
            this.c_btn_underline.Enabled = ft.IsStyleAvailable(System.Drawing.FontStyle.Underline);
        }
        public string FontName {
            get {
                return this.CoreFont.FontName;
            }
        }
        void font_PropChanged(object sender, EventArgs e)
        {
            __defFont();
            UpdateFontCap();
        }
        void _updateFontDefintion() {
            if (this.CoreFont == null)
                return;
            this.m_configuring = true;
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("FontName:{0}; Size:{1}; Style:{2} ;",
            this.cmb_font.SelectedItem,
            GetSize(),
            enuFontStyle.Regular | GetStyle()));
            sb.Append (string.Format("HorizontaAlignment:{0}; VerticalAlignment:{1}", 
                this.HorizontalAlignment ,
                this.VerticalAlignment 
                ));

            this.CoreFont.CopyDefinition (sb.ToString());


     
            this.m_configuring = false;
        }

        private enuFontStyle GetStyle()
        {
            return this.m_fontStyle;
        }

        private float GetSize()
        {
            CoreUnit u = this.c_txt_size.Text;
            return u.GetPixel();
        }
        void _DrawItemFont(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            ComboBox cmb = sender as ComboBox;
            GraphicsState v_state = null;
            Rectangle rc = e.Bounds;
            object v_fontobj = this.cmb_font.Items[e.Index];

            string v_str = v_fontobj.ToString();

            ICoreFontInfo m = CoreFont.GetFonts(v_str);

            FontFamily fm = (m != null) ? (FontFamily)m.FontFamily : null;// m.FontFamily();// new FontFamily(v_str);
            if (fm != null)
            {
                FontStyle fstyle = global::System.Drawing.FontStyle.Regular;
                CoreFont v_sft = null;
                float v_fontsize = e.Bounds.Height;
                if (!fm.IsStyleAvailable(fstyle))
                {
                    v_sft =
                        CoreFont.CreateFont(null, v_str);
                    fstyle = (FontStyle)v_sft.GetAllAvailableStyle();
                    v_fontsize = CoreFont.GetFontSize(v_sft,
                 (enuFontStyle)fstyle,
                 new Rectanglef(rc.X, rc.Y, rc.Width, rc.Height),
                 1);
                    v_sft.Dispose();
                }
                else
                {
                    float em = fm.GetEmHeight(fstyle);
                    float hm = fm.GetLineSpacing(fstyle);
                    float am = fm.GetCellAscent(fstyle);
                    float dm = fm.GetCellDescent(fstyle);
                    float f = (rc.Height * (em / hm));
                    v_fontsize = f;
                }
                //create a new from 
                if (sm_FontDocument != null)
                {
                    sm_FontDocument.Draw(e.Graphics, true, new Rectanglei(0, rc.Y, 16, 16), enuFlipMode.None);
                }
                rc.X += 16;
                rc.Width -= 16;
                using (Font ft = new Font(
                    fm,
                    (v_fontsize + "px").ToPixel(),
                    fstyle,
                    GraphicsUnit.Pixel))
                {
                    v_state = e.Graphics.Save();
                    e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    if ((e.State & DrawItemState.ComboBoxEdit) == DrawItemState.ComboBoxEdit)
                        e.Graphics.DrawString(v_str, e.Font, WinCoreBrushRegister.GetBrush(e.ForeColor), rc);
                    else
                        e.Graphics.DrawString(v_str, ft, WinCoreBrushRegister.GetBrush(e.ForeColor), rc);
                    e.Graphics.Restore(v_state);
                }
                //fm.Dispose();
            }
            e.DrawFocusRectangle();
        }

        /// <summary>
        /// get the vertical alignement
        /// </summary>
        public enuStringAlignment HorizontalAlignment { get {
            return this.CoreFont.HorizontalAlignment;
        }
            set {
                this.CoreFont.HorizontalAlignment = value;
            }
        }
        /// <summary>
        /// get the vertical alignement
        /// </summary>
        public enuStringAlignment VerticalAlignment { get {
            return this.CoreFont.VerticalAlignment;
        }
            set {
                this.CoreFont.VerticalAlignment = value;
            }
        }
    }
}
