

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XPaletteEditorSurface.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:XPaletteEditorSurface.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
namespace IGK.DrSStudio.PaletteBuilder.WinUI
{

    using IGK.ICore.WinCore;
    using IGK.ICore.WinCore.WinUI.Controls;

    [CoreSurface(PaletteBuilderConstant.SURFACE_NAME, 
        EnvironmentName = PaletteBuilderConstant.ENVIRONMENT)]
    internal class XPaletteEditorSurface : 
        IGKXWinCoreWorkingSurface , 
        ICoreWorkingSurface 
    {
        private IGKXButton xButton1;
        private IGKXRuleLabel xRuleLabel1;
        private IGKXSimpleColorView xSimpleColorView1;
        private IGKXSimpleColorView xSimpleColorView2;
        private IGKXNumericTextBox xNumericTextBox1;
        private IGKXPanel xPanel1;
        private IGKXTextBox xTextBox1;
        private ToolTip toolTip1;
        private System.ComponentModel.IContainer components;
        private IGKXLabel xLabel1;
        private IGKXLabel xLabel2;
        private IGKXLabel xLabel3;
        private IGKXLabel xLabel4;
        private WinCorePalette m_Palette;
        private IGKXLabel xLabel5;
        private IGKXComboBox c_paletteMode;
        private bool m_configuring;
        public WinCorePalette Palette
        {
            get { return m_Palette; }
            internal set { 
                if ((this.m_Palette != value) && (value !=null)) {
                    this.m_Palette = value;              
                 }
                this.m_configuring = true;
                    this.xNumericTextBox1.Value = this.m_Palette.Colors.Length;
                    this.xTextBox1.Text = this.m_Palette.Id;
                    BuildPalette(this.m_Palette.Colors);
                this.m_configuring = false;
            }
        }
        public XPaletteEditorSurface()
        {
            InitializeComponent();
            this.m_Palette = new WinCorePalette("PaletteName");
            this.Title = "title.paletteEditorSurface".R();
            this.xPanel1.SizeChanged += new EventHandler(xPanel1_SizeChanged);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.c_paletteMode.DataSource = Enum.GetValues(typeof(enuWinCorePaletteSavingMode));
        }
        void xPanel1_SizeChanged(object sender, EventArgs e)
        {
            if (this.xPanel1.Controls.Count > 0)
            {
                UpdateChildLocation();
            }
        }
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.xButton1 = new IGKXButton();
            this.xRuleLabel1 = new IGKXRuleLabel();
            this.xTextBox1 = new IGKXTextBox();
            this.xSimpleColorView1 = new IGKXSimpleColorView();
            this.xSimpleColorView2 = new IGKXSimpleColorView();
            this.xNumericTextBox1 = new IGKXNumericTextBox();
            this.xPanel1 = new IGKXPanel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.xLabel1 = new IGKXLabel();
            this.xLabel2 = new IGKXLabel();
            this.xLabel3 = new IGKXLabel();
            this.xLabel4 = new IGKXLabel();
            this.xLabel5 = new IGKXLabel();
            this.c_paletteMode = new IGKXComboBox();
            this.SuspendLayout();
            // 
            // xButton1
            // 
            this.xButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xButton1.ButtonImageSize = new Size2i(32, 32);
            this.xButton1.CaptionKey = "btn.save.caption";
            this.xButton1.Checked = false;
            this.xButton1.DialogResult = enuDialogResult.None;
            this.xButton1.Location = new System.Drawing.Point(262, 465);
            this.xButton1.Name = "xButton1";
            this.xButton1.ShowButtonImage = false;
            this.xButton1.Size = new System.Drawing.Size(104, 37);
            this.xButton1.State = enuButtonState.Normal;
            this.xButton1.TabIndex = 0;
            this.xButton1.Click += new System.EventHandler(this.xButton1_Click);
            // 
            // xRuleLabel1
            // 
            this.xRuleLabel1.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.xRuleLabel1.CaptionKey = null;
            this.xRuleLabel1.Location = new System.Drawing.Point(0, 153);
            this.xRuleLabel1.Name = "xRuleLabel1";
            this.xRuleLabel1.Size = new System.Drawing.Size(363, 11);
            this.xRuleLabel1.TabIndex = 1;
            // 
            // xTextBox1
            // 
            this.xTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.xTextBox1.Location = new System.Drawing.Point(191, 14);
            this.xTextBox1.Name = "xTextBox1";
            this.xTextBox1.Size = new System.Drawing.Size(164, 20);
            this.xTextBox1.TabIndex = 2;
            this.xTextBox1.Text = "Palette";
            this.xTextBox1.TextChanged += new System.EventHandler(this.xTextBox1_TextChanged);
            // 
            // xSimpleColorView1
            // 
            this.xSimpleColorView1.Color = Colorf.Red;
            this.xSimpleColorView1.Location = new System.Drawing.Point(191, 40);
            this.xSimpleColorView1.Name = "xSimpleColorView1";
            this.xSimpleColorView1.Selected = false;
            this.xSimpleColorView1.Size = new System.Drawing.Size(75, 23);
            this.xSimpleColorView1.TabIndex = 3;
            this.xSimpleColorView1.Click += new System.EventHandler(this.xSimpleColorView1_Click);
            // 
            // xSimpleColorView2
            // 
            this.xSimpleColorView2.Color = Colorf.Black;
            this.xSimpleColorView2.Location = new System.Drawing.Point(191, 69);
            this.xSimpleColorView2.Name = "xSimpleColorView2";
            this.xSimpleColorView2.Selected = false;
            this.xSimpleColorView2.Size = new System.Drawing.Size(75, 23);
            this.xSimpleColorView2.TabIndex = 4;
            this.xSimpleColorView2.Click += new System.EventHandler(this.xSimpleColorView2_Click);
            // 
            // xNumericTextBox1
            // 
            this.xNumericTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.xNumericTextBox1.Location = new System.Drawing.Point(191, 98);
            this.xNumericTextBox1.MaxLength = 10;
            this.xNumericTextBox1.Name = "xNumericTextBox1";
            this.xNumericTextBox1.Size = new System.Drawing.Size(163, 20);
            this.xNumericTextBox1.TabIndex = 5;
            this.xNumericTextBox1.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.xNumericTextBox1.ValueChanged += new System.EventHandler(this.xNumericTextBox1_ValueChanged);
            // 
            // xPanel1
            // 
            this.xPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.xPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.xPanel1.Location = new System.Drawing.Point(6, 169);
            this.xPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.xPanel1.Name = "xPanel1";
            this.xPanel1.Padding = new System.Windows.Forms.Padding(3);
            this.xPanel1.Size = new System.Drawing.Size(359, 286);
            this.xPanel1.TabIndex = 6;
            // 
            // xLabel1
            // 
            this.xLabel1.CaptionKey = "lb.startColor.caption";
            this.xLabel1.Location = new System.Drawing.Point(17, 43);
            this.xLabel1.Name = "xLabel1";
            this.xLabel1.Size = new System.Drawing.Size(120, 20);
            this.xLabel1.TabIndex = 7;
            // 
            // xLabel2
            // 
            this.xLabel2.CaptionKey = "lb.name.caption";
            this.xLabel2.Location = new System.Drawing.Point(17, 14);
            this.xLabel2.Name = "xLabel2";
            this.xLabel2.Size = new System.Drawing.Size(120, 20);
            this.xLabel2.TabIndex = 8;
            // 
            // xLabel3
            // 
            this.xLabel3.CaptionKey = "lb.endColor.caption";
            this.xLabel3.Location = new System.Drawing.Point(17, 69);
            this.xLabel3.Name = "xLabel3";
            this.xLabel3.Size = new System.Drawing.Size(120, 20);
            this.xLabel3.TabIndex = 9;
            // 
            // xLabel4
            // 
            this.xLabel4.CaptionKey = "lb.ColorNbr.caption";
            this.xLabel4.Location = new System.Drawing.Point(17, 98);
            this.xLabel4.Name = "xLabel4";
            this.xLabel4.Size = new System.Drawing.Size(120, 20);
            this.xLabel4.TabIndex = 10;
            // 
            // xLabel5
            // 
            this.xLabel5.CaptionKey = "lb.SaveAsMode.caption";
            this.xLabel5.Location = new System.Drawing.Point(17, 129);
            this.xLabel5.Name = "xLabel5";
            this.xLabel5.Size = new System.Drawing.Size(120, 20);
            this.xLabel5.TabIndex = 11;
            // 
            // xComboBox1
            // 
            this.c_paletteMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.c_paletteMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.c_paletteMode.FormattingEnabled = true;
            this.c_paletteMode.Location = new System.Drawing.Point(191, 129);
            this.c_paletteMode.Name = "xComboBox1";
            this.c_paletteMode.Size = new System.Drawing.Size(163, 21);
            this.c_paletteMode.TabIndex = 12;
            this.c_paletteMode.SelectedIndexChanged += new System.EventHandler(this.xComboBox1_SelectedIndexChanged);
            // 
            // XPaletteEditorSurface
            // 
            this.Controls.Add(this.c_paletteMode);
            this.Controls.Add(this.xLabel5);
            this.Controls.Add(this.xLabel4);
            this.Controls.Add(this.xLabel3);
            this.Controls.Add(this.xLabel2);
            this.Controls.Add(this.xLabel1);
            this.Controls.Add(this.xPanel1);
            this.Controls.Add(this.xNumericTextBox1);
            this.Controls.Add(this.xSimpleColorView2);
            this.Controls.Add(this.xSimpleColorView1);
            this.Controls.Add(this.xTextBox1);
            this.Controls.Add(this.xRuleLabel1);
            this.Controls.Add(this.xButton1);
            this.Name = "XPaletteEditorSurface";
            this.Size = new System.Drawing.Size(369, 514);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private void xButton1_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.FileName = this.m_Palette.FileName;
                sfd.Filter = "Filter.Palette".R() + "|*.gkpal";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    this.m_Palette.SaveTo(sfd.FileName);
                }
            }
        }
        private void xSimpleColorView1_Click(object sender, EventArgs e)
        {
            ChooseColor(this.xSimpleColorView1);
        }
        private void xSimpleColorView2_Click(object sender, EventArgs e)
        {
            ChooseColor(this.xSimpleColorView2);
        }
        private void ChooseColor(IGKXSimpleColorView ColorView)
        {
            using (ColorDialog clDiag = new ColorDialog())
            {
                if (clDiag.ShowDialog() == DialogResult.OK)
                {
                    ColorView.Color = clDiag.Color.CoreConvertFrom<Colorf>();
                    BuildPalette();
                }
            }
        }
        public Colorf StartColor { get { return this.xSimpleColorView1.Color; } }
        public Colorf EndColor { get { return this.xSimpleColorView2.Color; } }
        public int Number { get { return (int)this.xNumericTextBox1.Value; } }
        void BuildPalette(Colorf[] v_clTab)
        {
            IGKXSimpleColorSelector[] v_colors = new IGKXSimpleColorSelector[v_clTab.Length];
            int v_yoffset = 0;
            int v_xoffset = 0;
            IGKXSimpleColorSelector i = null;
            for (int j = 0; j < v_colors.Length; j++)
            {
                i = new IGKXSimpleColorSelector();
                i.Click += ChangeColor;
                i.Color = v_clTab[j];// System.Drawing.Color.FromArgb((int)c);
                i.Tag = j;
                // i.Owner = this;
                i.ColorName = i.Color.GetName();
                i.Location = new Point(16 + (v_xoffset * 18), v_yoffset + 3);
                this.toolTip1.SetToolTip(i, i.Color.GetName());
                v_colors[j] = i;
                v_xoffset++;
                if (((v_xoffset * 18) + 32) >= this.xPanel1.Width)
                {
                    v_yoffset += 18;
                    v_xoffset = 0;
                }
            }
            this.m_Palette.Colors = v_clTab;
            this.xPanel1.SuspendLayout();
            this.xPanel1.Controls.Clear();
            this.xPanel1.Controls.AddRange(v_colors);
            this.xPanel1.ResumeLayout();
        }
        void ChangeColor(object sender, EventArgs e)
        {
            IGKXSimpleColorSelector cl = sender as IGKXSimpleColorSelector;
            int index = (int)cl.Tag ;
            using (ColorDialog cldial = new ColorDialog())
            {
                cldial.Color = cl.Color.ToGdiColor();
                if (cldial.ShowDialog() == DialogResult.OK)
                {
                    cl.Color = cldial.Color.CoreConvertTo<Colorf>();
                    this.m_Palette.Colors[index] = cl.Color;
                }
            }
        }
        void BuildPalette()
        {
            Colorf[] v_clTab = GetPallette(StartColor, EndColor, Number);
            BuildPalette(v_clTab);
        }
        private void UpdateChildLocation()
        {
            int v_yoffset = 0;
            int v_xoffset = 0;
            this.xPanel1.SuspendLayout();
            foreach (Control item in this.xPanel1.Controls)
            {
                item.Location = new Point(16 + (v_xoffset * 18), v_yoffset + 3);
                v_xoffset++;
                if (((v_xoffset*18) + 32) >= this.xPanel1.Width)
                {
                    v_yoffset += 18;
                    v_xoffset = 0;
                }
            }
            this.xPanel1.ResumeLayout();
        }
        private Colorf[] GetPallette(Colorf StartColor, Colorf EndColor, int Number)
        {
            int step = Number;
            List<Colorf> m_color = new List<Colorf>();
            using (Bitmap bmp = new Bitmap(2, 2))
            {
                Rectangle rc = new Rectangle(0, 0, 2, 2);
                SolidBrush sb = new SolidBrush(StartColor.ToGdiColor());
                SolidBrush fb = new SolidBrush(Colorf.FromFloat(0, EndColor).ToGdiColor());
                float a = 0;
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    for (int i = 1; i <= step; i++)
                    {
                        g.FillRectangle(sb, rc);
                        g.FillRectangle(fb, rc);
                        a = fb.Color.A;
                        a = i / (float)step;
                        fb.Color = Colorf.FromFloat(a, EndColor).ToGdiColor();
                        m_color.Add(bmp.GetPixel(0, 0).CoreConvertFrom<Colorf>());
                    }
                }
            }
            return m_color.ToArray();
        }
        private void xNumericTextBox1_ValueChanged(object sender, EventArgs e)
        {
            if (!this.m_configuring && this.xNumericTextBox1.Value > 0)
                this.BuildPalette();
        }
        private void xTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.xTextBox1.Text))
            {
                this.m_Palette.Id = this.xTextBox1.Text;
            }
        }
        private void XPaletteEditorSurface_Load(object sender, EventArgs e)
        {
            //init saved mode
            this.m_configuring = true;
            this.c_paletteMode.DataSource = Enum.GetValues(typeof(enuWinCorePaletteSavingMode));
            this.c_paletteMode.SelectedItem = m_Palette.SavingMode;
            BuildPalette();
            this.m_configuring = false;
        }
        private void xComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_configuring)
                return;
            this.m_configuring = true;
            this.m_Palette.SavingMode = (enuWinCorePaletteSavingMode)this.c_paletteMode.SelectedItem;
            BuildPalette();
            this.m_configuring = false;
        }
    }
}

