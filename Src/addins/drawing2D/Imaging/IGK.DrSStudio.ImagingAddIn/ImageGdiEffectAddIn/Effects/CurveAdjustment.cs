

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CurveAdjustment.cs
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

using IGK.DrSStudio.Imaging.EffectsInternal;

namespace IGK.DrSStudio.Imaging.Effects
{
    /// <summary>
    /// Specifys what type of adjustment to perform for the ColorCurveEffect.
    /// </summary>
    public enum CurveAdjustment
    {
        /// <summary>
        /// Adjusts exposure (AdjustValue range is -255 to 255).
        /// </summary>
        [CurveAdjustmentValueRange(-255, 255)]
        Exposure = 0,

        /// <summary>
        /// Adjusts density (AdjustValue range is -255 to 255).
        /// </summary>
        [CurveAdjustmentValueRange(-255, 255)]
        Density = 1,

        /// <summary>
        /// Adjusts contrast (AdjustValue range is -100 to 100).
        /// </summary>
        [CurveAdjustmentValueRange(-100, 100)]
        Contrast = 2,

        /// <summary>
        /// Adjusts highlights (AdjustValue range is -100 to 100).
        /// </summary>
        [CurveAdjustmentValueRange(-100, 100)]
        Highlights = 3,

        /// <summary>
        /// Adjusts shadows (AdjustValue range is -100 to 100).
        /// </summary>
        [CurveAdjustmentValueRange(-100, 100)]
        Shadows = 4,

        /// <summary>
        /// Adjusts midtones (AdjustValue range is -100 to 100).
        /// </summary>
        [CurveAdjustmentValueRange(-100, 100)]
        Midtones = 5,

        /// <summary>
        /// Adjusts white saturation (AdjustValue range is 1 to 255).
        /// </summary>
        [CurveAdjustmentValueRange(1, 255)]
        WhiteSaturation = 6,

        /// <summary>
        /// Adjusts black saturation (AdjustValue range is 0 to 254).
        /// </summary>
        [CurveAdjustmentValueRange(0, 254)]
        BlackSaturation = 7
    }
}
