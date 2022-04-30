

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreWorkbenchBase.cs
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
file:CoreWorkbenchBase.cs
*/
using IGK.ICore;
using IGK.ICore.Actions;
using IGK.ICore.Menu;
using IGK.ICore.Tools;
using IGK.ICore.Web;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Common;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore
{
    [Serializable()]
    /// <summary>
    /// represent the base workbench for hosting main form and surface manager
    /// </summary>
    public abstract class CoreWorkbenchBase : 
        ICoreWorkbench,
        ICoreHelpWorkbench,
        ICoreSolutionManagerWorkbench,
        ICoreSurfaceManagerWorkbench ,
        ICoreFileManagerWorkbench ,
        ICoreWebWorkbench
    {
        
        private ICoreWorkingSurface m_currentSurface;
        private ICoreWorkbenchLayoutManager m_layoutManager;
        private ICoreMainForm m_mainForm;
        private ICoreWorkingSurfaceCollections m_surfaces;     
        private ICoreWorkingProject m_CurrentProject; //get the current project projet in workbench
        private ICoreWorkingProjectSolution  m_Solution;  // get the solution in drsstudio

        /// <summary>
        /// get or set the current solution
        /// </summary>
        public ICoreWorkingProjectSolution  Solution
        {
            get { return m_Solution; }
            set
            {
                if (m_Solution != value)
                {
                    CoreWorkingElementChangedEventArgs<ICoreWorkingProjectSolution> e = new CoreWorkingElementChangedEventArgs<ICoreWorkingProjectSolution>(this.m_Solution, value);
                    m_Solution = value;
                    OnSolutionChanged(e);
                }
            }
        }

        public event EventHandler SolutionChanged;

        protected virtual  void OnSolutionChanged(EventArgs eventArgs)
        {
           SolutionChanged?.Invoke(this, eventArgs);
            
        }

        /// <summary>
        /// get or set the currnet project
        /// </summary>
        public ICoreWorkingProject CurrentProject
        {
            get { return m_CurrentProject; }
            set
            {
                if (m_CurrentProject != value)
                {
                    CoreWorkingElementChangedEventArgs<ICoreWorkingProject> e = new CoreWorkingElementChangedEventArgs<ICoreWorkingProject>(m_CurrentProject, value);
                    m_CurrentProject = value;
                    OnCurrentProjectChanged(e);
                }
            }
        }
        protected virtual void OnCurrentProjectChanged(CoreWorkingElementChangedEventArgs<ICoreWorkingProject> e)
        {
            if (this.CurrentProjectChanged != null)
            {
                this.CurrentProjectChanged(this, e);
            }
        }
        
        public ICoreMainForm MainForm
        {
            get{ return this.m_mainForm;}
        }
        public CoreWorkbenchBase(ICoreMainForm mainForm)
        { 
            this.m_mainForm = mainForm;
            this.m_layoutManager = CreateLayoutManager();
            this.CurrentSurfaceChanged += _CurrentSurfaceChanged;
        }

        void _CurrentSurfaceChanged(object sender, CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            var g = e.NewElement as ICoreWorkingProjectSolutionSurface ;
            if (g != null)
            {
                this.Solution = g.Solution;
            }
            else
                this.Solution = null;
        }

        /// <summary>
        /// overrie this to create your layout manager
        /// </summary>
        /// <returns></returns>
        protected abstract ICoreWorkbenchLayoutManager CreateLayoutManager();

        public ICoreWorkingSurface CurrentSurface
        {
            get
            {
                return this.m_currentSurface;
            }
            set
            {
                //
                //replace the child with the current childs if not empty
                //
                if ((value !=null) && (value.CurrentChild !=null))
                    value = value .CurrentChild;    
                if (this.m_currentSurface != value)
                {
                    CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> c = new CoreWorkingElementChangedEventArgs<ICoreWorkingSurface>(this.m_currentSurface , value );
                    this.m_currentSurface = value;
                    OnCurrentSurfaceChanged(c);
                }
            }
        }
        protected virtual  void OnCurrentSurfaceChanged(CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            if (this.CurrentSurfaceChanged != null)
                this.CurrentSurfaceChanged(this, e);
        }
        public ICoreWorkbenchLayoutManager LayoutManager
        {
            get { return m_layoutManager; }
        }
        public virtual bool MenuActionMessageFiltering
        {
            get { return false; }
        }
        public abstract ICoreMenuMessageShortcutContainer FilteredAction
        {
            get;
            set;
        }
        public event EventHandler<CoreWorkingElementChangedEventArgs<ICoreWorkingSurface>> CurrentSurfaceChanged;
        public event EventHandler<CoreWorkingFileOpenEventArgs> FileOpened;
        public event EventHandler <CoreProjectOpenedEventArgs> ProjectOpened;
        protected virtual void OnProjectOpened(CoreProjectOpenedEventArgs e)
        {
            if (this.ProjectOpened != null)
                this.ProjectOpened(this, e);
        }
        protected virtual void OnFileOpened(CoreWorkingFileOpenEventArgs e)
        {


            if (this.FileOpened != null)
                this.FileOpened(this, e);
        }
        public abstract void OpenFile(params string[] files);
        public ICoreScreen Screen
        {
            get { return null; }
        }
        public abstract ICoreDialogForm CreateNewDialog();
        public abstract ICoreDialogForm CreateNewDialog(ICoreControl baseControl);
        public abstract void ShowAbout();
        public abstract void Open();
        public abstract bool CreateNewProject();
        public abstract bool CreateNewFile();
        public ICoreWorkingSurfaceCollections Surfaces
        {
            get {
                if (m_surfaces == null)
                {
                    m_surfaces = CreateSurfaceCollections();
                }
                return m_surfaces; 
            }
        }
        protected virtual  ICoreWorkingSurfaceCollections CreateSurfaceCollections()
        {
            return new CoreWorkbenchSurfaceCollections(this);
        }
        /// <summary>
        /// get the workbench tool renderer
        /// </summary>
        public abstract class CoreWorkbenchToolRenderer : ICoreDialogToolRenderer
        {
            private CoreWorkbenchBase m_workbench;
            private ICoreControl m_target;
            private ICoreWorkingConfigurableObject m_objectToConfigure;
            public  ICoreControl Target { get{return this.m_target ;} }
            public  ICoreWorkingConfigurableObject Object { get { return this.m_objectToConfigure; } }
           
            /// <summary>
            /// .ctr
            /// </summary>
            /// <param name="workbench">workbench</param>
            /// <param name="target">target control</param>
            /// <param name="objectToConfigure">object to configure</param>
            public CoreWorkbenchToolRenderer(CoreWorkbenchBase workbench, ICoreControl target, ICoreWorkingConfigurableObject objectToConfigure)
            {
                this.m_workbench = workbench;
                this.m_target = target;
                this.m_objectToConfigure = objectToConfigure;
            }
            /// <summary>
            /// override this method to render target item
            /// </summary>
            /// <param name="target"></param>
            /// <param name="obj"></param>
            /// <returns></returns>
            public abstract bool BuildWorkingProperty();
            public abstract void Reload();
            public abstract void Reset();
        }
        [Serializable()]
        public class CoreWorkbenchSurfaceCollections : ICoreWorkingSurfaceCollections 
        {
            private CoreWorkbenchBase m_workbench;
            private List<ICoreWorkingSurface> m_surfaces;
            public CoreWorkbenchSurfaceCollections(CoreWorkbenchBase coreWorkbenchBase)
            {
                this.m_workbench = coreWorkbenchBase;
                this.m_surfaces = new List<ICoreWorkingSurface>();
            }
            public virtual bool CanAdd
            {
                get { return true; }
            }
            public bool CanRemove
            {
                get { return true; }
            }
            public ICoreWorkingSurface this[int index]
            {
                get {
                    if ((index >=0)&& (index < this.Count ))
                        return this.m_surfaces[index];
                    return null;
                }
            }
            public ICoreWorkingSurface this[string key]
            {
                get { 
                    foreach (var item in this.m_surfaces)
	                {
                                        if (item.Id == key )
                                            return item;
	                }
                    return null;
                }
            }
            public int Count
            {
                get {
                    return this.m_surfaces.Count;
                }
            }
            public virtual void Add(ICoreWorkingSurface surface)
            {
                if ( (surface!=null) && !this.m_surfaces.Contains(surface))
                {
                    if (!this.m_workbench.IsValidSurface(surface ))
                    {
                        CoreMessageBox.NotifyMessage(
                            "title.error".R(),
                            "err.surfacenotvalidrequirement".R());
                    }
                    else
                    {
                        this.m_surfaces.Add(surface);
                        this.m_workbench.OnSurfaceAdded(new CoreItemEventArgs<ICoreWorkingSurface>(surface));
                    }
                }
            }
            public virtual void Remove(ICoreWorkingSurface surface)
            {
                if (this.m_surfaces.Contains(surface))
                {
                    int i = this.m_surfaces.IndexOf(surface);
                    this.m_surfaces.Remove(surface);
                    this.m_workbench.OnSurfaceRemoved(new CoreItemEventArgs<ICoreWorkingSurface>(surface));
                    if (this.Count > 0)
                    {
                        if (i > 0)
                            this.m_workbench.CurrentSurface = this.m_surfaces[i - 1];
                        else {
                            this.m_workbench.CurrentSurface = this.m_surfaces[0];
                        }
                    }
                }
            }
            public ICoreWorkingSurface[] ToArray()
            {
                return this.m_surfaces.ToArray();
            }
            public bool Contains(ICoreWorkingSurface surface)
            {
                return this.m_surfaces.Contains(surface);
            }
            public int IndexOf(ICoreWorkingSurface surface)
            {
                return this.m_surfaces.IndexOf(surface);
            }
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_surfaces.GetEnumerator();
            }
        }
        /// <summary>
        /// event raison when current project changed
        /// </summary>
        public event EventHandler<CoreWorkingElementChangedEventArgs<ICoreWorkingProject>> CurrentProjectChanged;
        /// <summary>
        /// event raised when curren surface added
        /// </summary>
        public event EventHandler<CoreItemEventArgs<ICoreWorkingSurface>> SurfaceAdded;
        public event EventHandler<CoreItemEventArgs<ICoreWorkingSurface>> SurfaceRemoved;
        ///<summary>
        ///raise the SurfaceRemoved 
        ///</summary>
        protected virtual void OnSurfaceRemoved(CoreItemEventArgs<ICoreWorkingSurface> e)
        {
            if (SurfaceRemoved != null)
                SurfaceRemoved(this, e);
            if (this.m_surfaces.Count == 0)
                this.CurrentSurface = null;

            OnSurfaceClosed(new CoreSurfaceClosedEventArgs(e.Item));
        }
        ///<summary>
        ///raise the SurfaceRemoved 
        ///</summary>
        protected virtual void OnSurfaceAdded(CoreItemEventArgs<ICoreWorkingSurface> e)
        {
            if (SurfaceAdded != null)
                SurfaceAdded(this, e);
            if (this.CurrentSurface == null)
                this.CurrentSurface = e.Item;
        }
        ///<summary>
        ///raise the HelpMessageChanged 
        ///</summary>
        public virtual void OnHelpMessage(string message)
        {
            if (this.m_helpMessageListener != null) {
                this.m_helpMessageListener.SendMessage (message);
            }
        }
        public abstract void ShowOptionsAddSetting();
        public abstract enuDialogResult ConfigureWorkingObject(ICoreWorkingConfigurableObject item, string tilen, bool allowCancel, Size2i defaultSize);
        public virtual enuDialogResult ConfigureWorkingObject(ICoreWorkingConfigurableObject item) {
            return this.ConfigureWorkingObject(item, "title.configure".R(), false, Size2i.Empty );
        }
        public abstract void BuildWorkingProperty(ICoreControl targetControl, ICoreWorkingConfigurableObject objToConfigure);
        public void CallAction(string actionName)
        {
            ICoreAction ack = CoreSystem.GetAction(actionName.ToLower ());
            if (ack != null) ack.DoAction();
        }
        public abstract  void OpenProject(string projectName);
        public abstract bool IsFileOpened(string filename);
        public abstract bool IsSolutionOpened(string filename);

        public abstract void RegisterMessageFilter(ICoreMessageFilter messageFilter);
        public abstract void UnregisterMessageFilter(ICoreMessageFilter messageFilter);
        public void Run(ICoreRunnableMainForm mainform)
        {
            if (mainform !=null)
                mainform.Run();
        }
        public ICoreStartForm StartForm
        {
            get {
                return null;
            }
        }
        public abstract ICoreActionRegisterTool ActionRegister
        {
            get;
        }

        public ICoreWorkbenchEnvironment Environment
        {
            get
            {
               return this.m_environment;
            }

            set
            {
                if (this.m_environment != value) {
                   var e = new CoreWorkingElementChangedEventArgs<ICoreWorkbenchEnvironment>(m_environment, value);
                    this.m_environment = value ;
                    OnEnvironmentChanged(e);
                }
            }
        }

        protected virtual  void OnEnvironmentChanged(CoreWorkingElementChangedEventArgs<ICoreWorkbenchEnvironment> e)=>this.EnvironmentChanged?.Invoke(this, e);
       

        public abstract enuDialogResult ShowDialog(string title, ICoreControl control);
        
        public abstract void Show(string title, ICoreControl control);

        public abstract IXCoreSaveDialog CreateNewSaveDialog();

        public abstract IXCoreOpenDialog CreateOpenFileDialog();
        public abstract IXCoreFontDialog CreateFontDialog();
        public abstract IXCoreColorDialog CreateColorDialog();
        public abstract IXCoreJobDialog CreateJobDialog();
        public abstract IXCoreWaitDialog CreateWaitDialog();

        public event EventHandler JobStart;

        public event CoreJobProgressEventHandler JobProgress;

        public event EventHandler JobComplete;

        public void BeginJob(object obj)
        {
            lock (this)
            {
                if (JobStart != null)
                    JobStart(obj, EventArgs.Empty);
            }
        }

        public void UpdateJob(object obj, int value)
        {
            lock (this)
            {
                if (JobProgress  != null)
                    JobProgress(obj, value);
            }
        }
        

        public void EndJob(object obj)
        {
            lock (this)
            {
                if (JobComplete != null)
                    JobComplete(obj, EventArgs.Empty);
            }
        }

        public event EventHandler ActionPerformed;

        public virtual void Init(CoreSystem coreSystem)
        {
            coreSystem.ActionPerformed += coreSystem_ActionPerformed;
            this.LayoutManager.InitLayout();
        }

        void coreSystem_ActionPerformed(object sender, EventArgs e)
        {
            //raise the bench action performed
            if (ActionPerformed != null)
                ActionPerformed(this, e);
        }


        public event EventHandler<CoreSurfaceClosedEventArgs> SurfaceClosed;
        public event EventHandler<CoreWorkingElementChangedEventArgs<ICoreWorkbenchEnvironment>> EnvironmentChanged;

        private ICoreWorkbenchHelpMessageListener m_helpMessageListener;
        private ICoreWorkbenchEnvironment m_environment;

        protected virtual void OnSurfaceClosed(CoreSurfaceClosedEventArgs e)
        {
            if (SurfaceClosed != null)
            {
                foreach (var s in this.SurfaceClosed.GetInvocationList())
                {
                    ((EventHandler<CoreSurfaceClosedEventArgs>)s).Invoke(this, e);
                    if (e.CancelDispose)
                        break;
                }
             
            }
            if (!e.CancelDispose)
            {
                e.Surface.Dispose();
            }
        }

        /// <summary>
        /// get the common dialog
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual IXCommonDialog  CreateCommonDialog(string name)
        {
            return null;            
        }
        public virtual T CreateCommonDialog<T>(string name)where T:IXCommonDialog 
        {
            var s = CreateCommonDialog(name);
            if (s is T)
                return (T)s;
            return default(T);
        }
        public T CreateCommonDialog<T>() where T : IXCommonDialog
        {
            return CreateCommonDialog<T>(typeof(T).Name);
        }
        internal protected bool IsValidSurface(ICoreWorkingSurface surface)
        {
            
                    CoreSurfaceAttribute
                        v_attr = CoreSurfaceAttribute.GetCustomAttribute(surface.GetType(),
                        typeof(CoreSurfaceAttribute)) as CoreSurfaceAttribute;
                    if (v_attr == null)
                    {
                        return false;
                    }
                    return true;
        }
        /// <summary>
        /// show the message box 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="enuMessageBoxType"></param>
        public virtual void ShowMessageBox(string title, 
            string message,
            enuCoreMessageBoxType enuMessageBoxType)
        {
            CoreMessageBox.Show(message, title, enuMessageBoxType);
        }


        public void AddSurface(ICoreWorkingSurface surface, bool selectCreatedSurface)
        {
            if (surface != null)
            {
                this.Surfaces.Add(surface);
                if (selectCreatedSurface)
                {
                    this.CurrentSurface = surface;
                }
            }
        }

        public void SetHelpMessageListener(ICoreWorkbenchHelpMessageListener helpMessageListener)
        {
            this.m_helpMessageListener = helpMessageListener;
        }

        public abstract ICoreWebDialogForm CreateWebBrowserDialog(ICoreWebScriptObject objectForScripting);

        public abstract ICoreWebDialogForm CreateWebBrowserDialog(ICoreWebScriptObject objectForScripting, string document);

        /// <summary>
        /// find the exact matching type surface
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T FindSurface<T>()
        {
            var t = typeof(T);
            foreach (var item in this.Surfaces)
            {
                if (item.GetType() == t)
                    return (T)item;
            }
            return default(T);
        }
    }
}

