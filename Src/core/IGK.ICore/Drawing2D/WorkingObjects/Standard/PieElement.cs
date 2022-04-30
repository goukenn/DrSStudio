

/*
IGKDEV @ 2008-2016
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
using IGK.ICore;
using IGK.ICore.Codec;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:PieElement.cs
*/
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Drawing2D
{
        [Core2DDrawingStandardElement("Pie",
     typeof(Mecanism),
     Keys = enuKeys.P)]
    public class PieElement : EllipseElement
    {
        private float m_StartAngle;
        private float m_SweepAngle;

        [CoreXMLAttribute()]
        [CoreConfigurableProperty()]
        public float StartAngle
        {
            get { return m_StartAngle; }
            set
            {
                if (m_StartAngle != value)
                {
                    m_StartAngle = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }

        [CoreXMLAttribute()]
        [CoreConfigurableProperty()]
        public float SweepAngle
        {
            get { return m_SweepAngle; }
            set
            {
                if (m_SweepAngle != value)
                {
                    m_SweepAngle = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
       
        public PieElement():base()
        {
            
        }
        protected override void InitializeElement()
        {
            base.InitializeElement();
            this.m_StartAngle = 0;
            this.m_SweepAngle = 270;
        }
            protected override void InitGraphicPath(IGK.ICore.Drawing2D.CoreGraphicsPath path)
            {
                path.Reset();
                Rectanglef v_rc = Rectanglef.Empty;
                for (int i = 0; i < this.Radius.Length; i++)
                {
                    v_rc = new Rectanglef(
                           Center.X - this.Radius[i].X,
                           Center.Y - this.Radius[i].Y,
                           2 * this.Radius[i].X,
                           2 * this.Radius[i].Y);
                    path.AddPie(v_rc,
                        this.StartAngle,
                        this.SweepAngle);
                    //path.AddRectangle(v_rc);
                }
                path.FillMode = this.FillMode;
            }

            public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
            {
                return base.GetParameters(parameters);
            }
            public new class Mecanism : EllipseElement.Mecanism
            {
                protected const int DM_CENTER = 1;
                protected const int DM_STARTANGLE = 2;
                protected const int DM_SWEEPANGLE = 4;
                public new PieElement Element {
                    get {
                        return base.Element as PieElement;
                    }
                }
                protected internal override void GenerateSnippets()
                {
                    this.DisposeSnippet();
                    int i = 0;
                    if (this.Element == null)
                        return;
                    AddSnippet(CurrentSurface.CreateSnippet(this,DM_CENTER, 0));
                    int c = this.Element.Radius.Length;
                    for (i = 0; i < this.Element.Radius .Length; i++)
                    {
                        AddSnippet(CurrentSurface.CreateSnippet(this, DM_STARTANGLE, i + 1 ));   
                    }
                    AddSnippet(CurrentSurface.CreateSnippet(this, DM_SWEEPANGLE, i + 1)); 

                    this.RegSnippets[i + 1].Shape =   enuSnippetShape.Diadmond;
                }
                protected internal override void InitSnippetsLocation()
                {
                    int i = 0;
                    this.RegSnippets[0].Location = this.CurrentSurface.GetScreenLocation(this.Element.Center);
                    int c = this.Element.Radius.Length;
                    Vector2f  v_maxradius = CoreMathOperation.GetMax(this.Element.Radius);
                    for (i = 0; i < this.Element.Radius.Length; i++)
                    {
                        this.RegSnippets[i + 1].Location = this.CurrentSurface.GetScreenLocation(
                                        CoreMathOperation.GetPoint(this.Element.Center,
                                        this.Element.Radius[i].X,
                                        this.Element.Radius[i].Y, 
                                        this.Element.m_StartAngle)
                                );
                    }
                    this.RegSnippets[i + 1].Location = this.CurrentSurface.GetScreenLocation(
CoreMathOperation.GetPoint(this.Element.Center,
v_maxradius.X / 2.0f,
v_maxradius.Y / 2.0f,
this.Element.m_StartAngle + this.Element.m_SweepAngle));
                }


                protected override void UpdateDrawing(CoreMouseEventArgs e)
                {
                    this.Element.Center = this.StartPoint;
                    this.EndPoint = e.FactorPoint;
                    if (this.IsShiftKey)
                    {
                        this.Element.Radius[0] = CoreMathOperation.GetDistanceP(this.EndPoint, this.StartPoint);
                    }
                    else
                    {
                        this.Element.Radius[0] = Vector2f.From(CoreMathOperation.GetDistance(this.EndPoint, this.StartPoint));
                        float v_angle = CoreMathOperation.GetAngle(
                           this.Element.Center, e.FactorPoint) * CoreMathOperation.ConvRdToDEGREE;
                        if (v_angle < 0)
                            v_angle += 360;
                        this.Element.m_StartAngle = v_angle;
                    }
                    this.Element.InitElement();
                    this.Invalidate();

                }
                protected override void UpdateSnippetEdit(CoreMouseEventArgs e)
                {
                    float m_angle = 0.0f;
                    PieElement v_e = this.Element;
                    switch (this.Snippet.Demand)
                    {
                        case DM_CENTER :
                            v_e.Center = e.FactorPoint;
                            break;
                        case DM_STARTANGLE :
                            if (Snippet.Index == 1)
                            {//setup start angle when element is index 1
                                m_angle = CoreMathOperation.GetAngle(
                              v_e.Center, e.FactorPoint) * CoreMathOperation.ConvRdToDEGREE;
                                if (m_angle < 0)
                                    m_angle += 360;
                                this.Element.m_StartAngle = m_angle;
                            }
                        if (this.IsControlKey  )
                        {
                            this.Element.Radius[Snippet.Index-1] = CoreMathOperation.GetDistanceP(e.FactorPoint, this.Element.Center);
                        }
                        else {
                            float v_radius = CoreMathOperation.GetDistance(e.FactorPoint, this.Element.Center);
                            v_e.Radius[Snippet.Index-1] = new Vector2f(v_radius, v_radius);
                        }
                            break;
                        case DM_SWEEPANGLE :
                            float pangle  = CoreMathOperation.GetAngle(
                           this.Element.Center, e.FactorPoint) * CoreMathOperation.ConvRdToDEGREE;
                            //expression des angle 
                            if (pangle < 0)
                                pangle += 360;
                            m_angle =  pangle - 
                            this.Element.StartAngle;
                            if (m_angle < 0)
                            {
                                m_angle += 360;
                            }
                            this.Element.m_SweepAngle = m_angle;
                            break;
                        default:
                            break;
                    }
                    v_e.InitElement();
                    this.Snippet.Location = e.Location;
                    this.Invalidate();
                }
                protected override void OnMouseDown(CoreMouseEventArgs e)
                {
                    if (e.Button == enuMouseButtons.Right)
                    {
                        if ((this.Element != null) && ((this.Snippet != null) && (this.Snippet.Demand == DM_STARTANGLE ) && this.Element.Radius.Length > 1))
                        {
                            RemoveRadius(this.Snippet.Index - 1);
                            return;
                        }
                    }
                    base.OnMouseDown(e);
                }
            }
    }
}

