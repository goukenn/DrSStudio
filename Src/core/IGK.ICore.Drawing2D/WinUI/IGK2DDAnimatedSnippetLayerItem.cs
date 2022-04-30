

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGK2DDAnimatedSnippetLayerItem.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Drawing2D;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:IGK2DDAnimatedSnippetLayerItem.cs
*/
using IGK.ICore.GraphicModels;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IGK.ICore.WinCore.WinUI;

namespace IGK.ICore.Drawing2D.WinUI
{
    /// <summary>
    /// Represent a snippet for layer item
    /// </summary>
    public class IGK2DDAnimatedSnippetLayerItem : ICoreSnippet
    {
        private int m_demand;
        private int m_index;
        private bool m_marked;
        private enuSnippetShape m_shape;
        private ICoreWorkingMecanism m_mecanism;
        private Vector2f m_location;
        private bool m_visible;
        private bool m_enabled;
        private bool m_isEditing;
        private ICoreWorkingRenderingSurface m_surface;
        private IGKD2DAnimatedSnippetLayer m_Layer;
        private Timer m_timer;
        private int m_inflate;
        public event MouseEventHandler SnippetBeginEdit;
        private CoreSnippetRenderProc m_renderProc;
        private int m_inflateSTATE;
        private const int INCREASE = 1;
        private const int DECREASE = 2;

        public Colorf Color { get; set; }
        public bool IsEditing
        {
            get
            {
                return this.m_isEditing;
            }
        }
        public bool Animate
        {
            get
            {
                if (this.m_timer != null)
                    return this.m_timer.Enabled;
                return false;
            }
            set
            {
                if ((this.m_timer != null) && (this.m_timer.Enabled != value))
                {
                    this.m_timer.Enabled = value;
                    this.Invalidate();
                }
            }
        }
        /// <summary>
        /// internal  get or set the client layer
        /// </summary>
        internal IGKD2DAnimatedSnippetLayer Layer
        {
            get { return m_Layer; }
            set
            {
                if (m_Layer != value)
                {
                    m_Layer = value;
                }
            }
        }
        public Rectanglef ClientRectangle
        {
            get
            {
                if (this.m_mecanism is ICoreSnippetMecanismObserver)
                {
                    return (this.m_mecanism as ICoreSnippetMecanismObserver).GetClientRectangle(this);
                }
                Rectanglef rc = new Rectanglef(this.Location.X, this.Location.Y, 0, 0);
                rc.Inflate(4, 4);
                return rc;
            }
        }
        public IGK2DDAnimatedSnippetLayerItem(ICoreWorkingRenderingSurface  surface, int index, 
            int demand,
            ICoreWorkingMecanism mecanism,
            CoreSnippetRenderProc proc = null)
        {
            this.m_surface = surface;
            this.m_index = index;
            this.m_demand = demand;
            this.m_mecanism = mecanism;
            this.m_visible = true;
            this.m_enabled = true;
            this.m_isEditing = false;
            this.m_shape = enuSnippetShape.Square;
            this.m_timer = new Timer();
            this.m_timer.Tick += new EventHandler(m_timer_Tick);
            this.m_timer.Interval = 20;
            this.m_renderProc = proc;
            this.Color = WinCoreControlRenderer.SnippetFillColor;
            //unregister mouse event
            this.m_surface.MouseLeave += new EventHandler(m_surface_MouseLeave);
            this.m_surface.MouseMove += m_surface_MouseMove;
            this.m_mecanism.SnippetChanged += m_mecanism_SnippetChanged;
            this.m_inflateSTATE = INCREASE;
        }
        private void Invalidate()
        {
            //no forcing for update
            this.m_surface.RefreshScene(true);
        }
        void m_timer_Tick(object sender, EventArgs e)
        {
            switch (this.m_inflateSTATE)
            {
                case DECREASE:
                    this.m_inflate -= 1;
                    if (this.m_inflate <= -3)
                    {
                        this.m_inflateSTATE = INCREASE;
                    }
                    break;
                case INCREASE:
                    this.m_inflate += 1;
                    if (this.m_inflate >= 3)
                    {
                        this.m_inflateSTATE = DECREASE;
                    }
                    break;
            }
            this.Invalidate();
        }
        void m_mecanism_SnippetChanged(object sender, EventArgs e)
        {
            if (((this.m_mecanism.Snippet != this) && this.Animate) || this.Animate)
            {
                this.Animate = false;
                this.InitDefault();
            }
            else
            {
                if ((this.Mecanism.Snippet == this) && !this.Animate)
                {
                    this.Animate = true;
                }
            }
        }
        private void InitDefault()
        {
            this.m_inflateSTATE = INCREASE;
            this.m_inflate = 0;
            this.Invalidate();
        }
        void m_surface_MouseMove(object sender, CoreMouseEventArgs e)
        {
            if (!this.Visible || this.IsEditing || (e.Button != enuMouseButtons.None))
                return;
            if (this.ClientRectangle.Contains(e.Location))
            {
                this.Mecanism.Snippet = this;
                //this.Mecanism.RegSnippets.Disabled();
            }
            else
            {
                if (this.Mecanism.Snippet == this)
                {
                    this.Mecanism.Snippet = null;
                }
            }
        }
        void m_surface_MouseLeave(object sender, EventArgs e)
        {
            if (this.m_mecanism.Snippet == this)
            {
                this.m_mecanism.Snippet = null;
            }
        }
        public ICoreWorkingMecanism Mecanism
        {
            get { return m_mecanism; }
        }
        public int Demand
        {
            get { return m_demand; }
        }
        public int Index
        {
            get { return m_index; }
        }
        public enuSnippetShape Shape
        {
            get
            {
                return this.m_shape;
            }
            set
            {
                this.m_shape = value;
            }
        }
        public Vector2f Location
        {
            get
            {
                return this.m_location;
            }
            set
            {
                if (!this.m_location.Equals(value))
                {
                    this.m_location = value;
                    this.m_surface.RefreshScene(false);
                 //   this.Invalidate();
                }
            }
        }
        public bool Visible
        {
            get
            {
                return this.m_visible;
            }
            set
            {
                this.m_visible = value;
            }
        }
        public bool Enabled
        {
            get
            {
                return this.m_enabled;
            }
            set
            {
                this.m_enabled = value;
            }
        }
   
