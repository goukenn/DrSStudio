

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXWinCoreStatus.cs
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
file:XWinCoreStatusStrip.cs
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.GraphicModels;
using IGK.ICore.Windows.Native;
using IGK.ICore.WinUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IGK.ICore.WinCore.WinUI.Controls;
using IGK.ICore.WinCore.WinUI;

namespace IGK.ICore.WinUI
{
    /// <summary>
    /// represent a status tool strip
    /// </summary>
    public class IGKXWinCoreStatus : IGKXControl , IXCoreStatus
    {
        private IGKWinCoreStatusItemsCollection  m_Items;
        const int STATUS_HEIGHT = 24;
        private float m_springWidth;
        private float m_springCount;

        protected override Control.ControlCollection CreateControlsInstance()
        {
            return new ControlCollection(this);
        }
        public new class ControlCollection : Control.ControlCollection
        {
            public ControlCollection(IGKXWinCoreStatus item):base(item)
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
                //do nothing
            }
            public override void Remove(Control value)
            {
                //do nothing
            }
        }
        /// <summary>
        /// get or set the status items
        /// </summary>
        public class IGKWinCoreStatusItemsCollection : 
            IEnumerable, 
            IXCoreStatusItemCollection,
            IComparer<IGKWinCoreStatusItemBase>
        {
            private List<IGKWinCoreStatusItemBase> m_items;
            private IGKXWinCoreStatus m_owner;

            public IGKWinCoreStatusItemsCollection(IGKXWinCoreStatus item)
            {
                this.m_owner = item;
                this.m_items = new List<IGKWinCoreStatusItemBase>();
            }
            public int Count
            {
                get
                {
                    return this.m_items.Count;
                }
            }
            public IGKWinCoreStatusItemBase[] ToArray() {
                return this.m_items.ToArray();
            }

            public void Add(IGKWinCoreStatusItemBase item)
            {
                if ((item != null) && !this.m_items.Contains(item))
                {
                    this.m_items.Add(item);
                    item.Parent = this.m_owner;
                    item.VisibleChanged += item_VisibleChanged;
                    this.m_items.Sort(this);
                    this.m_owner.SetupItemAndInvalidate();
                    this.m_owner.OnItemAdded(new CoreItemEventArgs<IGKWinCoreStatusItemBase>(item));
                }
            }

            void item_VisibleChanged(object sender, EventArgs e)
            {
                this.m_owner.SetupItemAndInvalidate();                
            }
            public void Remove(IGKWinCoreStatusItemBase item)
            {
                if ((item != null) && this.m_items.Contains(item))
                {
                    this.m_items.Remove (item);
                    item.VisibleChanged -= item_VisibleChanged;
                    item.Parent = null;
                    this.m_owner.SetupItemAndInvalidate();
                    this.m_owner.OnItemRemoved(new CoreItemEventArgs<IGKWinCoreStatusItemBase>(item));
                }
            }
            public IEnumerator GetEnumerator()
            {
                return this.m_items.GetEnumerator();
            }


            void IXCoreStatusItemCollection.Add(IXCoreStatusItem item)
            {
                this.Add(item as IGKWinCoreStatusItemBase);
            }

            public void Remove(IXCoreStatusItem item)
            {
                this.Remove(item as IGKWinCoreStatusItemBase);
            }

            public int Compare(IGKWinCoreStatusItemBase x, IGKWinCoreStatusItemBase y)
            {
                return x.Index.CompareTo(y.Index);
            }

            public IXCoreStatusItem Add(enuStatusItemType Status)
            {
                switch (Status)
                {
                    case enuStatusItemType.text:
                        var c =  new IGKWinCoreStatusTextItem();
                        this.Add(c);
                        return c;                        
                    default:
                        break;
                }
                return null;
            }
        }

