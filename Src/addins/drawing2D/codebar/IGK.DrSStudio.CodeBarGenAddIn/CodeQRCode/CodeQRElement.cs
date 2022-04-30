

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CodeQRElement.cs
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
file:CodeQRElement.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;


namespace IGK.DrSStudio.Drawing2D
{    
    
using IGK.QRCodeLib;
    using IGK.ICore.Codec;
    using IGK.ICore.WinUI;
    using IGK.ICore.ComponentModel;
    using IGK.ICore.WinUI.Configuration;
    using IGK.ICore.Drawing2D;
    using IGK.ICore;
    [CodeBarCategory ("QRCode", typeof (Mecanism ))]
    public sealed class CodeQRElement :
        CodeBarElementBase ,// RectangleElement, 
        ICoreCodeBarElement, ICodeBar 
    {
        private Vector2f m_Location;
        private float m_Size;
        private enuQRCodeErrorCorrection m_QrCodeErrorCorrection;
        private int m_QRVersion;
        private enuQRCodeEncodeMode  m_QrEncodeMode;
        private Rectanglef m_definitionBound;
        private QRCodeRectangle[] m_definition;
        [CoreXMLAttribute ()]
        [CoreXMLDefaultAttributeValue (enuQRCodeEncodeMode.AlphaNumeric )]
        [CoreConfigurableProperty(true)]
        public enuQRCodeEncodeMode  QrEncodeMode
        {
            get { return m_QrEncodeMode; }
            set
            {
                if (m_QrEncodeMode != value)
                {
                    m_QrEncodeMode = value;
                    GenerateDefinition(this.Text);
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue (7)]
        [CoreConfigurableProperty(true)]
        public int QrVersion
        {
            get { return m_QRVersion; }
            set
            {
                if ((m_QRVersion != value)&&(value >=2) && (value <=40))
                {
                    m_QRVersion = value;
                    GenerateDefinition(this.Text);
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue (enuQRCodeErrorCorrection.L )]
        [CoreConfigurableProperty(true)]
        public enuQRCodeErrorCorrection QrCodeErrorCorrection
        {
            get { return m_QrCodeErrorCorrection; }
            set
            {
                if (m_QrCodeErrorCorrection != value)
                {
                    m_QrCodeErrorCorrection = value;
                    GenerateDefinition(this.Text);
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        [CoreConfigurableProperty(true)]
        /// <summary>
        /// get or the text to encode
        /// </summary>
        public override string Text
        {
            get { return base.Text ; }
            set
            {
                if (base.Text  != value)
                {
                    
                    GenerateDefinition(value);
                    base.Text = value;
                    
                }
            }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue (0.0f)]
        public float Size
        {
            get { return m_Size; }
            set
            {
                if ((m_Size != value) && (value > 0))
                {
                    m_Size = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue ("0;0")]
        public Vector2f Location
        {
            get { return m_Location; }
            set
            {
                if (m_Location != value)
                {
                    m_Location = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        /// <summary>
        /// .ctr
        /// </summary>
        public CodeQRElement()
        {
               
        }
        protected override void InitializeElement()
        {
            base.InitializeElement();
            this.m_QRVersion = 7;
            this.m_QrCodeErrorCorrection = enuQRCodeErrorCorrection.L;
            this.m_QrEncodeMode = enuQRCodeEncodeMode.Byte;
            this.SmoothingMode = enuSmoothingMode.None;
            this.Text = "QRCODE_TEXT";
            this.SmoothingMode = enuSmoothingMode.None;
            this.GenerateDefinition(this.Text );  
        }
        void m_Marker_BrushDefinitionChanged(object sender, EventArgs e)
        {//raise brush definition
            OnPropertyChanged(new Core2DDrawingElementPropertyChangeEventArgs(enu2DPropertyChangedType.BrushChanged));
        }
        private void GenerateDefinition(string text)
        {
            QRCodeEncoder encoder = new QRCodeEncoder
            {
                QRCodeErrorCorrect = this.QrCodeErrorCorrection,
                QRCodeScale = 1,
                QRCodeVersion = this.QrVersion,
                QRCodeEncodeMode = this.QrEncodeMode
            };
            try
            { 
                QRCodeRectangle[] v_trc = encoder.EncodeToPath(text);
                m_definition = v_trc;
                m_definitionBound = GetBound(v_trc);
            }
            catch (Exception ex){
                CoreLog.WriteDebugLine("Can't generate Qr Code path : "+ex.Message );
            }
        }
        protected override void InitGraphicPath(CoreGraphicsPath v_p)
        {
            v_p.Reset();
            if (m_definition == null)
                return;
            Rectanglef v_brc = new Rectanglef(this.Location.X, this.Location.Y, this.Size, this.Size);
            if (!string.IsNullOrEmpty(this.Text))
            {
                //v_p.CloseFigure();
                Rectanglef v_rc = m_definitionBound;
                //v_rc.Inflate(4, 4);
                Rectanglef v_prc = CoreMathOperation.GetProportionalRectangle(v_brc , v_rc);
                Matrix v_mat = new Matrix();
                float ex = v_prc.Width / v_rc.Width;
                float ey = v_prc.Height / v_rc.Height;
                //v_mat.Scale(30, 30);
                v_mat.Scale( ex, ey, enuMatrixOrder.Append);
                v_mat.Translate(this.m_Location.X , this.m_Location.Y , enuMatrixOrder.Append);
                CoreGraphicsPath cpath = new CoreGraphicsPath();
                for (int i = 0; i < m_definition.Length; i++)
                {
                    cpath.AddRectangle(new Rectanglef(
                        m_definition[i].X,
                        m_definition[i].Y,
                        m_definition[i].Width,
                        m_definition[i].Height));
                }
                cpath.Transform(v_mat);
                //pav.Transform(v_mat);
                //v_p.SetMarkers();
                //v_p.AddPath(pav, false);
                //v_p.CloseFigure();
                //v_p.AddDefinition(pav.PathPoints.CoreConvertFrom<Vector2f[]>(), pav.PathTypes);
                //pav.Dispose();
                v_p.Add(cpath);
                //v_p.Transform(v_mat);
                cpath.Dispose();
                //v_p.AddRectangle(v_brc);
            }
            else {
                v_p.AddRectangle(v_brc);
                v_p.CloseFigure();
            }
        }
        private Rectanglef GetBound(QRCodeRectangle[] v_trc)
        {
            if ((v_trc == null) || (v_trc.Length == 0))
                return Rectanglef.Empty;
            Rectanglef v_rc;// = new Rectanglef();
            float minx = v_trc[0].X;
            float miny = v_trc[0].Y;
            float maxx = v_trc[0].X + v_trc[0].Width;
            float maxy = v_trc[0].Y + v_trc[0].Height;
            for (int i = 1; i < v_trc.Length; i++)
            {
                minx = Math.Min(minx, v_trc[i].X);
                miny = Math.Min(miny, v_trc[i].Y);
                maxx = Math.Max(maxx, v_trc[i].X + v_trc[i].Width);
                maxy = Math.Max(maxy, v_trc[i].Y + v_trc[i].Height);
            }
            v_rc = new Rectanglef(minx, miny, maxx - minx, maxy - miny);
            return v_rc;
        }
       
        public override bool CanRenderShadow
        {
            get
            {
                return false;
            }
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            //Type t = this.GetType();
            //var v_group = parameters.AddGroup("QRDefinition");
            //v_group.AddItem(t.GetProperty("EditBackground"));
            //v_group.AddItem(t.GetProperty("Text"));
            //v_group.AddItem(t.GetProperty("QrVersion"));
            //v_group.AddItem(t.GetProperty("QrEncodeMode"));
            //v_group.AddItem(t.GetProperty("QrCodeErrorCorrection"));
            return parameters;
        }
        new class Mecanism : RectangleElement.Mecanism 
        {
            internal new CodeQRElement Element {
                get {
                    return base.Element as CodeQRElement;
                }
            }
            protected override RectangleElement CreateNewElement()
            {
                return base.CreateNewElement();
            }
        
            protected override void GenerateSnippets()
            {
                this.DisableSnippet();
                if (this.Element != null)
                {
                    this.AddSnippet(CurrentSurface.CreateSnippet(this, 0, 0));
                    this.AddSnippet(CurrentSurface.CreateSnippet(this, 1, 1));
                }
            }
            protected override void InitSnippetsLocation()
            {
                CodeQRElement l = this.Element ;
                if (l != null)
                {
                    this.RegSnippets[0].Location = this.CurrentSurface.GetScreenLocation(l.Location);
                    this.RegSnippets[1].Location = this.CurrentSurface.GetScreenLocation(l.Location + l.Size);
                }
            }
            protected override void UpdateSnippetEdit(CoreMouseEventArgs e)
            {
                CodeQRElement l = this.Element;
                if ((l == null) || (this.Snippet == null))
                    return;
                switch (this.Snippet.Index)
                {
                    case 0:
                        l.m_Location = e.FactorPoint;                        
                        break;
                    case 1:
                        l.m_Size = (float)(CoreMathOperation.GetDistance ( e.FactorPoint , l.Location) / Math.Sqrt(2));
                        break;
                }
                this.Snippet.Location = e.Location;
                l.InitElement();
                this.CurrentSurface.RefreshScene();
            }
            protected override void BeginDrawing(CoreMouseEventArgs e)
            {
                base.BeginDrawing(e);
                CodeQRElement l = this.Element;
                l.m_Location = this.StartPoint;
            }
            protected override void UpdateDrawing(CoreMouseEventArgs e)
            {
                base.UpdateDrawing(e);
                this.EndPoint = e.FactorPoint;
                CodeQRElement l = this.Element;
                l.m_Size =(float)(CoreMathOperation.GetDistance(this.EndPoint, this.StartPoint)/ Math.Sqrt (2.0));
                l.InitElement();
                this.CurrentSurface.RefreshScene();
            }
        }
    }
}

