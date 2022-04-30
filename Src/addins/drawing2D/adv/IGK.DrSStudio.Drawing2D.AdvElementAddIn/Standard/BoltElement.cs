

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: BoltElement.cs
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
file:BoltElement.cs
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
using IGK.ICore.WinUI;
using IGK.ICore.MecanismActions;
using IGK.ICore.Drawing2D.Mecanism;
using IGK.ICore.Drawing2D.Segments;
using IGK.ICore.WinUI.Configuration;
using IGK.ICore.Codec;
namespace IGK.DrSStudio.Drawing2D.Standard
{
/// <summary>
/// represent bolt type
/// </summary>

        [Core2DDrawingStandardElement("Bolt", typeof(Mecanism))]
    public class BoltElement  :
        Core2DDrawingDualBrushElement ,
        ICoreAlignmentCircle,
        ICore2DFillModeElement ,
        ICore2DTensionElement 
    {
        private Vector2f  m_Center;
        private Vector2f m_InnerRadius;
        private Vector2f m_OuterRadius;
        private bool m_Rounded;
        private float m_InnerAngle;
        private float m_OuterAngle;
        private float m_StartAngle;
        private float m_OffsetAngle;
        private enuFillMode m_FillMode;
        private float m_Tension;
        private bool m_EnableTension;
        private enuBoltType m_BoltType;
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue ( enuBoltType .Arc )]
        public enuBoltType BoltType
        {
            get { return m_BoltType; }
            set
            {
                if (m_BoltType != value)
                {
                    m_BoltType = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);         
                }
            }
        }
        [CoreXMLAttribute()]
        public float OffsetAngle
        {
            get { return m_OffsetAngle; }
            set
            {
                if ((m_OffsetAngle != value) && 
                    (Math.Abs (value)<=360.0f))
                {
                    m_OffsetAngle = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);         
                }
            }
        }
        [CoreXMLAttribute()]
        public float StartAngle
        {
            get { return m_StartAngle; }
            set
            {
                if (m_StartAngle != value)
                {
                    m_StartAngle = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        public float OuterAngle
        {
            get { return m_OuterAngle; }
            set
            {
                if ((m_OuterAngle != value)&&(value >= 0))
                {
                    m_OuterAngle = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        public float InnerAngle
        {
            get { return m_InnerAngle; }
            set
            {
                if ((m_InnerAngle != value)&&(value >= 0))
                {
                    m_InnerAngle = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        public bool Rounded
        {
            get { return m_Rounded; }
            set
            {
                if (m_Rounded != value)
                {
                    m_Rounded = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        public bool EnableTension
        {
            get { return m_EnableTension; }
            set
            {
                if (m_EnableTension != value)
                {
                    m_EnableTension = value;
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

        public override Rectanglef GetAlignmentBound()
        {
            var g = Math.Max(this.InnerRadius.Distance(), this.OuterRadius.Distance());
            return CoreMathOperation.GetBounds(this.Center, g);

            //return base.GetAlignmentBound();
        }

        /// <summary>
        /// .ctr
        /// </summary>
        public BoltElement()
        {
            this.m_InnerAngle = 45;
            this.m_OuterAngle = 60;
            this.m_Rounded = false ;
            this.m_BoltType = enuBoltType.Arc;
        }

        public override void Align(enuCore2DAlignElement alignment, Rectanglef bounds)
        {
            CoreMathOperation.AlignCircle(this, alignment, bounds, out float dx, out float dy);

            this.Translate(dx, dy, enuMatrixOrder.Append, false);
        }
        protected override void InitGraphicPath(CoreGraphicsPath path)
        {
            PathSegment v_path = new PathSegment();
            
            path.Reset();
            float step = (float)(Math.PI / 180.0f);
            float _TOUR = (float)(Math.PI * 2);
            float ia = (float)(m_InnerAngle * Math.PI / 180.0f);
            float oa = (float)(m_OuterAngle * Math.PI / 180.0f);
            float sa = (float)(m_StartAngle * Math.PI / 180.0f);
            float ofa = (this.m_OffsetAngle <0)? 360 - Math.Abs (m_OffsetAngle)%360: m_OffsetAngle ;
            float v_offset = sa + (float)(ofa * CoreMathOperation.ConvDgToRadian);
            List<Vector2f> v_tab = new List<Vector2f>();
            float h = 0;
            int pha = 0;
            float v_a = 0.0f;
            float v_i = 0.0f;
            Rectanglef v_inrc = CoreMathOperation.GetBounds(this.m_Center, this.m_InnerRadius.X, this.m_InnerRadius.Y);
            Rectanglef v_outrc = CoreMathOperation.GetBounds(this.m_Center, this.m_OuterRadius.X, this.m_OuterRadius.Y);
            if ((v_inrc.Width <= 0) ||
                 (v_inrc.Height <= 0) ||
                 (v_outrc.Height <= 0) ||
                 (v_outrc.Width <= 0)
                )
            {
                return;
            }
            switch (this.BoltType)
            {
                case enuBoltType.ArcAlternate :
#region Arc-Alternate
                    {
                    int i = (int)Math.Ceiling((2 * (360 / (this.InnerAngle + this.OuterAngle))) - 1);
                    for (float t = 0; t < _TOUR; )
                    {
                        switch (pha)
                        {
                            case 0:
                                {
                                    //v_a = (t + v_offset) * CoreMathOperation.ConvRdToDEGREE;
                                    //v_i = ((v_a + this.m_InnerAngle)> 360)?
                                    //    ((v_a + this.m_InnerAngle)-360)
                                    //    : this.m_InnerAngle ;
                                    v_a = (t + v_offset) * CoreMathOperation.ConvRdToDEGREE;
                                    v_i = ((v_a - ofa + this.m_InnerAngle) > 360) ?
                                        m_InnerAngle - (-360
                                            + (v_a - ofa + this.m_InnerAngle))
                                        : this.m_InnerAngle;
                                    if (v_i != 0)
                                    {
                                        v_path.AddArc(v_inrc, v_a, v_i);
                                        v_path.AddArc(v_outrc, v_i + v_a, -v_i);
                                        v_path.CloseFigure();
                                    }
                                    pha = 1;
                                    t += Math.Abs(ia);
                                }
                                break;
                            case 1:
                                //v_path.AddArc(v_outrc, (t + sa) * CoreMathOperation.ConvRdToDEGREE, this.m_OuterAngle);
                                pha = 0;
                                t += Math.Abs(oa);
                                break;
                        }//end swicth
                        if (i <= 0)
                            break;
                        i--;
                    }
                    v_path.CloseFigure();
                    //end default

                    path.AddSegment(v_path);
            }
                    break;
#endregion
                case enuBoltType.Point: 
                    for (float t = 0; t <= _TOUR; t += step)
                    {
                        switch (pha)
                        {
                            case 0:
                                {
                                    v_tab.Add(new Vector2f((float)(this.Center.X + this.InnerRadius.X * Math.Cos(t + v_offset)),
                                                           (float)(Center.Y + this.InnerRadius.Y * Math.Sin(t + v_offset))));
                                    if (this.Rounded == true)
                                    {
                                        h += step;
                                        if (h > ia)
                                        {
                                            pha = 1;
                                            h = 0;
                                        }
                                    }
                                    else
                                    {
                                        h += ia;
                                        pha = 1;
                                        h = 0;
                                        t += ia - step;
                                    }
                                }
                                break;
                            case 1:
                                v_tab.Add(new Vector2f(
                                    (float)(Center.X + this.OuterRadius.X * Math.Cos(t + sa)),
                                    (float)(Center.Y + this.OuterRadius.Y * Math.Sin(t + sa))));
                                if (Rounded)
                                {
                                    h += step;
                                    if (h > oa)
                                    {
                                        pha = 0;
                                        h = 0;
                                    }
                                }
                                else
                                {
                                    pha = 0;
                                    h = 0;
                                    t += oa - step;
                                }
                                break;
                        }
                    }
                    Vector2f[] v_t = v_tab.ToArray();
                    if (
                        v_t.Length > 2)
                    {
                        if (this.Rounded)
                        {
                            if (this.EnableTension)
                                path.AddClosedCurve(v_t, Tension);
                            else
                            {
                                path.AddClosedCurve(v_t);
                            }
                        }
                        else
                        {
                            if (this.EnableTension)
                                path.AddClosedCurve(v_t, Tension);
                            else
                                path.AddPolygon(v_t);
                        }
                    }
                    path.FillMode = this.FillMode;
                    break;
                case enuBoltType.Arc:
                default:
                    {
                        int i = (int)Math.Ceiling((2 * (360 / (this.InnerAngle + this.OuterAngle))) - 1);
                        for (float t = 0; t < _TOUR; )
                        {
                            switch (pha)
                            {
                                case 0:
                                    {
                                        v_a = (t + v_offset) * CoreMathOperation.ConvRdToDEGREE;
                                        v_i = ((v_a -ofa  + this.m_InnerAngle) > 360) ?
                                            m_InnerAngle -( - 360 
                                                + (v_a - ofa + this.m_InnerAngle))
                                            : this.m_InnerAngle;
                                        if (v_i != 0)
                                        v_path.AddArc(v_inrc, v_a,v_i);
                                        pha = 1;
                                        t += Math.Abs(ia);
                                    }
                                    break;
                                case 1:
                                    v_a = (t + v_offset) * CoreMathOperation.ConvRdToDEGREE;
                                    v_i = ((v_a -ofa+ this.m_OuterAngle ) > 360) ?
                                        this.m_OuterAngle - ((v_a -ofa + this.m_OuterAngle ) - 360)
                                        : this.m_OuterAngle;
                                    if (v_i != 0)
                                    v_path.AddArc(v_outrc, v_a, v_i );
                                    pha = 0;
                                    t += Math.Abs(oa);
                                    break;
                            }//end swicth
                            //if (i <= 0)
                            //    break;
                            i--;
                        }
                        v_path.CloseFigure();
                        //end default

                        path.AddSegment(v_path);
                    }
                    break;
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
        [CoreXMLAttribute()]
        public Vector2f  OuterRadius
        {
            get { return m_OuterRadius; }
            set
            {
                if (!m_OuterRadius.Equals (value))
                {
                    m_OuterRadius = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        public Vector2f InnerRadius
        {
            get { return m_InnerRadius; }
            set
            {
                if (!m_InnerRadius.Equals (value))
                {
                    m_InnerRadius = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        public Vector2f  Center
        {
            get { return m_Center; }
            set
            {
                if (!m_Center.Equals (value))
                {
                    m_Center = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        protected override void BuildBeforeResetTransform()
        {
            Matrix m = this.GetMatrix();
            if (m.IsIdentity)
                return;
            this.m_Center = CoreMathOperation.TransformVector2fPoint(m, this.m_Center)[0];
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            ICoreParameterConfigCollections p = base.GetParameters(parameters);
            ICoreParameterGroup group = p.AddGroup("Definition");
            group.AddItem(GetType().GetProperty("Rounded"));
            //group.AddItem(GetType().GetProperty("FillMode"));
            group.AddItem(GetType().GetProperty("BoltType"));
            group.AddItem(GetType().GetProperty("InnerAngle"));
            group.AddItem(GetType().GetProperty("OuterAngle"));
            group.AddItem(GetType().GetProperty("OffsetAngle"));
            //group.AddItem(GetType().GetProperty("EnableTension"));
            //group.AddItem(GetType().GetProperty("Tension"));
            return p;
        }
        new class Mecanism : Core2DDrawingSurfaceMecanismBase<BoltElement>
        {
            sealed class ResetBoltElementAction : CoreMecanismActionBase
            {
                protected override bool PerformAction()
                {
                    Mecanism m = this.Mecanism as Mecanism;
                    //reset the bar angle
                    BoltElement v_l = m.Element;
                    if ((v_l != null) && (v_l.m_OffsetAngle != 0.0f))
                    {
                        v_l.m_OffsetAngle = 0;
                        v_l.InitElement();
                        m.Invalidate();
                    }
                    return false;
                }
            }
            sealed class ToogleBoltElementAction : CoreMecanismActionBase
            {
                protected override bool PerformAction()
                {
                    Mecanism c = this.Mecanism as Mecanism;
                    if (c.Element != null)
                    {
                        switch (c.Element.BoltType)
                        {
                            case enuBoltType.Arc:
                                c.Element.BoltType = enuBoltType.ArcAlternate;
                                break;
                            case enuBoltType.ArcAlternate:
                                c.Element.BoltType = enuBoltType.Point ;
                                break;
                            case enuBoltType.Point:
                            default :
                                c.Element.BoltType = enuBoltType.Arc;
                                break;
                        }
                        return true;
                    }
                    return false;
                }
            }
            protected override void GenerateActions()
            {
                base.GenerateActions();
                this.Actions[enuKeys.R] = new ResetBoltElementAction();
                this.Actions[enuKeys.T] = new ToogleBoltElementAction();
            }
         
            protected override void BeginDrawing(CoreMouseEventArgs e)
            {
                this.StartPoint = e.FactorPoint;
                this.EndPoint = e.FactorPoint;
                BoltElement v_l = this.Element;
                v_l.m_Center = e.FactorPoint;
                v_l.m_InnerRadius = new Vector2f(1, 1);
                v_l.m_InnerRadius = new Vector2f(2, 2);
                v_l.InitElement();
                this.State = ST_CREATING;
                this.Invalidate();
            }
            protected override void UpdateDrawing(CoreMouseEventArgs e)
            {
                BoltElement v_l = this.Element;
      
                        if (IsShiftKey)
                        {
                            v_l.m_OuterRadius = CoreMathOperation.GetDistanceP(e.FactorPoint, v_l.Center);
                            v_l.m_InnerRadius = v_l.m_OuterRadius / 2.0f;
                        }
                        else {
                            float r = CoreMathOperation.GetDistance(e.FactorPoint, v_l.Center);
                            v_l.m_OuterRadius = new Vector2f(r, r);
                            v_l.m_InnerRadius = v_l.m_OuterRadius / 2.0f;
                        }
                        if (this.IsControlKey)
                            v_l.m_OffsetAngle = CoreMathOperation.GetAngle(v_l.Center, e.FactorPoint) * CoreMathOperation.ConvRdToDEGREE;                       
                        v_l.InitElement();
                        this.Invalidate();
                  
            }
            protected override void EndDrawing(CoreMouseEventArgs e)
            {
                this.UpdateDrawing(e);
                this.InitSnippetsLocation();
                this.State = ST_EDITING;
            }
            
            protected override void UpdateSnippetEdit(CoreMouseEventArgs e)
            {
                BoltElement v_l = this.Element;
                switch (this.Snippet.Demand )
                {
                    case 0:
                        v_l.m_Center = e.FactorPoint;
                        break;
                    case 1:
                        if (IsShiftKey)
                        {
                            v_l.m_InnerRadius = CoreMathOperation.GetDistanceP(e.FactorPoint, v_l.Center);
                        }
                        else
                        {
                            float r = CoreMathOperation.GetDistance(e.FactorPoint, v_l.Center);
                            v_l.m_InnerRadius = new Vector2f(r, r);
                        }
                        if (this.IsControlKey)
                            v_l.m_InnerAngle  = CoreMathOperation.GetAngle(v_l.Center, e.FactorPoint) * CoreMathOperation.ConvRdToDEGREE;
                        break;
                    case 2:
                        if (IsShiftKey)
                        {
                            v_l.m_OuterRadius  = CoreMathOperation.GetDistanceP(e.FactorPoint, v_l.Center);
                        }
                        else
                        {
                            float r = CoreMathOperation.GetDistance(e.FactorPoint, v_l.Center);
                            v_l.m_OuterRadius = new Vector2f(r, r);
                        }
                        if (this.IsControlKey )
                        v_l.m_OuterAngle = CoreMathOperation.GetAngle(v_l.Center, e.FactorPoint) * CoreMathOperation.ConvRdToDEGREE;
                        break;
                    case 3 : //offset angle
                        v_l.m_OffsetAngle  = CoreMathOperation.GetAngle(v_l.Center, e.FactorPoint) * CoreMathOperation.ConvRdToDEGREE;
                        break;
                }
                v_l.InitElement();
                this.Snippet.Location = e.Location;
                this.Invalidate();
            }
            protected override void GenerateSnippets()
            {
                base.GenerateSnippets();
                this.AddSnippet(CurrentSurface.CreateSnippet(this, 0, 0));
                this.AddSnippet(CurrentSurface.CreateSnippet(this, 1, 1));
                this.AddSnippet(CurrentSurface.CreateSnippet(this, 2, 2));
                this.AddSnippet(CurrentSurface.CreateSnippet(this, 3, 3));
            }
            protected override void EndSnippetEdit(CoreMouseEventArgs e)
            {
                base.EndSnippetEdit(e);
            }
            protected override void InitSnippetsLocation()
            {
                base.InitSnippetsLocation();
                if ((this.Element == null) || (this.RegSnippets.Count == 0))
                    return;
                BoltElement v_l = this.Element;
                this.RegSnippets[0].Location = CurrentSurface.GetScreenLocation(v_l.Center);
                float v_a = CoreMathOperation.GetAngle (
                    v_l.Center, 
                    v_l.Center + v_l .InnerRadius ) ;
                this.RegSnippets[1].Location = CurrentSurface.GetScreenLocation(
                new  Vector2f(
                    (float)(v_l.Center.X + v_l .InnerRadius .X * Math.Cos(v_a )), 
                    (float)(v_l.Center.Y + v_l .InnerRadius .Y * Math.Sin(v_a ))
                    ))
                    ;
                this.RegSnippets[2].Location = CurrentSurface.GetScreenLocation(
                    new  Vector2f(
                    (float)(v_l.Center.X + v_l .OuterRadius.X * Math.Cos(v_a )), 
                    (float)(v_l.Center.Y + v_l .OuterRadius.Y * Math.Sin(v_a ))
                    ));
                this.RegSnippets[3].Location = CurrentSurface.GetScreenLocation(
                  CoreMathOperation.GetPoint ( 
                   v_l.Center ,
                   v_l.OuterRadius .X,
                   v_l.OuterRadius .Y,
                   v_l.OffsetAngle 
                   ));
            }
        }
    }
}

