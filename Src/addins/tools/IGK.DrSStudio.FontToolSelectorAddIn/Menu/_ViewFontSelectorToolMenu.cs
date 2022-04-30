

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ViewFontSelectorTool.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.DrSStudio.Tools;
using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:_ViewFontSelectorTool.cs
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
using System;
namespace IGK.DrSStudio.Menu
{
    [DrSStudioMenu("View.FontSelector",
        21,
        ShortcutText = "P",
        Shortcut = enuKeys.P,
        ImageKey=CoreImageKeys.MENU_POLICE_GKDS)]
    public sealed class _ViewFontSelectorToolMenu : CoreViewToolMenuBase 
    {
        public _ViewFontSelectorToolMenu()
            : base(CoreFontToolSelector.Instance)
        {
        }
        protected override void OnSurfaceChanged(EventArgs eventArgs)
        {
            base.OnSurfaceChanged(eventArgs);
            bool v = this.CurrentSurface is ICoreWorkingConfigElementSurface;
            this.Visible = v;
            this.Enabled = v;
        }
    }
}

