

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ConvertToGroupElement.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:_ConvertToGroupElement.cs
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
    using IGK.ICore;using IGK.DrSStudio.ContextMenu;
    [CoreContextMenu("Drawing2D.Convert.Group", 5,
      CaptionKey = "Edit.Group",
      ShortCut = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G )]
    sealed class _ConvertToGroupElement :  Core2DContextMenuBase 
    {
        public _ConvertToGroupElement()
        {
            this.IsRootMenu = false;
        }
        protected override void InitContextMenu()
        {
            base.InitContextMenu();
        }
        protected override bool PerformAction()
        {
            Workbench.CallAction("Edit.Group");
            return base.PerformAction();
        }
        protected override bool IsEnabled()
        {
            if (this.CurrentSurface ==null)
                return false ;
            return (this.OwnerContext != null) && (this.OwnerContext.SourceControl == this.CurrentSurface)
                && (this.CurrentSurface.CurrentLayer.SelectedElements.Count > 1);
        }
    }
}

