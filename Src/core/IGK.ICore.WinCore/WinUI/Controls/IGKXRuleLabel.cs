

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXRuleLabel.cs
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
file:IGKXRuleLabel.cs
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
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using IGK.ICore.WinCore;
using IGK.ICore.WinUI;
using IGK.ICore.WinCore.WinUI.Controls;
namespace IGK.ICore.WinUI
{
    /// <summary>
    /// represent the rule label
    /// </summary>
    public class IGKXRuleLabel : IGKXLabel 
    {
        #region Members
        StringAlignment m_alignment;
        #endregion
        #region Properties
        public StringAlignment Alignment
        {
            get
            {
                return m_alignment;
            }
            set
            {
                m_alignment = value;
                OnAlignmentChanged(EventArgs.Empty);
            }
        }
        #endregion
        #region Events
        public event EventHandler AlignmentChanged;
        #endregion

        protected override Size DefaultSize
        {
            get
            {
                return new Size(100, 24);
            }
        }
        //.ctr
        public IGKXRuleLabel()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.Selectable, false);
            this.CaptionKey = null;  
            this.Text = null;
            this.TabStop = false;
            this.AutoSize = false;
            this.Size = DefaultSize;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            _Paint(this, e);
        }
        void _Paint(object sender, PaintEventArgs e)
        {
            if (this.DesignMode)
            {
                e.Graphics.DrawString(this.CaptionKey,
                    this.Font,
                    SystemBrushes.ControlText,
                    Point.Empty);
            }
            else
            {
                StringFormat f = GetFormat();
                Brush p = WinCoreBrushRegister.GetBrush(CoreRenderer.ForeColor);
                if (p != null)
                {
                    if (this.Enabled)
                        e.Graphics.DrawString(this.Text, this.Font, p, this.ClientRectangle, f);
                    else
                    {
                        System.Drawing.Drawing2D.GraphicsState v_s = e.Graphics.Save();
                        e.Graphics.DrawString(this.Text,
                            this.Font,
                            p,
                            this.ClientRectangle, f);
                        e.Graphics.Restore(v_s);
                    }
                }
                DrawRule(e, f);
                f.Dispose();
            }
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            this.Invalidate(true);
            base.OnSizeChanged(e);
        }
        #region Overrided Method
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //let the system paint the background color for transparency effect
            base.OnPaintBackground(pevent);
        }
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            this.Invalidate();
        }
        #endregion
        #region Protected Method
        protected virtual void OnAlignmentChanged(EventArgs e)
        {
            this.Invalidate();
            if (this.AlignmentChanged != null)
                this.AlignmentChanged(this, e);
        }
        #endregion
        private StringFormat GetFormat()
        {
            StringFormat v = new StringFormat();
            v.Alignment = this.Alignment;
            return v;
        }
        private void DrawRule(PaintEventArgs e, StringFormat format)
        {
            SizeF vStr = e.Graphics.MeasureString(this.Text, this.Font, this.ClientSize, format);
            float h = vStr.Height / 2.0f;
            Pen p = WinCoreBrushRegister.GetPen(CoreRenderer.RuleSeparatorDark);
            Pen p2 = WinCoreBrushRegister.GetPen(CoreRenderer.RuleSeparatorLight);
            int margin = vStr.Width> 0? 16:0;
            if ((p != null) && (p2 != null))
            {
                switch (Alignment)
                {
                    case StringAlignment.Center:
                        e.Graphics.DrawLine(p, 0, h, -margin +(this.ClientSize.Width - vStr.Width) / 2.0f, h);
                        e.Graphics.DrawLine(p2, 0, h + 1, -margin +(this.ClientSize.Width - vStr.Width) / 2.0f, h + 1);
                        e.Graphics.DrawLine(p, (this.ClientSize.Width + vStr.Width) / 2.0f, h, this.ClientSize.Width, h);
                        e.Graphics.DrawLine(p2, (this.ClientSize.Width + vStr.Width) / 2.0f, h + 1, this.ClientSize.Width, h + 1);
                        break;
                    case StringAlignment.Far:
                        e.Graphics.DrawLine(p, 0, h, this.ClientSize.Width - vStr.Width - margin, h);
                        e.Graphics.DrawLine(p2, 0, h + 1, this.ClientSize.Width - vStr.Width - margin, h + 1);
                        break;
                    case StringAlignment.Near:
                        e.Graphics.DrawLine(p, vStr.Width + margin, h, this.ClientSize.Width, h);
                        e.Graphics.DrawLine(p2, vStr.Width+margin, h + 1, this.ClientSize.Width, h + 1);
                        break;
                }
            }
        }
    }
}

