

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: HistogramFormatChannelCountAttribute.cs
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

namespace IGK.DrSStudio.Imaging.Internal
{
    /// <summary>
    /// Provides an attribute for the HistogramFormat fields to specify the number of channels output.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple=false)] 
    internal class HistogramFormatChannelCountAttribute : Attribute
    {
        #region Protected Locals

        /// <summary>
        /// Contains the number of channels.
        /// </summary>
        protected int miChannelCount;

        #endregion

        #region Initialisation

        /// <summary>
        /// Creates a new channel count attribute.
        /// </summary>
        /// <param name="channelCount">The number of channels for this field.</param>
        public HistogramFormatChannelCountAttribute(int channelCount)
        {
            miChannelCount = channelCount;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the number of channels.
        /// </summary>
        public int ChannelCount
        {
            get
            {
                return miChannelCount;
            }
        }

        #endregion
    }
}
