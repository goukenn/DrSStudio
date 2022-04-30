using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.PDF
{
    public class PDFPage : PDFDocumentObject 
    {
        public PDFPageCollector Parent {
            get {
                return this.Dictionary.GetValue < PDFPageCollector>(PDFNames.Parent);
            }
            set {
                this.Dictionary.Add (PDFNames.Parent, value );
            }
        }
        public PDFPage(PDFDocument document):base(document )
        {
            this.Dictionary.Add(PDFNames.Type, PDFNames.Page);
        }

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


        public PDFItemBase Contents
        {
            get
            {
                return this.Dictionary.GetValue<PDFItemBase>(PDFNames.Contents);
            }
            set
            {
                this.Dictionary.Add(PDFNames.Contents, value);
            }
        }
    }
}
