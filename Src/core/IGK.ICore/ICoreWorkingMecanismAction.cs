

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreWorkingMecanismAction.cs
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
file:ICoreWorkingMecanismAction.cs
*/
using IGK.ICore;using IGK.ICore.Actions;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore
{
    /// <summary>
    /// represent a base class for mecanism action parent
    /// </summary>
    public interface  ICoreWorkingMecanismAction :        
        ICoreWorkingSurfaceHost, 
        IDisposable  
    {
        /// <summary>
        /// get the action collection
        /// </summary>
        ICoreMecanismActionCollections Actions { get; }
    }
}

