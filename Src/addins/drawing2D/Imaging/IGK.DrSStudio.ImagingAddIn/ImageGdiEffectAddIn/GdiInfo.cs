

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GdiInfo.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Imaging
{
    /// <summary>
    /// Provides general information for the GDIPlusX namespace.
    /// </summary>
    public static class GdiInfo
    {
        #region Public Static Properties

        /// <summary>
        /// Gets whether GDI version 1.0 functions are available. Should always be true.
        /// </summary>
        public static bool Ver10Available
        {
            get
            {
                return GdiNativeFunction.Ver10Available;
            }
        }

        /// <summary>
        /// Gets whether GDI Version 1.1 functions are available. (Should be true for Vista and above).
        /// </summary>
        public static bool Ver11Available
        {
            get
            {
                return GdiNativeFunction.Ver11Available;
            }
        }

        /// <summary>
        /// Gets whether this GDIPlusX library was compiled using faster unsafe code, or slower safe code.
        /// </summary>
        public static bool CompiledWithUnsafe
        {
            get
            {
#if Unsafe
                return true;
#else
                return false;
#endif
            }
        }

        #endregion
    }
}
