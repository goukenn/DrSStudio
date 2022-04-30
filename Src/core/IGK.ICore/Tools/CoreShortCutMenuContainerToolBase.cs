

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreShortCutMenuContainerToolBase.cs
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
file:CoreShortCutMenuContainerToolBase.cs
*/
using IGK.ICore;using IGK.ICore.Actions;
using IGK.ICore.Menu;
using IGK.ICore.Resources;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Tools
{
    public abstract class CoreShortCutMenuContainerToolBase : 
        CoreToolBase,
        ICoreMenuMessageShortcutContainer
    {
        private enuKeys m_Key;
        /// <summary>
        /// get or set the key of this shortcut
        /// </summary>
        public enuKeys Key
        {
            get { return m_Key; }
            set
            {
                if (m_Key != value)
                {
                    m_Key = value;
                }
            }
        }
        private ICoreFilterToolMessage m_filter;
        private Dictionary<enuKeys, CoreMenuActionBase > sm_dicTool;
        protected  CoreShortCutMenuContainerToolBase()
        {
            sm_dicTool = new Dictionary<enuKeys, CoreMenuActionBase>();
        }
        public override bool CanShow
        {
            get { return false; }
        }
        protected override void GenerateHostedControl()
        {
            this.HostedControl = null;
        }
        public void Register(ICoreMenuAction menu)
        {
            this.AddTool(menu as CoreMenuActionBase);
        }
        public void Unregister(ICoreMenuAction menu)
        {
            this.RemoveTool(menu as CoreMenuActionBase);
        }
        protected virtual  void RemoveTool(CoreMenuActionBase item)
        {
            if (item == null) return;
            if (sm_dicTool.ContainsKey(item.ShortCut))
                sm_dicTool.Remove(item.ShortCut);
        }
        protected virtual void AddTool(CoreMenuActionBase item)
        {
            
            if (item == null) return;
            if ((item.ShortCut != enuKeys.None) && (!sm_dicTool.ContainsKey(item.ShortCut)))
            {
                CoreActionRegisterTool.Instance.RemoveKeysAction(item);
                sm_dicTool.Add(item.ShortCut, item);
                if (item.MenuItem != null)
                {
                    item.MenuItem.ShortcutKeys = enuKeys.None;
                    item.MenuItem.ShortcutKeyDisplayString = CoreResources.GetString(
                        enuKeys.Control | this.Key) + "," + CoreResources.GetString(item.ShortCut);
                }
            }
        }
        public bool IsFiltering
        {
            get
            {
                return (m_filter != null);
            }
        }
        public void StartFilter()
        {
            if (this.m_filter != null)
                return;
            if (this.Workbench is ICoreWorkbenchMessageFilter m)
            {
                this.m_filter = CoreApplicationManager.Application.ControlManager.CreateMenuMessageFilter(m,
                    this, this.Key);
                this.m_filter.StartFiltering();
            }
        }
        public void EndFilter()
        {
            if (this.m_filter != null)
            {
                this.m_filter.EndFilter();
            }
            this.m_filter = null;
            this.Workbench.FilteredAction = null;
        }
        public new ICoreWorkbenchMessageFilter Workbench {
            get {
                return base.Workbench as ICoreWorkbenchMessageFilter;
            }
        }
        #region ICoreMenuShortcutContainer Members
        public virtual void Call(enuKeys k)
        {            
            sm_dicTool[k].DoAction();
        }
        public virtual bool Contains(enuKeys k)
        {
            return sm_dicTool.ContainsKey(k);
        }
        #endregion
        public int Count
        {
            get { return sm_dicTool.Count; }
        }
        public System.Collections.IEnumerator GetEnumerator()
        {
            return sm_dicTool.Values.GetEnumerator();
        }
    }
}

