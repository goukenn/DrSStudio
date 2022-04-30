

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: SplineElement.cs
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
file:SplineElement.cs
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
    [Core2DDrawingStandardItem("Spline",
       typeof(Mecanism),
       Keys = Keys.N)]
    public class SplineElement :
        CustomPolygonElement,
        ICore2DClosableElement ,
        ICore2DTensionElement ,
        ICore2DPathBrushStyleElement
    {
        private bool m_EnabledTension;
        private bool m_Closed;
        private CorePathBrushStyleBase m_PathBrushStyle;
        [IGK.DrSStudio.Codec.CoreXMLElement()]
        public CorePathBrushStyleBase PathBrushStyle
        {
            get { return m_PathBrushStyle; }
            set
            {
                if (this.m_PathBrushStyle != value)
                {
                    if (this.m_PathBrushStyle != null) this.m_PathBrushStyle.PropertyChanged -= PathBrushChanged;
                    this.m_PathBrushStyle = value;
                    if (this.m_PathBrushStyle != null) this.m_PathBrushStyle.PropertyChanged += PathBrushChanged;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        //raise the property changed
        void PathBrushChanged(object sender, EventArgs e)
        {
            OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(false)]
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
        [CoreXMLDefaultAttributeValue (false )]
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
        public override IGK.DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(IGK.DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            IGK.DrSStudio.WinUI.ICoreParameterConfigCollections p = base.GetParameters(parameters);
            var g = p.AddGroup(CoreConstant.PARAM_DEFINITION);
            g.AddItem(GetType().GetProperty("Closed"));
            g.AddItem(GetType().GetProperty("EnableTension"));            
            g.AddItem(GetType().GetProperty("Tension"));
            g.AddItem(GetType().GetProperty("enuFillMode"));
            return p;
        }
        protected override void GeneratePath()
        {
            if ((this.Points == null) || (this.Points.Length < 2))
            {
                SetPath(null);
                return;
            }
            CoreGraphicsPath v_p = new CoreGraphicsPath();
            //PointF[] v_tp = new PointF[Points.Length];
            ////copyp array to point array
            //for (int i = 0; i < v_tp.Length; i++)
            //{
            //    v_tp[i] = Points [i];
            //}
            if (this.Closed && (Points.Length > 2))
            {
                if (this.EnableTension)
                {
                    v_p.AddClosedCurve(Points, Tension);
                }
                else
                    v_p.AddClosedCurve(Points);
            }
            else
            {
                if (this.EnableTension)
                {
                    v_p.AddCurve(v_tp, this.Tension);
                }
                else
                    v_p.AddCurve(v_tp); 
            }
            if (this.PathBrushStyle != null)
            {
                this.PathBrushStyle.Generate(v_p);
            }
            v_p.enuFillMode = this.enuFillMode;
            this.SetPath(v_p);
        }
        protected new class Mecanism : CustomPolygonElement .Mecanism 
        {
        }
        /// <summary>
        /// create a spline element
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static SplineElement Create(PointF[] points)
        {
            if ((points == null) || (points.Length == 0))
                return null;
            Vector2f[] t = new Vector2f[points.Length];
            SplineElement v_l = new SplineElement();
            for (int i = 0; i < t.Length; i++)
            {
                t[i] = points[i];
            }
            v_l.Points = t;
            return v_l;
        }
    }
}

