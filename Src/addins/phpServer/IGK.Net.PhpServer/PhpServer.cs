

using IGK.ICore;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PhpServer.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace IGK.Net
{
    /// <summary>
    /// represent a php server
    /// </summary>
    public class PhpServer : IPhpServer
    {
        private string m_FileName;


        private string m_DocumentRoot;
        private string m_TargetSDKFolder; 
        private int m_Port;
        private bool m_isRunning;
        private bool m_webContext;//web context
        private TcpListener m_listener;
        private Thread m_runnerThread;       
        private Dictionary<string, string> m_serverParams;
        private Dictionary<string, string> m_mimes = new Dictionary<string, string>();
        private IPhpFileServerListener m_filelistener;

        
        /// <summary>
        /// get or set the current location referer
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// get or set the php target lib install folder;
        /// </summary>
        public string TargetSDKFolder
        {
            get { return m_TargetSDKFolder; }
            set
            {
                if (m_TargetSDKFolder != value)
                {
                    m_TargetSDKFolder = value;
                }
            }
        }

        /// <summary>
        /// get if  this web server in on web context
        /// </summary>
        public bool IsInWebContext {
            get {
                if (string.IsNullOrEmpty(this.m_FileName))
                    return false;
                return Path.GetFileName(this.m_FileName).ToLower() == "php-cgi.exe";
            }
        }
        /// <summary>
        /// get if this web server in on application context
        /// </summary>
        public bool IsInAppContext
        {
            get
            {
                if (string.IsNullOrEmpty(this.m_FileName))
                    return false;
                return Path.GetFileName(this.m_FileName).ToLower() == "php.exe";
            }
        }
        /// <summary>
        /// get the server adress
        /// </summary>
        public EndPoint ServerAddress {
            get {
                if (this.m_listener != null)
                    return this.m_listener.LocalEndpoint;
                return null;
            }
        }
        /// <summary>
        /// get or set the port
        /// </summary>
        public int Port
        {
            get { return m_Port; }
            set
            {
                if (m_Port != value)
                {
                    m_Port = value;
                }
            }
        }
        /// <summary>
        /// get or set the filename to execute
        /// </summary>
        public string FileName
        {
            get { return m_FileName; }          
        }

        
        /// <summary>
        /// Get or set the root folder
        /// </summary>
        public string DocumentRoot
        {
            get { return m_DocumentRoot; }
            set
            {
                if (m_DocumentRoot != value)
                {
                    m_DocumentRoot = value;
                }
            }
        }

        /// <summary>
        /// .ctr
        /// </summary>
        public PhpServer()
        {
            this.m_serverParams = new Dictionary<string, string>();
            this.m_webContext = true;
        }
        /// <summary>
        /// start listening on server
        /// </summary>
        public void StartServer()
        {
            if (this.m_isRunning || string.IsNullOrEmpty (this.TargetSDKFolder))
                return;
            try
            {
                if (this.WebContext)
                {
                    this.m_FileName = Path.Combine(this.TargetSDKFolder, "php-cgi.exe");
                }
                else {
                    this.m_FileName = Path.Combine(this.TargetSDKFolder, "php.exe");
                }

                //get server info
                this.GetServerInfo();

                this.m_listener = new TcpListener(IPAddress.Any, this.Port>0? this.Port : 0);
                this.m_listener.Start();
                m_runnerThread = new Thread(_RunServer)
                {
                    IsBackground = true
                };
                m_runnerThread.Start();
                this.m_isRunning = true;
                this.m_Port = ((IPEndPoint)this.m_listener.LocalEndpoint).Port;
            }
            catch(Exception ex) {
                this.m_isRunning = false;
                CoreLog.WriteLine("PhpServer Failed to launch : "+ex.Message );
            }
        }

        private void GetServerInfo()
        {
            string v_script = Path.Combine(this.TargetSDKFolder, "php.exe");
            if (File.Exists(v_script) == false) 
                return;

            ProcessStartInfo v_info = new ProcessStartInfo
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Hidden,
                WorkingDirectory = this.TargetSDKFolder,
                ErrorDialog = true,
                CreateNoWindow = true,
                FileName = v_script,
                Arguments = string.Format("-r \"{0}\"", GetServerInfoScript())
            };
            Process p = new Process
            {
                StartInfo = v_info
            };
            p.Start();

            //retrieve info
                BinaryReader bReader = new BinaryReader(p.StandardOutput.BaseStream);
                Byte[] data = new byte[4096];
                int count = 0;

                string v_response = string.Empty;
                while ((count = bReader.Read(data, 0, data.Length)) > 0)
                {
                    v_response += ASCIIEncoding.Default.GetString(data, 0, count);                    
                }
                string[] tab = v_response.Split(new char[]{';'}, StringSplitOptions.RemoveEmptyEntries );
                foreach (string item in tab)
                {
                    var h = item.Split (':');
                    if (h.Length ==2)
                    this.SetParam(h[0], h[1]);
                }

        }

        private void SetParam(string name, string value)
        {
            this.m_serverParams.Add(name, value);
        }
        private string GetParam(string name)
        {
            return this.m_serverParams[name];
        }
        /// <summary>
        /// get the server version
        /// </summary>
        public string Version { get { 
            if (this.m_serverParams .ContainsKey(PhpServerConstants.VERSION_KEY))
                return this.m_serverParams[PhpServerConstants.VERSION_KEY];
            return "0.0.0.0";
        } }
        /// <summary>
        /// get the server name
        /// </summary>
        public string ServerName { get {
            if (this.m_serverParams.ContainsKey(PhpServerConstants.SERVERNAME_KEY))
                return this.m_serverParams[PhpServerConstants.SERVERNAME_KEY];
            return "IGK.PhpServer";
        } }
        private string GetServerInfoScript()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("echo 'VERSION:'.phpversion().';';");
           // sb.AppendLine("echo 'SERVERNAME:';");
            return sb.ToString();
        }

        
        
        private void _RunServer()
        {
            try{
                while (true)
                {
                    try
                    {
                    Socket v_socket = this.m_listener.AcceptSocket();            
                    IPhpResponse rep =  this.CreateNewReponse(v_socket);
                    rep.Run();
                    }
                    catch(Exception ex)
                    {                    
                        Debug.WriteLine("Error : " + ex.Message);
                    }
                    
                }
            }
            catch(Exception ex) {
                Debug.WriteLine("Error : " + ex.Message);
            }
            finally{
                StopServer ();
            }
        }
        /// <summary>
        /// create a response 
        /// </summary>
        /// <param name="socket">Web Response</param>
        /// <returns></returns>
        protected virtual IPhpResponse CreateNewReponse(Socket socket)
        {
            return new PhpAsyncResponse(socket, this);                
        }

        public void StopServer()
        {
            if (this.m_isRunning) {
                Debug.WriteLine("Stopping IGK.Net.PhpServer...");
                this.m_listener.Stop();
                this.m_runnerThread.Abort(); 
                this.m_runnerThread.Join(10000);
                this.m_runnerThread = null;
                this.m_isRunning = false;
                this.m_listener = null;
                Debug.WriteLine("IGK.Net.PhpServer stopped ... OK");
            }
        }

        /// <summary>
        /// get or set if in web context
        /// </summary>
        public bool WebContext { get{
            return this.m_webContext;
        } set{
            this.m_webContext = value;
        } }



        public virtual string GetMimeTypeFromExtension(string p)
        {
            if (this.m_mimes.ContainsKey(p))
                return this.m_mimes[p];
            switch (p.ToLower())
            {
                case ".js":
                return "text/javascript";
                case ".css":
                return "text/css";
                case ".png":
                return "image/png";
                case ".jpg":
                case ".jpeg":
                return "image/png";

                case ".xml":
                case ".xsl":
                    return "application/xml";
                case ".json":
                    return "text/html"; // "application/json";

            }
            return "text/html";
        }

        public void SetRespondListener(IPhpFileServerListener listener)
        {
            this.m_filelistener = listener;
        }


        public virtual bool HandleResponse
        {
            get { return (this.m_filelistener != null); }
        }

        public virtual void SendResponse(PhpResponseBase phpAsyncResponse)
        {
            if (this.m_filelistener != null) {
                this.m_filelistener.SendResponse(phpAsyncResponse);
            }
        }

        public bool IsRunning { get { return this.m_isRunning; } }
    }
}
