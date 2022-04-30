using IGK.Net;
using System.IO;

namespace IGK.DrSStudio.BalafonViewDesigner.Test
{
    internal class BalafonServerResponse : IPhpFileServerListener
    {
        private PhpServer server;
        private TestForm form;

        public BalafonServerResponse(PhpServer server, TestForm testForm)
        {
            this.server = server;
            this.form = testForm;
        }

       
        public T GetService<T>()
        {

            //var t = typeof(T);
            //if (m_services.ContainsKey(t))
            //    return (T)m_services[t];

            //if (t.Equals(typeof(AssetManager)))
            //{

            //    var b = CreateAssetManagerService();
            //    m_services[t] = b;
            //    return (T)b;
            //}
            return default(T);

        }

        // System.Drawing.Text.PrivateFontCollection pc; 

        public void SendResponse(PhpResponseBase phpAsyncResponse)
        {
            ICore.CoreLog.WriteDebug("Requestfile : "+phpAsyncResponse.RequestFile);

         

            if (phpAsyncResponse.RequestProtocol == "OPTIONS") {

                var o = phpAsyncResponse.GetParam("Origin");
                var c = phpAsyncResponse.GetReceiveRequest();
                var hh = phpAsyncResponse.GetParam("Access-Control-Request-Headers");

                phpAsyncResponse.ClearParams();

                phpAsyncResponse.SetParam("CORS", "1");
                phpAsyncResponse.SetParam("Access-Control-Allow-Origin", "*");                
                phpAsyncResponse.SetParam("Access-Control-Allow-Headers", hh);                
                phpAsyncResponse.SetParam("Access-Control-Allow-Methods", "GET,POST,OPTIONS,DELETE,PUT");
                // phpAsyncResponse.SetParam("Access-Control-Allow-Credentials", "true");
                // phpAsyncResponse.SetParam("Access-Control-Allow-Method", "GET");
                // phpAsyncResponse.SetParam("Host", "localhost:5858");

                phpAsyncResponse.StatusCode = "200";
                phpAsyncResponse.SendData("", true);
                return;
            }
            if (phpAsyncResponse.ParamsContainsKey("Origin")) {

                string source = phpAsyncResponse.GetParam("Origin");
                phpAsyncResponse.RemoveParam("Origin");
                phpAsyncResponse.StatusCode = "302 found";
                phpAsyncResponse.SetParam("Location", "http://localhost:5858");
                //phpAsyncResponse.SetParam("Referer", "http://localhost:5858");
                phpAsyncResponse.SetParam("Access-Control-Allow-Origin", "*");
                phpAsyncResponse.SetParam("CORSResponse", "true");
            }

            if (server.DocumentRoot == null)
            {
                //send error response
                phpAsyncResponse.StatusCode = "404";
                phpAsyncResponse.SendData("<h2>No Document root setup 404 Page Not Found </h2>", true);
                return;
            }
            if (!string.IsNullOrEmpty(this.form.Cookie)){
                phpAsyncResponse.SetParam("Cookie", this.form.Cookie);
            }            

            string dir = !string.IsNullOrEmpty(phpAsyncResponse.RequestFile) ?
                Path.GetDirectoryName(phpAsyncResponse.RequestFile) : "";
            if (!string.IsNullOrEmpty(dir) && dir.StartsWith("assets"))
            {

                //AssetManager manager = this.GetService<AssetManager>();
                //var b = manager.GetAssets(Path.GetFileNameWithoutExtension(phpAsyncResponse.RequestFile));
                //if (b != null)
                //    phpAsyncResponse.SendData(b, true);
                //phpAsyncResponse.SendData(File.ReadAllBytes(@"D:\Pictures\dbc.jpg"), true);


                //-----------------------------------------------------------------------------------------------------
                //Font services
                //-----------------------------------------------------------------------------------------------------

                //string ext = Path.GetExtension(phpAsyncResponse.RequestFile);

                //switch (ext) {
                //    case ".ttf":
                //        phpAsyncResponse.MimeType = "application/x-font-truetype";
                //        break;
                //    case ".woff":
                //        phpAsyncResponse.MimeType = "application/font-woff";
                //        break;
                //    default:
                //        phpAsyncResponse.MimeType = "application/font-woff2";
                //        break;
                //}

                //string file = @"D:\wamp\www\igkdev\Lib\igk\Ext\ControllerModel\GoogleControllers\Styles\Fonts\" + Path.GetFileName(phpAsyncResponse.RequestFile);
                //if (pc == null)
                //    pc = new System.Drawing.Text.PrivateFontCollection();

                //int ln = pc.Families.Length;

                //pc.AddFontFile(file);
                //if (ln == pc.Families.Length)
                //{
                //    //failed to add font
                //}
                //else {

                //}
                //phpAsyncResponse.SendData(File.ReadAllBytes(@"D:\wamp\www\igkdev\Lib\igk\Ext\ControllerModel\GoogleControllers\Styles\Fonts\"+Path.GetFileName(phpAsyncResponse.RequestFile)), true);
                //  phpAsyncResponse.SendData("", true);
                return;
            }

            //phpAsyncResponse.MimeType = "text /html";
            //phpAsyncResponse.StatCode = "200";
            //phpAsyncResponse.Execute ("<h2>GET IN DATA: </h2>"
            //phpAsyncResponse.SendData("<h2>GET IN DATA: </h2>", true);
            string v_ruri = string.IsNullOrEmpty(phpAsyncResponse.RequestFile) ? "index.php" : phpAsyncResponse.RequestFile;
            string fn = Path.GetFullPath(System.IO.Path.Combine(server.DocumentRoot, v_ruri));
            if (!File.Exists(fn))
            {

                if (Directory.Exists(fn))
                {

                    phpAsyncResponse.Execute(Path.GetFullPath(System.IO.Path.Combine(fn, "index.php")));
                    return;
                }

                phpAsyncResponse.SetParam("REDIRECTION", phpAsyncResponse.RequestFile);
                phpAsyncResponse.SetParam("REQUEST_URI", phpAsyncResponse.RequestFile);
                phpAsyncResponse.SetParam("REDIRECT_URL", phpAsyncResponse.RequestFile);

                if (!string.IsNullOrEmpty(phpAsyncResponse.RequestQuery)) {
                    phpAsyncResponse.SetParam("QUERY_STRING", phpAsyncResponse.RequestQuery);
                    phpAsyncResponse.SetParam("REQUEST_URI", phpAsyncResponse.GetFullQueryRequest());
                }
                //phpAsyncResponse.Execute(Path.GetFullPath(System.IO.Path.Combine(server.DocumentRoot,
                //    "index.php")));

                phpAsyncResponse.RemoveParam("Content-Type");
                phpAsyncResponse.RemoveParam("Content-Length");
                phpAsyncResponse.SetParam("EndPointPort", phpAsyncResponse.From);

                
                phpAsyncResponse.Execute(Path.GetFullPath(System.IO.Path.Combine(server.DocumentRoot,
               "Lib/igk/igk_redirection.php")));
                return;

            }
            //phpAsyncResponse.
            string h = phpAsyncResponse.Execute(fn);// Path.GetFullPath(System.IO.Path.Combine(server.DocumentRoot, "index.html")));
            string g = phpAsyncResponse.GetHeaderResponse("Set-Cookie");
          

        }
    }
}