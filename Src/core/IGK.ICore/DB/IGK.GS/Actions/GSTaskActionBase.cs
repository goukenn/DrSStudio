using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.WinUI.Configuration;
using IGK.ICore.Settings;






﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.GS.WinUI;

namespace IGK.GS.Actions
{
    /// <summary>
    /// represent the task action
    /// </summary>
    public abstract class GSTaskActionBase : GSActionBase 
    {
        private string m_Taskgroup;
        private Dictionary<string, GSActionBase> m_actions;
        protected virtual void BuildRegAction()
        {
            m_actions.Clear();
           
        }
        public  string[] GetActionsList() {
            return m_actions.Keys.ToArray();
        }
        protected void RegAction(string funcName, GSActionBase action)
        {
            if (!string.IsNullOrEmpty(funcName) &&
                !m_actions.ContainsKey(funcName) && (action != null))
            {
                m_actions.Add(funcName, action);
            }
        }
        public GSTaskActionBase()
        {
            this.m_actions = new Dictionary<string, GSActionBase>();
            this.BuildRegAction();
        }

        /// <summary>
        /// represent the group name of this task
        /// </summary>
        public string Taskgroup
        {
            get { return m_Taskgroup; }
            set
            {
                if (m_Taskgroup != value)
                {
                    m_Taskgroup = value;
                }
            }
        }
        /// <summary>
        /// return the exposed action list of this Task
        /// </summary>
        /// <param name="funcName"></param>
        /// <returns></returns>
        public virtual GSActionBase GetAction(string funcName)
        {
            if (this.m_actions.ContainsKey (funcName))
                return this.m_actions[funcName];
            return null;
        }
    }
}
