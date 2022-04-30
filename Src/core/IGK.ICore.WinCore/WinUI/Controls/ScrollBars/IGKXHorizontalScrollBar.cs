

using IGK.ICore.WinCore;
using IGK.ICore;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXHorizontalScrollBar.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.Resources;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.WinCore.WinUI.Controls
{
    public class IGKXHorizontalScrollBar : IGKXScrollBar 
    {
        public IGKXHorizontalScrollBar()
        {
            this.Paint += _Paint;
            this.PrevButton.ButtonDocument = CoreButtonDocument.Create(CoreResources.GetAllDocuments(CoreImageKeys.HSCROLLPREV_GKDS));
            this.NextButton.ButtonDocument = CoreButtonDocument.Create(CoreResources.GetAllDocuments(CoreImageKeys.HSCROLLNEXT_GKDS));
        }

        protected override void InitLayout()
        {
            base.InitLayout();
            this.InitBounds();
        }

      
        protected override void UpdateValue(Vector2i mouseLocation)
        {
            int h = this.PrevButton.Width;
            float d = (MaxValue - MinValue);
            int w = (int)((mouseLocation.X - h) / (float)(this.Width - (h * 2)) * d);
            w += MinValue;
            if (w < this.MinValue)
                this.Value = this.MinValue;
            else
                if (w > this.MaxValue)
                    this.Value =  MaxValue ;
                else
                    this.Value = w;
         
        }

    

     

        protected override void InitBounds()
        {
            int h = this.Height ;
            this.PrevButton.Bounds = new System.Drawing.Rectangle(0, 0, h, h);
            this.NextButton.Bounds = new System.Drawing.Rectangle(this.Width - h, 0, h, h);
            int w = Math.Max(100, this.Width - (2*h) - Math.Abs(this.MaxValue - this.MinValue));
            int offsetx = 0;
            float d = (this.MaxValue-this.MinValue );
            if (d>0)
            {
                //100 -> this.Width - 32
                //x -> 
                //1 -> Max - min
                //x -> value;
                //--------------
                //x = value/(Max-Min)
                float ff =( (this.Value - this.MinValue ) / d) * (this.Width - (2 * h) -w );
                offsetx = (int)(Math.Round(ff));//(this.Value - this.MinValue) / d) * (this.Width - (2 * h));
            }            
            this.BodyButton.Bounds = new System.Drawing.Rectangle(h + offsetx , 0, w, h);
        }
        void _Paint(object sender, CorePaintEventArgs e)
        {
            e.Graphics.Clear(WinCoreControlRenderer.ScrollbarBackgroundColor);
        }
    }
}
