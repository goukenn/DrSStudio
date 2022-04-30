

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXLinearColorPicker.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.WinUI
{
    using IGK.ICore.WinCore;
    using IGK.ICore.WinCore.WinUI.Controls;

    /// <summary>
    /// represent a linear color picker
    /// </summary>
    public class IGKXLinearColorPicker : IGKXControl 
    {
        protected override System.Drawing.Size DefaultSize
        {
            get
            {
                return new System.Drawing.Size(100, 32);
            }
        }
        public IGKXLinearColorPicker()
        {
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }
        protected override void OnCorePaint(CorePaintEventArgs e)
        {
            base.OnCorePaint(e);
            if (DesignMode)
            {
                e.Graphics.DrawString("linear color", this.Font,
                    Brushes.Black, 0, 0);
            }
            Rectangle rc = this.ClientRectangle;
            float v_wstep = rc.Width / 360.0f;
            if (v_wstep > 0)
            {
                Colorf v_cl = Colorf.Empty;
                Colorf v_cl2 = Colorf.Black;
                using (LinearGradientBrush lb = new LinearGradientBrush(new RectangleF(0, 0, v_wstep, rc.Height),
                    Color.Empty,
                    Color.Empty, 90.0f))
                {
                    lb.GammaCorrection = false ;
                    for (int i = 0; i < 360; i++)
                    {
                        v_cl = CoreColorHandle.HSVtoColorf(((255 * i) / 360), 255, 255);
                        lb.InterpolationColors = new ColorBlend(2)
                        {
                            Colors = new Color[] { 
                        v_cl .ToGdiColor (),
                        Colorf.FromFloat (0.2f).ToGdiColor()
                    },
                            Positions = new float[] { 0, 1 }
                        };
                        e.Graphics.FillRectangle(lb, i * v_wstep, 0, v_wstep, rc.Height);
                        //e.Graphics.DrawRectangle (v_cl, i * v_wstep, 0, v_wstep, rc.Height);
                        //e.Graphics.FillRectangle(lb, i * v_wstep, 0, v_wstep, rc.Height);
                    }
                }
            }
            else 
            { 
            }
        }

    }
}
