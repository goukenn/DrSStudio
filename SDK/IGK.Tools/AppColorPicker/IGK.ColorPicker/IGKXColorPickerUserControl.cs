using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.ColorPicker
{
    using IGK.ICore.WinCore;

    public partial class IGKXColorPickerUserControl : UserControl
    {
        public IGKXColorPickerUserControl()
        {
            InitializeComponent();
            this.Load+=_Load;
        }

        private void _Load(object sender, EventArgs e)
        {
            _UpdateColor();
        }
        private void _UpdateColor()
        {
            this.label1.Text = this.igkxColorPickerControl1.Color.ToString(false);
            this.label2.BackColor =
                       this.igkxColorPickerControl1.Color.ToGdiColor();
        }
        
    }
}
