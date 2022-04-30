

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuActionType.cs
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
file:enuActionType.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.Actions
{
    [Flags ()]
    /// <summary>
    /// represent the action mecanism type
    /// </summary>
    public enum enuActionType
    {
        /// <summary>
        /// represent a global system action
        /// </summary>
        SystemAction = 0x1,
        /// <summary>
        /// represent a menu action
        /// </summary>
        MenuAction = 0x2,
        /// <summary>
        /// represent the context menu action
        /// </summary>
        ContextMenuAction = 0x4,
        /// <summary>
        /// represent a spécific mecanism action
        /// </summary>
        MecanismAction = 0x8,
        /// <summary>
        /// represent a surface action
        /// </summary>
        SurfaceAction = 0x10
    }
}

