

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _Paste.cs
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
file:_Paste.cs
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
using IGK.ICore.Actions;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.DrSStudio.Menu.Edit
{
    [DrSStudioMenu("Edit.Paste", 3, 
        Shortcut = enuKeys.Control | enuKeys .V,
        ImageKey=CoreImageKeys.MENU_PASTE_GKDS)]
    class _PasteMenu : CoreEditableMenuBase
    {
        public override enuActionType ActionType
        {
            get
            {
                return enuActionType.SurfaceAction;
            }
        }
        protected override bool PerformAction()
        {
            if (this.CurrentSurface.CanPaste )
            {
                this.CurrentSurface.Paste();
            }
            return false;
        }
        protected override bool IsEnabled()
        {
            return (this.CurrentSurface != null);
        }

        protected override bool IsVisible()
        {
            return  base.IsVisible();
        }
    }
}

