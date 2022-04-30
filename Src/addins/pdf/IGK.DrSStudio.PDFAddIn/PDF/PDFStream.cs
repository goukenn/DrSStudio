using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.PDF
{
    public class PDFStream: PDFItemBase
    {
        private string m_Value;

        public string Value
        {
            get { return m_Value; }
            set
            {
                if (m_Value != value)
                {
                    m_Value = value;
                }
            }
        }
        public override string ToString()
        {
            return base.ToString();
        }
        public PDFStream()
        {
        }
        public override string Render()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("stream");
            sb.AppendLine(this.Value);
            sb.AppendLine("endstream");
            return sb.ToString();
        }

    }
}
