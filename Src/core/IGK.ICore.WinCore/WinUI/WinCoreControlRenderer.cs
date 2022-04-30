using IGK.ICore.Drawing2D;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Theme;
using System.Reflection;

namespace IGK.ICore.WinCore.WinUI
{
    /// <summary>
    /// represent a wincore Renderer
    /// </summary>
    public static class WinCoreControlRenderer
    {
        static WinCoreControlRenderer() {
            CoreRendererBase.InitRenderer(MethodInfo.GetCurrentMethod().DeclaringType);
        }

        public static Colorf ButtonDisabledTextColor { get { return CoreRenderer.GetColor(nameof(ButtonDisabledTextColor), Colorf.White); } }
        public static Colorf ButtonDisabledBackgroundColor { get { return CoreRenderer.GetColor(nameof(ButtonDisabledBackgroundColor), Colorf.FromFloat(0.8f)); } }
        public static Colorf ButtonFocusNormalBackgroundColor { get { return CoreRenderer.GetColor(nameof(ButtonFocusNormalBackgroundColor), Colorf.FromFloat (0.7f, 0.8f, 1.0f)); } }
        public static Colorf ButtonFocusNormalStrokeColor { get { return CoreRenderer.GetColor(nameof(ButtonFocusNormalStrokeColor), Colorf.Black); } }
        public static Colorf ButtonBackgroundColor { get { return CoreRenderer.GetColor(nameof(ButtonBackgroundColor), Colorf.FromFloat (0.1f,Colorf.WhiteSmoke)); } }
        public static Colorf ButtonFocusColor { get { return CoreRenderer.GetColor(nameof(ButtonFocusColor), Colorf.White); } }
        public static Colorf ButtonNormalColor { get { return CoreRenderer.GetColor(nameof(ButtonNormalColor), System.Drawing.SystemColors.ButtonFace.ToIGKColor()); } }
        public static Colorf ButtonTextHighLightColor { get { return CoreRenderer.GetColor(nameof(ButtonTextHighLightColor), Colorf.LightBlue); } }
        public static Colorf ButtonHighLightColor { get { return CoreRenderer.GetColor(nameof(ButtonHighLightColor), Colorf.Orange); } }
        public static Colorf ButtonTextNormalColor { get { return CoreRenderer.GetColor(nameof(ButtonTextNormalColor), System.Drawing.SystemColors.ControlText.ToIGKColor()); } }
        public static CoreFont ButtonFont { get { return CoreThemeManager.GetValue<CoreFont>("ButtonFont", "FontName:consolas; Size:8pt; HotKeyPrefix:Show;");  } }




        #region Fonts

        public static CoreFont DefaultFont { get { return CoreThemeManager.GetValue<CoreFont>("DefaultFont", "FontName:Tahoma; size:8pt;"); } }
        public static CoreFont MenuFont {
            get {
                // return CoreThemeManager.GetValue<CoreFont>("MenuFont", "FontName:Consolas; size:11pt;");
                return CoreThemeManager.GetValue<CoreFont>("MenuFont", "FontName:Tahoma; size:8pt;");
            }


        }
        public static CoreFont ContextMenuFont { get { return CoreThemeManager.GetValue<CoreFont>("ContextMenuFont", "FontName:Tahoma; size:8pt;"); } }
        public static CoreFont MainFormCaptionBarTextFont { get { return CoreThemeManager.GetValue<CoreFont>("MainFormCaptionBarTextFont", "FontName:Tahoma; size:10pt;"); } }

        #endregion

        public static Colorf RuleBackgroundColor { get { return CoreRenderer.GetColor(nameof(RuleBackgroundColor), Colorf.FromFloat(0.25f)); } }

        public static Colorf ScrollbarBackgroundColor { get { return CoreRenderer.GetColor(nameof(ScrollbarBackgroundColor), Colorf.WhiteSmoke); } }
        public static Colorf ScrollbarBodyBackgroundColor { get { return CoreRenderer.GetColor(nameof(ScrollbarBodyBackgroundColor), Colorf.Black); } }


        #region SnippetManagement
        public static float SnippetDefaultSize { get { return CoreRenderer.GetFloat("SnippetDefaultSize", 4); } }
        public static float SnippetMinInflate { get { return CoreRenderer.GetFloat("SnippetMinInflate", 3); } }
        public static Colorf SnippetFillColor { get { return CoreRenderer.GetColor(nameof(SnippetFillColor), Colorf.SkyBlue); } }
        public static Colorf SnippetBorderColor { get { return CoreRenderer.GetColor(nameof(SnippetBorderColor), Colorf.Blue); } }
        #endregion

        #region "SurfaceContainer"

        public static Colorf SurfaceContainerBackgroundColor { get { return CoreRenderer.GetColor(nameof(SurfaceContainerBackgroundColor), Colorf.WhiteSmoke); } }
        public static Colorf SurfaceContainerTabBorderColor { get { return CoreRenderer.GetColor(nameof(SurfaceContainerTabBorderColor), Colorf.FromFloat(0.20f)); } }
        public static Colorf SurfaceContainerTabOngletBackgroundColor { get { return CoreRenderer.GetColor(nameof(SurfaceContainerTabOngletBackgroundColor), Colorf.FromFloat(0.6f)); } }
        public static Colorf SurfaceContainerTabOngletForeColor { get { return CoreRenderer.GetColor(nameof(SurfaceContainerTabOngletForeColor), Colorf.Black); } }
        public static Colorf SurfaceContainerTabOngletSelectedForeColor { get { return CoreRenderer.GetColor(nameof(SurfaceContainerTabOngletSelectedForeColor), Colorf.White); } }
        public static Colorf SurfaceContainerTabBackgroundColor { get { return CoreRenderer.GetColor(nameof(SurfaceContainerTabBackgroundColor), Colorf.FromFloat (0.6f)); } }
        public static Colorf SurfaceContainerTabHoverBackgroundColor { get { return CoreRenderer.GetColor(nameof(SurfaceContainerTabHoverBackgroundColor), Colorf.OrangeRed); } }
        public static Colorf SurfaceContainerTabHoverForeColor { get { return CoreRenderer.GetColor(nameof(SurfaceContainerTabHoverForeColor), Colorf.White); } }
        #endregion

