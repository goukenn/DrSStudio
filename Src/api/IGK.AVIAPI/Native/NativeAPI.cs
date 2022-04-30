

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: NativeAPI.cs
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
file:NativeAPI.cs
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
namespace IGK.AVIApi
{
    internal static class NativeAPI
    {
        public const int SEVERITY_ERROR = 1;
        public const int FACILITY_ITF = 4;
        public const int FACILITY_WIN32 = 7;
        public static string mmoiToString(int v)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append((char)(v & 255));
            sb.Append((char)((v >> 8) & 255));
            sb.Append((char)((v >> 16) & 255));
            sb.Append((char)((v >> 24) & 255));
            return sb.ToString();
        }
        public static int mmoiCount(char ch0, char ch1, char ch2, char ch3)
        {
            return ((int)ch0 | ((int)ch1 << 8) | ((int)ch2 << 16) | ((int)ch3 << 24));
        }
        public static long MAKE_CODE(int sev, int fac, int code)
        {
            return (long)((sev << 31) | (fac << 16) | (code));
        }
        public static long MAKE_AVIERR(int code)
        {
            return MAKE_CODE(SEVERITY_ERROR, FACILITY_ITF, code);
        }
    }
}

