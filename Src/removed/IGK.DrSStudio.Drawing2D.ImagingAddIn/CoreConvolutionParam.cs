

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreConvolutionParam.cs
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
file:CoreConvolutionParam.cs
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
﻿
/* 
-------------------------------------------------------------
This file is part of iGK-DEV-DrawingStudio
-------------------------------------------------------------
-------------------------------------------------------------
-------------------------------------------------------------
view license file in Documentation folder to get more info
Copyright (c) 2008-2010 
Author  : Charles A.D. BONDJE DOUE 
mail : bondje.doue@hotmail.com
-------------------------------------------------------------
-------------------------------------------------------------
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
namespace IGK.DrSStudio.Drawing2D
{
    /// <summary>
    /// represent the convolution parameter
    /// </summary>
    public sealed class CoreConvolutionParam
    {
        private float m_moyenne;
        private int[] iTab;
        private int[] jTab;
        private float[] m_data;
        private float offset;
        private int sizex = 3;
        private int sizey = 3;
        internal static readonly float Sqrt2 = (float)Math.Sqrt(2);
        //property
        public int Width { get { return sizex; } }
        public int Height { get { return sizey; } }
        public float[] Coefficients { get { return m_data; } }
        public static readonly CoreConvolutionParam Relief = new CoreConvolutionParam(2, new float[] {
                                  0, 0, 0,  0, 0 ,
                                  -20, -20, 1, -20, -20, 0, 0, 0, 0, 0, 20, 20, 1, 20, 20, 0, 0, 0, 0, 0 }, 5, 5);
        public static readonly CoreConvolutionParam FilterWhiteRhoner = new CoreConvolutionParam(1, new float[] {
                                  0, 0, 1,  0, 0 ,
                                  0, 0,  0,  0, 0 ,
                                  1, 0, -4,  0, 1 ,
                                  0, 0,  0,  0, 0 ,
                                  0, 0,  1,  0, 0 }, 5, 5);
        public static readonly CoreConvolutionParam FilterMarkHildreth = new CoreConvolutionParam(1, new float[] {            
                                  1/Sqrt2 , 1,  1/Sqrt2 ,
                                  1, -4, 1,
                                  1/Sqrt2 , 1,  1/Sqrt2 
                                   });
        public static readonly CoreConvolutionParam Emboss = new CoreConvolutionParam(1, new float[]{
            -2,-1,0,
            -1,1,1,
            0,1,2
            });
        //------------------------------------------------------------------------------------------------------------
        //Pass Bas Filter
        //------------------------------------------------------------------------------------------------------------
        //flou median smooth
        public static readonly CoreConvolutionParam PB_H1 = new CoreConvolutionParam(9, new float[]{
            1,1,1,
            1,1,1,
            1,1,1
            });
        //flou 
        public static readonly CoreConvolutionParam PB_H2 = new CoreConvolutionParam(10, new float[]{
            1,1,1,
            1,2,1,
            1,1,1
            });
        /// <summary>
        /// gaussian blur 
        /// </summary>
        public static readonly CoreConvolutionParam PB_H3 = new CoreConvolutionParam(16, new float[]{
            1,2,1,
            2,4,2,
            1,2,1
            });
        //------------------------------------------------------------------------------------------------------------
        //Pass Haut
        //------------------------------------------------------------------------------------------------------------
        //accenturation
        public static readonly CoreConvolutionParam PH_Sharpen = new CoreConvolutionParam(1, new float[]{
            0,-1,0,
            -1,5,-1,
            0,-1,0
            });
        //mean removal
        public static readonly CoreConvolutionParam PH_H5 = new CoreConvolutionParam(1, new float[]{
            -1,-1,-1,
            -1,9,-1,
            -1,-1,-1
            });
        public static readonly CoreConvolutionParam PH_H6 = new CoreConvolutionParam(1, new float[]{
            1,-2,1,
            -2,5,-2,
            1,-2,1
            });
        //convolution accentuation
        public static readonly CoreConvolutionParam H3_3 = new CoreConvolutionParam(4, new float[]{
                -1,-2,-1,
                -2,16,-2,
                -1,-2,-1});
        //------------------------------------------------------------------------------------------------------------
        //Laplace
        //------------------------------------------------------------------------------------------------------------
        public static readonly CoreConvolutionParam LP_H7 = new CoreConvolutionParam(1, new float[]{
            0,-1,0,
            -1,4,-1,
            0,-1,0
            });
        public static readonly CoreConvolutionParam LP_H8 = new CoreConvolutionParam(1, new float[]{
            -1,-1,-1,
            -1,8,-1,
            -1,-1,-1
            });
        public static readonly CoreConvolutionParam LP_H9 = new CoreConvolutionParam(1, new float[]{
            1,-2,1,
            -2,4,-2,
            1,-2,1
            });
        //------------------------------------------------------------------------------------------------------------
        //Segment
        //------------------------------------------------------------------------------------------------------------
        public static readonly CoreConvolutionParam SM_H10 = new CoreConvolutionParam(1, new float[]{
            -1,2,1,
            -1,2,1,
            -1,2,1
            });
        public static readonly CoreConvolutionParam SM_H11 = new CoreConvolutionParam(1, new float[]{
            -1,-1,-1,
            2,2,2,
            -1,-1,-1
            });
        public static readonly CoreConvolutionParam SM_H12 = new CoreConvolutionParam(1, new float[]{
            2,-1,-1,
            -1,2,-1,
            -1,-1,2
            });
        public static readonly CoreConvolutionParam SM_H13 = new CoreConvolutionParam(1, new float[]{
            -1,-1,2,
            -1,2,-1,
            2,-1,-1
            });
        //--------------------------------------------------------------------------------------------------------------
        //Norme
        //--------------------------------------------------------------------------------------------------------------
        //Prewit
        public static readonly CoreConvolutionParam NORM_H14 = new CoreConvolutionParam(1, new float[]{
            1,0,-1,
            1,0,-1,
            1,0,-1
            }, 127);
        public static readonly CoreConvolutionParam NORM_H15 = new CoreConvolutionParam(1, new float[]{
            1,1,1,
            0,0,0,
            -1,-1,-1
            }, 127);
        //sobel
        public static readonly CoreConvolutionParam SOBEL_H16 = new CoreConvolutionParam(1, new float[]{
            1,0,-1,
            2,0, 2,
            1,0,-1
            });
        public static readonly CoreConvolutionParam SOBEL_H17 = new CoreConvolutionParam(1, new float[]{
            1,2,1,
            0,0,0,
            -1,-2,-1
            });
        //operateur de chen et frei
        public static readonly CoreConvolutionParam CHENFREI_H18 = new CoreConvolutionParam(1, new float[]{
            1,0,-1,
            Sqrt2,0,-Sqrt2,
            1,0,-1
            });
        public static readonly CoreConvolutionParam CHENFREI_H19 = new CoreConvolutionParam(1, new float[]{
            1,Sqrt2 ,1,
            0,0,0,
            -1,-Sqrt2 ,-1
            });
        public static readonly CoreConvolutionParam CHENFREI_H20 = new CoreConvolutionParam(1, new float[]{
            Sqrt2 ,-1,0,
            -1,0,1,
            0,1,-Sqrt2
            });
        public static readonly CoreConvolutionParam CHENFREI_H21 = new CoreConvolutionParam(1, new float[]{
            0,-1,Sqrt2,
            1,0,-1,
            -Sqrt2 ,1,0
            });
        public static readonly CoreConvolutionParam CHENFREI_H22 = new CoreConvolutionParam(1, new float[]{
            0,1,0,
            -1,0,-1,
            0,1,0
            });
        public static readonly CoreConvolutionParam CHENFREI_H23 = new CoreConvolutionParam(1, new float[]{
            -1,0,1,
            0,0,0,
            1,0,-1
            });
        public static readonly CoreConvolutionParam CHENFREI_H24 = new CoreConvolutionParam(1, new float[]{
            1,-2,1,
            -2,4,-2,
            1,-2,1
            });
        public static readonly CoreConvolutionParam CHENFREI_H25 = new CoreConvolutionParam(1, new float[]{
            -2,1,-2,
            1,4,1,
            -2,1,-2
            });
        //operateur de Kirsch : masque de kirsh
        public static readonly CoreConvolutionParam OPKIRSH_H26 = new CoreConvolutionParam(1, new float[]{
             5, 5, 5,
            -3, 0,-3,
            -3,-3,-3
            });
        public static readonly CoreConvolutionParam OPKIRSH_H261 = new CoreConvolutionParam(1, new float[]{
             -3, 5, 5,
            -3, 0, 5,
            -3,-3,-3
            });
        public static readonly CoreConvolutionParam OPKIRSH_H262 = new CoreConvolutionParam(1, new float[]{             
             -3, -3,5,
             -3, 0, 5,
             -3, -3,5
            });
        public static readonly CoreConvolutionParam OPKIRSH_H263 = new CoreConvolutionParam(1, new float[]{
             -3, -3, -3,
            -3, 0, 5,
            -3,5,5
            });
        public static readonly CoreConvolutionParam OPKIRSH_H264 = new CoreConvolutionParam(1, new float[]{             
             -3, -3,-3,
             -3, 0,-3,
              5, 5,5
            });
        public static readonly CoreConvolutionParam OPKIRSH_H265 = new CoreConvolutionParam(1, new float[]{
             -3, -3, -3,
             5, 0,-3,
             5,5,-3
            });
        public static readonly CoreConvolutionParam OPKIRSH_H266 = new CoreConvolutionParam(1, new float[]{
             5, -3, -3,
             5, 0,-3,
             5,-3,-3
            });
        public static readonly CoreConvolutionParam OPKIRSH_H267 = new CoreConvolutionParam(1, new float[]{
             5, 5, -3,
             5, 0,-3,
             -3,-3,-3
            });
        //operateur : masque de boussole directionnelle 
        public static readonly CoreConvolutionParam BSDIR_H270 = new CoreConvolutionParam(1, new float[]{
             1,1,1,
             1,-2,1,
             -1,-1,-1
            });
        public static readonly CoreConvolutionParam BSDIR_H271 = new CoreConvolutionParam(1, new float[]{
             1,1,1,
             -1,-2,1,
             -1,-1,1
            });
        public static readonly CoreConvolutionParam BSDIR_H272 = new CoreConvolutionParam(1, new float[]{
             -1,1,1,
             -1,-2,1,
             -1,1,1
            });
        public static readonly CoreConvolutionParam BSDIR_H273 = new CoreConvolutionParam(1, new float[]{
             -1,-1,1,
             -1,-2,1,
             1,1,1
            });
        public static readonly CoreConvolutionParam BSDIR_H274 = new CoreConvolutionParam(1, new float[]{
             -1,-1,-1,
             1,-2,1,
             1,1,1
            });
        public static readonly CoreConvolutionParam BSDIR_H275 = new CoreConvolutionParam(1, new float[]{
             1,-1,-1,
             1,-2,-1,
             1,1,1
            });
        public static readonly CoreConvolutionParam BSDIR_H276 = new CoreConvolutionParam(1, new float[]{
             1,1,-1,
             1,-2,-1,
             1,1,-1
            });
        public static readonly CoreConvolutionParam BSDIR_H277 = new CoreConvolutionParam(1, new float[]{
             1,1,1,
             1,-2,-1,
             1,-1,-1
            });
        //--------------------------------------------------------------------------------------------------------------
        //Other Convolotion 
        //--------------------------------------------------------------------------------------------------------------
        public static readonly CoreConvolutionParam Sharpen = new CoreConvolutionParam(3, new float[]{
           0,-2,0,
            -2,11,-2,
            0,-2,0
            });
        //quick edge dection . H15
        public static readonly CoreConvolutionParam QuickEdgeDetection = new CoreConvolutionParam(1, new float[]{
           1,1,1,
            0,0,0,
            -1,-1,-1            
            }, 127);
        public static readonly CoreConvolutionParam LaplaceEmbossing = new CoreConvolutionParam(1, new float[]{
            -1,0,-1,
             0,4,0,
            -1,0,-1
            }, 127);
        /// <summary>
        /// obtient taille de convolution
        /// </summary>
        public int Size
        {
            get
            {
                return m_data.Length;
            }
        }
        public CoreConvolutionParam(float moyenne, float[] coffecients)
        {
            int cou = (int)Math.Sqrt(coffecients.Length);
            if (cou == 0)
                throw new Exception("invalid convolution");
            if (moyenne == 0)
                moyenne = 1;
            initConvolution(cou, moyenne, coffecients, 0);
        }
        public CoreConvolutionParam(float moyenne, float[] coffecients, int sizex, int sizey)
        {
            int cou = (int)Math.Sqrt(coffecients.Length);
            if (cou == 0)
                throw new Exception("invalid convolution");
            if (moyenne == 0)
                moyenne = 1;
            this.sizex = sizex;
            this.sizey = sizey;
            initConvolution(cou, moyenne, coffecients, 0);
        }
        public CoreConvolutionParam(float moyenne, float[] coffecients, int offset)
        {
            int cou = (int)Math.Sqrt(coffecients.Length);
            if (cou == 0)
                throw new Exception("invalid convolution");
            if (moyenne == 0)
                moyenne = 1;
            initConvolution(cou, moyenne, coffecients, offset);
        }
        private void initConvolution(int cou, float moyenne, float[] coffecients, int p)
        {
            this.iTab = new int[coffecients.Length];
            this.jTab = new int[coffecients.Length];
            int init = -((cou - 1) / 2);
            int t = init;
            int h = init;
            int l = cou - 1;
            int m = cou - 1;
            for (int i = 0; i < coffecients.Length; i++)
            {
                this.jTab[i] = t;
                if (l == 0)
                {
                    t++;
                    l = cou - 1;
                }
                else
                    l--;
                this.iTab[i] = h;
                if (m == 0)
                {
                    h = init;
                    m = cou - 1;
                }
                else
                {
                    h++;
                    m--;
                }
            }
            this.m_moyenne = moyenne;
            this.offset = p;
            this.m_data = (float[])coffecients.Clone();
        }
        public override string ToString()
        {
            return string.Format("{0}x{1}", this.sizex, sizey) + " m:" + m_moyenne + "  c:" + m_data.Length;
        }
        public Bitmap ApplyConvolution(Bitmap bmp, CoreConvolutionCallBack callback)
        {           
            DateTime d = DateTime.Now;
            Bitmap v_bmp = ApplySConvolution(bmp ,callback );
            TimeSpan s = DateTime.Now - d;         
            return v_bmp;
        }
        public static void ApplyConvolution(ref Bitmap bmp, CoreConvolutionParam param, CoreConvolutionCallBack callback)
        {
            Bitmap vbmp = param.ApplyConvolution(bmp,callback );
            bmp.Dispose();
            bmp = vbmp;
        }
        private Bitmap ApplySConvolution(Bitmap bmp, CoreConvolutionCallBack callback)
        {
            Bitmap vBmp = bmp.Clone() as Bitmap;
            Rectangle rc = new Rectangle(Point.Empty, bmp.Size);
            //get data in array
            BitmapData dataSrc = bmp.LockBits(rc, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            BitmapData dataDest = vBmp.LockBits(rc, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            byte[] dtSrc = new byte[dataSrc.Stride * dataSrc.Height];
            byte[] dtDest = new byte[dataDest.Stride * dataDest.Height];
            //copy data
            Marshal.Copy(dataSrc.Scan0, dtSrc, 0, dtSrc.Length);
            Marshal.Copy(dataDest.Scan0, dtDest, 0, dtDest.Length);
            int voffset = 0;
            float r, g, b, a;
            int h = vBmp.Height;
            int w = vBmp.Width;
            int ih = 0;
            int iw = 0;
            int index = 0;
            for (int i = 0; i < h; ++i)
            {
                for (int j = 0; j < w; ++j)
                {
                    voffset = i * dataSrc.Stride + j * 4;
                    a = dtSrc[voffset + 3];
                    r = 0;
                    g = 0;
                    b = 0;
                    for (int k = 0; k < m_data.Length; ++k)
                    {
                        ih = i + jTab[k];
                        iw = j + iTab[k];
                        if (((ih < 0) || (ih >= h)) || ((iw < 0) || (iw >= w)))
                        {
                            //r += 127 / m_data.Length;
                            //g += 127 / m_data.Length;
                            //b += 127 / m_data.Length;
                            continue;
                        }
                        index = ih * dataSrc.Stride + iw * 4;
                        r += dtSrc[index] * m_data[k];
                        g += dtSrc[index + 1] * m_data[k];
                        b += dtSrc[index + 2] * m_data[k];
                    }
                    r = offset + r / m_moyenne;
                    g = offset + g / m_moyenne;
                    b = offset + b / m_moyenne;
                    dtDest[voffset] = (byte)((r < 0) ? 0 : (r > 255) ? 255 : r);
                    dtDest[voffset + 1] = (byte)((g < 0) ? 0 : (g > 255) ? 255 : g);
                    dtDest[voffset + 2] = (byte)((b < 0) ? 0 : (b > 255) ? 255 : b);
                    dtDest[voffset + 3] = (byte)((a < 0) ? 0 : (a > 255) ? 255 : a);
                }
                if (callback!=null)
                {
                    callback(i / (float)h); 
                }
            }
            //write destination
            Marshal.Copy(dtDest, 0, dataDest.Scan0, dtDest.Length);
            //free data
            bmp.UnlockBits(dataSrc);
            vBmp.UnlockBits(dataDest);
            return vBmp;
        }
        //private Bitmap Conv3x3(Bitmap bmp)
        //{
        //    //create a little bigget bitmap
        //    Bitmap bSrc = new Bitmap(bmp.Width + 2, bmp.Height + 2, PixelFormat.Format32bppPArgb);
        //    bSrc.SetResolution(bmp.HorizontalResolution, bmp.VerticalResolution);
        //    Graphics gr = Graphics.FromImage(bSrc);
        //    gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
        //    gr.InterpolationMode = InterpolationMode.NearestNeighbor;
        //    gr.SmoothingMode = SmoothingMode.AntiAlias;
        //    gr.DrawImage(bmp, new Point(1, 1));
        //    gr.Dispose();
        //    //Bitmap bSrc = (Bitmap)bmp.Clone();
        //    BitmapData bmData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppPArgb);
        //    BitmapData bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppPArgb);
        //    int strideSrc = bmSrc.Stride;
        //    int stride = strideSrc;// bmData.Stride;
        //    int stride2 = strideSrc * 2;//stride * 2;
        //    int outstride = bmData.Stride;
        //    System.IntPtr Scan0 = bmData.Scan0;
        //    System.IntPtr SrcScan0 = bmSrc.Scan0;
        //    unsafe
        //    {
        //        byte* p = (byte*)(void*)Scan0;
        //        byte* pSrc = (byte*)(void*)SrcScan0;
        //        IntPtr h = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(float)) * this.Size);
        //        Marshal.Copy(this.m_data, 0, h, this.m_data.Length);
        //        float* vcoff = (float*)h;
        //        int nOffset = -bmp.Width * 4;// outstride -
        //        int srcOffset = -bSrc.Width * 4;//strideSrc 
        //        int nWidth = bmp.Width;
        //        int nHeight = bmp.Height;// -2;
        //        float r = 0;
        //        float g = 0;
        //        float b = 0;
        //        //float a = 0;
        //        for (int y = 0; y < nHeight; ++y)
        //        {
        //            for (int x = 0; x < nWidth; ++x)
        //            {
        //                r = 0;
        //                g = 0;
        //                b = 0;
        //                r = (((
        //                    //top line
        //                    (pSrc[0] * vcoff[0]) + (pSrc[4] * vcoff[1]) + (pSrc[8] * vcoff[2]) +
        //                    //current line
        //                    (pSrc[stride] * vcoff[3]) + (pSrc[4 + stride] * vcoff[4]) + (pSrc[8 + stride] * vcoff[5]) +
        //                    //botoom line
        //                    (pSrc[stride2] * vcoff[6]) + (pSrc[4 + stride2] * vcoff[7]) + (pSrc[8 + stride2] * vcoff[7]))
        //                    / this.m_moyenne) + this.offset);
        //                g = (((
        //                    (pSrc[1] * vcoff[0]) + (pSrc[5] * vcoff[1]) + (pSrc[9] * vcoff[2]) +
        //                    (pSrc[1 + stride] * vcoff[3]) + (pSrc[5 + stride] * vcoff[4]) + (pSrc[9 + stride] * vcoff[5]) +
        //                    (pSrc[1 + stride2] * vcoff[6]) + (pSrc[5 + stride2] * vcoff[7]) + (pSrc[9 + stride2] * vcoff[7]))
        //                    / this.m_moyenne) + this.offset);
        //                b = (((
        //                    (pSrc[2] * vcoff[0]) + (pSrc[6] * vcoff[1]) + (pSrc[10] * vcoff[2]) +
        //                    (pSrc[2 + stride] * vcoff[3]) + (pSrc[6 + stride] * vcoff[4]) + (pSrc[10 + stride] * vcoff[5]) +
        //                    (pSrc[2 + stride2] * vcoff[6]) + (pSrc[6 + stride2] * vcoff[7]) + (pSrc[10 + stride2] * vcoff[7]))
        //                    / this.m_moyenne) + this.offset);
        //                if (r < 0) r = 0;
        //                if (r > 255) r = 255;
        //                if (g < 0) g = 0;
        //                if (g > 255) g = 255;
        //                if (b < 0) b = 0;
        //                if (b > 255) b = 255;
        //                //a
        //                p[3] = pSrc[7 + stride];
        //                p[2] = (byte)b;
        //                p[1] = (byte)g;
        //                p[0] = (byte)r;
        //                p += 4;
        //                pSrc += 4;
        //            }
        //            p += nOffset + outstride;
        //            pSrc += nOffset + stride;
        //        }
        //        Marshal.FreeCoTaskMem(h);
        //    }
        //    bmp.UnlockBits(bmData);
        //    bSrc.UnlockBits(bmSrc);
        //    bSrc.Dispose();
        //    return new Bitmap(bmp);
        //}    
    }
}

