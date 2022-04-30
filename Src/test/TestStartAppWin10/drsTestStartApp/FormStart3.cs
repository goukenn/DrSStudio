using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace drsTestStartApp
{
    public partial class FormStart3 : Form
    {
        public FormStart3()
        {
            InitializeComponent();

        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.c_webBrowser1.ObjectForScripting = new OBJScript();
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            
            this.c_webBrowser1.Refresh( WebBrowserRefreshOption.Completely ); 
        }
    }

    [ComVisible(true)]
    public class OBJScript
    {
        public void notify(object action) {
            MessageBox.Show("Test : "+action);
        }
    }
}
