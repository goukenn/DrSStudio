

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _Undo.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Menu;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:_Undo.cs
*/
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.DrSStudio.Menu.Edit
{
    [CoreMenu  ("Edit.Undo", 11, 
        ImageKey=CoreImageKeys.MENU_UNDO_GKDS, 
        Shortcut=enuKeys.Z | enuKeys.Control ,
        SeparatorBefore=true)]
    class _Undo : UndoableMenucAction
    {
        protected override bool PerformAction()
        {
            if (this.CurrentSurface.CanUndo )
                this.CurrentSurface.Undo();
            return base.PerformAction();
        }
    }
}

