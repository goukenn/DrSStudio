

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PhpResponse.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.Codec;
using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using IGK.ICore;
using IGK.ICore.Imaging;
using IGK.ICore.IO;

namespace IGK.Net
{
    public class PhpResponse : PhpResponseBase, IDisposable, IPhpResponse 
    {
        private System.Net.Sockets.Socket m_socket;
       // private Dictionary<string, string> m_params;
        
        private Thread m_thread;
        private string m_receiveString;
        private EndPoint m_from;
        private string m_Protocol = "HTTP/1.1";
        private PhpServer m_server;
        private string m_requestUri;
        private string m_requestQuery;
        private object m_syncobj = new object ();
        private string m_sessionId;
        private  Dictionary<string,object> m_queryParams;
        
        public string RemoteAddress {
            get
            {
                return this.m_from.ToString().Split(':')[0];
            }
        }
        public int EndPointPort {
            get {
                return int.Parse (this.m_from.ToString().Split(':')[1]);
            }
        }
        public PhpResponse(Socket socket, PhpServer server):base(server )
        {
            this.m_sessionId = string.Empty;
            this.m_server = server;
            this.m_socket = socket;
            this.StatusCode = "200 OK";            
            this.m_from = socket.RemoteEndPoint;
            
            //Debug.WriteLine("Request from : " + this.m_from.ToString());
            byte[] data = new  byte[1024];
            int i = 0;
            StringBuilder sb = new StringBuilder();
            //Debug.WriteLine("Recieving Message ...." + socket.Available);
            if (socket.Available > 0)
            {
                while ((i = socket.Receive(data, data.Length, SocketFlags.None)) > 0)
                {
                    sb.Append(Encoding.ASCII.GetString(data, 0, i));
                    if (i < data.Length)
                        break;
                }
                     this.m_receiveString = sb.ToString();

                     if (!string.IsNullOrEmpty(this.m_receiveString))
                     {
                         //Debug.WriteLine("ReceiveMessage:" + this.m_receiveString);
                         this.m_thread = new Thread(_doJob);
                         this.LoadReceiveString();
                     }
            }
            else 
            {
                this.StatusCode = "200";
                this.SendData("", true);
               // this.m_socket.Close();
            }
        }

