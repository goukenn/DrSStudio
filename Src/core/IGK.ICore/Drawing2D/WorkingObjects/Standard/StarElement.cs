

/*
IGKDEV @ 2008-2016
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
using IGK.ICore;using IGK.ICore.Codec;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:StarElement.cs
*/
using IGK.ICore.ComponentModel;
using IGK.ICore.Drawing2D.Mecanism;
using IGK.ICore.MecanismActions;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Drawing2D
{
    [Core2DDrawingStandardElement("Star", typeof(Mecanism),
        Keys = enuKeys.C | enuKeys.Shift )]
    public class StarElement : Core2DDrawingDualBrushElement   ,
        ICore2DTensionElement ,
        ICore2DFillModeElement 
    {
        private float m_Tension;
        private bool m_EnableTension;
        private float m_Angle;
        private float m_OffsetAngle;
        private enuFillMode m_FillMode;
        private Vector2f m_Center;
        [CoreXMLAttribute]
       [CoreConfigurableProperty(true, Group = CoreConstant.DEFAULT_CONFIGURATION_GROUP)]
        public enuFillMode FillMode
        {
            get { return m_FillMode; }
            set
            {
                if (m_FillMode != value)
                {
                    m_FillMode = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }
        
        [CoreXMLAttribute]
        [CoreConfigurableProperty(true, Group = CoreConstant.DEFAULT_CONFIGURATION_GROUP)]
        [CoreXMLDefaultAttributeValue(0.0f)]
        public float OffsetAngle
        {
            get { return m_OffsetAngle; }
            set
            {
                if (m_OffsetAngle != value)
                {
                    m_OffsetAngle = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }
        [CoreConfigurableProperty(true, Group = CoreConstant.DEFAULT_CONFIGURATION_GROUP)]
        [CoreXMLAttribute]
        [CoreXMLDefaultAttributeValue (0.0f)]
        public float Angle
        {
            get { return m_Angle; }
            set
            {
                if (m_Angle != value)
                {
                    m_Angle = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }
        [CoreXMLAttribute]
        [CoreConfigurableProperty(true, Group = CoreConstant.DEFAULT_CONFIGURATION_GROUP)]
        [CoreXMLDefaultAttributeValue (false )]
        public bool EnableTension
        {
            get { return m_EnableTension; }
            set
            {
                if (m_EnableTension != value)
                {
                    m_EnableTension = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }

        [CoreXMLAttribute]
        [CoreConfigurableProperty(true, Group = CoreConstant.DEFAULT_CONFIGURATION_GROUP)]
        [CoreXMLDefaultAttributeValue(0)]
        /// <summary>
        /// get or set the tension
        /// </summary>
        public float Tension
        {
            get { return m_Tension; }
            set
            {
                if (m_Tension != value)
                {
                    m_Tension = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }
        [CoreXMLAttribute]
        public Vector2f Center
        {
            get { return m_Center; }
            set
            {
                if (m_Center != value)
                {
                    m_Center = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }
        private float m_InnerRadius;
        private float m_OuterRadius;
        private int m_Count;

        [CoreXMLAttribute]
        [CoreConfigurableProperty(true, Group=CoreConstant.DEFAULT_CONFIGURATION_GROUP)]
        public int Count
        {
            get { return m_Count; }
            set
            {
                if ((m_Count != value) && (value >3))
                {
                    m_Count = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }

        [CoreXMLAttribute]
        [CoreConfigurableProperty(true, Group=CoreConstant.DEFAULT_CONFIGURATION_GROUP)]
        public float OuterRadius
        {
            get { return m_OuterRadius; }
            set
            {
                if (m_OuterRadius != value)
                {
                    m_OuterRadius = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }

        [CoreXMLAttribute]
        [CoreConfigurableProperty(true, Group = CoreConstant.DEFAULT_CONFIGURATION_GROUP)]
        public float InnerRadius
        {
            get { return m_InnerRadius; }
            set
            {
                if (m_InnerRadius != value)
                {
                    m_InnerRadius = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }
       
        
        public StarElement()
        {
            
        }
        protected override void InitializeElement()
        {
            base.InitializeElement();
            this.m_Count = 5;
            this.m_Tension = 0.0f;
            this.m_EnableTension = false;
        }
        protected override void BuildBeforeResetTransform()
        {
#pragma warning disable IDE0054 // Use compound assignment
            this.m_Center = this.m_Center * this.Matrix ;
#pragma warning restore IDE0054 // Use compound assignment
            base.BuildBeforeResetTransform();
        }
        protected override void InitGraphicPath(CoreGraphicsPath path)
        {
            path.Reset();
            int p = this.Count * 2;
            Vector2f[] vtab = new Vector2f[p];
            float step = (float)((360 / (float)p) * CoreMathOperation.ConvDgToRadian);
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
                    vtab[i] = new Vector2f (
                        (float)(Center.X + this.OuterRadius * Math.Cos(i * step + vangle)),
                        (float)(Center.Y + this.OuterRadius * Math.Sin(i * step + vangle)));
                }
            }
            if (vtab.Length > 1)
            {
                if (this.EnableTension)
                    path.AddClosedCurve(vtab, this.Tension);
                else
                    path.AddPolygon(vtab);
            }
            path.FillMode = this.FillMode;
        }
        public new class Mecanism : Core2DDrawingSurfaceMecanismBase<StarElement>
        {
            
            class ResetElementMecanismAction : CoreMecanismActionBase
            {
                private Mecanism mecanism;
                public ResetElementMecanismAction(Mecanism mecanism)
                {                    
                    this.mecanism = mecanism;
                }
                protected override bool PerformAction()
                {
                    if (this.mecanism.Element != null)
                    {
                        StarElement c = this.mecanism.Element;
                        c.m_EnableTension = false;
                        c.m_Count = 5;
                        c.m_OffsetAngle = 0.0f;
                        c.m_Angle = 0.0f;
                        c.InitElement();
                        this.mecanism.Invalidate();
                        return true;
                    }
                    return false ;
                }
            }
            const int SN_CENTER = 0;
            const int SN_INNERRADIUS = 1;
            const int SN_OUTERRADIUS = 2;
            protected override void GenerateActions()
            {
                base.GenerateActions();
                this.AddAction(enuKeys.R, new ResetElementMecanismAction(this));
            }
            protected internal override void GenerateSnippets()
            {
                base.GenerateSnippets();
                if (this.Element != null)
                {
                    AddSnippet(this.CurrentSurface.CreateSnippet(this, SN_CENTER, SN_CENTER));
                    AddSnippet(this.CurrentSurface.CreateSnippet(this, SN_INNERRADIUS, SN_INNERRADIUS));
                    AddSnippet(this.CurrentSurface.CreateSnippet(this, SN_OUTERRADIUS, SN_OUTERRADIUS));
                }
            }
            protected internal override void InitSnippetsLocation()
            {
                base.InitSnippetsLocation();
                if ((this.Element != null) && (this.RegSnippets.Count >0))
                {
                    this.RegSnippets[SN_CENTER].Location = this.CurrentSurface.GetScreenLocation(this.Element.Center);
                    this.RegSnippets[SN_INNERRADIUS].Location = this.CurrentSurface.GetScreenLocation(
                        this.Element.Center +
                        (float)(this.Element.InnerRadius * Math.Sqrt(2) / 2.0f));
                    float angle = this.Element.Angle;
                    this.RegSnippets[SN_OUTERRADIUS].Location = this.CurrentSurface.GetScreenLocation(
                        CoreMathOperation.GetPoint(this.Element.Center, (float)(this.Element.OuterRadius * Math.Sqrt(2) / 2.0f), angle));
                }
            }
          
            protected override void BeginDrawing(CoreMouseEventArgs e)
            {
                base.BeginDrawing(e);
                this.Element.m_Center = e.FactorPoint;
                this.Element.m_InnerRadius = 0;
                this.Element.m_OuterRadius = 0;
                this.State = ST_CREATING;
                this.Invalidate();
            }
            protected override void UpdateDrawing(CoreMouseEventArgs e)
            {
                float v_rad = CoreMathOperation.GetDistance(e.FactorPoint, this.Element.m_Center);
                Vector2f v_f = CoreMathOperation.GetDistanceP(e.FactorPoint, this.Element.m_Center);
                this.Element.m_InnerRadius = v_rad / 2.0f;
                this.Element.m_OuterRadius = v_rad;
                this.Element.m_Angle =
                  CoreMathOperation.GetAngle(this.Element.Center, e.FactorPoint) * CoreMathOperation.ConvRdToDEGREE;
                this.Element.InitElement();
                this.Invalidate();
            }
            protected override void EndDrawing(CoreMouseEventArgs e)
            {                
                this.UpdateDrawing(e);
                this.GenerateSnippets();
                this.InitSnippetsLocation();
                this.State = ST_EDITING;
            }
            protected override void BeginSnippetEdit(CoreMouseEventArgs e)
            {
                base.BeginSnippetEdit(e);
            }
            protected override void UpdateSnippetEdit(CoreMouseEventArgs e)
            {
                switch (this.Snippet.Demand)
                {
                    case SN_CENTER:
                        this.Element.m_Center = e.FactorPoint;
                        break;
                    case SN_INNERRADIUS:
                        this.Element.m_InnerRadius = CoreMathOperation.GetDistance(e.FactorPoint, this.Element.Center);
                        this.Element.m_OffsetAngle =
                            CoreMathOperation.GetAngle(this.Element.Center, e.FactorPoint) * CoreMathOperation.ConvRdToDEGREE;
                        break;
                    case SN_OUTERRADIUS:
                        this.Element.m_OuterRadius = CoreMathOperation.GetDistance(e.FactorPoint, this.Element.Center);
                        this.Element.m_Angle =
                            CoreMathOperation.GetAngle(this.Element.Center, e.FactorPoint) * CoreMathOperation.ConvRdToDEGREE;
                        break;
                }
                this.Element.InitElement();
                this.Snippet.Location = e.Location;
                this.Invalidate();
            }
            protected override void EndSnippetEdit(CoreMouseEventArgs e)
            {
                this.UpdateSnippetEdit(e);
                this.InitSnippetsLocation();
            }

            
        }
    }
}

