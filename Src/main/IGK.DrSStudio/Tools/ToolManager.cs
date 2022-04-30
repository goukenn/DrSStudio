

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ToolManager.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.DrSStudio.WinUI;
using IGK.ICore;
using IGK.ICore.Resources;
using IGK.ICore.Tools;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.Tools
{
    [Serializable()]
    public class ToolManager : DrSStudioToolBase , IDockingManager 
    {
        XDockingPanel c_panLeft;
        XDockingPanel c_panTop;
        XDockingPanel c_panRight;
        XDockingPanel c_panBottom;
        private Dictionary<ICoreTool, Dictionary<string, WinLauncherToolPanelProperty>> m_panelProperties;
        private DrSStudioLayoutManager m_lmanager;
        private WinLauncherDockingPageCollections m_panelCollection;
        private bool m_configuring;


        public XDockingPanel PanLeft { get { return c_panLeft; } }
        public XDockingPanel PanRight { get { return c_panRight; } }
        public XDockingPanel PanBottom { get { return c_panBottom; } }
        public XDockingPanel PanTop { get { return c_panTop; } }

        public  ToolManager(DrSStudioLayoutManager lmanager)
        {
            this.m_lmanager = lmanager;
            this.m_panelCollection = new WinLauncherDockingPageCollections(lmanager);
        }

       
        public  IDockingForm CreateToolForm()
        {
            IDockingForm c =  m_lmanager.CreateDockingToolForm();         
            return c;
        }
    

        internal void HideTool(DrSStudioLayoutManager layoutManager, ICoreTool tool)
        {
            this.HideTool(GetPanelProperty(tool));
        }

        internal void HideTool(WinLauncherToolPanelProperty panProperty)
        {
            if (panProperty == null)
                return;
            if (panProperty.IsToolStrip)
            {
                switch (panProperty.ToolDisplay)
                {
                    case enuLayoutToolDisplay.Bottom:
                    case enuLayoutToolDisplay.Top:
                    case enuLayoutToolDisplay.Left:
                    case enuLayoutToolDisplay.Right:
                        ToolStrip c = (panProperty.Tool.HostedControl as ToolStrip);
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
                switch (panProperty.ToolDisplay)
                {
                    case enuLayoutToolDisplay.Top:
                    case enuLayoutToolDisplay.Bottom:
                    case enuLayoutToolDisplay.Left:
                    case enuLayoutToolDisplay.Right:
                        if (panProperty.Panel != null)
                        {
                            panProperty.Panel.Pages.RemovePage(panProperty.Page);
                        }
                        panProperty.Tool.Visible = false;
                        break;

                    case enuLayoutToolDisplay.Float:
                        if (panProperty.Form != null)
                            panProperty.Form.Hide();
                        break;
                    default:
                        break;
                }
            }
            this.m_lmanager.RaiseToolRemoved(
                new CoreItemEventArgs<CoreToolBase>(
                    panProperty.Tool as CoreToolBase));
        }

        /// <summary>
        /// show tool
        /// </summary>
        /// <param name="layoutManager"></param>
        /// <param name="tool"></param>
        public  void ShowTool(DrSStudioLayoutManager layoutManager, ICoreTool tool)
        {
            if ((tool.HostedControl == null) || (!tool.CanShow))
                return;
            ShowTool(GetPanelProperty(tool));      
        }
        /// <summary>
        /// show tool
        /// </summary>
        /// <param name="panProperty"></param>
        internal void ShowTool(WinLauncherToolPanelProperty panProperty)
        {
            WinLauncherToolPanelProperty prop = panProperty;
            if (prop == null)
                return;
            if (prop.IsToolStrip)
            {
                switch (prop.ToolDisplay)
                {
                    case enuLayoutToolDisplay.Left:
                        this.m_lmanager.LeftPanel.Controls.Add(prop.Tool.HostedControl as ToolStrip);
                        prop.Tool.HostedControl.Visible = true;
                        break;
                    case enuLayoutToolDisplay.Right:
                        this.m_lmanager.RightPanel.Controls.Add(prop.Tool.HostedControl as ToolStrip);
                        prop.Tool.HostedControl.Visible = true;
                        break;
                    case enuLayoutToolDisplay.Top:
                        this.m_lmanager.TopPanel.Controls.Add(prop.Tool.HostedControl as ToolStrip);
                        prop.Tool.HostedControl.Visible = true;
                        break;
                    case enuLayoutToolDisplay.Bottom:
                        this.m_lmanager.BottomPanel.Controls.Add(prop.Tool.HostedControl as ToolStrip);
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
                        if ((prop.Form == null) ||(prop.Form .IsDisposed ))
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
            this.m_lmanager.RaiseToolAdded( new CoreItemEventArgs<CoreToolBase>(
            prop.Tool as CoreToolBase ));

            
        }


        internal WinLauncherToolPanelProperty GetPanelProperty(ICoreTool tool)
        {
            return GetPanelProperty(tool, this.m_lmanager.Environment);
        }
        internal WinLauncherToolPanelProperty GetPanelProperty(ICoreTool tool, string EnvironmentName)
        {
            if (string.IsNullOrEmpty(EnvironmentName))
                EnvironmentName = "_default:EnvironmentName";
            if (m_panelProperties == null)
                m_panelProperties = new Dictionary<ICoreTool, Dictionary<string, WinLauncherToolPanelProperty>>();
           
                if (m_panelProperties.ContainsKey(tool))
                {
                    if (!string.IsNullOrEmpty(EnvironmentName) && m_panelProperties[tool].ContainsKey(EnvironmentName))
                    {
                        return m_panelProperties[tool][EnvironmentName];
                    }
                    else
                    {
                        WinLauncherToolPanelProperty p = new WinLauncherToolPanelProperty(this.m_lmanager, tool, EnvironmentName);
                        m_panelProperties[tool].Add(EnvironmentName, p);
                        return p;
                    }
                }
                else
                {
                    m_panelProperties.Add(tool, new Dictionary<string, WinLauncherToolPanelProperty>());
                    WinLauncherToolPanelProperty p = new WinLauncherToolPanelProperty(this.m_lmanager, tool, EnvironmentName);
                    m_panelProperties[tool].Add(EnvironmentName, p);
                    return p;
                }
            
        }


        /// <summary>
        /// represent property that store a page and the attached tools.
        /// </summary>
        public class WinLauncherToolPanelProperty 
        {
            private XDockingPage m_Page;
            private ICoreTool m_tool;
            private DrSStudioLayoutManager m_lmanager;
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
            public XForm DockedForm
            {
                get
                {
                    return m_DockedForm;
                }
            }
            public bool IsToolStrip
            {
                get
                {
                    return (m_tool.HostedControl is ToolStrip);
                }
            }
            public bool IsDockingHostedControl
            {
                get
                {
                    return !IsToolStrip;
                }
            }

            internal WinLauncherToolPanelProperty(DrSStudioLayoutManager lmanager, ICoreTool tool, string EnvironmentName)
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
                    OnToolDisplayChanged(EventArgs.Empty);
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

            public event EventHandler LocationChanged
            {
                add
                {
                    this.Page.HostedControl.LocationChanged += value;
                }
                remove
                {
                    this.Page.HostedControl.LocationChanged -= value;
                }
            }
            internal void InitNewForm()
            {
                IDockingForm v_Form = this.m_lmanager.ToolManager.CreateToolForm();
                v_Form.Owner = this.Page.LayoutManager.MainForm;
                v_Form.Title = CoreResources.GetString(this.Page.HostedControl.CaptionKey);
                this.m_Page.DockingForm = v_Form;

                Form frm = v_Form as Form;
                if (frm != null)
                {
                    frm.FormClosing += (o, e) =>
                    {
                        switch (e.CloseReason)
                        {
                            case CloseReason.ApplicationExitCall:
                            case CloseReason.FormOwnerClosing:
                                break;
                            default:
                                e.Cancel = true;
                                frm.Hide();
                                this.m_Page.HideTool();
                                break;
                        }
                    };
                }
            }
            }
        /// <summary>
        /// represent the default docking panel collection
        /// </summary>
        internal class WinLauncherDockingPageCollections : IDockingPanelCollections
        {
            DrSStudioLayoutManager m_layoutManager;
            List<IDockingPanel> m_pages;
            public WinLauncherDockingPageCollections(DrSStudioLayoutManager manager)
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





        internal void InitLayout(System.Collections.IList list)
        {
            
            c_panBottom = new XDockingPanel(this );
            c_panBottom.Dock = DockStyle.Bottom;
            c_panLeft = new XDockingPanel(this);
            c_panLeft.Dock = DockStyle.Left;
            c_panRight = new XDockingPanel(this);
            c_panRight.Dock = DockStyle.Right;
            c_panTop = new XDockingPanel(this);
            c_panTop.Dock = DockStyle.Top;

            c_panBottom.Visible = false;
            c_panLeft.Visible = false;
            c_panRight.Visible = false;

            list.Add(c_panBottom);
            list.Add(c_panLeft);
            list.Add(c_panRight);
            //list.Add(c_panTop);

            c_panLeft.PageAdded += c_panLeft_PageAdded;
            c_panLeft.PageRemoved += c_panLeft_PageRemoved;
            c_panBottom.PageAdded += c_panLeft_PageAdded;
            c_panBottom.PageRemoved += c_panLeft_PageRemoved;
            c_panRight.PageAdded += c_panLeft_PageAdded;
            c_panRight.PageRemoved += c_panLeft_PageRemoved; 
        }

        void c_panLeft_PageRemoved(object sender, DockingPageEventArgs e)
        {
            if (this.m_configuring)
                return;

            this.m_configuring = true;
            XDockingPage page = e.Page as XDockingPage;
            if (page != null)
            {
                page.HideTool();
            }
            this.m_configuring = false;
        }

        void c_panLeft_PageAdded(object sender, DockingPageEventArgs e)
        {
            
        }

        public IDockingPanelCollections Panels
        {
            get { return m_panelCollection; }
        }
    }
}
