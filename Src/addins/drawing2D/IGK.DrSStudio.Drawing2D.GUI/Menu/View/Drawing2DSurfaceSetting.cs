

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Drawing2DSurfaceSetting.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Drawing2D;
using IGK.ICore.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D.Settings
{
    [CoreAppSetting(Name = "Drawing2DZoomSetting")]
    sealed class Drawing2DZoomSetting : CoreSettingBase
    {
        private static Drawing2DZoomSetting sm_instance;
        private Drawing2DZoomSetting()
        {
        }

        public static Drawing2DZoomSetting Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static Drawing2DZoomSetting()
        {
            sm_instance = new Drawing2DZoomSetting();
        }
        [CoreSettingProperty ()]
        [CoreSettingDefaultValue (enuZoomMode.NormalCenter )]
        public enuZoomMode ZoomMode { get { return (enuZoomMode)this["ZoomMode"].Value; } set { this["ZoomMode"].Value = value; } }
        [CoreSettingProperty ()]
        [CoreSettingDefaultValue (true)]
        public bool ShowScroll { get { return (bool)this["ShowScroll"].Value; } set { this["ShowScroll"].Value = value; } }
        [CoreSettingProperty()]
        [CoreSettingDefaultValue(true)]
        public bool ShowRule { get { return (bool)this["ShowRule"].Value; } set { this["ShowRule"].Value = value; } }
        
    }
}
