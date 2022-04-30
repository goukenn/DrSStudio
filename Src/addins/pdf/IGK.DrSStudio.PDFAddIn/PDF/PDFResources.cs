using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.PDF
{
    public class PDFResources : PDFDocumentObject 
    {
        private List<PDFNameObject> m_regobj;
        private PDFDictionary m_resObject;

        public PDFResources(PDFDocument document):base(document)
        {
            m_regobj = new List<PDFNameObject>();
            this.m_resObject = new PDFDictionary();
            this.Register(PDFNames.PDF);
            this.Dictionary.Add(PDFNames.ProcSet, new PDFResourceProcSet(this) );
            this.Dictionary.Add(PDFNames.XObject, this.m_resObject);
        }
        public override string ToString()
        {
            return base.ToString();
        }

        internal void Register(PDFNameObject pDFNameObject, PDFNameObject name=null, PDFObject obj=null)
        {
            if (m_regobj.Contains(pDFNameObject))
                return;
            m_regobj.Add(pDFNameObject);
            if ((name !=null) && (obj !=null))
                this.m_resObject.Add(name, obj);
        }
        public override string Render()
        {
            return base.Render();
        }

        class PDFResourceProcSet : PDFItemBase
        {
            private PDFResources pDFResources;

            public PDFResourceProcSet(PDFResources pDFResources)
            {
                this.pDFResources = pDFResources;
            }
            public override string Render()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("[");
                int i = 0;
                foreach (var item in this.pDFResources.m_regobj )
                {
                    if (i == 1)
                        sb.Append(" ");
                    sb.Append(item);
                    i = 1;
                }
                sb.Append("]");
                return sb.ToString();
            }
        }

        
    }
}
