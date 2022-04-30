

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: BitmapExtensions.cs
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
using System.Drawing;

namespace IGK.DrSStudio.Imaging.Effects
{
    /// <summary>
    /// Provides effect related extensions for the Bitmap class.
    /// </summary>
    public static class BitmapExtensions
    {
        /// <summary>
        /// Applys an effect to a Bitmap.
        /// </summary>
        /// <param name="bitmap">The Bitmap to apply the effect to.</param>
        /// <param name="effect">The Effect to apply.</param>
        /// <param name="rectOfInterest">
        /// The rectangle to apply the Effect or Rectangle.Empty 
        /// for entire bitmap.
        /// </param>
        /// <exception cref="GDIPlusX.GDIPlus11NotAvailableException">GDI Plus 1.1 functions not available.</exception>
        /// <exception cref="System.ArgumentNullException">bitmap is null or effect is null.</exception>
        /// <exception cref="GDIPlusX.Effects.EffectValidateException">Effect validation with bitmap failed.</exception>
        /// <remarks>Auxillary data is calculated if the effect supports it.</remarks>
        public static void ApplyEffect(this Bitmap bitmap, Effect effect, Rectangle rectOfInterest)
        {
            // Exception checks
            if (effect == null) throw new ArgumentNullException("effect");

            // Apply effect
            effect.ApplyToBitmap(bitmap, rectOfInterest);
        }

        /// <summary>
        /// Copies a bitmap and applys an effect
        /// </summary>
        /// <param name="bitmap">The Bitmap to apply the effect to.</param>
        /// <param name="effect">The Effect to apply.</param>
        /// <returns>A new bitmap object with the effect applied.</returns>
        /// <exception cref="GDIPlusX.GDIPlus11NotAvailableException">GDI Plus 1.1 functions not available.</exception>
        /// <exception cref="System.ArgumentNullException">bitmap is null or effect is null.</exception>
        /// <exception cref="GDIPlusX.Effects.EffectValidateException">Effect validation with bitmap failed.</exception>
        /// <remarks>Auxillary data is calculated if the effect supports it.</remarks>
        public static Bitmap CloneWithEffect(this Bitmap bitmap, Effect effect)
        {
            Rectangle lrRect = Rectangle.Empty;
            return CloneWithEffect(bitmap, effect, ref lrRect);
        }

        /// <summary>
        /// Copies an area bitmap and applys an effect
        /// </summary>
        /// <param name="bitmap">The Bitmap to apply the effect to.</param>
        /// <param name="effect">The Effect to apply.</param>
        /// <param name="rect">
        /// The rectangle to apply the Effect or Rectangle.Empty for 
        /// entire bitmap, on out the area actually applied.
        /// </param>
        /// <returns>A new bitmap object with the effect applied.</returns>
        /// <exception cref="GDIPlusX.GDIPlus11NotAvailableException">GDI Plus 1.1 functions not available.</exception>
        /// <exception cref="System.ArgumentNullException">bitmap is null or effect is null.</exception>
        /// <exception cref="GDIPlusX.Effects.EffectValidateException">Effect validation with bitmap failed.</exception>
        /// <remarks>Auxillary data is calculated if the effect supports it.</remarks>
        public static Bitmap CloneWithEffect(this Bitmap bitmap, Effect effect, ref Rectangle rect)
        {
            // Exception checks
            if (effect == null) throw new ArgumentNullException("effect");

            // Apply effect
            return effect.CloneApply(bitmap, ref rect);
        }
    }
}