        private void LoadReceiveString()
        {
            //get info
            string[] t = this.m_receiveString.Split(' ');
            this.RequestProtocol = t[0];
            string[] v_info = null;

            string f = t[1];//.Substring(3, 4);
            while (f.StartsWith("/"))
            {
                f = f.Substring(1);
            }
            m_requestUri = f;
            v_info = f.Split('?');
            for (int i = 0; i < v_info.Length; i++)
            {
                if (i == 0)
                {
                    this.RequestFile = v_info[0];
                }
                else
                {
                    this.m_requestQuery += v_info[i];
                }
            }
            string[] v_tab = this.m_receiveString.Split(new char[] { '\n' });
            int v_dataIndex = this.m_receiveString.IndexOf("\r\n\r\n");
            foreach (string item in v_tab)
            {
                var h = item.Trim().Split(new char[]{':'}, StringSplitOptions.RemoveEmptyEntries );
                if (h.Length > 1)
                {
                    this.SetParam(h[0], item.Substring (item.IndexOf(':',0)+1).Trim ());
                }
            }
            //Debug.WriteLine("Request URI : " + m_requestUri);
            //Debug.WriteLine("Protocol  : " + m_requestProtocol);

            switch (RequestProtocol.ToUpper())
            { 
                case "POST":
                    string o = this.GetParam("Content-Type");
                    string olength = this.GetParam("Content-Length");
                    int len = int.Parse(olength);
                    switch (o)
                    { 
                        case "application/x-www-form-urlencoded":
                            //get the post data
                            
                            if ((v_dataIndex > 0)&&(this.m_receiveString.Length > (v_dataIndex +4)))
                            {
                                this.PostData = this.m_receiveString.Substring(v_dataIndex + 4,len);
                            }
                            
                            break;
                        default:
                            string v_entype = o.Split(';')[0];                     
                            if ((v_dataIndex > 0) && (this.m_receiveString.Length > (v_dataIndex + 4)))
                            {
                                this.PostData = this.m_receiveString.Substring(v_dataIndex+4, len);
                            }                            
                            break;
                    }
                    break;
                case "GET":
                    break;
            }

        }
        ///// <summary>
        ///// set param
        ///// </summary>
        ///// <param name="name"></param>
        ///// <param name="value"></param>
        //public  void SetParam(string name, string value)
        //{
        //    if (!string.IsNullOrEmpty(name) && !this.m_params.ContainsKey(name))
        //    {
        //        this.m_params.Add(name, value.Trim());
        //    }
        //}
        ///// <summary>
        ///// get the params
        ///// </summary>
        ///// <param name="name"></param>
        ///// <returns></returns>
        //public string getParam(string name)
        //{
        //    if (this.m_params.ContainsKey(name))
        //        return m_params[name];
        //    return string.Empty;
        //}
        /// <summary>
        /// send header message
        /// </summary>
        /// <param name="mimeType"></param>
        /// <param name="length"></param>
        /// <param name="statusCode"></param>
        protected void SendHeader(string mimeType, int length, string statusCode)
        {
            mimeType = string.IsNullOrEmpty (mimeType )? "text/html":mimeType;

            StringBuilder sb = new StringBuilder();
            sb.Append(this.m_Protocol);
            sb.Append(" "+statusCode).AppendLine();
            sb.AppendLine("Server: iGKNET-NETPhpServer");
            sb.Append("Content-Type: ");
            sb.Append(mimeType).Append ("; charset=utf-8; ").AppendLine();

            sb.AppendLine("Cache-Control:public;");

            if (this.ParamsContainsKey("Referer"))
                    sb.AppendLine("Referer:"+this.GetParam("Referer"));

            if (this.ParamsContainsKey("Connection"))
                sb.AppendLine("Connection:" +this.GetParam("Connection"));
            else
                sb.AppendLine("Connection:Keep-Alive");
            if (!string.IsNullOrEmpty(this.m_sessionId))
            {
                sb.AppendLine(string.Format("Set-Cookie: {0};", this.m_sessionId));
            }
            sb.Append("Accept-Ranges: bytes").AppendLine();
               string v_redirectLocation = this.GetParam ("RedirectLocation");
               if (v_redirectLocation != null)
               {
                   sb.AppendLine("Location: "+v_redirectLocation);
               }
            sb.Append("Content-Length: ");            
            //append 2 line to limit header information
            sb.Append(length).AppendLine().AppendLine();
            Byte[] data = Encoding.Default.GetBytes(sb.ToString());
            //send header
            try
            {
                m_socket.Send(data);
            }
            catch (Exception ex){
                Debug.WriteLine("Exception : " + ex.Message);
            }
            sb.Clear();

        }
        /// <summary>
        /// send data
        /// </summary>
        /// <param name="mem"></param>
        /// <param name="closed"></param>
        protected void SendData(MemoryStream mem, bool closed)
        {
            try
            {
                Byte[] tb_messages = new Byte[mem.Length];
                SendHeader(this.MimeType, (int)mem.Length, this.StatusCode);
                mem.Read(tb_messages, 0, tb_messages.Length);
                m_socket.Send(tb_messages);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception : " + ex.Message);
            }
            finally
            {
                if (closed)
                {
                    m_socket.Close();
                }
            }
        }
        /// <summary>
        /// send  data 
        /// </summary>
        /// <param name="message">string message</param>
        /// <param name="closed">closed or not</param>
        public override void SendData(string message, bool closed)
        {   
            try
            {
                Byte[] tb_messages = Encoding.Default.GetBytes(message);
                SendHeader(this.MimeType, tb_messages.Length, this.StatusCode);
                m_socket.Send(tb_messages);
            }
            catch(Exception ex)
            {
                CoreLog.WriteDebug ("Exception : " + ex.Message);
            }
            finally {
                if (closed)
                {
                    
                    m_socket.Close();
                    m_socket = null;
                }
            }
        }
        /// <summary>
        /// .doing job htread
        /// </summary>
        protected void _doJob()
        {
            //lock (this.m_syncobj)
            //{
                //Debug.WriteLine("PhpResponse : _doJob");
                this.MimeType = "text/html";
                string message = this.Execute(Path.GetFullPath(System.IO.Path.Combine(this.m_server.DocumentRoot, this.RequestFile)));
                if (message != null)
                {
                    this.SendData(message,true );                  
                }
                else
                {
                   
                    if ((this.m_socket !=null) &&  (this.m_socket.Connected))
                    {
                        this.StatusCode = "404";
                        this.SendData("Page Not Found : " + this.m_receiveString, true);
                    }
                }
                //Debug.WriteLine("PhpResponse : job End.");
          //  }
        }
        /// <summary>
        /// process the filename
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public override  string  Execute(string filename)
        {
            //Debug.WriteLine("ProcessFile : " + filename);
            if (!System.IO.File.Exists(filename))
            {
                if (System.IO.Directory.Exists(filename))
                {
                    //redirector to 
                    string v_hfilenmane = GetUriPath (filename);
                    if (!string.IsNullOrEmpty(this.RequestFile) && v_hfilenmane.EndsWith("/") == false)
                    {
                        this.SetParam("Redirect", "1");
                        this.SetParam("RedirectStatus", "302 Found");
                        this.SetParam("RedirectLocation", GetScriptName(filename + "/")
                            + this.GetFullQueryRequest());                    
                        this.StatusCode = "302 Found";
                        return "Must be redirected-Not Implement. Refresh the page . Please <a href='" + GetScriptName(filename + "/") + "'>Click Here</a>";
                      
                    }
                    else
                        return Execute(Path.GetFullPath (System.IO.Path.Combine(filename, "index.php")));
                    
                }
                return null;
            }
            string ext = System.IO.Path.GetExtension(filename).ToLower();
            switch (ext)
            {
                case ".gkds":
                    /*
                     * Treat GKDS FILES
                     * 
                     * */
                    
                    ICore2DDrawingDocument[] t = CoreDecoder.Instance.OpenFileDocument(filename).ConvertTo<ICore2DDrawingDocument>();
                    if ((t != null) && (t.Length == 1))
                    {
                        int w =Convert.ToInt32 ( this.GetQueryParam("w", -1));
                        int h = Convert.ToInt32(this.GetQueryParam("h", -1));
                        int dpi = Convert.ToInt32(this.GetQueryParam("dpi", 300));
                        MemoryStream mem = new MemoryStream();

                        ICoreBitmap bmp = null;
                        if ((w>0) && (h>0) && (dpi >0))
                            bmp = t[0].ToBitmap(w, h, true , dpi,dpi);
                        else 
                            bmp = 
                            t[0].ToBitmap();
                        bmp.Save(mem, CoreBitmapFormat.Png);                    
                        mem.Seek(0, SeekOrigin.Begin);

                        this.MimeType = "image/png";
                        this.SendData(mem, true);
                        mem.Dispose();
                        return null;
                    }
                    //get picture extension 
                    //this.MimeType = "image/png";
                    //Bitmap bmp = new Bitmap(256, 256);
                    //Graphics g = Graphics.FromImage(bmp);
                    //g.Clear(Color.Red);
                    //g.Flush();
                    //MemoryStream mem = new MemoryStream();
                    //bmp.Save(mem, ImageFormat.Png);
                    //Byte[] t = new byte[mem.Length];
                    
                    ////StreamReader sm = new StreamReader(mem);
                    //mem.Seek(0, SeekOrigin.Begin);
                    //mem.Read(t, 0, t.Length);
                    //string h = ASCIIEncoding.Default.GetString(t, 0, t.Length);
                    //mem.Dispose();
                    //bmp.Dispose();
                    //return h;
                    break;
                case ".php":
                case ".phtml"://script server 
                    lock (m_syncobj)
                    {
                        if (!File.Exists(this.m_server.FileName))
                            return null;
                        Process p = new Process();
                        ProcessStartInfo v_info = new ProcessStartInfo
                        {
                            FileName = this.m_server.FileName
                        };
                        if (this.m_server.IsInAppContext)
                        {
                            /*
                             * 
                             * 
                             * 
                             * 
                             * */
                            v_info.Arguments = string.Format("-r \"{0}\"", GetExcutionQuery(GetFullRequest(filename)));
                        }
                        else if (this.m_server.IsInWebContext)
                        {
                            /*
                             * web context
                             * cgi.force_redirect ok
                             * user custom php ini file
                             * */

                            v_info.Arguments = 
                                //@"-c ""D:\wamp\bin\apache\Apache2.4.4\bin\php.ini"""+
                                string.Format(" -d cgi.force_redirect=0 -d doc_root=\"" +
                                this.m_server.DocumentRoot + "\" \"{0}\"", Path.GetFullPath(GetFullRequest(filename)));
                            this.RequestUri = this.m_requestUri;
                        }
                        v_info.RedirectStandardOutput = true;
                        v_info.RedirectStandardError = true;
                        v_info.RedirectStandardInput = true;
                        v_info.CreateNoWindow = true;

                        v_info.ErrorDialog = true;
                        v_info.UseShellExecute = false; //necessaire pour la redirection

                        this._setupEnvironment(v_info, filename );
                      
                        v_info.WorkingDirectory = PathUtils.GetDirectoryName(filename);                        
                        
                        p.StartInfo = v_info;
                        
                     
                        p.Start();
                        //post data required
                        if (PostDataLength > 0)
                        {
                            StreamWriter rw = p.StandardInput;
                            rw.WriteLine(PostData);
                            rw.Flush();
                        }
                    

                       BinaryReader bReader = new BinaryReader(p.StandardOutput.BaseStream);
                        Byte[] data = new byte[4096];
                        int count = 0;
                        //get response
                        string v_response = string.Empty;
                        try
                        {
                            while ((count = bReader.Read(data, 0, data.Length)) > 0)
                            {
                                v_response += ASCIIEncoding.Default.GetString(data, 0, count);
                            }
                        }
                        catch (Exception ex){
                            Debug.WriteLine("Exception : " + ex.Message);
                        }
                        p.Close();
                        //detect response value
                        if  (this.m_server.IsInAppContext)
                        {
                            if (v_response.Length > 3)
                            {//detecting png file response
                                if (!string.IsNullOrEmpty(v_response) && (v_response.Substring(1, 3) == "PNG"))
                                {
                                    this.MimeType = "image/png";
                                }
                            }
                        }
                        else{
                            //build header
                            string v_headerInfo = v_response.Split(new string[]{"\r\n\r\n"}, StringSplitOptions.None)[0];
                            //process headerInfo
                            ProcessHeaderInfo(v_headerInfo);
                            int i = v_response.IndexOf("\r\n\r\n", 0);
                            v_response = v_response.Substring(i + 4);
                            string v_redirectLocation = this.GetParam ("RedirectLocation");
                            if  (!string.IsNullOrEmpty (v_redirectLocation))
                            {
                                //this.StatusCode = "302 Found";
                                string v_msg=  "Must be redirected-Not Implement. Refresh the page . Please <a href='"+GetFullPathUri(v_redirectLocation)+"'>Click Here</a>";

                                this.StatusCode = "302 Found";
                                //v_msg = "OK";//Must be redirected-Not Implement. Refresh the page . Please <a href='" + GetFullPathUri(v_redirectLocation) + "'>Click Here</a>";
                                SendData(string.Empty, true);
                                //this.StatusCode = "200 OK";
                                //SendData("REP OK", true);
                                return null;//v_msg;
                            }
                        }
                        this.StatusCode = "200 OK";
                        return v_response;
                    }
                    
                case ".htaccess":
                    {
                        //protected htaccess file

                        return System.IO.File.ReadAllText(filename);
                    }
                default :
                    this.StatusCode = "304 Not Modified";
                    this.SendData(File.ReadAllText(filename), true);
                    break;
                    
            }
            return null;

        }

