

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidDeviceManagerTool.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿

using IGK.ICore;using IGK.DrSStudio.Android.WinUI;
using IGK.ICore.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Tools
{
    [CoreTools("Tool.AndroidDeviceManager")]
    public class AndroidDeviceManagerTool : AndroidToolBase 
    {
        private static AndroidDeviceManagerTool sm_instance;
        private AndroidDeviceManagerTool()
        {
        }

        public static AndroidDeviceManagerTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static AndroidDeviceManagerTool()
        {
            sm_instance = new AndroidDeviceManagerTool();
        }
        protected override void GenerateHostedControl()
        {
            this.HostedControl = new AndroidDeviceManagerGUI();
        }
        internal object[] GetInstalledDevice()
        {
            string v_devs = AndroidTool.Instance.Cmd("adb.exe devices");
            return null;
        }
        internal object[] GetInstalledApps()
        {
            string v_devs = AndroidTool.Instance.Cmd("adb.exe shell pm list packages");
            string[] t = v_devs.Split(new string[]{"\r\r\n"}, StringSplitOptions.RemoveEmptyEntries );

            return t;
        }
    }
}
