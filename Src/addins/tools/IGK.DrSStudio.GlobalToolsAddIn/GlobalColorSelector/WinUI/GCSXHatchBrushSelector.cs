

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GCSXHatchBrushSelector.cs
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
file:HatchBrushSelector.cs
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
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D ;
namespace IGK.DrSStudio.WinUI
{
    using IGK.ICore.Tools;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinCore.WinUI.Controls;
    /// <summary>
    /// represent a hatch brush designer
    /// </summary>
    internal class HatchBrushSelector :
        GCSXBrushConfigureBase,
        IBrushSelector
    {
        private IGKXComboBox cmb_hatchstyle;
        private IGKXLabel xLabel2;
        private IGKXLabel xLabel3;
        private IGKXSimpleColorView c_foreColor;
        private IGKXSimpleColorView c_backColor;
        private IGKXLabel xLabel1;
        private bool m_FrontMode;
        private bool m_configuring;
       
        public enuHatchStyle HatchStyle { get { return (enuHatchStyle)this.cmb_hatchstyle.SelectedItem; } }
        public Colorf HatchFrontColor { get { return c_foreColor.Color; } }
        public Colorf HatchBackColor { get { return c_backColor.Color; } }
        public bool FrontMode
        {
            get { return m_FrontMode; }
            set
            {
                if (m_FrontMode != value)
                {
                    m_FrontMode = value;
                }
            }
        }
     
        protected override void OnBrushToConfigureChanged(EventArgs eventArgs)
        {
            if (this.Enabled)
            {
                ConfigureFrom(this.BrushToConfigure);
                ConfigureBrush();
            }
            base.OnBrushToConfigureChanged(eventArgs);
        }
        public override void SetColor(Colorf color) {
            if (m_FrontMode)
            {
                this.c_foreColor.Color = color;
            }
            else
                this.c_backColor.Color = color;
            ConfigureBrush();
        }
        public HatchBrushSelector()
        {
            this.InitializeComponent ();
            this.c_backColor.MouseClick += c_backColor_MouseClick;
            this.c_foreColor.MouseClick += c_foreColor_MouseClick;
        }
        void c_foreColor_MouseClick(object sender, CoreMouseEventArgs e)
        {
            if (e.Button == enuMouseButtons.Right)
            {
                PicColor(c_foreColor);
            } 
        }
        void c_backColor_MouseClick(object sender, CoreMouseEventArgs e)
        {
            if (e.Button == enuMouseButtons.Right)
            {
                PicColor(c_backColor);
            }
        }
        private void PicColor(IGKXSimpleColorView cl)
        {
            using (ColorDialog cldial = new ColorDialog())
            {
                cldial.Color = cl.Color.CoreConvertTo<Color>();
                if (cldial.ShowDialog() == DialogResult.OK)
                {
                    this.SetColor (cldial.Color.CoreConvertFrom<Colorf>());
                }
            }
        }
        private void InitControl()
        {
            this.cmb_hatchstyle.Items.Clear();            
            foreach (System.Drawing.Drawing2D.HatchStyle v_str in Enum.GetValues(typeof(System.Drawing.Drawing2D.HatchStyle)))
            {
                if (!this.cmb_hatchstyle.Items.Contains(v_str))
                {
                    this.cmb_hatchstyle.Items.Add(v_str);
                }
            }
            this.cmb_hatchstyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_hatchstyle.SelectedIndex = 0;
            this.cmb_hatchstyle.SelectedIndexChanged += new EventHandler(cmb_hatchstyle_SelectedIndexChanged);
            this.m_FrontMode = true;
            this.c_backColor.Enabled = true;
            this.c_foreColor.Enabled = false;
        }
        void cmb_hatchstyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConfigureBrush();
        }
        protected override void ConfigFromBrush(ICoreBrush value)
        {
            base.ConfigFromBrush(value);
        }
        protected  override void ConfigureBrush()
        {
            if ((this.ColorOwner == null) || ((this.ColorOwner.BrushSupport & enuBrushSupport.Hatch) != enuBrushSupport.Hatch))
                return;
            if (this.m_configuring)
                return;
            ICoreBrush br = this.BrushToConfigure;
            if (br == null)
                return;
            if (br != null)
            {
                br.SetHatchBrush(
                    this.HatchFrontColor ,
                    this.HatchBackColor ,
                    this.HatchStyle );
            }
        }
        public void ConfigureFrom(ICoreBrush br)
        {
            if (br == null) return;
            if (br.BrushType == enuBrushType.Hatch)
            {
                this.m_configuring = true;
                this.c_foreColor.Color = br.Colors[0];
                this.c_backColor.Color = br.Colors[1];
                this.cmb_hatchstyle.SelectedItem = br.HatchStyle;
                this.m_configuring = false;
            }
        }
        private void InitializeComponent()
        {
            this.xLabel1 = new IGKXLabel();
            this.cmb_hatchstyle = new IGKXComboBox();
            this.xLabel2 = new IGKXLabel();
            this.xLabel3 = new IGKXLabel();
            this.c_foreColor = new IGKXSimpleColorView();
            this.c_backColor = new IGKXSimpleColorView();
            this.SuspendLayout();
            // 
            // xLabel1
            // 
            this.xLabel1.CaptionKey = "lb.HatchStyle.caption";
            this.xLabel1.Location = new System.Drawing.Point(13, 14);
            this.xLabel1.Name = "xLabel1";
            this.xLabel1.Size = new System.Drawing.Size(58, 14);
            this.xLabel1.TabIndex = 0;
            this.xLabel1.TabStop = false;
            // 
            // cmb_hatchstyle
            // 
            this.cmb_hatchstyle.FormattingEnabled = true;
            this.cmb_hatchstyle.Location = new System.Drawing.Point(100, 13);
            this.cmb_hatchstyle.Name = "cmb_hatchstyle";
            this.cmb_hatchstyle.Size = new System.Drawing.Size(116, 21);
            this.cmb_hatchstyle.TabIndex = 1;
            // 
            // xLabel2
            // 
            this.xLabel2.CaptionKey = "lb.foreColor.caption";
            this.xLabel2.Location = new System.Drawing.Point(13, 51);
            this.xLabel2.Name = "xLabel2";
            this.xLabel2.Size = new System.Drawing.Size(61, 13);
            this.xLabel2.TabIndex = 2;
            this.xLabel2.TabStop = false;
            // 
            // xLabel3
            // 
            this.xLabel3.CaptionKey = "lb.backgroundColor.caption";
            this.xLabel3.Location = new System.Drawing.Point(13, 80);
            this.xLabel3.Name = "xLabel3";
            this.xLabel3.Size = new System.Drawing.Size(61, 13);
            this.xLabel3.TabIndex = 3;
            this.xLabel3.TabStop = false;
            // 
            // c_foreColor
            // 
            this.c_foreColor.CaptionKey = null;
            this.c_foreColor.Color = Colorf.FromFloat(0F, 0F, 0F, 0F);
            this.c_foreColor.Location = new System.Drawing.Point(100, 40);
            this.c_foreColor.MinimumSize = new System.Drawing.Size(16, 16);
            this.c_foreColor.Name = "c_foreColor";
            this.c_foreColor.Selected = false;
            this.c_foreColor.Size = new System.Drawing.Size(32, 32);
            this.c_foreColor.TabIndex = 4;
            this.c_foreColor.Click += new System.EventHandler(this.frontcolor_Click);
            // 
            // c_backColor
            // 
            this.c_backColor.CaptionKey = null;
            this.c_backColor.Color = Colorf.FromFloat(0F, 0F, 0F, 0F);
            this.c_backColor.Location = new System.Drawing.Point(100, 80);
            this.c_backColor.MinimumSize = new System.Drawing.Size(16, 16);
            this.c_backColor.Name = "c_backColor";
            this.c_backColor.Selected = false;
            this.c_backColor.Size = new System.Drawing.Size(32, 32);
            this.c_backColor.TabIndex = 5;
            this.c_backColor.Click += new System.EventHandler(this.backColor_Click);
            // 
            // HatchBrushSelector
            // 
            this.Controls.Add(this.c_backColor);
            this.Controls.Add(this.c_foreColor);
            this.Controls.Add(this.xLabel3);
            this.Controls.Add(this.xLabel2);
            this.Controls.Add(this.cmb_hatchstyle);
            this.Controls.Add(this.xLabel1);
            this.Name = "HatchBrushSelector";
            this.Size = new System.Drawing.Size(242, 198);
            this.Load += new System.EventHandler(this.HatchBrushSelector_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private void frontcolor_Click(object sender, EventArgs e)
        {
            this.m_FrontMode = true;
            this.c_backColor.Enabled = true;
            this.c_foreColor.Enabled = true ;
            if (this.ColorOwner != null)
            {
                this.ColorOwner.SetColor(this.HatchFrontColor);
            }
        }
        private void backColor_Click(object sender, EventArgs e)
        {
            this.m_FrontMode = false;
            this.c_backColor.Enabled = true;
            this.c_foreColor.Enabled = true;
            if (this.ColorOwner != null)
            {
                this.ColorOwner.SetColor(this.HatchBackColor);
            }
        }
        #region IBrushSelector Members
        public ICoreBrush GetEditableBrush()
        {
            return null;
        }
        #endregion
        private void HatchBrushSelector_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
            this.InitControl();
        }
    }
}

