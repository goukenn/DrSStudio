using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.WinCore.WinUI.Controls
{
    public class IGKXComboBoxItem<T>
    {
        private T m_Item;
        private IXComboBoxDisplayListener<T> m_Listerner;

        public T Item
        {
            get { return m_Item; }
        }
        public IGKXComboBoxItem(T item)
        {
            this.m_Item = item;
            this.m_Listerner = null;
        }
        
        public IXComboBoxDisplayListener<T> Listerner
        {
            get { return m_Listerner; }
            set
            {
                if (m_Listerner != value)
                {
                    m_Listerner = value;
                }
            }
        }
        public override string ToString()
        {
            if (m_Listerner != null)
                return m_Listerner(this);
            return base.ToString();
        }
    }
}