        private object GetQueryParam(string name, object defaultvalue)
        {
            if (string.IsNullOrEmpty(this.m_requestQuery))
                return defaultvalue;

            if (this.m_queryParams == null)
            {
                this.m_queryParams = new Dictionary<string, object>();
                string[] t = this.m_requestQuery.Split(new char[] { '&', '=' });
                for (int i = 0; i < t.Length; i+=2)
                {
                    if (this.m_queryParams.ContainsKey(t[i]))
                        this.m_queryParams[t[i]] = t[i + 1];
                    else 
                        this.m_queryParams.Add(t[i], t[i + 1]);
                }
            }
            if (this.m_queryParams.ContainsKey(name))
                return this.m_queryParams[name];
            return defaultvalue;

        }

        

        private void _setupEnvironment(ProcessStartInfo v_finfo, string filename)
        {
            PhpServerUtility.SetupEnvironment(this, v_finfo, filename);
        }
        /// <summary>
        /// load received loader info
        /// </summary>
        /// <param name="headerInfo"></param>
        private void ProcessHeaderInfo(string headerInfo)
        {
            string[] t = headerInfo.Split ('\n');
             Dictionary<string, string> v_data = new Dictionary<string,string> ();
            char[] split = new char[]{':'};
            foreach (string i in t)
            {
                var r = i.Trim().Split(split,  StringSplitOptions.RemoveEmptyEntries  );
                if (r.Length == 2)
                {
                    var k = r[0].ToLower();
                    if (!v_data.ContainsKey(k))
                    {
                        v_data.Add(k, r[1].Trim());
                    }
                    else { 
                        //replace 
                        v_data[k] = r[1].Trim();
                    }
                }                    
            }
            if (v_data.ContainsKey("content-type")) {
               this.MimeType = v_data["content-type"].Split (';')[0].Trim();
            }
            if (v_data.ContainsKey("set-cookie"))
            {
                this.m_sessionId = v_data["set-cookie"];
            }
            if (v_data.ContainsKey("location"))
            {
                if (v_data.ContainsKey("status"))
                { 
                    //redirect if necessairy
                    this.SetParam("Redirect", "1");
                    this.SetParam("RedirectLocation", v_data["location"]);
                    this.SetParam("RedirectStatus", v_data["status"]);
                }
            }
        }

