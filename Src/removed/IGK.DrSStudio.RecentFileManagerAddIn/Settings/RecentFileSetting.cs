

/*
IGKDEV @ 2008 - 2014
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
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:RecentFileSetting.cs
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
using System.Linq;
using System.Text;
using Microsoft.Win32;
namespace IGK.DrSStudio.Settings
{
    [CoreAppSetting(Name = "RecentFiles")]
    class RecentFileSetting : CoreSettingBase
    {
        private List<string> m_listFiles;
        private static RecentFileSetting sm_instance;
        public string[] Files {
            get {
                return this.m_listFiles.ToArray();
            }
        }
        private RecentFileSetting():base()
        {
            m_listFiles = new List<string>();            
        }
        internal void AddFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || m_listFiles.Contains(filePath))
            {
                return;
            }
            int v_c = this.m_listFiles.Count;
            if (v_c >= 10)
            {
                this.m_listFiles.Remove(this.m_listFiles[0]);
            }
            if (v_c < 10)
            {
                m_listFiles.Add(filePath);
                AddSettingFile(filePath);
            }
            else 
            {
                AddFile(filePath);
            }
        }
        private void AddSettingFile(string filePath)
        {
            string k = string.Format("File{0}", this.Count);
            this[k].Value = filePath;
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
        protected override void InitDefaultProperty(System.Reflection.PropertyInfo prInfo, CoreSettingDefaultValueAttribute attrib)
        {
            base.InitDefaultProperty(prInfo, attrib);            
        }
        public override void Load(ICoreSetting setting)
        {
            base.Load(setting);
        }
        internal void Bind(IGK.DrSStudio.Tools.ToolRecentFile tool)
        {
            ICoreSetting d = CoreSystem.Instance.Settings[this.Id];
            //load file;
            int v_c = 0;
            string f = null;
            System.Collections.IEnumerator e = d.GetEnumerator();
            while(e.MoveNext ())
            {
                KeyValuePair<string, ICoreApplicationSetting> app = (KeyValuePair<string, ICoreApplicationSetting>)e.Current ;
                f = System.IO.Path.GetFullPath ( app.Value.Value.ToString());
                if (!System.IO.File.Exists(f) || m_listFiles .Contains (f))
                    continue;
                this.m_listFiles.Add (f);               
                v_c++;
                if (v_c >= 10)
                    break;
            }
            this.Clear();
            foreach (string item in this.m_listFiles)
            {
                this.AddSettingFile(item);
            }
        }
        internal new void Clear()
        {
            base.Clear();
            this.m_listFiles.Clear();
        }
    }
}

