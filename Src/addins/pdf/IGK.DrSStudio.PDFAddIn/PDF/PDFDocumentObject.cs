using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.PDF
{
    /// <summary>
    /// represent the PDF Document
    /// </summary>
    public class PDFDocumentObject : PDFObject 
    {
        private PDFDictionary m_dics;
        /// <summary>
        /// get global dictionary
        /// </summary>
        protected internal PDFDictionary Dictionary {
            get {
                return this.m_dics;
            }
        }

      
        public PDFDocumentObject(PDFDocument  document):base(document )
        {
            m_dics = new PDFDictionary();
            this.Add(m_dics);
        }
        internal override void Add(PDFItemBase  dic)
        {
            base.Add(dic);
        }
    }
}
