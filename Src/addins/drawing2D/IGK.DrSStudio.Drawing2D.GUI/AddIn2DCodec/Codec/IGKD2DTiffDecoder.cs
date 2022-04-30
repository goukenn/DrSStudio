

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DTiffDecoder.cs
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
file:IGKD2DTiffDecoder.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
namespace IGK.DrSStudio.Drawing2D.Codec
{
    using IGK.ICore.WinCore;
using IGK.ICore.Codec;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.Drawing2D.WinUI;
    using IGK.ICore.WinUI;
    using IGK.ICore;

    [IGKD2DBitmapDecoder("Tiff", "pictures/Tiff", "tiff")]
    public class IGKD2DTiffDecoder : 
        CoreDecoderBase,
        ICoreBitmapDecoder
    {
        public override bool Open(ICoreWorkbench bench, string filename, bool selected)
        {
            Type t = CoreSystem.GetWorkingObjectType(CoreConstant.DRAWING2D_SURFACE_TYPE);
            if (t == null)
                return false;
                try
                {
                    Image[] v_tab = GetAllPages(filename);
                    if ((v_tab  == null)||(v_tab .Length ==0))
                        return false;
                    ICore2DDrawingSurface v_surface = t.Assembly.CreateInstance(t.FullName) as ICore2DDrawingSurface;
                    ICore2DDrawingDocument v_docs = null;
                    for (int i = 0; i < v_tab.Length; ++i)
                    {
                        ImageElement img = ImageElement.CreateFromBitmap (WinCoreBitmap.Create (v_tab[i] as Bitmap ));
                        if (img == null) 
                            continue;
                        if (i == 0)
                        {
                            v_surface.CurrentLayer.Elements.Add(img);
                            v_docs = v_surface.CurrentDocument;
                        }
                        else
                        {
                            v_docs = v_surface.CreateNewDocument();
                            v_docs.CurrentLayer.Elements.Add(img);
                            v_surface.Documents.Add(v_docs);
                        }
                        v_docs.SetSize(img.Width, img.Height);
                    }
                    bench.AddSurface (v_surface, selected );
                }
                catch
                {
                    CoreLog.WriteDebug("Error When Opening file");
                    return false;
                }
                return true;
        }
        ICoreBitmap ICoreBitmapDecoder.GetBitmap(string file)
        {
            return CoreApplicationManager.Application.ResourcesManager.CreateBitmapFromFile(file);
        }
        public Image GetBitmap(string filename)
        {
            Image[] v_img = GetAllPages(filename);
            if (v_img.Length == 0)
                return null;
            return v_img[0];
        }
        public Image GetBitmap(string filename, int index)
        {
            Image[] v_img = GetAllPages(filename);
            if ((v_img.Length == 0) || ((index < 0) && (index >= v_img.Length)))
                return null;
            return v_img[index];
        }
        private Image[] GetAllPages(string file)
        {
            MemoryStream byteStream = null;
            List<Image> images = new List<Image>();
            Bitmap bitmap = null;
            try
            {
                bitmap = (Bitmap)Image.FromFile(file);
            }
            catch
            {
                return null;
            }
            int count = bitmap.GetFrameCount(FrameDimension.Page);
            for (int idx = 0; idx < count; idx++)
            {
                // save each frame to a bytestream
                bitmap.SelectActiveFrame(FrameDimension.Page, idx);
                byteStream = new MemoryStream();
                bitmap.Save(byteStream, ImageFormat.Tiff);
                // and then create a new Image from it
                images.Add((Bitmap)Image.FromStream(byteStream));
                byteStream.Close();
            }
            bitmap.Dispose();
            return images.ToArray();
        }
        #region ICoreBitmapDecoder Members
        public Bitmap GetBitmap(string file, int width, int height, bool proportional)
        {
            Bitmap bmp = new Bitmap(file);
            Bitmap obmp = new Bitmap(width, height);
            float ex = obmp.Width / (float)bmp.Width;
            float ey = obmp.Height / (float)bmp.Height;
            int x = 0;
            int y = 0;
            int w = obmp.Width;
            int h = obmp.Height;
            if (proportional)
            {
                ex = Math.Min(ex, ey);
                x = (int)(((-bmp.Width * ex) / 2.0f) + (width / 2.0f));
                y = (int)(((-bmp.Height * ex) / 2.0f) + (height / 2.0f));
                w = (int)(ex * bmp.Width);
                h = (int)(ex * bmp.Height);
            }
            using (Graphics g = Graphics.FromImage(obmp))
            {
                g.DrawImage(bmp, new Rectangle(x, y, w, h), new Rectangle(0, 0, bmp.Width, bmp.Height),
                    GraphicsUnit.Pixel);
            }
            bmp.Dispose();
            return obmp;
        }
        public Bitmap GetBitmap(string file, int with, int height)
        {
            try
            {
                Bitmap bmp = new Bitmap(file);
                Bitmap obmp = new Bitmap(bmp, with, height);
                bmp.Dispose();
                return obmp;
            }
            catch { 
            }
            return null;
        }
        #endregion
    }
}

