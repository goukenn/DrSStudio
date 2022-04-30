using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.WinUI.Configuration;
using IGK.ICore.Settings;






﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.GS
{
    /// <summary>
    /// represetn a gs system default setting class
    /// </summary>
    class GSSystemDefaultSetting : GSSystemSettingBase, IGSSystemSetting 
    {
        private static GSSystemDefaultSetting sm_instance;
        private GSSystemDefaultSetting()
        {
        }

        public static GSSystemDefaultSetting Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static GSSystemDefaultSetting()
        {
            sm_instance = new GSSystemDefaultSetting();

        }

        public override string AppMainThreadName
        {
            get { return GSConstant.DEFAULT_MAIN_THREAD_NAME; }
        }
    }
}
