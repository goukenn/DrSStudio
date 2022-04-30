

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DAdvCardinoideElement.cs
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
file:IGKD2DCardinoideElement.cs
*/

using IGK.ICore.Codec;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Configuration;
using System; using IGK.ICore; using IGK.ICore.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D.Functions
{
    [IGKD2DAdvFunction("Cardinoide", typeof(Mecanism))]
    class IGKD2DAdvCardinoideElement : PolygonElement 
    {
        private float m_StepAngle;//in degree
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(0.0f)]
        public float StepAngle
        {
            get { return m_StepAngle; }
            set
            {
                if ((m_StepAngle != value) && (value >= 0))
                {
                    m_StepAngle = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        public IGKD2DAdvCardinoideElement()
        {
            
        }
        protected override void InitializeElement()
        {
            this.m_StepAngle = 0;
            base.InitializeElement();
        }
        protected override void InitGraphicPath(CoreGraphicsPath path)
        {
            path.Reset();
            List<Vector2f> pts = new List<Vector2f>();
           // const double _2PI = 2 * Math.PI;
           // const double _TOUR = _2PI;
            double step = this.StepAngle * Math.PI / 180.0f;
            //calcur de la progression du rayon
            double r = 0.0f;
            double a = this.Angle * CoreMathOperation.ConvDgToRadian;
            double aa = 0;
            if (step == 0)
            {
                step = Math.PI / 180.0f;
                aa = 1;
            }
            else {
                aa = this.StepAngle;
            }
            Vector2f pt;
            float ex = 0.0f;
            for (int j = 0; j < this.Radius.Length; j++)
            {
                r = this.Radius[j];
                for (double  k = 0; k <= 360; k+= aa)
                {
                    double t = k * CoreMathOperation.ConvDgToRadian ;
                    ex = (float)(1 - Math.Sin(t * this.Count));//t;// Math.Cos(_TOUR - t);
                    pt = new Vector2f(
                        this.Center.X + (float)((r * ex) * Math.Cos(t + a)),
                        this.Center.Y + (float)((r * ex) * Math.Sin(t + a))
                        );
                    pts.Add(pt);
                }
                if (pts.Count > 2)
                {
                    if (EnableTension)
                    {
                        path.AddClosedCurve(pts.ToArray(), this.Tension);
                    }
                    else
                    {
                        path.AddClosedCurve(pts.ToArray());//, 0.5f);
                    }
                }
                    //.CoreMathOperation/path.AddPolygon(pts.ToArray());
            }
            path.FillMode = FillMode;
        }

        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            parameters.AddGroup(CoreConstant.PARAM_DEFINITION).AddItem(GetType().GetProperty("StepAngle"));
            return parameters;
        }
        new class Mecanism : PolygonElement.Mecanism
        {
        }
    }
}

