
using IGK.DrSStudio.Drawing2D;
using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Segments;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D
{
    /// <summary>
    /// represent a 128 code bar
    /// </summary>
    [CodeBarCategory("Code128", typeof(Mecanism), 
        ImageKey = CodeBarConstant.DE_CODEBAR)]
    public class Code128Element : 
        CodeBarElementBase, 
        ICoreCodeBarElement,
        ICodeBar,
        ICoreTextElement,
        ICore2DDrawingVisitable
    {
        private RectangleElement m_background;
        private bool m_ShowBorder;

        [CoreXMLAttribute()]
        [CoreConfigurableProperty()]
        [CoreXMLDefaultAttributeValue(false)]
        public bool ShowBorder
        {
            get { return m_ShowBorder; }
            set
            {
                if (m_ShowBorder != value)
                {
                    m_ShowBorder = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        public override void Dispose()
        {
            if (m_background != null) { 
            }
            base.Dispose();
        }
        private float m_Padding;
        
        private enuCode128Model m_Model;

        [CoreXMLAttribute()]
        [CoreConfigurableProperty()]
        [CoreXMLDefaultAttributeValue(enuCode128Model.Auto )]
        public enuCode128Model Model
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
        [CoreXMLAttribute()]
        [CoreConfigurableProperty()]
        /// <summary>
        /// get or set the padding
        /// </summary>
        public float Padding
        {
            get { return m_Padding; }
            set
            {
                if (m_Padding != value)
                {
                    m_Padding = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
      

        public Code128Element():base()
        {
            
        }
        protected override void InitializeElement()
        {
            this.m_background = new RectangleElement();
            base.InitializeElement();
            this.Text = "123456";
            this.m_ShowBorder = false;
            this.m_background.FillBrush.SetSolidColor(Colorf.Transparent);
            this.m_background.StrokeBrush.SetSolidColor(Colorf.Black);
        }
        public override Rectanglef GetBound()
        {
            Rectanglef v_rc = this.Bounds;
            float d = Math.Abs(this.Padding);
           // v_rc.Inflate(-d, -d);
            var m = this.GetMatrix();
            if (!m.IsIdentity)
                v_rc = CoreMathOperation.GetBounds(m.TransformPoints(CoreMathOperation.GetResizePoints(v_rc)));
            return v_rc;
        }
        
        protected override void InitGraphicPath(CoreGraphicsPath p)
        {
            p.Reset();
            Rectanglef v_rc = this.Bounds;
            this.m_background.Bounds = v_rc;
            float d = Math.Abs (this.Padding);
            v_rc.Inflate(-d, -d);
            if (string.IsNullOrEmpty(this.Text) || v_rc.IsEmptyOrSizeNegative)
            {
                p.AddRectangle(this.Bounds);
                return;
            }

            string c = string.Empty;
                switch (this.Model)
                {
                    case enuCode128Model.ModelA:                    
                       c =  Code128Lib.EncodeCode128A(this.Text);
                        break;
                    case enuCode128Model.ModelC:
                        c = Code128Lib.EncodeCode128C(this.Text);
                        break;
                    case enuCode128Model.Auto :
                         c = Code128Lib.EncodeCode128Auto(this.Text);
                        break;
                    case enuCode128Model.ModelB:
                    default :                        
                        c  = Code128Lib.EncodeCode128B(this.Text);
                        break;
                }
            c = c.Replace('b', '1').Replace('w', '0');
            int ln = c.Length;
            float v_barWidth = v_rc.Width / (float)c.Length;

            float v_lineH = ShowValue ? this.Font.GetLineHeight() : 0;

            AddInsert(p, c, v_barWidth, 0, v_rc.X, v_rc.Y , v_rc.Height - v_lineH );

            if (this.ShowValue )
            {
                p.AddText (this.Text ,                     
             new  Rectanglef(v_rc.X, v_rc .Bottom-v_lineH , v_rc.Width , v_lineH ),
             this.Font);
            }
        }
        
        private void AddInsert(CoreGraphicsPath path, string pattern, float v_segmentSize,
         int startIndex, float xoffset,
         float yoffset,
         float height)
        {
            for (int i = 0; i < pattern.Length; i++)
            {
                if (pattern[i] == '0')
                    continue;
                path.AddRectangle(
                    new Rectanglef(
                        xoffset + (startIndex + i) * v_segmentSize,
                        yoffset,
                        v_segmentSize,
                       height));
            }
        }
     
        
        public new class Mecanism : RectangleElement.Mecanism 
        {
        }



        public bool Accept(ICore2DDrawingVisitor visitor)
        {
            return visitor != null;
        }

        public void Visit(ICore2DDrawingVisitor visitor)
        {
            if ((this.m_background != null) && (this.ShowBorder))
            {
                Matrix m = this.GetMatrix();
                var obj = visitor.Save();
                visitor.MultiplyTransform(m, enuMatrixOrder.Prepend );
                visitor.Visit(this.m_background);
                visitor.Restore(obj);
            }
            visitor.Visit(this, typeof(RectangleElement));
        }
    }
}