        //fonts
        public static CoreFont RuleTextFont { get { return CoreThemeManager.GetValue<CoreFont>("RuleTextFont", "FontName:Tahoma; size:8pt;"); } }



        public static Colorf TreeviewSelectedBackgroundColor { get { return CoreRenderer.GetColor(nameof(TreeviewSelectedBackgroundColor), Colorf.FromFloat(0.6f)); } }
        public static Colorf TreeviewForeColor { get { return CoreRenderer.GetColor(nameof(TreeviewForeColor), Colorf.Black); } }
        public static Colorf TreeviewSelectedForeColor { get { return CoreRenderer.GetColor(nameof(TreeviewSelectedForeColor), Colorf.White); } }


        public static Colorf ConfirmHeaderColor { get { return CoreRenderer.GetColor(nameof(ConfirmHeaderColor), Colorf.DarkGray); } }
        public static Colorf ConfirmContentColor { get { return CoreRenderer.GetColor(nameof(ConfirmContentColor), Colorf.CornflowerBlue); } }
        public static Colorf ConfirmFooterColor { get { return CoreRenderer.GetColor(nameof(ConfirmFooterColor), Colorf.DarkGray); } }




        public static Colorf ProgessBackgroundColor { get { return CoreRenderer.GetColor(nameof(ProgessBackgroundColor), Colorf.Black); } }
        public static Colorf ProgessValueColor { get { return CoreRenderer.GetColor(nameof(ProgessValueColor), Colorf.White); } }

        #region Menu
        public static Colorf MenuButtonSelectedHighlight { get { return CoreRenderer.GetColor(nameof(MenuButtonSelectedHighlight), Colorf.Black); } }
        public static Colorf MenuButtonSelectedHighlightBorder { get { return CoreRenderer.GetColor(nameof(MenuButtonSelectedHighlightBorder), Colorf.Black); } }
        public static Colorf MenuButtonPressedHighlight { get { return CoreRenderer.GetColor(nameof(MenuButtonPressedHighlight), Colorf.Black); } }
        public static Colorf MenuButtonPressedHighlightBorder { get { return CoreRenderer.GetColor(nameof(MenuButtonPressedHighlightBorder), Colorf.Black); } }
        public static Colorf MenuButtonCheckedHighlight { get { return CoreRenderer.GetColor(nameof(MenuButtonCheckedHighlight), Colorf.Black); } }
        public static Colorf MenuButtonCheckedHighlightBorder { get { return CoreRenderer.GetColor(nameof(MenuButtonCheckedHighlightBorder), Colorf.Black); } }
        public static Colorf MenuButtonPressedBorder { get { return CoreRenderer.GetColor(nameof(MenuButtonPressedBorder), Colorf.Black); } }
        public static Colorf MenuButtonSelectedBorder { get { return CoreRenderer.GetColor(nameof(MenuButtonSelectedBorder), Colorf.Black); } }
        public static Colorf MenuButtonCheckedGradientBegin { get { return CoreRenderer.GetColor(nameof(MenuButtonCheckedGradientBegin), Colorf.Black); } }
        public static Colorf MenuButtonCheckedGradientMiddle { get { return CoreRenderer.GetColor(nameof(MenuButtonCheckedGradientMiddle), Colorf.Black); } }
        public static Colorf MenuButtonCheckedGradientEnd { get { return CoreRenderer.GetColor(nameof(MenuButtonCheckedGradientEnd), Colorf.Black); } }
        public static Colorf MenuButtonSelectedGradientBegin { get { return CoreRenderer.GetColor(nameof(MenuButtonSelectedGradientBegin), Colorf.Black); } }
        public static Colorf MenuButtonSelectedGradientMiddle { get { return CoreRenderer.GetColor(nameof(MenuButtonSelectedGradientMiddle), Colorf.Black); } }
        public static Colorf MenuButtonSelectedGradientEnd { get { return CoreRenderer.GetColor(nameof(MenuButtonSelectedGradientEnd), Colorf.Black); } }
        public static Colorf MenuButtonPressedGradientBegin { get { return CoreRenderer.GetColor(nameof(MenuButtonPressedGradientBegin), Colorf.Black); } }
        public static Colorf MenuButtonPressedGradientMiddle { get { return CoreRenderer.GetColor(nameof(MenuButtonPressedGradientMiddle), Colorf.Black); } }
        public static Colorf MenuButtonPressedGradientEnd { get { return CoreRenderer.GetColor(nameof(MenuButtonPressedGradientEnd), Colorf.Black); } }
        public static Colorf MenuCheckBackground { get { return CoreRenderer.GetColor(nameof(MenuCheckBackground), Colorf.Black); } }
        public static Colorf MenuCheckSelectedBackground { get { return CoreRenderer.GetColor(nameof(MenuCheckSelectedBackground), Colorf.Black); } }
        public static Colorf MenuCheckPressedBackground { get { return CoreRenderer.GetColor(nameof(MenuCheckPressedBackground), Colorf.Black); } }
        public static Colorf MenuGripDark { get { return CoreRenderer.GetColor(nameof(MenuGripDark), Colorf.Black); } }
        public static Colorf MenuGripLight { get { return CoreRenderer.GetColor(nameof(MenuGripLight), Colorf.Black); } }
        public static Colorf MenuImageMarginGradientBegin { get { return CoreRenderer.GetColor(nameof(MenuImageMarginGradientBegin), Colorf.Black); } }
        public static Colorf MenuImageMarginGradientMiddle { get { return CoreRenderer.GetColor(nameof(MenuImageMarginGradientMiddle), Colorf.Black); } }
        public static Colorf MenuImageMarginGradientEnd { get { return CoreRenderer.GetColor(nameof(MenuImageMarginGradientEnd), Colorf.Black); } }
        public static Colorf MenuImageMarginRevealedGradientBegin { get { return CoreRenderer.GetColor(nameof(MenuImageMarginRevealedGradientBegin), Colorf.Black); } }
        public static Colorf MenuImageMarginRevealedGradientMiddle { get { return CoreRenderer.GetColor(nameof(MenuImageMarginRevealedGradientMiddle), Colorf.Black); } }

