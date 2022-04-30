

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: LineElement.cs
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
file:LineElement.cs
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
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore;using IGK.DrSStudio.Drawing2D;
    using IGK ;
    using IGK.DrSStudio.Codec ;
    using IGK.DrSStudio.Drawing2D.Codec ;
    [Core2DDrawingStandardItem("Line", 
        typeof (Mecanism),
        Keys= Keys.L)]
    public class LineElement :
        Core2DDrawingLayeredElement,
        ICore2DLineElement ,
        ICore2DBrushOwner
    {
        private Vector2f m_startPoint;
        private Vector2f m_endPoint;
        private ICorePen m_StrokeBrush;
        #region ICore2DLineElement Members
        public Vector2f StartPoint
        {
            get
            {
                return this.m_startPoint;
            }
            set
            {
                if (!this.m_startPoint.Equals(value))
                {
                    this.m_startPoint = value;
                }
            }
        }
        public Vector2f EndPoint
        {
            get
            {
                return m_endPoint;
            }
            set
            {
                m_endPoint = value;
            }
        }
        #endregion
        public LineElement()
        {            
            this.m_StrokeBrush =new CorePen(this);
            this.m_StrokeBrush.SetSolidColor(Colorf.Black);
            this.StrokeBrush.BrushDefinitionChanged += new EventHandler(StrokeBrush_BrushDefinitionChanged);
        }
        public override Rectanglef GetBound()
        {
            Pen p = null;
            if (this.StrokeBrush!= null) 
                p = this.StrokeBrush.GetPen();
            Rectanglef v_rc = CoreMathOperation.GetBounds(this.StartPoint, this.EndPoint);
            v_rc.Width = Math.Max(v_rc.Width, 1.0f);
            v_rc.Height = Math.Max(v_rc.Height, 1.0f);
            if (p!=null)
            v_rc.Inflate(p.Width, p.Width);
            return v_rc;
        }
        public override Rectanglef GetGlobalBound()
        {
            Rectanglef v_rc = this.GetBound();
            v_rc.Inflate(this.m_StrokeBrush.Width, this.m_StrokeBrush.Width);
            return v_rc;
        }
        public override bool Contains(Vector2f position)
        {
            if (this.m_StrokeBrush  == null)
            {
                return false;
            }
            CoreGraphicsPath v_p = this.GetPath();
            Pen v_pen = this.m_StrokeBrush.GetPen();
            if (v_p != null)
            {
                //return v_p.IsOutlineVisible (position, v_pen );
            }
            return false;
        }
        void StrokeBrush_BrushDefinitionChanged(object sender, EventArgs e)
        {
            OnPropertyChanged(new CoreWorkingObjectPropertyChangedEventArgs(
                (enuPropertyChanged) enu2DPropertyChangedType.BrushChanged ));
        }
        public override  enuBrushSupport BrushSupport
        {
            get
            {
                return enuBrushSupport.StrokeOnly;
            }
        }
        public override  ICoreBrush GetBrush(enuBrushMode mode) 
        {
            if (mode == enuBrushMode.Stroke)
                return this.StrokeBrush;
            return null;
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultStrokeAttribute ()]
        public ICorePen StrokeBrush
        {
            get { return m_StrokeBrush; }          
        }
        protected override void GeneratePath()
        {
            CoreGraphicsPath path = new CoreGraphicsPath();
            path.AddLine(StartPoint, EndPoint);
            this.SetPath(path);            
        }
        protected override void BuildBeforeResetTransform()
        {
            Matrix m = this.GetMatrix();
            PointF[] tab = { this.StartPoint, this.EndPoint };
            m.TransformPoints (tab);
            this.m_endPoint = tab[1];
            this.m_startPoint = tab[0];
        }
        protected override void OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs e)
        {
            switch ((enu2DPropertyChangedType )e.ID)
            {
                case enu2DPropertyChangedType .MatrixChanged :
                    ResetTransform();
                    break;
            }
            base.OnPropertyChanged(e);
        }
        public override void Dispose()
        {
            if (this.m_StrokeBrush != null)
            {
                this.m_StrokeBrush.Dispose();
                this.m_StrokeBrush = null;
            }
            base.Dispose();
        }
        public override void Draw(Graphics g)
        {
            Region rg = g.Clip;            
            CoreGraphicsPath v_p = this.GetPath();
            if ((rg == null) || (v_p == null) || (this.m_StrokeBrush == null))
                return;
            rg.Intersect(this.GetBound());
            if (rg.IsEmpty(g)) return;
            GraphicsState v_s = g.Save();            
            this.SetGraphicsProperty(g);
            Pen p = this.m_StrokeBrush.GetPen();
            if (p != null)
            {
                g.DrawPath(p, v_p);
            }
            g.Restore(v_s);
        }
        class Mecanism : Core2DDrawingMecanismBase, ICoreHandStyleMecanism 
        {
            public new LineElement Element { get { return base.Element as LineElement; } set { base.Element = value; } }
            private enuHandStyle m_HandStyle;
            /// <summary>
            /// get or set the hand style
            /// </summary>
            public enuHandStyle HandStyle
            {
                get { return m_HandStyle; }
                set
                {
                    if (m_HandStyle != value)
                    {
                        m_HandStyle = value;
                        OnHandlStyleChanged(EventArgs.Empty);
                    }
                }
            }
            public event EventHandler HandStyleChanged;
            protected virtual void OnHandlStyleChanged(EventArgs eventArgs)
            {
                if (this.HandStyleChanged != null)
                {
                    this.HandStyleChanged(this, eventArgs);
                }
            }
            protected override void InitNewCreateElement(ICore2DDrawingElement element)
            {
                if (!(element is LineElement))
                    return;
                LineElement v_l = element as LineElement;
                v_l.m_StrokeBrush.Copy(this.CurrentSurface.StrokeBrush);                
            }
            protected override void OnMouseDown(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left :
                        switch(this.State)
                        {
                            case ST_NONE :
                                if ((this.Element !=null) && (this.Snippet != null))
                                {
                                    return;
                                }
                            base.Element = this.CreateNewElement();
                            if (this.Element != null)
                            {
                                this.GenerateSnippets();
                                this.StartPoint = e.FactorPoint;
                                this.EndPoint = e.FactorPoint;
                                this.Element.m_endPoint = e.FactorPoint;
                                this.Element.m_startPoint = e.FactorPoint;
                                this.CurrentLayer.Elements.Add(this.Element);
                                this.CurrentLayer.Select(this.Element);
                                this.State = ST_CREATING;
                                this.CurrentSurface.Invalidate();
                            }
                            break;
                            case ST_CREATING :
                            case ST_EDITING :
                            if (this.Snippet == null)
                            {
                                this.State = ST_NONE;
                                OnMouseDown(e);
                                return;
                            }
                            this.State = ST_EDITING;
                            this.StartPoint = e.FactorPoint;
                            this.EndPoint = e.FactorPoint;
                            break;
                        }
                        break;
                }
            }
            protected override void GenerateActions()
            {
                base.GenerateActions();
                //toggle vertical
                IGK.DrSStudio.Drawing2D.Actions.ToogleToVertical v = new IGK.DrSStudio.Drawing2D.Actions.ToogleToVertical();
                this.AddAction(Keys.V, v);
                this.AddAction(Keys.H, v);
                this.AddAction(Keys.F, v);
            }
            public override void Edit(ICoreWorkingObject element)
            {
                base.Edit(element);
            }
            protected override void OnMouseMove(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:;
                        switch (this.State)
                        {
                            case ST_CREATING:
                                if (this.Element != null)
                                {
                                    this.UpdateCreateElement(e);
                                }
                                else
                                    this.State = ST_NONE;
                                break;
                            case ST_EDITING :
                            case ST_NONE :
                                if (this.Snippet != null)
                                {
                                    this.UpdateSnippetElement(e);
                                }
                                break;
                        }
                        break;
                }
            }
            protected override void OnMouseUp(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left: 
                        switch (this.State )
                        {
                            case ST_CREATING :
                                if (this.Element != null)
                                {
                                    UpdateCreateElement(e);
                                    this.Element.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                                    this.State = ST_EDITING;
                                }
                                break;
                            case ST_EDITING :
                                if (this.Element != null)
                                {
                                    if (this.Snippet == null)
                                        this.State = ST_NONE;
                                    InitSnippetsLocation();
                                }
                                break;
                            case ST_NONE:
                                if (this.Snippet != null)
                                {
                                    this.UpdateSnippetElement(e);
                                    this.Element.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                                }
                                break;
                        }
                        break;
                }
            }
            protected override void GenerateSnippets()
            {
                base.GenerateSnippets();
                AddSnippet(this.CurrentSurface.CreateSnippet(this, 0, 0));
                AddSnippet(this.CurrentSurface.CreateSnippet(this, 1, 1));
            }
            protected override void InitSnippetsLocation()
            { 
                RegSnippets[0].Location = CurrentSurface.GetScreenLocation ( Element.m_startPoint );
                RegSnippets[1].Location = CurrentSurface.GetScreenLocation(Element.m_endPoint);
            }
            protected override void UpdateCreateElement(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                LineElement v_l = this.Element;
                if (this.IsShiftKey)
                {
                    Vector2f ppts  = CoreMathOperation.GetAnglePoint(v_l.StartPoint, e.FactorPoint, 45);
                    v_l.m_endPoint = ppts;
                }
                else
                {
                    switch (this.HandStyle)
                    {
                        case enuHandStyle.Horizontal:
                            this.Element.m_endPoint = new Vector2f(e.FactorPoint.X,
                                v_l.StartPoint.Y);
                            break;
                        case enuHandStyle.Vertical:
                            this.Element.m_endPoint = new Vector2f(
                                v_l.StartPoint.X,
                                e.FactorPoint.Y);
                            break;
                        case enuHandStyle.FreeHand:
                        default:
                            v_l.m_endPoint = e.FactorPoint;
                            break;
                    }
                }
                this.Element.InitElement();
                this.CurrentSurface.Invalidate();
                this.InitSnippetsLocation();
            }
            protected override void UpdateSnippetElement(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                Vector2f vp = e.FactorPoint;
                LineElement vl = this.Element ;
                switch (this.HandStyle)
                { 
                    case enuHandStyle.Horizontal :
                        switch (this.Snippet.Index)
                        {
                            case 0:
                                vl.m_startPoint = new Vector2f (vp.X, vl .EndPoint .Y ) ;
                                break;
                            case 1:
                                vl.m_endPoint = new Vector2f(vp.X, vl.StartPoint .Y) ;
                                break;
                        }
                        break;
                    case enuHandStyle.Vertical :
                        switch (this.Snippet.Index)
                        {
                            case 0:
                                vl.m_startPoint = new Vector2f( vl.EndPoint.X, vp.Y );
                                break;
                            case 1:
                                vl.m_endPoint = new Vector2f( vl.StartPoint.X, vp.Y );
                                break;
                        }
                        break;
                    case enuHandStyle.FreeHand :
                    default :
                        switch (this.Snippet.Index)
                        {
                            case 0:
                                vl.m_startPoint = vp;
                                break;
                            case 1:
                                vl.m_endPoint = vp;
                                break;
                        }
                        break;
                }
                this.Snippet.Location = e.Location;
                vl.InitElement();
                this.CurrentSurface.Invalidate();
            }
            protected override void OnKeyDown(KeyEventArgs e)
            {
                base.OnKeyDown(e);
            }
        }
    }
}

