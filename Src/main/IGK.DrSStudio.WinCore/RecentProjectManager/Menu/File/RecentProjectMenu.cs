

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: RecentProjectMenu.cs
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
file:RecentProjectMenu.cs
*/
using IGK.ICore.WinCore;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.Settings;
using IGK.ICore;
using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinUI;
namespace IGK.DrSStudio.Menu.File
{
    [DrSStudioMenu("File.RecentProject", 0x81, SeparatorAfter=true)]
    sealed class RecentProjectMenu : CoreApplicationMenu 
    {
        protected override void InitMenu()
        {
            base.InitMenu();
            BuildMenu();
            this.SetupEnableAndVisibility();
        }
        protected override bool IsEnabled()
        {
            return (this.Childs.Count > 0);
        }
        protected override bool IsVisible()
        {
            return this.IsEnabled();
        }
        private void BuildMenu()
        {
            //build menus
            this.Childs.Clear();
            string[] Files = RecentProjectSetting.Instance.Projects;
            if (Files.Length > 0)
            {
                foreach (string item in Files)
                {
                    this.Childs.Add(new RecentProjectMenuItem(item));
                }
                //build recent file submenu
                string v_id = string.Format(this.Id + ".ClearRecentProjectMenu");
                ClearRecentProjectList c = CoreSystem.GetMenuAction(v_id) as ClearRecentProjectList;
                if (c == null)
                {
                    c = new ClearRecentProjectList(this);
                    CoreMenuAttribute v_attr = new CoreMenuAttribute(v_id, short.MaxValue) { 
                        SeparatorBefore = true 
                    };
                    if (this.Register (v_attr, c))
                    {
                        c.SetAttribute (v_attr);
                    }
                }
                this.Childs.Add(c);
            }
        }
        class RecentProjectMenuItem : CoreApplicationMenu
        {
            private string m_projectFile;

            public new ICoreProjectManagerWorkbench Workbench {
                get { 
                    return base.Workbench as ICoreProjectManagerWorkbench;
                }
            }
            protected override bool IsEnabled()
            {
                return this.Workbench != null;
            }
            public RecentProjectMenuItem(string projectName)
            {
                this.m_projectFile = projectName;
            }
            protected override bool PerformAction()
            {
                this.Workbench.OpenProject(this.m_projectFile);
                return true;
            }
        }
        class ClearRecentProjectList : CoreApplicationMenu
        {
            private RecentProjectMenu m_RecentProjectMenu;
            public ClearRecentProjectList(RecentProjectMenu projectMenu)
            {
                this.m_RecentProjectMenu = projectMenu;
            }
            protected override bool PerformAction()
            {
                if (RecentProjectSetting.Instance.Projects.Length > 0)
                {
                    RecentProjectSetting.Instance.Clear();
                    this.m_RecentProjectMenu.BuildMenu();
                    this.m_RecentProjectMenu.SetupEnableAndVisibility();
                    return true;
                }
                return false;
            }
        }
    }
}

