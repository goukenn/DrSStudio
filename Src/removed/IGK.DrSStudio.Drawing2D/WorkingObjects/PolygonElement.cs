

/*
IGKDEV @ 2008 - 2014
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
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:PolygonElement.cs
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
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore;using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio.Drawing2D.WinUI;
    using IGK.DrSStudio.Codec;
    [Core2DDrawingStandardItem("Polygon",
       typeof(Mecanism),
       Keys = Keys.P)]
    /// <summary>
    /// represent a polygon element
    /// </summary>
    public class PolygonElement : CoreCircleElementBase ,
        ICore2DTensionElement 
    {
        private int m_Count;
        private float m_Angle;
        private bool m_EnabledTension;
        private float m_Tension;
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
        [CoreXMLDefaultAttributeValue(false )]
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
        [CoreXMLDefaultAttributeValue(6)]
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
        public override IGK.DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(IGK.DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            IGK.DrSStudio.WinUI.ICoreParameterConfigCollections p = base.GetParameters(parameters);
            var g = p.AddGroup(CoreConstant.PARAM_DEFINITION);
           g.AddItem(GetType().GetProperty("enuFillMode"));
           g.AddItem(GetType().GetProperty("Closed"));
           g.AddItem(GetType().GetProperty("Count"));
           g.AddItem(GetType().GetProperty("Angle"));
           g.AddItem(GetType().GetProperty("EnableTension")); 
           g.AddItem(GetType().GetProperty("Tension"));   
            return p;
        }
        protected override void GeneratePath()
        {
            if (this.Radius == null)
            {
                this.SetPath(null);
                return;
            }
            CoreGraphicsPath v_path = new CoreGraphicsPath();
            Vector2f[] vtab = new Vector2f[this.m_Count];
            float step = (float)((360 / (float)m_Count) * (Math.PI / 180.0f));
            float v_angle = (float)(m_Angle * CoreMathOperation.ConvDgToRadian) ;
            for (int j = 0; j < this.Radius.Length; j++)
            {
                for (int i = 0; i < this.m_Count; i++)
                {
                    vtab[i] = new Vector2f(
                        (float)(this.Center.X + this.Radius[j] * Math.Cos(i * step + v_angle)),
                        (float)(this.Center.Y + this.Radius[j] * Math.Sin(i * step + v_angle)));
                }
                if (this.EnableTension )
                    v_path.AddClosedCurve(vtab, this.Tension);
                else 
                    v_path.AddPolygon(vtab);
            }
            v_path.enuFillMode = this.enuFillMode;
            this.SetPath(v_path);
        }
        public PolygonElement()
        {
            this.m_Count = 6;
        }
        protected new class Mecanism : CoreCircleElementBase.Mecanism
        {
            public new PolygonElement Element{
                get{return base.Element as PolygonElement ;}
            }
            protected override void InitSnippetsLocation()
            {
                float a = this.Element.Angle * CoreMathOperation.ConvDgToRadian ;
                for (int i = 0; i < this.Element.Radius.Length; i++)
                {
                    if (!this.RegSnippets.Contains(i)) continue;
                    this.RegSnippets[i].Location = CurrentSurface.GetScreenLocation(
                        this.Element.Center +
                       new Vector2f (
                           (float)(this.Element.Radius[i] * Math.Cos (a)),
                           (float)(this.Element.Radius[i] * Math.Sin (a))));
                }
                //center point
                RegSnippets[-1].Location = CurrentSurface.GetScreenLocation(this.Element.Center);
            }
            protected override void UpdateSnippetElement(CoreMouseEventArgs e)
            {
                base.UpdateSnippetElement(e);
            }
            protected override void OnMouseMove(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        switch (this.State)
                        {
                            case ST_CREATING:
                                {
                                    this.Element.Radius[0] = CoreMathOperation.GetDistance(
                                        this.Element.Center, e.FactorPoint);
                                    this.Element.m_Angle = CoreMathOperation.GetAngle (
                                        this.Element.Center, e.FactorPoint) * CoreMathOperation.ConvRdToDEGREE;
                                    this.Element.InitElement();
                                    this.CurrentSurface.Invalidate();
                                    return;
                                }                                
                            case ST_CONFIGURING:
                            case ST_EDITING :
                                if (!IsShiftKey && (this.Snippet !=null)&& (this.Snippet .Demand != -1))
                                {
                                            //update new point
                                            this.Element.Radius[this.Snippet.Demand] =
                                              CoreMathOperation.GetDistance(this.Element.Center,
                                              e.FactorPoint);
                                            this.Element.m_Angle = CoreMathOperation.GetAngle(
                                                this.Element.Center, e.FactorPoint) * CoreMathOperation.ConvRdToDEGREE;
                                            this.Element.InitElement();
                                            this.CurrentSurface.Invalidate();
                                            this.Snippet.Location = e.Location;
                                            return;
                                }
                                break;
                        }
                        break;
                }
                base.OnMouseMove(e);
            }
        }
    }
}

