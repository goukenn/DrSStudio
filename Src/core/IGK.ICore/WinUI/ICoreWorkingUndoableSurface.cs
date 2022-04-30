

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreWorkingUndoableSurface.cs
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
file:ICoreWorkingUndoableSurface.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.WinUI
{
    using IGK.ICore;using IGK.ICore.Actions;
    /// <summary>
    /// represent a surface that can't support Undo/Redo operation
    /// </summary>
    public interface ICoreWorkingUndoableSurface :
        ICoreWorkingSurface ,
        ICoreUndoAndRedoAction
    {
        /// <summary>
        /// get if can undo
        /// </summary>
        bool CanUndo { get; }
        /// <summary>
        /// gete if can redo
        /// </summary>
        bool CanRedo { get; }
    }
}

