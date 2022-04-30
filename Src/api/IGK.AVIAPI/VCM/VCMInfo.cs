

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: VCMInfo.cs
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
file:VCMInfo.cs
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
using System.Runtime.InteropServices;
namespace IGK.AVIApi.VCM
{
    [StructLayout (LayoutKind.Sequential )]
    public struct VCMInfo
    {
            public int dwSize;
            public int  fccType;
            public int  fccHandler;
            public int  dwFlags;
            public int  dwVersion;
            public int  dwVersionICM;
            [MarshalAs(UnmanagedType.ByValArray   , SizeConst =32/* 16*/)]
            internal byte[]  szName;//[16];
            [MarshalAs(UnmanagedType.ByValArray /*.ByValTStr*/, SizeConst =256 /*128*/)]
            internal byte[] szDescription;//[128];
            [MarshalAs(UnmanagedType.ByValArray /*.ByValTStr*/, SizeConst =256/* 128*/)]
            internal byte[] szDriver;//[128];
            public string FCCType { get { return NativeAPI.mmoiToString(fccType); } }
            public string FCCHandler { get { return NativeAPI.mmoiToString(fccHandler); } }
            public string Name { get { return GetUnicodeString(this.szName); } }
            public string Driver { get { return GetUnicodeString(this.szDriver); } }
            public string Description { get { return GetUnicodeString(this.szDescription); } }
            static string GetUnicodeString(byte[] t)
            {
                string str = System.Text.Encoding.Unicode.GetString(t);
                str = str.Split('\0')[0];
                return str;
            }
            static string GetUnicodeString(char[] t)
            {
                int c = UnicodeEncoding.ASCII .GetByteCount(t, 0, t.Length);
            //   System.Text.Encoding.Convert (
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < t.Length; i+=2)
                {
                    sb.Append(t[i]);
                }
                return sb.ToString();
            }
    }
}

