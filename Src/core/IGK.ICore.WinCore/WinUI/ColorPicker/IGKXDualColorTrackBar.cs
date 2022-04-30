

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXDualColorTrackBar.cs
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
file:IGKXDualColorTrackBar.cs
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
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using IGK.ICore.WinCore;
using IGK.ICore.GraphicModels;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore;
using IGK.ICore.WinCore.WinUI.Controls;

namespace IGK.ICore.WinUI
{

    /// <summary>
    /// represent a dual color tracking value
    /// </summary>
    [DesignerAttribute(
        WinCoreConstant.CTRL_DESIGNER ,        
        typeof(ControlDesigner) )]
    public class IGKXDualColorTrackBar : IGKXControl 
    {
        private float minvalue;
        private float maxvalue;
        private float value;
        private Colorf m_BorderColor;
        private XCursor cursor;
        private LinearGradientBrush m_brush;
        private float step;
        [Description("gets or set the min value")]
        public float MinValue { get { return minvalue; } set { minvalue = value; this.OnValueChanged(EventArgs.Empty); } }
        [Description("gets or set the max value")]
        public float MaxValue { get { return maxvalue; } set { maxvalue = value; this.OnValueChanged(EventArgs.Empty); } }
        [Description("gets or set the value")]
        public float Value
        {
            get { return value; }
            set
            {
                if (this.value == value)
                    return;
                if (value < minvalue)
                    this.value = minvalue;
                else if (value > maxvalue)
                    this.value = maxvalue;
                else
                {
                    this.value = value;
                }
                if (Math.Abs((this.value - (int)this.value)) > 0.0f)
                {
                    this.value = (float)Math.Ceiling(value);
                }
                OnValueChanged(EventArgs.Empty);
            }
        }
        public override ISite Site
        {
            get
            {
                return base.Site;
            }
            set
            {              
                base.Site = value;
            }
        }
        [Browsable(false)]
        [Description("gets or set thes two colors")]
        public Colorf[] Colors { get { return new Colorf[] { m_startColor, m_endColor }; } }
        private Colorf m_startColor;
        private Colorf m_endColor;
        [Category ("Color")]
        [Browsable(true)]
        public Colorf StartColor { get { return m_startColor; } set { m_startColor = value; GenerateBrush(); this.Invalidate(); } }
        [Category("Color")]
        [Browsable(true)]
        public Colorf EndColor { get { return m_endColor; } set { m_endColor = value; GenerateBrush(); this.Invalidate(); } }
        
        
        [Category("Color")]
        [Browsable (true  )]
        /// <summary>
        /// get or set the border color
        /// </summary>
        public Colorf BorderColor
        {
            get { return m_BorderColor; }
            set
            {
                if (m_BorderColor != value)
                {
                    m_BorderColor = value;
                    Invalidate();
                }
            }
        }
        public float Step { get { return step; } set { step = value; } }

        public bool DrawDash { get; set; }
        

