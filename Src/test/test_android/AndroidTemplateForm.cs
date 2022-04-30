using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_android
{
    public partial class AndroidTemplateForm : Form
    {
        public AndroidTemplateForm()
        {
            InitializeComponent();
        }

        public void RunSurface(ICoreWorkingSurface surface)
        {
            var ctrl = surface as Control;
            ctrl.Dock = DockStyle.Fill;
            this.Controls.Add(ctrl);
        }
    }
}
