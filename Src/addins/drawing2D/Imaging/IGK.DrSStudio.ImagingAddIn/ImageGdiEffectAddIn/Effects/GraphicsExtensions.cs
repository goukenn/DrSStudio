

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GraphicsExtensions.cs
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
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;


namespace IGK.DrSStudio.Imaging.Effects
{
    using IGK.DrSStudio.Imaging.Internal;
    /// <summary>
    /// Provides effects extensions for the Graphics class.
    /// </summary>
    public static class GraphicsExtensions
    {
        /// <summary>
        /// Draws an image apply an effect as its drawn.
        /// </summary>
        /// <param name="graphics">The graphics to draw on.</param>
        /// <param name="image">The image to draw. This image is not altered by the effect.</param>
        /// <param name="source">The source rectangle to draw.</param>
        /// <param name="effect">The effect to apply.</param>
        /// <param name="transform">The transform to apply. Null for none.</param>
        /// <param name="imageAttributes">The image attributes to apply. Null for none.</param>
        /// <param name="srcUnits">The source rectangle units.</param>
        /// <exception cref="GDIPlusX.GDIPlus11NotAvailableException">GDI Plus version 1.1 is not available.</exception>
        /// <exception cref="System.ArgumentNullException">graphics is null.  -or- image is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">srcUnits enumeration is out of range.</exception>
        /// <exception cref="GDIPlusX.Effects.EffectValidateException">Effect failed to validate with image.</exception>
        public static void DrawImage(
            this Graphics graphics, 
            Image image, 
            RectangleF source, 
            Effect effect,
            global::System.Drawing.Drawing2D.Matrix transform, 
            ImageAttributes imageAttributes, 
            GraphicsUnit srcUnits)
        {
            // Check for errors
            if (graphics == null) throw new ArgumentNullException("graphics");
            if (image == null) throw new ArgumentNullException("image");
            GdiNativeFunction.CheckEnumRange<GraphicsUnit>(srcUnits, GraphicsUnit.World, GraphicsUnit.Millimeter, "srcUnits");
            if (effect != null) effect.Validate(image);

            if(!GdiNativeFunction.Ver11Available)
            {
                graphics.LegacyDrawImage(image, source, effect, transform, imageAttributes, srcUnits);
                return;
            }

            // Setup values
            GdiNativeFunction.GdiRectF lrSource = new GdiNativeFunction.GdiRectF(source);

            // Call function
            int liStatus = GdiNativeFunction.GdipDrawImageFX(
                new HandleRef(graphics, graphics.NativeHandle()),
                new HandleRef(image, image.NativeHandle()),
                ref lrSource,
                (transform == null ? new HandleRef() : new HandleRef(transform, transform.NativeHandle())),
                (effect == null ? new HandleRef() : new HandleRef(effect, effect.NativeHandle())),
                (imageAttributes == null ? new HandleRef() : new HandleRef(imageAttributes, imageAttributes.NativeHandle())),
                (GdiNativeFunction.GdiUnit)srcUnits);

            // Check for errors
            GdiNativeFunction.CheckErrorStatus(liStatus);
        }

        /// <summary>
        /// Draws an image apply an effect as its drawn using legacy code.
        /// </summary>
        /// <param name="graphics">The graphics to draw on.</param>
        /// <param name="image">The image to draw. This image is not altered by the effect.</param>
        /// <param name="source">The source rectangle to draw.</param>
        /// <param name="effect">The effect to apply.</param>
        /// <param name="transform">The transform to apply. Null for none.</param>
        /// <param name="imageAttributes">The image attributes to apply. Null for none.</param>
        /// <param name="srcUnits">The source rectangle units.</param>
        internal static void LegacyDrawImage(
            this Graphics graphics, Image image, RectangleF source, Effect effect,
            global::System.Drawing.Drawing2D.Matrix transform, 
            ImageAttributes imageAttributes, 
            GraphicsUnit srcUnits)
        {
            if (transform == null) transform = new global::System.Drawing.Drawing2D.Matrix ();
            if (imageAttributes == null) imageAttributes = new ImageAttributes();

            global::System.Drawing.Drawing2D.Matrix lmOldTransform = graphics.Transform;
            transform.Multiply(graphics.Transform);
            graphics.Transform = transform;

            try
            {
                Rectangle lrSource = Rectangle.Round(source);

                if (effect != null)
                {
                    Bitmap lbmpBitmap = new Bitmap(lrSource.Width, lrSource.Height, PixelFormat.Format32bppPArgb);
                    Rectangle lrTempRect = new Rectangle(new Point(), lrSource.Size);
                    using (Graphics lgGraphics = Graphics.FromImage(lbmpBitmap))
                    {
                        lgGraphics.DrawImage(
                            image,
                            lrTempRect,
                            lrSource,
                            GraphicsUnit.Pixel);
                    }

                    lbmpBitmap.ApplyEffect(effect, lrTempRect);

                    graphics.DrawImage(
                        lbmpBitmap,
                        lrTempRect,
                        source.X, source.Y, source.Width, source.Height,
                        srcUnits,
                        imageAttributes);
                }
                else
                {
                    graphics.DrawImage(
                        image,
                        lrSource,
                        source.X, source.Y, source.Width, source.Height,
                        srcUnits,
                        imageAttributes);
                }
            }
            finally
            {
                graphics.Transform = lmOldTransform;
            }
        }
    }
}
