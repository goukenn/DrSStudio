

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PolygonElement.cs
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
file:PolygonElement.cs
*/
using IGK.ICore;using IGK.ICore.Codec;
using IGK.ICore.Drawing2D.Mecanism;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Drawing2D
{
         [Core2DDrawingStandardElement("Polygon",
        typeof(Mecanism),
        Keys =  enuKeys.H)]
    public class PolygonElement : CirclesElementBase ,
             ICore2DDrawingLayeredElement,
             ICore2DCircleElement ,
             ICore2DTensionElement 
    {
        const int DEFAULT_COUNT = 5;
        private int m_Count;
        private float m_Angle;
        private bool m_EnabledTension;
        private float m_Tension;
        public PolygonElement():base()
        {
            this.m_Count = DEFAULT_COUNT;
            this.m_EnabledTension = false;
            this.m_Tension = 0.0f;            
        }
        protected override void BuildBeforeResetTransform()
        {
            base.BuildBeforeResetTransform();
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(0.0f)]
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
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(false)]
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
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(DEFAULT_COUNT)]
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
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            ICoreParameterConfigCollections p = base.GetParameters(parameters);
            var g = p.AddGroup(CoreConstant.PARAM_DEFINITION);
            g.AddItem(GetType().GetProperty("enuFillMode"));
            g.AddItem(GetType().GetProperty("Closed"));
            g.AddItem(GetType().GetProperty("Count"));
            g.AddItem(GetType().GetProperty("Angle"));
            return p;
        }
             public new class Mecanism :  CirclesElementBase.Mecanims<PolygonElement>
             {
                 protected internal override void InitSnippetsLocation()
                 {
                     if ((this.Element == null) || !(this.RegSnippets.Count >0))
                         return;
                     this.RegSnippets[0].Location = CurrentSurface.GetScreenLocation(this.Element.Center);
                     float a = this.Element.Angle * CoreMathOperation.ConvDgToRadian;
                     float[] v_radius = this.Element.Radius;
                     for (int i = 0; i < v_radius.Length; i++)
                     {
                             this.RegSnippets[1 + i].Location = CurrentSurface.GetScreenLocation(
                                  this.Element.Center +
                                           new Vector2f (
                           (float)(v_radius[i] * Math.Cos (a)),
                           (float)(v_radius[i]* Math.Sin (a))));
                                 //(float)(v_radius[i] * Math.Sqrt(2.0f) *
                                 //this.Element.Angle * CoreMathOperation.ConvDgToRadian / 2.0f));
                     }
                 }
                 protected override void UpdateDrawing(CoreMouseEventArgs e)
                 {
                     this.Element.m_Angle = CoreMathOperation.GetAngle(this.Element.m_Center, e.FactorPoint) * CoreMathOperation.ConvRdToDEGREE;
                     base.UpdateDrawing(e);
                 }
                 protected override void UpdateSnippetEdit(CoreMouseEventArgs e)
                 {
                     //
                     //check for snippet
                     //
                     if (this.Element == null) {
                         Snippet = null;
                         this.State = ST_NONE;
                         return;
                     }

                     switch (this.Snippet.Demand)
                     {
                         case 0:
                             this.Element.Center = e.FactorPoint;
                             this.Snippet.Location = e.Location;
                             break;
                         case 1:
                                 this.Element.m_Radius[this.Snippet.Index - 1] = CoreMathOperation.GetDistance(this.Element.m_Center, e.FactorPoint);
                                 this.Element.m_Angle = CoreMathOperation.GetAngle(this.Element.m_Center, e.FactorPoint) * CoreMathOperation.ConvRdToDEGREE ;
                                 this.Snippet.Location = e.Location;
                             break;
                         default:
                             break;
                     }
                     this.Element.InitElement();
                     this.Invalidate();
                 }
             }
             protected override void InitGraphicPath(CoreGraphicsPath path)
             {
                 path.Reset();
                 if (this.Count >= 3)
                 {
                     Vector2f[] vtab = new Vector2f[this.m_Count];
                     float step = (float)((360 / (float)m_Count) * (Math.PI / 180.0f));
                     float v_angle = (float)(m_Angle * CoreMathOperation.ConvDgToRadian);
                     for (int j = 0; j < this.Radius.Length; j++)
                     {
                         for (int i = 0; i < this.m_Count; i++)
                         {
                             vtab[i] = new Vector2f(
                                 (float)(this.Center.X + this.Radius[j] * Math.Cos(i * step + v_angle)),
                                 (float)(this.Center.Y + this.Radius[j] * Math.Sin(i * step + v_angle)));
                         }
                         if (this.EnableTension)
                         {
                             path.AddClosedCurve(vtab, this.Tension);
                         }
                         else
                             path.AddPolygon(vtab);
                     }
                     path.FillMode = this.FillMode;
                 }
             }
    }
}

