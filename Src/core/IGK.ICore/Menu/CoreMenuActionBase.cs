

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreMenuActionBase.cs
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
file:CoreMenuActionBase.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Menu
{
    using IGK.ICore;using IGK.ICore.Menu ;
    using IGK.ICore.WinUI ;
    using IGK.ICore.Actions;
    using IGK.ICore.Resources;
    using IGK.ICore.Tools;
    using IGK.ICore.Settings;
    [Serializable ()]
    public class CoreMenuActionBase
: MarshalByRefObject,        
        ICoreMenuAction ,
        ICoreCaptionItem,
        ICoreWorkingObject 
    {
        private ICoreMenuAction m_Parent;
        private string m_id;
        private string m_captionKey;
        private int m_index;
        private string m_imageKey;
        private CoreMenuActionCollection m_childs;
        private IXCoreMenuItemContainer m_MenuItem;
        private IXCoreMenuItemSeparator m_sepBefore;
        private IXCoreMenuItemSeparator m_sepAfter;
        private ICoreSystemWorkbench m_WorkBench;
        private bool m_defaultVisible;
        private enuKeys m_shortCut;
        private string m_shortCutText;
        private bool m_IsRootMenu;
        private bool m_isSepBefore;
        private bool m_isSepAfter;
        private ICoreMainForm m_mainForm;
        /// <summary>
        /// get the main form
        /// </summary>
        public ICoreMainForm MainForm
        {
            get
            {               
                return m_mainForm;
            }
        }
        /// <summary>
        /// get or set the caption key
        /// </summary>
        public string CaptionKey
        {
            get
            {
                return this.m_captionKey;
            }
            set
            {
                this.m_captionKey = value;
            }
        }
        protected bool DefaultVisible { get { return this.m_defaultVisible; } }
        public override string ToString()
        {
            return "CoreMenu:["+this.Id+"]";
        }
        public virtual enuActionType ActionType { get { return enuActionType.MenuAction;  } }
        public event CoreMenuActionEventHandler MenuAdded;
        public event CoreMenuActionEventHandler MenuRemoved;
        protected IXCoreMenuItemSeparator SepBefore
        {
            get {
                return this.m_sepBefore;
            }
        }
        protected IXCoreMenuItemSeparator SepAfter
        {
            get
            {
                return this.m_sepAfter;
            }
        }
        /// <summary>
        /// get if the menu must be consider as root menu
        /// </summary>
        public bool IsRootMenu
        {
            get { return m_IsRootMenu; }
            protected set
            {
                if (m_IsRootMenu != value)
                {
                    m_IsRootMenu = value;
                }
            }
        }
        public bool IsSepearatorBefore { get { return this.m_isSepBefore ;} }
        public bool IsSepearatorAfter { get { return this.m_isSepAfter; } }
        public string ShortcutText {
            get {
                return this.m_shortCutText;
            }
        }
        public enuKeys ShortCut {
            get {
                return this.m_shortCut;
            }
        }
        /// <summary>
        /// get the Workbench
        /// </summary>
        public ICoreSystemWorkbench Workbench
        {
            get { return m_WorkBench; }
            internal protected set {
                if (this.m_WorkBench != value)
                {
                    if (this.m_WorkBench != null)
                        this.UnregisterBenchEvent(this.m_WorkBench);
                    this.m_WorkBench = value;
                    if (this.m_WorkBench != null)
                    {
                        this.RegisterBenchEvent(this.m_WorkBench);
                        InitMenu();
                    }
                    OnWorkbenchChanged(EventArgs.Empty);
                }
            }
        }
        public ICoreWorkingSurface CurrentSurface { get {            
            if (this.Workbench !=null)                
            return this.Workbench.CurrentSurface; 
            return null;
        }
        }
        /// <summary>
        /// override this to register Workbench event
        /// </summary>
        /// <param name="iCoreWorkBench"></param>
        protected virtual void RegisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            if (this.Workbench is ICoreApplicationWorkbench)
            {
                ((ICoreApplicationWorkbench)this.Workbench).MainForm.Closed += MainForm_Closed;
            }
            this.MenuAdded += _MenuAdded;
            this.MenuRemoved += _MenuRemoved;
        }
        /// <summary>
        /// override this to unregister Workbench event
        /// </summary>
        /// <param name="iCoreWorkBench"></param>
        protected virtual void UnregisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            if (this.Workbench is ICoreApplicationWorkbench)
            {
                var b = this.Workbench as ICoreApplicationWorkbench;
                b.MainForm.Closed -= MainForm_Closed;
            }
            this.MenuAdded -= _MenuAdded;
            this.MenuRemoved -= _MenuRemoved;
        }
        void MainForm_Closed(object sender, CoreFormClosedEventArgs e)
        {
            this.DisposeMenu(); 
        }
        public virtual void Dispose()
        {
            this.DisposeMenu();
        }
        private void DisposeMenu()
        {
            if (this.m_sepAfter != null) this.m_sepAfter.Dispose();
            if (this.m_sepBefore != null) this.m_sepBefore.Dispose();
            if (this.m_MenuItem !=null) this.m_MenuItem .Dispose ();
            this.m_sepBefore = null;
            this.m_sepAfter = null;
            this.m_MenuItem = null;

            if (this.m_WorkBench != null)
            {
                this.UnregisterBenchEvent(this.m_WorkBench);
                this.m_WorkBench = null;
            }
        }
        protected virtual void OnWorkbenchChanged(EventArgs eventArgs)
        {
            //for performance
            if (this.m_WorkBench is ICoreApplicationWorkbench)
            {
                this.m_mainForm = (this.m_WorkBench as ICoreApplicationWorkbench).MainForm;
            }
            else {
                this.m_mainForm = null;
            }
            if (this.WorkBenchChanged != null)
                WorkBenchChanged(this, eventArgs);
        }
        public event EventHandler WorkBenchChanged;
        /// <summary>
        /// represent the menu item
        /// </summary>
        public IXCoreMenuItemContainer MenuItem
        {
            get { return m_MenuItem; }
            protected set { m_MenuItem = value ; }
        }
        private bool m_visible;
        private bool m_enable;
        public bool Enabled {
            get {
                return m_enable;
            }
            set {
                if (m_enable != value)
                {
                    m_enable = value;
                    OnEnableChanged(EventArgs.Empty);
                }
            }
        }
        public bool Visible {
            get {
                if (m_defaultVisible)
                {
                    return this.m_visible;
                }
                return m_defaultVisible;
            }
            set {
                if (this.m_defaultVisible)
                {
                    if (this.m_visible != value)
                    {
                        this.m_visible = value;
                        OnVisibleChanged(EventArgs.Empty);
                    }
                }
            }
        }
        protected CoreMenuActionBase():base()
        {           
            CoreMenuAttribute v_attr  = 
                (CoreMenuAttribute)
                Attribute.GetCustomAttribute (this.GetType (),
                typeof(CoreMenuAttribute ));
            this.SetAttribute(v_attr );          
            this.m_childs = new CoreMenuActionCollection(this);
            this.WorkBenchChanged += _WorkBenchChanged;
        }
        private void _MenuRemoved(object o, CoreMenuActionEventArgs e)
        {
            UnlinkMenuItem(e.Action);
            e.Action.Workbench = null;
        }
        void _MenuAdded(object o, CoreMenuActionEventArgs e)
        {
            e.Action.Workbench = this.Workbench;
            LinkMenuItem(e.Action);
        }
        void _WorkBenchChanged(object sender, EventArgs e)
        {
            RegisterChildMenu();
        }
        private void RegisterChildMenu()
        {
            //Register childs Menu To workbench on existing childs
            foreach (CoreMenuActionBase m in this.m_childs)
            {
                m.Workbench = this.Workbench;
                this.LinkMenuItem(m);
            }
        }
        private void UnlinkMenuItem(CoreMenuActionBase m)
        {
            if (m.MenuItem != null)
            {//build menu item
                //insert menu
                if (m.m_sepBefore != null)
                    this.MenuItem.Remove (m.m_sepBefore);
                this.MenuItem.Remove(m.MenuItem);
                if (m.m_sepAfter != null)
                    this.MenuItem.Remove(m.m_sepAfter);
            }
        }
        private void LinkMenuItem(CoreMenuActionBase m)
        {
            if (m.MenuItem != null)
            {//build menu item
                //insert menu
                if (m.m_sepBefore != null)
                    this.MenuItem.Add(m.m_sepBefore);
                this.MenuItem.Add(m.MenuItem);
                if (m.m_sepAfter != null)
                    this.MenuItem.Add(m.m_sepAfter);
            }
        }
        public void SetAttribute(CoreMenuAttribute v_attr)
        {
            if (v_attr != null)
            {
                //mark this width the current attribute
                this.m_attribute = v_attr;
                this.m_index = v_attr.Index;
                this.m_id = v_attr.Name;
                this.m_captionKey = v_attr.CaptionKey;
                this.m_defaultVisible = v_attr.IsVisible;
                this.m_shortCut = v_attr.Shortcut;
                this.m_shortCutText = v_attr.ShortcutText;
                this.m_isSepBefore = v_attr.SeparatorBefore;
                this.m_isSepAfter = v_attr.SeparatorAfter;
                this.m_imageKey = v_attr.ImageKey;
                this.m_isShortcutMenuChild = v_attr.IsShortcutMenuChild;
                this.m_shortcutMenuContainerTypeTool = v_attr.ShortCutMenuContainerTypeTool;
            }
            else
            {
                this.m_defaultVisible = true;
            }
        }
        /// <summary>
        /// Get or set the image key
        /// </summary>
        public string ImageKey {
            get { return this.m_imageKey; }
            set { this.m_imageKey = value; }
        }
        protected virtual IXCoreMenuItemSeparator CreateSeparator()
        {
            return CoreControlFactory.CreateControl(CoreConstant.CTRL_MENU_SEPARATOR) as IXCoreMenuItemSeparator;
        }
        protected virtual void InitMenu()
        {            
            //create menu item
            if (this.m_MenuItem == null)
            {
                var b = this.Workbench as ICoreLayoutManagerWorkbench;
                if (b == null)
                    return;
                m_MenuItem = b.CreateMenuItem();
            }
            CoreActionRegisterTool.Instance.Register(this);
            //setup dthe default menu
            if (m_MenuItem != null)
            {
                this.m_visible  = m_defaultVisible;
                new MenuItemEVENTHOST(this, new MenuItemHost ( m_MenuItem));
                m_MenuItem.Click += new EventHandler(m_menu_Click);
               m_MenuItem.VisibleChanged += new EventHandler(m_MenuItem_VisibleChanged);
               if (this.IsShortcutMenuChild == false)
               {
                   if (!string.IsNullOrEmpty(this.m_shortCutText))
                       m_MenuItem.ShortcutKeyDisplayString = this.m_shortCutText;
                   else
                   {
                       if (this.m_shortCut != enuKeys.None)
                       {
                           m_MenuItem.ShortcutKeyDisplayString = CoreUtils.GetShortcutText(this.m_shortCut);
                       }
                   }
               }
               else {
                   if (this.m_attribute !=null)
                       this.m_attribute.RegisterShortCutMenu(this);
               }
               if  (!string.IsNullOrEmpty(this.m_imageKey))
                {
                    this.m_MenuItem.MenuDocument = CoreResources.GetDocument(this.m_imageKey);                    
                }
                IGK.ICore.Settings.CoreApplicationSetting.Instance.LangReloaded+= new EventHandler(_LangChangedChanged);

#if DEBUG
                IGK.ICore.Settings.CoreApplicationSetting.Instance.SettingChanged  +=(o,e)=>{
                    if (e.Value.Name == "ShowMenuIndex")
                        this.LoadDisplayText();
                };
#endif
                if (this.m_isSepBefore )
                    this.m_sepBefore = CreateSeparator();
                if (this.m_isSepAfter)
                    this.m_sepAfter = CreateSeparator();
                this.LoadDisplayText();

                
            }
             SetupEnableAndVisibility();
        }
        /// <summary>
        /// call IsEnabled and IsVisible
        /// </summary>
        protected void SetupEnableAndVisibility()
        {
            this.Enabled = IsEnabled();
            bool v = IsVisible();
            if (v && this.IsRootMenu)
            { 
                //check that 
                int child = this.GetVisibleChild();
                if (child == 0)
                    v = false;
            }          
            this.Visible = v;
        }
        private int GetVisibleChild()
        {
            int i = 0;
            foreach (CoreMenuActionBase  item in this.m_childs)
            {
                if (item.Visible)
                    i++;
            }
            return i;
        }
        void _LangChangedChanged(object sender, EventArgs e)
        {
            this.LoadDisplayText();
        }
        void m_MenuItem_VisibleChanged(object sender, EventArgs e)
        {
            OnMenuItemVisibleChanged();
        }
        protected virtual void OnMenuItemVisibleChanged()
        {
            if (this.m_sepAfter != null)
                this.m_sepAfter.Available = this.MenuItem.Available;
            if (this.m_sepBefore != null)
                this.m_sepBefore.Available = this.MenuItem.Available;
        }
        protected virtual void OnVisibleChanged(EventArgs e)
        {
            if ((this.m_Parent != null) && (this.m_Parent.IsRootMenu ))
            {
                (this.m_Parent as CoreMenuActionBase).SetupEnableAndVisibility();
            }
            VisibleChanged?.Invoke(this, e);
        }
        protected virtual void OnEnableChanged(EventArgs e)
        {
                EnabledChanged?.Invoke(this, e);
        }
        /// <summary>
        /// class the on menu item clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_menu_Click(object sender, EventArgs e)
        {
            OnMenuItemClicked();
        }
        protected virtual void OnMenuItemClicked()
        {
            var e = (this.Workbench as ICoreApplicationWorkbench);
            if (e != null)
            {
                e.MainForm.BeginInvoke((CoreMethodInvoker)delegate()
                {
                    this.DoAction();
                });
            }
        }
        protected virtual bool PerformAction()
        {
            return false;
        }
        public event EventHandler VisibleChanged;
        public event EventHandler EnabledChanged;
        #region ICoreMenuAction Members
        public virtual bool CanShow
        {
            get { return true; }
        }
        #endregion
        #region ICoreAction Members
        public void DoAction()
        {
#if SHOWMENU
            MessageBox.Show("Menu + "+this.Id + " + "+this.GetType ().FullName );
#endif

            if (this.Enabled)
            {
#if DEBUG
                CoreLog.WriteLine("[IGK] - CoreMenuAction [" + this.Id+"]");
#endif

                System.AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
                if (this.PerformAction())
                {
                    OnActionPerformed (EventArgs.Empty );
                }
                System.AppDomain.CurrentDomain.AssemblyResolve -= CurrentDomain_AssemblyResolve;
            }
        }

        private System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return null;
        }
        #endregion
        #region ICoreIdentifier Members
        public string Id
        {
            get { return this.m_id ;}
            internal protected set { this.m_id = value; this.LoadDisplayText(); }
        }
        #endregion
        #region ICoreAction Members
        public event EventHandler ActionPerformed;
        private bool m_isShortcutMenuChild;
        private Type m_shortcutMenuContainerTypeTool;
        private CoreMenuAttribute m_attribute;
        private List<string> m_context;
        #endregion
        protected virtual void OnActionPerformed(EventArgs e)
        {
            if (this.ActionPerformed !=null)
                this.ActionPerformed (this, e);
        }
        #region ICoreMenuAction Members
        public int Index
        {
            get { return this.m_index; }
            protected set { this.m_index =value ; }
        }
        public ICoreMenuAction Parent
        {
            get { return this.m_Parent; }
        }
        /// <summary>
        /// Get childs collections of this menu
        /// </summary>
        public ICoreMenuActionCollections Childs
        {
            get { return this.m_childs; }
        }
        #endregion
   #region ICoreCaptionItem Members
        public virtual void LoadDisplayText()
        {
            if (this.m_MenuItem != null)
            {
#if DEBUG
                if (CoreApplicationSetting.Instance.ShowMenuIndex)
                {
                    m_MenuItem.Text = this.Index + " : "+ this.CaptionKey.R();
                }
                else {
                    m_MenuItem.Text = this.CaptionKey.R();
                }
#else 
                m_MenuItem.Text = this.CaptionKey.R();
#endif


            }
        }
        #endregion

        /// <summary>
        /// manage the default properties is enabled
        /// </summary>
        /// <returns></returns>
        protected virtual bool IsEnabled()
        {
            return (this.CurrentSurface != null);
        }

        /// <summary>
        /// manage the default properties is visible
        /// </summary>
        /// <returns></returns>
        protected virtual bool IsVisible()
        {
            return m_defaultVisible &&  (this.CurrentSurface != null);
        }

        internal void Unregister(string name)
        {
            CoreWorkingActionCollections.GetActionCollections().Unregister(name);
        }
        /// <summary>
        /// call this function to register menu action in core system. this action if well register
        /// can be call by workbench directly
        /// </summary>
        /// <param name="attrib"></param>
        /// <param name="menu"></param>
        /// <returns></returns>
        protected bool Register(CoreMenuAttribute attrib, CoreMenuActionBase menu)
        {
            if ((attrib == null) || (menu == null))
                return false;
            if (CoreSystem.RegisterAction(attrib, menu) == false)
            {
#if DEBUG
                CoreMessageBox.Show(CoreConstant.ERR_MENUNOTREGISTERED_1.R (attrib.Name ));
#endif
                return false;
            }
            return true;
        }
        protected virtual void OnItemAdded(CoreMenuActionEventArgs e) => MenuAdded?.Invoke(this, e);
        protected virtual void OnItemRemoved(CoreMenuActionEventArgs e) => MenuRemoved?.Invoke(this, e);
        /// <summary>
        /// represent a menu item event host
        /// </summary>
        class MenuItemEVENTHOST
        {
            MenuItemHost menu;
            CoreMenuActionBase action;
            bool v_config;
            public MenuItemEVENTHOST(CoreMenuActionBase action, MenuItemHost  menu)
            {
                this.menu = menu;
                this.action = action;
                menu.Available = true;
                menu.Visible = action.Visible;
                menu.Enabled = action.Enabled;
                this.action.VisibleChanged += new EventHandler(action_VisibleChanged);
                this.action.EnabledChanged += new EventHandler(action_EnabledChanged);
                this.menu.VisibleChanged += new EventHandler(menu_VisibleChanged);
                this.menu.EnabledChanged += new EventHandler(menu_EnabledChanged);
            }
            void action_EnabledChanged(object sender, EventArgs e)
            {
                if ((!v_config) && (action.Enabled != menu.Enabled))
                {
                    v_config = true;
                    menu.Enabled = action.Enabled;
                    v_config = false;
                }
            }
            void action_VisibleChanged(object sender, EventArgs e)
            {
                if (v_config)
                    return;
                v_config = true;
                this.menu.Visible = this.action.Visible;
                v_config = false;
            }
            void menu_EnabledChanged(object sender, EventArgs e)
            {
                if (!v_config)
                {
                    this.action.OnEnableChanged(e);
                }
            }
            void menu_VisibleChanged(object sender, EventArgs e)
            {
                if (v_config)
                    return;
                v_config = true;
                this.action.OnVisibleChanged(e);
                v_config = false;
            }
        }
        /// <summary>
        /// represent a menu item host
        /// </summary>
        class MenuItemHost 
        {
            IXCoreMenuItem menu;
            #region ICoreMenuItem Members
            public event EventHandler VisibleChanged
            {
                add { menu.VisibleChanged += value; }
                remove { menu.VisibleChanged -= value; }
            }
            public event EventHandler EnabledChanged
            {
                add { menu.EnabledChanged += value; }
                remove { menu.EnabledChanged -= value; }
            }
            #endregion
            public MenuItemHost(IXCoreMenuItem menu)
            {
                this.menu = menu;
                this.menu.Available = false;
            }
            #region ICoreMenuItem Members
            public bool Visible
            {
                get
                {
                    return this.menu.Visible;
                }
                set
                {
                    this.menu.Visible = value;
                }
            }
            public bool Available
            {
                get
                {
                    return this.menu.Available;
                }
                set
                {
                    this.menu.Available = value;
                }
            }
            public bool Enabled
            {
                get
                {
                    return this.menu.Enabled;
                }
                set
                {
                    this.menu.Enabled = value;
                }
            }
            #endregion
        }
        /// <summary>
        /// represent a menu item action collection
        /// </summary>
        class CoreMenuActionCollection : ICoreMenuActionCollections
        {
            List<ICoreMenuAction> m_menu;
            CoreMenuActionBase m_owner;
            public CoreMenuActionCollection(CoreMenuActionBase e)
            {
                this.m_owner = e;
                this.m_menu = new List<ICoreMenuAction>();
            }
            #region ICoreMenuActionCollections Members
            public ICoreMenuAction this[int index]
            {
                get { return this.m_menu[index]; }
            }
            public void Add(ICoreMenuAction action)
            {
                this.m_menu.Add(action);
                CoreMenuActionBase c = action as CoreMenuActionBase;
                if (c != null)
                {
                    c.m_Parent = this.m_owner;
                    this.m_owner.OnItemAdded(new CoreMenuActionEventArgs(c));
                }
            }
            public void Remove(ICoreMenuAction actions)
            {
                this.m_menu.Remove(actions);
                CoreMenuActionBase c = actions as CoreMenuActionBase;
                if (c != null)
                {
                    c.m_Parent = null;
                    this.m_owner.OnItemRemoved(new CoreMenuActionEventArgs(c));
                }
            }
            public int Count
            {
                get { return this.m_menu.Count; }
            }
            #endregion
            #region IEnumerable Members
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_menu.GetEnumerator();
            }
            #endregion
            #region ICoreMenuActionCollections Members
            public ICoreMenuAction[] ToArray()
            {
                return this.m_menu.ToArray();
            }
            public void Clear()
            {
                ICoreMenuAction[] t = this.ToArray();
                for (int i = 0; i < t.Length; i++)
                {
                    this.Remove(t[i]);
                }
            }
            public void AddRange(ICoreMenuAction[] v_tab)
            {
                for (int i = 0; i < v_tab.Length; i++)
                {
                    if (v_tab[i] != null)
                        this.Add(v_tab[i]);
                }
            }
            public void Sort()
            {
                CoreMenuComparer comp = new CoreMenuComparer();
                this.m_menu.Sort(comp);
                //reorder child menu
                ICoreMenuAction[] menu = this.ToArray();
                CoreMenuActionBase m = null;
                for (int i = 0; i < menu.Length; i++)
                {
                    m = menu[i] as CoreMenuActionBase;
                    menu[i].Childs.Sort();
                }
            }
            #endregion
        }
        public bool IsShortcutMenuChild { get { return this.m_isShortcutMenuChild; } protected set { this.m_isShortcutMenuChild = value; } }


        public void BindExecutionContext(string name)
        {
            if(m_context ==null)
                m_context = new List<string>();
            m_context.Clear();
            if (!string.IsNullOrEmpty (name ))
            m_context.AddRange(name.ToLower().Split(','));

        }
        public bool IsInContext(string name)
        {
            if (!string.IsNullOrEmpty(name))
                return m_context.Contains(name.ToLower());
            return false ;
            
        }
        public virtual object GetActionContext(string name)
        {
            return CoreSystem.GetActionContext(name);
        }
    }
}

