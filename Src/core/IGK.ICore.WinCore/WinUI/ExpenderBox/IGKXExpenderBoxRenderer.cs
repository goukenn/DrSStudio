

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXExpenderBoxRenderer.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.Resources;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace IGK.ICore.WinCore.WinUI
{
    using IGK.ICore;
    using IGK.ICore.WinCore.WinUI;
    public class IGKXExpenderBoxRenderer
    {
        protected IGKXExpenderBox box;
        public IGKXExpenderBoxRenderer()
        {
        }
        public IGKXExpenderBoxRenderer(IGKXExpenderBox box)
        {
            this.box = box;
        }
        public virtual  void RenderBox(IGKXExpenderBox expenderBox, CorePaintEventArgs e)
        {
            e.Graphics.Clear(Colorf.FromFloat(0.3f, Colorf.White));
        }
        public virtual  void RenderBoxItem(IGKXExpenderBoxItem item, CorePaintEventArgs e)
        {
           // e.Graphics.Clear(Colorf.FromFloat(0.3f));
            bool v_isselected = (box.SelectedGroup == item.ParentGroup);
            bool v_isover = (item.MouseState == enuMouseState.Hover);
            Colorf cl1 = !v_isover ? WinCoreControlRenderer.ExpenderItemColor : WinCoreControlRenderer.ExpenderItemOverColor;
            Colorf cl2 = !v_isover ? WinCoreControlRenderer.ExpenderItemForeColor : WinCoreControlRenderer.ExpenderItemOverForeColor;
            Colorf bg = v_isover ? Colorf.Black : Colorf.Red;

            Rectanglei rc = new Rectanglei(0, 0, item.Width, item.Height);
          //  e.Graphics.FillRectangle(cl1, 0, 0, item.Width, item.Height);

            ICore2DDrawingDocument d = CoreResources.GetDocument(item.ImageKey);
            if (d != null)
            {
                d.Draw(e.Graphics, new Rectanglei(2, 2, item.Height - 4, item.Height - 4));
            }
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.FormatFlags = StringFormatFlags.NoClip | StringFormatFlags.NoWrap | StringFormatFlags.MeasureTrailingSpaces;
            sf.Trimming = StringTrimming.EllipsisWord;


            //draw caption string
            rc.X += 16;
            rc.Width -= 16;

            e.Graphics.DrawString(
                CoreSystem.GetString (item.CaptionKey ),
                item.Font,
                WinCoreBrushRegister.GetBrush(cl2),
                rc,
                sf);
            sf.Dispose();
            e.Graphics.DrawRectangle(WinCoreControlRenderer.ExpenderItemBorderColor, 0, 0, item.Width - 1, item.Height - 1);
        }
        public virtual  void RenderBoxGroup(IGKXExpenderBoxGroup group, CorePaintEventArgs e)
        {
            e.Graphics.Clear(WinCoreControlRenderer.ExpenderGroupBackgroundColor);

            bool v_isselected = (box.SelectedGroup == group);
            bool v_isover = (group.MouseState == enuMouseState.Hover);
            Colorf cl1 = WinCoreControlRenderer.ExpenderGroupEndColor;
            Colorf cl2 = WinCoreControlRenderer.ExpenderGroupStartColor;
            Colorf v_fcolor = WinCoreControlRenderer.ExpenderGroupForeColor;
            int v_h = group.DefaultHeight;
            if (v_isselected)
            {
                if (v_isover)
                {
                    cl2 = WinCoreControlRenderer.ExpenderGroupSelectedOverEndColor;
                    cl1 = WinCoreControlRenderer.ExpenderGroupSelectedOverStartColor;
                    v_fcolor = WinCoreControlRenderer.ExpenderGroupSelectedOverForeColor;
                }
                else {
                    cl2 = WinCoreControlRenderer.ExpenderGroupSelectedEndColor;
                    cl1 = WinCoreControlRenderer.ExpenderGroupSelectedStartColor;
                    v_fcolor = WinCoreControlRenderer.ExpenderGroupSelectedForeColor;
                }
            }
            else if(v_isover)
            {
                cl2 = WinCoreControlRenderer.ExpenderGroupOverEndColor;
                cl1 = WinCoreControlRenderer.ExpenderGroupOverStartColor;
                v_fcolor = WinCoreControlRenderer.ExpenderGroupOverForeColor;
            }
            Rectanglei rc = new Rectanglei(0, 0, group.Width, v_h);


            using (Brush ln = WinCoreBrushRegister.CreateBrush(
                rc,
                cl1,
                cl2,
                90.0f))
            {
                e.Graphics.FillRectangle(ln, rc);
            }
            if (v_isselected )
            {
                e.Graphics.FillRectangle(WinCoreBrushRegister.GetBrush(WinCoreControlRenderer.ExpenderGroupSelectedTopColor), 
                    new Rectanglef(rc.X, rc.Y, 8, 
                        group.DefaultHeight
                       // group.Height 
                        ));
            }

            if (string.IsNullOrEmpty(group.ImageKey))
            {
                ICore2DDrawingDocument doc = CoreResources.GetDocument(group.ImageKey);
                if (doc != null)
                {
                    doc.Draw(e.Graphics, new Rectanglei(2, 2, 14, 14));
                }
            }

            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.FormatFlags = StringFormatFlags.NoClip | StringFormatFlags.NoWrap | StringFormatFlags.MeasureTrailingSpaces;
            sf.Trimming = StringTrimming.EllipsisWord;


            //draw caption string
            rc.X += 16;
            rc.Width -= 16;
            rc.Height = group.DefaultHeight;
            e.Graphics.DrawString(
                CoreResources.GetString(group.CaptionKey),
                group.Font,
                WinCoreBrushRegister.GetBrush(v_fcolor),
                rc,
                sf);
            sf.Dispose();
            e.Graphics.DrawRectangle(WinCoreControlRenderer.ExpenderGroupBorderColor, 0, 0,group.Width -1,
              //  group.Height
                          group.DefaultHeight
                -1);
        }
    }
}