        //private string GetScriptName(string filename)
        //{
        //        string uriPath = GetUriPath(filename);
        //        string v_documentroot = GetUriPath(m_server.DocumentRoot);
        //        string h =  uriPath.Substring(v_documentroot.Length);
        //        return h;
        //}

        private string GetExcutionQuery(string filename)
        {
            StringBuilder sb = new StringBuilder();
            string uriPath = GetUriPath(filename);
            string v_documentroot = GetUriPath(m_server.DocumentRoot);
            string self = uriPath.Substring(v_documentroot.Length);
            sb.Append(
@"parse_str($_SERVER['QUERY_STRING'], $_GET);
parse_str($_SERVER['QUERY_STRING'], $_REQUTEST);
parse_str($_SERVER['QUERY_STRING'], $_POST);

");
            if (string.IsNullOrEmpty (this.m_sessionId ) == false )
sb.AppendLine(string.Format("$_COOKIE['PHPSESSID'] = '{0}';", this.m_sessionId));
            sb.AppendLine(string.Format ("$_SERVER['DOCUMENT_ROOT']='{0}';", v_documentroot ));
sb.AppendLine(string.Format("$_SERVER['PHP_VER'] = phpversion();"));
sb.AppendLine(string.Format("$_SERVER['HTTP_HOST'] = '{0}';", m_server.ServerAddress));
//sb.AppendLine(string.Format("$_SERVER['HTTP_USER_AGENT'] = {0};","");
//sb.AppendLine(string.Format("$_SERVER['HTTP_ACCEPT'] = {0};","");
//sb.AppendLine(string.Format("$_SERVER['HTTP_ACCEPT_LANGUAGE'] = {0};", fr,fr-fr;q=0.8,en-us;q=0.5,en;q=0.3
//sb.AppendLine(string.Format("$_SERVER['HTTP_ACCEPT_ENCODING'] = {0};", gzip, deflate
//sb.AppendLine(string.Format("$_SERVER['HTTP_CONNECTION'] = {0};", keep-alive
//sb.AppendLine(string.Format("$_SERVER['PATH'] = {0};", C:\WINDOWS\system32;C:\WINDOWS;C:\WINDOWS\System32\Wbem;C:\WINDOWS\System32\WindowsPowerShell\v1.0\;C:\Program Files\Microsoft SQL Server\110\Tools\Binn\;C:\Program Files\Microsoft Windows Performance Toolkit\;C:\Program Files (x86)\Windows Kits\8.0\Windows Performance Toolkit\;C:\Program Files\Microsoft\Web Platform Installer\;C:\Program Files (x86)\QuickTime\QTSystem\;c:\Program Files (x86)\Microsoft SQL Server\100\Tools\Binn\;c:\Program Files\Microsoft SQL Server\100\Tools\Binn\;c:\Program Files\Microsoft SQL Server\100\DTS\Binn\;C:\Program Files (x86)\GtkSharp\2.12\bin;D:\wamp\bin\php\php5.3.5;C:\Program Files (x86)\Belgium Identity Card;C:\Program Files (x86)\Windows Installer XML v3.5\bin;D:\Android\android-sdk\tools;D:\Android\apache-ant-1.8.4\bin;C:\Program Files (x86)\Java\jdk1.6.0_39\bin;C:\Program Files (x86)\Smart Projects\IsoBuster;D:\Android\adt-bundle-windows-x86_64-20130729\sdk\platform-tools;C:\Program Files (x86)\Support Tools\;C:\Windows\Microsoft.NET\Framework64\v4.0.30319;D:\wamp\bin\php\php5.4.12;
//sb.AppendLine(string.Format("$_SERVER['SystemRoot'] = {0};", C:\WINDOWS
//sb.AppendLine(string.Format("$_SERVER['COMSPEC'] = {0};", C:\WINDOWS\system32\cmd.exe
//sb.AppendLine(string.Format("$_SERVER['PATHEXT'] = {0};", .COM;.EXE;.BAT;.CMD;.VBS;.VBE;.JS;.JSE;.WSF;.WSH;.MSC
//sb.AppendLine(string.Format("$_SERVER['WINDIR'] = {0};", C:\WINDOWS
//sb.AppendLine(string.Format("$_SERVER['SERVER_SIGNATURE'] = {0};", 
sb.AppendLine(string.Format("$_SERVER['SERVER_SOFTWARE'] = '{0}';", "IGKDEVWEBSERVER"));//Apache/2.4.4 (Win64) PHP/5.4.12
sb.AppendLine(string.Format("$_SERVER['SERVER_NAME'] = '{0}';", ""));//localhost
sb.AppendLine(string.Format("$_SERVER['SERVER_ADDR'] = '{0}';", m_server.ServerAddress ));
sb.AppendLine(string.Format("$_SERVER['SERVER_PORT'] = '{0}';", m_server.Port));
sb.AppendLine(string.Format("$_SERVER['REMOTE_ADDR'] = '{0}';", this.RemoteAddress));
sb.AppendLine(string.Format("$_SERVER['REMOTE_PORT'] = '{0}';",this.EndPointPort));
//sb.AppendLine(string.Format("$_SERVER['DOCUMENT_ROOT'] = {0};", D:/wamp/www/
//sb.AppendLine(string.Format("$_SERVER['REQUEST_SCHEME'] = {0};", http
//sb.AppendLine(string.Format("$_SERVER['CONTEXT_PREFIX'] = {0};", 
//sb.AppendLine(string.Format("$_SERVER['CONTEXT_DOCUMENT_ROOT'] = {0};", D:/wamp/www/
//sb.AppendLine(string.Format("$_SERVER['SERVER_ADMIN'] = {0};", admin@example.com
sb.AppendLine(string.Format("$_SERVER['SCRIPT_FILENAME'] = '{0}';", GetUriPath(filename)));

//sb.AppendLine(string.Format("$_SERVER['GATEWAY_INTERFACE'] = {0};", CGI/1.1
sb.AppendLine(string.Format("$_SERVER['SERVER_PROTOCOL'] = '{0}';", "HTTP/1.1"));
//sb.AppendLine(string.Format("$_SERVER['REQUEST_METHOD'] = {0};", GET
sb.AppendLine(string.Format("$_SERVER['QUERY_STRING'] = '{0}';", string.IsNullOrEmpty (this.m_requestQuery)?"": this.m_requestQuery));
sb.AppendLine(string.Format("$_SERVER['REQUEST_URI'] = '{0}';", "/"+this.m_requestUri ));
sb.AppendLine(string.Format("$_SERVER['SCRIPT_NAME'] = '{0}';", self));
sb.AppendLine(string.Format("$_SERVER['PHP_SELF'] = '{0}';", self));
sb.AppendLine(string.Format("unset($_SERVER['argv']);"));
sb.AppendLine(string.Format("unset($_SERVER['argc']);"));
//sb.AppendLine("require_once('" + filename + "'); echo 'end'; ");
sb.AppendLine (@"//post data in uri script a return data
function igk_post_uri($uri, $args=null, $content=""Application/x-www-form-urlencoded"")
{
$postdata = null;

if ($args!=null)
	$postdata = http_build_query(
    $args
);

$opts = array('http' =>
    array(
        'method'  => 'POST',
        'header'  => 'Content-type: '.$content,
        'content' => $postdata
    )
);
$context  = stream_context_create($opts);
$result = file_get_contents($uri, false, $context);
return $result;
}");

sb.AppendLine("echo igk_post_uri('" +filename + "');");
sb.AppendLine("echo 'end';");

            return sb.ToString().Replace ('"','\'');
        }

        private string GetFileUri(string filename)
        {
            string uriPath = GetUriPath(filename);
            string v_documentroot = GetUriPath(m_server.DocumentRoot);
            string self = uriPath.Substring(v_documentroot.Length);
            //return "http://localhost:" + this.m_server.Port + "" + 
                return "."+self;
        }

        //private string GetUriPath(string filename)
        //{
        //    return filename.Replace("\\", "/");
        //}

        //private string GetFullRequest(string filename)
        //{
        //    //if ((this.m_requestProtocol == "GET") && !string.IsNullOrEmpty (this.m_requestQuery ))
        //    //    return filename + "?" + this.m_requestQuery;
        //    return filename;
        //}

        /// <summary>
        /// run application
        /// </summary>
        public virtual void Run()
        {
            if (this.m_thread != null)
            {
                this.m_thread.SetApartmentState(ApartmentState.STA);
                this.m_thread.IsBackground = true;
                this.m_thread.Start();
            }
        }


        public string RequestUri { get; set; }

        public override Socket Socket => m_socket;

        public void Dispose()
        {
            this.m_socket.Dispose();
        }

        public override void SendData(byte[] data, bool closed)
        {
            throw new NotImplementedException();
        }

        public override string GetHeaderResponse(string hname)
        {
            throw new NotImplementedException();
        }
    }
}
