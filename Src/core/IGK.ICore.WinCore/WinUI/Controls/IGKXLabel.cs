

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXLabel.cs
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
file:IGKXLabel.cs
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.ICore.WinCore.WinUI.Controls
{
    using IGK.ICore;
    using IGK.ICore.WinCore.WinUI.Controls;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.GraphicModels;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinCore;
    /// <summary>
    /// represent the default label
    /// </summary>
    public class IGKXLabel : IGKXControl, ICoreControl
    {

        private enuStringAlignment m_HorizontalAlignment;
        private enuStringAlignment m_VerticalAlignment;
        private bool m_configuring;

        public enuStringAlignment VerticalAlignment
        {
            get { return m_VerticalAlignment; }
            set
            {
                if (m_VerticalAlignment != value)
                {
                    m_VerticalAlignment = value;
                    this.TrimSize();
                    this.Refresh();
                }
            }
        }
        public enuStringAlignment HorizontalAlignment
        {
            get { return m_HorizontalAlignment; }
            set
            {
                if (m_HorizontalAlignment != value)
                {
                    m_HorizontalAlignment = value;
                    this.TrimSize();
                    this.Refresh();
                }
            }
        }

        public override void LoadDisplayText()
        {
            base.LoadDisplayText();
            this.TrimSize();
        }
        protected override void OnCaptionKeyChanged(EventArgs eventArgs)
        {
            base.OnCaptionKeyChanged(eventArgs);

        }
        
        [Browsable (false)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }
      
        [DefaultValue (true )]
        [Browsable (true )]
        public override bool AutoSize
        {
            get
            {
                return base.AutoSize;
            }
            set
            {
                base.AutoSize = value;
                this.TrimSize();
            }
        }
        public IGKXLabel()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.TextChanged += _TextChanged;
            this.CaptionKey = "lb.Label";
            this.AutoSize = true;
            this.TabStop = false;
            this.m_HorizontalAlignment = enuStringAlignment.Near;
            this.m_VerticalAlignment = enuStringAlignment.Near;
            
            this.BackColor = Color.Transparent;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                this.TextChanged -= _TextChanged;
            }
            base.Dispose(disposing);
        }
        void _TextChanged(object sender, EventArgs e)
        {
            Invalidate();
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            this.TrimSize();
            base.OnSizeChanged(e);
        }
        protected override void OnTextChanged(EventArgs e)
        {
            this.TrimSize();
            base.OnTextChanged(e);
        }

        private void TrimSize()
        {
            if (this.AutoSize && !this.IsDisposed)
            {
                if (this.m_configuring)
                    return;
                
                this.m_configuring = true;
                Graphics g = Graphics.FromHwnd(this.Handle);
                StringFormat v_format = new StringFormat();
                v_format.Alignment = StringAlignment.Near;
                v_format.LineAlignment = StringAlignment.Near;
                SizeF c = g.MeasureString(this.Text, this.Font, new Size(short.MaxValue, short.MaxValue), 
                    v_format);
                this.Size = new System.Drawing.Size((int)Math.Ceiling(c.Width), (int)Math.Ceiling(c.Height));
                g.Dispose();
                v_format.Dispose();
                this.m_configuring = false;
            }
        }
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //for transparent setting background
            base.OnPaintBackground(pevent);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            System.Drawing.Brush br = WinCoreBrushRegister.GetBrush(CoreRenderer.ForeColor) ?? Brushes.White;
            //ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
            if (this.DesignMode)
            {
                e.Graphics.Clear(CoreRenderer.BackgroundColor);
                
                try
                {
                    e.Graphics.DrawString(this.CaptionKey,
                        this.Font,
                        br,
                        Point.Empty);
                }
                catch{

                }
            }
            else
            {
                using (StringFormat v_format = new StringFormat())
                {
                    v_format.Alignment =(StringAlignment) this.HorizontalAlignment;
                    v_format.LineAlignment = (StringAlignment)this.VerticalAlignment;
                    v_format.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.LineLimit;
                    e.Graphics.DrawString(
                        this.Text,
                        this.Font,
                        br,
                        this.ClientRectangle,
                        v_format);
                }
            }
        }
    }
}

