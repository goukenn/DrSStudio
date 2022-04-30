

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebHtmlText.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
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
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:WebHtmlText.cs
*/
using IGK.ICore.GraphicModels;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.WebAddIn.WorkingObject
{
    [WebElementAttribute("WebHtmlText", typeof(Mecanism))]
    class WebHtmlText : WebHtmlElementBase,
        ICoreTextElement ,
        ICore2DDrawingVisitable 
        
    {
        private string m_Text;
        private CoreFont m_Font;
        private WebTextSegmentCollection m_TextSegments;
        private enuTextRenderingMode  m_RenderingMode;
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue (enuTextRenderingMode.AntiAliazed )]
        /// <summary>
        /// get or set the text rendering mode
        /// </summary>
        public enuTextRenderingMode  TextRenderingMode
        {
            get { return m_RenderingMode; }
            set
            {
                if (m_RenderingMode != value)
                {
                    m_RenderingMode = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        static readonly StringFormatFlags StringFormatFlag =           
               StringFormatFlags.FitBlackBox
              | StringFormatFlags.MeasureTrailingSpaces
              | StringFormatFlags.NoFontFallback
              | StringFormatFlags.DisplayFormatControl
              | StringFormatFlags.NoClip;
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            parameters  = base.GetParameters(parameters);
            var group = parameters.AddGroup("Info");
            group.AddItem(GetType().GetProperty("Text"));
            group.AddItem(GetType().GetProperty("TextRenderingMode"));
            return parameters;
        }
        public override enuBrushSupport BrushSupport
        {
            get
            {
                return enuBrushSupport.Fill | enuBrushSupport.Solid;
            }
        }
        public WebHtmlText()
        {
            this.m_Font = CoreFont.CreateFrom ("Courier new", this);
            this.m_Text = "Sample Text";
            this.m_TextSegments = new WebTextSegmentCollection();
            this.m_RenderingMode = enuTextRenderingMode.AntiAliazed;
            this.m_Font .FontDefinitionChanged += m_Font_FontDefinitionChanged;
          }
        void m_Font_FontDefinitionChanged(object sender, EventArgs e)
        {
            InitElement();
            OnPropertyChanged(new Core2DDrawingElementPropertyChangeEventArgs(enu2DPropertyChangedType.FontChanged));
        }
      [CoreXMLAttribute()]
        public string Text
        {
            get { return m_Text; }
            set
            {
                if (m_Text != value)
                {
                    m_Text = value;
                    LoadSegment();
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        private void LoadSegment()
        {
        }
        public void LoadString(string px)
        { 
        }
        //public override void RenderShadow(Graphics g)
        //{
        //    if (!this.AllowShadow) return;
        //    GraphicsState v_state = g.Save ();
        //    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        //    g.TranslateTransform (this.ShadowProperty.Offset.X ,
        //        this.ShadowProperty.Offset.Y , enuMatrixOrder.Append );
        //    g.MultiplyTransform(this.GetMatrix(), enuMatrixOrder.Prepend);
        //    StringFormat v_frm = this.GetNewStringFormat();
        //    g.DrawString(this.m_Text, this.Font.GetFont(),
        //        this.ShadowProperty.Brush.GetBrush() ,
        //        this.Bound, v_frm );
        //    v_frm.Dispose();
        //    g.Restore (v_state);
        //}
        //protected override void SetGraphicsProperty(Graphics g)
        //{
        //    base.SetGraphicsProperty(g);
        //    g.TextRenderingHint = (System.Drawing.Text.TextRenderingHint)this.TextRenderingMode;// System.Drawing.Text.TextRenderingHint.AntiAlias;
        //}
      

        

        /// <summary>
        /// return new string format
        /// </summary>
        /// <returns></returns>
        protected StringFormat GetNewStringFormat()
        {//get a new string format
            StringFormat v_stringFormat = new StringFormat();
            v_stringFormat.Trimming = StringTrimming.None;
            v_stringFormat.FormatFlags = StringFormatFlag;
            v_stringFormat.Alignment = (StringAlignment)m_Font.HorizontalAlignment;
            v_stringFormat.LineAlignment = (StringAlignment)m_Font.VerticalAlignment;
            return v_stringFormat;
        }
        new class Mecanism : RectangleElement.Mecanism 
        { 
        }
        class WebTextSegment
        {
            public string Value{get;set;}
            public Colorf Color{get;set;}
            public Font Font{get;set;}
        }
        class WebTextSegmentCollection : IEnumerable<WebTextSegment >
        {
            private List<WebTextSegment> m_segments;
            public WebTextSegmentCollection()
            {
                this.m_segments = new List<WebTextSegment>();
            }
            public IEnumerator<WebTextSegment> GetEnumerator()
            {
                return this.m_segments.GetEnumerator();
            }
            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return this.m_segments.GetEnumerator();
            }
        }
        public CoreFont Font
        {
            get { return this.m_Font; }
        }

        public override bool Accept(ICore2DDrawingVisitor visitor)
        {
            return visitor != null;
        }

        public override void Visit(ICore2DDrawingVisitor visitor)
        {
            object v_state = visitor.Save();
            visitor.SetupGraphicsDevice(this);
            visitor.MultiplyTransform(this.GetMatrix(), enuMatrixOrder.Prepend);
            visitor.DrawString(
                this.m_Text,
                this.Font,
                this.FillBrush,
                this.Bounds);
            //foreach (WebTextSegment item in this.m_TextSegments)
            //{
            //   // g.DrawString (this.m_Text , 
            //}
            visitor.Restore(v_state);
        }

        ICoreFont ICoreTextElement.Font
        {
            get { return this.Font; }
        }
    }
}

