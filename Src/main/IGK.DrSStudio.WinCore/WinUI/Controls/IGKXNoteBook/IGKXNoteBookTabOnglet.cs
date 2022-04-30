

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXNoteBookTabOnglet.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Resources;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinCore.WinUI.Controls;
using IGK.ICore.WinCore.WinUI;

namespace IGK.DrSStudio.WinUI
{
    public class IGKXNoteBookTabOnglet : IGKXControl 
    {
        private IGKXNoteBookTabBar m_noteBookBar;
        private IGKXNoteBookPage m_notePage;

        protected override System.Drawing.Size DefaultSize
        {
            get
            {
                return new System.Drawing.Size(100, 0);
            }
        }
        protected override System.Drawing.Size DefaultMaximumSize
        {
            get
            {
                return base.DefaultMaximumSize;
            }
        }
        public int Index
        {
            get { return this.NoteBookBar.Onglets.IndexOf(this); }
        }
        public IGKXNoteBookTabBar NoteBookBar {
            get {
                return this.m_noteBookBar;
            }
        }
        IGKXNoteBook NoteBook {
            get {
                return this.NoteBookBar.NoteBook;
            }
        }
        IGKXNoteBookPage NotePage {
            get {
                return this.NoteBook.TabPages[this.Index];
            }
        }

        public IGKXNoteBookTabOnglet()
        {
            this.Click += _Click;
            this.Paint += _Paint;            
            CoreMouseStateManager.Register(this);
        }

        enuNoteBookOrientation Orientation
        {
            get {
                var e = this.NoteBook;
                if (e == null)
                    return enuNoteBookOrientation.Top;
                return e.Orientation;
            }
        }
        void _Paint(object sender, CorePaintEventArgs e)
        {
            RenderOnglet(e);
        }
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            this.SetUpBound();
        }

        private void SetUpBound()
        {
            var e = this.NoteBook;
            if (e == null)
                return;
           
        }
        
        public bool IsSelected
        {
            get { return (this.NotePage == this.NoteBook.SelectedPage ); }
        }
        protected virtual  void RenderOnglet(CorePaintEventArgs e)
        {
            e.Graphics.Clear(WinCoreControlRenderer.NoteBookBarBackgroundColor);
            bool v_isMSOver = this.MouseState == enuMouseState.Hover;
            Colorf v_bgcolor =v_isMSOver ?
                WinCoreControlRenderer.NoteBookBarOngletOverBackgroundColor:
                WinCoreControlRenderer.NoteBookBarOngletBackgroundColor
                ;
            Colorf v_ftcolor =v_isMSOver?
                WinCoreControlRenderer.NoteBookBarOngletOverForeColor
                :
                WinCoreControlRenderer.NoteBookBarOngletForeColor;
            Colorf v_bordercolor = WinCoreControlRenderer.NoteBookBarOngletBorderColor;
            Rectanglef rc = new Rectanglef(0, 0, this.Width, this.Height);

            if (this.IsSelected)
            {
                v_bgcolor = v_isMSOver ?
                WinCoreControlRenderer.NoteBookBarOngletSelectedOverBackgroundColor :
                WinCoreControlRenderer.NoteBookBarOngletSelectedBackgroundColor
                ;
                v_ftcolor = v_isMSOver ?
                WinCoreControlRenderer.NoteBookBarOngletSelectedOverForeColor
                :
                WinCoreControlRenderer.NoteBookBarOngletSelectedForeColor;
            }
            e.Graphics.FillRectangle(v_bgcolor, rc.X, rc.Y , rc.Width , rc.Height );
            e.Graphics.DrawRectangle(v_bordercolor, rc.X, rc.Y, rc.Width-1, rc.Height-1);
            Rectanglef v_orientationRc = rc;
            switch  (this.Orientation)
            { 
                case enuNoteBookOrientation.Top :
                 v_orientationRc = new Rectanglef ( rc.X, 
                        rc.Y+rc.Height - 4, rc.Width, 4);
                 break;
                case enuNoteBookOrientation.Bottom :
                 v_orientationRc = new Rectanglef(rc.X,
                     rc.Y , rc.Width, 4);
                    break;
                case  enuNoteBookOrientation.Left :
                    v_orientationRc = new Rectanglef(rc.X + rc.Width - 4,
                     rc.Y, 4, rc.Height);
                    break;
                case enuNoteBookOrientation.Right :
                    v_orientationRc = new Rectanglef(rc.X,
                     rc.Y, 4, rc.Height);
                    break;
            }

            e.Graphics.FillRectangle(
                        WinCoreControlRenderer.NoteBookBorderColor, v_orientationRc.X,
                        v_orientationRc.Y , v_orientationRc.Width , v_orientationRc.Height );

            StringFormat v_sfg = new StringFormat()
            {
                Trimming = StringTrimming.EllipsisPath,
                FormatFlags = StringFormatFlags.NoWrap,
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            string v_text = CoreResources.GetString (this.NotePage.CaptionKey );
            if (string.IsNullOrEmpty(v_text) == false)
            {
                e.Graphics.DrawString(
                    v_text
                    , this.Font,
                    WinCoreBrushRegister.GetBrush(v_ftcolor),
                    rc,
                    v_sfg
                    );
            }
            v_sfg.Dispose();
        }

        public IGKXNoteBookTabOnglet(IGKXNoteBookTabBar TabBar, IGKXNoteBookPage NoteBookPage):this()
        {
            this.m_noteBookBar = TabBar;
            this.m_notePage = NoteBookPage;
            this.NoteBook.PageRemoved += NoteBook_PageRemoved;
            this.NoteBook.SelectedPageChanged += NoteBook_SelectedTabChanged;
        }

        void NoteBook_SelectedTabChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        void NoteBook_PageRemoved(object sender, CoreItemEventArgs<IGKXNoteBookPage> e)
        {
            if (this.m_notePage == e.Item)
            {
                this.NoteBook.PageRemoved -= NoteBook_PageRemoved;
                this.NoteBookBar.Onglets.Remove(this);
            }
        }

        void _Click(object sender, EventArgs e)
        {
            this.NoteBook.SelectedPage = this.NotePage;
        }


        public int GetPreferredWidth()
        {
            Graphics g = CreateGraphics();
            int w = 0;
            w += ((16 + 1) * 3); //button size
            string V_title = this.Text;
            SizeF v = g.MeasureString(V_title, this.Font, new SizeF(short.MaxValue, short.MaxValue));
            w += (int)(Math.Ceiling(v.Width));
            g.Dispose();
            return w;
        }
    }
}
