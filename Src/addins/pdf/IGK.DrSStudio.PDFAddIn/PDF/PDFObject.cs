using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.PDF
{
    public class PDFObject : PDFItemBase 
    {
        private int m_NumberId;
        private int m_BuildId;
        private List<PDFItemBase> m_datas;
        private PDFDocument pDFDocument;
        internal PDFObject()
        {
            m_datas = new List<PDFItemBase>();
        }

        public PDFDocument Document {
            get {
                return this.pDFDocument;
            }
        }
        public PDFObject CreateObject()
        {
            return this.pDFDocument.CreateObject();
        }
        public T CreateObject<T> (string name) where T : PDFObject
        {
            return this.pDFDocument.CreateObject<T>(name);
        }
        public PDFObject(PDFDocument pDFDocument):this()
        {
            this.pDFDocument = pDFDocument;
        }
        public int BuildId
        {
            get { return m_BuildId; }
            set
            {
                if (m_BuildId != value)
                {
                    m_BuildId = value;
                }
            }
        }
        public int NumberId
        {
            get { return m_NumberId; }
            set
            {
                if (m_NumberId != value)
                {
                    m_NumberId = value;
                }
            }
        }
        public override string Render()
        {
            StringBuilder sb = new StringBuilder ();
            sb.AppendLine (string.Format ("{0} {1} obj", this.NumberId, this.BuildId ));
            foreach (var item in this.m_datas)
            {
                sb.Append(item.Render());
            }
            sb.AppendLine ("endobj");
            return sb.ToString();
        }


        internal string GetReferenceString()
        {
            return string.Format("{0} {1} R", this.NumberId, this.BuildId);
        }

        internal virtual void Add(PDFItemBase  item)
        {
            if (item == null)
                return;
            if (this.m_datas.Contains (item) == false )
                this.m_datas.Add(item);
        }
    }
}
