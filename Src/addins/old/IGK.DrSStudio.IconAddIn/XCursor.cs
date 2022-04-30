

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XCursor.cs
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
file:XCursor.cs
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
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
namespace IGK.DrSStudio.XIcon
{
    /// <summary>
    /// represent a cursor hold by the system
    /// </summary>
    public sealed class  XCursor
    {
        private WinSafeApi.IconHeader header;
        private WinSafeApi.CursorEntity[] entity;
        private byte[][] curdates;
        private int hotpsotx;
        private int hotpsoty;
        public int Count { get { return this.header.count; } }
        private XCursor()
        { 
        }
        public static XCursor OpenFromStream(Stream fs)
        {
            BinaryReader binR = new BinaryReader(fs);
            if (binR.BaseStream.Length == 0)
            {
                binR.BaseStream.Close();
                return null;
            }
            Byte[] tab = new byte[binR.BaseStream.Length];
            binR.Read(tab, 0, tab.Length);
            IntPtr dh = Marshal.AllocCoTaskMem((int)(tab.Length * Marshal.SizeOf(typeof(byte))));
            Marshal.Copy(tab, 0, dh, tab.Length);
            WinSafeApi.IconHeader h = (WinSafeApi.IconHeader)Marshal.PtrToStructure(dh, typeof(WinSafeApi.IconHeader));
            if ((h.type != 2) || (h.count == 0))
            {
                Marshal.FreeCoTaskMem(dh);
                return null;
            }
            WinSafeApi.CursorEntity[] entity = new WinSafeApi.CursorEntity[h.count];
            byte[][] data = new byte[h.count][];
            int offset = 6 + h.count * 16;
            for (int i = 0; i < h.count; i++)
            {
                entity[i] = (WinSafeApi.CursorEntity)Marshal.PtrToStructure(new IntPtr(dh.ToInt32() + 6 + (16 * i)),
                    typeof(WinSafeApi.CursorEntity));
                entity[i].offset = offset;
                data[i] = new byte[entity[i].imageSize];
                Marshal.Copy(new IntPtr(dh.ToInt32() + offset), data[i], 0, data[i].Length);
                offset += entity[i].imageSize;
            }
            Marshal.FreeCoTaskMem(dh);
            XCursor cr = new XCursor();
            cr.header = h;
            cr.entity = entity;
            cr.curdates = data;
            cr.hotpsotx = entity[0].offsetx;
            cr.hotpsoty = entity[0].offsety;
            return cr;
        }
        /// <summary>
        /// Create a cursor form file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static XCursor OpenFile(string filename)
        {
            BinaryReader binR = new BinaryReader(File.OpenRead(filename));
            if (binR.BaseStream.Length == 0)
            {
                binR.BaseStream.Close();
                return null;
            }
            return OpenFromStream(binR.BaseStream);
        }
        public static XCursor FromXIcon(XIcon icon, int hotspotx, int hotspoty)
        {
            if (icon == null) 
                return null;
            MemoryStream mem = new MemoryStream ();
            icon.Save (mem);
            mem.Flush ();
            mem.Seek (0,SeekOrigin.Begin );
            IconStruct icoStruct = IconStruct.OpenFromStream(mem);
            mem.Close ();
            if (icoStruct.Equals(IconStruct.Empty))
                return null;
            XCursor ctr = new XCursor();
            ctr.header = new WinSafeApi.IconHeader();
            ctr.header.count = (short)icoStruct.Count;
            ctr.header.type = 2;
            ctr.entity = new WinSafeApi.CursorEntity[ctr.header.count];
            ctr.curdates = new byte[ctr.header.count][];
            for (int i = 0; i < ctr.header.count; ++i)
            {
                ctr.entity[i] = (WinSafeApi.CursorEntity)icoStruct.Entity [i];
                ctr.entity[i].offsetx = (short)hotspotx;
                ctr.entity[i].offsety =(short) hotspoty;
                ctr.curdates[i] = icoStruct.Data[i];
            }
            int vo = 0;
            for (int j = ctr.curdates[0].Length  - (32 * 32 / 8); j < ctr.curdates[0].Length; j++)
            {
                vo = ctr.curdates[0][j];
                ctr.curdates[0][j] = 0;
            }
            return ctr;
        }
        public Bitmap ToBitmap(int index)
        {
            if ((index < 0) || (index >= this.header.count))
                return null;
            Bitmap bmp = null;
            MemoryStream stream = new MemoryStream();
            this.SaveAsBitmap(stream, index);
            stream.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            bmp = new Bitmap(stream);
            stream.Close();
            return bmp;
        }
        /// <summary>
        /// Create a cursor Handle From this XCursor Object
        /// </summary>
        /// <returns></returns>
        public IntPtr CreateCursorHandle()
        {
            Icon ico = this.ToIcon();
            IntPtr hico = ico.Handle;            
            WinSafeApi.IconInfo icoInfo = new WinSafeApi.IconInfo ();
            WinSafeApi.GetIconInfo(hico, ref icoInfo);
            icoInfo.hotspotx = this.hotpsotx;
            icoInfo.hotspoty = this.hotpsoty;
            ico.Dispose();
            icoInfo.isIcon = 0;// false;
            return WinSafeApi.CreateIconIndirect(ref icoInfo);
        }
        /// <summary>
        /// save to filename
        /// </summary>
        /// <param name="filename"></param>
        public void Save(string filename)
        {
            FileStream fs = File.Create(filename);
            Save(fs);
            fs.Flush();
            fs.Close();
            fs.Dispose();
        }
        /// <summary>
        /// Save to Stream
        /// </summary>
        /// <param name="stream"></param>
        public void Save(Stream stream)
        {
            BinaryWriter binW = new BinaryWriter(stream);
            binW.Write(this.header.reserved);
            binW.Write(this.header.type);
            binW.Write(this.header.count);
            for (int i = 0; i < this.header.count; i++)
            {
                binW.Write(this.entity[i].width);
                binW.Write(this.entity[i].height);
                binW.Write(this.entity[i].colors);
                binW.Write(this.entity[i].reserved);
                binW.Write(this.entity[i].offsetx );
                binW.Write(this.entity[i].offsety );
                binW.Write(this.entity[i].imageSize);
                binW.Write(this.entity[i].offset);
            }
            for (int i = 0; i < this.header.count; i++)
            {
                binW.Write(this.curdates[i], 0, this.curdates[i].Length);
            }
            binW.Flush();
        }
        /// <summary>
        /// Get the Icon présentation of this
        /// </summary>
        /// <returns></returns>
        public Icon ToIcon()
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter binW = new BinaryWriter(stream);
            binW.Write(this.header.reserved);
            binW.Write((short)1);
            binW.Write(this.header.count);
            int size = 40;
            IntPtr dh = Marshal.AllocCoTaskMem(size);
            for (int i = 0; i < this.header.count; i++)
            {
                Marshal.Copy(this.curdates[i], 0, dh, size);
                CoreBmpHeaderInfo hinfo = (CoreBmpHeaderInfo)Marshal.PtrToStructure(dh, typeof(CoreBmpHeaderInfo));
                binW.Write(this.entity[i].width);
                binW.Write(this.entity[i].height );
                binW.Write(this.entity[i].colors );   
                binW.Write(this.entity[i].reserved);
                binW.Write((short)hinfo.plane);
                binW.Write((short)hinfo.bitcount );            
                binW.Write(this.entity[i].imageSize );
                binW.Write(this.entity[i].offset );
            }
            Marshal.FreeCoTaskMem(dh);
            for (int i = 0; i < this.header.count; i++)
            {
                binW.Write(this.curdates[i], 0, this.curdates[i].Length);
            }
            binW.Flush();
            binW.Seek(0, SeekOrigin.Begin);
            Icon c = new Icon(stream);
            stream.Close();
            return c;
        }
        public void SaveAsBitmap(Stream stream, int index)
        {
            if ((index < 0) || (index >= this.header.count))
                return;
            BinaryWriter binW = new BinaryWriter(stream);
            CoreBmpHeader header = CoreBmpHeader.BMP;
            header.offset = 54;
            int size = 40;
            IntPtr dh = Marshal.AllocCoTaskMem(size);
            Marshal.Copy(this.curdates[index], 0, dh, size);
            CoreBmpHeaderInfo hinfo = (CoreBmpHeaderInfo)Marshal.PtrToStructure(dh, typeof(CoreBmpHeaderInfo));
            header.bmpSize = 14 + this.entity[index].imageSize;
            hinfo.height /= 2;
            Marshal.FreeCoTaskMem(dh);
            binW.Write(header.bmptype);
            binW.Write(header.reserved);
            binW.Write(header.bmpSize);
            binW.Write(header.offset);
            hinfo.WriteData(binW);
            int masklength = XIcon.GetMaskLength(hinfo.width, hinfo.height);
            binW.Write(this.curdates[index], 40, this.curdates[index].Length-(40+masklength) );
            binW.Flush();           
        }
    }
}

