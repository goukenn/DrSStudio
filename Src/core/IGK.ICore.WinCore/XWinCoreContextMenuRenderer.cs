

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XWinCoreContextMenuRenderer.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IGK.ICore.WinUI
{
    using IGK.ICore;
    using IGK.ICore.WinCore;
    using IGK.ICore.WinCore.WinUI;
    using IGK.ICore.WinUI;

    /// <summary>
    /// represent a context menu renderer
    /// </summary>
    public class XWinCoreContextMenuRenderer : XWinCoreMenuRenderer
    {
        public XWinCoreContextMenuRenderer():base(new WinCoreContextMenuRendererColorTable())
        {
            this.RoundedEdges = false; 
        }
        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            base.OnRenderItemCheck(e);
        }
        protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
        {
            IGKXContextMenuItem v_item = e.Item as IGKXContextMenuItem;
            if (v_item != null)
            {
                ICore2DDrawingDocument v = v_item.MenuDocument;
                if (v != null)
                {

                    WinCoreExtensions.Draw(v,e.Graphics, e.ImageRectangle);
                    return;
                }
            }
            base.OnRenderItemImage(e);
        }
        protected override void OnRenderLabelBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderLabelBackground(e);
        }
        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            base.OnRenderToolStripBackground(e);
        }
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {

            base.OnRenderMenuItemBackground(e);
        }
        protected override void OnRenderToolStripStatusLabelBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderToolStripStatusLabelBackground(e);
        }
        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderButtonBackground(e);
        }
        protected override void OnRenderItemBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderItemBackground(e);
        }
        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            if (e.Item.Enabled)
            {
                e.TextFormat -= TextFormatFlags.HidePrefix;
                if (e.TextColor.IsSystemColor)
                {
                    switch (e.TextColor.Name)
                    {
                        case "MenuText":
                            if (e.Item.OwnerItem == null)
                            {
                                e.TextColor = WinCoreControlRenderer.ContextMenuStripRootTextColor.ToGdiColor();
                            }
                            else {
                                e.TextColor = WinCoreControlRenderer.ContextMenuStripTextColor.ToGdiColor();
                            }
                            break;
                        case "HighlightText":
                            if (e.Item.OwnerItem == null)
                            {
                                e.TextColor = WinCoreControlRenderer.ContextMenuStripRootHightLightColor.ToGdiColor();
                            }
                            else
                            {
                                e.TextColor = WinCoreControlRenderer.ContextMenuStripHightLightColor.ToGdiColor();
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
               // System.Drawing.StringFormat sf = new System.Drawing.StringFormat();
               // if ((e.TextFormat & TextFormatFlags.Right) == TextFormatFlags.Right)
               //     sf.Alignment = System.Drawing.StringAlignment.Far;
               //// if ((e.TextFormat & TextFormatFlags.HidePrefix) == TextFormatFlags.HidePrefix)
               //     sf.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
               // if ((e.TextFormat & TextFormatFlags.VerticalCenter) == TextFormatFlags.VerticalCenter)
               //     sf.LineAlignment = System.Drawing.StringAlignment.Center;

               // //e.Graphics.DrawString(e.Text,
               // //    e.TextFont,
               // //    WinCoreBrushRegister.GetBrush(WinCoreControlRenderer.MenuStripDisableTextColor),
               // //    e.TextRectangle, sf);
               // sf.Dispose();
               // return;
            }
            base.OnRenderItemText(e);
        }
        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            base.OnRenderArrow(e);
        }
        protected override void OnRenderGrip(ToolStripGripRenderEventArgs e)
        {
            base.OnRenderGrip(e);
        }
        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            //no image margin rendering
            e.Graphics.FillRectangle(
               WinCoreControlRenderer.ContextMenuStripMarginSeparatorColor,
                new Rectanglef(e.AffectedBounds.Right, 0, 1, e.ToolStrip.Height)
                );

        }
        protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderDropDownButtonBackground(e);
        }
        protected override void OnRenderSplitButtonBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderSplitButtonBackground(e);
        }
        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            base.OnRenderSeparator(e);
        }

        class WinCoreContextMenuRendererColorTable : XWinCoreMenuRenderer.XWinCoreMenuRendererColorTable
        {
            public override System.Drawing.Color ToolStripDropDownBackground
            {
                get
                {
                    return WinCoreControlRenderer.ContextMenuStripDropDownBackgroundColor.ToGdiColor();
                }
            }
            public override System.Drawing.Color MenuItemSelected
            {
                get
                {
                    return WinCoreControlRenderer.ContextMenuStripSelectedItemBackgroundColor.ToGdiColor();
                }
            }
        }
    }
}
