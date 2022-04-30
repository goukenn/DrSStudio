

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XToolStripRenderer.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.Drawing2D;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:XToolStripRenderer.cs
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IGK.ICore.WinCore.WinUI.Controls
{
    using IGK.ICore;
    using IGK.ICore.WinCore;
    using IGK.ICore.WinCore.WinUI;

    public class XToolStripRenderer : ToolStripProfessionalRenderer
    {
        public XToolStripRenderer():base(new XToolStripColorTable())
        {
            this.RoundedEdges = false;
            
            
        }
        protected override void OnRenderGrip(ToolStripGripRenderEventArgs e)
        {
            base.OnRenderGrip(e);
        }
        protected override void InitializeItem(ToolStripItem item)
        {
            base.InitializeItem(item);
        }
        protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
        {
            if ((e.ImageRectangle.Width > 0) &&
                (e.ImageRectangle.Height > 0)
                )
            {
                if (e.Item is IGKXToolStripButton)
                {
                    ICore2DDrawingDocument doc = (e.Item as IGKXToolStripButton).ImageDocument;
                    if (doc != null)
                    {

                        doc.Draw(e.Graphics, true, new Rectangle(0, 0,
                            e.Item.Bounds.Width,
                            e.Item.Bounds.Height), enuFlipMode.None);
                        return;
                    }
                }
            }
            else 
            { 
            }
            base.OnRenderItemImage(e);
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            base.OnRenderSeparator(e);
        }
        public class XToolStripColorTable : ProfessionalColorTable
        {
            
            public override Color ButtonCheckedGradientBegin
            {
                get
                {
                    return WinCoreControlRenderer.ToolStripButttonCheckedGradientBegin.ToGdiColor();
                }
            }
            public override Color ButtonCheckedGradientMiddle
            {
                get
                {
                    return WinCoreControlRenderer.ToolStripButttonCheckedGradientMiddle.ToGdiColor();
                }
            }
            public override Color ButtonCheckedGradientEnd
            {
                get
                {
                    return WinCoreControlRenderer.ToolStripButttonCheckedGradientEnd.ToGdiColor();
                }
            }
            public override Color ToolStripBorder
            {
                get
                {
                    return WinCoreControlRenderer.ToolStripBorder.ToGdiColor();
                }
            }
            public override Color ToolStripDropDownBackground
            {
                get
                {
                    return WinCoreControlRenderer.ToolStripDropDownBackground.ToGdiColor();
                }
            }
            public override Color ToolStripGradientBegin
            {
                get
                {
                    return WinCoreControlRenderer.ToolStripGradientBegin.ToGdiColor();
                }
            }
            public override Color ToolStripGradientMiddle
            {
                get
                {
                    return WinCoreControlRenderer.ToolStripGradientMiddle.ToGdiColor();
                }
            }
            public override Color ToolStripGradientEnd
            {
                get
                {
                    return WinCoreControlRenderer.ToolStripGradientEnd.ToGdiColor();
                }
            }
            public override Color SeparatorLight
            {
                get
                {
                    return WinCoreControlRenderer.ToolStripSeparatorLight.ToGdiColor();
                }
            }
            public override Color SeparatorDark
            {
                get
                {
                    return WinCoreControlRenderer.ToolStripSeparatorDark.ToGdiColor();
                }
            }
            public override Color CheckBackground
            {
                get
                {
                    return WinCoreControlRenderer.ToolStripCheckBackground.ToGdiColor();
                }
            }
            public override Color ImageMarginGradientEnd
            {
                get
                {
                    return WinCoreControlRenderer.ToolStripImageMarginGradientEnd.ToGdiColor();
                }
            }
            public override Color ImageMarginGradientBegin
            {
                get
                {
                    return WinCoreControlRenderer.ToolStripImageMarginGradientBegin.ToGdiColor();
                }
            }
            public override Color ImageMarginGradientMiddle
            {
                get
                {
                    return WinCoreControlRenderer.ToolStripImageMarginGradientMiddle.ToGdiColor();
                }
            }
            public override Color OverflowButtonGradientBegin
            {
                get
                {
                    return WinCoreControlRenderer.ToolStripOverflowGradientBegin.ToGdiColor();
                }
            }
            public override Color OverflowButtonGradientEnd
            {
                get
                {
                    return WinCoreControlRenderer.ToolStripOverflowGradientEnd.ToGdiColor();
                }
            }
            public override Color OverflowButtonGradientMiddle
            {
                get
                {
                    return WinCoreControlRenderer.ToolStripOverflowGradientMiddle.ToGdiColor();
                }
            }
            public override Color ButtonCheckedHighlight
            {
                get
                {
                    return WinCoreControlRenderer.ToolStripButtonCheckedHighlight.ToGdiColor();
                }
            }
            public override Color ButtonPressedGradientBegin
            {
                get
                {
                    return WinCoreControlRenderer.ToolStripButtonPressedGradientBegin.ToGdiColor();
                }
            }
            public override Color ButtonPressedGradientEnd
            {
                get
                {
                    return WinCoreControlRenderer.ToolStripButtonPressedGradientEnd.ToGdiColor();
                }
            }
            public override Color ButtonPressedGradientMiddle
            {
                get
                {
                    return WinCoreControlRenderer.ToolStripButtonPressedGradientMiddle.ToGdiColor();
                }
            }
            public override Color ButtonPressedHighlight
            {
                get
                {
                    return WinCoreControlRenderer.ToolStripButtonPressedHighlight.ToGdiColor();
                }
            }
            public override Color ButtonSelectedBorder
            {
                get
                {
                    return WinCoreControlRenderer.ToolStripButtonSelectedBorder.ToGdiColor();
                }
            }
            public override Color ButtonSelectedGradientBegin
            {
                get
                {
                    return WinCoreControlRenderer.ToolStripButtonSelectedGradientBegin.ToGdiColor();
                }
            }
            public override Color ButtonSelectedGradientEnd
            {
                get
                {
                    return WinCoreControlRenderer.ToolStripButtonSelectedGradientEnd.ToGdiColor();
                }
            }
            public override Color ButtonSelectedGradientMiddle
            {
                get
                {
                    return WinCoreControlRenderer.ToolStripButtonSelectedGradientMiddle.ToGdiColor();
                }
            }
            public override Color ButtonSelectedHighlight
            {
                get
                {
                    return WinCoreControlRenderer.ToolStripButtonSelectedHighlight.ToGdiColor();
                }
            }
            public override Color ButtonSelectedHighlightBorder
            {
                get
                {
                    return WinCoreControlRenderer.ToolStripButtonSelectedHighlightBorder.ToGdiColor();
                }
            }
        }
    }
}

