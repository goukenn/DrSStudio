

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: FBDirectoryBrowserItem.cs
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
file:FBDirectoryBrowserItem.cs
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
﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
namespace IGK.DrSStudio.Drawing3D.FileBrowser.WinUI
{
    
using IGK.ICore;using IGK.DrSStudio.WinUI;
    /// <summary>
    /// represent a file directory browser node
    /// </summary>
    public class FBDirectoryBrowserItem : IDirectoryBrowserItemOwner, IDirectoryBrowserItem, IDisposable  
    {
        internal static readonly Icon DIRECTORY_ICON;
        internal static readonly Icon DRIVE_ICON_32x32;
        private bool m_Visible;
        private bool m_Expended;
        private bool m_initialized;
        private FBDirectoryBrowserItem  m_Parent;
        private FBDirectoryBrowserItemCollection m_Childs;
        private string m_FullPath;
        private int m_Depth;
        public int Depth {
            get {
                return this.m_Depth;
            }
        }
        public virtual string DisplayName {
            get {
                if (this.IsDirectory)
                {
                    return new System.IO.DirectoryInfo(this.FullPath).Name;
                }
                return System.IO.Path.GetFileName(this.FullPath);
            }
        }
        public FBDirectoryBrowserItemCollection Nodes
        {
            get { return m_Childs; }
            set
            {
                if (m_Childs != value)
                {
                    m_Childs = value;
                }
            }
        }
        public int Index {
            get { 
                if (this.Parent == null)
                    return -1;
                return this.Parent.Nodes.IndexOf(this);
            }
        }
        public FBDirectoryBrowserItem Next {
            get { 
                if (this.Parent == null)
                    return null;
                int i= this.Index ;
                if ((this.Parent.Nodes.Count> 0) && (i< (this.Parent.Nodes.Count -1)))
                    return this.Parent.Nodes[i + 1 ];
                return null;
            }
        }
        public FBDirectoryBrowserItem Previous
        {
            get
            {
                if (this.Parent == null)
                    return null;
                int i = this.Index;
                if (( i >0) && (this.Parent.Nodes.Count > 0) && (this.Parent.Nodes.Count > i))
                    return this.Parent.Nodes[i - 1];
                return null;
            }
        }
        public FBDirectoryBrowserItem  Parent
        {
            get { return m_Parent; }
            set
            {
                if (m_Parent != value)
                {
                    m_Parent = value;
                }
            }
        }
        public string FullPath
        {
            get { return m_FullPath; }
        }
        public bool Expended
        {
            get { return m_Expended; }
            set
            {
                if (m_Expended != value)
                {
                    m_Expended = value;
                    OnExpandedChanged(EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// get or set if visible 
        /// </summary>
        public bool Visible
        {
            get { return m_Visible; }
            set
            {
                if (m_Visible != value)
                {
                    m_Visible = value;
                    OnVisibleChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler VisibleChanged;
        public event EventHandler ExpandedChanged;
        ///<summary>
        ///raise the ExpandedChanged 
        ///</summary>
        protected virtual void OnExpandedChanged(EventArgs e)
        {
            if (!this.m_initialized)
                this.Initialize();
            if (ExpandedChanged != null)
                ExpandedChanged(this, e);
        }
        ///<summary>
        ///raise the VisibleChanged 
        ///</summary>
        protected virtual void OnVisibleChanged(EventArgs e)
        {
            if (VisibleChanged != null)
                VisibleChanged(this, e);
        }
        static FBDirectoryBrowserItem() {
            DIRECTORY_ICON = WinCoreWinUIUtils.GetDirectoryIcon(32, 32);
            DRIVE_ICON_32x32 = WinCoreWinUIUtils.GetDriveIcon(32, 32);
        }
        public FBDirectoryBrowserItem(string fullpath)
        {
            this.m_Childs = new FBDirectoryBrowserItemCollection(this);
            this.m_FullPath = fullpath;
            this.m_Expended = false;           
        }
        protected virtual void Initialize()
        {
            if (System.IO.Directory.Exists(FullPath ))
            {
                this.Nodes.Clear();
                try
                {
                    foreach (string item in System.IO.Directory.GetDirectories(FullPath))
                    {
                        this.Nodes.Add(new FBDirectoryBrowserItem(item));
                    }
                }
                catch { 
                }
                this.m_initialized = true;
            }
        }
        public class FBDirectoryBrowserItemCollection : 
            IDirectoryBrowserItemCollections , 
            IEnumerable ,
            IEnumerator ,
            IFBDirectoryBrowserItemEnumerator
        {
            private List<FBDirectoryBrowserItem> m_childs;
            private FBDirectoryBrowserItem m_item;
            IDirectoryBrowserItem IFBDirectoryBrowserItemEnumerator.Current
            {
                get
                {
                    return m_current;
                }
            }
            internal FBDirectoryBrowserItemCollection(FBDirectoryBrowserItem item)
            {
                this.m_childs = new List<FBDirectoryBrowserItem>();
                this.m_item = item; 
                this.m_first = true ;
                this.m_level = 0;
            }
            public override string ToString()
            {
                string s  = base.ToString();
                s = string.Format("Childs : [{0}]", this.Count);
                return s;
            }
            public int Count
            {
                get { return this.m_childs .Count ; }
            }
            #region IEnumerable Members
            public IEnumerator GetEnumerator()
            {
                return this.m_childs.GetEnumerator();
            }
            #endregion
            #region IDirectoryBrowserItemCollections Members
            public IDirectoryBrowserItem LastNode
            {
                get { 
                    if (this.m_childs.Count > 0)
                        return  this.m_childs[this.Count - 1];
                     return null;
                }
            }
            public FBDirectoryBrowserItem Add(string path, string displayName)
            {                
                FBDirectoryBrowserItem dic = new FBDirectoryBrowserItem(path);
                this.Add(dic );
                return dic ;
            }
            public void Remove(FBDirectoryBrowserItem item)
            {
                if (this.m_childs.Contains(item))
                {
                    this.m_childs.Remove(item);
                }
            }
            IDirectoryBrowserItem IDirectoryBrowserItemCollections.Add(string path, string displayName)
            {
                return Add(path, displayName);
            }
            void IDirectoryBrowserItemCollections.Remove(IDirectoryBrowserItem item)
            {
                this.Remove(item as FBDirectoryBrowserItem);
            }
            public void Add(FBDirectoryBrowserItem db)
            {
                if ((db != null ) && !this.m_childs.Contains (db))
                {
                    this.m_childs.Add(db);
                    if (this.m_item != null)
                    {
                        db.m_Depth = this.m_item.Depth + 1;
                    }
                    else
                        db.m_Depth = 0;
                    db.m_Parent = this.m_item;
                }
            }
            void IDirectoryBrowserItemCollections.Add(IDirectoryBrowserItem item)
            {
                this.Add(item as FBDirectoryBrowserItem);
            }
            public void Clear()
            {
                if (this.m_childs.Count > 0)
                {
                    this.m_childs.Clear();                    
                }
            }
            public FBDirectoryBrowserItem this[int index]{
                get{
                    return this.m_childs [index ];
                }
            }
            #endregion
            #region IEnumerator Members
            private FBDirectoryBrowserItem m_current;
            private bool m_first;
            private int  m_level;
            public FBDirectoryBrowserItem Current {
                get {
                    return m_current;
                }
            }
            object IEnumerator.Current
            {
                get { return this.Current ; }
            }
            public bool MoveNext()
            {
                if (this.m_first)
                {
                    if (this.Count > 0)
                    {
                        this.m_current = this[0];
                        this.m_first = false;
                        return true;
                    }
                    return false;
                }
                if (this.m_current == null)
                    return false;
                if (this.m_current.Nodes.Count > 0)
                {
                    this.m_current = this.m_current.Nodes[0];
                    //increse deep
                    this.m_level++;
                }
                else {
                    FBDirectoryBrowserItem v_next = this.m_current.Next;
                    FBDirectoryBrowserItem v_parent = this.m_current.Parent;
                    while (v_next == null)
                    {
                        if (v_parent == null)
                        {
                            //no parent
                            v_next = this.m_current;
                            return false;
                        }
                        else { 
                            //go to parent
                            v_parent = v_parent.Parent;
                            this.m_level--;
                        }
                    }
                    this.m_current = v_next;
                }
                return true;
            }
            public void Reset()
            {
                this.m_current = null;
                this.m_level = 0;
                this.m_first = true;
            }
            #endregion
            public int IndexOf(FBDirectoryBrowserItem item)
            {
                if (item == null)
                    return -1;
                return this.m_childs.IndexOf(item);
            }

           
        }
        #region IDisposable Members
        public virtual void Dispose()
        {
            //free resources
        }
        #endregion
        #region IDirectoryBrowserItem Members
        public virtual bool IsDirectory
        {
            get { 
                return System.IO.Directory.Exists (this.m_FullPath );
            }
        }
        #endregion
        #region IDirectoryBrowserItemOwner Members
        IDirectoryBrowserItemCollections IDirectoryBrowserItemOwner.Nodes
        {
            get { return Nodes; }
        }
        #endregion
    }
}

