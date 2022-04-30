

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DAdvSinElementFunction.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Drawing2D;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:IGKD2DSinElementFunction.cs
*/
using IGK.ICore.Drawing2D.Mecanism;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D.Functions
{
    [IGKD2DAdvFunction ("Sin", typeof(Mecanism))]
    class IGKD2DAdvSinElementFunction : RectangleElement 
    {
        private enuFillMode m_FillMode;
        private bool m_Closed;
        private float m_Tension;
        private float m_StepAngle;//in degree
        private float m_Tour;
        private float m_ModuleScale;//
        private float m_OffsetAngle;//in degre
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(0.0f)]
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
        [CoreXMLDefaultAttributeValue(1.0f)]
        public float ModuleScale
        {
            get { return m_ModuleScale; }
            set
            {
                if (m_ModuleScale != value)
                {
                    m_ModuleScale = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(2.0f)]
        public float Tour
        {
            get { return m_Tour; }
            set
            {
                if ((m_Tour != value) && (value != 0.0f))
                {
                    m_Tour = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(1.0f)]
        public float StepAngle
        {
            get { return m_StepAngle; }
            set
            {
                if ((m_StepAngle != value) && (value > 0))
                {
                    m_StepAngle = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
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
        [CoreXMLAttribute()]
        public bool Closed
        {
            get { return m_Closed; }
            set
            {
                if (m_Closed != value)
                {
                    m_Closed = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
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
        public IGKD2DAdvSinElementFunction()
        {
            this.m_ModuleScale = 1.0f;
            this.m_Tour = 2.0f;
            this.m_Tension = 1.0f;
            this.m_StepAngle = 1.0f;
            this.m_OffsetAngle = 0.0f;
        }
        protected override void InitGraphicPath(CoreGraphicsPath p)
        {
            p.Reset();
            List<Vector2f > t = new List<Vector2f>();
            Rectanglef vrect = this.Bounds;
            float x = vrect.X;
            float y = vrect.Y;
            float w = vrect.Width;
            float h = vrect.Height;
            float hH = y + h / 2.0f;
            float amp = (h / 2.0f);
            float xx = 0.0f;
            float yy = 0.0f;
            float tp = m_StepAngle * w / (360.0f * Tour);
            float vangle = (float)(this.m_OffsetAngle * Math.PI / 180f);
            //     t.Add(new Vector2f(x, y + h));
            for (float s = 0; s < w; s += tp)
            {
                xx = x + s;
                yy = hH + (float)(amp * this.m_ModuleScale * Math.Sin((s * Tour * 2 * Math.PI / w) + vangle));
                t.Add(new Vector2f(xx, yy));
            }
            if ((t.Count > 0) && (t[t.Count - 1].X != x + w))
            {
                t.Add(new Vector2f(x + w, t[t.Count - 1].Y));
            }
            //  t.Add(new Vector2f(x + w, y + h));
            if (t.Count < 3)
                return;
            if (this.Closed)
            {
                p.AddClosedCurve(t.ToArray(), Tension);
            }
            else
                p.AddCurve(t.ToArray(), Tension, this.Closed);
            p.FillMode = this.FillMode;
        }
        public override enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            ICoreParameterGroup group = parameters.AddGroup("Definition");
            group.AddItem(GetType().GetProperty("Closed"));
            group.AddItem(GetType().GetProperty("Tension"));
            group.AddItem(GetType().GetProperty("FillMode"));
            group.AddItem(GetType().GetProperty("Tour"));
            group.AddItem(GetType().GetProperty("StepAngle"));
            group.AddItem(GetType().GetProperty("OffsetAngle"));
            return parameters;
        }
        public new class Mecanism : RectangleElement.Mecanism
        {
        }
    }
}

