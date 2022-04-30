

/*
IGKDEV @ 2008-2016
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
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:SelectionElement.cs
*/
using IGK.ICore;
using IGK.ICore.Actions;
using IGK.ICore.Drawing2D.Mecanism;
using IGK.ICore.Drawing2D.MecanismActions;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.GraphicModels;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Drawing2D
{
    [Core2DDrawingSelectionAttribute("Selection",
        typeof(Mecanism),
        Keys=enuKeys.S)]
    public class SelectionElement : SelectElementBase
    {
        /// <summary>
        /// .only for hériter element
        /// </summary>
        protected SelectionElement ()
        {
        }
        public class Mecanism : Core2DDrawingSurfaceMecanismBase<Core2DDrawingLayeredElement>     
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
            Rectanglef m_oldDisplayBounds; //old display bound
            Rectanglef m_oldFactorBounds; //old factor bound
            public const int ST_INFLATING = 5;
            public const int ST_RESIZING = 6;
            public const int ST_ROTATING = 7;
            public const int ST_MULTISELECTING = 8; //state stand for selecting multiple element by rectangle selection
            public const int ST_DIAGONAL_RESIZING = 10;
            public Mecanism()
            {
                this.DesignMode = false;
            }
            ICore2DDrawingLayeredElement[] m_SelectedElements;//selected elements
            new Core2DDrawingLayeredElement Element {
                get { return base.Element as Core2DDrawingLayeredElement ; }
                set { base.Element = value; }
            }

            protected override void GenerateActions()
            {
                base.GenerateActions();
                // TODO: SELECTION MECANISM SHORTCUT
                this.AddAction(enuKeys.Control | enuKeys.A, new Core2DDrawingSelectAllMecanismAction());
                this.AddAction(enuKeys.Control | enuKeys.NumPad0, new Core2DDrawingZoom0());
                //this.AddAction(enuKeys.Control | enuKeys.NumPad1, new Core2DDrawingZoom0());
                //this.AddAction(enuKeys.Control | enuKeys.NumPad2, new Core2DDrawingZoom0());
                //this.AddAction(enuKeys.Control | enuKeys.NumPad3, new Core2DDrawingZoom0());
                //this.AddAction(enuKeys.Control | enuKeys.NumPad4, new Core2DDrawingZoom0());
                //this.AddAction(enuKeys.Control | enuKeys.NumPad6, new Core2DDrawingZoom0());
                //this.AddAction(enuKeys.Control | enuKeys.OemMinus, new Core2DDrawingZoom0());
                //this.AddAction(enuKeys.Control | enuKeys.Oemplus, new Core2DDrawingZoom0());

            }

            /*
             * 
             * 
             * */

            protected override void OnElementPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs e)
            {
                switch ((enu2DPropertyChangedType)e.ID)
                {
                    case enu2DPropertyChangedType.MatrixChanged:
                    case enu2DPropertyChangedType.DefinitionChanged:
                        Core2DDrawingLayeredElement v_l = this.Element as Core2DDrawingLayeredElement;
                        if (v_l != null)
                        {
                            this.InitSnippetsLocation();
                            this.Invalidate();
                        }
                        break;
                    case enu2DPropertyChangedType.BrushChanged :
                        this.Invalidate();
                        break ;
                }
            }
            /// <summary>
            /// get if this tools select element.
            /// </summary>
            bool IsSelected {
                get {
                    if ((m_SelectedElements == null) || (m_SelectedElements.Length == 0))
                        return false;
                    return true;
                }
            }
            /// <summary>
            /// get if this Tools select multiple element
            /// </summary>
            bool IsMulti {
                get {
                    return (IsSelected && (this.m_SelectedElements.Length > 1));
                }
            }
            /// <summary>
            /// get the elements selected by this current tool
            /// </summary>
            public ICore2DDrawingLayeredElement[] SelectedElements {
                get {
                    return m_SelectedElements;
                }
                set {
                    this.m_SelectedElements = value;
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
                if (e.OldElement  !=null)
                {
                    this.m_SelectedElements = null;
                    e.OldElement.Select(null);
                    this.CurrentSurface.RefreshScene();
                }
                base.OnCurrentLayerChanged(e);
            }
            protected override void OnCurrentDocumentChanged(CoreWorkingDocumentChangedEventArgs e)
            {
                if (this.SelectedElements != null)
                {
                    this.SelectedElements = null;
                    this.State = ST_NONE;
                }
                base.OnCurrentDocumentChanged(e);
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
                    if (!item.Intersect (e).IsEmpty )
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
            public override bool UnRegister()
            {
                return base.UnRegister();
            }
            protected override void OnLayerSelectedElementChanged(EventArgs eventArgs)
            {
                if (this.CurrentLayer == null) {
                    throw new Exception("Select element changed on non surface");
                }
                this.m_SelectedElements = this.CurrentLayer.SelectedElements.ToArray();
                this.InitSelected();              
            }
            public override void Render(ICoreGraphics device)
            {            
                switch (this.State)
                {
                    case ST_INFLATING:
                        {
                            Rectanglef v = this.m_inflateNewBound;
                            Matrix m = this.Element.GetDocumentMatrix();
                            CoreMathOperation.ApplyMatrix(v, m);
                            m.Dispose();
                            Rectanglef c = CurrentSurface.GetScreenBound(this.m_inflateNewBound);
                            device.DrawRectangle(
                                Colorf.White ,
                                c.X, c.Y, c.Width, c.Height);
                            device.DrawRectangle(Colorf.Black, 1.0f, enuDashStyle.Dot, c.X, c.Y, c.Width, c.Height);
                        }
                        break;
                    case ST_MULTISELECTING:
                        {
                            var obj = device.Save();
                            device.SmoothingMode = enuSmoothingMode.None;
                            Rectanglef c = this.CurrentSurface.GetScreenBound(
                                CoreMathOperation.GetBounds (this.StartPoint , this.EndPoint ));
                            device.DrawRectangle(Colorf.White, c.X, c.Y, c.Width, c.Height);
                            device.DrawRectangle(Colorf.Black,1.0f, enuDashStyle.Dot , c.X, c.Y, c.Width, c.Height);
                            device.Restore(obj);
                        }
                        break;
                }
            }
            protected override void OnMouseDoubleClick(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case enuMouseButtons.Left:
                        {
                            if (this.Element != null)
                            {
                                Core2DDrawingLayeredElement l = this.Element;
                                if (this.State != ST_NONE)
                                {
                                    //end edition
                                    OnMouseUp(e);
                                }
                                if (l.IsLoading)
                                    this.Element.ResumeLayout();
                                if (l.CanEdit)
                                {
                                    CallAction("Drawing2D.CoreMecanismEditElement");
                                }
                                return;
                            }
                        }
                        break;
                    case enuMouseButtons.Right :
                        break;
                }
                base.OnMouseDoubleClick(e);
            }

        

            /*
             * 
             * remember that mouse click appears before mouseup
             * 
             * */
            /// <summary>
            ///
            /// </summary>
            /// <param name="e"></param>
            /// <remarks>onmouse clickmouse click </remarks>
            protected override void  OnMouseClick(CoreMouseEventArgs e)
            {
                Vector2f v = e.FactorPoint;                
                switch (e.Button)
                {
                    case enuMouseButtons.Left:
                        switch (this.State)
                        {
                            case ST_MOVING:
                                break;
                            default:
                                if ((this.State != ST_MULTISELECTING) && (this.Snippet ==null))
                                {
                                    if (IsShiftKey) {
                                        this.CurrentLayer.Select(e.FactorPoint, true);
                                        break;
                                    }

                                    var h = this.SelectTopElement(e.FactorPoint);
                                    if (IsControlKey)
                                    {
                                        if (h == null)
                                            _clearSelection();
                                        else
                                        {
                                            if (this.IsSelected)
                                            {

                                                List<ICore2DDrawingLayeredElement> p = new List<ICore2DDrawingLayeredElement>();
                                                p.AddRange(this.SelectedElements);
                                                if (p.Contains(h))
                                                {
                                                    p.Remove(h);
                                                }
                                                else
                                                    p.Add(h);
                                                if (p.Count > 0)
                                                {
                                                    this._selectElements(p.ToArray());
                                                }
                                                else
                                                    _clearSelection();

                                            }
                                            else
                                            {
                                                //add element to list
                                                List<ICore2DDrawingLayeredElement> p = new List<ICore2DDrawingLayeredElement>();

                                                if ((this.Element != null) && (this.Element != h))
                                                {
                                                    p.Add(this.Element);
                                                }
                                                p.Add(h);
                                                _selectElements(p.ToArray());

                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (h == null)
                                            _clearSelection();
                                        else
                                        {
                                            _selectElements(new ICore2DDrawingLayeredElement[] { h });

                                        }
                                    }
                                }
                                break;
                        }
                        break;
                    case enuMouseButtons.Right:
                        //if you rich here no context menu is show
                        if (IsSelected && !this.IsSnippetSelected )
                        {
                            this.SelectedElements = null;
                            this.CurrentLayer.Select(null);
                        } 	
                        break;
                }
            }

            private void _selectElements(ICore2DDrawingLayeredElement[] tab)
            {
                this.Element = null;
                this.SelectedElements =tab;
                this.CurrentLayer.Select(tab);
               // this.InitSelected();
            }

            private void _clearSelection()
            {
                this.Element = null;
                this.SelectedElements = null;
                this.CurrentLayer.Select(null);
                this.InitSelected();
            }

            private void SelectSingleElement(Vector2f v)
            {
                ICore2DDrawingLayeredElement t = SelectTopElement(v);
                if (t != null)
                {
                    this.SelectedElements = new ICore2DDrawingLayeredElement[] { t };
                    this.CurrentLayer.Select(this.SelectedElements);
                    InitSelected();
                }
                else {
                    if (this.Element != null)
                    {
                        this.CurrentLayer.Select(null);
                        InitSelected();
                    }
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
                if (this.IsSelected)
                {
                    if (this.SelectedElements.Length == 1)
                    {
                        this.Element = this.SelectedElements[0] as Core2DDrawingLayeredElement;
                        GenerateSnippets();
                        InitSnippetsLocation();
                    }
                    else if (this.m_SelectedElements.Length > 1)
                    {
                        this.Element = null;
                        DisableSnippet();
                    }
                }
                else
                {
                    this.DisableSnippet();
                    this.Element = null;
                }
            }
            protected override void DefineElementProperty(CoreWorkingElementChangedEventArgs<Core2DDrawingLayeredElement> e)
            {
                if (this.IsMulti)
                { 
                    //do nothing
                }
                else                    
                base.DefineElementProperty(e);
            }
            protected internal override void GenerateSnippets()
            {
 	             base.GenerateSnippets();
                 if ((this.IsSelected) && (this.Element !=null))
                {
                    if (this.Element.CanReSize)
                    {
                        Rectanglef v_rc = this.SelectedElements[0].GetBound();
                        Vector2f[] v_pts = CoreMathOperation.GetResizePoints(v_rc);
                        for (int i = 0; i < v_pts.Length; i++)
                        {
                            this.AddSnippet(
                            this.CurrentSurface.CreateSnippet(this, ST_RESIZING, i));
                        }
                    }
                    if (this.Element.CanRotate)
                    {
                        ICoreSnippet o = this.CurrentSurface.CreateSnippet(this, ST_ROTATING, 10);
                        o.Shape = IGK.ICore.WinUI.enuSnippetShape.Circle;
                        this.AddSnippet(o);
                    }
                }
            }
            protected internal override void InitSnippetsLocation()
            {
                if ((RegSnippets.Count >  0) && (this.IsSelected) && (this.SelectedElements.Length == 1))
                {
                    Rectanglef v_rc = this.SelectedElements[0].GetSelectionBound();
                    if (this.Element.CanReSize)
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
            protected override void  OnMouseMove(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case enuMouseButtons.Left:
                        if (IsSelected)
                        {
                            switch (this.State)
                            {
                                case ST_MULTISELECTING :
                                    this.EndPoint = e.FactorPoint;
                                    this.Invalidate();
                                    break;
                                case ST_MOVING:
                                    UpdateMove(e);
                                    break;
                                default:
                                    //selected element containt factor point begin move
                                    if (this.Snippet != null)
                                    {
                                        UpdateSnippetEdit(e);
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
                                this.Invalidate();
                            }
                            else 
                                CheckMultiSelection(e);
                        }
                        break;
                    case enuMouseButtons.Right:
                        if (IsSelected)
                        {
                            //selected element containt factor point begin move
                            if (this.Snippet != null)
                            {
                                this.AllowContextMenu = false;
                                UpdateSnippetEdit(e);
                            }
                        }
                        break;
                }
            }
            private void CheckMultiSelection(CoreMouseEventArgs e)
            {
                this.EndPoint = e.FactorPoint;
                Vector2f  c = CoreMathOperation.GetDistanceP(e.Location, 
                   this.CurrentSurface .GetScreenLocation ( this.StartPoint));
                if ((Math.Abs (c.X) >= DragSize.Width) &&
                    (Math.Abs (c.Y) >= DragSize.Height))
                {
                    this.State = ST_MULTISELECTING;
                    this.Invalidate();
                }
            }
            private void UpdateRotate(CoreMouseEventArgs e)
            {
                this.EndPoint = e.FactorPoint;
                float d = CoreMathOperation.GetAngle(StartPoint, EndPoint) * 
                    CoreMathOperation.ConvRdToDEGREE ;
                Rotate(d, this.StartPoint , true);                
                this.CurrentSurface.RefreshScene();
            }
            private void EndRotate(CoreMouseEventArgs e)
            {
                this.State = ST_EDITING;
                this.EndPoint = e.FactorPoint;
                float d = CoreMathOperation.GetAngle(StartPoint, EndPoint) *
                    CoreMathOperation.ConvRdToDEGREE;
                Rotate(d, this.StartPoint, false );
                this.InitSnippetsLocation();
                this.CurrentSurface.RefreshScene();
            }
            private void Rotate(float angle, Vector2f center,  bool p)
            {
                if (this.Element == null)
                    return;
                this.Element.Rotate(angle, center, enuMatrixOrder.Append, p);
            }
           protected override void OnMouseUp(CoreMouseEventArgs e)
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
                        //
                        //click raised before MouseUp
                        //

                        this.SelectedElements = SelectAt(CoreMathOperation.GetBounds(this.StartPoint, this.EndPoint));
                        this.CurrentLayer.Select(this.m_SelectedElements);
                        this.InitSelected();
                        this.State = ST_NONE;
                        this.Invalidate();
                        break;
                    default:
                        
                        break;
                }


                this.StartPoint = Vector2f.Zero;
                this.EndPoint = Vector2f.Zero;
            }

            protected override void BeginMove(CoreMouseEventArgs e)
            {
                this.DisableSnippet();
                if ((this.Element == null) && (this.m_SelectedElements == null))
                    return;
                this.CurrentSurface.Cursor = CoreCursors.Hand;
                this.StartPoint = e.FactorPoint;
                this.EndPoint = e.FactorPoint;
                this.State = ST_MOVING;
            }
            
            protected override void UpdateMove(CoreMouseEventArgs e)
            {
                base.UpdateMove(e);
            }
            protected override void Move(Vector2f d, bool temp)
            {
                if (this.IsMulti || (this.Element == null))
                {
                    Core2DDrawingLayeredElement l = null;
                    for (int i = 0; i < this.m_SelectedElements.Length; i++)
                    {
                        l = this.m_SelectedElements[i] as Core2DDrawingLayeredElement;
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
        
            protected override void EndMove(CoreMouseEventArgs e)
            {
                this.EndPoint = e.FactorPoint;
                Vector2f d = CoreMathOperation.GetDistanceP(EndPoint, StartPoint);
                //move all selected element
                Move(d, false);
                if (this.Element != null)
                {
                    if (this.Element.IsLoading)
                        this.Element.ResumeLayout();
                    this.InitSnippetsLocation();
                    this.EnabledSnippet();
                }
                if (this.State == ST_MOVING)
                    this.State = ST_EDITING;
                this.CurrentSurface.Cursor = this.GetCursor();

                this.Invalidate();
            }
       
        
            
            protected override void OnMouseDown(CoreMouseEventArgs e)
            {
                this.StartPoint = e.FactorPoint;
                this.EndPoint = e.FactorPoint;
                switch (e.Button)
                {
                    case enuMouseButtons.Left:
                        if (!this.IsControlKey)
                        {
                            if ((this.Snippet == null) && this.IsSelected)
                            {
                                //check if can move
                                for (int i = 0; i < this.m_SelectedElements.Length; i++)
                                {
                                    if ( this.SelectedElements[i] ==null) 
                                        break;
                                    if (this.SelectedElements[i].Contains(e.FactorPoint))
                                    {
                                        
                                        this.BeginMove(e);
                                        return;
                                    }
                                }
                            }
                        }
                      
                        break;
                    case enuMouseButtons.Right:
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
            protected override void UpdateSnippetEdit(CoreMouseEventArgs e)
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
                            case enuMouseButtons.Left:
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
                            case enuMouseButtons.Middle:
                                break;
                            case enuMouseButtons.None:
                                break;
                            case enuMouseButtons.Right:
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
                            case enuMouseButtons.XButton1:
                                break;
                            case enuMouseButtons.XButton2:
                                break;
                            default:
                                break;
                        }
                        break ;
                }
            }
            private void BeginDiagonalResizing(CoreMouseEventArgs e)
            {
                this.StartPoint = e.FactorPoint;
                this.EndPoint = e.FactorPoint;
                m_oldFactorBounds = this.Element.GetBound();
            }
            private void UpdateDiagonalResizing(CoreMouseEventArgs e, bool temp)
            {
                Vector2f v_t = new Vector2f(m_oldFactorBounds.Width, m_oldFactorBounds.Height);
                v_t.Normalize();
                if (v_t == Vector2f.Zero)
                    return;
                float f = m_oldFactorBounds.Diagonal;
                float ex = 1.0f;
                float ey = 1.0f;
                Vector2f v_d = e.FactorPoint - m_oldFactorBounds.Location;
                ex = (v_d.X ) / m_oldFactorBounds.Width ;
                ey = (v_d.Y ) / m_oldFactorBounds.Height ;
                ex = Math.Max(ex, ey);
                ey = ex;
                this.Element.Scale(ex, ey, this.m_oldFactorBounds,
                        this.m_oldFactorBounds.Location , enuMatrixOrder.Append, temp);
                this.CurrentSurface.RefreshScene();
            }
            private void EndDiagonalResizing(CoreMouseEventArgs e)
            {
                UpdateDiagonalResizing(e, false);
                this.InitSnippetsLocation();
                this.State = ST_NONE;
            }
            Rectanglef m_inflateOldBound;
            Rectanglef m_inflateNewBound;
            protected Rectanglef InflateNewBound { get { return this.m_inflateNewBound; } }
            protected void BeginInflate(CoreMouseEventArgs e, int sMode)
            {
                if ((this.Element == null) || (this.Element.Locked))
                    return;
                this.StartPoint = e.FactorPoint;
                this.EndPoint = e.FactorPoint;
                this.m_inflateOldBound = this.CurrentSurface.GetScreenBound (this.Element.GetBound());//.GetDisplayBounds ();//.GetBound ();
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
                Vector2f d = CoreMathOperation.GetDistanceP(EndPoint, StartPoint);
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
                this.CurrentSurface.RefreshScene();
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
                this.m_oldFactorBounds = this.Element.GetBound ();
                this.m_oldDisplayBounds = this.CurrentSurface.GetScreenBound(this.m_oldFactorBounds);
                this.Element.SuspendLayout();
                this.State = ST_RESIZING;
            }
            protected void UpdateResize(CoreMouseEventArgs e, bool temp)
            {
                if (this.Element == null)
                    return;
                if (this.Element.Locked)
                    return;
                float ex = 1.0f;
                float ey = 1.0f;
                Vector2f point = e.Location;// e.FactorPoint;
                //seput end point
                SetUpPoint(point);
                Vector2f d = CoreMathOperation.GetDistanceP(EndPoint, StartPoint);
                //Rectanglef vrect = CoreMathOperation.GetBounds(
                //    this.StartPoint,
                //    this.EndPoint);
                //transform  according to display
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
                        locx = d.X;
                        locy = d.Y;
                        break;
                    case RS_BOTTOMLEFT:
                        #region
                        //ok
                        w = this.m_oldDisplayBounds.Width - d.X;
                        h = this.m_oldDisplayBounds.Height + d.Y;
                        locx = d.X;
                        #endregion
                        break;
                    case RS_TOPRIGHT:
                        w = this.m_oldDisplayBounds.Width + d.X;
                        h = this.m_oldDisplayBounds.Height - d.Y;
                        locy = d.Y;
                        break;
                    case RS_BOTTOMRIGHT:
                        w = this.m_oldDisplayBounds.Width + d.X;
                        h = this.m_oldDisplayBounds.Height + d.Y;
                        break;
                }
                Rectanglef r = Rectanglef.Empty;
                Vector2f[] locpt = new Vector2f[] { new Vector2f(locx, locy) };
                Matrix vm = this.Element.GetDocumentMatrix();
                if (vm.IsInvertible)
                    vm.Invert();
                vm.TransformVectors(locpt);
                Vector2f ct = locpt[0];
                ct = CurrentSurface.GetFactorLocation(ct);
                r.Location = this.m_oldFactorBounds.Location;
                ex *= w / this.m_oldDisplayBounds.Width;
                ey *= h / this.m_oldDisplayBounds.Height;
                r.X += (locpt[0].X / this.CurrentSurface.ZoomX);
                r.Y += (locpt[0].Y / this.CurrentSurface.ZoomY);
                vm.Dispose();
                    this.Element.Scale(
                        ex, ey,
                        this.m_oldFactorBounds,
                        r.Location,
                        enuMatrixOrder.Append, temp);
                    this.Invalidate();
            }
            protected void EndResize(CoreMouseEventArgs e)
            {
                if (this.Element == null)
                    return;
                if (!this.Element.Locked)
                {                
                    UpdateResize(e, false);
                    this.Element.ResumeLayout();
                    this.InitSnippetsLocation();
                }
                m_rsMode = -1;
                if (this.State == ST_RESIZING)
                    this.State = ST_EDITING;
                this.CurrentSurface.Cursor = this.GetCursor();
                
                this.Invalidate();
            }
            private void SetUpPoint(Vector2f point)
            {
                Vector2f stP = this.StartPoint;
                Vector2f enP = this.EndPoint;
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
                        this.EndPoint = new Vector2f(point.X,
                           this.m_oldDisplayBounds.Y + this.m_oldDisplayBounds.Height);
                        break;
                    case RS_MIDRIGHT:
                        stP.Y = this.m_oldDisplayBounds.Y;
                        //this.StartPoint.Y =this.oldBounds.Y;
                        this.StartPoint = stP;
                        this.EndPoint = new Vector2f(point.X,
                           this.m_oldDisplayBounds.Y + this.m_oldDisplayBounds.Height);
                        break;
                    case RS_BOTTOMMID:
                    case RS_TOPMID:
                        stP.X = this.m_oldDisplayBounds.X;
                        //this.StartPoint.X =this.oldBounds.X;
                        this.StartPoint = stP;
                        this.EndPoint = new Vector2f(
                           this.m_oldDisplayBounds.X + this.m_oldDisplayBounds.Width,
                            point.Y);
                        break;
                }
            }
            //protected override void OnKeyPress(KeyPressEventArgs e)
            //{
            //    switch (e.KeyChar)
            //    {
            //        case (char)0x1B://escape
            //            if (this.IsMulti)
            //            {
            //                this.CurrentLayer.Select(null);                            
            //            }
            //            else
            //            {
            //                //don't go to any tool
            //                if (this.Selected)
            //                    this.CurrentLayer.Select(null);
            //            }                        
            //            e.Handled = true;
            //            break;
            //    }
            //    base.OnKeyPress(e);
            //}
            protected void CancelMoving()
            {
                //this.Element.Invalidate(false);
                if (this.IsMulti)
                {
                    Core2DDrawingLayeredElement l = null;
                    for (int i = 0; i < this.m_SelectedElements.Length; i++)
                    {
                        l = this.m_SelectedElements[i] as Core2DDrawingLayeredElement;
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