        public static Colorf MenuImageMarginRevealedGradientEnd { get { return CoreRenderer.GetColor(nameof(MenuImageMarginRevealedGradientEnd), Colorf.Black); } }

        public static Colorf MenuStripGradientBegin { get { return CoreRenderer.GetColor(nameof(MenuStripGradientBegin), Colorf.FromFloat(.25f)); } }
        public static Colorf MenuStripGradientEnd { get { return CoreRenderer.GetColor(nameof(MenuStripGradientEnd), Colorf.FromFloat(.25f)); } }

        public static Colorf MenuItemSelectedBackgroundColor { get { return CoreRenderer.GetColor(nameof(MenuItemSelectedBackgroundColor), Colorf.FromFloat (0.6f)); } }
        public static Colorf MenuItemBorder { get { return CoreRenderer.GetColor(nameof(MenuItemBorder), Colorf.Transparent ); } }
        public static Colorf MenuBorder { get { return CoreRenderer.GetColor(nameof(MenuBorder), Colorf.Black); } }

        public static Colorf MenuItemSelectedGradientBegin { get { return CoreRenderer.GetColor(nameof(MenuItemSelectedGradientBegin), Colorf.FromFloat(.35f)); } }
        public static Colorf MenuItemSelectedGradientEnd { get { return CoreRenderer.GetColor(nameof(MenuItemSelectedGradientEnd), Colorf.FromFloat(.35f)); } }

        public static Colorf MenuItemPressedGradientBegin { get { return CoreRenderer.GetColor(nameof(MenuItemPressedGradientBegin), Colorf.FromFloat(.47f) ); } }
        public static Colorf MenuItemPressedGradientMiddle { get { return CoreRenderer.GetColor(nameof(MenuItemPressedGradientMiddle),  Colorf.FromFloat(.47f)); } }
        public static Colorf MenuItemPressedGradientEnd { get { return CoreRenderer.GetColor(nameof(MenuItemPressedGradientEnd), Colorf.FromFloat(.47f)); } }

        public static Colorf MenuRaftingContainerGradientBegin { get { return CoreRenderer.GetColor(nameof(MenuRaftingContainerGradientBegin), Colorf.Black); } }
        public static Colorf MenuRaftingContainerGradientEnd { get { return CoreRenderer.GetColor(nameof(MenuRaftingContainerGradientEnd), Colorf.Black); } }
        public static Colorf MenuSeparatorDark { get { return CoreRenderer.GetColor(nameof(MenuSeparatorDark), Colorf.Black); } }
        public static Colorf MenuSeparatorLight { get { return CoreRenderer.GetColor(nameof(MenuSeparatorLight), Colorf.Black); } }
        public static Colorf MenuStatusStripGradientBegin { get { return CoreRenderer.GetColor(nameof(MenuStatusStripGradientBegin), Colorf.Black); } }
        public static Colorf MenuStatusStripGradientEnd { get { return CoreRenderer.GetColor(nameof(MenuStatusStripGradientEnd), Colorf.Black); } }
        public static Colorf MenuToolStripBorder { get { return CoreRenderer.GetColor(nameof(MenuToolStripBorder), Colorf.Black); } }
        public static Colorf MenuToolStripDropDownBackground { get { return CoreRenderer.GetColor(nameof(MenuToolStripDropDownBackground), Colorf.FromFloat(0.35f)); } }
        public static Colorf MenuToolStripGradientBegin { get { return CoreRenderer.GetColor(nameof(MenuToolStripGradientBegin), Colorf.Black); } }
        public static Colorf MenuToolStripGradientMiddle { get { return CoreRenderer.GetColor(nameof(MenuToolStripGradientMiddle), Colorf.Black); } }
        public static Colorf MenuToolStripGradientEnd { get { return CoreRenderer.GetColor(nameof(MenuToolStripGradientEnd), Colorf.Black); } }
        public static Colorf MenuToolStripContentPanelGradientBegin { get { return CoreRenderer.GetColor(nameof(MenuToolStripContentPanelGradientBegin), Colorf.Black); } }
        public static Colorf MenuToolStripContentPanelGradientEnd { get { return CoreRenderer.GetColor(nameof(MenuToolStripContentPanelGradientEnd), Colorf.Black); } }
        public static Colorf MenuToolStripPanelGradientBegin { get { return CoreRenderer.GetColor(nameof(MenuToolStripPanelGradientBegin), Colorf.Black); } }
        public static Colorf MenuToolStripPanelGradientEnd { get { return CoreRenderer.GetColor(nameof(MenuToolStripPanelGradientEnd), Colorf.Black); } }
        public static Colorf MenuOverflowButtonGradientBegin { get { return CoreRenderer.GetColor(nameof(MenuOverflowButtonGradientBegin), Colorf.Black); } }
        public static Colorf MenuOverflowButtonGradientMiddle { get { return CoreRenderer.GetColor(nameof(MenuOverflowButtonGradientMiddle), Colorf.Black); } }
        public static Colorf MenuOverflowButtonGradientEnd { get { return CoreRenderer.GetColor(nameof(MenuOverflowButtonGradientEnd), Colorf.Black); } }
        public static Colorf MenuStripDisableTextColor { get { return CoreRenderer.GetColor(nameof(MenuStripDisableTextColor), Colorf.FromFloat (0.32f)); } }
        public static Colorf MenuStripHightLightTextColor { get { return CoreRenderer.GetColor(nameof(MenuStripHightLightTextColor), Colorf.FromFloat (0.1f)); } }        
        public static Colorf MenuStripTextColor { get { return CoreRenderer.GetColor(nameof(MenuStripTextColor), Colorf.FromFloat (0.9f)); } }
        public static Colorf MenuStripRootHightLightTextColor { get { return CoreRenderer.GetColor(nameof(MenuStripRootHightLightTextColor), Colorf.FromFloat (0.9f)); } }
        public static Colorf MenuStripRootTextColor { get { return CoreRenderer.GetColor(nameof(MenuStripRootTextColor), Colorf.WhiteSmoke); } }
        public static Colorf MenuStripBorderColor { get { return CoreRenderer.GetColor(nameof(MenuStripBorderColor), Colorf.Black); } }
        public static Colorf MenuStripMarginSeparatorColor { get { return CoreRenderer.GetColor(nameof(MenuStripMarginSeparatorColor), Colorf.Transparent); } }
        public static Colorf MenuStripArrawColor { get { return CoreRenderer.GetColor(nameof(MenuStripArrawColor), Colorf.FromFloat(0.9f)); } }
        #endregion Menu


