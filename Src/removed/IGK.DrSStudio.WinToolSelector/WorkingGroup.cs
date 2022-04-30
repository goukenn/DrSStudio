

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WorkingGroup.cs
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
file:WorkingGroup.cs
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing ;
using System.Drawing.Drawing2D ;
using System.Windows.Forms;
namespace IGK.DrSStudio
{
    using IGK.ICore;using IGK.DrSStudio.WinUI ;
    public class WorkingGroup : IWorkingItemGroup
    {
        string m_name;
        const string TNAME = "Group.";
        private bool m_visible;
        private IWorkingGroupOwner m_owner;
        private IWorkingItemCollections m_items;
        private bool m_collapsed;
        private int m_innerHeight;        
        public event EventHandler CollapsedChanged;
        private string m_environment;
        private Type m_targetSurface;
        private string m_ImageKey;
        /// <summary>
        /// get or set the group imag key
        /// </summary>
        public string ImageKey
        {
            get { return m_ImageKey; }
            set
            {
                if (m_ImageKey != value)
                {
                    m_ImageKey = value;
                }
            }
        }
        public Rectanglei Bound
        {
            get {
                if (this.Visible)
                    return this.GetBound ();//.Owner.GetBound(this);
                else
                    return Rectanglei.Empty;
            }           
        }
        public string Environment { get { return this.m_environment; } }
        public Type TargetSurface { get { return this.m_targetSurface; } }
        public bool Collapsed { get { return this.m_collapsed; } private set{ 
            this.m_collapsed = value;
            OnCollapseChanged(EventArgs.Empty);
        }}
        #region IWorkingItemGroup Members
        public string Title
        {
            get { return TNAME + m_name; }
        }
        public bool Visible
        {
            get
            {
                return this.m_visible;
            }
            set
            {
                if (this.m_visible != value)
                {
                    this.m_visible = value;
                    OnVisibleChanged(EventArgs.Empty);
                }
            }
        }
        private void OnVisibleChanged(EventArgs eventArgs)
        {
            if (this.VisibleChanged != null)
                this.VisibleChanged(this, eventArgs);
        }
        public IWorkingItemCollections Items
        {
            get { return m_items; }
        }
        public IWorkingGroupOwner Owner
        {
            get { return this.m_owner; }
        }
        public void Expand()
        {
            if (this.m_collapsed != false)
            {
                this.m_collapsed = false;
                OnCollapseChanged(EventArgs.Empty);
            }
        }
        public void Collapse()
        {
            if (this.m_collapsed != true)
            {
                this.m_collapsed = true;
                OnCollapseChanged(EventArgs.Empty);
            }
        }
        /// <summary>
        /// get the index of the group
        /// </summary>
        public int Index
        {
            get
            {
                return this.Owner.Groups.IndexOf(this);
            }
        }
        public event EventHandler VisibleChanged;
        #endregion
        public override string ToString()
        {
            return "WorkingGroup["+this.m_name +"]";
        }
        public WorkingGroup(string name,string ImageKey,string environement, IWorkingGroupOwner owner)
        {
            if (string.IsNullOrEmpty(name))
                throw new CoreException(enuExceptionType.ArgumentIsNull, "name");
            this.m_name = name;
            this.m_items = new ItemsCollections(this);
            this.m_owner = owner;
            this.m_visible = false;
            this.m_environment = environement;
            this.m_owner = owner;
            this.m_targetSurface = null;
            this.m_ImageKey = (ImageKey == null)? string.Format ("group_{0}", name): ImageKey ;
            this.m_owner.SizeChanged += new EventHandler(m_owner_SizeChanged);
            m_innerHeight = 0;
            this.m_collapsed = true;
            RegisterOwnerEvent();
        }
        private void RegisterOwnerEvent()
        {
            Owner.GroupExpandedChanged += new GroupExpandedChangedEventHandler(Owner_GroupExpandedChanged);
            Owner.MouseClick += new MouseEventHandler(Owner_MouseClick);
        }
        void Owner_MouseClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (this.Visible  && this.Contains(e.Location))
                    {
                        this.Collapsed = !Collapsed;
                    }
                    break;
            }
        }
        public Rectanglei GetBound()
        {
            int index = this.Index;
            int y = 0;
            for (int h = 0; h < index; h++)
            {
                if (Owner.Groups[h].Visible)
                {
                    y += WinToolSelectorConstant.DIM_GROUP_HEIGHT;
                    if (Owner.Groups[h].Collapsed == false)
                    {
                        y += Owner.Groups[h].InnerHeight;
                    }
                }
            }
            return new Rectanglei (0, y, this.m_owner.VScrollBar .Visible ? 
                this.m_owner.Width - this.m_owner.VScrollBar.Width : 
                this.m_owner.Width,
                WinToolSelectorConstant.DIM_GROUP_HEIGHT);
        }
        public bool Contains(Point pts)
        {
            int y = 0;
            if (m_owner.VScrollBar.Visible)
            {
                Point pt = pts;
                y = Owner.VScrollBar.Value - Owner.VScrollBar.Minimum;
                pt.Y += y;
                bool v = this.GetBound ().Contains(pt);
                return v;
            }
            return this.Bound.Contains(pts);
        }
        void Owner_GroupExpandedChanged(object sender, GroupExpandedChangedEventArgs e)
        {
            //if (e.Group == this)
            //{
            //    return;
            //}
            //else
            //{
            //    int i = e.Index;
            //    int index = m_owner.Groups.IndexOf(this);
            //    if (index > i)
            //    {
            //        GenBound();
            //    }
            //}
        }
        void m_owner_SizeChanged(object sender, EventArgs e)
        {
        }
        private void OnCollapseChanged(EventArgs eventArgs)
        {
            if (this.CollapsedChanged != null)
                this.CollapsedChanged(this, eventArgs);               
        }
        class ItemsCollections : IWorkingItemCollections, 
            IComparer<IWorkingItem>
        {
            List<IWorkingItem> m_items;
            WorkingGroup m_owner;
            public ItemsCollections(WorkingGroup owner)
            {
                this.m_owner = owner;
                this.m_items = new List<IWorkingItem>();
            }
            #region IWorkingItemCollections Members
            public IWorkingItem this[int index]
            {
                get { return this.m_items[index]; }
            }
            public int Count
            {
                get { return this.m_items.Count; }
            }
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_items.GetEnumerator();
            }
            public void Add(IWorkingItem item)
            {
                if ((item != null) && (!this.m_items.Contains (item)))
                {
                    this.m_items.Add(item);
                    this.m_owner.m_innerHeight += WinToolSelectorConstant.DIM_ITEM_HEIGHT;
                }
            }
            #endregion
            #region IWorkingItemCollections Members
            public int IndexOf(WorkingItem workingItem)
            {
                return this.m_items.IndexOf(workingItem);
            }
            #endregion
            #region IWorkingItemCollections Members
            public void Sort()
            {
                this.m_items.Sort(this);
            }
            #endregion
            #region IComparer<IWorkingItem> Members
            public int Compare(IWorkingItem x, IWorkingItem y)
            {
                return CoreSystem.GetString(x.Title).CompareTo(CoreSystem.GetString(y.Title));
            }
            #endregion
        }
        #region IWorkingItemGroup Members
        public int InnerHeight
        {
            get { return this.m_innerHeight; }
        }
        #endregion
    }
}

