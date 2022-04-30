

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinLauncherLayoutManager.cs
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
file:WinLauncherLayoutManager.cs
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
using System.Windows.Forms ;
namespace IGK.DrSStudio.WinLauncher
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.Tools;
    using IGK.DrSStudio.Settings;
    using IGK.DrSStudio.Actions;
    using IGK.DrSStudio.WinLauncher.Actions;
    using IGK.DrSStudio.WinLauncher.Tools;
    using IGK.DrSStudio.WinUI.MainFormDisposition;
    public class WinLauncherLayoutManager : 
        IGK.DrSStudio.LayoutManager.LayoutManagerBase,
        IDockingManager
    {
        private XMainFormCaptionBar m_captionBar;        
        private DrSStartSurface m_startSurface;//start up surface
        private XLayoutManagerSurfaceContainer m_surfaceContainer; // the surface container
        private Dictionary<ICoreTool, Dictionary<string, WinLauncherToolPanelProperty>> m_panelProperties;
        private WinLauncherDockingPageCollections m_pages;
        internal XDockingPanel c_panLeft;
        internal XDockingPanel c_panRight;
        internal XDockingPanel c_panBottom;
        private bool m_lLayoutSuspended;
       /* private GlobalApplicationMessageFilter m_GlobalAction;
        public GlobalApplicationMessageFilter GlobalAction
        {
            get
            {                
                return  m_GlobalAction ;
            }
        }
        IDockingPanelCollections  IDockingManager.Panels {
            get {
                return m_pages;
            }
        }
        public event EventHandler LoadingToolComplete;
        /// <summary>
        /// override generate  tool specification
        /// </summary>
        protected override void GenerateTool()
        {
            base.GenerateTool();
            if (LoadingToolComplete != null)
                LoadingToolComplete(this, EventArgs.Empty);
        }*/
        /// <summary>
        /// get the surface container
        /// </summary>
        public XLayoutManagerSurfaceContainer SurfaceContainer {
            get {
                return this.m_surfaceContainer;
            }
        }
        /// <summary>
        /// get the startup surface
        /// </summary>
        public ICoreWorkingSurface StartSurface
        {
            get { return this.m_startSurface; }
        }
        internal void ResumeLayout()
        {
            this.MainForm.ResumeLayout ();
            m_lLayoutSuspended = false;
        }
        internal void SuspendLayout()
        {
            this.MainForm.SuspendLayout ();
            this.m_lLayoutSuspended = true;
        }
        internal  WinLauncherLayoutManager(WinLauncherWorkBench workbench):
            base(workbench)
        {
            m_startSurface = new DrSStartSurface();
            this.m_pages = new WinLauncherDockingPageCollections(this);           
            StartDocumentManager.Instance.ManagerForm.WebBrowser = m_startSurface.WebBrowser;
            m_surfaceContainer = new XLayoutManagerSurfaceContainer();
            m_GlobalAction = new GlobalApplicationMessageFilter (this);
            m_surfaceContainer.LayoutManager = this;
            workbench.SurfaceRemoved += new IGK.DrSStudio.CoreWorkingSurfaceEventHandler(workbench_SurfaceRemoved);
            workbench.SurfaceAdded += new IGK.DrSStudio.CoreWorkingSurfaceEventHandler(workbench_SurfaceAdded);
            workbench.CurrentSurfaceChanged += new IGK.DrSStudio.CoreWorkingSurfaceChangedEventHandler(workbench_CurrentSurfaceChanged);
            workbench.ActionRegister.AddFilterMessage(this.GlobalAction);
            m_startSurface.WebBrowser.ScrollBarsEnabled = false;
        }
        public override void InitLayout()
        {
            base.InitLayout();
            this.RegisterGlobalMenu();
        }
        protected virtual  void RegisterGlobalMenu()
        {
            this.GlobalAction.Add(Keys.LWin, new MainForm_WindowKey());
            this.GlobalAction.Add(Keys.Left, new MainForm_DockToLeft());
            this.GlobalAction.Add(Keys.Right , new MainForm_DockToRight());
            this.GlobalAction.Add(Keys.Up , new MainForm_DockToUp());
            this.GlobalAction.Add(Keys.Down, new MainForm_DockToDown());
            this.GlobalAction.Add(Keys.Shift | Keys.Up , new MainForm_Maximize ());
            this.GlobalAction.Add(Keys.Shift | Keys.Down, new MainForm_Reduce ());
        }
        void workbench_CurrentSurfaceChanged(object o, IGK.DrSStudio.CoreWorkingSurfaceChangedEventArgs e)
        {
            this.m_surfaceContainer.AddSurface(e.NewSurface);
        }
        void workbench_SurfaceAdded(object o, IGK.DrSStudio.CoreItemEventArgs<ICoreWorkingSurface> e)
        {            
            if (Workbench.Surfaces.Count > 0)
            {
                this.m_surfaceContainer.Visible = true;                
                this.Workbench.CurrentSurface = e.Surface;
            }
        }
        void workbench_SurfaceRemoved(object o, IGK.DrSStudio.CoreItemEventArgs<ICoreWorkingSurface> e)
        {
            this.m_surfaceContainer.RemoveSurface(e.Surface);
            if (Workbench.Surfaces.Count == 0)
            {
                //show default rendering
                this.m_surfaceContainer.Visible = false;                
            }
            if (e.Surface != this.m_startSurface)
            {
                CoreLog.WriteDebug("Dispose Surface Layout");
                e.Surface.Dispose();
            }
        }
        protected override System.Windows.Forms.Control CreateCaptionBar()
        {
            this.m_captionBar =  new XMainFormCaptionBar();
            this.Workbench.MainForm.TitleChanged += new EventHandler(MainForm_TitleChanged);
            return this.m_captionBar;
        }
        public override ToolStripMenuItem CreateToolStripMenuItem()
        {
            return new XToolStripMenuItem();
        }
        protected override ToolStripPanel CreateToolStripPanel()
        {
            return new XToolStripPanel();
        }
        void MainForm_TitleChanged(object sender, EventArgs e)
        {
            this.m_captionBar.Invalidate();
        }
        protected override Control CreateMenuContainer()
        {
            return new XRibonBar();
        }
        protected override MenuStrip CreateMenuStrip(){
            return  new XMainMenuStrip();
        }
        protected override void GenerateMainContain()
        {
            MainForm.Controls.Add(this.m_surfaceContainer);
           c_panBottom = new XDockingPanel(this);
           c_panRight = new XDockingPanel(this);
           c_panLeft = new XDockingPanel(this);
           c_panBottom.Dock = DockStyle.Bottom;
           c_panLeft.Dock = DockStyle.Left;
           c_panRight.Dock = DockStyle.Right;
           c_panBottom.Visible = false;
           c_panLeft.Visible = false;
           c_panRight.Visible = false;
           this.MainForm.Controls.Add(c_panBottom);
           this.MainForm.Controls.Add(c_panLeft);
           this.MainForm.Controls.Add(c_panRight);
           this.m_surfaceContainer.Visible = false;
           this.Workbench.Surfaces.Add(this.m_startSurface);
        }
        protected override void OnToolAdded(CoreToolEventArgs e)
        {
            if (this.m_lLayoutSuspended) return;
            base.OnToolAdded(e);
        }
        protected override void OnToolRemoved(CoreToolEventArgs e)
        {
            if (this.m_lLayoutSuspended) return;
            base.OnToolRemoved(e);
        }
        internal void ShowTool(WinLauncherToolPanelProperty pan)
        {
            WinLauncherToolPanelProperty prop = pan;
            if (prop == null)
                return;
            if (prop.IsToolStrip)
            {
                switch (prop.ToolDisplay)
                {
                    case enuLayoutToolDisplay.Left:
                        this.LeftPanel.Controls.Add(prop.Tool.HostedControl as ToolStrip);
                        prop.Tool.HostedControl.Visible = true;
                        break;
                    case enuLayoutToolDisplay.Right:
                        this.RightPanel.Controls.Add(prop.Tool.HostedControl as ToolStrip);
                        prop.Tool.HostedControl.Visible = true;
                        break;
                    case enuLayoutToolDisplay.Top:
                        this.TopPanel.Controls.Add(prop.Tool.HostedControl as ToolStrip);
                        prop.Tool.HostedControl.Visible = true;
                        break;
                    case enuLayoutToolDisplay.Bottom:
                        this.BottomPanel.Controls.Add(prop.Tool.HostedControl as ToolStrip);
                        prop.Tool.HostedControl.Visible = true;
                        break;
                }
            }
            else
            {
                switch (prop.ToolDisplay)
                {
                    case enuLayoutToolDisplay.Top:
                        break;
                    case enuLayoutToolDisplay.Bottom:
                        this.c_panBottom.Pages.AddPage(prop.Page);
                        this.c_panBottom.SelectedPage = prop.Page;
                        break;
                    case enuLayoutToolDisplay.Left:
                        this.c_panLeft.Pages.AddPage(prop.Page);
                        this.c_panLeft.SelectedPage = prop.Page;
                        break;
                    case enuLayoutToolDisplay.Right:
                        this.c_panRight.Pages.AddPage(prop.Page);
                        this.c_panRight.SelectedPage = prop.Page;
                        break;
                    case enuLayoutToolDisplay.Float:
                        //add page do document panel
                        if (prop.Form == null)
                        {
                            prop.InitNewForm();
                        }
                        if (!prop.Form.DockingPanel.Pages.Contains(prop.Page))
                            prop.Form.DockingPanel.Pages.AddPage(prop.Page);
                        prop.Form.Show();
                        prop.Tool.HostedControl.Visible = true;
                        break;
                    default:
                        break;
                }
            }
            this.OnToolAdded(new CoreToolEventArgs(prop.Tool));
        }
        public override void ShowTool(ICoreTool tool)
        {
            if ((tool.HostedControl == null)||(!tool.CanShow ))
                return;
            ShowTool(GetPanelProperty(tool));            
        }
        internal WinLauncherToolPanelProperty GetPanelProperty(ICoreTool tool)
        {
            return GetPanelProperty(tool, this.Environment);
        }
        internal WinLauncherToolPanelProperty GetPanelProperty(ICoreTool tool, string EnvironmentName)
        {
            if (m_panelProperties == null)
                m_panelProperties = new Dictionary<ICoreTool, Dictionary<string, WinLauncherToolPanelProperty>>();
            if (m_panelProperties.ContainsKey(tool))
            {
                if (m_panelProperties[tool].ContainsKey(EnvironmentName))
                {
                    return m_panelProperties[tool][EnvironmentName];
                }
                else
                {
                    WinLauncherToolPanelProperty p = new WinLauncherToolPanelProperty(this, tool, EnvironmentName);
                    m_panelProperties[tool].Add(EnvironmentName, p);
                    return p;
                }
            }
            else
            {
                m_panelProperties.Add(tool, new Dictionary<string, WinLauncherToolPanelProperty>());
                WinLauncherToolPanelProperty p = new WinLauncherToolPanelProperty(this, tool, EnvironmentName);
                m_panelProperties[tool].Add(EnvironmentName, p);
                return p;
            }
        }
        public override void HideTool(ICoreTool tool)
        {
            this.HideTool(GetPanelProperty(tool));
            //EnvironmentToolManager.PanelToolManager man = EnvironmentToolManager.GetEnvironment(this.Environment);
            //if (man != null)
            //{
            //    if (!man.Contains(tool))
            //    {
            //        //just hidde don't raise event
            //        return;
            //    }
            //    HideTool(man[tool]);
            //    OnToolRemoved(new CoreToolEventArgs(tool));
            //}
            //else {
            //    string n = this.Workbench.CurrentSurface.GetType().AssemblyQualifiedName;
            //        CoreLog.WriteDebug ("No Environment Name for : "+n);
            //}
        }
        //internal PanelProperty CreateNewDefaultPanelProperty(ICoreTool tool)
        //{
        //    if (tool.HostedControl == null)
        //        return null;
        //    if (!tool.CanShow)
        //        return null;
        //    PanelProperty  prop = new PanelProperty(tool);
        //    prop.Page = new XDockingPage(tool, this);           
        //    return prop;
        //}
        //private void InitDefaultTool(ICoreTool tool)
        //{
        //    PanelProperty prop = CreateNewDefaultPanelProperty(tool);
        //    //EnvironmentToolManager.PanelToolManager man = EnvironmentToolManager.GetEnvironment(this.Environment);
        //    //man.Add(tool, prop);
        //    //if (tool.HostedControl is XToolStrip)
        //    //{
        //    //    XToolStrip strip = tool.HostedControl as XToolStrip;
        //    //    strip.Visible = false;
        //    //    //default panel
        //    //    //-------------
        //    //    this.TopPanel.Controls.Add(strip);
        //    //    prop.ToolDisplay = enuLayoutToolDisplay.TopToolStripPanel;
        //    //    strip.Visible = true;
        //    //}
        //    //else
        //    //{
        //    //    //create a new panel property                
        //    //    this.c_panLeft.Pages.AddPage(prop.Page);
        //    //    prop.Page.ToolDisplay = enuLayoutToolDisplay.Left;
        //    //    prop.ToolDisplay = enuLayoutToolDisplay.Left;
        //    //}
        //    OnToolAdded(new LayoutManagerPanelPropertyEventArgs(tool, prop));                
        //}
        internal  void HideTool(WinLauncherToolPanelProperty prop)
        {
            if (prop == null)
                return;
            if (prop.IsToolStrip)
            {
                switch (prop.ToolDisplay)
                {
                    case enuLayoutToolDisplay.Bottom:
                    case enuLayoutToolDisplay.Top:
                    case enuLayoutToolDisplay.Left:
                    case enuLayoutToolDisplay.Right:
                        ToolStrip c = (prop.Tool.HostedControl as ToolStrip);
                        if ((c != null) && (c.Parent != null))
                        {
                            c.Parent.Controls.Remove(c);
                            c.Visible = false;
                        }
                        break;
                }
            }
            else
            {
                switch (prop.ToolDisplay)
                {
                    case enuLayoutToolDisplay.Top:
                    case enuLayoutToolDisplay.Bottom:
                    case enuLayoutToolDisplay.Left:
                    case enuLayoutToolDisplay.Right:
                        if (prop.Panel != null)
                        {
                            prop.Panel.Pages.RemovePage(prop.Page);
                        }
                        prop.Tool.Visible = false;
                        break;
                    case enuLayoutToolDisplay.Float:
                        if (prop.Form != null)
                            prop.Form.Hide();
                        break;
                    default:
                        break;
                }
            }
            OnToolRemoved(new CoreToolEventArgs(prop.Tool));
        }
        protected override ICoreContextMenuStrip CreateContextMenuStrip()
        {            
            return new WinLauncherContextMenuStrip(this, CoreSystem.Instance.ContextMenus);
        }
        protected override StatusStrip CreateStatusStrip()
        {
            StatusStrip t = new XStatusStrip();            
            XToolStripStatusLabel v_lb = new XToolStripStatusLabel ();
                t.Items.Add(v_lb);
            v_lb.Text = CoreSystem.GetString("Help");//) as ToolStripStatusLabel;
            v_lb.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            v_lb.Name = "Help";
            v_lb.Spring = true;               
            return t;
        }
         /// <summary>
        /// show start page
        /// </summary>
        public  void ShowStartPage()
        {
            if (!this.Workbench.Surfaces.Contains(this.m_startSurface))
            {
                this.Workbench.Surfaces.Add(this.m_startSurface);
            }
            else {
                this.Workbench.CurrentSurface = this.m_startSurface;
                if (this.m_startSurface.DocBaseUrl != this.m_startSurface.DocUrl)
                {
                    this.m_startSurface.WebBrowser.Url = new Uri (this.m_startSurface.DocBaseUrl);
                }
            }
        }
        public IDockingForm CreateNewToolForm()
        {
            return new XDockingForm(this);
        }
        public override void Refresh()
        {
            base.Refresh();            
        }
        /// <summary>
        /// represent property that store a page and the attached tools.
        /// </summary>
        public class WinLauncherToolPanelProperty : IGK.DrSStudio.WinUI.ILayoutPanelPageProperty
        {
            private XDockingPage m_Page;
            private ICoreTool m_tool;
            private WinLauncherLayoutManager m_lmanager;
            private enuLayoutToolDisplay m_layoutToolDisplay;
            private XForm m_DockedForm;
            private string m_EnvironmentName;
            /// <summary>
            /// get the environement name of this panel property
            /// </summary>
            public string EnvironmentName
            {
                get { return m_EnvironmentName; }             
            }
            /// <summary>
            /// return the docked form if this PanelProperty is type float
            /// </summary>
            public XForm DockedForm {
                get {
                    return m_DockedForm;
                }
            }
            public bool IsToolStrip {
                get {
                    return (m_tool.HostedControl is ToolStrip);
                }
            }
            public bool IsDockingHostedControl {
                get {
                    return !IsToolStrip;
                }
            }
            internal  WinLauncherToolPanelProperty(WinLauncherLayoutManager  lmanager,  ICoreTool tool, string EnvironmentName)
            {
                //if (tool.HostedControl == null)
                //    throw new ArgumentNullException("Null Exception");
                this.m_tool = tool;
                this.m_lmanager = lmanager;
                this.m_EnvironmentName = EnvironmentName;
                this.m_Page = null;
                this.m_DockedForm = null;
                if (this.IsToolStrip)
                    this.m_layoutToolDisplay = enuLayoutToolDisplay.Top;
                else
                {
                    this.m_layoutToolDisplay = enuLayoutToolDisplay.Left;
                    this.m_Page = new XDockingPage(tool, this.m_lmanager);
                    this.m_Page.ToolDisplayChanged += m_Page_ToolDisplayChanged;
                }
            }
            void m_Page_ToolDisplayChanged(object sender, EventArgs e)
            {
                this.m_layoutToolDisplay = this.m_Page.ToolDisplay;               
                this.m_lmanager.ShowTool(this.Tool);
            }
            /// <summary>
            /// Get the tool attached to the panel property
            /// </summary>
            public ICoreTool Tool
            {
                get { return this.m_tool; }
            }
            /// <summary>
            /// get the page attached to the tool
            /// </summary>
            public XDockingPage Page
            {
                get
                {
                    return this.m_Page;
                }
                internal set
                {
                    this.m_Page = value;
                }
            }
            /// <summary>
            /// parent panel
            /// </summary>
            public IDockingPanel Panel
            {
                get
                {
                    return Page.Panel;
                }
            }
            /// <summary>
            /// get the form that will host the control tool
            /// </summary>
            public IDockingForm Form
            {
                get
                {
                    return this.m_Page.DockingForm;
                }
            }
            void m_Form_FormClosing(object sender, FormClosingEventArgs e)
            {
                switch (e.CloseReason)
                {
                    case CloseReason.ApplicationExitCall:
                        break;
                    case CloseReason.FormOwnerClosing:
                        break;
                    case CloseReason.MdiFormClosing:
                        break;
                    case CloseReason.None:
                        break;
                    case CloseReason.TaskManagerClosing:
                        break;
                    case CloseReason.WindowsShutDown:
                        break;
                    case CloseReason.UserClosing:
                    default:
                        e.Cancel = true;
                        this.m_Page.DockingForm.Hide();
                        //this.m_Page.Tool.Visible = false;
                        break;
                }
            }
            /// <summary>
            /// get or set the layout tool display
            /// </summary>
            public enuLayoutToolDisplay ToolDisplay
            {
                get { return m_layoutToolDisplay; }
                set
                {
                    m_layoutToolDisplay = value;
                    OnToolDisplayChanged(EventArgs.Empty );
                }
            }
            private void OnToolDisplayChanged(EventArgs eventArgs)
            {
                if (ToolDisplayChanged != null)
                {
                    this.ToolDisplayChanged(this, eventArgs);
                }
            }
            /// <summary>
            /// event raise when tool display changed. environment will handle it
            /// </summary>
            public event System.EventHandler ToolDisplayChanged;
            public string ToolName
            {
                get { return this.m_tool.Id; }
            }
            /// <summary>
            /// set the location of the hosted control
            /// </summary>
            public Vector2i Location
            {
                get
                {
                    if (this.m_Page == null)
                        return Vector2i.Zero;
                    switch (this.m_Page.ToolDisplay)
                    {
                        case enuLayoutToolDisplay.Float:
                            return this.m_Page.DockingForm.Location;
                    }
                    return Page.HostedControl.Location;
                }
                set
                {
                    if (this.m_Page == null)
                        return;
                    switch (this.m_Page.ToolDisplay)
                    {
                        case enuLayoutToolDisplay.Float:
                            if (this.m_Page.DockingForm == null)
                            {
                                this.InitNewForm();
                            }
                            this.m_Page.DockingForm.Location = value;
                            break;
                        case enuLayoutToolDisplay.Bottom:
                        case enuLayoutToolDisplay.Top:
                        case enuLayoutToolDisplay.Right:
                        case enuLayoutToolDisplay.Left:
                            this.m_Page.HostedControl.Location = value;
                            break;
                    }
                }
            }
            /// <summary>
            /// set the size of the hosted control
            /// </summary>
            public Size2i Size
            {
                get
                {
                    return this.Tool.HostedControl.Size;
                }
                set
                {
                    this.Tool.HostedControl.Size = value;
                }
            }
            public event EventHandler SizeChanged
            {
                add
                {
                    this.Page.HostedControl.SizeChanged += value;
                }
                remove
                {
                    this.Page.HostedControl.SizeChanged -= value;
                }
            }
            public event EventHandler LocationChanged {
                add {
                    this.Page.HostedControl.LocationChanged += value;
                }
                remove {
                    this.Page.HostedControl.LocationChanged -= value;
                }
            }
            internal void InitNewForm()
            {
                IDockingForm v_Form = (this.m_Page.LayoutManager as WinLauncherLayoutManager).CreateNewToolForm();
                v_Form.Owner = this.Page.LayoutManager.MainForm as Form;
                //m_Form.Caption =  CoreSystem.GetString ( this.Page.HostedControl.CaptionKey);
                v_Form.FormClosing += new FormClosingEventHandler(m_Form_FormClosing);                
                this.m_Page.DockingForm = v_Form;
            }
        }
        /// <summary>
        /// represent the default docking panel collection
        /// </summary>
        internal class WinLauncherDockingPageCollections : IDockingPanelCollections
        {
            WinLauncherLayoutManager m_layoutManager;
            List<IDockingPanel> m_pages;
            public WinLauncherDockingPageCollections(WinLauncherLayoutManager manager)
            {
                this.m_layoutManager = manager;
                m_pages = new List<IDockingPanel>();
            }
            public void Add(IDockingPanel page)
            {
                if (page == null)
                    throw new CoreException(enuExceptionType.ArgumentIsNull, "page");
                if (!this.m_pages.Contains(page))
                {
                    this.m_pages.Add(page);
                }
            }
            public void Remove(IDockingPanel page)
            {
                if ((page != null) && (this.m_pages.Contains(page)))
                    this.m_pages.Remove(page);
            }
            public int Count
            {
                get { return this.m_pages.Count; }
            }
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_pages.GetEnumerator();
            }
        }
    }
}

