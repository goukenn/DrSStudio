

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PhpResponseBase.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace IGK.Net
{
    public abstract class PhpResponseBase
    {

        public abstract string GetHeaderResponse(string hname);
        IPhpServer m_server;
        private string m_RequestProtocol;
        private string m_RequestQuery;
        private Dictionary<string, string> m_params;
        private string m_mimeType;
        private string m_requestFile;
        private string m_statusCode;
        private string m_postData; //get or set the post data

        /// <summary>
        /// get the socket in use
        /// </summary>
        public abstract Socket Socket { get; }
        public virtual string From { get; }

        public virtual string GetReceiveRequest()
        {
            return string.Empty;
        }

        public virtual void ClearParams()
        {
            this.m_params.Clear();
        }

        /// <summary>
        /// get or set the status code
        /// </summary>
        public string StatusCode { get { return m_statusCode; } set { this.m_statusCode = value; } }

        /// <summary>
        /// get the requested faile
        /// </summary>
        public string RequestFile { get { return m_requestFile;  } protected set { this.m_requestFile = value; } }
        /// <summary>
        /// get posted data string
        /// </summary>
        public string PostData { get { return m_postData; } protected set { this.m_postData = value; } }

        /// <summary>
        /// get the requested query
        /// </summary>
        public string RequestQuery
        {
            get { return m_RequestQuery; }
            protected set
            {
                if (m_RequestQuery != value)
                {
                    m_RequestQuery = value;
                }
            }
        }
        /// <summary>
        /// get the posted data length
        /// </summary>
        public int PostDataLength
        {
            get
            {
                if (string.IsNullOrEmpty(this.m_postData) == false)
                    return this.m_postData.Length;
                return 0;
            }
        }
        /// <summary>
        /// get the requested protocol
        /// </summary>
        public string RequestProtocol
        {
            get { return m_RequestProtocol; }
            protected set { this.m_RequestProtocol = value; }
        }
        public IPhpServer Server{
            get {
                return this.m_server;
            }
        }
        public PhpResponseBase(IPhpServer server)
        {
            this.m_server = server ?? throw new ArgumentNullException(nameof(server));
            this.m_mimeType = "text/html";
            this.m_params = new Dictionary<string, string>();
        }

        public void EnumParams(Action<string, string> callback) {
            foreach (KeyValuePair<string, string> d in this.m_params) {
                callback.Invoke(d.Key, d.Value);
            }
        }
        public bool ParamsContainsKey(string p)
        {
            if (this.m_params.ContainsKey(p))
                return true;
            return false ;
        }
        /// <summary>
        /// get the params
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetParam(string name)
        {
            if (name == "Set-Cookie") {
            }

            if (this.m_params.ContainsKey(name))
                return m_params[name];
            return string.Empty;
        }

       

        /// <summary>
        /// set parameter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void SetParam(string name, string value)
        {
            if (!string.IsNullOrEmpty(name) && !this.m_params.ContainsKey(name))
            {
                this.m_params.Add(name, value.Trim());
            }
            else {
                this.m_params[name] = value +";"+this.m_params[name];
            }
        }

        public void RemoveParam(string name) {
            if (this.m_params.ContainsKey(name))
            {
                this.m_params.Remove(name);
            }
        }

        public string GetUriPath(string filename)
        {
            if (string.IsNullOrEmpty (filename ))
                return filename;
            return filename.Replace("\\", "/");
        }

        public  string GetFullRequest(string filename)
        {
            //if ((this.m_requestProtocol == "GET") && !string.IsNullOrEmpty (this.m_requestQuery ))
            //    return filename + "?" + this.m_requestQuery;
            return filename;
        }
        /// <summary>
        /// get the scrip name according to document root
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public string GetScriptName(string filename)
        {
            
            string uriPath = GetUriPath(filename);
            string v_documentroot = GetUriPath(m_server.DocumentRoot);
            if (v_documentroot != null)
            {
                string h = uriPath.Substring(v_documentroot.Length);
                return h;
            }
            return string.Empty;
        }

        /// <summary>
        /// get the full request query
        /// </summary>
        /// <returns></returns>
        public  string GetFullQueryRequest()
        {
            if (string.IsNullOrEmpty(this.RequestQuery))
                return string.Empty;
            return "?" + this.RequestQuery;
        }

        public string GetFullPathUri(string v_redirectLocation)
        {
            return v_redirectLocation;
        }

        public abstract  void SendData(string message, bool closed);
        public abstract void SendData(byte[] data, bool closed);


        /// <summary>
        /// execute the filename 
        /// </summary>
        /// <param name="filename">execute the server filename. and return a string of status</param>
        /// <returns></returns>
        public abstract string Execute(string filename);

        /// <summary>
        /// get or set the mimetype
        /// </summary>
        public string MimeType { get { return this.m_mimeType; } set { this.m_mimeType = value; } }
      
    }
}
