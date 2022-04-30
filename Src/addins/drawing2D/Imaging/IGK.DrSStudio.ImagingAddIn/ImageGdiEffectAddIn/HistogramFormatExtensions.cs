

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: HistogramFormatExtensions.cs
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
using System;
using System.Reflection;

namespace IGK.DrSStudio.Imaging
{
    /// <summary>
    /// Provides extension methods for the HistogramFormat enumeration.
    /// </summary>
    public static class HistogramFormatExtensions
    {
        /// <summary>
        /// Gets the number of channels for a HistogramFormat enumeration value.
        /// </summary>
        /// <param name="format">The format to get the number of channels for or 0 if not defined.</param>
        /// <returns>The number of channels</returns>
        public static int ChannelCount(this HistogramFormat format)
        {
            // Get the member information
            Type ltType = typeof(HistogramFormat);
            MemberInfo[] lmiInfos = ltType.GetMember(format.ToString());

            if (lmiInfos.Length > 0)
            {
                // Get the attribute
                object[] loAtts =
                    lmiInfos[0].GetCustomAttributes(typeof(HistogramFormatChannelCountAttribute), false);

                // Return the count
                if (loAtts.Length > 0)
                    return ((HistogramFormatChannelCountAttribute)loAtts[0]).ChannelCount;
            }

            return 0;
        }
    }
}
