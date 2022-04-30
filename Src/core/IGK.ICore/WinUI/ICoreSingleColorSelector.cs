

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreSingleColorSelector.cs
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
file:ICoreSingleColorSelector.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.WinUI
{
    /// <summary>
    /// represent an interface to select a single color
    /// </summary>
    public interface ICoreSingleColorSelector
    {
        /// <summary>
        /// get or set the color
        /// </summary>
        Colorf Color { get; set; }
        /// <summary>
        /// event raised when color changer
        /// </summary>
        event EventHandler ColorChanged;
    }
}

