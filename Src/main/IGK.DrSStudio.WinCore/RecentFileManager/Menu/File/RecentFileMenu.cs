

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: RecentFileMenu.cs
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
file:RecentFileMenu.cs
*/

using IGK.ICore.WinCore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Menu.File
{
    using IGK.DrSStudio.Menu;
    using IGK.DrSStudio.Settings;
    using IGK.DrSStudio.WinUI;
    using IGK.ICore;

    abstract class RecentFileMenuBase : CoreApplicationMenu
    {
        protected void SetWorkbench(ICoreWorkbench workbench)
        {
            this.Workbench = workbench;
        }
    }
    [DrSStudioMenu("File.RecentFile", 0x80)]
    class _RecentFileMenu : RecentFileMenuBase 
    {
        public _RecentFileMenu()
        {
        }
        protected override void InitMenu()
        {
            base.InitMenu();
            this.BuildMenu();
            this.SetupEnableAndVisibility();
        }
        protected override void OnWorkbenchChanged(EventArgs eventArgs)
        {
            base.OnWorkbenchChanged(eventArgs);
        }
        protected override void RegisterBenchEvent(ICoreSystemWorkbench workbench)
        {
            base.RegisterBenchEvent(workbench);
            ICoreFileManagerWorkbench fb = workbench as ICoreFileManagerWorkbench ;
            if (fb != null)
            {
                fb.FileOpened += Workbench_FileOpened;
            }
        }
        protected override void UnregisterBenchEvent(ICoreSystemWorkbench workbench)
        {
            ICoreFileManagerWorkbench fb = workbench as ICoreFileManagerWorkbench;
            if (fb != null)
            {
                fb.FileOpened -= Workbench_FileOpened;
            }
            base.UnregisterBenchEvent(workbench);
        }
        void Workbench_FileOpened(object sender, CoreWorkingFileOpenEventArgs e)
        {
            var t = RecentFileSetting.Instance.Files;

            RecentFileSetting.Instance.Add(e.Path);

            if (RecentFileSetting.Instance.Files.Length < t.Length)
            {
                this.BuildMenu();
            }
            this.SetupEnableAndVisibility();
        }
        protected override bool IsVisible()
        {
            return (this.Childs.Count > 0);
        }
        protected override bool IsEnabled()
        {
            return this.IsVisible();
        }
        private void BuildMenu()
        {
            this.Childs.Clear();
            string[] v_files = RecentFileSetting.Instance.Files;
            if (v_files.Length > 0)
            {
                int i = 0;
                foreach (string s in v_files )
                {
                    RecentFileMenuItem m = new RecentFileMenuItem(s);                 
                    this.Childs.Add(m);
                    m.MenuItem.Text = s;
                    i++;
                }
                //build recent file submenu
                string v_id = this.Id + ".ClearRecentFile";
                CoreMenuActionBase c = CoreSystem.GetMenuAction (v_id) as CoreMenuActionBase ;
                if (c == null)
                {
                    c = new ClearRecentFileListMenu(this);
                    //register a single clear file menu list
                    CoreMenuAttribute v_attr = new CoreMenuAttribute(v_id, short.MaxValue)
                    {
                        SeparatorBefore = true
                    };
                    c.SetAttribute(v_attr);
                    if (!this.Register(v_attr, c))
                    {
                        CoreLog.WriteDebug($"{nameof(ClearRecentFileListMenu)} Not Registrated");
                    }
                }
                this.Childs.Add(c);
            }
        }
        /// <summary>
        /// recent file menu item
        /// </summary>
        class RecentFileMenuItem : RecentFileMenuBase 
        {
            private string m_recentFileName;

             public new   ICoreFileManagerWorkbench Workbench{
                 get{
                     return base.Workbench as ICoreFileManagerWorkbench ;
                 }
             }
             protected override bool IsEnabled()
             {
                 return this.Workbench != null;
             }
            public RecentFileMenuItem(string filename)
            {
                this.m_recentFileName = filename;
            }
            protected override bool PerformAction()
            {
                this.Workbench.OpenFile(this.m_recentFileName);
                return base.PerformAction();
            }
            protected override void InitMenu()
            {
                base.InitMenu();
            }
        }

        /// <summary>
        /// Clear Recent File List
        /// </summary>
        class ClearRecentFileListMenu : CoreApplicationMenu 
        {
            private _RecentFileMenu _RecentFileMenu;
            public ClearRecentFileListMenu(_RecentFileMenu _RecentFileMenu)
            {                
                this._RecentFileMenu = _RecentFileMenu;
            }
            protected override bool PerformAction()
            {
                if (RecentFileSetting.Instance.Files.Length > 0)
                {
                    RecentFileSetting.Instance.Clear();
                    this._RecentFileMenu.BuildMenu();
                    this._RecentFileMenu.SetupEnableAndVisibility();
                    return true;
                }
                return false;
            }
        }
    }
}

