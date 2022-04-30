

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXExpenderBoxGroup.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.WinUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using IGK.ICore.WinCore.WinUI.Controls;

namespace IGK.ICore.WinCore.WinUI
{
    public class IGKXExpenderBoxGroup : IGKXControl 
    {
        private IGKXExpenderGroupItemCollection m_items;
        private int m_Index;
        private string m_ImageKey;
        private IGKXExpenderBox  m_ExpenderBox;
        private IGKXExpenderBoxItem m_SelectedGroupItem;
        private IGKXExpenderBoxItem m_DefaultGroupItem;
        private Timer m_timer;
        private enuExpenderGroupState m_State;




        public enuExpenderGroupState State
        {
            get { return m_State; }
            set
            {
                if (m_State != value)
                {
                    m_State = value;
                }
            }
        }
        public override LayoutEngine LayoutEngine
        {
            get
            {
                if (this.m_layoutEngine ==null)
                    m_layoutEngine = new IGKXExpenderBoxGroupLayoutEngine(this);
                return m_layoutEngine;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (m_timer != null)
            {
                m_timer.Dispose();
                m_timer = null;
            }
            base.Dispose(disposing);
        }
        public IGKXExpenderBoxItem DefaultGroupItem
        {
            get { return m_DefaultGroupItem; }
            set
            {
                if (m_DefaultGroupItem != value)
                {
                    m_DefaultGroupItem = value;
                }
            }
        }

        public IGKXExpenderBoxItem SelectedGroupItem
        {
            get { return m_SelectedGroupItem; }
            set
            {
                if (m_SelectedGroupItem != value)
                {
                    m_SelectedGroupItem = value;
                    OnSelectedGroupItemChanged(EventArgs.Empty);
                }
                else {
                    if (this.ExpenderBox.SelectedGroup != this)
                    {
                        this.ExpenderBox.SelectedGroup = this;
                        OnSelectedGroupItemChanged(EventArgs.Empty);
                    }
                }
            }
        }
        public event EventHandler SelectedGroupItemChanged;
        private bool m_animation;
        private IGKXExpenderBoxGroupLayoutEngine m_layoutEngine;
        ///<summary>
        ///raise the SelectedGroupItem 
        ///</summary>
        protected virtual void OnSelectedGroupItemChanged(EventArgs e)
        {
            if (SelectedGroupItemChanged != null)
                SelectedGroupItemChanged(this, e);
        }


        public IGKXExpenderBox  ExpenderBox
        {
            get { return m_ExpenderBox; }
            set
            {
                if (m_ExpenderBox != value)
                {
                    m_ExpenderBox = value;
                }
            }
        }
        public int ToTalChildHeight {
            get {
                int v_h = 0;
                if (this.Items.Count > 0)
                {
                    var lastItem = this.Items[this.Items.Count - 1];

                    System.Drawing.Rectangle rc = lastItem.Bounds;
                    v_h = rc.Bottom -this.DefaultSize.Height;
                }             
                return v_h;
            }
        }
        public void Expand()
        {
            if (!this.ExpenderBox.Animate)
            {
                this.SetExpandSize();
                this.State = enuExpenderGroupState.Expended;
                return;
            }
            if (this.m_animation || !CanContinueExtend())
                return;

            this.m_timer = new Timer();
            this.m_timer.Interval = 20;
            this.m_animation = true;
            this.m_timer.Tick += new IGKXExpenderMethod(this).Extend;
            this.m_timer.Enabled = true;
        }

        private void SetExpandSize()
        {
            this.Size = new System.Drawing.Size(this.Width, this.DefaultSize.Height + this.ToTalChildHeight +  2);            
        }
        private void SetCollapseSize()
        {
            this.Size = new System.Drawing.Size(this.Width, this.DefaultSize.Height);
        }
        protected override Control.ControlCollection CreateControlsInstance()
        {
            return new IGKXExpenderGroupItemControlCollection(this);
        }
        class IGKXExpenderGroupItemControlCollection : Control.ControlCollection
        {
            public IGKXExpenderGroupItemControlCollection(IGKXExpenderBoxGroup group):base(group)
            {
            }
            public override bool IsReadOnly
            {
                get
                {
                    return true;
                }
            }
            public override void Add(Control value)
            {
                if (value is IGKXExpenderBoxItem) 
                    base.Add(value);
            }
            public override void Remove(Control value)
            {
                if (value is IGKXExpenderBoxItem)
                    base.Remove(value);
            }
        }
        class IGKXExpenderMethod {
            int i = 0;
            int m_maxChildHeight;
            private IGKXExpenderBoxGroup m_group;

            public IGKXExpenderMethod(IGKXExpenderBoxGroup group)
            {                
                this.m_group = group;
                this.m_maxChildHeight = group.ToTalChildHeight;
            }
            public void Extend(object sender, EventArgs e)
            {
                if ( (i>=this.m_group.Items.Count ) || ( this.CanContinueExtend() == false))
                {
                    //if show all admin sequence stop extension
                    this.m_group._stopTimer();
                    this.m_group.m_animation = false;
                    this.m_group.SetExpandSize();
                    this.m_group.State = enuExpenderGroupState.Expended;
                    
                }
                else
                {
                    //1000 => this.m_maxChildHeight 
                    //timInterval =>x = 
                    int step = (int)(Math.Ceiling(this.m_maxChildHeight * this.m_group.m_timer.Interval / 100.0f));
                    this.m_group.Height += step; // this.m_group.Items[i].Height;
                    i++;
                }
            }

            private bool CanContinueExtend()
            {
                int y = this.m_group.Height - this.m_group.DefaultSize.Height;
                return y < this.m_maxChildHeight;
            }

        }

        class IGKXExpenderBoxGroupLayoutEngine : LayoutEngine
        {
            private IGKXExpenderBoxGroup m_owner;

            public IGKXExpenderBoxGroupLayoutEngine(IGKXExpenderBoxGroup group)
            {                
                this.m_owner = group;
            }
            public override bool Layout(object container, LayoutEventArgs layoutEventArgs)
            {
                int y = this.m_owner.DefaultSize.Height - (this.m_owner.AutoScrollOffset.Y );//+ this.m_owner.VerticalScroll.Value);
                //if (this.m_owner.HScroll)
                //    this.m_owner.HorizontalScroll.Visible = false;
                //int w = this.m_owner.VScroll ? SystemInformation.VerticalScrollBarWidth : 0;
                int v_W = this.m_owner.Width - 4;
                int x = 2;
                foreach (Control item in m_owner.Items)
                {
                    item.Size = new System.Drawing.Size(v_W, item.Height);
                    item.Margin = Padding.Empty;
                    item.Padding = Padding.Empty;
                    item.Location = new System.Drawing.Point(x, y);
                    y += item.Height;
                }
                return true;
            }
        }
        class IGKXCollapseMethod
        {
            int i = 0;
            private int m_maxChildHeight;
            private IGKXExpenderBoxGroup m_group;

            public IGKXCollapseMethod(IGKXExpenderBoxGroup group)
            {                
                this.m_group = group;
                this.m_maxChildHeight = group.ToTalChildHeight;
                i = group.Items.Count-1;
            }
            public void Collapse(object sender, EventArgs e)
            {
                if ((i < 0) || (this.CanContinueCollapse() == false))
                {
                    //if show all admin sequence tom extendion
                    this.m_group._stopTimer();
                    this.m_group.SetCollapseSize();
                    this.m_group.m_animation = false;
                    this.m_group.State = enuExpenderGroupState.Collapsed ;
                    
                }
                else
                {
                    int step = (int)(Math.Ceiling(this.m_maxChildHeight * this.m_group.m_timer.Interval / 100.0f));
                    this.m_group.Height -= step;// this.m_group.Items[i].Height;
                    i--;
                }
            }

            private bool CanContinueCollapse()
            {
                int y = this.m_group.Height;
                return y > this.m_group.DefaultSize.Height;
            }
        }
     
        private void _stopTimer()
        {
            if (this.m_timer == null)
                return;
            this.m_timer.Enabled = false;
            this.m_timer.Dispose();
            this.m_timer = null;
        }

        private bool CanContinueExtend()
        {
            int y = this.Height - this.DefaultSize.Height;
            return y < this.ToTalChildHeight;
        }
        public void Collapse() {        
            if (!this.ExpenderBox.Animate)
            {
                this.SetCollapseSize();
                this.State = enuExpenderGroupState.Collapsed;
                return;
            }
            if (this.m_animation || !CanContinueCollapse())
                return;

            this.m_timer = new Timer();
            this.m_timer.Interval = 20;
            this.m_animation = true;
            this.m_timer.Tick += new IGKXCollapseMethod(this).Collapse;
            this.m_timer.Enabled = true;        
        }
        
        bool  CanContinueCollapse()
        {
            return this.Height > this.DefaultSize.Height;
        }

        protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
        {
                             
            this.MouseState = this.ClientRectangle.Contains (e.Location)?
                enuMouseState.Hover : enuMouseState.None ;                
            base.OnMouseMove(e);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            this.MouseState = enuMouseState.None;
            base.OnMouseLeave(e);
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            this.MouseState = enuMouseState.Hover;
            base.OnMouseEnter(e);
        }
        protected override void OnMouseStateChanged(EventArgs eventArgs)
        {
            this.Invalidate();
            base.OnMouseStateChanged(eventArgs);
        }
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
        }
        /// <summary>
        /// get or set the image key
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
        /// <summary>
        /// get or set the require index
        /// </summary>
        public int Index
        {
            get { return m_Index; }
            set
            {
                if (m_Index != value)
                {
                    m_Index = value;
                }
            }
        }
        public IGKXExpenderGroupItemCollection Items {
            get {
                return this.m_items;
            }
        }
        public override string ToString()
        {
            return GetType().Name + "["+this.Name+"]";
        }
        internal IGKXExpenderBoxGroup()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = global::System.Drawing.Color.Transparent;
            this.m_State = enuExpenderGroupState.Collapsed;
            this.m_items = new IGKXExpenderGroupItemCollection(this);
            this.Paint += _Paint;
            this.Click += _Click;
            this.ItemAdded += _ItemAdded;
            this.ItemRemoved += _ItemRemoved;
        }

