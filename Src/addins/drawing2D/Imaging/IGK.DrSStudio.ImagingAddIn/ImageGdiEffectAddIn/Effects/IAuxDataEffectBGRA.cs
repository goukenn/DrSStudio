

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IAuxDataEffectBGRA.cs
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

namespace IGK.DrSStudio.Imaging.Effects
{
    /// <summary>
    /// Encapsulates the ability for an effect which has BGRA lookup tables after its applied.
    /// </summary>
    public interface IAuxDataEffectBGRA
    {
        /// <summary>
        /// Gets the Red channel lookup table after the effect has been applied.
        /// </summary>
        byte[] LUTInfoR { get; }

        /// <summary>
        /// Gets the Green channel lookup table after the effect has been applied.
        /// </summary>
        byte[] LUTInfoG { get; }

        /// <summary>
        /// Gets the Blue channel lookup table after the effect has been applied.
        /// </summary>
        byte[] LUTInfoB { get; }

        /// <summary>
        /// Gets the Alpha channel lookup table after the effect has been applied.
        /// </summary>
        byte[] LUTInfoA { get; }
    }
}
