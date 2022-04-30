

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreActionRegisterTool.cs
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
file:CoreActionRegisterTool.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IGK.ICore.Tools.ActionRegister;

namespace IGK.ICore.Tools
{
    using IGK.ICore;using IGK.ICore.Actions;
    using IGK.ICore.WinUI;
    using System.Collections;
    using System.Threading;
    /// <summary>
    /// represent a base action register to filter system actions
    /// </summary>
    [CoreTools("Tool.CoreActionRegisterTool")]
    public sealed class CoreActionRegisterTool : CoreToolBase, ICoreActionRegisterTool, ICoreMessageFilter
    {
        private static CoreActionRegisterTool sm_instance;
        private FilterMessageActionCollection m_filterMessages;
        private CoreActionRegisterCollection m_actions;
        private List<Thread > m_threads;
        private bool sm_regAppExit;
        private ICoreActionRegisterWorkbench m_bench;

        public new ICoreActionRegisterWorkbench Workbench {
            get {
                return base.Workbench as ICoreActionRegisterWorkbench;
            }
             set {
                base.Workbench = value as ICoreWorkbench ;
            }
        }
        private CoreActionRegisterTool()
        {
            this.m_actions = new CoreActionRegisterCollection(this);
            this.m_filterMessages = new FilterMessageActionCollection(this);
            m_threads = new List<Thread>();
        }
        public override bool CanShow
        {
            get
            {
                return false;
            }
        }
        protected override void GenerateHostedControl()
        {
            base.GenerateHostedControl();
            RegOnThread();
        }
        public static CoreActionRegisterTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static CoreActionRegisterTool()
        {
            sm_instance = new CoreActionRegisterTool();
            //register on thread
            sm_instance.RegOnThread();
            CoreSystem.Instance.WorkbenchChanged += Instance_WorkbenchChanged;
        }

        private static void Instance_WorkbenchChanged(object sender, EventArgs e)
        {
            sm_instance.m_bench?.UnregisterMessageFilter(sm_instance);
            (sm_instance as CoreToolBase).Workbench = CoreSystem.Instance.Workbench;
            //register on thread
           // sm_instance.RegOnThread();
        }

        /// <summary>
        /// register on current thread for prefilter message
        /// </summary>
        private void RegOnThread()
        {
            var v_wb = this.Workbench;
            if ((v_wb == null) ||this.m_threads.Contains(Thread.CurrentThread))
                return;
            v_wb.RegisterMessageFilter(this);
            if (!sm_regAppExit)
            {
                CoreApplicationManager.ApplicationExit += _ApplicationExit;
                sm_regAppExit = true;
            }
            this.m_threads.Add(Thread.CurrentThread);
            this.m_bench = v_wb;
        }
        private void _ApplicationExit(object sender, EventArgs e)
        {
            if (this.Workbench != null)
                this.Workbench.UnregisterMessageFilter(sm_instance);
        }
        /// <summary>
        /// get the registered action
        /// </summary>
        public CoreActionRegisterCollection Actions {
            get {
                return this.m_actions;
            }
        }
        public void Register(ICoreAction action)
        {
            this.m_actions.Add(action, action.ActionType);
        }
        public void Unregister(ICoreAction action)
        {
            this.m_actions.Remove(action);
        }
        public void AddFilterMessage(ICoreFilterMessageAction prefilter)
        {
            if ((prefilter == null) || (this.m_filterMessages.Contains(prefilter)))
                return ;
            this.m_filterMessages.Add(prefilter);
        }
        public void RemoveFilterMessage(ICoreFilterMessageAction prefilter)
        {
            if ((prefilter == null) || (!this.m_filterMessages.Contains(prefilter)))
                return ;
            this.m_filterMessages.Remove(prefilter);
        }
        /// <summary>
        /// Filter message
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public bool PreFilterMessage(ref ICoreMessage m)
        {
            //#if DEBUG
            //            CoreLog.WriteDebug("ActionRegister Message : "+ m.ToString ());
            //#endif
            //m.Msg == User32.WM_KEYDOWN
            if (m.IsKeyInputMessage())
            {
                foreach (ICoreFilterMessageAction item in this.m_filterMessages)
                {
                    if (item.PreFilterMessage(ref m))
                        return true;
                }
                return this.m_actions.PreFilterMessage(ref m);
            }
            return false;
        }
        /// <summary>
        /// represent a filtered message action
        /// </summary>
        sealed class FilterMessageActionCollection :IEnumerable,
            IComparer<ICoreFilterMessageAction> 
        {
            private List<ICoreFilterMessageAction> m_filters;
            private CoreActionRegisterTool m_tool;
            public FilterMessageActionCollection(CoreActionRegisterTool tool)
            {
                this.m_tool = tool;
                this.m_filters = new List<ICoreFilterMessageAction>();
            }
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_filters.GetEnumerator ();
            }
            public void Add(ICoreFilterMessageAction filter)
            {
                if ((filter == null) || (this.m_filters.Contains(filter)))
                    return;
                this.m_filters.Add(filter);
                this.m_filters.Sort(this);
            }
            public void Remove(ICoreFilterMessageAction filter)
            {
                if ((filter == null) || (!this.m_filters.Contains(filter)))
                    return;
                this.m_filters.Remove(filter);
                this.m_filters.Sort(this);
            }
            public int Count { get { return this.m_filters.Count;  } }
            public override string ToString()
            {
                return string.Format("FilterMessage#[{0}]", this.Count);
            }
            internal bool Contains(ICoreFilterMessageAction prefilter)
            {
                if (prefilter == null) return false ;
                return (this.m_filters.Contains(prefilter));
            }

            public int Compare(ICoreFilterMessageAction x, ICoreFilterMessageAction y)
            {
                return x.Priority.CompareTo(y.Priority);
            }
        }
        internal void RemoveKeysAction(ICoreAction action)
        {
            this.m_actions.Remove (action);
        }
    }
}

