

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XIcon.cs
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
file:XIcon.cs
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
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing ;
using System.Drawing.Drawing2D ;
using System.Windows.Forms ;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
namespace IGK.DrSStudio.XIcon
{
    /// <summary>
    /// represent an icon for drawin mecanism. used IconStruct to manipulate icon data
    /// </summary>
    public sealed class XIcon : IDisposable
    {
        private enuIconType type;
        private enuIconColor m_colorType;
        private Color[] m_palette;
        private Bitmap Bitmap;
        private byte[] MaskData;
        private List<XIcon> m_icons;
        private bool isTopParent;
        private int width;
        private int height;
        private XIcon()
        {
            m_icons = new List<XIcon>();
            this.isTopParent = true;
        }
        ~XIcon()
        {
            Dispose();
        }
        /// <summary>
        /// get the type of this icon object
        /// </summary>
        public enuIconType Type{get{return type;}}
        /// <summary>
        /// get the color dpi
        /// </summary>
        public enuIconColor ColorType{get{return this.m_colorType;}}
        /// <summary>
        /// get he palette of color
        /// </summary>
        public Color[] Palette { get { return this.m_palette; } }
        /// <summary>
        /// get the number of icon
        /// </summary>
        public int Count { get { return this.m_icons.Count+1; } }
        public void AddIcon(int w, int h, enuIconColor color)
        {
            XIcon ico = CreateIcon(w, h, color,null,null);
            if (ico != null)
            {
                ico.isTopParent = false;
                this.m_icons.Add(ico);
            }
        }
        internal void AddIcon(XIcon xIcon)
        {
            if (xIcon != null)
            {
                xIcon.isTopParent = false;
                this.m_icons.Add(xIcon);
            }
        }
        public void AddIcon(int w, int h, int bitcount, Bitmap bmp, Color[] Palette)
        {
            enuIconColor c = enuIconColor.trueColorRGBA;
            switch(bitcount)
            {
                case 1:
                    c = enuIconColor.bpp1 ;
                break;
                    case 4:
                    c = enuIconColor.bpp4 ;
                break;
                    case 8:
                    c = enuIconColor.bpp8 ;
                break;
                case 24:
                    c = enuIconColor.trueColorRGB  ;
                break;
                   case 32:
                default:
                    c = enuIconColor.trueColorRGBA ;
                break;
            }
            XIcon ico = CreateIcon(w, h, c,bmp,Palette);
            if (ico != null)
            {
                ico.isTopParent = false;
                this.m_icons.Add(ico);
            }
        }
        public static XIcon CreateIcon(int w, int h, enuIconColor color, Bitmap bmp, Color[] Palette)
        {
            XIcon ico = new XIcon();
            ico.type = enuIconType.Icon;
            if ((bmp == null)||(bmp.PixelFormat == PixelFormat.Undefined ))
                ico.Bitmap = new Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            else
                ico.Bitmap = new Bitmap (bmp);
            if ((w < 256) && (h < 256))
            {
                ico.m_colorType = color;
                Bitmap vbmp = null;
                switch (color)
                {
                    case enuIconColor.bpp1:
                        //create an get the palete color
                        vbmp = new Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format1bppIndexed);
                        ico.m_palette  = vbmp.Palette.Entries ;
                        vbmp.Dispose();
                        break;
                    case enuIconColor.bpp4:
                        if ((Palette != null) && (Palette.Length == 16))
                        {
                            ico.m_palette = (Color[])Palette.Clone();
                        }
                        else
                        {
                            vbmp = new Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format4bppIndexed);
                            ico.m_palette = vbmp.Palette.Entries;
                            vbmp.Dispose();
                        }
                        break;
                    case enuIconColor.bpp8:
                        if ((Palette != null) && (Palette.Length == 256))
                        {
                            ico.m_palette = (Color[])Palette.Clone();
                        }
                        else
                        {
                            vbmp = new Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                            ico.m_palette = vbmp.Palette.Entries;
                            vbmp.Dispose();
                        }
                        break;
                    default:
                        if ((Palette == null) || (Palette.Length > 0))
                        ico.m_palette = new Color[0];
                        else
                            ico.m_palette = Palette;
                        break;
                }
                ico.MaskData = XIcon.GetMaskFromBitmap(ico.Bitmap);// GetMask(w, h);
            }
            else {
                //vista icon
                ico.m_palette = new Color[0];
                ico.m_colorType = enuIconColor.trueColorRGBA;
            }
            ico.width = w;
            ico.height = h;            
            return ico;
        }
        private static byte[] GetMask(int w, int h)
        {
            byte[] dmask = new byte[(h * w) / 16];
            return dmask;
        }
        /// <summary>
        /// return a clone copy of this bitmap object
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Bitmap GetClonedImage(int index)
        {
            if (index == 0)
            {
                if ((this.Bitmap != null)&&(this.Bitmap .PixelFormat != PixelFormat.Undefined ))
                {
                    return this.Bitmap.Clone () as Bitmap;
                }
                else {
                    this.Bitmap = new Bitmap(this.width, this.height);
                    return this.Bitmap;
                }
            }
            else
            {
                return this.m_icons[index - 1].Bitmap.Clone() as Bitmap;
            }
        }
        public Bitmap GetImage(int index)
        {
            if (index == 0)
                return this.Bitmap;
            else
            {
                return this.m_icons[index - 1].Bitmap;
            }
        }
        /// <summary>
        /// change the type of this icon
        /// </summary>
        /// <param name="t"></param>
        public void ChangeType(enuIconType t)
        {
            this.type = t;
        }
        public void Save(int index, Stream stream)
        {
            BinaryWriter binW = new BinaryWriter(stream);
                binW.Write((short)0);
                binW.Write((short)this.type);
                binW.Write((short)1);
            WinSafeApi.IconEntity entity = new WinSafeApi.IconEntity();
            int offset = 6 + 16 * 1;
            byte[][] data = new byte[1][];
            WinSafeApi.IconEntity[] v_tentity = new WinSafeApi.IconEntity[1];
            if (index == 0)
                data[0] = this.GetBitmapData();
            else
                data[0] = this.m_icons[index-1].GetBitmapData();
            entity = new WinSafeApi.IconEntity();
            entity.imageSize = data[0].Length;
            entity.offset = offset;
            entity.plane = 1;
            entity.colors = (byte)((this.Palette.Length < 256) ? this.Palette.Length : 0);
            entity.bitperPixel = getBipPerPixel();
            entity.width = (byte)this.width;
            entity.height = (byte)this.height;
            v_tentity[0] = entity;
            entity.WriteToStream(binW);
            for (int i = 0; i < data.Length ; ++i)
            {
                binW.Write(data[i], 0, data[i].Length);
            }
            binW.Flush();
        }
            /// <summary>
        /// Save this Cursor
        /// </summary>
        /// <param name="filename"></param>
        public void Save(Stream stream)
        {
            BinaryWriter binW = new BinaryWriter(stream);
            if (this.isTopParent)
            {
                binW.Write((short)0);
                binW.Write((short)this.type);
                binW.Write((short)this.Count);
            }
            WinSafeApi.IconEntity entity = new WinSafeApi.IconEntity();
            int offset = 6 + 16 * this.Count;
            byte[][] data = new byte[this.Count][];
            WinSafeApi.IconEntity[] v_tentity = new WinSafeApi.IconEntity[this.Count];
            data[0] = this.GetBitmapData();
            entity = new WinSafeApi.IconEntity();
            entity.imageSize = data[0].Length;
            entity.offset = offset;
            entity.plane = 1;
            entity.colors = (byte)((this.Palette.Length<256)?this.Palette.Length:0);
            entity.bitperPixel = getBipPerPixel();
            entity.width = (byte)this.width;
            entity.height = (byte)this.height;
            v_tentity[0] = entity;
            entity.WriteToStream(binW);
            offset += entity.imageSize;
            //get child data
            for (int i = 0; i < this.Count - 1; ++i)
            {
                data[i + 1] = this.m_icons[i].GetBitmapData();
                entity = this.m_icons[i].GetIconEntiry();
                entity.imageSize = data[i + 1].Length;
                entity.colors =(byte) this.m_icons[i].Palette.Length;
                entity.offset = offset;
                entity.WriteToStream(binW);
                offset += entity.imageSize;
            }
            for (int i = 0; i < this.Count; ++i)
            {
                binW.Write(data[i], 0, data[i].Length);
            }
            binW.Flush();
        }
        /// <summary>
        /// Save this Icon
        /// </summary>
        /// <param name="filename"></param>
        public void Save(string filename)
        {
            FileStream fs = null;
            try
            {
                fs = File.Create(filename);
                this.Save(fs);
                fs.Flush();
            }
            catch(Exception ex)
            {
                CoreLog.WriteDebug(ex.Message);
            }
            finally {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
        }
        internal void Save(int index, string filename)
        {
            FileStream fs = File.Create(filename);
            Save(index, fs);
            fs.Flush();
            fs.Close();   
        }
        private WinSafeApi.IconEntity GetIconEntiry()
        {
            WinSafeApi.IconEntity entity =  new WinSafeApi.IconEntity();
            entity.plane = 1;
            entity.colors =(byte) this.Palette.Length;
            entity.bitperPixel = getBipPerPixel();
            entity.width =(byte)( (this.width >255)?0:this.width);
            entity.height = (byte)((this.height>255)?0:this.height );
            return entity;
        }
        private short getBipPerPixel()
        {
            switch (this.m_colorType)
            {
                case enuIconColor.bpp1 :
                    return 1;
                case enuIconColor.bpp4 :
                    return 4;
                case enuIconColor.bpp8 :
                    return 8;
                case enuIconColor.trueColorRGB :
                    return 24;
                case enuIconColor.trueColorRGBA :
                    return 32;
                case enuIconColor .bpp32 :
                    return 32;
                case enuIconColor.bpp24 :
                    return 24;
            }
            return 0;
        }
        /// <summary>
        /// get the bitmap
        /// </summary>
        /// <returns></returns>
        private byte[] GetBitmapData()
        {
            MemoryStream mem = new MemoryStream();
            BinaryWriter binW = new BinaryWriter(mem);
            if ((this.width < 256) && (this.height < 256))
                this.SaveBitmap(binW);
            else
                this.SavePng(binW);
            binW.Flush();
            mem.Seek(0, SeekOrigin.Begin);
            byte[] vtab = new byte[mem.Length];
            mem.Read(vtab, 0, vtab.Length);
            mem.Close();
            mem.Dispose();
            return vtab;
        }
        private void Save(BinaryWriter binW)
        {            
            SaveBitmap(binW);
        }
        private void SaveBitmap(BinaryWriter binW)
        {
            int h = this.Bitmap.Height;
            int w = this.Bitmap .Width ;
            Bitmap v_hbmp = this.Bitmap.Clone() as Bitmap;
            BitmapData d = null;
            byte[] b = null;
            //Write Bitmap Header Info
            CoreBmpHeaderInfo v_bmpHinfo = CoreBmpHeaderInfo.Empty;
            v_bmpHinfo.width = w;
            v_bmpHinfo.height = h * 2;
            v_bmpHinfo.bitcount = this.getBipPerPixel();
            v_bmpHinfo.WriteData(binW);
            switch (this.m_colorType)
            {
                case enuIconColor .trueColorRGBA :
                case enuIconColor.bpp32 :
                    v_hbmp.RotateFlip(RotateFlipType.Rotate180FlipX);
                    d = v_hbmp.LockBits(new Rectangle(0, 0, v_hbmp.Width, v_hbmp.Height),
                        System.Drawing.Imaging.ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);                    
                    b = new byte[d.Stride * d.Height];
                    Marshal.Copy(d.Scan0, b, 0, b.Length);
                    v_hbmp.UnlockBits(d);
                    d = null;
                    v_bmpHinfo.bitcount = 32;
                    binW.Write(b);
                    binW.Write(GetMaskFromBitmap(v_hbmp));
                    //restore
                    v_hbmp.RotateFlip(RotateFlipType.Rotate180FlipX);
                    //save mask
                    //binW.Write(this.MaskData);
                    break;
                case enuIconColor.trueColorRGB :
                case enuIconColor .bpp24 :
                    this.Bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
                    v_bmpHinfo.bitcount = 24;
                    d = this.Bitmap.LockBits(new Rectangle(0, 0, this.Bitmap.Width, this.Bitmap.Height),
                        System.Drawing.Imaging.ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                    b = new byte[d.Stride * d.Height];
                    Marshal.Copy(d.Scan0, b, 0, b.Length);
                    this.Bitmap.UnlockBits(d);
                    d = null;
                    binW.Write(b);
                    binW.Write(GetMaskFromBitmap(this.Bitmap));
                    this.Bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
                    //save mask                    
                    break;
                    //for other bitmap color
                case enuIconColor.bpp8:
                    {
                        Bitmap v = null;// WinCoreBitmapOperation.SaveBitmapAs8bpp(this.Bitmap);
                        v = v_hbmp.Clone(new Rectangle(Point.Empty, this.Bitmap.Size),
                            PixelFormat.Format8bppIndexed);
                        //save palete
                        //for(int i = 0; i < v.Palette.Entries.Length; i++)
                        // {

                        // }



                        this.m_palette = v.Palette.Entries;
                    v_bmpHinfo.bitcount = 8;
                    v_bmpHinfo.colorspalette = this.m_palette.Length;
                    v.RotateFlip(RotateFlipType.Rotate180FlipX);
                    d = v.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);
                    Byte[] t = new byte[d.Stride * d.Height];
                    Marshal.Copy(d.Scan0, t, 0, t.Length);
                        //write palette
                    this.__storePatelete(binW, this.m_palette);
                    
                    v.UnlockBits(d);
                 
                    //write data
                    binW.Write(t, 0, t.Length);                                       
                    //write mask
                    v.RotateFlip(RotateFlipType.Rotate180FlipX);
                        binW.Write(GetMaskFromBitmap(v));
                        v.RotateFlip(RotateFlipType.Rotate180FlipX);

                        v.Dispose();
                    }
                    break;
                case enuIconColor.bpp4:  
                    {
                        v_bmpHinfo.bitcount = 4;                       
                       
                        v_hbmp.RotateFlip(RotateFlipType.Rotate180FlipX);
                        Bitmap v_bmp =
                        this.Bitmap.Clone(new Rectangle(Point.Empty, this.Bitmap.Size),
                        PixelFormat.Format4bppIndexed);
                        //WinCoreBitmapOperation.SaveBitmapAs4bpp(v_hbmp);
                        this.m_palette = v_bmp.Palette.Entries;
                        v_bmpHinfo.colorspalette = this.m_palette.Length;
                        v_bmp.RotateFlip(RotateFlipType.Rotate180FlipX);
                        d = v_bmp.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, PixelFormat.Format4bppIndexed);
                        Byte[] t = new byte[d.Stride * d.Height];
                        Marshal.Copy(d.Scan0, t, 0, t.Length);
                        this.__storePatelete(binW, this.m_palette);
                     
                        //write data
                        binW.Write(t, 0, t.Length);
                       // //SaveBitmapData(binW, d, h, w);
                        v_bmp.UnlockBits(d);
                    
                       ////v.RotateFlip(RotateFlipType.Rotate180FlipX);
                        this.Bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
                       // binW.Write(GetMaskFromBitmap(v_bmp));
                        this.Bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);

                        if (v_bmp != null)
                            v_bmp.Dispose();
                      
                    }
                    break ;
                   // goto treat;
                case enuIconColor.bpp1:
                    v_bmpHinfo.bitcount = 1;
                    v_bmpHinfo.colorspalette = 2;
                    goto treat;
                treat:
                    d = this.Bitmap.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                SaveBitmapData(binW, d, h, w);
                this.Bitmap.UnlockBits(d);
                this.Bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
                binW.Write(GetMaskFromBitmap(this.Bitmap));
                this.Bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
                break;
            }
            binW.Seek(0, SeekOrigin.Begin);
            v_bmpHinfo.bitmapSize = Convert.ToInt32(binW.BaseStream.Length-40);
            v_bmpHinfo.WriteData(binW);
            binW.Flush();

            //dispose cloned bitmap
            v_hbmp.Dispose();
            //restore the old data
        }

        private void __storePatelete(BinaryWriter binW, Color[] m_palette)
        {
            //write palette
            for (int i = 0; i < m_palette.Length; ++i)
            {
                CoreRGBAColor vc = CoreRGBAColor.FromColor(m_palette[i]);
                //vc.a = 0;
                binW.Write((byte)vc.b); //blue
                binW.Write((byte)vc.g); //green
                binW.Write((byte)vc.r); //red
                binW.Write((byte)vc.a); //alpha not consider in most case
                                        //write palette
            }
        }

        private void SavePng(BinaryWriter binW)
        {
            this.Bitmap.Save(binW.BaseStream, ImageFormat.Png);
        }
        private void SaveBitmapData(BinaryWriter binW, BitmapData d, int h,  int w)
        {
            switch (this.m_colorType)
            {
                case enuIconColor.bpp8:                   
                        return;
            }
            //generate dictionary
            Dictionary<CoreRGBAColor, int> dic = new Dictionary<CoreRGBAColor, int>();
            CoreRGBAColor vc;
            for (int i = 0; i < this.Palette.Length; ++i)
            {
                vc =  CoreRGBAColor.FromColor (this.Palette[i]);
                vc.a = 0;
                if (dic.ContainsKey (vc)== false)
                    dic.Add(vc, i);
                binW.Write((byte)vc.b ); //blue
                binW.Write((byte)vc.g ); //green
                binW.Write((byte)vc.r ); //red
                binW.Write((byte)255); //alpha not consider in most case
                //write palette
            }
            int offset = 0;
            byte vr = 0;
            byte vg = 0;
            byte vb = 0;
            byte va = 0;
            int index = -1;
            int mask = 8;   //default mast
            int decal = 0; //pour le décalage
            int value = 0; //value to write
            if (this.ColorType == enuIconColor.bpp1)
                decal = 1;
            else
                if (this.ColorType == enuIconColor.bpp4)
                    decal = 4;
                else
                    decal = 8;
            int stride = w * decal/8;
            int dstride = ((dstride =(stride % 4))>0?4 - dstride : 0);
            byte[] dmask = new byte[dstride];
            if (dmask.Length > 0)
            {
                //mise du non transparent
                for (int i = 0; i < dmask.Length; i++)
                {
                    dmask[i] = 0xFF;
                }
            }
            //copy des donnée du bitmap
            byte[] data = new byte[d.Stride * d.Height];
            Marshal.Copy(d.Scan0, data, 0, data.Length);
            //commencer du début de la ligne jusqu'a la fin pour des spécification windows
            //pour une autre spécification voir documentation
            byte[] outdata = new byte[(w + dmask.Length) * h * decal / 8];
            int count = 0;
            for (int y = h-1; y >= 0; --y)
            {
                for (int x = 0; x < w; ++x)
                {
                    offset = (y * d.Stride) + (x * 4);
                    //lecture d'une pixel
                    va = 0;// data[offset + 3];//;// Marshal.ReadByte(d.Scan0, offset);
                    vr = data[offset+2];// Marshal.ReadByte(d.Scan0, offset + 1);
                    vg = data[offset+1];// Marshal.ReadByte(d.Scan0, offset + 2); //green
                    vb = data[offset];// Marshal.ReadByte(d.Scan0, offset + 3);
                    vc  = new CoreRGBAColor (vr, vg, vb,va);
                    if (dic.ContainsKey(vc) == true)
                    {                    
                        index = dic[vc];
                    }
                    else
                    {
                        //ne contient pas la couleur
                        index = 0;
                    }
                    if (index > 0)
                    {
#pragma warning disable IDE0054 // Use compound assignment
                        value = (value + index);
#pragma warning restore IDE0054 // Use compound assignment
                    }
#pragma warning disable IDE0054 // Use compound assignment
                    mask = mask - decal;
#pragma warning restore IDE0054 // Use compound assignment
                    if (mask <= 0)
                    {
                        mask = 8;
                        //binW.Write((byte)value);
                        outdata[count] = (byte)value;
                        count++;
                         value = 0;
                    }
                    else
                    {
#pragma warning disable IDE0054 // Use compound assignment
                        value = value << decal;
#pragma warning restore IDE0054 // Use compound assignment
                    }
                }
                //fill the line end of each line
                if (dmask.Length > 0)
                {
                    //binW.Write(dmask);
                    for (int i = 0; i < dmask.Length; i++)
                    {
                        outdata[count+i] = dmask[i];// = 0xFF;
                    }
                    count += dmask.Length;
                }
            }
            binW.Write(outdata);
        }
        public static XIcon OpenFile(string filename)
        {
            IconStruct ico = IconStruct.OpenFromFile(filename);
            return FromIconStruct(ico);
        }
        public static XIcon FromIconStruct(IconStruct ico)
        {
            if (ico.Equals(IconStruct.Empty)) return null;
            int index = 0;
            int bpp = ico[index].Bitcount;
            Bitmap bmp = ico.GetImage(index);            
            XIcon xico = XIcon.CreateIcon(bmp.Width, bmp.Height, (enuIconColor)bpp, bmp, ico.GetPalette(index));
            //System.Windows.Forms.PictureBox box = new System.Windows.Forms.PictureBox();
            //box.Image = bmp;
            //System.Windows.Forms.Form frm = new System.Windows.Forms.Form();
            //box.Show();
            //frm.Controls.Add(box);
            //frm.ShowDialog();
            //bmp.Dispose();
            //int bpp = 0;
            for (int i = 1; i < ico.Count; i++)
            {
                bmp = ico.GetImage(i);                
                bpp = ico[i].Bitcount;
                xico.AddIcon(bmp.Width, bmp.Height, bpp, bmp, ico.GetPalette(i));
                bmp.Dispose();
            }
            return xico;
        }
        public static XIcon CreateIcon(int width, int height, enuIconColor enuIconColor)
        {
            return XIcon.CreateIcon(width, height, enuIconColor, null,null);
        }
        /// <summary>
        /// static get the mask lenght
        /// </summary>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <returns></returns>
        public static int GetMaskLength(int w, int h)
        {
            int stride = w / 8;
            //nombre d'octet ajouter
            int dstride = ((dstride = stride % 4) > 0) ? 4 - dstride : 0;
            stride = (stride + dstride) * h;
            return stride;
        }
        static Byte[] GetMaskFromBitmap(Bitmap bmp)
        {
            int h = bmp.Height;
            int w = bmp.Width;
            bmp.MakeTransparent();
            Bitmap bitmap2 = WinCoreBitmapOperation.GetMask(bmp, 0,true,false  );
            WinCoreBitmapData vd = WinCoreBitmapData.FromBitmap(bitmap2);
            //calcul du maskage
            //stride /8
            int stride = w / 8;
            //nombre d'octet ajouter
            int dstride = ((dstride = stride % 4)>0)? 4 - dstride : 0;
            stride = (stride + dstride)*h;
            byte[] dmask = new byte[stride];
            int mask = 0;
            int offset = 0;
            int count = 0;
            int decal = 0;
            for (int j = 0; j < h; j++)
            {
                decal = 0;
                mask = 0;
                for (int i = 0; i < w; i++)
                {
                    offset = (int)((j * vd.Stride) + (i * 4));
                    if (vd.Data[offset] == 255)
                    {
                        //mask += 0
                    }
                    else
                    {
                        mask += 1;
                    }
                    if (decal > 6)
                    {
                        decal = 0;
                        dmask[count] = (byte)mask;
                        mask = 0;
                        ++count;
                    }
                    else
                    {
#pragma warning disable IDE0054 // Use compound assignment
                        mask = mask << 1;
#pragma warning restore IDE0054 // Use compound assignment
                        ++decal;
                    }
                }
                count+= dstride;
            }            
            return dmask;
        }
        public void  Dispose()
        {
            if (this.Bitmap != null)
            {
                this.Bitmap.Dispose();
                this.Bitmap = null;
            }
            foreach (XIcon i in this.m_icons)
            {
                i.Dispose();
            }
            this.m_icons.Clear();
        }
        internal void ResetImage()
        {
            int w = this.Bitmap .Width ;
            int h = this.Bitmap.Height;
            if (this.Bitmap != null)
            {
                this.Bitmap.Dispose();
                this.Bitmap = null;
            }
            this.Bitmap = new Bitmap(w, h);
            foreach (XIcon i in this.m_icons)
            {
                i.ResetImage();
            }            
        }
        internal object Clone()
        {
            XIcon ico = new XIcon();
            ico.Bitmap = this.Bitmap.Clone() as Bitmap;
            ico.height = this.height;
            ico.width = this.width;
            ico.type = this.type;
            ico.m_palette = (Color[])this.m_palette.Clone();
            ico.isTopParent = this.isTopParent;
            ico.m_colorType = this.m_colorType;
            ico.MaskData = (Byte[])this.MaskData.Clone();
            ico.m_icons = new List<XIcon>();
            foreach (XIcon fico in this.m_icons )
            {
                ico.AddIcon (fico.Clone () as XIcon );
            }
            return ico;
        }
    }
}

