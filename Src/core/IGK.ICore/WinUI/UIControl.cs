

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: UIControl.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.WinUI
{
    
    /// <summary>
    /// represent a control in ICore
    /// </summary>
    public class UIControl : MarshalByRefObject , IDisposable 
    {
        private Rectanglef m_Bounds;
        private bool m_Visible;
        private bool m_Enabled;
        private UIControl m_Parent;
        private object m_syncobj = new object();

        /// <summary>
        /// get the parent control
        /// </summary>
        public UIControl Parent
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
        /// <summary>
        /// get or set if this element is enabled
        /// </summary>
        public bool Enabled
        {
            get { return m_Enabled; }
            set
            {
                if (m_Enabled != value)
                {
                    m_Enabled = value;
                }
            }
        }
        /// <summary>
        /// get or set if this element is visible
        /// </summary>
        public bool Visible
        {
            get { return m_Visible; }
            set
            {
                if (m_Visible != value)
                {
                    m_Visible = value;
                }
            }
        }
        /// <summary>
        /// get or set if this element is disposed
        /// </summary>
        public bool IsDisposed { get { return (this.m_syncobj != null); } }
       
        /// <summary>
        /// get or set the bounds of the controls
        /// </summary>
        public Rectanglef Bounds
        {
            get { return m_Bounds; }
            set
            {
                if (m_Bounds != value)
                {
                    m_Bounds = value;
                }
            }
        }

        public virtual ControlCollection CreateControlInstance()
        {
            return new ControlCollection(this);
        }

        public class ControlCollection : IEnumerable
        {
            private UIControl m_control;
            private List<UIControl> m_childs;
            public ControlCollection(UIControl control)
            {
                this.m_control = control;
                this.m_childs = new List<UIControl>();
            }
            public int Count { get { return this.m_childs.Count; } }
            public UIControl[] ToArray() {
                return this.m_childs.ToArray();
            }
            public void Add(UIControl control)
            {
                if ((control != null) && (!this.m_childs.Contains(control)))
                {
                    this.m_childs.Add(control);
                    control.Parent = this.m_control;
                    this.m_control.OnItemAdded(new CoreItemEventArgs<UIControl>(control));
                }
            }
            public void Remove(UIControl control)
            {
                if (this.m_childs.Contains(control))
                {
                    this.m_childs.Remove(control);
                    control.Parent = this.m_control;
                    this.m_control.OnItemRemoved(new CoreItemEventArgs<UIControl>(control));
                }
            }


            public IEnumerator GetEnumerator()
            {
                return this.m_childs.GetEnumerator();
            }
        }

        #region "EVENTS"
        public event EventHandler<CoreItemEventArgs<UIControl>> ItemAdded;
        public event EventHandler<CoreItemEventArgs<UIControl>> ItemRemoved;
        public event EventHandler ParentChanged;

        #endregion
        protected virtual void OnParentChanged(EventArgs e)
        {
            if (this.ParentChanged != null)
                this.ParentChanged(this, e);

        }
        protected virtual void OnItemAdded(CoreItemEventArgs<UIControl> e)
        {
            if (this.ItemAdded != null)
                this.ItemAdded(this, e);
            
        }

        protected virtual void OnItemRemoved(CoreItemEventArgs<UIControl> e)
        {
            if (this.ItemRemoved != null)
                this.ItemRemoved(this, e);
        }
        
        public void Dispose()
        {
            this.Dispose(false);
            this.m_syncobj = null;
         
        }
        /// <summary>
        /// override this method to dispose your element
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {            
        }
    }
}