        public IGKWinCoreStatusItemsCollection  Items
        {
            get { return m_Items; }
        }
        public IGKXWinCoreStatus()
        {
            this.m_Items = new IGKWinCoreStatusItemsCollection(this);
            this.Height = STATUS_HEIGHT;
            this.MinimumSize = new Size(0, STATUS_HEIGHT);
            this.SetStyle(
                  ControlStyles.UserPaint |
                  ControlStyles.ResizeRedraw |
                  ControlStyles.OptimizedDoubleBuffer |
                  ControlStyles.AllPaintingInWmPaint | 
                  ControlStyles.SupportsTransparentBackColor ,
                   true);
            this.Dock = DockStyle.Bottom;
            this.SizeChanged += _SizeChanged;
            this.Paint += _Paint;
            this.SetupItemAndInvalidate();
        }

        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //avoid flickering
            //base.OnPaintBackground(pevent);
        }
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (this.Parent != null)
            {
                UnRegisterEvent();
                Form frm = this.FindForm();

                frm.Activated += frm_Activated;
                frm.GotFocus += frm_GotFocus;
                frm.LostFocus += frm_LostFocus;
                this.m_parentForm = frm;
            }
        }
        void RegisterEvent()
        { 
        }
        void UnRegisterEvent()
        { }
        void frm_LostFocus(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        void frm_GotFocus(object sender, EventArgs e)
        {
            
        }

        void frm_Activated(object sender, EventArgs e)
        {
            this.Invalidate();
        }
        protected override void WndProc(ref Message m)
        {
            
            switch (m.Msg)
            {
                case User32.WM_ACTIVATE:
                    this.Invalidate();
                    break;
            }            
            base.WndProc(ref m);
        }
        private void _Paint(object sender, CorePaintEventArgs e)
        {
            bool v_active = FindForm() == Form.ActiveForm;
            Colorf v_bgColor = v_active ?
                WinCoreControlRenderer.WinCoreActiveStatusBackgroundColor :
                WinCoreControlRenderer.WinCoreInActiveStatusBackgroundColor;
            e.Graphics.Clear(v_bgColor);
            foreach (IGKWinCoreStatusItemBase  item in this.Items)
            {
                if (item.Visible )
                item.Render(e.Graphics, v_active );
            }
        }

        private void _SizeChanged(object sender, EventArgs e)
        {
            this.UpdateSpringWidth();
            this.SetupItemAndInvalidate();
        }

        private void UpdateSpringWidth()
        {
            float w  = this.Width - ((this.Items.Count >0) ? (2* (this.Items.Count-1)) : 0);
            m_springCount = 0;

            foreach (IGKWinCoreStatusItemBase item in this.Items)
            {
                if (item.Spring)
                    m_springCount++;
                if (!item.Visible || item.Spring) 
                    continue;

                w -= item.Bounds.Width;
            }
            this.m_springWidth = w;
        }

        internal  void SetupItemAndInvalidate()
        {
            this.UpdateSpringWidth();
            this.UpdateItems();
            this.Invalidate();
        }

        private void UpdateItems()
        {
            float x = 0;
            float y = 0;
            float w = this.Width;            
            float h = this.Height;
            int c = this.Items.Count;
            float v_sw = this.m_springWidth / this.m_springCount;
            foreach (IGKWinCoreStatusItemBase  item in this.m_Items)
            {
                if (item.Visible)
                {
                    if (item.Spring)
                    {
                        w = v_sw;                        
                    }
                    else {
                        w = item.Bounds.Width;
                    }
                    item.Bounds = new Rectanglef(x, y, w, h);
                    x += w+2;
                }
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            
            base.OnPaint(e);            
        }
       
        public IGKWinCoreStatusItemBase GetNode(string name)
        {
            return null;// this.Items[name];
        }

        public event EventHandler<CoreItemEventArgs<IGKWinCoreStatusItemBase>> ItemAdded;
        public event EventHandler<CoreItemEventArgs<IGKWinCoreStatusItemBase>> ItemRemoved;
        private Form m_parentForm;
        internal void OnItemAdded(CoreItemEventArgs<IGKWinCoreStatusItemBase> e)
        {
            if (ItemAdded != null)
                this.ItemAdded(this, e);
        }

        internal void OnItemRemoved(CoreItemEventArgs<IGKWinCoreStatusItemBase> e)
        {
            if (ItemRemoved != null)
                this.ItemRemoved(this, e);
        }

        
        IXCoreStatusItemCollection IXCoreStatus.Items
        {
            get { return this.m_Items; }
        }

        public string Id
        {
            get { return this.Name; }
        }

        
    }
}

