

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: HexaRectangleElement.cs
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
file:HexaRectangleElement.cs
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
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D ;
namespace IGK.DrSStudio.Drawing2D
{
    public enum enumHexaModel
    { 
        Horizontal,
        Vertical
    }
    [Core2DDrawingStandardItem ("HexaItem", typeof (Mecanism))]
    class HexaRectangleElement : Core2DDrawingDualBrushBoundElement 
    {
        public HexaRectangleElement()
        {
            this.m_RadiusX = 20;
            this.m_RadiusY = 20;
            this.m_Precision = 4;
            this.m_Model = enumHexaModel.Vertical;
        }
        private float m_RadiusX;
        private float m_RadiusY;
        private float  m_Precision;
        private enumHexaModel m_Model;
        private Vector2f m_Center;
        [IGK.DrSStudio.Codec.CoreXMLAttribute()]
        public Vector2f Center
        {
            get { return m_Center; }
            set
            {
                if (m_Center != value)
                {
                    m_Center = value;
                }
            }
        }
        [IGK.DrSStudio.Codec.CoreXMLAttribute ()]
        [IGK.DrSStudio.Codec.CoreXMLDefaultAttributeValue (enumHexaModel.Vertical)]
        public enumHexaModel Model
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
        [IGK.DrSStudio.Codec.CoreXMLAttribute()]
        [IGK.DrSStudio.Codec.CoreXMLDefaultAttributeValue(4)]
        /// <summary>
        /// get or set the precision
        /// </summary>
        public float  Precision
        {
            get { return m_Precision; }
            set
            {
                if ((m_Precision != value)&&(value >= 4))
                {
                    m_Precision = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [IGK.DrSStudio.Codec.CoreXMLAttribute()]
       /// <summary>
       /// get the radius y
       /// </summary>
        public float RadiusY
        {
            get { return m_RadiusY; }
            set
            {
                if ((m_RadiusY != value)&&(value > this.Precision * 2))
                {
                    m_RadiusY = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [IGK.DrSStudio.Codec.CoreXMLAttribute()]
        public float RadiusX
        {
            get { return m_RadiusX; }
            set
            {
                if ((m_RadiusX != value)&&( value > this.Precision * 2))
                {
                    m_RadiusX = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        protected override void GeneratePath()
        {
            if (this.Bound.IsEmpty)
            {
                this.SetPath(null);
                return;
            }
            GraphicsPath v_path = CreateHex(this.Bound , CoreMathOperation.GetCenter (this.Bound ), this.RadiusX, this.RadiusY,
                this.Precision,
                this.Model );
            this.SetPath(v_path);
            //generate path hexa element            
        }
        private static GraphicsPath CreateHex(Rectanglef rc, 
            Vector2f Center, 
            float radiusX, 
            float radiusY, 
            float prescision, 
            enumHexaModel model)
        {
            GraphicsPath centerpath = new GraphicsPath();
            float v_radiusX =radiusX ;
            float v_radiusY =radiusY;
            float v_pres = prescision ;
            const int NBR_ITEM = 6;
            List<Vector2f> points = new List<Vector2f>();
            Queue<Vector2f> mpoint = new Queue<Vector2f>();
            Vector2f Icenter = Center;
            Vector2f center = Icenter;
            mpoint.Enqueue(center);
            const float STEP = (float)Math.PI / 3.0f;
            float Angle = 90;            
            float OFFSET = 0;// (float)Math.PI / 6.0f;
            if (model == enumHexaModel.Vertical)
            {
                OFFSET = (float)Math.PI / 6.0f;
                Angle = 0;
            }
            Vector2f t_center = Point.Empty;
            Vector2f[] t = null;
            float MP = (float)Math.Sqrt(3);
            float CRadiusX = 0.0f;
            float CRadiusY = 0.0f;
            //voisin visible
            List<Vector2f> v_voisin = new List<Vector2f>();
            int c_count = 0;
            float rx = 0;
            float ry = 0;
            int level = 0;
            while (mpoint.Count > 0)
            {
                c_count = mpoint.Count;
                CRadiusX = (float)(level * v_radiusX * Math.Cos(Math.PI / 6));//// (float)level * 2.0f * v_radiusX * MP/3.0f;// (float)Math.Max(Math.Abs(GetDistance(Icenter, center)), CRadiusX);
                CRadiusY = (float)(level * v_radiusY * Math.Sin(Math.PI / 3));// (float)level * 2.0f * v_radiusY * MP/3.0f;// (float)Math.Max(Math.Abs(GetDistance(Icenter, center)), CRadiusX);
                level++;
                v_voisin.Clear();
                for (int m = 0; m < c_count; m++)
                {
                    center = mpoint.Dequeue();
                    if (centerpath.IsVisible(center))
                        continue;
                    {
                        Rectanglef h = new Rectanglef(center, Size.Empty);
                        h.Inflate(v_pres, v_pres);
                        centerpath.AddRectangle(h);
                    }
                    if (rc.Contains(center))
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            t_center = new Vector2f(
                             (float)(center.X + (v_radiusX * MP) * Math.Cos(i * STEP + OFFSET)),
                                (float)(center.Y + (v_radiusY * MP) * Math.Sin(i * STEP + OFFSET)));
                            rx = (float)Math.Ceiling(Math.Abs(t_center.X - Icenter.X));
                            ry = (float)Math.Ceiling(Math.Abs(t_center.Y - Icenter.Y));
                            //if (
                            //Math.Floor(((rx * rx) / (CRadiusX * CRadiusX)) +
                            //     ((ry * ry) / (CRadiusY * CRadiusY))) <= 1)
                            // {
                            //     continue;
                            // }
                            if (centerpath.IsVisible(t_center))
                                continue;
                            if (!v_voisin.Contains(Point.Round(t_center)))
                            {
                                v_voisin.Add(Point.Round(t_center));
                            }
                        }
                        t = CoreMathOperation.GetPolygons (center, v_radiusX, v_radiusY, NBR_ITEM, Angle);
                        points.AddRange(t);
                    }
                }
                foreach (Vector2f item in v_voisin)
                {
                    mpoint.Enqueue(item);
                }
            }
            GraphicsPath v_path = new GraphicsPath();
            int vi = 0;            
            for ( vi = 0; vi < points.Count; vi += NBR_ITEM)
            {
                v_path.AddPolygon(points.GetRange(vi, NBR_ITEM).ToArray());
            }
            return v_path;
        }
        public override IGK.DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(IGK.DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            IGK.DrSStudio.WinUI.ICoreParameterConfigCollections p = base.GetParameters(parameters);
            IGK.DrSStudio.WinUI.ICoreParameterGroup g = p.AddGroup("HexDefinition");
            g.AddItem(GetType().GetProperty("RadiusX"));
            g.AddItem(GetType().GetProperty("RadiusY"));
            g.AddItem(GetType().GetProperty("Model"));
            g.AddItem(GetType().GetProperty("Precision"));
            return p;
        }
        internal new sealed class Mecanism : Core2DDrawingDualBrushBoundElement.Mecanism
        { 
        }
    }
}

