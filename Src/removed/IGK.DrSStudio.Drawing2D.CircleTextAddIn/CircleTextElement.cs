

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CircleTextElement.cs
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
file:CircleTextElement.cs
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore;using IGK.DrSStudio.Drawing2D.Menu;
    using IGK.DrSStudio.Drawing2D.WinUI;
    using IGK.DrSStudio.Codec;
    using IGK.DrSStudio.WinUI.Configuration;
    using IGK.DrSStudio.Drawing2D.Text;
    public enum CircleTextOrientation
    {
        InnerNormal,
        OuterNormal,
        OuterMirroring
    }
    [TextElementCategoryAttribute("CircleText", typeof(Mecanism), ImageKey = "DE_TextCircle")]
    public sealed class CircleTextElement : Core2DDrawingLayeredDualBrushElement, ICoreTextElement 
    {
        private Vector2f m_Center;
        private float m_Radius;        
        private string m_Text;
        private CircleTextOrientation m_Orientation;
        private float m_OffsetAngle; //in degree
        private float m_orientationAngle; // ind degree
        private float m_ScaleX;
        private float m_ScaleY;
        [CoreXMLAttribute (true)]
        [CoreXMLDefaultAttributeValue (1.0f)]
        public float ScaleY
        {
            get { return m_ScaleY; }
            set
            {
                if ((value != 0.0f) && (value != this.m_ScaleY))
                {
                    m_ScaleY = value;
                    OnPropertyChanged( CoreWorkingObjectPropertyChangedEventArgs .Definition);
                }
            }
        }
        [CoreXMLAttribute (true)]
        [CoreXMLDefaultAttributeValue (1.0f)]
        public float ScaleX
        {
            get { return m_ScaleX; }
            set
            {
                if ((value != 0.0f) && (value != this.m_ScaleX))
                {
                    m_ScaleX = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);         
                }
            }
        }
        [CoreXMLAttribute (true)]
        [CoreXMLDefaultAttributeValue (0.0f)]
        public float OrientationAngle
        {
            get { return m_orientationAngle ; }
            set
            {
                if ((this.m_orientationAngle != value) && ((value >= 0) && (value < 360)))
                {
                    m_orientationAngle  = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute (true)]
        [CoreXMLDefaultAttributeValue (0.0f)]
        public float OffsetAngle
        {
            get { return m_OffsetAngle; }
            set {
                if ((this.m_OffsetAngle !=value ) && (( value >=0)&&(value <360)))
                {
                    m_OffsetAngle = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        private float m_PortionAngle;
        [CoreXMLAttribute (true)]
        [CoreXMLDefaultAttributeValue (360.0f)]
        public float PortionAngle
        {
            get { return m_PortionAngle; }
            set
            {
                if ((this.m_PortionAngle != value) && ((value >= 0) && (value <= 360)))
                {
                    m_PortionAngle = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute (true)]
        [CoreXMLDefaultAttributeValue (CircleTextOrientation .InnerNormal)]
        public CircleTextOrientation Orientation
        {
            get { return m_Orientation; }
            set { m_Orientation = value;
            OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
            }
        }
        [CoreXMLAttribute (true)]
        [CoreXMLDefaultAttributeValue ("0;0")]
        public Vector2f  Center
        {
            get
            {
                return this.m_Center;
            }
            set
            {
                this.m_Center = value;
                OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
            }
        }
        public override enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.CustomControl;
        }
        public override IGK.DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(IGK.DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            return base.GetParameters(parameters);
        }
        public override Control GetConfigControl()
        {
            return new CircleTextEditor(this);
        }
        public override void RenderSelection(Graphics g, ICore2DDrawingSurface surface)
        {
            base.RenderSelection(g, surface);
            if (!surface.Mecanism.DesignMode)
                return;
            //Vector2f c = surface.GetScreenLocation(this.Center);           
            g.DrawEllipse (Pens.Yellow , surface.GetScreenBound (CoreMathOperation.GetBounds (this.Center, this.Radius )));
        }
        protected override void BuildBeforeResetTransform()
        {
            Matrix m = this.GetMatrix();
            if (m.IsIdentity) return;
            Vector2f[] t = new Vector2f[] { this.Center };
            m.TransformPoints (t);
            this.m_Center = t[0];
        }
        [CoreXMLAttribute  (true )]
        [CoreXMLDefaultAttributeValue  (1.0f)]
        public float Radius {
            get {
                return this.m_Radius;
            }
            set {
                this.m_Radius = value;
                OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
            }
        }
        [CoreXMLElement  (true )]
        [CoreXMLDefaultAttributeValue ("")]
        public string Text {
            get {
                return this.m_Text;
            }
            set {
                this.m_Text = value;
                OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
            }
        }
        public CircleTextElement()
        {
            this.m_Text = "Default";
            this.m_PortionAngle = 360.0f;
            this.m_OffsetAngle = 0.0f;
            this.m_orientationAngle = 0.0f;
            this.m_ScaleX = 1.0f;
            this.m_ScaleY = 1.0f;
            this.m_font = new CoreFont(this, CoreFont.GetInstalledFamilies()[0]);
            this.RegisterFont();
        }
        private void RegisterFont()
        {
            this.m_font.FontDefinitionChanged += new EventHandler(m_font_FontDefinitionChanged);
        }
        void m_font_FontDefinitionChanged(object sender, EventArgs e)
        {
            this.InitElement();
        }
        public static CircleTextElement CreateElement(string text, CoreFont font)
        {
            CircleTextElement cl = new CircleTextElement();
            return cl;
        }
        public override void Align( enuCore2DAlignElement alignment,Rectanglef bounds)
        {
            Matrix m = this.GetMatrix();
            Vector2f c =  CoreMathOperation.MultMatrixTransformPoint (m , this.Center)[0] ;
            Vector2f bc = CoreMathOperation.GetCenter (bounds );
            Vector2f p = CoreMathOperation.GetDistanceP(bc, c);
            switch (alignment)
            {
                case enuCore2DAlignElement.TopLeft:
                    break;
                case enuCore2DAlignElement.TopMiddle:
                    break;
                case enuCore2DAlignElement.TopRight:
                    break;
                case enuCore2DAlignElement.MiddleLeft:
                    break;
                case enuCore2DAlignElement.Center:
                    this.Translate(p.X, p.Y, enuMatrixOrder.Append);
                    return;                    
                case enuCore2DAlignElement.CenterVertical:
                    this.Translate(0, p.Y, enuMatrixOrder.Append);
                    return;                    
                case enuCore2DAlignElement.CenterHorizontal:
                    this.Translate(p.X, 0, enuMatrixOrder.Append);
                    return;                    
                case enuCore2DAlignElement.MiddleRight:
                    break;
                case enuCore2DAlignElement.BottomLeft:
                    break;
                case enuCore2DAlignElement.BottomMiddle:
                    break;
                case enuCore2DAlignElement.BottomRight:
                    break;
                default:
                    break;
            }
            base.Align(alignment , bounds);
        }
        protected override void GeneratePath()
        {
            GraphicsPath outP = new GraphicsPath();
            GraphicsPath cP = null;
            StringFormat sf = new StringFormat();
            Matrix matrix = new Matrix();
            float tetha = 0.0f;
            //e.Graphics.DrawEllipse(Pens.Yellow,
            //    new Rectangle(200, 200, 400, 400));
            string v_text = string.IsNullOrEmpty (this.Text )? IGK.DrSStudio.CoreSystem.GetString ("tip.StringIsEmpty"): m_Text ;
            int v_Direction = 1;
            float v_ex = 1.0f;
            float v_ey = 1.0f;
            float v_angle = 90;
            if (this.m_font == null)
            {
                this.m_font = new CoreFont(this, CoreFont.GetInstalledFamilies()[0]);
                this.RegisterFont();
            }
            if (this.m_font.Support(this.FontStyle) == false)
            {
                this.m_fontStyle = this.m_font.GetAllAvailableStyle();
            }
            switch (this.m_Orientation)
            {
                case CircleTextOrientation.InnerNormal :
                    break;
                case CircleTextOrientation.OuterNormal :
                    v_angle = 90;
                    v_ey = 1;
                    v_ex = -1;                    
                    break;
                case CircleTextOrientation.OuterMirroring :
                    v_angle = 90;                    
                    v_ey = -1;
                    v_ex = 1;
                    break;
            }
            v_ex *= ScaleX;
            v_ey *= ScaleY;
            float v_orientationAngle = this.OrientationAngle  * CoreMathOperation.ConvDgToRadian;
            float v_portionAngle = this.PortionAngle * CoreMathOperation.ConvDgToRadian;
            float period = (float)(this.PortionAngle * Math.PI / 180.0f);
            Vector2f v_cPCenter = Vector2f.Empty;
            for (int i = 0; i < v_text.Length; i++)
            {
                if (v_text[i] == ' ') continue;
                cP = new GraphicsPath();
                cP.AddString(v_text[i].ToString(),
                    this.Font.GetFontFamilly (),
                    (int)this.Font.FontStyle ,
                   this.Font.FontSize , Point.Empty,
                    sf);
                v_cPCenter = CoreMathOperation .GetCenter (cP.GetBounds());
                tetha = (float)(period * i / v_text.Length);
                matrix.Scale(
                    v_ex ,
                    v_ey ,
                    enuMatrixOrder.Append);
                //matrix.RotateAt(v_angle + (float)(tetha * 180 / Math.PI),v_cPCenter , enuMatrixOrder.Prepend );
                matrix.RotateAt(v_angle + (float)(tetha * 180 / Math.PI), v_cPCenter, enuMatrixOrder.Append );
                matrix.Translate(
                    (float)(this.Center.X + v_Direction *this.Radius  * Math.Cos(tetha + v_orientationAngle  )),
                    (float)(this.Center.Y + v_Direction *this.Radius  * Math.Sin(tetha + v_orientationAngle  )),
                    enuMatrixOrder.Append);
                cP.Transform(matrix);
                outP.SetMarkers();
                outP.AddPath(cP, false);
                matrix.Reset();
            }
            matrix.RotateAt(this.OffsetAngle, this.Center);
            outP.Transform(matrix);
            this.SetPath(outP);
        }
        new class Mecanism : Core2DDrawingLayeredDualBrushElement.Mecanism  
        {
            //snippetFlags
            const int SN_CENTER = 10;
            const int SN_RADIUS = 11;
            const int SN_FONTSIZE = 12;
            new CircleTextElement Element { get { return base.Element as CircleTextElement ; }
                set { base.Element = value; }
            }
            protected override void OnPaint(PaintEventArgs e)
            {
                if ((this.Element == null) || (!this.Element.Visible))
                    return;
            }
            protected override void GenerateSnippets()
            {                
                base.GenerateSnippets();
              this.AddSnippet ( this.CurrentSurface .CreateSnippet (this, SN_CENTER, SN_CENTER));
                this.AddSnippet ( this.CurrentSurface .CreateSnippet (this, SN_RADIUS, SN_RADIUS));
                this.AddSnippet ( this.CurrentSurface .CreateSnippet (this, SN_FONTSIZE, SN_FONTSIZE));
            }
            protected override void InitSnippetsLocation()
            {
                base.InitSnippetsLocation();
                if (this.RegSnippets.Count > 3)
                {
                CircleTextElement c = this.Element;
                this.RegSnippets[SN_CENTER].Location = (CurrentSurface.GetScreenLocation(this.Element.Center));
                float angle = CoreMathOperation.GetAngle(c.Center, c.Center + c.Radius);
                float dx = (float)Math.Cos(angle) * c.Radius;
                float dy = (float)Math.Sin(angle) * c.Radius;
                this.RegSnippets[SN_RADIUS].Location = (CurrentSurface.GetScreenLocation(new Vector2f(c.Center.X + dx, c.Center.Y + dy)));// +(float) Math.Sqrt (c.Radius)));
                //RegSnippets[SN_RADIUS].SetLocation(CurrentSurface.GetScreenLocation(
                //           new Vector2f(this.Element.Center.X + this.Element.Radius,
                //               this.Element.Center.Y + this.Element.Radius
                //                )));
                this.RegSnippets[SN_FONTSIZE].Location  = (CurrentSurface.GetScreenLocation(
                    new Vector2f(this.Element.Center.X + this.Element.Radius - this.Element.FontSize ,
                        this.Element.Center .Y )));
                    }
            }
            protected override void OnMouseDown(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case System.Windows.Forms.MouseButtons.Left :
                        switch (this.State)
                        {
                            case ST_NONE:
                            case ST_CREATING :
                                this.Element = this.CreateNewElement () as CircleTextElement ;
                                if (this.Element != null)
                                {
                                    this.StartPoint = e.FactorPoint;
                                    this.Element.Center = e.FactorPoint;
                                    this.Element.Radius = 1.0f;
                                    this.CurrentLayer.Elements.Add(this.Element);
                                    this.CurrentLayer.Select(this.Element);
                                    this.Element.Invalidate(true);
                                    this.State = ST_CREATING;
                                }
                                break;
                            case ST_EDITING :
                                if (this.Element.Contains(e.FactorPoint))
                                {
                                    this.BeginMove(e);
                                }
                                break;
                        }
                        break;
                    case System.Windows.Forms.MouseButtons.Right :
                        this.AllowContextMenu  = false;
                        break;
                    case MouseButtons.Left | MouseButtons.Right :
                        break;
                }                
            }
            protected override void OnMouseMove(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case System.Windows.Forms.MouseButtons.Left :
                        switch (State)
                        {
                            case ST_CREATING :
                                if (this.Element != null)
                                {
                                    this.Element.Invalidate(false);
                                    this.Element.Radius = CoreMathOperation.GetDistance(e.FactorPoint, this.StartPoint);
                                    this.Element.FontSize = this.Element.Radius / 3.0f;
                                    this.Element.InitElement();
                                    this.Element.Invalidate(true);
                                }
                                else
                                {
                                    this.State = ST_NONE;
                                }
                                break;
                            case ST_EDITING :
                                if((this.Snippet !=null ) && (this.Snippet .Demand != ST_NONE ))
                                {
                                    this.Element.Invalidate(false);
                                    switch (this.Snippet .Demand)
                                    {
                                        case SN_CENTER :
                                            this.Element.Center = e.FactorPoint;
                                            break;
                                        case SN_FONTSIZE :
                                            float v_fSize = 0.0f;
                                            v_fSize = this.Element.Radius - CoreMathOperation.GetDistance(this.Element.Center, e.FactorPoint);
                                            this.Element.FontSize = v_fSize;
                                            break;
                                        case SN_RADIUS :
                                            this.Element.Radius = CoreMathOperation.GetDistance(this.Element.Center, e.FactorPoint);
                                            break;
                                    }
                                    this.Element.Invalidate(true);
                                    this.Snippet.Location = (e.Location);
                                    return;
                                }
                                break;
                            case ST_MOVING :
                                this.UpdateMove(e);
                                break;
                        }
                        break;
                }                
            }
            protected override void OnMouseUp(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case System.Windows.Forms.MouseButtons.Left:
                        switch (State)
                        {
                            case ST_CREATING:
                                if (this.Element != null)
                                {
                                    this.Element.Invalidate(false);
                                    this.Element.Radius = CoreMathOperation.GetDistance(e.FactorPoint, this.StartPoint);
                                    this.Element.Invalidate(true);
                                    this.State = ST_EDITING;
                                }
                                else
                                {
                                    this.State = ST_NONE;
                                }
                                return;
                            case ST_EDITING :
                                break;
                            case ST_MOVING :
                                this.EndMove(e);
                                this.State = ST_EDITING;
                                break;
                        }
                        break;
                    case MouseButtons.Right :
                        if (this.Element != null)
                        {
                            this.EndEdition();
                            this.State = ST_NONE;
                        }
                        break;
                }                
            }
        }
        #region ICoreFontObject Members
        private CoreFont m_font;
        [IGK.DrSStudio.Codec.CoreXMLAttribute ()]
        public CoreFont Font
        {
            get
            {
                return m_font;
            }
            set
            {
                if (this.m_font != value)
                {
                    m_font = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        private FontStyle m_fontStyle;
        public FontStyle FontStyle
        {
            get
            {
                return m_fontStyle;
            }
            set
            {
                if (this.m_fontStyle != value)
                {
                    this.m_fontStyle = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        private float m_fontSize;
        public float FontSize
        {
            get
            {
                if (this.m_fontSize <= 0)
                    this.m_fontSize = 1.0f;                    
                return m_fontSize;
            }
            set
            {
                if ((this.m_fontSize != value) && (value >= 1))
                {
                    this.m_fontSize = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        #endregion
        #region ICoreTextElement Members
        ICoreFont ICoreTextElement.Font
        {
            get { return this.Font; }
        }
        #endregion
    }
}

