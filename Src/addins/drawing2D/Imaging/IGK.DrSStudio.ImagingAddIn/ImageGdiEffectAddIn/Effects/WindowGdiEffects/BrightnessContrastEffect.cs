

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: BrightnessContrastEffect.cs
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
using IGK.DrSStudio.Imaging.EffectsInternal;
using IGK.DrSStudio.Imaging.Internal;

namespace IGK.DrSStudio.Imaging.Effects
{
    /// <summary>
    /// Encapsulates an effect which alters the brightness and contrast of an area.
    /// </summary>
    /// <see>http://msdn.microsoft.com/en-us/library/ms534423(v=VS.85).aspx</see>
    public class BrightnessContrastEffect : LUTTablesLegacyAuxDataEffectBGRA
    {
        #region Protected Static Locals

        /// <summary>
        /// GUID for the GDI+ brightness contrast effect.
        /// </summary>
        protected static Guid mgEffectGuid = new Guid("{D3A1DBE1-8EC4-4C17-9F4C-EA97AD1C343D}");

        #endregion

        #region Private Locals

        /// <summary>
        /// Holds the parameters for the brightness contrast effect.
        /// </summary>
        private GdiNativeFunction.BrightnessContrastParams mbcParams;


        /// <summary>
        /// Creatse a new brightness contrast effect.
        /// </summary>
        public BrightnessContrastEffect()
            : this(0, 0)
        {
        }

        /// <summary>
        /// Creatse a new brightness contrast effect.
        /// </summary>
        /// <param name="brightnessLevel">The brightness level for the effect. -255 to 255. 0 is no change.</param>
        /// <param name="contrastLevel">The contrast level for the effect. -100 to 100. 0 is no change.</param>
        public BrightnessContrastEffect(int brightnessLevel, int contrastLevel)
            : base(mgEffectGuid)
        {
            BrightnessLevel = brightnessLevel;
            ContrastLevel = contrastLevel;
        }

     
        /// <summary>
        /// Returns the parameter data for this effect.
        /// </summary>
        /// <returns>An object containing the parameter data.</returns>
        protected override object InitialiseParameterData()
        {
            return mbcParams;
        }

        /// <summary>
        /// Gets the lookup tables for this effect when in legacy mode.
        /// </summary>
        /// <param name="b">The alpha lookup table.</param>
        /// <param name="g">The red lookup table.</param>
        /// <param name="r">The green lookup table.</param>
        /// <param name="a">The blue lookup table.</param>
        protected override void GetLegacyLookupTables(out byte[] b, out byte[] g, out byte[] r, out byte[] a)
        {
            r = null;
            LegacyBitmapPerPixelEffect.GetBrightnessContrastLUT(
                mbcParams.BrightnessLevel, mbcParams.ContrastLevel, ref r);

            g = (byte[])r.Clone();
            b = (byte[])r.Clone();

            a = null;
            BitmapPerPixelProcessing.StandardLUT(ref a);
        }

 
        /// <summary>
        /// Gets or sets the brightness level for the effect -255 to 255. 0 is no change.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">BrightnessLevel must be from -255 to 255.</exception>
        public int BrightnessLevel
        {
            get
            {
                return mbcParams.BrightnessLevel;
            }
            set
            {
                if (BrightnessLevel != value)
                {
                    if (value < -255 || value > 255) 
                        throw new ArgumentOutOfRangeException("BrightnessLevel", value, "Must be from -255 to 255");

                    mbcParams.BrightnessLevel = value;

                    // Invalidate the effect parameters
                    InvalidateParameters();
                }
            }
        }

        /// <summary>
        /// Gets or sets the contrast level for the effect -100 to 100. 0 is no change.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">ContrastLevel must be from -100 to 100.</exception>
        public int ContrastLevel
        {
            get
            {
                return mbcParams.ContrastLevel;
            }
            set
            {
                if (ContrastLevel != value)
                {
                    if (value < -100 || value > 100)
                        throw new ArgumentOutOfRangeException("ContrastLevel", value, "Must be from -100 to 100");

                    mbcParams.ContrastLevel = value;

                    // Invalidate the effect parameters
                    InvalidateParameters();
                }
            }
        }

        #endregion
    }
}
