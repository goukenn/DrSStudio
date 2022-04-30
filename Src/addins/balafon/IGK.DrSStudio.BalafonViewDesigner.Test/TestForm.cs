using IGK.DrSStudio.BalafonDesigner;
using IGK.DRSStudio.BalafonDesigner.WinUI;
using IGK.ICore.WinCore.WinUI.Controls;
using IGK.Net;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace IGK.DrSStudio.BalafonViewDesigner.Test
{
    public partial class TestForm : Form,
        IBindToolHost,
        IWebBrowserHostStreamResolver
    {
        private Control c_surface;
        PhpServer server;
        private string m_cookie;
        private string m_endpoint;

        private string m_Url;

        [Browsable(true)]
        public string Url
        {
            get { return m_Url; }
            set
            {
                if (m_Url != value)
                {
                    m_Url = value;
                }
            }
        }

        public TestForm()
        {
            m_endpoint = "0"; //initialia end point
            InitializeComponent();
        }

        public int Port => server.Port;
        public string Cookie => m_cookie;

        private void TestForm_Load(object sender, EventArgs e)
        {

            Environment.SetEnvironmentVariable("IGKAPP", "TEST");
            Environment.SetEnvironmentVariable("IGK_IS_WEBAPP", "1");
            Environment.SetEnvironmentVariable("IGKAPP_VERSION", "1.0");
            server = new PhpServer() {
                TargetSDKFolder = @"D:\wamp3.1\bin\php\php7.1.9",
                WebContext = true,
                DocumentRoot = @"d:/wamp/www/igkdev/" ,
                Port=5858

            };

            server.SetRespondListener(new BalafonServerResponse(server, this));
            server.StartServer();



            c_surface = BalafonExposer.CreateSurface();

            c_surface.Dock = DockStyle.Fill;

            if (c_surface is IBalafonViewDesignerSurface _surface) {

                _surface.SetUriStreamResolver(this);
                _surface.BindTool = this;
                _surface.Navigate("/bdoc");
               // _surface.InvokeScript("igk.initWebApp({appName:'demo})"); 
                    
            }
            this.Controls.Add(c_surface);
            this.Controls.SetChildIndex( c_surface, 0);
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (c_surface is IBalafonViewDesignerSurface _surface)
            {
                _surface.Reload();
              }

        }

        public Stream Resolve(Uri uri)
        {
            WebClient client = new WebClient();

            string cookie = EndPointCookie(this.m_endpoint);



            //client.Headers.Add("Access-Control-Allow-Origin", "*");
            //client.Headers.Add("Access-Control-Allow-Methods", "POST, GET, PUT, DELETE, PATCH, OPTIONS");

            if (cookie != null)
            client.Headers.Add(HttpRequestHeader.Cookie, cookie);
            var o = client.OpenRead(uri);     

            var g = client.ResponseHeaders.Get("Set-Cookie");
            if (g != null) {
                this.m_cookie = g;
                string from = client.ResponseHeaders.Get("EndPointPort");

                
            }  
            return o;
        }

        private string EndPointCookie(string m_endpoint)
        {
            return this.m_cookie;
        }
    }
}
