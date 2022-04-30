

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreMecanismBase.cs
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
file:CoreMecanismBase.cs
*/
using IGK.ICore;using IGK.ICore.Actions;
using IGK.ICore.MecanismActions;
using IGK.ICore.Tools;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.Mecanism
{
    /// <summary>
    /// represent a core mecanism base
    /// </summary>
    public abstract class CoreMecanismBase :
        IDisposable ,
        ICoreWorkingMecanism 
    {
        
        private ICoreSnippet m_Snippet; //snippet on mecanism
        private ICoreSnippetCollections m_RegSnippets; //registrated snippets
        private ICoreWorkingSurface m_currentSurface;
        private bool m_allowContextMenu;

        protected const int ST_NONE = 0;//none
        protected const int ST_CREATING = 1;//create an element
        protected const int ST_EDITING = 2; //edit the current element
        protected const int ST_MOVING = 4;
        protected const int ST_CONFIGURING = 0x10;

        private bool m_isDisposed;
        private bool m_DesignMode;
        private ICoreMecanismActionCollections m_Actions; //actions
        private int m_State;//state in this mecanism

        /// <summary>
        /// call an action
        /// </summary>
        /// <param name="p"></param>
        protected void CallAction(string p)
        {
            if (CoreSystem.Instance.Workbench != null)
            {
                CoreSystem.Instance.Workbench.CallAction(p);
            }
        }
        protected virtual Size2i DragSize {
            get {
                return new Size2i(16, 16);
            }
        }
        public abstract ICoreWorkingSurface Surface
        {
            get;
        }

        public bool MenuActionMessageFiltering
        {
            get
            {
                ICoreWorkbench b = CoreSystem.GetWorkbench();
                if (b != null)
                    return b.MenuActionMessageFiltering;
                return false;
            }
        }
        /// <summary>
        /// send help message 
        /// </summary>
        /// <param name="message"></param>
        protected virtual void SendHelpMessage(string message)
        {
            ICoreHelpWorkbench b = CoreSystem.GetWorkbench<ICoreHelpWorkbench>();
            if (b!=null)
                b.OnHelpMessage(message) ;
        }
      
        /// <summary>
        /// Get the current State of the mecanism
        /// </summary>
        public int State
        {
            get { return m_State; }
            protected set
            {
                if (m_State != value)
                {
                    m_State = value;
                }
            }
        }
        /// <summary>
        /// get the default mecanism cursor
        /// </summary>
        /// <returns></returns>
        protected virtual  ICoreCursor GetCursor()
        {
            return CoreCursors.Normal;
        }
        protected virtual bool IsSnippetSelected {
            get {
                return (this.Snippet != null);
            }
        }
        /// <summary>
        /// show snippets 
        /// </summary>
        protected void ShowSnippets()
        {
            this.GenerateSnippets();
            if (this.RegSnippets.Count > 0)
            {
                this.InitSnippetsLocation();
                this.EnabledSnippet();
            }
        }
        protected void HideSnippets() {
            if (this.RegSnippets.Count > 0)
            {
                this.DisableSnippet();
                this.DisposeSnippet();
            }
        }
        public ICoreMecanismActionCollections Actions
        {
            get {
                if (this.m_Actions == null)
                {
                    this.m_Actions = CreateActionMecanismCollections();               
                }
                return this.m_Actions;
            }
        }
        public event EventHandler SnippetChanged;
        public event EventHandler StateChanged;
        
        ///<summary>
        ///raise the StateChanged 
        ///</summary>
        protected virtual void OnStateChanged(EventArgs e)
        {
            if (StateChanged != null)
                StateChanged(this, e);
        }
        /// <summary>
        /// get or set the snippet changed
        /// </summary>
        public ICoreSnippet Snippet
        {
            get
            {
                return this.m_Snippet;
            }
            set
            {
                if (this.m_Snippet != value)
                {
                    this.m_Snippet = value;
                    OnSnippetChanged(EventArgs.Empty);
                }
            }
        }
        private void OnSnippetChanged(EventArgs eventArgs)
        {
            if (this.SnippetChanged != null)
                this.SnippetChanged(this, eventArgs);
        }
        public ICoreSnippetCollections RegSnippets
        {
            get
            {
                if (this.m_RegSnippets == null)
                    this.m_RegSnippets = CreateSnippetCollections();
                return this.m_RegSnippets;
            }
        }
        public CoreMecanismBase()
        {
            this.m_Snippet = null;
            this.GenerateActions();
        }
        public bool DesignMode
        {
            get { return m_DesignMode; }
            protected set
            {
                this.m_DesignMode = value;
            }
        }
        public bool IsShiftKey
        {
            get
            {
                return CoreApplicationManager.Application.ControlManager.IsShifKey;
            }
        }
        public bool IsControlKey
        {
            get
            {
                return CoreApplicationManager.Application.ControlManager.IsControlKey;
            }
        }
        protected virtual void GenerateActions()
        {
            this.Actions.Clear();
            this.AddAction(enuKeys.Control | enuKeys.E, new CoreEditPropertyMecanismAction());
            this.AddAction(enuKeys.Delete, new CoreDeleteElementMecanismAction());
        }
        public virtual void Dispose()
        {
            if (!this.m_isDisposed)
            {
                this.DisposeSnippet();
                this.m_isDisposed = true;
            }
        }
        protected void AddAction(enuKeys key, ICoreMecanismAction action)
        {
            this.Actions.Add(key, action);
        }
        protected abstract ICoreMecanismActionCollections CreateActionMecanismCollections();
        protected abstract ICoreSnippetCollections CreateSnippetCollections();


        protected internal void EnabledSnippet()
        {
            this.RegSnippets.Enable();
        }
        protected internal void DisableSnippet()
        {
            this.RegSnippets.Disabled();
        }
        protected internal void DisposeSnippet()
        {
            this.RegSnippets.Dispose();
        }
        protected internal virtual void GenerateSnippets()
        {
            DisposeSnippet();
            this.Snippet = null; //disable the current snippet
        }
        protected internal virtual void InitSnippetsLocation()
        {
        }
        protected abstract  ICoreSnippet CreateSnippet(int demand, int index);
        protected internal abstract WinUI.Configuration.ICoreWorkingConfigurableObject GetEditElement();

        /// <summary>
        /// override this method to determine if this mecanism can process a an message
        /// </summary>
        /// <param name="message"></param>
        /// <returns>true if can process message otherwise false</returns>
        public abstract bool CanProcessActionMessage(ICoreMessage message);
        /// <summary>
        /// invilade the surface. used for action that require to invalidate the surface when property changed
        /// </summary>
        public abstract void Invalidate();
        internal virtual void Delete(ICoreWorkingObject element)
        {
        }


        public bool AllowContextMenu
        {
            get {
                return this.m_allowContextMenu;
            }
            set {
                this.m_allowContextMenu = value;
            }
        }

        private bool m_AllowActions;
        public bool AllowActions
        {
            get
            {
                return m_AllowActions;
            }
            protected set
            {
                m_AllowActions = value;
            }
        }

        public virtual bool Register(ICoreWorkingSurface surface) {
            if (surface !=null)
                CoreActionRegisterTool.Instance.AddFilterMessage(this.Actions);
            this.m_currentSurface = surface;
            return true;
        }

        public virtual bool UnRegister() {            
                CoreActionRegisterTool.Instance.RemoveFilterMessage(this.Actions);
                return true;
        }

        public abstract bool IsFreezed
        {
            get;
        }

        public abstract void Freeze();

        public abstract void UnFreeze();

        public abstract void Edit(ICoreWorkingObject e);

        /// <summary>
        /// get or set the current surface
        /// </summary>
        public ICoreWorkingSurface CurrentSurface
        {
            get { return this.m_currentSurface; }
            protected set { 
                this.m_currentSurface = value;
            }
        }
    }
}

