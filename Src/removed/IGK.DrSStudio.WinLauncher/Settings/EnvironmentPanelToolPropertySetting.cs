

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: EnvironmentPanelToolPropertySetting.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:EnvironmentPanelToolPropertySetting.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WinLauncher
{
    using IGK.ICore;using IGK.DrSStudio.WinLauncher;
    using IGK.DrSStudio.Settings;
    /// <summary>
    /// represent a panel environment layout setting
    /// </summary>
    sealed class EnvironmentPanelToolPropertySetting : CorePropertySetting
    {
        private WinLauncherLayoutManager  m_LayoutManager;
        const string SL_L = "SelectLeft";
        const string SL_R = "SelectRight";
        const string SL_B = "SelectBottom";
        public WinLauncherLayoutManager  LayoutManager
        {
            get { return m_LayoutManager; }
        }
        public EnvironmentPanelToolPropertySetting(WinLauncherLayoutManager lmanager, string enName):base(enName )
        {
            this.m_LayoutManager = lmanager;
            this.Add(SL_L, null, null);
            this.Add(SL_R, null, null);
            this.Add(SL_B, null, null);
        }
        internal void Bind(WinLauncherLayoutManager lmanager)
        {
            this.m_LayoutManager = lmanager;
        }
        internal void Load(ICoreApplicationSetting dummySetting)
        {
            if (dummySetting[SL_R ]!=null)
            this[SL_R].Value  = dummySetting[SL_R].Value;
            if (dummySetting[SL_L] != null)
            this[SL_L].Value = dummySetting[SL_L].Value;
            if (dummySetting[SL_B] != null)
            this[SL_B].Value = dummySetting[SL_B].Value;
        }
    }
}

