

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidBuildTools.cs
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
    /// <summary>
    /// used to manage toolstrip
    /// </summary>
    [CoreTools("Tool.AndroidBuilToolStrip")]
    public class AndroidBuildTools : AndroidToolBase
    {
        private static AndroidBuildTools sm_instance;
        private AndroidBuildTools()
        {
        }

        public static AndroidBuildTools Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static AndroidBuildTools()
        {
            sm_instance = new AndroidBuildTools();
        }
        protected override void GenerateHostedControl()
        {
            this.HostedControl = new AndroidBuildToolGUI();
        }


    }
}
