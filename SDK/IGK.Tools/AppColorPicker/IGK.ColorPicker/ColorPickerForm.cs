using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.ColorPicker
{
    using IGK.ICore.WinCore;

    public partial class ColorPickerForm : Form
    {
        public ColorPickerForm()
        {
            InitializeComponent();
            this.igkxColorPickerControl1.ColorChanged += igkxColorPickerControl1_ColorChanged;
            this.button1.Click += button1_Click;
            this._UpdateColor();
        }

        private void _UpdateColor()
        {
            this.label1.Text = this.igkxColorPickerControl1.Color.ToString(false);
            this.label2.BackColor =
                       this.igkxColorPickerControl1.Color.ToGdiColor();
        }

        void button1_Click(object sender, EventArgs e)
        {
         
            Clipboard.SetText (
                this.igkxColorPickerControl1.Color.ToString(true));
        }

        void igkxColorPickerControl1_ColorChanged(object sender, EventArgs e)
        {
            _UpdateColor();
        }

        private void ColorPickerForm_Load(object sender, EventArgs e)
        {
            _UpdateColor();
        }
    }
}
