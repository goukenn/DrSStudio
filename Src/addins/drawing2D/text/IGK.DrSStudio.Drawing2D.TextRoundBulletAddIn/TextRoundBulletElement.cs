
using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Drawing2D;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore;

    [Core2DDrawingTextGrouplement("TextRoundBullet", typeof (Mecanism ))]
    public class TextRoundBulletElement : RectangleElement,ICore2DDrawingVisitable, ICoreTextElement 
    {
        private TextElement c_text;
        private RoundRectangleElement c_rc;
        private float m_Padding;
        private CoreUnit m_Radius;
        private bool m_initPath;

        public static readonly enuBrushMode TEXTFILLBRUSH = (enuBrushMode)((int)enuBrushMode.Fill | 0x10);
        public static readonly enuBrushMode TEXTSTROKEBRUSH = (enuBrushMode)((int)enuBrushMode.Stroke  | 0x10);
        public override void Dispose()
        {
            if (this.c_text != null)
            {
                this.c_text.Dispose();
                this.c_text = null;
            }
            if (this.c_rc != null)
            {
                this.c_rc.Dispose();
                this.c_rc = null;
            }
            base.Dispose();
        }
        [CoreXMLAttribute]
        public ICoreFont Font {
            get {
                return this.c_text.Font;
            }
        }
        [CoreXMLAttribute]
        [CoreXMLDefaultAttributeValue(CoreConstant.DEFAULT_STROKEBRUSH)]
        public ICorePen BackgroundStrokeBrush {
            get {
                return this.c_rc.StrokeBrush;
            }
        }
        [CoreXMLAttribute]
        [CoreXMLDefaultAttributeValue(CoreConstant.DEFAULT_FILLBRUSH)]
        public ICoreBrush BackgroundFillBrush
        {
            get
            {
                return this.c_rc.FillBrush ;
            }
        }


        [CoreXMLAttribute]
        [CoreXMLDefaultAttributeValue(CoreConstant.DEFAULT_STROKEBRUSH)]
        public ICorePen TextStrokeBrush
        {
            get
            {
                return this.c_text.StrokeBrush;
            }
        }
        [CoreXMLAttribute]
        [CoreXMLDefaultAttributeValue(CoreConstant.DEFAULT_FILLBRUSH)]
        public ICoreBrush TextFillBrush
        {
            get
            {
                return this.c_text.FillBrush ;
            }
        }
     

        
        [CoreConfigurableProperty()]
        [CoreXMLAttribute]
        [CoreXMLDefaultAttributeValue("5mm")]
        public CoreUnit Radius
        {
            get { return m_Radius; }
            set
            {
                if (m_Radius != value)
                {
                    m_Radius = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreConfigurableProperty()]
        [CoreXMLAttribute]
        [CoreXMLDefaultAttributeUnitValueAttribute("2mm")]
        public float Padding
        {
            get { return m_Padding; }
            set
            {
                
                if ((m_Padding != value)&&(value >=0))
                {
                    m_Padding = value;
                    OnPropertyChanged (CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
     
        
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            var r =  base.GetParameters(parameters);
            var g = r.AddGroup("text");
            g.AddItem("Text", "lb.Text", enuParameterType.MultiTextLine, (o, e) =>
            {
                this.Text = ((string)e.Value) ?? string.Empty;
            });
            return r;
        }

        public override ICoreBrush GetBrush(enuBrushMode mode)
        {
            if (((int)mode & 0x10) == 0x10)
            {
                mode = (enuBrushMode)((int)mode & ~0x10);
                return this.c_text.GetBrush(mode);
            }
            return this.c_rc.GetBrush(mode);
        }
        protected override void InitializeElement()
        {
            base.InitializeElement();
            this.m_Radius = "5mm";
            this.m_Padding = "2mm".ToPixel ();

            this.c_text = new TextElement();
            this.c_text.StrokeBrush.SetSolidColor(Colorf.Transparent);
            this.c_text.FillBrush.SetSolidColor(Colorf.White);

            this.c_rc = new RoundRectangleElement();
            this.c_rc.FillBrush.SetSolidColor(Colorf.DarkGray);
            this.c_rc.StrokeBrush.SetSolidColor(Colorf.FromFloat(0.5f, 0.5f));
            this.c_rc.PropertyChanged += _recChanged;
            this.c_text.PropertyChanged += _textPropChanged;
        }

        private void _textPropChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
        {
            if (!m_initPath )
            OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
        }

        private void _recChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
        {
            if (!m_initPath)
            OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
        }
        public override Rectanglef GetBound()
        {
            //this.c_rc.GetBound();//.Bounds
            return  base.GetBound();
        }
        protected override void InitGraphicPath(CoreGraphicsPath p)
        {
            if (m_initPath)
                return;
            m_initPath = true;
            p.Reset();           
            
            Rectanglef v_rc = this.Bounds;
            var v_textrc = this.c_text.GetPath().GetBounds(); //reference text 
            v_rc.Inflate(Padding, Padding);
            v_textrc.Inflate(Padding, Padding);

            float v_radius = this.Radius.GetPixel();
            v_textrc.Inflate(v_radius, v_radius);

            var v_rc2 = new Rectanglef(v_rc.X, v_rc.Y, Math.Max(v_radius * 2, v_rc.Width), Math.Max(v_radius * 2, v_rc.Height));
            v_rc2.Width = Math.Max(v_textrc.Width, v_rc2.Width);
            v_rc2.Height = Math.Max(v_textrc.Height, v_rc2.Height);

            c_text.SuspendLayout();
            c_text.Bounds = v_rc2;
            c_text.Font.HorizontalAlignment = enuStringAlignment.Center;
            c_text.Font.VerticalAlignment = enuStringAlignment.Center;
            c_text.ResumeLayout();



            if (v_rc.IsEmptyOrSizeNegative)
            {
                Rectanglef v_p = new Rectanglef(v_rc.Location, Size2f.Empty);
                v_p.Inflate(Padding, Padding);
                p.AddRectangle(v_p);
                this.m_initPath = false;
                return;
            }

            c_rc.SuspendLayout();
            c_rc.Bounds = v_rc2;
            c_rc.RoundStyle = enuRoundStyle.Standard;
            c_rc.SetAllRadius(this.Radius, this.Radius);//"5mm", "5mm"); 

            c_rc.ResumeLayout();
            p.Add(c_rc.GetPath());

            //c_text.SuspendLayout();
            //c_text.Bounds = v_rc;
            //c_text.Font.HorizontalAlignment = enuStringAlignment.Center;
            //c_text.Font.VerticalAlignment = enuStringAlignment.Center;            
            //c_text.ResumeLayout();


            //var v_textrc = this.c_text.GetPath().GetBounds();

            //if (v_rc.IsEmptyOrSizeNegative)
            //{
            //    Rectanglef v_p = new Rectanglef(v_rc.Location , Size2f.Empty );
            //    v_p.Inflate (Padding,Padding );
            //    p.AddRectangle (v_p);
            //    this.m_initPath = false;
            //    return;
            //}

             
            //v_rc.Inflate(Padding, Padding);
            //float t = this.Radius.GetPixel();
            //var v_rc2 = new Rectanglef(v_rc.X, v_rc.Y , Math.Max (t* 2, v_rc.Width), Math.Max ( t * 2, v_rc.Height));
            //v_rc2.Width = Math.Max(v_textrc.Width, v_rc2.Width);
            //v_rc2.Height = Math.Max(v_textrc.Height, v_rc2.Height);

            //c_rc.SuspendLayout();
            //c_rc.Bounds = v_rc2 ;
            //c_rc.RoundStyle = enuRoundStyle.Standard;
            //c_rc.SetAllRadius(this.Radius, this.Radius);//"5mm", "5mm"); 

            //c_rc.ResumeLayout();
            //p.Add(c_rc.GetPath());
            m_initPath = false;
        }
        public bool Accept(ICore2DDrawingVisitor visitor)
        {
            return visitor != null;
        }

        public void Visit(ICore2DDrawingVisitor visitor)
        {
            if (!this.Accept (visitor))
            {
                return ;
            }

            Rectanglef v_rc = this.Bounds;
            if (v_rc.IsEmptyOrSizeNegative )
                return;
            object v_obj = visitor.Save();
            visitor.MultiplyTransform(this.GetMatrix(), enuMatrixOrder.Prepend );
            visitor.SetupGraphicsDevice(this);
            visitor.Visit(this.c_rc);
            visitor.Visit(this.c_text);
            //v_rc = this.c_text.GetBound();
            //visitor.DrawRectangle(Colorf.Red, v_rc.X, v_rc.Y, v_rc.Width, v_rc.Height);

            //v_rc = this.c_text.GetPath().GetBounds();
            //visitor.DrawRectangle(Colorf.Green , v_rc.X, v_rc.Y, v_rc.Width, v_rc.Height);
            visitor.Restore(v_obj);
        }

    
        new class Mecanism : RectangleElement.Mecanism   
        {
                

            protected override void GenerateActions()
            {
                base.GenerateActions();
                if (this.Element != null)
                {
                    var sn = CurrentSurface.CreateSnippet(this, TextElement.Mecanism.DM_FONTSIZE, 5);
                    sn.Shape = enuSnippetShape.Diadmond;
                    this.AddSnippet(sn);
                }
            }
            protected override void GenerateSnippets()
            {
                base.GenerateSnippets();
            }
            protected override void BeginSnippetEdit(CoreMouseEventArgs e)
            {
                base.BeginSnippetEdit(e);
            }
            protected override void InitSnippetsLocation()
            {
                base.InitSnippetsLocation();
            }
            protected override void UpdateSnippetEdit(CoreMouseEventArgs e)
            {
                base.UpdateSnippetEdit(e);
            }
            protected override void EndSnippetEdit(CoreMouseEventArgs e)
            {
                base.EndSnippetEdit(e);
            }

        }

        ICoreFont ICoreTextElement.Font
        {
            get { return this.c_text.Font ; }
        }


        [CoreXMLAttribute ()]
        [CoreConfigurableProperty ()]
        [CoreXMLDefaultAttributeValue("Text")]
        public string Text
        {
            get
            {
                return this.c_text.Text;
            }
            set
            {
                this.c_text.Text = value;
            }
        }
    }
}
