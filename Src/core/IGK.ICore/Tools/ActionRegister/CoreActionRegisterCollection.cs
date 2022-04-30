

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreActionRegisterCollection.cs
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
file:CoreActionRegisterCollection.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.Tools
{
    using IGK.ICore;using IGK.ICore.Actions;
    using IGK.ICore.Menu;
    using IGK.ICore.WinUI;
    /// <summary>
    /// Represent core action register
    /// </summary>
    public class CoreActionRegisterCollection : System.Collections.IEnumerable 
    {
        internal const int WM_KEYDOWN = 0x0100;
        internal const int WM_KEYUP = 0x0101;
        internal const int WM_CHAR = 0x0102;


        Dictionary<ICoreAction, CoreActionItem> m_actions;
        Dictionary<enuKeys, List<CoreActionItem>> m_keyActions;
        private CoreActionRegisterTool coreActionRegisterTool;
        /// <summary>
        /// register action on tool
        /// </summary>
        /// <param name="tool"></param>
        public CoreActionRegisterCollection(CoreActionRegisterTool tool)
        {
            this.coreActionRegisterTool = tool;
            this.m_actions = new Dictionary<ICoreAction, CoreActionItem>();
            this.m_keyActions = new Dictionary<enuKeys, List<CoreActionItem>>();
        }
        /// <summary>
        /// get ienumerator of the action items
        /// </summary>
        /// <returns></returns>
        public System.Collections.IEnumerator GetEnumerator()
        {
            return this.m_actions.GetEnumerator();
        }
        internal  CoreActionItem[] ToArray() {
            return this.m_actions.Values.ToArray();//<CoreActionItem>();
        }
        public int Count { get { return this.m_actions.Count; } }
        public void Serialize(IGK.ICore.Codec.CoreXMLSerializer seri)
        { }
        public void Deserialize(IGK.ICore.Codec.CoreXMLDeserializer deseri)
        { }
        internal void Add(ICoreAction action, enuActionType actionType)
        {
            if ((action == null) || (action is ICoreMenuShortCutChild) || (this.m_actions.ContainsKey(action)))
                return;
            CoreActionItem item = new CoreActionItem(this, action, action.Id , actionType);
            this.m_actions.Add(action, item);
        }
        internal void Remove(ICoreAction action)
        {
            if ((action == null) || (!this.m_actions.ContainsKey(action)))
                return;
            CoreActionItem v_item = this.m_actions[action];
            this.RemoveKeysAction(v_item.Action.ShortCut, v_item);
            this.m_actions.Remove(action);
            v_item.Dispose();
        }
        public bool Contains(enuKeys key)
        {
            return this.m_keyActions.ContainsKey(key);
        }
        public bool PreFilterMessage(ref ICoreMessage m)
       {
            if (m.Msg == 0x20a)    //Mouse WHEEL        
            {
                return false;
            }
            if (CoreApplicationManager.ActiveForm != CoreSystem.GetMainForm())
            {
                return false ;
            }
            enuKeys c = m.WParam.ToKey() | CoreApplicationManager.ModifierKeys;
            switch (m.Msg)
            {
                case WM_KEYDOWN:
                    if (this.Contains(c))
                    {
                        return CallAction(c , m);
                    }
                    return false;
                case WM_KEYUP:
                    if (this.Contains(c))
                    {
                        //allready call return true
                        return true;
                    }
                    break;
            }
            return false;
        }
        private bool CallAction(enuKeys c, ICoreMessage msg)
        {
            if (this.m_keyActions[c].Count == 1)
            {
                var s = CoreSystem.Instance.Workbench.CurrentSurface;
                CoreActionItem action = this.m_keyActions[c][0];
                                    
                    if (action.ActionType == enuActionType.SurfaceAction)
                    {
                        if ((s !=null) && s.CanProcess(msg))
                        {
                            action.DoAction();
                            return true;
                        }
                        return false;
                    }
                    else
                    {
                        action.DoAction();
                        return true;
                    }
                
            }
            else
            { 
                System.Diagnostics.Debug.WriteLine("Location Key for multiple shortcut");
               
            }
            return false;
        }
        private bool KeyChar(ref ICoreMessage m)
        {
            int ik = m.WParam.ToInt32();
            enuKeys k = (enuKeys)char.ToUpper((char)(byte)ik);
            enuKeys s = CoreApplicationManager.ModifierKeys | k;
            if (this.Contains(s))
            {
                CallAction(s, m);
                //this.m_keyActions[s].DoAction();
                return true;
            }
            return false;
        }
        internal void RemoveKeysAction(enuKeys keys, CoreActionItem iCoreAction)
        {
            if (this.m_keyActions.ContainsKey (keys))                
            {
                this.m_keyActions[keys].Remove(iCoreAction );
                if (this.m_keyActions[keys].Count == 0) {
                    this.m_keyActions.Remove(keys);
                }
            }
        }
        internal void AddKeysAction(enuKeys keys, CoreActionItem iCoreAction)
        {
            if (iCoreAction == null) return;
            List<CoreActionItem> v_list;
            if (!this.m_keyActions.ContainsKey(keys))
            {
                v_list = new List<CoreActionItem>
                {
                    iCoreAction
                };
                this.m_keyActions.Add(keys, v_list);
            }
            else {
                v_list = this.m_keyActions[keys];
                if (!v_list .Contains (iCoreAction ))
                v_list.Add(iCoreAction);
            }
        }
        internal void RemoveKeysAction(enuKeys key, ICoreAction action)
        {
            this.Remove(action);
        }
    }
}

