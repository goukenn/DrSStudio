

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AdjustlLevel.cs
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
file:AdjustlLevel.cs
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
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
namespace IGK.DrSStudio.Drawing2D.Menu.Effect
{
    [IGK.DrSStudio.Menu.CoreMenu("Image.Filters.AdjustLevel", 1)]
    sealed class AdjustlLevel : ImageMenuBase, IBitmapEffect
    {
        protected override bool PerformAction()
        {
            this.ImageElement.SetBitmap(
                ApplyEffect(this.ImageElement.Bitmap), false);
            this.ImageElement.Invalidate(true);
            return false;
        }
        #region IBitmapEffect Members
        public System.Drawing.Bitmap ApplyEffect(System.Drawing.Bitmap bmp)
        {
            int stride = 0;
            Bitmap v_outBmp = new Bitmap(bmp.Width, bmp.Height);
            Rectangle v_rc = new Rectangle(Point.Empty, bmp.Size);
            BitmapData v_inBMData = bmp.LockBits(v_rc, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            Byte[] v_inData = new Byte[v_inBMData.Stride * v_inBMData.Height];
            Byte[] v_outData = new Byte[v_inBMData.Stride * v_inBMData.Height];
            stride = v_inBMData.Stride;
            Marshal.Copy(v_inBMData.Scan0, v_inData, 0, v_inData.Length);
            bmp.UnlockBits(v_inBMData);
            int offset = 0;
            Color minIn = Color.Black;
            Color maxIn = Color.White;
            Color minOut = Color.Black ;
            Color maxOut = Color.White;
            float[] gm = new float[] { 1.2f, 1.0f, 0.8f };
            //Colorf rf = Colorf.Empty;
            int r, g, b;
            int v = 0;
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    offset = y * stride + 4 * x;
                    r =             v_inData[offset];
                    g =         v_inData[offset + 1];
                    b =      v_inData[offset + 2];
                    //for red 
                    v = r - minIn.R;
                    if (v < 0)
                    {
                        r = minOut.R;
                    }
                    else if (v >= maxIn.R)
                    {
                        r = maxOut.R;
                    }
                    else {
                        r =(int)( minOut.R + (maxOut.R - minOut.R) * Math.Pow(
                            v /(float) (maxOut.R - minOut.R), gm[0]));
                    }
                    //for green
                    v = g - minIn.G;
                    if (v < 0)
                    {
                        g = minOut.G;
                    }
                    else if (v >= maxIn.G)
                    {
                        g = maxOut.G;
                    }
                    else
                    {
                        g = (int)(minOut.G + (maxOut.G - minOut.G) * Math.Pow(
                            v / (float)(maxOut.G - minOut.G), gm[1]));
                    }
                    //for blue
                    v = b - minIn.B;
                    if (v < 0)
                    {
                        b = minOut.B;
                    }
                    else if (v >= maxIn.B)
                    {
                        b = maxOut.B;
                    }
                    else
                    {
                        b = (int)(minOut.B + (maxOut.B - minOut.B) * Math.Pow(
                            v / (float)(maxOut.B - minOut.B), gm[2]));
                    }
                    v_outData[offset] = (byte) ((r>255)? 255: r <0? 0 : r);
                    v_outData[offset + 1] = (byte)((g > 255) ? 255 : g < 0 ? 0 : g);
                    v_outData[offset + 2] = (byte)((b > 255) ? 255 : b < 0 ? 0 : b);
                    v_outData[offset + 3] = v_inData[offset + 3];
                }
            }
            BitmapData v_outBMData = v_outBmp.LockBits(v_rc, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(v_outData, 0, v_outBMData.Scan0, v_outData.Length);
            v_outBmp.UnlockBits(v_outBMData);
            return v_outBmp;
        }
#endregion
    }
}