        //event
        public event EventHandler ValueChanged;
        protected override void Dispose(bool disposing)
        {
            if (this.Disposing)
            {
                if (this.m_brush != null)
                {
                    this.m_brush.Dispose();
                    this.m_brush = null;
                }
            }
            base.Dispose(disposing);
        }
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            this.Refresh();
        }
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            this.Refresh();
        }
        protected virtual void OnValueChanged(EventArgs e)
        {
            this.cursor.GetPos();
            this.Refresh ();//.Invalidate();
            if (ValueChanged != null)
            {
                ValueChanged(this, e);
            }
        }
        public IGKXDualColorTrackBar()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.BackColor = Color.Transparent;
            this.cursor = new XCursor(this);
            this.m_startColor = Colorf.Black;
            this.m_endColor = Colorf.White;
            minvalue = 0;
            maxvalue = 255;
            value = 255;
            step = 1;
            this.m_BorderColor = Colorf.White;
            this.Size = new Size(120, 24);
            this.Cursor = Cursors.Hand;
            this.cursor.GetPos();
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            GenerateBrush();
            cursor.GetPos();
            base.OnSizeChanged(e);
        }
        protected void GenerateBrush()
        {
            if (m_brush != null)
            {
                m_brush.Dispose();
                m_brush = null;
            }
            if (this.Width > 0)
            {
                m_brush = WinCoreBrushRegister.CreateBrush(
                        this.ClientRectangle, 
                        this.m_startColor,
                        this.m_endColor, 
                        0.0f);
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            if (DesignMode) {
                //draw rectangle
                //ControlPaint.FillReversibleRectangle(this.Bounds, Color.Red);
                ControlPaint.DrawBorder(e.Graphics,
                    new Rectangle(Point.Empty , this.Size), Color.Black, ButtonBorderStyle.Solid);
                return;
            }
            base.OnPaint(e);
        }
        protected override void OnCorePaint(CorePaintEventArgs e)
        {
            DrawBar(e);
            base.OnCorePaint(e);
        }
        protected virtual void DrawBar(CorePaintEventArgs e)
        {
            Rectangle v_rc = this.ClientRectangle;

            Object v_state = e.Graphics.Save();
            e.Graphics.Clear(CoreRenderer.BackgroundColor);
            e.Graphics.SmoothingMode = enuSmoothingMode.AntiAliazed;
            e.Graphics.PixelOffsetMode = enuPixelOffset.None;

            if (this.DrawDash)
            {
                
                if (m_endColor.A < 1.0f) {
                    var br =
                        WinCoreBrushes.GetBrushes(WinCoreBrushesNames.DASH);
                    if (br != null)
                    {
                        e.Graphics.FillRectangle(br,
                        0, 0, this.Width - 1, 
                        this.Height - 1);
                    }


                }

            }

            //e.Graphics.Clear(CoreRenderer.BackgroundColor);
            //e.Graphics.SmoothingMode = enuSmoothingMode.AntiAliazed;
            //e.Graphics.PixelOffsetMode = enuPixelOffset.None;
            //  WinCoreControlRenderer.DrawDash(e.Graphics, new Rectanglei(0, 0, this.Width, this.Height));
            if (this.Enabled)
            {
                if (m_brush != null)
                {
                    e.Graphics.FillRectangle(m_brush, v_rc);
                }
            }
            else
            {
                e.Graphics.FillRectangle(SystemBrushes.InactiveBorder, v_rc);
            }
            e.Graphics.DrawRectangle(Colorf.FromFloat(0.7f), v_rc.X, v_rc.Y, v_rc.Width - 1, v_rc.Height - 1);
            e.Graphics.Restore(v_state);
            //}
            //else {
            //    e.Graphics.DrawRectangle(Colorf.FromFloat(0.7f), v_rc.X, v_rc.Y, v_rc.Width - 1, v_rc.Height - 1);
            //}
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (this.Focused == false)
                        this.Focus();
                    //if (cursor.Contains(e.Location))
                    beginMoveCursor(e.Location);
                    break;
            }
            base.OnMouseDown(e);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Left) && (cursorMoveFlag))
                updatecursor(e.Location);
            base.OnMouseMove(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Left) && (cursorMoveFlag))
                endMoveCursor(e.Location);
            base.OnMouseUp(e);
        }
        bool cursorMoveFlag;
        internal void beginMoveCursor(Point point)
        {
            cursorMoveFlag = true;
            updatecursor(point);
        }
        private void updatecursor(Point point)
        {
            float x = point.X - 6;
            float t = maxvalue - minvalue;
            if (x < 0.0f)
                x = 0;
            else
            {
                if (x > (Width - 12))
                    x = Width - 12;
            }
            float v = ((x / (float)(this.Width - 12)) * t) + minvalue;
            if (v < minvalue)
                v = minvalue;
            else if (v > maxvalue)
                v = maxvalue;
            this.Value = v;
        }
        protected override bool IsInputChar(char charCode)
        {
            return base.IsInputChar(charCode);
        }
        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                    return true;
                default:
                    break;
            }
            return base.IsInputKey(keyData);
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (!this.Focused)
            {
                return;
            }
            base.OnKeyDown(e);
            switch (e.KeyData)
            {
                case Keys.Left:
                case Keys.Down:
                    if (Value > (MinValue + step))
                    {
                        this.Value -= step;
                    }
                    else
                    {
                        this.Value = MinValue;
                    }
                    break;
                case Keys.Right:
                case Keys.Up:
                    if (Value < (MaxValue - step))
                    {
                        this.Value += step;
                    }
                    else
                        this.Value = MaxValue;
                    break;
            }
        }
        private void endMoveCursor(Point point)
        {
            updatecursor(point);
            cursorMoveFlag = false;
        }
        /// <summary>
        /// Represent the cursor documument of this tranc baal
        /// </summary>
        class XCursor 
        {
            IGKXDualColorTrackBar owner;
            RectangleF location;
            private Rectangle m_Bounds;
            public Rectangle Bounds
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
            //protected override CreateParams CreateParams
            //{
            //    get
            //    {
            //        CreateParams p = base.CreateParams;
            //        p.Style |= 0x4000000 << 1;
            //        return p;
            //    }
            //}
            public XCursor(IGKXDualColorTrackBar owner)
                : base()
            {
                this.owner = owner;
                this.owner.MouseDown += owner_MouseDown;
                this.owner.Paint += owner_Paint;
                //this.Parent = owner;
                //this.SetStyle(ControlStyles.Selectable, false);
                //this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                //this.SetStyle(ControlStyles.ResizeRedraw, true);
                //this.Show();
            }
            void owner_Paint(object sender, CorePaintEventArgs e)
            {
                this.Render(e.Graphics);
            }
            void owner_MouseDown(object sender, CoreMouseEventArgs e)
            {
                if (e.Button == enuMouseButtons.Left)
                    this.owner.beginMoveCursor(new Point((int)
                        this.location.X,
                        (int)this.location.Y));
            }
            //protected override void OnPaint(PaintEventArgs e)
            //{
            //    base.OnPaint(e);
            //    Render(e.Graphics);
            //}
            //protected override void OnMouseDown(MouseEventArgs e)
            //{
            //    base.OnMouseDown(e);
            //    this.owner.Capture = true;
            //}
            private void Render(ICoreGraphics graphics)
            {
                object  v_st = graphics.Save();
                graphics.SmoothingMode = enuSmoothingMode.None ;
                graphics.PixelOffsetMode = enuPixelOffset.Half;
                Rectangle r = this.Bounds;
                using (Brush br = new SolidBrush(Color.FromArgb(100, Color.White)))
                {
                    graphics.FillRectangle(br, r);
                }
                //ControlPaint.DrawBorder(
                //    graphics,
                //r,
                //Color.Black,
                //ButtonBorderStyle .Solid );
                if (owner.Focused)
                {
                    r.Inflate(1, 1);
                   // graphics.DrawRectangle(Pens.White, r.X, r.Y, r.Width, r.Height);
                }
                graphics.Restore(v_st);
            }
            private Vector2f GetCursorPos()
            {
                float t = owner.maxvalue - owner.minvalue;
                float x = 0;
                if (t < 0)
                    return Vector2f.Zero;
                x = 6 + (((owner.Width - 12) * (owner.Value - owner.MinValue)) / t);
                return new Vector2f(x, 1);
            }
            internal void GetPos()
            {
                Vector2f pos = GetCursorPos();
                Rectangle vr = new Rectangle((int)pos.X ,(int) pos.Y , 0,0);
                vr.X -= 4;
                vr.Width = 4;
                vr.Height = owner.Height - 1;
                this.location = vr;
                this.Bounds = vr;
            }
            internal bool Contains(Point point)
            {
                return location.Contains(point);
            }
        }
    }
}

