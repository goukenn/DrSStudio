

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PrintAPI.cs
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
file:PrintAPI.cs
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
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
namespace IGK.Native
{
    internal static class PrintAPI
    {
        [DllImport("winspool.Drv",
    EntryPoint = "DocumentPropertiesW", SetLastError = true,
    ExactSpelling = true,
    CallingConvention = CallingConvention.StdCall)]
        internal static extern int DocumentProperties(IntPtr hwnd, IntPtr hPrinter,
            [MarshalAs(UnmanagedType.LPWStr)] string pDeviceName,
            IntPtr pDevModeOutput, ref IntPtr pDevModeInput, int fMode);
        [DllImport("kernel32.dll")]
        internal static extern IntPtr GlobalLock(IntPtr hMem);
        [DllImport("kernel32.dll")]
        internal static extern bool GlobalUnlock(IntPtr hMem);
        [DllImport("kernel32.dll")]
        internal static extern bool GlobalFree(IntPtr hMem);
        /// <summary>
        /// show the printer setting dialog
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="printerSettings"></param>
        internal static void ShowPrinterSettingDialog(IntPtr hwnd, PrinterSettings printerSettings)
        {
            IntPtr hDevMode = printerSettings.GetHdevmode(printerSettings.DefaultPageSettings);
            IntPtr pDevMode = GlobalLock(hDevMode);
            int sizeNeeded = DocumentProperties(hwnd, IntPtr.Zero, printerSettings.PrinterName, IntPtr.Zero , ref pDevMode, 0);
            if (sizeNeeded > 0)
            {
                IntPtr devModeData = Marshal.AllocHGlobal(sizeNeeded);
                int i = DocumentProperties(hwnd, IntPtr.Zero, printerSettings.PrinterName, devModeData, ref pDevMode, 14);
                GlobalUnlock(hDevMode);
                switch (i)
                {
                    case 2://cancel
                        break;
                    case 1: //ok hit
                    default:
                        printerSettings.SetHdevmode(devModeData);
                        printerSettings.DefaultPageSettings.SetHdevmode(devModeData);
                        break;
                }
                Marshal.FreeHGlobal(devModeData);
            }
            GlobalFree(hDevMode);
        }
    }
}

