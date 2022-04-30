

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKGTShortcutManager.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:IGKGTShortcutManager.cs
*/
using IGK.ICore.Menu;
using IGK.ICore.Resources;
using IGK.ICore.Tools;
using IGK.ICore.WinUI;
using System; 
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Tools
{
    [CoreTools("Tool.IGKGTShortcutManager")]
    class IGKGTShortcutManager :
        CoreShortCutMenuContainerToolBase,
        ICoreMenuMessageShortcutContainer
    {
        public sealed class IGKGTGroupCollections: IEnumerable,
             IComparer<IWorkingItemGroup> 
        {
            List<IWorkingItemGroup> m_groups;
            private IGKGTShortcutManager m_tool;
            public IGKGTGroupCollections(IGKGTShortcutManager tool)
            {                
                this.m_tool = tool;
                this.m_groups = new List<IWorkingItemGroup>();
            }
            public void Add(IWorkingItemGroup group)
            {
                this.m_groups.Add(group);
            }
            public void Remove(IWorkingItemGroup group)
            {
                this.m_groups.Remove(group);
            }
            public IEnumerator GetEnumerator()
            {
                return this.m_groups.GetEnumerator();
            }
            internal void Sort()
            {
                this.m_groups.Sort(this);
            }
            public int Compare(IWorkingItemGroup x, IWorkingItemGroup y)
            {
                if (x.Position == y.Position)
                    return x.Title.CompareTo(y.Title);
                return x.Position.CompareTo(y.Position);
            }
        }
        //private CoreMenuMessageFilter m_menuMessageFilter;
        private Dictionary<string, IWorkingItemGroup> m_items;
        private Dictionary<enuKeys, Type> m_dicTool;//touche raccourcis , type
        public IWorkingItemGroup[] GetGroups()
        {
            return m_items.Values.ToArray<IWorkingItemGroup>();
        }
        public override void Call(enuKeys k)
        {
            this.CurrentSurface.CurrentTool = this.m_dicTool[k];
        }
        public override bool Contains(enuKeys k)
       { 
            return this.m_dicTool.ContainsKey(k);
        }
        public new ICoreWorkingToolManagerSurface CurrentSurface
        {
            get
            {
                return base.CurrentSurface as ICoreWorkingToolManagerSurface;
            }
        }
        private static IGKGTShortcutManager sm_instance;
        private IGKGTShortcutManager()
            : base()
        {
            this.m_dicTool = new Dictionary<enuKeys, Type>();
            this.m_groups = new IGKGTGroupCollections(this);
            this.m_items = new Dictionary<string, IWorkingItemGroup>();
        }
        public IGKGTGroupCollections Groups {
            get { return this.m_groups; }
        }
        public static IGKGTShortcutManager Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static IGKGTShortcutManager()
        {
            sm_instance = new IGKGTShortcutManager();
        }
        protected override void GenerateHostedControl()
        {
            base.GenerateHostedControl();
            this.Key = enuKeys.T;
        }
        protected override void RegisterBenchEvent(ICoreSystemWorkbench workbench)
        {
            base.RegisterBenchEvent(workbench);
            if (Workbench is ICoreLayoutManagerWorkbench m)
                m.LayoutManager.EnvironmentChanged +=LayoutManager_EnvironmentChanged;
            if (Workbench is ICoreWorkingSurfaceHandler s)
                s.CurrentSurfaceChanged +=workbench_CurrentSurfaceChanged;
            InitWorkingItems();
        }
        protected override void UnregisterBenchEvent(ICoreSystemWorkbench workbench)
        {
            //this.m_items.Clear();
            workbench.GetLayoutManager().EnvironmentChanged -= LayoutManager_EnvironmentChanged;
            if (Workbench is ICoreWorkingSurfaceHandler s)
                s.CurrentSurfaceChanged -= workbench_CurrentSurfaceChanged;
            base.UnregisterBenchEvent(workbench);
        }
        private void workbench_CurrentSurfaceChanged(object sender, CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {

            this.UpdateVisibility();
            if (e.OldElement  is ICoreWorkingToolManagerSurface)
                UnRegisterSurfaceEvent(e.OldElement as ICoreWorkingToolManagerSurface);
            if (e.NewElement is ICoreWorkingToolManagerSurface)
                RegisterSurfaceEvent(e.NewElement as ICoreWorkingToolManagerSurface);

        }
      
        public event EventHandler SelectedWorkingTypeChanged;
        private IGKGTGroupCollections m_groups;
        public Type SelectedWorkingType
        {
            get
            {
                if (this.CurrentSurface == null)
                    return null;
                return this.CurrentSurface.CurrentTool;
            }
            set
            {
                if (this.CurrentSurface != null)
                    this.CurrentSurface.CurrentTool = value;
            }
        }
        void LayoutManager_EnvironmentChanged(object sender, EventArgs e)
        {
            UpdateVisibility();          
        }

        private void UpdateVisibility()
        {
            var s = this.CurrentSurface;
            if (s == null)
            {

                foreach (IWorkingItemGroup item in this.Groups)
                {
                    item.Visible = false;
                }
            }
            else
            {
                foreach (IWorkingItemGroup item in this.Groups)
                {
                    //tools group visible on environemnt 
                    //item.Visible =                    
                    //    (this.Workbench.LayoutManager.Environment ==
                    //    item.Environment);
                    int v_visibleCount = 0;
                    foreach (ICoreGTWorkingItem r in item.Items)
                    {
                        if (s.IsToolValid(r.ToolType))
                        {
                            v_visibleCount++;
                        }
                    }
                    item.Visible = (v_visibleCount > 0);
                }
            }
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
            surface.CurrentToolChanged -= surface_CurrentToolChanged;
        }
        private void InitWorkingItems()
        {
            //this.m_items.Clear();
            this.m_dicTool.Clear();
            ICoreWorkingGroupObjectAttribute v_attr;
            IWorkingItemGroup v_group;
            string v_name = string.Empty;
            string v_imgk = string.Empty;
            ///register group and item
            foreach (KeyValuePair<string, ICoreWorkingObjectInfo> i in CoreSystem.GetWorkingObjects())// CoreWorkingObjects.WorkingObjects)
            {
                v_attr = i.Value.Attribute as ICoreWorkingGroupObjectAttribute;
                if ((v_attr == null) || (!v_attr.IsVisible))
                    continue;
                v_name = string.Format("{0}.{1}", v_attr.Environment,v_attr.GroupName);
                if (!m_items.ContainsKey(v_name))
                {
                    v_group = new WorkingGroup(v_attr.GroupName, v_attr.GroupImageKey, v_attr.Environment);
                    m_items.Add(v_name, v_group);
                    this.Groups.Add(v_group);
                    v_group.VisibleChanged += new EventHandler(v_group_VisibleChanged);
                }
                else
                {
                    v_group = m_items[v_name] ;
                }
                if (string.IsNullOrEmpty(v_attr.ImageKey))
                {
                    v_imgk = $"DE_{ v_attr.Name}_gkds";
                }
                else
                    v_imgk = v_attr.ImageKey;
                v_group.Items.Add(new WorkingItem(v_group,
                    i.Value.Type,
                    v_attr.CaptionKey,
                    v_imgk,
                    v_attr.Keys,
                    v_attr.CaptionKey.R()
                    ));
            }
            this.Groups.Sort();
            foreach (IWorkingItemGroup item in this.Groups)
            {
                item.Items.Sort();
            }
        }
        void v_group_VisibleChanged(object sender, EventArgs e)
        {
            IWorkingItemGroup v_group = sender as IWorkingItemGroup;
            viewGroup(v_group);
        }
        private void viewGroup(IWorkingItemGroup group)
        {
            if (group.Visible)
            {//group is visible
                foreach (ICoreGTWorkingItem item in group.Items)
                {
                    if (item.Keys != enuKeys.None)
                    {
                        //register tool                    
                        if (!this.m_dicTool.ContainsKey(item.Keys))
                        {
                            this.m_dicTool.Add(
                                item.Keys
                            , item.ToolType);// e.Current.Key);
                        }
                        else if (this.m_dicTool[item.Keys] != item.ToolType)
                        {
                            //replace
                            this.m_dicTool[item.Keys] = item.ToolType;
                        }
                    }
                }
            }
            else
            {//not visible
                foreach (ICoreGTWorkingItem item in group.Items)
                {
                    if (item.Keys != enuKeys.None)
                    {
                        //register tool     
                        if (this.m_dicTool.ContainsKey(item.Keys) && (this.m_dicTool[item.Keys] == item.ToolType))
                        {
                            this.m_dicTool.Remove(item.Keys);
                        }
                    }
                }
            }
        }
    }
}