        void _ItemRemoved(object sender, CoreItemEventArgs<IGKXExpenderBoxItem> e)
        {
            this.Controls.Remove(e.Item);

            this.PerformLayout();
        }

        void _ItemAdded(object sender, CoreItemEventArgs<IGKXExpenderBoxItem > e)
        {
            this.Controls.Add(e.Item);

            this.PerformLayout();
            e.Item.Click += Item_Click;
        }

        void Item_Click(object sender, EventArgs e)
        {
            this.SelectedGroupItem = sender as IGKXExpenderBoxGroupItem;
        }

        private void _Click(object sender, EventArgs e)
        {
            if (this.State == enuExpenderGroupState.Expended)
            {
                this.Collapse();
            }
            else
            {
                if (this.Items.Count > 0)
                {
                    this.Expand();
                }
            }
           
        }

        void _Paint(object sender, CorePaintEventArgs e)
        {
            if ((this.ExpenderBox != null) && (this.ExpenderBox.Renderer!=null))
                this.ExpenderBox.Renderer.RenderBoxGroup(this, e);
            
        }
        protected override System.Drawing.Size DefaultSize
        {
            get
            {
                return new System.Drawing.Size(32, 32);
            }
        }

        public class IGKXExpenderGroupItemCollection : IEnumerable
        {
            private IGKXExpenderBoxGroup m_group;
            List<IGKXExpenderBoxItem> m_items;
            public IGKXExpenderBoxItem this[int index]
            {
                get {
                    if (this.m_items.IndexExists (index))
                        return this.m_items[index];
                    return null;
                }
            }
            public IGKXExpenderGroupItemCollection(IGKXExpenderBoxGroup group)
            {
                this.m_items = new List<IGKXExpenderBoxItem>();
                this.m_group = group;
            }
            public int Count { get { return this.m_items.Count; } }

