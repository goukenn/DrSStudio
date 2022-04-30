

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IAuxDataEffect.cs
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
﻿//////////////////////////////////////////////////////////////////////////////////
//	GDI+ Extensions
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://csharpgdiplus11.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

using System;

namespace IGK.DrSStudio.Imaging.Effects
{
    /// <summary>
    /// Provides functionality for an effect which supports auxillary data.
    /// </summary>
    internal interface IAuxDataEffect
    {
        /// <summary>
        /// Gets whether the effect should process and return auxillary data.
        /// </summary>
        bool UseAuxData { get; }

        /// <summary>
        /// Sets the data to the effect after an ApplyEffect or DrawImageFX call.
        /// </summary>
        /// <param name="data">A pointer to the data.</param>
        /// <param name="size">The size of the data.</param>
        /// <remarks>The data should NOT be freed, this is automatically done.</remarks>
        void SetAuxData(IntPtr data, int size);
    }
}
