

using IGK.ICore.WinCore;
using IGK.ICore;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXWinCoreStatusItemBase.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.GraphicModels;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.WinUI
{
    /// <summary>
    /// Represent a base status item. that will be renderer on IGKXWinCoreStatus Control
    /// </summary>
    public class IGKWinCoreStatusItemBase : IDisposable , IXCoreStatusItem 
    {
        private Rectanglef m_Bounds;
        private IGKXWinCoreStatus  m_Parent;
        private bool m_Spring;
        private bool m_Visible;
        private int m_Index;
        /// <summary>
        /// get or set the required index
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
        ///<summary>
        ///raise the VisibleChanged 
        ///</summary>
        protected virtual void OnVisibleChanged(EventArgs e)
        {
            if (VisibleChanged != null)
                VisibleChanged(this, e);
        }

        /// <summary>
        /// get or this if this element is plenty of spring
        /// </summary>
        public bool Spring
        {
            get { return m_Spring; }
            set
            {
                if (m_Spring != value)
                {
                    m_Spring = value;
                }
            }
        }
        /// <summary>
        /// get or set the parent item
        /// </summary>
        public IGKXWinCoreStatus  Parent
        {
            get { return m_Parent; }
            set
            {
                if (m_Parent != value)
                {
                    m_Parent = value;
                    OnParentChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler ParentChanged;
        ///<summary>
        ///raise the ParentChanged 
        ///</summary>
        protected virtual void OnParentChanged(EventArgs e)
        {
            if (ParentChanged != null)
                ParentChanged(this, e);
        }

        
        /// <summary>
        /// get or set the bounds
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
        public IGKWinCoreStatusItemBase()
        {
            this.m_Visible = true;
            this.m_Spring = false;
            this.m_Parent = null;
            this.m_Bounds = Rectanglef.Empty;
        }
        /// <summary>
        /// override this method to render the item on tab
        /// </summary>
        /// <param name="graphics"></param>
        public virtual void Render(ICoreGraphics graphics, bool active)
        { 

        }


        public virtual void Dispose()
        {
            
        }
    }
}
