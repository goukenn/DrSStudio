

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: EllipseElement.cs
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
file:EllipseElement.cs
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
    using IGK.ICore;using IGK.DrSStudio.Codec;
    using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio.Drawing2D.WinUI;
    [Core2DDrawingStandardItem("Ellipse",
       typeof(Mecanism),
       Keys = Keys.E)]
    public class EllipseElement :
        Core2DDrawingLayeredDualBrushElement,
        ICore2DFillModeElement,
        ICore2DCircleModelElement
    {
        private Vector2f[] m_Radius;
        private Vector2f m_Center;
        private enuFillMode m_FillMode;
        private enuCircleModel m_Model;
        public EllipseElement()
        {
            this.m_Model = enuCircleModel.Ellipse;
        }
        [CoreXMLDefaultAttributeValue(enuCircleModel.Ellipse)]
        /// <summary>
        /// get or set the circle model
        /// </summary>
        public enuCircleModel Model
        {
            get { return m_Model; }
            set
            {
                if (m_Model != value)
                {
                    m_Model = value;
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
        protected override void BuildBeforeResetTransform()
        {
            Matrix m = this.GetMatrix();
            if (m.IsIdentity) return;
            this.m_Center = CoreMathOperation.TransformVector2fPoint(m, new Vector2f[] { this.m_Center })[0];
        }
        [CoreXMLAttribute()]
        public Vector2f Center
        {
            get { return m_Center; }
            set
            {
                if (!m_Center.Equals(value))
                {
                    m_Center = value;
                }
            }
        }
        [CoreXMLAttribute()]
        [System.ComponentModel.TypeConverter(typeof(Vector2f.Vector2fArrayTypeConverter))]
        public Vector2f[] Radius
        {
            get { return m_Radius; }
            set
            {
                if (m_Radius != value)
                {
                    m_Radius = value;
                    this.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        protected override void GeneratePath()
        {
            CoreGraphicsPath v_p = new CoreGraphicsPath();
            Vector2f v_evector = Vector2f.Zero;
            Vector2f v_svector = Vector2f.Zero;
            for (int i = 0; i < this.Radius.Length; i++)
            {
                v_evector = Center + Radius[i];
                v_svector = Center - Radius[i];
                if (this.m_Model == enuCircleModel.Ellipse )
                     v_p.AddEllipse(Center ,   v_evector);
                else
                    v_p.AddRectangle(CoreMathOperation.GetBounds(v_svector,
                    v_evector));
            }
            v_p.enuFillMode = this.enuFillMode;
            this.SetPath(v_p);
        }
        new class Mecanism : Core2DDrawingLayeredDualBrushElement.Mecanism<EllipseElement>
        {
            protected override void OnMouseClick(MouseEventArgs e)
            {
                switch (e.Button)
                {
                    case MouseButtons.Right :
                        //remvoe snippet & point
                        if (this.Snippet != null)
                        {
                            //remove point
                            RemovePoint();
                        }
                        else
                        {
                            if (this.Element != null)
                                this.EndEdition();
                            else
                                this.GoToDefaultTool();
                        }
                        break;
                }
            }
            protected override void OnMouseDown(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        switch (this.State)
                        {
                            case ST_EDITING:
                            case ST_NONE :                            
                            case ST_CONFIGURING :
                                if (this.Snippet != null)
                                {
                                    //begin edit snippet
                                    UpdateSnippetElement(e);
                                    break;
                                }
                                this.Element = this.CreateNewElement() as EllipseElement;
                                if (this.Element != null)
                                {
                                    this.CurrentLayer.Elements.Add(this.Element);
                                    this.CurrentLayer.Select(this.Element);
                                    this.Element.m_Center = e.FactorPoint;
                                    this.Element.m_Radius = new Vector2f[] { Vector2f.Zero };
                                    this.Element.InitElement();
                                    this.State = ST_CREATING;
                                    this.GenerateSnippets();
                                    this.InitSnippetsLocation();
                                }
                                break;
                        }
                        break;
                }
            }
            protected override void GenerateSnippets()
            {
                base.GenerateSnippets();
                if (this.Element != null)
                {
                    for (int i = 0; i < this.Element.m_Radius.Length; i++)
                    {
                        AddSnippet(CurrentSurface.CreateSnippet(this, i, i));
                    }
                    //center point
                    AddSnippet(CurrentSurface.CreateSnippet(this, -1, -1));
                }
            }
            protected override void InitSnippetsLocation()
            {
                if (this.Element == null) return;
                for (int i = 0; i < this.Element.Radius.Length; i++)
                {
                    this.RegSnippets[i].Location = CurrentSurface.GetScreenLocation(
                        this.Element.Center + this.Element.Radius[i]);
                }
                //center point
                RegSnippets[-1].Location = CurrentSurface.GetScreenLocation(this.Element.Center);
            }
            protected override void OnMouseMove(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        switch (this.State)
                        {
                            case ST_CREATING:
                                {
                                    this.Element.m_Radius[0] = CoreMathOperation.GetDistanceP(
                                        this.Element.m_Center, e.FactorPoint);
                                    this.Element.InitElement();
                                    this.CurrentSurface.Invalidate();
                                }
                                break;
                            case ST_CONFIGURING:
                                if ((this.Snippet != null) && IsShiftKey)
                                {
                                    //update new point
                                    this.Element.m_Radius[this.Snippet.Demand] =
                                      CoreMathOperation.GetDistanceP(
                                      e.FactorPoint,
                                      this.Element.m_Center
                                      );
                                    this.Element.InitElement();
                                    this.CurrentSurface.Invalidate();
                                    this.Snippet.Location = e.Location;
                                }
                                break;
                            case ST_EDITING:
                                if (this.Snippet != null)
                                {
                                    switch (this.Snippet.Demand)
                                    {
                                        case -1: //for center
                                            this.Element.m_Center = e.FactorPoint;
                                            this.Element.InitElement();
                                            this.CurrentSurface.Invalidate();
                                            break;
                                        default:
                                            if (this.IsShiftKey)
                                            {
                                                //add new point 
                                                List<Vector2f> v = new List<Vector2f>();
                                                v.AddRange(this.Element.m_Radius);
                                                v.Insert(this.Snippet.Demand,
                                                this.Element.m_Radius[Snippet.Index]);
                                                this.Element.m_Radius = v.ToArray();
                                                this.State = ST_CONFIGURING;
                                                this.Element.InitElement();
                                                this.CurrentSurface.Invalidate();
                                                this.Snippet.Location = e.Location;
                                            }
                                            else
                                            {
                                                this.Element.m_Radius[this.Snippet.Demand] =
                                                    CoreMathOperation.GetDistanceP(
                                                    e.FactorPoint,
                                                    this.Element.m_Center
                                                    );
                                                this.Element.InitElement();
                                                this.Snippet.Location = e.Location;
                                                this.CurrentSurface.Invalidate();
                                            }
                                            break;
                                    }
                                }
                                break;
                        }
                        break;
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
                                {
                                    this.Element.m_Radius[0] = CoreMathOperation.GetDistanceP(
                                        e.FactorPoint ,
                                        this.Element.m_Center);
                                    this.Element.InitElement();
                                    this.CurrentSurface.Invalidate();
                                    this.State = ST_EDITING;
                                    this.InitSnippetsLocation();
                                }
                                break;
                            case ST_EDITING:
                                this.InitSnippetsLocation();
                                this.State = ST_EDITING;
                                break;
                            case ST_CONFIGURING:
                                this.GenerateSnippets();
                                this.InitSnippetsLocation();
                                this.State = ST_EDITING;
                                break;
                        }
                        break;
                    case MouseButtons.Right :
                        if (this.Snippet != null)
                        {
                            RemovePoint();
                        }
                        break;
                }
            }
            private void RemovePoint()
            {
                if (this.Element.Radius.Length <= 1)
                    return;
                List<Vector2f> v = new List<Vector2f>();
                v.AddRange(this.Element.m_Radius);
                v.RemoveAt(this.Snippet.Demand);
                this.Element.m_Radius = v.ToArray();
                this.State = ST_EDITING;
                this.Element.InitElement();
                this.CurrentSurface.Invalidate();
                this.GenerateSnippets();
                this.InitSnippetsLocation();
                this.Snippet = null;
            }
        }
    }
}

