

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _Copy.cs
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
file:_Copy.cs
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
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IGK.DrSStudio.ContextMenu;
namespace IGK.DrSStudio.Drawing2D.ContextMenu
{
    using IGK.ICore.ContextMenu;
    using IGK.ICore.WinUI;

    [DrSStudioContextMenu("Drawing2D.Copy", 
        IGKD2DrawingConstant.CONTEXT_MENU_BASE_INDEX - 300, 
        ImageKey = "Menu_Copy",
        ShortCut  = enuKeys.Control | enuKeys.C 
        )]
    class _CopyContextMenu : Editable2DSurfaceContextMenu 
    {
        protected override bool PerformAction()
        {
            if (this.CurrentSurface.CanCopy)
                this.CurrentSurface.Copy();
            return false;
        }
        /// <summary>
        /// .ctr of the copy menu
        /// </summary>
        public _CopyContextMenu()
        {

        }
        protected override void InitContextMenu()
        {
            base.InitContextMenu();
        }
        protected override bool IsVisible()
        {
            return base.IsVisible();
        }
        protected override bool IsEnabled()
        {
            return this.IsVisible() && this.CurrentSurface.CanCopy;
        }
    }
}

