

using IGK.ICore.WinCore;
using IGK.ICore;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXSimpleColorView.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinCore.WinUI.Controls;

namespace IGK.DrSStudio.WinUI
{
    public class IGKXSimpleColorView : IGKXControl 
    {
        private Colorf m_Color;
        private bool m_Selected;
        /// <summary>
        /// get or set if this simple color view is selected
        /// </summary>
        public bool Selected
        {
            get { return m_Selected; }
            set
            {
                if (m_Selected != value)
                {
                    m_Selected = value;
                    this.Invalidate();
                }
            }
        }
        
        public Colorf Color
        {
            get { return m_Color; }
            set
            {
                if (m_Color != value)
                {
                    m_Color = value;
                    this.Invalidate();
                }
            }
        }
        public IGKXSimpleColorView()
        {
            this.MinimumSize = new Size(16, 16);
            this.Paint += _Paint;
        }

        void _Paint(object sender, CorePaintEventArgs e)
        {
            RenderSimpleColorView(e);
        }

        protected virtual void RenderSimpleColorView(CorePaintEventArgs e)
        {
            e.Graphics.FillRectangle(this.Color, 0, 0, this.Width - 1, this.Height - 1);
            Rectanglef v_rc = new Rectanglef(0, 0, this.Width, this.Height);
            v_rc.Inflate(-2, -2);
            e.Graphics.DrawRectangle(Colorf.White, v_rc.X, v_rc.Y, v_rc.Width, v_rc.Height);
            e.Graphics.DrawRectangle(Colorf.Black, 0, 0, this.Width - 1, this.Height - 1);
        }
    }
}
