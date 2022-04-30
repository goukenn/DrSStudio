

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreWorkingSurfaceEventArgs.cs
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
file:CoreWorkingSurfaceEventArgs.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore
{
    using IGK.ICore;using IGK.ICore.WinUI;
    public delegate void CoreWorkingSurfaceEventHandler(object o,CoreItemEventArgs<ICoreWorkingSurface> e);
    /// <summary>
    /// argument for surface raiser
    /// </summary>
    public class CoreWorkingSurfaceEventArgs : EventArgs 
    {
        private ICoreWorkingSurface m_Surface;
        public ICoreWorkingSurface Surface
        {
            get { return m_Surface; }
        }
        public CoreWorkingSurfaceEventArgs(ICoreWorkingSurface surface)
        {
            this.m_Surface = surface;
        }
    }
}

