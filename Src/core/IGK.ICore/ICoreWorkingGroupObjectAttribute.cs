

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreWorkingGroupObjectAttribute.cs
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
file:ICoreWorkingGroupObjectAttribute.cs
*/

﻿using IGK.ICore;using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore
{
    public interface ICoreWorkingGroupObjectAttribute : ICoreWorkingObjectAttribute
    {
        string CaptionKey { get; set; }
        /// <summary>
        /// get the group name of the object
        /// </summary>
        string GroupName { get;  }
        /// <summary>
        /// get the group image key
        /// </summary>
        string GroupImageKey { get; }
        /// <summary>
        /// get the environment name that target the object
        /// </summary>
        string Environment { get;  }
        /// <summary>
        /// get if this working object is visible
        /// </summary>
        bool IsVisible { get; set; }
        /// <summary>
        /// get the keys that shortcut the element
        /// </summary>
        enuKeys Keys { get; set; }
    }
}

