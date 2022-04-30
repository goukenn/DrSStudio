

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WpfResources.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.Resources;
using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace IGK.DrSStudio.Wpf
{
    using IGK.ICore.Drawing2D;
    using IGK.ICore;
    using IGK.ICore.WinCore;
    public static class WpfResources
    {
         static Dictionary<string, System.Windows.Media.ImageSource> sm_res;
         static WpfResources()
         {
             CoreApplicationManager.ApplicationExit += Application_ApplicationExit;
         }

        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            sm_res.Clear();
        }

        public static System.Windows.Media.ImageSource GetImage(string p)
        {
            if (sm_res == null)
                sm_res = new Dictionary<string, System.Windows.Media.ImageSource>();

            if (sm_res.ContainsKey(p))
                return sm_res[p];
            var b = CoreResources.GetDocument(p).ToBitmap().ToGdiBitmap(true);
            if (b != null)
            {
                System.Windows.Media.ImageSourceConverter conv = new System.Windows.Media.ImageSourceConverter();
                System.IO.MemoryStream mem = new System.IO.MemoryStream();
                b.Save(mem, System.Drawing.Imaging.ImageFormat.Png);
                mem.Seek(0, System.IO.SeekOrigin.Begin);

                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = mem;
                image.EndInit();
                sm_res.Add(p, image);
                return image;
            }
            return null;
        }
    }
}
