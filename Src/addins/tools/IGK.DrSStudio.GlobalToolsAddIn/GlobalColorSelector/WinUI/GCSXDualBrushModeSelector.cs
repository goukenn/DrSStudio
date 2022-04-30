

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GCSXDualBrushModeSelector.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Drawing2D;
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
    /// Dual brush Mode Selector
    /// </summary>
    public class GCSXDualBrushModeSelector : IGKXUserControl 
    {
        private enuBrushMode m_BrushMode;
        private GCSXBrushSelector c_fillColor;
        private GCSXBrushSelector c_foreColor;

        /// <summary>
        /// enabled fillbrush
        /// </summary>
        public bool EnabledFillBrush {
            get {
                return c_fillColor.Enabled;
            }
            set {
                c_fillColor.Enabled = value;
            }
        }
        /// <summary>
        /// enabled stroke brush
        /// </summary>
        public bool EnabledStrokeBrush
        {
            get
            {
                return c_foreColor.Enabled;
            }
            set
            {
                c_foreColor.Enabled = value;
            }
        }
        
        /// <summary>
        /// get brush mode
        /// </summary>
        public enuBrushMode BrushMode
        {
            get { return m_BrushMode; }
            set
            {
                if (m_BrushMode != value)
                {
                    m_BrushMode = value;
                    OnBrushModeChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler BrushModeChanged;
        ///<summary>
        ///raise the BrushModeChanged 
        ///</summary>
        protected virtual void OnBrushModeChanged(EventArgs e)
        {
            if (BrushModeChanged != null)
                BrushModeChanged(this, e);
        }

        public GCSXDualBrushModeSelector()
        {
            this.InitializeComponent();
            this.c_fillColor.Click+=_fillColor_Click;
            this.c_foreColor.Click += c_foreColor_Click;
            this.SetStyle(ControlStyles.FixedHeight | ControlStyles.FixedWidth | ControlStyles.SupportsTransparentBackColor , true);
            this.BackColor = Color.Transparent;
        }

        void c_foreColor_Click(object sender, EventArgs e)
        {
            this.BrushMode = enuBrushMode.Stroke;
            this.c_foreColor.Selected = true;
            this.c_fillColor.Selected = false;
        }

        private void _fillColor_Click(object sender, EventArgs e)
        {
            this.BrushMode = enuBrushMode.Fill;
            this.c_foreColor.Selected = false;
            this.c_fillColor.Selected = true;
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.c_fillColor = new GCSXBrushSelector();
            this.c_foreColor = new GCSXBrushSelector();
            this.SuspendLayout();
            // 
            // c_fillColor
            // 
            
            this.c_fillColor.CaptionKey = null;
            this.c_fillColor.Location = new System.Drawing.Point(4, 4);
            this.c_fillColor.Name = "c_fillColor";
            this.c_fillColor.Size = new System.Drawing.Size(32, 32);
            this.c_fillColor.TabIndex = 0;
            // 
            // c_foreColor
            //             
            this.c_foreColor.CaptionKey = null;
            this.c_foreColor.Location = new System.Drawing.Point(25, 23);
            this.c_foreColor.Name = "c_foreColor";
            this.c_foreColor.Size = new System.Drawing.Size(16, 16);
            this.c_foreColor.TabIndex = 1;
            // 
            // GCSXDualBrushModeSelector
            // 
            this.Controls.Add(this.c_foreColor);
            this.Controls.Add(this.c_fillColor);
            this.Name = "GCSXDualBrushModeSelector";
            this.Size = new System.Drawing.Size(48, 48);
            this.ResumeLayout(false);

        }
        
        internal void UpdateBrush(enuBrushMode enuBrushMode, ICoreBrush coreBrush)
        {
            switch (enuBrushMode)
	        {
		        case enuBrushMode.Fill:
                    this.c_fillColor.Brush.Copy(coreBrush);
                    this.c_fillColor.Invalidate();
                    break;
                case enuBrushMode.Stroke:
                    this.c_foreColor.Brush.Copy(coreBrush);
                    this.c_foreColor.Invalidate();
                 break;
                default:
                 break;
	        }    
        }
    }
}
