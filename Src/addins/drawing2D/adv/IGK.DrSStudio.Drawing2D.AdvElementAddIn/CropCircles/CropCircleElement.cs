

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CropCircleElement.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿
using IGK.ICore.Drawing2D.Mecanism;
using IGK.ICore.WinUI;
using System; using IGK.ICore; using IGK.ICore.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore.Codec;
    using IGK.ICore.GraphicModels;
    using IGK.ICore.WinUI.Configuration;
    using System.ComponentModel;
    
    /// <summary>
    /// represent a crop circle element system
    /// </summary>
    [Core2DDrawingStandardElement ("Cropcircle", typeof (Mecanism ))]
    public class CropCircleElement :
        Core2DDrawingDualBrushElement ,
        ICore2DFillModeElement 
    {
        private Vector2f m_Center;
        private float m_Radius;
        private int m_Count;
        private float m_CircleRadius;
        private enuFillMode m_FillMode;
        private float m_Angle;

        /// <summary>
        /// .ctr
        /// </summary>
        public CropCircleElement()
        {
        }
        protected override void InitializeElement()
        {
            base.InitializeElement();
            this.m_Count = 4;
            this.m_Center = Vector2f.Zero;
            this.m_Radius = 0.0f;
            this.m_CircleRadius = 4.0f;
            this.m_Angle = 0.0f;
        }

          [
        CoreXMLAttribute(),
        CoreXMLDefaultAttributeValue(0.0f),
        CoreConfigurableProperty()
        ]
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
        
        [
        CoreXMLAttribute (),
        CoreXMLDefaultAttributeValue (4.0f),
        CoreConfigurableProperty ()
        ]
        public float CircleRadius
        {
            get { return m_CircleRadius; }
            set
            {
                if (m_CircleRadius != value)
                {
                    m_CircleRadius = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }


        [Description("number of circle in the crop")]
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue (4)]
        [CoreConfigurableProperty ()]
        public int Count
        {
            get { return m_Count; }
            set
            {
                if (m_Count != value)
                {
                    m_Count = value;

                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }

        [CoreXMLAttribute()]
        [CoreConfigurableProperty ()]
        public float Radius
        {
            get { return m_Radius; }
            set
            {
                if (m_Radius != value)
                {
                    m_Radius = value;

                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }

        [CoreXMLAttribute ()]
        [CoreConfigurableProperty ()]
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
        
        protected override void InitGraphicPath(CoreGraphicsPath path)
        {
            path.Reset();
            if ((this.m_Radius <= 0) || (this.m_CircleRadius <= 0) || (this.m_Count<= 0))
                return;

            float v_stepAngle = CoreMathOperation.ConvDgToRadian * (360.0f/ this.Count );
            float v_offset = CoreMathOperation.ConvDgToRadian * this.m_Angle;
            for (int i = 0; i < this.Count; i++)
			{
                     path.AddEllipse ( new Vector2f (
                this.m_Center.X + (m_Radius * Math.Cos((i * v_stepAngle) + v_offset)),
                this.m_Center.Y + (m_Radius * Math.Sin((i * v_stepAngle) + v_offset))
                ) ,
                new Vector2f (m_CircleRadius, m_CircleRadius ));
			}

            path.FillMode = this.FillMode;

        }
        protected override void BuildBeforeResetTransform()
        {
            //base.BuildBeforeResetTransform();
            Matrix m = this.GetMatrix();
            if (!m.IsIdentity)
            { 
                Vector2f[] tab = new  Vector2f []{
                    this.m_Center
                };
                CoreMathOperation.MultMatrixTransformVector(m, tab);
                this.m_Center = tab[0];
            }
        }
        new class Mecanism : Core2DDrawingSurfaceMecanismBase<CropCircleElement>
        {
            protected override void GenerateSnippets()
            {
                base.GenerateSnippets();
                this.AddSnippet(this.CreateSnippet(0, 0));//center
                this.AddSnippet(this.CreateSnippet(1, 1));//orientation and outer radius
                this.AddSnippet(this.CreateSnippet(2, 2)); //inner circle generator
            }
            protected override void InitSnippetsLocation()
            {
                base.InitSnippetsLocation();
                if (this.RegSnippets.Count >= 3)
                {
                    var e = this.Element;
                    float angle = e.m_Angle * CoreMathOperation.ConvDgToRadian;
                    Vector2f v_def = new Vector2f(
                             (float)(e.Center.X + e.Radius * Math.Cos(angle + Math.PI / 4.0f)),
                             (float)(e.Center.Y + e.Radius * Math.Sin(angle + Math.PI / 4.0f))
                            );
                    this.RegSnippets[0].Location = this.CurrentSurface.GetScreenLocation(e.Center);
                    this.RegSnippets[1].Location = this.CurrentSurface.GetScreenLocation(
                         v_def);
                    this.RegSnippets[2].Location = this.CurrentSurface.GetScreenLocation(
                         new Vector2f(
                             (float)(v_def.X+ e.CircleRadius * Math.Cos(angle + Math.PI / 4.0f)),
                             (float)(v_def.Y + e.CircleRadius * Math.Sin(angle + Math.PI / 4.0f))
                            ));
                }
            }

            public override void Render(ICoreGraphics device)
            {
                base.Render(device);
                var e = this.Element;
                if (e == null) return;

                Rectanglef rc = CurrentSurface.GetScreenBound(CoreMathOperation.GetBounds(e.Center, e.Radius));
                device.DrawEllipse(Colorf.Yellow, rc.X, rc.Y, rc.Width, rc.Height);
            }
            protected override void BeginDrawing(CoreMouseEventArgs e)
            {
                base.BeginDrawing(e);
                this.Element.Center = e.FactorPoint;
            }



            protected override void EndDrawing(CoreMouseEventArgs e)
            {
                base.EndDrawing(e);
            }
            protected override void InitNewCreatedElement(CropCircleElement element, Vector2f defPoint)
            {
                base.InitNewCreatedElement(element,defPoint );
            }

            protected override void GenerateActions()
            {
 	             base.GenerateActions();
            }

            protected override void Move(Vector2f d, bool temp)
            {
 	                base.Move(d, temp);
            }
            protected override void UpdateDrawing(CoreMouseEventArgs e)
            {
                base.UpdateDrawing(e);
                this.EndPoint = e.FactorPoint;
                this.Element.Radius = CoreMathOperation.GetDistance(this.EndPoint, this.StartPoint);
                this.Element.CircleRadius = 40.0f;
                this.Element.InitElement();
                this.Invalidate();
            }
          
            protected override void UpdateSnippetEdit(CoreMouseEventArgs e)
            {
                if (this.Element == null) return;

                var c = this.Element;
                switch (this.Snippet.Demand)
                {
                    case 0:
                        c.m_Center = e.FactorPoint;
                        break;
                    case 1:
                        c.m_Radius = CoreMathOperation.GetDistance(e.FactorPoint, c.Center);
                        c.m_Angle = -45 +
                        (CoreMathOperation.GetAngle(c.Center, e.FactorPoint) * CoreMathOperation.ConvRdToDEGREE);
                        break;
                    case 2:

                        c.m_CircleRadius = Math.Abs (CoreMathOperation.GetDistance(c.Center, e.FactorPoint)-c.Radius) ;
                        break;
                }
                this.Snippet.Location = e.Location;
                c.InitElement();
                this.Invalidate();
            }
            protected override void EndSnippetEdit(CoreMouseEventArgs e)
            {
                base.EndSnippetEdit(e);
            }
        }
    }
}
