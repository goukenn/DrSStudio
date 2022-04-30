

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PhpAsyncServerResponse.cs
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
    public class PhpAsyncResponse : PhpResponseBase, IDisposable,
        IPhpResponse
    {
        private System.Net.Sockets.Socket m_socket;
        private bool m_closeConnection;
        private Thread m_thread;
        private string m_receiveString;
        private EndPoint m_from;
        private string m_Protocol = "HTTP/1.1";
        private string m_requestUri;
        private object m_syncobj = new object();
        private string m_sessionId;
        private Dictionary<string, object> m_queryParams;
        private Byte[] m_listR;
        private IAsyncResult m_receive;
        private Dictionary<string, string> m_responseHeader;

        public string SessionId { get => m_sessionId; }
        public override Socket Socket { get { return m_socket; } }

        public override string From {
            get {
                return this.m_from.ToString();
            }
        }

        public override string GetReceiveRequest()
        {
            return this.m_receiveString;
        }
        public string RemoteAddress
        {
            get
            {
                return this.m_from.ToString().Split(':')[0];
            }
        }
        public int EndPointPort
        {
            get
            {
                return int.Parse(this.m_from.ToString().Split(':')[1]);
            }
        }

        public string RequestUri { get; set; }

        public PhpAsyncResponse(Socket socket, PhpServer server):base(server)
        {
            this.m_sessionId = string.Empty;
            this.StatusCode = "200 OK";
            this.m_socket = socket;
            this.m_from = socket.RemoteEndPoint;
            this.m_listR = new Byte[1024];
        }

        private void ReceiveCallBack(IAsyncResult ar)
        {
            try
            {
                if (!this.m_socket.Connected)
                    return;

                lock (this.m_socket)
                {
                    StringBuilder sb = new StringBuilder();
                    try
                    {
                        int i = this.m_socket.EndReceive(ar);
                        var s = this.m_from; 
                        if (i > 0)
                        {
                            //detect crypted data
                            if (this.m_listR[0] == 20) {
                               // 22,3,1,0,171...
                               // 22,3,1,
                            }

                            //start
                            if (
                                (this.m_listR[0] == 22)&&
                                (this.m_listR[1] == 3)
                                )
                            {
                                //ssl request
                                string data = Encoding.UTF32.GetString(this.m_listR);

                            }



                            sb.Append(Encoding.ASCII.GetString(this.m_listR, 0, i));
                            this.m_receiveString += sb.ToString();

                            //System.Diagnostics.Debug.WriteLine("RecieveDATA: " + this.m_receiveString.Length, "NET");

                            if (i == this.m_listR.Length)
                            {
                                //receive again
                                this.m_socket.BeginReceive(this.m_listR, 0, this.m_listR.Length, SocketFlags.None, this.ReceiveCallBack, this);
                            }
                            else
                            {
                                this.LoadReceiveString();


                                var expect = this.GetParam("Expect");
                                if (!string.IsNullOrEmpty(expect) && this.m_socket.Connected)
                                { ///100-continue
                                    // i = this.m_socket.Receive(this.m_listR, 0, this.m_listR.Length, SocketFlags.Peek, this.ReceiveCallBack, this);
                                }
                                else
                                {
                                    this.SendResponse();
                                    this.m_socket.Close();
                                }
                            }


                        }
                        else
                        {
                            this.LoadReceiveString();
                            this.m_socket.Close();
                        }

                    }
                    catch (Exception ex)
                    {
                        CoreLog.WriteDebug(ex.Message);
                    }
                }

            }
            catch {
            }
        }

        private void LoadReceiveString()
        {
            if (string.IsNullOrEmpty(this.m_receiveString))
                return;
            //get info
            string[] t = this.m_receiveString.Split(' ');

            if (t.Length <= 1)
            {
                this.SetParam("SSL_KEY", this.m_receiveString);
                return;
            }

            this.RequestProtocol = t[0];
            string f = t[1];//.Substring(3, 4);
            while (f.StartsWith("/"))
            {
                f = f.Substring(1);
            }
            m_requestUri = f;
            string[] v_info = f.Split('?');
            for (int i = 0; i < v_info.Length; i++)
            {
                if (i == 0)
                {
                    this.RequestFile = v_info[0];
                    //if (string.IsNullOrEmpty(this.RequestFile)) {
                    //    this.RequestFile = "index.php";
                    //}
                }
                else
                {
                    this.RequestQuery += v_info[i];
                }
            }
            string[] v_tab = this.m_receiveString.Split(new char[] { '\n' });
            int v_dataIndex = this.m_receiveString.IndexOf("\r\n\r\n");
            foreach (string item in v_tab)
            {
                var h = item.Trim().Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (h.Length > 1)
                {
                    this.SetParam(h[0], item.Substring(item.IndexOf(':', 0) + 1).Trim());
                }
            }

            //Debug.WriteLine("Receive protocol : " + RequestProtocol);
            switch (RequestProtocol.ToUpper())
            {
                case "POST":
                case "DELETE":
                case "PUT":
                case "SAVE":
                case "JSON":                    
                    {
                        string o = this.GetParam("Content-Type");
                        string olength = this.GetParam("Content-Length");
                        int len = int.Parse(olength);
                        switch (o)
                        {
                            case "application/x-www-form-urlencoded":
                                //get the post data

                                if ((v_dataIndex > 0) && (this.m_receiveString.Length > (v_dataIndex + 4)))
                                {
                                    this.PostData = this.m_receiveString.Substring(v_dataIndex + 4, len);
                                }

                                break;
                            default:
                                
                                if ((v_dataIndex > 0) && (this.m_receiveString.Length > (v_dataIndex + 4)))
                                {
                                    this.PostData = this.m_receiveString.Substring(v_dataIndex + 4, len);
                                }
                                break;
                        }
                    }
                    break;
                case "GET":
                default:
                    break;
              
            }

        }


        /// <summary>
        /// send header message
        /// </summary>
        /// <param name="mimeType"></param>
        /// <param name="length"></param>
        /// <param name="statusCode"></param>
        private void SendHeader(string mimeType, int length, string statusCode)
        {



            mimeType = string.IsNullOrEmpty(mimeType) ? "text/html" : mimeType;

            StringBuilder sb = new StringBuilder();
            sb.Append(this.m_Protocol);
            sb.Append(" " + statusCode).AppendLine();
            sb.AppendLine("Server: IGK-NETPhpServer");
            sb.Append("Content-Type: ");
            sb.Append(mimeType).Append("; charset=utf-8").AppendLine();
            sb.AppendLine("Cache-Control: public");


            if (this.ParamsContainsKey("CORS"))
            {
                this.RemoveParam("CORS");
                this.EnumParams(new Action<string, string>((i, j) =>
                {
                    sb.AppendLine($"{i}: {j}");
                }));
                
            }
            else
            {
                if (this.ParamsContainsKey("EndPointPort")) {
                    sb.AppendLine("EndPointPort:" + this.GetParam("EndPointPort"));
                }
                    if (this.ParamsContainsKey("Referer"))
                    sb.AppendLine("Referer:" + this.GetParam("Referer"));

                if (this.ParamsContainsKey("Connection"))
                    sb.AppendLine("Connection:" + this.GetParam("Connection"));
                else
                    sb.AppendLine("Connection:Keep-Alive");
                if (!string.IsNullOrEmpty(this.m_sessionId))
                {
                    sb.AppendLine(string.Format("Set-Cookie: {0};", this.m_sessionId));
                }
                sb.Append("Accept-Ranges: bytes").AppendLine();
                string v_redirectLocation = this.GetParam("RedirectLocation");
                if (!string.IsNullOrEmpty(v_redirectLocation))
                {
                    sb.AppendLine("Location: " + v_redirectLocation);
                }
                else if (this.ParamsContainsKey("Location")) {
                    sb.AppendLine("Location: " + this.GetParam("Location"));
                }

                if (this.ParamsContainsKey("CORSResponse")) {
                    sb.AppendLine("Access-Control-Allow-Origin: *");
                    sb.AppendLine("Cache-Control: no-cache");
                }

                    sb.Append("Content-Length: ");
                //append 2 line to limit header information
                sb.Append(length).AppendLine().AppendLine();

            }
            string v_str = sb.ToString();
            Byte[] data = Encoding.Default.GetBytes(v_str);// sb.ToString());
            //send header
            try
            {
                var e = m_socket.BeginSend (data, 0, data.Length,SocketFlags.None , null, this);// .Send(data);

                int h =  m_socket.EndSend(e);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception : " + ex.Message);
            }
            sb.Clear();

        }

        private void SendingCallBack(IAsyncResult ar)
        {
            lock (this.m_syncobj)
            {
                try
                {
                    if ((this.m_socket!=null) && this.m_socket.Connected)
                    {
                        int i = m_socket.EndSend(ar);
                        if (this.m_closeConnection)
                        {
                            m_socket.Close();
                        }
                    }
                }
                catch (Exception ex){
                    Debug.WriteLine("Failed SendingCallback: " + ex.Message);
                }
            }
        }
        /// <summary>
        /// send data
        /// </summary>
        /// <param name="mem"></param>
        /// <param name="closed"></param>
        private void SendData(MemoryStream mem, bool closed)
        {
            try
            {
                Byte[] tb_messages = new Byte[mem.Length];
                SendHeader(this.MimeType, (int)mem.Length, this.StatusCode);
                mem.Read(tb_messages, 0, tb_messages.Length);

                m_socket.BeginSend(tb_messages, 0, tb_messages.Length, SocketFlags.None, SendingCallBack, this);

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

        public override void SendData(byte[] data, bool closed)
        {
            try
            {
                SendHeader(this.MimeType, (int)data.Length, this.StatusCode);
               // mem.Read(tb_messages, 0, tb_messages.Length);
                m_socket.BeginSend(data, 0, data.Length, SocketFlags.None, SendingCallBack, this);

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

            IAsyncResult v_async = null;
            try
            {
                Byte[] tb_messages = 
                    //Encoding.UTF8.GetBytes(message);
                    Encoding.Default.GetBytes(message);
                SendHeader(this.MimeType, tb_messages.Length, this.StatusCode);
                v_async = m_socket.BeginSend(tb_messages, 0, tb_messages.Length, SocketFlags.None, SendingCallBack, this);


                //m_socket.EndSend(v_async);
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SendDataException : " + ex.Message);
            }
            finally
            {
                if (closed)
                {
                    if (v_async != null) {

                       // m_socket.EndSend(v_async);

                    }
                    this.m_closeConnection = closed;
           
                }
            }
        }
        private void SendResponse()
        {
            this.MimeType = "text/html";

            //string g = (string)this.GetParam("SSL_KEY");
            //if (!string.IsNullOrEmpty(g)){
            //    this.HandlePhpSSSLHandShake();
            //    //this.SendData(g, true);
            //    return;
            //}

            if (this.Server.HandleResponse)
            {
                this.Server.SendResponse(this);
                return;
            }

            if (this.Server.DocumentRoot == null) {
                // Send error response
                this.SendData("<h2>No Document root setup 404 Page Not Found <h2>", true);
                return;
            }
       
            string message = this.Execute(Path.GetFullPath(System.IO.Path.Combine(this.Server.DocumentRoot, this.RequestFile)));
#if DEBUG
            if (!this.StatusCode.StartsWith("200"))
            {
                CoreLog.WriteDebug("StatusCode  :" + this.StatusCode);
                CoreLog.WriteDebug("Process Result : " + message);
            }
#endif
            if (message == null)
            {
                if ((this.m_socket != null) && (this.m_socket.Connected))
                {
                    this.StatusCode = "404";
                    this.SendData("<h2>Error 404 Page Not Found <h2>", true);
                }
            }
        }

        /// <summary>
        /// .doing job htread
        /// </summary>
        protected void _doJob()    
        {
            //get file   

            try
            {
                m_receive = m_socket.BeginReceive(this.m_listR, 0, this.m_listR.Length, SocketFlags.None, this.ReceiveCallBack, this);
            }
            catch(Exception ex) {
                CoreLog.WriteDebug("Failed to begin receive : "+ex.Message);
            }
        }
        /// <summary>
        /// process the filename
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public override  string Execute(string filename)
        {
            //Debug.WriteLine("ProcessFile :>>> " + filename);
            if (!File.Exists(filename))
            {
                if (Directory.Exists(filename))
                {
                    //redirector to 
                    string v_hfilenmane = GetUriPath(filename);
                    if (!string.IsNullOrEmpty(this.RequestFile) && v_hfilenmane.EndsWith("/") == false)
                    {
                        this.SetParam("Redirect", "1");
             
                        this.SetParam("RedirectLocation", GetScriptName(filename + "/")
                            + this.GetFullQueryRequest());
                        //this.m_statusCode = "302 Found";
                        //this.setParam("RedirectStatus", "302 Found");
                        this.SetParam("RedirectStatus", "301 Move Permanently");
                        this.StatusCode = "301 Move Permanently";
                        string sb = string.Empty;// "Must be redirected-Not Implement. Refresh the page . Please <a href='" + GetScriptName(filename + "/") + "'>Click Here</a>";
                        this.SendData(sb, true);
                        return sb;

                    }
                    else
                    {
                        string ffile = Path.GetFullPath(System.IO.Path.Combine(filename, "index.php"));
                        if (!File.Exists(ffile))
                            return null;
                        filename = ffile;
                        //return Execute(Path.GetFullPath(System.IO.Path.Combine(filename, "index.php")));
                    }

                }
                //return null;
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
                        int w = Convert.ToInt32(this.GetQueryParam("w", -1));
                        int h = Convert.ToInt32(this.GetQueryParam("h", -1));
                        int dpi = Convert.ToInt32(this.GetQueryParam("dpi", 300));
                        MemoryStream mem = new MemoryStream();

                        ICoreBitmap bmp;
                        if ((w > 0) && (h > 0) && (dpi > 0))
                            bmp = t[0].ToBitmap(w, h, true, dpi, dpi);
                        else
                            bmp =  t[0].ToBitmap();
                        bmp.Save(mem, CoreBitmapFormat.Png);
                        mem.Seek(0, SeekOrigin.Begin);

                        this.MimeType = "image/png";
                        this.SendData(mem, true);
                        mem.Dispose();
                        return null;
                    }                  
                    break;
                case ".php":
                case ".phtml"://script server 
                    lock (m_syncobj)
                    {
                        try
                        {
                            return HandlePhpScript(filename);
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine("Failed to Handle " + ex.Message);
                        }
                    }
                    break;
                case ".htaccess":
                    {
                        //protected htaccess file
                        return "readProtected file "+ System.IO.File.ReadAllText(filename);
                    }
                default:

                    if (!File.Exists(filename)) {
                        //Redirect

                        //
                        return null;
                    }
                    //this cause the connection to be close prematuraly by the host
                    this.StatusCode = "304 Not Modified";
                    this.StatusCode = "200 OK";
                    int v_length = (int)(new FileInfo(filename).Length);
                    this.MimeType = GetMimeTypeFromExtension(Path.GetExtension(filename));
                   
                    this.SendHeader(this.MimeType, v_length, this.StatusCode);
                    if (this.m_socket.Connected)
                    {
                        try
                        {
                            var ei = this.m_socket.BeginSendFile(filename, null, this);

                            this.m_socket.EndSendFile(ei);
                            //this.m_socket.SendFile(filename);
                        }
                        catch(Exception ex)
                        {
                            CoreLog.WriteDebug("Connection close by host : "+ex.Message +" : "+filename);
                            
                        }
                        finally {
                            if (this.m_socket !=null)
                            this.m_socket.Close();

                        }
                    }
                    else 
                    {
                        Debug.WriteLine("No data sending");
                    }
                    return $"Request:{filename}";
            }
            return null;

        }


        public override string GetHeaderResponse(string name)
        {
            if (!string.IsNullOrEmpty(name) && (m_responseHeader != null) && m_responseHeader.ContainsKey(name.ToLower()))
                return m_responseHeader[name.ToLower ()];
            return null;

        }

        private string HandlePhpScript(string filename)
        {
            if (!File.Exists(this.Server.FileName))
                return null;
            Process p = new Process();
            ProcessStartInfo v_info = new ProcessStartInfo
            {
                FileName = this.Server.FileName
            };
            if (this.Server.IsInAppContext)
            {
                /*
                 * 
                 * 
                 * 
                 * 
                 * */
                v_info.Arguments = string.Format("-r \"{0}\"", GetExcutionQuery(GetFullRequest(filename)));
            }
            else if (this.Server.IsInWebContext)
            {
                /*
                 * web context
                 * cgi.force_redirect ok
                 * user custom php ini file
                 * */

                v_info.Arguments =
                    //@"-c ""D:\wamp\bin\apache\Apache2.4.4\bin\php.ini"""+
                    string.Format(" -d cgi.force_redirect=0 -d doc_root=\"" +
                    this.Server.DocumentRoot + "\" \"{0}\"", Path.GetFullPath(GetFullRequest(filename)));
                this.RequestUri = this.m_requestUri;
            }
            v_info.RedirectStandardOutput = true;
            v_info.RedirectStandardError = true;
            v_info.RedirectStandardInput = true;
            v_info.CreateNoWindow = true;

            v_info.ErrorDialog = true;
            v_info.UseShellExecute = false; //necessaire pour la redirection

        

            PhpServerUtility.SetupEnvironment(this, v_info, filename);
            

            v_info.WorkingDirectory = PathUtils.GetDirectoryName(filename);
            v_info.StandardOutputEncoding = Encoding.UTF8;

            p.StartInfo = v_info;
            this.RequestFile = filename;

            p.Start(); 

            //post data required
            if (PostDataLength > 0)
            {
                StreamWriter rw = p.StandardInput;
                rw.WriteLine(PostData);
                rw.Flush();
            }

             
            BinaryReader bReader = new BinaryReader(p.StandardOutput.BaseStream, Encoding.UTF8);
            Byte[] data = new byte[4096];
            int count;
            //get response
            string v_response = string.Empty;
            try
            {
                while ((count = bReader.Read(data, 0, data.Length)) > 0)
                {

                    v_response += Encoding.Default.GetString(data, 0, count);

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("HandlePhpScriptException : " + ex.Message);
            }
            p.Close();
            //detect response value
            if (this.Server.IsInAppContext)
            {
                if (v_response.Length > 3)
                {//detecting png file response
                    if (!string.IsNullOrEmpty(v_response) && (v_response.Substring(1, 3) == "PNG"))
                    {
                        this.MimeType = "image/png";
                    }
                }
            }
            else
            {
                //build header
                string[] v_data = v_response.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None);

                string v_headerInfo = v_data[0];
                //process headerInfo
                ProcessHeaderInfo(v_headerInfo);
                
                string v_redirectLocation = this.GetParam("RedirectLocation");
                int i = v_response.IndexOf("\r\n\r\n", 0);
                if( !string.IsNullOrEmpty(v_redirectLocation) || (i != -1))
                {
                    if (i !=-1)
                        v_response = v_response.Substring(i + 4);
              
                    if (!string.IsNullOrEmpty(v_redirectLocation))
                    {
                        this.StatusCode = "302 Found";
                        this.SetParam("RedirectStatus", "302 Found");
                        string sb = "Must be redirected-Not Implement. Refresh the page . Please <a href='" + GetFullPathUri(v_redirectLocation) + "'>Click Here</a>";
                        SendData(string.Empty, true);
                        return sb;
                    }
                }
            }
            this.StatusCode = "200 OK";          
            this.SendData(v_response, true);
            if (this.MimeType == "text/css") {

            }
            return v_response;
        }



        private string HandlePhpSSSLHandShake()
        {
            throw new Exception("HandlePhpSSLHandShake No Support of SSL");
            //Process p = new Process();
            //ProcessStartInfo v_info = new ProcessStartInfo{
            //    FileName = this.Server.FileName
            //};
            //string filename = "";

            //if (this.Server.IsInAppContext)
            //{
            //    /*
            //     * 
            //     * 
            //     * 
            //     * 
            //     * */
            //    v_info.Arguments = string.Format("-r \"{0}\"", GetExcutionQuery(GetFullRequest(filename)));
            //}
            //else if (this.Server.IsInWebContext)
            //{
            //    /*
            //     * web context
            //     * cgi.force_redirect ok
            //     * user custom php ini file
            //     * */

            //    v_info.Arguments =
            //        //@"-c ""D:\wamp\bin\apache\Apache2.4.4\bin\php.ini"""+
            //        string.Format(" -d cgi.force_redirect=0 -d doc_root=\"" +
            //        this.Server.DocumentRoot + "\" \"{0}\"", "");// Path.GetFullPath(GetFullRequest(filename)));
            //    this.RequestUri = this.m_requestUri;
            //}
            //v_info.RedirectStandardOutput = true;
            //v_info.RedirectStandardError = true;
            //v_info.RedirectStandardInput = true;
            //v_info.CreateNoWindow = true;

            //v_info.ErrorDialog = true;
            //v_info.UseShellExecute = false; //necessaire pour la redirection

            ////PhpServerUtility.SetupEnvironment(this, v_info, filename);


            ////v_info.WorkingDirectory = PathUtils.GetDirectoryName(filename);
            //v_info.StandardOutputEncoding = Encoding.UTF8;

            //p.StartInfo = v_info;
            //this.RequestFile = filename;

            //p.Start(); 

            ////post data required
            //if (PostDataLength > 0)
            //{
            //    StreamWriter rw = p.StandardInput;
            //    rw.WriteLine(PostData);
            //    rw.Flush();
            //}


            //Encoding enc = p.StandardOutput.CurrentEncoding;
            //BinaryReader bReader = new BinaryReader(p.StandardOutput.BaseStream, Encoding.UTF8);
            //Byte[] data = new byte[4096];
            //int count = 0;
            ////get response
            //string v_response = string.Empty;
            //try
            //{
            //    while ((count = bReader.Read(data, 0, data.Length)) > 0)
            //    {

            //        v_response += Encoding.Default.GetString(data, 0, count);

            //    }
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine("HandlePhpScriptException : " + ex.Message);
            //}
            //p.Close();
            ////detect response value
            //if (this.Server.IsInAppContext)
            //{
            //    if (v_response.Length > 3)
            //    {//detecting png file response
            //        if (!string.IsNullOrEmpty(v_response) && (v_response.Substring(1, 3) == "PNG"))
            //        {
            //            this.MimeType = "image/png";
            //        }
            //    }
            //}
            //else
            //{
            //    //build header
            //    string[] v_data = v_response.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None);

            //    string v_headerInfo = v_data[0];
            //    //process headerInfo
            //    ProcessHeaderInfo(v_headerInfo);

            //    string v_redirectLocation = this.GetParam("RedirectLocation");
            //    int i = v_response.IndexOf("\r\n\r\n", 0);
            //    if (!string.IsNullOrEmpty(v_redirectLocation) || (i != -1))
            //    {
            //        if (i != -1)
            //            v_response = v_response.Substring(i + 4);

            //        if (!string.IsNullOrEmpty(v_redirectLocation))
            //        {
            //            this.StatusCode = "302 Found";
            //            this.SetParam("RedirectStatus", "302 Found");
            //            string sb = "Must be redirected-Not Implement. Refresh the page . Please <a href='" + GetFullPathUri(v_redirectLocation) + "'>Click Here</a>";
            //            SendData(string.Empty, true);
            //            return sb;
            //        }
            //    }
            //}
            //this.StatusCode = "200 OK";
            //this.SendData(v_response, true);
            //return v_response;
        }

        protected string GetMimeTypeFromExtension(string p)
        {            
            return this.Server.GetMimeTypeFromExtension(p);
        }

        private object GetQueryParam(string name, object defaultvalue)
        {
            if (string.IsNullOrEmpty(this.RequestQuery))
                return defaultvalue;

            if (this.m_queryParams == null)
            {
                this.m_queryParams = new Dictionary<string, object>();
                string[] t = this.RequestQuery.Split(new char[] { '&', '=' });
                for (int i = 0; i < t.Length; i += 2)
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



   

        /// <summary>
        /// load received loader info
        /// </summary>
        /// <param name="headerInfo"></param>
        private void ProcessHeaderInfo(string headerInfo)
        {
            string[] t = headerInfo.Split(new string[] { "\r\n" } ,  StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, string> v_data = new Dictionary<string, string>();
            char[] split = new char[] { ':' };
            foreach (string i in t)
            {

                var r = i.Trim().Split(split, StringSplitOptions.RemoveEmptyEntries);
                if (r.Length > 0)
                {
                    var k = r[0].ToLower();
                    string[] v_t = new string[r.Length - 1];
                    for (int ci = 0; ci < v_t.Length; ci++)
                    {
                        v_t[ci] = r[ci + 1];

                    }

                    var v = string.Join(":", v_t).Trim();

                    if (!v_data.ContainsKey(k))
                    {
                        v_data.Add(k, v);
                    }
                    else
                    {
                        //replace 
                        //v_data[k] = v_data[k]+";"+v;
                    }
                }
            }
            if (v_data.ContainsKey("content-type"))
            {
                this.MimeType = v_data["content-type"].Split(';')[0].Trim();
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

            m_responseHeader = v_data;
        }

     

        private string GetExcutionQuery(string filename)
        {
             
            StringBuilder sb = new StringBuilder();
            string uriPath = GetUriPath(filename);
            string v_documentroot = GetUriPath(this.Server.DocumentRoot);
            string self = uriPath.Substring(v_documentroot.Length);
            sb.Append(
@"parse_str($_SERVER['QUERY_STRING'], $_GET);
parse_str($_SERVER['QUERY_STRING'], $_REQUTEST);
parse_str($_SERVER['QUERY_STRING'], $_POST);

");
            if (string.IsNullOrEmpty(this.m_sessionId) == false)
                sb.AppendLine(string.Format("$_COOKIE['PHPSESSID'] = '{0}';", this.m_sessionId));
            sb.AppendLine(string.Format("$_SERVER['DOCUMENT_ROOT']='{0}';", v_documentroot));
            sb.AppendLine(string.Format("$_SERVER['PHP_VER'] = phpversion();"));
            sb.AppendLine(string.Format("$_SERVER['HTTP_HOST'] = '{0}';", this.Server.ServerAddress));
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
            sb.AppendLine(string.Format("$_SERVER['SERVER_ADDR'] = '{0}';", this.Server.ServerAddress));
            sb.AppendLine(string.Format("$_SERVER['SERVER_PORT'] = '{0}';", this.Server.Port));
            sb.AppendLine(string.Format("$_SERVER['REMOTE_ADDR'] = '{0}';", this.RemoteAddress));
            sb.AppendLine(string.Format("$_SERVER['REMOTE_PORT'] = '{0}';", this.EndPointPort));
            //sb.AppendLine(string.Format("$_SERVER['DOCUMENT_ROOT'] = {0};", D:/wamp/www/
            //sb.AppendLine(string.Format("$_SERVER['REQUEST_SCHEME'] = {0};", http
            //sb.AppendLine(string.Format("$_SERVER['CONTEXT_PREFIX'] = {0};", 
            //sb.AppendLine(string.Format("$_SERVER['CONTEXT_DOCUMENT_ROOT'] = {0};", D:/wamp/www/
            //sb.AppendLine(string.Format("$_SERVER['SERVER_ADMIN'] = {0};", admin@example.com
            sb.AppendLine(string.Format("$_SERVER['SCRIPT_FILENAME'] = '{0}';", GetUriPath(filename)));

            //sb.AppendLine(string.Format("$_SERVER['GATEWAY_INTERFACE'] = {0};", CGI/1.1
            sb.AppendLine(string.Format("$_SERVER['SERVER_PROTOCOL'] = '{0}';", "HTTP/1.1"));
            //sb.AppendLine(string.Format("$_SERVER['REQUEST_METHOD'] = {0};", GET
            sb.AppendLine(string.Format("$_SERVER['QUERY_STRING'] = '{0}';", string.IsNullOrEmpty(this.RequestQuery) ? "" : this.RequestQuery));
            sb.AppendLine(string.Format("$_SERVER['REQUEST_URI'] = '{0}';", "/" + this.m_requestUri));
            sb.AppendLine(string.Format("$_SERVER['SCRIPT_NAME'] = '{0}';", self));
            sb.AppendLine(string.Format("$_SERVER['PHP_SELF'] = '{0}';", self));
            sb.AppendLine(string.Format("unset($_SERVER['argv']);"));
            sb.AppendLine(string.Format("unset($_SERVER['argc']);"));
            //sb.AppendLine("require_once('" + filename + "'); echo 'end'; ");
            sb.AppendLine(@"//post data in uri script a return data
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
            sb.AppendLine("echo igk_post_uri('" + filename + "');");
            sb.AppendLine("echo 'end';");

            return sb.ToString().Replace('"', '\'');
        }

        private string GetFileUri(string filename)
        {
            string uriPath = GetUriPath(filename);
            string v_documentroot = GetUriPath(this.Server.DocumentRoot);
            string self = uriPath.Substring(v_documentroot.Length);
            return "." + self;
        }

       

        /// <summary>
        /// run application
        /// </summary>
        public virtual  void Run()
        {
            if (this.m_thread == null)
            {
                this.m_thread = new Thread(this._doJob)
                {
                    Name = "PhpAsync_" + this.GetHashCode(),
                    IsBackground = true
                };
                this.m_thread.SetApartmentState(ApartmentState.STA);
                this.m_thread.Start();
            }
        }

        

        public void Dispose()
        {
            this.m_socket.Dispose();
        }

     
    }
}
