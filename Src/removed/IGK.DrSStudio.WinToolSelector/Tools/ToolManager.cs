

/*
IGKDEV @ 2008 - 2014
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
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:ToolManager.cs
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
using System.Windows.Forms;
namespace IGK.DrSStudio.Tools
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.Menu;
    [CoreTools ("Tool.WorkingTools", ImageKey ="Menu_Tools")]
    sealed class ToolManager :
        CoreToolBase,
        ICoreMenuMessageShortcutContainer
    {
        private static ToolManager sm_instance;
        private CoreMenuMessageFilter m_menuMessageFilter;
        //touche raccourcis , type
        private Dictionary<Keys , Type > m_dicTool;
        public new ICoreWorkingToolManagerSurface CurrentSurface
        {
            get
            {
                return base.CurrentSurface as ICoreWorkingToolManagerSurface;
            }
        }
        //Environment.groupName
        private Dictionary<string, IWorkingItemGroup> m_items;
        public new UIXWorkingElement HostedControl {
            get {
                return base.HostedControl as UIXWorkingElement ;
            }
            set {
                base.HostedControl = value;
            }
        }
        private ToolManager()
        {
            m_items = new Dictionary<string, IWorkingItemGroup>();
            m_dicTool = new Dictionary<Keys,Type> ();
        }
        private bool m_IsFiltering;
        public bool IsFiltering
        {
            get { return m_IsFiltering; }            
        }
        public static ToolManager Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static ToolManager()
        {
            sm_instance = new ToolManager();
        }
        protected override void GenerateHostedControl()
        {
            UIXWorkingElement v_ctr = new UIXWorkingElement();            
            v_ctr.CaptionKey = "Tools.ToolManager";            
            this.HostedControl = v_ctr;
            this.InitWorkingItems();
        }
        protected override void RegisterBenchEvent(ICoreWorkbench workbench)
        {
            base.RegisterBenchEvent(workbench);
            workbench.LayoutManager.EnvironmentChanged += new EventHandler(LayoutManager_EnvironmentChanged);
            workbench.CurrentSurfaceChanged += new CoreWorkingSurfaceChangedEventHandler(workbench_CurrentSurfaceChanged);
          // InitWorkingItems();
        }
        void workbench_CurrentSurfaceChanged(object o, CoreWorkingSurfaceChangedEventArgs e)
        {
            if (e.OldSurface is ICoreWorkingToolManagerSurface)
                UnRegisterSurfaceEvent(e.OldSurface as ICoreWorkingToolManagerSurface);
            if (e.NewSurface  is ICoreWorkingToolManagerSurface)
                RegisterSurfaceEvent(e.NewSurface as ICoreWorkingToolManagerSurface);
        }
        protected override void UnregisterBenchEvent(IGK.DrSStudio.WinUI.ICoreWorkbench workbench)
        {
            this.m_items.Clear();
            workbench.LayoutManager.EnvironmentChanged -= new EventHandler(LayoutManager_EnvironmentChanged);
            workbench.CurrentSurfaceChanged -= new CoreWorkingSurfaceChangedEventHandler(workbench_CurrentSurfaceChanged);          
            base.UnregisterBenchEvent(workbench);
        }
        public event EventHandler SelectedWorkingTypeChanged;
        public Type SelectedWorkingType
        {
            get
            {
                if (this.CurrentSurface == null)
                    return null;
                return this.CurrentSurface.CurrentTool;
            }
            set {
                if (this.CurrentSurface != null)
                    this.CurrentSurface.CurrentTool = value;
            }
        }
        void LayoutManager_EnvironmentChanged(object sender, EventArgs e)
        {
            foreach (IWorkingItemGroup  item in this.HostedControl.Groups)
            {
                item.Visible = (this.Workbench.LayoutManager.Environment ==
                    item.Environment);
            }
            if (this.HostedControl.Visible )
            this.HostedControl.Invalidate();
        }
        void RegisterSurfaceEvent(ICoreWorkingToolManagerSurface surface)
        {
            surface.CurrentToolChanged += new EventHandler(surface_CurrentToolChanged);
            OnCurrentToolChanged(EventArgs.Empty);
        }
        void surface_CurrentToolChanged(object sender, EventArgs e)
        {
            OnCurrentToolChanged(EventArgs.Empty);
        }
        private void OnCurrentToolChanged(EventArgs eventArgs)
        {
            if (this.SelectedWorkingTypeChanged != null)
                this.SelectedWorkingTypeChanged(this, eventArgs);
        }
        void UnRegisterSurfaceEvent(ICoreWorkingToolManagerSurface surface)
        {
            surface.CurrentToolChanged -= new EventHandler(surface_CurrentToolChanged);
        }
        private void InitWorkingItems()
        {
            this.m_items.Clear();
            this.m_dicTool.Clear ();
            ICoreWorkingGroupObjectAttribute  v_attr;
            IWorkingItemGroup v_group;
            string v_name = string.Empty ;
            string v_imgk = string.Empty;
            ///register group and item
            foreach (KeyValuePair<string, ICoreWorkingObjectInfo> i in CoreSystem.Instance.WorkingObjects)
            {
                v_attr = i.Value.Attribute as ICoreWorkingGroupObjectAttribute ;
                if ((v_attr == null)||(!v_attr.IsVisible ))
                    continue;
                v_name =string.Format ("{0}.{1}",v_attr .Environment ,
                    v_attr.GroupName );
                if (!m_items.ContainsKey(v_name))
                {
                    v_group = new WorkingGroup(v_attr.GroupName, v_attr .GroupImageKey,
                        v_attr.Environment , HostedControl);
                    m_items.Add(v_name, v_group);
                    this.HostedControl.Groups.Add(v_group);
                    v_group.VisibleChanged +=new EventHandler(v_group_VisibleChanged);
                }
                else
                {
                    v_group = m_items[v_name];
                }
                if (string.IsNullOrEmpty(v_attr.ImageKey))
                {
                    v_imgk = "DE_" + v_attr.Name;
                }
                else
                    v_imgk = v_attr.ImageKey;
                v_group.Items.Add( new WorkingItem(v_group,
                    i.Value.Type ,
                    v_attr .CaptionKey ,
                    v_imgk,
                    v_attr .Keys,
                    CoreSystem.GetString(v_attr.Keys )
                    ));
            }
            this.HostedControl.Groups.Sort();
            foreach (IWorkingItemGroup  item in this.HostedControl.Groups)
            {
                item.Items.Sort();
            }
        }
        void  v_group_VisibleChanged(object sender, EventArgs e)
        {
              IWorkingItemGroup  v_group = sender as IWorkingItemGroup ;            
              viewGroup(v_group);    
        }
        private void viewGroup(IWorkingItemGroup group)
        {
                    if (group.Visible)
                    {//group is visible
                        foreach (IWorkingItem item in group.Items)
                        {                 
                            if (item.Keys  != Keys.None)
                            {
                                //register tool                    
                                if (!this.m_dicTool.ContainsKey(item.Keys))
                                {
                                    this.m_dicTool.Add(
                                        item.Keys
                                    , item.Type);// e.Current.Key);
                                }
                                else if (this.m_dicTool[item.Keys]!= item .Type )
                                {
                                    //replace
                                    this.m_dicTool[item.Keys] = item.Type;
                                }
                            }
                        }
                    }
                    else
                    {//not visible
                        foreach (IWorkingItem item in group.Items)
                        {                    
                            if (item.Keys != Keys.None)
                            {
                                //register tool     
                                if (this.m_dicTool.ContainsKey(item.Keys) && (this.m_dicTool[item.Keys]== item.Type ))                            
                                {
                                    this.m_dicTool.Remove(item.Keys);
                                }
                            }
                        }
                    }
        }
        public void StartFilter()
        {
            if (this.m_menuMessageFilter == null)
            {
                this.m_menuMessageFilter = new CoreMenuMessageFilter(this.Workbench,
                    this,
                    'T');
                this.m_menuMessageFilter.StartFiltering();
                this.m_IsFiltering  =true ;
            }
        }
        public void EndFilter()
        {
            if (this.m_menuMessageFilter != null)
            {
                this.m_menuMessageFilter.EndFilter();                
            }
            this.m_menuMessageFilter = null;
            this.m_IsFiltering = false;
        }
        public bool Contains(Keys key)
        {
            return this.m_dicTool.ContainsKey(key);
        }
        public void Call(Keys key)
        {
           this.CurrentSurface.CurrentTool 
            =  this.m_dicTool[key];
        }
    }
}

