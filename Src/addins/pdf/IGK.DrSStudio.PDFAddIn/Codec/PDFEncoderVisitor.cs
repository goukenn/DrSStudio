using IGK.ICore.Drawing2D;
using IGK.PDF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.PDFConverter.Codec
{
    using IGK.ICore;

    class PDFEncoderVisitor
    {
        
        public bool SaveTo(string f, ICore2DDrawingDocument[] documents)
        {
            PDFWriter pdf = null;
            //var c = PDFUtils.InitCulture();
            //PDFGraphics g = PDFGraphics.CreateGraphics(pdf.Document.CurrentPage);
            PDFPage pg = null;
            for (int i = 0; i < documents.Length; i++)
            {
                var doc = documents[i];
                if (pdf == null)
                {
                    pdf = PDFWriter.Create(doc.Width.ToMm()+"mm", doc.Height.ToMm()+"mm" );//"210mm", "297mm");
                    pg = pdf.Document.CurrentPage;
                }
                else {
                    pg = pdf.Document.AddPage(doc.Width, doc.Height );
                }

                PDFGraphics gr = PDFGraphics.CreateGraphics(pg);

                gr.Draw(doc);

                gr.Flush();

            }
            try
            {
                pdf.Save(f);
            }
            catch (Exception ex){
                System.Windows.Forms.MessageBox.Show("Impossible d'enregistrer\n"+ex.Message );
            }
             return true;
        }


    }
}
