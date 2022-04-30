

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXSimpleColorSelector.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using IGK.ICore.WinCore;
using IGK.ICore.WinUI;

namespace IGK.DrSStudio.WinUI
{
    using IGK.ICore;
    using IGK.ICore.WinCore.WinUI.Controls;
    /// <summary>
    /// represent a simple color selector
    /// </summary>
    public class IGKXSimpleColorSelector : IGKXControl
    {
        private Colorf m_Color;
        private string m_ColorName;

        public string ColorName
        {
            get { return m_ColorName; }
            set
            {
                if (m_ColorName != value)
                {
                    m_ColorName = value;
                }
            }
        }
        public Colorf Color
        {
            get { return m_Color; }
            set
            {
                if (!m_Color.Equals(value))
                {
                    m_Color = value;
                }
            }
        }
        public override System.Drawing.Size MinimumSize
        {
            get
            {
                return new System.Drawing.Size(16, 16);
            }
            set
            {
                base.MinimumSize = value;
            }
        }
        public IGKXSimpleColorSelector()
        {
            this.SetStyle(ControlStyles.FixedHeight, true);
            this.SetStyle(ControlStyles.FixedWidth, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.Size = new System.Drawing.Size(16, 16);
            this.MouseHover += new EventHandler(XSimpleColorSelector_MouseHover);
            this.Paint += _Paint;

            this.MouseLeave += new EventHandler(XSimpleColorSelector_MouseLeave);
        }

        private void _Paint(object sender, CorePaintEventArgs e)
        {
            Rectangle v_rc = this.ClientRectangle;
            e.Graphics.FillRectangle(Brushes.Black, v_rc);
            v_rc.Inflate(-1, -1);
            e.Graphics.FillRectangle(Brushes.White, v_rc);
            v_rc.Inflate(-1, -1);
            e.Graphics.FillRectangle(this.Color, v_rc.X , v_rc.Y , v_rc.Width , v_rc.Height );
        }
        void RemovePalette()
        {

        }
        void XSimpleColorSelector_MouseHover(object sender, EventArgs e)
        {
            this.ShowToolTip();
        }
        void XSimpleColorSelector_MouseLeave(object sender, EventArgs e)
        {
            if (this.Parent != null)
            {
                this.HideToolTip();
            }

        }
        protected virtual void ShowToolTip()
        {
        }
        protected virtual void HideToolTip()
        {

        }

    }
}
