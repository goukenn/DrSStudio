

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ILegacyEffect.cs
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
﻿using System.Drawing;

namespace IGK.DrSStudio.Imaging.Effects
{
    /// <summary>
    /// Encapsulates the ability for an effect which has legacy capabilities.
    /// </summary>
    public interface ILegacyEffect 
    {
        /// <summary>
        /// Gets or sets whether to force legacy mode.
        /// </summary>
        bool ForceLegacy { get; set; }

        /// <summary>
        /// Gets whether effect will run in legacy mode.
        /// </summary>
        bool RunningLegacy { get; }
    }
}
