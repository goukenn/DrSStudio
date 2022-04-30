

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXGradientTrackBar.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿/* 
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
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using IGK.ICore.WinCore;
using IGK.ICore.Resources;
using IGK.ICore;
using IGK.ICore.WinCore.WinUI.Controls;

namespace IGK.DrSStudio.WinUI
{


    /// <summary>
    /// represent the gradient trackbar
    /// </summary>
    public class IGKXGradientTrackBar : IGKXControl
    {        
        int state = NONE;
        const int DEFAULT_COLOR_COUNT = 2;
        const int NONE = 0;
        const int MOVING_CURSOR = 1;
        private bool m_UnboundInnerColor;
        /// <summary>
        /// get or set the unbound Inner Color Mode
        /// </summary>
        public bool UnboundInnerColor
        {
            get { return m_UnboundInnerColor; }
            set
            {
                if (m_UnboundInnerColor != value)
                {
                    m_UnboundInnerColor = value;
                    OnUnboundInnerColorChange(EventArgs.Empty);
                }
            }
        }

        private void OnUnboundInnerColorChange(EventArgs eventArgs)
        {
            if (this.UnboundInnerColorChanged != null)
                this.UnboundInnerColorChanged(this, eventArgs);
        }

        private GradientCursor[] m_gradientCursor;
        private int m_selectedIndex;
        private static Bitmap sm_GradientBackroundImage;

        private Bitmap GradientBackroundImage
        {
            get
            {
                if (sm_GradientBackroundImage == null)
                {
                    sm_GradientBackroundImage = CoreResources.GetImage("dash") as Bitmap;
                }
                return sm_GradientBackroundImage;
            }
        }

        public Colorf StartColor
        {
            get
            {
                return this.m_gradientCursor[0].m_cursorColor;
            }
        }
        public Colorf EndColor
        {
            get
            {
                return this.m_gradientCursor[this.m_gradientCursor.Length - 1].m_cursorColor;
            }
        }
        /// <summary>
        /// graphics path to render
        /// </summary>
        private GraphicsPath m_path;
        private RectangleF m_barBounds;
        private enuGradientBarMode m_Mode;
        public Colorf[] Colors
        {
            get
            {
                switch (m_Mode)
                {

                    case enuGradientBarMode.DualBlendColor:
                        {
                            Colorf[] cls = new Colorf[2];
                            cls[0] = this.m_gradientCursor[0].m_cursorColor;
                            cls[1] = this.m_gradientCursor[this.m_gradientCursor.Length - 1].m_cursorColor;
                            return cls;
                        }
                    case enuGradientBarMode.MultiColor:
                    case enuGradientBarMode.DualColor:
                    default:
                        {

                            Colorf[] cls = new Colorf[this.m_gradientCursor.Length];
                            for (int i = 0; i < cls.Length; i++)
                            {
                                cls[i] = this.m_gradientCursor[i].m_cursorColor;
                            }

                            return cls;
                        }

                }
            }
        }
        public float[] Factors
        {
            get
            {
                float[] cls = new float[this.m_gradientCursor.Length];
                for (int i = 0; i < cls.Length; i++)
                {
                    cls[i] = this.m_gradientCursor[i].m_factor;
                }

                return cls;
            }
        }
        public float[] Positions
        {
            get
            {
                float[] cls = new float[this.m_gradientCursor.Length];
                for (int i = 0; i < cls.Length; i++)
                {
                    cls[i] = this.m_gradientCursor[i].m_position;
                }

                return cls;
            }
        }

        //events
        public event EventHandler GradientModeChanged;

        public enuGradientBarMode GradientMode
        {
            get
            {
                return m_Mode;
            }
            set
            {
                if (this.m_Mode != value)
                {
                    this.m_Mode = value;
                    OnGradientModeChanged(EventArgs.Empty);
                }
            }
        }

        private void OnGradientModeChanged(EventArgs eventArgs)
        {
            InitBars();
            this.Refresh();
            if (GradientModeChanged != null)
                this.GradientModeChanged(this, eventArgs);
        }
        public void ResetBar()
        {
            //OnGradientModeChanged(EventArgs.Empty);
            InitBars();
            this.Refresh();
        }

        private void InitBars()
        {
            this.m_selectedIndex = -1;
            m_gradientCursor = new GradientCursor[DEFAULT_COLOR_COUNT];

            m_gradientCursor[0] = new GradientCursor(this, 0);
            m_gradientCursor[0].m_cursorColor = Colorf.Black;
            m_gradientCursor[0].SetLocation(0.0f);
            m_gradientCursor[0].m_factor = 0.0f;
            m_gradientCursor[1] = new GradientCursor(this, 1);
            m_gradientCursor[1].m_cursorColor = Colorf.White;
            m_gradientCursor[1].SetLocation(1.0f);
            m_gradientCursor[1].m_factor = 1.0f;

            this.m_selectedIndex = 0;
        }

        /// <summary>
        /// raise when cursor is inserter, remove or moved
        /// </summary>
        public event EventHandler GradientCursorChanged;

        //insert cursor at position %
        void InsertCursor(float position)
        {
            if ((position <= 0.0f) || (position >= 1.0F))
                return;

            GradientCursor[] tmp = new GradientCursor[m_gradientCursor.Length + 1];
            int count = 0;
            for (int i = 0; i < this.m_gradientCursor.Length; i++)
            {
                tmp[count] = m_gradientCursor[i];
                if ((position > m_gradientCursor[i].m_position) && (position < m_gradientCursor[i + 1].m_position))
                {
                    tmp[count + 1] = new GradientCursor(this, count + 1);
                    tmp[count + 1].SetLocation(position);
                    tmp[count + 1].m_cursorColor = Colorf.Black;
                    count++;
                }
                else
                {
                    m_gradientCursor[i].index = count;
                }
                count++;
            }
            if (tmp.Length == count)
            {
                this.m_gradientCursor = tmp;
                OnGradienCursorChanged(EventArgs.Empty);

            }
            GC.Collect();
        }

        private void OnGradienCursorChanged(EventArgs eventArgs)
        {
            if (this.GradientCursorChanged != null)
            {
                this.GradientCursorChanged(this, eventArgs);
            }
        }

        public void RemoveCursor(int index)
        {
            if ((index == 0) || (index >= (this.m_gradientCursor.Length - 1)))
                return;
            GradientCursor[] tmp = new GradientCursor[m_gradientCursor.Length - 1];
            int count = 0;
            for (int i = 0; i < this.m_gradientCursor.Length; i++)
            {
                if (i == index)
                    continue;
                tmp[count] = m_gradientCursor[i];
                tmp[count].index = count;
                count++;
            }
            this.m_gradientCursor = tmp;
            this.OnGradienCursorChanged(EventArgs.Empty);
        }
        public void ClearCursors()
        {
            InitBars();
            this.Refresh();
        }


        public event EventHandler SelectedColorIndexChanged;
        public event EventHandler UnboundInnerColorChanged;
        /// <summary>
        /// get or set the current selected element
        /// </summary>
        public int SelectedColorIndex
        {
            get
            {
                return m_selectedIndex;
            }
            set
            {
                if ((value == -1) || ((value >= 0) && (value < this.m_gradientCursor.Length)))
                {
                    m_selectedIndex = value;
                    OnSelectedColorIndexChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// gets or sets the colors
        /// </summary>
        public Colorf SelectedColor
        {
            get
            {
                if ((this.SelectedColorIndex >= 0) && (this.SelectedColorIndex < this.m_gradientCursor.Length))
                    return this.m_gradientCursor[this.SelectedColorIndex].m_cursorColor;
                return Colorf.Empty;


            }
            set
            {
                if ((this.SelectedColorIndex >= 0) && (this.SelectedColorIndex < this.m_gradientCursor.Length))
                {
                    this.m_gradientCursor[SelectedColorIndex].m_cursorColor = value;
                    this.SuspendLayout();
                    this.Refresh();
                    this.ResumeLayout();
                    OnSelectedColorChanged(EventArgs.Empty);
                }
            }
        }

        public event EventHandler SelectedColorChanged;

        private void OnSelectedColorChanged(EventArgs eventArgs)
        {
            if (SelectedColorChanged != null)
                SelectedColorChanged(this, eventArgs);
        }

        private void OnSelectedColorIndexChanged(EventArgs eventArgs)
        {
            this.Refresh();
            if (this.SelectedColorIndexChanged != null)
                this.SelectedColorIndexChanged(this, eventArgs);
        }
        protected override Size DefaultSize
        {
            get
            {
                return new Size(100, 25);
            }
        }
        protected override Size DefaultMinimumSize
        {
            get
            {
                return new Size(100, 25);
            }
        }

        GradientCursor[] GradientCursors
        {
            get
            {
                return m_gradientCursor;
            }
        }
        //.ctr
        public IGKXGradientTrackBar()
        {
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
            this.m_UnboundInnerColor = false;
            InitBars();
            GeneratePath();
            GenerateCursor();
            this.UnboundInnerColorChanged += XGradientTrackBar_UnboundInnerColorChanged;
        }

        void XGradientTrackBar_UnboundInnerColorChanged(object sender, EventArgs e)
        {
            if (this.UnboundInnerColor == false)
            {
                for (int i = 0; i < this.m_gradientCursor.Length - 1; i++)
                {
                    if (this.m_gradientCursor[i].m_factor > this.m_gradientCursor[i + 1].m_factor)
                    {
                        this.m_gradientCursor[i + 1].m_factor = this.m_gradientCursor[i].m_factor;
                    }
                }
            }
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            GeneratePath();
            GenerateCursor();
            base.OnSizeChanged(e);
        }

        private void GenerateCursor()
        {
            GradientCursor[] t = this.GradientCursors;

            for (int i = 0; i < t.Length; i++)
            {
                if (t[i] != null)
                    t[i].Refresh();
            }
        }

        void SetPath(GraphicsPath path)
        {
            if (m_path != null)
                m_path.Dispose();
            m_path = path;
        }
        void GeneratePath()
        {
            Rectangle vrec = this.ClientRectangle;
            vrec.X += 10;// 5;
            vrec.Y++;
            vrec.Width -= 5;//5
            vrec.Height -= 10;//3;


            //p.AddArc(vrec.X, 0, 5, 5, 180, 90);

            //p.AddArc(vrec.Width - 5, 0, 5, 5, -90, 90);
            ////note for small
            //p.AddArc(vrec.Width - 6, vrec.Height - 6 - 12, 6, 6, 0, 90);
            //p.AddArc(vrec.X, vrec.Height - 5 - 12, 5, 5, 90, 90);
            //p.CloseFigure();

            vrec.Width -= 10;
            vrec.Height -= 12;//12
            this.m_barBounds = vrec;
            GraphicsPath p = new GraphicsPath();
            p.AddRectangle(m_barBounds);
            SetPath(p);
        }
        protected override void OnPaint(PaintEventArgs e)
        {

            base.OnPaint(e);
            Drawbar(e);
            Drawcursor(e);
        }

        void Drawcursor(PaintEventArgs e)
        {
            GradientCursor[] t = this.GradientCursors;

            for (int i = 0; i < t.Length; i++)
            {
                if (t[i] != null)
                    t[i].Draw(e.Graphics);
            }
        }

        void Drawbar(PaintEventArgs e)
        {

            if (m_path != null)
            {
                Region r = new Region(m_path);
                RectangleF v_rc = r.GetBounds(e.Graphics);
                Bitmap bmp = GradientBackroundImage;

                using (LinearGradientBrush br = 
                    WinCoreBrushRegister.CreateBrush <LinearGradientBrush>(
                    v_rc.X, v_rc.Y, v_rc.Width ,v_rc .Height,
                    StartColor, EndColor, 0.0f))
                {
                    if (this.GradientMode == enuGradientBarMode.MultiColor)
                    {
                        ColorBlend bl = new ColorBlend();
                        bl.Positions = this.Positions;
                        bl.Colors = this.Colors.CoreConvertTo<Color[]>();
                        br.InterpolationColors = bl;
                    }
                    else if (this.GradientMode == enuGradientBarMode.DualBlendColor)
                    {
                        Blend l = new Blend();
                        l.Factors = this.Factors;
                        l.Positions = this.Positions;
                        br.Blend = l;
                    }

                    //         br.LinearColors = this.Colors;


                    if (bmp != null)
                    {
                        using (TextureBrush brt = new TextureBrush(bmp))
                        {
                            e.Graphics.FillRegion(brt, r);
                        }
                    }
                    else
                        e.Graphics.FillRegion(Brushes.White, r);


                    if (this.Enabled)
                    {
                        e.Graphics.FillRegion(br, r);
                    }
                    else
                    {
                        e.Graphics.FillRegion(SystemBrushes.InactiveBorder, r);
                    }
                }
                e.Graphics.DrawPath(Pens.Black, m_path);
                r.Dispose();
            }

        }
        bool CheckContains(Point location, ref int index)
        {
            GradientCursor[] t = this.GradientCursors;
            for (int i = 0; i < t.Length; i++)
            {
                if ((t[i] != null) && t[i].Containts(location))
                {
                    index = i;
                    return true;
                }
            }
            return false;
        }

        private void ChangedMode()
        {
            switch (this.m_Mode)
            {
                case enuGradientBarMode.DualColor:
                    this.GradientMode = enuGradientBarMode.DualBlendColor;
                    break;
                case enuGradientBarMode.DualBlendColor:
                    this.GradientMode = enuGradientBarMode.MultiColor;
                    break;
                case enuGradientBarMode.MultiColor:
                    this.GradientMode = enuGradientBarMode.DualColor;
                    break;
            }
        }
        //mouse down click
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            int i = 0;
            switch (e.Button)
            {
                case MouseButtons.Left:
                    i = 0;
                    if (state == NONE)
                    {
                        if (this.CheckContains(e.Location, ref i))
                        {
                            //change de selection
                            this.SelectedColorIndex = i;
                            this.Invalidate();
                        }
                        else
                        {
                            if (this.m_Mode != enuGradientBarMode.DualColor)
                            {
                                //insère un nouveau curseur
                                this.InsertCursor(e.X / m_barBounds.Width);
                                this.Invalidate();
                            }
                        }
                    }
                    else
                    {
                        this.OnSelectedColorIndexChanged(EventArgs.Empty);
                        state = NONE;
                    }
                    break;
                case MouseButtons.Right:
                    {
                        //supriméer un onglet
                        i = 0;
                        if (this.CheckContains(e.Location, ref i))
                        {
                            //change de selection
                            if ((i != 0) && (i != (this.m_gradientCursor.Length - 1)))
                            {
                                this.RemoveCursor(i);
                            }
                            this.Invalidate();
                        }
                    }
                    break;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            switch (e.Button)
            {
                case MouseButtons.Left:

                    int i = 0;
                    if (CheckContains(e.Location, ref i))
                    {

                        //begin move
                        if ((i > 0) && (i < (this.m_gradientCursor.Length - 1)))
                        {
                            state = MOVING_CURSOR;
                            this.m_selectedIndex = i;
                        }
                        else
                            this.m_selectedIndex = -1;
                    }
                    else
                        this.m_selectedIndex = -1;
                    break;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if ((this.m_selectedIndex != -1) && (state == MOVING_CURSOR))
                    {
                        int s = this.m_selectedIndex;
                        //on a selectionner un curseur
                        float f = (e.Location.X - this.m_barBounds.X) / this.m_barBounds.Width;
                        if (this.UnboundInnerColor)
                        {
                        }
                        else
                        {
                            if (f < this.m_gradientCursor[s - 1].m_position)
                                f = this.m_gradientCursor[s - 1].m_position;
                            else if (f > this.m_gradientCursor[s + 1].m_position)
                                f = this.m_gradientCursor[s + 1].m_position;
                        }

                        this.m_gradientCursor[s].SetLocation(f);
                        this.OnGradienCursorChanged(EventArgs.Empty);
                        this.Refresh();
                    }
                    break;
                case MouseButtons.None:
                    int i = 0;
                    if (CheckContains(e.Location, ref i))
                    {
                        this.Cursor = Cursors.Hand;
                    }
                    else
                        this.Cursor = Cursors.Default;
                    break;
            }
        }

        public void Configure(float[] factors, float[] Positions)
        {
            if ((factors == null) || (Positions == null))
                return;
            if (factors.Length == Positions.Length)
            {
                m_gradientCursor = new GradientCursor[factors.Length];

                for (int i = 0; i < factors.Length; i++)
                {
                    m_gradientCursor[i] = new GradientCursor(this, i);
                    m_gradientCursor[i].SetLocation(Positions[i]);
                    m_gradientCursor[i].m_factor = factors[i];

                }
            }
            this.Refresh();
        }

        public void Configure(enuGradientBarMode mode, Colorf[] color, float[] factors, float[] positions)
        {
            m_Mode = mode;

            switch (m_Mode)
            {
                case enuGradientBarMode.DualColor:
                    if (color.Length == 2)
                    {
                        InitBars();
                        m_gradientCursor[0].m_cursorColor = color[0];
                        m_gradientCursor[1].m_cursorColor = color[1];
                    }
                    break;
                case enuGradientBarMode.DualBlendColor:
                    if ((color.Length == 2) && (factors.Length == positions.Length) && (factors.Length >= 2))
                    {
                        m_gradientCursor = new GradientCursor[factors.Length];
                        for (int i = 0; i < factors.Length; i++)
                        {
                            m_gradientCursor[i] = new GradientCursor(this, i);
                            if (i == 0)
                                m_gradientCursor[i].m_cursorColor = color[0];
                            if ((i + 1) == factors.Length)
                                m_gradientCursor[i].m_cursorColor = color[1];
                            m_gradientCursor[i].SetLocation(positions[i]);
                            m_gradientCursor[i].m_factor = factors[i];
                        }
                    }
                    break;
                case enuGradientBarMode.MultiColor:
                    if ((positions.Length >= 2) && (color.Length == positions.Length))
                    {
                        m_gradientCursor = new GradientCursor[positions.Length];
                        for (int i = 0; i < m_gradientCursor.Length; i++)
                        {
                            m_gradientCursor[i] = new GradientCursor(this, i);

                            m_gradientCursor[i].SetLocation(positions[i]);
                            m_gradientCursor[i].m_factor = 0.0f;
                            m_gradientCursor[i].m_cursorColor = color[i];

                        }
                    }
                    break;
            }
            this.Refresh();
        }

        public void SetColor(int index, Colorf color)
        {
            if ((index >= 0) && (index < this.m_gradientCursor.Length))
            {
                int old = this.m_selectedIndex;
                this.m_selectedIndex = index;
                SetColor(color);
                this.m_selectedIndex = old;
            }
        }

        public void SetColor(Colorf color)
        {
            switch (m_Mode)
            {
                case enuGradientBarMode.DualColor:
                    if ((this.SelectedColorIndex == 0) || (this.SelectedColorIndex == 1))
                    {
                        SelectedColor = color;
                    }
                    break;
                case enuGradientBarMode.MultiColor:
                    if ((this.SelectedColorIndex >= 0) || (this.SelectedColorIndex < (this.m_gradientCursor.Length - 1)))
                    {
                        SelectedColor = color;
                    }
                    break;
                case enuGradientBarMode.DualBlendColor:
                    SelectedColor = color;

                    break;
            }
        }

        public void SetFactor(float p)
        {
            if ((this.GradientMode == enuGradientBarMode.DualBlendColor) && (this.SelectedColorIndex != -1))
            {
                this.m_gradientCursor[this.SelectedColorIndex].m_factor = p;
                this.Refresh();
            }
        }

        /// <summary>
        /// represent the gradient cursor
        /// </summary>
        public class GradientCursor : IDisposable
        {
            internal IGKXGradientTrackBar m_owner;
            internal GraphicsPath m_path;
            internal int index;
            internal float m_position;
            internal float m_factor;
            internal PointF m_location;
            internal Colorf m_cursorColor;

            public GradientCursor(IGKXGradientTrackBar owner, int index)
            {
                this.m_owner = owner;
                this.index = index;
                GeneratePath();
            }

            void DisposePath()
            {
                if (m_path != null)
                {
                    m_path.Dispose();
                    m_path = null;
                }
            }
            //position en pourcentage de la bar
            public void SetLocation(float percent)
            {
                m_position = percent;
                GeneratePath();
            }
            public void Refresh()
            {
                GeneratePath();
            }
            private void GeneratePath()
            {
                RectangleF vr = this.m_owner.m_barBounds;
                //float v_x = (m_position * (vr.Width - 1));
                this.m_location =
                    new PointF(vr.X + (m_position * (vr.Width - 1)),
                    vr.Y + vr.Height);

                DisposePath();
                m_path = new GraphicsPath();
                m_path.AddPolygon(new PointF[] { 
                    new PointF(m_location.X ,m_location.Y ),
                    new PointF(m_location.X+6,m_location.Y +8),
                    new PointF(m_location.X-6,m_location.Y +8),
                });
            }

            public void SetColor(Colorf color)
            {
                this.m_cursorColor = color;
                this.m_owner.Refresh();
            }

            public void Draw(Graphics g)
            {
                if (m_path == null)
                    return;
                GraphicsState s = g.Save();
                Region r = new Region(this.m_path);
                RectangleF rc = this.m_path.GetBounds();
                g.SmoothingMode = SmoothingMode.None;
                g.FillRegion(WinCoreBrushRegister.GetBrush(m_cursorColor), r);
                
                r.Dispose();

                if (index == this.m_owner.SelectedColorIndex)
                    g.DrawPath(Pens.Red, m_path);
                else
                    g.DrawPath(Pens.Black, m_path);

                rc.Width = 12;
                rc.Height = 12;
                rc.Y += 10;
                g.FillRectangle(Brushes.DarkGray, rc);
                rc.Inflate(-2, -2);
                g.FillRectangle(CoreBrushRegisterManager.GetBrush<Brush>(this.m_cursorColor),
                    rc.X , rc.Y , rc.Width , rc.Height );
                g.Restore(s);

            }

            public bool Containts(PointF point)
            {
                if (m_path == null) return false;
                return m_path.IsVisible(point);
            }

            #region IDisposable Members

            public void Dispose()
            {
                DisposePath();
            }

            #endregion
        }


    }
}
