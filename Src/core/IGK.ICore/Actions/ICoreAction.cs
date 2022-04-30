

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreAction.cs
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
file:ICoreAction.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Actions
{
    using IGK.ICore;using IGK.ICore.WinUI;
    /// <summary>
    /// represent the base action
    /// </summary>
    public interface ICoreAction : ICoreIdentifier
    {
        /// <summary>
        /// raised when action performed
        /// </summary>
        event EventHandler ActionPerformed;
        /// <summary>
        /// the default shortcut keys
        /// </summary>
        enuKeys ShortCut { get;}  
        /// <summary>
        /// action
        /// </summary>
        enuActionType ActionType { get;}
        /// <summary>
        /// get the image key
        /// </summary>
        string ImageKey { get; set; }
        /// <summary>
        /// do action
        /// </summary>
        void DoAction();

        /// <summary>
        /// bind exécution context. must be call before DoAction
        /// </summary>
        /// <param name="name"></param>
        void BindExecutionContext(string name);
        object GetActionContext(string name);
        bool IsInContext(string name);
    }
}

