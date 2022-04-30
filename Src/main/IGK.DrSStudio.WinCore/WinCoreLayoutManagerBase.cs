

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinCoreLayoutManagerBase.cs
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
file:WinCoreLayoutManagerBase.cs
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
using System.Text;
using System.Windows.Forms;
using IGK.ICore.WinCore;
using IGK.DrSStudio;
namespace IGK.DrSStudio.WinUI
{
    using IGK.DrSStudio.WinUI;
    
    using IGK.DrSStudio.Tools;
    using IGK.DrSStudio.Menu;
    using IGK.ICore.WinUI;
    using IGK.ICore;
    using IGK.ICore.Tools;
    [Serializable ()]
    /// <summary>
    /// represent the base layout manager . for winui interface
    /// </summary>
    public class WinCoreLayoutManagerBase :  
        CoreLayoutManagerBase,
        ICoreWorkbenchLayoutManager 
    {
        private ToolStripPanel m_TopPanel;
        private ToolStripPanel m_BottomPanel;
        private ToolStripPanel m_RightPanel;
        private ToolStripPanel m_LeftPanel;
        private IXCoreSurfaceContainer c_surfaceContainer;
        private List<Control> m_registratedContextControls;
        /// <summary>
        /// get the current surface container
        /// </summary>
        public IXCoreSurfaceContainer SurfaceContainer
        {
            get
            {
                return c_surfaceContainer;
            }
            protected set {
                this.c_surfaceContainer = value;
            }
        }
        public override  void Dispose()
        {
            //free manager

        }
        public ToolStripPanel LeftPanel
        {
            get { return m_LeftPanel; }
        }
        public ToolStripPanel RightPanel
        {
            get { return m_RightPanel; }
        }
        public ToolStripPanel BottomPanel
        {
            get { return m_BottomPanel; }
        }
        public ToolStripPanel TopPanel
        {
            get { return m_TopPanel; }
        }
        #region ICoreWorkbenchLayoutManager Members
        public override  void InitLayout()
        {
            this.Workbench.MainForm.SuspendLayout();
            this.GenerateMainContain();
            this.GenerateTool();
            this.GenerateToolStripPanel();
            this.GenerateMenu();
            this.GenerateContextMenu();
            //init caption bar 
            Control ctr = this.CreateCaptionBar() as Control ;
            if (ctr != null)
            {
                ctr.Dock = DockStyle.Top;
                this.Workbench.MainForm.Controls.Add(ctr);
            }            
            if (this.StatusControl == null)
                throw new CoreException(CoreConstant.ERR_LAYOUTMANAGER_STATUSREQUIRE);
            this.Workbench.MainForm.Controls.Add(this.StatusControl);            
            this.Workbench.MainForm.ResumeLayout();            
        }
        /// <summary>
        /// initialize all tool
        /// </summary>
        protected virtual void GenerateTool()
        {
            CoreWorkbenchUtility.InitTools(this.Workbench);
        }
        protected  virtual void GenerateToolStripPanel()
        {
            this.m_BottomPanel = CreateToolStripPanel();
            this.m_RightPanel = CreateToolStripPanel();
            this.m_TopPanel = CreateToolStripPanel();
            this.m_LeftPanel = CreateToolStripPanel();
            this.m_BottomPanel.Dock = DockStyle.Bottom;
            this.m_LeftPanel.Dock = DockStyle.Left;
            this.m_RightPanel.Dock = DockStyle.Right;
            this.m_TopPanel.Dock = DockStyle.Top;
            this.Workbench.MainForm.Controls.Add(this.m_RightPanel);
            this.Workbench.MainForm.Controls.Add(this.m_LeftPanel );
            this.Workbench.MainForm.Controls.Add(this.m_TopPanel);
            this.Workbench.MainForm.Controls.Add(this.m_BottomPanel);
        }
        protected virtual void GenerateMainContain()
        {
            IXCoreSurfaceContainer c = this.CreateSurfaceContainer();
            if (c != null)
            {
                this.c_surfaceContainer = c;
                this.Workbench.MainForm.Controls.Add(c);
                new WinCoreSurfaceContainerManager(this, this.c_surfaceContainer);
            }
        }
        /// <summary>
        /// overrride this to generate workbench context menu
        /// </summary>
        protected override void GenerateContextMenu()
        {
            ICoreContextMenu v_mainContextMenu = CreateContextMenu();
            if (v_mainContextMenu == null)
                return;            
            ICoreContextMenuCollections v_contextMenu = CoreSystem.GetContextMenus();
            v_contextMenu.Sort();
            this.InitContextMenu(v_mainContextMenu, CoreSystem.GetContextRootMenus());          
            this.Workbench.MainForm.AppContextMenu  = v_mainContextMenu;
        }
      
        /// <summary>
        /// override this to generate your menu
        /// </summary>
        protected override void GenerateMenu()
        {
            ICoreMenu v_mainMenu = CreateMenu();
            if (v_mainMenu == null)
                return;
            //generateMenu
            CoreSystem.GetMenus ().GenerateMenu (v_mainMenu, this.Workbench);
            //set the menu strip
            this.Workbench.MainForm.AppMenu  = v_mainMenu;
            ICoreMenuHostControl ctr = this.CreateMenuContainer();
            if (ctr != null)
            {
                ctr.SuspendLayout();
                ctr.Add(v_mainMenu);
                ctr.ResumeLayout();
                this.Workbench.MainForm.Controls.Add(ctr);
            }
            else
                this.Workbench.MainForm.Controls.Add(v_mainMenu);
        }
        #endregion
        internal protected WinCoreLayoutManagerBase(ICoreApplicationWorkbench workbench):base(workbench )
        {
            this.Workbench.CurrentSurfaceChanged += m_Workbench_CurrentSurfaceChanged;
            this.Workbench.MainForm.AppContextMenuChanged += _AppContextMenuChanged;
            RegisterContextControls();
        }

        private void _AppContextMenuChanged(object sender, EventArgs e)
        {
            RegisterContextControls();
            
        }

        private void RegisterContextControls()
        {
            //registert application context menu
            if (this.m_registratedContextControls == null)
                return;

            ContextMenuStrip r = this.Workbench.MainForm.AppContextMenu as ContextMenuStrip;
            foreach (Control item in this.m_registratedContextControls)
            {
                item.ContextMenuStrip = r;
            }
        }
        void m_Workbench_CurrentSurfaceChanged(object o, CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            //unregister surface event
            if (e.OldElement is ICoreWorkingFilemanagerSurface)
            {
                ICoreWorkingFilemanagerSurface v_s = e.OldElement as ICoreWorkingFilemanagerSurface;
                v_s.NeedToSaveChanged -= new EventHandler(v_s_NeedToSaveChanged);
                v_s.FileNameChanged -= new EventHandler(v_s_FileNameChanged);
            }
            else if (e.OldElement is ICoreWorkingRecordableSurface)
            {
                ICoreWorkingRecordableSurface v_s = e.OldElement as ICoreWorkingRecordableSurface;
                v_s.NeedToSaveChanged -= new EventHandler(v_s_NeedToSaveChanged);
            }
            //register surface event
            if (e.NewElement is ICoreWorkingFilemanagerSurface)
            {
                ICoreWorkingFilemanagerSurface v_s = e.NewElement as ICoreWorkingFilemanagerSurface;
                v_s.NeedToSaveChanged += new EventHandler(v_s_NeedToSaveChanged);
                v_s.FileNameChanged +=new EventHandler(v_s_FileNameChanged);
            }
            else if (e.NewElement is ICoreWorkingRecordableSurface)
            {
                ICoreWorkingRecordableSurface v_s = e.NewElement as ICoreWorkingRecordableSurface;
                v_s.NeedToSaveChanged += new EventHandler(v_s_NeedToSaveChanged);
            }
            SetUpTitle();
        }
        void v_s_NeedToSaveChanged(object sender, EventArgs e)
        {
            this.SetUpTitle();
        }
        void v_s_FileNameChanged(object sender, EventArgs e)
        {
            this.SetUpTitle();
        }
        /// <summary>
        /// setup the main form title
        /// </summary>
        protected void SetUpTitle()
        {
            if (this.Workbench.CurrentSurface != null)
            {
                //string v_g = string.Empty;
                //v_g = this.Workbench.CurrentSurface.Title;
                //if (this.Workbench.CurrentSurface is ICoreWorkingRecordableSurface)
                //{
                //    ICoreWorkingRecordableSurface s = (this.Workbench.CurrentSurface as ICoreWorkingRecordableSurface);
                //    if (s.NeedToSave)
                //        v_g += CoreConstant.SURFACE_CHANGED_CHAR;
                //}
                //this.Workbench.MainForm.Title =CoreConstant.APP_MAINFORM_SURFACE_TITLE_2.RFormat (CoreConstant.VERSION, v_g);
                this.Workbench.CurrentSurface.AppContextMenu = this.Workbench.MainForm.AppContextMenu;
                this.Environment = this.Workbench.CurrentSurface.SurfaceEnvironment;
            }
            else
            {
               // this.Workbench.MainForm.Title = CoreConstant.APP_MAINFORM_TITLE_1.RFormat (CoreConstant.VERSION);
                this.Environment = null;
            }
        }
        /// <summary>
        /// override this method to create a caption bar
        /// </summary>
        /// <returns></returns>
        protected virtual IXCoreCaptionBarControl CreateCaptionBar()
        {
            return null;
        }
        protected virtual ICoreMenuHostControl CreateMenuContainer()
        {
            XWinCoreMenuHost c =  new XWinCoreMenuHost();
            c.Dock = DockStyle.Top;
            return c;
        }
        public override ICoreMenu CreateMenu()
        {
            return new IGKXWinCoreMenu();
        }
        public override  ICoreContextMenu CreateContextMenu() {
            return new IGKXContextMenuStrip ();
        }
        /// <summary>
        /// override the creation of status tools
        /// </summary>
        /// <returns></returns>
        public override IXCoreStatus CreateStatusContainer()
        {
            return new IGKXWinCoreStatus();
        }
     
        protected virtual ToolStripPanel CreateToolStripPanel()
        {
            return new ToolStripPanel();
        }
        public virtual ToolStripMenuItem CreateToolStripMenuItem()
        {
            return new ToolStripMenuItem();
        }
        public virtual ICoreColorPicker CreateColorPicker(){
            return null;
        }
        public virtual ICoreBrushPicker CreateBrushPicker()
        {
            return null;
        }
        public override IXCoreMenuItemContainer CreateMenuItem()
        {
            return new IGKXWinCoreMenuItem();
        }
        public IXCoreMenuItem CreateToolItemSeparator()
        {
            return null;
        }
        protected override IXCoreSurfaceContainer CreateSurfaceContainer()
        {
 	        return new XWinCoreSurfaceContainer(this);
        }
        public override ICoreControl CreateControl(Type interfaceType)
        {
            
            return null;
        }
        //public abstract  void ShowTool(ICoreTool tool);
        //public abstract  void HideTool(ICoreTool tool);
        public override IXCoreContextMenuItemContainer CreateContextMenuItem()
        {
            return new IGKXContextMenuItem();
        }

        public override void ShowTool(ICoreTool tool)
        {
            CoreMessageBox.Show("Can't show tool");
        }

        public override void HideTool(ICoreTool tool)
        {
            CoreMessageBox.Show("Can't hide tool");
        }

       
        /// <summary>
        /// registe to context menu
        /// </summary>
        /// <param name="control"></param>
        public void RegisterToContextMenu(Control control)
        {
            if (control == null)
                return;
            if (m_registratedContextControls == null)
                m_registratedContextControls = new List<Control>();
            if (this.Workbench.MainForm.AppContextMenu !=null)
            {
                control.ContextMenuStrip = this.Workbench.MainForm.AppContextMenu
                    as ContextMenuStrip;
            }
            if (!this.m_registratedContextControls.Contains(control))
            {
                this.m_registratedContextControls.Add(control);
                control.Disposed += control_Disposed;
            }
        }

        void control_Disposed(object sender, EventArgs e)
        {
            //unregister controller for free memory usage
            this.m_registratedContextControls.Remove(sender as Control);
        }

        public override void RegisterToContextMenu(ICoreControl ctr)
        {
            this.RegisterToContextMenu(ctr as Control);
        }
    }
}

