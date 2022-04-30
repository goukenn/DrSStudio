

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CustomPolygonElement.cs
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
file:CustomPolygonElement.cs
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
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore;using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio.Drawing2D.WinUI;
    using IGK.DrSStudio.Codec;
    [Core2DDrawingStandardItem("CustomPolygon",
       typeof(Mecanism),
       Keys = Keys.Shift | Keys.P)]
    public class CustomPolygonElement : Core2DDrawingLayeredDualBrushElement
    {
        private Vector2f[] m_Points;
        private enuFillMode m_FillMode;
        [CoreXMLAttribute()]
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
            this.m_Points =  CoreMathOperation.TransformVector2fPoint(m, this.m_Points );
        }
        public override IGK.DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(IGK.DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            IGK.DrSStudio.WinUI.ICoreParameterGroup group =  parameters.AddGroup("Definition");
            group.AddItem(GetType().GetProperty("enuFillMode"));
            return parameters;
        }
        protected override void GeneratePath()
        {
            if ((this.Points == null) || (this.Points.Length < 2))
            {
                SetPath(null);
                return;
            }
            CoreGraphicsPath v_p = new CoreGraphicsPath();
            if (Points.Length == 2)
                v_p.AddLine(Points[0], Points[1]);
            else
                v_p.AddPolygon(Points);
            v_p.enuFillMode = this.enuFillMode;
            this.SetPath(v_p);
        }
        /// <summary>
        /// represent the default Custom polygon mecanism
        /// </summary>
        protected new class Mecanism : Core2DDrawingLayeredDualBrushElement.Mecanism,
            ICoreHandStyleMecanism 
        {
            private int m_index;
            protected const int ST_INSERTNEWPOINT = ST_CONFIGURING + 100;
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
            public new CustomPolygonElement Element
            {
                get
                {
                    return base.Element as CustomPolygonElement;
                }
                set
                {
                    base.Element = value;
                }
            }
            protected override void OnMouseDown(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        this.EndPoint = e.FactorPoint;
                        this.StartPoint = e.FactorPoint ;
                        if (this.Element == null)
                        {
                            this.Element = CreateNewElement() as CustomPolygonElement;
                            if (this.Element != null)
                            {
                                this.CurrentLayer.Elements.Add(this.Element);
                                this.CurrentLayer.Select(this.Element);
                                //add 2 points
                                this.Element.m_Points = new Vector2f[] { 
                                    e.FactorPoint ,
                                    e.FactorPoint 
                                };
                                this.State = ST_CREATING;
                                this.GenerateSnippets();
                                this.InitSnippetsLocation();
                                this.m_index = 1;
                            }
                        }
                        else
                        {
                            if (this.Snippet == null)
                            {
                                //add new point
                                List<Vector2f> vl = new List<Vector2f>();
                                vl.AddRange(this.Element.Points);
                                vl.Add(e.FactorPoint);
                                this.Element.m_Points = vl.ToArray();
                                this.Element.InitElement();
                                this.m_index = vl.Count - 1;
                                this.State = ST_CREATING;
                                this.CurrentSurface.Invalidate();
                            }
                            else
                            {
                                if (this.IsControlKey)
                                {
                                    //insert new point at index
                                    InsertNewPoint(e);
                                }
                                else
                                {
                                    //move the snippet point
                                    this.m_index = this.Snippet.Demand;
                                    this.State = ST_CREATING;
                                }
                            }
                        }
                        break;
                }
            }
            protected void UpdateIndex(Vector2f fpoint, int index)
            {
                CustomPolygonElement g = this.Element;
                if ((index < 0)|| (index >= g.m_Points.Length ))
                    return;
                if (index == 0)
                {
                    g.m_Points[index] = fpoint;
                    return;
                }
                Vector2f v_c = g.m_Points[index - 1];
                if (this.IsShiftKey)
                {
                    g.m_Points[index] = CoreMathOperation.GetAnglePoint(v_c, fpoint, 45);
                }
                else
                {
                    switch (this.HandStyle)
                    {
                        case enuHandStyle.Horizontal:
                            g.m_Points[index] = new Vector2f(fpoint.X, v_c.Y);
                            break;
                        case enuHandStyle.Vertical:
                            g.m_Points[index] = new Vector2f(v_c.X, fpoint.Y);
                            break;
                        case enuHandStyle.FreeHand:
                        default:
                            g.m_Points[index] = fpoint;
                            break;
                    }
                }
            }
            protected override void OnMouseUp(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        switch (this.State)
                        {
                            case ST_CREATING:
                                UpdateCreateElement(e);
                                this.GenerateSnippets();
                                this.InitSnippetsLocation();
                                this.State = ST_EDITING;
                                this.Element.OnPropertyChanged(
                                   Core2DDrawingElementPropertyChangeEventArgs.Definition);
                                break;
                            case ST_INSERTNEWPOINT : 
                            case ST_EDITING:                                
                            case ST_CONFIGURING :
                                if (this.Snippet != null)
                                {
                                    this.m_index = this.Snippet.Index;
                                    UpdateCreateElement(e);
                                    this.Element.OnPropertyChanged(
                                        Core2DDrawingElementPropertyChangeEventArgs.Definition);
                                    this.State = ST_EDITING;
                                }
                                break;
                        }
                        break;
                    case MouseButtons.Right:
                        //remove point, end edition or go to default tool
                        if (this.Element != null)
                        {
                            if (this.Snippet != null)
                                this.RemovePoint();
                            else
                                EndEdition();
                        }
                        else
                        {
                            GoToDefaultTool();
                        }
                        break;
                }
            }
            protected override void OnMouseClick(MouseEventArgs e)
            {
                //do nothing
            }
            private void RemovePoint()
            {
                List<Vector2f> v_p = new List<Vector2f>();
                v_p.AddRange(this.Element.Points);
                if (v_p.Count <= 2)
                    return;
                int index = this.Snippet .Index ;
                if ((index >= 0) && (index < v_p.Count))
                {
                    v_p.RemoveAt(this.Snippet.Index);
                    this.Element.m_Points = v_p.ToArray();
                    this.Element.InitElement();
                    this.CurrentSurface.Invalidate();
                    this.GenerateSnippets();
                    this.InitSnippetsLocation();
                }
            }
            protected override void OnMouseMove(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        switch (this.State)
                        {
                            case ST_CREATING:
                                UpdateCreateElement(e);
                                break;
                            case ST_CONFIGURING :
                            case ST_INSERTNEWPOINT :
                                if (this.Snippet != null)
                                {
                                    this.UpdateSnippetElement(e);
                                }
                                break;                         
                        }
                        break;
                }
            }
            private void InsertNewPoint(CoreMouseEventArgs e)
            {
                this.State = ST_INSERTNEWPOINT; //configure insertion point
                List<Vector2f> vl = new List<Vector2f>();
                vl.AddRange(this.Element.Points);
                int index =this.Snippet .Index+1 ; 
                vl.Insert(index , e.FactorPoint);
                this.Element.m_Points = vl.ToArray();
                int c = this.Element.m_Points.Length -1;
                this.AddSnippet(this.CurrentSurface.CreateSnippet(this, c, c));
                this.UpdateSnippetElement(e);
            }
            protected override void UpdateSnippetElement(CoreMouseEventArgs e)
            {
                switch (this.State)
                { 
                    case ST_INSERTNEWPOINT:                        
                    case ST_CONFIGURING :
                        if (this.Snippet == null)
                            return ;
                        CustomPolygonElement v_l = this.Element;
                        UpdateIndex(e.FactorPoint, this.Snippet.Index);
                        //v_l.m_Points[this.Snippet.Index] = e.FactorPoint;
                        v_l.InitElement();
                        this.CurrentSurface.Invalidate();
                        this.Snippet.Location = e.Location;
                        break;
                }
            }
            protected override void UpdateCreateElement(CoreMouseEventArgs e)
            {
                this.UpdateIndex(e.FactorPoint, m_index);
                this.Element.InitElement();
                this.CurrentSurface.Invalidate();
                if (this.Snippet != null)
                    this.Snippet.Location = e.Location;
            }
            protected override void GenerateSnippets()
            {
                base.GenerateSnippets();
                if (this.Element == null) return;
                for (int i = 0; i < this.Element.Points.Length; i++)
                {
                    AddSnippet(CurrentSurface.CreateSnippet(this, i, i));
                }
            }
            protected override void InitSnippetsLocation()
            {
                if (this.Element == null) return;
                for (int i = 0; (this.RegSnippets.Count == 
                    this.Element .Points .Length )&&
                    (i < this.Element.Points.Length); i++)
                {
                    this.RegSnippets[i].Location = this.CurrentSurface.GetScreenLocation(
                        this.Element.Points[i]);
                }
            }
            protected override void GenerateActions()
            {
                base.GenerateActions();
                IGK.DrSStudio.Drawing2D.Actions.ToogleToVertical v = new IGK.DrSStudio.Drawing2D.Actions.ToogleToVertical();
                this.AddAction(Keys.V, v);
                this.AddAction(Keys.H, v);
                this.AddAction(Keys.F, v);
            }
        }
    }
}

