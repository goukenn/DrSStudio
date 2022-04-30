

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinCoreBitmapOperation.cs
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
file:WinCoreBitmapOperation.cs
*/
using IGK.ICore.GraphicModels;
using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGK.ICore;
using IGK.ICore.WinUI;
namespace IGK.ICore.WinCore
{
    /// <summary>
    /// series of fonction utility to manipulate bitmap
    /// </summary>
    public static class WinCoreBitmapOperation
    {
        public static Bitmap WinCoreInvert(this Bitmap bmp)
        {
            float[][] t = new float[][]{
                    new float[]{-1,0,0,0,0},
                    new float[]{0,-1,0,0,0,0},
                    new float[]{0,0,-1,0,0,},
                    new float[]{0,0,0,1,0},
                    new float[]{1,1,1,0,0}
                };
            ImageAttributes attr = new ImageAttributes();
            attr.SetColorMatrix(new ColorMatrix(t));
            Bitmap v_cbmp = new Bitmap(bmp.Width, bmp.Height);
            using (Graphics g = Graphics.FromImage(v_cbmp))
            {
                g.DrawImage(bmp, new Rectangle(Point.Empty, bmp.Size), 0, 0, bmp.Width, bmp.Height, GraphicsUnit.Pixel, attr);
            }
            return v_cbmp;
        }
        //public static Bitmap GetMask(Bitmap bmp, byte tolerance, bool white, bool consideralpha)
        //{
        //    WinCoreBitmapData vd = WinCoreBitmapData.FromBitmap(bmp);
        //    Color cl = Color.White;
        //    if (!white)
        //        cl = Color.Black;
        //    //float a = 255.0f / (float)vd.Height;
        //    int offset = 0;
        //    for (int j = 0; j < vd.Height; j++)
        //    {
        //        for (int i = 0; i < vd.Width; i++)
        //        {
        //            offset = (int)((j * vd.Stride) + (i * 4));
        //            if (vd.Data[offset + 3] <= tolerance)
        //            {
        //                //totaly black or transparent
        //                if (consideralpha)
        //                {
        //                    vd.Data[offset] = cl.R;
        //                    vd.Data[offset + 1] = cl.G;
        //                    vd.Data[offset + 2] = cl.B;
        //                    vd.Data[offset + 3] = cl.A;
        //                }
        //                else
        //                {
        //                    vd.Data[offset] = 0;
        //                    vd.Data[offset + 1] = 0;
        //                    vd.Data[offset + 2] = 0;
        //                    vd.Data[offset + 3] = 0;
        //                }
        //            }
        //            else
        //            {
        //                //totaly white
        //                if (!consideralpha)
        //                {
        //                    vd.Data[offset] = cl.R;
        //                    vd.Data[offset + 1] = cl.G;
        //                    vd.Data[offset + 2] = cl.B;
        //                    vd.Data[offset + 3] = cl.A;
        //                }
        //                else
        //                {
        //                    vd.Data[offset] = 255;
        //                    vd.Data[offset + 1] = 255;
        //                    vd.Data[offset + 2] = 255;
        //                    vd.Data[offset + 3] = 255;
        //                }
        //            }
        //        }
        //    }
        //    return vd.ToBitmap();
        //}
         internal static byte[] GetMaskData(Bitmap bmp, int p)
         {
             int w = bmp.Width;
             int h = bmp.Height;
             Bitmap bitmap2 = GetMask(bmp, 127, true, false);
             WinCoreBitmapData vd = WinCoreBitmapData.FromBitmap(bitmap2);
             byte[] dmask = new byte[(h * w)];
             int mask = 0;
             int offset = 0;
             int count = 0;
             int decal = 0;
             for (int j = 0; j < h / 2; j++)
             {
                 for (int i = 0; i < w; i++)
                 {
                     offset = (int)((j * vd.Stride) + (i * 4));
                     if ((vd.Data[offset] == 0) && (vd.Data[offset] == vd.Data[offset + 1]) && (vd.Data[offset + 2] == vd.Data[offset + 3]))
                     {
                         //mask += 0
                     }
                     else
                     {
                         mask += p;
                     }
#pragma warning disable IDE0054 // Use compound assignment
                     mask = mask << 1;
#pragma warning restore IDE0054 // Use compound assignment
                     ++decal;
                     if (decal > 7)
                     {
                         decal = 0;
                         dmask[count] = (byte)mask;
                         mask = 0;
                         count++;
                     }
                 }
             }
             bitmap2.Dispose();
             return dmask;
         }
        
         internal const int SRCCOPY = 0x00CC0020; /* dest = source                   */
         internal const int SRCPAINT = 0x00EE0086; /* dest = source OR dest           */
         internal const int SRCAND = 0x008800C6; /* dest = source AND dest          */
         internal const int SRCINVERT = 0x00660046; /* dest = source XOR dest          */
         internal const int SRCERASE = 0x00440328; /* dest = source AND (NOT dest )   */
         internal const int NOTSRCCOPY = 0x00330008; /* dest = (NOT source)             */
         internal const int NOTSRCERASE = 0x001100A6; /* dest = (NOT src) AND (NOT dest) */
         internal const int MERGECOPY = 0x00C000CA; /* dest = (source AND pattern)     */
         internal const int MERGEPAINT = 0x00BB0226; /* dest = (NOT source) OR dest     */
         internal const int PATCOPY = 0x00F00021; /* dest = pattern                  */
         internal const int PATPAINT = 0x00FB0A09; /* dest = DPSnoo                   */
         internal const int PATINVERT = 0x005A0049; /* dest = pattern XOR dest         */
         internal const int DSTINVERT = 0x00550009; /* dest = (NOT dest)               */
         internal const int BLACKNESS = 0x00000042; /* dest = BLACK                    */
         internal const int WHITENESS = 0x00FF0062; /* dest = WHITE                    */
         internal const int OP_ADD = 1;
         [DllImport("user32.Dll")]
         internal static extern IntPtr GetDC(IntPtr hwnd);
         [DllImport("user32.Dll")]
         internal static extern IntPtr GetWindowDC(IntPtr hwnd);
         [DllImport("user32.Dll", SetLastError = true)]
         internal static extern bool ReleaseDC(IntPtr hwnd, IntPtr hdc);
         [DllImport("gdi32.dll")]
         internal static extern IntPtr SelectObject(IntPtr hdc, IntPtr hdc1);
         [DllImport("gdi32.dll")]
         internal static extern IntPtr DeleteObject(IntPtr hdc);
         [DllImport("gdi32.dll")]
         static extern IntPtr CreateCompatibleDC(IntPtr hdc);
         [DllImport("gdi32.dll")]
         static extern bool DeleteDC(IntPtr hdc);
         [DllImport("gdi32.dll")]
         static extern bool BitBlt(
             IntPtr hdc,
             int x,
             int y,
             int cx,
             int cy,
             IntPtr hdcSrc,
             int x1,
             int y1,
             uint rop);
         /// <summary>
         /// get the hdc
         /// </summary>
         /// <param name="hwnd"></param>
         /// <returns></returns>
         public static IntPtr GetHDC(IntPtr hwnd)
         {
             return GetWindowDC(hwnd);          
         }
         public static bool ReleaseHDC(IntPtr hwnd, IntPtr hdc)
         {
             return ReleaseDC(hwnd, hdc);
         }
         public static IntPtr CreateCompatibleHDC(IntPtr hdc)
         {
             return CreateCompatibleDC(hdc);
         }
         public static bool DeleteCompatibleHDC(IntPtr hdc)
         {
             return DeleteDC(hdc);
         }
         /// <summary>
         /// Copy Hdc Bitmap
         /// </summary>
         /// <param name="srcHdc"></param>
         /// <param name="destHdc"></param>
         /// <param name="xSource"></param>
         /// <param name="ySource"></param>
         /// <param name="wSource"></param>
         /// <param name="hSource"></param>
         /// <param name="xDest">where x</param>
         /// <param name="yDest">where y</param>
         /// <returns></returns>
         public static bool CopyBitBlt(IntPtr srcHdc, IntPtr destHdc, 
             int xSource, int ySource, 
             int wSource, int hSource,
             int xDest,
             int yDest, uint mode)
         {
             bool v = BitBlt(
                 destHdc,
                 xDest,
                 yDest,
                 wSource,
                 hSource,
                 srcHdc,
                 xSource,
                 ySource,
                 mode);
             return v;
         }
         /// <summary>
         /// apply filter to bitmap
         /// </summary>
         /// <param name="bitmap"></param>
         /// <param name="factor"></param>
         /// <param name="mode"></param>
         /// <param name="inQuality"></param>
         /// <param name="outMode"></param>
         /// <param name="outQuality"></param>
         /// <param name="expended"></param>
         /// <returns></returns>
         public static Bitmap SmoothBitmap(Bitmap bitmap, float factor,
             InterpolationMode mode,
             CompositingQuality inQuality,
             InterpolationMode outMode,
             CompositingQuality outQuality,
             Boolean expended)
         {
             //-------------------------------------------------------------------------------------
             //create a small image tha correspond to image
             //scale the image
             // factor scale factor 1x 2x 4x 8x 16x 32x 64x 100x
             //--------------------------------------------------------------------------------
             int w = (int)(bitmap.Width / (float)factor);
             int h = (int)(bitmap.Height / (float)factor);
             if ((w <= 0) || (h <= 0))
                 return bitmap;
             Bitmap bmp = new Bitmap(w, h);
             Bitmap outbmp = new Bitmap(bitmap.Width, bitmap.Height);
             Graphics g = Graphics.FromImage(bmp);
             g.InterpolationMode = mode;
             g.CompositingQuality = inQuality;
             g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
             //draw bitmap to the small image
             //important to reduce the bitmap
             Rectangle v_rc = Rectangle.Empty;
             if (expended)
             {
                 v_rc = new Rectangle(-1, -1, w + 2, h + 2);
             }
             else
                 v_rc = new Rectangle(1, 1, w - 2, h - 2);
             g.DrawImage(bitmap, v_rc,
             new Rectangle(0, 0, bitmap.Width, bitmap.Height),
             GraphicsUnit.Pixel
                 );
             g.Flush();
             g.Dispose();
             //draw the out bitmap
             g = Graphics.FromImage(outbmp);
             g.InterpolationMode = outMode;
             g.CompositingQuality = outQuality;
             g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
             //stretch image
             g.DrawImage(bmp, new Rectangle(Point.Empty, outbmp.Size));
             g.Flush();
             g.Dispose();
             return outbmp;
         }
         public static Bitmap ConvertToCoreBitmap(Bitmap bmp)
         {
             if (bmp == null) return null;
             if (bmp.PixelFormat == PixelFormat.Format32bppPArgb) return bmp.Clone() as Bitmap;
             Bitmap vbmp = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format32bppPArgb);
             vbmp.SetResolution(bmp.HorizontalResolution, bmp.VerticalResolution);
             Graphics g = Graphics.FromImage(vbmp);
             g.DrawImage(bmp, Point.Empty);
             g.Flush();
             g.Dispose();
             return vbmp;
         }

