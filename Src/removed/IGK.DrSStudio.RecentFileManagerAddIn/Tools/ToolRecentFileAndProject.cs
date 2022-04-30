

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ToolRecentFileAndProject.cs
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
file:ToolRecentFileAndProject.cs
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
namespace IGK.DrSStudio.Tools
{
    using IGK.ICore;using IGK.DrSStudio.Tools;
    using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.Settings;
    [CoreTools ("Tools.RecentFilesAndProject")]
    public class ToolRecentFile : CoreToolBase , ICoreRecentFileTool
    {        
        List<string> m_recentProject;
        /// <summary>
        /// get the recent file setting
        /// </summary>
        private RecentFileSetting RecentFileSetting {
            get {
                return RecentFileSetting.Instance;
            }
        }        
        public override bool CanShow
        {
            get
            {
                return false;
            }
        }
        protected override void GenerateHostedControl()
        {
            this.HostedControl = null;
            this.RecentFileSetting.Bind(this);
        }
        private static ToolRecentFile sm_instance;
        private ToolRecentFile()
        {
            this.m_recentProject = new List<string>();
        }
        public static ToolRecentFile Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static ToolRecentFile()
        {
            sm_instance = new ToolRecentFile();
        }
        protected override void RegisterBenchEvent(IGK.DrSStudio.WinUI.ICoreWorkbench workbench)
        {
            workbench.FileOpened += workbench_FileOpened;
            workbench.ProjectOpened += workbench_ProjectOpened;
        }
        protected override void UnregisterBenchEvent(ICoreWorkbench workbench)
        {
            workbench.FileOpened -= workbench_FileOpened;
            workbench.ProjectOpened -= workbench_ProjectOpened;
        }
        void workbench_ProjectOpened(object o, CoreProjectOpenedEventArgs e)
        {
            this.m_recentProject.Add(e.Path);
        }
        void workbench_FileOpened(object o, CoreWorkingFileOpenEventArgs e)
        {
            this.RecentFileSetting.AddFile(e.Path);
        }
        public string[] GetRecentFiles()
        {
            return this.RecentFileSetting.Files;
        }
        public void ClearRecentList()
        {
            this.RecentFileSetting.Clear();
        }
        public string[] GetRecentProjects()
        {
            return this.m_recentProject.ToArray();
        }
    }
}