        //context menu 
        public static Colorf ContextMenuStripTextColor { get { return CoreRenderer.GetColor(nameof(ContextMenuStripTextColor), Colorf.Black); } }
        public static Colorf ContextMenuStripRootHightLightColor { get { return CoreRenderer.GetColor(nameof(ContextMenuStripRootHightLightColor), Colorf.White); } }
        public static Colorf ContextMenuStripRootTextColor { get { return CoreRenderer.GetColor(nameof(ContextMenuStripRootTextColor), Colorf.Black ); } }
        public static Colorf ContextMenuStripHightLightColor { get { return CoreRenderer.GetColor(nameof(ContextMenuStripHightLightColor), Colorf.IndianRed ); } }
        public static Colorf ContextMenuStripMarginSeparatorColor { get { return CoreRenderer.GetColor(nameof(ContextMenuStripMarginSeparatorColor), Colorf.WhiteSmoke); } }
        public static Colorf ContextMenuStripDropDownBackgroundColor { get { return CoreRenderer.GetColor(nameof(ContextMenuStripDropDownBackgroundColor), Colorf.FromFloat (0.65f)); } }
        public static Colorf ContextMenuStripSelectedItemBackgroundColor { get { return CoreRenderer.GetColor(nameof(ContextMenuStripSelectedItemBackgroundColor), Colorf.FromFloat (0.85f, Colorf.OrangeRed)); } }


        //note bool
        public static Colorf NoteBookBarBackgroundColor { get { return CoreRenderer.GetColor(nameof(NoteBookBarBackgroundColor), Colorf.Transparent ); } }
        public static Colorf NoteBookBarForeColor { get { return CoreRenderer.GetColor(nameof(NoteBookBarForeColor), Colorf.White); } }
        public static Colorf NoteBookBarOngletBorderColor { get { return CoreRenderer.GetColor(nameof(NoteBookBarOngletBorderColor), Colorf.Transparent ); } }
        public static Colorf NoteBookBarOngletOverBackgroundColor { get { return CoreRenderer.GetColor(nameof(NoteBookBarOngletOverBackgroundColor), Colorf.Gray); } }
        public static Colorf NoteBookBarOngletBackgroundColor { get { return CoreRenderer.GetColor(nameof(NoteBookBarOngletBackgroundColor), Colorf.FromFloat (0.45f)); } }
        public static Colorf NoteBookBarOngletSelectedForeColor { get { return CoreRenderer.GetColor(nameof(NoteBookBarOngletSelectedForeColor), Colorf.White); } }
        public static Colorf NoteBookBarOngletSelectedOverForeColor { get { return CoreRenderer.GetColor(nameof(NoteBookBarOngletSelectedOverForeColor), Colorf.Black); } }

        public static Colorf NoteBookBarOngletForeColor { get { return CoreRenderer.GetColor(nameof(NoteBookBarOngletForeColor), Colorf.White); } }
        public static Colorf NoteBookBarOngletOverForeColor { get { return CoreRenderer.GetColor(nameof(NoteBookBarOngletOverForeColor), Colorf.White); } }
        public static Colorf NoteBookBorderColor { get { return CoreRenderer.GetColor(nameof(NoteBookBorderColor), Colorf.Transparent); } }
        public static Colorf NoteBookBarOngletSelectedOverBackgroundColor { get { return CoreRenderer.GetColor(nameof(NoteBookBarOngletSelectedOverBackgroundColor), Colorf.WhiteSmoke); } }
        public static Colorf NoteBookBarOngletSelectedBackgroundColor { get { return CoreRenderer.GetColor(nameof(NoteBookBarOngletSelectedBackgroundColor), Colorf.FromFloat (0.25f)); } }


        public static Colorf PictureViewBrowserBorderColor { get { return CoreRenderer.GetColor(nameof(PictureViewBrowserBorderColor), Colorf.DarkBlue); } }
        public static Colorf PictureViewBrowserFileItemBackgroundColor { get { return CoreRenderer.GetColor(nameof(PictureViewBrowserFileItemBackgroundColor), Colorf.FromFloat(0.9f, 0.0f, 0.0f, 0.0f)); } }



