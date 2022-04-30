

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Kernell.IconAndCur.cs
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
file:Kernell.IconAndCur.cs
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
﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging ;
using IGK.DrSStudio.XIcon;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;
namespace IGK.DrSStudio
{
    /// <summary>
    /// represent some kernell class
    /// </summary>
    public  static partial class Kernell
    {
        private static int m_iconCount;
        private static string m_outFolder;
        /// <summary>
        /// Extraxt IconFrom libray to 
        /// </summary>
        /// <param name="libFile"></param>
        /// <param name="outfolder"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static bool ExtractIconsTo(string filename, string outfolder, ref int i)
        {
            if (string.IsNullOrEmpty(filename))
                return false;
            m_iconCount = 0;
            m_outFolder = outfolder;
            if (System.IO.Directory.Exists(outfolder) == false)
            {
                //folder don't exist
                i = 0;
                return false;
            }
            IntPtr hLib = WinSafeApi.LoadLibrary(filename);
            if (hLib == IntPtr.Zero)
            {
                i = 0;
                return false;
            }
            IntPtr RT_GROUP_ICON = new IntPtr(WinSafeApi.RT_GROUP_ICON);
            WinSafeApi.EnumResourceNames(hLib, RT_GROUP_ICON, EnumAndExtractIconsTo, IntPtr.Zero);
            WinSafeApi.FreeLibrary(hLib);
            i = m_iconCount;
            return true;            
        }
        /// <summary>
        /// Extraxt IconFrom libray to 
        /// </summary>
        /// <param name="libFile"></param>
        /// <param name="outfolder"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static bool ExtractCursorsTo(string filename, string outfolder, ref int i)
        {            
                m_iconCount = 0;
                m_outFolder = outfolder;
                if (System.IO.Directory.Exists(outfolder) == false)
                {
                    //folder don't exist
                    i = 0;
                    return false;
                }
                IntPtr hLib = WinSafeApi.LoadLibrary(filename);
                if (hLib == IntPtr.Zero)
                {
                    i = 0;
                    return false;
                }
                IntPtr RT_GROUP_CURSOR = new IntPtr(WinSafeApi.RT_GROUP_CURSOR);
                WinSafeApi.EnumResourceNames(hLib, RT_GROUP_CURSOR, EnumAndExtractCursorsTo, IntPtr.Zero);
                WinSafeApi.FreeLibrary(hLib);
                i = m_iconCount;
                return true;
        }
        static bool EnumAndExtractCursorsTo(IntPtr hModule, IntPtr lpType, IntPtr lpName, IntPtr lParam)
        {
            string str = Marshal.PtrToStringAnsi(lpName);
            if (string.IsNullOrEmpty(str)) str = lpName.ToString();
            IntPtr res = WinSafeApi.FindResource(hModule, lpName, lpType);
            if (res != IntPtr.Zero)
            {                
                IntPtr lb = WinSafeApi.LoadResource(hModule, res);
                WinSafeApi.CursorGroupInfo grp = WinSafeApi.CursorGroupInfo.CreateInfo(hModule, lb);
                System.IO.FileStream fs = System.IO.File.Create(m_outFolder + "/cur_" + lpName.ToString() + ".cur");
                grp.SaveCursor (fs);
                fs.Seek(0, SeekOrigin.Begin);
                XCursor ct = XCursor.OpenFromStream(fs);
                System.IO.FileStream fs2 = System.IO.File.Create(m_outFolder + "/_cur_" + lpName.ToString() + ".cur");
                ct.Save(fs2);
                fs2.Close();
                fs.Close();
                //ico.Dispose();
                m_iconCount++;
                WinSafeApi.FreeResource(lb);
            }
            return true;
        }
        static bool EnumAndExtractIconsTo(IntPtr hModule, IntPtr lpType, IntPtr lpName, IntPtr lParam)
        {
            string str = Marshal.PtrToStringAnsi(lpName);
            if (string.IsNullOrEmpty(str)) str = lpName.ToString();
            IntPtr res = WinSafeApi.FindResource(hModule, lpName, lpType);
            if (res != IntPtr.Zero)
            {
                IntPtr lb = WinSafeApi.LoadResource(hModule, res);
                WinSafeApi.IconGroupInfo grp = WinSafeApi.IconGroupInfo.CreateInfo(hModule, lb);
                Icon ico = grp.ToIconHandle();
                System.IO.FileStream fs = System.IO.File.Create(m_outFolder + "/icon_" + lpName.ToString() + ".ico");
                ico.Save(fs);
                fs.Close();
                ico.Dispose();
                m_iconCount++;
                WinSafeApi.FreeResource(lb);
            }
            return true;
        }
        public static bool ExtractPng(string filename, string outfolder, ref int i)
        {
            pngCount = 0;
            m_outFolder = outfolder;
            if (System.IO.Directory.Exists(outfolder) == false)
            {
                //folder don't exist
                i = 0;
                return false;
            }
            IntPtr hLib = WinSafeApi.LoadLibrary(filename);
            if (hLib == IntPtr.Zero)
            {
                i = 0;
                return false;
            }
            pngCount = 0;            
            WinSafeApi.EnumResourceTypes(hLib, GetPngResourceType, IntPtr.Zero);
            WinSafeApi.FreeLibrary(hLib);
            i = pngCount;
            return true;            
        }
        public static bool ExtractImage(string filename, string outfolder, ref int i)
        {
            pngCount = 0;
            m_outFolder = outfolder;
            if (System.IO.Directory.Exists(outfolder) == false)
            {
                //folder don't exist
                i = 0;
                return false;
            }
            IntPtr hLib = WinSafeApi.LoadLibrary(filename);
            if (hLib == IntPtr.Zero)
            {
                i = 0;
                return false;
            }
            pngCount = 0;
            WinSafeApi.EnumResourceTypes(hLib, GetImageResourceType, IntPtr.Zero);
            WinSafeApi.FreeLibrary(hLib);
            i = pngCount;
            return true;
        }
        static bool GetPngResourceType(IntPtr hModule, IntPtr lpType, IntPtr lParam)
        {
            string vs = Marshal.PtrToStringAnsi(lpType);
            if (string.IsNullOrEmpty(vs)==false )
            {
                if (vs == "PNG")
                {
                    WinSafeApi.EnumResourceNames(hModule, lpType, EnumExtractPngResource, IntPtr.Zero);
                }             
            }            
            return true;
        }
        static bool GetImageResourceType(IntPtr hModule, IntPtr lpType, IntPtr lParam)
        {
            string vs = Marshal.PtrToStringAnsi(lpType);
            if (string.IsNullOrEmpty(vs) == false)
            {
                if (vs == "IMAGE")
                {
                    WinSafeApi.EnumResourceNames(hModule, lpType, EnumExtractPngResource, IntPtr.Zero);
                }
            }
            return true;
        }
        static int pngCount = 0;
        static bool EnumExtractPngResource(IntPtr hModule, IntPtr lpType, IntPtr lpName, IntPtr lParam)
        {
            string str = Marshal.PtrToStringAnsi(lpName);
            if (string.IsNullOrEmpty(str)) str = lpName.ToString();
            IntPtr res = WinSafeApi.FindResource(hModule, lpName, lpType);
            if (res != IntPtr.Zero)
            {
                int size = WinSafeApi.SizeofResource(hModule, res);
                byte[] t = new byte[size];
                IntPtr lb = WinSafeApi.LoadResource(hModule, res);         
                System.IO.FileStream fs = System.IO.File.Create(m_outFolder + "/image_" + str + ".png");
                Marshal.Copy(lb, t, 0, t.Length);              
                fs.Write(t, 0, t.Length);
                fs.Close();
                pngCount++;
                WinSafeApi.FreeResource(lb);
                return true;
            }
            return false;
        }
        static int bmpCount;
        public static bool ExtractBitmap(string filename, string outfolder, ref int i)
        {
            bmpCount = 0;
            m_outFolder = outfolder;
            if (System.IO.Directory.Exists(outfolder) == false)
            {
                //folder don't exist
                i = 0;
                return false;
            }
            IntPtr hLib = WinSafeApi.LoadLibrary(filename);
            if (hLib == IntPtr.Zero)
            {
                i = 0;
                return false;
            }
            IntPtr RT_BMP = new IntPtr(2);
            bmpCount = 0;
            WinSafeApi.EnumResourceNames(hLib, RT_BMP, EnumExtractBMPResource, IntPtr.Zero);           
            WinSafeApi.FreeLibrary(hLib);
            i = bmpCount;
            return true;
        }
        static bool EnumExtractBMPResource(IntPtr hModule, IntPtr lpType, IntPtr lpName, IntPtr lParam)
        {
            string str = Marshal.PtrToStringAnsi(lpName);
            if (string.IsNullOrEmpty(str)) str = lpName.ToString();
            IntPtr res = WinSafeApi.FindResource(hModule, lpName, lpType);
            if (res != IntPtr.Zero)
            {
                int size = WinSafeApi.SizeofResource(hModule, res);
                byte[] t = new byte[size];
                IntPtr lb = WinSafeApi.LoadResource(hModule, res);         
                System.IO.FileStream fs = System.IO.File.Create(m_outFolder + "/image_" + str + ".bmp");
                Marshal.Copy(lb, t, 0, t.Length);
                CoreBmpHeader header =  CoreBmpHeader.BMP ;
                header.offset = 54;
                header.bmpSize = t.Length;
                BinaryWriter binW = new BinaryWriter(fs);
                header.WriteData(binW);
                binW.Flush();
                fs.Write(t, 0, t.Length);
                fs.Close();
                bmpCount++;
                WinSafeApi.FreeResource(lb);
            }
            return true;
        }
        private static List<IntPtr> typeList;
        internal static IntPtr[] EnumResourcesType(string filename)
        {
            IntPtr hLib = WinSafeApi.LoadLibrary(filename);
            if (hLib == IntPtr.Zero)
            {            
                return null;
            }
            typeList = new List<IntPtr>();
            WinSafeApi.EnumResourceTypes (hLib, EnumResourceType, IntPtr.Zero);
            WinSafeApi.FreeLibrary(hLib);
            return typeList.ToArray();
        }
        static bool EnumResourceType(IntPtr hModule, IntPtr lpType, IntPtr lParam)
        {
            string vs = Marshal.PtrToStringAnsi(lpType);
            if (string.IsNullOrEmpty(vs))
                Console.WriteLine(lpType);
            else
                Console.WriteLine(vs);
            typeList.Add(lpType);
            return true;
        }
        //for getting information
        private static ResourceInfo nameList;
        internal static ResourceInfo EnumResourcesName(string filename, IntPtr t)
        {
            nameList = new ResourceInfo(t);
            IntPtr hLib = WinSafeApi.LoadLibrary(filename);
            if (hLib == IntPtr.Zero)
            {
                return null;
            }
            typeList = new List<IntPtr>();
            WinSafeApi.EnumResourceNames(hLib,t, EnumResourceInfoName, IntPtr.Zero);
            WinSafeApi.FreeLibrary(hLib);
            return nameList;
        }
        static bool EnumResourceInfoName(IntPtr hModule, IntPtr lpType, IntPtr lpName, IntPtr lParam)
        {
            string vs = Marshal.PtrToStringAnsi(lpType);            
            nameList.Count++;
            return true;
        }
        private static List<Icon> icons;
        public static Icon[] ExtractIcon(string filename)
        {
            icons = new List<Icon>();
            IntPtr hLib = WinSafeApi.LoadLibrary(filename);
            if (hLib == IntPtr.Zero)
            {
                return null;
            }
            WinSafeApi.EnumResourceNames(hLib, new IntPtr(WinSafeApi.RT_GROUP_ICON), ExtractIcon, IntPtr.Zero);
            WinSafeApi.FreeLibrary(hLib);
            return icons.ToArray();
        }
        private static bool ExtractIcon(IntPtr hModule, IntPtr lpType, IntPtr lpName, IntPtr lParam)
        {
            string str = Marshal.PtrToStringAnsi(lpName);
            if (string.IsNullOrEmpty(str)) str = lpName.ToString();
            IntPtr res = WinSafeApi.FindResource(hModule, lpName, lpType);
            if (res != IntPtr.Zero)
            {
                IntPtr lb = WinSafeApi.LoadResource(hModule, res);
                WinSafeApi.IconGroupInfo grp = WinSafeApi.IconGroupInfo.CreateInfo(hModule, lb);
                Icon ico = grp.ToIconHandle();
                icons.Add(ico);
                WinSafeApi.FreeResource(lb);
            }
            return true;
        }
    }
}

