

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: StarElement.cs
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
file:StarElement.cs
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
    [Core2DDrawingStandardItem("Star",
       typeof(Mecanism),
       Keys = Keys.Shift |  Keys.S)]
    public class StarElement :
        Core2DDrawingLayeredDualBrushElement,        
        ICore2DTensionElement ,
        ICore2DCountElement        
    {
        private Vector2f m_Center;
        private float m_InnerRadius;
        private float m_OuterRadius;
        private float m_Angle;
        private float m_OffsetAngle;
        private int m_Count;
        private enuFillMode m_FillMode;
        public StarElement()
        {
            this.m_Count = 5;
            this.m_FillMode = enuFillMode.Alternate;
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
                    return;                    
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
        [CoreXMLDefaultAttributeValue(5)]
        public int Count
        {
            get { return m_Count; }
            set
            {
                if (m_Count != value)
                {
                    m_Count = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        public override IGK.DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(IGK.DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            IGK.DrSStudio.WinUI.ICoreParameterConfigCollections p = base.GetParameters(parameters);
            IGK.DrSStudio.WinUI.ICoreParameterGroup group = p.AddGroup("Definition");
            group.AddItem(this.GetType().GetProperty ("Count"));
            group.AddItem(this.GetType().GetProperty("enuFillMode"));
            group.AddItem(this.GetType().GetProperty("EnableTension"));
            group.AddItem(this.GetType().GetProperty("Tension"));
            return p;
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue (0.0f)]
        public float OffsetAngle
        {
            get { return m_OffsetAngle; }
            set
            {
                if (m_OffsetAngle != value)
                {
                    m_OffsetAngle = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(0.0f)]
        public float Angle
        {
            get { return m_Angle; }
            set
            {
                if (m_Angle != value)
                {
                    m_Angle = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        public float OuterRadius
        {
            get { return m_OuterRadius; }
            set
            {
                if (m_OuterRadius != value)
                {
                    m_OuterRadius = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        public float InnerRadius
        {
            get { return m_InnerRadius; }
            set
            {
                if (m_InnerRadius != value)
                {
                    m_InnerRadius = value;
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
        private bool m_EnabledTension;     
        [CoreXMLAttribute()]
        public bool EnableTension
        {
            get { return m_EnabledTension; }
            set
            {
                if (m_EnabledTension != value)
                {
                    m_EnabledTension = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        private float m_Tension;
        [CoreXMLAttribute()]
        public float Tension
        {
            get { return m_Tension; }
            set
            {
                if (m_Tension != value)
                {
                    m_Tension = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        protected override void BuildBeforeResetTransform()
        {
            Matrix mp = this.Matrix;
            if (mp.IsIdentity) return;
            this.m_Center = CoreMathOperation.TransformVector2fPoint(mp, new Vector2f[] { this.m_Center })[0];
        }
        protected override void GeneratePath()
        {
            int p = this.Count * 2;            
            Vector2f[] vtab = new Vector2f[p];
            float step = (float)((360 / (float)p) * CoreMathOperation .ConvDgToRadian) ;
            float vangle = (float)(this.Angle * CoreMathOperation.ConvDgToRadian);
            float v_offAngle = (float)(this.OffsetAngle * CoreMathOperation.ConvDgToRadian);
            for (int i = 0; i < vtab.Length; i++)
            {
                if ((i % 2) == 0)
                {
                    //for inner radius
                    vtab[i] = new Vector2f(
                        (float)(this.Center.X + this.InnerRadius * Math.Cos(i * step + vangle + v_offAngle)),
                        (float)(Center.Y + this.InnerRadius * Math.Sin(i * step + vangle + v_offAngle)));
                }
                else
                {
                    vtab[i] = new Vector2f(
                        (float)(Center.X + this.OuterRadius * Math.Cos(i * step + vangle)),
                        (float)(Center.Y + this.OuterRadius * Math.Sin(i * step + vangle)));
                }
            }
            CoreGraphicsPath v_p = new CoreGraphicsPath();
            if (this.EnableTension)
                v_p.AddClosedCurve(vtab, this.Tension);
            else
                v_p.AddPolygon(vtab);
            v_p.enuFillMode = this.enuFillMode;
            this.SetPath(v_p);
        }
        new class Mecanism : Core2DDrawingLayeredDualBrushElement.Mecanism
        {
            const int SN_CENTER = 0;
            const int SN_INNERRADIUS = 1;
            const int SN_OUTERRADIUS = 2;
            public new StarElement Element {
                get { return base.Element as StarElement ; }
                set { base.Element = value; }
            }
            protected override void OnMouseDown(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        if ((this.Element != null) && (this.Snippet != null))
                            break;
                        if ((this.Element == null)||(!this.Element .Contains (e.FactorPoint )))
                        {
                            this.Element = this.CreateNewElement() as StarElement ;
                            if (this.Element != null)
                            {
                                //register element
                                this.CurrentLayer.Elements.Add(this.Element);
                                this.CurrentLayer.Select(this.Element);
                                this.Element.m_Center = e.FactorPoint;
                                this.Element.m_InnerRadius = 0;
                                this.Element.m_OuterRadius = 0;
                                this.State = ST_CREATING;
                            }
                        }
                        break;
                    case MouseButtons.Middle:
                        break;
                    case MouseButtons.None:
                        break;
                    case MouseButtons.Right:
                        break;
                    case MouseButtons.XButton1:
                        break;
                    case MouseButtons.XButton2:
                        break;
                    default:
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
                                UpdateCreateElement(e);
                                this.State = ST_EDITING;
                                this.GenerateSnippets();
                                this.InitSnippetsLocation();
                                break;
                            case ST_EDITING :
                                if (this.Snippet != null)
                                {
                                    UpdateSnippetElement(e);
                                    this.Element.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                                }
                                break;
                            default :
                                this.InitSnippetsLocation();
                                break;
                        }
                        break;
                }
            }
            protected override void OnMouseMove(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left :
                        switch (this.State)
                        {
                            case ST_CREATING :
                                UpdateCreateElement(e);
                                break;
                            case ST_EDITING :
                                if (this.Snippet != null)
                                {
                                    UpdateSnippetElement(e);                                    
                                }
                                break;
                        }
                        break;
                }
            }
            protected override void UpdateCreateElement(CoreMouseEventArgs e)
            {
                float v_rad = CoreMathOperation.GetDistance(e.FactorPoint, this.Element.m_Center);
                Vector2f v_f = CoreMathOperation.GetDistanceP(e.FactorPoint, this.Element.m_Center);
                this.Element.m_InnerRadius = v_rad / 2.0f;
                this.Element.m_OuterRadius = v_rad;
                this.Element.m_Angle =
                  CoreMathOperation.GetAngle(this.Element.Center, e.FactorPoint) * CoreMathOperation.ConvRdToDEGREE;              
                this.Element.InitElement();
                this.CurrentSurface.Invalidate();
            }
            protected override void UpdateSnippetElement(CoreMouseEventArgs e)
            {
                switch (this.Snippet.Demand)
                {
                    case SN_CENTER :
                        this.Element.m_Center = e.FactorPoint;
                        break;
                    case SN_INNERRADIUS :
                        this.Element.m_InnerRadius = CoreMathOperation.GetDistance(e.FactorPoint, this.Element.Center);
                        this.Element.m_OffsetAngle =
                            CoreMathOperation.GetAngle(this.Element.Center, e.FactorPoint) * CoreMathOperation.ConvRdToDEGREE ;
                        break;
                    case SN_OUTERRADIUS :
                        this.Element.m_OuterRadius  = CoreMathOperation.GetDistance(e.FactorPoint, this.Element.Center);
                        this.Element.m_Angle =
                            CoreMathOperation.GetAngle(this.Element.Center, e.FactorPoint) * CoreMathOperation.ConvRdToDEGREE;
                        break;
                }
                this.Element.InitElement();
                this.Snippet.Location = e.Location;
                this.CurrentSurface.Invalidate();
            }
            protected override void GenerateSnippets()
            {
                base.GenerateSnippets();
                if (this.Element != null)
                {
                    AddSnippet(this.CurrentSurface.CreateSnippet(this, SN_CENTER, SN_CENTER));
                    AddSnippet(this.CurrentSurface.CreateSnippet(this, SN_INNERRADIUS, SN_INNERRADIUS));
                    AddSnippet(this.CurrentSurface.CreateSnippet(this, SN_OUTERRADIUS, SN_OUTERRADIUS ));
                }
            }
            protected override void InitSnippetsLocation()
            {
                base.InitSnippetsLocation();
                if (this.Element != null)
                {
                    this.RegSnippets[SN_CENTER].Location = this.CurrentSurface.GetScreenLocation(this.Element.Center);
                    this.RegSnippets[SN_INNERRADIUS].Location = this.CurrentSurface.GetScreenLocation(
                        this.Element.Center+
                        (float)(this.Element.InnerRadius * Math.Sqrt (2)/2.0f));
                    float angle = this.Element.Angle;
                    this.RegSnippets[SN_OUTERRADIUS].Location = this.CurrentSurface.GetScreenLocation(
                        CoreMathOperation.GetPoint (this.Element.Center , (float)(this.Element.OuterRadius  * Math.Sqrt(2) / 2.0f), angle ));
                }
            }
            protected override void OnKeyPress(KeyPressEventArgs e)
            {
                switch (e.KeyChar)
                {
                    case 'r':
                    case 'R':
                        if (this.Element != null)
                        {
                            //reset angle
                            this.Element.m_OffsetAngle = 0.0f;
                            this.Element.m_Angle = 0.0f;
                            this.Element.InitElement();
                            this.CurrentSurface.Invalidate();
                            e.Handled = true;
                        }
                        break;
                }
                base.OnKeyPress(e);
            }
        }
    }
}

