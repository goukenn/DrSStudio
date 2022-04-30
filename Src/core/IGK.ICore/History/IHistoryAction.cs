

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IHistoryAction.cs
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
file:IHistoryAction.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.History
{
    using IGK.ICore;using IGK.ICore.Actions;
    /// <summary>
    /// represent an history action
    /// </summary>
    public interface IHistoryAction : ICoreUndoAndRedoAction
    {
        IHistoryList Owner { get; set; }
        /// <summary>
        /// get the index of the current history action
        /// </summary>
        int Index { get; }
        IHistoryAction Previous { get; set; }
        IHistoryAction Next { get; set; }
        string Info { get; }
        string ImgKey { get; }
    }
}

