

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CircleElement.cs
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
file:CircleElement.cs
*/
using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.ComponentModel;
using IGK.ICore.Drawing2D.Mecanism;
using IGK.ICore.Drawing2D.MecanismActions;
using IGK.ICore.Drawing2D.WinUI;
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
    [Core2DDrawingStandardElement ("Circle", typeof (CircleElement.Mecanism),
        Keys= enuKeys.C )]
    public class CircleElement :
        CirclesElementBase, 
        ICore2DCircleElement , 
        ICore2DFillModeElement 
    {
        private enuCircleModel m_Model;
        public CircleElement():base()
        {
        }
        protected override void InitializeElement()
        {
            base.InitializeElement();
        }
        protected override void BuildBeforeResetTransform()
        {
            base.BuildBeforeResetTransform();
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            ICoreParameterConfigCollections p = base.GetParameters(parameters);
            var g = p.AddGroup(CoreConstant.PARAM_DEFINITION);
            CoreFloatArrayTypeConverter conv = new CoreFloatArrayTypeConverter();
            string v_str = conv.ConvertToString(this.Radius);
            g = p.AddGroup(CoreConstant.PARAM_DEFINITION);
            g.AddItem("Radius", "lb.Radius.caption", v_str, enuParameterType.Text, RadiusParameterChanged);
            g.AddItem(GetType().GetProperty("enuFillMode"));
            g.AddItem(GetType().GetProperty("Model"));
            return p;
        }
        private void RadiusParameterChanged(object sender, IGK.ICore.WinUI.Configuration.CoreParameterChangedEventArgs e)
        {
            if (e.Value == null) return;
            string v = e.Value.ToString();
            CoreFloatArrayTypeConverter conv = new CoreFloatArrayTypeConverter();
            try
            {
                float[] t = (float[])conv.ConvertFromString(v);
                if (t.Length > 0)
                {
                    this.Radius = t;
                }
            }
            catch
            {
            }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(enuCircleModel.Circle)]
        /// <summary>
        /// get or set the circle model
        /// </summary>
        public enuCircleModel Model
        {
            get { return m_Model; }
            set
            {
                if (m_Model != value)
                {
                    m_Model = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        protected override void InitGraphicPath(CoreGraphicsPath path)
        {
            path.Reset();
            path.FillMode = this.FillMode;
            if (this.m_Model == enuCircleModel.Circle)
            {
                foreach (float radius in this.Radius)
                {
                    path.AddEllipse(Center, new Vector2f(radius, radius));
                }
            }
            else {
                foreach (float radius in this.Radius)
                {
                    float v_radius = radius;
                    // (float)(radius * Math.Sqrt(2.0f) / 2.0f);
                    path.AddRectangle( 
                        new Rectanglef (
                            Center.X - v_radius,
                            Center.Y - v_radius,
                            2 * v_radius ,
                            2 * v_radius ));
                }
            }
        }
        public new class Mecanism : CirclesElementBase.Mecanims<CircleElement>
        {
            class ToogleCircleModel : CoreMecanismActionBase
            {
                private Mecanism mecanism;
                public ToogleCircleModel(Mecanism mecanism)
                {
                    this.mecanism = mecanism;
                }
                protected override bool PerformAction()
                {
                    if (this.mecanism.Element != null)
                    {
                        if (this.mecanism.Element.Model == enuCircleModel.Circle)
                        {
                            this.mecanism.Element.Model = enuCircleModel.Square;
                        }
                        else {
                            this.mecanism.Element.Model = enuCircleModel.Circle;
                        }
                        this.mecanism.InitSnippetsLocation();
                        this.mecanism.Invalidate();
                    }
                    return true;
                }
            }
            protected override void GenerateActions()
            {
                base.GenerateActions();
                this.AddAction(enuKeys.T, new ToogleCircleModel(this));
            }
            protected internal override void InitSnippetsLocation()
            {
                if (this.RegSnippets.Count == 0)
                    return ;
                this.RegSnippets[0].Location = CurrentSurface.GetScreenLocation(this.Element.Center);
                float[] v_radius = this.Element.Radius;
                enuCircleModel v_model = this.Element.Model;
                for (int i = 0; i < v_radius.Length; i++)
                {
                    if (v_model == enuCircleModel.Circle)
                    {
                        this.RegSnippets[1 + i].Location = CurrentSurface.GetScreenLocation(
                             this.Element.Center +
                            (float)(v_radius[i] * Math.Sqrt(2.0f) / 2.0f));
                    }
                    else
                    {
                        this.RegSnippets[1 + i].Location = CurrentSurface.GetScreenLocation(
            this.Element.Center +
           v_radius[i]);
                    }
                }
            }
            protected override void UpdateSnippetEdit(CoreMouseEventArgs e)
            {
                switch (this.Snippet.Demand)
                {
                    case 0:
                        this.Element.Center = e.FactorPoint;
                        this.Snippet.Location = e.Location;
                        break;
                    case 1:
                        if (this.Element.Model == enuCircleModel.Circle)
                        {
                            this.Element.m_Radius[this.Snippet.Index - 1] = CoreMathOperation.GetDistance(this.Element.m_Center, e.FactorPoint);
                            this.Snippet.Location = e.Location;
                        }
                        else
                        {
                            this.Element.m_Radius[this.Snippet.Index - 1] =
                  (float)(CoreMathOperation.GetDistance(this.Element.Center, e.FactorPoint) * Math.Sqrt(2.0f) / 2.0f);
                            //CoreMathOperation.GetDistance(this.Element.m_Center, e.FactorPoint);
                            this.Snippet.Location = e.Location;
                        }
                        break;
                    default:
                        break;
                }
                this.Element.InitElement();
                this.Invalidate();
            }
        }        
    }
}

