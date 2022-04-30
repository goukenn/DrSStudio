

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreToolBase.cs
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
file:CoreToolBase.cs
*/
using System;
namespace IGK.ICore.Tools 
{
	using IGK.ICore;using IGK.ICore.WinUI ;
	 /// <summary>
    /// represent the base class of all tool 
    /// </summary>
    public abstract class CoreToolBase : ICoreTool 
    {
        private ICoreToolHostedControl m_HostedControl;
        private string m_id;
        private ICoreSystemWorkbench m_WorkBench;
        private bool m_Enabled;
        public ICoreMainForm MainForm {
            get {
                if (this.m_WorkBench is ICoreApplicationWorkbench )
                    return (this.m_WorkBench as ICoreApplicationWorkbench).MainForm;
                return null;
            }
        }
        /// <summary>
        /// return a core tool base
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static CoreToolBase GetTool(string name)
        {
            return CoreSystem.GetTool(name) as CoreToolBase ;
        }
        /// <summary>
        /// configure the tool.
        /// </summary>
        public virtual void Configure()
        {
        }
        #region ICoreTool Members
        public ICoreToolHostedControl HostedControl
        {
            get { return m_HostedControl; }
            protected set {
                if (this.m_HostedControl != value)
                {
                    if (m_HostedControl != null)
                        this.UnregisterHostEvent();
                    m_HostedControl = value;
                    if (m_HostedControl != null)
                    {
                        m_HostedControl.Visible = false;
                        this.RegisterHostEvent();
                    }
                }
            }
        }
        private void UnregisterHostEvent()
        {
            this.m_HostedControl.VisibleChanged -= new EventHandler(this._VisibleChanged);
        }
        private void RegisterHostEvent()
        {
            this.m_HostedControl.VisibleChanged += new EventHandler(this._VisibleChanged);
        }
        public ICoreSystemWorkbench Workbench { 
            get { return this.m_WorkBench; }
            internal protected  set {
                if (this.m_WorkBench != value)
                {
                    if (this.m_WorkBench != null)
                        UnregisterBenchEvent(this.m_WorkBench);
                    this.m_WorkBench = value;
                    if (this.m_WorkBench != null)
                    {
                        RegisterBenchEvent(this.m_WorkBench);
                        GenerateHostedControl();                      
                    }
                    OnWorkbenchChanged(EventArgs.Empty);
                }
            }
        }
        protected virtual void UnregisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            if (this.Workbench is ICoreApplicationWorkbench s)
            s.MainForm.Closed -= MainForm_Closed;
        }
        protected virtual  void RegisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            if (this.Workbench is ICoreApplicationWorkbench s)
            s.MainForm.Closed += MainForm_Closed; 
        }
        void MainForm_Closed(object sender, CoreFormClosedEventArgs e)
        {
            if (this.HostedControl != null)
            {
                this.HostedControl.Dispose();
                this.HostedControl = null;
                this.m_WorkBench = null;
            }
        }
        private void OnWorkbenchChanged(EventArgs eventArgs)
        {
            WorkBenchChanged?.Invoke(this, eventArgs);
        }
        public event EventHandler WorkBenchChanged;
        public ICoreWorkingSurface CurrentSurface
        {
            get
            {
                if (this.m_WorkBench !=null)
                return m_WorkBench.CurrentSurface;
                return null;
            }
        }
        #endregion
        #region ICoreIdentifier Members
        public string Id
        {
            get { return this.m_id ; }
        }
        #endregion
        string m_ToolImageKey;
        public string ToolImageKey { get { return this.m_ToolImageKey; } }
        protected CoreToolBase()
        {
            CoreToolsAttribute v_attr
                =
                Attribute.GetCustomAttribute(this.GetType(),
                typeof(CoreToolsAttribute)) as CoreToolsAttribute;
            if (v_attr != null)
            {
                this.m_id = v_attr.Name;
                this.m_ToolImageKey = v_attr.ImageKey;
            }
        }
        void _VisibleChanged(object sender, EventArgs e)
        {
            OnVisibleChanged(EventArgs.Empty);
        }
        protected virtual void OnVisibleChanged(EventArgs e)
        {
            this.Configure();            
            VisibleChanged?.Invoke (this, e);
        }
        public virtual bool CanShow { get { return true; } }
        /// <summary>
        /// override this to generate your how hosted control
        /// </summary>
        protected virtual void GenerateHostedControl() { }
        #region ICoreTool Members
        public bool Enabled
        {
            get
            {
                return this.m_Enabled;
            }
            set
            {
                if (this.m_Enabled != value)
                {
                    this.m_Enabled = value;
                    OnEnabledChanged(EventArgs.Empty);
                }
            }
        }
        public bool Visible
        {
            get
            {
                if (this.HostedControl != null)
                {
                    return this.HostedControl.Visible;
                }
                return false;
            }
            set
            {
                if (this.HostedControl !=null)
                    this.HostedControl.Visible =value;               
            }
        }
        public event EventHandler VisibleChanged;
        public event EventHandler EnabledChanged;
        #endregion
        //protected virtual void OnVisibleChanged(EventArgs e)
        //{
        //    if (this.VisibleChanged != null)
        //        this.VisibleChanged(this, e);
        //}
        protected virtual void OnEnabledChanged(EventArgs e)
        {            
                EnabledChanged?.Invoke(this, e);
        }
}
}

