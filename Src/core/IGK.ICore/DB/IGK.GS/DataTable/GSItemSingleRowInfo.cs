using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.GS.DataTable
{
    public class GSItemSingleRowInfo<T>
    {
        private T m_item;
        public T Item { get { return this.m_item; } }
        public override string ToString()
        {
            return m_item.ToString();
        }
        public GSItemSingleRowInfo(T item)
        {
            if (item == null)
                throw new ArgumentException("item");
            this.m_item = item;
        }
    }
}
