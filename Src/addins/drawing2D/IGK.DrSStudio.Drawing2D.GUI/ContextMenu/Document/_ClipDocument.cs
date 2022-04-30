

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ClipDocument.cs
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
file:_ClipDocument.cs
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
    using IGK.ICore.Drawing2D;
    [DrSStudioContextMenu("Drawing2D.Clip.Document", 3)]
    class _ClipDocument : IGKD2DDrawingContextMenuBase  
    {
        
        protected override bool PerformAction()
        {
            if (this.CurrentSurface.CurrentLayer.SelectedElements.Count == 1)
            {
                ICore2DDrawingLayeredElement v_element =  this.CurrentSurface.CurrentLayer.SelectedElements[0];
                this.CurrentSurface.CurrentDocument.SetClip(v_element);
                this.CurrentSurface.RefreshScene();
                return true;
            }
            return false;
        }
        public _ClipDocument()
        {
            this.IsRootMenu = false;
        }
        protected override bool IsVisible()
        {
            return base.IsVisible() && !this.CurrentSurface.CurrentDocument.IsClipped;
        }
    }
}

