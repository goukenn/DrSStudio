

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreWorkingSurfaceHost.cs
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
file:ICoreWorkingSurfaceHost.cs
*/
using IGK.ICore;using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore
{
    /// <summary>
    /// hosting a readonly CurrentSurface property
    /// </summary>
    public interface ICoreWorkingSurfaceHost
    {
        /// <summary>
        /// get the current surface
        /// </summary>
        ICoreWorkingSurface CurrentSurface { get;  }
    }
}

