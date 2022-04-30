using IGK.GS.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TogoGS.EncodeTaskModule.WinUI
{
    public class GSComboBoxObjectViewer
    {
        private GSComboBoxObjectViewerListener m_listener;
        public object Item { get; set; }
        public GSComboBoxObjectViewer(object item, GSComboBoxObjectViewerListener i)
        {
            this.Item = item;
            this.m_listener = i;
        }
        public override string ToString()
        {
            if (this.m_listener != null)
                return this.m_listener();
            return base.ToString();
        }
    }
}
