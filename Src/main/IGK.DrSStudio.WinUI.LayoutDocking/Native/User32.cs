

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: User32.cs
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
file:User32.cs
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
﻿
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
namespace IGK.DrSStudio.WinUI.Native
{
    internal static class User32
    {
        [DllImport("user32.dll")]
        internal static extern bool ShowWindow(IntPtr hwnd, int flag);
        [DllImport("user32.dll")]
        internal static extern bool CloseWindow(IntPtr HWND);
        [DllImport("user32.dll")]
        internal static extern bool SendMessage(IntPtr hwnd, int Msg, IntPtr Wparam, IntPtr outParam);
        [DllImport("user32.dll")]
        internal static extern bool PostMessage(IntPtr hwnd, int Msg, IntPtr Wparam, IntPtr outParam);
        internal const int TRUE = 1;
        internal const int FALSE = 0;
        internal const int WM_KEYDOWN = 0x0100;
        internal const int WM_KEYUP = 0x0101;
        internal const int WM_CHAR = 0x0102;
        internal const int VK_CONTROL = 0x11;
        internal const int VK_SHIFT = 0x10;
        internal const int VK_ALT = 0x12;
        internal const int SW_SHOWNOACTIVATE = 4;
        //create value
        //System message 
        internal const int SC_SIZE = 0xF000;
        internal const int SC_MOVE = 0xF010;
        internal const int SC_CLOSE = 0xF060;
        internal const int SC_KEYMENU = 0xF100;
        internal const int SC_MINIMIZE = 0xF020;
        internal const int SC_MAXIMIZE = 0xF030;
        internal const int WM_NCLBUTTONDOWN = 0x00A1;
        internal const int WM_NCLBUTTONUP = 0x00A2;
        internal const int WM_NCRBUTTONDOWN = 0x00A4;
        internal const int WM_NCRBUTTONUP = 0x00A5;
        internal const int WM_MOUSEMOVE = 0x0200;
        internal const int WM_LBUTTONUP = 0x0202;
        internal const int WM_RBUTTONUP = 0x0205;
        //WM_NCHITTEST RESULT
        internal const int HTERROR = (-2);
        internal const int HTTRANSPARENT = (-1);
        internal const int HTNOWHERE = 0;
        internal const int HTCLIENT = 1;
        internal const int HTCLOSE = 20;
        internal const int HTCAPTION = 2;
        internal const int HTSYSMENU = 3;
        internal const int HTGROWBOX = 4;
        internal const int HTSIZE = HTGROWBOX;
        internal const int HTMENU = 5;
        internal const int HTHSCROLL = 6;
        internal const int HTVSCROLL = 7;
        internal const int HTMINBUTTON = 8;
        internal const int HTMAXBUTTON = 9;
        internal const int HTLEFT = 10;
        internal const int HTRIGHT = 11;
        internal const int HTTOP = 12;
        internal const int HTTOPLEFT = 13;
        internal const int HTTOPRIGHT = 14;
        internal const int HTBOTTOM = 15;
        internal const int HTBOTTOMLEFT = 16;
        internal const int HTBOTTOMRIGHT = 17;
        internal const int HTBORDER = 18;
        internal const int HTREDUCE = HTMINBUTTON;
        internal const int HTZOOM = HTMAXBUTTON;
        internal const int HTSIZEFIRST = HTLEFT;
        internal const int HTSIZELAST = HTBOTTOMRIGHT;
        /*
        * WM_NCCALCSIZE "window valid rect" return values
        */
        internal const int WVR_ALIGNTOP = 0x0010;
        internal const int WVR_ALIGNLEFT = 0x0020;
        internal const int WVR_ALIGNBOTTOM = 0x0040;
        internal const int WVR_ALIGNRIGHT = 0x0080;
        internal const int WVR_HREDRAW = 0x0100;
        internal const int WVR_VREDRAW = 0x0200;
        internal const int WVR_REDRAW = (WVR_HREDRAW |
                        WVR_VREDRAW);
        internal const int WVR_VALIDRECTS = 0x0400;
        internal const int WM_GETMINMAXINFO = 0x24;
        public static int GET_Y_LPARAM(IntPtr lParam)
        {
            long i = lParam.ToInt64();
            return (int)((short)((i & 0xFFFF0000) >> 16));
        }
        public static int GET_X_LPARAM(IntPtr lParam)
        {
            long i = lParam.ToInt64();
            return (int)(((short)i) & 0x0000FFFF);
        }
        //windows EX STYLE 
        public const int WS_EX_MDICHILD = 0x00000040;
        public const int WS_EX_TOOLWINDOW = 0x00000080;
        public const int WS_EX_WINDOWEDGE = 0x00000100;
        public const int WS_EX_CLIENTEDGE = 0x00000200;
        public const int WS_EX_CONTEXTHELP = 0x00000400;
        [DllImport("user32.dll")]
        internal static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        internal static extern IntPtr GetCapture();
        [DllImport("user32.dll")]
        internal static extern IntPtr SetCapture(IntPtr hwnd);
        internal const int TPM_LEFTALIGN = 0x0000;
        internal const int TPM_RETURNCMD = 0x0100;
        [DllImport("user32.dll")]
        internal static extern int TrackPopupMenuEx(IntPtr hmenu, int flag, int x, int y, IntPtr hwnd, IntPtr param);
        [DllImport("user32.dll")]
        internal static extern IntPtr GetSystemMenu(IntPtr hwnd, bool bRevert);
    }
}

