

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: TextElement.cs
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
file:TextElement.cs
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
using System.Drawing;
using System.Drawing.Drawing2D ;
using System.Drawing.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore;using IGK.DrSStudio ;
    using IGK.DrSStudio.WinUI ;
    using IGK.DrSStudio.Drawing2D.TextElementActions;
    using IGK.DrSStudio.WinUI.Configuration;
    /// <summary>
    /// represent the base text element
    /// </summary>
    [Core2DDrawingStandardItem ("Text",
        typeof (Mecanism ),
        Keys = Keys.T )]
    public class TextElement : 
        Core2DDrawingLayeredDualBrushElement ,
        ICore2DTextElement,
        ICore2DFillModeElement ,
        ICoreWorkingConfigurableObject 
    {
        internal protected const char LINE_SEPARATOR = '\n';
        static readonly StringFormatFlags StringFormatFlag =
             StringFormatFlags.NoWrap
                | StringFormatFlags.FitBlackBox
                | StringFormatFlags.MeasureTrailingSpaces
                | StringFormatFlags.NoFontFallback
                | StringFormatFlags.DisplayFormatControl
                | StringFormatFlags.NoClip;
        private bool m_ClipToBound;
        private enuTextRenderingMode m_TextRenderingHint;
        private int m_TextContrast;
        private bool m_AutoAdjust;
        public event EventHandler  TextChanged;
        [IGK.DrSStudio.Codec.CoreXMLAttribute()]
        [IGK.DrSStudio.Codec.CoreXMLDefaultAttributeValue(false)]   
        /// <summary>
        /// get or set if the text element must auto adjust when text property changed.
        /// default is false.
        /// </summary>
        public bool AutoAdjust
        {
            get { return m_AutoAdjust; }
            set
            {
                if (m_AutoAdjust != value)
                {
                    m_AutoAdjust = value;
                    this.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);             
                }
            }
        }
        [IGK.DrSStudio.Codec.CoreXMLAttribute()]
        [IGK.DrSStudio.Codec.CoreXMLDefaultAttributeValue(4)]    
        public int TextContrast
        {
            get { return m_TextContrast; }
            set
            {
                if ((m_TextContrast != value)&&(value > 0) && (value <= 12))
                {
                    m_TextContrast = value;
                    this.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);             
                }
            }
        }
        [IGK.DrSStudio.Codec.CoreXMLAttribute()]
        [IGK.DrSStudio.Codec.CoreXMLDefaultAttributeValue(enuTextRenderingMode.AntiAliazed )]
        public enuTextRenderingMode TextRenderingHint
        {
            get { return m_TextRenderingHint; }
            set
            {
                if (m_TextRenderingHint != value)
                {
                    m_TextRenderingHint = value;
                    this.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [IGK.DrSStudio.Codec.CoreXMLAttribute ()]
        [IGK.DrSStudio.Codec.CoreXMLDefaultAttributeValue (true)]
        /// <summary>
        /// clip this element to region
        /// </summary>
        public bool ClipToBound
        {
            get { return m_ClipToBound; }
            set
            {
                if (m_ClipToBound != value)
                {
                    m_ClipToBound = value;
                    this.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition );
                }
            }
        }
        public TextElement()
        {
            this.m_Text = "text";
            this.m_RenderingMode = enuTextElementRenderingMode.Path;
            this.m_TextRenderingHint = enuTextRenderingMode.AntiAliazed;
            this.m_TextContrast = 4;            
            this.m_font = new CoreFont(this,"Courier New");
            this.m_font.FontDefinitionChanged += new EventHandler(m_font_FontDefinitionChanged);            
            this.m_ClipToBound = true;
        }
        public override IGK.DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(IGK.DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            ICoreParameterConfigCollections p = base.GetParameters(parameters);
            ICoreParameterGroup group  = p.AddGroup("Definition");
            group.AddItem(GetType().GetProperty("enuFillMode"));
            group.AddItem(GetType().GetProperty("AutoAdjust"));
            group.AddItem("ClipToBound", "lb.ClipToBound",this.ClipToBound , enuParameterType .Bool, 
                new CoreParameterChangedEventHandler (prop_ClipToBound) );
            group = p.AddGroup("GraphicsProperties","lb.GraphicsProperties.caption");
            group.AddItem(GetType().GetProperty("RenderingMode"));
            group.AddItem(GetType().GetProperty("TextRenderingHint"),"lb.TextRenderingHint.caption");
            group.AddItem("TextContrast", "txb.TextContrast", 
                this.TextContrast ,
                enuParameterType.SingleNumber , 
                new CoreParameterChangedEventHandler(prop_TextContrast));
            group = p.AddGroup("Value");
            group.AddItem(GetType().GetProperty("Content"));
            return p;
        }
        private void prop_TextContrast(object sender, CoreParameterChangedEventArgs e)
        {
            this.TextContrast = Convert.ToInt32  (e.Value);
        }
        private void prop_ClipToBound(object sender, CoreParameterChangedEventArgs e)
        {
            this.ClipToBound = Convert.ToBoolean(e.Value);
        }
        void m_font_FontDefinitionChanged(object sender, EventArgs e)
        {
            InitElement();
            OnPropertyChanged(new Core2DDrawingElementPropertyChangeEventArgs(enu2DPropertyChangedType.FontChanged)); 
        }
        //single font of the text
        private CoreFont m_font;
        /// <summary>
        /// global bound area
        /// </summary>
        private Rectanglef m_bound;
        /// <summary>
        /// corresponding single line bound
        /// </summary>
        private Rectanglef m_singleLineBound;
        public Rectanglef SingleLineBound { get { return this.m_singleLineBound; } }
        [IGK.DrSStudio.Codec.CoreXMLAttribute ()]
        /// <summary>
        /// get ret
        /// </summary>
        public Rectanglef Bound
        {
            get { return this.m_bound; }
            set
            {
                if (!this.m_bound.Equals(value))
                {
                    this.m_bound = value;
                    this.GenerateSingleLineFromBound();
                    this.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Id);
                }
            }
        }
        public int LineCount {
            get {
                if (string.IsNullOrEmpty(this.Content))
                    return 0;
                return this.Content.Split(LINE_SEPARATOR ).Length;
            }
        }
        public string[] Lines {
            get {
                return this.Content.Split(LINE_SEPARATOR );
            }
        }
        private enuTextElementRenderingMode m_RenderingMode;
        private enuFillMode m_FillMode;
        public enuFillMode enuFillMode
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
        private string m_Text;
        [IGK.DrSStudio.Codec.CoreXMLElement ()]
        [IGK.DrSStudio.Codec.CoreXMLDefaultAttributeValue ("text")]
        /// <summary>
        /// get or set the text of this element
        /// </summary>
        public string Content
        {
            get { return m_Text; }
            set
            {
                if (m_Text != value)
                {
                    m_Text = value;
                    this.OnTextChanged(EventArgs.Empty);
                    this.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);       
                }
            }
        }
        private void OnTextChanged(EventArgs eventArgs)
        {
            if (this.TextChanged != null)
                this.TextChanged(this, eventArgs);
        }
        [IGK.DrSStudio.Codec.CoreXMLAttribute()]
        [IGK.DrSStudio.Codec.CoreXMLDefaultAttributeValue(enuTextElementRenderingMode.Path )]
        public enuTextElementRenderingMode RenderingMode
        {
            get { return m_RenderingMode; }
            set
            {
                if (m_RenderingMode != value)
                {
                    m_RenderingMode = value;
                    this.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        /// <summary>
        /// get property of the font size according to current bound definition
        /// </summary>
        internal void GenerateSingleLineFromBound()
        {
            int v_lineCount  = this.LineCount ;
            if (v_lineCount ==0)
            {
                this.m_singleLineBound = Rectanglef .Empty ;
                this.m_font .FontSize = 1.0f;
                return ;
            }
            float v_fontsize = (this.m_bound.Height / (float)v_lineCount) * this.m_font.GetEmHeight() / this.m_font.GetLineSpacing();
            this.m_font.FontSize = v_fontsize;// (this.m_bound.Height / v_lineCount) * this.m_font.GetEmHeight() / this.m_font.GetLineSpacing();
             this.m_singleLineBound = new Rectanglef (
                    this.m_bound.X , 
                    this.m_bound.Y, 
                    this.m_bound.Width ,
                    this.m_font.FontSize );
        }
        private void GenerateSingleLineFromLoadingComplete()
        {
            int v_lineCount = this.LineCount;
            if (v_lineCount == 0)
            {
                this.m_singleLineBound = Rectanglef.Empty;
                this.m_font.FontSize = 1.0f;
                return;
            }
            //float v_fontsize = (this.m_bound.Height / (float)v_lineCount) * this.m_font.GetEmHeight() / this.m_font.GetLineSpacing();
            //this.m_font.FontSize = v_fontsize;// (this.m_bound.Height / v_lineCount) * this.m_font.GetEmHeight() / this.m_font.GetLineSpacing();
            this.m_singleLineBound = new Rectanglef(
                   this.m_bound.X,
                   this.m_bound.Y,
                   this.m_bound.Width,
                   this.m_font.FontSize);
        }
        /// <summary>
        /// get property of the font size according to current bound definition
        /// </summary>
        internal void GenerateSingleLineFromFont()
        {
            int i = this.LineCount;
            if (i == 0)
            {
                this.m_singleLineBound = Rectanglef.Empty;
                this.m_font.FontSize = 1.0f;
                return;
            }
             this.m_singleLineBound = new Rectanglef(
                this.m_bound.X,
                this.m_bound.Y,
                this.m_bound.Width,
                this.m_font.FontSize);
        }
        protected override void OnLoadingComplete(EventArgs eventArgs)
        {
            base.OnLoadingComplete(eventArgs);
            //this.GenerateSingleLineFromBound();
            this.GenerateSingleLineFromLoadingComplete();
        }
        protected override void BuildBeforeResetTransform()
        {
            Matrix m = this.GetMatrix ();
            this.m_bound = CoreMathOperation.ApplyMatrix(this.m_bound, m);
            this.GenerateSingleLineFromBound();            
        }
        protected override void SetGraphicsProperty(Graphics g)
        {
            base.SetGraphicsProperty(g);
            g.TextRenderingHint =(TextRenderingHint ) this.TextRenderingHint;
            g.TextContrast = this.TextContrast;
        }
        public override void Draw(Graphics g)
        {
            switch (this.RenderingMode)
            {
                case enuTextElementRenderingMode.Path:
                    {
                    GraphicsState s = g.Save();
                        if (this.ClipToBound && (this.TempMatrix ==null))
                            g.Clip = new Region(this.GetBound());
                    base.Draw(g);
                    g.Restore(s);
                      }
                    break;
                case enuTextElementRenderingMode.Text:
                    {
                        Matrix m = this.GetMatrix();
                        GraphicsState s = g.Save();
                        if (this.ClipToBound && (this.TempMatrix == null))
                            g.Clip = new Region(this.GetBound());
                        this.SetGraphicsProperty(g);
                        g.MultiplyTransform(m, enuMatrixOrder.Prepend);
                        Font ft = this.m_font.GetFont();
                        Rectanglef v_rc = new Rectanglef (this.m_singleLineBound.X,
                        this.m_singleLineBound.Y ,
                        m_bound.Width, 
                        this.m_singleLineBound.Height );
                        StringFormat v_sf = this.GetNewStringFormat();
                        foreach (string v_txt in this.Lines)
                        {
                            g.DrawString(v_txt, ft,
                                this.FillBrush.GetBrush(),
                                v_rc,
                                v_sf);
                            v_rc.Y += this.m_font.GetLineHeight();
                        }
                        g.Restore(s);
                        v_sf.Dispose();
                    }
                    break;
            }
        }
        protected override void GeneratePath()
        {
            CoreGraphicsPath v_path = new CoreGraphicsPath();
            StringFormat v_stringFormat = GetNewStringFormat(); 
            if (this.AutoAdjust)
            {
                //measuree string
                Graphics g = Graphics.FromHwnd(IntPtr.Zero);
                SizeF sf = g.MeasureString(this.Content,
                    m_font.GetFont(),
                    new SizeF(short.MaxValue, short.MaxValue),
                    v_stringFormat);
                m_bound = new RectangleF(m_bound.Location, sf);
                this.m_singleLineBound.Width = m_bound.Width;
                g.Dispose();
            }
            if (this.RenderingMode == enuTextElementRenderingMode.Path)
            {
                FontFamily v_fontFamily = new FontFamily(m_font.FontName);
                v_path.AddString(this.Content,
                    v_fontFamily,
                    (int)m_font.FontStyle,
                    m_font.FontSize,
                    m_bound,
                    v_stringFormat);
                v_fontFamily.Dispose();
            }
            else {
                if (this.AutoAdjust)
                {
                    //measuree string
                    Graphics g = Graphics.FromHwnd(IntPtr.Zero);
                    SizeF sf = g.MeasureString(this.Content,
                        m_font.GetFont(),
                        new SizeF(short.MaxValue, short.MaxValue),
                        v_stringFormat);
                    m_bound = new RectangleF(m_bound.Location, sf);
                    g.Dispose();
                }
                v_path.AddRectangle(this.m_bound);
            }
            v_stringFormat.Dispose();
            v_path.enuFillMode = this.enuFillMode;           
            this.SetPath(v_path);
        }
        /// <summary>
        /// return new string format
        /// </summary>
        /// <returns></returns>
        protected StringFormat GetNewStringFormat()
        {//get a new string format
            StringFormat v_stringFormat = new StringFormat();
            v_stringFormat.Trimming = StringTrimming.None;
            v_stringFormat.FormatFlags = StringFormatFlag;
            v_stringFormat.Alignment = m_font.HorizontalAlignment;
            v_stringFormat.LineAlignment = m_font.VerticalAlignment;
            return v_stringFormat;
        }
        public override Rectanglef GetBound()
        {
            return CoreMathOperation.ApplyMatrix(this.m_bound, this.Matrix);
        }
        public override void RenderSelection(Graphics g, IGK.DrSStudio.Drawing2D.WinUI.ICore2DDrawingSurface surface)
        {            
            Rectanglef v_rc = surface.GetScreenBound(this.GetSelectionBound());
            using (Pen p = new Pen(new HatchBrush(HatchStyle.Percent60, Color.Black, Color.Transparent)))
            {
                p.Width = 4;
                g.DrawRectangle(p, Rectanglef.Round (v_rc));
                System.Windows.Forms.ControlPaint.DrawBorder(
                    g,
                    Rectanglef.Round(v_rc),
                    System.Drawing.Color.White,
                     System.Windows.Forms.ButtonBorderStyle.Solid);
                System.Windows.Forms.ControlPaint.DrawBorder(
                    g,
                    Rectanglef.Round(v_rc),
                    System.Drawing.Color.Black,
                     System.Windows.Forms.ButtonBorderStyle.Dotted);
            }
        }
        #region ICore2DTextElement Members
        [IGK.DrSStudio.Codec.CoreXMLAttribute()]
        public ICoreFont Font
        {
            get { return this.m_font; }
        }
        #endregion
        /// <summary>
        /// represent the base mecanism of the element
        /// </summary>
        public new class Mecanism : Core2DDrawingLayeredDualBrushElement.Mecanism
        {
            private ICoreCaret m_coreCaret;            
            private int m_line; //line position
            private int m_posx; //col position
            private int m_charCount; // number of character in 
            private int m_selection;// get the current selection
            private TextBuffer m_textBuffer;//represent the text buffer
            public int PosX { get { return m_posx; } set { m_posx = value; } }
            public int PosLine { get { return m_line; } set { m_line = value; } }
            //maintain the selection 
            public int Selection { get { return m_selection; } set { m_selection = value; } }
            public int CharCount { get { return this.m_charCount; } set { this.m_charCount = value; } }
            /// <summary>
            /// get the text element
            /// </summary>
            public new TextElement Element{
                get{return base.Element as TextElement ;}
                set{base.Element = value ;}
            }
            protected internal override void RegisterSurface(IGK.DrSStudio.Drawing2D.WinUI.ICore2DDrawingSurface surface)
            {
                base.RegisterSurface(surface);
                this.m_coreCaret = surface.CreateCaret();
            }
            public override void Edit(ICoreWorkingObject element)
            {
                base.Edit(element);
                if (this.Element != null)
                {
                    InitCaret(true);
                }
            }
            public Mecanism()
            {
                this.m_textBuffer = new TextBuffer(this) ;
                this.SnippetChanged += new EventHandler(Mecanism_SnippetChanged);
            }
            void Mecanism_SnippetChanged(object sender, EventArgs e)
            {
            }
            protected internal override void UnRegisterSurface(IGK.DrSStudio.Drawing2D.WinUI.ICore2DDrawingSurface surface)
            {
                base.UnRegisterSurface(surface);
            }
            public override void Dispose()
            {
                if (this.m_coreCaret != null)
                {
                    this.m_coreCaret.Dispose();
                    this.m_coreCaret = null;
                }
                base.Dispose();
            }
            protected override void OnSurfaceSizeChanged()
            {
                base.OnSurfaceSizeChanged();
                if (this.Element != null) InitCaret(true);
            }
            protected override void OnSurfaceZoomChanged()
            {
                base.OnSurfaceZoomChanged();
                if (this.Element != null) InitCaret(true);
            }
            internal void SetStringContent(string txt)
            {
                this.m_textBuffer.Populate();
            }
            protected override void OnElementChanged(CoreElementChangedEventArgs<ICoreWorkingObject> e)
            {                
                if (this.Element != null)
                {
                    TextElement v_l = this.Element;
                    this.SetStringContent(v_l.Content);
                    this.InitTextProperty();
                }
                else {
                    if (this.m_coreCaret !=null)
                        this.m_coreCaret.Hide();
                }
                base.OnElementChanged(e);
            }
            /// <summary>
            /// init text property
            /// </summary>
            private void InitTextProperty()
            {
                this.m_line = Element.LineCount;
                this.m_posx = Element.Lines[this.m_line -1].Length;
                this.m_charCount = 0;
                foreach (string txt in this.Element.Lines)
                {
                    this.m_charCount += txt.Length;
                }
            }
            protected override void UnRegisterElementEvent(ICore2DDrawingObject element)
            {
                base.UnRegisterElementEvent(element);
                TextElement p = element as TextElement;
                if (p != null)
                {
                    p.Font.FontDefinitionChanged -= new EventHandler(Element_FontDefinitionChanged);
                    p.TextChanged -= new EventHandler(p_TextChanged);
                }
            }
            protected override void RegisterElementEvent(ICore2DDrawingObject element)
            {
                base.RegisterElementEvent(element);
                TextElement p = element as TextElement;
                if (p !=null )
                {
                    p.Font.FontDefinitionChanged += new EventHandler(Element_FontDefinitionChanged);
                    p.TextChanged += new EventHandler(p_TextChanged);
                }
            }
            void p_TextChanged(object sender, EventArgs e)
            {
                this.m_textBuffer.Populate();
            }
            void Element_FontDefinitionChanged(object sender, EventArgs e)
            {
                if (this.State == ST_CONFIGURING)
                {
                    BuildAndInitFont();               
                }
            }
            /// <summary>
            /// this method is used to setup bound from font property according to line count
            /// </summary>
            internal void BuildAndInitFont()
            {
                TextElement v_l = this.Element;
                v_l.GenerateSingleLineFromFont();
                float v_lSpacing = v_l.m_font.GetLineSpacing();
                float v_lEmH = v_l.m_font.GetEmHeight();
                float v_h =
                     (v_l.Font.FontSize * v_l.LineCount) / (v_lEmH / v_lSpacing);
                v_l.m_bound =
                    new Rectanglef(v_l.SingleLineBound .X,
                        v_l.SingleLineBound.Y,
                        v_l.SingleLineBound.Width,
                        v_h);
                this.InitSnippetsLocation();
                this.UpdateCaretPos();
            }
            protected override void BeginMove(WinUI.CoreMouseEventArgs e)
            {
                base.BeginMove(e);
                if (this.m_coreCaret != null)
                {
                    this.m_coreCaret.Hide();
                }
            }
            protected override void EndMove(WinUI.CoreMouseEventArgs e)
            {
                base.EndMove(e);
                if (this.m_coreCaret != null && (this.Element !=null))
                {
                    this.InitCaret(true);
                }
            }
            protected override void OnMouseDown(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case System.Windows.Forms.MouseButtons .Left :
                        switch (this.State)
                        {
                            case ST_EDITING:
                            case ST_CONFIGURING :
                                if (this.Snippet ==null)
                                {
                                    if((this.Element !=null) && (this.Element .Contains (e.FactorPoint )))
                                    {
                                        this.BeginMove(e);
                                    }
                                }
                                else{
                                    if (this.m_coreCaret !=null){
                                    this.m_coreCaret .Hide ();
                                    }
                                }
                                break;
                            case ST_NONE :
                            case ST_CREATING :                            
                                this.Element = this.CreateNewElement() as  TextElement;
                                if (this.Element != null)
                                {
                                    this.StartPoint = e.FactorPoint;
                                    this.EndPoint = e.FactorPoint;
                                    this.CurrentSurface.CurrentDocument.CurrentLayer.Elements.Add(this.Element);
                                    this.CurrentSurface.CurrentDocument.CurrentLayer.Select(this.Element);
                                    this.Element.InitElement();
                                    this.GenerateSnippets();
                                    this.State = ST_CREATING;
                                }
                                break;
                        }
                        break;
                }
            }
            protected override void OnMouseMove(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case System.Windows.Forms.MouseButtons.Left:
                        switch (this.State)
                        {
                            case ST_NONE:
                                break;
                            case ST_MOVING :
                                this.UpdateMove(e);
                                break;
                            case ST_CREATING :
                                UpdateCreateElement(e);
                                break;
                            case ST_CONFIGURING :
                                if (this.Snippet != null)
                                {
                                    this.State = ST_EDITING;
                                    UpdateSnippetElement(e);
                                }
                                break;
                            case ST_EDITING :
                                if (this.Snippet != null)
                                    UpdateSnippetElement(e);
                                break;
                        }
                        break;
                }
            }
            protected override void OnMouseUp(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case System.Windows.Forms.MouseButtons.Left:
                        switch (this.State)
                        {
                            case ST_NONE:
                                break;
                            case ST_MOVING :
                                this.EndMove(e);
                                //
                                Matrix m = this.Element.Matrix;
                                this.Element.m_bound = CoreMathOperation.ApplyMatrix(this.Element.m_bound, m);
                                this.Element.m_singleLineBound = CoreMathOperation.ApplyMatrix(this.Element.m_singleLineBound , m);
                                m.Reset();
                                this.Element.InitElement();
                                this.InitSnippetsLocation();
                                this.CurrentSurface .Invalidate();
                                this.UpdateCaretPos();
                                break;
                            case ST_CREATING:
                                UpdateCreateElement(e);
                                this.State = ST_CONFIGURING;
                                this.Element.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                                this.InitSnippetsLocation();
                                this.InitCaret(true );
                                break;
                            case ST_EDITING:
                                if (this.Snippet != null)
                                {
                                    UpdateSnippetElement(e);
                                    this.Element.GenerateSingleLineFromBound();
                                    this.Element.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                                    InitSnippetsLocation();
                                    InitCaret(true);                                    
                                }
                                this.State = ST_CONFIGURING;
                                break;
                            case ST_CONFIGURING :
                                break;
                        }
                        break;
                }
            }
            private void InitCaret(bool show)
            {
                int v_h = (int)Math.Floor(CurrentSurface.GetScreenLocation(new Vector2f(0,
                    this.Element.Font.FontSize)).Y);
                int v_h2 = (int)(this.Element.Font.FontSize * this.CurrentSurface.ZoomY);
                this.m_coreCaret.Size = new Size(1, v_h2);
                UpdateCaretPos();
                if (show)
                this.m_coreCaret.Show();
            }
            internal void UpdateCaretPos()
            {
                //set caret position for string element
                CoreFont v_ft = this.Element.m_font;
                int v_linePos = this.m_line-1;
                if ((v_linePos == -1) )
                     return;
                    System.Diagnostics.Debug.Assert(!(this.Element.Lines.Length < v_linePos), "bad cursor position");
                    string v_txtb = this.Element.Lines[v_linePos];
                    RectangleF v_rc = this.Element.m_bound;
                    Vector2f v_pos = v_rc.Location;
                    float p = (v_ft.GetLineSpacing() + v_ft.GetCellDescent());
                    float em = v_ft.GetEmHeight();
                    float lineHeight = v_ft.GetLineHeight();
                if ((this.Element.LineCount == 0 ) || string.IsNullOrEmpty(v_txtb))
                {
                    v_pos.Y += (v_linePos * lineHeight);
                    v_pos = this.CurrentSurface.GetScreenLocation(v_pos);
                    this.m_coreCaret.Location = Vector2f.Round(v_pos);
                    return;
                }
                if ((v_linePos >= this.Element.LineCount)||(PosX <0))
                {
                    return;
                }
                string v_txt = v_txtb.Replace(' ', '_').Substring (0, PosX );
                //measeure text 
                Graphics g = Graphics.FromHwnd(IntPtr.Zero );
                StringFormat v_sf = new StringFormat ();
                Font  v_fft = v_ft.GetFont ();
                v_sf.LineAlignment = v_ft.VerticalAlignment;
                v_sf.Alignment = v_ft.HorizontalAlignment;
                v_sf.SetMeasurableCharacterRanges(new CharacterRange[] { 
                    new CharacterRange (0, this.PosX  )
                });
                float w = g.MeasureString (v_txt, this.Element.Font.GetFont (),
                    new SizeF (this.Element.Bound.Width, this.Element.Bound.Height ) ,
                    v_sf ).Width ;
                switch (v_ft.HorizontalAlignment)
                {
                    case StringAlignment .Far:
                        v_pos.X += this.Element.Bound .Width ;
                        break;
                    case StringAlignment .Center :
                        Region[] rg = g.MeasureCharacterRanges(v_txtb, v_fft,
                  v_rc,
                  v_sf);
                        if (rg.Length == 1)
                        {
                         RectangleF v_crc = rg[0].GetBounds(g);
                         v_pos.X = v_crc.Right ;
                        }
                        break;
                    default :
                        v_pos.X += w;
                        break;
                }
                v_pos.Y +=   (v_linePos * lineHeight )  ;
                v_pos = this.CurrentSurface.GetScreenLocation(v_pos);
                this.m_coreCaret.Location = Vector2f.Round ( v_pos);
                v_sf.Dispose();
                g.Dispose();
            }
            protected override void OnKeyPress(KeyPressEventArgs e)
            {
                 if (this.Element == null)
                    return;
                float lineHeight =0;
                switch (e.KeyChar)
                {
                    case '\r':
                        lineHeight = this.Element.m_font.GetLineHeight();
                        this.m_textBuffer.AppendNewLine();
                        Rectanglef v_rc = this.Element.Bound;
                        v_rc .Height += lineHeight ;
                        this.m_line++;
                        this.m_posx = 0;
                        this.Element.Bound = v_rc;
                        this.InitSnippetsLocation();
                        break;
                    default :
                        if (!char.IsControl(e.KeyChar))
                        {
                            this.TextBuffer.Append(e.KeyChar);                            
                        }
                        break;
                }                
                e.Handled = true;
                this.Element.InitElement();
                this.UpdateCaretPos();
                this.CurrentSurface.Invalidate();
                base.OnKeyPress(e);
            }
            internal void UpdateElementBound()
            {
                if (this.Element.LineCount > 0)
                {
                    float lineHeight = this.Element.m_font.GetLineHeight();
                    Rectanglef rc = this.Element.Bound;
                    rc.Height = lineHeight * this.Element.LineCount;
                    this.Element.Bound = rc;
                }
            }
            public override void EndEdition()
            {
                this.m_coreCaret.Hide();
                base.EndEdition();
            }
            protected override  void UpdateCreateElement(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                this.EndPoint = e.FactorPoint;
                this.Element.m_bound = CoreMathOperation.GetBounds(this.StartPoint, this.EndPoint);
                this.Element.GenerateSingleLineFromBound();
                this.Element.InitElement();                
                this.CurrentSurface.Invalidate();
            }
            protected override void BeginCreateElement(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                base.BeginCreateElement(e);
            }
            protected override void OnSnippetBeginEdit(MouseEventArgs e)
            {
                if (this.m_coreCaret != null)
                {
                    this.m_coreCaret.Hide();
                }
            }
            protected override void UpdateSnippetElement(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                Rectanglef v_rc = this.Element.m_bound;
                this.Snippet.Location = e.Location;
                switch (this.Snippet.Index)
                {
                    case 3:
                        if (e.FactorPoint.X < v_rc.Right)
                        {
                            v_rc.Width = Math.Abs(v_rc.Right - e.FactorPoint.X);
                            v_rc.X = e.FactorPoint.X;
                        }
                        break;
                    case 0:
                        if (e.FactorPoint.Y < v_rc.Bottom )
                        {
                            v_rc.Height  = Math.Abs(v_rc.Bottom - e.FactorPoint.Y);                          
                            v_rc.Y = e.FactorPoint.Y;
                        }
                        break;
                    case 2:
                        if (e.FactorPoint.Y > v_rc.Top )
                        {
                            v_rc.Height = Math.Abs(e.FactorPoint.Y - v_rc.Top);
                        }
                        break;
                    case 1:
                        if (e.FactorPoint.X > v_rc.Left)
                        {
                            v_rc.Width = Math.Abs( e.FactorPoint.X - v_rc.Left  );                        
                            //v_rc.X = e.FactorPoint.X;
                        }
                        break;
                }
                this.Element.m_bound = v_rc;
                this.Element.GenerateSingleLineFromBound ();//.m_font.FontSize = v_rc.Height ;//.GenerateTextBound();
                this.Element.InitElement();
                this.CurrentSurface.Invalidate();
            }
            protected override void GenerateSnippets()
            {
                base.GenerateSnippets();
                AddSnippet(this.CurrentSurface.CreateSnippet(this, 0, 0));
                AddSnippet(this.CurrentSurface.CreateSnippet(this, 1, 1));
                AddSnippet(this.CurrentSurface.CreateSnippet(this, 2, 2));
                AddSnippet(this.CurrentSurface.CreateSnippet(this, 3, 3));
                AddSnippet(this.CurrentSurface.CreateSnippet(this, 4, 4));
            }
            protected override void InitSnippetsLocation()
            {
                if (this.RegSnippets.Count >= 4)
                {
                    Vector2f[] t = CoreMathOperation.GetResizePoints(this.Element.m_bound);
                    this.RegSnippets[0].Location = CurrentSurface.GetScreenLocation(t[1]);
                    this.RegSnippets[1].Location = CurrentSurface.GetScreenLocation(t[3]);
                    this.RegSnippets[2].Location = CurrentSurface.GetScreenLocation(t[5]);
                    this.RegSnippets[3].Location = CurrentSurface.GetScreenLocation(t[7]);
                }
            }
            protected override void OnPaint(PaintEventArgs e)
            {
                if (this.Element != null)
                {
                    e.Graphics.DrawRectangle(Pens.Yellow,
                        Rectanglef.Round(this.CurrentSurface.GetScreenBound(this.Element.m_singleLineBound)));
                }
            }
            protected override void OnMouseClick(MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if ((this.State == ST_CONFIGURING) && (this.Element != null))
                    {
                        this.SetCursorAt(CurrentSurface.GetFactorLocation(e.Location));
                        return;
                    }
                }
                base.OnMouseClick(e);
            }
            protected override void InitNewCreateElement(ICore2DDrawingElement element)
            {
                var v = CoreSystem.Instance.Settings["Core.FontProperties"];//CoreToolFontSetting
                if (v != null)
                {
                    (element as TextElement).m_font.FontName = v["DefaultFontName"].Value  as string ;
                }
                base.InitNewCreateElement(element);
            }
            public void SetCursorAt(Vector2f factorPoint)
            {
                try
                {
                    CoreFont v_ft = this.Element.m_font;
                    int v_linePos = this.m_line - 1;
                    string v_txtb = this.Element.Lines[v_linePos];
                    RectangleF v_rc = this.Element.m_bound;
                    //Vector2f v_pos = v_rc.Location;
                    //float p = (v_ft.GetLineSpacing() + v_ft.GetCellDescent());
                    //float em = v_ft.GetEmHeight();
                    float lineHeight = v_ft.GetLineHeight();
                    StringFormat v_sf = new StringFormat();
                    v_sf.FormatFlags = TextElement.StringFormatFlag;
                    v_sf.LineAlignment = v_ft.VerticalAlignment;
                    v_sf.Alignment = v_ft.HorizontalAlignment;
                    Graphics g = Graphics.FromHwnd(IntPtr.Zero);
                    SizeF w = g.MeasureString("_", this.Element.Font.GetFont(),
                       new SizeF(this.Element.Bound.Width, this.Element.Bound.Height),
                       v_sf);
                    int posy = (int)Math.Floor((factorPoint.Y - v_rc.Top) / lineHeight);
                    int posx = (int)Math.Floor((factorPoint.X - v_rc.X) / w.Width);
                    if ((posy >= 0) && (posy < this.Element.LineCount))
                    {
                        this.PosLine = posy + 1;
                        v_txtb = this.Element.Lines[posy];
                        if (posx < 0)
                            this.PosX = 0;
                        else if ((posx > v_txtb.Length))
                            this.PosX = v_txtb.Length;
                        else
                        {
                            CharacterRange[] t = new CharacterRange[v_txtb.Length];
                            for (int i = 0; i < t.Length; i++)
                            {
                                t[i] = new CharacterRange(i, 1);
                            }
                            v_sf.SetMeasurableCharacterRanges(t);
                            Region[] rg = g.MeasureCharacterRanges(v_txtb,
                                v_ft.GetFont(), new RectangleF(0, 0, short.MaxValue, short.MaxValue), v_sf);
                            float x = factorPoint.X - v_rc.X;
                            float y = w.Height / 2.0f;
                            bool v_cv = false;
                            for (int i = 0; i < rg.Length; i++)
                            {
                                if (rg[i].IsVisible(new PointF(x, y)))
                                {
                                    this.PosX = i;
                                    v_cv = true;
                                    break;
                                }
                            }
                            if (!v_cv)
                                this.PosX = posx;
                        }
                    }
                    v_sf.Dispose();
                    g.Dispose();
                    this.UpdateCaretPos();
                }
                catch { 
                }
            }
            protected override void GenerateActions()
            {
                base.GenerateActions();
                AddAction(Keys.Control | Keys.V, new PasteTextAction());
                this.Actions [Keys.Left] = new  MoveCaretLeftAction();
                this.Actions [Keys.Right] =  new MoveCaretRightAction();
                this.Actions [Keys.Up] = new MoveCaretUpAction();
                this.Actions [Keys.Down] =  new MoveCaretDownAction();
                AddAction(Keys.Back, new BackKeyAction());
                AddAction(Keys.End , new EndKeyAction());
                AddAction(Keys.Home , new HomeKeyAction());
                this.Actions [Keys.Delete ]= new DeleteTextAction();
            }
            internal void SetText(string text)
            {
                this.Element.m_Text = text;
            }
            public TextBuffer TextBuffer {
                get {
                    return m_textBuffer;
                }
            }
        }
        public class TextBuffer
        {
            TextElement.Mecanism m_mecanism;
            List<string> m_text;
            StringBuilder m_stringBuilder; //global string builder
            StringBuilder m_lstringBuilder; //line string builder
            public TextElement.Mecanism Mecanism
            {
                get {
                    return this.m_mecanism;
                }
            }
            public TextBuffer(TextElement .Mecanism mecanism)
            {
                this.m_mecanism = mecanism;
                this.m_text = new List<string>();
                this.m_stringBuilder = new StringBuilder();
            }
            public void Populate()
            {
                this.Clear();
                this.m_text.AddRange(this.m_mecanism.Element.Lines);
            }
            public void Clear()
            {
                this.m_text.Clear();
            }
            public void Flush()
            {                
                m_stringBuilder.Length = 0;
                m_stringBuilder.Append(string.Join(LINE_SEPARATOR.ToString(), m_text.ToArray()));
                this.m_mecanism.Element.Content = m_stringBuilder.ToString();
            }
            internal void AppendNewLine()
            {
                this.m_text[this.Mecanism.PosLine - 1] = this.m_text[this.Mecanism.PosLine - 1].Insert(this.m_mecanism.PosX, LINE_SEPARATOR.ToString());
                this.m_mecanism.PosX++;
                this.Flush();
            }
            internal void Append(char p)
            {
                m_lstringBuilder = new StringBuilder(this.m_text[this.Mecanism.PosLine - 1]);
                m_lstringBuilder.Insert(m_mecanism.PosX, p);
                this.m_text[this.Mecanism.PosLine - 1] = m_lstringBuilder.ToString();
                this.m_mecanism.PosX ++;
                this.Flush();
            }
            public void Remove(int xpos, int length)
            {
                m_lstringBuilder = new StringBuilder (this.m_text[this.Mecanism .PosLine -1]);
                m_lstringBuilder.Remove (xpos , length );
                this.m_text[this.Mecanism.PosLine - 1] = m_lstringBuilder.ToString();
                this.Flush();
            }
            internal void RemoveCurrentLine()
            {
                if (this.m_mecanism.PosLine > 1)
                {
                    int index = this.m_mecanism.PosLine - 1;
                    this.m_text.RemoveAt(index );
                    this.m_mecanism.PosLine = index;
                    this.m_mecanism.PosX = this.m_text[index-1].Length - 1;
                    this.Flush();
                }
            }
        }
    }
}

