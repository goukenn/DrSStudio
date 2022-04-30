

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreMecanismActionBaseCollections_1.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore;using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Actions
{
    /// <summary>
    /// repesent the base mecanism action
    /// </summary>
    public class CoreMecanismActionBaseCollections :
              ICoreMecanismActionCollections,
              ICoreFilterMessageAction 
    {

        private Dictionary<enuKeys, ICoreMecanismAction> m_actions;
        private ICoreWorkingMecanism m_mecanism;
        public virtual int Priority { get { return 0; } }
        public ICoreWorkingMecanism Mecanism
        {
            get { return m_mecanism; }
        }
        public ICoreWorkbench Workbench
        {
            get
            {
                return CoreSystem.GetWorkbench();
            }
        }
        /// <summary>
        /// .ctr
        /// </summary>
        /// <param name="mecanism"></param>
        public CoreMecanismActionBaseCollections(ICoreWorkingMecanism mecanism)
        {
            this.m_mecanism = mecanism;
            this.m_actions = new Dictionary<enuKeys, ICoreMecanismAction>();
        }
        public void Add(enuKeys key, ICoreMecanismAction action)
        {
            if ((key != enuKeys.None) && (!this.Contains(key)) && (action != null))
            {
                this.m_actions.Add(key, action);
                action.Mecanism = this.m_mecanism;
            }
        }
        public void Clear()
        {
            this.m_actions.Clear();
        }
        public int Count
        {
            get { return this.m_actions.Count; }
        }
        public bool Contains(enuKeys key)
        {
            if (key == enuKeys.None)
                return false;
            return this.m_actions.ContainsKey(key);
        }
        /// <summary>
        /// remove action
        /// </summary>
        /// <param name="key"></param>
        public void Remove(enuKeys key)
        {
            if (this.Contains(key))
            {
                this.m_actions.Remove(key);
            }
        }
        public ICoreMecanismAction this[enuKeys key]
        {
            get
            {
                if (this.m_actions.ContainsKey(key))
                    return this.m_actions[key];
                return null;
            }
            set
            {
                if (this.Contains(key))
                {
                    if (value == null)
                        this.m_actions.Remove(key);
                    else
                    {
                        //replace action
                        CoreLog.WriteLine("Mecanism Action replaced : " + key);
                        this.m_actions[key].Mecanism = null;
                        this.m_actions[key] = value;
                        value.Mecanism = this.m_mecanism;
                    }
                }
                else if (value != null)
                {
                    this.m_actions.Add(key, value);
                    value.Mecanism = this.m_mecanism;
                }
            }
        }
        #region IMessageFilter Members
        internal const int WM_KEYDOWN = 0x0100;
        internal const int WM_KEYUP = 0x0101;
        internal const int WM_CHAR = 0x0102;
        /// <summary>
        /// return true if something required before process the message
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public virtual bool IsNotAvailable(ICoreMessage m)
        {
            if ((this.Workbench != null) && (this.Workbench.CurrentSurface != this.m_mecanism.CurrentSurface))
            {
                return true;
            }
            if (m.Msg == 0x20a)    //Mouse WHEEL        
            {
                return false;
            }
            if (this.m_mecanism.CurrentSurface != null)
            {
                return !this.m_mecanism.CanProcessActionMessage(m);
            }
            return false;
        }
        /// <summary>
        /// prefilter message
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public virtual bool PreFilterMessage(ref ICoreMessage m)
        {
            if (this.IsNotAvailable(m))
                return false;
            enuKeys c = (enuKeys)m.WParam | CoreApplicationManager.ModifierKeys;
            switch (m.Msg)
            {
                case WM_KEYDOWN:
                    if (this.Contains(c))
                    {
                        this.m_actions[c].ShortCutDemand = c;
                        this.m_actions[c].DoAction();
                        return true;
                    }
                    return false;
                case WM_KEYUP:
                    if (this.Contains(c))
                    {
                        //already do action return false                    
                        return true;
                    }
                    break;
            }
            return false;
        }
        #endregion
    }
}
