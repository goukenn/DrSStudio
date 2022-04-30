

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: SelectionElement.cs
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
file:SelectionElement.cs
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
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore;using IGK.DrSStudio.Drawing2D.WinUI;
    [Core2DDrawingSelectionItem("Selection", 
        typeof (Mecanism),
        Keys = Keys.S)]
    public class SelectionElement : Core2DDrawingObjectBase
    {
        public override bool CanEdit
        {
            get
            {
                return false;
            }
        }
        /// <summary>
        /// represent a selection mecanism class
        /// </summary>
        public class Mecanism : Core2DDrawingMecanismBase 
        {
            int m_rsMode = ST_NONE;
            protected const int RS_TOPLEFT = 0;
            protected const int RS_TOPMID = 1;
            protected const int RS_TOPRIGHT = 2;
            protected const int RS_MIDLEFT = 7;
            protected const int RS_MIDRIGHT = 3;
            protected const int RS_BOTTOMLEFT = 6;
            protected const int RS_BOTTOMMID = 5;
            protected const int RS_BOTTOMRIGHT = 4;
            Rectanglef m_oldDisplayBounds;
            Rectanglef m_oldBounds;
            public const int ST_INFLATING = 5;
            public const int ST_RESIZING = 6;
            public const int ST_ROTATING = 7;
            public const int ST_MULTISELECTING = 8;
            public const int ST_DIAGONAL_RESIZING = 10;
            public Mecanism()
            {
                this.DesignMode = false;
            }
            ICore2DDrawingLayeredElement[] m_elements;
            new Core2DDrawingLayeredElement Element {
                get { return base.Element as Core2DDrawingLayeredElement ; }
                set { base.Element = value; }
            }
            protected override void OnElementChanged(CoreElementChangedEventArgs<ICoreWorkingObject> e)
            {
                base.OnElementChanged(e);
            }
            protected override void RegisterElementEvent(ICore2DDrawingObject element)
            {
                base.RegisterElementEvent(element);
            }
            protected override void UnRegisterElementEvent(ICore2DDrawingObject element)
            {
                base.UnRegisterElementEvent(element);
            }
            /// <summary>
            /// get if this tools select element.
            /// </summary>
            bool Selected {
                get {
                    if ((m_elements == null) || (m_elements.Length == 0))
                        return false;
                    return true;
                }
            }
            /// <summary>
            /// get if this Tools select multiple element
            /// </summary>
            bool IsMulti {
                get {
                    return (Selected && (this.m_elements.Length > 0));
                }
            }
            /// <summary>
            /// get the elements 
            /// </summary>
            public ICore2DDrawingLayeredElement[] Elements {
                get {
                    return m_elements;
                }
                set {
                    this.m_elements = value;
                }
            }
            /// <summary>
            /// select all at
            /// </summary>
            /// <param name="e"></param>
            /// <returns></returns>
            protected ICore2DDrawingLayeredElement[] SelectAt(Vector2f e)
            {
                List<ICore2DDrawingLayeredElement> m_list = new List<ICore2DDrawingLayeredElement>(); 
                foreach (ICore2DDrawingLayeredElement item in this.CurrentLayer.Elements)
                {
                    if (item.Contains(e))
                    {
                        m_list.Add(item);
                    }
                }
                return m_list.ToArray();
            }
            protected override void OnCurrentLayerChanged(Core2DDrawingLayerChangedEventArgs e)
            {
                if (e.OldLayer !=null)
                {
                    this.m_elements = null;
                    e.OldLayer.Select(null);
                    this.CurrentSurface.Invalidate();
                }
                base.OnCurrentLayerChanged(e);
            }
            /// <summary>
            /// select all at that intersect 
            /// </summary>
            /// <param name="e"></param>
            /// <returns></returns>
            protected ICore2DDrawingLayeredElement[] SelectAt(Rectanglef e)
            {
                List<ICore2DDrawingLayeredElement> m_list = new List<ICore2DDrawingLayeredElement>();
                foreach (ICore2DDrawingLayeredElement item in this.CurrentLayer.Elements)
                {
                    if (!RectangleF.Intersect (item.GetBound (), e).IsEmpty)
                    {
                        m_list.Add(item);
                    }
                }
                return m_list.ToArray();
            }
            protected ICore2DDrawingLayeredElement SelectTopElement(Vector2f e)
            {
                for (int i = this.CurrentLayer .Elements .Count -1; i >=0; i--)
                {
                    if (this.CurrentLayer.Elements[i].Contains(e))
                    {
                        return this.CurrentLayer.Elements[i];
                    }                    
                }
                return null;
            }
            protected override void OnLayerSelectedElementChanged(EventArgs eventArgs)
            {
                this.m_elements = this.CurrentLayer.SelectedElements.ToArray();
                this.InitSelected();              
            }
            protected override void OnPaint(PaintEventArgs e)
            {            
                base.OnPaint(e);
                switch (this.State)
                {
                    case ST_INFLATING:
                        {
                            //RectangleF v = this.m_inflateNewBound;
                            //Matrix m = this.Element.GetDocumentMatrix();
                            //CoreMathOperation.ApplyMatrix(v, m);
                            //m.Dispose();
                            Rectanglef c = CurrentSurface .GetScreenBound ( this.m_inflateNewBound);
                            ControlPaint.DrawBorder(e.Graphics,
                                Rectanglef.Round(c),
                                Color.White,
                                 ButtonBorderStyle.Solid);
                            ControlPaint.DrawBorder(e.Graphics,
                                Rectanglef.Round(c),
                                Color.Black,
                                 ButtonBorderStyle.Dotted);
                        }
                        break;
                    case ST_MULTISELECTING:
                        {
                            Rectanglef c = this.CurrentSurface.GetScreenBound(
                                CoreMathOperation.GetBounds (this.StartPoint , this.EndPoint ));
                            ControlPaint.DrawBorder(e.Graphics,
                                Rectanglef.Round(c),
                                Color.White,
                                 ButtonBorderStyle.Solid);
                            ControlPaint.DrawBorder(e.Graphics,
                                Rectanglef.Round(c),
                                Color.Black,
                                 ButtonBorderStyle.Dotted);
                        }
                        break;
                }
            }
            protected override void OnMouseDoubleClick(MouseEventArgs e)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        {
                            if (this.Element != null)
                            {
                                Core2DDrawingLayeredElement l = this.Element;
                                if (this.State != ST_NONE)
                                {
                                    //end edition
                                    OnMouseUp(new CoreMouseEventArgs(e, CurrentSurface.GetFactorLocation(e.Location)));
                                }
                                if (l.CanEdit)
                                {
                                    //edit selected elememnt
                                    CoreSystem.Instance.Workbench.CallAction("Drawing2D.Edit");
                                }
                                return;
                            }
                        }
                        break;
                    case MouseButtons.Right :
                        break;
                }
                base.OnMouseDoubleClick(e);
            }
            protected override void  OnMouseClick(MouseEventArgs e)
            {
                Vector2f v = CurrentSurface.GetFactorLocation(e.Location);
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        if (this.State != ST_MULTISELECTING)
                        {
                            if (!this.IsControlKey)
                            {
                                if (!Selected)
                                {
                                    CheckOne(v);
                                }
                                else
                                {
                                    switch (this.State)
                                    {
                                        case ST_EDITING:
                                        case ST_NONE:
                                            CheckOne(v);
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                //control key 
                                if (this.Selected)
                                {
                                    List<ICore2DDrawingLayeredElement> m_l = new List<ICore2DDrawingLayeredElement>();
                                    m_l.AddRange(this.m_elements);
                                    ICore2DDrawingLayeredElement t = SelectTopElement(v);
                                    if (t != null)
                                    {
                                        if (m_l.Contains(t))
                                        { //remove element
                                            m_l.Remove(t);
                                        }
                                        else
                                        {
                                            m_l.Add(t);
                                        }
                                        this.m_elements = m_l.ToArray();
                                        this.CurrentLayer.Select(this.m_elements);
                                        this.InitSelected();
                                    }
                                }
                                else
                                {
                                    ICore2DDrawingLayeredElement t = SelectTopElement(v);
                                    if (t != null)
                                    {
                                        this.m_elements = new ICore2DDrawingLayeredElement[] { t };
                                        this.CurrentLayer.Select(this.m_elements);
                                        InitSelected();
                                    }
                                }
                            }
                        }
                        break;
                    case MouseButtons.Right:
                        //if you rich here no context menu is show
                        if (Selected && !this.IsSnippetSelected )
                        {
                            this.m_elements = null;
                            this.CurrentLayer.Select(null);
                        } 	
                        break;
                }
            }
            private void CheckOne(Vector2f v)
            {
                ICore2DDrawingLayeredElement t = SelectTopElement(v);
                if (t != null)
                {
                    this.CurrentLayer.Select(t);
                }
                else
                {
                    this.CurrentLayer.Select(null);         
                    this.State = ST_NONE;
                }
            }
            private void InitSelected()
            {
                if (this.Selected && this.m_elements.Length == 1)
                {
                    this.Element = this.m_elements[0] as Core2DDrawingLayeredElement;
                    GenerateSnippets();
                    InitSnippetsLocation();
                }
                else
                {
                    this.DisableSnippet();
                    this.Element = null;
                }
            }
            protected override void  GenerateSnippets()
            {
 	             base.GenerateSnippets();
                 if ((this.Selected) && (this.Element !=null))
                {
                    RectangleF v_rc = this.Elements[0].GetBound();
                    if (this.Element.CanResize)
                    {
                        Vector2f[] v_pts = CoreMathOperation.GetResizePoints(v_rc);
                        for (int i = 0; i < v_pts.Length; i++)
                        {
                            this.AddSnippet(
                            this.CurrentSurface.CreateSnippet(this, ST_RESIZING, i));
                        }
                    }
                    if (this.Element.CanRotate)
                    {
                        IGK.DrSStudio.WinUI.ICoreSnippets o = this.CurrentSurface.CreateSnippet(this, ST_ROTATING, 10);
                        o.Shape = IGK.DrSStudio.WinUI.enuSnippetShape.Circle;
                        this.AddSnippet(o);
                    }
                }
            }
            protected override void InitSnippetsLocation()
            {
                if ((RegSnippets.Count >  0) && (this.Selected) && (this.Elements.Length == 1))
                {
                    RectangleF v_rc = this.Elements[0].GetSelectionBound();
                    if (this.Element.CanResize)
                    {
                        Vector2f [] v_pts = CoreMathOperation.GetResizePoints(v_rc);
                        for (int i = 0; i < v_pts.Length; i++)
                        {
                            RegSnippets[i].Location = CurrentSurface.GetScreenLocation(v_pts[i]);
                        }
                    }
                    if (this.Element.CanRotate)
                    {
                        RegSnippets[10].Location = CurrentSurface.GetScreenLocation(
                            CoreMathOperation.GetCenter(v_rc));
                    }
                }
            }
            protected override void  OnMouseMove(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        if (Selected)
                        {
                            switch (this.State)
                            {
                                case ST_MULTISELECTING :
                                    this.EndPoint = e.FactorPoint;
                                    this.CurrentSurface.Invalidate();
                                    break;
                                case ST_MOVING:
                                    UpdateMove(e);
                                    break;
                                default:
                                    //selected element containt factor point begin move
                                    if (this.Snippet != null)
                                    {
                                        UpdateSnippetElement(e);
                                    }
                                    else
                                    {
                                       switch (this.State)
                                       {
                                           case  ST_MOVING:
                                            UpdateMove(e);
                                               break;
                                           case ST_ROTATING :
                                               UpdateRotate(e);
                                               break;
                                           default:
                                               {
                                                   CheckMultiSelection(e); 
                                               }
                                               break;
                                        }
                                    }
                                    break;
                            }
                        }
                        else {
                            if (this.State == ST_MULTISELECTING)
                            {
                                this.EndPoint = e.FactorPoint;
                                this.CurrentSurface.Invalidate();
                            }
                            else 
                                CheckMultiSelection(e);
                        }
                        break;
                    case MouseButtons.Right:
                        if (Selected)
                        {
                            //selected element containt factor point begin move
                            if (this.Snippet != null)
                            {
                                this.AllowContextMenu = false;
                                UpdateSnippetElement(e);
                            }
                        }
                        break;
                }
            }
            private void CheckMultiSelection(CoreMouseEventArgs e)
            {
                this.EndPoint = e.FactorPoint;
                PointF c = CoreMathOperation.GetDistanceP(e.Location, 
                   this.CurrentSurface .GetScreenLocation ( this.StartPoint));
                if ((Math.Abs (c.X) >= System.Windows.Forms.SystemInformation.DragSize.Width) &&
                    (Math.Abs (c.Y) >= System.Windows.Forms.SystemInformation.DragSize.Height))
                {
                    this.State = ST_MULTISELECTING;
                    this.CurrentSurface.Invalidate();
                }
            }
            private void UpdateRotate(CoreMouseEventArgs e)
            {
                this.EndPoint = e.FactorPoint;
                float d = CoreMathOperation.GetAngle(StartPoint, EndPoint) * 
                    CoreMathOperation.ConvRdToDEGREE ;
                Rotate(d, this.StartPoint , true);
                //this.Element.Invalidate(true);
                this.CurrentSurface.Invalidate();
            }
            private void EndRotate(CoreMouseEventArgs e)
            {
                this.State = ST_EDITING;
                this.EndPoint = e.FactorPoint;
                float d = CoreMathOperation.GetAngle(StartPoint, EndPoint) *
                    CoreMathOperation.ConvRdToDEGREE;
                Rotate(d, this.StartPoint, false );
                this.InitSnippetsLocation();
                this.CurrentSurface.Invalidate();
            }
            private void Rotate(float angle, Vector2f center,  bool p)
            {
                if (this.Element == null)
                    return;
                this.Element.Rotate(angle, center, enuMatrixOrder.Append, p);
            }
            protected override void OnMouseUp(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                switch (this.State )
                {
                    case ST_MOVING :
                        EndMove(e);                        
                        break;
                    case ST_ROTATING :
                        this.EndRotate(e);
                        break;
                    case ST_RESIZING :
                        EndResize(e);
                        break;
                    case ST_INFLATING :
                        EndInflate(e);
                        break;
                    case ST_DIAGONAL_RESIZING :
                        EndDiagonalResizing(e);
                        break;
                    case ST_MULTISELECTING:
                        this.m_elements = SelectAt(CoreMathOperation.GetBounds(this.StartPoint, this.EndPoint));
                        this.CurrentLayer.Select(this.m_elements);
                        this.InitSelected();
                        this.State = ST_NONE;
                        this.CurrentSurface.Invalidate();
                        break;
                    default:
                        break;
                }
            }
            protected override void OnMouseDown(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                this.StartPoint = e.FactorPoint;
                this.EndPoint = e.FactorPoint;
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        if (!this.IsControlKey)
                        {
                            if ((this.Snippet==null) &&  this.Selected)
                            {
                                //check if can move
                                for (int i = 0; i < this.m_elements.Length; i++)
                                {
                                    if (this.m_elements[i].Contains(e.FactorPoint))
                                    {
                                        this.BeginMove(e);
                                        return;
                                    }
                                }
                            }
                        }
                        break;
                    case MouseButtons.Right:
                        if (this.Snippet == null)
                        {
                            this.AllowContextMenu = true;
                        }
                        else
                            this.AllowContextMenu = false;
                        break;
                    default:
                        break;
                }
            }
            protected override void UpdateSnippetElement(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                this.m_rsMode = this.Snippet.Index;
                switch (this.State)
                {
                    case ST_DIAGONAL_RESIZING:
                        {
                            if (this.IsShiftKey)
                            {
                                UpdateDiagonalResizing(e, true);
                            }
                            else {
                                this.State = ST_RESIZING;
                                UpdateResize(e, true);
                            }
                            this.Snippet.Location = e.Location;
                        }
                        break;
                    case ST_RESIZING:
                        {
                            if (this.IsShiftKey)
                            {
                                this.State = ST_DIAGONAL_RESIZING;
                                UpdateDiagonalResizing(e, true);
                            }
                            else 
                                 UpdateResize (e, true );
                            this.Snippet.Location = e.Location;
                        }
                        break;
                    case ST_INFLATING:
                        {
                            UpdateInflate(e, true);
                            this.Snippet.Location = e.Location;
                        }
                        break;
                    default :
                        switch (e.Button)
                        {
                            case MouseButtons.Left:
                                if (this.Snippet.Demand != ST_ROTATING)
                                {
                                    if ((this.Snippet.Index == RS_BOTTOMRIGHT)&&(this.IsShiftKey ))
                                    {
                                        this.State = ST_DIAGONAL_RESIZING;
                                        this.BeginDiagonalResizing(e);
                                    }
                                    else
                                    {
                                        this.State = ST_RESIZING;
                                        BeginResize(e, this.Snippet.Index);
                                    }
                                }
                                else
                                {//begin rotate
                                    this.State = ST_ROTATING;
                                    this.StartPoint = CurrentSurface.GetFactorLocation(Snippet.Location);
                                    this.EndPoint = this.StartPoint;
                                    UpdateRotate(e);
                                }
                                break;
                            case MouseButtons.Middle:
                                break;
                            case MouseButtons.None:
                                break;
                            case MouseButtons.Right:
                                if (this.Snippet.Demand == ST_RESIZING)
                                {
                                    switch (this.Snippet.Index)
                                    {
                                        case RS_BOTTOMLEFT :
                                        case RS_BOTTOMRIGHT :
                                        case RS_TOPLEFT :
                                        case RS_TOPRIGHT :
                                            this.State = ST_INFLATING;
                                            BeginInflate(e, this.Snippet.Demand);
                                            break;
                                    }
                                }
                                break;
                            case MouseButtons.XButton1:
                                break;
                            case MouseButtons.XButton2:
                                break;
                            default:
                                break;
                        }
                        break ;
                }
            }
            private void BeginDiagonalResizing(CoreMouseEventArgs e)
            {
                m_oldBounds = this.Element.GetBound();
                this.StartPoint = e.FactorPoint;
                this.EndPoint = e.FactorPoint;
            }
            private void UpdateDiagonalResizing(CoreMouseEventArgs e, bool temp)
            {
                Vector2f v_t = new Vector2f(m_oldBounds.Width, m_oldBounds.Height);
                v_t.Normalize();
                if (v_t == Vector2f.Zero)
                    return;
                float f = m_oldBounds.Diagonal;
                float ex = 1.0f;
                float ey = 1.0f;
                Vector2f v_d = e.FactorPoint - m_oldBounds.Location;
                ex = (v_d.X ) / m_oldBounds.Width ;
                ey = (v_d.Y ) / m_oldBounds.Height ;
                ex = Math.Max(ex, ey);
                ey = ex;
                this.Element.Scale(ex, ey, this.m_oldBounds,
                        this.m_oldBounds.Location , enuMatrixOrder.Append, temp);
                this.CurrentSurface.Invalidate();
            }
            private void EndDiagonalResizing(CoreMouseEventArgs e)
            {
                UpdateDiagonalResizing(e, false);
                this.State = ST_NONE;
            }
            RectangleF m_inflateOldBound;
            RectangleF m_inflateNewBound;
            protected RectangleF InflateNewBound { get { return this.m_inflateNewBound; } }
            protected void BeginInflate(CoreMouseEventArgs e, int sMode)
            {
                if ((this.Element == null) || (this.Element.Locked))
                    return;
                this.StartPoint = e.FactorPoint;
                this.EndPoint = e.FactorPoint;
                this.m_inflateOldBound = this.Element.GetDisplayBounds ();//.GetBound ();
                this.m_inflateNewBound = this.m_inflateOldBound;
                this.State = ST_INFLATING;
                this.m_rsMode = sMode;
            }
            protected virtual void UpdateInflate(CoreMouseEventArgs e, bool temp)
            {
                if ((this.Element == null) || (this.Element.Locked))
                    return;
                EndPoint = e.FactorPoint;
                float x = 0.0f;
                float y = 0.0f;
                PointF d = CoreMathOperation.GetDistanceP(EndPoint, StartPoint);
                if (this.IsShiftKey)
                {
                    int f = 1;
                    if ((d.X < 0))
                        f = -1;
                    x = CoreMathOperation.GetDistance(EndPoint, StartPoint) * f;
                    y = x;
                }
                else
                {
                    x = d.X;
                    y = d.Y;
                }
                switch (this.m_rsMode)
                {
                    case RS_TOPLEFT:
                        x *= -1;
                        y *= -1;
                        break;
                    case RS_TOPRIGHT:
                        //x *= -1;
                        y *= -1;
                        break;
                    case RS_BOTTOMLEFT:
                        x *= -1;
                        break;
                    case RS_BOTTOMRIGHT:
                        break;
                    default:
                        return;
                }
                if (temp == false)
                {
                    //this.Element.Invalidate(false);
                    this.Element.Inflate(x, y, enuMatrixOrder.Append );
                    //this.Element.Invalidate(true);
                }
                else
                {
                   // this.CurrentSurface.InvalidZoomRectangle(this.m_inflateNewBound, 1.0f);
                    this.m_inflateNewBound = this.m_inflateOldBound;
                    this.m_inflateNewBound.Inflate(x, y);
                    //this.CurrentSurface.InvalidZoomRectangle(this.m_inflateNewBound, 1.0f);
                }
                this.CurrentSurface.Invalidate();
            }
            protected void EndInflate(CoreMouseEventArgs e)
            {
                if ((this.Element == null) || (this.Element.Locked))
                    return;
                UpdateInflate(e, false);
                if (this.State == ST_INFLATING)
                {
                    this.State = ST_EDITING;
                }
                InitSnippetsLocation();
            }
            protected void BeginResize(CoreMouseEventArgs e, int rsMode)
            {
                if (this.Element == null)
                    return;
                if (this.Element.Locked) 
                    return;
                this.StartPoint = e.Location;// e.FactorPoint;
                this.EndPoint = e.Location;// e.FactorPoint;
                this.m_rsMode = rsMode;
                this.m_oldBounds = this.Element.GetBound ();
                this.m_oldDisplayBounds = this.CurrentSurface.GetScreenBound ( this.Element.GetDisplayBounds());
                this.State = ST_RESIZING;
            }
            protected void EndResize(CoreMouseEventArgs e)
            {
                if (this.Element == null)
                    return;
                if (!this.Element.Locked)
                {                
                    UpdateResize(e, false);
                }
                m_rsMode = -1;
                if (this.State == ST_RESIZING)
                    this.State = ST_EDITING;
                this.CurrentSurface.Cursor = this.GetCursor();
                this.InitSnippetsLocation();
            }
            protected void UpdateResize(CoreMouseEventArgs e, bool temp)
            {
                if (this.Element == null)
                    return;
                if (this.Element.Locked) 
                    return;
                float ex = 1.0f;
                float ey = 1.0f;
                PointF point = e.Location;// e.FactorPoint;
                SetUpPoint(point);
                PointF d = CoreMathOperation.GetDistanceP(EndPoint, StartPoint);
                //RectangleF vrect = CoreMathOperation.GetBounds(
                //    this.StartPoint,
                //    this.EndPoint);
                float w = 0.0f;
                float h = 0.0f;
                float locx = 0.0f;
                float locy = 0.0f;
                switch (this.m_rsMode)
                {
                    case RS_MIDRIGHT:
                        //ok
                        w = this.m_oldDisplayBounds.Width + d.X;
                        h = this.m_oldDisplayBounds.Height;
                        break;
                    case RS_MIDLEFT:
                        //ok
                        w = this.m_oldDisplayBounds.Width - d.X;
                        h = this.m_oldDisplayBounds.Height;
                        //r.X = this.m_oldDisplayBounds.X + d.X;
                        locx = d.X;
                        break;
                    case RS_TOPMID:
                        //ok
                        w = this.m_oldDisplayBounds.Width;
                        h = this.m_oldDisplayBounds.Height - d.Y;
                        // r.Y = this.m_oldDisplayBounds.Y + d.Y;
                        locy = d.Y;
                        break;
                    case RS_BOTTOMMID:
                        w = this.m_oldDisplayBounds.Width;
                        h = this.m_oldDisplayBounds.Height + d.Y;
                        break;
                    case RS_TOPLEFT:
                        //ok
                        w = this.m_oldDisplayBounds.Width - d.X;
                        h = this.m_oldDisplayBounds.Height - d.Y;
                        //r.X = this.m_oldDisplayBounds.X + d.X;
                        //r.Y = this.m_oldDisplayBounds.Y + d.Y;
                        locx = d.X;
                        locy = d.Y;
                        break;
                    case RS_BOTTOMLEFT:
                        #region
                        //ok
                        w = this.m_oldDisplayBounds.Width - d.X;
                        h = this.m_oldDisplayBounds.Height + d.Y;
                        //r.X = this.m_oldDisplayBounds.X + d.X;
                        locx = d.X;
                        #endregion
                        break;
                    case RS_TOPRIGHT:
                        w = this.m_oldDisplayBounds.Width + d.X;
                        h = this.m_oldDisplayBounds.Height - d.Y;
                        //r.X = this.m_oldDisplayBounds.X;
                        //r.Y = this.m_oldDisplayBounds.Y + d.Y;
                        locy = d.Y;
                        break;
                    case RS_BOTTOMRIGHT:
                        w = this.m_oldDisplayBounds.Width + d.X;
                        h = this.m_oldDisplayBounds.Height + d.Y;
                        break;
                }
                RectangleF r = Rectangle.Empty;
                PointF[] locpt = new PointF[] { new PointF(locx, locy) };
                Matrix vm = this.Element.GetDocumentMatrix();
                if (vm.IsInvertible)
                    vm.Invert();
                vm.TransformVectors(locpt);
                r.Location = this.m_oldBounds.Location;
                ex *= w / this.m_oldDisplayBounds.Width;
                ey *= h / this.m_oldDisplayBounds.Height;
                r.X += (locpt[0].X / this.CurrentSurface.ZoomX);
                r.Y += (locpt[0].Y / this.CurrentSurface.ZoomY);
                vm.Dispose();
                if (this.Element != null)
                {
                    //this.Element.Invalidate(false);
                    this.Element.Scale(ex, ey,
                        this.m_oldBounds,
                        r.Location,
                        enuMatrixOrder.Append, temp);
                    this.CurrentSurface.Invalidate();
                }
            }
            private void SetUpPoint(PointF point)
            {
                PointF stP = this.StartPoint;
                PointF enP = this.EndPoint;
                switch (this.m_rsMode)
                {
                    case RS_BOTTOMRIGHT:
                    case RS_BOTTOMLEFT:
                    case RS_TOPLEFT:
                    case RS_TOPRIGHT:
                        this.EndPoint = point;
                        break;
                    case RS_MIDLEFT:
                        stP.Y = this.m_oldDisplayBounds.Y;
                        //this.StartPoint.Y =this.oldBounds.Y;
                        this.StartPoint = stP;
                        this.EndPoint = new PointF(point.X,
                           this.m_oldDisplayBounds.Y + this.m_oldDisplayBounds.Height);
                        break;
                    case RS_MIDRIGHT:
                        stP.Y = this.m_oldDisplayBounds.Y;
                        //this.StartPoint.Y =this.oldBounds.Y;
                        this.StartPoint = stP;
                        this.EndPoint = new PointF(point.X,
                           this.m_oldDisplayBounds.Y + this.m_oldDisplayBounds.Height);
                        break;
                    case RS_BOTTOMMID:
                    case RS_TOPMID:
                        stP.X = this.m_oldDisplayBounds.X;
                        //this.StartPoint.X =this.oldBounds.X;
                        this.StartPoint = stP;
                        this.EndPoint = new PointF(
                           this.m_oldDisplayBounds.X + this.m_oldDisplayBounds.Width,
                            point.Y);
                        break;
                }
            }
            protected override void OnKeyPress(KeyPressEventArgs e)
            {
                switch (e.KeyChar)
                {
                    case (char)0x1B://escape
                        if (this.IsMulti)
                        {
                            this.CurrentLayer.Select(null);                            
                        }
                        else
                        {
                            //don't go to any tool
                            if (this.Selected)
                                this.CurrentLayer.Select(null);
                        }                        
                        e.Handled = true;
                        break;
                }
                base.OnKeyPress(e);
            }
            protected override void OnKeyUp(KeyEventArgs e)
            {
                base.OnKeyUp(e);
            }
            protected override void Move(PointF d, bool temp)
            {
                if (this.IsMulti || (this.Element == null))
                {
                    Core2DDrawingLayeredElement l = null;
                    for (int i = 0; i < this.m_elements.Length; i++)
                    {
                        l = this.m_elements[i] as Core2DDrawingLayeredElement;
                        if (l == null) continue;
                        if (l.CanTranslate && !l.Locked)
                        {
                            l.Translate(d.X, d.Y, enuMatrixOrder.Append, temp);
                        }
                    }
                }
                else
                {
                    this.Element.Translate(d.X, d.Y, enuMatrixOrder.Append, temp);
                }
            }
            protected void CancelMoving()
            {
                //this.Element.Invalidate(false);
                if (this.IsMulti)
                {
                    Core2DDrawingLayeredElement l = null;
                    for (int i = 0; i < this.m_elements.Length; i++)
                    {
                        l = this.m_elements[i] as Core2DDrawingLayeredElement;
                        if (l == null) continue;
                        if (l.CanTranslate && !l.Locked)
                        {
                            this.Element.Translate(0, 0, enuMatrixOrder.Append, true);
                        }
                    }
                }
                else
                {
                    this.Element.Translate(0, 0, enuMatrixOrder.Append, true);
                }
                //   this.Element.Translate(0, 0, enuMatrixOrder.Append, true);
                // this.Element.Invalidate(true);                
                this.InitSnippetsLocation();
                this.EnabledSnippet();
                if (this.State == ST_MOVING)
                    this.State = ST_EDITING;
                this.CurrentSurface.Cursor = this.GetCursor();
            }
        }
    }
}