        public static Colorf WindowToolSelectedBackgroundColor { get { return CoreRenderer.GetColor(nameof(WindowToolSelectedBackgroundColor), Colorf.FromString("1;1;1;1")); } }
        public static Colorf WindowToolSelectedtemBorderColor { get { return CoreRenderer.GetColor(nameof(WindowToolSelectedtemBorderColor), Colorf.FromString("1;0;0;0")); } }
        public static Colorf WindowToolSelectedForeColor { get { return CoreRenderer.GetColor(nameof(WindowToolSelectedForeColor), Colorf.FromString("1;0;0;0")); } }
        public static Colorf WindowToolSelectedOverForeColor { get { return CoreRenderer.GetColor(nameof(WindowToolSelectedOverForeColor), Colorf.FromString("1;0.53;0.81;0.92")); } }
        public static Colorf ExpenderGroupBackgroundColor { get { return CoreRenderer.GetColor(nameof(ExpenderGroupBackgroundColor), Colorf.FromFloat (0.30f)); } }
        public static Colorf ExpenderGroupStartColor { get { return CoreRenderer.GetColor(nameof(ExpenderGroupStartColor), Colorf.FromString("0;1;1;1")); } }
        public static Colorf ExpenderGroupEndColor { get { return CoreRenderer.GetColor(nameof(ExpenderGroupEndColor), Colorf.FromString("0;0;0;0")); } }
        public static Colorf ExpenderGroupOverEndColor { get { return CoreRenderer.GetColor(nameof(ExpenderGroupOverEndColor), Colorf.FromString("1;1;0.1176471;0")); } }
        public static Colorf ExpenderGroupOverStartColor { get { return CoreRenderer.GetColor(nameof(ExpenderGroupOverStartColor), Colorf.FromString("1;1;0.4705882;0")); } }
        public static Colorf ExpenderGroupForeColor { get { return CoreRenderer.GetColor(nameof(ExpenderGroupForeColor), Colorf.FromString("0.8235294;1;1;1")); } }
        public static Colorf ExpenderGroupOverForeColor { get { return CoreRenderer.GetColor(nameof(ExpenderGroupOverForeColor), Colorf.FromString("0.9921569;1;1;1")); } }
        public static Colorf ExpenderGroupBorderColor { get { return CoreRenderer.GetColor(nameof(ExpenderGroupBorderColor), Colorf.FromString("0.1333333;0.8;0.8;0.8")); } }
        public static Colorf ExpenderItemBorderColor { get { return CoreRenderer.GetColor(nameof(ExpenderItemBorderColor), Colorf.Transparent ); } }
        public static Colorf ExpenderStartColor { get { return CoreRenderer.GetColor(nameof(ExpenderStartColor), Colorf.FromString("1;1;0;0.254902")); } }
        public static Colorf ExpenderEndColor { get { return CoreRenderer.GetColor(nameof(ExpenderEndColor), Colorf.FromString("1;0.4;0.4;0.4")); } }
        public static Colorf ExpenderOverStartColor { get { return CoreRenderer.GetColor(nameof(ExpenderOverStartColor), Colorf.FromString("1;1;0;0.1411765")); } }
        public static Colorf ExpenderOverEndColor { get { return CoreRenderer.GetColor(nameof(ExpenderOverEndColor), Colorf.FromString("0.3;0;1;1")); } }
        public static Colorf ExpenderForeColor { get { return CoreRenderer.GetColor(nameof(ExpenderForeColor), Colorf.FromString("1;0.6;0.6;0.6")); } }
        public static Colorf ExpenderOverForeColor { get { return CoreRenderer.GetColor(nameof(ExpenderOverForeColor), Colorf.FromString("1;1;0.4941176;0")); } }
        public static Colorf ExpenderItemForeColor { get { return CoreRenderer.GetColor(nameof(ExpenderItemForeColor), Colorf.FromString("1;0;0;0")); } }
        public static Colorf ExpenderItemOverForeColor { get { return CoreRenderer.GetColor(nameof(ExpenderItemOverForeColor), Colorf.FromByteRgb (255,162,0)); } }
        public static Colorf ExpenderItemColor { get { return CoreRenderer.GetColor(nameof(ExpenderItemColor), Colorf.FromString("1;0.8431373;0.8431373;0.8431373")); } }
        public static Colorf ExpenderItemOverColor { get { return CoreRenderer.GetColor(nameof(ExpenderItemOverColor), Colorf.FromString("1;0;0;0")); } }
        public static Colorf ExpenderGroupSelectedOverEndColor { get { return CoreRenderer.GetColor(nameof(ExpenderGroupSelectedOverEndColor), Colorf.FromFloat(0.45f)); } }
        public static Colorf ExpenderGroupSelectedOverStartColor { get { return CoreRenderer.GetColor(nameof(ExpenderGroupSelectedOverStartColor), Colorf.FromFloat (0.50f)); } }
        public static Colorf ExpenderGroupSelectedOverForeColor { get { return CoreRenderer.GetColor(nameof(ExpenderGroupSelectedOverForeColor), Colorf.Black); } }
        public static Colorf ExpenderGroupSelectedEndColor { get { return CoreRenderer.GetColor(nameof(ExpenderGroupSelectedEndColor), Colorf.FromFloat(0.25f)); } }
        public static Colorf ExpenderGroupSelectedStartColor { get { return CoreRenderer.GetColor(nameof(ExpenderGroupSelectedStartColor), Colorf.FromFloat(0.30f)); } }
        public static Colorf ExpenderGroupSelectedForeColor { get { return CoreRenderer.GetColor(nameof(ExpenderGroupSelectedOverForeColor), Colorf.White); } }
        public static Colorf ExpenderGroupSelectedTopColor { get { return CoreRenderer.GetColor(nameof(ExpenderGroupSelectedTopColor), Colorf.FromFloat(0.8f, Colorf.Black)); } }


