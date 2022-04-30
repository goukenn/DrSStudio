

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: UnaryPixelOps.cs
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
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:UnaryPixelOps.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D.Effects
{
    /// <summary>
    /// represent a set of pixels operation
    /// </summary>
    public sealed class UnaryPixelOps
    {
        /// <summary>
        /// Computes alpha for r OVER l operation.
        /// </summary>
        public static byte ComputeAlpha(byte la, byte ra)
        {
            return (byte)(((la * (256 - (ra + (ra >> 7)))) >> 8) + ra);
        }
        [Serializable]
        public class BlendConstant
            : UnaryPixelOp
        {
            private Colorf blendColor;
            public override Colorf Apply(Colorf color)
            {
                float a = blendColor.A;
                float invA = 1.0f - a;
                float r = ((color.R * invA) + (blendColor.R * a));
                float g = ((color.G * invA) + (blendColor.G * a));
                float b = ((color.B * invA) + (blendColor.B * a));
                float a2 = UnaryPixelOps.ComputeAlpha((byte)(color.A * 255), (byte)(blendColor.A *255))  / 255.0f;
                return Colorf.FromFloat(a2, r, g, b);
            }
            public BlendConstant(Colorf blendColor)
            {
                this.blendColor = blendColor;
            }
        }
    }
}

