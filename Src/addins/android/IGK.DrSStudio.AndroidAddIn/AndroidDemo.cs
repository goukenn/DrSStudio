

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidDemo.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.DrSStudio.Android.Settings;

namespace IGK.DrSStudio.Android
{
    class AndroidDemo
    {
        public static void CallDemo()
        {
            CoreSystem.Init();
            AndroidSetting.Instance.PlatformSDK = @"D:\Android\adt-bundle-windows-x86_64-20130729\sdk";
            AndroidSetting.Instance.JavaSDK = @"C:\Program Files (x86)\Java\jdk1.6.0_39";
            AndroidSetting.Instance.AntSDK = @"D:\Android\apache-ant-1.8.4";

            var p = Android.Tools.AndroidTool.Instance.GetAndroidTargets();
            //if (p != null)
            //{
            //    foreach (var item in p)
            //    {
            //        Console.WriteLine(item);
            //    }
            //}
            string dir = CoreConstant.DebugTempFolder+"\\igk.android.tutorial001";
            //Console.WriteLine(Android.Tools.AndroidTool.Instance.CreateProject(
            //    "tutorial001",
            //    dir,
            //    "igk.android.tutorial001",
            //    "tutorial001",
            //     p[9]));

            Console.WriteLine(Android.Tools.AndroidTool.Instance.CompileProject(dir, true));



            Android.Tools.AndroidTool.Instance.DeployUnInstall("igk.android.tutorial001");
            //install android activity
            Android.Tools.AndroidTool.Instance.DeployInstall(
                Path.Combine(dir + "/bin/tutorial001-debug.apk"));

            //start android activity
            Console.WriteLine(Android.Tools.AndroidTool.Instance.Cmd("adb shell am start igk.android.tutorial001/.tutorial001"));

            //Cmd("adb install "+
            //    Path.Combine(dir + "/bin/tutorial001-debug.apk"), Application.StartupPath);


            //Console.WriteLine (Android.Tools.AndroidTool.Instance.Cmd("adb bin\\", dir));

            Console.WriteLine("End");
        }
    }
}
