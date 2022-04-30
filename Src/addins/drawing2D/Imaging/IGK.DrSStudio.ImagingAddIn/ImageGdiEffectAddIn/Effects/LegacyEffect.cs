

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: LegacyEffect.cs
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
using System.Drawing.Imaging;
using IGK.DrSStudio.Imaging.EffectsInternal;
using IGK.DrSStudio.Imaging.Internal;

namespace IGK.DrSStudio.Imaging.Effects
{
    /// <summary>
    /// Encapsulates a basic effect which has legacy support.
    /// </summary>
    public abstract class LegacyEffect : Effect, ILegacyEffectApply
    {
        #region Protected Locals

        /// <summary>
        /// True to force legacy mode.
        /// </summary>
        protected bool mbForceLegacy = false;

        #endregion

        #region Initialisation

        /// <summary>
        /// Creates a new effect from a Guid.
        /// </summary>
        /// <param name="guid">The Guid for the effect.</param>
        public LegacyEffect(Guid guid)
            : base(guid)
        {
        }

        #endregion

        #region Protected Abstract Methods

        /// <summary>
        /// Applys an effect to a Bitmap using legacy code.
        /// </summary>
        /// <param name="bitmap">The Bitmap to apply the effect to.</param>
        /// <param name="rectOfInterest">
        /// The rectangle to apply the Effect or Rectangle.Empty 
        /// for entire bitmap.
        /// </param>
        /// <exception cref="GDIPlusX.GDIPlus11NotAvailableException">GDI Plus 1.1 functions not available.</exception>
        /// <exception cref="System.ArgumentNullException">bitmap is null or effect is null.</exception>
        /// <exception cref="GDIPlusX.Effects.EffectValidateException">Effect validation with bitmap failed.</exception>
        /// <remarks>Auxillary data is calculated if the effect supports it.</remarks>
        protected abstract void LegacyApplyToBitmap(Bitmap bitmap, Rectangle rectOfInterest);

        #endregion

        #region Internal Static Methods

        /// <summary>
        /// Gets the pixel format for the legacy clone apply function.
        /// </summary>
        /// <returns>The pixel format.</returns>
        internal static PixelFormat DefaultLegacyCloneApplyPixelFormat()
        {
            return PixelFormat.Format32bppPArgb;
        }

        /// <summary>
        /// Copies an area bitmap and applys an effect using legacy code.
        /// </summary>
        /// <param name="effect">The effect to apply.</param>
        /// <param name="bitmap">The Bitmap to apply the effect to.</param>
        /// <param name="rect">
        /// The rectangle to apply the Effect or Rectangle.Empty for 
        /// entire bitmap, on out the area actually applied.
        /// </param>
        /// <param name="pixelFormat">The pixel format for the new bitmap.</param>
        /// <returns>A new bitmap object with the effect applied.</returns>
        /// <exception cref="GDIPlusX.GDIPlus11NotAvailableException">GDI Plus 1.1 functions not available.</exception>
        /// <exception cref="System.ArgumentNullException">bitmap is null or effect is null.</exception>
        /// <exception cref="GDIPlusX.Effects.EffectValidateException">Effect validation with bitmap failed.</exception>
        /// <remarks>Auxillary data is calculated if the effect supports it.</remarks>
        internal static Bitmap DefaultLegacyCloneApply(Effect effect, Bitmap bitmap, ref Rectangle rect, PixelFormat pixelFormat)
        {
            // Intersect the rectangle
            Rectangle lrEntire = new Rectangle(new Point(), bitmap.Size);
            rect.Intersect(lrEntire);

            // Clone the bitmap
            Bitmap lbmpBitmap = bitmap.Clone(lrEntire, pixelFormat);

            // Apply to bitmap
            lbmpBitmap.ApplyEffect(effect, rect);

            // Return the value
            return lbmpBitmap;
        }

        #endregion

        #region Protected Virtual Methods

        /// <summary>
        /// Gets the pixel format for the legacy clone apply function.
        /// </summary>
        /// <returns>The pixel format.</returns>
        protected virtual PixelFormat LegacyCloneApplyPixelFormat()
        {
            return DefaultLegacyCloneApplyPixelFormat();
        }

