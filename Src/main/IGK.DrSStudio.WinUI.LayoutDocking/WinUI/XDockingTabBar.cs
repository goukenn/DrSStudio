

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XDockingTabBar.cs
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
file:XDockingTabBar.cs
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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
namespace IGK.DrSStudio.WinUI
{
    using IGK.ICore.WinCore;
using IGK.ICore.Drawing2D;
    using IGK.ICore.Resources;
    using IGK.ICore.GraphicModels;
    using IGK.ICore.WinUI;
    using IGK.ICore;
    using IGK.ICore.WinCore.WinUI.Controls;
    /// <summary>
    /// represent the tab bar items
    /// </summary>
    public class XDockingTabBar : IGKXControl 
    {
        internal const int SIZE = 24;
        internal const int ONGLET_SIZE = 120;
        internal const int ONGLET_MIN_SIZE = 120;
        internal const int ONGLET_MAX_SIZE = ONGLET_MIN_SIZE + 30;
        internal const int ONGLET_SPACE = ONGLET_SIZE + 1;
        internal const int NAV_BUTTONSIZE = 16;
        internal const int TAB_NO_SEL = -1;
        internal const int TAB_NAV_FORWARD = -2;
        internal const int TAB_NAV_PREVIOUS = -3;
        internal const string TAB_NAV_IMG_KEY = "navigation_btn";
        private enuTabBarDirection m_tagdirection;
        
        private int m_OverIndex;
        private XDockingPanel m_owner;
        private bool m_navigationButtonVisible;
        private int m_offsetTab;
        private NavigationButton c_btn_next; //prevent navigation button
        private NavigationButton c_btn_previous; //prevent navigation button


        class NavigationButton : IGKXButton 
        {
            public NavigationButton():base()
            {
                this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                this.SetStyle(ControlStyles.ResizeRedraw, true);
                this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
                this.SetStyle(ControlStyles.UserPaint, true);
                this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            }
        }
       
        protected int OverIndex {
            get {
                return this.m_OverIndex;
            }
            set {
                if (this.m_OverIndex != value)
                {
                    this.m_OverIndex = value;                
                    this.Invalidate(false);
                }
            }
        }
        public XDockingTabBar(XDockingPanel owner) 
        {
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.m_owner = owner;
            this.m_offsetTab = 0;
            this.m_navigationButtonVisible = false;
            this.c_btn_next = new NavigationButton();
            this.c_btn_next.ButtonDocument = CoreButtonDocument.Create ( CoreResources.GetAllDocuments("navigation_btn_nextv"));
            this.c_btn_next.Click += new EventHandler(c_btn_next_Click);
            this.c_btn_next.Visible = false;
            this.c_btn_previous = new NavigationButton();
            this.c_btn_previous.Visible = false;
            this.c_btn_previous.ButtonDocument = CoreButtonDocument.Create(CoreResources.GetAllDocuments(TAB_NAV_IMG_KEY));
            this.c_btn_previous.Click += new EventHandler(c_btn_previous_Click);
            this.c_btn_previous.MouseDown += c_btn_previous_MouseDown;
            this.Controls.Add(c_btn_next);
            this.Controls.Add(c_btn_previous);
            this.m_owner.SelectedPageChanged += new EventHandler(m_owner_SelectedPageChanged);
            this.m_owner.PageAdded += new DockingPageEventHandler(m_owner_PageAdded);
            this.m_owner.PageRemoved += new DockingPageEventHandler(m_owner_PageRemoved);
            this.SizeChanged += new EventHandler(_SizeChanged);
            this.Paint += _Paint;
            this.Click += new EventHandler(_Click);
            this.DoubleClick += new EventHandler(DockingTabBar_DoubleClick);
            this.MouseDown += _MouseDown;
        }

        void c_btn_previous_MouseDown(object sender, CoreMouseEventArgs e)
        {
            //starting down mouse next
            new PreviousMouseAccelerator(this, this.c_btn_previous);
        }
        class NavigationButtonAccelerator
        { 
            private NavigationButton navigationButton;
            private Timer m_timer;
            protected XDockingTabBar xDockingTabBar;

