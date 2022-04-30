

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreWorkingMecanism_1.cs
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
file:ICoreWorkingMecanism_1.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore
{
    using IGK.ICore;using IGK.ICore.WinUI;
    using IGK.ICore.Actions;
    /// <summary>
    /// represent the base default working mecanism
    /// </summary>
    public interface ICoreWorkingMecanism<T> :
        ICoreWorkingMecanism ,
        ICoreEditableWorkingMecanism
        where T : class , ICoreWorkingObject 
    {
        /// <summary>
        /// get the working element
        /// </summary>
        T Element { get;  }
        /// <summary>
        /// edit the working element
        /// </summary>
        /// <param name="element"></param>
        void Edit(T element);     
        bool AllowActions { get; }

        event EventHandler<CoreWorkingElementChangedEventArgs<T>> ElementChanged;
        
    }
}