            public IEnumerator GetEnumerator()
            {
                return this.m_items.GetEnumerator();
            }

            public void Add(IGKXExpenderBoxItem item)
            {
                if ((item == null) || (this.m_items.Contains(item)))
                    return;
                this.m_items.Add(item);
                item.ParentGroup = this.m_group;
                this.m_group.OnItemAdded(new CoreItemEventArgs<IGKXExpenderBoxItem>(item));
            }
            public void Remove(IGKXExpenderBoxItem item)
            {
                if (this.m_items.Contains(item))
                {
                    this.m_items.Remove(item);
                    item.ParentGroup = null;
                    this.m_group.OnItemRemoved(new CoreItemEventArgs<IGKXExpenderBoxItem>(item));
                }
            }
            public  void Sort(IComparer comparer)
            {
                IComparer<IGKXExpenderBoxItem> c = comparer as IComparer<IGKXExpenderBoxItem>;
                if (c != null)
                {
                    this.m_items.Sort(c);
                    this.m_group.PerformLayout();
                }
            }
        }

        public event EventHandler<CoreItemEventArgs<IGKXExpenderBoxItem>> ItemAdded;
        public event EventHandler<CoreItemEventArgs<IGKXExpenderBoxItem>> ItemRemoved;

        protected virtual void OnItemAdded(CoreItemEventArgs<IGKXExpenderBoxItem> e)
        {
            if (this.ItemAdded !=null)
                this.ItemAdded(this, e);
            
        }

        protected virtual void OnItemRemoved(CoreItemEventArgs<IGKXExpenderBoxItem> e)
        {
            
            if (this.ItemRemoved!=null)
                this.ItemRemoved(this, e);
        }

        public int DefaultHeight { get { return this.DefaultSize.Height; } }
    }
}

