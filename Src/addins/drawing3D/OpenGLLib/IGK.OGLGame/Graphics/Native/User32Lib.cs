

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: User32Lib.cs
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
file:User32Lib.cs
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
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igk-dev.be
App : DrSStudio
powered by IGK - DEV 2008-2011
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace IGK.OGLGame.Native
{
    internal static class User32Lib
    {

#if OS_WINDOWS
        const string LIBNAME = "user32.dll";
#else 
                const string LIBNAME = "user32.dll";
#endif
#if OS_WINDOWS
        internal const int WM_KEYDOWN = 0x0100;
        internal const int WM_KEYUP = 0x0101;
        internal const int WM_CHAR = 0x0102;
        public const int WM_CREATE = 0x0001;
        public const int WM_DESTROY = 0x0002;
        public const int WM_MOVE = 0x0003;
        public const int WM_SIZE = 0x0005;
        public const int WM_MOUSEMOVE = 0x200;
        internal const int WM_NCLBUTTONDOWN = 0x00A1;
        internal const int WM_NCLBUTTONUP = 0x00A2;
        internal const int WM_NCRBUTTONDOWN = 0x00A4;
        internal const int WM_NCRBUTTONUP = 0x00A5;
        internal const int WM_ERASEBKGND = 0x015;
        internal const int WM_PAINT = 0xf;
#endif
        internal const int WS_EX_TRANSPARENT = 0x20;


        const string USER32 = "User32.dll";
        [DllImport(USER32)]
        internal static extern IntPtr GetDC(IntPtr hwnd);
        [DllImport(USER32)]
        internal static extern IntPtr GetWindowDC(IntPtr hwnd);
        [DllImport(USER32, SetLastError = true)]
        internal static extern bool ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [DllImport("user32.dll")]
        internal static extern bool PeekMessage(
            ref Message msg,
            IntPtr hwnd,
            uint wMsgFilterMin,
            uint wMsgFilterMax,
            uint wRemoveMsg);
        [DllImport("user32.dll")]
        internal static extern void TranslateMessage(ref Message msg);
        [DllImport("user32.dll")]
        internal static extern void DispatchMessage(ref Message msg);
        internal const uint PM_NOREMOVE = 0x0001;
        internal const uint PM_REMOVE = 0x0001;
        internal const long   WS_OVERLAPPED       =0x00000000L;
internal const long   WS_POPUP            =0x80000000L;
internal const long   WS_CHILD            =0x40000000L;
internal const long   WS_MINIMIZE         =0x20000000L;
internal const long   WS_VISIBLE          =0x10000000L;
internal const long   WS_DISABLED         =0x08000000L;
internal const long   WS_CLIPSIBLINGS     =0x04000000L;
internal const long   WS_CLIPCHILDREN     =0x02000000L;
internal const long   WS_MAXIMIZE         =0x01000000L;
internal const long   WS_CAPTION          =0x00C00000L ;    /* WS_BORDER | WS_DLGFRAME  */
internal const long   WS_BORDER           =0x00800000L;
internal const long   WS_DLGFRAME         =0x00400000L;
internal const long   WS_VSCROLL          =0x00200000L;
internal const long   WS_HSCROLL          =0x00100000L;
internal const long   WS_SYSMENU          =0x00080000L;
internal const long   WS_THICKFRAME       =0x00040000L;
internal const long   WS_GROUP            =0x00020000L;
internal const long   WS_TABSTOP          =0x00010000L;
internal const long   WS_MINIMIZEBOX      =0x00020000L;
internal const long   WS_MAXIMIZEBOX      =0x00010000L;
internal const long   WS_TILED       =     WS_OVERLAPPED;
internal const long   WS_ICONIC     =      WS_MINIMIZE;
internal const long   WS_SIZEBOX   =       WS_THICKFRAME;
internal const long   WS_TILEDWINDOW =     WS_OVERLAPPEDWINDOW;
/*
 * Common Window Styles
 */
internal const long   WS_OVERLAPPEDWINDOW  = (WS_OVERLAPPED     | 
                             WS_CAPTION        | 
                             WS_SYSMENU        | 
                             WS_THICKFRAME     | 
                             WS_MINIMIZEBOX    | 
                             WS_MAXIMIZEBOX);
internal const long   WS_POPUPWINDOW      =(WS_POPUP          | 
                             WS_BORDER         | 
                             WS_SYSMENU);
internal const long   WS_CHILDWINDOW      =(WS_CHILD);
/*
 * Extended Window Styles
 */
internal const long   WS_EX_DLGMODALFRAME     =0x00000001L;
internal const long   WS_EX_NOPARENTNOTIFY    =0x00000004L;
internal const long   WS_EX_TOPMOST           =0x00000008L;
internal const long   WS_EX_ACCEPTFILES       =0x00000010L;
//internal const long   WS_EX_TRANSPARENT       =0x00000020L;
#if WINVER_4
internal const long   WS_EX_MDICHILD          =0x00000040L;
internal const long   WS_EX_TOOLWINDOW        =0x00000080L;
internal const long   WS_EX_WINDOWEDGE        =0x00000100L;
internal const long   WS_EX_CLIENTEDGE        =0x00000200L;
internal const long   WS_EX_CONTEXTHELP       =0x00000400L;
#endif 
#if WINVER_G4
internal const long   WS_EX_RIGHT             =0x00001000L;
internal const long   WS_EX_LEFT              =0x00000000L;
internal const long   WS_EX_RTLREADING        =0x00002000L;
internal const long   WS_EX_LTRREADING        =0x00000000L;
internal const long   WS_EX_LEFTSCROLLBAR     =0x00004000L;
internal const long   WS_EX_RIGHTSCROLLBAR    =0x00000000L;
internal const long   WS_EX_CONTROLPARENT     =0x00010000L;
internal const long   WS_EX_STATICEDGE        =0x00020000L;
internal const long   WS_EX_APPWINDOW         =0x00040000L;
internal const long   WS_EX_OVERLAPPEDWINDOW  =(WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE);
internal const long   WS_EX_PALETTEWINDOW     =(WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST);
#endif 
        /* WINVER >= =0x0400 */
#if _WIN32_WINNT 
internal const long   WS_EX_LAYERED           =0x00080000;
#endif 
        /* _WIN32_WINNT >= =0x0500 */
#if WINVER_G5
internal const long   WS_EX_NOINHERITLAYOUT   =0x00100000L ;// Disable inheritence of mirroring by children
internal const long   WS_EX_LAYOUTRTL         =0x00400000L ;// Right to left mirroring
#endif 
        /* WINVER >= =0x0500 */
#if _WIN32_WINNT
internal const long   WS_EX_COMPOSITED        =0x02000000L;
#endif
        /* _WIN32_WINNT >= =0x0501 */
#if _WIN32_WINNT
internal const long   WS_EX_NOACTIVATE        =0x08000000L;
#endif 
        /* _WIN32_WINNT >= =0x0500 */
      }
}

