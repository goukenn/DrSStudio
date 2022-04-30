

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _Cut.cs
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
file:_Cut.cs
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
    [DrSStudioMenu("Edit.Cut", 2,
        Shortcut = enuKeys.Control | enuKeys.X,
        ImageKey=CoreImageKeys.MENU_CUT_GKDS)]
    class _Cut : CoreEditableMenuBase
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
            if (this.CurrentSurface.CanCut )
            {
                this.CurrentSurface.Cut ();
            }
            return false;
        }
        protected override bool IsEnabled()
        {
            return (this.CurrentSurface != null);
        }
    }
}

