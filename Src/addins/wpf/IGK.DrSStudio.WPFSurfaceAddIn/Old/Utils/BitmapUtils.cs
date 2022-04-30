

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: BitmapUtils.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:BitmapUtils.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows ;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;
using System.Runtime.InteropServices;
namespace IGK.DrSStudio.WPFSurfaceAddIn.Utils
{
    public static class BitmapUtils
    {
        [DllImport("gdi32.dll")]
        static extern int DeleteObject(IntPtr hobject);
        //public static System.Drawing.Bitmap bitmap GetBitmap(System.Windows.FrameworkElement.UI
        public static System.Drawing.Bitmap GetBitmap(
            System.Windows.UIElement uIElement)
        {
            return GetBitmap(uIElement, IGK.DrSStudio.WinUI.CoreScreen.DpiX,
                IGK.DrSStudio.WinUI.CoreScreen.DpiY);
        }
        public static System.Drawing.Bitmap GetBitmap(
            System.Windows.UIElement element, float dpix, float dpiy)
        {
            Transform transform = element.RenderTransform;
            element.RenderTransform = null;
            element.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
    //        element.Arrange(new Rect(new Point(), new Size (500,500)));
            var w = (double)element.GetValue(System.Windows.Controls.Canvas.ActualWidthProperty);
            var h = (double)element.GetValue(System.Windows.Controls.Canvas.ActualHeightProperty);
            RenderTargetBitmap rtb =
              new RenderTargetBitmap(
                (int)w,
                (int)h,
                dpix, dpiy, PixelFormats.Pbgra32);
            rtb.Render(element);
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(rtb));
            System.Drawing.Bitmap bmp = null;
            using (var ms = new MemoryStream())
            {
                encoder.Save(ms);
                bmp = new System.Drawing.Bitmap(ms);
            }
            element.RenderTransform = transform;
            return bmp;
        }
        public  static System.Windows.Media.ImageSource GetSourceFromBitmap(System.Drawing.Bitmap bmp)
        {
            System.Windows.Media.ImageSource v_src = null;
            IntPtr hbimap = bmp.GetHbitmap();
            try
            {
                v_src = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    hbimap,
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());
            }
            catch
            {
            }
            finally {
                DeleteObject(hbimap);
            }
            return v_src;
        }
    }
}

