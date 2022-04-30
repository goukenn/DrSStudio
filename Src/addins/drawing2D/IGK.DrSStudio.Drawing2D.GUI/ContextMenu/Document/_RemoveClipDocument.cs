

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _RemoveClipDocument.cs
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
file:_RemoveClipDocument.cs
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
namespace IGK.DrSStudio.Drawing2D.ContextMenu
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.ContextMenu;
    using IGK.ICore.Actions;
    using IGK.DrSStudio.ContextMenu;

    [DrSStudioContextMenu("Drawing2D.Clip.RemoveClipDocument", 4)]
    class _RemoveClipDocument : IGKD2DDrawingContextMenuBase  
    {
        protected override bool PerformAction()
        {
            if (this.CurrentSurface.CurrentDocument .IsClipped )
            {
                this.CurrentSurface.CurrentDocument.SetClip(null);
                this.CurrentSurface.RefreshScene();
            }
            return base.PerformAction();
        }
        public _RemoveClipDocument()
        {
            this.IsRootMenu = false;
        }
        protected override bool IsVisible()
        {
            return base.IsVisible() && this.CurrentSurface.CurrentDocument.IsClipped; 
        }
        protected override bool IsEnabled()
        {
            return base.IsEnabled();
        }
    }
}