        /// <summary>
        /// Copies an area bitmap and applys an effect using legacy code.
        /// </summary>
        /// <param name="bitmap">The Bitmap to apply the effect to.</param>
        /// <param name="rect">
        /// The rectangle to apply the Effect or Rectangle.Empty for 
        /// entire bitmap, on out the area actually applied.
        /// </param>
        /// <returns>A new bitmap object with the effect applied.</returns>
        /// <exception cref="GDIPlusX.GDIPlus11NotAvailableException">GDI Plus 1.1 functions not available.</exception>
        /// <exception cref="System.ArgumentNullException">bitmap is null or effect is null.</exception>
        /// <exception cref="GDIPlusX.Effects.EffectValidateException">Effect validation with bitmap failed.</exception>
        /// <remarks>Auxillary data is calculated if the effect supports it.</remarks>
        protected virtual Bitmap LegacyCloneApply(Bitmap bitmap, ref Rectangle rect)
        {
            return DefaultLegacyCloneApply(this, bitmap, ref rect, LegacyCloneApplyPixelFormat());
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets whether to force native mode.
        /// </summary>
        public bool ForceLegacy
        {
            get
            {
                return mbForceLegacy;
            }
            set
            {
                if (value != mbForceLegacy)
                {
                    if (value) DisposeEffect();
                    InvalidateParameters();
                    mbForceLegacy = value;
                }
            }
        }

        /// <summary>
        /// Gets whether the effect can be applied in the current environment.
        /// </summary>
        public override bool CanApply
        {
            get
            {
                return RunningLegacy || GdiNativeFunction.Ver11Available;
            }
        }

        /// <summary>
        /// Gets whether the effect is running in legacy mode.
        /// </summary>
        public bool RunningLegacy
        {
            get
            {
                return (!GdiNativeFunction.Ver11Available || mbForceLegacy);
            }
        }

        #endregion

        #region ILegacyEffect Members

        /// <summary>
        /// Applys an effect to a Bitmap using legacy code.
        /// </summary>
        /// <param name="bitmap">The Bitmap to apply the effect to.</param>
        /// <param name="rectOfInterest">
        /// The rectangle to apply the Effect or Rectangle.Empty 
        /// for entire bitmap.
        /// </param>
        /// <exception cref="GDIPlusX.GDIPlus11NotAvailableException">GDI Plus 1.1 functions not available.</exception>
        /// <exception cref="System.ArgumentNullException">bitmap is null or effect is null.</exception>
        /// <exception cref="GDIPlusX.Effects.EffectValidateException">Effect validation with bitmap failed.</exception>
        /// <remarks>Auxillary data is calculated if the effect supports it.</remarks>
        void ILegacyEffectApply.LegacyApplyToBitmap(Bitmap bitmap, Rectangle rectOfInterest)
        {
            LegacyApplyToBitmap(bitmap, rectOfInterest);
        }

        /// <summary>
        /// Copies an area bitmap and applys an effect using legacy code.
        /// </summary>
        /// <param name="bitmap">The Bitmap to apply the effect to.</param>
        /// <param name="rect">
        /// The rectangle to apply the Effect or Rectangle.Empty for 
        /// entire bitmap, on out the area actually applied.
        /// </param>
        /// <returns>A new bitmap object with the effect applied.</returns>
        /// <exception cref="GDIPlusX.GDIPlus11NotAvailableException">GDI Plus 1.1 functions not available.</exception>
        /// <exception cref="System.ArgumentNullException">bitmap is null or effect is null.</exception>
        /// <exception cref="GDIPlusX.Effects.EffectValidateException">Effect validation with bitmap failed.</exception>
        /// <remarks>Auxillary data is calculated if the effect supports it.</remarks>        
        Bitmap ILegacyEffectApply.LegacyCloneApply(Bitmap bitmap, ref Rectangle rect)
        {
            return LegacyCloneApply(bitmap, ref rect);
        }

        #endregion
    }
}
