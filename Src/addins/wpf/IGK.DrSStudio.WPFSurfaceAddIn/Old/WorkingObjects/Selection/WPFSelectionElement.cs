

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WPFSelectionElement.cs
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
file:WPFSelectionElement.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Drawing2D;
    [WPFSelectionElement("Selection", typeof(Mecanism))]
    public class WPFSelectionElement : WPFElementBase 
    {
        class Mecanism : WPFBaseMecanism
        {
            const int ST_SELECT_SINGLE = 100;
            const int ST_SELECT_MULTI  = ST_SELECT_SINGLE+1;
            int SelectionMode = SL_NONE;
            const int SL_NONE = -1;
            const int SL_ONE = 1;
            const int SL_MULTI = 2;
            int m_rsMode = ST_NONE;
            protected const int RS_TOPLEFT = 0;
            protected const int RS_TOPMID = 1;
            protected const int RS_TOPRIGHT = 2;
            protected const int RS_MIDLEFT = 7;
            protected const int RS_MIDRIGHT = 3;
            protected const int RS_BOTTOMLEFT = 6;
            protected const int RS_BOTTOMMID = 5;
            protected const int RS_BOTTOMRIGHT = 4;
            public Mecanism()
            {
                this.m_Elements = null;
                this.ElementChanged += Mecanism_ElementChanged;
            }
            void Mecanism_ElementChanged(object sender, CoreWorkingElementChangedEventArgs<ICoreWorkingObject> e)
            {                
                if (this.Element != null)
                {
                    this.GenerateSnippets();
                    this.InitSnippetsLocation();
                }
                else {
                    this.RegSnippets.Disabled();
                }
            }
            public new WPFLayeredElement Element
            {
                get { return base.Element as WPFLayeredElement ; }
                set
                {
                    base.Element = value;
                }
            }
            private WPFLayeredElement[] m_Elements;
            public WPFLayeredElement[] Elements
            {
                get { return m_Elements; }            
            }
            protected override void InitSnippetsLocation()
            {
                base.InitSnippetsLocation();
                if (this.Element is WPFTransformableElement)
                {
                    Rectangled rd = (this.Element as WPFTransformableElement).GetTransformBound();
                    Vector2d[] d = rd.GetResizePoints();
                    for (int i = 0; i < 8; i++)
                    {
                        this.RegSnippets[i].Location = d[i];
                    }
                }
            }
            protected override void GenerateSnippets()
            {
                base.GenerateSnippets();
                for (int i = 0; i < 8; i++)
                {
                    this.RegSnippets.Add(i, this.CurrentSurface.CreateSnippet(
                        this, i, ST_RESIZING));
                }         
            }
            protected override void OnMouseDown(IGK.DrSStudio.WPFSurfaceAddIn.WinUI.WPFMouseButtonEventArgs e)
            {
                switch (e.ChangedButton)
                {
                    case System.Windows.Input.MouseButton.Left:
                        this.StartPoint = e.Location;
                        this.EndPoint = e.Location;
                        if (this.Snippet == null)
                        {
                            if ((this.Element != null) && (this.Element.Contains(e.Location)))
                            {
                                if (e.ClickCount == 2)
                                {
                                    this.CurrentSurface.CanvasMouseUp += new IGK.DrSStudio.WPFSurfaceAddIn.WinUI.WPFMouseButtonEventHandler(doubleClic_CanvasMouseUp);
                                    this.State = ST_NONE;
                                    return;
                                }
                                this.BeginMove(e.Location);
                            }
                        }
                        else {
                            switch (this.Snippet.Demand)
                            {
                                case ST_RESIZING :
                                    this.BeginScale(e.Location );
                                    break;
                                case ST_ROTATING :
                                    this.BeginRotate(e.Location);
                                    break;
                            }
                        }
                        break;
                    case System.Windows.Input.MouseButton.Right:
                        break;
                }
            }
            void doubleClic_CanvasMouseUp(object sender, IGK.DrSStudio.WPFSurfaceAddIn.WinUI.WPFMouseButtonEventArgs e)
            {
                this.CurrentSurface.CanvasMouseUp -= doubleClic_CanvasMouseUp;
                if (this.Element != null)
                {
                    IWPFLayeredElement l = this.Element;
                    this.CurrentSurface.CurrentTool =
                        this.Element.GetType();
                    this.CurrentSurface.Mecanism.Edit(l);
                }  
            }
            protected override void OnMouseUp(IGK.DrSStudio.WPFSurfaceAddIn.WinUI.WPFMouseButtonEventArgs e)
            {
                switch (e.ChangedButton)
                {
                    case System.Windows.Input.MouseButton.Left:
                        switch (this.State)
                        {
                            case ST_MOVING:
                                this.EndMove(e.Location);
                                break;
                            case ST_RESIZING:
                                EndScale (e.Location);
                                break;
                            case ST_ROTATING:
                                EndRotate(e.Location);
                                return;
                            default:
                                if (this.SelectSingleElement(e.Location) == false)
                                    this.CurrentLayer.Select(null);
                                break;
                        }
                        break;
                    case System.Windows.Input.MouseButton.Right:
                        if (this.Element != null)
                        {
                            this.EndEdition();
                        }
                        break;
                }
            }
            protected override void OnMouseMove(IGK.DrSStudio.WPFSurfaceAddIn.WinUI.WPFMouseEventArgs e)
            {
                switch (e.LeftButton)
                {
                    case System.Windows.Input.MouseButtonState.Pressed :
                        this.EndPoint = e.Location;
                        switch (this.State)
                        {
                            case ST_MOVING :
                                this.UpdateMove(e.Location);
                                return;
                            case ST_RESIZING:
                                if (this.Element != null)
                                    UpdateResize(e.Location, true);
                                else
                                    this.State = ST_NONE;
                                break;
                            case ST_ROTATING :
                                if (this.Element != null)
                                    UpdateRotate(e.Location, true); 
                                else
                                    this.State = ST_NONE;                               
                                return;
                        }
                        break;
                }
            }
            protected override void UpdateSnippetElement(IGK.DrSStudio.WPFSurfaceAddIn.WinUI.WPFMouseEventArgs e)
            {
            }
            protected override void OnLayerSelectedElementChanged(EventArgs e)
            {
                if (this.CurrentLayer.SelectedElements.Count == 1)
                {
                    this.Element = this.CurrentLayer.SelectedElements[0];
                    this.SelectionMode = SL_ONE;
                }
                else {
                    this.Element = null;
                    this.SelectionMode = SL_NONE;
                }
            }
            #region moving
            void BeginMove(Vector2d location)
            {
                if (this.SelectionMode == SL_NONE)
                    return;
                this.State = ST_MOVING;
                this.StartPoint = location;
                this.EndPoint = location;
                this.RegSnippets.Disabled();
            }
            void UpdateMove(Vector2d location)
            {
                if (this.SelectionMode == SL_NONE)
                    return;
                Vector2d v_s = Vector2d.DistanceP(this.StartPoint , this.EndPoint);
                this.Element.Translate (v_s.X, v_s.Y, IGK.DrSStudio.Drawing2D.enuTransformOrder.Append ,
                    true);
            }
            void EndMove(Vector2d location)
            {
                if (this.SelectionMode == SL_NONE)
                    return;
                Vector2d v_s = Vector2d.DistanceP(this.StartPoint, this.EndPoint);
                this.Element.Translate(v_s.X, v_s.Y, IGK.DrSStudio.Drawing2D.enuTransformOrder.Append,
                    false);                
                this.State = ST_EDITING;
                this.RegSnippets.Enable();
                this.InitSnippetsLocation();
            }
            #endregion
            #region rotating
            void BeginRotate(Vector2d location)
            {
                if (this.SelectionMode != SL_NONE)
                    return;
                this.State = ST_ROTATING ;
                this.RegSnippets.Disabled();
            }
            void UpdateRotate(Vector2d location, bool temp)
            {
                if (this.SelectionMode != SL_ONE )
                    return;
                double angle = Vector2d.GetAngle (this.StartPoint, this.EndPoint);
                this.Element.Rotate (angle * CoreMathOperation.ConvRdToDEGREE , 
                    this.StartPoint ,
                    IGK.DrSStudio.Drawing2D.enuTransformOrder.Append,
                    temp );
            }
            void EndRotate(Vector2d location)
            {
                if (this.SelectionMode == SL_NONE)
                    return;
                UpdateRotate(location, false);
                this.State = ST_EDITING;
                this.RegSnippets.Enable();
                this.InitSnippetsLocation();
            }
            #endregion
            #region scaling
            void BeginScale(Vector2d location)
            {
                if (this.SelectionMode != SL_ONE)
                    return;
                this.State = ST_RESIZING ;
                this.StartPoint = location;
                this.EndPoint = location;
                this.m_rsMode = this.Snippet.Index;
                this.m_oldBounds = this.Element.GetBound();
                this.m_oldDisplayBounds = this.Element.GetTransformBound();
                this.RegSnippets.Disabled();
            }
            void UpdateScale(Vector2d location)
            {
                if (this.SelectionMode != SL_NONE)
                    return;
                UpdateResize(location, true);
            }
            void EndScale(Vector2d location)
            {
                if (this.SelectionMode != SL_ONE)
                    return;
                UpdateResize(location, false );
                this.State = ST_EDITING;
                this.Snippet = null;
                this.RegSnippets.Enable();
                this.InitSnippetsLocation();
            }      
            #endregion
            #region "Resize"
            Rectangled m_oldDisplayBounds;            
            Rectangled m_oldBounds;
            private void SetUpPoint(Vector2d point)
            {
                Vector2d stP = this.StartPoint;
                Vector2d enP = this.EndPoint;
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
                        this.EndPoint = new Vector2d(point.X,
                           this.m_oldDisplayBounds.Y + this.m_oldDisplayBounds.Height);
                        break;
                    case RS_MIDRIGHT:
                        stP.Y = this.m_oldDisplayBounds.Y;
                        this.StartPoint = stP;
                        this.EndPoint = new Vector2d(point.X,
                           this.m_oldDisplayBounds.Y + this.m_oldDisplayBounds.Height);
                        break;
                    case RS_BOTTOMMID:
                    case RS_TOPMID:
                        stP.X = this.m_oldDisplayBounds.X;
                        //this.StartPoint.X =this.oldBounds.X;
                        this.StartPoint = stP;
                        this.EndPoint = new Vector2d(
                           this.m_oldDisplayBounds.X + this.m_oldDisplayBounds.Width,
                            point.Y);
                        break;
                }
            }
            void UpdateResize(Vector2d location, bool temp)
            {
                if (this.Element.Locked)
                    return;
                double ex = 1.0f;
                double ey = 1.0f;
                this.EndPoint = location;
                Vector2d  point = location;
                SetUpPoint(point);
                Vector2d d = CoreMathOperation.GetDistanceP(EndPoint, StartPoint);
                double w = 0.0f;
                double h = 0.0f;
                double locx = 0.0f;
                double locy = 0.0f;
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
                Rectangled r = Rectangled.Empty;
                Vector2d[] locpt = new Vector2d[] { new Vector2d(locx, locy) };
                //System.Windows.Media.Matrix vm = new System.Windows.Media.Matrix();
                //vm.Append ((this.Element as WPFTransformableElement).GetGlobalMatrix());
                //if (vm.Determinant >0)
                //    vm.Invert();
                //locpt.Transform(vm);
                r.Location = this.m_oldBounds.Location;
                ex *= w / this.m_oldDisplayBounds.Width;
                ey *= h / this.m_oldDisplayBounds.Height;
                r.X += (locpt[0].X / this.CurrentSurface.ZoomX);
                r.Y += (locpt[0].Y / this.CurrentSurface.ZoomY);
                if (this.Element != null)
                {
                    this.Element.Scale(ex, ey,                                                
                        enuTransformOrder.Append, 
                        temp);
                }
            }
            #endregion
        }
    }
}

