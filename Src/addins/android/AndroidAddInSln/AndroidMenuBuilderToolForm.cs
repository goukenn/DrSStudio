using IGK.DrSStudio.Android.AndroidMenuBuilder.Menu.File;
using IGK.DrSStudio.Android.AndroidMenuBuilder.WinUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AndroidAddInSln
{
    public partial class AndroidMenuBuilderToolForm : Form
    {
        private AndroidMenuBuilderPanelControl v_surface;
        public AndroidMenuBuilderToolForm()
        {
            InitializeComponent();
            this.Load += _Load;
        }

        private void _Load(object sender, EventArgs e)
        {
            v_surface = new AndroidMenuBuilderPanelControl();
            v_surface.ShowPropertiesTreeview = false;
            v_surface.Dock = DockStyle.Fill ;

            this.Controls.Add (v_surface );
            this.Controls.SetChildIndex(v_surface, 0);
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void clearMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.v_surface.ClearMenu();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    this.v_surface.Save(sfd.FileName);
                }
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewAndroidMenu ac = new NewAndroidMenu();
            ac.DoAction();
        }
    }
}
