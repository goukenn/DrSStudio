

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: MangaEllipseBullet.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace IGK.DrSStudio.MangaStuffAddIn.WorkingElements
{
    using IGK.ICore.WinUI.Configuration;
    using IGK.ICore.Codec;
    [MangaStuffElementAttribute ("EllipseBullet", typeof (Mecanism))]
    public class MangaEllipseBullet : RectangleElement
    {
        private enuFillMode m_fillMode;
        private float m_startAngle;
        private float m_endAngle;
        private Vector2f  m_defPoint;

        protected override void InitGraphicPath(CoreGraphicsPath path)
        {
            path.Reset();
            if (this.Bounds.IsEmpty || (this.m_startAngle == 0) && (this.m_endAngle == 0))
            {
                return;
            }
            Rectanglef rc = this.Bounds;
            Vector2f v_center = CoreMathOperation.GetCenter(rc);
            float v_rx = rc.Width / 2.0f;
            float v_ry = rc.Height / 2.0f;
            Vector2f v_pt1 = CoreMathOperation.GetPoint(v_center, v_rx, v_ry, m_startAngle);
            Vector2f v_pt2 = CoreMathOperation.GetPoint(v_center, v_rx, v_ry, m_endAngle + m_startAngle);
            //path.AddArc(rc, this.m_startAngle, 350);// this.m_endAngle);
            List<Vector2f> pts = new List<Vector2f>();
            float step = (float)(Math.PI / 180.0f);
            float theta = this.m_startAngle * CoreMathOperation.ConvDgToRadian;
            if (m_endAngle < 0)
            {
                m_endAngle += 360;
            }
            float etheta = this.m_endAngle * CoreMathOperation.ConvDgToRadian;
            for (float a = 0.0f; a < etheta; a += step)
            {
                pts.Add(new Vector2f(
                    (float)(v_center.X + v_rx * Math.Cos(a + theta)),
                    (float)(v_center.Y + v_ry * Math.Sin(a + theta))
                    ));
            }
            pts.AddRange(new Vector2f[] { 
                v_pt2,
                m_defPoint ,
                v_pt1});
            //path.AddCurve(new Vector2f[] { 
            //    v_pt2,
            //    m_defPoint ,
            //    v_pt1

            //},0.0f);
            if (pts.Count > 0)
            {
                path.AddCurve(pts.ToArray(), 0, true );
            }
            path.FillMode = this.FillMode;
        }

        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            var g = parameters.AddGroup(CoreConstant.PARAM_DEFINITION);
            g.AddItem(GetType().GetProperty("enuFillMode"));
            g.AddItem(GetType().GetProperty("StartAngle"));
            g.AddItem(GetType().GetProperty("SweepAngle"));
            return parameters;
        }
        protected override void BuildBeforeResetTransform()
        {            
            Matrix m = this.GetMatrix();
            if (m.IsIdentity) return;


            Vector2f[] tb = new Vector2f[] { m_defPoint };
            m.TransformPoints(tb);
            this.m_defPoint = tb[0];
            base.BuildBeforeResetTransform();
        }



        #region ICoreArcElement Members

        [CoreXMLAttribute (true)]
        public Vector2f  DeftPoint
        {
            get
            {
                return this.m_defPoint;
            }
            set
            {
                if (this.m_defPoint != value)
                {
                    this.m_defPoint = value;
                    this.OnPropertyChanged (CoreWorkingObjectPropertyChangedEventArgs .Definition );
                }
            }
        }
        [CoreXMLAttribute(true)]
        public float StartAngle
        {
            get
            {
                return this.m_startAngle;
            }
            set
            {
                if ((value != this.m_startAngle) && (value >= 0) && (value < 360))
                {
                    this.m_startAngle = value;
                    this.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute(true)]
        public float SweepAngle
        {
            get
            {
                return this.m_endAngle;
            }
            set
            {
                if ((value != this.m_endAngle) && (value >= 0) && (value < 360))
                {
                    this.m_endAngle = value;
                    this.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }

        #endregion

        #region ICoreFillModeElement Members
        [CoreXMLAttribute(true)]
        public enuFillMode FillMode
        {
            get
            {
                return this.m_fillMode;
            }
            set
            {
                if (this.m_fillMode != value)
                {
                    this.m_fillMode = value;
                    this.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }

        #endregion

        sealed new class Mecanism : RectangleElement.Mecanism 
        {

            const int DM_SIZE_BOTTOM = 30;
            const int DM_STARTANGLE =  DM_SIZE_BOTTOM + 1;
            const int DM_SWEEPANGLE = DM_SIZE_BOTTOM + 2;
            const int DM_DEFPOINT = DM_SIZE_BOTTOM + 3;

            public new MangaEllipseBullet  Element
            {
                get
                {

                    return base.Element as MangaEllipseBullet;
                }
            }
            protected override void UpdateDrawing(CoreMouseEventArgs e)
            {
                
                switch (this.State)
                {
                    case ST_CREATING:
                        Rectanglef rc = CoreMathOperation.GetBounds(this.StartPoint, this.EndPoint);
                        this.Element.m_defPoint = new Vector2f (
                            rc.X + rc.Width,
                            rc.Y + rc.Height
                            );
                        this.Element.m_startAngle = 65;
                        this.Element.m_endAngle = 360 - 45;
                        break;
                }
                base.UpdateDrawing(e);
            }

            protected override void InitSnippetsLocation()
            {
                base.InitSnippetsLocation();

                Rectanglef rc = this.Element.Bounds;
                Vector2f v_center = CoreMathOperation.GetCenter(rc);
                Vector2f v_pt1 = CoreMathOperation.GetPoint(v_center, rc.Width / 2.0f, rc.Height / 2.0f, this.Element.m_startAngle);
                float v_angle = this.Element.m_startAngle + this.Element.m_endAngle;
                if (v_angle > 360)
                {
                    v_angle -= 360;
                }
                Vector2f v_pt2 = CoreMathOperation.GetPoint(v_center, rc.Width / 2.0f, rc.Height / 2.0f, v_angle);
                
                this.RegSnippets[DM_DEFPOINT].Location = CurrentSurface.GetScreenLocation(this.Element.DeftPoint);
                this.RegSnippets[DM_STARTANGLE].Location = CurrentSurface.GetScreenLocation(v_pt1);
                this.RegSnippets[DM_SWEEPANGLE].Location = CurrentSurface.GetScreenLocation(v_pt2);
            }
            protected override void GenerateSnippets()
            {
                base.GenerateSnippets();
                this.AddSnippet(this.CurrentSurface.CreateSnippet(this, DM_STARTANGLE, DM_STARTANGLE));
                this.AddSnippet(this.CurrentSurface.CreateSnippet(this, DM_SWEEPANGLE, DM_SWEEPANGLE));
                this.AddSnippet(this.CurrentSurface.CreateSnippet(this, DM_DEFPOINT, DM_DEFPOINT));
            }
            protected override void  OnMouseMove(CoreMouseEventArgs e)
            {
 
                switch (e.Button)
                {
                    case enuMouseButtons.Left:
                        if (this.Snippet !=null)
                        switch (this.Snippet .Demand )
                        {
                            case DM_DEFPOINT:
                                this.Element.Invalidate(false);
                                this.Element.m_defPoint = e.FactorPoint;
                                this.Element.InitElement();
                                this.Element.Invalidate(true);
                                return;

                            case DM_STARTANGLE:
                                {
                                    this.Element.Invalidate(false);
                                    Vector2f v_center = CoreMathOperation.GetCenter(this.Element.Bounds);
                                    this.Element.m_startAngle = CoreMathOperation.GetAngle(v_center, e.FactorPoint) * CoreMathOperation.ConvRdToDEGREE;

                                    this.Element.InitElement();
                                    this.InitSnippetsLocation();
                                    this.Element.Invalidate(true);
                                }
                                return;

                            case DM_SWEEPANGLE:
                                {
                                    this.Element.Invalidate(false);

                                    Vector2f v_center = CoreMathOperation.GetCenter(this.Element.Bounds);
                                    this.Element.m_endAngle = -this.Element.m_startAngle + (CoreMathOperation.GetAngle(v_center, e.FactorPoint) * CoreMathOperation.ConvRdToDEGREE);
                                    this.Element.InitElement();
                                    this.InitSnippetsLocation();
                                    this.Element.Invalidate(true);
                                }
                                return;
                        }
                        break;
                }
                base.OnMouseMove(e);
            }
       
         
        }
    }
}
