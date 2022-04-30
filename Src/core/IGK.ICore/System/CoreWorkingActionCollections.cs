

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreWorkingActionCollections.cs
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
file:CoreWorkingActionCollections.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore
{
    using IGK.ICore;using IGK.ICore.Actions ;
using IGK.ICore.ContextMenu;
using IGK.ICore.Menu;
    [Serializable ()]
    public sealed class CoreWorkingActionCollections
    {
        internal const string CTXMENU = "CTXMENU.{0}"; //context Menu Prefix
        internal const string MENU = "MENU.{0}"; //MenuPrefix
        private Dictionary<string, IGK.ICore.Actions .ICoreAction > m_actions
            = new Dictionary<string,IGK.ICore.Actions.ICoreAction> ();
        CoreSystem m_owner;


        public static CoreWorkingActionCollections GetActionCollections() {
            return CoreSystem.Instance.m_actions;
        }

        public ICoreAction this[string Action] {
            get {
                if (string.IsNullOrEmpty(Action))
                    return null;
                Action = Action.ToLower();
                if (m_actions.ContainsKey(Action))
                    return m_actions[Action];
                else if (m_actions.ContainsKey (string .Format (CTXMENU ,Action ).ToLower()))
                {
                    return m_actions[string.Format(CTXMENU, Action).ToLower()];
                }
                else if (m_actions.ContainsKey(string.Format(MENU, Action).ToLower ()))
                {
                    return m_actions[string.Format(MENU, Action).ToLower()];
                }
                return null;
            }
        }
        public int Count { get { return m_actions.Count; } }
        public CoreWorkingActionCollections(CoreSystem owner)
        {
            this.m_owner = owner;
            this.m_owner.RegisterTypeLoader (LoadType);
        }       

        void item_ActionPerformed(object sender, EventArgs e)
        {
            m_owner.PerformActionsEvent(sender, e);
        }
        void LoadType(Type type)
        {
            CoreActionAttribute a_k =
                Attribute.GetCustomAttribute(type, typeof(CoreActionAttribute)) as CoreActionAttribute;
            if ((a_k == null) || 
                (a_k is IGK.ICore.Menu.CoreMenuAttribute)||
                (a_k is IGK.ICore.ContextMenu.CoreContextMenuAttribute))
            {
                return;
            }
            //register simple action menu
            ICoreAction v_ack = type.Assembly.CreateInstance(type.FullName) as ICoreAction;
            string id = a_k.Name.ToLower();
            if ((v_ack != null)&& (!m_actions.ContainsKey (id)))
            {
                this.m_actions.Add(id, v_ack);
                OnActionRegistered(new CoreItemEventArgs<ICoreAction>(v_ack));
            }
        }
        //register context menu action
        internal bool Register(CoreContextMenuAttribute attr, ICoreContextMenuAction menu)
        {
            string v_CTXMENU = string.Format(CTXMENU , attr.Name).ToLower();
           
            if (!m_actions.ContainsKey(v_CTXMENU))
            {
                m_actions.Add(v_CTXMENU, menu);
                CoreActionManager.RegisterAction(menu);
                OnActionRegistered(new CoreItemEventArgs<ICoreAction>(menu));
                
                return true;
            }
            return false;
        }

        internal bool Register(CoreMenuAttribute attr, ICoreMenuAction menu)
        {
            string v_MENU = string.Format(MENU, attr.Name).ToLower();
          
            if (!m_actions.ContainsKey(v_MENU))
            {
                m_actions.Add(v_MENU, menu);
                CoreSystem.Instance.m_menus.Register(attr, menu);//bind menu

                CoreActionManager.RegisterAction(menu);
                OnActionRegistered(new CoreItemEventArgs<ICoreAction>(menu));
                return true;
            }
            return false;
        }

        public event EventHandler<CoreItemEventArgs<ICoreAction>> ActionRegistered;
        public void OnActionRegistered(CoreItemEventArgs<ICoreAction> e)
        {
            e.Item.ActionPerformed += item_ActionPerformed;
            if (this.ActionRegistered != null)
                this.ActionRegistered(this, e);
        }

        /// <summary>
        /// retrieve the menu action
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ICoreMenuAction GetMenuAction(string name)
        {
            string v_MENU = string.Format(MENU, name).ToLower ();
            if (this.m_actions.ContainsKey (v_MENU ))
                return this.m_actions[v_MENU] as ICoreMenuAction ;
            return null;
        }
        /// <summary>
        /// retreive a context menu action
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ICoreContextMenuAction GetContextMenuAction(string name)
        {
            string v_MENU = string.Format(CTXMENU, name).ToLower ();
            if (this.m_actions.ContainsKey (v_MENU ))
                return this.m_actions[v_MENU] as ICoreContextMenuAction ;
            return null;
        }

        public bool Unregister(string name) { 
                string v_MENU = string.Format(CTXMENU, name).ToLower ();
                if (this.m_actions.ContainsKey(v_MENU))
                {
                    this.m_actions.Remove(v_MENU);
                    this.m_owner.m_contextMenu.Unregister(name);
                    return true;
                }
               v_MENU = string.Format(MENU, name).ToLower ();
               if (this.m_actions.ContainsKey(v_MENU)) {
                   this.m_actions.Remove(v_MENU);
                   this.m_owner.m_menus.Unregister(name);
                   return true;
               }
               if (this.m_actions.ContainsKey(name.ToLower()))
               {
                   this.m_actions.Remove(name.ToLower());
                   return true;
               }
               return false;
        }

        public ICoreAction[] GetActions()
        {
            List<ICoreAction> tab = new List<ICoreAction>();
            foreach (var s in this.m_actions.Values)
            {
                tab.Add(s);
            }
            tab.Sort(new Comparison<ICoreAction>((x, y) =>
            {
                return x.Id.CompareTo(y.Id);
            }));
            return tab.ToArray();
        }
    }
}