            private NavigationButtonAccelerator(NavigationButton navigationButton)
            {                
                this.navigationButton = navigationButton;
                this.navigationButton.MouseUp += navigationButton_MouseUp;
                this.m_timer = new  Timer();
                this.m_timer.Tick += m_timer_Tick;
                this.m_timer.Interval = 1000;
                this.m_timer.Enabled = true;
            }

            public NavigationButtonAccelerator(XDockingTabBar xDockingTabBar, NavigationButton navigationButton)
                : this(navigationButton)
            {
                this.xDockingTabBar = xDockingTabBar;
            }

            void m_timer_Tick(object sender, EventArgs e)
            {
                if (this.m_timer.Interval == 1000)
                    this.m_timer.Interval = 200;
                this.Update();
            }

            protected virtual  void Update()
            {
                
            }

            void navigationButton_MouseUp(object sender, CoreMouseEventArgs e)
            {
                this.m_timer.Enabled = false;
                this.m_timer.Dispose();
            }
        }
        class PreviousMouseAccelerator : NavigationButtonAccelerator
        {            
            public PreviousMouseAccelerator(XDockingTabBar xDockingTabBar, NavigationButton navigationButton):base(xDockingTabBar, navigationButton )
            {
            }
            protected override void Update()
            {
                this.xDockingTabBar.MoveToPrevious(); 
            }
        }
        class NextsMouseAccelerator : NavigationButtonAccelerator
        {
            public NextsMouseAccelerator(XDockingTabBar xDockingTabBar, NavigationButton navigationButton)
                : base(xDockingTabBar, navigationButton)
            {
            }
            protected override void Update()
            {
                this.xDockingTabBar.MoveToNext();
            }
        }
      
        void c_btn_previous_Click(object sender, EventArgs e)
        {
            this.MoveToPrevious();
        }

        private void MoveToPrevious()
        {

            if (this.m_offsetTab > 0)
            {
                this.m_offsetTab -= 1;
                this.Invalidate(false);
            }
        }
        void c_btn_next_Click(object sender, EventArgs e)
        {
            this.MoveToNext();
        }

