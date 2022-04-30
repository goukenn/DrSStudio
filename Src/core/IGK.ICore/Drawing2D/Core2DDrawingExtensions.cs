

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DDrawingExtensions.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:Core2DDrawingExtensions.cs
*/
using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Drawing2D;
using IGK.ICore.GraphicModels;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Drawing2D
{
    /// <summary>
    /// drawing 2D Extension to draw 2d Document
    /// </summary>
    public static class  Core2DDrawingExtensions
    {
        public static void Draw(this CoreButtonDocument document, enuButtonState state, 
            ICoreGraphics device,
            bool proportional,
            Rectanglei bounds,
            enuFlipMode flipMode)
        {
            if (document == null)
                return;
            ICore2DDrawingDocument v_doc = document.GetDocument(state);
            if (v_doc != null)
                v_doc.Draw(device, proportional, bounds, flipMode);
        }
        public static void Draw(this CoreButtonDocument document, enuButtonState state, ICoreGraphics device)
        {
            if (document == null)
                return;
            switch (state)
            {
                case enuButtonState.Normal:
                    document.Normal.Draw(device);
                    break;
                case enuButtonState.Hover:
                    document.Hover.Draw(device);
                    break;
                case enuButtonState.Down:
                    document.Down.Draw(device);
                    break;
                case enuButtonState.Up:
                    document.Up.Draw(device);
                    break;
                case enuButtonState.Disabled:
                    document.Disabled.Draw(device);
                    break;
                default:
                    document.Normal.Draw(device);
                    break;
            }
        }
        
        public static void Draw(this ICore2DDrawingDocument d, ICoreGraphics device)
        {
            if (d == null) return;
             d.Draw(device, true, new Rectanglei(0,0, d.Width, d.Height ), enuFlipMode.None);
        }
        public static void Draw(this ICore2DDrawingDocument d, ICoreGraphics device, Rectanglei rc)
        {
            if ((d == null)|| (device ==null) )
                return;
            d.Draw(device, true, rc,  enuFlipMode.None);
        }
        public static void Draw(this ICore2DDrawingDocument  d,
            ICoreGraphics device,
            bool proportional,
            Rectanglei rectangle,
            enuFlipMode flipMode)
        {
            if ((d == null) ||(device == null))
                return;
            if (device.Accept(d))
            {
                device.Draw(d, proportional, rectangle, flipMode);
            }
        }
        /// <summary>
        /// save a document as bitmap
        /// </summary>
        /// <param name="d"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static ICoreBitmap ToBitmap(this ICore2DDrawingDocument d , ICore2DDrawingLayer layer)
        {
            if (d == null)
                return null;
            return d.ToBitmap(layer, d.Width, d.Height, true, CoreScreen.DpiX, CoreScreen.DpiY);
        }
        public static ICoreBitmap ToBitmap(this ICore2DDrawingDocument d)
        {
            if (d == null)
                return null;
            return d.ToBitmap(d.Width, d.Height, true, CoreScreen.DpiX, CoreScreen.DpiY);
        }
        public static ICoreBitmap ToBitmapDpi(this ICore2DDrawingDocument d, float dpix, float dpiy)
        {
            if (d == null)
                return null;
            return d.ToBitmap(d.Width, d.Height, true, dpix , dpiy);
        }
        public static ICoreBitmap ToBitmap(this ICore2DDrawingDocument d, int width, int height, bool proportional, float dpix, float dpiy)
        {
            if (d == null)
                return null;
            ICoreBitmap v_bmp =  CoreApplicationManager.Application.ResourcesManager.CreateBitmap(width, height);
            if (v_bmp != null)
            {
                v_bmp.SetResolution(dpix, dpiy);
                using (ICoreGraphics g = CoreApplicationManager.Application.ResourcesManager.CreateDevice(v_bmp))
                {
                    if (g != null)
                    {
                        g.SetupGraphicsDevice(d);
                        d.Draw(g, proportional, new Rectanglei (0,0, width, height), enuFlipMode.None  );
                        g.Flush();
                    }
                }
                return v_bmp;
            }
            return null;
        }
        /// <summary>
        /// render layer 
        /// </summary>
        /// <param name="document">document reference of rendering</param>
        /// <param name="layer">layer to draw</param>
        /// <param name="width">width expeded</param>
        /// <param name="height">height expected</param>
        /// <param name="proportional"></param>
        /// <param name="dpix">dpix</param>
        /// <param name="dpiy">dpiy</param>
        /// <returns></returns>
        public static ICoreBitmap ToBitmap(this ICore2DDrawingDocument document, ICore2DDrawingLayer layer, int width, int height, bool proportional, float dpix, float dpiy)
        {
            if (document == null)
                return null;
            ICoreBitmap v_bmp = CoreApplicationManager.Application.ResourcesManager.CreateBitmap(width, height);
            if (v_bmp != null)
            {
                v_bmp.SetResolution(dpix, dpiy);
                using (ICoreGraphics g = CoreApplicationManager.Application.ResourcesManager.CreateDevice(v_bmp))
                {
                    if (g != null)
                    {
                        g.SetupGraphicsDevice(document);
                        layer.Draw(g);
                        g.Flush();
                    }
                }
                return v_bmp;
            }
            return null;
        }

        public static void Draw(this ICore2DDrawingObject obj, ICoreGraphics device)
        {
            device.Draw(obj);
        }
        /// <summary>
        /// draw a visitable object
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="device"></param>
        public static void Visit(this ICore2DDrawingVisitable obj, ICoreGraphics device)
        {
            if ((obj == null) || (device == null)) return;
            if (device.Accept(obj))
            {
                device.Visit(obj);
            }
        }
        /// <summary>
        /// save drawing document
        /// </summary>
        /// <param name="document"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static bool SaveToFile(this ICore2DDrawingDocument document, string filename)
        {            
            GKDSElement c = GKDSElement.Create(null, new ICoreWorkingDocument[] { document });
            if (c != null)
            {
                File.WriteAllText(filename, c.Render());
                return true;
            }
            return false;

        }
    }
}

