

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: TogglePageViewBackward.cs
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
file:TogglePageViewBackward.cs
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
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio.WinUI
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.WinUI;
    using IGK.ICore.Menu;
    using IGK.DrSStudio.Menu;
    [DrSStudioMenu("LayoutManager.ToggleView.Backward", -1,
        IsVisible = false, Shortcut = enuKeys.Control | enuKeys.Tab | enuKeys.Shift 
        )]
    sealed class TogglePaveViewBackward : CoreApplicationMenu
    {
        public TogglePaveViewBackward()
        {
            this.IsRootMenu = true;
        }
        protected override bool IsEnabled()
        {
            return true;
        }
        protected override void InitMenu()
        {
            base.InitMenu();
        }
        protected override bool PerformAction()
        {
            if (Workbench.GetLayoutManager() is DrSStudioLayoutManager l)
            l.SurfaceContainer.MoveToPreviousTab();
            return base.PerformAction();
        }
        protected override void OnWorkbenchChanged(EventArgs eventArgs)
        {
            base.OnWorkbenchChanged(eventArgs);
        }
    }
}

