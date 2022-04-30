

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: HistogramFormat.cs
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

using IGK.DrSStudio.Imaging.Internal;

namespace IGK.DrSStudio.Imaging
{
    /// <summary>
    /// Specifies the format for a histogram
    /// </summary>
    public enum HistogramFormat
    {
        /// <summary>
        /// Alpha, Red, Green, Blue channels.
        /// </summary>
        [HistogramFormatChannelCount(4)]
        ARGB,

        /// <summary>
        /// Premultiplied Alpha, Red, Green, Blue channels.
        /// </summary>
        [HistogramFormatChannelCount(4)]
        PremultipliedARGB,

        /// <summary>
        /// Red, Green, Blue channels.
        /// </summary>
        [HistogramFormatChannelCount(3)]
        RGB,

        /// <summary>
        /// Gray channel only.
        /// </summary>
        [HistogramFormatChannelCount(1)]
        Gray,

        /// <summary>
        /// Blue channel only.
        /// </summary>
        [HistogramFormatChannelCount(1)]
        B,

        /// <summary>
        /// Green channel only.
        /// </summary>
        [HistogramFormatChannelCount(1)]
        G,

        /// <summary>
        /// Red channel only.
        /// </summary>
        [HistogramFormatChannelCount(1)]
        R,


        /// <summary>
        /// Alpha channel only.
        /// </summary>
        [HistogramFormatChannelCount(1)]
        A
    }
}
