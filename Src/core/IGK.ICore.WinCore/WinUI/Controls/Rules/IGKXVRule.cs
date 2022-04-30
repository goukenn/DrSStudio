

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXVRule.cs
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
file:XVRule.cs
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
    using IGK.ICore.WinUI;
    using IGK.ICore.Drawing2D.WinUI;
    using IGK.ICore;
    using IGK.ICore.WinCore.WinUI;
    public sealed class IGKXVRule : IGKXRuleBase
    {
        public IGKXVRule(ICore2DDrawingRuleSurface surface)
            : base(surface)
        {
            //this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            //this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            //this.SetStyle(ControlStyles.UserPaint, true);
            this.Paint += _Paint;
         }

        private void _Paint(object sender, CorePaintEventArgs e)
        {
            float v_posx = Surface.PosX;
            float v_posy = Surface.PosY;
            int h = this.Surface.CurrentDocument.Height;
            int w = this.Surface.CurrentDocument.Width;
            e.Graphics.FillRectangle(
                WinCoreControlRenderer.RuleBackgroundColor,
                this.ClientRectangle.X,
                this.ClientRectangle.Y,
                this.ClientRectangle.Width,
            this.ClientRectangle.Height);
            Rectanglef rc = Surface.GetScreenBound(Surface.CurrentDocument.Bounds);
            float v_gh= rc.Y - Bounds.Y;
            Rectanglef v_vrc = new Rectanglef(0,v_gh ,
                this.Width, rc.Height +((v_gh<0)?v_gh:0));
            e.Graphics.FillRectangle(Brushes.WhiteSmoke, v_vrc);
            e.Graphics.DrawLine(Pens.Black, 0, rc.Y - Bounds.Y,
                this.Width, rc.Y - Bounds.Y);
            e.Graphics.DrawLine(Pens.Black, 0, rc.Y - Bounds.Y + rc.Height,
             this.Width, rc.Y + rc.Height - Bounds.Y);
            StringFormat frm = new StringFormat();
            frm.FormatFlags |= StringFormatFlags.DirectionVertical;
          //  Font v_ft = null;// ThemeManager. ((IGK.ICore.Drawing2D.CoreFont)"FontName:Tahoma;Size:8pt").ToGdiFont();
            int v_h = 5;
            float v_unit;
            for (float i = 0; i < rc.Height; i += 5)
            {
                if ((i > 0) && ((i % 25) == 0))
                {
                    v_h = 8;
                }
                else
                    v_h = 5;
                e.Graphics.DrawLine(Pens.Black,
                   this.Width - v_h, i + v_vrc.Y,
                   this.Width, i + v_vrc.Y);
                if ((i != 0) && ((i % 100) == 0))
                {
                    //unit calcule
                    v_unit = (h * i) / (rc.Height);
                    object v_state = e.Graphics.Save();
                    e.Graphics.RotateTransform(-90, enuMatrixOrder.Append);
                    e.Graphics.TranslateTransform(0, v_vrc.Y + i, enuMatrixOrder.Append);
                    e.Graphics.DrawString(
                        Math.Floor(v_unit).ToString(),
                        //Math.Ceiling ((i/ Surface.ZoomX )) .ToString(), 
                        this.Font,
                        Brushes.Black,
                        0,
                        0
                        );
                    e.Graphics.Restore(v_state);
                }
            }
            //Draw cursor
            e.Graphics.DrawLine(Pens.Black,
                0,
                MouseLocation.Y,
                this.Width,
                MouseLocation.Y);
            //e.Graphics.DrawString("0", Font, Brushes.Black, v_vrc, frm);
            //e.Graphics.DrawString(
            //    h.ToString(), 
            //    Font, 
            //    Brushes.Black, 
            //    v_vrc.X,
            //    (v_vrc.Y + v_vrc.Height),
            //    frm);
            //frm.Dispose();
            //WinCoreControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
        }
        protected override void SetUpBounds()
        {
            //if (this.Surface.ShowRules)
            //{
            //    int h = this.Surface.Height - (SystemInformation.HorizontalScrollBarHeight + 1);
            //    if (this.Surface.ShowScroll)
            //    {
            //        h -= (SystemInformation.HorizontalScrollBarHeight);
            //    }
            //    this.Bounds = new System.Drawing.Rectangle(0, SystemInformation.HorizontalScrollBarHeight,
            //        SystemInformation.VerticalScrollBarWidth, h
            //        );
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

