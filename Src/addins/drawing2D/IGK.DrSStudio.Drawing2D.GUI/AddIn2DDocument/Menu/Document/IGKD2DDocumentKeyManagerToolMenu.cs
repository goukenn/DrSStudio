

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DDocumentKeyManagerToolMenu.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.DrSStudio.Drawing2D.Tools;
using IGK.ICore;
using IGK.ICore.Actions;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:IGKD2DDocumentKeyManagerToolMenu.cs
*/
using IGK.ICore.Drawing2D.Tools;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D.Menu.Document
{
    [IGKD2DDocumentMenuAttribute("KeyManager", 0,
        Shortcut = enuKeys.Control |enuKeys.D,
        IsVisible=false,
        IsShortcutMenuChild=false)]
    sealed class IGKD2DDocumentKeyManagerToolMenu : IGKD2DDrawingDocumentMenuBase
    {
        protected override bool PerformAction()
        {
            if (this.Workbench is ICoreWorkbenchMessageFilter m)
                CoreActionBase.StartFilteringAction(
             m,
             IGKD2DDocumentKeyManagerTool.Instance);
            return true;
        }
        protected override bool IsEnabled()
        {
            return (this.CurrentSurface != null);
        }
        protected override bool IsVisible()
        {
            return base.IsVisible();
        }
      
    }
}

