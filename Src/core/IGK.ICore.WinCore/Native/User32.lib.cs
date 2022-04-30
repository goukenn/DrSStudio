

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: User32.lib.cs
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
file:User32.lib.cs
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
using System.Runtime.InteropServices;
using System.Drawing;

namespace IGK.ICore.Windows.Native
{
    /// <summary>
    /// represent he user32 native class
    /// </summary>
    internal static class User32
    {
        //[StructLayout(LayoutKind.Sequential)]
        //internal struct IconInfo
        //{
        //    internal byte isIcon;
        //    internal int hotspotx;
        //    internal int hotspoty;
        //    internal IntPtr hbmMask;
        //    internal IntPtr hbmColor;
        //}
        [DllImport("user32.dll")]
        internal extern static IntPtr CreateIconIndirect(
        ref IconInfo piconinfo);

        //[DllImport("user32.dll")]
        //internal extern static IntPtr CreateIconIndirect(
        //ref IconInfo piconinfo);

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        internal static extern bool DeleteObject(IntPtr hObject);
        [StructLayout(LayoutKind.Sequential)]
        internal struct MINMAXINFO
        {
            public Point ptReserved;
            //maximum size for fullscreen
            public Point ptMaxSize;
            //maximum position
            public Point ptMaxPosition;
            //minimum track size. for resizing
            public Point ptMinTrackSize;
            //maximum track size. for resizing
            public Point ptMaxTrackSize;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct BLENDFUNCTION
        {
            internal byte BlendOp, BlendFlags, SourceConstantAlpha, AlphaFormat;
            public BLENDFUNCTION(byte t)
            {
                this.BlendOp = AC_SRC_OVER;
                this.BlendFlags = 0;
                this.SourceConstantAlpha = t;
                this.AlphaFormat = AC_SRC_ALPHA;
            }
        }
        [StructLayout(LayoutKind.Sequential)]
        internal struct SCROLLBARINFO
        {
            internal int cbSize;
            internal RECT rcScrollBar;
            internal int dxyLineButton, xyThumbTop, xyThumbBottom, reserved;
            internal int scrollbar, incbtn, pgup, thumb, pgdn, decbtn;
        }
            //windows keys action
            internal const int WM_KEYDOWN = 0x0100;
            internal const int WM_KEYUP = 0x0101;
            internal const int WM_CHAR = 0x0102;
            //windows EX STYLE 
            internal const int WS_EX_MDICHILD = 0x00000040;
            internal const int WS_EX_TOOLWINDOW = 0x00000080;
            internal const int WS_EX_WINDOWEDGE = 0x00000100;
            internal const int WS_EX_CLIENTEDGE = 0x00000200;
            internal const int WS_EX_CONTEXTHELP = 0x00000400;
            internal const int WS_EX_LAYERED_WINDOW = 0x80000;
            internal const int  WS_EX_CONTROLPARENT   =  0x00010000;
            internal const int  WS_EX_STATICEDGE      =  0x00020000;
            internal const int WS_EX_APPWINDOW = 0x00040000;
            internal const int  WS_OVERLAPPED       =0x00000000;
            internal const int  WS_POPUP            =0x40000000<<1;
            internal const int  WS_CHILD            =0x40000000;
            internal const int  WS_MINIMIZE         =0x20000000;
            internal const int  WS_VISIBLE          =0x10000000;
            internal const int  WS_DISABLED         =0x08000000;
            internal const int WS_CLIPSIBLINGS = 0x04000000;
            internal const int  WS_CLIPCHILDREN     =0x02000000;
            internal const int  WS_MAXIMIZE         =0x01000000;
            internal const int  WS_CAPTION          =0x00C00000;     /* WS_BORDER | WS_DLGFRAME  */
            internal const int  WS_BORDER           =0x00800000;
            internal const int  WS_DLGFRAME         =0x00400000;
            internal const int  WS_VSCROLL          =0x00200000;
            internal const int  WS_HSCROLL          =0x00100000;
            internal const int  WS_SYSMENU          =0x00080000;
            internal const int  WS_THICKFRAME       =0x00040000;
            internal const int  WS_GROUP            =0x00020000;
            internal const int  WS_TABSTOP          =0x00010000;
            internal const int  WS_MINIMIZEBOX     = 0x00020000;
            internal const int WS_MAXIMIZEBOX      =0x00010000;
            internal const int   WS_TILED            =WS_OVERLAPPED;
            internal const int  WS_ICONIC           =WS_MINIMIZE;
            internal const int  WS_SIZEBOX          =WS_THICKFRAME;
            internal const int  WS_TILEDWINDOW      =WS_OVERLAPPEDWINDOW;
            internal const int  WS_OVERLAPPEDWINDOW  = (WS_OVERLAPPED     | 
                        WS_CAPTION        | 
                        WS_SYSMENU        | 
                        WS_THICKFRAME     | 
                        WS_MINIMIZEBOX    | 
                        WS_MAXIMIZEBOX);
            internal const int  WS_POPUPWINDOW      =(WS_POPUP          | 
                        WS_BORDER         | 
                        WS_SYSMENU);
            internal const int  WS_CHILDWINDOW      =(WS_CHILD);
            /*window message */
            internal const int WM_NULL = 0x0000;
            internal const int WM_CREATE = 0x0001;
            internal const int WM_DESTROY = 0x0002;
            internal const int WM_MOVE = 0x0003;
            internal const int WM_SIZE = 0x0005;
            internal const int WM_ACTIVATE = 0x0006;
            internal const int WM_SETFOCUS =0x0007;
            internal const int WM_ACTIVATEAPP = 0x001C;
            internal const int WM_NCCREATE = 0x0081;
            internal const int WM_NCDESTROY = 0x0082;
            internal const int WM_NCCALCSIZE = 0x0083;
            internal const int WM_NCHITTEST = 0x0084;
            internal const int WM_NCPAINT = 0x0085;
            internal const int WM_NCACTIVATE = 0x0086;
            internal const int WM_KILLFOCUS = 0x0008;
            internal const int WM_GETMINMAXINFO = 0x24;
            internal const int WM_GETTEXT = 0x000D;
            internal const int WM_GETTEXTLENGTH = 0x000E;
            internal const int WM_MOUSEMOVE = 0x0200;
            internal const int WM_LBUTTONUP = 0x0202;
            internal const int WM_RBUTTONUP = 0x0205;
            internal const int WM_ERASEBKGND = 0x0014;
            internal const int WM_ENTERSIZEMOVE = 0x231;
            internal const int WM_EXITSIZEMOVE = 0x232;
            internal const int WM_PAINT = 0xf;
            internal const int WM_SIZING = 0x214;
            internal const int WM_MOVING = 0x216;
            internal const int WM_SYSCOMMAND = 0x0112;
        internal const int DCX_WINDOW = 0x00000001;
        internal const int DCX_CACHE = 0x00000002;
        internal const int DCX_NORESETATTRS = 0x00000004;
        internal const int DCX_CLIPCHILDREN = 0x00000008;
        internal const int DCX_CLIPSIBLINGS = 0x00000010;
        internal const int DCX_PARENTCLIP = 0x00000020;
        internal const int DCX_EXCLUDERGN = 0x00000040;
        internal const int DCX_INTERSECTRGN = 0x00000080;
        internal const int DCX_EXCLUDEUPDATE = 0x00000100;
        internal const int DCX_INTERSECTUPDATE = 0x00000200;
        internal const int DCX_LOCKWINDOWUPDATE = 0x00000400;
        internal const int DCX_VALIDATE = 0x00200000;
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
        internal const int AC_SRC_OVER = 0x0;
        internal const int AC_SRC_ALPHA = 0x1;
        internal const int ULW_ALPHA = 0x2;
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
        //NON CLIENT BUTTON
        internal const int WM_NCLBUTTONDOWN = 0x00A1;
        internal const int WM_NCLBUTTONUP = 0x00A2;
        internal const int WM_NCRBUTTONDOWN = 0x00A4;
        internal const int WM_NCRBUTTONUP = 0x00A5;
        [DllImport("user32.dll")]
        internal static extern IntPtr GetDCEx(IntPtr hwnd, IntPtr rgeion, int flag);
        [DllImport("user32.dll")]
        internal static extern bool ReleaseDC(IntPtr hwnd, IntPtr hdc);
        [DllImport("user32.dll")]
        internal static extern bool GetWindowRect(IntPtr hwnd, ref RECT rec);
        [DllImport("user32.dll")]
        internal extern static bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst,
                 ref Point pptDst, ref Size psize,
                 IntPtr hdcSrc,
                 ref Point pptSrc,
                 Int32 crKey,
                 ref BLENDFUNCTION pblend,
                 Int32 dwFlags);
        [DllImport("user32.dll")]
        internal extern static bool GetScrollBarInfo(IntPtr intPtr, uint idObject, ref SCROLLBARINFO info);
        [DllImport("user32.dll")]
        internal static extern bool ShowWindow(IntPtr hwnd, int flag);
        [DllImport("user32.dll")]
        internal static extern bool CloseWindow(IntPtr HWND);
        [DllImport("user32.dll")]
        internal static extern bool SendMessage(IntPtr hwnd, int Msg, IntPtr Wparam, IntPtr outParam);
        [DllImport("user32.dll")]
        internal static extern bool PostMessage(IntPtr hwnd, int Msg, IntPtr Wparam, IntPtr outParam);
        internal static RECT GetNativeRect(Rectangle rc)
        {
            RECT p = new RECT();
            p.left = rc.Left;
            p.right = rc.Right;
            p.bottom = rc.Bottom;
            p.top = rc.Top;
            return p;
        }
        internal static Rectangle GetRectangleFrom(RECT c)
        {
            return new Rectangle(c.left, c.top, c.right - c.left, c.bottom - c.top);
        }
        internal  static System.Drawing.Point GetPoint(IntPtr LParam)
        {
            long v = LParam.ToInt32();
            int w = (int)(v & 0xFFFF);
            if ((w & 0x8000) != 0)
            {
                w = -ushort.MaxValue + w;
            }
            int h = (int)((LParam.ToInt32() & 0xFFFF0000) >> 16);
            if ((h & 0x8000) != 0)
            {
                h = -ushort.MaxValue + h;
            }
            return new System.Drawing.Point(w, h);
        }
        [DllImport ("user32.dll")]
        internal extern static IntPtr GetWindowDC(IntPtr hwnd);
        [DllImport("user32.dll")]
        internal static extern uint
PrivateExtractIcons(
    string szFileName,
    int nIconIndex,
    int cxIcon,
    int cyIcon,
    ref IntPtr phicon,
    ref int piconid,
    uint nIcons,
    uint flags);
        [DllImport ("User32.dll")]
        internal static extern void SetForegroundWindow(IntPtr v_hwnd);
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct NCCALPARAMS
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        internal RECT[] rgrc;
        internal WINDOWPOS lppos;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct WINDOWPOS
    {
        internal IntPtr hwnd;
        internal IntPtr hwndInsertAfter;
        internal int x;
        internal int y;
        internal int cx;
        internal int cy;
        internal uint flags;
    }
    [StructLayout(LayoutKind.Sequential)]
    struct RECT
    {
        internal int left;
        internal int top;
        internal int right;
        internal int bottom;
        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3}", left, top, right, bottom);
        }
    }
}

