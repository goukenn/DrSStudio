

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreMecanismAction.cs
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
file:ICoreMecanismAction.cs
*/
using IGK.ICore;using IGK.ICore.WinUI;

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Actions
{
    /// <summary>
    /// represent the mecanism action 
    /// </summary>
    public interface  ICoreMecanismAction : ICoreAction 
    {
        /// <summary>
        /// get or set the params to send to this action
        /// </summary>
        Object Param { get; set; }
        /// <summary>
        /// get or set the key parameter that will throw the action
        /// </summary>
        enuKeys ShortCutDemand {get; set;}
        /// <summary>
        /// get the the mecanism of that action
        /// </summary>
        ICoreWorkingMecanismAction Mecanism { get; set; }
    }
}

