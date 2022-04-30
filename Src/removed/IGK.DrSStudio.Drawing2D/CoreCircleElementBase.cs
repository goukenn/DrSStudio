

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreCircleElementBase.cs
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
file:CoreCircleElementBase.cs
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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore;using IGK.DrSStudio.Codec;
    using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.Drawing2D.WinUI;
    using IGK.DrSStudio.WinUI.Configuration;
    /// <summary>
    /// represent the base class of circle element
    /// </summary>
    public  abstract class CoreCircleElementBase :
        Core2DDrawingLayeredDualBrushElement,
        ICore2DFillModeElement,
        ICore2DCircleElement
    {
        protected Vector2f m_Center;
        protected float[] m_Radius;
        protected enuFillMode m_FillMode;
        public event EventHandler RadiusChanged;
        protected void OnRadiusChanged(EventArgs e)
        {
            if (this.RadiusChanged != null)
            {
                this.RadiusChanged(this, e);
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
        public Vector2f Center
        {
            get { return this.m_Center; }
            set
            {
                if (!this.m_Center.Equals(value))
                {
                    this.m_Center = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        [System.ComponentModel.TypeConverter(typeof(CoreFloatArrayTypeConverter))]
        public float[] Radius
        {
            get { return this.m_Radius; }
            set
            {
                    this.m_Radius = value;
                    this.OnRadiusChanged(EventArgs.Empty);
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
            }
        }
        public override IGK.DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(IGK.DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            IGK.DrSStudio.WinUI.ICoreParameterConfigCollections p = base.GetParameters(parameters);
            CoreFloatArrayTypeConverter conv = new CoreFloatArrayTypeConverter();
            string v_str = conv.ConvertToString(this.Radius);
            var g = p.AddGroup(CoreConstant.PARAM_DEFINITION);
            g.AddItem("Radius", "lb.Radius.caption",v_str, enuParameterType.Text, RadiusParameterChanged);
            return p;
        }
        private void RadiusParameterChanged(object sender, IGK.DrSStudio.WinUI.Configuration.CoreParameterChangedEventArgs e)
        {
            if (e.Value == null) return;
            string v = e.Value.ToString();
            CoreFloatArrayTypeConverter conv = new CoreFloatArrayTypeConverter();
            try
            {
                float[] t = (float[])conv.ConvertFromString(v);
                if (t.Length > 0)
                {
                    this.Radius = t;
                }
            }
            catch
            {
            }
        }
        protected override void BuildBeforeResetTransform()
        {
            Matrix m = this.GetMatrix();
            if (m.IsIdentity)
                return;
            this.m_Center  = CoreMathOperation.TransformVector2fPoint(m, new Vector2f[] { this.Center })[0];
        }
        public override void Align(enuCore2DAlignElement alignment, Rectanglef bounds)
        {
            Vector2f v_p = Vector2f.Zero;
            Vector2f v_c = Vector2f.Zero;
            switch (alignment)
            {
                case enuCore2DAlignElement.TopLeft:
                    break;
                case enuCore2DAlignElement.TopMiddle:
                    break;
                case enuCore2DAlignElement.TopRight:
                    break;
                case enuCore2DAlignElement.MiddleLeft:
                    break;
                case enuCore2DAlignElement.Center:
                      v_c = CoreMathOperation.TransformVector2fPoint (GetMatrix(), this.Center)[0];
                    v_p = bounds.Center - v_c ;
                    this.Translate(v_p.X ,v_p.Y , enuMatrixOrder.Append, false);
                    return ;
                case enuCore2DAlignElement.CenterVertical:
                    break;
                case enuCore2DAlignElement.CenterHorizontal:
                    break;
                case enuCore2DAlignElement.MiddleRight:
                    break;
                case enuCore2DAlignElement.BottomLeft:
                    break;
                case enuCore2DAlignElement.BottomMiddle:
                    break;
                case enuCore2DAlignElement.BottomRight:
                    break;
                default:
                    break;
            }
            base.Align(alignment, bounds);
        }
        /// <summary>
        /// reprensent the default mecanism
        /// </summary>
        protected new class Mecanism : Core2DDrawingLayeredDualBrushElement.Mecanism<CoreCircleElementBase>
        {
            protected override void RegisterElementEvent(ICore2DDrawingObject element)
            {
                base.RegisterElementEvent(element);
                if (element is CoreCircleElementBase)
                (element as CoreCircleElementBase).RadiusChanged += new EventHandler(Element_RadiusChanged);
            }
            protected override void UnRegisterElementEvent(ICore2DDrawingObject element)
            {
                if (element is CoreCircleElementBase)
                (element as CoreCircleElementBase).RadiusChanged -= new EventHandler(Element_RadiusChanged);
                base.UnRegisterElementEvent(element);
            }
            void Element_RadiusChanged(object sender, EventArgs e)
            {
                if (this.Element.Radius != null)
                {
                    if ((this.Element.Radius.Length) != (this.RegSnippets.Count - 1))
                    {
                        this.GenerateSnippets();
                    }
                    this.InitSnippetsLocation();
                }
                else {
                    this.DisableSnippet ();
                }
            }
            protected override void OnMouseClick(MouseEventArgs e)
            {
                //switch (e.Button)
                //{
                //    case MouseButtons.Right:
                //        //remvoe snippet & point
                //        if (this.Snippet != null)
                //        {
                //            //remove point
                //            RemovePoint();
                //        }
                //        break;
                //}
            }
            private void RemovePoint()
            {
                if ((this.Element == null) || (this.Element.Radius.Length <= 1))
                    return;
                List<float> v = new List<float>();
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
            protected override void OnMouseDown(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        switch (this.State)
                        {
                            case ST_EDITING:
                            case ST_NONE :
                            case ST_CREATING :
                                if (this.Snippet != null)
                                {
                                    break;
                                }                                
                                this.Element = this.CreateNewElement() as CoreCircleElementBase;
                                if (this.Element != null)
                                {
                                    this.CurrentLayer.Elements.Add(this.Element);
                                    this.CurrentLayer.Select(this.Element);
                                    this.Element.m_Center = e.FactorPoint;
                                    this.Element.m_Radius = new float[] { 1 };
                                    this.Element.InitElement();
                                    this.State = ST_CREATING;
                                }
                                break;
                            default:
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
                    for (int i = 0; i < this.Element.Radius.Length; i++)
                    {
                        AddSnippet(CurrentSurface.CreateSnippet(this, i, i));
                    }
                    //center point
                    AddSnippet(CurrentSurface.CreateSnippet(this, -1, -1));
                }
            }
            protected override void InitSnippetsLocation()
            {
                for (int i = 0; i < this.Element.Radius.Length; i++)
                {
                    if (!this.RegSnippets.Contains(i)) continue;
                    this.RegSnippets[i].Location = CurrentSurface.GetScreenLocation(
                        this.Element.Center +
                        (float)(this.Element.Radius[i] * Math.Sqrt(2.0f) / 2.0f));
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
                                    this.Element.m_Radius[0] = CoreMathOperation.GetDistance(
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
                                      CoreMathOperation.GetDistance(this.Element.m_Center,
                                      e.FactorPoint);
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
                                                List<float> v = new List<float>();
                                                v.AddRange(this.Element.m_Radius);
                                                v.Insert(this.Snippet.Demand,
                                                this.Element.Radius[Snippet.Index]);
                                                this.Element.m_Radius = v.ToArray();
                                                this.State = ST_CONFIGURING;
                                                this.Element.InitElement();
                                                this.CurrentSurface.Invalidate();
                                                this.Snippet.Location = e.Location;
                                            }
                                            else
                                            {
                                                this.Element.m_Radius[this.Snippet.Demand] =
                                                    CoreMathOperation.GetDistance(this.Element.m_Center,
                                                    e.FactorPoint);
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
                                    this.Element.m_Radius[0] = CoreMathOperation.GetDistance(
                                                              this.Element.m_Center, e.FactorPoint);
                                    this.Element.InitElement();
                                    this.Element.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                                    this.CurrentSurface.Invalidate();
                                    this.State = ST_EDITING;
                                    this.GenerateSnippets();
                                    this.InitSnippetsLocation();
                                }
                                break;
                            case ST_EDITING:
                                if (this.Snippet != null)
                                { this.Element.OnPropertyChanged(Core2DDrawingElementPropertyChangeEventArgs.Definition); }
                                this.InitSnippetsLocation();
                                this.State = ST_EDITING;
                                this.CurrentSurface.Invalidate();
                                break;
                            case ST_CONFIGURING:
                                this.GenerateSnippets();
                                this.InitSnippetsLocation();
                                this.State = ST_EDITING;
                                break;
                        }
                        break;
                    case MouseButtons.Right:
                        if (this.Snippet != null)
                        {
                            this.RemovePoint();
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
        }
    }
}

