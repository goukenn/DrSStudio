

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD3DRasterBitmapEncoder.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

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
﻿using IGK.ICore.Codec;
using IGK.DrSStudio.Drawing3D.OpenGL.Codec;
using IGK.OGLGame.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.Drawing2D.OpenGL.Codec
{
    /// <summary>
    /// represent a raster bitmap encoder
    /// </summary>
    class IGKD3DRasterBitmapEncoder : CoreEncoderBase
    {
        private static IGKD3DRasterBitmapEncoder sm_instance;
        private IGKD3DRasterBitmapUserControl m_glControl;

        class IGKD3DRasterBitmapUserControl : Control
        {
            private OGLGraphicsDevice m_glDevice;
            public IGKD3DRasterBitmapUserControl()
            {
                this.SetStyle(ControlStyles.ResizeRedraw, false);
                this.SetStyle(ControlStyles.UserPaint, false);
                this.SetStyle(ControlStyles.Opaque, true);
                this.SetStyle(ControlStyles.OptimizedDoubleBuffer, false);
                this.CreateControl();
            }
            protected override void CreateHandle()
            {
                base.CreateHandle();
            }
            protected override void OnHandleCreated(EventArgs e)
            {
                base.OnHandleCreated(e);
                this.m_glDevice = OGLGraphicsDevice.CreateDeviceFromHWND(this.Handle);
                if (this.m_glDevice == null)
                {
                    throw new CoreException("msg.err.cantcreategldevice".R());
                }
            }
            protected override void OnHandleDestroyed(EventArgs e)
            {
                base.OnHandleDestroyed(e);
            }
            public Bitmap Save(ICore2DDrawingDocument document)
            {
                if (this.m_glDevice == null)
                    return null;

                var dev = this.m_glDevice;

                this.m_glDevice.MakeCurrent();
                this.Size = new Size(document.Width, document.Height);
                this.m_glDevice.Viewport = new Rectanglei(0, 0, document.Width, document.Height);
                Bitmap bmp = null;

               bmp= new Bitmap (document.Width , document.Height );

                    var  visitor = IGKOGL2DDrawingDeviceVisitor.Create(dev);
                    SpriteBatch batch = new SpriteBatch(dev, new BitmapSpriteBatchGraphicsView(bmp));


                    dev.Clear(Colorf.Transparent);
                    batch.Begin();
                    visitor.Visit(document);
                    batch.End();
                    dev.Flush();
                    dev.FreeResources();

                BitmapData v_data = bmp.LockBits (new Rectangle(0,0, bmp.Width, bmp.Height ), ImageLockMode.ReadWrite , PixelFormat.Format32bppArgb);
                dev.ReadPixels(new Rectanglei(0, 0, document.Width, document.Height),
                     GraphicsPixelColorMode.BGRA,
                      GraphicsPixelColorType.UByte,
                      v_data.Scan0);
                bmp.UnlockBits(v_data);

                bmp.RotateFlip(RotateFlipType.Rotate180FlipX);
                return bmp;

            }
        }

        
        private IGKD3DRasterBitmapEncoder():base()
        {
            this.m_glControl = new IGKD3DRasterBitmapUserControl();
            this.m_glControl.CreateControl();


        }

        public static IGKD3DRasterBitmapEncoder Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static IGKD3DRasterBitmapEncoder()
        {
            sm_instance = new IGKD3DRasterBitmapEncoder();
        }
        public bool SaveToFile(string filename, ICore2DDrawingDocument document)
        {
            if (document == null)
                return false;
            ImageFormat format = GetImageFormatFromExtension(Path.GetExtension(filename));
            if (format == null)
                return false;

            using (Bitmap bmp = this.m_glControl.Save(document))
            {
                if (bmp != null)
                {
                    bmp.Save(filename, format);
                }
            }


            //using (Bitmap rbmp = this.SaveBitmapDocument(document))
            //{
            //    if (rbmp != null)
            //    {
            //        rbmp.Save(filename, format);
            //    }
            //}
            return true;
        }
        /*
         * 
         * this method save rater bitmap but it failed for texture more large enougth
         * */
        private Bitmap SaveBitmapDocument(ICore2DDrawingDocument document)
        {
            if ((document ==null) || (document.Width <=0) || (document.Height <=0))
                return null;
            Bitmap bmp = new Bitmap(document.Width, document.Height);
            
                Graphics g = Graphics.FromImage(bmp);
                IntPtr v_hdc = g.GetHdc();
                IGKOGL2DDrawingDeviceVisitor visitor = null;
                using (OGLGraphicsDevice dev = OGLGraphicsDevice.CreateDeviceFromHDC(v_hdc))
                {
                    if (dev != null)
                    {
                        visitor = IGKOGL2DDrawingDeviceVisitor.Create(dev);
                        SpriteBatch batch = new SpriteBatch(dev, new BitmapSpriteBatchGraphicsView(bmp));
                        dev.MakeCurrent();

                        dev.Viewport = new Rectanglei(0, 0, bmp.Height, bmp.Height);
                        batch.Begin();
                        visitor.Visit(document);
                        batch.End();
                        dev.Flush();

                    }
                }
                g.ReleaseHdc(v_hdc);
                g.Flush();
                bmp.RotateFlip(RotateFlipType.Rotate180FlipX);
            return bmp;
        }

        private ImageFormat GetImageFormatFromExtension(string extension)
        {
            switch (extension.ToLower())
            { 
                case ".png":
                    return ImageFormat.Png;
                case ".jpeg":
                case ".jpg":
                    return ImageFormat.Jpeg;
                case ".tiff":
                    return ImageFormat.Tiff;
                case ".bmp":
                    return ImageFormat.Bmp;
            }
            return null;
        }

        public override bool Save(ICoreWorkingSurface surface, string filename, params ICoreWorkingDocument[] documents)
        {
            return false;
        }
    }
}
