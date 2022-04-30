


using IGK.ICore;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Utils.cs
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
file:Utils.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.OGLGame
{
    /// <summary>
    /// represent a utily of the system ogl game
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// save current rendering state to bitmap
        /// </summary>
        /// <param name="device"></param>
        /// <param name="with"></param>
        /// <param name="height"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static System.Drawing.Bitmap SaveToBitmap(IGK.OGLGame.Graphics.OGLGraphicsDevice device, int with, int height,
             IGK.OGLGame.Graphics.GraphicsPixelColorMode mode)
        {
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(with, height);
            System.Drawing.Imaging.BitmapData data = bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height),
                 System.Drawing.Imaging.ImageLockMode.ReadWrite,
                  System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            device.ReadPixels (new Rectanglei (0,0, with ,height), mode , IGK.OGLGame.Graphics.GraphicsPixelColorType .UByte , data.Scan0 );
            bmp.UnlockBits(data);
            bmp.RotateFlip(System.Drawing.RotateFlipType.RotateNoneFlipY );
            return bmp;
        }
    }
}

