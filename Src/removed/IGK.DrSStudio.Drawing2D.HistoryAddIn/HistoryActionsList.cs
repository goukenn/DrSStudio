

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: HistoryActionsList.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:HistoryActionsList.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
/* 
-------------------------------------------------------------
This file is part of iGK-DEV-DrawingStudio
-------------------------------------------------------------
-------------------------------------------------------------
-------------------------------------------------------------
view license file in Documentation folder to get more info
Copyright (c) 2008-2010 
Author  : Charles A.D. BONDJE DOUE 
mail : bondje.doue@hotmail.com
-------------------------------------------------------------
-------------------------------------------------------------
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.Codec;
    using IGK.DrSStudio.Drawing2D.WinUI;
    using IGK.DrSStudio.History;
    /// <summary>
    /// history action list
    /// </summary>
    public class HistoryActionsList :
        IHistory2DUndoAndRedo , 
        IEnumerable ,
        IHistoryActionList
    {
        private ICore2DDrawingSurface m_surface;     //action for surface
        private List<HistoryActionBase> m_actions;  //action list
        private string m_Folder;                    //folder to save 
        private bool m_undoing;                     //true when history list is undoing
        private bool m_redoing;                     //true when history  list is redoing
        private enuHistoryState m_historyState;        //get history state
        private int m_historyIndex;                 //reprennt the current history indexx
        private IHistoryAction m_CurrentAction;  //represent the current history action
        private IHistorySurfaceManager m_manager; //get the manager
        public event HistoryItemEventHandler HistoryItemAdded;
        public event HistoryChangedEventHandler HistoryChanged;
        public event EventHandler HistoryClear;
        public event EventHandler HistoryClearAt;
        /// <summary>
        /// get the surface that own element
        /// </summary>
        public ICore2DDrawingSurface CurrentSurface {
            get {
                return this.m_surface;
            }
        }
        public IHistorySurfaceManager Manager{get{return this.m_manager ;}}
        /// <summary>
        /// get the current history state
        /// </summary>
        public enuHistoryState HistoryState { get { return this.m_historyState; } }
        /// <summary>
        /// get the current history index
        /// </summary>
        public int HistoryIndex { get { return this.m_historyIndex; } }
        /// <summary>
        /// get if this can add items
        /// </summary>
        public bool CanAdd {
            get { 
                return !(this.m_redoing || this.m_undoing );
            }
        }
        internal HistoryActionsList(ICore2DDrawingSurface surface, IHistorySurfaceManager manager, string folder) 
        {
            if (manager ==null)
                throw new ArgumentNullException ("manager");
            if (surface == null)
                throw new ArgumentNullException ("surface");
            if (!IGK.DrSStudio.IO.PathUtils.CreateDir(folder)) 
                throw new CoreException (enuExceptionType.ArgumentIsNull , "HistoryActionList->folder");
            this.m_surface = surface;
            this.m_Folder = folder;
            this.m_actions = new List<HistoryActionBase>();
            this.m_manager = manager ;
            this.Clear();
            this.m_surface.Disposed += new EventHandler(m_surface_Disposed);
        }
        void m_surface_Disposed(object sender, EventArgs e)
        {
            //clear tempory folder
            if (System.IO.Directory.Exists(this.m_Folder))
            {
                Clear();
                System.IO.Directory.Delete(m_Folder, true);
                this.Manager.Remove(this);
            }
        }
        public string Folder
        {
            get { return m_Folder; }
        }
        public bool Undoing { get { return this.m_undoing; } }
        public bool Redoing { get { return this.m_redoing; } }
        /// <summary>
        /// get the nuber of action in this list
        /// </summary>
        public int Count {
            get {
                return this.m_actions .Count ;
            }
        }
        #region IUndoAndRedo Members
        public void Undo()
        {
            if ((this.m_CurrentAction ==null))
                return;
            if (this.m_undoing) return;
            HistoryChangedEventArgs e = null;
            this.m_undoing = true;
            switch (this.m_historyState)
            {
                case enuHistoryState.Undo:
                    this.m_CurrentAction.Undo();
                    if (this.m_CurrentAction.Previous != null)
                    {
                        this.m_historyIndex--;
                        this.m_historyState = enuHistoryState.Undo;
                        e = new HistoryChangedEventArgs(
                                this.m_historyState,
                                m_CurrentAction,
                                m_CurrentAction.Previous);
                        this.m_CurrentAction = m_CurrentAction.Previous;
                    }
                    else
                    {
                        this.m_historyState = enuHistoryState.Start;
                        e = new HistoryChangedEventArgs(
                               this.m_historyState,
                               m_CurrentAction,null);                               
                        this.m_historyIndex = -1;
                    }
                    break;
                case enuHistoryState.Redo:
                    //spécial case 
                    if (this.m_CurrentAction.Previous != null)
                    {
                        IHistoryAction n = null;
                        if (this.m_CurrentAction.Previous.Previous == null)
                        {
                            this.m_historyState = enuHistoryState.Start;//start reached                           
                            n = this.m_CurrentAction.Previous;
                            this.m_historyIndex = -1;
                            e = new HistoryChangedEventArgs(
                            this.m_historyState,
                            this.m_CurrentAction,
                            n);
                        }
                        else
                        {
                            this.m_historyState = enuHistoryState.Undo;
                            n = this.m_CurrentAction.Previous.Previous  ;
                            e = new HistoryChangedEventArgs(
                            this.m_historyState,
                            this.m_CurrentAction.Previous ,
                            this.m_CurrentAction.Previous.Previous);
                            this.m_historyIndex --;//-= 2;
                        }
                        this.m_CurrentAction.Previous.Undo();
                        this.m_CurrentAction = n;
                    }
                    else 
                    {
                        //not possible
                        this.m_historyState  = enuHistoryState.Start;
                        this.m_CurrentAction.Undo();
                        e = new HistoryChangedEventArgs(
                       this.m_historyState,
                       this.m_CurrentAction,
                       null);
                        this.m_historyIndex = -1;
                    }
                    break;
                case enuHistoryState.End:
                    this.m_CurrentAction.Undo();                    
                    if (this.m_CurrentAction.Previous != null)
                    {
                        this.m_historyIndex--;
                        this.m_historyState = enuHistoryState.Undo;
                        e = new HistoryChangedEventArgs(
                                this.m_historyState,
                                m_CurrentAction,
                                m_CurrentAction.Previous);
                        this.m_CurrentAction = m_CurrentAction.Previous;
                    }
                    else {
                        this.m_historyState = enuHistoryState.Start;
                        e = new HistoryChangedEventArgs(
                       this.m_historyState,
                       m_CurrentAction,
                       null);
                    }
                    break;
                case enuHistoryState.Start:
                    //no undo possible
                    this.m_undoing = false;
                    this.m_historyIndex = -1;
                    return;
                default:
                    break;
            }
            //undo the action                        
            this.OnHistoryActionChanged(e);
            this.m_undoing = false;
        }
        public void Redo()
        {
            if ((this.m_CurrentAction == null)||(this.m_redoing ))
                return;
            this.m_redoing = true;
            HistoryChangedEventArgs e = null;
            switch (this.m_historyState)
            {
                case enuHistoryState.Undo:
                    //spécial case 
                    if (this.m_CurrentAction.Next != null)
                    {
                        IHistoryAction n = null;
                        if (this.m_CurrentAction.Next.Next == null)
                        {
                            this.m_historyState = enuHistoryState.End;
                            n = this.m_CurrentAction.Next;
                            this.m_historyIndex = this.Count - 1;
                            e = new HistoryChangedEventArgs(
                               this.m_historyState,
                               m_CurrentAction,
                               n);
                        }
                        else
                        {
                            this.m_historyState = enuHistoryState.Redo;
                            n = this.m_CurrentAction.Next.Next;
                            e = new HistoryChangedEventArgs(
                               this.m_historyState,
                               m_CurrentAction,
                               m_CurrentAction.Next );
                            m_historyIndex++;//=2;
                        }
                        this.m_CurrentAction.Next.Redo();
                        this.m_CurrentAction = n ;
                    }
                    else
                    {
                    //no valid 
                        return;
                    }
                    break;
                case enuHistoryState.Redo:
                    if (this.m_CurrentAction.Next == null)
                    {
                        this.m_CurrentAction.Redo();
                        this.m_historyState = enuHistoryState.End;
                        e = new HistoryChangedEventArgs(
                                this.m_historyState,
                                m_CurrentAction.Previous ,
                                this.m_CurrentAction);
                        this.m_historyIndex = this.Count - 1;
                    }
                    else
                    {//conserver the redo                        
                        this.m_CurrentAction.Redo ();                        
                        this.m_historyState = enuHistoryState.Redo;
                        e = new HistoryChangedEventArgs(
                                this.m_historyState,
                                m_CurrentAction.Previous ,
                                m_CurrentAction);
                        this.m_CurrentAction = m_CurrentAction.Next;
                        m_historyIndex = this.m_CurrentAction.Index-1;
                    }
                    break;
                case enuHistoryState.End:
                    //no redo possible
                    this.m_redoing = false;
                    this.m_historyIndex = this.Count-1;
                    return; 
                case enuHistoryState.Start:
                    //start
                    this.m_CurrentAction.Redo();
                    if (this.m_CurrentAction.Next == null)
                    {
                        this.m_historyState = enuHistoryState.End;
                        e = new HistoryChangedEventArgs(
                                this.m_historyState,
                                m_CurrentAction,
                                null);
                        this.m_historyIndex = this.Count - 1;
                    }
                    else
                    {
                        this.m_historyState = enuHistoryState.Redo;
                        e = new HistoryChangedEventArgs(
                                this.m_historyState,
                                m_CurrentAction ,
                                null);
                        this.m_CurrentAction = m_CurrentAction.Next;
                        m_historyIndex=0;
                    }
                    break;
                default:
                    break;
            }
            //undo the action                        
            this.OnHistoryActionChanged(e);
            this.m_redoing = false;            
        }
        #endregion
        public void Add(HistoryActionBase action)
        {
            if ((action == null)||(Contains(action )))
                return;
            if ((this.m_historyIndex != -1)&& (this.m_historyIndex < (this.Count-1)))
            {
                this.ClearAtCurrentIndex();
                this.m_historyIndex = -1;
            }
            switch (this.Count)
            {
                case 0:
                    this.m_historyIndex = 0;
                    action.Next = null;
                    action.Previous = null;
                    this.m_historyState = enuHistoryState.Start;
                    break;
                default :
                    this.m_historyIndex= this.Count;
                    action.Previous = this.m_CurrentAction;
                    this.m_CurrentAction.Next = action;
                    this.m_historyState = enuHistoryState.End;
                    break;
            }
            m_actions.Add (action );
            this.m_CurrentAction = action;
            this.m_CurrentAction.Owner = this;
            OnHistoryActionAdded(new HistoryItemEventArgs(action)); 
        }
        private void OnHistoryActionAdded(HistoryItemEventArgs h)
        {
            switch (this.m_historyState)
            {
                case enuHistoryState.Start :
                    this.m_historyState = enuHistoryState.End;
                    break;
            }
            if (this.HistoryItemAdded != null)
                this.HistoryItemAdded(this, h);
        }
        private void OnHistoryActionChanged(HistoryChangedEventArgs h)
        {
            if (this.HistoryChanged != null)
                this.HistoryChanged(this, h);
        }
        private bool Contains(HistoryActionBase action)
        {
            return this.m_actions.Contains(action);
        }       
        public void Clear()
        {
            if (this.Count > 0)
            {
                this.m_redoing = false;
                this.m_undoing = false;
                this.m_actions.Clear();
                this.m_historyIndex = -1;
                this.m_CurrentAction = null;
                this.m_historyState = enuHistoryState.Start;
                this.OnHistoryClear(EventArgs.Empty);
            }
        }
        private void OnHistoryClear(EventArgs eventArgs)
        {
            //restore the history state to the begin
            this.m_historyState = enuHistoryState.Start;
            if (this.HistoryClear!=null)
                this.HistoryClear(this, eventArgs);
        }
        private void ClearAtCurrentIndex()
        {
            int c = this.Count;
            int s = this.m_historyIndex;
            if (this.m_CurrentAction !=null)
            {
                switch (this.m_historyState)
	            {
		            case enuHistoryState.Undo:
                        s += 1;
                        this.m_CurrentAction.Next = null;
                        this.m_historyState = enuHistoryState.End;
                     break;
                    case enuHistoryState.Redo:
                     this.m_CurrentAction.Previous.Next = null;
                     this.m_CurrentAction = this.m_CurrentAction.Previous;
                     this.m_historyState = enuHistoryState.End;
                     break;
                    case enuHistoryState.End:
                     break;
                    case enuHistoryState.Start:
                        //remove all 
                        s = 0;
                        this.m_CurrentAction = null;
                     break;
                    default:
                     break;
	            }
            }
            this.m_actions.RemoveRange(s, c - s);
            this.m_historyIndex = s;
            OnHistoryClearAt(EventArgs.Empty);
        }
        private void OnHistoryClearAt(EventArgs eventArgs)
        {
            if (HistoryClearAt != null)
                this.HistoryClearAt(this, eventArgs);
        }
        #region IEnumerable Members
        public IEnumerator GetEnumerator()
        {
            return this.m_actions.GetEnumerator();
        }
        #endregion
        internal void SaveHistory()
        {
            //not implementent yet
        }
        #region IHistoryList Members
        public void Add(IGK.DrSStudio.History.IHistoryAction action)
        {
            this.Add(action as HistoryActionBase);
        }
        #endregion
        #region IHistoryList Members
        public bool CanUndo
        {
            get { return (this.HistoryState != enuHistoryState.Start); }
        }
        public bool CanRedo
        {
            get { return (this.HistoryState != enuHistoryState.End); }
        }
        #endregion
        #region IHistoryList Members
        public int IndexOf(IHistoryAction Action)
        {
            return this.m_actions.IndexOf(Action as HistoryActionBase );
        }
        #endregion
        #region IHistoryList Members
        ICoreWorkingSurface IHistoryList.CurrentSurface
        {
            get { return this.CurrentSurface; }
        }
        #endregion
    }
}

