

using IGK.ICore.WinCore;
using IGK.ICore;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXWinCoreStatusProgressBar.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.Drawing2D;
using IGK.ICore.GraphicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinCore.WinUI;

namespace IGK.ICore.WinUI
{
    /// <summary>
    /// Represent a progress bar that will be writed in in progress control
    /// </summary>
    public class IGKWinCoreStatusProgressBar : IGKWinCoreStatusItemBase
    {
        private int m_Value;

        /// <summary>
        /// get or set the value
        /// </summary>
        public int Value
        {
            get { return m_Value; }
            set
            {
                if (m_Value != value)
                {
                    m_Value = value;
                    if ((this.Parent != null) && (this.Visible))
                        this.Parent.Invalidate();
                    OnValueChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler ValueChanged;
        ///<summary>
        ///raise the ValueChanged 
        ///</summary>
        protected virtual void OnValueChanged(EventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged(this, e);
        }

        public IGKWinCoreStatusProgressBar()
        {
            this.Value = 10;
        }
        public override void Render(ICoreGraphics graphics, bool active)
        {
            Rectanglef rc = this.Bounds;
            rc.Inflate(-2, -2);
            Object obj = graphics.Save();
            graphics.SmoothingMode = enuSmoothingMode.None;
            graphics.FillRectangle (WinCoreControlRenderer.ProgessBackgroundColor, rc);            
            float w = (this.Value / 100.0f) * rc.Width;
            graphics.FillRectangle(WinCoreControlRenderer.ProgessValueColor , rc.X, rc.Y, w, rc.Height );
            graphics.DrawLine (Colorf.Black,2 , enuDashStyle.Solid , rc.X, rc.Y+1, rc.Right  - 2, rc.Y+1 );
            graphics.DrawRectangle(Colorf.White, rc.X, rc.Y,rc.Width -1, rc.Height-1);            
            graphics.Restore(obj);

        }
    }
}
