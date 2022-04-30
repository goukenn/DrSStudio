

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: User32.dll.cs
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
file:User32.dll.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime .InteropServices ;
namespace IGK.AVICaptureApi.Native
{
    internal static class User32
    {
        /*MESSAGe*/
        internal const int WS_OVERLAPPED = 0x00000000;
        internal const int WS_POPUP = 0x40000000 << 1;
        internal const int WS_CHILD = 0x40000000;
        internal const int WS_MINIMIZE = 0x20000000;
        internal const int WS_VISIBLE = 0x10000000;
        internal const int WS_DISABLED = 0x08000000;
        internal const int WS_CLIPSIBLINGS = 0x04000000;
        internal const int WS_CLIPCHILDREN = 0x02000000;
        internal const int WS_MAXIMIZE = 0x01000000;
        internal const int WS_CAPTION = 0x00C00000;     /* WS_BORDER | WS_DLGFRAME  */
        internal const int WS_BORDER = 0x00800000;
        internal const int WS_DLGFRAME = 0x00400000;
        internal const int WS_VSCROLL = 0x00200000;
        internal const int WS_HSCROLL = 0x00100000;
        internal const int WS_SYSMENU = 0x00080000;
        internal const int WS_THICKFRAME = 0x00040000;
        internal const int WS_GROUP = 0x00020000;
        internal const int WS_TABSTOP = 0x00010000;
        internal const int WS_MINIMIZEBOX = 0x00020000;
        internal const int WS_MAXIMIZEBOX = 0x00010000;
        [DllImport("user32", EntryPoint="SendMessage")]
        internal static extern bool BSendMessage(IntPtr  hWnd, int wMsg, IntPtr wParam, IntPtr lParam);
        [DllImport ("user32.dll")]
        internal static extern int SendMessage(IntPtr hwnd,int msg, IntPtr param1, IntPtr param2);
        [DllImport("user32.dll")]
        internal static extern int SendMessage(IntPtr hwnd, uint msg, int param1, int param2);
        [DllImport("user32.dll")]
        internal static extern int SendMessage(IntPtr hwnd, int msg, long param1, long param2);
        [DllImport("user32.dll")]
        internal extern static void CloseWindow(IntPtr hwnd);
        [DllImport("user32.dll")]
        internal extern static IntPtr FindWindow(IntPtr _class, IntPtr name);
        [DllImport("user32.dll")]
        internal extern static bool DestroyWindow(IntPtr hwnd);
    }
}

