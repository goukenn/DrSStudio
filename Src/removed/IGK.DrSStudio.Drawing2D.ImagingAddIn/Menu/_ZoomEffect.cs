

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ZoomEffect.cs
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
file:_ZoomEffect.cs
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
using System.Drawing ;
using System.Drawing.Drawing2D ;
using System.Drawing.Imaging ;
using System.Runtime.InteropServices ;
namespace IGK.DrSStudio.Drawing2D.Menu
{
    [IGK.DrSStudio.Menu.CoreMenu("Image.Effects.ZoomEffect", 3)]
    public sealed class _ZoomEffect
         : ImageMenuBase
    {
        /*php code doent work
             $red = ( $index["red"] * 0.393 + $index["green"] * 0.769 + $index["blue"] * 0.189 ) / 1.351;
        $green = ( $index["red"] * 0.349 + $index["green"] * 0.686 + $index["blue"] * 0.168 ) / 1.203;
        $blue = ( $index["red"] * 0.272 + $index["green"] * 0.534 + $index["blue"] * 0.131 ) / 2.140;
         */
        protected override bool PerformAction()
        {
            if (this.ImageElement != null)
            {
                this.ImageElement.SetBitmap(ApplyFilter(
                    this.ImageElement.Bitmap),
                    false);
                this.ImageElement.Invalidate(true);
            }
            return false;
        }
        public System.Drawing.Bitmap ApplyFilter(Bitmap bmp)
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
            //filter operation
            int rt = 0;
            int gt = 0;
            int bt = 0;
            int at = 0;
            long w = v_rc.Width ;
            long h = v_rc.Height;
            long fcx = w << 15;
            long fcy = h << 15;
            long v_radius = 100; // radius
            int offset = 0;
            const int n = 128;
           for (int j = 0; j < h; j++)
            {
                for (int i = 0; i < w; ++i)
                {
                    offset = (j * stride)  + 4 * i;               
                    for (int x = 0; x < 10; ++x)
                    {
                        long fx = (i << 16) - fcx;
                        long fy = (j << 16) - fcy;
                        int sr = 0;
                        int sg = 0;
                        int sb = 0;
                        int sa = 0;
                        int sc = 0;
                        rt = v_inData[offset];
                        gt = v_inData[offset + 1];
                        bt = v_inData[offset + 2];
                        at = v_inData[offset + 3];
                        //current point
                        sr += rt * at;
                        sg += gt * at;
                        sb += bt * at;
                        sa += at;
                        ++sc;
                        for (int y = 0; y < n; y++)
                        {
                            fx -= ((fx >> 4) * v_radius) >> 10;
                            fy -= ((fy >> 4) * v_radius) >> 10;
                            int u = (int)(fx + fcx + 32768 >> 16);
                            int v = (int)(fy + fcy + 32768 >> 16);
                            //calculate new offset
                            offset = (u) * 4 + (v) * stride;
                            //ColorBgra* srcPtr2 = src.GetPointAddressUnchecked(u, v);
                            rt = v_inData[offset];
                            gt = v_inData[offset + 1];
                            bt = v_inData[offset + 2];
                            at = v_inData[offset + 3];
                            sr += rt * at;
                            sg += gt * at;
                            sb += bt * at;
                            sa += at;
                            //sr += srcPtr2->R * srcPtr2->A;
                            //sg += srcPtr2->G * srcPtr2->A;
                            //sb += srcPtr2->B * srcPtr2->A;
                            //sa += srcPtr2->A;
                            ++sc;
                        }
                        if (sa != 0)
                        {
                            offset = (j * stride) + 4 * i;
                            bt = sb / sa;
                            rt = sr / sa;
                            gt = sg / sa;
                            at = sa / sc;
                            v_outData[offset] = (byte)((rt > 255) ? 255 : ((rt < 0) ? 0 : rt));
                            v_outData[offset + 1] = (byte)((gt > 255) ? 255 : ((gt < 0) ? 0 : gt));
                            v_outData[offset + 2] = (byte)((bt > 255) ? 255 : ((bt < 0) ? 0 : bt));
                            v_outData[offset + 3] =  (byte)((at > 255) ? 255 : ((at < 0) ? 0 : at));
                        }
                    }
                }
            } 
            // My implementation
            //int offset = 0;
            /*
            Vector2f dir = Vector2i.Zero;
            Vector2f center = new Vector2f(bmp.Width, bmp.Height); // CoreMathOperation.GetCenter(v_rc);
            int radius = 100;
            int v_w = 0;
            int v_h = 0;            
            int count = 0;
            int sa = 0;
            float v_factor = 0.0f;
            float L = 400;
            for (int j = 0; j < v_rc.Height; j++)
            {
                for (int x = 0; x < v_rc.Width; x++)
                {
                    dir = CoreMathOperation.GetDistanceP(new Vector2f(x, j), center );
                    dir.Normalize();
                    count = 0;
                    offset = j * stride + 4 * x;
                    rt = (int)(v_inData[offset]);
                    gt = (int)(v_inData[offset+1]);
                    bt = (int) (v_inData[offset+2]);
                    sa = 255;
                    count = 1;
                    for (int i = 1; i < radius ; i++)
                    {
                        v_w =(int)( x - (dir.X * i) );
                        v_h = (int)(j - (dir.Y * i));
                        if (v_w < 0) continue;
                        else if (v_w >= v_rc.Width) continue;
                        if (v_h < 0) continue;
                        else if (v_h >= v_rc.Height) continue;
                        offset = v_h * stride + 4 * v_w;
                        v_factor = ((radius - i) / (float)radius);
                        rt += (int)(v_inData[offset] * v_factor   );
                        gt += (int)(v_inData[offset + 1] *v_factor );
                        bt += (int)(v_inData[offset + 2] * v_factor);
                        sa += (int)(255 * ((radius - i)/ (float)radius));
                        count++;
                    }
                  //new color
                    rt = (int)(255 * rt / ((float)sa) );
                    gt =(int)( 255 *  gt / ((float)sa) );
                    bt =(int)(255 *  bt / ((float)sa) ) ;
                    at = 255 ;
                    offset = j * stride + 4 * x;
                    v_outData[offset] = (byte)((rt > 255) ? 255 : ((rt < 0) ? 0 : rt));
                    v_outData[offset + 1] = (byte)((gt > 255) ? 255 : ((gt < 0) ? 0 : gt));
                    v_outData[offset + 2] = (byte)((bt > 255) ? 255 : ((bt < 0) ? 0 : bt));
                    v_outData[offset + 3] = (byte)((at > 255) ? 255 : ((at < 0) ? 0 : at));
                }
            }
             * */
            //----//-
            //free//
            //----//-
            BitmapData v_outBMData = v_outBmp.LockBits(v_rc, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(v_outData, 0, v_outBMData.Scan0, v_outData.Length);
            v_outBmp.UnlockBits(v_outBMData);
            return v_outBmp;
        }
    }    
}

