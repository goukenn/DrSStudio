

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreActionBase.cs
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
file:CoreActionBase.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Actions
{
    using IGK.ICore;using IGK.ICore.WinUI;
    using IGK.ICore.Menu;
    using IGK.ICore.Tools;
    /// <summary>
    /// represent the base action single
    /// </summary>
    public abstract class CoreActionBase : MarshalByRefObject ,  ICoreAction, ICoreIdentifier
    {       

        private string m_id;
        private string m_ImageKey;

        public virtual object GetActionContext(string name)
        {
            return CoreSystem.GetActionContext(name);
        }
        public void BindExecutionContext(string name)
        {
            if (m_context == null)
                m_context = new List<string>();
            m_context.Clear();
            if (!string.IsNullOrEmpty(name))
            {
                string[] tb = name.ToLower().Split(',');
                foreach (var item in tb)
                {
                    if (CoreSystem.GetActionContext(item) != null)
                    {
                        m_context.Add(item);
                    }
                    
                }
                
            }

        }
        public bool IsInContext(string name)
        {
            if (!string.IsNullOrEmpty(name))
                return m_context.Contains(name.ToLower());
            return false;

        }
        /// <summary>
        /// .ctr
        /// </summary>
        public CoreActionBase()
        {
            CoreActionAttribute c = CoreActionAttribute.GetCustomAttribute(
                this.GetType(), typeof(CoreActionAttribute)) as CoreActionAttribute ;
            if (c != null)
            {
                this.SetAttribute(c);
            }
        }

        protected virtual void SetAttribute(CoreActionAttribute attr)
        {
            this.Id = attr.Name;
            
        }
        /// <summary>
        /// get the image key of this action
        /// </summary>
        public virtual string ImageKey
        {
            get { return m_ImageKey; }
            set
            {
                if (m_ImageKey != value)
                {
                    m_ImageKey = value;
                }
            }
        }
        #region ICoreAction Members
        public event EventHandler ActionPerformed;
        private List<string> m_context;
        public void DoAction()
        {
            if (PerformAction())  
            {
                OnActionPerformed(EventArgs.Empty);
            }
        }
        protected abstract bool PerformAction();
        #endregion
        /// <summary>
        /// override to get the primary shortcut
        /// </summary>
        public virtual enuKeys ShortCut { get { return enuKeys.None; } }
        public virtual string Id { get { return this.m_id; } set { this.m_id = value; } }
        /// <summary>
        /// raise action perform
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnActionPerformed(EventArgs e)
        {
            if (this.ActionPerformed !=null)
                this.ActionPerformed (this, e);
        }
        public static bool StartFilteringAction(ICoreWorkbenchMessageFilter Workbench, ICoreMenuMessageShortcutContainer managerAction)
        {
            if (!Workbench.MenuActionMessageFiltering && !managerAction.IsFiltering)
            {
                Workbench.FilteredAction = managerAction;
                managerAction.StartFilter();
                return true;
            }
            return false;
        }
        public virtual  enuActionType ActionType
        {
            get { return enuActionType.SystemAction; }
        }
    }
}

