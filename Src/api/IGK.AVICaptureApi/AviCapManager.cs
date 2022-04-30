

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AviCapManager.cs
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
file:AviCapManager.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;


namespace IGK.AVICaptureApi
{
    
    
    using IGK.AVICaptureApi.WinUI;
    using IGK.AVICaptureApi;
    using IGK.AVICaptureApi.Native;

    /// <summary>
    /// use to manage avi cap capability.
    /// </summary>
    public class AviCapManager
    {
        /// <summary>
        /// get the installed drivers
        /// </summary>
        /// <returns></returns>
        public static AviCapDriverInfo[] GetInstalledDriver()
        {
            List<AviCapDriverInfo> v_drivers = new List<AviCapDriverInfo>();
            const int SIZE = 255;
            IntPtr v_name = Marshal .AllocCoTaskMem (SIZE);
            IntPtr v_version = Marshal .AllocCoTaskMem (SIZE);
            for (uint i = 0; i < 10; i++)
            {
                if (AviCap32.capGetDriverDescription(i,
                    v_name,
                    SIZE,
                    v_version,
                    SIZE))
                {
                    v_drivers.Add(new AviCapDriverInfo(
                        Marshal.PtrToStringAnsi(v_name),
                        Marshal.PtrToStringAnsi(v_version), i));
                }
#if DEBUG
                else {
                    Debug.WriteLine(" AviCapDriverInfo Not Found : " + i);
                }
#endif
            }
            Marshal.FreeCoTaskMem(v_name);
            Marshal.FreeCoTaskMem(v_version);
            return v_drivers.ToArray();
        }
        /// <summary>
        /// get a capture windows
        /// </summary>
        /// <returns></returns>
        public static CaptureWindow CreateCaptureWindow(string title, int width, int height, bool child, IntPtr parentHwnd)
        {
            int v_style = 0;
            if (child == false)
              v_style =   User32.WS_OVERLAPPED /*| User32.WS_CHILD*/ | User32.WS_VISIBLE | User32.WS_CAPTION | User32.WS_MINIMIZEBOX  ;//..WS_MINIMIZEBOX ;
            else
            {
                v_style =  User32.WS_CHILD | User32.WS_VISIBLE;
            }
            IntPtr v_cap = AviCap32.capCreateCaptureWindow (
                title,
                v_style , 
                0, 0,width, height ,
                parentHwnd , 
                0);
            if (v_cap == IntPtr.Zero)
                return null;
            CaptureWindow v_win = new CaptureWindow();
            v_win.Handle = v_cap;
            v_win.m_ParentHandle = parentHwnd;
            return v_win;
        }
    }
}

