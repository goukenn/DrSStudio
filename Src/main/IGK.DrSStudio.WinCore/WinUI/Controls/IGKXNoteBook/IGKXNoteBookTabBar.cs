

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXNoteBookTabBar.cs
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
using System.Threading.Tasks;
using System.Windows.Forms.Layout;
using IGK.ICore.WinCore.WinUI.Controls;
using IGK.ICore.WinCore.WinUI;

namespace IGK.DrSStudio.WinUI
{
    public class IGKXNoteBookTabBar : IGKXControl 
    {
        public override LayoutEngine LayoutEngine
        {
            get
            {
                if (m_lEngine == null)
                    m_lEngine = new IGKXNoteBookTabBarLayoutEngine();
                return m_lEngine;
            }
        }

        class IGKXNoteBookTabBarLayoutEngine : LayoutEngine
        {
            public IGKXNoteBookTabBarLayoutEngine()
                : base()
            {

            }
            public override bool Layout(object container, System.Windows.Forms.LayoutEventArgs layoutEventArgs)
            {
                IGKXNoteBookTabBar bar = container as IGKXNoteBookTabBar;
                int x = 0;
                int y = 0;
                int v_size = 0;
                int v_startIndex = 0;
                int v_index = 0;
                foreach (IGKXNoteBookTabOnglet item in bar.m_Onglets)
                {
                    if (v_index < v_startIndex)
                    {
                        v_index++;
                        continue;
                    }
                    v_size = (int)Math.Max(bar.NoteBook.OngletMinSize, Math.Min(bar.NoteBook.OngletMaxSize, item.GetPreferredWidth()));

                    switch (bar.NoteBook.Orientation)
                    {
                        case enuNoteBookOrientation.Top:                       
                        case enuNoteBookOrientation.Bottom:
                            item.Bounds = new System.Drawing.Rectangle(
                        x, y,
                        v_size,
                        bar.Height
                        );
                            x += v_size + 1;
                            break;
                        case enuNoteBookOrientation.Left:
                        case enuNoteBookOrientation.Right:
                            item.Bounds = new System.Drawing.Rectangle(
                        x, y,
                        bar.Width,
                        v_size
                        );
                            y += item.Height + 1;
                            break;
                    }
                }
                v_index++;
                 return true;
            }
        }


        private IGKXNoteBook  m_NoteBook;
        private OngletCollection m_Onglets;
        private LayoutEngine m_lEngine;

        public OngletCollection Onglets
        {
            get { return m_Onglets; }
        }
        public IGKXNoteBook NoteBook { get { return this.m_NoteBook; } }
        public override System.Windows.Forms.DockStyle Dock
        {
            get
            {
                return System.Windows.Forms.DockStyle.None;
            }
            set
            {
                if (value != System.Windows.Forms.DockStyle.None)
                    throw new Exception("Not Allowed");
                base.Dock = value;
            }
        }
     
        protected override System.Drawing.Size DefaultSize
        {
            get
            {
                return base.DefaultSize;
            }
        }
        public  IGKXNoteBookTabBar()
        {
            this.m_Onglets = new OngletCollection(this);
        }
        internal  IGKXNoteBookTabBar(IGKXNoteBook notebook):this()
        {
            this.m_NoteBook = notebook;
            this.m_NoteBook.PageAdded += m_NoteBook_PageAdded;
            this.m_NoteBook.PageRemoved += m_NoteBook_PageRemoved;
            this.Paint += _Paint;
        }

        void m_NoteBook_PageRemoved(object sender, CoreItemEventArgs<IGKXNoteBookPage> e)
        {
            
        }
        void m_NoteBook_PageAdded(object sender, CoreItemEventArgs<IGKXNoteBookPage> e)
        {
            this.m_Onglets.Add (new IGKXNoteBookTabOnglet  (this, e.Item ));
        }

        void _Paint(object sender, CorePaintEventArgs e)
        {
            RenderTabBar(e);
  
        }
        enuNoteBookOrientation Orientation
        {
            get
            {
                var e = this.NoteBook;
                if (e == null)
                    return enuNoteBookOrientation.Top;
                return e.Orientation;
            }
        }

        private void RenderTabBar(CorePaintEventArgs e)
        {
            if (this.NoteBook.Renderer != null)
            {
                this.NoteBook.Renderer.Render(this, e);
                return;
            }
            e.Graphics.Clear(WinCoreControlRenderer.NoteBookBarBackgroundColor);
            Rectanglef rc = new Rectanglef(0,0, this.Width , Height );
            Rectanglef v_orientationRc = rc;
            switch (this.Orientation)
            {
                case enuNoteBookOrientation.Top:
                    v_orientationRc = new Rectanglef(rc.X,
                           rc.Y + rc.Height - 4, rc.Width, 4);
                    break;
                case enuNoteBookOrientation.Bottom:
                    v_orientationRc = new Rectanglef(rc.X,
                        rc.Y, rc.Width, 4);
                    break;
                case enuNoteBookOrientation.Left:
                    v_orientationRc = new Rectanglef(rc.X + rc.Width - 4,
                     rc.Y, 4, rc.Height);
                    break;
                case enuNoteBookOrientation.Right:
                    v_orientationRc = new Rectanglef(rc.X,
                     rc.Y, 4, rc.Height);
                    break;
            }

            e.Graphics.FillRectangle(
                        WinCoreControlRenderer.NoteBookBorderColor, v_orientationRc.X,
                        v_orientationRc.Y + v_orientationRc.Height - 4, v_orientationRc.Width, 4);
        }

        public class OngletCollection : IEnumerable 
        {
            List<IGKXNoteBookTabOnglet> m_onglets;
            private IGKXNoteBookTabBar m_owner;

            public OngletCollection(IGKXNoteBookTabBar iGKXNoteBookTabBar)
            {
                this.m_onglets = new List<IGKXNoteBookTabOnglet>();
                this.m_owner = iGKXNoteBookTabBar;
            }
            public int IndexOf(IGKXNoteBookTabOnglet onglet)
            {
                return this.m_onglets.IndexOf(onglet);
            }
            public void Add(IGKXNoteBookTabOnglet onglet)
            {
                if (this.m_onglets.CanAdd(onglet))
                {
                    this.m_onglets.Add(onglet);
                    this.m_owner.Controls.Add(onglet);
                }
                
            }
            public void Remove(IGKXNoteBookTabOnglet onglet)
            {

                if (this.m_onglets.CanRemove(onglet))
                {
                    this.m_owner.Controls.Remove(onglet);
                    this.m_onglets.Remove(onglet);
                }
            }




            public IEnumerator GetEnumerator()
            {
                return this.m_onglets.GetEnumerator();
            }
        }
    }
}
