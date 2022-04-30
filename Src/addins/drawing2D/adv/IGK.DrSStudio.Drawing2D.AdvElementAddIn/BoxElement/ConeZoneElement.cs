

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ConeZoneElement.cs
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
file:ConeZoneElement.cs
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
    using IGK.ICore.WinUI.Configuration;
    [IGKD2DDrawingAdvancedElement("ConeZone", typeof(Mecanism) , ImageKey = "DE_Cone")]
    public sealed class ConeZoneElement : RectangleElement , ICore2DFillModeElement
    {
        private enuFillMode m_FillMode;
        private CoreUnit  m_RadialSize;
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(20)]
        public CoreUnit RadialSize
        {
            get { return m_RadialSize; }
            set
            {
                if (m_RadialSize != value)
                {
                    if (value.UnitType == enuUnitType.percent)
                    {
                        if ((value.Value < 0) || (value.Value > 100))
                            return;
                    }
                    m_RadialSize = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(enuCrossType.Cross)]
        public enuFillMode FillMode
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
        public ConeZoneElement()
        {
            this.m_RadialSize = "20%";
        }
        float GetRadialSize()
        {
            float v_w = 0.0f;
            CoreUnit rd = m_RadialSize ?? "0px";
            if (rd.UnitType == enuUnitType.percent)
            {
                v_w = this.Bounds.Width * (this.m_RadialSize.Value / 100.0f);
            }
            else
            {
                v_w = ((ICoreUnitPixel)rd).Value;
            }
            return v_w;
        }
        protected override void InitGraphicPath(CoreGraphicsPath p)
        {
            
            Rectanglef v_rc = this.Bounds;
            p.Reset();
            if (v_rc.IsEmpty)
            {
                return;
            }
            float v_w = this.GetRadialSize();
            p.AddArc(new Rectanglef(v_rc.X, v_rc.Y, v_w, v_rc.Height), 90.0f, 180.0f);
            p.AddLine(v_rc.MiddleRight, v_rc.MiddleRight);
            p.CloseFigure();
            p.FillMode = this.FillMode;
            p.CloseFigure();
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
        }
    }
}

