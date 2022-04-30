

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreSurfaceClosedEventArgs.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.WinUI
{
    /// <summary>
    /// argument passed when a surafce a removed from workbench and determine that the surface must be disposed
    /// </summary>
    public class CoreSurfaceClosedEventArgs : EventArgs 
    {
        private bool m_CancelDispose;
        private enuSurfaceCloseReason m_Reason;
        
        public enuSurfaceCloseReason Reason
        {
            get { return m_Reason; }
            set
            {
                if (m_Reason != value)
                {
                    m_Reason = value;
                }
            }
        }

        public bool CancelDispose
        {
            get { return m_CancelDispose; }
            set
            {
                if (m_CancelDispose != value)
                {
                    m_CancelDispose = value;
                }
            }
        }
        private ICoreWorkingSurface m_Surface;

        public ICoreWorkingSurface Surface
        {
            get { return m_Surface; }
        }
        public CoreSurfaceClosedEventArgs(ICoreWorkingSurface surface)
        {
            this.m_Surface = surface;
            this.m_Reason = enuSurfaceCloseReason.SurfaceRemoved;
        }
    }
}
