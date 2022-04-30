

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreMenuViewToolBase.cs
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
file:CoreMenuViewToolBase.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IGK.ICore.Menu
{
    using IGK.ICore;using IGK.ICore.Menu;
    using IGK.ICore.Resources;
    using IGK.ICore.Tools;
    using IGK.ICore.WinUI;
    
    /// <summary>
    /// represent the base class of view menu
    /// </summary>
    public class CoreViewToolMenuBase : CoreApplicationMenu, ICoreMenuShortCutChild 
    {
        ICoreTool m_Tool;
        public ICoreTool Tool
        {
            get { return this.m_Tool; }       
        }
        public CoreViewToolMenuBase()
        {
            this.IsShortcutMenuChild = true;
        }
        void m_Tool_VisibleChanged(object sender, EventArgs e)
        {
            if (this.MenuItem != null)
                this.MenuItem.Checked = Tool.Visible;
        }
        protected override void InitMenu()
        {

            this.Enabled = false;
            this.Visible = false;
            base.InitMenu();
            if ( this.ShortCut != enuKeys.None)
            {
                this.MenuItem.ShortcutKeys = IGK.ICore.WinUI.enuKeys.None;
                this.MenuItem.ShortcutKeyDisplayString = "Ctrl+W, " + CoreResources.GetShortcutText(this.ShortCut);
                ViewKeyManagerTool.Instance.Register(this);
            }
        }
        protected override void RegisterBenchEvent(ICoreSystemWorkbench workbench)
        {
            base.RegisterBenchEvent(workbench);
            if (workbench is ICoreWorkingSurfaceHandler s)
            s.CurrentSurfaceChanged += workbench_CurrentSurfaceChanged;
        }
        protected override void UnregisterBenchEvent(ICoreSystemWorkbench workbench)
        {
            base.UnregisterBenchEvent(workbench);
            if (workbench is ICoreWorkingSurfaceHandler s)
            s.CurrentSurfaceChanged -= workbench_CurrentSurfaceChanged;
        }
        void workbench_CurrentSurfaceChanged(object o, CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            OnSurfaceChanged(EventArgs.Empty);
        }
        protected virtual void OnSurfaceChanged(EventArgs eventArgs)
        {
            this.SetupEnableAndVisibility();
        }
        protected override bool PerformAction()
        {
            var l = this.Workbench.GetLayoutManager();            
            if ((l!=null) && (this.Tool != null) && (!this.Tool.Visible))
                l.ShowTool(this.Tool);
            return false;
        }
        /// <summary>
        /// get a shortcut child
        /// </summary>
        public ICoreMenuMessageShortcutContainer ShortCutContainer
        {
            get { return ViewKeyManagerTool.Instance; }
        }
        protected  CoreViewToolMenuBase(CoreToolBase tool):base()
        {
            if (tool == null)
                throw new ArgumentNullException("tool");
            this.m_Tool = tool;
            this.m_Tool.VisibleChanged += new EventHandler(m_Tool_VisibleChanged);
        }
    }
}

