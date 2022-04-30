using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.PDF
{
    public class PDFDictionary : PDFItemBase
    {
        Dictionary<PDFNameObject , object> datas;
        public PDFDictionary Add(PDFNameObject d, object item)
        {
            if (datas.ContainsKey(d)) {
                if (item == null)
                    datas.Remove(d);
                else
                    datas[d] = item;
            }
            else 
                datas.Add(d, item);
            return this;
        }
        public object this[PDFNameObject obj] {
            get {
                if (this.datas.ContainsKey(obj))
                    return this.datas[obj]; 
                return null;
            }
        }
        public T GetValue<T>(PDFNameObject obj) {
            var r = this[obj];

            if (r == null)
                return default(T);
            if (r is T)
            {
                return (T)r;
            }
            return default(T);
        }
        public void Remove(PDFNameObject obj)
        {
                datas.Remove(obj);
        }
        public int Count
        {
            get
            {
                return datas.Count;
            }
        }
        public override string ToString()
        {
            return base.ToString();
        }
        public PDFDictionary()
        {
            datas = new Dictionary<PDFNameObject, object>();
        }
        public override string Render()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<<");
            object c = null;
            foreach (var item in datas)
            {
                c =  PDFUtils.GetValue(item.Value);
                if (c!=null)
                sb.AppendLine(item.Key.Render()+ " "+c);
              
            }
            sb.Append(">>");
            return sb.ToString();
        }


       
    }
}
