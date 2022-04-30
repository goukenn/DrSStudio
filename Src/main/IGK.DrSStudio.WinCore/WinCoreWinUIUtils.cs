

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinCoreWinUIUtils.cs
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
file:WinCoreWinUIUtils.cs
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
using System.IO;
using System.Drawing;
using Microsoft.Win32;
using IGK.ICore.WinCore;
using IGK.Native;
using IGK.ICore.Windows.Native;
using IGK.ICore.WinUI;
using IGK.ICore.Xml;
using IGK.ICore.WinUI.Registrable;
using IGK.ICore;

namespace IGK.DrSStudio.WinUI
{
    public static class WinCoreWinUIUtils
    {
        /// <summary>
        /// get the directory browser item
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Icon GetDirectoryIcon(int width, int height)
        {
            RegistryKey rg = null;
            Icon h = null;
            try
            {
                rg = Registry.ClassesRoot.OpenSubKey("Directory\\DefaultIcon");
                string v = rg.GetValue("").ToString();
                string[] t = v.Split(',');
                IntPtr vh = IntPtr.Zero;
                int p = 0;
                uint r = User32.PrivateExtractIcons(t[0], Convert.ToInt32(t[1]), width, height, ref vh, ref p, 1, 0);
                h = Icon.FromHandle(vh);
            }
            catch
            {
            }
            finally
            {
                if (rg != null)
                {
                    rg.Close();
                    rg = null;
                }
            }
            return h;
        }
        public static Icon GetDriveIcon(int width, int height)
        {
            return GetRegSystemIcon("Drive\\DefaultIcon",width ,height);
        }
        public static Icon ExtractIcon(string filname, int index, int width, int height)
        {
            Icon h = null;
                IntPtr vh = IntPtr.Zero;
                int p = 0;
                uint r = User32.PrivateExtractIcons(filname , index, width, height, ref vh, ref p, 1, 0);
            if (r != 0)
                h = Icon.FromHandle(vh);
            return h;
        }
        
        
        
        private static IntPtr getHandle(string n, int width , int height)
        {
            string[] t = n.Split(',');
            IntPtr vh = IntPtr.Zero;
            int p = 0;
            if (t.Length > 1)
            {
                uint r = User32.PrivateExtractIcons(t[0], Convert.ToInt32(t[1]), width, height, ref vh, ref p, 1, 0);
            }
            return vh;
        }
        public static Icon GetRegSystemIcon(string regkeyPath ,int width, int height)
        {
            RegistryKey rg = null;
            Icon h = null;
            object v_obj = null;
            try
            {
                rg = Registry.ClassesRoot.OpenSubKey(regkeyPath);// "Directory\\DefaultIcon");
                if (rg == null)
                {
                    return GetUnknowIcon(width, height);
                }
                else
                {
                    v_obj = rg.GetValue("");
                    if (v_obj != null)
                    {
                        string v = v_obj.ToString();
                        string[] t = v.Split(',');
                        IntPtr vh = IntPtr.Zero;
                        int p = 0;
                        uint r = 0;
                        if (t.Length == 2)
                            r = User32.PrivateExtractIcons(t[0], Convert.ToInt32(t[1]), width, height, ref vh, ref p, 1, 0);
                        else
                        {
                            rg = Registry.ClassesRoot.OpenSubKey(t[0] + "\\DefaultIcon");
                            if (rg != null)
                            {
                                string v_n = rg.GetValue("").ToString();
                                vh = getHandle(v_n, width, height);
                            }
                            else {
                                return GetUnknowIcon(width, height );
                            }
                        }
                        if (vh!= IntPtr.Zero )
                        h = Icon.FromHandle(vh);
                    }
                }
            }
            catch(Exception ex)
            {
                CoreLog.WriteLine(ex.Message);
            }
            finally
            {
                if (rg != null)
                {
                    rg.Close();
                    rg = null;
                }
            }
            return h;
        }
        private static Icon GetUnknowIcon(int width, int height)
        {
            IntPtr vh = IntPtr.Zero;
            object v_obj = null;
            RegistryKey 
            rg = Registry.ClassesRoot.OpenSubKey("Unknown\\DefaultIcon");
            if (rg == null)
                return null;
            v_obj = rg.GetValue("");
            v_obj = rg.GetValue("");
            if (v_obj != null)
            {
                string v = v_obj.ToString();
                string[] t = v.Split(',');                
                int p = 0;
                uint r = User32.PrivateExtractIcons(t[0], Convert.ToInt32(t[1]), width, height, ref vh, ref p, 1, 0);
                if (vh != IntPtr.Zero)
                    return  Icon.FromHandle(vh);
            }
            return null;
        }

    }
}

