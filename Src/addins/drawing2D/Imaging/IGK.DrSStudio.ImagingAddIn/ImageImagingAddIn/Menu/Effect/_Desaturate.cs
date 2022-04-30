

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _Desaturate.cs
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
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:_Desaturate.cs
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
    [DrSStudioMenu("Image.Filters.Desaturate", 1)]
    public class _Desaturate : ImageMenuBase, IBitmapEffect
    {
        protected override bool PerformAction()
        {
            this.ImageElement.SetBitmap(
                ApplyEffect(this.ImageElement.Bitmap.ToGdiBitmap()).ToCoreBitmap(), false);
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
            Colorf rf = Colorf.Empty ;
            float l = 0;
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    offset = y * stride + 4 * x;
                    l = Colorf.GetLuminosity ( Colorf.FromByteArgb (
                        v_inData[offset +3],
                        v_inData[offset ],
                        v_inData[offset +1],
                        v_inData[offset +2]
                        )) * 255 / 100.0f;
                    v_outData[offset] = (byte)l;
                    v_outData[offset+1] = (byte)l;
                    v_outData[offset+2] = (byte)l;
                    v_outData[offset + 3] = 255;
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

