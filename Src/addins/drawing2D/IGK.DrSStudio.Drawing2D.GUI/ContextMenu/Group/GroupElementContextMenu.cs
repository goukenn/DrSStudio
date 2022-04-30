

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GroupElementContextMenu.cs
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
file:GroupElementContextMenu.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D.ContextMenu.Group
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.ContextMenu;
    using IGK.ICore.Actions;
    using IGK.ICore.WinUI;
    using IGK.DrSStudio.ContextMenu;
    using IGK.ICore.Drawing2D;
       [DrSStudioContextMenu(
           IGKD2DrawingConstant.CMENU_GROUP_ITEM,
       IGKD2DrawingConstant.CONTEXT_MENU_BASE_INDEX + 0x50,
       ShortCut =
       enuKeys.Control |
       enuKeys.G)]
    sealed class GroupElementContextMenu : IGKD2DDrawingChildContextMenuBase
    {
           protected override bool PerformAction()
           {
               this.Workbench.CallAction(IGKD2DrawingConstant.MENU_GROUP);
                return base.PerformAction();
           }
           protected override bool IsEnabled()
           {
               bool v = base.IsEnabled();
               if (v)
               {
                   ICore2DDrawingLayer l = this.CurrentSurface.CurrentLayer;
                   v = (l.SelectedElements.Count > 1);
               }
               return v;
           }
    }
}

