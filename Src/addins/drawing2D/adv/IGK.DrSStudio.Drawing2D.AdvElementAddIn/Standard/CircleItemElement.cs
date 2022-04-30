

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CircleItemElement.cs
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
file:CircleItemElement.cs
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
using System; 
using IGK.ICore;  using IGK.ICore.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore.Codec;
    using IGK.ICore.Drawing2D.WinUI;
    using IGK.ICore.WinUI;
    using IGK.ICore.Actions;
using IGK.ICore.Drawing2D.Mecanism;
    using IGK.ICore.WinUI.Configuration;
    using IGK.ICore.MecanismActions;
    public enum enuCircleType
    {
        Rectangle,
        Circle,
        Custom
    }
    [LineCorner("CircleItem", 
        typeof(Mecanism),
        ImageKey =CoreImageKeys.DE_RINGS_GKDS
        )]
    public sealed class CicleItemElement :
        Core2DDrawingDualBrushElement,
        ICore2DFillModeElement
    {
        private int m_count;
        private Vector2f m_center;
        private float m_radius;
        private float m_itemradius;
        private float _angle;
        private enuCircleType m_Mode;
        private enuFillMode m_FillMode;
        [CoreXMLAttribute(true)]
        public enuFillMode FillMode
        {
            get { return m_FillMode; }
            set
            {
                if (this.m_FillMode != value)
                {
                    m_FillMode = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute (true)]
        [CoreXMLDefaultAttributeValue (enuCircleType.Circle)]
        public enuCircleType Mode
        {
            get { return m_Mode; }
            set
            {
                if (m_Mode != value)
                {
                    m_Mode = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute(true)]
        [CoreXMLDefaultAttributeValue(3)]
        public int Count { get { return m_count; } set { if ((value >= 2) && (value < 180)) { m_count = value;
        OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
        }
        }
        }
        [CoreXMLAttribute(true)]
        public float Radius { get { return m_radius; } set { m_radius = value; OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition); ; } }
        [CoreXMLAttribute(true)]
        [CoreXMLDefaultAttributeValue(1.0f)]
        public float ItemRadial
        {
            get { return m_itemradius; }
            set
            {
                if ((m_itemradius != value) && (value >= 0))
                {
                    this.m_itemradius = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute(true)]
        [CoreXMLDefaultAttributeValue("0;0")]
        public Vector2f Center { get { return m_center; } set { m_center = value; OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);  } }
        [CoreXMLAttribute(true)]
        [CoreXMLDefaultAttributeValue(0.0f)]
        public float Angle
        {
            get { return _angle; }
            set
            {
                if (_angle != value)
                {
                    _angle = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        protected override void BuildBeforeResetTransform()
        {
             Matrix m = this.GetMatrix();
            if (m.IsIdentity) return;
            Vector2f[] tb = new Vector2f[] { m_center, new Vector2f(m_center.X + Radius, m_center.Y) };
            m.TransformPoints(tb);
            m_center = tb[0];
            m_radius = CoreMathOperation.GetDistance(tb[1], tb[0]);            
        }
        public CicleItemElement()
        {
            this.m_count = 3;
            this.m_Mode = enuCircleType.Circle;
            this.m_radius = 1.0f;
            this.m_itemradius = 1.0f;
        }
        public static CicleItemElement CreateElement()
        {
            CicleItemElement s = new CicleItemElement();
            return s;
        }
       protected override void InitGraphicPath(CoreGraphicsPath path)
{
 	
            Vector2f[] vtab = new Vector2f[this.m_count];
            float step = (float)((360 / (float)m_count) * (Math.PI / 180.0f));
            float vangle = (float)(_angle * (Math.PI / 180.0f));
            for (int i = 0; i < this.m_count; i++)
            {
                vtab[i] = new Vector2f(
                    (float)(this.Center.X + this.Radius * Math.Cos(i * step + vangle)),
                    (float)(this.Center.Y + this.Radius * Math.Sin(i * step + vangle)));
            }
            if ((vtab == null) || (vtab.Length == 0))
            {
                
                return;
            }
            path.Reset();
            switch (Mode)
            {
                case enuCircleType.Rectangle:
                    for (int i = 0; i < vtab.Length; i++)
                    {
                        path.AddRectangle(CoreMathOperation.GetBounds(vtab[i], this.m_itemradius));
                    }
                    break;
                case enuCircleType.Circle:
                default:
                    for (int i = 0; i < vtab.Length; i++)
                    {
                        path.AddEllipse(CoreMathOperation.GetBounds(vtab[i], this.m_itemradius));
                    }
                    break;
            }
            path.FillMode = this.FillMode;
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            ICoreParameterConfigCollections p = base.GetParameters(parameters);
            var group = p.AddGroup(CoreConstant.PARAM_DEFINITION);
            group.AddItem(GetType().GetProperty("Count"));
            group.AddItem(GetType().GetProperty("FillMode"));
            group.AddItem(GetType().GetProperty("Mode"));
            return p;
        }
        new class Mecanism : Core2DDrawingSurfaceMecanismBase< CicleItemElement>
        {
            //Actions
            class CircleItemChangeModeAction : CoreMecanismActionBase
            {
                new Mecanism Mecanism {
                    get {
                        return base.Mecanism as Mecanism ;
                    }
                }
                protected override bool PerformAction()
                {
                    if (this.Mecanism.Element != null)
                    {
                        this.Mecanism.Element.Invalidate(false);
                        this.Mecanism.Element.ChangeMode();
                        this.Mecanism.Element.Invalidate(true);
                        return true;
                    }
                    return false;
                }
            }


            protected override void GenerateActions()
            {
                base.GenerateActions();
                this.AddAction(enuKeys.M, new CircleItemChangeModeAction()); 
            }
           
          
            protected override void UpdateDrawing(CoreMouseEventArgs e)
            {
                CicleItemElement r = this.Element;
                switch (this.State)
                {
                    case ST_CREATING:
                        r.m_center = this.StartPoint;
                        r.m_radius = CoreMathOperation.GetDistance(e.FactorPoint  , this.StartPoint);
                        r.m_itemradius = r.m_radius;
                        r._angle = (float)((180.0f / Math.PI) * CoreMathOperation.GetAngle(this.StartPoint, this.EndPoint));
                        r.InitElement();
                        this.Invalidate();
                        break;
                }
            }
            protected override void OnMouseMove(CoreMouseEventArgs e)
            {
                if (e.Button == enuMouseButtons.Left)
                {
                    if (this.Snippet !=null)
                    {
                        CicleItemElement c = this.Element;
                        if (c != null)
                        {
                            c.Invalidate(false);
                            switch (this.Snippet.Index)
                            {
                                case 0:
                                    c.m_center = e.FactorPoint;
                                    break;
                                case 1:
                                    c._angle = (float)(CoreMathOperation.GetAngle(c.m_center, e.FactorPoint) * 180 / Math.PI);
                                    break;
                                case 2:
                                    c.m_itemradius = CoreMathOperation.GetDistance(c.m_center, e.FactorPoint);
                                    break;
                                case 3:
                                    c.m_radius = CoreMathOperation.GetDistance(c.m_center, e.FactorPoint);
                                    break;
                            }
                            this.Snippet.Location = (e.Location);
                            c.InitElement();
                            this.Invalidate();
                            return;
                        }
                    }
                }
                base.OnMouseMove(e);
            }
            protected override void GenerateSnippets()
            {
                base.GenerateSnippets();
                AddSnippet( this.CurrentSurface .CreateSnippet (this, 0, 0));
                AddSnippet( this.CurrentSurface .CreateSnippet (this, 1, 1));
                AddSnippet(this.CurrentSurface.CreateSnippet(this, 2, 2));
                AddSnippet(this.CurrentSurface.CreateSnippet(this, 3, 3));
            }
            protected override void InitSnippetsLocation()
            {
                base.InitSnippetsLocation();
                CicleItemElement c = this.Element;
                if ((c != null)&& (RegSnippets.Count > 0))
                {
                    //radius
                    RegSnippets[0].Location = (CurrentSurface.GetScreenLocation(
                  c.Center
                  ));
                    //angle
                    RegSnippets[1].Location=(CurrentSurface.GetScreenLocation(
                        CoreMathOperation.GetPoint(c.Center, c.Radius, c._angle)
                        ));
                    RegSnippets[2].Location=(CurrentSurface.GetScreenLocation(
                        new Vector2f(
                        c.Center.X,
                        c.Center.Y + c.ItemRadial 
                        )
                        ));
                    RegSnippets[3].Location = (CurrentSurface.GetScreenLocation(
                   c.Center + c.Radius 
                   ));
                }
            }
        }
        internal void ChangeMode()
        {
            switch (Mode)
            {
                case enuCircleType.Rectangle:
                    this.m_Mode = enuCircleType.Circle;
                    break;
                case enuCircleType.Circle:
                    this.m_Mode = enuCircleType.Rectangle;
                    break;
                default:
                    break;
            }
            OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
        }
    }
}

