

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: FontSelectorBase.cs
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
file:FontSelectorBase.cs
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
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace IGK.DrSStudio.WinUI
{
    using IGK.ICore.WinCore;
using IGK.ICore.Tools;
    using IGK.ICore.Drawing2D;
    using IGK.ICore;
    using IGK.ICore.Resources;
    using IGK.ICore.WinCore.WinUI.Controls;
    /// <summary>
    /// preprsent a font definition tools strip
    /// </summary>
    public class FontSelectorBase : IGKXToolStripCoreToolHost
    {
        ToolStripComboBox cmb_font;
        ToolStripComboBox cmb_fontsize;
        IGKXToolStripButton btn_bold;
        IGKXToolStripButton btn_italic;
        IGKXToolStripButton btn_underline;
        IGKXToolStripButton btn_strikeout;
        IGKXToolStripButton btn_lalignLeft;
        IGKXToolStripButton btn_lalignCenter;
        IGKXToolStripButton btn_lalignRight;
        IGKXToolStripButton btn_valignRight;
        IGKXToolStripButton btn_valignCenter;
        IGKXToolStripButton btn_valignLeft;
        private enuFontStyle m_fontStyle;
        private enuStringAlignment m_fontHAlignment;
        private enuStringAlignment m_fontVAlignment;
        private CoreUnit m_fontSize;
        private enuUnitType m_DisplayUnit;

        public enuUnitType DisplayUnit
        {
            get { return m_DisplayUnit; }
            set
            {
                if (m_DisplayUnit != value)
                {
                    m_DisplayUnit = value;
                }
            }
        }

        private static ICore2DDrawingDocument sm_FontDocument;


        static FontSelectorBase()
        {
            sm_FontDocument = CoreResources.GetDocument ("Police");
        }
        public string FontName
        {
            get
            {
                if (this.cmb_font.SelectedItem != null)
                    return this.cmb_font.SelectedItem.ToString();
                return null;
            }
        }
        public CoreUnit FontSize { get { return m_fontSize; } }
        public enuFontStyle FontStyle { get { return this.m_fontStyle; } }
        public enuStringAlignment FontHorizontalAlignment { get { return this.m_fontHAlignment; } }
        public enuStringAlignment FontVercalAlignment { get { return this.m_fontVAlignment; } }
        public event EventHandler FontDefinitionChanged;
        private bool m_configuring;
        void OnFontDefinitionChanged(EventArgs e)
        {
            if (m_configuring) return;
            FontDefinitionChanged?.Invoke(this, e);
        }
        internal void Update(ICoreFont iCoreFont)
        {
            this.Configure(iCoreFont);
        }
        internal void Configure(ICoreFont font)
        {
            if ((font == null)|| string.IsNullOrEmpty(font.FontName))
            {
                //do nothing
            }
            else
            {
                this.m_configuring = true;
                this.cmb_font.SelectedItem = font.FontName;
                int i = this.cmb_fontsize.SelectionStart;

                Font rt = font.ToGdiFont();
                CoreUnit ru = ((font.FontSize) + "px");

                this.m_fontSize = string.Format("{0:##0.00} {1} ", ru.GetValue (this.DisplayUnit ), this.DisplayUnit);//font.FontSize);
                this.cmb_fontsize.Text = this.m_fontSize.ToString();
                this.cmb_fontsize.SelectionStart = i;
                this.m_fontHAlignment = font.HorizontalAlignment;
                this.m_fontVAlignment = font.VerticalAlignment;
                this.btn_bold.Checked = FontIs(font.FontStyle, enuFontStyle.Bold);
                this.btn_italic.Checked = FontIs(font.FontStyle, enuFontStyle.Italic);
                this.btn_underline.Checked = FontIs(font.FontStyle, enuFontStyle.Underline);
                this.btn_strikeout.Checked = FontIs(font.FontStyle, enuFontStyle.Strikeout);
                this.m_fontStyle = font.FontStyle;
                UpdateFontCap();
                UpdateHAlign(font.HorizontalAlignment);
                UpdateVAlign(font.VerticalAlignment);
                this.m_configuring = false;
            }
        }
        private void UpdateVAlign(enuStringAlignment stringAlignment)
        {
            this.btn_valignCenter.Checked = FontAlignIs(stringAlignment, enuStringAlignment.Center);
            this.btn_valignLeft.Checked = FontAlignIs(stringAlignment, enuStringAlignment.Near);
            this.btn_valignRight.Checked = FontAlignIs(stringAlignment, enuStringAlignment.Far);
        }
        private void UpdateHAlign(enuStringAlignment stringAlignment)
        {
            this.btn_lalignCenter.Checked = FontAlignIs(stringAlignment, enuStringAlignment.Center);
            this.btn_lalignLeft.Checked = FontAlignIs(stringAlignment, enuStringAlignment.Near);
            this.btn_lalignRight.Checked = FontAlignIs(stringAlignment, enuStringAlignment.Far);
        }
        private void UpdateFontCap()
        {

            if (string.IsNullOrEmpty(this.FontName))
                return;

            System.Drawing.FontFamily ft = CoreFont.GetFonts(this.FontName).FontFamily 
                as System.Drawing.FontFamily;
            this.btn_bold.Enabled = ft.IsStyleAvailable( System.Drawing.FontStyle.Bold);
            this.btn_italic.Enabled = ft.IsStyleAvailable(System.Drawing.FontStyle.Italic);
            this.btn_strikeout.Enabled = ft.IsStyleAvailable(System.Drawing.FontStyle.Strikeout);
            this.btn_underline.Enabled = ft.IsStyleAvailable(System.Drawing.FontStyle.Underline);
        }
        private bool FontAlignIs(enuStringAlignment stringAlignment, enuStringAlignment stringAlignment_2)
        {
            bool v = stringAlignment == stringAlignment_2;
            return v;
        }
        private bool FontIs(enuFontStyle fontStyle, enuFontStyle font2)
        {
            return (fontStyle & font2) == font2;
        }
        public FontSelectorBase()
            : base()
        {

            this.m_DisplayUnit = enuUnitType.px;
            this.m_fontStyle = enuFontStyle.Regular;
            InitTools();
        }
        public ToolStripComboBox SizeComboBox { get { return this.cmb_fontsize; } }
        public ToolStripComboBox FontComboBox { get { return this.cmb_font; } }
        private void InitTools()
        {
            this.SuspendLayout();
            cmb_font = new FontCombobox();
            cmb_fontsize = new ToolStripComboBox();
            btn_bold = new IGKXToolStripButton();
            btn_italic = new IGKXToolStripButton();
            btn_underline = new IGKXToolStripButton();
            btn_strikeout = new IGKXToolStripButton();
            btn_lalignLeft = new IGKXToolStripButton();
            btn_lalignCenter = new IGKXToolStripButton();
            btn_lalignRight = new IGKXToolStripButton();
            btn_valignRight = new IGKXToolStripButton();
            btn_valignCenter = new IGKXToolStripButton();
            btn_valignLeft = new IGKXToolStripButton();
            btn_bold.ImageDocument = CoreResources.GetDocument(CoreImageKeys.BTN_BOLD_GKDS);
            btn_italic.ImageDocument = CoreResources.GetDocument(CoreImageKeys.BTN_ITALIC_GKDS);
            btn_underline.ImageDocument = CoreResources.GetDocument(CoreImageKeys.BTN_UNDERLINE_GKDS);
            btn_strikeout.ImageDocument = CoreResources.GetDocument(CoreImageKeys.BTN_STRIKEOUT_GKDS);

            //ligne align
            btn_lalignLeft.ImageDocument = CoreResources.GetDocument(CoreImageKeys.BTN_ALIGNLEFT_GKDS);
            btn_lalignCenter.ImageDocument = CoreResources.GetDocument(CoreImageKeys.BTN_ALIGNCENTER_GKDS);
            btn_lalignRight.ImageDocument = CoreResources.GetDocument(CoreImageKeys.BTN_ALIGNRIGHT_GKDS);

            //vertical align
            btn_valignRight.ImageDocument = CoreResources.GetDocument(CoreImageKeys.BTN_ALIGNRIGHT_GKDS);
            btn_valignCenter.ImageDocument = CoreResources.GetDocument(CoreImageKeys.BTN_ALIGNCENTER_GKDS);
            btn_valignLeft.ImageDocument = CoreResources.GetDocument(CoreImageKeys.BTN_ALIGNLEFT_GKDS);


            btn_valignLeft.CheckOnClick = true;
            btn_valignCenter.CheckOnClick = true;
            btn_valignRight.CheckOnClick = true;
            btn_lalignLeft.CheckOnClick = true;
            btn_lalignCenter.CheckOnClick = true;
            btn_lalignRight.CheckOnClick = true;
            this.Items.AddRange(
                new ToolStripItem[]{
                    cmb_font,
                    cmb_fontsize ,
                    btn_bold,
                    btn_italic,
                    btn_underline,
                    btn_strikeout,
                    new ToolStripSeparator (),
                    btn_lalignLeft,
                    btn_lalignCenter,
                    btn_lalignRight,
                    new ToolStripSeparator (),
                    btn_valignLeft,
                    btn_valignCenter,
                    btn_valignRight
                }
                );
            AddRemoveButton(null);
            this.btn_bold.Tag = enuFontStyle.Bold;
            this.btn_italic.Tag = enuFontStyle.Italic;
            this.btn_underline.Tag = enuFontStyle.Underline;
            this.btn_strikeout.Tag = enuFontStyle.Strikeout;
            this.btn_bold.CheckOnClick = true;
            this.btn_italic.CheckOnClick = true;
            this.btn_underline.CheckOnClick = true;
            this.btn_strikeout.CheckOnClick = true;
            EventHandler v_checkedChanged = new EventHandler(FontStyleCheckedChanged);
            this.btn_bold.CheckedChanged += v_checkedChanged;
            this.btn_italic.CheckedChanged += v_checkedChanged;
            this.btn_underline.CheckedChanged += v_checkedChanged;
            this.btn_strikeout.CheckedChanged += v_checkedChanged;
            v_checkedChanged = new EventHandler(HorizontalStyleCheckedChanged);
            this.btn_lalignRight.CheckedChanged += v_checkedChanged;
            this.btn_lalignCenter.CheckedChanged += v_checkedChanged;
            this.btn_lalignLeft.CheckedChanged += v_checkedChanged;
            this.btn_lalignRight.Tag = enuStringAlignment.Far;
            this.btn_lalignCenter.Tag = enuStringAlignment.Center;
            this.btn_lalignLeft.Tag = enuStringAlignment.Near;
            v_checkedChanged = new EventHandler(VerticalStyleCheckedChanged);
            this.btn_valignRight.CheckedChanged += v_checkedChanged;
            this.btn_valignCenter.CheckedChanged += v_checkedChanged;
            this.btn_valignLeft.CheckedChanged += v_checkedChanged;
            this.btn_valignRight.Tag = enuStringAlignment.Far;
            this.btn_valignCenter.Tag = enuStringAlignment.Center;
            this.btn_valignLeft.Tag = enuStringAlignment.Near;
            //build combobox
            this.cmb_font.ComboBox.DrawMode = DrawMode.OwnerDrawFixed;
            this.cmb_font.ComboBox.DrawItem += _DrawItemFont;
            this.cmb_font.Items.Clear();
            this.cmb_font.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmb_font.BeginUpdate();
            string[] v_t = CoreFont.GetInstalledFamilies();
            Array.Sort(v_t);
            foreach (string i in v_t)
            {
                this.cmb_font.Items.Add(i);
            }
            this.cmb_font.SelectedIndex  = 0;
            this.cmb_font.EndUpdate();
            EventHandler v_d = new EventHandler(Font_PropChanged);
            this.cmb_font.SelectedIndexChanged += v_d;
            this.cmb_fontsize.TextChanged += new EventHandler(Cmb_fontsize_TextChanged);
            this.cmb_font.AutoSize = false;
            this.cmb_font.Width = 180;
            this.cmb_fontsize.AutoSize = false;
            this.cmb_fontsize.Width = 85;
            this.ResumeLayout();
        }
        void HorizontalStyleCheckedChanged(object o, EventArgs e)
        {
            if (m_configuring) return;
            ToolStripButton btn = o as ToolStripButton;
            enuStringAlignment t = (enuStringAlignment)btn.Tag;
            if (btn.Checked)
            {
                this.m_configuring = true;
                this.m_fontHAlignment = t;
                UpdateHAlign(this.m_fontHAlignment);
                m_configuring = false;
                OnFontDefinitionChanged(EventArgs.Empty);
            }
            else {
                if (this.m_fontHAlignment == t) {
                    //maintain checked
                    m_configuring = true;
                    btn.Checked = true;
                    m_configuring = false;
                }
            }
        }
        void VerticalStyleCheckedChanged(object o, EventArgs e)
        {
            if (m_configuring) return;
            ToolStripButton btn = o as ToolStripButton;
            enuStringAlignment t = (enuStringAlignment)btn.Tag;
            if (btn.Checked)
            {
                m_configuring = true;
                this.m_fontVAlignment = t;
                UpdateVAlign(this.m_fontVAlignment);
                m_configuring = false;
                OnFontDefinitionChanged(EventArgs.Empty);
            }
            else
            {
                if (this.m_fontVAlignment == t)
                {
                    //maintain checked
                    m_configuring = true;
                    btn.Checked = true;
                    m_configuring = false;
                }
            }
        }
        void FontStyleCheckedChanged(object o, EventArgs e)
        {
            if (m_configuring) return;
            m_configuring = true;
            ToolStripButton btn = o as ToolStripButton;
            enuFontStyle t = (enuFontStyle)btn.Tag;
            FontFamily ft =(FontFamily) CoreFont.GetFonts (this.FontName).FontFamily ;
            if (btn.Checked)
            {
                t = this.m_fontStyle | t;
                if (ft.IsStyleAvailable((FontStyle)t))
                {
                    this.m_fontStyle = t;
                }
            }
            else
            {
                if ((this.m_fontStyle & t) == t)
                {
                    t = (int)this.m_fontStyle - t;
                    if (!ft.IsStyleAvailable((FontStyle)t))
                    {
                        btn.Checked = true;
                        this.m_configuring = false;
                        ft.Dispose();
                        return;
                    }
                    else
                    {
                        this.m_fontStyle = t;
                    }
                }
            }
            this.m_configuring = false;
            OnFontDefinitionChanged(EventArgs.Empty);
        }
        void _DrawItemFont(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            ComboBox cmb = sender as ComboBox;
            GraphicsState v_state = null;
            Rectangle rc = e.Bounds;
            object v_fontobj= this.cmb_font.Items[e.Index];

            string v_str = v_fontobj.ToString();

            ICoreFontInfo m = CoreFont.GetFonts(v_str);

            FontFamily fm = (m!=null)? (FontFamily)m.FontFamily : null;// m.FontFamily();// new FontFamily(v_str);
            if (fm != null)
            {
                FontStyle fstyle = global::System.Drawing.FontStyle.Regular;
                CoreFont v_sft = null;
                float v_fontsize =  e.Bounds.Height;
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
                else {
                    float em = fm.GetEmHeight(fstyle);
                    float hm = fm.GetLineSpacing(fstyle);
                    float am = fm.GetCellAscent (fstyle);
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
                    (v_fontsize+"px").ToPixel(),
                    fstyle,
                    GraphicsUnit.Pixel ))
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
        void Cmb_fontsize_TextChanged(object sender, EventArgs e)
        {
            if (this.m_configuring)
                return;
            CoreUnit t = this.cmb_fontsize.Text;
            if (t.Value > 0)
            {
                this.m_fontSize = t;
                this.DisplayUnit = this.m_fontSize.UnitType;
                OnFontDefinitionChanged(e);
            }
        }
        void Font_PropChanged(object sender, EventArgs e)
        {
            if (this.m_configuring)
                return;
            UpdateFontCap();
            this.OnFontDefinitionChanged(e);
        }
    }
}

