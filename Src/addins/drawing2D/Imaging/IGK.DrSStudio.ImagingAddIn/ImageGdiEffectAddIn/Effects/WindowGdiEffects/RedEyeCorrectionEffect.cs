

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: RedEyeCorrectionEffect.cs
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
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using IGK.DrSStudio.Imaging.EffectsInternal;
using IGK.DrSStudio.Imaging.Internal;

namespace IGK.DrSStudio.Imaging.Effects
{

    using IGK.DrSStudio.Imaging.Internal;
    /// <summary>
    /// Encapsulates an effect which allows for red eye correction to be applied to specific areas.
    /// </summary>
    /// <see>msdn.microsoft.com/en-us/library/ms534499(v=VS.85).aspx</see>
    public class RedEyeCorrectionEffect : LegacyEffect
    {
        #region Protected Static Locals

        /// <summary>
        /// GUID for the GDI+ red eye correction effect.
        /// </summary>
        protected static Guid mgEffectGuid = new Guid("{74D29D05-69A4-4266-9549-3CC52836B632}");

        #endregion

        #region Private Locals

        /// <summary>
        /// Holds the rectangular areas to be corrected for red eye.
        /// </summary>
        private Rectangle[] mrAreas;

#if !Unsafe
        /// <summary>
        /// Hold the last garbage collection handle created for the parameters.
        /// </summary>
        private GCHandle mhLastHandle;
#endif

        #endregion

        #region Initialisation

        /// <summary>
        /// Creates a new red eye correction effect with a single empty rectangle.
        /// </summary>
        public RedEyeCorrectionEffect()
            : this(new Rectangle[] { new Rectangle() })
        {
        }

        /// <summary>
        /// Creates a new red eye correction effect.
        /// </summary>
        /// <param name="rectangles">The rectangles for the eyes to be corrected.</param>
        public RedEyeCorrectionEffect(Rectangle[] rectangles)
            : base(mgEffectGuid)
        {
            Areas = rectangles;
        }

        #endregion

        #region Public Overrides

        /// <summary>
        /// Validates the effect with an image.
        /// </summary>
        /// <param name="image">The image to validate with.</param>
        /// <exception cref="GDIPlusX.Effects.EffectValidateException">Eye rectangle is out of range.</exception>
        public override void Validate(Image image)
        {
            Rectangle lrRect = new Rectangle(0, 0, image.Width, image.Height);

            for (int liIndex = 0; liIndex < mrAreas.Length; liIndex++)
            {
                Rectangle lrR = mrAreas[liIndex];

                if (!lrRect.Contains(lrR))
                    throw new EffectValidateException(
                        string.Format("Area rectangle element #{0} is out of range for image", liIndex));
            }
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
            LegacyBitmapPerPixelEffect.ApplyRedEyeReduction(
                bitmap, rectOfInterest, mrAreas, Effect.LegacyThreads, PixelFormat.Format32bppRgb);
        }
        
