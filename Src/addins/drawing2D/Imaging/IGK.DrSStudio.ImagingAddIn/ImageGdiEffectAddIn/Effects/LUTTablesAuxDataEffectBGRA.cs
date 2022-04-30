

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: LUTTablesAuxDataEffectBGRA.cs
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
using System.Runtime.InteropServices;

namespace IGK.DrSStudio.Imaging.Effects
{
    /// <summary>
    /// Encapsulates an effect which has lookup table auxillary data.
    /// </summary>
    public abstract class LUTTablesAuxDataEffectBGRA : LUTTablesAuxDataEffect, IAuxDataEffectBGRA
    {
        #region Initialisation

        /// <summary>
        /// Creates a new lookup table effect.
        /// </summary>
        /// <param name="guid">The Guid for the effect.</param>
        public LUTTablesAuxDataEffectBGRA(Guid guid)
            : base(guid)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The last red channel lookup table data.
        /// </summary>
        public byte[] LUTInfoR
        {
            get
            {
                return mbLUTInfo2;
            }
        }

        /// <summary>
        /// The last green channel lookup table data.
        /// </summary>
        public byte[] LUTInfoG
        {
            get
            {
                return mbLUTInfo1;
            }
        }

        /// <summary>
        /// The last blue channel lookup table data.
        /// </summary>
        public byte[] LUTInfoB
        {
            get
            {
                return mbLUTInfo0;
            }
        }

        /// <summary>
        /// The last alpha channel lookup table data.
        /// </summary>
        public byte[] LUTInfoA
        {
            get
            {
                return mbLUTInfo3;
            }
        }

        #endregion
    }
}
