

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ContextCopySelection.cs
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
file:ContextCopySelection.cs
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
namespace IGK.DrSStudio.Drawing2D.ImageSelection.ContextMenu
{
    using IGK.ICore;using IGK.DrSStudio.ContextMenu;
    [CoreContextMenu(_ImageSelection.CONTEXTMENU_COPYSELECTION, 51)]
    sealed class CoreContextMenu_CopySelection : _ImageSelectionContextMenuBase
    {
        protected override bool PerformAction()
        {
            try
            {
                Mecanism.CopySelection();
            }
            catch{
                CoreLog.WriteDebug("Copy mecanism failed");
            }
            return true;
        }
    }
}

