

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _PasteHere.cs
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
file:_PasteHere.cs
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
namespace IGK.DrSStudio.Drawing2D.ContextMenu.Edit
{
    using IGK.ICore.WinCore;
using IGK.ICore;
    using IGK.ICore.WinUI;
    using IGK.ICore.Drawing2D.WinUI;
    using System.Drawing;
    using IGK.DrSStudio.ContextMenu;
    [DrSStudioContextMenu("Drawing2D.PasteHere",
        IGKD2DrawingConstant.CONTEXT_MENU_BASE_INDEX - 140,
        SeparatorAfter=true)]
    class _PasteHereContextMenu : _PasteContextMenu
    {
        public new ICoreWorkingPasteAtSurface CurrentSurface {
            get {
                return base.CurrentSurface as ICoreWorkingPasteAtSurface;
            }
        }
        protected override bool PerformAction()
        {
            if (this.CurrentSurface.CanPaste)
            {
                Vector2f v_ptf = this.OwnerContext.MouseOpeningLocation;
                this.CurrentSurface.PasteAt (v_ptf);
            }
            return false;
        }
        protected override bool IsEnabled()
        {
            return base.IsEnabled();
        }
        protected override bool IsVisible()
        {
            return base.IsVisible();
        }
        protected override void OnOpening(EventArgs e)
        {
            this.SetupEnableAndVisibility();
       //     ICore2DDrawingSurface v_s = this.CurrentSurface
       //as ICore2DDrawingSurface;
       //     bool v_v = (v_s != null) && (this.OwnerContext.SourceControl == v_s) && (v_s.Mecanism.AllowContextMenu);
       //     this.Visible = v_v;
       //     this.Enabled = v_v && base.CurrentSurface.CanPaste;
        }
    }
}

