

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinCoreBitmapData.cs
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
file:WinCoreBitmapData.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
CoreApplicationManager.Instance : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace IGK.ICore.WinCore
{
    using IGK.ICore.Drawing2D;
    using IGK.ICore;

    /// <summary>
    /// Represent the core bitmap representation
    /// </summary>
    public sealed class WinCoreBitmapData
    {
        byte[] data;
        long stride;
        int width;
        int height;
        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public long Stride { get { return stride; } }
        public byte[] Data { get { return data; } }
        private WinCoreBitmapData()
        { }
        public static WinCoreBitmapData FromBitmap(ICoreBitmap Bitmap)
        {
            if (Bitmap == null)
                return null;
            Bitmap cp = Bitmap.ToGdiBitmap();
            if (cp != null)
            {
                WinCoreBitmapData d = FromBitmap(cp);
                cp.Dispose();
                return d;
            }
            return null;
        }
        /// <summary>
        /// create a corebitmap data from gdi+ bitmap
        /// </summary>
        /// <param name="Bitmap">Bitmap to convert to WinCoreBitmapData</param>
        /// <exception cref="OutOfMemoryException">if ex</exception>
        /// <returns></returns>
        public static WinCoreBitmapData FromBitmap(Bitmap Bitmap)
        {
            if (Bitmap.PixelFormat == PixelFormat.Undefined)
                return null;
            WinCoreBitmapData d = new WinCoreBitmapData();
            BitmapData dt =
                Bitmap.LockBits(new Rectangle(0, 0, Bitmap.Width, Bitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly,
                PixelFormat.Format32bppPArgb);
            d.data = new byte[dt.Stride * dt.Height];
            d.stride = dt.Stride;
            d.width = Bitmap.Width;
            d.height = Bitmap.Height;
            Marshal.Copy(dt.Scan0, d.data, 0, d.data.Length);
            Bitmap.UnlockBits(dt);
            return d;
        }
        public Bitmap ToBitmap()
        {
            Bitmap rbmp = new Bitmap(this.Width, this.Height, PixelFormat.Format32bppPArgb);
            Rectangle vrect = new Rectangle(0, 0, this.Width, this.Height);
            BitmapData data = rbmp.LockBits(vrect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Byte[] d = Data;
            Marshal.Copy(d, 0, data.Scan0, this.Data.Length);
            rbmp.UnlockBits(data);
            return rbmp;
        }
        public Color ReadPixel(int w, int h)
        {
            if ((w < 0) || (w >= this.Width) || (h < 0) || (h >= this.Height))
            {
                return Color.FromArgb(255, 255, 255, 255);
            }
            //not a valid width
            int offset = (int)((h * this.Stride) + (w * 4));
            Color cl = Color.FromArgb(this.data[offset + 3],
                this.data[offset+ 2],
                this.data[offset+ 1],
                this.data[offset]);
            return cl;
        }
        public bool WritePixel(Colorf cl, int w, int h)
        {
            if ((w < 0) || (w >= this.Width) || (h < 0) || (h >= this.Height))
            {
                return false;
            }
            //not a valid width
            int offset = (int)((h * this.Stride) + (w * 4));
            
            this.data[offset + 3] = (cl.A * 255).TrimByte();
            this.data[offset] =( 255 * cl.B).TrimByte();
            this.data[offset + 1] =( 255 *cl.G).TrimByte();
            this.data[offset + 2] = (255 * cl.R).TrimByte();
            return true;
        }
        public bool WritePixel(Color cl, int w, int h)
        {
            if ((w < 0) || (w >= this.Width) || (h < 0) || (h >= this.Height))
            {
                return false;
            }
            //not a valid width
            int offset = (int)((h * this.Stride) + (w * 4));

            this.data[offset + 3] = cl.A;
            this.data[offset] =  cl.B;
            this.data[offset + 1] = cl.G;
            this.data[offset + 2] = cl.R;
            return true;
        }
        /// <summary>
        /// get a clone CoreBitmap Data
        /// </summary>
        /// <returns></returns>
        public WinCoreBitmapData Clone()
        {
            WinCoreBitmapData data = new WinCoreBitmapData();
            data.data = (byte[])this.data.Clone();
            data.height = this.height;
            data.width = this.width;
            data.stride = this.stride;
            return data;
        }
        public static WinCoreBitmapData operator +(WinCoreBitmapData data1, WinCoreBitmapData data2)
        {
            if ((data1.Width != data2.Width) || (data1.Height != data2.Height))
                return null;
            WinCoreBitmapData data3 = data1.Clone();
            int v = 0;
            for (int i = 0; i < data1.data.Length; i++)
            {
                if ((i != 0) && ((i + 1) % 4 == 0))
                    continue;
                v =
                data3.data[i] = (data1.data[i] + data2.data[i]).TrimByte ();
            }
            return data3;
        }
        public static WinCoreBitmapData operator -(WinCoreBitmapData data1, WinCoreBitmapData data2)
        {
            if ((data1.Width != data2.Width) || (data1.Height != data2.Height)) return null;
            WinCoreBitmapData data3 = data1.Clone();
            for (int i = 0; i < data1.data.Length; i++)
            {
                if ((i != 0) && ((i + 1) % 4 == 0))
                    continue;
                data3.data[i] = (data1.data[i] - data2.data[i]).TrimByte();
            }
            return data3;
        }
        public static WinCoreBitmapData operator *(WinCoreBitmapData data1, float factor)
        {
            WinCoreBitmapData data3 = data1.Clone();
            for (int i = 0; i < data1.data.Length; i++)
            {
                if ((i != 0) && ((i + 1) % 4 == 0))
                    continue;
                data3.data[i] = ((int)(factor * data1.data[i])).TrimByte();
            }
            return data3;
        }
        public static WinCoreBitmapData operator *(float factor, WinCoreBitmapData data1)
        {
            return data1 * factor;
        }
        public static WinCoreBitmapData operator /(WinCoreBitmapData data1, float factor)
        {
            if (factor == 0)
                return null;
            WinCoreBitmapData data3 = data1.Clone();
            int v = 0;
            for (int i = 0; i < data1.data.Length; i++)
            {
                if ((i != 0) && ((i + 1) % 4 == 0))
                    continue;
                v = (int)(data1.data[i] / factor);
                v = ((v < 0) ? 0 : (v > 255) ? 255 : v);
                data3.data[i] = (byte)v;
            }
            return data3;
        }
        /// <summary>
        /// write value to spécified offset
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        public bool WritePixel(int x, int y, int r, int g, int b)
        {
            if ((x < 0) || (x >= this.Width) || (y < 0) || (y >= Height))
                return false;
            int offset = (int)((y * this.Stride) + (x * 4));
            data[offset] = (byte)((r < 0) ? 0 : (r > 255) ? 255 : r);
            data[offset + 1] = (byte)((g < 0) ? 0 : (g > 255) ? 255 : g);
            data[offset + 2] = (byte)((b < 0) ? 0 : (b > 255) ? 255 : b);
            return true;
        }
        /// <summary>
        /// generate a core bitmap data 
        /// </summary>
        /// <param name="w">width</param>
        /// <param name="h">height</param>
        /// <param name="data">data</param>
        public static WinCoreBitmapData FromData(int w, int h, byte[] data)
        {
            WinCoreBitmapData vdata = new WinCoreBitmapData();
            vdata.data = data;
            vdata.width = w;
            vdata.height = h;
            vdata.stride = w * 4;
            return vdata;
        }
        internal static Bitmap FromDataFile(int w, int h, string file)
        {
            byte[] d = File.ReadAllBytes(file);
            Bitmap bmp = new Bitmap(w, h);
            BitmapData vdata = bmp.LockBits(new Rectangle(Point.Empty, bmp.Size), System.Drawing.Imaging.ImageLockMode.ReadWrite, 
                PixelFormat.Format32bppPArgb);
            Marshal.Copy(d, 0, vdata.Scan0, d.Length);
            bmp.UnlockBits(vdata);
            return bmp;
        }
        /// <summary>
        /// save bitmap to core bitmap data file
        /// </summary>
        /// <param name="_filename"></param>
        /// <param name="image"></param>
        public static void SaveData(string filename, ICoreBitmap  image)
        {
            if (string.IsNullOrEmpty(filename))
                throw new CoreException(enuExceptionType.ArgumentIsNull, "filename");
            if ((image == null) && (image.PixelFormat == enuPixelFormat.Undefined))
                throw new CoreException(enuExceptionType.ArgumentIsNull, "image");
            Stream f = File.Create(filename);
            SaveData(f, image);
            f.Close();
        }
        public static bool SaveData(Stream stream, ICoreBitmap image)
        {
            if ((image == null) && (image.PixelFormat == enuPixelFormat.Undefined))
                throw new CoreException(enuExceptionType.ArgumentIsNull, "image");
            bool v_result = false;
            try
            {
                Bitmap bmp = (Bitmap)image.ToGdiBitmap();
                BitmapData data = bmp.LockBits(new Rectangle(Point.Empty, bmp.Size), 
                    ImageLockMode.ReadWrite, 
                    PixelFormat.Format32bppPArgb);
                byte[] d = new byte[data.Height * data.Stride];
                BinaryWriter binWriter = new BinaryWriter(stream);
                //write with and height
                binWriter.Write((int)bmp.Width);
                binWriter.Write((int)bmp.Height);
                //read data 
                Marshal.Copy(data.Scan0, d, 0, d.Length);
                binWriter.Write(d, 0, d.Length);
                bmp.UnlockBits(data);
                //realease data file
                binWriter.Flush();
                v_result = true;
            }
            catch
            {
                //stome time out of memory
            }
            return v_result;
        }
        /// <summary>
        /// core bitmap read data
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static ICoreBitmap ReadData(string filename)
        {
            if (File.Exists(filename) == false)
                throw new CoreException(enuExceptionType.FileNotFound, filename);
            Bitmap v_obmp = null;
            Stream f = File.Open(filename, FileMode.Open);
            BinaryReader binR = new BinaryReader(f);
            //read width
            int w = binR.ReadInt32();
            int h = binR.ReadInt32();
            if ((w <= 0) || (h <= 0))
                return null;
            v_obmp = new Bitmap(w, h);
            BitmapData v_data = v_obmp.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, PixelFormat.Format32bppPArgb);
            byte[] d = new byte[v_data.Height * v_data.Stride];
            //read data 
            binR.Read(d, 0, d.Length);
            //unlock data
            binR.Close();
            Marshal.Copy(d, 0, v_data.Scan0, d.Length);
            //unlock data
            v_obmp.UnlockBits(v_data);
            WinCoreBitmap bmp = WinCoreBitmap.Create(v_obmp);

            return bmp;
        }
        /// <summary>
        /// transpose the data byte
        /// </summary>
        public void Transpose()
        {
            long offsetx = 0;
            long offsety = 0;
            for (int y = 0; y < this.height; y++)
            {
                for (int x = 0; x < this.width; x++)
                {
                    offsetx = (y * this.stride) + (x * 4);
                    offsety = (this.height - y - 1) * this.stride + ((this.width - x - 1) * 4);
                    this.Data[offsetx] = this.Data[offsety];
                    this.Data[offsetx + 1] = this.Data[offsety + 1];
                    this.Data[offsetx + 2] = this.Data[offsety + 2];
                    this.Data[offsetx + 3] = this.Data[offsety + 3];
                }
            }
        }
    }
}