        /// <summary>
        /// Returns the parameter data for this effect.
        /// </summary>
        /// <returns>An object containing the parameter data.</returns>
        /// <exception cref="System.NotSupportedException">Embedded initialise calls are not supported.</exception>
        protected override object InitialiseParameterData()
        {
            // Convert the rectangles
            GdiNativeFunction.GdiRECT[] lrRects = GdiNativeFunction.GdiRECT.FromRectangles(mrAreas);
            IntPtr lipAreas = IntPtr.Zero;
            uint luiAreaCount = 0;

#if Unsafe
            unsafe
            {
                // Fix the rectangles memory location
                fixed (GdiNativeFunction.GdiRECT* lipRects = lrRects)
                {
                    // Calculate the bytes needed 
                    int liSize = Marshal.SizeOf(typeof(GdiNativeFunction.GdiRECT)) * lrRects.Length;

                    // Allocate the memory
                    lipAreas = Marshal.AllocHGlobal(liSize);

                    // Move the memory.
                    GdiNativeFunction.MoveMemory(lipAreas, (IntPtr)lipRects, liSize);
                }
            }
#else
            // Check to make sure we arent recursively calling
            if (mhLastHandle.IsAllocated)
                throw new NotSupportedException("Embedded initialise calls not supported");

            // Pin the rectangle array 
            mhLastHandle = GCHandle.Alloc(lrRects, GCHandleType.Pinned);

            // Set the pointer
            lipAreas = mhLastHandle.AddrOfPinnedObject();

#endif
            // Set the length
            luiAreaCount = (uint)lrRects.Length;

            object loParams = null;

            if (GdiNativeFunction.Is64BitOS)
            {
                // If running in 64-bit environment, then Areas needs to be 64-bit aligned.
                GdiNativeFunction.RedEyeCorrectionParams64Bit lrecParams = new GdiNativeFunction.RedEyeCorrectionParams64Bit();
                lrecParams.Areas = lipAreas;
                lrecParams.NumberOfAreas = luiAreaCount;
                loParams = lrecParams;
            }
            else
            {
                // If running in 32-bit environment, then Areas needs to be 32-bit aligned.
                // This is untested, it is assumed this is the case, ie. running Vista or 7 32-bit.
                GdiNativeFunction.RedEyeCorrectionParams32Bit lrecParams = new GdiNativeFunction.RedEyeCorrectionParams32Bit();
                lrecParams.Areas = lipAreas;
                lrecParams.NumberOfAreas = luiAreaCount;
                loParams = lrecParams;
            }

            return loParams;
        }

        /// <summary>
        /// Gets the parameter size to send to the SetEffectParameterSize GDI+ function.
        /// </summary>
        /// <param name="structSize">The structure size of the effect parameters.</param>
        /// <param name="data">The data object for the structure of the effect parameters.</param>
        /// <returns>A uint value for the size.</returns>
        protected override uint GetParameterDataSize(int structSize, object data)
        {
            // Size for parameter should be size of entire struct + size of rects,
            // this isnt documented anywhere, except by looking at the GDI+
            // SDK source code.
            if(GdiNativeFunction.Is64BitOS) 
                // If its a 64-bit OS then cast as such
                return 
                    (uint)(structSize + 
                    ((uint)(((GdiNativeFunction.RedEyeCorrectionParams64Bit)data).NumberOfAreas) * 
                    Marshal.SizeOf(typeof(GdiNativeFunction.GdiRECT))));
            else
                // If its a 32-bit OS then cast as such
                return
                    (uint)(structSize +
                    ((uint)(((GdiNativeFunction.RedEyeCorrectionParams32Bit)data).NumberOfAreas) *
                    Marshal.SizeOf(typeof(GdiNativeFunction.GdiRECT))));
        }

        /// <summary>
        /// Finalises parameter data.
        /// </summary>
        /// <param name="data">The structure containing the parameters to finalise.</param>
        protected override void FinaliseParameterData(object data)
        {
#if Unsafe
            if (GdiNativeFunction.Is64BitOS)
                // 64-bit os then cast as such
                Marshal.FreeHGlobal(((GdiNativeFunction.RedEyeCorrectionParams64Bit)data).Areas);
            else
                // 32-bit os then cast as such
                Marshal.FreeHGlobal(((GdiNativeFunction.RedEyeCorrectionParams32Bit)data).Areas);
#else
            // Free the pinned handle
            mhLastHandle.Free();
#endif

            base.FinaliseParameterData(data);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the rectangles for the eyes to be corrected.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Areas is null.</exception>
        /// <exception cref="System.ArgumentException">Areas contains 0 elements.</exception>
        public Rectangle[] Areas
        {
            get
            {
                return mrAreas;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Areas");

                if (value.Length == 0)
                    throw new ArgumentException("Must contain at least one element", "Areas");

                mrAreas = value;
            }
        }

        #endregion
    }
}
