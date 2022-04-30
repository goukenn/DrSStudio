using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.PDF
{
    public class PDFValue : PDFItemBase
    {
        private object m_Value;

        public object Value
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
        public PDFValue(object value)
        {
            this.m_Value = value;
        }
        public override string Render()
        {
            return this.Value != null ? this.Value.ToString() : null ;
        }
    }
}
