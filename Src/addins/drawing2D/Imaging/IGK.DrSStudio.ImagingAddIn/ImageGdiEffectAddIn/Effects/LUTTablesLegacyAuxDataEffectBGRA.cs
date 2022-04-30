

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: LUTTablesLegacyAuxDataEffectBGRA.cs
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
using IGK.DrSStudio.Imaging.Internal;
using System.Drawing.Imaging;
using IGK.DrSStudio.Imaging.EffectsInternal;

namespace IGK.DrSStudio.Imaging.Effects
{
    /// <summary>
    /// Encapsulates an effect which uses BGRA lookup tables and has auxillary data for them.
    /// </summary>
    public abstract class LUTTablesLegacyAuxDataEffectBGRA : LUTTablesLegacyAuxDataEffect, IAuxDataEffectBGRA 
    {
        #region Initialisation

        /// <summary>
        /// Creatse a new legacy BGRA aux data effect.
        /// </summary>
        /// <param name="effectGuid">The Guid for this effect.</param>
        public LUTTablesLegacyAuxDataEffectBGRA(Guid effectGuid)
            : base(effectGuid)
        {
        }

        #endregion

        #region Protected Overrides

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
        protected override void LegacyApplyToBitmap(Bitmap bitmap, Rectangle rectOfInterest)
        {
            byte[] lb3, lb2, lb1, lb0;
            GetLegacyLookupTables(out lb0, out lb1, out lb2, out lb3);

            LegacyBitmapPerPixelEffect.ApplyLookupTables(
                bitmap, lb3, lb2, lb1, lb0,
                rectOfInterest, Effect.LegacyThreads, PixelFormat.Format32bppPArgb);

            if (ProcessLUTInfo)
            {
                mbLUTInfo3 = lb3;
                mbLUTInfo2 = lb2;
                mbLUTInfo1 = lb1;
                mbLUTInfo0 = lb0;
            }
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
