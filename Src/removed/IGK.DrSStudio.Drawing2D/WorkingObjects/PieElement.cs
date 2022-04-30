

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PieElement.cs
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
file:PieElement.cs
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
using System.Drawing.Imaging;
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore;using IGK.DrSStudio.Drawing2D.WinUI;
    [Core2DDrawingStandardItem ("Pie",
        typeof (Mecanism ))]
    public class PieElement : 
        Core2DDrawingLayeredDualBrushElement ,
        ICore2DPieElement 
    {
        private Vector2f  m_Radius;
        private Vector2f m_Center;
        private float m_StartAngle;
        private float m_SweepAngle;
        [IGK.DrSStudio.Codec.CoreXMLAttribute()]
        public float SweepAngle
        {
            get { return m_SweepAngle; }
            set
            {
                if (m_SweepAngle != value)
                {
                    m_SweepAngle = value;
                    OnPropertyChanged(new Core2DDrawingElementPropertyChangeEventArgs(enu2DPropertyChangedType.DefinitionChanged));
                }
            }
        }
        [IGK.DrSStudio.Codec.CoreXMLAttribute()]
        public float StartAngle
        {
            get { return m_StartAngle; }
            set
            {
                if (m_StartAngle != value)
                {
                    m_StartAngle = value;
                    OnPropertyChanged(new Core2DDrawingElementPropertyChangeEventArgs(enu2DPropertyChangedType.DefinitionChanged));
                }
            }
        }
        [IGK.DrSStudio.Codec.CoreXMLAttribute ()]
        public Vector2f Center
        {
            get { return m_Center; }
            set
            {
                if (!m_Center.Equals (value))
                {
                    m_Center = value;
                    OnPropertyChanged(new Core2DDrawingElementPropertyChangeEventArgs(enu2DPropertyChangedType.DefinitionChanged));
                }
            }
        }
        [IGK.DrSStudio.Codec.CoreXMLAttribute()]
        public Vector2f  Radius
        {
            get { return m_Radius; }
            set
            {
                if (!m_Radius.Equals ( value))
                {
                    m_Radius = value;
                    OnPropertyChanged(new Core2DDrawingElementPropertyChangeEventArgs(enu2DPropertyChangedType.DefinitionChanged));
                }
            }
        }
        public override IGK.DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(IGK.DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            IGK.DrSStudio.WinUI.ICoreParameterConfigCollections p = base.GetParameters(parameters);
            var g = p.AddGroup("Angles");
            g.AddItem(GetType().GetProperty("StartAngle"));
            g.AddItem(GetType().GetProperty("SweepAngle"));
            g = p.AddGroup("Definition");
            g.AddItem(GetType().GetProperty("Center"));
            g.AddItem(GetType().GetProperty("Radius"));
            return p;
        }
        protected override void GeneratePath()
        {
            if (this.Radius.Equals (Vector2f.Zero ))
            {
                this.SetPath(null);
                return;
            }
            CoreGraphicsPath v_path = new CoreGraphicsPath();
            v_path.AddPie(
                Rectanglef.Round (CoreMathOperation.GetBounds(
                this.Center,
                this.Radius.X , this.Radius.Y )),
                this.StartAngle,
                this.SweepAngle);
            this.SetPath(v_path);
        }
        protected override void BuildBeforeResetTransform()
        {
            Matrix m = this.GetMatrix();
            if (m.IsIdentity) return;
            this.m_Center = CoreMathOperation.TransformVector2fPoint(m, this.Center)[0];
        }
        protected new class Mecanism : Core2DDrawingLayeredDualBrushElement.Mecanism
        {
            public new PieElement Element { get { return base.Element as PieElement; } set { base.Element = value; } }
            protected const int DM_CENTER = 1;
            //protected const int DM_RADIUS = 2;
            protected const int DM_STARTANGLE = 3;
            protected const int DM_SWEEPANGLE = 4;
            protected override void OnMouseDown(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {                
                base.OnMouseDown(e);
            }
            protected override void OnMouseMove(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                base.OnMouseMove(e);
            }
            protected override void OnMouseUp(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                base.OnMouseUp(e);
            }
            protected override void GenerateSnippets()
            {
                base.GenerateSnippets();
                AddSnippet(CurrentSurface.CreateSnippet(this, DM_CENTER, DM_CENTER));
                AddSnippet(CurrentSurface.CreateSnippet(this, DM_STARTANGLE , DM_STARTANGLE ));
                AddSnippet(CurrentSurface.CreateSnippet(this, DM_SWEEPANGLE , DM_SWEEPANGLE ));
                //AddSnippet(CurrentSurface.CreateSnippet(this, DM_RADIUS, DM_RADIUS));
            }
            protected override void InitSnippetsLocation()
            {
                base.InitSnippetsLocation();
                this.RegSnippets[DM_CENTER].Location = this.CurrentSurface.GetScreenLocation(this.Element.m_Center);
                //this.RegSnippets[DM_RADIUS].Location = this.CurrentSurface.GetScreenLocation(this.Element.m_Center);
                this.RegSnippets[DM_SWEEPANGLE].Location = this.CurrentSurface.GetScreenLocation(
                    CoreMathOperation .GetPoint (this.Element.m_Center , 
                    this.Element.Radius.X  /2.0f ,
                    this.Element.Radius.Y / 2.0f, 
                    this.Element.m_StartAngle + this.Element.m_SweepAngle ));
                this.RegSnippets[DM_STARTANGLE].Location = this.CurrentSurface.GetScreenLocation(
                     CoreMathOperation.GetPoint(this.Element.m_Center, 
                     this.Element.Radius.X ,
                     this.Element.Radius.Y , this.Element.m_StartAngle));
            }
            protected override void BeginCreateElement(CoreMouseEventArgs e)
            {
                this.StartPoint = e.FactorPoint;
                this.EndPoint = e.FactorPoint;
                this.Element.m_Center = e.FactorPoint;
                this.Element.m_Radius = new Vector2f (1,1);
                this.Element.InitElement();
                this.CurrentSurface.Invalidate();
            }
            protected override void UpdateCreateElement(CoreMouseEventArgs e)
            {
                this.EndPoint = e.FactorPoint;
                if (this.IsShiftKey)
                {
                    this.Element.m_Radius = CoreMathOperation.GetDistanceP(e.FactorPoint, this.Element.m_Center);
                }
                else
                {
                    float v_radius = CoreMathOperation.GetDistance(e.FactorPoint, this.Element.m_Center);
                    this.Element.m_Radius = new Vector2f(v_radius, v_radius);                    
                }
                this.Element.m_StartAngle = 45;
                this.Element.m_SweepAngle = 360;
                this.Element.InitElement();
                this.CurrentSurface.Invalidate();
            }
            protected override void EndCreateElement(CoreMouseEventArgs e)
            {
                this.UpdateCreateElement(e);
                this.Element.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);                             
                this.State = ST_EDITING;
                this.GenerateSnippets();
                this.InitSnippetsLocation();
            }
            protected override void UpdateSnippetElement(CoreMouseEventArgs e)
            {
                float m_angle = 0.0f;
                switch (this.Snippet.Demand)
                {
                    case DM_CENTER :
                        this.Element.m_Center = e.FactorPoint;
                        break;
                    //case DM_RADIUS :
                    //    break;
                    case DM_STARTANGLE :
                        m_angle = CoreMathOperation.GetAngle(
                            this.Element.m_Center,e.FactorPoint) * CoreMathOperation.ConvRdToDEGREE;
                        if (m_angle < 0)
                            m_angle += 360;
                        this.Element.m_StartAngle = m_angle;
                        if (this.IsShiftKey)
                        {
                            this.Element.m_Radius = CoreMathOperation.GetDistanceP(e.FactorPoint, this.Element.m_Center);
                        }
                        else {
                            float v_radius = CoreMathOperation.GetDistance(e.FactorPoint, this.Element.m_Center);
                            this.Element.m_Radius = new Vector2f(v_radius, v_radius);
                        }
                        break;
                    case DM_SWEEPANGLE :
                        m_angle =   CoreMathOperation.GetAngle(
                             this.Element.m_Center, e.FactorPoint) * CoreMathOperation.ConvRdToDEGREE - 
                            this.Element.m_StartAngle ;
                        if (m_angle <0)
                            m_angle += 360;
                        this.Element.m_SweepAngle = m_angle 
                           ;
                        break;
                }
                this.Element.InitElement();
                this.Snippet.Location = e.Location;
                this.CurrentSurface.Invalidate();
            }
        }
    }
}

