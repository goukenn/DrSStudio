using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.PDF
{
    public class PDFImageObject : PDFDocumentObject 
    {
        private PDFStream m_stream;
        private PDFNameObject m_Name;

        public PDFNameObject Name
        {
            get { return m_Name; }            
        }
        public string Value{
            get{
                return this.m_stream.Value;
            }
            set {
                this.m_stream.Value = value;
            }
        }
        public PDFImageObject(PDFDocument document):base(document )
        {
            this.Dictionary.Add(PDFNames.Type, PDFNames.XObject);
            this.Dictionary.Add(PDFNames.Subtype, PDFNames.Image);     
            m_stream = new PDFStream();
            this.Add(m_stream);
            this.m_Name = document.CreateName("img");
            document.Resources.Register(PDFNames.ImageB, this.m_Name , this);
            
        }
        public override string Render()
        {
            return base.Render();
        }

        public int Width { get { return this.Dictionary.GetValue<int>(PDFNames.Width); } set { this.Dictionary.Add(PDFNames.Width, value); } }
        public int Height { get { return this.Dictionary.GetValue<int>(PDFNames.Height); } set { this.Dictionary.Add(PDFNames.Height, value); } }
        public int Columns { get { return this.Dictionary.GetValue<int>(PDFNames.Columns); } set { this.Dictionary.Add(PDFNames.Columns, value); } }
        public int Length { get { return this.Dictionary.GetValue<int>(PDFNames.Length); } set { this.Dictionary.Add(PDFNames.Length, value); } }
        public int Colors { get { return this.Dictionary.GetValue<int>(PDFNames.Colors); } set { this.Dictionary.Add(PDFNames.Colors, value); } }
        public int Predictor { get { return this.Dictionary.GetValue<int>(PDFNames.Predictor); } set { this.Dictionary.Add(PDFNames.Predictor, value); } }
        public int BitsPerComponent { get { return this.Dictionary.GetValue<int>(PDFNames.BitsPerComponent); } set { this.Dictionary.Add(PDFNames.BitsPerComponent, value); } }
        
        public PDFNameObject ColorSpace { get { return this.Dictionary.GetValue<PDFNameObject>(PDFNames.ColorSpace); } set { this.Dictionary.Add(PDFNames.ColorSpace, value); } }

        public PDFItemBase Filter { get { return this.Dictionary.GetValue<PDFItemBase   >(PDFNames.Filter); } set { this.Dictionary.Add(PDFNames.Filter, value); } }
    }
}