        public static Colorf ToolStripButtonPressedGradientBegin { get { return CoreRenderer.GetColor(nameof(ToolStripButtonPressedGradientBegin), Colorf.FromFloat(0.6f)); } }
        public static Colorf ToolStripButtonPressedGradientEnd { get { return CoreRenderer.GetColor(nameof(ToolStripButtonPressedGradientEnd), Colorf.FromFloat(0.6f)); } }
        public static Colorf ToolStripButtonPressedGradientMiddle { get { return CoreRenderer.GetColor(nameof(ToolStripButtonPressedGradientMiddle), Colorf.FromFloat(0.6f)); } }
        public static Colorf ToolStripButtonSelectedBorder { get { return CoreRenderer.GetColor(nameof(ToolStripButtonSelectedBorder), Colorf.FromFloat(.7f)); } }
        public static Colorf ToolStripDropDownBackground { get { return CoreRenderer.GetColor(nameof(ToolStripDropDownBackground), Colorf.FromFloat(0.3f)); } }
        public static Colorf ToolStripBorder { get { return CoreRenderer.GetColor(nameof(ToolStripBorder), Colorf.Black); } }
        public static Colorf ToolStripButtonSelectedGradientBegin { get { return CoreRenderer.GetColor(nameof(ToolStripButtonSelectedGradientBegin), Colorf.FromFloat(0.6f)); } }
        public static Colorf ToolStripButtonSelectedGradientEnd { get { return CoreRenderer.GetColor(nameof(ToolStripButtonSelectedGradientEnd), Colorf.FromFloat(0.8f)); } }
        public static Colorf ToolStripButtonSelectedGradientMiddle { get { return CoreRenderer.GetColor(nameof(ToolStripButtonSelectedGradientMiddle), Colorf.FromFloat(0.7f)); } }
        public static Colorf ToolStripButtonSelectedHighlightBorder { get { return CoreRenderer.GetColor(nameof(ToolStripButtonSelectedHighlightBorder), Colorf.Black); } }
        public static Colorf ToolStripButtonSelectedHighlight { get { return CoreRenderer.GetColor(nameof(ToolStripButtonSelectedHighlight), Colorf.Black); } }
        public static Colorf ToolStripButtonPressedHighlight { get { return CoreRenderer.GetColor(nameof(ToolStripButtonPressedHighlight), Colorf.CornflowerBlue); } }
        public static Colorf ToolStripButtonCheckedHighlight { get { return CoreRenderer.GetColor(nameof(ToolStripButtonCheckedHighlight), Colorf.Black); } }
        public static Colorf ToolStripOverflowGradientBegin { get { return CoreRenderer.GetColor(nameof(ToolStripOverflowGradientBegin), Colorf.FromFloat(.6f)); } }
        public static Colorf ToolStripOverflowGradientMiddle { get { return CoreRenderer.GetColor(nameof(ToolStripOverflowGradientMiddle), Colorf.FromFloat(.6f)); } }
        public static Colorf ToolStripOverflowGradientEnd { get { return CoreRenderer.GetColor(nameof(ToolStripOverflowGradientEnd), Colorf.FromFloat(.6f)); } }
        public static Colorf ToolStripSeparatorLight { get { return CoreRenderer.GetColor(nameof(ToolStripSeparatorLight), Colorf.FromFloat(.6f)); } }
        public static Colorf ToolStripSeparatorDark { get { return CoreRenderer.GetColor(nameof(ToolStripSeparatorDark), Colorf.FromFloat(.6f)); } }
        public static Colorf ToolStripImageMarginGradientEnd { get { return CoreRenderer.GetColor(nameof(ToolStripImageMarginGradientEnd), Colorf.Black); } }
        public static Colorf ToolStripImageMarginGradientBegin { get { return CoreRenderer.GetColor(nameof(ToolStripImageMarginGradientBegin), Colorf.Black); } }
        public static Colorf ToolStripImageMarginGradientMiddle { get { return CoreRenderer.GetColor(nameof(ToolStripImageMarginGradientMiddle), Colorf.Black); } }
        public static Colorf ToolStripCheckBackground { get { return CoreRenderer.GetColor(nameof(ToolStripCheckBackground), Colorf.Black); } }
        public static Colorf ToolStripButttonCheckedGradientEnd { get { return CoreRenderer.GetColor(nameof(ToolStripButttonCheckedGradientEnd), Colorf.FromFloat(0.6f, 0.6f, 1.0f)); } }
        public static Colorf ToolStripButttonCheckedGradientMiddle { get { return CoreRenderer.GetColor(nameof(ToolStripButttonCheckedGradientMiddle), Colorf.FromFloat(0.6f, 0.6f, 1.0f)); } }
        public static Colorf ToolStripButttonCheckedGradientBegin { get { return CoreRenderer.GetColor(nameof(ToolStripButttonCheckedGradientBegin), Colorf.FromFloat(0.8f, 0.8f, 1.0f)); } }
        public static Colorf ToolStripPanelBackgroundStartColor { get { return CoreRenderer.GetColor(nameof(ToolStripPanelBackgroundStartColor), Colorf.FromFloat(.3f)); } }
        public static Colorf ToolStripPanelBackgroundEndColor { get { return CoreRenderer.GetColor(nameof(ToolStripPanelBackgroundEndColor), Colorf.FromFloat(.3f)); } }
        public static Colorf ToolStripGradientMiddle { get { return CoreRenderer.GetColor(nameof(ToolStripGradientMiddle), Colorf.FromFloat(.4f)); } }
        public static Colorf ToolStripGradientEnd { get { return CoreRenderer.GetColor(nameof(ToolStripGradientEnd), Colorf.FromFloat(.4f)); } }
        public static Colorf ToolStripGradientBegin { get { return CoreRenderer.GetColor(nameof(ToolStripGradientBegin), Colorf.FromFloat(.4f)); } }
        public static Colorf MenuStripCheckedBackColor { get { return CoreRenderer.GetColor(nameof(MenuStripCheckedBackColor), Colorf.FromFloat(0.6f, .4f)); } }
        public static Colorf MenuStripCheckedBorderColor { get { return CoreRenderer.GetColor(nameof(MenuStripCheckedBorderColor), Colorf.FromFloat(.80f, .8f, 1.0f)); } }
        public static Colorf MenuDropDownBackgroundColor { get { return CoreRenderer.GetColor(nameof(MenuDropDownBackgroundColor), Colorf.FromFloat(0.4f)); } }
        public static Colorf ForeColor
        {
            get
            {
                return CoreRenderer.ForeColor;
            }
        }
        public static Colorf BackgroundColor
        {
            get { return CoreRenderer.BackgroundColor; }
        }
        public static Colorf MainFormCaptionBarStartColor
        {
            get
            {
                return CoreRenderer.GetColor(nameof(MainFormCaptionBarStartColor), Colorf.FromFloat(0.4f));
            }
        }
        public static Colorf MainFormEndBorderColor { get { return CoreRenderer.GetColor(nameof(MainFormEndBorderColor), Colorf.FromFloat(0.7f)); } }
        public static Colorf MainFormStartBorderColor { get { return CoreRenderer.GetColor(nameof(MainFormStartBorderColor), Colorf.FromFloat(0.4f)); } }

