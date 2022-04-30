

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CurveAdjustmentExtensions.cs
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
using System.Reflection;
using IGK.DrSStudio.Imaging.EffectsInternal;

namespace IGK.DrSStudio.Imaging.Effects
{
    /// <summary>
    /// Provides extension methods for the CurveAdjustment enumeration.
    /// </summary>
    public static class CurveAdjustmentExtensions
    {
        /// <summary>
        /// Gets the minimum and maximum adjustment value for a CurveAdjustment member.
        /// </summary>
        /// <param name="curveAdjustment">The curve adjustment to get the min and max for.</param>
        /// <param name="min">On out the minimum adjustment value.</param>
        /// <param name="max">On out the maximum adjustment value.</param>
        /// <exception cref="System.InvalidOperationException">Attribute cannot be found.</exception>
        public static void MinMax(this CurveAdjustment curveAdjustment, out int min, out int max)
        {
            Type ltType = typeof(CurveAdjustment);

            // Get the member information
            MemberInfo[] lmiInfos = ltType.GetMember(curveAdjustment.ToString());

            if (lmiInfos.Length > 0)
            {
                // Get the attribute
                object[] loAtts =
                    lmiInfos[0].GetCustomAttributes(typeof(CurveAdjustmentValueRangeAttribute), false);

                if (loAtts.Length > 0)
                {
                    CurveAdjustmentValueRangeAttribute laAttr = 
                        (CurveAdjustmentValueRangeAttribute)loAtts[0];

                    // Set the min max
                    min = laAttr.Min;
                    max = laAttr.Max;
                    return;
                }
            }

            throw new InvalidOperationException();
        }
    }
}
