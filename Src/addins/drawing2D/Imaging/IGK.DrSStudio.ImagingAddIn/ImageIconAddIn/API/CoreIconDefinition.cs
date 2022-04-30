

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreIconDefinition.cs
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
file:CoreIconDefinition.cs
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
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
namespace IGK.DrSStudio.XIcon
{
    public enum enuIconType : short
    {
        Icon = 1,
        Cursor = 2
    }
    public enum enuIconColor
    {
        /// <summary>
        /// 1 bits : 2 colors
        /// </summary>
        bpp1 = 1,
        /// <summary>
        /// 4 bits : 16 colors
        /// </summary>
        bpp4 = 4,        
        /// <summary>
        /// 8 bits : 256 colors
        /// </summary>
        bpp8 = 8,
        /// <summary>
        /// 24 bits
        /// </summary>
        bpp24 = 24,
        /// <summary>
        /// 32 bits
        /// </summary>
        bpp32 = 32,
        /// <summary>
        /// 24 bits : 
        /// </summary>
        trueColorRGB = -1, 
        /// <summary>
        /// 32 bits
        /// </summary>
        trueColorRGBA = -2
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct IconStruct
    {
        private WinSafeApi.IconHeader header;
        private WinSafeApi.IconEntity[] entity;
        private byte[][] data;
        internal WinSafeApi.IconHeader Header { get { return header; } }
        internal WinSafeApi.IconEntity[] Entity { get { return entity; } }
        internal Byte[][] Data { get { return data; } }
        internal Color[] GetPalette(int index)
        {
            int bpp = entity[index].bitperPixel;
            int cl = entity[index].colors;
            if ((index < 0)||(index>= this.Count ))
                return new Color[0];
            if ((bpp == 0)||(bpp == 32)||(bpp==24) || (bpp==16))
            {
                //CORRECT bpp 16
                return new Color[0];
            }
            if ( cl ==0)
            {
                cl = (int)Math.Pow(2, bpp);
            }
            Color[] t = new Color [cl];
            int offset = 40;
            var d = data[index];
            for (int i = 0; i < t.Length; ++i)
            {
                t[i] = Color.FromArgb(
                        d[offset+3],
                        d[offset+2],
                        d[offset+1],
                        d[offset+0]
                    );
                offset += 4;
            }
            return t;
        }
        //get number of icon
        public int Count
        {
            get
            {
                return header.count;
            }
        }
        //get type of icon 1 for icon 2 for cursor
        public int Type
        {
            get
            {
                return header.type;
            }
            internal set
            {
                WinSafeApi.IconHeader h = this.header;
                h.type = (short)value;
                this.header = h;
            }
        }
        public int Width
        {
            get
            {
                if (this.Count == 1)
                    return this.entity[0].width;
                return -1;
            }
        }
        public int Height
        {
            get
            {
                if (this.Count == 1)
                    return this.entity[0].height;
                return -1;
            }
        }
        public int Bitcount
        {
            get
            {
                if (this.Count == 1)
                    return this.entity[0].bitperPixel;
                return -1;
            }
        }
        public static IconStruct Empty;
        //.ctr static
        static IconStruct()
        {
            Empty = new IconStruct();
            WinSafeApi.IconHeader h = new WinSafeApi.IconHeader();
            h.count = 0;
            h.reserved = 0;
            h.type = 1;
            Empty.header = h;
            Empty.entity = new WinSafeApi.IconEntity[0];
            Empty.data = new byte[0][];
        }
        //.ctr
        internal IconStruct(byte width, byte heigth, short plane, byte[] data)
        {
            this.header = new WinSafeApi.IconHeader();
            this.header.type = 1;
            this.header.reserved = 0;
            this.header.count = 1;
            this.entity = new WinSafeApi.IconEntity[1];
            this.entity[0].width = width;
            this.entity[0].height = heigth;
            this.entity[0].colors = 0;
            this.entity[0].reserved = 0;
            this.entity[0].plane = plane;
            this.entity[0].bitperPixel = 32;
            this.entity[0].imageSize = data.Length;
            this.entity[0].offset = 22;
            this.data = new byte[1][];
            this.data[0] = data;
            IntPtr alloc = Marshal.AllocCoTaskMem(40);
            Marshal.Copy(data, 0, alloc, 40);
            CoreBmpHeaderInfo bb = (CoreBmpHeaderInfo)Marshal.PtrToStructure(alloc, typeof(CoreBmpHeaderInfo));
            if (bb.size == 40)
            {
                //bitmap file
                bb.height *= 2;
                Marshal.StructureToPtr(bb, alloc, true);
                Marshal.Copy(alloc, this.data[0], 0, bb.size);
            }
            Marshal.FreeCoTaskMem(alloc);
        }
        //indexer
        public IconStruct this[int index]
        {
            get
            {
                if ((index < 0) || (index > this.Count) || (this.Count == 0))
                    return Empty;
                return GetIcon(index);
            }
        }
        //add ico
        public void AddIcon(IconStruct ico)
        {
            if (ico.Count <= 0)
                return;
            int count = this.Count + ico.Count;
            //get start offset
            int offset = 6 + 16 * (count);
            WinSafeApi.IconEntity[] tab = new WinSafeApi.IconEntity[count];
            this.entity.CopyTo(tab, 0);
            ico.entity.CopyTo(tab, count-ico.Count);
            //get new cell
            for (int i = 0; i < tab.Length; i++)
            {
                tab[i].offset = offset;
                offset += tab[i].imageSize;
            }
            //merge data
            byte[][] tab1 = new byte[count][];
            this.data.CopyTo(tab1, 0);
            ico.data.CopyTo(tab1, count-ico.Count);
            this.data = tab1;
            this.header.count = (short)count;
            this.entity = tab;
        }
        public void AddIconImage(Bitmap bmp)
        {
            IconStruct ico = IconStruct.FromBitmap(bmp);
            AddIcon(ico);
        }
        //internal void AddIcon(IconStruct ico, Bitmap newBitmap)
        //{
        //    IconStruct copyico = IconStruct.FromBitmap(newBitmap);
        //    if (copyico.Width != ico.Width )return;
        //    if (copyico.Height != ico.Height )return;
        //    copyico.Data;            
        //    AddIcon(ico);
        //}
        public void RemoveIcon(int index)
        {
            if ((index < 0) && (index > Count))
                return;
        }
        internal static IconStruct OpenFromStream(Stream stream)
        {
            BinaryReader binR = new BinaryReader(stream);
            bool ok = false;
            WinSafeApi.IconHeader h = new WinSafeApi.IconHeader();
            WinSafeApi.IconEntity[] t = null;
            byte[][] d = null;
            //Read Header
            try
            {
                h.reserved = binR.ReadInt16();
                h.type = binR.ReadInt16();
                h.count = binR.ReadInt16();
                t = new WinSafeApi.IconEntity[h.count];
                d = new byte[h.count][];
                for (int i = 0; i < h.count; i++)
                {
                    t[i] = new WinSafeApi.IconEntity();
                    t[i].width = binR.ReadByte();
                    t[i].height = binR.ReadByte();
                    t[i].colors = binR.ReadByte();
                    t[i].reserved = binR.ReadByte();
                    t[i].plane = binR.ReadInt16();
                    t[i].bitperPixel = binR.ReadInt16();
                    t[i].imageSize = binR.ReadInt32();
                    t[i].offset = binR.ReadInt32();
                }
                for (int i = 0; i < h.count; i++)
                {
                    d[i] = new byte[t[i].imageSize];
                    binR.Read(d[i], 0, d[i].Length);
                }
                ok = true;
            }
            catch
            {
            }
            finally
            {
                binR.Close();
            }
            if (ok)
            {
                IconStruct ii = new IconStruct();
                ii.header = h;
                ii.entity = t;
                ii.data = d;
                return ii;
            }
            return Empty;
        }
        //get icon struc 
        public static IconStruct OpenFromFile(string fileName)
        {
            if (!File.Exists(fileName))
                return Empty;
            FileStream fs = File.Open(fileName, FileMode.Open, FileAccess.Read);
            IconStruct cr = OpenFromStream(fs);
            fs.Close ();
            return cr;
        }
        public void Save(Stream stream)
        {
            //init to start stream
            stream.Seek(0, SeekOrigin.Begin);
            BinaryWriter binW = new BinaryWriter(stream);
            binW.Write((short)header.reserved);
            binW.Write((short)header.type);
            binW.Write((short)header.count);
            for (int i = 0; i < header.count; i++)
            {
                binW.Write(this.entity[i].width);
                binW.Write(this.entity[i].height);
                binW.Write(this.entity[i].colors);
                binW.Write(this.entity[i].reserved);
                binW.Write(this.entity[i].plane);
                binW.Write(this.entity[i].bitperPixel);
                binW.Write(this.entity[i].imageSize);
                binW.Write(this.entity[i].offset);
            }
            for (int i = 0; i < header.count; i++)
            {
                binW.Write(data[i], 0, data[i].Length);
            }
            for (int j = 1064; j < data[0].Length; j++)
            {
                data[0][j] = 255;
            }
            binW.Flush();
        }
        internal Bitmap GetImage(int p)
        {
            if ((this.Count == 0) || (p < 0) || (p > this.header.count))
                return null;
            //buil image form index
            IconStruct i = GetIcon(p);
            Bitmap bmp = null;
            MemoryStream mem = null;
            byte[] v_data = i.data[0];
            if ((i.entity[0].width == 0) && (i.entity[0].height == 0))
            {
                //vista icon
                mem = new MemoryStream();
                BinaryWriter binw = new BinaryWriter (mem );
                if (v_data[0] == 40)
                {
                    IntPtr t = Marshal.AllocCoTaskMem(40);
                    Marshal.Copy(v_data, 0, t, 40);
                    CoreBmpHeaderInfo c = (CoreBmpHeaderInfo)Marshal.PtrToStructure(t, typeof(CoreBmpHeaderInfo));
                    CoreBmpHeader v_h = CoreBmpHeader.BMP;
                    v_h.bmpSize = v_data.Length;
                    v_h.offset = 54;
                    v_h.WriteData(binw);
                    c.height /= 2;
                    c.WriteData(binw);
                    mem.Write(v_data, 40, v_data.Length-40);
                }
                else
                {
                    mem.Write(i.data[0], 0, i.data[0].Length);
                }
                mem.Flush();
                mem.Seek(0, SeekOrigin.Begin);
                bmp = new Bitmap(mem);
                mem.Dispose();
            }
            else
            {
                if (i.Bitcount == 32)
                {
                    mem = new MemoryStream();
                    i.Save(mem);
                    mem.Seek(0, SeekOrigin.Begin);
                    Icon ico = new Icon(mem);
                    bmp = ico.ToBitmap();
                    mem.Dispose();
                    ico.Dispose();
                    return bmp;
                }
                else {
                    return i.ToBitmap();
                }
            }
            Bitmap rbmp = new Bitmap(bmp.Width, bmp.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics g = Graphics.FromImage(rbmp);
            g.DrawImage(bmp, Point.Empty);
            g.Dispose();
            bmp.Dispose();
            return rbmp;
        }
        internal IconStruct GetIcon(int p)
        {
            if ((Count == 0) || (p < 0) || (p >= Count))
                return IconStruct.Empty;
            IconStruct i = new IconStruct();
            WinSafeApi.IconHeader h = new WinSafeApi.IconHeader();
            h.reserved = 0;
            h.count = 1;
            h.type = 1;
            WinSafeApi.IconEntity t = new WinSafeApi.IconEntity();
            t = this.entity[p];
            t.offset = 22;
            i.header = h;
            i.entity = new WinSafeApi.IconEntity[1];
            i.entity[0] = t;
            i.data = new byte[1][];
            i.data[0] = this.data[p];
            return i;
        }
        internal static IconStruct FromBitmap(Bitmap bitmap)
        {
            int w = bitmap.Width;
            int h = bitmap.Height;
            bool vistaIco = ((w > 255) || (h > 255));
            MemoryStream stream = new MemoryStream();
            Bitmap bmp = bitmap.Clone() as Bitmap;
            if (vistaIco == false)
                bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
            else
                bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            stream.Seek(0, SeekOrigin.Begin);
            BinaryReader binR = new BinaryReader(stream);
            WinSafeApi.IconHeader ih = new WinSafeApi.IconHeader();
            ih.count = 1;
            ih.reserved = 0;
            ih.type = 1;
            WinSafeApi.IconEntity[] t = new WinSafeApi.IconEntity[1];
            byte[] d = null;
            //Read Header
            t[0] = new WinSafeApi.IconEntity();
            t[0].width = (bitmap.Width > 255) ? (byte)0 : (byte)bitmap.Width;
            t[0].height = (bitmap.Height > 255) ? (byte)0 : (byte)bitmap.Height;
            t[0].colors = 0;
            t[0].reserved = 0;
            t[0].plane = 0;
            t[0].bitperPixel = 32;
            t[0].offset = 22;
            if (vistaIco == false)
            {
                t[0].imageSize = 40 + 4 * (h * w) + (h * w) / 8;
                d = new byte[t[0].imageSize];
                stream.Seek(22, SeekOrigin.Begin);
                h *= 2;
                stream.Write(new byte[] {(byte) ((h & 0x000000FF)),
            (byte) ((h & 0x0000FF00)>>8),
            (byte) ((h & 0x00FF0000)>> 16),
            (byte) ((h & 0xFF000000)>> 24)}, 0, 4);
                stream.Seek(14, SeekOrigin.Begin);
                Bitmap bitmap2 = WinCoreBitmapOperation.GetMask(bitmap, 255,true,false);
                WinCoreBitmapData vd = WinCoreBitmapData.FromBitmap(bitmap2);
                byte[] dmask  = new byte[(h*w)/8];
                int mask = 0;
                int offset = 0;
                int count = 0;
                int decal=0;
                for (int j = 0; j < h/2; j++)
                {
                    for (int i = 0; i < w; i++)
                    {
                        offset = (int)((j * vd.Stride) + (i * 4));
                        if ((vd.Data[offset] == vd.Data[offset + 1]) && (vd.Data[offset + 2] == vd.Data[offset+3]))
                        {
                            //mask += 0
                        }
                        else
                        {
                            mask += 1;
                        }
#pragma warning disable IDE0054 // Use compound assignment
                        mask = mask << 1;
#pragma warning restore IDE0054 // Use compound assignment
                        ++decal;
                        if (decal > 7)
                        {
                            decal = 0;
                            dmask[count] = (byte)mask;
                            mask = 0;
                            count++;
                        }
                    }
                }
                bitmap2.Dispose();
                dmask.CopyTo(d, d.Length - dmask.Length);
                binR.Read(d, 0, d.Length );// (int)(binR.BaseStream.Length));
            }
            else
            {
                d = new byte[(int)binR.BaseStream.Length];
                t[0].imageSize = d.Length;
                binR.Read(d, 0, d.Length);
            }
            binR.Close();
            IconStruct ii = new IconStruct();
            ii.header = ih;
            ii.entity = t;
            ii.data = new byte[][] { d };
            return ii;
        }
        internal object GetIconInfo(int i)
        {
            IconStruct p = GetIcon(i);
            if (p.Equals(IconStruct.Empty))
                return null;
            return p;
        }
        public Bitmap ToBitmap()
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter rwriter = new BinaryWriter(stream);
            this.header.WriteToStream(rwriter);
            for (int i = 0; i < this.Count; i++)
            {
                this.entity[i].WriteToStream(rwriter);
            }
            for (int i = 0; i < this.Count; i++)
            {
                rwriter.Write(data[i], 0, data[i].Length);
            }
            stream.Seek(0, SeekOrigin.Begin);
            Icon ico = new Icon(stream);
            Bitmap bmp = ico.ToBitmap();
            ico.Dispose();
            stream.Close();
            stream.Dispose();
            Bitmap rbmp = new Bitmap(bmp.Width, bmp.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics g = Graphics.FromImage(rbmp);
            g.DrawImage(bmp, Point.Empty);
            bmp.Dispose();
            g.Dispose();
            return rbmp;
        }
    }
}

