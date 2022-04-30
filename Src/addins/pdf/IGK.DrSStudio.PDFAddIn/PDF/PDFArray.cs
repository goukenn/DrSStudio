using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.PDF
{
    public class PDFArray : PDFItemBase
    {
        List<object> datas;
        public PDFArray Add(object item)
        {
            if (item!=null) 
                datas.Add(item);
            return this;
        }
        public void Remove(object item) {
            if (datas.Contains(item))
                datas.Remove(item);
        }
        public int Count {
            get
            {
                return datas.Count;
            }
        }
        public override string ToString()
        {
            return base.ToString();
        }
        public PDFArray()
        {
            datas = new List<object>();
        }
        public override string Render()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            bool r = false;
            foreach (var item in datas)
            {
                if (r)
                    sb.Append(" ");
                sb.Append(PDFUtils.GetValue(item));
                r = true;
            }
            sb.Append("]");
            return sb.ToString();
        }
      
    }
}
