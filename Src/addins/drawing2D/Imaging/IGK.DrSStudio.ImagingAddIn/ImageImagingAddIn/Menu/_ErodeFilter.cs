

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ErodeFilter.cs
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
file:_ErodeFilter.cs
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
using System.Drawing.Drawing2D ;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
namespace IGK.DrSStudio.Drawing2D.Menu
{
    [DrSStudioMenu("Image.Filters.Erode", 0)]  
    class _ErodeFilter :  ImageMenuBase
    {
        protected override bool PerformAction()
        {
            if (this.ImageElement != null)
            {
                this.ImageElement.SetBitmap(ApplyFilter(
                    this.ImageElement.Bitmap.ToGdiBitmap ()).ToCoreBitmap(),
                    false);
                this.ImageElement.Invalidate(true);
            }
            return false;
        }
        System.Drawing.Bitmap ApplyFilter(Bitmap bmp)
        {
            int stride = 0;
            Bitmap v_outBmp = new Bitmap(bmp.Width, bmp.Height);
            Rectangle v_rc = new Rectangle (Point.Empty , bmp.Size );
            BitmapData v_inBMData = bmp.LockBits(v_rc, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
           Byte[] v_inData = new Byte[v_inBMData .Stride * v_inBMData.Height ];
            Byte[] v_outData = new Byte[v_inBMData .Stride * v_inBMData.Height ];
            stride = v_inBMData.Stride ;
            Marshal.Copy(v_inBMData.Scan0, v_inData, 0, v_inData.Length);
            bmp.UnlockBits(v_inBMData);
            //filter operation
            int r = 0;
            int g = 0;
            int b = 0;
            int rt = 0;
            int gt = 0;
            int bt = 0;
            int offset = 0;
            float v_l = 0;
            int[] v_tabi = new int[] { -1, 0, 1 ,-1,0,1,-1,0,1};
            int[] v_tabj = new int[] { -1, -1, -1,0,0,0,1,1,1 };
            int v_h = 0;
            int v_w = 0;
            float v_t = 0;
            for (int j = 0; j < v_rc.Height; j++)
            {
            for (int x = 0; x < v_rc .Width; x++)
            {
                v_l = 0;
                v_t = 0;
                r = 0;
                g = 0;
                b = 0;
                for (int i = 0; i < 9; i++)
                {
                    v_w = v_tabi[i] + x;
                    v_h = v_tabj[i] + j;
#pragma warning disable IDE0054 // Use compound assignment
                    if (v_w < 0) v_w = v_w + v_rc.Width;
#pragma warning restore IDE0054 // Use compound assignment
#pragma warning disable IDE0054 // Use compound assignment
                    else if (v_w >= v_rc.Width) v_w = v_w - v_rc.Width;
#pragma warning restore IDE0054 // Use compound assignment
#pragma warning disable IDE0054 // Use compound assignment
                    if (v_h < 0) v_h = v_h + v_rc.Height;
#pragma warning restore IDE0054 // Use compound assignment
#pragma warning disable IDE0054 // Use compound assignment
                    else if (v_h >= v_rc.Height) v_h = v_h - v_rc.Height;
#pragma warning restore IDE0054 // Use compound assignment
                    offset = v_h  * stride + 4 * v_w ;
                    rt = v_inData[offset];
                    gt = v_inData[offset + 1];
                    bt = v_inData[offset + 2];
                    v_l = (((rt * 299) + (gt * 587) + (bt * 114)) / 10.0f);
                    if (v_l > v_t)
                    {
                        v_t = v_l;
                        r = rt;
                        g = gt;
                        b = bt;
                    }
                }
                offset = j * stride + 4 * x;
                    v_outData[offset ] = (byte)((r > 255) ? 255 : ((r < 0) ? 0 : r));
                    v_outData[offset + 1] = (byte)((g > 255) ? 255 : ((g < 0) ? 0 : g));
                    v_outData[offset + 2] = (byte)((b > 255) ? 255 : ((b < 0) ? 0 : b));
                    v_outData[offset + 3] = v_inData[offset + 3];
                }
            }
            BitmapData v_outBMData = v_outBmp.LockBits(v_rc, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(v_outData , 0, v_outBMData.Scan0, v_outData.Length);
            v_outBmp.UnlockBits(v_outBMData);
            //free
            return v_outBmp;
        }
    }
}

