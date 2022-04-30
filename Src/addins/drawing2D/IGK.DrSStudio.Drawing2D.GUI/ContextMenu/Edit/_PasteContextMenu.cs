

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
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio.Drawing2D.ContextMenu
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.WinUI;
    using IGK.ICore.Drawing2D;
    using  IGK.DrSStudio.Drawing2D.WinUI;
    using IGK.DrSStudio.ContextMenu;
    [DrSStudioContextMenu("Drawing2D.Paste", 
        IGKD2DrawingConstant.CONTEXT_MENU_BASE_INDEX -  150,
        ImageKey = "Menu_Paste",
        ShortCut =enuKeys.Control | enuKeys.V,
        SeparatorAfter=true)]
    class _PasteContextMenu : Editable2DSurfaceContextMenu 
    {
        protected override bool PerformAction()
        {     
            if (base.CurrentSurface.CanPaste)
            {                
                base.CurrentSurface.Paste();
            }
            return false;
        }
        protected override bool IsEnabled()
        {
            return this.IsVisible() && this.CurrentSurface.CanPaste;
        }
        protected override bool IsVisible()
        {
            return this.AllowContextMenu && (this.CurrentSurface !=null) && (this.OwnerContext.SourceControl == this.CurrentSurface);
        }
        protected override void OnOpening(EventArgs e)
        {
            this.SetupEnableAndVisibility();
        }
    }
}

