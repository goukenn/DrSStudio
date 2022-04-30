

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PlumeElement.cs
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
file:PlumeElement.cs
*/
using System;
using IGK.ICore;
using System.Collections.Generic;
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore.WinUI;
    using IGK.ICore.GraphicModels;
    using IGK.ICore.Drawing2D;
    using IGK.DrSStudio.Drawing2D.Actions;
    /// <summary>
    /// represent a plume element mecanism tool.Create a path Element with clo
    /// </summary>
    [Core2DDrawingStandardElement ("Plume", typeof (Mecanism))]
    public sealed class PlumeElement : Core2DDrawingDualBrushElement
    {
        protected override void InitGraphicPath(CoreGraphicsPath path)
        {
            path.Reset(); 
        }
        public PlumeElement()
        {
            //don't create directory a plume element used to manage a create a graphics paht
        }
        internal sealed new class Mecanism : Core2DDrawingDualBrushElement.Mecanism
        {
            private PathElement m_pathElement;
            private CoreGraphicsPath m_path;
            private List<Vector2f> m_points;
            private List<byte> m_pointType;
            private int m_index;
            private enuPlumeMode m_Mode;

            /// <summary>
            /// Toggle mode
            /// </summary>
            public enuPlumeMode Mode
            {
                get { return m_Mode; }
                set
                {
                    if (m_Mode != value)
                    {
                        m_Mode = value;
                        OnModeChanged(EventArgs.Empty);
                    }
                }
            }
            private void OnModeChanged(EventArgs eventArgs)
            {
                this.SendHelpMessage(string.Format("PlumeMecanism : {0}, {1} to change Mode".R(this.Mode.ToString(), "M")));
            }
            public Mecanism()
            {
                this.m_index = 0;
                this.m_path = null;
                this.m_points = new List<Vector2f>();
                this.m_pointType = new List<byte>();
            }
            void Clear()
            {
                m_index = 0;
                this.m_points.Clear();
                this.m_pointType.Clear();
            }
            
            //protected override Core2DDrawingLayeredElement CreateNewElement()
            //{
            //    PathElement element = PathElement.Create(this.m_path);
            //    this.InitNewCreateElement(element);
            //    return element;                
            //}
            protected override Core2DDrawingDualBrushElement CreateNewElement()
            {
                PathElement element = new PathElement();
                return element;
            }
            protected override void RegisterElementEvent(Core2DDrawingDualBrushElement element)
            {
                base.RegisterElementEvent(element);
                element.PropertyChanged += Element_PropertyChanged;
            }
            protected override void UnRegisterElementEvent(Core2DDrawingDualBrushElement element)
            {

                element.PropertyChanged -= Element_PropertyChanged;
                base.UnRegisterElementEvent(element);
            }

            protected override void RegisterLayerEvent(ICore2DDrawingLayer layer)
            {
                base.RegisterLayerEvent(layer);
                layer.ElementRemoved += Layer_ElementRemoved;
            }

            protected override void UnRegisterLayerEvent(ICore2DDrawingLayer layer)
            {

                layer.ElementRemoved -= Layer_ElementRemoved;
                base.UnRegisterLayerEvent(layer);
            }


            private void Layer_ElementRemoved(object sender, CoreItemEventArgs<ICore2DDrawingLayeredElement> e)
            {
                if (e.Item == this.Element) {
                    this.Element = null;
                }
            }

            private void Element_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
            {
                this.UpdatePath();
            }

            public override void Render(ICoreGraphics device)
            {
               // render path
            }
            void UpdatePath()
            {
                if (this.m_path == null)
                    return;
                this.m_path.Reset();
                CoreGraphicsPath v_path = new CoreGraphicsPath();
                v_path.AddDefinition (m_points.ToArray (), this.m_pointType.ToArray());
                //v_path.FillMode = (Element as PathElement)?.FillMode ?? enuFillMode.Alternate;
                this.m_path.Add(v_path);

                this.m_path.FillMode = (Element as PathElement)?.FillMode ?? enuFillMode.Alternate;


            }
            protected override void OnMouseDown(CoreMouseEventArgs e)
            {
                this.StartPoint = e.FactorPoint;
                this.EndPoint = e.FactorPoint;
                switch (e.Button)
                {
                    case enuMouseButtons.Left:
                        if (this.Element == null)
                            this.State = ST_NONE;

                        switch (this.State)
                        { 
                            case ST_NONE :
                            case ST_CREATING:
                                this.Clear();
                                DisposePath();
                             
                                this.AddStart(e.FactorPoint );
                                this.m_pathElement = this.CreateNewElement() as PathElement ;
                                if (this.m_pathElement != null)
                                {
                                    //init graphics path
                                    this.m_path = this.m_pathElement.GetPath();
                                    this.m_path.Reset();
                                    this.m_path.AddLine(e.FactorPoint, e.FactorPoint);

                                    this.InitNewCreatedElement(m_pathElement, e.FactorPoint);
                                    this.CurrentLayer.Elements.Add(this.m_pathElement);
                                    this.Element = this.m_pathElement;
                                    this.State = ST_CREATING;
                                    this.m_index = 1;
                                }
                                break;
                            case ST_CONFIGURING :
                            case ST_EDITING :
                                if (this.Snippet != null)
                                    this.UpdateSnippetEdit(e);
                                else
                                {
                                    if (this.Element != null)
                                    {
                                        switch (this.Mode)
                                        {
                                            case enuPlumeMode.AddLine:
                                                this.AddLine(e.FactorPoint);
                                                break;
                                            case enuPlumeMode.AddBezier:
                                                this.AddBezier(e.FactorPoint);
                                                break;
                                        }
                                    }
                                }
                                break;
                        }
                        break;
                }
            }
            private void AddStart(Vector2f point)
            {             
             this.m_points.Add(point);
             this.m_points.Add (point);
             this.m_pointType.Add  ((byte) enuGdiGraphicPathType.StartFigure );
             this.m_pointType.Add((byte)(enuGdiGraphicPathType.LinePoint | enuGdiGraphicPathType.EndPoint));
            }
            public override void Dispose()
            {
                DisposePath();
                base.Dispose();
            }
            private void DisposePath()
            {
                if (this.m_path !=null)
                {
                    this.m_path.Dispose();
                    this.m_path = null;
                }
            }
            protected override void OnMouseMove(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case enuMouseButtons.Left:
                        switch (this.State)
                        { 
                            case ST_CREATING :
                                this.UpdateDrawing(e);
                                this.UpdateIndex(this.m_index, e.FactorPoint);
                                break;
                            case ST_CONFIGURING :
                            case ST_EDITING :
                                if (this.Snippet != null)
                                    this.UpdateSnippetEdit(e);
                                else
                                {
                                    if (this.Element != null)
                                    {
                                        this.UpdateDrawing(e);
                                        this.UpdateIndex(this.m_index, e.FactorPoint);
                                    }
                                }
                                break;
                        }
                        break;
                }
            }
            private void UpdateIndex(int index, Vector2f vector2f)
            {
                if ((index >= 0) && (index < m_points.Count))
                {
                    this.m_points[index] = vector2f;
                    this.UpdatePath();
                    this.Invalidate();
                }
            }
            protected override void OnMouseUp(CoreMouseEventArgs e)
            {
                switch (e.Button)
                { 
                    case enuMouseButtons.Left :
                        switch (this.State)
                        { 
                            case ST_CREATING :
                                this.EndDrawing(e);
                                this.State = ST_EDITING;
                                this.GenerateSnippets();
                                this.InitSnippetsLocation();
                                return;
                            case ST_CONFIGURING :
                            case ST_EDITING:
                                if (this.Snippet == null)
                                {
                                    this.UpdateIndex(this.m_index, e.FactorPoint);
                                }
                                this.State = ST_CONFIGURING;
                                this.GenerateSnippets();
                                this.InitSnippetsLocation();
                                break;
                        }
                        break;
                    case enuMouseButtons .Right :
                        if (this.Snippet != null)
                        {
                            RemoveSnippet();
                        }
                        else {
                            if (this.Element != null)
                                this.EndEdition();
                            else
                                this.GotoDefaultTool();  
                        }
                        break;
                }
            }
            protected override void OnMouseClick(CoreMouseEventArgs e)
            {
               // base.OnMouseClick(e);
            }
            private void RemoveSnippet()
            {
                int index = this.Snippet.Index;
                byte c = 0;
                if ((index >= 0) && (index < this.m_pointType.Count))
                {
                    c = this.m_pointType[index];
                    switch (c)
                    {
                        case (byte)enuGdiGraphicPathType.LinePoint:                       
                            {
                                if (this.m_points.Count > 2)
                                {
                                    this.m_points.RemoveAt(this.Snippet.Index);
                                    this.m_pointType.RemoveAt(this.Snippet.Index);
                                    this.m_index--;
                                    this.UpdatePath();
                                    this.GenerateSnippets();
                                    this.InitSnippetsLocation();
                                }
                            }
                            break;
                        case (byte)(enuGdiGraphicPathType.LinePoint | enuGdiGraphicPathType.EndPoint):
                            {
                                if (this.m_points.Count > 2)
                                {
                                    this.m_points.RemoveAt(this.Snippet.Index);
                                    this.m_pointType.RemoveAt(this.Snippet.Index);
                                    this.m_index--;
                                    this.m_pointType[this.m_pointType.Count - 1] = (byte)(
                             this.m_pointType[this.m_pointType.Count - 1] | (byte)enuGdiGraphicPathType.EndPoint);
                                    this.UpdatePath();
                                    this.GenerateSnippets();
                                    this.InitSnippetsLocation();
                                }
                            }
                            break;
                        case (byte)enuGdiGraphicPathType.ControlPoint:
                            break;
                        case (byte)(enuGdiGraphicPathType.ControlPoint | enuGdiGraphicPathType.EndPoint):
                            this.m_points.RemoveAt(index);
                            this.m_points.RemoveAt(index - 1);
                            this.m_points.RemoveAt(index - 2);
                            this.m_pointType.RemoveAt(index);
                            this.m_pointType.RemoveAt(index - 1);
                            this.m_pointType.RemoveAt(index - 2);
                            this.m_pointType[this.m_pointType.Count - 1] =(byte)(
                                this.m_pointType[this.m_pointType.Count - 1] | (byte)enuGdiGraphicPathType.EndPoint);
                            this.m_index -= 3;
                            this.UpdatePath();
                            this.GenerateSnippets();
                            this.InitSnippetsLocation();
                            break;
                    }
                }
            }
            protected override void GenerateActions()
            {
                base.GenerateActions();
                this.AddAction(enuKeys.M, new PlumeToggleModeAction());
            }
            protected override void GenerateSnippets()
            {
                this.DisposeSnippet();
                int i = 0;
                ICoreSnippet v_snippet = null;
                foreach (Vector2f  item in this.m_points)
                {
                    v_snippet = this.CurrentSurface.CreateSnippet (this, i, i);
                    if (((enuGdiGraphicPathType)m_pointType[i] & enuGdiGraphicPathType.ControlPoint  ) == enuGdiGraphicPathType.ControlPoint )
                    {
                        v_snippet.Shape = enuSnippetShape.Circle;
                    }
                    this.AddSnippet(v_snippet);
                    i++;
                }
            }
            protected override void InitSnippetsLocation()
            {
                if (this.RegSnippets.Count != this.m_points.Count)
                    return;
                for(int i = 0; i < this.RegSnippets.Count ;i++)
                { 
                    this.RegSnippets[i].Location = this.CurrentSurface.GetScreenLocation (this.m_points[i]);
                }
            }
            void AddLine(Vector2f line) {
                this.m_points.Add(line);               
                this.m_pointType.Add((byte)(enuGdiGraphicPathType.LinePoint | enuGdiGraphicPathType.EndPoint));
                if ((this.m_index >= 0) && (m_index < this.m_pointType.Count))
                {
                    byte c = this.m_pointType[m_index];
                    c -= (byte)enuGdiGraphicPathType.EndPoint;
                    this.m_pointType[m_index] = c;
                    this.m_index++;
                }
            }
            void AddBezier(Vector2f line) 
            {
                this.m_points.Add(line);
                this.m_points.Add(line);
                this.m_points.Add(line);
                this.m_pointType.Add((byte)(enuGdiGraphicPathType.ControlPoint ));
                this.m_pointType.Add((byte)(enuGdiGraphicPathType.ControlPoint ));
                this.m_pointType.Add((byte)(enuGdiGraphicPathType.ControlPoint  | enuGdiGraphicPathType.EndPoint));
                byte c = this.m_pointType[m_index];
                c -= (byte)enuGdiGraphicPathType.EndPoint;
                this.m_pointType[m_index] = c;
                this.m_index+=3;
            }
            void TransportLineToBezier(int index)
            { 
            }
            public override void EndEdition()
            {
                if (this.m_pathElement != null)
                {
                    this.m_pathElement.SetDefinition(
                        this.m_points.ToArray(),
                        this.m_pointType.ToArray());
                    this.Clear();
                    this.m_pathElement = null;
                    this.m_path = null;
                    this.DisposePath();
                }
                base.EndEdition();
            }
            protected override void UpdateSnippetEdit(CoreMouseEventArgs e)
            {
                if ((this.Snippet.Index >= 0)&& (this.Snippet.Index < this.m_points.Count))
                {
                    this.m_points[this.Snippet.Index] = e.FactorPoint;
                    this.Snippet.Location = e.Location;
                    this.UpdatePath();
                }
            }
            internal void ToggleMode()
            {
                if (this.Mode == enuPlumeMode.AddLine)
                    this.Mode = enuPlumeMode.AddBezier;
                else
                    this.Mode = enuPlumeMode.AddLine;
            }
        }
    }
}

