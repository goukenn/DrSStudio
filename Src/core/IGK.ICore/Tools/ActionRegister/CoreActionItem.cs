

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreActionItem.cs
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
file:CoreActionItem.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.Tools
{
    using IGK.ICore;using IGK.ICore.Actions;
    using IGK.ICore.Menu;
    /// <summary>
    /// represent a core action item class
    /// </summary>
    internal class CoreActionItem : IDisposable 
    {
        private string m_Name;
        private enuActionType  m_ActionType;
        private ICoreAction m_Action;
        private CoreActionRegisterCollection m_actionregister;
        /// <summary>
        /// get the hosted action
        /// </summary>
        public ICoreAction Action
        {
            get { return m_Action; }
        }
        /// <summary>
        /// get or set the action type
        /// </summary>
        public enuActionType  ActionType
        {
            get { return m_ActionType; }
            set {
                if (m_ActionType != value)
                {
                    m_ActionType = value;
                }
            }
        }
        /// <summary>
        /// get or set name of this action
        /// </summary>
        public string Name
        {
            get { return m_Name; }
            set
            {
                if (m_Name != value)
                {
                    m_Name = value;
                }
            }
        }
        public CoreActionItem(CoreActionRegisterCollection actionregister, ICoreAction action, string name , enuActionType type)
        {
            this.m_actionregister = actionregister;
            this.m_Name = name;
            this.m_ActionType = type;
            this.m_Action = action;
            CoreMenuActionBase v_menu = action as CoreMenuActionBase;
            if (v_menu != null)
            {
                v_menu.EnabledChanged += new EventHandler(v_menu_EnabledChanged);
                this.Update(v_menu);
            }
        }
        public void DoAction()
        {
            this.m_Action.DoAction();
        }
        private void Update(CoreMenuActionBase v_menu)
        {
            if (v_menu.Enabled)
            {
                this.Update();
            }
            else
            {
                this.Disable();
            }
        }
        void v_menu_EnabledChanged(object sender, EventArgs e) {
            CoreMenuActionBase v_menu = sender as CoreMenuActionBase;
            Update(v_menu);
        }
        private void Disable()
        {
            this.m_actionregister.RemoveKeysAction(this.m_Action.ShortCut, this);
        }
        void Update()
        {         
             this.m_actionregister.AddKeysAction(this.m_Action.ShortCut, this);
        }
        public override string ToString()
        {
            return string.Format("CoreActionItem:[#{0}]", this.m_Action.Id);
        }
        public void Dispose()
        {
            CoreMenuActionBase v_menu = this.m_Action  as CoreMenuActionBase;
            if (v_menu != null)
            {
                v_menu.EnabledChanged -= new EventHandler(v_menu_EnabledChanged);
                this.Update(v_menu);
            }
        }
    }
}

