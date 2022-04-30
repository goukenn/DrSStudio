

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
using IGK.ICore.WinCore;
using IGK.ICore.Drawing2D.WinUI;
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
﻿using IGK.DrSStudio.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IGK.DrSStudio.ContextMenu;
using IGK.ICore.WinUI;
namespace IGK.DrSStudio.Drawing2D.ContextMenu.Edit
{
    using IGK.ICore;

    [DrSStudioContextMenu("Drawing2D.Cut",
        IGKD2DrawingConstant.CONTEXT_MENU_BASE_INDEX - 250,
        ImageKey = "Menu_Cut",
        ShortCut = enuKeys.Control | enuKeys.X)]
    class _Cut : Editable2DSurfaceContextMenu 
    {
        protected override bool PerformAction()
        {
            if(this.CurrentSurface.CanCut )
                this.CurrentSurface.Cut();
            return false;
        }
        protected override void OnOpening(EventArgs e)
        {
            ICore2DDrawingSurface v_s = this.CurrentSurface
            as ICore2DDrawingSurface;
            bool v_v = (v_s != null) && (this.OwnerContext.SourceControl == v_s) && (this.AllowContextMenu);
            this.Visible = v_v;
            this.Enabled = v_v && this.CheckOverElements(v_s.PointToClient (
                Vector2f.Round(this.OwnerContext.MouseOpeningLocation)));
        }
    }
}

