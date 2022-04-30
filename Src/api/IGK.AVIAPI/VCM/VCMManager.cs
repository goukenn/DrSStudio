

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: VCMManager.cs
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
file:VCMManager.cs
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
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace IGK.AVIApi.VCM
{
    /// <summary>
    /// represent the public Video controller Manager
    /// </summary>
    public static class VCMManager
    {
        /// <summary>
        /// retrieve the installer info
        /// </summary>
        /// <returns></returns>
        public static VCMDriverInfo[] GetInstalledDrivers()
        {
            List<VCMDriverInfo> v_infos = new List<VCMDriverInfo>();
            VCMInfo tb = new VCMInfo ();
            VCMInfo nb = new VCMInfo ();
            int fcctype = 0;
            IntPtr hic = IntPtr.Zero;
            int s = Marshal.SizeOf(nb);
            int v = 0;
            nb.dwSize = s;
            IntPtr alloc = IntPtr.Zero;
            for (int i = 0; VCMApi.ICInfo (fcctype, i, ref tb); i++)
            {
                if (alloc == IntPtr.Zero)
                    alloc = Marshal.AllocCoTaskMem(tb.dwSize);
                hic = VCMApi.ICOpen(tb.fccType, tb.fccHandler, enuVCMLocateFlags.Query);
                if (hic != IntPtr.Zero)
                {
                    v = VCMApi.ICGetInfo(hic, alloc, tb.dwSize);
                    if (v > 0)
                    {
                        nb = (VCMInfo)Marshal.PtrToStructure(alloc, typeof(VCMInfo));
                        v_infos.Add(new VCMDriverInfo(nb));
                    }
                    VCMApi.ICClose(hic);
                }             
            }
            Marshal.FreeCoTaskMem(alloc);
            return v_infos.ToArray ();
        }
        public static VCMDriverInfo GetDriver(string typeName)
        {
            typeName = typeName.ToUpper();
            foreach (VCMDriverInfo  drv in GetInstalledDrivers ())
            {
                if (drv.TypeHandler.ToUpper() == typeName)
                    return drv;                
            }
            return null;
        }
        /// <summary>
        /// configure the video driver
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        public static bool Configure(VCMDriverInfo driver, IntPtr hwnd) {
            bool recOpen = !driver.IsOpen;
            bool ret = true;
            if(!recOpen )
            driver.Open(enuVCMLocateFlags.Query);
            if (driver.CanConfigure)
                driver.Configure(hwnd);
            else
                ret = false;
            if (!recOpen )
            driver.Close();
            return ret;
        }
    }
    public delegate long DriverProcHandler(
    int dwDriverId,
    IntPtr hdrvr,
    uint msg,
    IntPtr lParam1,
    IntPtr lParam2
);
}

