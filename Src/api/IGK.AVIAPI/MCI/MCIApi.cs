

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: MCIApi.cs
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
file:MCIApi.cs
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
namespace IGK.AVIApi.MCI
{
    /// <summary>
    /// represent the internal mci api implementation. mmsystem.dll
    /// </summary>
    internal  static class MCIApi
    {
        /// <summary>
        /// send a command string to the mci controller
        /// </summary>
        /// <param name="command"></param>
        /// <param name="result"></param>
        /// <param name="sizeOfResult"></param>
        /// <param name="callBack"></param>
        /// <returns>Mci Error</returns>
        [DllImport("winmm.dll")]
        internal extern static int mciSendString(string command,
            IntPtr result,
            int sizeOfResult,
            IntPtr callBack);
        [DllImport("winmm.dll")]
        internal extern static bool mciGetErrorString(
            int fdwError,
            IntPtr lpszErrorText,
            int cchErrorText
            );
        [DllImport("winmm.dll")]
        internal extern static uint mmsystemGetVersion();
        [DllImport("winmm.dll")]
        internal extern static IntPtr CloseDriver(IntPtr hdriver,long lparam1, long lparam2);
        [DllImport("winmm.dll")]
        internal extern static IntPtr OpenDriver(string lpDriverName, string lpSectionName,
    IntPtr lParam);
        [DllImport("winmm.dll")]
        internal extern static IntPtr SendDriverMessage(IntPtr  hdrvr,uint msg,long lParam1,
        long lParam2);
    }
}

