

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXExpenderBox.cs
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
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGK.ICore.WinCore.WinUI.Controls;

namespace IGK.ICore.WinCore.WinUI
{

   // [Designer( "IGK.DrSStudio.WinCore.Design.ExpenderBoxDesigner, IGK.DrSStudio.WinCoreDesign")]
    /// <summary>
    /// represent an expender box
    /// </summary>
    public class IGKXExpenderBox : IGKXUserControl 
    {
        private IGKXExpenderBoxGroup m_SelectedGroup;
        private IGKXExpenderBoxRenderer m_Renderer;
        private bool m_Animate;
        private bool m_AutoCollapse;
        [Category("ExpenderBox")]
        [DefaultValue(false)]
        public bool AutoCollapse
        {
            get { return m_AutoCollapse; }
            set
            {
                if (m_AutoCollapse != value)
                {
                    m_AutoCollapse = value;
                    OnAutoCollaspeChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler AutoCollapseChanged;

        private void OnAutoCollaspeChanged(EventArgs eventArgs)
        {
            if (AutoCollapseChanged != null)
                AutoCollapseChanged(this, eventArgs);
        }
        [Category ("ExpenderBox")]
        [DefaultValue (false )]
        public bool Animate
        {
            get { return m_Animate; }
            set
            {
                if (m_Animate != value)
                {
                    m_Animate = value;

                }
            }
        }
        [Browsable (false )]
        [DesignerSerializationVisibility ( DesignerSerializationVisibility.Hidden )]
        /// <summary>
        /// get or set the Renderer 
        /// </summary>
        public IGKXExpenderBoxRenderer Renderer
        {
            get { return m_Renderer; }
            set
            {
                this.m_Renderer = value;
            }
        }
        private IGKXExpenderBoxGroupCollections m_Groups;
        [Browsable(false)]
        public IGKXExpenderBoxGroupCollections Groups
        {
            get { return m_Groups; }         
        }
        [Browsable(false)]
        public IGKXExpenderBoxGroup SelectedGroup
        {
            get { return m_SelectedGroup; }
            set
            {
                if (m_SelectedGroup != value)
                {
                    m_SelectedGroup = value;
                    this.Invalidate(true);
                    OnSelectedGroupChanged(EventArgs.Empty);
                    
                }
            }
        }
        public IGKXExpenderBoxItem SelectedItem {
            get {
                if (this.SelectedGroup !=null)
                    return this.SelectedGroup.SelectedGroupItem;
                return null;
            }
        }
        public event EventHandler SelectedGroupChanged;
        public event EventHandler<CoreItemEventArgs<IGKXExpenderBoxGroup>> GroupRemoved;
        public event EventHandler<CoreItemEventArgs<IGKXExpenderBoxGroup>> GroupAdded;
        private IGKXExpenderBoxLayoutEngine m_expenderLayoutEngine;
        
        ///<summary>
        ///raise the SelectedGroupChanged 
        ///</summary>
        protected virtual void OnSelectedGroupChanged(EventArgs e)
        {
            if (SelectedGroupChanged != null)
                SelectedGroupChanged(this, e);
        }

        /// <summary>
        /// .Ctr
        /// </summary>
        public IGKXExpenderBox()
        {
            this.m_Groups = new IGKXExpenderBoxGroupCollections(this);
            this.InitializeComponent();
            this.Paint += _Paint;
            this.GroupAdded += _GroupAdded;
            this.GroupRemoved += _GroupRemoved;
            this.Renderer = new IGKXExpenderBoxRenderer(this);
            this.AutoScrollOffset = System.Drawing.Point.Empty;
            this.AutoScrollMargin = System.Drawing.Size.Empty;
            this.m_Animate = false;
            this.Margin = Padding.Empty;
            this.Padding = Padding.Empty;
            this.AutoScroll = true;
            this.HScroll = false;
            this.AutoCollapse = true;
            new ExpenderCollapseManager(this);
        }
        
        [DefaultValue (true)]
        [Browsable (false )]
        public override bool AutoScroll
        {
            get
            {
                return base.AutoScroll;
            }
            set
            {
                base.AutoScroll = value;
            }
        }

        void _GroupRemoved(object sender, CoreItemEventArgs<IGKXExpenderBoxGroup> e)
        {
            e.Item.Click -= Item_Click;
            e.Item.SelectedGroupItemChanged -= Item_SelectedGroupItemChanged;
        }

        void _GroupAdded(object sender, CoreItemEventArgs<IGKXExpenderBoxGroup> e)
        {
            e.Item.Click += Item_Click;
            e.Item.SelectedGroupItemChanged += Item_SelectedGroupItemChanged;
        }

        void Item_SelectedGroupItemChanged(object sender, EventArgs e)
        {
            if (sender == this.SelectedGroup)
            {
                this.OnItemSelected(EventArgs.Empty);
            }
            else 
            {
                this.SelectedGroup = (sender as IGKXExpenderBoxGroup);
                this.OnItemSelected(EventArgs.Empty);
            }
        }
        public event EventHandler ItemSelected;

        protected  void OnItemSelected(EventArgs e)
        {
            if (ItemSelected != null) {
                this.ItemSelected(this, e);
            }
        }
        

        void Item_Click(object sender, EventArgs e)
        {
            this.SelectedGroup = sender as IGKXExpenderBoxGroup;            
        }
        protected override Control.ControlCollection CreateControlsInstance()
        {
            return new IGKXExpenderBoxControlCollection(this);
        }

        void _Paint(object sender, CorePaintEventArgs e)
        {
            if (!DesignMode && (this.Renderer != null))
            {
                Renderer.RenderBox(this, e);
            }
            else
            {
                e.Graphics.Clear(CoreRenderer.BackgroundColor);
            }
        }

        private void InitializeComponent()
        {
            
        }
        protected override void OnCorePaint(CorePaintEventArgs e)
        {
            base.OnCorePaint(e); 
        }

        public override System.Windows.Forms.Layout.LayoutEngine LayoutEngine
        {
            get
            {
                if (this.m_expenderLayoutEngine == null)
                {
                    m_expenderLayoutEngine = new IGKXExpenderBoxLayoutEngine(this);
                }
                return m_expenderLayoutEngine;
            }
        }


        public class IGKXExpenderBoxLayoutEngine : System.Windows.Forms.Layout.LayoutEngine
        {
            private IGKXExpenderBox m_owner;

            public IGKXExpenderBoxLayoutEngine(IGKXExpenderBox box)
            {                
                this.m_owner = box;

            }
            public override void InitLayout(object child, System.Windows.Forms.BoundsSpecified specified)
            {
               // base.InitLayout(child, specified);
            }
            public override bool Layout(object container, System.Windows.Forms.LayoutEventArgs layoutEventArgs)
            {
                int y = 1 -(this.m_owner.AutoScrollOffset.Y + this.m_owner.VerticalScroll.Value);
                if (this.m_owner.HScroll)
                    this.m_owner.HorizontalScroll.Visible = false;
                int w = this.m_owner.VScroll ? SystemInformation.VerticalScrollBarWidth : 0;
                int v_W = this.m_owner.Width - w - 3 -(this.m_owner.Margin.Right );
                int x = 1;
                
                foreach (Control item in m_owner.Controls)
                {
                    if (!item.Visible) 
                        continue;
                    item.Margin = Padding.Empty;
                    item.Padding = Padding.Empty;
                    item.Size = new System.Drawing.Size(v_W, item.Height);
                    item.Location = new System.Drawing.Point(x, y);                    
                    y += item.Height+1;
                }
                return true;
            }
            
        }

        private class ExpenderCollapseManager
        {
            IGKXExpenderBoxGroup  m_oldGroup;
            private IGKXExpenderBox iGKXExpenderBox;

            public ExpenderCollapseManager(IGKXExpenderBox iGKXExpenderBox)
            {
                this.iGKXExpenderBox = iGKXExpenderBox;
                this.iGKXExpenderBox.SelectedGroupChanged += iGKXExpenderBox_SelectedGroupChanged;
                this.iGKXExpenderBox.AutoCollapseChanged += _changed;
                this.m_oldGroup = iGKXExpenderBox.SelectedGroup;
            }

            private void _changed(object sender, EventArgs e)
            {
                if (iGKXExpenderBox.AutoCollapse)
                {
                    foreach (IGKXExpenderBoxGroup g in this.iGKXExpenderBox.Groups)
                    {
                        g.Collapse();
                    }
                    if (this.m_oldGroup != null)
                    {
                        this.m_oldGroup.Expand();
                    }
                }
            }

            void iGKXExpenderBox_SelectedGroupChanged(object sender, EventArgs e)
            {
                if (this.iGKXExpenderBox.AutoCollapse)
                {
                    if (m_oldGroup != this.iGKXExpenderBox.SelectedGroup)
                    {
                        if (m_oldGroup != null)
                        {
                            m_oldGroup.Collapse();
                        }
                        m_oldGroup = this.iGKXExpenderBox.SelectedGroup;
                    }
                }
            }
        }
        
        public class IGKXExpenderBoxControlCollection : Control.ControlCollection
        {
            public IGKXExpenderBoxControlCollection(IGKXExpenderBox box):base(box)
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
                if (value is IGKXExpenderBoxGroup)
                 base.Add(value);
            }
            public override void Remove(Control value)
            {
                if (value is IGKXExpenderBoxGroup)
                base.Remove(value);
            }
        }
        public class IGKXExpenderBoxGroupCollections : IEnumerable
        {
            private IGKXExpenderBox m_expenderBox;
            private List<IGKXExpenderBoxGroup> m_groups;
            

            public IGKXExpenderBoxGroup this[string id]
            {
                get {
                    foreach (IGKXExpenderBoxGroup item in this)
                    {
                        if (item.Name == id)
                            return item;
                    }
                    return null;
                }
               
            }
            public IGKXExpenderBoxGroupCollections(IGKXExpenderBox expenderBox)
            {
                this.m_groups = new List<IGKXExpenderBoxGroup>();
                this.m_expenderBox = expenderBox;
            }


            public int Count { get { return this.m_groups.Count; } }
            public IEnumerator GetEnumerator()
            {
                return this.m_groups.GetEnumerator();
            }
            public void Add(IGKXExpenderBoxGroup group) {
                if ((group == null) || this.m_groups.Contains(group))
                    return;
                this.m_groups.Add(group);
                group.ExpenderBox = this.m_expenderBox;
                this.m_expenderBox.Controls.Add(group);
                this.m_expenderBox.OnGroupAdded(new CoreItemEventArgs<IGKXExpenderBoxGroup>(group));
            }
            public void Remove(IGKXExpenderBoxGroup group)
            {
                if (this.m_groups.Contains(group))
                {
                    this.m_groups.Remove(group);
                    group.ExpenderBox = null;
                    this.m_expenderBox.Controls.Remove(group);
                    this.m_expenderBox.OnGroupRemoved(new CoreItemEventArgs<IGKXExpenderBoxGroup>(group));
                }
            }

            public void Sort(IComparer comparer)
            {
                IComparer<IGKXExpenderBoxGroup> c = comparer as IComparer<IGKXExpenderBoxGroup>;
                if (c != null)
                {
                    this.m_groups.Sort(c);
                    foreach (IGKXExpenderBoxGroup item in this.m_groups)
                    {
                        item.Items.Sort(comparer);
                    }
                }
            }
            /// <summary>
            /// add a nex expender group
            /// </summary>
            /// <param name="Name"></param>
            /// <returns></returns>
            public IGKXExpenderBoxGroup Add(string Name)
            {
                if (this[Name] == null)
                {
                    IGKXExpenderBoxGroup c = new IGKXExpenderBoxGroup();
                    c.Name = Name;
                    c.CaptionKey = string.Format("group." + Name);
                    this.Add(c);
                    return c;
                }
                return this[Name];
            }

            public void Clear()
            {
                for (int i = 0; i < this.m_groups.Count; i++)
                {
                    this.m_groups[i].Dispose();
                }
                this.m_groups.Clear();

            }
        }

        protected virtual  void OnGroupRemoved(CoreItemEventArgs<IGKXExpenderBoxGroup> e)
        {
            if (this.GroupRemoved != null)
                this.GroupRemoved(this, e);
        }

        protected virtual void OnGroupAdded(CoreItemEventArgs<IGKXExpenderBoxGroup> e)
        {
            if (this.GroupAdded != null)
                this.GroupAdded(this, e);
        }

        /// <summary>
        /// add a group to the expender
        /// </summary>
        /// <param name="groupName">group name and display text</param>
        /// <returns></returns>
        public IGKXExpenderBoxGroup AddGroup(string groupName)
        {
            return this.Groups.Add(groupName);
        }
        /// <summary>
        /// sotr group
        /// </summary>
        /// <param name="comparer"></param>
        public void Sort(IComparer comparer)
        {
            this.m_Groups.Sort(comparer);
        }
        /// <summary>
        /// clear all groups
        /// </summary>
        public void Clear()
        {
            this.m_Groups.Clear();
        }
    }
}