        public static Colorf MainFormCaptionBarEndColor
        {
            get
            {
                return CoreRenderer.GetColor(nameof(MainFormCaptionBarEndColor), Colorf.FromFloat(0.4f));
            }
        }
        public static Colorf MainFormCaptionBarForeColor
        {
            get
            {
                return CoreRenderer.GetColor(nameof(MainFormCaptionBarForeColor), Colorf.FromFloat(0.9f));
            }
        }
        public static Colorf MainFormRibonBarInnerColor
        {
            get
            {
                return CoreRenderer.GetColor(nameof(MainFormRibonBarInnerColor), Colorf.FromFloat(0.4f));
            }
        }
        public static Colorf MainFormRibonBarOuterColor
        {
            get
            {
                return CoreRenderer.GetColor(nameof(MainFormRibonBarOuterColor), Colorf.FromFloat(0.4f));
            }
        }
        public static Colorf PanelBorderColor { get { return CoreRenderer.GetColor(nameof(PanelBorderColor), Colorf.FromFloat(0.8f)); } }
        #region "Layout Rendering"
        public static Colorf LayoutContainerSurfaceBorderColor { get { return CoreRenderer.GetColor(nameof(LayoutContainerSurfaceBorderColor), Colorf.Transparent); } }
        public static Colorf LayoutContainerTabBorder { get { return CoreRenderer.GetColor(nameof(LayoutContainerTabBorder), LayoutContainerSurfaceBorderColor); } }
        public static Colorf LayoutContainerTabBegin { get { return CoreRenderer.GetColor(nameof(LayoutContainerTabBegin), Colorf.FromFloat(0.3f)); } }
        public static Colorf LayoutContainerTabEnd { get { return CoreRenderer.GetColor(nameof(LayoutContainerTabEnd), Colorf.FromFloat(0.35f)); } }
        public static Colorf LayoutContainerTabOngletOverBegin { get { return CoreRenderer.GetColor(nameof(LayoutContainerTabOngletOverBegin), Colorf.FromFloat(0.5f)); } }
        public static Colorf LayoutContainerTabOngletOverEnd { get { return CoreRenderer.GetColor(nameof(LayoutContainerTabOngletOverEnd), Colorf.FromFloat(0.5f)); } }
        public static Colorf LayoutContainerTabOngletOverFore { get { return CoreRenderer.GetColor(nameof(LayoutContainerTabOngletOverFore), Colorf.White); } }
        public static Colorf LayoutContainerTabOngletFore { get { return CoreRenderer.GetColor(nameof(LayoutContainerTabOngletFore), Colorf.White); } }
        public static Colorf LayoutContainerTabOngletBorder { get { return CoreRenderer.GetColor(nameof(LayoutContainerTabOngletBorder), Colorf.Black); } }
        public static Colorf LayoutContainerTabOngletBackgroundBegin { get { return CoreRenderer.GetColor(nameof(LayoutContainerTabOngletBackgroundBegin), Colorf.FromFloat(0.3f)); } }
        public static Colorf LayoutContainerTabOngletBackgroundEnd { get { return CoreRenderer.GetColor(nameof(LayoutContainerTabOngletBackgroundEnd), Colorf.FromFloat(0.35f)); } }
        //selected
        public static Colorf LayoutContainerTabOngletSelectedOverBegin { get { return CoreRenderer.GetColor(nameof(LayoutContainerTabOngletSelectedOverBegin), new Colorf(0.25f, 0.25f, 0.25f)); } }
        public static Colorf LayoutContainerTabOngletSelectedOverEnd { get { return CoreRenderer.GetColor(nameof(LayoutContainerTabOngletSelectedOverEnd), Colorf.Black); } }
        public static Colorf LayoutContainerTabOngletSelectedBar { get { return CoreRenderer.GetColor(nameof(LayoutContainerTabOngletSelectedBar), Colorf.FromFloat(0.8f)); } }
        public static Colorf LayoutContainerTabOngletSelectedFore { get { return CoreRenderer.GetColor(nameof(LayoutContainerTabOngletSelectedFore), Colorf.White); } }
        //unselected
        public static Colorf LayoutContainerSurfaceUnselectedTabBackgroundColor { get { return CoreRenderer.GetColor(nameof(LayoutContainerSurfaceUnselectedTabBackgroundColor), Colorf.FromFloat(0.4f)); } }
        public static Colorf LayoutContainerSurfaceUnselectedTabOverBackgroundColor { get { return CoreRenderer.GetColor(nameof(LayoutContainerSurfaceUnselectedTabOverBackgroundColor), Colorf.FromFloat(0.8f)); } }
        public static Colorf LayoutContainerSurfaceUnselectedForeColor { get { return CoreRenderer.GetColor(nameof(LayoutContainerSurfaceUnselectedForeColor), Colorf.FromFloat(0.8f)); } }
        public static Colorf LayoutContainerSurfaceUnselectedOverForeColor { get { return CoreRenderer.GetColor(nameof(LayoutContainerSurfaceUnselectedOverForeColor), Colorf.Black); } }
        public static Colorf LayoutContainerTabOngletSelectedOverFore { get { return CoreRenderer.GetColor(nameof(LayoutContainerTabOngletSelectedOverFore), Colorf.White); } }
        public static Colorf LayoutContainerTabBorderColor { get { return CoreRenderer.GetColor(nameof(LayoutContainerTabBorderColor), Colorf.LightGray); } }
        #endregion
        public static Colorf RuleBarBackColor { get { return CoreRenderer.GetColor(nameof(RuleBarBackColor), Colorf.DarkGray); } }

