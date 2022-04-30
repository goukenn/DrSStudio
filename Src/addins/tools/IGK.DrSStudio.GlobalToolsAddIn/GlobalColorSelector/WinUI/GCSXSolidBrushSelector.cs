

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GCSXSolidBrushSelector.cs
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
file:SolidBrushSelector.cs
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
using System.Windows.Forms ;
using System.Drawing;
namespace IGK.DrSStudio.WinUI
{
    using IGK.ICore.Drawing2D;
    using IGK.ICore.Resources;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinCore.WinUI.Controls;
    class GCSXSolidBrushSelector : GCSXBrushConfigureBase 
    {
        private IGKXToolStrip xToolStrip1;
        private Colorf m_Color;
        private IGKXRuleLabel xRuleLabel1;
        private ToolTip toolTip1;
        private System.ComponentModel.IContainer components;
        private IGKXRuleLabel xRuleLabel2;
        private IGKXToolStripButton c_savePalette;
        private IGKXToolStripButton c_selectPalette;
        private IGKXToolStripButton c_removePalette;
        private WinCorePalette m_Palette;
        /// <summary>
        /// get the palette
        /// </summary>
        public WinCorePalette Palette
        {
            get { return m_Palette; }
            set
            {
                if (m_Palette != value)
                {
                    m_Palette = value;
                }
            }
        }
        public Colorf Color
        {
            get { return m_Color; }
            set
            {
                if (!m_Color.Equals ( value))
                {
                    m_Color = value;
                    OnColorChanged(EventArgs.Empty);
                    //(this.ColorOwner as GCColorBrushSelector).Color = value;
                }
            }
        }
        public event EventHandler ColorChanged;
        ///<summary>
        ///raise the ColorChanged 
        ///</summary>
        protected virtual void OnColorChanged(EventArgs e)
        {
            if (ColorChanged != null)
                ColorChanged(this, e);
        }

      
        public GCSXSolidBrushSelector()
        {
            this.InitializeComponent();
            this.AutoScroll = true;
            this.c_savePalette.ImageDocument = CoreResources.GetDocument(CoreImageKeys.MENU_SAVE_GKDS);
            this.c_selectPalette.ImageDocument = CoreResources.GetDocument(CoreImageKeys.BTN_FOLDER_GKDS);
            this.c_removePalette.ImageDocument = CoreResources.GetDocument(CoreImageKeys.BTN_CLEAR_GKDS);
            this.c_savePalette.Click += new EventHandler(c_savePalette_Click);
            this.c_selectPalette.Click += new EventHandler(c_selectPalette_Click);
            this.c_removePalette.Click += new EventHandler(c_removePalette_Click);
        }
        void c_removePalette_Click(object sender, EventArgs e)
        {
            this.RemovePalette();
        }
        void c_selectPalette_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog  ofd = new OpenFileDialog())
            {
                ofd.Title = CoreSystem.GetString("DLG.OpenPalette");
                if (this.Palette != null)
                    ofd.FileName = this.Palette.FileName;
                ofd.Filter = "IGK DrSStudio Palette | *.gkpal";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    WinCorePalette pal = WinCorePalette.LoadFile(ofd.FileName);
                    if (pal != null)
                    {
                        this.Palette = pal;
                        this.LoadPaletteColor();
                    }
                }
            }
        }
        void RemovePalette()
        {
            if (this.Palette != null)
            {
                this.Palette = null;
                this.LoadWebColors();
            }
        }
        void c_savePalette_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog()) 
            {
                sfd.Title = CoreSystem.GetString ( "DLG.SAVEPALETTE");
                sfd.Filter = "IGK DrSStudio Palette | *.gkpal";
                if (sfd.ShowDialog () == DialogResult.OK )
                {
                    if (this.Palette == null)
                    {
                        Colorf[] cols = new Colorf[m_WebColors.Length];
                        for (int i = 0; i < cols.Length; i++)
			{
                            cols [i]= m_WebColors[i].Color ;
			}
                        WinCorePalette pal = new WinCorePalette("Palette", cols);
                        pal.SaveTo(sfd.FileName);
                    }
                    else
                    {
                        this.Palette.SaveTo(sfd.FileName);
                        this.Palette.FileName = sfd.FileName;
                    }
                }
            }
        }
        protected override void ConfigureBrush()
        {
           this.BrushToConfigure .SetSolidColor (this.m_Color);
        }
        public override void SetColor(Colorf color)
        {
            this.m_Color = color;
            ConfigureBrush();
        }
        protected override  void ConfigFromBrush(ICoreBrush value)
        {
            this.m_Color = this.BrushToConfigure.Colors[0];
        }
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.c_selectPalette = new IGKXToolStripButton();
            this.c_savePalette = new IGKXToolStripButton();
            this.c_removePalette = new IGKXToolStripButton();
            this.xToolStrip1 = new IGKXToolStrip();
            this.xRuleLabel1 = new IGKXRuleLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.xRuleLabel2 = new IGKXRuleLabel();
            this.xToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // c_selectPalette
            // 
            this.c_selectPalette.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.c_selectPalette.ImageDocument = null;
            this.c_selectPalette.Name = "c_selectPalette";
            this.c_selectPalette.Size = new System.Drawing.Size(23, 22);
            // 
            // c_savePalette
            // 
            this.c_savePalette.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.c_savePalette.ImageDocument = null;
            this.c_savePalette.Name = "c_savePalette";
            this.c_savePalette.Size = new System.Drawing.Size(23, 22);
            // 
            // c_removePalette
            // 
            this.c_removePalette.ImageDocument = null;
            this.c_removePalette.Name = "c_removePalette";
            this.c_removePalette.Size = new System.Drawing.Size(23, 22);
            // 
            // xToolStrip1
            // 
            this.xToolStrip1.CaptionKey = null;
            this.xToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.c_savePalette,
            this.c_selectPalette,
            this.c_removePalette});
            this.xToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.xToolStrip1.Name = "xToolStrip1";
            this.xToolStrip1.Size = new System.Drawing.Size(0, 25);
            this.xToolStrip1.TabIndex = 0;
            // 
            // xRuleLabel1
            // 
            this.xRuleLabel1.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xRuleLabel1.CaptionKey = "lb.Rainbow.caption";
            this.xRuleLabel1.Location = new System.Drawing.Point(0, 45);
            this.xRuleLabel1.Name = "xRuleLabel1";
            this.xRuleLabel1.Size = new System.Drawing.Size(0, 32);
            this.xRuleLabel1.TabIndex = 1;
            this.xRuleLabel1.TabStop = false;
            // 
            // xRuleLabel2
            // 
            this.xRuleLabel2.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xRuleLabel2.CaptionKey = null;
            this.xRuleLabel2.Location = new System.Drawing.Point(1, 103);
            this.xRuleLabel2.Name = "xRuleLabel2";
            this.xRuleLabel2.Size = new System.Drawing.Size(0, 32);
            this.xRuleLabel2.TabIndex = 2;
            this.xRuleLabel2.TabStop = false;
            // 
            // GCSXSolidBrushSelector
            // 
            this.Controls.Add(this.xRuleLabel2);
            this.Controls.Add(this.xRuleLabel1);
            this.Controls.Add(this.xToolStrip1);
            this.Name = "GCSXSolidBrushSelector";
            this.Size = new System.Drawing.Size(0, 0);
            this.Load += new System.EventHandler(this.SolidBrushSelector_Load);
            this.xToolStrip1.ResumeLayout(false);
            this.xToolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #region IBrushSelector Members
        public ICoreBrush GetEditableBrush()
        {
            throw new NotImplementedException();
        }
        #endregion
        private void SolidBrushSelector_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
            this.LoadRainBow();
            this.LoadWebColors();
        }
        XISimpleColorSelector[] m_RainBowColor;
        XISimpleColorSelector[] m_WebColors;
        private void LoadRainBow()
        {
            XISimpleColorSelector i = null;
            Array t = Enum.GetValues (typeof (enuRainbowColor ));
            m_RainBowColor = new XISimpleColorSelector[t.Length ];
            long  v_value = 0;
            for (int j = 0; j < t.Length; j++)
			{
                i  = new XISimpleColorSelector ();
                v_value =Convert.ToInt64 (t.GetValue(j));
                i.Color = Colorf.FromIntArgb ((int)v_value );
                i.Owner = this;
                i.ColorName = t.GetValue(j).ToString();
                i.Location = new Point(16 + j * 18, xRuleLabel1.Bottom + 2);
                this.toolTip1.SetToolTip(i, i.Color.GetName());
                m_RainBowColor[j] = i;
            }
            this.SuspendLayout();
            this.Controls.AddRange(m_RainBowColor);
            this.ResumeLayout();
        }
        private void LoadWebColors()
        {
            XISimpleColorSelector i = null;
            Array t = Enum.GetNames (typeof(enuCoreWebColors  ));
            m_WebColors = new XISimpleColorSelector[t.Length];
            enuCoreWebColors c = enuCoreWebColors.Transparent;
            int v_yoffset = 0;
            int v_xoffset = 0;
            for (int j = 0; j < t.Length; j++)
            {
                c = (enuCoreWebColors)Enum.Parse(typeof(enuCoreWebColors), t.GetValue(j).ToString());
                i = new XISimpleColorSelector();
                i.Color =Colorf.FromIntArgb((int)c);
                i.Owner = this;
                i.ColorName = t.GetValue(j).ToString();
                i.Location = new Point(16 + (v_xoffset* 18), xRuleLabel2.Bottom + v_yoffset + 3);
                this.toolTip1.SetToolTip(i, i.Color.GetName());
                m_WebColors[j] = i;
                v_xoffset++;
                if ((v_xoffset % 15) == 0)
                {
                    v_yoffset += 18;
                    v_xoffset = 0;
                }
            }
            this.SuspendLayout();
            this.Controls.AddRange(m_WebColors);
            this.xRuleLabel2.CaptionKey = "lb.WebColors.caption";
            this.ResumeLayout();
        }
        public void LoadPaletteColor()
        {
            if (this.Palette == null)
                return;
            XISimpleColorSelector i = null;
            //remove color
            if (m_WebColors != null)
            {
                foreach (var m in m_WebColors)
                {
                    this.Controls.Remove(m);
                }
            }
            m_WebColors = new XISimpleColorSelector[Palette.Colors.Length ];
            int v_yoffset = 0;
            int v_xoffset = 0;
            for (int j = 0; j < m_WebColors.Length; j++)
            {
                i = new XISimpleColorSelector();
                i.Color = Palette.Colors[j];// Colorf.FromArgb((int)c);
                i.Owner = this;
                i.ColorName = i.Color.GetName();
                i.Location = new Point(16 + (v_xoffset * 18), xRuleLabel2.Bottom + v_yoffset + 3);
                this.toolTip1.SetToolTip(i, i.Color.GetName());
                m_WebColors[j] = i;
                v_xoffset++;
                if ((v_xoffset % 15) == 0)
                {
                    v_yoffset += 18;
                    v_xoffset = 0;
                }
            }
            this.SuspendLayout();
            this.Controls.AddRange(m_WebColors);
            this.xRuleLabel2.CaptionKey = "lb.Palette.caption";
            this.ResumeLayout();
        }
        /// <summary>
        /// represent a simple color selector
        /// </summary>
        class XISimpleColorSelector  : IGKXSimpleColorSelector 
        {
            private GCSXSolidBrushSelector m_Owner;
            public GCSXSolidBrushSelector Owner
            {
                get { return m_Owner; }
                set
                {
                    if (m_Owner != value)
                    {
                        m_Owner = value;
                    }
                }
            }
            public XISimpleColorSelector():base()
            {
                this.SetStyle(ControlStyles.FixedHeight, true);
                this.SetStyle(ControlStyles.FixedWidth , true);
                this.SetStyle(ControlStyles.ResizeRedraw, true);
                this.SetStyle(ControlStyles.UserPaint, true);
                this.Size = new System.Drawing.Size(16, 16);
                this.Click += new EventHandler(XSimpleColorSelector_Click);
            }
            void XSimpleColorSelector_Click(object sender, EventArgs e)
            {
                if (this.Owner != null)
                {
                    this.Owner.Color = this.Color;
                }
            }
            protected override void  ShowToolTip()
{
 	            if (this.Owner != null)
                {
                    //this.Owner.toolTip1.SetToolTip(this, Color.GetName());
                    //this.Owner.toolTip1.Show(Color.GetName(), this);
                    string v_str = this.ColorName;// Color.GetName();
                    if (string.IsNullOrEmpty(v_str))
                    {
                        v_str = Colorf.ConvertToString(Color);
                    }
                    this.Owner.toolTip1.Show(v_str ,
                        this, new Point(10, 10),
                        2000);
                }
            }
    protected override void  HideToolTip()
{
 	            if (this.Owner != null)
                {
                    this.m_Owner.toolTip1.Hide(this);
                    //if (this.Owner.toolTip1.Active)
                    //    this.Owner.toolTip1.Active = false;
                }
            }
        }
    }
}

