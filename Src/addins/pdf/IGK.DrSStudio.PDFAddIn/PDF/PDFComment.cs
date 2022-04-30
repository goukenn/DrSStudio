using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.PDF
{
    class PDFComment : PDFItemBase 
    {
        private string m_Content;

        public string Content
        {
            get { return m_Content; }
            set
            {
                if (m_Content != value)
                {
                    m_Content = value;
                }
            }
        }
        public override string Render()
        {
            StringBuilder sb = new StringBuilder ();
            sb.AppendLine ("%"+this.Content);
            return sb.ToString();
        }
    }
}
