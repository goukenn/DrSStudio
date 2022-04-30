using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.PDF
{
    public class PDFPageCollector :  PDFDocumentObject 
    {
        public PDFRectangle MediaBox
        {
            get
            {
                return this.Dictionary.GetValue<PDFRectangle>(PDFNames.MediaBox);
            }
            set
            {
                this.Dictionary.Add(PDFNames.MediaBox, value);
            }
        }
        public PDFArray Kids {
            get {
                return this.Dictionary.GetValue<PDFArray>(PDFNames.Kids);
            }
        }
        public int Count
        {
            get
            {
                return this.Dictionary.GetValue<int>(PDFNames.Count);
            }
            private set {
                 this.Dictionary.Add(PDFNames.Count, value );
            }
            
        }
      
        public PDFPageCollector(PDFDocument document):base(document )
        {
            this.Dictionary.Add(PDFNames.Type, PDFNames.Pages);
            this.Dictionary.Add(PDFNames.Kids, new PDFArray());
            this.Dictionary.Add(PDFNames.Count, 0);            
        }

        internal void Add(PDFPage p)
        {
            p.Parent = this;
            this.Kids.Add(p);
            this.Count = this.Kids.Count;
            
        }
        public override string Render()
        {
            return base.Render();
        }
    }
}
