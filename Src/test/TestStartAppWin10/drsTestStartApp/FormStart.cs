using Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace drsTestStartApp
{

    //windows web view component 

    public partial class FormStart : Form
    {
        Dictionary<string, string> headers = new Dictionary<string, string>();

      
        public FormStart()
        {


            // voici comment envoyer des headers personnalisé au serveur.
            // pour appache ces paramètres seront convertie. les (-) seront transformer en (_) et le nom de la variable  sera préfixé de 'HTTP_'

            headers.Add("IGK-APP", "DesktopApp");
            headers.Add("IGK-APPVERSION", "1.0");


            // mauvais header
            // headers.Add("IGK_BAD", "DesktopApp");
            // BON header
            // headers.Add("IGKBAD", "DesktopApp");



            InitializeComponent();
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {


            this.c_webView1.Navigate(this.c_webView1.Source, HttpMethod.Get, null, headers);
        }

        private void C_webView1_ScriptNotify(object sender, WebViewControlScriptNotifyEventArgs e)
        {
            MessageBox.Show("invoke : "+e.Value );
        }

        private void FormStart_Load(object sender, EventArgs e)
        {
            //((ISupportInitialize)c_webView1).BeginInit();
            this.c_webView1.IsScriptNotifyAllowed = true; //enable script notification
            this.c_webView1.ScriptNotify += C_webView1_ScriptNotify;
            this.c_webView1.NavigationStarting += C_webView1_NavigationStarting;
            this.c_webView1.ContentLoading += C_webView1_ContentLoading;

            // ((ISupportInitialize)c_webView1).EndInit();
            this.c_webView1.Navigate(new Uri(Constants.GURI, UriKind.Absolute),
HttpMethod.Get, null, headers);


           // this.c_webView1.
            

        }

        private void C_webView1_ContentLoading(object sender, WebViewControlContentLoadingEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Navigaton content : "+e.Uri);
        }

        private void C_webView1_NavigationStarting(object sender, WebViewControlNavigationStartingEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Navigaton start");
        }
    }
}