        private void MoveToNext()
        {

            if (this.m_offsetTab < this.Owner.Pages.Count - 1)
            {
                this.m_offsetTab += 1;
                this.Invalidate(false);
            }
        }
         private void _Paint(object sender, CorePaintEventArgs e)
        {
             RenderTab(e);
        }
        void _SizeChanged(object sender, EventArgs e)
        {
            this.SetupNavigation();
        }
        private bool NavigationButtonVisible {
            get {
                return this.m_navigationButtonVisible;
            }
            set {
                if (this.m_navigationButtonVisible != value)
                {
                    this.m_navigationButtonVisible = value;
                    if (value)
                    {
                        this.m_offsetTab = this.Owner.Pages.IndexOf(this.Owner.SelectedPage);
                    }
                    else
                        this.m_offsetTab = 0;
                    OnNavigationButtonVisibleChanged(EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// event raise when navigation changed
        /// </summary>
        public event EventHandler NavigationButtonVisibleChanged;
        protected virtual void OnNavigationButtonVisibleChanged(EventArgs eventArgs)
        {
            this.c_btn_next.Visible = this.NavigationButtonVisible;
            this.c_btn_previous.Visible = this.NavigationButtonVisible;
            SetupNavigationButtonBound();
            if (this.NavigationButtonVisibleChanged != null)
                this.NavigationButtonVisibleChanged(this, eventArgs);
        }
        private void SetupNavigationButtonBound()
        {
            if (this.NavigationButtonVisible)
            {
                Rectangle rc = Rectangle.Empty;
                //update size
                switch (Direction)
                {
                    case enuTabBarDirection.Horizontal:
                        rc = new Rectangle(0, 0, NAV_BUTTONSIZE, this.Height);
                        this.c_btn_previous.Bounds = rc;
                        rc.Y = this.Height - NAV_BUTTONSIZE;
                        this.c_btn_next.Bounds = rc;
                        break;
                    case enuTabBarDirection.VerticalLeft :
                    case enuTabBarDirection.VerticalRight :
                    default:
                        rc = new Rectangle(1, 1, this.Width, NAV_BUTTONSIZE);
                        this.c_btn_previous.Bounds = rc;
                        rc.Y = this.Height - NAV_BUTTONSIZE;
                        this.c_btn_next.Bounds = rc;

         //               this.c_btn_next.ButtonDocument = CoreButtonDocumentUtils.Rotate(
         //CoreButtonDocument.Create(CoreResources.GetAllDocuments(TAB_NAV_IMG_KEY)),
         // enuButtonDocumentRotation.FlipHorizontal
         //);
                        break;
                }
            }
        }
        void SetupNavigation()
        {
            int v_maxcount = 0;
            switch (this.Direction)
            {
                case enuTabBarDirection.Horizontal :
                    v_maxcount = (this.Width - (2 * (NAV_BUTTONSIZE + 1))) / ONGLET_SIZE;
                    break;
                case enuTabBarDirection.VerticalLeft :                    
                case enuTabBarDirection.VerticalRight :
                    v_maxcount = (this.Height - (2 * (NAV_BUTTONSIZE + 1))) / ONGLET_SIZE;
                    break;
            }
            this.NavigationButtonVisible = (v_maxcount < this.Owner.Pages.Count);
            this.SetupNavigationButtonBound();
        }
        void DockingTabBar_DoubleClick(object sender, EventArgs e)
        {
            if ((this.OverIndex >= 0) && (this.OverIndex < this.Owner.Pages.Count))
            {
                this.Owner.SelectedPage = this.Owner.Pages[this.OverIndex];                
                this.Owner.Expand();
            }
        }
        void _MouseDown(object sender, CoreMouseEventArgs e)
        {
            switch (e.Button)
            {
                case enuMouseButtons.Left:
                    if (this.NavigationButtonVisible)
                    {
                        Navigate();                       
                    }
                    break;
            }
        }
        private void Navigate()
        {
            //Move tab offset if NavigationButtonVisible
            switch (this.OverIndex)
            {
                case TAB_NAV_FORWARD:
                    if (this.m_offsetTab < this.Owner.Pages.Count - 1)
                    {
                        this.m_offsetTab += 1;
                        this.Invalidate(false);
                    }
                    break;
                case TAB_NAV_PREVIOUS:
                    if (this.m_offsetTab > 0)
                    {
                        this.m_offsetTab -= 1;
                        this.Invalidate(false );
                    }
                    break;
            }
        }
        void _Click(object sender, EventArgs e)
        {
            if ((this.OverIndex >= 0) && (this.OverIndex < this.Owner.Pages.Count))
            {
                this.Owner.SelectedPage = this.Owner.Pages[this.OverIndex];
            }
        }
        void m_owner_PageRemoved(object sender, DockingPageEventArgs e)
        {
            this.SetupNavigation();
            this.Invalidate(false);
        }
        void m_owner_PageAdded(object sender, DockingPageEventArgs e)
        {
            this.SetupNavigation();
            this.Invalidate(false);
        }
        void m_owner_SelectedPageChanged(object sender, EventArgs e)
        {
            if (this.NavigationButtonVisible)
            {
                int i= this.Owner.Pages.IndexOf(this.Owner.SelectedPage);
                if ( i < m_offsetTab)
                {
                    if (i == -1)
                        m_offsetTab = 0;
                    else 
                        m_offsetTab = i;
                }
            }
            this.Invalidate(false);
        }
        private void CheckTabIndex(MouseEventArgs e)
        {
            if (this.Owner.Pages.Count == 0)
            {
                m_OverIndex = TAB_NO_SEL ;
                return;
            }
            int offset = 0;
            if (this.NavigationButtonVisible)
            {
                offset = NAV_BUTTONSIZE;
                Rectangle rc = Rectangle.Empty ;
                if (this.Direction == enuTabBarDirection.Horizontal)
                {
                    rc = new Rectangle(0, 0, NAV_BUTTONSIZE, this.Height);
                    if (rc.Contains(e.Location))
                    {
                        OverIndex = TAB_NAV_PREVIOUS;
                        return;
                    }
                    else
                    {
                        rc.X = this.Width - NAV_BUTTONSIZE;
                        if (rc.Contains(e.Location))
                        {
                            OverIndex = TAB_NAV_FORWARD;
                            return;
                        }
                    }
                }
                else
                {                    
                    rc = new Rectangle(0, 0, this.Width , NAV_BUTTONSIZE);
                    if (rc.Contains(e.Location))
                    {
                        OverIndex = TAB_NAV_PREVIOUS;
                        return;
                    }
                    else
                    {
                        rc.Y = this.Height  - NAV_BUTTONSIZE;
                        if (rc.Contains(e.Location))
                        {
                            OverIndex = TAB_NAV_FORWARD;
                            return;
                        }
                    }
                }                    
            }
            int v_index = TAB_NO_SEL;
            if (this.Direction == enuTabBarDirection.Horizontal)
                v_index = (-offset + e.Location.X) / ONGLET_SPACE;
            else
                v_index = (-offset + e.Location.Y) / ONGLET_SPACE;
            v_index += m_offsetTab;
            if ((v_index >= 0) && (v_index < this.Owner.Pages.Count))
            {
                OverIndex = v_index;
                return;
            }
            OverIndex = TAB_NO_SEL ;
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            switch(e.Button)
            {
                case MouseButtons.None:
                case MouseButtons.Left :
                    {
                        MouseState = enuMouseState.Hover;// enuButtonState.Over;
                        CheckTabIndex(e);
                    }
                    break;
            }
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.OverIndex = -1;
            MouseState = enuMouseState.None;
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if ((OverIndex >= 0) && (OverIndex < this.Owner .Pages.Count))
                    {
                        MouseState = enuMouseState.None;
                    }
                    break;
            }
            base.OnMouseUp(e);
        }
        private void RenderTab(CorePaintEventArgs e)
        {
            ICoreGraphics g = e.Graphics;
            Rectangle rc = this.ClientRectangle;
            DockingDrawTabOngletEventArgs v_e = null;
            float angle = 0.0f;
            enuTabBarDirection v_direction = Direction;
            int v_maxcount = 0;
            if (this.Direction == enuTabBarDirection.Horizontal)
            {
                v_maxcount = this.Width / ONGLET_SPACE;
            }
            else
                v_maxcount = this.Height / ONGLET_SPACE;
            switch (v_direction)
            {
                case enuTabBarDirection.Horizontal:
                    angle = 90;
                    break;
                case enuTabBarDirection.VerticalLeft:
                    angle = 0;
                    break;
                case enuTabBarDirection.VerticalRight:
                    angle = 180;
                    break;
            }
            Colorf cl1 = DockingRenderer.DockingTabBarStartColor;
            Colorf cl2 = DockingRenderer.DockingTabBarEndColor;
            Colorf cl3 = DockingRenderer.DockingTabBarOngletColor;
            Colorf cl4 = DockingRenderer.DockingTabBarOngletBorderColor;
            //--------------------------------------------------
            //draw background
            //--------------------------------------------------
            using (Brush lbr =
                WinCoreBrushRegister.CreateBrush(rc, cl1, cl2, angle))
            {
                e.Graphics.FillRectangle(lbr, rc);
            }
            rc = GetOngletRectangle(rc, 0);
            if (!this.Enabled)
            {
                cl3 = DockingRenderer.DockingTabBarDisableColor;
            }
            cl2 = DockingRenderer.DockingTabBarOngletUnselectedColor;
            IDockingPage page = null;
            for (int i = this.m_offsetTab; i < this.Owner.Pages.Count; i++)
            {
                //if (i < 0) continue;
                page = this.Owner.Pages[i];
                
                if (page == this.m_owner.SelectedPage)
                {
                    using (LinearGradientBrush lbr = WinCoreBrushRegister.CreateBrush(rc,
                        DockingRenderer.DockingTabBarSelectedStartColor,
                        DockingRenderer.DockingTabBarSelectedEndColor,
                        angle))
                    {
                        e.Graphics.FillRectangle(lbr, rc);
                    }
                }
                else
                    e.Graphics.FillRectangle(cl2, rc.X, rc.Y, rc.Width, rc.Height);
                //ControlPaint.DrawBorder(graphics, rc, cl4, ButtonBorderStyle.Solid);
                if ((page != null) && (page.HostedControl != null))
                {
                    v_e = new DockingDrawTabOngletEventArgs(
                        e.Graphics,
                        new Rectanglef(rc.X, rc.Y, rc.Width, rc.Height),
                        page);
                    OnDrawTabOnglet(v_e);
                }
                if (v_direction != enuTabBarDirection.Horizontal)
                    rc.Y += ONGLET_SPACE;
                else
                    rc.X += ONGLET_SPACE;
            }
            //render Navigation button
            //if (this.NavigationButtonVisible)
            //{
            //    switch (Direction)
            //    {
            //        case enuTabBarDirection.Horizontal:
            //            {
            //                //Rectanglei v_rc = new Rectanglei(0, 0, NAV_BUTTONSIZE, this.Height);
            //                //g.FillRectangle(Brushes.SaddleBrown, rc);
            //                //ControlPaint.DrawBorder(e, rc, Color.DarkBlue, ButtonBorderStyle.Solid);
            //                //if (this.m_navigationButton != null)
            //                //    this.m_navigationButton.Draw(g, false, v_rc, enuFlipMode.None);
            //                //rc.Y = this.Height - NAV_BUTTONSIZE;
            //                //g.FillRectangle(Brushes.SaddleBrown, rc);
            //                //ControlPaint.DrawBorder(e, rc, Color.DarkBlue, ButtonBorderStyle.Solid);
            //                //if (this.m_navigationButton != null)
            //                //    this.m_navigationButton.Draw(g, false, v_rc, enuFlipMode.FlipHorizontal);
            //            }
            //            break;
            //        default:
            //            {
            //                //Rectanglei v_rc = new Rectanglei(0, 0, this.Width, NAV_BUTTONSIZE);
            //                //g.FillRectangle(Brushes.SaddleBrown, rc);
            //                ////ControlPaint.DrawBorder(e, rc, Color.DarkBlue, ButtonBorderStyle.Solid);
            //                //if (this.m_navigationButton != null)
            //                //    this.m_navigationButton.Draw(g, false, v_rc, enuFlipMode.None);
            //                //rc.Y = this.Height - NAV_BUTTONSIZE;
            //                //g.FillRectangle(Brushes.SaddleBrown, rc);
            //                ////ControlPaint.DrawBorder(e, rc, Color.DarkBlue, ButtonBorderStyle.Solid);
            //                //if (this.m_navigationButton != null)
            //                //    this.m_navigationButton.Draw(g, false, v_rc, enuFlipMode.FlipVertical);
            //            }
            //            break;
            //    }
            //}
        }
        private Rectangle GetOngletRectangle(Rectangle rc, int index)
        {
            Rectangle v_rc = rc;
            int step = 0;
            if (this.NavigationButtonVisible)
            {
                step = NAV_BUTTONSIZE;
            }
            if ((this.Direction == enuTabBarDirection.VerticalLeft )||
                (this.Direction == enuTabBarDirection.VerticalRight ))
            {
                v_rc.Height = ONGLET_SIZE;
                v_rc.Width += 1;
                v_rc.Y =step + ONGLET_SIZE  * index;
            }
            else
            {
                v_rc.X = step +  ONGLET_SIZE  * index;
                v_rc.Width = ONGLET_SIZE ;
                v_rc.Height += 1;
            }
            return v_rc;
        }
        protected virtual void OnDrawTabOnglet(DockingDrawTabOngletEventArgs e)
        {
            //-------------------------------------------------------------------------
            //render tab onglet
            //-------------------------------------------------------------------------
            //get direction

            Rectanglef v_rcf = new Rectanglef(e.Rectangle.X, e.Rectangle.Y,
                e.Rectangle.Width , e.Rectangle.Height );
            StringFormat frm = new StringFormat();
            frm.FormatFlags |= StringFormatFlags.FitBlackBox | StringFormatFlags.NoWrap;
            if ((this.Direction == enuTabBarDirection.VerticalLeft  )||
                (this.Direction == enuTabBarDirection.VerticalRight ))
            {
                frm.FormatFlags |= StringFormatFlags.DirectionVertical;
            }
            else
            {
                frm.Alignment = StringAlignment.Near;
                frm.LineAlignment = StringAlignment.Center;
            }
            frm.Trimming = StringTrimming.EllipsisPath;
            FontStyle fs = FontStyle.Regular;
            Colorf cl = DockingRenderer.DockingTabBarOngletForeColor;
            if (e.Page == this.m_owner.SelectedPage)
            {
                fs |= FontStyle.Bold;
                cl = DockingRenderer.DockingTabBarSelectedForeColor;
            }
            Font ft = new Font(this.Font, fs);
            object  v_state = e.Graphics.Save();

            if (this.Direction == enuTabBarDirection .VerticalLeft )
            {                
                Vector2f pts = CoreMathOperation.GetCenter (v_rcf);
                e.Graphics.TranslateTransform(-pts.X, -pts.Y, enuMatrixOrder.Append);
                e.Graphics.RotateTransform(180, enuMatrixOrder.Append);
                e.Graphics.TranslateTransform(pts.X, pts.Y, enuMatrixOrder.Append);
            }
            switch (this.MouseState )
            {
                case  enuMouseState.Hover  :
                    if (this.OverIndex == this.Owner.Pages.IndexOf(e.Page))
                        cl = DockingRenderer.DockingTabBarOverForeColor;
                    break;
            }
            Rectanglei v_irc = Rectanglef.Round(e.Rectangle);
            Rectanglei v_rc = v_irc;
            ICore2DDrawingDocument doc = e.Page.HostedControl.ToolDocument;
            if (doc != null)
            {
                object  v_state2 = e.Graphics.Save();
                switch (this.Direction)
                {
                    case enuTabBarDirection.VerticalLeft :
                        e.Graphics.ResetTransform();
                        doc.Draw(e.Graphics, new Rectanglei(v_rc.X + 4, v_rc.Bottom - 20, 16, 16));
                        v_rc = new Rectanglei(v_rc.X, v_irc.Y + 24, v_irc.Width, v_irc.Height - 24);
                        break;
                    case enuTabBarDirection.VerticalRight :                        
                        doc.Draw(e.Graphics, new Rectanglei(v_rc.X + 4, v_rc.Y + 4, 16, 16));
                        v_rc = new Rectanglei(v_rc.X, v_irc.Y + 24, v_irc.Width, v_irc.Height - 24);
                        break;
                    case enuTabBarDirection.Horizontal:
                        doc.Draw(e.Graphics, new Rectanglei(v_rc.X + 4, v_rc.Y + 4, 16, 16));
                        v_rc = new Rectanglei(v_rc.X + 24, v_irc.Y, v_irc.Width - 24, v_irc.Height);
                        break;
                    default:
                        v_rc = new Rectanglei(v_rc.X, v_irc.Y + 24, v_irc.Width, v_irc.Height - 24);
                        break;
                }
                e.Graphics.Restore(v_state2);
            }
            e.Graphics.DrawString(
                CoreSystem.GetString(e.Page.HostedControl.CaptionKey),
                ft,
                WinCoreBrushRegister.GetBrush(cl),
                v_rc, 
                frm);
            e.Graphics.Restore(v_state);
            ft.Dispose();
            frm.Dispose();
        }
        #region IDockingTabBar Members
        public enuTabBarDirection Direction
        {
            get
            {
                return this.m_tagdirection;
            }
            set
            {//set the orientation
                m_tagdirection = value;
            }
        }
        #endregion
       /// <summary>
        /// get the owner
        /// </summary>
        public XDockingPanel Owner
        {
            get { return m_owner; }
        }
    }
}

