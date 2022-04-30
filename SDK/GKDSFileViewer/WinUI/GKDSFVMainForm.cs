using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.FileViewer.WinUI
{
    public partial class GKDSFVMainForm : Form
    {
        public GKDSFVMainForm()
        {
            InitializeComponent();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "gkds file | *.gkds";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    this.gkdsfvView1.FileName = ofd.FileName;
                }
            }
        }
    }
}
