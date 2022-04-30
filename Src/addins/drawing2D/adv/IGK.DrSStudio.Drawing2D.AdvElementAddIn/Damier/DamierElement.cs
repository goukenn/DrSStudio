

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DamierElement.cs
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
file:DamierElement.cs
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
/* 
-------------------------------------------------------------
This file is part of iGK-DEV-DrawingStudio
-------------------------------------------------------------
-------------------------------------------------------------
-------------------------------------------------------------
view license file in Documentation folder to get more info
Copyright (c) 2008-2010 
Author  : Charles A.D. BONDJE DOUE 
mail : bondje.doue@hotmail.com
-------------------------------------------------------------
-------------------------------------------------------------
*/
using System; 
using IGK.ICore; using IGK.ICore.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore.Codec;
    using IGK.ICore.WinUI.Configuration;
    using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.MecanismActions;
    using IGK.ICore.WinUI;
    [LineCorner("Damier", typeof(Mecanism))]
    public class DamierElement :
        RectangleElement        ,
        ICore2DFillModeElement 
    {
        private int m_VerticalCount;
        private int m_HorizontalCount;
        private enuDamierMode m_mode;
        private float m_angle;
        private int m_Count;
        private enuFillMode  m_FillMode;
        private bool m_Alternate;
        [CoreXMLAttribute(true)]
        [CoreXMLDefaultAttributeValue(true)]
        public bool Alternate
        {
            get { return m_Alternate; }
            set
            {
                if (m_Alternate != value)
                {
                    m_Alternate = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
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
        [CoreXMLAttribute(true)]
        [CoreXMLDefaultAttributeValue(6)]
        public int Count
        {
            get { return m_Count; }
            set
            {
                if ((m_Count != value)&&(value > 2 ) && (value < 180))
                {
                    m_Count = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute(true)]
        [CoreXMLDefaultAttributeValue(enuDamierMode.Rectangle)]
        public enuDamierMode Mode {
            get {
                return this.m_mode;
            }
            set {
                if (this.m_mode !=value)
                {
                    this.m_mode = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute (true)]
        [CoreXMLDefaultAttributeValue (5)]
        ///<summaray>
        ///get or set HorizontalCount
        ///<summary>
        public int HorizontalCount
        {
            get
            {
                if (m_HorizontalCount <= 0)
                    m_HorizontalCount = 1;
                return m_HorizontalCount;
            }
            set
            {
                if ((this.m_HorizontalCount != value) && (value > 0))
                {
                    m_HorizontalCount = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute(true)]
        [CoreXMLDefaultAttributeValue(5)]
        ///<summaray>
        ///get or set VerticalCount
        ///<summary>
        public int VerticalCount
        {
            get
            {
                if (this.m_VerticalCount <= 0)
                    this.m_VerticalCount  = 0;
                return m_VerticalCount;
            }
            set
            {
                if( (this.m_VerticalCount !=value)&&(value>0))
                {
                    m_VerticalCount = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        /// <summary>
        /// .ctr
        /// </summary>
        public DamierElement():base()
        {          
        }
        /// <summary>
        /// override initializeElement functions
        /// </summary>
        protected override void InitializeElement()
        {
            base.InitializeElement();
            this.m_Count = 6;
            this.m_HorizontalCount = 5;
            this.m_VerticalCount = 5;
            this.m_angle = 45;
            this.m_Alternate = true;
            this.m_mode = enuDamierMode.Rectangle;
            this.FillBrush.SetSolidColor (Colorf.Black );
        }
        protected override void InitGraphicPath(CoreGraphicsPath path)
        {
            path.Reset();
            Rectanglef v_bound = this.Bounds;
            int x = (int)v_bound.X;
            int y = (int)v_bound.Y;
            float w = (v_bound.Width / (float)this.HorizontalCount);
            float h = (v_bound.Height / (float)this.VerticalCount);
            Rectanglef rc = Rectanglef.Empty;
            for (float i = 0; i < this.VerticalCount; i++)
            {
                for (float  j = 0; j < this.HorizontalCount; j++)
                {
                    if (!Alternate || (Alternate && (i + j) % 2 == 0))
                    {
                        rc = new Rectanglef(
                            x + (j * w),
                            y+ (i * h),
                            w, h);
                        switch (this.m_mode)
                        {
                            case enuDamierMode.Rectangle:
                            default:
                                    path.AddRectangle(rc);
                                    break;
                            case enuDamierMode.Polygon :
                                    path.AddPolygon(CoreMathOperation.GetPolygons ( CoreMathOperation.GetCenter (rc),
                                        w/2.0f, h /2.0f, this.Count , this.Angle ));
                                    break;
                            case enuDamierMode.Diamond:
                                path.AddPolygon(CoreMathOperation.GetDiamond(rc));
                                break;
                            case enuDamierMode.Ellipse:
                                path.AddEllipse(rc.Center, rc.BottomRight - rc.Center);
                                break;
                            case enuDamierMode.Triangle:
                                    path.AddPolygon(new Vector2f[] { 
                                    new Vector2f(rc.X, rc.Y+rc.Height),
                                    new Vector2f(rc.X+rc.Width /2.0F, rc.Y),
                                    new Vector2f(rc.X+rc.Width , rc.Y+rc.Height)
                                });
                                break;
                        }
                }
                    }
                }
            path.CloseFigure();
            path.FillMode = this.FillMode;
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            ICoreParameterConfigCollections c = base.GetParameters(parameters);
            ICoreParameterGroup group = c.AddGroup(CoreConstant.PARAM_GROUP_DESCRIPTION);
            CoreParameterChangedEventHandler d = delegate(object o,CoreParameterChangedEventArgs e)
            {
                if (e.Item.Name == "NumsCol")
                    this.HorizontalCount = int.Parse(e.Value.ToString());
                else
                    this.VerticalCount = int.Parse(e.Value.ToString());
            };
            group.AddItem ("NumsCol", "lb.NumsCol", this.HorizontalCount ,enuParameterType.IntNumber, d);
            group.AddItem("NumsRow", "lb.NumsRow", this.VerticalCount, enuParameterType.IntNumber, d);
            group = c.AddGroup("GraphicsProperty");
            group.AddItem(GetType().GetProperty("FillMode"));
            group.AddItem(GetType().GetProperty("Mode"));
            group.AddItem(GetType().GetProperty("Angle"));
            group.AddItem(GetType().GetProperty("Count"));
            group.AddItem(GetType().GetProperty("Alternate"));
            return c;
        }
        protected internal new class Mecanism : RectangleElement.Mecanism 
        {
            class DamierMecanismToggleModeAction : CoreMecanismActionBase
            {
                protected override bool PerformAction()
                {
                    Mecanism m = this.Mecanism as Mecanism;
                    if (m.Element != null)
                    {
                        m.Element.ToggleMode();
                        return true;
                    }
                    return false;
                }
            }
            public new DamierElement Element {
                get {
                    return base.Element as DamierElement;
                }
            }
         
        }
        #region ICoreAngleElement Members
        [CoreXMLDefaultAttributeValue (45)]
        public float Angle
        {
            get
            {
                return this.m_angle;
            }
            set
            {
                if ((this.m_angle != value) && ((value >=0)&&(value <=360)))
                {
                    this.m_angle = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        #endregion
        internal void ToggleMode()
        {
            switch (this.Mode)
            {
                case enuDamierMode.Rectangle:
                    this.Mode = enuDamierMode.Ellipse;
                    break;
                case enuDamierMode.Ellipse:
                    this.Mode = enuDamierMode.Diamond;
                    break;
                case enuDamierMode.Diamond:
                    this.Mode = enuDamierMode.Polygon ;
                    break;
                case enuDamierMode.Polygon :
                    this.Mode = enuDamierMode.Triangle;
                    break;
                case enuDamierMode.Triangle:
                    this.Mode = enuDamierMode.Rectangle;
                    break;
                default:
                    break;
            }
        }
    }
}

