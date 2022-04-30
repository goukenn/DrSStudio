

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XAniCursor.cs
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
file:XAniCursor.cs
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
/* This file is part of iGK-DrawingSoft.
*    iGK-DrawingSoft is free software: you can redistribute it and/or modify
*    it under the terms of the GNU Lesser General Public License as published by
*    the Free Software Foundation, either version 3 of the License, or
*    (at your option) any later version.
*    iGK-DrawingSoft is distributed in the hope that it will be useful,
*    but WITHOUT ANY WARRANTY; without even the implied warranty of
*    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*    GNU General Public License for more details.
*    You should have received a copy of the GNU Lesser General Public License
*    along with IGK-DRAWING SOFT.  If not, see <http://www.gnu.org/licenses/>.
*
*    Copyright (c) 2008-2009 
*    Author : C.A.D. BONDJE DOUE
*    mail : bondje.doue@hotmail.com
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace IGK.DrSStudio.XIcon
{
    /// <summary>
    /// Non 
    /// </summary>
    public sealed class XAniCursor
    {
        internal static readonly int RIFF = WinSafeApi.getChunck(new char[] { 'R', 'I', 'F', 'F' });
        internal static readonly int ACON = WinSafeApi.getChunck(new char[] { 'A', 'C', 'O', 'N' });
        internal static readonly int anih = WinSafeApi.getChunck(new char[] { 'a', 'n', 'i', 'h' });
        internal static readonly int seq = WinSafeApi.getChunck(new char[] { 's', 'e', 'q', ' ' });
        internal static readonly int icon = WinSafeApi.getChunck(new char[] { 'i', 'c', 'o', 'n' });
        internal static readonly int fram = WinSafeApi.getChunck(new char[] { 'f', 'r', 'a', 'm' });
        internal static readonly int rate = WinSafeApi.getChunck(new char[] { 'r', 'a', 't', 'e' });
        internal static readonly int LIST = WinSafeApi.getChunck(new char[] { 'L', 'I', 'S', 'T' });
        private WinSafeApi.AniHeaderInfo m_AniHInfo;
        private List<IconCursor> m_icoCursors;
        private int[] m_rates;
        private int sizeOfData;
        public int Width { get { return this.m_AniHInfo.numWidth; } }
        public int Height { get { return this.m_AniHInfo.numHeight ; } }
        public int DisplayRate { get { return this.m_AniHInfo.aniDisplayRate; } }
        public int BitCount{get{return this.m_AniHInfo .aniBitCount ;}}
        /// <summary>
        /// get the number of coun
        /// </summary>
        public int Count { get { return this.m_AniHInfo.numSteps; } }
        //.ctr
        private XAniCursor()
        { 
        }
        public static XAniCursor CreateAni(int w, int h, int bitcount, int displayRate)
        {
            XAniCursor cr = new XAniCursor();
            cr.m_AniHInfo = new WinSafeApi.AniHeaderInfo();
            cr.m_AniHInfo.hSize = 36;
            cr.m_AniHInfo.numWidth = w;
            cr.m_AniHInfo.numHeight = h;
            cr.m_AniHInfo.aniBitCount = bitcount;
            cr.m_AniHInfo.aniDisplayRate = displayRate;
            cr.m_AniHInfo.aniPlane = 1;
            cr.m_AniHInfo.Flag = 1;
            cr.m_AniHInfo.numSteps = 0;
            cr.m_rates = new int[0];
            cr.m_icoCursors = new List<IconCursor>();
            return cr;
        }
        public Bitmap GetFrame(int index)
        {
            if ((index < 0)||(index> this.Count -1))
                return null;
          IconCursor cr =  this.m_icoCursors[index];
          MemoryStream mem = new MemoryStream();
          cr.SaveTo(mem);
          mem.Seek(0, SeekOrigin.Begin);
            Bitmap bmp = null;
            if (cr.IsIcon())
            {
                Icon c = new Icon(mem);
                bmp = c.ToBitmap();
                c.Dispose();
                mem.Close();
                return bmp;
            }
            else {
                XCursor c = XCursor.OpenFromStream(mem);
                bmp = c.ToBitmap(0);
            }
            return bmp;
        }
        public void ExtractAllFrameTo(string folder, string filename)
        {
            int i = 0;
            string ext = "";
            foreach (IconCursor cr in this.m_icoCursors)
            {
                if (cr.IsIcon()) ext = ".ico";
                else
                    ext = ".cur";
                cr.SaveTo(folder + "/" + filename + "_"+i+ext);
                ++i;
            }
        }
        public void AddFrame(IconCursor ctr, int framerate)
        {
            int[] t = new int [m_rates.Length +1];
            Array.Copy(this.m_rates, t, this.m_rates.Length);
            t[t.Length - 1] = framerate;
            this.m_icoCursors.Add(ctr);
            this.sizeOfData += ctr.size+4+4;
            WinSafeApi.AniHeaderInfo h = this.m_AniHInfo;
            h.numFrames++;
            h.numSteps++;
            this.m_AniHInfo = h;
            this.m_rates = t;
        }
        public void AddFrame(IconStruct ico, int framerate)
        {
            IIconStructEntity[] d = new IIconStructEntity[ico.Count];
            Array.Copy(ico.Entity, d, d.Length);
            XAniCursor.IconCursor cr = new XAniCursor.IconCursor(ico.Header, d, ico.Data);
            AddFrame(cr, framerate);
        }
        internal void AddFrame(Bitmap bitmap, int framerate)
        {
            IconStruct ico = IconStruct.FromBitmap(bitmap);
            MemoryStream mem = new MemoryStream();
            ico.Save(mem);
            mem.Seek(0, SeekOrigin.Begin);
            IconCursor cr = OpenIcon(mem);
            mem.Close();
            AddFrame(cr, framerate);
        }
        public void RemoveFrame(int index)
        {
            if ((index < 0) || (index >= this.Count)) return;
            WinSafeApi.AniHeaderInfo h = this.m_AniHInfo;
            h.numFrames--;
            h.numSteps--;
            //remove size of icon + icon definition + frame length
            sizeOfData -= ( this.m_icoCursors[index].size + 4 + 4);
            this.m_icoCursors.Remove(this.m_icoCursors[index]);
            int[] newrates  = new int [this.m_rates.Length-1];
            for (int i = 0, j = 0; i < this.m_rates.Length; i++)
            {
                if (i == index) continue;
                newrates[j] = this.m_rates[i];
                j++;
            }
            this.m_AniHInfo = h;
            this.m_rates = newrates;
        }
        /// <summary>
        /// Represent a cursor in a ani files
        /// </summary>
        public class IconCursor
        {
            internal WinSafeApi.IconHeader header;
            internal IIconStructEntity[] entity;
            internal byte[][] data;
            internal int size;
            internal IconCursor( WinSafeApi.IconHeader header, IIconStructEntity[] entity, byte[][] data)
            {
                this.header= header;
                this.header.type = 2;
                this.entity = new IIconStructEntity[entity.Length];
                WinSafeApi.CursorEntity ce;
                WinSafeApi.IconEntity ie;
                for (int i = 0; i < this.entity.Length ; i++)
                {
                    if (entity[i] is WinSafeApi.CursorEntity)
                    {
                        ce = (WinSafeApi.CursorEntity)entity[i];
                    }
                    else
                    {
                        ce = new WinSafeApi.CursorEntity();
                        ie = (WinSafeApi.IconEntity)entity[i];
                        ce.colors = ie.colors;
                        ce.imageSize = ie.imageSize;
                        ce.width = ie.width;
                        ce.height = ie.height;
                        ce.offset = ie.offset;
                    }
                    this.entity[i] = ce; 
                }
                this.data = data;
                this.size = this.GetSize ();
            }
            public bool IsIcon()
            {
                return (header.type == 1);
            }
            internal void SaveTo(string filename)
            {
                FileStream fs = File.Create(filename);
                SaveTo(fs);
                fs.Close();
                fs.Dispose();
            }
            internal void SaveTo(Stream stream)
            {
                BinaryWriter binW = new BinaryWriter(stream);
                binW.Write(this.header.reserved);
                binW.Write(this.header.type );
                binW.Write(this.header.count);
                for (int i = 0; i < this.header.count; i++)
                {
                    this.entity[i].SaveToStream (binW.BaseStream );
                }
                for (int i = 0; i < this.header.count; i++)
                {
                    binW.Write(this.data[i],0, this.data[i].Length);
                }
                binW.Flush();
            }
            /// <summary>
            /// get the main size of this icon resources
            /// </summary>
            /// <returns></returns>
            internal int GetSize()
            {
                int ih = 6 + (16 * this.header.count);
                for (int i = 0; i < this.header.count; i++)
                {
                    ih += this.data[i].Length;
                }
                return ih;
            }
        }
        public static XAniCursor OpenFile(string filename)
        {
            BinaryReader binR = new BinaryReader(File.OpenRead(filename));
            if (binR.BaseStream.Length == 0)
            {
                binR.BaseStream.Close();
                return null;
            }
            return OpenFromStream(binR.BaseStream);
        }
        public static XAniCursor OpenFromStream(Stream fs)
        {
            BinaryReader binR = new BinaryReader(fs);
            if (binR.BaseStream.Length == 0)
            {
                binR.BaseStream.Close();
                return null;
            }
            IntPtr dh = IntPtr.Zero;
            int isriff = binR.ReadInt32();
            if (isriff != RIFF)
            {
                binR.BaseStream.Close();
                return null;
            }
            int sizeofdata = binR.ReadInt32();
            XAniCursor ctr = new XAniCursor();
            ctr.sizeOfData = sizeofdata;
            int isainfo = 0;
            string str = "";
            WinSafeApi.AniHeaderInfo header = new WinSafeApi.AniHeaderInfo();
            int d = 0;
            List<IconCursor> icons = new List<IconCursor>();
            int[] rates =null;
            while ((binR.BaseStream.Position < binR.BaseStream.Length))
            {
                isainfo = binR.ReadInt32();
                str = WinSafeApi.getChunckString(isainfo);
                switch (str)
                {
                    case "ACON":
                        //anih
                        d = binR.ReadInt32();                        
                        if (d == anih)
                        {
                            //readsize
                            d = binR.ReadInt32();
                            Byte[] tab = new byte[d];
                            binR.Read(tab, 0, tab.Length);
                            dh = Marshal.AllocCoTaskMem((int)(tab.Length));
                            Marshal.Copy(tab, 0, dh, tab.Length);
                            header = (WinSafeApi.AniHeaderInfo)Marshal.PtrToStructure(dh, typeof(WinSafeApi.AniHeaderInfo));
                            Marshal.FreeCoTaskMem(dh);
                        }
                        break;
                    case "rate":
                        //read size in byte
                        d = binR.ReadInt32();
                        rates = new int[header.numSteps ] ;
                        for (int i = 0; i < rates.Length ; i++)
                        {
                            rates[i] = binR.ReadInt32();
                        }
                        break;
                    case "LIST":
                        //readsize
                        d = binR.ReadInt32();
                        int f =binR.ReadInt32 ();                                              
                        if (f == fram)
                        {
                            //readframe value acording to header.numSteps
                            int hf =binR.ReadInt32 ();                           
                            if(hf==icon)
                            {
                                //readinco frame
                                int iconsize = binR.ReadInt32();
                                        IconCursor cr = OpenIcon(binR.BaseStream);
                                        icons.Add(cr);
                                        break;
                            }
                        }
                        break;
                    case "icon":
                        {
                            //readinco frame
                            int iconsize = binR.ReadInt32();
                            IconCursor cr = OpenIcon(binR.BaseStream);
                            icons.Add(cr);
                        }
                        break;
                    default:
                        break;
                }
            }
            ctr.m_AniHInfo = header;
            ctr.m_icoCursors = icons;
            ctr.m_rates = rates;
            return ctr;
        }
        private static IconCursor OpenIcon(Stream stream)
        {
            BinaryReader binR = new BinaryReader(stream);
            WinSafeApi.IconHeader header = new WinSafeApi.IconHeader();
            header.reserved = binR.ReadInt16();
            header.type = binR.ReadInt16();
            header.count = binR.ReadInt16();
            byte[][] data = new byte[header.count][];
            IIconStructEntity[] d = new IIconStructEntity[header.count];
            if (header.type  == 1)
            {
                //read icon 
                WinSafeApi.IconEntity[] t = new WinSafeApi.IconEntity[header.count ];
                for (int i = 0; i < header.count; i++)
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
                //copy entity
                Array.Copy(t, d, d.Length);
            }
            else { 
                //readcursor info
                WinSafeApi.CursorEntity[] t = new WinSafeApi.CursorEntity[header.count];
                for (int i = 0; i < header.count; i++)
                {
                    t[i] = new WinSafeApi.CursorEntity ();
                    t[i].width = binR.ReadByte();
                    t[i].height = binR.ReadByte();
                    t[i].colors = binR.ReadByte();
                    t[i].reserved = binR.ReadByte();
                    t[i].offsetx  = binR.ReadInt16();
                    t[i].offsety  = binR.ReadInt16();
                    t[i].imageSize = binR.ReadInt32();
                    t[i].offset = binR.ReadInt32();
                }
                Array.Copy(t, d, d.Length);
            }
            //reade data
            for (int i = 0; i < header.count; i++)
            {
                data[i] = new byte[d[i].BitmapSize];
                binR.Read(data[i], 0, data[i].Length);
            }
            return new IconCursor(header, d, data);
        }
        /// <summary>
        /// Save filename
        /// </summary>
        /// <param name="filename"></param>
        public void Save(string filename)
        {
            FileStream fs = File.Create(filename);
            Save(fs);
            fs.Flush();
            fs.Close();
        }
        /// <summary>
        /// Save any to cursor
        /// </summary>
        /// <param name="stream"></param>
        public void Save(Stream stream)
        {
            BinaryWriter binW = new BinaryWriter(stream);
            binW.Write(RIFF);
            binW.Write(this.sizeOfData);  //try old data
            binW.Write(ACON);
            binW.Write(anih);
            binW.Write((int)this.m_AniHInfo.hSize);
            this.m_AniHInfo.Save(stream);
            //save rate
            binW.Write((int)rate);
            binW.Write ((int)(this.m_rates .Length * 4));
            for (int i = 0; i < this.m_rates.Length; i++)
            {
                binW.Write((int)this.m_rates[i]);
            }
                MemoryStream mem = new MemoryStream();
                BinaryWriter vbi = new BinaryWriter (mem);
                foreach (IconCursor c in this.m_icoCursors )
                {
                    vbi.Write((int)icon);
                    vbi.Write((int)c.GetSize());
                    c.SaveTo (vbi.BaseStream );
                }
                vbi.Flush();                
                mem.Seek(0,SeekOrigin.Begin );
            //save List
                binW.Write((int)LIST);
            //size of list
            binW.Write((int)(mem.Length+4));
            binW.Write((int)fram);
            BinaryReader r = new BinaryReader(mem);
            byte[] t = new byte[4096];
            int cc = 0;
            while ((cc = r.Read(t, 0, 4096)) > 0)
            {
                binW.Write(t, 0, cc);
            }
            //mem.WriteTo(binW.BaseStream);
            binW.Flush();
            binW.Seek(4, SeekOrigin.Begin);
            //size of data
            int s = (int)(binW.BaseStream.Length - 4);
            binW.Write(s);
            binW.Flush();
            mem.Close();
        }
    }
}

