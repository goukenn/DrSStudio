

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: RecentProjectSetting.cs
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
file:RecentProjectSetting.cs
*/
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Settings;
using IGK.ICore.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Settings
{
    [CoreAppSetting (Name="Core.RecentProjectSetting")]
    sealed class RecentProjectSetting : CoreSettingBase
    {
        private static RecentProjectSetting sm_instance;
        List<string> m_Projects;
        public new int Count { get { return this.m_Projects.Count; } }
        public string[] Projects
        {
            get { return m_Projects.ToArray (); }
        }
        private RecentProjectSetting()
        {
            m_Projects = new List<string>();
        }
        
        protected override bool LoadDummyChildSetting(KeyValuePair<string, ICoreSettingValue> d)
        {
            return base.LoadDummyChildSetting(d);
        }
        public static RecentProjectSetting Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static RecentProjectSetting()
        {
            sm_instance = new RecentProjectSetting();
        }
        public override void Clear()
        {
            base.Clear();
            this.m_Projects.Clear();
        }
    }
}

