using IGK.ICore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.PDF
{
    public class PDFWriter
    {
        private PDFDocument m_pdfDocument;
        private enuPDFPageUnit m_PdfUnit;
        private PDFWriter()
        {
            this.m_PdfUnit = enuPDFPageUnit.mm;
        }
        public enuPDFPageUnit PdfUnit
        {
            get { return m_PdfUnit; }
            set
            {
                if (m_PdfUnit != value)
                {
                    m_PdfUnit = value;
                }
            }
        }
        /// <summary>
        /// get the current pdf document
        /// </summary>
        public PDFDocument Document { get { return this.m_pdfDocument; } }

        public static PDFWriter Create(PDFUnit width, PDFUnit height)      
        {
            PDFWriter pdf = new PDFWriter();
            pdf.m_pdfDocument = new PDFDocument(pdf);
            pdf.m_pdfDocument.Pages.MediaBox = new PDFRectangle(0, 0, width.GetPoint (),
                height.GetPoint());
            return pdf;
        }
        public static PDFWriter Create(int width, int height)//unit in pixel
        {
            PDFWriter pdf = new PDFWriter();
            pdf.m_pdfDocument = new PDFDocument();          
            return pdf;
        }
        public void Save(string filename) {
            File.WriteAllText(filename, m_pdfDocument.Render(), Encoding.Default);
        }
    }
}
