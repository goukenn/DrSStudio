

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidAdvManagerTool.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Tools;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Tools
{
    [CoreTools("Tool.AndroidSdkManager", ImageKey=AndroidConstant.ANDROID_IMG_SDK_MANAGER)]
    class AndroidAdvManagerTool : AndroidToolBase 
    {
        private static AndroidAdvManagerTool sm_instance;
        private AndroidAdvManagerTool()
        {
        }

        public static AndroidAdvManagerTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static AndroidAdvManagerTool()
        {
            sm_instance = new AndroidAdvManagerTool();

        }

        public void ShowADVManager()
        {
            string v_path = System.IO.Path.Combine(Settings.AndroidSetting.Instance.PlatformSDK, "AVD Manager.exe");
            if (System.IO.File.Exists(v_path))
                System.Diagnostics.Process.Start(v_path);
            else 
                CoreMessageBox.Show("ADV manager not found");
        }
    }
}
