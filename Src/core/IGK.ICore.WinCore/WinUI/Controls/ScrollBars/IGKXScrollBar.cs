

using IGK.ICore.WinCore;
using IGK.ICore;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXScrollBar.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.WinCore.WinUI.Controls
{
    public class IGKXScrollBar : IGKXControl
    {
        private int m_Value;
        private int m_MinValue;
        private int m_MaxValue;
        private IGKXButton c_prev;
        private IGKXButton c_next;
        private IGKXScrollBody c_barValue;

        public int MaxValue
        {
            get { return m_MaxValue; }
            set
            {
                if (m_MaxValue != value)
                {
                    m_MaxValue = value;
                    this.InitBounds();
                }
            }
        }
        public int MinValue
        {
            get { return m_MinValue; }
            set
            {
                if (m_MinValue != value)
                {
                    m_MinValue = value;
                    this.InitBounds();
                }
            }
        }
        public int Value
        {
            get { return m_Value; }
            set
            {
                if ((m_Value != value) && (value >= MinValue) && (value <= MaxValue ))
                {
                    m_Value = value;
                    this.InitBounds();
                    this.Refresh();
                    OnValueChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler ValueChanged;

        private bool m_StartBody;

        public bool StartBody
        {
            get { return m_StartBody; }
            set
            {
                if (m_StartBody != value)
                {
                    m_StartBody = value;
                }
            }
        }
        ///<summary>
        ///raise the ValueChanged 
        ///</summary>
        protected virtual void OnValueChanged(EventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged(this, e);
        }

        protected IGKXButton PrevButton { get { return c_prev; } }
        protected IGKXButton NextButton { get { return c_next; } }
        protected IGKXScrollBody BodyButton { get { return c_barValue;  } }


        protected class IGKXScrollBody : IGKXControl
        {
            public IGKXScrollBody()
            {
                this.SetStyle (System.Windows.Forms.ControlStyles.SupportsTransparentBackColor , true);
                this.BackColor = System.Drawing.Color.Transparent;
                this.Paint += _Paint;
            }

            void _Paint(object sender, CorePaintEventArgs e)
            {
                Rectanglef rc = new Rectanglef(0, 0, this.Width, this.Height);
                rc.Inflate(-4, -4);
                e.Graphics.FillRectangle(WinCoreControlRenderer.ScrollbarBodyBackgroundColor,
                    rc);
            }
        }
        public IGKXScrollBar()
        {
            this.c_next = new IGKXButton();
            this.c_prev = new IGKXButton();
            this.c_barValue = new IGKXScrollBody();
            this.Paint += _Paint;
            this.SizeChanged += IGKXScrollBar_SizeChanged;
            this.c_barValue.MouseDown += c_body_MouseDown;
            this.MouseMove += IGKXScrollBar_MouseMove;
            this.Controls.Add(c_next);
            this.Controls.Add(c_prev);
            this.Controls.Add(c_barValue);
        }

        void IGKXScrollBar_MouseMove(object sender, CoreMouseEventArgs e)
        {
            if (this.Capture && this.StartBody && (e.Button == enuMouseButtons.Left))
            {
                UpdateValue(e.Location);
                this.Refresh();
            }
        }

        protected virtual void UpdateValue(Vector2i mouseLocation)
        {
            
        }

        void c_body_MouseDown(object sender, CoreMouseEventArgs e)
        {
            if (e.Button == enuMouseButtons.Left)
            {
                this.m_StartBody = true;
                this.Capture = true;
            }
        }

        void IGKXScrollBar_SizeChanged(object sender, EventArgs e)
        {
            this.InitBounds();
        }
        /// <summary>
        /// init display bounds
        /// </summary>
        protected virtual void InitBounds()
        {
            //init item bound
        }

        void _Paint(object sender, CorePaintEventArgs e)
        {
            e.Graphics.Clear(WinCoreControlRenderer.ScrollbarBackgroundColor);
        }

    }
}
