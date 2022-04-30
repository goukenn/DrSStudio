

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IconAndCurAPI.cs
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
file:IconAndCurAPI.cs
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
using System.Runtime.InteropServices;
using System.IO;
namespace IGK.DrSStudio
{
    public interface IIconStructEntity
    {
        int Width { get; }
        int Height { get; }
        int BitmapSize { get; }
        void SaveToStream(Stream writer);
    }
    public static class WinSafeApi
    {
        [DllImport("shell32.dll")]
        internal extern static IntPtr ExtractIcon(IntPtr phandle, string filename, int index);
        [DllImport("user32.dll")]
        internal extern static IntPtr LoadCursorFromFile(string filename);
        [DllImport("user32.dll")]
        internal extern static IntPtr CreateIcon(IntPtr hInstance,int nWidth,
        int nHeight,
        byte cPlanes,
        byte cBitsPixel,
        byte[] lpbANDbits,
        byte[] lpbXORbits);
        [DllImport("user32.dll")]
        internal extern static bool DestroyIcon(
        IntPtr hIcon);
        [DllImport("user32.dll")]
        internal extern static IntPtr CreateIconIndirect(
        ref IconInfo piconinfo);
        [DllImport("user32.dll")]
        internal extern static IntPtr LoadIconFromFile(string filename);
        [DllImport("user32.dll")]
        internal extern static bool GetIconInfo(
        IntPtr hIcon,
        ref IconInfo piconinfo);        
        [DllImport("user32.dll")]
        internal extern static IntPtr CreateCursor(
        IntPtr HINSTANCE ,
        int xHotSpot,
        int yHotSpot,
        int nWidth,
        int nHeight,
        byte[] pvANDPlane,
        byte[] pvXORPlane);
        [DllImport("user32.dll")]
        internal extern static bool DestroyCursor(IntPtr hCursor);
        internal const int IMAGE_BITMAP     =   0;
        internal const int IMAGE_ICON          =1;
        internal const int IMAGE_CURSOR        =2;
        [StructLayout(LayoutKind.Sequential)]
        internal struct CursorGroupInfo
        {
            IconHeader header;
            CursorGroupEntity[] entities;
            byte[][] icondatas;
            internal static CursorGroupInfo CreateInfo(IntPtr hModule, IntPtr hRes)
            {
                IconHeader h = (IconHeader)Marshal.PtrToStructure(hRes, typeof(IconHeader));
                int offset = 6;
                CursorGroupEntity[] ventity = new CursorGroupEntity[h.count];
                byte[][] icondatas = new byte[h.count][];
                for (int i = 0; i < h.count; i++)
                {
                    ventity[i] = (CursorGroupEntity)Marshal.PtrToStructure(new IntPtr(hRes.ToInt32() + offset), typeof(CursorGroupEntity));
                    offset += 14;
                    icondatas[i] = GetCursorData(hModule, ventity[i].id, ventity[i].imageSize);
                }
                CursorGroupInfo c = new CursorGroupInfo();
                c.header = h;
                c.entities = ventity;
                c.icondatas = icondatas;
                return c;
            }
            private static byte[] GetCursorData(IntPtr hModule, int id, int imagesize)
            {
                byte[] t = new byte[imagesize];
                IntPtr res = FindResource(hModule, new IntPtr(id), new IntPtr(RT_CURSOR));
                IntPtr icoH = LoadResource(hModule, res);
                Marshal.Copy(icoH, t, 0, t.Length);
                FreeResource(icoH);
                return t;
            }
            //internal IntPtr ToCursorHandle()
            //{
            //}
            public void SaveCursor(Stream stream)
            {
                BinaryWriter binW = new BinaryWriter(stream);
                binW.Write(this.header.reserved);
                binW.Write(this.header.type );
                binW.Write(this.header.count);
                int offset = 6 + 16 * this.header.count;
                //write hieder definition
                int hsize = Marshal.SizeOf(typeof(hotspot));
                IntPtr hspot = Marshal.AllocCoTaskMem(hsize);
                hotspot spot = new hotspot();
                for (int i = 0; i < this.header.count; i++)
                {
                    Marshal.Copy(this.icondatas[i], 0, hspot, hsize);
                    spot = (hotspot)Marshal.PtrToStructure(hspot, typeof(hotspot));
            //                internal byte width;
            //internal byte height;
            //internal byte colors;
            //internal byte reserved;
            //internal short offsetx;
            //internal short offsety;            
            //internal int imageSize;
            //internal int offset;
                    binW.Write ((byte)this.entities [i].width);
                    binW.Write ((byte)(this.entities[i].height));
                    binW.Write ((byte)this.entities[i].bitperPixel );
                    binW.Write ((byte)0);
                    binW.Write((short)spot.hotspotx);
                    binW.Write((short)spot.hotspoty);                
                    binW.Write((int)this.entities[i].imageSize-4);
                    binW.Write((int)offset);
                    //binW.Write(this.entities[i].imageSize);
                    //binW.Write(offset);
                    offset += this.entities[i].imageSize-4;
                }
                Marshal.FreeCoTaskMem(hspot);
                for (int i = 0; i < this.header.count; i++)
                {
                    binW.Write(this.icondatas[i], 4, this.icondatas[i].Length-4);
                }
                binW.Flush();                
            }
            [StructLayout(LayoutKind.Sequential)]
            struct hotspot{
                internal short hotspotx;
                internal short hotspoty;
            }
        }
        [StructLayout(LayoutKind.Sequential)]
        internal struct  CursorGroupEntity
        {
            internal short width;
            internal short height;
            //internal byte colors;
            //internal byte reserved;
            internal short plane;
            internal short bitperPixel;
            internal int imageSize;
            internal short id;
        }
        [StructLayout(LayoutKind.Sequential)]
        internal struct CursorEntity : IIconStructEntity
        {
            internal byte width;
            internal byte height;
            internal byte colors;
            internal byte reserved;
            internal short offsetx;
            internal short offsety;            
            internal int imageSize;
            internal int offset;
            public int Width { get { return this.width ; } }
            public int Height { get { return this.height ; } }
            public int BitmapSize { get { return this.imageSize; } }
            public  void SaveToStream(Stream stream)
            {
                BinaryWriter writrer = new BinaryWriter(stream);
                    writrer.Write(width);
                    writrer.Write(height);
                    writrer.Write(colors);
                    writrer.Write(reserved);
                    writrer.Write( offsetx);
                    writrer.Write( offsety);            
                    writrer.Write(imageSize);
                    writrer.Write(offset);
                    writrer.Flush();
            }
            public static explicit operator CursorEntity(IconEntity entity)
            {
                CursorEntity ventity = new CursorEntity ();
                ventity.width = entity.width;
                ventity.height = entity.height;
                ventity.colors = entity.colors;
                ventity.reserved = entity.reserved;
                ventity.imageSize = entity.imageSize;
                ventity.offset = entity.offset;
                return ventity;
            }
        }
        [StructLayout(LayoutKind.Sequential)]
        internal struct IconGroupInfo
        {
            IconHeader header;
            IconGroupEntity[] entities;
            byte[][] icondatas;
            public static IconGroupInfo CreateInfo(IntPtr hModule,IntPtr hRes)
            {
                IconHeader h = (IconHeader)Marshal.PtrToStructure(hRes, typeof(IconHeader));
                int offset = 6;
                IconGroupEntity[] ventity = new IconGroupEntity[h.count];
                byte[][] icondatas = new byte[h.count][];
                for (int i = 0; i < h.count; i++)
                {
                    ventity[i] = (IconGroupEntity)Marshal.PtrToStructure(new IntPtr(hRes.ToInt32() + offset),typeof(IconGroupEntity));
                    offset += 14;
                    icondatas[i] = GetIconData(hModule, ventity[i].id , ventity[i].imageSize );
                }
                IconGroupInfo cpr = new IconGroupInfo();
                cpr.header = h;
                cpr.entities = ventity;
                cpr.icondatas = icondatas;
                return cpr;
            }
            private static byte[] GetIconData(IntPtr hModule, int id, int imagesize)
            {
                byte[] t = new byte[imagesize];
                IntPtr res = FindResource(hModule, new IntPtr(id), new IntPtr(RT_ICON));
                IntPtr icoH = LoadResource(hModule, res);
                Marshal.Copy(icoH, t, 0, t.Length);
                FreeResource(icoH);
                return t;
            }
            internal System.Drawing.Icon ToIconHandle()
            {
                BinaryWriter binW = new BinaryWriter(new MemoryStream());
                binW.Write(this.header.reserved);
                binW.Write(this.header.type );
                binW.Write(this.header.count);
                int offset = 6 + 16 * this.header.count;
                //write hieder definition
                for (int i = 0; i < this.header.count; i++)
                { 
                    binW.Write (this.entities[i].width);
                    binW.Write (this.entities[i].height);
                    binW.Write (this.entities[i].colors);
                    binW.Write (this.entities[i].reserved);
                    binW.Write (this.entities[i].plane);
                    binW.Write (this.entities[i].bitperPixel);
                    binW.Write(this.entities[i].imageSize);
                    binW.Write(offset);
                    offset += this.entities[i].imageSize;
                }
                for (int i = 0; i < this.header.count; i++)
                {
                    binW.Write(this.icondatas[i]);
                }
                binW.Flush();
                binW.BaseStream.Seek(0, SeekOrigin.Begin);
                System.Drawing.Icon ico = new System.Drawing.Icon(binW.BaseStream);
                binW.Close();                
                return ico;
            }
        }
        [StructLayout(LayoutKind.Sequential)]       
        internal struct IconHeader
        {
            internal short reserved;
            internal short type;
            internal short count;
            internal void WriteToStream(BinaryWriter rwriter)
            {
                rwriter.Write(reserved);
                rwriter.Write(type);
                rwriter.Write(count);
            }
        }
        [StructLayout(LayoutKind.Sequential)]
        internal struct IconGroupEntity
        {
            internal byte width;
            internal byte height;
            internal byte colors;
            internal byte reserved;
            internal short plane;
            internal short bitperPixel;
            internal int imageSize;
            internal short id;
        }
        [StructLayout(LayoutKind.Sequential)]
        internal struct IconEntity : IIconStructEntity
        {
            internal byte width;
            internal byte height;
            internal byte colors;
            internal byte reserved;
            internal short plane;
            internal short bitperPixel;
            internal int imageSize;
            internal int offset;
            public int Width { get { return this.width; } }
            public int Height { get { return this.height; } }
            public int BitmapSize { get { return this.imageSize; } }
            internal void WriteToStream(BinaryWriter rwriter)
            {
                rwriter.Write(width);
                rwriter.Write(height);
                rwriter.Write(colors);
                rwriter.Write(reserved);
                rwriter.Write(plane);
                rwriter.Write(bitperPixel);
                rwriter.Write(imageSize);
                rwriter.Write(offset);
            }
            public void SaveToStream(Stream stream)
            {
                BinaryWriter writrer = new BinaryWriter(stream);
                WriteToStream(writrer);
                writrer.Flush();
            }
        }
        [StructLayout(LayoutKind.Sequential )]
        internal struct IconInfo
        {
            internal byte isIcon;
            internal int hotspotx;
            internal int hotspoty;
            internal IntPtr dataBMP;
            internal IntPtr dataMask;
        }
        [StructLayout(LayoutKind.Sequential )]
        internal struct CURSORSHAPE
        {
            int     xHotSpot;
            int     yHotSpot;
            int     cx;
            int     cy;
            int     cbWidth;
            byte    Planes;
            byte    BitsPixel;
        }
        internal const int RT_CURSOR = (1);
        internal const int RT_ICON = (3);
        internal const int DIFFERENCE = 11;
        internal const int RT_GROUP_CURSOR = (1 + DIFFERENCE);
        internal const int RT_GROUP_ICON  = (3 + DIFFERENCE);
        internal const int RT_ANICURSOR = (21);
        internal const int RT_ANIICON  = (22);
        [DllImport("kernel32.dll")]
        internal extern static IntPtr LoadLibrary(string filename);
        [DllImport("kernel32.dll")]
        internal extern static IntPtr FreeLibrary(IntPtr phandle);
        [DllImport("kernel32.dll")]
        internal extern static IntPtr LoadResource(
                IntPtr hModule,
                IntPtr hResInfo
            );
        [DllImport("kernel32.dll")]
        internal extern static  void LockResource(
                IntPtr hResData
        );
        [DllImport("kernel32.dll")]
        internal extern static  bool FreeResource(
            IntPtr ResData
        );
        [DllImport("kernel32.dll")]
        internal static extern IntPtr
        FindResource(    IntPtr hModule,    IntPtr lpName,    IntPtr lpType);
        [DllImport("kernel32.dll")]
        internal static extern int SizeofResource(
            IntPtr hModule,
            IntPtr hResInfo
            );
        [DllImport("kernel32.dll")]
        internal static extern bool EnumResourceTypes(
         IntPtr  hModule,
         ENUMRESTYPEPROC lpEnumFunc,
         IntPtr lParam
        );
        [DllImport("kernel32.dll")]
        internal static extern bool EnumResourceNames(
        IntPtr hModule,
        IntPtr lpType,
        ENUMRESNAMEPROC lpEnumFunc,
        IntPtr lParam
        );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hModule">handle of the module</param>
        /// <param name="lpType">type of resource (Resource or string) </param>
        /// <param name="lParam">Long param value</param>
        /// <returns></returns>
        internal delegate bool ENUMRESTYPEPROC(IntPtr hModule, IntPtr lpType, IntPtr lParam);
        internal delegate bool ENUMRESNAMEPROC (IntPtr hModule, IntPtr lpType, IntPtr lpName, IntPtr lParam);
        internal static int getChunck(char[] tab)
        {
            int r = 0;
            for (int i = tab.Length-1; i >=0; --i)
            {
                r += tab[i];
                if (i >0)
                    r = r << 8;
            }
            return r;
        }
        internal static string getChunckString(int t)
        {
            string str = ""+(char)(t& 0xFF)+
                            (char)((t& 0xFF00)>>8)+
                            (char)((t& 0xFF0000)>>16)+
                            (char)((t& 0xFF000000)>>24);
            return str;
        }
        [StructLayout(LayoutKind.Sequential )]
        internal struct AniHeaderInfo
        {
            internal int hSize; //default is 32;
            internal int numFrames;
            internal int numSteps;
            internal int numWidth;
            internal int numHeight;
            internal int aniBitCount;
            internal int aniPlane;
            internal int aniDisplayRate; //frequency
            internal int Flag;  /*
                                 * Reserved 32..2 unused
                                 * SequenceFlag (1)
                                 * IconFlag (1): true frame are icon data false = raw data
                                 * */
            public bool IsSequencFlag()
            {
                return (((this.Flag & 0x02)>>1)==1);
            }
            public bool IsIconFlag()
            {
                return (((this.Flag & 0x01)) == 1);
            }
            internal void Save(Stream stream)
            {
                BinaryWriter binW = new BinaryWriter (stream);
                binW.Write(hSize); //default is 32;
                binW.Write(numFrames);
                binW.Write(numSteps);
                binW.Write(numWidth);
                binW.Write(numHeight);
                binW.Write(aniBitCount);
                binW.Write(aniPlane);
                binW.Write(aniDisplayRate); //frequency
                binW.Write(Flag);
                binW.Flush ();
            }
        }
    }
}

