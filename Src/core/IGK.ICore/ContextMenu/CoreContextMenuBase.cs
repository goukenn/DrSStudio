

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreContextMenuBase.cs
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
file:CoreContextMenuBase.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
namespace IGK.ICore.ContextMenu
{
    using IGK.ICore;using IGK.ICore.Menu;
    using IGK.ICore.ContextMenu;
    using IGK.ICore.WinUI ;
    using IGK.ICore.Actions;
    using IGK.ICore.Resources;
using IGK.ICore.Settings;
    /// <summary>
    /// the conxtextual menu of the Core. default visible
    /// </summary>
    public abstract class CoreContextMenuBase :         
        ICoreContextMenuAction         
    {
        private string m_id;
        private ICoreSystemWorkbench m_WorkBench;
        private IXCoreContextMenuItemContainer m_MenuItem;
        private int m_index;
        private string m_captionKey;
        protected ICoreContextMenuAction m_Parent;
        private ICoreContextMenuActionCollections m_childs;
        private IXCoreMenuItemSeparator m_sepBefore;
        private IXCoreMenuItemSeparator m_sepAfter;
        private enuKeys m_shortCut;
        private bool m_IsRootMenu;
        private bool m_registerContext;
        private ICoreContextMenu m_ownerContext;
        private bool m_isSepBefore;
        private bool m_isSepAfter;

        public  virtual object GetActionContext(string name)
        {
            return CoreSystem.GetActionContext(name);
        }

        public void BindExecutionContext(string name)
        {
            if (m_context == null)
                m_context = new List<string>();
            m_context.Clear();
            if (!string.IsNullOrEmpty(name))
                m_context.AddRange(name.ToLower().Split(','));

        }
        public bool IsInContext(string name)
        {
            if (!string.IsNullOrEmpty(name))
                return m_context.Contains(name.ToLower());
            return false;

        }
        /// <summary>
        /// get if this base Alloc context menu
        /// </summary>
        protected bool AllowContextMenu {
            get {
                ICoreWorkingToolManagerSurface v_surface = this.CurrentSurface as ICoreWorkingToolManagerSurface;
                if ((v_surface != null)&&(v_surface.Mecanism!=null))
                    return v_surface.Mecanism.AllowContextMenu;
                return true;
            }
        }
        /// <summary>
        /// get the short cut
        /// </summary>
        public enuKeys ShortCut {
            get {
                return this.m_shortCut;
            }
            protected set {
                this.m_shortCut = value;
            }
        }
        public bool IsSepearatorBefore { get { return this.m_isSepBefore; } }
        public bool IsSepearatorAfter { get { return this.m_isSepAfter; } }
        /// <summary>
        /// get the before separator. null if not definie;
        /// </summary>
        public IXCoreMenuItemSeparator SeparatorBefore { get { return this.m_sepBefore; } }
        /// <summary>
        /// get the after separator. null if not define
        /// </summary>
        public IXCoreMenuItemSeparator SeparatorAfter { get { return this.m_sepAfter; } }
        /// <summary>
        /// get if this context menu is root menu
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
        public string ImageKey { get { return this.m_imageKey; } set{
            this.m_imageKey = value;
        }}
        public ICoreContextMenu OwnerContext {
            get {
                return m_ownerContext;
            }
            private set {
                m_ownerContext = value;
            }
        }
        public CoreContextMenuBase()
        {            
            CoreContextMenuAttribute v_attr =
               (CoreContextMenuAttribute)
               Attribute.GetCustomAttribute(this.GetType(),
               typeof(CoreContextMenuAttribute));
            this.SetAttribValue(v_attr);
            this.m_registerContext = false;
            this.m_childs = new CoreContextMenuActionCollection(this);
            this.m_IsRootMenu = true;
            this.WorkBenchChanged += new EventHandler(_WorkBenchChanged);
        }
        /// <summary>
        /// used this method to init menu action perperties
        /// </summary>
        /// <param name="attr"></param>
        public void SetAttribValue(CoreContextMenuAttribute attr)
        {
            if (attr != null)
            {
                this.m_index = attr.Index;
                this.m_id = attr.Name;
                this.m_imageKey = attr.ImageKey;
                this.m_captionKey = attr.CaptionKey;
                this.m_shortCut = attr.ShortCut ;
                this.m_isSepBefore = attr.SeparatorBefore;
                this.m_isSepAfter = attr.SeparatorAfter;
            }
        }
        void _WorkBenchChanged(object sender, EventArgs e)
        {
            foreach (ICoreContextMenuAction item in this.Childs)
            {
                CoreContextMenuBase t = item as CoreContextMenuBase;
                if (t != null)
                    t.Workbench = this.Workbench;
                //add tools trip menu items
                if (t.MenuItem != null)
                {
                    if (t.m_sepBefore != null)
                        this.MenuItem.Add(t.m_sepBefore);
                    this.MenuItem.Add(t.MenuItem);
                    if (t.m_sepAfter != null)
                        this.MenuItem.Add(t.m_sepAfter);
                }
            }
        }
        protected  IXCoreMenuItemSeparator CreateSeparator()
        {
            object obj = CoreControlFactory.CreateControl(CoreConstant.CTRL_MENU_SEPARATOR);
            return obj as IXCoreMenuItemSeparator;
        }
        public int Index
        {
            get { return m_index; }
            protected set
            {
                if (m_index != value)
                {
                    m_index = value;
                }
            }
        }
        public ICoreWorkingSurface CurrentSurface
        {
            get
            {
                if (this.Workbench != null)
                    return this.Workbench.CurrentSurface;
                return null;
            }
        }
        public event EventHandler WorkBenchChanged;
        protected virtual void OnWorkbenchChanged(EventArgs eventArgs)
        {
            if (WorkBenchChanged != null)
                this.WorkBenchChanged(this, eventArgs);
        }
        #region ICoreContextMenuAction Members
        public bool Visible
        {
            get {
                if (this.m_MenuItem !=null)
                    return this.m_MenuItem.Available;
                return false;
            }
            set {
                if (this.m_MenuItem != null)
                {
                    this.m_MenuItem.Available = value;
                }

            }
        }
        public bool Enabled
        {
            get {return  this.m_MenuItem.Enabled; }
            set { 
                if (this.m_MenuItem!=null)
                    this.m_MenuItem.Enabled = value; 
            }
        }
        public event EventHandler VisibleChanged {
            add { this.m_MenuItem.AvailableChanged  += value; }
            remove {this.m_MenuItem .AvailableChanged  -= value;}
        }
        public event EventHandler EnabledChanged {
            add { this.m_MenuItem.EnabledChanged += value; }
            remove { this.m_MenuItem.EnabledChanged -= value; }
        }
        #endregion
        #region ICoreAction Members
        public void DoAction()
        {
            if (this.Enabled)

            {
#if DEBUG
                CoreLog.WriteLine("[IGK] - CoreContextMenuAction [" + this.Id + "]");
#endif
                if (this.PerformAction())
                {
                    OnActionPerformed(EventArgs.Empty);
                }
            }
        }
        protected virtual  bool PerformAction()
        {
            return false;
        }
        private void OnActionPerformed(EventArgs eventArgs)
        {
            if (this.ActionPerformed != null)
                this.ActionPerformed(this, eventArgs);
        }
        #endregion
        #region ICoreIdentifier Members
        public string Id
        {
            get { return this.m_id; }
            protected internal set { this.m_id = value; }
        }
        #endregion
        #region ICoreAction Members
        public event EventHandler ActionPerformed;
        private string m_imageKey;
        private List<string> m_context;
        
        #endregion
        protected virtual void InitContextMenu()
        {
            //create menu item
            if (this.m_MenuItem == null)
            {
                var b = this.Workbench as ICoreLayoutManagerWorkbench;
                if (b == null)
                    return;
                this.m_MenuItem = b.CreateContextMenuItem();
            }
            if (this.m_MenuItem != null)
            {
                m_MenuItem.Click += m_menu_Click;
                m_MenuItem.OwnerChanged += m_MenuItem_OwnerChanged;
                m_MenuItem.EnabledChanged += m_MenuItem_EnabledChanged;
                m_MenuItem.AvailableChanged += m_MenuItem_VisibleChanged;
                m_MenuItem.DropDownClosed += m_MenuItem_DropDownClosed;
                m_MenuItem.ShowShortcutKeys = true;
                m_MenuItem.ContextMenuParent = this;
                if (this.m_shortCut !=  enuKeys .None )
                {
                    m_MenuItem.ShortcutKeyDisplayString = CoreResources.GetShortcutText (this.m_shortCut);
                }
                if (!string.IsNullOrEmpty (this.ImageKey ))
                {                    
                    this.m_MenuItem.MenuDocument = CoreResources.GetDocument(this.ImageKey);
                }
      
                if (this.IsSepearatorBefore )
                    this.m_sepBefore = CreateSeparator();
                if (this.IsSepearatorAfter)
                    this.m_sepAfter = CreateSeparator();
                IGK.ICore.Settings.CoreApplicationSetting.Instance.LangReloaded += _LangChangedChanged;
#if DEBUG
                IGK.ICore.Settings.CoreApplicationSetting.Instance.SettingChanged += Instance_SettingChanged;
#endif
                this.LoadDisplayText();
            }
        }
#if DEBUG
        void Instance_SettingChanged(object sender, Settings.CoreSettingChangedEventArgs e)
        {
            if (e.Value.Name == "ShowMenuIndex")
            {
                this.LoadDisplayText();
            }

        }
#endif
        private void _LangChangedChanged(object sender, EventArgs e)
        {
            this.LoadDisplayText();
        }
        void m_MenuItem_DropDownClosed(object sender, EventArgs e)
        {
            if (this.CurrentSurface != null)
            {
                CoreSystem.GetMainForm().Refresh();
            }
        }
        void m_MenuItem_VisibleChanged(object sender, EventArgs e)
        {
            //available changed
            if (this.m_sepAfter != null)
                this.m_sepAfter.Available = Visible;
            if (this.m_sepBefore != null)
                this.m_sepBefore.Available = Visible;
        }
        void m_MenuItem_EnabledChanged(object sender, EventArgs e)
        {
        }
        protected void RegisterChildMenu(CoreContextMenuBase childMenu, string subName, int index, string imageKey)
        {
            string v_name = string.Format(this.Id + ".{0}", subName);
            CoreContextMenuAttribute v_attr = new CoreContextMenuAttribute(v_name, index);
            v_attr.ImageKey = imageKey;
            childMenu.SetAttribValue(v_attr);
            childMenu.m_IsRootMenu = false;
            if (CoreSystem.RegisterAction(v_attr, childMenu) == false)
            {
                CoreMessageBox.Show(string.Format("Element {0} not registered", v_name));
            }
        }
        void m_MenuItem_OwnerChanged(object sender, EventArgs e)
        {
            //ToolStripDropDownMenu item = this.m_MenuItem.Owner as
            //    ToolStripDropDownMenu;
            ICoreContextMenu v_context = null;
            var m = this.m_MenuItem.Owner;
            if (this.m_MenuItem.Owner is ICoreContextMenu)
            {
                v_context = this.m_MenuItem.Owner as ICoreContextMenu;
                RegisterContext(v_context);               
            }
        }
        /// <summary>
        /// call IsEnabled and IsVisible
        /// </summary>
        protected void SetupEnableAndVisibility()
        {
            this.Visible = IsVisible();
            this.Enabled = IsEnabled();
        }
        private void RegisterContext(ICoreContextMenu v_context)
        {
            if (this.m_registerContext)
                return;
            if (v_context != null)
            {//register context event
                v_context.CheckForVisibility += context_Opening;
                //v_context.Opening += new System.ComponentModel.CancelEventHandler(context_Opening);
                v_context.Opened += context_Opened;
                v_context.Closed +=v_context_Closed;
                //    += new ToolStripDropDownClosedEventHandler(v_context_Closed);
                v_context.Closing +=v_context_Closing;
                  //  += new ToolStripDropDownClosingEventHandler(v_context_Closing);
                v_context.ItemAdded +=v_context_ItemAdded;
                    //+= new ToolStripItemEventHandler(v_context_ItemAdded);
                v_context.ItemRemoved +=v_context_ItemRemoved;
                    //+= new ToolStripItemEventHandler(v_context_ItemRemoved);
            }
            foreach (CoreContextMenuBase i in this.Childs)
            {
                i.RegisterContext(v_context);
            }
            this.m_ownerContext = v_context;
            this.m_registerContext = true;
        }
        void v_context_Closing(object sender, EventArgs  e)
        {
            OnContextClosing(e);
        }
        protected virtual  void OnContextClosing(EventArgs e)
        {
        }
        void v_context_Closed(object sender, EventArgs e)
        {
              OnContextClosed(e);
        }
        protected virtual void OnContextClosed(EventArgs e)
        {
        }
        void v_context_ItemRemoved(object sender, CoreItemEventArgs<CoreContextMenuBase> e)
        {
            //ToolStrip item = sender as ToolStrip;
            //if (e.Item == this.MenuItem)
            //{
            //    if (this.m_sepAfter != null) item.Items.Remove(this.m_sepAfter);
            //    if (this.m_sepBefore!= null) item.Items.Remove(this.m_sepBefore );
            //}
        }
        void v_context_ItemAdded(object sender, CoreItemEventArgs<CoreContextMenuBase> e)
        {
            if ((e.Item !=null) && (e.Item.MenuItem == this.MenuItem))
            {
                if (this.IsRootMenu)
                {
                    int i = this.MenuItem.Owner.IndexOf(this.MenuItem); 
                    if (this.m_sepBefore != null)
                    {
                        this.MenuItem.Owner.Insert(i-1, this.m_sepBefore);
                    }
                    if (this.m_sepAfter != null)
                        this.MenuItem.Owner.Insert(
                            this.MenuItem.Owner.IndexOf(this.MenuItem) + 1, this.m_sepAfter);
                }
            }
        }
        void context_Opened(object sender, EventArgs e)
        {
            OnOpened(e);
        }
        void context_Opening(object sender, EventArgs e)
        {
            OnOpening(e);
            if (this.m_sepAfter != null)
                this.m_sepAfter.Available = Visible;
            if (this.m_sepBefore != null)
            {
                this.m_sepBefore.Available = Visible;                
            }
        }
        protected virtual void OnOpening(EventArgs e)
        {
            this.SetupEnableAndVisibility();
        }
        /// <summary>
        /// return this method to check the visibility of the context menu strip
        /// </summary>
        /// <returns></returns>
        protected virtual bool IsVisible()
        {
            ICoreContextMenu v_ctx = this.OwnerContext;
            bool v = false;
            if (v_ctx == null)
            {
                v = false;
            }
            else
                v = (v_ctx.SourceControl == CurrentSurface) ;
            return v;
        }
        /// <summary>
        /// override this method to check the enabled of the context menu strip
        /// </summary>
        /// <returns></returns>
        protected virtual bool IsEnabled()
        {
            return this.IsVisible();
        }
        protected virtual void OnOpened(EventArgs e)
        {
        }
        void m_menu_Click(object sender, EventArgs e)
        {
            this.DoAction();
        }
        #region ICoreAction Members
        public ICoreSystemWorkbench Workbench
        {
            get { return this.m_WorkBench; }
            set {
                if (this.m_WorkBench != value)
                {
                    if (this.m_WorkBench != null)
                    {
                        UnregisterBenchEvent(this.m_WorkBench);
                    }
                    this.m_WorkBench = value;
                    if (this.m_WorkBench != null)
                    {
                        RegisterBenchEvent(this.m_WorkBench);
                        this.InitContextMenu();
                    }
                    OnWorkbenchChanged(EventArgs.Empty);
                }
            }
        }
        protected virtual  void RegisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            if ( Workbench is ICoreApplicationWorkbench c)
                c.MainForm.Closed += _MainFormClosed;
        }
        protected virtual void UnregisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            if (Workbench is ICoreApplicationWorkbench c)
                c.MainForm.Closed -= _MainFormClosed;
        }
        private void _MainFormClosed(object sender, CoreFormClosedEventArgs e)
        {
            DisposeMenu();
        }
        public virtual void Dispose()
        {
            DisposeMenu();
        }
        private void DisposeMenu()
        {
            if (this.m_sepBefore != null) this.m_sepBefore.Dispose();
            if (this.m_sepAfter != null) this.m_sepAfter.Dispose();
            if (this.m_MenuItem !=null) this.m_MenuItem.Dispose();

            //releas all handle
            this.m_sepAfter = null;
            this.m_sepBefore = null;
            this.m_MenuItem = null;
            this.m_registerContext = false;
            if (this.m_WorkBench != null)
            {
                this.UnregisterBenchEvent(this.m_WorkBench);
                this.m_WorkBench = null;
            }
        }
        #endregion
        public IXCoreContextMenuItemContainer  MenuItem {
            get {
                return this.m_MenuItem;
            }
        }
        #region ICoreContextMenuAction Members
        public ICoreContextMenuAction Parent
        {
            get { return this.m_Parent; }
        }
        public ICoreContextMenuActionCollections Childs
        {
            get {
                return this.m_childs;
            }
        }
        #endregion
        #region ICoreCaptionItem Members
        public void LoadDisplayText()
        {
#if DEBUG
            if (CoreApplicationSetting.Instance.ShowMenuIndex )
            {
                 this.MenuItem.Text = this.Index + ":"+ this.m_captionKey.R();
            }
            else{
                 this.MenuItem.Text = this.m_captionKey.R();
            }
#else
            this.MenuItem.Text = CoreSystem.GetString(this.m_captionKey );
#endif
        }
        #endregion
        class CoreContextMenuActionCollection : ICoreContextMenuActionCollections
        {
            List<ICoreContextMenuAction> m_menu;
            CoreContextMenuBase m_owner;
            public CoreContextMenuActionCollection (CoreContextMenuBase e)
            {
                this.m_owner = e;
                this.m_menu = new List<ICoreContextMenuAction>();
            }
            #region ICoreContextMenuActionCollections Members
            public ICoreContextMenuAction this[int index]
            {
                get { return this.m_menu[index]; }
            }
            public void Add(ICoreContextMenuAction action)
            {
                this.m_menu.Add(action);
                CoreContextMenuBase c = action as CoreContextMenuBase;
                if (c != null)
                {
                    c.m_Parent = this.m_owner;
                   // this.m_owner.MenuItem.DropDownItems.Add(c.MenuItem);
                }
            }
            public void Remove(ICoreContextMenuAction actions)
            {
                this.m_menu.Remove(actions);
                CoreContextMenuBase c = actions as CoreContextMenuBase;
                if (c != null)
                {
                    c.m_Parent = null;
                    if (c.MenuItem !=null)
                    c.MenuItem.Remove(c.MenuItem);
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
            #region ICoreContextMenuActionCollections Members
            public ICoreContextMenuAction[] ToArray()
            {
                return this.m_menu.ToArray();
            }
            public void Clear()
            {
                ICoreContextMenuAction[] t = this.ToArray();
                for (int i = 0; i < t.Length; i++)
                {
                    this.Remove(t[i]);
                }
            }
            public void AddRange(ICoreContextMenuAction[] v_tab)
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
                if (this.m_owner.MenuItem != null)
                {
                    //re-order child menu
                    ICoreContextMenuAction[] menu = this.ToArray();            
                    for (int i = 0; i < menu.Length; i++)
                    {
                        menu[i].Childs.Sort();
                    }
                }
            }
            #endregion
        }
        public virtual enuActionType ActionType
        {
            get { return enuActionType.ContextMenuAction; }
        }
        public string CaptionKey
        {
            get
            {
                return (this.Id);
            }
            set
            {
            }
        }
    }
}

