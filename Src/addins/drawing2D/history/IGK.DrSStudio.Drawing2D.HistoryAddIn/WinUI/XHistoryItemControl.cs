

/*
IGKDEV @ 2008-2016
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
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.History;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
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
    
    using IGK.ICore.Resources;
    using IGK.ICore.GraphicModels;
    using IGK.ICore.WinCore.WinUI.Controls;

    class XHistoryItemControl : IGKXControl
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
        /// <summary>
        /// get the history item
        /// </summary>
        public IHistoryAction History {
            get {
                return this.m_history;
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

        public void Draw(ICoreGraphics g)
        {
            Rectangle v_rc = this.ClientRectangle;
            Colorf v_fcolor = HistoryRenderer.HistoryForeColor;
            Colorf v_bgcolor = HistoryRenderer.HistoryBackgroundColor;
            Colorf v_scolor = HistoryRenderer.HistoryBackgroundStartColor;
            Colorf v_ecolor = HistoryRenderer.HistoryBackgroundEndColor;

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
                        case enuButtonState.Hover :
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
                v_scolor = Colorf.Gray;
                v_ecolor = Colorf.Gray;
            }

            using (LinearGradientBrush br = WinCoreBrushRegister.CreateBrush(this.ClientRectangle, v_scolor, v_ecolor, 90.0f))
            {
                    g.FillRectangle(br, this.ClientRectangle);
           }
           
          
            ICore2DDrawingDocument  m_document = CoreResources.GetDocument(this.m_history.ImgKey);
            if (m_document != null)
            {
                m_document.Draw(g, false, new Rectanglei(0, 0, 16, SIZE), enuFlipMode.None);
            }
            StringFormat sf = new StringFormat();
            sf.Trimming = StringTrimming.EllipsisCharacter;
            g.DrawString(this.Info , this.Font,
                WinCoreBrushRegister.GetBrush (v_fcolor),
                new Rectanglef(16, 0, this.Width, SIZE ),
                sf);
            sf.Dispose();
            //ControlPaint.DrawBorder(g, this.ClientRectangle,
            //    HistoryRenderer.HistoryBorderColor ,
            //    ButtonBorderStyle.Solid);
                
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
            this.Paint += _Paint;
            this.MouseClick += _MouseClick;
            this.MouseMove +=_MouseMove;
            this.MouseLeave += _MouseLeave;
            this.m_Owner.HistoryList.HistoryItemAdded += new HistoryItemEventHandler(HistoryList_HistoryItemAdded);
            this.m_Owner.HistoryList.HistoryChanged += new HistoryChangedEventHandler(HistoryList_HistoryChanged);
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
                this.Invalidate();
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
   private void _MouseMove(object sender, CoreMouseEventArgs e)
        {
           
            if (this.Enabled)
            {
                if (e.Button == enuMouseButtons.None)
                {
                    this.ButtonState = enuButtonState.Hover;
                }
                else
                    this.ButtonState = enuButtonState.Down;
            }
            else
                this.ButtonState = enuButtonState.Disabled;
            
        }
     

        private void _MouseClick(object sender, CoreMouseEventArgs e)
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

         private void _Paint(object sender, CorePaintEventArgs e)
        {
          
            Draw(e.Graphics);
        }
    }
}
