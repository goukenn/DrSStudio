using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.GS.WinUI
{
    public class GSComboBoxDataRowViewer
    {
        private GSComboBoxObjectViewerListener listener;
        public IGSDataRow Row { get; set; }
        public GSComboBoxDataRowViewer(IGSDataRow row, GSComboBoxObjectViewerListener listener)
        {
            this.Row = row;
            this.listener = listener;
        }
        public override string ToString()
        {
            if (this.listener != null)
                return this.listener();
            return base.ToString();
        }
    }
}
