

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XWinCoreMenuRenderer.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IGK.ICore.WinUI
{
    using IGK.ICore;
    using IGK.ICore.WinCore;
    using IGK.ICore.WinCore.WinUI;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.Resources;
    using System.Drawing.Drawing2D;
    /// <summary>
    /// represent a menu renderer 
    /// </summary>
    public class XWinCoreMenuRenderer : System.Windows.Forms.ToolStripProfessionalRenderer 
    {
        public XWinCoreMenuRenderer():base(new XWinCoreMenuRendererColorTable())
        {
            this.RoundedEdges = false;
        }
        protected XWinCoreMenuRenderer(ProfessionalColorTable tab)
            : base(tab)
        {
        }
        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            //no image margin rendering
            //base.OnRenderImageMargin(e);
            
            e.Graphics.FillRectangle (
               WinCoreControlRenderer.MenuStripMarginSeparatorColor,
                new Rectanglef (e.AffectedBounds.Right,0, 2 , e.ToolStrip.Height )
                );
     
        }
        protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
        {
            IGKXWinCoreMenuItem v_item = e.Item as IGKXWinCoreMenuItem;
            if (v_item != null)
            {
                ICore2DDrawingDocument v = v_item.MenuDocument;
                if (v != null)
                {
                   // v.Draw(e.Graphics, e.ImageRectangle);
                    v.Draw(e.Graphics, true,
                            e.ImageRectangle,
                             enuFlipMode.None);
                    return;
                }
            }
            //base.OnRenderItemImage(e);
        }
        
        
        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            e.ArrowColor = 
                e.Item.Enabled ?
                WinCoreControlRenderer.MenuStripArrawColor.ToGdiColor() :
                WinCoreControlRenderer.MenuStripDisableTextColor.ToGdiColor()
                ;
            base.OnRenderArrow(e);
        }
        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderButtonBackground(e);
        }
        protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderDropDownButtonBackground(e);
        }
        protected override void OnRenderGrip(ToolStripGripRenderEventArgs e)
        {
            base.OnRenderGrip(e);
        }
        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
              IGKXWinCoreMenuItem v_item = e.Item as IGKXWinCoreMenuItem;
              if (v_item != null)
              {
                  typeof(ToolStripProfessionalRenderer).GetMethod("RenderCheckBackground",
                       System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic 
                        | System.Reflection.BindingFlags.Public ).Invoke(this,
                      new Object[] { e});
                  return;
              }
            base.OnRenderItemCheck(e);
        }
        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            if (e.Item.Enabled)
            {
                bool child = e.Item.OwnerItem != null;
                if (e.TextColor.IsSystemColor)
                {
                    switch (e.TextColor.Name)
                    {
                        case "MenuText":
                            if (e.Item.OwnerItem == null)
                            {
                                e.TextColor = WinCoreControlRenderer.MenuStripRootTextColor.ToGdiColor();
                            }
                            else
                            {
                                e.TextColor = WinCoreControlRenderer.MenuStripTextColor.ToGdiColor();
                            }
                            break;
                        case "HighlightText":
                            if (e.Item.OwnerItem == null)
                            {
                                //for root menu
                                e.TextColor = WinCoreControlRenderer.MenuStripRootHightLightTextColor.ToGdiColor();
                            }
                            else
                            {
                                //for child menu
                                e.TextColor = WinCoreControlRenderer.MenuStripHightLightTextColor.ToGdiColor();
                            }
                            break;
                        default:
                            break;
                    }
                }
                //if (child)
                //    e.TextFont = new System.Drawing.Font(e.TextFont.FontFamily ,
                //        e.TextFont.Size * 0.8f, System.Drawing.FontStyle.Regular);
            }
            else 
            {
                System.Drawing.StringFormat sf = new System.Drawing.StringFormat();
                sf.Trimming = System.Drawing.StringTrimming.EllipsisCharacter;
                sf.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Hide;
                if ((e.TextFormat & TextFormatFlags.Right ) == TextFormatFlags.Right)
                    sf.Alignment = System.Drawing.StringAlignment.Far ;
                if ((e.TextFormat & TextFormatFlags.HidePrefix) == TextFormatFlags.HidePrefix)
                    sf.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Hide;
                if ((e.TextFormat & TextFormatFlags.VerticalCenter) == TextFormatFlags.VerticalCenter)
                    sf.LineAlignment = System.Drawing.StringAlignment.Center;

                e.Graphics.DrawString(e.Text,
                    e.TextFont,
                    WinCoreBrushRegister.GetBrush(WinCoreControlRenderer.MenuStripDisableTextColor),
                    e.TextRectangle, sf);
                sf.Dispose();
                return;
            }
            base.OnRenderItemText(e);
        }

        //background 
        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            base.OnRenderToolStripBorder(e);
        }
        protected override void OnRenderToolStripContentPanelBackground(ToolStripContentPanelRenderEventArgs e)
        {
            base.OnRenderToolStripContentPanelBackground(e);
        }
        protected override void OnRenderToolStripPanelBackground(ToolStripPanelRenderEventArgs e)
        {
            base.OnRenderToolStripPanelBackground(e);
        }
        protected override void OnRenderToolStripStatusLabelBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderToolStripStatusLabelBackground(e);
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            switch (e.BackColor.Name)
            {
                case "MenuBar":
                    LinearGradientMode mode = LinearGradientMode.Vertical;

                    using (LinearGradientBrush b = new LinearGradientBrush(e.AffectedBounds, 
                        ColorTable.MenuStripGradientBegin, 
                        ColorTable.MenuStripGradientBegin, 
                        mode))
                    {
                        e.Graphics.FillRectangle(b, e.AffectedBounds);
                    }
                    return ;
                default:
                    //Console.WriteLine ("Draw For : "+ e.BackColor.Name);
                    break;
            }
         
            base.OnRenderToolStripBackground(e);
        }
        protected override void OnRenderLabelBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderLabelBackground(e);
        }
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {

            base.OnRenderMenuItemBackground(e);

        }
        
        protected override void OnRenderItemBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderItemBackground(e);
         }
        protected override void OnRenderOverflowButtonBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderOverflowButtonBackground(e);
        }
        protected override void OnRenderSplitButtonBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderSplitButtonBackground(e);
        }
        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            base.OnRenderSeparator(e);
        }
        protected override void OnRenderStatusStripSizingGrip(ToolStripRenderEventArgs e)
        {
            base.OnRenderStatusStripSizingGrip(e);
        }
       
      
      
        
       
      
        
        
        
        internal class XWinCoreMenuRendererColorTable : ProfessionalColorTable
        {

            public XWinCoreMenuRendererColorTable(){
                this.UseSystemColors = false;
            }
            public override System.Drawing.Color SeparatorDark{
                get{
                    return WinCoreControlRenderer.MenuSeparatorDark.ToGdiColor();
                }
            }
            public override System.Drawing.Color SeparatorLight
            {
                get
                {
                    return WinCoreControlRenderer.MenuSeparatorLight.ToGdiColor();
                }
            }
            public override System.Drawing.Color ToolStripBorder
            {
                get
                {
                    return WinCoreControlRenderer.ToolStripBorder.ToGdiColor();
                }
            }
            public override System.Drawing.Color MenuItemSelected
            {
                get
                {
                    return WinCoreControlRenderer.MenuItemSelectedBackgroundColor.ToGdiColor();
                }
            }
            public override System.Drawing.Color MenuBorder
            {
                get
                {
                    return WinCoreControlRenderer.MenuBorder.ToGdiColor();
                }
            }
            public override System.Drawing.Color ToolStripDropDownBackground
            {
                get
                {
                    return WinCoreControlRenderer.MenuToolStripDropDownBackground.ToGdiColor();
                }
            }
            

            public override System.Drawing.Color ButtonCheckedGradientBegin
            {
                get
                {
                    return WinCoreControlRenderer.MenuButtonCheckedGradientBegin.ToGdiColor();
                }
            }
            public override System.Drawing.Color ButtonCheckedGradientEnd
            {
                get
                {
                    return WinCoreControlRenderer.MenuButtonCheckedGradientEnd.ToGdiColor();
                }
            }
           
            public override System.Drawing.Color CheckBackground
            {
                get
                {
                    return WinCoreControlRenderer.MenuCheckBackground.ToGdiColor();
                }
            }
            public override System.Drawing.Color CheckPressedBackground
            {
                get
                {
                    return WinCoreControlRenderer.MenuCheckPressedBackground.ToGdiColor();
                }
            }
            public override System.Drawing.Color ButtonCheckedGradientMiddle
            {
                get
                {
                    return WinCoreControlRenderer.MenuButtonCheckedGradientMiddle.ToGdiColor();
                }
            }
            public override System.Drawing.Color ButtonCheckedHighlight
            {
                get
                {
                    return WinCoreControlRenderer.MenuButtonCheckedHighlight.ToGdiColor();
                }
            }
            public override System.Drawing.Color ButtonCheckedHighlightBorder
            {
                get
                {
                    return WinCoreControlRenderer.MenuButtonCheckedHighlightBorder.ToGdiColor();
                }
            }
          
            public override System.Drawing.Color ButtonPressedBorder
            {
                get
                {
                    return WinCoreControlRenderer.MenuButtonPressedBorder.ToGdiColor();
                }
            }
            public override System.Drawing.Color ButtonPressedGradientBegin
            {
                get
                {
                    return WinCoreControlRenderer.MenuButtonPressedGradientBegin.ToGdiColor();
                }
            }
        
            public override System.Drawing.Color ButtonPressedGradientEnd
            {
                get
                {
                    return WinCoreControlRenderer.MenuButtonPressedGradientEnd.ToGdiColor();
                }
            }
         
            public override System.Drawing.Color ButtonPressedGradientMiddle
            {
                get
                {
                    return WinCoreControlRenderer.MenuButtonPressedGradientMiddle.ToGdiColor();
                }
            }
          
            public override System.Drawing.Color MenuItemBorder
            {
                get
                {
                    return WinCoreControlRenderer.MenuItemBorder.ToGdiColor();
                }
            }
            public override System.Drawing.Color MenuStripGradientBegin
            {
                get
                {
                    return WinCoreControlRenderer.MenuStripGradientBegin.ToGdiColor();
                }
            }
            public override System.Drawing.Color MenuStripGradientEnd
            {
                get
                {
                    return WinCoreControlRenderer.MenuStripGradientEnd.ToGdiColor();
                }
            }
            public override System.Drawing.Color MenuItemPressedGradientBegin
            {
                get
                {
                    return WinCoreControlRenderer.MenuItemPressedGradientBegin.ToGdiColor();
                }
            }

            public override System.Drawing.Color MenuItemPressedGradientEnd
            {
                get
                {
                    return WinCoreControlRenderer.MenuItemPressedGradientEnd.ToGdiColor();
                }

            }
            public override System.Drawing.Color MenuItemPressedGradientMiddle
            {
                get
                {
                    return WinCoreControlRenderer.MenuItemPressedGradientMiddle.ToGdiColor();
                }
            }
        
            public override System.Drawing.Color MenuItemSelectedGradientBegin
            {
                get
                {
                    return WinCoreControlRenderer.MenuItemSelectedGradientBegin.ToGdiColor();
                }
            }
          
            public override System.Drawing.Color MenuItemSelectedGradientEnd
            {
                get
                {
                    return WinCoreControlRenderer.MenuItemSelectedGradientEnd.ToGdiColor();
                }
            }
            public override System.Drawing.Color ToolStripPanelGradientEnd
            {
                get
                {
                    return WinCoreControlRenderer.MenuToolStripPanelGradientEnd.ToGdiColor();
                }
            }
            public override System.Drawing.Color ToolStripPanelGradientBegin
            {
                get
                {
                    return WinCoreControlRenderer.MenuToolStripPanelGradientBegin.ToGdiColor();
                }
            }
            public override System.Drawing.Color ButtonPressedHighlight
            {
                get
                {
                    return WinCoreControlRenderer.MenuButtonPressedHighlight.ToGdiColor();
                }
            }

            public override System.Drawing.Color ButtonPressedHighlightBorder
            {
                get
                {
                    return WinCoreControlRenderer.MenuButtonPressedHighlightBorder.ToGdiColor();
                }
            }
            public override System.Drawing.Color ButtonSelectedBorder
            {
                get
                {
                    return WinCoreControlRenderer.MenuButtonSelectedBorder.ToGdiColor();
                }
            }
           
            public override System.Drawing.Color ButtonSelectedGradientBegin
            {
                get
                {
                    return WinCoreControlRenderer.MenuButtonSelectedGradientBegin.ToGdiColor();
                }
            }
            public override System.Drawing.Color ButtonSelectedGradientEnd
            {
                get
                {
                    return WinCoreControlRenderer.MenuButtonSelectedGradientEnd.ToGdiColor();
                }
            }
            public override System.Drawing.Color ButtonSelectedHighlightBorder
            {
                get
                {
                    return WinCoreControlRenderer.MenuButtonSelectedHighlightBorder.ToGdiColor();
                }
            }
            public override System.Drawing.Color ButtonSelectedGradientMiddle
            {
                get
                {
                    return WinCoreControlRenderer.MenuButtonSelectedGradientMiddle.ToGdiColor();
                }
            }
        
            public override System.Drawing.Color ButtonSelectedHighlight
            {
                get
                {
                    return WinCoreControlRenderer.MenuButtonSelectedHighlight.ToGdiColor();
                }
            }
         
            public override System.Drawing.Color CheckSelectedBackground
            {
                get
                {
                    return WinCoreControlRenderer.MenuCheckSelectedBackground.ToGdiColor();
                }
            }

            public override System.Drawing.Color ToolStripContentPanelGradientBegin
            {
                get
                {
                    return WinCoreControlRenderer.MenuToolStripContentPanelGradientBegin.ToGdiColor();
                }
            }
            public override System.Drawing.Color ImageMarginGradientBegin
            {
                get
                {
                    return WinCoreControlRenderer.MenuImageMarginGradientBegin.ToGdiColor();
                }
            }
            public override System.Drawing.Color ImageMarginGradientEnd
            {
                get
                {
                    return WinCoreControlRenderer.MenuImageMarginGradientEnd.ToGdiColor();
                }
            }
            public override System.Drawing.Color ImageMarginGradientMiddle
            {
                get
                {
                    return WinCoreControlRenderer.MenuImageMarginGradientMiddle.ToGdiColor();
                }
            }
            public override System.Drawing.Color GripDark
            {
                get
                {
                    return WinCoreControlRenderer.MenuGripDark.ToGdiColor();
                }
            }
            public override System.Drawing.Color GripLight
            {
                get
                {
                    return WinCoreControlRenderer.MenuGripLight.ToGdiColor();
                }
            }
            public override System.Drawing.Color ImageMarginRevealedGradientBegin
            {
                get
                {
                    return WinCoreControlRenderer.MenuImageMarginRevealedGradientBegin.ToGdiColor();
                }
            }
            public override System.Drawing.Color ImageMarginRevealedGradientEnd
            {
                get
                {
                    return WinCoreControlRenderer.MenuImageMarginRevealedGradientEnd.ToGdiColor();
                }
            }
            public override System.Drawing.Color ImageMarginRevealedGradientMiddle
            {
                get
                {
                    return WinCoreControlRenderer.MenuImageMarginRevealedGradientMiddle.ToGdiColor();
                }
            }
            public override System.Drawing.Color OverflowButtonGradientBegin
            {
                get
                {
                    return WinCoreControlRenderer.MenuOverflowButtonGradientBegin.ToGdiColor();
                }
            }
         
            public override System.Drawing.Color OverflowButtonGradientEnd
            {
                get
                {
                    return WinCoreControlRenderer.MenuOverflowButtonGradientEnd.ToGdiColor();
                }
            }
            public override System.Drawing.Color OverflowButtonGradientMiddle
            {
                get
                {
                    return WinCoreControlRenderer.MenuOverflowButtonGradientMiddle.ToGdiColor();
                }
            }
            public override System.Drawing.Color RaftingContainerGradientBegin
            {
                get
                {
                    return WinCoreControlRenderer.MenuRaftingContainerGradientBegin.ToGdiColor();
                }
            }
            public override System.Drawing.Color RaftingContainerGradientEnd
            {
                get
                {
                    return WinCoreControlRenderer.MenuRaftingContainerGradientEnd.ToGdiColor();
                }
            }
        
            public override System.Drawing.Color StatusStripGradientBegin
            {
                get
                {
                    return WinCoreControlRenderer.MenuStatusStripGradientBegin.ToGdiColor();
                }
            }
            public override System.Drawing.Color StatusStripGradientEnd
            {
                get
                {
                    return WinCoreControlRenderer.MenuStatusStripGradientEnd.ToGdiColor();
                }
            }
            public override System.Drawing.Color ToolStripContentPanelGradientEnd
            {
                get
                {
                    return WinCoreControlRenderer.MenuToolStripContentPanelGradientEnd.ToGdiColor();
                }
            }
            public override System.Drawing.Color ToolStripGradientBegin
            {
                get
                {
                    return WinCoreControlRenderer.MenuToolStripGradientBegin.ToGdiColor();
                }
            }
          
            public override System.Drawing.Color ToolStripGradientEnd
            {
                get
                {
                    return WinCoreControlRenderer.MenuToolStripGradientEnd.ToGdiColor();
                }
            }
            public override System.Drawing.Color ToolStripGradientMiddle
            {
                get
                {
                    return WinCoreControlRenderer.MenuToolStripGradientMiddle.ToGdiColor();
                }
            }
            
        }
    }
}
