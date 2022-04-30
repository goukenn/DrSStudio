

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ColorCurveEffect.cs
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
    /// Encapsulates an effect which alters the brightness curve of an area.
    /// </summary>
    /// <see>http://msdn.microsoft.com/en-us/library/ms534429(v=VS.85).aspx</see>
    public class ColorCurveEffect : LUTTablesLegacyAuxDataEffectBGRA
    {
        #region Protected Static Locals

        /// <summary>
        /// GUID for the GDI+ brightness color curve effect.
        /// </summary>
        protected static Guid mgEffectGuid = new Guid("{DD6A0022-58E4-4A67-9D9B-D48EB881A53D}");
    


        /// <summary>
        /// Holds the parameters for the color curve effect.
        /// </summary>
        private GdiNativeFunction.ColorCurveParams mccParams;

        #endregion

        #region Initialisation

        /// <summary>
        /// Creates a new color curve effect
        /// </summary>
        public ColorCurveEffect()
            : this(CurveAdjustment.Contrast, CurveChannel.All, 0)
        {
        }

        /// <summary>
        /// Creates a new color curve effect
        /// </summary>
        /// <param name="adjustment">The type of adjustment to make.</param>
        /// <param name="channel">The channel(s) to adjust.</param>
        /// <param name="adjustValue">The value to adjust the channel(s) by. Range depends on adjustment. -255 to 255.</param>
        public ColorCurveEffect(CurveAdjustment adjustment, CurveChannel channel, int adjustValue)
            : base(mgEffectGuid)
        {
            Adjustment = adjustment;
            Channel = channel;
            AdjustValue = adjustValue;
        }

        #endregion

        #region Protected Overrides

        /// <summary>
        /// Returns the parameter data for this effect.
        /// </summary>
        /// <returns>An object containing the parameter data.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Adjust value out of range.</exception>
        protected override object InitialiseParameterData()
        {
            int liMin, liMax;
            GetAdjustmentValueRange(Adjustment, out liMin, out liMax);

            if (AdjustValue < liMin || AdjustValue > liMax)
                throw new ArgumentOutOfRangeException(
                    "AdjustValue", AdjustValue,
                    String.Format(
                        "Must be from {0} to {1} when Adjustment is {2}.{3}",
                        liMin, liMax, typeof(CurveAdjustment).FullName, Adjustment)
                    );

            return mccParams;
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
            r = g = b = a = null;

            byte[] lbLUT = null;
            LegacyBitmapPerPixelEffect.GetColorCurveLUT(
                mccParams.Adjustment, mccParams.AdjustValue, ref lbLUT);

            byte[] lbStandard = null;
            BitmapPerPixelProcessing.StandardLUT(ref lbStandard);
            a = lbStandard;
            
            if (mccParams.Channel != GdiNativeFunction.GpCurveChannel.CurveChannelAll)
            {
                if (mccParams.Channel == GdiNativeFunction.GpCurveChannel.CurveChannelBlue) b = lbLUT; else b = (byte[])lbStandard.Clone();
                if (mccParams.Channel == GdiNativeFunction.GpCurveChannel.CurveChannelGreen) g = lbLUT; else g = (byte[])lbStandard.Clone();
                if (mccParams.Channel == GdiNativeFunction.GpCurveChannel.CurveChannelRed) r = lbLUT; else r = (byte[])lbStandard.Clone();
            }
            else
            {
                r = lbLUT;
                g = (byte[])lbLUT.Clone();
                b = (byte[])lbLUT.Clone();
            }
        }

        /// <summary>
        /// Gets the allowed range for a CurveAdjustment type.
        /// </summary>
        /// <param name="adjustment">The adjustment type to get the range for.</param>
        /// <param name="min">On out the minimum adjustment value for the adjustment type.</param>
        /// <param name="max">On out the maximum adjustment value for the adjustment type.</param>
        public static void GetAdjustmentValueRange(CurveAdjustment adjustment, out int min, out int max)
        {
            adjustment.MinMax(out min, out max);
        }
        /// <summary>
        /// Gets the allowed range for the adjustment value based on current adjustment type.
        /// </summary>
        /// <param name="min">On out the minimum adjustment value for the adjustment type.</param>
        /// <param name="max">On out the maximum adjustment value for the adjustment type.</param>
        public void GetAdjustValueRange(out int min, out int max)
        {
            GetAdjustmentValueRange(Adjustment, out min, out max);
        }

        /// <summary>
        /// Gets or sets the adjustment type.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value is out of range.</exception>
        public CurveAdjustment Adjustment
        {
            get
            {
                return (CurveAdjustment)mccParams.Adjustment;
            }
            set
            {
                if (Adjustment != value)
                {
                    GdiNativeFunction.CheckEnumRange<CurveAdjustment>(
                        value,
                        CurveAdjustment.Exposure,
                        CurveAdjustment.BlackSaturation, "Adjustment");

                    mccParams.Adjustment = (GdiNativeFunction.GpCurveAdjustments)value;
                    InvalidateParameters();
                }
            }
        }

        /// <summary>
        /// Gets or sets the adjustment channels.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Value is out of range.</exception>
        public CurveChannel Channel
        {
            get
            {
                return (CurveChannel)mccParams.Channel;
            }
            set
            {
                if (Channel != value)
                {
                    GdiNativeFunction.CheckEnumRange<CurveChannel>(
                        value,
                        CurveChannel.All,
                        CurveChannel.Blue, "Channel");

                    mccParams.Channel = (GdiNativeFunction.GpCurveChannel)value;
                    InvalidateParameters();
                }
            }
        }

        /// <summary>
        /// Gets or sets the adjustment value. Actual range depends on adjustment type. -255 to 255.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">AdjustValue must be from -255 to 255.</exception>
        public int AdjustValue
        {
            get
            {
                return mccParams.AdjustValue;
            }
            set
            {
                if (value < -255 || value > 255)
                    throw new ArgumentOutOfRangeException("AdjustValue", value, "Must be from -255 to 255");

                mccParams.AdjustValue = value;
                InvalidateParameters();
            }
        }

        #endregion
    }
}
