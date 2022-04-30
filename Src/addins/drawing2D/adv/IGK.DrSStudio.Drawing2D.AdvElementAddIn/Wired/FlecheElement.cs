

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: FlecheElement.cs
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
file:FlecheElement.cs
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
using System; 
using IGK.ICore;  using IGK.ICore.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace IGK.DrSStudio.Drawing2D.Standard
{
    using IGK.ICore.Codec;
    using IGK.ICore.Drawing2D.WinUI;
    using IGK.ICore.WinUI;
    using IGK.ICore.Actions;
    using IGK.ICore.WinUI.Configuration;
    [LineCorner("Fleche", typeof(Mecanism))]
    public class FlecheElement : Core2DDrawingDualBrushElement 
    {
        private Vector2f m_StartPoint;
        private Vector2f m_EndPoint;
        private float m_BarWidth;
        private float m_AnchorWidth;
        private float  m_AnchorSize;
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(20.0f)]
        public float  AnchorSize
        {
            get { return m_AnchorSize; }
            set
            {
                if ((m_AnchorSize != value)&&((value > 0.0)&&(value <= 100.0f)))
                {
                    m_AnchorSize = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Empty);
                }
            }
        }
        public FlecheElement()
        {
            this.m_BarWidth = 5.0f;
            this.m_AnchorWidth = 10.0f;
            this.m_AnchorSize = 20.0f;
        }
        public override enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            base.GetParameters(parameters);
            ICoreParameterGroup g = parameters.AddGroup("Definition");
            g.AddItem (GetType ().GetProperty ("BarWidth"));
            g.AddItem(GetType().GetProperty("AnchorWidth"));
            g.AddItem(GetType().GetProperty("AnchorSize"));
            return parameters;
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(10.0f)]
        public float AnchorWidth
        {
            get { return m_AnchorWidth; }
            set
            {
                if (m_AnchorWidth != value)
                {
                    m_AnchorWidth = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Empty);
                }
            }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue (5.0f)]
        public float BarWidth
        {
            get { return m_BarWidth; }
            set
            {
                if (m_BarWidth != value)
                {
                    m_BarWidth = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Empty);
                }
            }
        }
        [CoreXMLAttribute ()]
        public Vector2f EndPoint
        {
            get { return m_EndPoint; }
            set
            {
                if (!m_EndPoint.Equals (value))
                {
                    m_EndPoint = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Empty);
                }
            }
        }
        [CoreXMLAttribute()]
        public Vector2f StartPoint
        {
            get { return m_StartPoint; }
            set
            {
                if (!m_StartPoint.Equals (value))
                {
                    m_StartPoint = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Empty);
                }
            }
        }
        protected override void InitGraphicPath(CoreGraphicsPath path)
        {
            path.Reset();
            float d = CoreMathOperation.GetDistance(EndPoint, this.StartPoint);
            float angle = CoreMathOperation.ConvRdToDEGREE *  CoreMathOperation.GetAngle( this.StartPoint,EndPoint);
            float size = this.m_BarWidth;
            float Lsize = this.m_AnchorWidth;
            float hD = d *  (100- this.m_AnchorSize) / 100.0f;
            Vector2f fs = new Vector2f(this.StartPoint.X, this.StartPoint.Y);
            Vector2f ef = new Vector2f(this.StartPoint.X + d, this.StartPoint.Y);
            Vector2f[] v_pts = new Vector2f[] { 
                //top point
                new Vector2f (fs.X  , fs.Y - size),
                new Vector2f (fs.X  , fs.Y + size),
                new Vector2f (fs.X + hD  , fs.Y + size),
                new Vector2f (fs.X + hD  , fs.Y + Lsize ),
                new Vector2f (fs.X + d  , fs.Y ),
                new Vector2f (fs.X + hD  , fs.Y - Lsize ),
                new Vector2f (fs.X + hD  , fs.Y - size)
            };
            Matrix m = new Matrix();
            m.Rotate (-angle, this.StartPoint );
            v_pts = m.TransformPoints(v_pts);
            path.AddPolygon(v_pts);
            m.Dispose();
        }
        protected override void BuildBeforeResetTransform()
        {
            Matrix m = this.GetMatrix();
            if (m.IsIdentity) return;
            Vector2f[] t = new Vector2f []{this.StartPoint, this.EndPoint };
            m.TransformPoints(t);
            this.m_StartPoint = t[0];
            this.m_EndPoint = t[1];
        }
        new class Mecanism : Core2DDrawingDualBrushElement.Mecanism
        {
            public new FlecheElement Element {
                get { return base.Element as FlecheElement ; }
            }
            protected override void BeginDrawing(CoreMouseEventArgs e)
            {
                base.BeginDrawing(e);
                this.Element.m_StartPoint = e.FactorPoint;
                this.Element.m_EndPoint  = e.FactorPoint;
                this.Element.InitElement();
            }
            protected override void UpdateDrawing(CoreMouseEventArgs e)
            {
                this.Element.m_EndPoint = e.FactorPoint;
                this.Element.InitElement();
                this.Invalidate();
            }
            protected override void GenerateSnippets()
            {
                base.GenerateSnippets();
                this.AddSnippet (this.CurrentSurface.CreateSnippet (this, 0,0));
                this.AddSnippet(this.CurrentSurface.CreateSnippet(this, 1, 1));
            }
            protected override void InitSnippetsLocation()
            {
                base.InitSnippetsLocation();
                if ((this.Element != null) && (this.RegSnippets.Count >=2))
                {
                    this.RegSnippets[0].Location = this.CurrentSurface.GetScreenLocation(Element.StartPoint);
                    this.RegSnippets[1].Location = this.CurrentSurface.GetScreenLocation(Element.EndPoint);
                }
            }
            protected override void UpdateSnippetEdit(CoreMouseEventArgs e)
            {
                if (this.Snippet == null)
                    return;
                switch (this.Snippet.Demand)
                {
                    case 0: this.Element.m_StartPoint = e.FactorPoint;
                        break;
                    case 1: this.Element.m_EndPoint = e.FactorPoint;
                        break;
                }
                this.Element.InitElement();
                this.Invalidate();
            }
        }
    }
}

