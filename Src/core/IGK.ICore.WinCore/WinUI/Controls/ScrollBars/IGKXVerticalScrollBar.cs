

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXVerticalScrollBar.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Resources;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.WinCore.WinUI.Controls
{
    public class IGKXVerticalScrollBar : IGKXScrollBar 
    {
        public IGKXVerticalScrollBar()
        {
            this.Paint += _Paint;
            this.PrevButton.ButtonDocument = CoreButtonDocument.CreateFromRes(CoreImageKeys.VSCROLLTOP_GKDS);//.CoreResources.GetAllDocuments("vscrolltop"));
            this.NextButton.ButtonDocument = CoreButtonDocument.CreateFromRes(CoreImageKeys.VSCROLLBOTTOM_GKDS );//CoreResources.GetAllDocuments("vscrollbottom"));
        }
        protected override void InitBounds()
        {
            int w = this.Width;
            this.PrevButton.Bounds = new System.Drawing.Rectangle(0, 0, w, w);
            this.NextButton.Bounds = new System.Drawing.Rectangle(0, this.Height - w, w, w);
            int h = Math.Max(100, this.Height  - (2 * w) - Math.Abs(this.MaxValue - this.MinValue));

            int offsetx = 0;
            float d = (this.MaxValue - this.MinValue);
            if (d > 0)
            {
                //100 -> this.Width - 32
                //x -> 
                //1 -> Max - min
                //x -> value;
                //--------------
                //x = value/(Max-Min)
                float ff = ((this.Value - this.MinValue) / d) * (this.Height - (2 * w) - h);
                offsetx = (int)(Math.Round(ff));//(this.Value - this.MinValue) / d) * (this.Width - (2 * h));
            }
            
            this.BodyButton.Bounds = new System.Drawing.Rectangle(0, w + offsetx, w, h);
        }

        protected override void UpdateValue(Vector2i mouseLocation)
        {
            int h = this.PrevButton.Height;
            float d = (MaxValue - MinValue);
            int w = (int)((mouseLocation.Y - h) / (float)(this.Height - (h * 2)) * d);
            w += MinValue;
            if (w < this.MinValue)
                this.Value = this.MinValue;
            else
                if (w > this.MaxValue)
                    this.Value = MaxValue;
                else
                    this.Value = w;
        }

        private void _Paint(object sender, CorePaintEventArgs e)
        {
            
        }
    }
}
