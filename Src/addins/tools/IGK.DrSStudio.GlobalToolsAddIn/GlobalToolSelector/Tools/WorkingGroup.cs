

/*
IGKDEV @ 2008-2016
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
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:WorkingGroup.cs
*/
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Tools
{
    public class WorkingGroup : IWorkingItemGroup 
    {
        private int m_Position;
        public string Title
        {
            get { return ("group."+this.Name).R(); }
        }
        public int Position
        {
            get { return m_Position; }
            set
            {
                if (m_Position != value)
                {
                    m_Position = value;
                    OnPositionChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler PositionChanged;
        ///<summary>
        ///raise the PositionChanged 
        ///</summary>
        protected virtual void OnPositionChanged(EventArgs e)
        {
            if (PositionChanged != null)
                PositionChanged(this, e);
        }
        public WorkingGroup(string name, string imageKey, string environment)
        {
            this.m_Name = name;
            this.m_ImageKey = imageKey;
            this.m_EnvironmentName = environment;            
            this.Visible = false;
            this.m_items = new WorkingItemCollections(this);
        }
        private string m_Name;
        private string m_EnvironmentName;
        private string m_ImageKey;
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
        public string Environment
        {
            get { return m_EnvironmentName; }
        }
        public string Name
        {
            get { return m_Name; }
            set
            {
                if (m_Name != value)
                {
                    m_Name = value;
                }
            }
        }
        private bool m_Visible;
        private IWorkingItemCollections m_items;
        public event EventHandler VisibleChanged;
        ///<summary>
        ///raise the VisibleChanged 
        ///</summary>
        protected virtual void OnVisibleChanged(EventArgs e)
        {
            if (VisibleChanged != null)
                VisibleChanged(this, e);
        }
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
        public IWorkingItemCollections Items
        {
            get
            {
                return this.m_items;
            }
        }
        class WorkingItemCollections : IWorkingItemCollections,
               IComparer<ICoreGTWorkingItem>
        {
            private WorkingGroup workingGroup;
            private List<ICoreGTWorkingItem> m_items;
            public WorkingItemCollections(WorkingGroup workingGroup)
            {
                this.m_items = new List<ICoreGTWorkingItem>();
                this.workingGroup = workingGroup;
            }
            public int Count
            {
                get { return this.m_items.Count;  }
            }
            public void Sort()
            {
                this.m_items.Sort(this);
            }
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_items.GetEnumerator();
            }
            public int Compare(ICoreGTWorkingItem x, ICoreGTWorkingItem y)
            {
                return x.Title.CompareTo(y.Title);
            }
            public void Add(ICoreGTWorkingItem workingItem)
            {
                if ((workingItem !=null) && !this.m_items.Contains (workingItem))
                    this.m_items.Add(workingItem);
            }
            public void Remove(ICoreGTWorkingItem workingItem)
            {
                if (this.m_items.Contains (workingItem))
                this.m_items.Remove (workingItem);
            }
        }


        public int VisibleCount
        {
            get { return this.Items.Count; }
        }
    }
}

