

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: BezierElement.cs
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
file:BezierElement.cs
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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml;
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore;using IGK.DrSStudio.Codec;
    using IGK.DrSStudio.Drawing2D.WinUI;
    using IGK.DrSStudio.Drawing2D.Actions.Bezier;
    [Core2DDrawingStandardItem("Bezier",
       typeof(Mecanism),
       Keys =  Keys.B
       )]
    public class BezierElement :
        Core2DDrawingLayeredDualBrushElement,
        ICore2DClosableElement,
        ICore2DFillModeElement 
    {
        private bool m_closed;
        private enuFillMode m_FillMode;
        private Vector2f[] m_Points;
        public BezierElement()
        {
            this.m_closed = true;
            this.m_FillMode = enuFillMode;
        }
        [CoreXMLAttribute(true)]
        [CoreXMLDefaultAttributeValue(true)]
        public bool Closed
        {
            get
            {
                return m_closed;
            }
            set
            {
                if (this.m_closed != value)
                {
                    m_closed = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(enuFillMode.Alternate)]
        public enuFillMode enuFillMode
        {
            get { return m_FillMode; }
            set
            {
                if (m_FillMode != value)
                {
                    m_FillMode = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        [System.ComponentModel.TypeConverter(typeof(Vector2f.Vector2fArrayTypeConverter))]
        public Vector2f[] Points
        {
            get { return m_Points; }
            set
            {
                if (m_Points != value)
                {
                    m_Points = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        protected override void BuildBeforeResetTransform()
        {
            Matrix m = this.Matrix;
            if (m.IsIdentity) return;
            this.m_Points = CoreMathOperation.TransformVector2fPoint(m, this.m_Points);
        }
        protected override void GeneratePath()
        {
            if ((this.Points == null) || (this.Points.Length < 4))
            {
                SetPath(null);
                return;
            }
            CoreGraphicsPath v_p = new CoreGraphicsPath();
            Vector2f[] v_tp = new Vector2f[Points.Length];
            //copyp array to point array
            for (int i = 0; i < v_tp.Length; i++)
            {
                v_tp[i] = Points[i];
            }
            if (v_tp.Length == 4)
                v_p.AddBezier(v_tp[0],
                    v_tp[1],
                    v_tp[2],
                    v_tp[3]);
            else
            {
                //v_p.AddBeziers(v_tp);
                for (int i = 0; i < v_tp.Length; i+=3)
                {
                    v_p.AddBezier(v_tp[i+0],
                    v_tp[i+1],
                    v_tp[i+2],
                    v_tp[i+3]);
                }
            }
            if (this.Closed)
                v_p.CloseAllFigures();
            v_p.enuFillMode = this.enuFillMode;
            this.SetPath(v_p);
        }
        public new class Mecanism : Core2DDrawingLayeredDualBrushElement.Mecanism
        {
            private int m_segment;
            private enuBezierPathDrawingMode m_bezierDrawingMode;
            private enuBezierPathControlMode m_controlMode;
            const int DEF_POINT = 1;        //;definition point
            const int DEF_MPOINT = 2;       //;man point
            public enuBezierPathDrawingMode BezierDrawingMode
            {
                get
                {
                    return m_bezierDrawingMode;
                }
                set
                {
                    m_bezierDrawingMode = value;
                }
            }
            public enuBezierPathControlMode ControlMode
            {
                get
                {
                    return m_controlMode;
                }
                set
                {
                    m_controlMode = value;
                }
            }
            public new 
                BezierElement Element
            {
                get
                {
                    return base.Element as BezierElement;
                }
                set
                {
                    base.Element = value;
                }
            }
            public Mecanism()
            {
                m_segment = -1;
                m_bezierDrawingMode = enuBezierPathDrawingMode.singleSegment;
                m_controlMode = enuBezierPathControlMode.Free;
            }
            protected override void GenerateActions()
            {
                base.GenerateActions();
                AddAction(Keys.N, new GoToNextSegmentAction());
                AddAction(Keys.P, new GoToPreviousSegmentAction());
                AddAction(Keys.T, new ToggleControlModeAction());
                AddAction(Keys.R, new ResetSegementAction ());
                AddAction(Keys.M, new BezierToggleViewMode());
            }
            protected override void GenerateSnippets()
            {
                base.GenerateSnippets();
                if (this.m_bezierDrawingMode == enuBezierPathDrawingMode.singleSegment)
                {
                    AddSnippet(CurrentSurface.CreateSnippet(this, DEF_POINT, 0));
                    AddSnippet(CurrentSurface.CreateSnippet(this, DEF_MPOINT, 1));
                    AddSnippet(CurrentSurface.CreateSnippet(this, DEF_MPOINT, 2));
                    AddSnippet(CurrentSurface.CreateSnippet(this, DEF_POINT, 3));
                    this.RegSnippets[1].Shape = IGK.DrSStudio.WinUI.enuSnippetShape.Circle;
                    this.RegSnippets[2].Shape = IGK.DrSStudio.WinUI.enuSnippetShape.Circle;
                }
                else {
                    if (this.Element != null)
                    {
                        Vector2f[] tb = this.Element.Points;
                        for (int i = 0; i < tb.Length ; i++)
			            {
                            if ((i % 3) == 0)
                            {
                                AddSnippet(CurrentSurface.CreateSnippet(this, DEF_POINT, i));
                                this.RegSnippets[i].Shape = IGK.DrSStudio.WinUI.enuSnippetShape.Square;
                            }
                            else
                            {
                                AddSnippet(CurrentSurface.CreateSnippet(this, DEF_MPOINT, i));
                                this.RegSnippets[i].Shape = IGK.DrSStudio.WinUI.enuSnippetShape.Square;
                            }
			            }
                    }
                }
            }
            protected override void InitSnippetsLocation()
            {
                base.InitSnippetsLocation();
                if (this.m_bezierDrawingMode == enuBezierPathDrawingMode.singleSegment)
                {
                    int v_i = GetIndex();
                    if (v_i >= 0)
                    {
                        this.RegSnippets[0].Location = CurrentSurface.GetScreenLocation(this.Element.m_Points[v_i]);
                        this.RegSnippets[1].Location = CurrentSurface.GetScreenLocation(this.Element.m_Points[v_i + 1]);
                        this.RegSnippets[2].Location = CurrentSurface.GetScreenLocation(this.Element.m_Points[v_i + 2]);
                        this.RegSnippets[3].Location = CurrentSurface.GetScreenLocation(this.Element.m_Points[v_i + 3]);
                    }
                }
                else {
                    if (this.Element != null)
                    {
                        Vector2f[] tb = this.Element.Points;
                        for (int i = 0;(this.RegSnippets.Count == tb.Length ) && ( i < tb.Length); i++)
                        {
                            this.RegSnippets[i].Location = CurrentSurface.GetScreenLocation(tb[i]);
                        }
                    }
                }
            }
            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                BezierElement b = this.Element;
                if (b == null)
                    return;
                if (this.m_bezierDrawingMode == enuBezierPathDrawingMode.singleSegment)
                {
                    int v_i = this.GetIndex();
                    if (RegSnippets.Count != 4)
                        return;
                    PointF[] v_pt = new PointF[4];
                    v_pt[0] = RegSnippets[0].Location;
                    v_pt[1] = RegSnippets[1].Location;
                    v_pt[2] = RegSnippets[2].Location;
                    v_pt[3] = RegSnippets[3].Location;
                    e.Graphics.DrawLine(Pens.Pink, 
                        v_pt[0],
                        v_pt[1]);
                    e.Graphics.DrawLine(Pens.Pink,
                        v_pt[2],
                        v_pt[3]);
                    //e.Graphics.DrawLine(Pens.Pink, v_pt[1], v_pt[2]);
                }
                else {
                    Vector2f[] tb = b.m_Points;
                    e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
                    e.Graphics.SmoothingMode = global::System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    for (int i = 1; i < tb.Length; i+=3)
                    {
                        e.Graphics.DrawLine(Pens.Pink, 
                            this.CurrentSurface.GetScreenLocation(tb[i]),
                            this.CurrentSurface.GetScreenLocation(tb[i+1]));    
                    }
                    e.Graphics.SmoothingMode = global::System.Drawing.Drawing2D.SmoothingMode.Default;
                }
            }
            public override void Edit(ICoreWorkingObject element)
            {
                this.m_segment = 0;
                base.Edit(element);
            }
            protected override void OnMouseDown(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        switch (this.State)
                        {
                            case ST_NONE :
                                this.Element = CreateNewElement() as BezierElement;
                                if (this.Element != null)
                                {
                                    this.CurrentLayer.Elements.Add(this.Element);
                                    this.CurrentLayer.Select(this.Element);
                                    this.BeginCreateElement(e);
                                    return;
                                }
                                break;
                            case ST_EDITING :
                                if (this.Snippet != null)
                                {
                                    //begin update snippet
                                }
                                else { 
                                    //no action
                                    AddSegment(e);
                                    return;
                                }
                                break;
                        }
                        break;
                }
                base.OnMouseDown(e);
            }
            protected override void OnMouseUp(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        switch (this.State)
                        {
                            case ST_CREATING:                                
                                if (this.Element != null)
                                {
                                    this.EndCreateElement (e);
                                }
                                break;
                            case ST_EDITING :
                                this.InitSnippetsLocation();
                                break;
                        }
                        break;
                    case MouseButtons.Right:
                        if (this.Snippet != null)
                        {
                            //remove segment
                            this.m_segment -= this.RemoveSegment(this.m_segment);                            
                            this.InitSnippetsLocation();
                            return;
                        }
                        break;
                }
                base.OnMouseUp(e);
            }
            protected override void OnMouseMove(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        switch (this.State)
                        {
                            case ST_CREATING:
                                if (this.Element != null)
                                {
                                    this.UpdateCreateElement(e);
                                }
                                else
                                {
                                    this.State = ST_NONE;
                                }
                                break;
                            case ST_EDITING :
                                if (this.Snippet != null)
                                    this.UpdateSnippetElement(e);
                                else { 
                                    //update new add segment
                                    this.State = ST_CREATING;
                                    this.UpdateCreateElement(e);                                    
                                }
                                break;
                        }
                        break;
                }
            }
            protected override void UpdateSnippetElement(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                BezierElement v_l = this.Element;
                if (this.m_bezierDrawingMode == enuBezierPathDrawingMode.singleSegment)
                {
                    int v_i = GetIndex();
                    if (v_i < 0)
                        return;
                    int hi = this.Snippet.Index;
                    switch (hi)
                    {
                        case 0:
                        case 3:
                            this.Snippet.Location = e.Location;
                            v_l.m_Points[v_i + hi] = e.FactorPoint;
                            this.Element.InitElement();
                            this.CurrentSurface.Invalidate();
                            return;
                    }
                    switch (this.ControlMode)
                    {
                        case enuBezierPathControlMode.StartPoint:
                            {
                                Vector2f def = CoreMathOperation.GetDistanceP(e.FactorPoint, v_l.m_Points[v_i]);
                                v_l.m_Points[v_i + 1] = e.FactorPoint;
                                v_l.m_Points[v_i + 2] = v_l.m_Points[v_i] - def;
                            }
                            break;
                        case enuBezierPathControlMode.EndPoint:
                            {
                                Vector2f def = CoreMathOperation.GetDistanceP(e.FactorPoint, v_l.m_Points[v_i + 3]);
                                v_l.m_Points[v_i + 1] = v_l.m_Points[v_i + 3] + def;
                                v_l.m_Points[v_i + 2] = v_l.m_Points[v_i + 3] - def;
                            }
                            break;
                        case enuBezierPathControlMode.CenterPoint:
                            {
                                Vector2f mid = CoreMathOperation.GetMiddlePoint(v_l.m_Points[v_i],
                                    v_l.m_Points[v_i + 3]);
                                Vector2f def = CoreMathOperation.GetDistanceP(e.FactorPoint, mid);
                                v_l.m_Points[v_i + 1] = e.FactorPoint;
                                v_l.m_Points[v_i + 2] = mid - def;
                            }
                            break;
                        case enuBezierPathControlMode.BothMiddle:
                            v_l.m_Points[v_i + 1] = e.FactorPoint;
                            v_l.m_Points[v_i + 2] = e.FactorPoint;
                            break;
                        case enuBezierPathControlMode.Free:
                        default:
                            this.Element.m_Points[v_i + this.Snippet.Index] = e.FactorPoint;
                            break;
                    }
                    RegSnippets[1].Location = this.CurrentSurface.GetScreenLocation(v_l.m_Points[v_i + 1]);
                    RegSnippets[2].Location = this.CurrentSurface.GetScreenLocation(v_l.m_Points[v_i + 2]);// e.FactorPoint;
                }
                else { 
                    //for all segement
                    this.Snippet.Location = e.Location;
                    v_l.m_Points[this.Snippet.Index] = e.FactorPoint;
                }
                this.Element.InitElement();
                this.CurrentSurface.Invalidate();
            }
            protected override void BeginCreateElement(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                this.State = ST_CREATING;
                this.Element.m_Points = new Vector2f[4];
                this.Element.m_Points[0] = e.FactorPoint;
                this.Element.m_Points[1] = e.FactorPoint;
                this.Element.m_Points[2] = e.FactorPoint;
                this.Element.m_Points[3] = e.FactorPoint;
                this.m_segment = 0;
                this.Element.InitElement();
            }
            protected override void UpdateCreateElement(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                int v_i = GetIndex();
                Vector2f v_mid = CoreMathOperation.GetMiddlePoint(
                    this.Element.m_Points[v_i],
                    e.FactorPoint);
                this.Element.m_Points[v_i+1] = v_mid;
                this.Element.m_Points[v_i+2] = v_mid;
                this.Element.m_Points[v_i+3] = e.FactorPoint;
                this.Element.InitElement();
                this.CurrentSurface.Invalidate();
            }
            /// <summary>
            /// get segment type index
            /// </summary>
            /// <returns></returns>
            private int GetIndex()
            {
                return this.m_segment * 3;
            }
            protected override void EndCreateElement(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                UpdateCreateElement(e);
                this.State = ST_EDITING;
                this.GenerateSnippets();
                this.InitSnippetsLocation();
            }
            internal void GoToPreviousSegment()
            {
                if (m_segment > 0)
                {
                    this.m_segment--;
                    this.InitSnippetsLocation();
                    this.CurrentSurface.Invalidate(this.Element);
                }
            }
            internal void GoToNextSegment()
            {
                int i = GetNumberOfSegment();
                if ((i > 0) && (m_segment < (i - 1)))
                {
                    this.m_segment++;
                    this.InitSnippetsLocation();
                    this.CurrentSurface.Invalidate(this.Element);
                }
            }
            void AddSegment(CoreMouseEventArgs e)
            {
                Vector2f v_end = this.Element .m_Points[this.Element .m_Points.Length -1];
                Vector2f[] v_copy = new Vector2f[this.Element.m_Points.Length + 3];
                Array.Copy(this.Element.m_Points, v_copy, this.Element.m_Points.Length);
                m_segment++;
                v_copy[v_copy.Length - 1] = v_end;
                v_copy[v_copy.Length - 2] = v_end;
                v_copy[v_copy.Length - 3] = v_end;
                this.Element.m_Points = v_copy;
            }
            /// <summary>
            /// remove th current segment
            /// </summary>
            /// <param name="segment"></param>
            /// <returns></returns>
                int RemoveSegment(int segment)
                {
                    if (this.m_bezierDrawingMode != enuBezierPathDrawingMode.singleSegment)
                        return segment;
                    int v_numberOfSegment = GetNumberOfSegment();
                    if (v_numberOfSegment == 1)
                        return 0;
                    if ((segment >= 0) && (segment < v_numberOfSegment))
                    {
                        Vector2f[] v_copy = new Vector2f[this.Element.m_Points.Length - 3];                        
                        List<Vector2f > v_tab = new List<Vector2f> ();
                        v_tab .AddRange (this.Element .m_Points );
                        v_tab.RemoveRange (segment *3+1, 3);
                        this.Element.m_Points = v_tab.ToArray () ;
                        this.Element.InitElement();
                        this.CurrentSurface.Invalidate();
                        return 1;
                    }
                    return 0;
                }
                private int GetNumberOfSegment()
                {
                    return ((this.Element.m_Points.Length - 1) / 3); 
                }
                internal void ResetSegmentAction()
                {
                    BezierElement bl = this.Element;
                    if (bl == null) return;
                    if (this.m_bezierDrawingMode == enuBezierPathDrawingMode.singleSegment )
                    {                        
                        List<Vector2f> v_tab = new List<Vector2f>();
                        v_tab.AddRange(this.Element.m_Points);
                        int i = this.m_segment * 3;
                        PointF c = CoreMathOperation.GetMiddlePoint(v_tab[i], v_tab[i + 3]);
                        v_tab[i + 1] = c;
                        v_tab[i + 2] = c;
                        this.Element.m_Points = v_tab.ToArray();
                        this.Element.InitElement();
                        this.InitSnippetsLocation();
                        this.CurrentSurface.Invalidate();
                    }
                }
                internal void ToogleViewMode()
                {
                    if (this.m_bezierDrawingMode == enuBezierPathDrawingMode.allSegment)
                        this.m_bezierDrawingMode = enuBezierPathDrawingMode.singleSegment;
                    else
                        this.m_bezierDrawingMode = enuBezierPathDrawingMode.allSegment;
                    this.GenerateSnippets();
                    this.InitSnippetsLocation();
                    this.CurrentSurface.Invalidate();
                }
        }
    }
}

