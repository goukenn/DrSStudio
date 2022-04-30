

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ILegacyEffectApply.cs
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

using System.Drawing;
using IGK.DrSStudio.Imaging.Effects;

namespace IGK.DrSStudio.Imaging.EffectsInternal
{
    /// <summary>
    /// Provides functionality for an effect which which supports legacy applying to bitmap.
    /// </summary>
    internal interface ILegacyEffectApply : ILegacyEffect 
    {
        /// <summary>
        /// Applys an effect to a Bitmap using legacy code.
        /// </summary>
        /// <param name="bitmap">The Bitmap to apply the effect to.</param>
        /// <param name="rectOfInterest">
        /// The rectangle to apply the Effect or Rectangle.Empty 
        /// for entire bitmap.
        /// </param>
        /// <remarks>Auxillary data is calculated if the effect supports it.</remarks>
        void LegacyApplyToBitmap(Bitmap bitmap, Rectangle rectOfInterest);

        /// <summary>
        /// Copies an area bitmap and applys an effect using legacy code.
        /// </summary>
        /// <param name="bitmap">The Bitmap to apply the effect to.</param>
        /// <param name="rect">
        /// The rectangle to apply the Effect or Rectangle.Empty for 
        /// entire bitmap, on out the area actually applied.
        /// </param>
        /// <returns>A new bitmap object with the effect applied.</returns>
        /// <remarks>Auxillary data is calculated if the effect supports it.</remarks>
        Bitmap LegacyCloneApply(Bitmap bitmap, ref Rectangle rect);
    }
}
