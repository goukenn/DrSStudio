

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: RecentFileSetting.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Settings;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:RecentFileSetting.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Settings
{

    /// <summary>
    /// store recent file list
    /// </summary>
    [CoreAppSetting(Name="Core.RecentFileSetting")]
    public sealed class RecentFileSetting : CoreSettingBase
    {
        private static RecentFileSetting sm_instance;
        private List<string> m_files;
        public string[] Files {
            get {
                return this.m_files.ToArray();
            }
        }
        public new int Count { get { return this.m_files.Count; } }
     
        private RecentFileSetting()
        {
            this.m_files = new List<string>();
        }
        public static RecentFileSetting Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static RecentFileSetting()
        {
            sm_instance = new RecentFileSetting();
        }
        public override void Clear()
        {
 	         base.Clear();
            this.m_files.Clear ();
        }
        internal void Add(string p)
        {
            if (!this.m_files.Contains(p))
            {
                this.m_files.Add(p);
            }
        }
    }    
}

