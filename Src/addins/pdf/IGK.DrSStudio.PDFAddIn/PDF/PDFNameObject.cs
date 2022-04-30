using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.PDF
{
    public class PDFNameObject : PDFItemBase
    {
        private string m_Content;

        internal  PDFNameObject(string data)
        {
            this.m_Content = data;
        }

        public string Value
        {
            get { return m_Content; }
            internal set
            {
                if (m_Content != value)
                {
                    m_Content = value;
                }
            }
        }
        
        public override string ToString()
        {
            return "/" + this.m_Content;
        }
        public override string Render()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("/" + this.Value);
            return sb.ToString();
        }
        public static implicit operator PDFNameObject (string data){
            if (string.IsNullOrEmpty (data ))
                return null;
            return new PDFNameObject (data);
        }
    }
}
