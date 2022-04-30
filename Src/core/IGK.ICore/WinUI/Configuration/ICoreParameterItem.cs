

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreParameterItem.cs
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
file:ICoreParameterItem.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.WinUI.Configuration
{
    /// <summary>
    /// represent the base parameters type
    /// </summary>
    public interface ICoreParameterItem : ICoreParameterEntry
    {
        /// <summary>
        /// get or set the value of the item
        /// </summary>
        object Value { get; set; }
        event EventHandler ValueChanged;
        ///// <summary>
        ///// restore the default value
        ///// </summary>
        void RestoreDefault();
    }
}

