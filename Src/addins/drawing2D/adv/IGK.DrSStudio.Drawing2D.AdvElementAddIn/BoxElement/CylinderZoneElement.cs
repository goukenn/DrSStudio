

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CylinderZoneElement.cs
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
file:CylinderZoneElement.cs
*/
using System; 
using IGK.ICore; using IGK.ICore.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace IGK.DrSStudio.Drawing2D.BoxElement
{
    using IGK.ICore.Codec;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinUI.Configuration;
    [IGKD2DDrawingAdvancedElement("CylinderZone", typeof(Mecanism), ImageKey = "DE_Cylinder")]
    public sealed class CylinderZoneElement : RectangleElement , ICore2DFillModeElement 
    {
        private enuFillMode  m_FillMode;
        private CoreUnit m_RadialSize;

      
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(20)]
        public CoreUnit RadialSize
        {
            get {
                if (m_RadialSize == null)
                    m_RadialSize = "0%";
                return m_RadialSize; 
            }
            set
            {
                if (m_RadialSize != value)
                {
                    if (value.UnitType == enuUnitType.percent)
                    { 
                        if( (value.Value <0) || (value.Value>100))
                        return;
                    }
                    m_RadialSize = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(enuCrossType.Cross)]
        public enuFillMode  FillMode
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
        public CylinderZoneElement()
        {
            
        }
        protected override void InitializeElement()
        {
            base.InitializeElement();
            this.m_RadialSize = "20%";
        }
        float GetRadialSize()
        {
            float v_w = 0.0f;

            if (RadialSize.UnitType == enuUnitType.percent)
            {
                v_w = this.Bounds.Width * (this.m_RadialSize.Value / 100.0f);
            }
            else
            {
                v_w = ((ICoreUnitPixel)this.m_RadialSize).Value;
            }
            return v_w;
        }
        protected override void InitGraphicPath(CoreGraphicsPath v_p)
        {
            v_p.Reset();
            Rectanglef v_rc = this.Bounds ;
            float v_w = 0.0f;
            v_w = GetRadialSize();
            if (v_rc.IsEmpty)
            {
                return;
            }
            IGK.ICore.Drawing2D.Segments.PathSegment cSegment = null;
            if (v_w > 0)
            {
                cSegment = new IGK.ICore.Drawing2D.Segments.PathSegment();
                cSegment.AddArc(new Rectanglef(v_rc.X, v_rc.Y, v_w, v_rc.Height), 90.0f, 180.0f);
                cSegment.AddArc(new Rectanglef(v_rc.Right - v_w, v_rc.Y, v_w, v_rc.Height), -90.0f, 180.0f);
                cSegment.CloseFigure();
                v_p.AddSegment(cSegment);
                cSegment = new ICore.Drawing2D.Segments.PathSegment();
                cSegment.AddArc(new Rectanglef(v_rc.Right - v_w, v_rc.Y, v_w, v_rc.Height), -90.0f, 360.0f);
                v_p.AddSegment(cSegment);
                v_p.FillMode = this.FillMode;
                v_p.CloseFigure();
            }
            else
                v_p.AddRectangle(v_rc);
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            var group = parameters.AddGroup("Default");
            group.AddItem(GetType().GetProperty("FillMode"));
            group.AddItem(GetType().GetProperty("RadialSize"));
            return parameters;
        }
        public new class Mecanism : RectangleElement.Mecanism
        {
            const int SNIPPET_SIDE = 30;
            protected override void GenerateActions()
            {
                base.GenerateActions();
            }
            protected override void GenerateSnippets()
            {
                base.GenerateSnippets();
                this.RegSnippets.Add ( this.CurrentSurface.CreateSnippet (this, SNIPPET_SIDE, SNIPPET_SIDE));
            }
            protected override void InitSnippetsLocation()
            {
                base.InitSnippetsLocation();
                CylinderZoneElement l = this.Element as CylinderZoneElement;
                float v_w = 0.0f;
                v_w = l.GetRadialSize();
                this.RegSnippets[SNIPPET_SIDE].Location
                    = this.CurrentSurface.GetScreenLocation(
                    l.Bounds.MiddleRight - new  Vector2f (v_w,0)
                    );
            }
            protected override void UpdateSnippetEdit(CoreMouseEventArgs e)
            {
                CylinderZoneElement l = this.Element as CylinderZoneElement;
                switch (this.Snippet.Demand)
                { 
                    case SNIPPET_SIDE :
                        if ((l.Bounds.Location .X <= e.FactorPoint.X )&&
                            (l.Bounds.Right >= e.FactorPoint.X ))
                        {

                            float x = l.Bounds.Right - e.FactorPoint.X;
                            float f = (x / l.Bounds .Width )* 100.0f;
                            CoreUnit u = string.Format("{0}{1}", f, "%");
                            l.RadialSize = u;
                        }
                        break;
                }
                base.UpdateSnippetEdit(e);
            }
        }
    }
}

