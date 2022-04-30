

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GdiSharpenFilter.cs
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
namespace IGK.DrSStudio.Imaging.RegistratedEffect
{
    using IGK.ICore.Drawing2D.Imaging;
    using IGK.DrSStudio.Imaging.Effects;
    using System;

    public class GdiSharpenFilter :
        GdiFilterBase,
        ICore2DDrawingSharpenFilter
    {
        SharpenEffect m_Effect;

        public GdiSharpenFilter()
        {
            this.m_Effect = new SharpenEffect();
        }
        public override bool CanApply
        {
            get
            {
                return this.m_Effect.CanApply;
            }
        }
        public override void Apply(ICoreBitmap bitmap, Rectanglei requireSize)
        {
            WinCoreBitmap bmp = bitmap as WinCoreBitmap;

            this.m_Effect.ApplyToBitmap(bmp.Bitmap,
                new System.Drawing.Rectangle(requireSize.X,
                    requireSize.Y,
                    requireSize.Width,
                    requireSize.Height));
        }

        public float Radius
        {
            get
            {
                return this.m_Effect.Radius;
            }
            set
            {
                if ((value>=0) && (value <= 255))
                    this.m_Effect.Radius = value;
            }
        }

        public float Amount
        {
            get
            {
                return this.m_Effect.Amount ;
            }
            set
            {
                if ((value >= 0) && (value <= 100))
                {
                    this.m_Effect.Amount = value;
                }
            }
        }
    }
}