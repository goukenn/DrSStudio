

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XHistoryItemControl.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:XHistoryItemControl.cs
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
/* 
-------------------------------------------------------------
This file is part of iGK-DEV-DrawingStudio
-------------------------------------------------------------
-------------------------------------------------------------
-------------------------------------------------------------
view license file in Documentation folder to get more info
Copyright (c) 2008-2010 
Author  : Charles A.D. BONDJE DOUE 
mail : bondje.doue@hotmail.com
-------------------------------------------------------------
-------------------------------------------------------------
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing .Drawing2D ;
using System.Drawing;
using System.Windows.Forms ;
namespace IGK.DrSStudio.Drawing2D.WinUI
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio.History;
    class XHistoryItemControl : XControl
    {
        internal const int SIZE = 16;
        private enuButtonState m_ButtonState;
        private UIXHistoryControl m_Owner;
        private IHistoryAction m_history;
        protected enuButtonState ButtonState {
            get {
                return this.m_ButtonState;
            }
            set {
                if (m_ButtonState != value)
                {
                    this.m_ButtonState = value;
                    this.Invalidate();
                }
            }
        }
        public UIXHistoryControl Owner {
            get {
                return m_Owner;
            }
        }
        public string Info
        {
            get { return this.m_history.Info; }            
        }
        public void Draw(Graphics g)
        {
            Rectangle v_rc = this.ClientRectangle;
            Color v_fcolor = HistoryRenderer.HistoryForeColor;
            Color v_bgcolor = HistoryRenderer.HistoryBackgroundColor;
            Color v_scolor = HistoryRenderer.HistoryBackgroundStartColor;
            Color v_ecolor = HistoryRenderer.HistoryBackgroundEndColor;
            bool v_selected = (this.m_history.Index == this.Owner.HistoryList.HistoryIndex);
            if (this.Enabled)
            {
                if (v_selected)
                {
                    v_scolor = HistoryRenderer.HistorySelectedStartColor;
                    v_ecolor = HistoryRenderer.HistorySelectedEndColor;
                    v_fcolor = HistoryRenderer.HistorySelectedForeColor;
                }
                else
                {
                    switch (this.ButtonState)
                    {
                        case enuButtonState.Over :
                            v_scolor = HistoryRenderer.HistoryOverStartColor;
                            v_ecolor = HistoryRenderer.HistoryOverEndColor;
                            v_fcolor = HistoryRenderer.HistoryOverForeColor;
                            break;
                        case enuButtonState.Normal :
                            break;
                        case enuButtonState.Down:
                            break;
                        case enuButtonState.Disabled :
                            break;
                    }
                }
            }
            else {
                v_scolor = Color.Gray;
                v_ecolor = Color.Gray;
            }
            using (LinearGradientBrush br = new LinearGradientBrush(this.ClientRectangle, v_scolor, v_ecolor, 90.0f))
            {
                    g.FillRectangle(br, this.ClientRectangle);
           }
            ICore2DDrawingDocument  m_document = CoreResources.GetDocument(this.m_history.ImgKey);
            if (m_document != null)
            {
                m_document.Draw(g, false, new Rectangle(0, 0, 16, SIZE), enuFlipMode.None);
            }
            StringFormat sf = new StringFormat();
            sf.Trimming = StringTrimming.EllipsisCharacter;
            g.DrawString (this.Info , this.Font,
                CoreBrushRegister.GetBrush (v_fcolor),
                new Rectangle(new Point (16, 0), new Size (this.Width, SIZE )),sf);
            sf.Dispose();
            ControlPaint.DrawBorder(g, this.ClientRectangle,
                HistoryRenderer.HistoryBorderColor ,
                ButtonBorderStyle.Solid);
        }
        internal XHistoryItemControl(UIXHistoryControl owner,
            IHistoryAction history)
        {
            this.SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(System.Windows.Forms.ControlStyles.ResizeRedraw, true);
            this.m_Owner = owner;
            this.m_history = history;
            this.Width = owner.Width;
            this.ButtonState = enuButtonState.Normal;
            this.Height = SIZE;
            this.Paint += new PaintEventHandler(_Paint);
            this.MouseClick += new MouseEventHandler(XHistoryItemControl_MouseClick);
            this.MouseMove += new MouseEventHandler(_MouseMove);
            this.MouseLeave += new EventHandler(_MouseLeave);
            this.m_Owner.SizeChanged += new EventHandler(_SizeChanged);
            this.m_Owner.HistoryList.HistoryItemAdded += new HistoryItemEventHandler(HistoryList_HistoryItemAdded);
            this.m_Owner.HistoryList.HistoryChanged += new HistoryChangedEventHandler(HistoryList_HistoryChanged);
            this.m_Owner.VisibleChanged += new EventHandler(m_Owner_VisibleChanged);
            this.m_Owner.Scroll += new ScrollEventHandler(m_Owner_Scroll);
            this.BuildBound();
        }
        void m_Owner_Scroll(object sender, ScrollEventArgs e)
        {
            this.BuildBound();
              }
        void m_Owner_VisibleChanged(object sender, EventArgs e)
        {
            this.BuildBound();
        }
        protected override void Dispose(bool disposing)
        {
            this.m_Owner.HistoryList.HistoryItemAdded -= new HistoryItemEventHandler(HistoryList_HistoryItemAdded);
            this.m_Owner.HistoryList.HistoryChanged -= new HistoryChangedEventHandler(HistoryList_HistoryChanged);
            base.Dispose(disposing);
        }
        void HistoryList_HistoryChanged(object o, HistoryChangedEventArgs e)
        {
            if (e == null)
                return;
            if ((e.Current == this.m_history) ||
                (e.Previous == this.m_history))
            {
                this.Refresh();
            }
            else
            {
                if (this.Enabled)
                {
                    this.ButtonState = enuButtonState.Normal;
                }
                else
                    this.ButtonState = enuButtonState.Disabled ;
            }
        }
        void HistoryList_HistoryItemAdded(object o, HistoryItemEventArgs e)
        {
            if (this.Enabled)
            {
                this.ButtonState = enuButtonState.Normal;
            }
            else
                this.ButtonState = enuButtonState.Disabled;
            this.Invalidate();
        }
        void _MouseLeave(object sender, EventArgs e)
        {
            if (this.Enabled)
            {
                this.ButtonState = enuButtonState.Normal;
            }
            else
                this.ButtonState = enuButtonState.Disabled;
        }
        void _MouseMove(object sender, MouseEventArgs e)
        {
            if (this.Enabled)
            {
                if (e.Button == MouseButtons.None)
                {
                    this.ButtonState = enuButtonState.Over;
                }
                else
                    this.ButtonState = enuButtonState.Down;
            }
            else
                this.ButtonState = enuButtonState.Disabled;
        }
        void XHistoryItemControl_MouseClick(object sender, MouseEventArgs e)
        {
            int i =Math.Abs ( m_history.Index - this.m_history.Owner.HistoryIndex);
            if (m_history.Index < this.m_history.Owner.HistoryIndex)
            {
                //call undo for de firrencte
                while (m_history.Index < this.m_history.Owner.HistoryIndex)
                {
                    this.m_history.Owner.Undo();
                }
            }
            else if(m_history.Index > this.m_history.Owner.HistoryIndex)
            {
               while (m_history.Index > this.m_history .Owner.HistoryIndex )
               {
                    this.m_history.Owner.Redo ();
               }
            }
        }
        void _SizeChanged(object sender, EventArgs e)
        {
            this.BuildBound();           
        }
        private void BuildBound()
        {
            if (this.m_Owner.Visible)
            {
                int w = (this.m_Owner .ScrollVisible) ? System.Windows.Forms.SystemInformation.VerticalScrollBarWidth : 0;
                this.Bounds = new Rectangle(0, this.m_history.Index * 16, this.Owner.Width - w, 16);
                this.Location = new Point(0, (SIZE * (this.m_history.Index - Owner.ScrollValue)));   
            }
        }
        void _Paint(object sender, PaintEventArgs e)
        {
            Draw(e.Graphics);
        }
    }
}

