

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: HSLColorConverter.cs
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

namespace IGK.DrSStudio.Imaging.EffectsInternal
{
    #region Aliases

    using Scalar = System.Double;

    #endregion

    /// <summary>
    /// Provides static utility functions for converting from and to HSL color values.
    /// </summary>
    internal static class HSLColorConverter
    {
        #region Public Static Methods

        /// <summary>
        /// Rolls a value to make it in the range 0 to 1.
        /// </summary>
        /// <param name="value">The value to roll into range.</param>
        /// <returns>The rolled value.</returns>
        public static Scalar Roll(Scalar value)
        {
            // If the value is less than zero then add 1 to fractional value
            if (value < 0) value = (Scalar)1.0 + (value - (Scalar)Math.Truncate(value));

            // If the value is more than 1 then subtract whole value
#pragma warning disable IDE0054 // Use compound assignment
            if (value > (Scalar)1.0) value = value - (Scalar)Math.Truncate(value);
#pragma warning restore IDE0054 // Use compound assignment

            // return value
            return value;
        }

        /// <summary>
        /// Clamps a value to make iti in the range 0 to 1.
        /// </summary>
        /// <param name="value">The value to clamp into range.</param>
        /// <returns>The clamped value.</returns>
        public static Scalar Clamp(Scalar value)
        {
            if (value < (Scalar)0.0)
                value = (Scalar)0.0;
            else if (value > (Scalar)1.0)
                value = (Scalar)1.0;
            return value;
        }

        /// <summary>
        /// Applies HSL to alter an RGB value.
        /// </summary>
        /// <param name="r">The Red channel value to alter (0 - 255).</param>
        /// <param name="g">The Green channel value to alter (0 - 255).</param>
        /// <param name="b">The Blue channel value to alter (0 - 255).</param>
        /// <param name="h">The Hue value to alter by (0 - 1).</param>
        /// <param name="s">The Saturation value to multiply by (0 - 1).</param>
        /// <param name="l">The Luminosity value to alter by (0 - 1).</param>
        public static void ApplyHSLToRGB(ref byte r, ref byte g, ref byte b, Scalar h, Scalar s, Scalar l)
        {
            Scalar lsH, lsS, lsL;
            RGB2HSL(r, g, b, out lsH, out lsS, out lsL);
            lsH += h; lsS *= s; lsL += l;
            HSL2RGB(out r, out g, out b, Roll(lsH), Clamp(lsS), Clamp(lsL));
        }

        /// <summary>
        /// Converts HSL values to RGB values.
        /// </summary>
        /// <param name="red">The value to contain the Red channel (0 - 255).</param>
        /// <param name="green">The value to contain the Green channel (0 - 255).</param>
        /// <param name="blue">The value to contain the Blue channel (0 - 255).</param>
        /// <param name="hue">The Hue value to convert (0 - 1).</param>
        /// <param name="sat">The Saturation value to convert (0 - 1).</param>
        /// <param name="lum">The Luminosity value to convert (0 - 1).</param>
        public static void HSL2RGB(out byte red, out byte green, out byte blue, Scalar hue, Scalar sat, Scalar lum)
        {
            Scalar lsValue;
            Scalar lsR, lsG, lsB;

            // Default to gray
            lsR = lsG = lsB = lum;  
 
            // Calculate value
            lsValue = (lum <= (Scalar)0.5) ? (lum * ((Scalar)1.0 + sat)) : (lum + sat - lum * sat);

            if (lsValue > 0)
            {
                Scalar lsM, lsSV, lfSextantAngle, lsVSF, lsMid1, lsMid2;
                int liSextant;

                lsM = lum + lum - lsValue;
                lsSV = (lsValue - lsM) / lsValue;
                hue *= (Scalar)6.0;
                liSextant = (int)hue;
                lfSextantAngle = hue - liSextant;
                lsVSF = lsValue * lsSV * lfSextantAngle;
                lsMid1 = lsM + lsVSF;
                lsMid2 = lsValue - lsVSF;

                switch (liSextant)
                {
                    case 0:
                        lsR = lsValue; lsG = lsMid1; lsB = lsM;
                        break;

                    case 1:
                        lsR = lsMid2; lsG = lsValue; lsB = lsM;
                        break;

                    case 2:
                        lsR = lsM; lsG = lsValue; lsB = lsMid1;
                        break;

                    case 3:
                        lsR = lsM; lsG = lsMid2; lsB = lsValue;
                        break;

                    case 4:
                        lsR = lsMid1; lsG = lsM; lsB = lsValue;
                        break;

                    case 5:
                        lsR = lsValue; lsG = lsM; lsB = lsMid2;
                        break;
                }

            }

            red = (byte)(lsR * 255.0f);
            green = (byte)(lsG * 255.0f);
            blue = (byte)(lsB * 255.0f);
        }

        public static void RGB2HSL(byte rIn, byte gIn, byte bIn, out Scalar hue, out Scalar sat, out Scalar lum)
        {
            Scalar lsR = rIn / (Scalar)255.0;
            Scalar lsG = gIn / (Scalar)255.0;
            Scalar lsB = bIn / (Scalar)255.0;
            Scalar lsMax;
            Scalar lsMin;
            Scalar lsRange;
            Scalar lsNormR, lsNormG, lsNormB;

            // Default to black
            hue = sat = lum = 0; 

            // Calculate ranges
            lsMax = Math.Max(lsR, Math.Max(lsG, lsB));
            lsMin = Math.Min(lsR, Math.Min(lsG, lsB));

            // Calculate luminosity
            lum = (lsMin + lsMax) / (Scalar)2.0;
            if (lum <= 0.0) return;

            // Calculate range
            lsRange = lsMax - lsMin;

            // Calculate saturation
            sat = lsRange;
            if (sat > 0.0)
                sat /= (lum <= (Scalar)0.5) ? (lsMax + lsMin) : ((Scalar)2.0 - lsMax - lsMin);
            else
                return;

            // Calculate normalised values
            lsNormR = (lsMax - lsR) / lsRange;
            lsNormG = (lsMax - lsG) / lsRange;
            lsNormB = (lsMax - lsB) / lsRange;

            // Calculate hue
            if (lsR == lsMax)
                hue = (lsG == lsMin ? (Scalar)5.0 + lsNormB : (Scalar)1.0 - lsNormG);
            else 
                if (lsG == lsMax)
                    hue = (lsB == lsMin ? (Scalar)1.0 + lsNormR : (Scalar)3.0 - lsNormB);
                else
                    hue = (lsR == lsMin ? (Scalar)3.0 + lsNormG : (Scalar)5.0 - lsNormR);
            hue /= (Scalar)6.0;
        }

        #endregion
    }
}