         public static Bitmap BlurBitmap(Bitmap bmp, float factor , InterpolationMode source, InterpolationMode destination)
         {
            float w = bmp.Width / factor;
            float h = bmp.Height / factor;

            if (!( (w >=1) && (h>=1)))
            {
                return bmp;
            }
            try
            {
                Bitmap rbmp = new Bitmap((int)w, (int)h);
                Graphics g = Graphics.FromImage(rbmp);
                g.InterpolationMode = source;
                g.DrawImage(bmp, new Rectangle(Point.Empty, rbmp.Size), new Rectangle(Point.Empty, bmp.Size), GraphicsUnit.Pixel);
                g.Flush();
                g.Dispose();

                g = Graphics.FromImage(bmp);
                g.InterpolationMode = destination;
                g.Clear(Color.Transparent);
                g.DrawImage(rbmp, new Rectangle(Point.Empty, bmp.Size), new Rectangle(Point.Empty, rbmp.Size), GraphicsUnit.Pixel);
                g.Flush();
                g.Dispose();
                rbmp.Dispose();
                return bmp;
            }
            catch { 
            }
            return bmp;

        }

         public static Bitmap InvertColor(Bitmap bitmap)
         {
             return ApplyColorMatrix(bitmap, new ColorMatrix(
                 new float[][]{
                            new float[]{-1,0,0,0,0},
                            new float[]{0,-1,0,0,0,0},
                            new float[]{0,0,-1,0,0,},
                            new float[]{0,0,0,1,0},
                            new float[]{1,1,1,0,0}
                        }
             ));
         }
         public static Bitmap MakeWB(Bitmap bitmap)
         {
             return ApplyColorMatrix(bitmap, new ColorMatrix(
                 new float[][]{
                            new float[]{0.3f,0.3f,0.3f,0,0},
                            new float[]{0.6f,0.6f,0.6f,0,0,0},
                            new float[]{0.1f,0.1f,0.1f,0,0,},
                            new float[]{0,0,0,1,0},
                            new float[]{0,0,0,0,0}
                        }
             ));
         }
         /// <summary>
         /// apply matrix
         /// </summary>
         /// <param name="bitmap"></param>
         /// <param name="colorMatrix"></param>
         /// <returns></returns>
         public static Bitmap ApplyColorMatrix(Bitmap bitmap, ColorMatrix colorMatrix)
         {
             return ApplyColorMatrix(bitmap, colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
         }
         public static ColorMatrix GetColorMatrix(float[] colorMatrix)
         {
             float[][] m = new float[5][]{ 
                 new float[5],
                 new float[5],
                 new float[5],
                 new float[5],
                 new float[5]
             };
             int k = 0;
             int s = 0;
             for (int i = 0; i < colorMatrix.Length; i++)
             {
                 m[k][s] = colorMatrix[i];
                 s++;
                 if (s >= 5)
                 {
                     s = 0;
                     k++;
                 }
             }
             return new ColorMatrix(m);
         }
         public static Bitmap ApplyColorMatrix(Bitmap bitmap, float[] colorMatrix)
         {
             ColorMatrix c = GetColorMatrix(colorMatrix);
             return ApplyColorMatrix (bitmap, c, ColorMatrixFlag.Default , ColorAdjustType.Bitmap );
         }
         public static Bitmap ApplyColorMatrix(Bitmap bmp, ColorMatrix mat, ColorMatrixFlag colorMatrixFlag, ColorAdjustType colorAdjustType)
         {
             if (bmp == null)
                 return null;
             Bitmap rbmp = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format32bppArgb);
             rbmp.SetResolution(bmp.HorizontalResolution, bmp.VerticalResolution);
             ImageAttributes attrib = new ImageAttributes();
             try
             {
                 attrib.SetColorMatrix(mat, colorMatrixFlag, colorAdjustType);
                 Graphics g = Graphics.FromImage(rbmp);
                 g.DrawImage(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, bmp.Width, bmp.Height, GraphicsUnit.Pixel, attrib);
                 g.Dispose();
                 return rbmp;
             }
             catch
             {
                 return bmp.Clone() as Bitmap;
             }
         }
         public static byte TrimByte(int i)
         {
             if (i < 0)
                 i *= -1;
             if (i > 255) return 255;
             return (byte)i;
         }
         /// <summary>
         /// get color mask
         /// </summary>
         /// <param name="bmp"></param>
         /// <param name="alpha"></param>
         /// <param name="white"></param>
         /// <returns></returns>
         public static Bitmap GetMask(Bitmap bmp, byte tolerance, bool white, bool consideralpha)
         {
             WinCoreBitmapData vd = WinCoreBitmapData.FromBitmap(bmp);
             Color cl = Color.White;
             if (!white)
                 cl = Color.Black;
             //float a = 255.0f / (float)vd.Height;
             int offset = 0;
             for (int j = 0; j < vd.Height; j++)
             {
                 for (int i = 0; i < vd.Width; i++)
                 {
                     offset = (int)((j * vd.Stride) + (i * 4));
                     if (vd.Data[offset + 3] <= tolerance)
                     {
                         //totaly black or transparent
                         if (consideralpha)
                         {
                             vd.Data[offset] = cl.R;
                             vd.Data[offset + 1] = cl.G;
                             vd.Data[offset + 2] = cl.B;
                             vd.Data[offset + 3] = cl.A;
                         }
                         else
                         {
                             vd.Data[offset] = 0;
                             vd.Data[offset + 1] = 0;
                             vd.Data[offset + 2] = 0;
                             vd.Data[offset + 3] = 0;
                         }
                     }
                     else
                     {
                         //totaly white
                         if (!consideralpha)
                         {
                             vd.Data[offset] = cl.R;
                             vd.Data[offset + 1] = cl.G;
                             vd.Data[offset + 2] = cl.B;
                             vd.Data[offset + 3] = cl.A;
                         }
                         else
                         {
                             vd.Data[offset] = 255;
                             vd.Data[offset + 1] = 255;
                             vd.Data[offset + 2] = 255;
                             vd.Data[offset + 3] = 255;
                         }
                     }
                 }
             }
             return vd.ToBitmap();
         }
         internal static byte TrimByte(float i)
         {
             if (i < 0)
                 return 0;
             else if (i > 255)
                 return 255;
             return (byte)i;
         }
         public static Bitmap SetTransparency(Bitmap bmp, float p)
         {
             if ((bmp == null) || (bmp.PixelFormat == PixelFormat.Undefined))
                 throw new ArgumentException($"{nameof(bmp)}");
             if (bmp.PixelFormat == PixelFormat.Undefined)
                 return null;
             ColorMatrix mat = new ColorMatrix(new float[][]{
                        new float []{ 1.0f,  0.0f, 0.0f, 0.0f, 0.0f},
                        new float []{ 0.0f,  1.0f, 0.0f, 0.0f, 0.0f},
                        new float []{ 0.0f,  0.0f, 1.0f, 0.0f, 0.0f},
                        new float []{ 0.0f,  0.0f, 0.0f, 0, 0.0f},
                        new float []{ 0.0f,  0.0f, 0.0f, p, 1.0f},
                    });
             return ApplyColorMatrix(bmp, mat, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
         }
         public static Bitmap SetBalance(Bitmap bmp, float r, float g, float b)
         {
             if (bmp.PixelFormat == PixelFormat.Undefined)
                 return null;
             ColorMatrix mat = new ColorMatrix(new float[][]{
                        new float []{ r,  0.0f, 0.0f, 0.0f, 0.0f},
                        new float []{ 0.0f,  g, 0.0f, 0.0f, 0.0f},
                        new float []{ 0.0f,  0.0f, b, 0.0f, 0.0f},
                        new float []{ 0.0f,  0.0f, 0.0f, 1, 0.0f},
                        new float []{ 0.0f,  0.0f, 0.0f, 0, 1.0f},
                    });
             return ApplyColorMatrix(bmp, mat, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
         }
         /// <summary>
         /// set the contrast on an image
         /// </summary>
         /// <param name="bmp"></param>
         /// <param name="alpha"></param>
         public static Bitmap SetContrast(Bitmap bmp, byte alpha)
         {
             WinCoreBitmapData data1 = WinCoreBitmapData.FromBitmap(bmp);
             int w = bmp.Width;
             int h = bmp.Height;
             float fContrast = alpha / 128.0f;
             int r = 0;
             int g = 0;
             int b = 0;
             Color cl = Color.Empty;
             for (int i = 0; i < h; i++)
             {
                 for (int j = 0; j < w; j++)
                 {
                     cl = data1.ReadPixel(j, i);
                     r = cl.R;
                     g = cl.G;
                     b = cl.B;
                     r += (int)(fContrast * (r - 127));
                     r = TrimByte(r);
                     g += (int)(fContrast * (g - 127));
                     g = TrimByte(g);
                     b += (int)(fContrast * (b - 127));
                     b = TrimByte(b);
                     data1.WritePixel(Color.FromArgb(cl.A, r, g, b), j, i);
                 }
             }
             return data1.ToBitmap();
         }
         public static Bitmap Pixeliser(Bitmap bmp, int size)
         {
             WinCoreBitmapData data1 = WinCoreBitmapData.FromBitmap(bmp);
             WinCoreBitmapData data2 = WinCoreBitmapData.FromBitmap(bmp);
             //size = 20;
             int offset = 0;
             int w = bmp.Width;
             int h = bmp.Height;
             Color cl = Color.Empty;
             float r, g, b;
             int t = size - 2;
             for (int c = 0; c < h; c += size)
             {
                 for (int l = 0; l < w; l += size)
                 {
                     b = 0;
                     g = 0;
                     r = 0;
                     for (int i = c - t; i <= c + t; i++)
                     {
                         for (int j = l - t; j <= l + t; j++)
                         {
                             cl = data1.ReadPixel(j, i);
                             if (cl.IsEmpty)
                                 cl = Color.Black;
                             r += (cl.R / 255.0f);
                             g += (cl.G / 255.0f);
                             b += (cl.B / 255.0f);
                         }
                     }
                     r = r * 255 / (size * size);
                     b = b * 255 / (size * size);
                     g = g * 255 / (size * size);
                     for (int x = c - size; x < (c + size); x++)
                     {
                         for (int y = l - size; y < (l + size); y++)
                         {
                             if ((x < 0) || (x >= h) || (y < 0) || (y >= w))
                                 continue;
                             offset = (int)((x * data1.Stride) + (y * 4));
                             data2.Data[offset] = (byte)((r < 0) ? 0 : (r > 255) ? 255 : r);
                             data2.Data[offset + 1] = (byte)((g < 0) ? 0 : (g > 255) ? 255 : g);
                             data2.Data[offset + 2] = (byte)((b < 0) ? 0 : (b > 255) ? 255 : b);
                         }
                     }
                 }
             }
             return data2.ToBitmap();
         }
         public static Bitmap MakeBW(Bitmap bitmap, float R, float G, float B)
         {
             if ((bitmap.PixelFormat == PixelFormat.Undefined))
                 return null;
             return MakeBW(WinCoreBitmapData.FromBitmap(bitmap), R, G, B);
         }
         public static Bitmap MakeBW(WinCoreBitmapData data, float R, float G, float B)
         {
             WinCoreBitmapData data1 = data;
             int w = data.Width;
             int h = data.Height;
             int offset = 0;
             Color cl = Color.Empty;
             int d = 0;
             for (int c = 0; c < h; ++c)
             {
                 for (int l = 0; l < w; ++l)
                 {
                     offset = (int)((c * data1.Stride) + (l * 4));
                     cl = data1.ReadPixel(l, c);
                     d = (int)((cl.R * R + cl.B * B + cl.G * G) / 3.0f);
                     if (d < 0) d = 0;
                     else if (d > 255) d = 255;
                     cl = Color.FromArgb(cl.A, d, d, d);
                     data1.WritePixel(cl, l, c);
                 }
             }
             return data1.ToBitmap();
         }
         public static Bitmap RemapTable(Bitmap bmp, Color beforeColor, Color nexColor)
         {
             Bitmap rbmp = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format32bppArgb);
             rbmp.SetResolution(rbmp.HorizontalResolution, rbmp.VerticalResolution);
             ImageAttributes attrib = new ImageAttributes();
             ColorMap cl = new ColorMap();
             cl.OldColor = beforeColor;
             cl.NewColor = nexColor;
             //  attrib.SetBrushRemapTable(new ColorMap[] {cl});
             attrib.SetRemapTable(new ColorMap[] { cl }, ColorAdjustType.Bitmap);
             Graphics g = Graphics.FromImage(rbmp);
             g.DrawImage(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, bmp.Width, bmp.Height, GraphicsUnit.Pixel, attrib);
             g.Dispose();
             return rbmp;
         }
         /// <summary>
         /// to core Bitmap data
         /// </summary>
         /// <param name="bmp"></param>
         /// <param name="format"></param>
         /// <returns></returns>
         public static Bitmap ToCoreBitmap(Bitmap bmp, PixelFormat format)
         {
             Bitmap vbmp = new Bitmap(bmp.Width, bmp.Height, format);
             vbmp.SetResolution(bmp.HorizontalResolution, bmp.VerticalResolution);
             Graphics g = Graphics.FromImage(vbmp);
             g.CompositingQuality = CompositingQuality.Default;
             g.SmoothingMode = SmoothingMode.Default;
             g.PixelOffsetMode = PixelOffsetMode.HighQuality;
             g.InterpolationMode = InterpolationMode.NearestNeighbor;//.HighQualityBilinear;
             g.DrawImage(bmp, Point.Empty);
             g.Flush();
             g.Dispose();
             return vbmp;
         }
         public static Bitmap MakeGradient(Bitmap bmp)
         {
             WinCoreBitmapData vd = WinCoreBitmapData.FromBitmap(bmp);
             int r = 0;
             float a = 255.0f / (float)vd.Height;
             for (int j = 0; j < vd.Height; j++)
             {
                 r = 255 - (byte)(a * j);
                 for (int i = 0; i < vd.Width; i++)
                 {
                     vd.Data[((j * vd.Stride) + (i * 4) + 3)] = (byte)r;
                 }
             }
             return vd.ToBitmap();
         }
         public static Bitmap GetRed(Bitmap bmp)
         {
             if ((bmp.PixelFormat == PixelFormat.Undefined)) return null;
             ColorMatrix mat = new ColorMatrix(new float[][]{
                        new float []{ 1,  0.0f, 0.0f, 0.0f, 0.0f},
                        new float []{ 0.0f,  0.0f, 0.0f, 0.0f, 0.0f},
                        new float []{ 0.0f,  0.0f, 0.0f, 0.0f, 0.0f},
                        new float []{ 0.0f,  0.0f, 0.0f, 1.0f, 0.0f},
                        new float []{ 0.0f,  0.0f, 0.0f, 0.0f, 1.0f},
                    });
             return ApplyColorMatrix(bmp, mat, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
         }
         public static Bitmap GetGreen(Bitmap bmp)
         {
             if ((bmp.PixelFormat == PixelFormat.Undefined)) return null;
             ColorMatrix mat = new ColorMatrix(new float[][]{
                        new float []{ 0,  0.0f, 0.0f, 0.0f, 0.0f},
                        new float []{ 0.0f,  1.0f, 0.0f, 0.0f, 0.0f},
                        new float []{ 0.0f,  0.0f, 0.0f, 0.0f, 0.0f},
                        new float []{ 0.0f,  0.0f, 0.0f, 1.0f, 0.0f},
                        new float []{ 0.0f,  0.0f, 0.0f, 0.0f, 1.0f},
                    });
             return ApplyColorMatrix(bmp, mat, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
         }
         public static Bitmap GetBlue(Bitmap bmp)
         {
             if ((bmp.PixelFormat == PixelFormat.Undefined)) return null;
             ColorMatrix mat = new ColorMatrix(new float[][]{
                        new float []{ 0,  0.0f, 0.0f, 0.0f, 0.0f},
                        new float []{ 0.0f,  0.0f, 0.0f, 0.0f, 0.0f},
                        new float []{ 0.0f,  0.0f, 1.0f, 0.0f, 0.0f},
                        new float []{ 0.0f,  0.0f, 0.0f, 1.0f, 0.0f},
                        new float []{ 0.0f,  0.0f, 0.0f, 0.0f, 1.0f},
                    });
             return ApplyColorMatrix(bmp, mat, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
         }
         public static Bitmap GetAlpha(Bitmap bmp)
         {
             if ((bmp.PixelFormat == PixelFormat.Undefined)) return null;
             ColorMatrix mat = new ColorMatrix(new float[][]{
                        new float []{ 0,  0.0f, 0.0f, 0.0f, 0.0f},
                        new float []{ 0.0f,  0.0f, 0.0f, 0.0f, 0.0f},
                        new float []{ 0.0f,  0.0f, 0.0f, 0.0f, 0.0f},
                        new float []{ 0.0f,  0.0f, 0.0f, 1.0f, 0.0f},
                        new float []{ 1.0f,  1.0f, 1.0f, 0.0f, 1.0f},
                    });
             return ApplyColorMatrix(bmp, mat, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
         }
         public static Bitmap GetYellow(Bitmap bmp)
         {
             if ((bmp.PixelFormat == PixelFormat.Undefined)) return null;
             ColorMatrix mat = new ColorMatrix(new float[][]
                    { //R  V  B  A  T
                        new float []{ 0.0f,  0.0f,  0.0f, 0.0f, 0.0f},
                        new float []{ 0.0f,  0.0f,  0.0f, 0.0f, 0.0f},
                        new float []{ -1.0f, -1.0f, 0.0f, 0.0f, 0.0f},
                        new float []{ 0.0f,  0.0f, 0.0f, 1.0f, 0.0f},
                        new float []{ 1.0f,  1.0f, 0.0f, 0.0f, 1.0f},
                    });
             return ApplyColorMatrix(bmp, mat, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
         }
         public static Bitmap GetMagenta(Bitmap bmp)
         {
             if ((bmp.PixelFormat == PixelFormat.Undefined)) return null;
             ColorMatrix mat = new ColorMatrix(new float[][]{
                        new float []{ 0,  0.0f, 0.0f, 0.0f, 0.0f},
                        new float []{ -1.0f,  0.0f, -1.0f, 0.0f, 0.0f},
                        new float []{ 0.0f,  0.0f, 0.0f, 0.0f, 0.0f},
                        new float []{ 0.0f,  0.0f, 0.0f, 1.0f, 0.0f},
                        new float []{ 1.0f,  .0f, 1.0f, 0.0f, 1.0f},
                    });
             return ApplyColorMatrix(bmp, mat, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
         }
         public static Bitmap GetCyan(Bitmap bmp)
         {
             if ((bmp.PixelFormat == PixelFormat.Undefined)) return null;
             ColorMatrix mat = new ColorMatrix(new float[][]{
                        new float []{ 0.0f,  -1.0f, -1.0f, 0.0f, 0.0f},
                        new float []{ 0.0f,  0.0f, 0.0f, 0.0f, 0.0f},
                        new float []{ 0.0f,  0.0f, 0.0f, 0.0f, 0.0f},
                        new float []{ 0.0f,  0.0f, 0.0f, 1.0f, 0.0f},
                        new float []{ 0.0f,  1.0f, 1.0f, 0.0f, 1.0f},
                    });
             return ApplyColorMatrix(bmp, mat, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
         }
         public static Bitmap RotateColor(Bitmap bmp, float rAngle, float gAngle, float bAngle)
         {
             if ((bmp.PixelFormat == PixelFormat.Undefined)) return null;
             float[] mat = null;
             const float theata = (float)(Math.PI / 180.0F);
             float t = rAngle * theata;
             float cost = (float)Math.Cos(t);
             float sint = (float)Math.Sin(t);
             //first tranform
             mat = new float[]{
                         cost , sint, 0.0f, 0.0f, 0.0f,
                         -sint, cost, 0.0f, 0.0f, 0.0f,
                         0.0f,  0.0f, 1.0f, 0.0f, 0.0f,
                         0.0f,  0.0f, 0.0f, 1.0f, 0.0f,
                         0.0f,  0.0f, 0.0f, 0.0f, 1.0f
                    };
             t = gAngle * theata;
             cost = (float)Math.Cos(t);
             sint = (float)Math.Sin(t);
             //second transform
             mat = CoreMathOperation.MultMatrix(mat, new float[]{
                        1 , 0 , 0.0f, 0.0f, 0.0f,
                        0 ,  cost, sint, 0.0f, 0.0f,
                        0.0f,  -sint , cost, 0.0f, 0.0f,
                        0.0f,  0.0f, 0.0f, 1.0f, 0.0f,
                        0.0f,  0.0f, 0.0f, 0.0f, 1.0f,
                    }, 5, 5);
             t = bAngle * theata;
             cost = (float)Math.Cos(t);
             sint = (float)Math.Sin(t);
             //third transform
             mat = CoreMathOperation.MultMatrix(mat, new float[]{
                        cost,  0.0f, -sint,0.0f, 0.0f,
                        0.0f,  1.0f, 0.0f, 0.0f, 0.0f,
                        sint,  0.0f, cost, 0.0f, 0.0f,
                        0.0f,  0.0f, 0.0f, 1.0f, 0.0f,
                        0.0f,  0.0f, 0.0f, 0.0f, 1.0f
                    }, 5,5);
             return ApplyColorMatrix(bmp, GetColorMatrix ( mat), ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
         }
         public static Bitmap Rotate(Bitmap bmp, float angle, float x, float y, float z)
         {
             if ((bmp.PixelFormat == PixelFormat.Undefined)) return null;
             ColorMatrix mat = null;
             float t = (float)(angle * (Math.PI / 180.0f));
             float cost = (float)Math.Cos(t);
             float sint = (float)Math.Sin(t);
             mat = new ColorMatrix(new float[][]{
                        new float []{ cost , sint , 0.0f, 0.0f, 0.0f},
                        new float []{ -sint,  cost, 0.0f, 0.0f, 0.0f},
                        new float []{ 0.0f,  0.0f, 1.0f, 0.0f, 0.0f},
                        new float []{ 0.0f,  0.0f, 0.0f, 1.0f, 0.0f},
                        new float []{ 0.0f,  0.0f, 0.0f, 0.0f, 1.0f},
                    });
             Bitmap bmp1 = ApplyColorMatrix(bmp, mat, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
             mat = new ColorMatrix(new float[][]{
                        new float []{ 1 , 0 , 0.0f, 0.0f, 0.0f},
                        new float []{ 0 ,  cost, sint, 0.0f, 0.0f},
                        new float []{ 0.0f,  -sint , cost, 0.0f, 0.0f},
                        new float []{ 0.0f,  0.0f, 0.0f, 1.0f, 0.0f},
                        new float []{ 0.0f,  0.0f, 0.0f, 0.0f, 1.0f},
                    });
             Bitmap bmp2 = ApplyColorMatrix(bmp, mat, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
             //mat = new ColorMatrix(new float[][]{
             //    new float []{ cost , 0 , -sint, 0.0f, 0.0f},
             //    new float []{ 0,  0, 0.0f, 0.0f, 0.0f},
             //    new float []{ sint,  0.0f, cost , 0.0f, 0.0f},
             //    new float []{ 0.0f,  0.0f, 0.0f, 1.0f, 0.0f},
             //    new float []{ 0.0f,  0.0f, 0.0f, 0.0f, 1.0f},
             //});
             Bitmap bmp3 = ApplyColorMatrix(bmp, mat, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
             WinCoreBitmapData data1 = WinCoreBitmapData.FromBitmap(bmp1);
             WinCoreBitmapData data2 = WinCoreBitmapData.FromBitmap(bmp2);
             WinCoreBitmapData data3 = WinCoreBitmapData.FromBitmap(bmp3);
             Byte[] dt = data1.Data;
             int d = 0;
             for (int i = 0; i < dt.Length; ++i)
             {
                 d = dt[i] + data2.Data[i] + data3.Data[i];
                 if (d > 255) d = 255;
                 else
                     if (d < 0) d = 0;
                 dt[i] = (byte)d;
             }
             return data1.ToBitmap();
         }
         /// <summary>
         /// Get the BW Image from spécifiq Tolerance
         /// </summary>
         /// <param name="data1"></param>
         /// <param name="R"></param>
         /// <param name="G"></param>
         /// <param name="B"></param>
         /// <param name="A">new new Alpha Value</param>
         /// <param name="forBlack">true to consider black color otherwise false for white color</param>
         /// <returns></returns>
         public static Bitmap SetBlackAndWhiteSeuil(WinCoreBitmapData data1, int R, int G, int B, int A, bool forBlack)
         {
             DateTime t = DateTime.Now;
             int w = data1.Width;
             int h = data1.Height;
             int offset = 0;
             byte va = (byte)A;
             for (int c = 0; c < h; ++c)
             {
                 for (int l = 0; l < w; ++l)
                 {
                     offset = (int)((c * data1.Stride) + (l * 4));
                     if ((data1.Data[offset] < R) && (data1.Data[offset + 1] < G) && (data1.Data[offset] < B))
                     {
                         data1.Data[offset] = 0;
                         data1.Data[offset + 1] = 0;
                         data1.Data[offset + 2] = 0;
                         if (forBlack)
                             data1.Data[offset + 3] = va;
                     }
                     else
                     {
                         data1.Data[offset] = 255;
                         data1.Data[offset + 1] = 255;
                         data1.Data[offset + 2] = 255;
                         if (!forBlack)
                             data1.Data[offset + 3] = va;
                     }
                 }
             }
             return data1.ToBitmap();
         }
         public static Bitmap SetBlackAndWhiteSeuil(Bitmap bitmap, int R, int G, int B, int A, bool forBlack)
         {
             WinCoreBitmapData data1 = WinCoreBitmapData.FromBitmap(bitmap);
             return SetBlackAndWhiteSeuil(data1, R, G, B, A, forBlack);
         }
         public static Bitmap SetTransparencyKeyInterval(
             Bitmap bmp,
             LayerTransparencyKey Color,
             bool exclude)
         {
             WinCoreBitmapData v_cbmp = null;
             Bitmap v_obmp = null;
             try
             {
                 if (!exclude)
                 {
                     v_obmp = new Bitmap(bmp.Width, bmp.Height); ;// bmp.Clone() as Bitmap;
                     using (Graphics g = Graphics.FromImage(v_obmp))
                     {
                         ImageAttributes attr = new ImageAttributes();
                         attr.SetColorKey(
                             Color.Color1.CoreConvertTo<Color>(),
                             Color.Color2.CoreConvertTo<Color>()
                             );
                         g.DrawImage(bmp, new Rectangle(Point.Empty, bmp.Size),
                             0, 0,
                             bmp.Width, bmp.Height,
                             GraphicsUnit.Pixel,
                             attr);
                         g.Flush();
                     }
                     return v_obmp;
                 }
                 else
                 {
                     v_cbmp = WinCoreBitmapData.FromBitmap(bmp);
                     DateTime t = DateTime.Now;
                     long v_offset = 0;
                     for (int h = 0; h < v_cbmp.Height; h++)
                     {
                         for (int w = 0; w < v_cbmp.Width; w++)
                         {
                             v_offset = h * v_cbmp.Stride + w * 4;
                             if (((v_cbmp.Data[v_offset] <= Color.Color1.R) || (v_cbmp.Data[v_offset] >= Color.Color2.R)) &&
                                 ((v_cbmp.Data[v_offset + 1] <= Color.Color1.G) || (v_cbmp.Data[v_offset + 1] >= Color.Color2.G)) &&
                                 ((v_cbmp.Data[v_offset + 2] <= Color.Color1.B) || (v_cbmp.Data[v_offset + 2] >= Color.Color2.B)))
                             {
                                 v_cbmp.Data[v_offset + 3] = 0;
                             }
                         }
                     }
                     return v_cbmp.ToBitmap();
                 }
             }
             catch (Exception ex)
             {
                 CoreLog.WriteDebug("ex : " + ex.Message);
                 if (v_obmp != null)
                     v_obmp.Dispose();
             }
             finally
             {
             }
             return null;
         }
         public static Bitmap SetLight(Bitmap bmp, float luminance)
         {
             if ((bmp.PixelFormat == PixelFormat.Undefined)) return null;
             ColorMatrix mat = new ColorMatrix(new float[][]{
                        new float []{ 1.0f,  0.0f, 0.0f, 0.0f, 0.0f},
                        new float []{ 0.0f,  1.0f, 0.0f, 0.0f, 0.0f},
                        new float []{ 0.0f,  0.0f, 1.0f, 0.0f, 0.0f},
                        new float []{ 0.0f,  0.0f, 0.0f, 1.0f, 0.0f},
                        new float []{luminance, luminance,luminance, 0.0f, 1.0f},
                    });
             return ApplyColorMatrix(bmp, mat, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
         }
         public static Bitmap SetLight(WinCoreBitmapData vd, float luminance)
         {
             //float a = 255.0f / (float)vd.Height;
             int offset = 0;
             for (int j = 0; j < vd.Height; j++)
             {
                 for (int i = 0; i < vd.Width; i++)
                 {
                     offset = (int)((j * vd.Stride) + (i * 4));
                     vd.Data[offset] = TrimByte(vd.Data[offset] + luminance);
                     vd.Data[offset + 1] = TrimByte(vd.Data[offset + 1] + luminance);
                     vd.Data[offset + 2] = TrimByte(vd.Data[offset + 2] + luminance);
                 }
             }
             return vd.ToBitmap();
         }
         /// <summary>
         /// Adjust image
         /// </summary>
         /// <param name="luminance">translation</param>
         /// <param name="saturation">scale</param>
         /// <param name="constrast">contract</param>
         /// <param name="hue"></param>
         /// <returns></returns>
         public static ColorMatrix GetAdjustmentMatrix(float scale, float luminance, float saturation)
         {
             ////scale
             //float[] m = new float[] { 
             //                   scale  , 0,0,0,0,
             //                   0,scale ,0 ,0,0,
             //                   0,0,scale , 0,0,
             //                   0,0,0, 1,0 ,
             //                   0  ,0,0,0,1
             //               };
             //////luminance
             //m = CoreMathOperation.MultMatrix(m, new float[] { 
             //                   1 , 0,0,0,0,
             //                   0,1 ,0 ,0,0,
             //                   0,0,1 , 0,0,
             //                   0,0,0, 1,0 ,
             //                   luminance  ,luminance  ,luminance,0,1
             //               }, 5, 5);
             //////saturation
             //float s = saturation;
             //// The luminance weight factors for the RGB color space.
             //// These values are actually preferable to the better known factors of
             //// Y = 0.30R + 0.59G + 0.11B, the formula which is used in color television technique.
             //const float lumR = 0.3086f;
             //const float lumG = 0.6094f;
             //const float lumB = 0.0820f;
             //float satCompl = 1.0f - (saturation);
             //float satComplR = lumR * satCompl;
             //float satComplG = lumG * satCompl;
             //float satComplB = lumB * satCompl;
             //m = CoreMathOperation.MultMatrix(m, new float[]
             //                   {
             //                       satComplR + s,	satComplR,	satComplR,	0.0f, 0.0f,
             //                       satComplG,	satComplG + s,	satComplG,	0.0f, 0.0f,
             //                       satComplB,	satComplB,	satComplB + s,	0.0f, 0.0f,
             //                       0.0f,	0.0f,	0.0f,	1.0f,	0.0f,
             //                       0.0f,	0.0f,	0.0f,	0.0f,	1.0f
             //                   }, 5 , 5);
             //float[][] h = new float[5][];
             //int offset = 0;
             //for (int i = 0; i < 5; i++)
             //{
             //    h[i] = new float[5];
             //    offset = i * 5;
             //    for (int j = 0; j < 5; j++)
             //    {
             //        h[i][j] = m[offset + j];
             //    }
             //}

             ColorMatrix c = new ColorMatrix ();
             return c;
         }
         public static void SetGamma(Bitmap bmp, float p)
         {
             Graphics g = Graphics.FromImage(bmp);
             ImageAttributes attrib = new ImageAttributes();
             attrib.SetGamma(p);
             g.DrawImage(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, bmp.Width, bmp.Height, GraphicsUnit.Pixel, attrib);
             g.Dispose();
             attrib.Dispose();
         }
         /// <summary>
         /// apply bit operation
         /// </summary>
         /// <param name="dest">destination bitmap</param>
         /// <param name="Src">source bitmap</param>
         /// <param name="Operation">layer operation</param>
         /// <param name="opacity">opacity range between 0.0f and 1.0f</param>
         public static void ApplyBitOperation(Bitmap dest, Bitmap Src, enuCoreLayerOperation Operation, float opacity)
         {
             if ((dest == null) || (dest.PixelFormat == PixelFormat.Undefined))
                 throw new CoreException(enuExceptionType.ArgumentNotValid, "dest");
             if ((Src == null) || (Src.PixelFormat == PixelFormat.Undefined))
                 throw new CoreException(enuExceptionType.ArgumentNotValid, "src");
             if (dest.Size != Src.Size)
                 throw new CoreException(enuExceptionType.ArgumentNotValid, "Size don't match");
             if ((opacity < 0) || (opacity > 1.0f))
                 throw new CoreException(enuExceptionType.ArgumentNotValid, "opacity");
             BitmapData v_destdata = dest.LockBits(new Rectangle(Point.Empty, dest.Size),
                 ImageLockMode.ReadWrite, PixelFormat.Format32bppPArgb);
             BitmapData v_srcdata = Src.LockBits(new Rectangle(Point.Empty, dest.Size),
                 ImageLockMode.ReadWrite, PixelFormat.Format32bppPArgb);
             float r, g, b;
             int length = v_destdata.Stride * v_destdata.Height;
             Byte[] v_destbyte = new Byte[length];
             Byte[] v_srcbyte = new Byte[length];
             Marshal.Copy(v_destdata.Scan0, v_destbyte, 0, v_destbyte.Length);
             Marshal.Copy(v_srcdata.Scan0, v_srcbyte, 0, v_srcbyte.Length);
             for (int i = 0; i < length; ++i)
             {
                 switch (Operation)
                 {
                     case enuCoreLayerOperation.ReplaceMask:
                         if (v_srcbyte[i + 3] == 0)
                         {
                             v_destbyte[i + 3] = 0;
                             break;
                         }
                         goto case enuCoreLayerOperation.Replace;
                     case enuCoreLayerOperation.Replace: //replace destination with source
                         v_destbyte[i] = (byte)(v_srcbyte[i] * opacity);
                         v_destbyte[i + 1] = (byte)(v_srcbyte[i + 1] * opacity);
                         v_destbyte[i + 2] = (byte)(v_srcbyte[i + 2] * opacity);
                         v_destbyte[i + 3] = (byte)(v_srcbyte[i + 3] * opacity);
                         break;
                     case enuCoreLayerOperation.AddMask:
                         if (v_srcbyte[i + 3] == 0)
                         {
                             v_destbyte[i + 3] = 0;
                             break;
                         }
                         goto case enuCoreLayerOperation.Add;
                     case enuCoreLayerOperation.Add: //add el
                         r = v_destbyte[i] + v_srcbyte[i] * opacity;
                         g = v_destbyte[i + 1] + v_srcbyte[i + 1] * opacity;
                         b = v_destbyte[i + 2] + v_srcbyte[i + 2] * opacity;
                         v_destbyte[i] = (byte)((r < 0) ? 0 : (r > 255) ? 255 : r);
                         v_destbyte[i + 1] = (byte)((g < 0) ? 0 : (g > 255) ? 255 : g);
                         v_destbyte[i + 2] = (byte)((b < 0) ? 0 : (b > 255) ? 255 : b);
                         break;
                     case enuCoreLayerOperation.SubsrcMask:
                         if (v_srcbyte[i + 3] == 0)
                         {
                             v_destbyte[i + 3] = 0;
                             break;
                         }
                         goto case enuCoreLayerOperation.SubSrc;
                     case enuCoreLayerOperation.SubSrc: //sub element
                         r = v_destbyte[i] - v_srcbyte[i] * opacity;
                         g = v_destbyte[i + 1] - v_srcbyte[i + 1] * opacity;
                         b = v_destbyte[i + 2] - v_srcbyte[i + 2] * opacity;
                         v_destbyte[i] = (byte)((r < 0) ? 0 : (r > 255) ? 255 : r);
                         v_destbyte[i + 1] = (byte)((g < 0) ? 0 : (g > 255) ? 255 : g);
                         v_destbyte[i + 2] = (byte)((b < 0) ? 0 : (b > 255) ? 255 : b);
                         break;
                     case enuCoreLayerOperation.SubdestMask:
                         if (v_srcbyte[i + 3] == 0)
                         {
                             v_destbyte[i + 3] = 0;
                             break;
                         }
                         goto case enuCoreLayerOperation.SubDest;
                     case enuCoreLayerOperation.SubDest: //sub dest
                         r = -v_destbyte[i] + v_srcbyte[i] * opacity;
                         g = -v_destbyte[i + 1] + v_srcbyte[i + 1] * opacity;
                         b = -v_destbyte[i + 2] + v_srcbyte[i + 2] * opacity;
                         v_destbyte[i] = (byte)((r < 0) ? 0 : (r > 255) ? 255 : r);
                         v_destbyte[i + 1] = (byte)((g < 0) ? 0 : (g > 255) ? 255 : g);
                         v_destbyte[i + 2] = (byte)((b < 0) ? 0 : (b > 255) ? 255 : b);
                         break;
                     case enuCoreLayerOperation.XorMask:
                         if (v_srcbyte[i + 3] == 0)
                         {
                             v_destbyte[i + 3] = 0;
                             break;
                         }
                         goto case enuCoreLayerOperation.Xor;
                     case enuCoreLayerOperation.Xor:
                         v_srcbyte[i] = (byte)(v_srcbyte[i] * opacity);
                         v_srcbyte[i + 1] = (byte)(v_srcbyte[i + 1] * opacity);
                         v_srcbyte[i + 2] = (byte)(v_srcbyte[i + 2] * opacity);
                         r = (v_destbyte[i] & (255 - v_srcbyte[i])) + ((255 - v_destbyte[i]) & v_srcbyte[i]);
                         g = (v_destbyte[i + 1] & (255 - v_srcbyte[i + 1])) + ((255 - v_destbyte[i + 1]) & v_srcbyte[i + 1]);
                         b = (v_destbyte[i + 2] & (255 - v_srcbyte[i + 2])) + ((255 - v_destbyte[i + 2]) & v_srcbyte[i + 2]);
                         v_destbyte[i] = (byte)((r < 0) ? 0 : (r > 255) ? 255 : r);
                         v_destbyte[i + 1] = (byte)((g < 0) ? 0 : (g > 255) ? 255 : g);
                         v_destbyte[i + 2] = (byte)((b < 0) ? 0 : (b > 255) ? 255 : b);
                         break;
                 }
#pragma warning disable IDE0054 // Use compound assignment
                 i = i + 3;
#pragma warning restore IDE0054 // Use compound assignment
             }
             Marshal.Copy(v_destbyte, 0, v_destdata.Scan0, length);
             dest.UnlockBits(v_destdata);
             Src.UnlockBits(v_srcdata);
         }
         //static Bitmap CopyRegionSelection(Bitmap bitmap, Region region)
         //{
         //    //clone old image
         //    Bitmap v_bitmap = bitmap.Clone() as Bitmap;
         //    BitmapData data = v_bitmap.LockBits(new Rectangle(Point.Empty, bitmap.Size),
         //        ImageLockMode.ReadWrite, PixelFormat.Format32bppPArgb);
         //    Byte[] dat = new byte[data.Stride * data.Height];
         //    Marshal.Copy(data.Scan0, dat, 0, dat.Length);
         //    Bitmap v_maskbmp = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppPArgb);
         //    Graphics v_g = Graphics.FromImage(v_maskbmp);
         //    v_g.InterpolationMode = InterpolationMode.NearestNeighbor;
         //    v_g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
         //    v_g.SmoothingMode = SmoothingMode.Default;
         //    v_g.Clear(Color.White);
         //    v_g.FillRegion(Brushes.Black, region);
         //    Rectanglef rc = region.GetBounds(v_g);
         //    v_g.Flush();
         //    v_g.Dispose();
         //    BitmapData v_maskBitmapData = v_maskbmp.LockBits(new Rectangle(Point.Empty, bitmap.Size),
         //        ImageLockMode.ReadWrite, PixelFormat.Format32bppPArgb);
         //    Byte[] v_maskData = new Byte[v_maskBitmapData.Stride * v_maskBitmapData.Height];
         //    Marshal.Copy(v_maskBitmapData.Scan0, v_maskData, 0, v_maskData.Length);
         //    //get only black color and apply opertion to this
         //    int r, g, b;
         //    for (int i = 0; i < v_maskData.Length; ++i)
         //    {
         //        r = v_maskData[i];
         //        g = v_maskData[i + 1];
         //        b = v_maskData[i + 2];
         //        if ((r == g) && (g == b) && (b == 0))
         //        {
         //            v_maskData[i] = dat[i];
         //            v_maskData[i + 1] = dat[i + 1];
         //            v_maskData[i + 2] = dat[i + 2];
         //            v_maskData[i + 3] = dat[i + 3];
         //        }
         //        else
         //        {
         //            v_maskData[i] = 0;
         //            v_maskData[i + 1] = 0;
         //            v_maskData[i + 2] = 0;
         //            v_maskData[i + 3] = 0;
         //        }
         //        i = i + 3;
         //    }
         //    Marshal.Copy(v_maskData, 0, v_maskBitmapData.Scan0, v_maskData.Length);
         //    //unloack bit
         //    bitmap.UnlockBits(data);
         //    v_maskbmp.UnlockBits(v_maskBitmapData);
         //    //apply bitmap
         //    Bitmap newBitmap = new Bitmap((int)rc.Width, (int)rc.Height);
         //    v_g = Graphics.FromImage(newBitmap);
         //    v_g.TranslateTransform(-rc.X, -rc.Y, enuMatrixOrder.Append);
         //    v_g.DrawImage(v_maskbmp, Point.Empty);
         //    v_g.Flush();
         //    v_g.Dispose();
         //    v_maskbmp.Dispose();
         //    bitmap.Dispose();
         //    return newBitmap;
         //}
         public static Bitmap SaveBitmapAs4bpp(Bitmap bmp)
         {
             //Save 
             Bitmap v_bmp = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format4bppIndexed);
             Bitmap obmp = new Bitmap(4, 4);
             Graphics gr = Graphics.FromImage(obmp);
             gr.Clip = new Region(new Rectangle(Point.Empty, obmp.Size));
             gr.InterpolationMode = InterpolationMode.NearestNeighbor;
             gr.DrawImage(bmp, new Rectangle(Point.Empty, obmp.Size));
             gr.Flush();
             gr.Dispose();
             Color[] p = GetTheMost16Color(obmp); // GetTheMost256Color(bmp);
             obmp.Dispose();
             Color[] v_newpal = new Color[p.Length];
             Array.Copy(p, v_newpal, Math.Min(p.Length, 16));
             ColorPalette pal = v_bmp.Palette;
             FieldInfo f = pal.GetType().GetField("entries", BindingFlags.NonPublic | BindingFlags.Instance);
             //set the new palette palette
             f.SetValue(pal, v_newpal);
             //
             //f = pal.GetType().GetField("flags", BindingFlags.NonPublic | BindingFlags.Instance);
             // f.SetValue(pal, 4);       
             //change the palette
             v_bmp.Palette = pal;
             BitmapData v_data = bmp.LockBits(new Rectangle(Point.Empty, bmp.Size),
               ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
             BitmapData v_data2 = v_bmp.LockBits(new Rectangle(Point.Empty, bmp.Size),
                           ImageLockMode.ReadWrite, PixelFormat.Format4bppIndexed);
             int offset = 0;
             int offset2 = 0;
             int r = 0;
             int g = 0;
             int b = 0;
             Dictionary<Color, int> dic = new Dictionary<Color, int>();
             //poluate dictionnay
             for (int i = 0; i < v_newpal.Length; i++)
             {
                 if (!dic.ContainsKey(v_newpal[i]))
                     dic.Add(v_newpal[i], i);
             }
             Color v_cl = Color.Empty;
             byte v_index = 0;
             int offset3 = 0;
             int v_sindex = 0;
             for (int h = 0; h < bmp.Height; h++)
             {
                 for (int w = 0; w < bmp.Width; w++)
                 {
                     offset = h * v_data.Stride + w * 3;
                     offset2 = h * v_data2.Stride + w;// pour un byte
                     b = Marshal.ReadByte(v_data.Scan0, offset);
                     g = Marshal.ReadByte(v_data.Scan0, offset + 1);
                     r = Marshal.ReadByte(v_data.Scan0, offset + 2);
                     v_cl = Color.FromArgb(r, g, b);
                     if (dic.ContainsKey(v_cl))
                     {
                         //Marshal.WriteInt16(v_data2.Scan0, offset2, (short)dic[v_cl]);
                         v_sindex = dic[v_cl];
                     }
                     else v_sindex = 0;
                     if ((offset2 % 2) == 0)
                     {
                         v_index += (byte)((v_sindex & 0x0F) << 4);
                     }
                     else
                     {
                         offset3 = (h * v_data2.Stride + (w / 2));
                         v_index += (byte)((v_sindex & 0x0F));
                         Marshal.WriteByte(v_data2.Scan0, offset3, v_index);
                         v_index = 0;
                     }
                 }
             }
             bmp.UnlockBits(v_data);
             v_bmp.UnlockBits(v_data2);
             return v_bmp;
         }
         /// <summary>
         /// save as 8pbmp by generate the color palette
         /// </summary>
         /// <param name="bmp"></param>
         /// <returns></returns>
         public static Bitmap SaveBitmapAs8bpp(Bitmap bmp)
         {
             Color[] p = null;
             //result bitmap
             Bitmap v_bmp = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format8bppIndexed);
             int v_w = bmp.Width;
             int v_h = bmp.Height;
             //pour chaque block de 255 x 255 get pixel 
             Bitmap obmp = null;
             //obmp = new Bitmap(16, 16);
             //gr = Graphics.FromImage(obmp);
             //gr.InterpolationMode = InterpolationMode.NearestNeighbor;
             //gr.DrawImage(bmp, new Rectangle(Point.Empty, obmp.Size));
             //gr.Flush();
             //gr.Dispose();
             obmp = (Bitmap)bmp.GetThumbnailImage(16, 16, null, IntPtr.Zero);
             p = GetTheMost256Color(obmp);
             obmp.Dispose();
             if (p != null)
             {
                 //set the color palette
                 ColorPalette pal = v_bmp.Palette;
                 FieldInfo f = pal.GetType().GetField("entries", BindingFlags.NonPublic | BindingFlags.Instance);
                 //set the palette
                 f.SetValue(pal, p);
                 v_bmp.Palette = pal;
                 BitmapData v_data = bmp.LockBits(new Rectangle(Point.Empty, bmp.Size),
                   ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                 BitmapData v_data2 = v_bmp.LockBits(new Rectangle(Point.Empty, bmp.Size),
                               ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);
                 int offset = 0;
                 int offset2 = 0;
                 int r = 0;
                 int g = 0;
                 int b = 0;
                 Dictionary<Color, int> dic = new Dictionary<Color, int>();
                 for (int i = 0; i < p.Length; i++)
                 {
                     dic.Add(p[i], i);
                 }
                 Color v_cl = Color.Empty;
                 for (int h = 0; h < bmp.Height; h++)
                 {
                     for (int w = 0; w < bmp.Width; w++)
                     {
                         offset = h * v_data.Stride + w * 3;
                         offset2 = h * v_data2.Stride + w;
                         b = Marshal.ReadByte(v_data.Scan0, offset);
                         g = Marshal.ReadByte(v_data.Scan0, offset + 1);
                         r = Marshal.ReadByte(v_data.Scan0, offset + 2);
                         v_cl = Color.FromArgb(r, g, b);
                         if (dic.ContainsKey(v_cl))
                             Marshal.WriteByte(v_data2.Scan0, offset2, (byte)dic[v_cl]);
                     }
                 }
                 bmp.UnlockBits(v_data);
                 v_bmp.UnlockBits(v_data2);
             }
             return v_bmp;
         }
         /// <summary>
         /// get the most most 256 color 
         /// </summary>
         /// <param name="bmp"></param>
         /// <returns></returns>
         private static Color[] GetTheMost256Color(Bitmap bmp)
         {
             if ((bmp == null) || (bmp.PixelFormat == PixelFormat.Undefined))
                 return null;
             Dictionary<Color, ColorMark> dic = new Dictionary<Color, ColorMark>();
             Color[] cl = null;
             Color v_cl = Color.Empty;
             BitmapData v_data = bmp.LockBits(new Rectangle(Point.Empty, bmp.Size),
                 ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
             int offset = 0;
             int r = 0;
             int g = 0;
             int b = 0;
             //dic.Add(Color.Transparent, new ColorMark(Color.Transparent, bmp.Width * bmp.Height));
             for (int h = 0; h < bmp.Height; h++)
             {
                 for (int w = 0; w < bmp.Width; w++)
                 {
                     offset = h * v_data.Stride + w * 3;
                     b = Marshal.ReadByte(v_data.Scan0, offset);
                     g = Marshal.ReadByte(v_data.Scan0, offset + 1);
                     r = Marshal.ReadByte(v_data.Scan0, offset + 2);
                     v_cl = Color.FromArgb(r, g, b);
                     if (dic.ContainsKey(v_cl))
                     {
                         ColorMark p = dic[v_cl];
                         p.count++;
                         dic[v_cl] = p;
                     }
                     else
                         dic[v_cl] = new ColorMark(v_cl, 1);
                 }
             }
             bmp.UnlockBits(v_data);
             List<ColorMark> mp = null;
             ColorMark[] tb = null;
             if (dic.Count <= 256)
             {
                 cl = new Color[dic.Count];
                 //copy color
                 mp = new List<ColorMark>();
                 tb = new ColorMark[dic.Count];
                 dic.Values.CopyTo(tb, 0);
                 mp.AddRange(tb);
                 mp.Sort();
                 // Dictionary<Color, ColorMark>.Enumerator e = dic.GetEnumerator();
                 for (int i = 0; i < cl.Length; i++)
                 {
                     cl[i] = mp[i].color;
                 }
                 //while (e.MoveNext())
                 //{
                 //    cl[i] = e.Current.Key;
                 //    i++;
                 //}
                 return cl;
             }
             cl = new Color[256];
             tb = new ColorMark[dic.Count];
             mp = new List<ColorMark>();
             dic.Values.CopyTo(tb, 0);
             mp.AddRange(tb);
             mp.Sort();
             for (int i = 0; i < cl.Length; i++)
             {
                 cl[i] = mp[i].color;
                 // Console.WriteLine("Percent : " + mp[i].GetPercent(bmp));
             }
             return cl;
         }
         static Color[] GetTheMost16Color(Bitmap bmp)
         {
             //16 x 16 
             Dictionary<Color, ColorMark> dic = new Dictionary<Color, ColorMark>();
             Color[] cl = null;
             Color v_cl = Color.Empty;
             BitmapData v_data = bmp.LockBits(new Rectangle(Point.Empty, bmp.Size),
                 ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
             int offset = 0;
             int r = 0;
             int g = 0;
             int b = 0;
             for (int h = 0; h < bmp.Height; h++)
             {
                 for (int w = 0; w < bmp.Width; w++)
                 {
                     offset = h * v_data.Stride + w * 3;
                     b = Marshal.ReadByte(v_data.Scan0, offset);
                     g = Marshal.ReadByte(v_data.Scan0, offset + 1);
                     r = Marshal.ReadByte(v_data.Scan0, offset + 2);
                     v_cl = Color.FromArgb(r, g, b);
                     if (dic.ContainsKey(v_cl))
                     {
                         ColorMark p = dic[v_cl];
                         p.count++;
                         dic[v_cl] = p;
                     }
                     else
                         dic[v_cl] = new ColorMark(v_cl, 1);
                 }
             }
             bmp.UnlockBits(v_data);
             if (dic.Count <= 16)
             {
                 cl = new Color[dic.Count];
                 //copy color
                 int i = 0;
                 Dictionary<Color, ColorMark>.Enumerator e = dic.GetEnumerator();
                 while (e.MoveNext())
                 {
                     cl[i] = e.Current.Key;
                     i++;
                 }
                 return cl;
             }
             cl = new Color[16];
             ColorMark[] tb = new ColorMark[dic.Count];
             List<ColorMark> mp = new List<ColorMark>();
             dic.Values.CopyTo(tb, 0);
             mp.AddRange(tb);
             mp.Sort();
             for (int i = 0; i < cl.Length; i++)
             {
                 cl[i] = mp[i].color;
             }
             return cl;
         }
         public static byte[] GetImageData(Bitmap bmp, int type)
         {
             System.IO.MemoryStream mem = new System.IO.MemoryStream();
             switch (type)
             {
                 case 0:
                     bmp.Save(mem, ImageFormat.Bmp);
                     break;
                 case 1:
                 default:
                     bmp.Save(mem, ImageFormat.Png);
                     break;
             }
             mem.Seek(0, System.IO.SeekOrigin.Begin);
             BinaryReader sreader = new BinaryReader(mem);
             byte[] tab = new byte[mem.Length];
             sreader.Read(tab, 0, tab.Length);
             mem.Close();
             return tab;
         }
         /// <summary>
         /// 
         /// </summary>
         /// <param name="bmp"></param>
         /// <param name="type">0 for bmp and 1 for png. other value will result of png</param>
         /// <returns></returns>
         public static string BitmapToBase64String(Bitmap bmp,
             int type)
         {
             System.IO.MemoryStream mem = new System.IO.MemoryStream();
             switch (type)
             {
                 case 0:
                     bmp.Save(mem, ImageFormat.Bmp);
                     break;
                 case 1:
                 default:
                     bmp.Save(mem, ImageFormat.Png);
                     break;
             }
             mem.Seek(0, System.IO.SeekOrigin.Begin);
             BinaryReader sreader = new BinaryReader(mem);
             byte[] tab = new byte[mem.Length];
             sreader.Read(tab, 0, tab.Length);
             string _o = Convert.ToBase64String(tab, 0, tab.Length);
             mem.Close();
             return _o;
         }
         /// <summary>
         /// convert bitmap to base 64 string using png save format
         /// </summary>
         /// <param name="bmp"></param>
         /// <param name="inputSize"></param>
         /// <returns></returns>
         public static string BitmapToBase64String(Bitmap bmp, bool inputSize)
         {
             string v_out = null;
             MemoryStream mem = new MemoryStream();
             try
             {
                 using (var cbmp = new Bitmap(bmp))
                 {
                     cbmp.Save(mem, ImageFormat.Png);
                 }
                 mem.Seek(0, SeekOrigin.Begin);
                 Byte[] v_tab = new byte[mem.Length];
                 mem.Read(v_tab, 0, v_tab.Length);//new StreamReader(mem).ReadToEnd();
                 if (inputSize)
                 {
                     v_out = bmp.Width + "|" + bmp.Height + "|";
                 }
                 v_out = Convert.ToBase64String(v_tab, 0, v_tab.Length);
             }
             catch(Exception ex) {
                 CoreLog.WriteLine("impossible "+ex.Message );
             }
             return v_out;
             //string v_out = string.Empty;
             //byte[] v_tab = new byte[bmp.Width * bmp.Height * 4];
             //BitmapData v_data = bmp.LockBits(new Rectangle(Point.Empty, bmp.Size), System.Drawing.Imaging.ImageLockMode.ReadOnly, enuPixelFormat.Format32bppArgb);
             //System.Runtime.InteropServices.Marshal.Copy(v_data.Scan0, v_tab, 0, v_tab.Length);
             //bmp.UnlockBits(v_data);
             //if (inputSize)
             //{
             //    v_out = bmp.Width + "|" + bmp.Height + "|" + Convert.ToBase64String(v_tab, 0, v_tab.Length);
             //}
             //return v_out;
         }
         public static string BitmapToBase64String(Bitmap bmp)
         {
             return BitmapToBase64String(bmp, true);
         }
         public static Bitmap StringBase64ToBitmap(string value)
         {
             Bitmap bmp = null;
            //private string value to avoid out of memory form large file
            List<string> sb = new List<string>();
            value = value.Trim();
            StringBuilder v_sb = new StringBuilder();
            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] == '|') {

                    if (v_sb.Length > 0)
                        sb.Add(v_sb.ToString());
                    v_sb.Length = 0;
                    continue;
                }
                v_sb.Append(value[i]);
            }
            if (v_sb.Length > 0)
                sb.Add(v_sb.ToString());

            string[] tb = sb.ToArray();

            //out of memory

             //string[] tb = value.Trim().Split(new  string[]{"|"}, StringSplitOptions.RemoveEmptyEntries ) ;
             MemoryStream mem = null;
             if (tb.Length == 1)
             {
                 try
                 {
                     byte[] t = Convert.FromBase64String(value);
                     mem = new MemoryStream();        
                     mem.Write(t, 0, t.Length);
                     mem.Seek(0, SeekOrigin.Begin);
                     try
                     {
                         bmp = Bitmap.FromStream(mem) as Bitmap;
                         return bmp;
                     }
                     catch
                     {
                     }
                 }
                 catch { 

                 }
             }
             if (tb.Length != 3)
             {
                 return null;
             }
             bmp = new Bitmap(Int32.Parse(tb[0]), Int32.Parse(tb[1]), PixelFormat.Format32bppArgb);
             Byte[] v_tab = Convert.FromBase64String(tb[2]);
             mem = new MemoryStream();
             mem.Write(v_tab, 0, v_tab.Length);
             mem.Seek(0, SeekOrigin.Begin);
             using (Bitmap c = Bitmap.FromStream(mem) as Bitmap)
             {
                 if (c != null)
                 {
                     Graphics g = Graphics.FromImage(bmp);
                     g.DrawImage(c, Point.Empty);
                 }
             }

             //BitmapData v_data = bmp.LockBits(new Rectangle(Point.Empty, bmp.Size), System.Drawing.Imaging.ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
             //System.Runtime.InteropServices.Marshal.Copy(v_tab, 0, v_data.Scan0, v_tab.Length);
             //bmp.UnlockBits(v_data);
             return bmp;
         }
         public static Metafile BuildMetaFileFromBitmap(Bitmap bitmap)
         {
             if (bitmap == null)
                 return null;
             Bitmap bmp = bitmap.Clone() as Bitmap;
             Graphics g = Graphics.FromImage(bmp);
             IntPtr hdc = g.GetHdc();
             System.IO.MemoryStream mem = new System.IO.MemoryStream();
             Metafile mfile = new Metafile(mem, hdc);
             //draw picture
             using (Graphics c = Graphics.FromImage(mfile))
             {
                 c.DrawImage(bitmap, Point.Empty);
             }
             g.ReleaseHdc(hdc);
             g.Dispose();
             bmp.Dispose();
             //mem.Flush();
             //mem.Seek(0, System.IO.SeekOrigin.Begin);
             //Stream fs = File.Create("d:\\outddf.emf");
             //mem.WriteTo(fs);
             //fs.Flush();
             //fs.Close();
             mem.Dispose();
             return mfile;
         }
         struct ColorMark : IComparable
         {
             internal int count;
             internal Color color;
             public float GetPercent(Bitmap bmp)
             {
                 return (count / (float)(bmp.Width * bmp.Height)) * 100;
             }
             public ColorMark(Color cl, int count)
             {
                 this.color = cl;
                 this.count = count;
             }
             #region IComparable Members
             public int CompareTo(object obj)
             {
                 if (!(obj is ColorMark))
                     return -1;
                 ColorMark m = (ColorMark)obj;
                 int r = m.count.CompareTo(this.count);//.CompareTo(m.count);
                 return r;
             }
             #endregion
             public override string ToString()
             {
                 return color.ToString() + " : " + count;
             }
         }
   
         public static ICoreBitmap GetBitmap(ICore2DDrawingDocument document, float dpix, float dpiy)
         {
             if (document == null)
                 return null;
             return document.ToBitmap(document.Width, document.Height, true, dpix, dpiy);
        
         }
         public static ICoreBitmap GetBitmap(ICore2DDrawingLayeredElement element)
         {
             return GetBitmap(element, CoreScreen.DpiX, CoreScreen.DpiY);
         }
         public static ICoreBitmap GetBitmap(ICore2DDrawingLayeredElement element, float dpix, float dpiy)
         {
             
             Rectanglei rc = Rectanglef.Round(element.GetBound ());
             WinCoreBitmap v_bmp = WinCoreBitmap.Create(rc.Width, rc.Height);
             v_bmp.SetResolution(dpix, dpiy);
             using (ICoreGraphics g =   WinCoreBitmapDeviceVisitor.Create ( Graphics.FromImage (v_bmp.Bitmap )))
             {
             //Bitmap v_bmp = new Bitmap(rc.Width, rc.Height);

                g.TranslateTransform (-rc.X, -rc.Y, enuMatrixOrder.Append);
                element.Draw (g);
                g.Flush();
             }
        
             return v_bmp;
         }
         public static Bitmap GetGdiBitmap(ICore2DDrawingDocument document)
         {
             return GetGdiBitmap(document, CoreScreen.DpiX, CoreScreen.DpiY);
         }
         public static Bitmap GetGdiBitmap(ICore2DDrawingDocument document, float dpix, float dpiy)
         {
             ICoreBitmap bmp = WinCoreBitmapOperation.GetBitmap(document, dpix, dpiy );
             Bitmap v_bmp =  bmp.ToGdiBitmap();
             bmp.Dispose();
             return v_bmp;
         }

        /// <summary>
        /// get an icon from bitmap
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="format"></param>
        /// <returns></returns>
         public static Icon GetSystemIcon(Bitmap bmp)
         {
            //convert string image 
             int w = bmp.Width;
             int h = bmp.Height;
             bmp.RotateFlip(RotateFlipType.Rotate180FlipX);
             BitmapData v_bmpdata = bmp.LockBits(new Rectangle(Point.Empty, bmp.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
             //int bitcount = 4;
           
             int s = v_bmpdata.Stride;
             MemoryStream v_stream = new MemoryStream();
             BinaryWriter v_binW = new BinaryWriter(v_stream);
             //build cursor definition
             //1. write definition
             v_binW.Write((short)0);//reserved
             v_binW.Write((short)1);//for cursor
             v_binW.Write((short)1);//only on icon
             //2. write entry
            //internal byte width;
            //internal byte height;
            //internal byte colors;
            //internal byte reserved;
            //internal short plane;
            //internal short bitperPixel;
            //internal int imageSize;
            //internal int offset;
             v_binW.Write((byte)((w < 256) ? w : 0));
             v_binW.Write((byte)((h < 256) ? h : 0));
             v_binW.Write((byte)0);//colors;
             v_binW.Write((byte)0);//reserved;
             v_binW.Write((short)0);//16);//hotspot x - plane;//plane
             v_binW.Write((short)32);//16);//hotsopt y - bitperPixel;//bitperPixel; //!\ important for safari
             v_binW.Write((int)(40 + (s * h) + (w * h) / 8));//imageSize;
             v_binW.Write((int)22);//offset;
             //write bmp info header
             v_binW.Write((int)40);//size
             v_binW.Write((int)w);
             v_binW.Write((int)(h * 2));
             v_binW.Write((short)1);// plane;
             v_binW.Write((short) 32);// bitcount;
             v_binW.Write((int)0);//no  compression;
             v_binW.Write((int)(40 + (s * h) + (w * h) / 8));// int bitmapSize;
             v_binW.Write((int)0);// dpih;
             v_binW.Write((int)0);// dpiv;
             v_binW.Write((int)0);// colorspalette;
             v_binW.Write((int)0);// importantcolor;
             //write data
             Byte[] d = new byte[s * h];
             // int offset = 0;
             //for (int i = 0; i < w; i++)
             //{
             //    for (int j = 0; j < h; j++)
             //    {
             //        offset = j * s + i * 4;
             //        d[offset] = 0;       //blue
             //        d[offset + 1] = 0;   //green
             //        d[offset + 2] = 255; //red
             //        d[offset + 3] = 255; //alpha
             //    }
             //}
             Marshal.Copy(v_bmpdata.Scan0, d, 0, d.Length);
             v_binW.Write(d, 0, d.Length);
             bmp.UnlockBits(v_bmpdata);
             //write mask
             d = new byte[w * h / 8];
             //for (int i = 0; i < d.Length; i++)
             //{
             //    d[i] = 0xAA;
             //}
             //d = GetMaskFromBitmap(bmp);
             v_binW.Write(d);
             v_binW.Flush();
             v_stream.Seek(0, SeekOrigin.Begin);
             Icon c = new Icon(v_stream);
             v_stream.Close();
             v_stream.Dispose();
             return c;
         }
         /// <summary>
         /// get cursor form data
         /// </summary>
         /// <param name="bmp"></param>
         /// <param name="p"></param>
         /// <returns></returns>
         internal static Cursor GetCursorFrom(Bitmap bmp, int p)
         {
             int w = bmp.Width;
             int h = bmp.Height;
             BitmapData v_bmpdata = bmp.LockBits(new Rectangle(Point.Empty, bmp.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
             int s = v_bmpdata.Stride;
             MemoryStream v_stream = new MemoryStream();
             BinaryWriter v_binW = new BinaryWriter(v_stream);
             //build cursor definition
             //1. write definition
             v_binW.Write((short)0);//reserved
             v_binW.Write((short)2);//for cursor
             v_binW.Write((short)1);//only on icon
             //2. write entry
             v_binW.Write((byte)((w < 256) ? w : 0));
             v_binW.Write((byte)((h < 256) ? h : 0));
             v_binW.Write((byte)0);//colors;
             v_binW.Write((byte)0);//reserved;
             v_binW.Write((short)16);//hotspot x - plane;
             v_binW.Write((short)16);//hotsopt y - bitperPixel;
             v_binW.Write((int)40 + s * h + (s * h) / 8);//imageSize;
             v_binW.Write((int)22);//offset;
             //write bmp info header
             v_binW.Write((int)40);//size
             v_binW.Write((int)w);
             v_binW.Write((int)h * 2);
             v_binW.Write((short)1);// plane;
             v_binW.Write((short)32);// bitcount;
             v_binW.Write((int)0);//no  compression;
             v_binW.Write((int)(40 + s * h + (w * h) / 8));// int bitmapSize;
             v_binW.Write((int)0);// dpih;
             v_binW.Write((int)0);// dpiv;
             v_binW.Write((int)0);// colorspalette;
             v_binW.Write((int)0);// importantcolor;
             //write data
             Byte[] d = new byte[s * h];
             int offset = 0;
             for (int i = 0; i < w; i++)
             {
                 for (int j = 0; j < h; j++)
                 {
                     offset = j * s + i * 4;
                     d[offset] = 0;       //blue
                     d[offset + 1] = 0;   //green
                     d[offset + 2] = 255; //red
                     d[offset + 3] = 255; //alpha
                 }
             }
             Marshal.Copy(v_bmpdata.Scan0, d, 0, d.Length);
             v_binW.Write(d, 0, d.Length);
             bmp.UnlockBits(v_bmpdata);
             //write mask
             d = new byte[s * h / 8];
             for (int i = 0; i < d.Length; i++)
             {
                 d[i] = 0xAA;
             }
             //d = GetMaskFromBitmap(bmp);
             v_binW.Write(d);
             v_binW.Flush();
             v_stream.Seek(0, SeekOrigin.Begin);
             //Icon c = new Icon(v_stream);
             //Bitmap cbmp = c.ToBitmap();
             //using (Form frm = new Form())
             //{
             //    frm.BackgroundImage = cbmp;
             //    frm.ShowDialog();
             //}
             Cursor cr = new Cursor(v_stream);
             return cr;
         }
         static Byte[] GetMaskFromBitmap(Bitmap bmp)
         {
             int h = bmp.Height;
             int w = bmp.Width;
             bmp.MakeTransparent();
             Bitmap bitmap2 = GetMask(bmp, 0, true, false);
             WinCoreBitmapData vd = WinCoreBitmapData.FromBitmap(bitmap2);
             //calcul du maskage
             //stride /8
             int stride = w / 8;
             //nombre d'octet ajouter
             int dstride = ((dstride = stride % 4) > 0) ? 4 - dstride : 0;
             stride = (stride + dstride) * h;
             byte[] dmask = new byte[stride];
             int mask = 0;
             int offset = 0;
             int count = 0;
             int decal = 0;
             for (int j = 0; j < h; j++)
             {
                 decal = 0;
                 mask = 0;
                 for (int i = 0; i < w; i++)
                 {
                     offset = (int)((j * vd.Stride) + (i * 4));
                     if (vd.Data[offset] == 255)
                     {
                         //mask += 0
                     }
                     else
                     {
                         mask += 1;
                     }
                     if (decal > 6)
                     {
                         decal = 0;
                         dmask[count] = (byte)mask;
                         mask = 0;
                         ++count;
                     }
                     else
                     {
#pragma warning disable IDE0054 // Use compound assignment
                         mask = mask << 1;
#pragma warning restore IDE0054 // Use compound assignment
                         ++decal;
                     }
                 }
                 count += dstride;
             }
             return dmask;
         }
    }
}