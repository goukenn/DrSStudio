

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXHRule.cs
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
file:XHRule.cs
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
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace IGK.ICore.WinCore
{
    using IGK.ICore.WinCore;
    using IGK.ICore;
    using IGK.ICore.WinUI;
    using IGK.ICore.Drawing2D.WinUI;
    using IGK.ICore.WinCore.WinUI;
    /// <summary>
    /// represent the Horizontal rule base
    /// </summary>
    public sealed class IGKXHRule : IGKXRuleBase
    {
        public IGKXHRule(ICore2DDrawingRuleSurface surface)
            : base(surface)
        {
            this.Paint +=_Paint;
        }
        void _Paint(object sender, CorePaintEventArgs e)
        {
            //float v_posx = Surface.PosX;
            //float v_posy = Surface.PosY;
            //int h = this.Surface.CurrentDocument.Height;
            int w = this.Surface.CurrentDocument.Width;
            e.Graphics.FillRectangle(
                WinCoreControlRenderer.RuleBackgroundColor
                ,
                this.ClientRectangle.X, this.ClientRectangle.Y, this.ClientRectangle.Width, this.ClientRectangle.Height );


            Rectanglef rc = Surface.GetScreenBound(
                Surface.CurrentDocument.Bounds
                );
            float v_gx = rc.X - Bounds.X;
            Rectanglef v_vrc = new Rectanglef(v_gx, 0,
               rc.Width + ((v_gx<0)? v_gx:0), this.Height);
            e.Graphics.FillRectangle(Brushes.WhiteSmoke, v_vrc);


            //return;


            e.Graphics.DrawLine(Pens.Black, rc.X - Bounds.X, 0,
                rc.X - Bounds.X, this.Height);
            e.Graphics.DrawLine(Pens.Black, rc.X + rc.Width - Bounds.X, 0,
                rc.X + rc.Width - Bounds.X, this.Height);
            //draw index every modulo 5 
            int v_h = 5;
            float unit = 0.0F;
            for (float i = 0; i < rc.Width; i += 5)
            {
                if ((i > 0) && ((i % 25) == 0))
                {
                    v_h = 8;
                }
                else
                    v_h = 5;
                e.Graphics.DrawLine(Pens.Black,
                    i + v_vrc.X, this.Height - v_h, i + v_vrc.X, this.Height);
                if ((i != 0) && ((i % 100) == 0))
                {
                    //unit calcule
                    unit = (w * i) / (rc.Width);
                    //Math.Ceiling ((i/ Surface.ZoomX )) .ToString(), 
                    e.Graphics.DrawString(
                        Math.Floor(unit).ToString(),
                        Font,
                        Brushes.Black,
                        new Rectanglef(v_vrc.X + i,
                           0,
                           v_vrc.X + i,
                            this.Height)
                        );
                }
            }
            e.Graphics.DrawString("0", Font, Brushes.Black, v_vrc,null );
            e.Graphics.DrawString(w.ToString(), Font, Brushes.Black,
                v_vrc.X +
                v_vrc.Width, 0);
            //draw cursor
            e.Graphics.DrawLine(Pens.Black,
                MouseLocation.X,
                0,
                MouseLocation.X, this.Height);
           //var v_vrc1 = this.ClientRectangle;
           //e.Graphics.DrawRectangle(Colorf.Black, v_vrc1.X, v_vrc1.Y, v_vrc1.Width - 1, v_vrc1.Height - 1);
        }
        protected override void SetUpBounds()
        {
            //if (this.Surface.ShowRules)
            //{
            //    int w = this.Surface.Width - (SystemInformation.VerticalScrollBarWidth + 1);
            //    if (this.Surface.ShowScroll)
            //    {
            //        w = w - (SystemInformation.VerticalScrollBarWidth);
            //    }
            //    this.Bounds = new System.Drawing.Rectangle(SystemInformation.VerticalScrollBarWidth, 0,
            //        w, SystemInformation.HorizontalScrollBarHeight);
            //}
            //else
            //    this.Bounds = Rectangle.Empty;
        }
        protected override void OnShowRuleChanged(EventArgs eventArgs)
        {
            SetUpBounds();
        }
    }
}