        protected virtual void OnSnippetBeginEdit(MouseEventArgs e)
        {
            if (SnippetBeginEdit != null)
                this.SnippetBeginEdit(this, e);
        }
        public void Dispose()
        {
            //-----------------------
            //remove mose event
            //-----------------------
            this.m_surface.MouseLeave -= new EventHandler(m_surface_MouseLeave);
            this.m_surface.MouseMove -= m_surface_MouseMove;
            this.m_mecanism.SnippetChanged -= m_mecanism_SnippetChanged;

            if (this.m_timer != null)
            {
                this.m_timer.Tick -= m_timer_Tick;
                this.m_timer.Enabled = false;
                this.m_timer.Dispose();
                this.m_timer = null;
            }
            if (this.Layer != null)
                this.Layer.Remove(this);
        }
        /// <summary>
        /// render the snippet on the graphics surface
        /// </summary>
        /// <param name="g"></param>
        internal void Render(ICoreGraphics g)
        {
            if (this.m_renderProc != null)
            {
                this.m_renderProc(this, g, m_inflate);
                return;
            }
            Rectanglef v_rc = this.ClientRectangle;
            Brush v_br = CoreBrushRegisterManager.GetBrush<Brush>(this.Color);
            v_rc.Inflate(m_inflate, m_inflate);
            Rectanglei i = Rectanglei.Empty;
            if (v_rc.IsEmptyOrSizeNegative)
                return;
            switch (this.Shape)
            {
                case enuSnippetShape.Square:
                    g.FillRectangle(
                        v_br,
                        v_rc
                        );
                    v_rc.Inflate(-1, -1);
                    i  = Rectanglef.Round(v_rc);
                    g.DrawRectangle(
                        Colorf.White,
                        i.X , i.Y, i.Width, i.Height );
                    v_rc.Inflate(1, 1);
                    i = Rectanglef.Round(v_rc);
                  g.DrawRectangle (
                        WinCoreControlRenderer.SnippetBorderColor,
                           i.X, i.Y, i.Width, i.Height
                          );
                    break;
                case enuSnippetShape.Circle:
                    //v_rc.Inflate(-1, -1);
                    g.SmoothingMode = enuSmoothingMode.AntiAliazed;
                    g.FillEllipse(
                      Colorf.Black,
                      v_rc.X, v_rc.Y, v_rc.Width, v_rc.Height 
                      );
                    v_rc.Inflate(-1, -1);
                    g.FillEllipse(
                       WinCoreControlRenderer.SnippetFillColor,
                        v_rc.X, v_rc.Y, v_rc.Width, v_rc.Height );
                    g.SmoothingMode = enuSmoothingMode.None;
                    break;
                case enuSnippetShape.Diadmond:
                    Vector2f[] t = new Vector2f[] { 
                        v_rc .MiddleLeft ,
                        v_rc .MiddleTop ,
                        v_rc.MiddleRight ,
                        v_rc .MiddleBottom 
                    };
                    g.FillPolygon(WinCoreControlRenderer.SnippetBorderColor, t);
                    v_rc.Inflate(-2, -2);
                    t = new Vector2f[] { 
                        v_rc .MiddleLeft ,
                        v_rc .MiddleTop ,
                        v_rc.MiddleRight ,
                        v_rc .MiddleBottom 
                    };
                    g.FillPolygon(this.Color, t);
                    break;
                case enuSnippetShape.Custom:
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// get or set if this snippet is marked
        /// </summary>
        public bool Marked
        {
            get
            {
                return this.m_marked;
            }
            set
            {
                this.m_marked = value;
            }
        }
    }
}