        public static Colorf DialogBorderColor { get { return CoreRenderer.GetColor(nameof(DialogBorderColor), Colorf.LightGray); } }
        public static Colorf DialogCaptionBorderColor { get { return CoreRenderer.GetColor(nameof(DialogCaptionBorderColor), Colorf.Black); } }
        public static Colorf DialogCaptionStartColor { get { return CoreRenderer.GetColor(nameof(DialogCaptionStartColor), Colorf.FromFloat(0.6f)); } }
        public static Colorf DialogCaptionEndColor { get { return CoreRenderer.GetColor(nameof(DialogCaptionEndColor), Colorf.FromFloat(0.65f)); } }
        public static Colorf DialogCaptionForeColor { get { return CoreRenderer.GetColor(nameof(DialogCaptionForeColor), Colorf.White); } }
        public static float DialogCaptionAngle { get { return CoreRenderer.GetFloat("DialogCaptionAngle", 90.0f); } }
        public static Colorf TabControlBorderColor { get { return CoreRenderer.GetColor(nameof(TabControlBorderColor), Colorf.FromFloat(0.5f)); } }
        public static Colorf TabControlSelectedBorderColor { get { return CoreRenderer.GetColor(nameof(TabControlSelectedBorderColor), Colorf.FromFloat(0.4f)); } }
        public static Colorf TabControlOngletSelectedBorderColor { get { return CoreRenderer.GetColor(nameof(TabControlOngletSelectedBorderColor), Colorf.SkyBlue); } }
        public static Colorf TabControlOngletSelectedStartColor { get { return CoreRenderer.GetColor(nameof(TabControlOngletSelectedStartColor), Colorf.Orange); } }
        public static Colorf TabControlOngletSelectedEndColor { get { return CoreRenderer.GetColor(nameof(TabControlOngletSelectedEndColor), Colorf.Orange); } }
        public static Colorf TabControlOngletSelectedForeColor { get { return CoreRenderer.GetColor(nameof(TabControlOngletSelectedForeColor), Colorf.White); } }


        //status bar
        public static Colorf WinCoreActiveStatusBackgroundColor { get { return CoreRenderer.GetColor(nameof(WinCoreActiveStatusBackgroundColor), Colorf.FromFloat (0.05f)); } }
        public static Colorf WinCoreInActiveStatusBackgroundColor { get { return CoreRenderer.GetColor(nameof(WinCoreInActiveStatusBackgroundColor), Colorf.FromFloat (0.60f)); } }
        public static Colorf WinCoreStatusActiveForeColor { get { return CoreRenderer.GetColor(nameof(WinCoreStatusActiveForeColor), Colorf.White ); } }
        public static Colorf WinCoreStatusInactiveForeColor { get { return CoreRenderer.GetColor(nameof(WinCoreStatusInactiveForeColor), Colorf.Black); } }



        public static Colorf ScrollBarBorderDarkColor { get { return CoreRenderer.GetColor(nameof(ScrollBarBorderDarkColor), Colorf.FromFloat(0.1f)); } }
        public static Colorf ScrollBarBorderLightColor { get { return CoreRenderer.GetColor(nameof(ScrollBarBorderLightColor), Colorf.FromFloat(.6f)); } }
        public static Colorf ContextMenuSelectedColor { get { return CoreRenderer.GetColor(nameof(ContextMenuSelectedColor), Colorf.Black); } }
        public static Colorf TrackbarBackgroundEndColor { get { return CoreRenderer.GetColor(nameof(TrackbarBackgroundEndColor), Colorf.Black); } }
        public static Colorf TrackbarBackgroundStartColor { get { return CoreRenderer.GetColor(nameof(TrackbarBackgroundStartColor), Colorf.White); } }
        public static Colorf TrackbarForeEndColor { get { return CoreRenderer.GetColor(nameof(TrackbarForeEndColor), Colorf.Aqua); } }
        public static Colorf TrackbarForeStartColor { get { return CoreRenderer.GetColor(nameof(TrackbarForeStartColor), Colorf.DarkBlue); } }

     
     
    }
}
