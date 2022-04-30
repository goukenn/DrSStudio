

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXNoteBook.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinCore.WinUI.Controls;

namespace IGK.DrSStudio.WinUI
{
    /// <summary>
    /// represent a note book
    /// </summary>
    public class IGKXNoteBook : IGKXUserControl 
    {
        private IGKXNoteBookTabBar c_tabBar;
        private IGKXNoteBookContentZone c_contentZone;
        private enuNoteBookOrientation m_Orientation;
        const int TAB_SIZE = 32;
        private TabPageCollection m_TabPages;
        private IGKNoteBookRenderer m_Renderer;
        private IGKXNoteBookPage  m_SelectedPage;
        private int m_OngletMinSize;
        private int m_OngletMaxSize;

        public int OngletMaxSize
        {
            get { return m_OngletMaxSize; }
            set
            {
                if (m_OngletMaxSize != value)
                {
                    m_OngletMaxSize = value;
                }
            }
        }
        public int OngletMinSize
        {
            get { return m_OngletMinSize; }
            set
            {
                if (m_OngletMinSize != value)
                {
                    m_OngletMinSize = value;
                }
            }
        }
        public IGKXNoteBookPage  SelectedPage
        {
            get { return m_SelectedPage; }
            set
            {
                if (m_SelectedPage != value)
                {
                    m_SelectedPage = value;
                    OnSelectedTabChanged(EventArgs.Empty);
                }
            }
        }
        public IGKNoteBookRenderer Renderer
        {
            get { return m_Renderer; }
            set
            {
                if (m_Renderer != value)
                {
                    m_Renderer = value;
                }
            }
        }
        public TabPageCollection TabPages
        {
            get { return m_TabPages; }
        }
        public override System.Drawing.Rectangle DisplayRectangle
        {
            get
            {
                Rectangle rc = base.DisplayRectangle;
                switch (this.Orientation)
                {
                    case enuNoteBookOrientation.Top:
                        rc.Y += TAB_SIZE;
                        rc.Height -= TAB_SIZE;
                        break;
                    case enuNoteBookOrientation.Bottom:                        
                        rc.Height -= TAB_SIZE;
                        break;
                    case enuNoteBookOrientation.Left:
                        rc.X += TAB_SIZE;
                        rc.Width  -= TAB_SIZE;
                        break;
                    case enuNoteBookOrientation.Right:                        
                        rc.Width  -= TAB_SIZE;
                        break;
                    default:
                        break;
                }
                return rc;
                
            }
        }


        protected override Size DefaultSize
        {
            get
            {
                return base.DefaultSize;
            }
        }
        [DefaultValue(enuNoteBookOrientation.Top )]
        public enuNoteBookOrientation Orientation
        {
            get { return m_Orientation; }
            set
            {
                if (m_Orientation != value)
                {
                    m_Orientation = value;
                    OnOrientationChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler OrientationChanged;
        public event EventHandler SelectedPageChanged;
        ///<summary>
        ///raise the SelectedPageChanged event
        ///</summary>
        protected virtual void OnSelectedTabChanged(EventArgs e)
        {
            if (SelectedPageChanged != null)
                SelectedPageChanged(this, e);
        }

        ///<summary>
        ///raise the OrientationChanged 
        ///</summary>
        protected virtual void OnOrientationChanged(EventArgs e)
        {
            this.UpdateBarBounds();
            this.Invalidate(true);
            if (OrientationChanged != null)
                OrientationChanged(this, e);
        }
        protected override void Dispose(bool disposing)
        {

            base.Dispose(disposing);
        }
    
        public IGKXNoteBook()
        {
            this.m_TabPages = new TabPageCollection(this);
            this.c_tabBar = new IGKXNoteBookTabBar(this);
            this.c_contentZone = new IGKXNoteBookContentZone(this);
            this.c_contentZone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_Orientation = enuNoteBookOrientation.Top;
            this.m_OngletMaxSize = 150;
            this.m_OngletMinSize = 100;

            this.Controls.Add(c_tabBar);
            this.Controls.Add(c_contentZone);
            this.PageAdded += _PageAdded;
            this.UpdateBarBounds();
        }

        void _PageAdded(object sender, CoreItemEventArgs<IGKXNoteBookPage> e)
        {
            if (this.TabPages.Count == 1)
            {
                this.SelectedPage = e.Item;
            }
            else if (this.TabPages.Count == 0) {
                this.SelectedPage = null;
            }
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            this.UpdateBarBounds();
            base.OnSizeChanged(e);
        }

        private void UpdateBarBounds()
        {
            
            Rectangle rc2 = this.ClientRectangle;   
            switch (this.Orientation)
            {
                case enuNoteBookOrientation.Top:
                    rc2.Height = TAB_SIZE;
                    break;
                case enuNoteBookOrientation.Bottom:

                    rc2.Y = rc2.Height - TAB_SIZE;
                    rc2.Height = TAB_SIZE;
                    break;
                case enuNoteBookOrientation.Left:
                    rc2.Width = TAB_SIZE;
                    rc2.X = 0;                    
                    break;
                case enuNoteBookOrientation.Right:
                    rc2.X = rc2.Width - TAB_SIZE;
                    rc2.Width = TAB_SIZE;
               
                    break;
                default:
                    break;
            }

            this.c_tabBar.Bounds = rc2;
         
        }

        public class TabPageCollection : IEnumerable
        {
            private IGKXNoteBook c_noteBook;
            private List<IGKXNoteBookPage> m_pages;
            public IGKXNoteBookPage this[int index] {
                get {
                    if (m_pages.IndexExists(index))
                        return m_pages[index];
                    return null;
                }
            }
            public int Count
            {
                get { return this.m_pages.Count ; }
            }
            public TabPageCollection(IGKXNoteBook iGKXNoteBook)
            {
                
                this.c_noteBook = iGKXNoteBook;
                this.m_pages = new List<IGKXNoteBookPage>();
            }


            public IEnumerator GetEnumerator()
            {
                return this.m_pages.GetEnumerator();
            }
            public void Add(IGKXNoteBookPage Page)
            {
                if (this.m_pages.CanAdd(Page))
                {
                    
                    this.m_pages.Add(Page);
                    this.c_noteBook.OnPageAdded(new CoreItemEventArgs<IGKXNoteBookPage>(Page));
                }
            }
            public void Remove (IGKXNoteBookPage page)
            {
                if (this.m_pages.CanRemove(page))
                {
                    this.m_pages.Remove(page);
                    this.c_noteBook.OnPageRemoved(new CoreItemEventArgs<IGKXNoteBookPage>(page));
                }
            }
        }

        public event EventHandler<CoreItemEventArgs<IGKXNoteBookPage>> PageAdded;
        public event EventHandler<CoreItemEventArgs<IGKXNoteBookPage>> PageRemoved;
        ///<summary>
        ///raise the PageRemoved 
        ///</summary>
        protected virtual void OnPageRemoved(CoreItemEventArgs<IGKXNoteBookPage> e)
        {
            if (PageRemoved != null)
                PageRemoved(this, e);
        }

        ///<summary>
        ///raise the PageAdded 
        ///</summary>
        protected virtual void OnPageAdded(CoreItemEventArgs<IGKXNoteBookPage> e)
        {
            if (PageAdded != null)
                PageAdded(this, e);
        }



    }
}
