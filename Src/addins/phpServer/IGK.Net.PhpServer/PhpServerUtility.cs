

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PhpServerUtility.cs
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
using System.Threading.Tasks;

namespace IGK.Net
{
    public class PhpServerUtility
    {

        internal static void SetupEnvironment(IPhpResponse phpResponse, System.Diagnostics.ProcessStartInfo v_info, string filename)
        {
            //v_info.EnvironmentVariables.Add("REMOTE_ADDR", phpResponse.m_from.ToString());
            //setting environment variables
            //v_info.EnvironmentVariables.Clear();
            //StringBuilder sb = new StringBuilder();
            //foreach (System.Collections.DictionaryEntry e in v_info.EnvironmentVariables)
            //{
            //    sb.AppendLine(e.Key.ToString() +"=" +e.Value);
            //}
            //File.WriteAllText(@CoreConstant.DEBUG_TEMP_FOLDER+"\\outu.txt", sb.ToString());

            //!!!!! important for mysql server to reservol remote address
            //v_info.EnvironmentVariables.Add("SystemRoot", Environment.GetEnvironmentVariable("SystemRoot"));


            //use for document root
            v_info.EnvironmentVariables.Add("DOCUMENT_ROOT", phpResponse.GetUriPath(phpResponse.Server.DocumentRoot));
            v_info.EnvironmentVariables.Add("PHP_VERSION", phpResponse.Server.Version);
            //set server information
            string v_srvname = phpResponse.GetParam("Host")?.Split(':')[0];
            if (string.IsNullOrEmpty(v_srvname))
                v_srvname = "localhost";
            v_info.EnvironmentVariables.Add("SERVER_NAME", v_srvname);
            if (phpResponse.Server.ServerAddress.AddressFamily == AddressFamily.InterNetwork)
            {
                v_info.EnvironmentVariables.Add("SERVER_ADDR", "127.0.0.1");
            }
            else
            {
                v_info.EnvironmentVariables.Add("SERVER_ADDR", phpResponse.Server.ServerAddress.ToString());
            }
            if (phpResponse.Server.Port != 80)
                v_info.EnvironmentVariables.Add("SERVER_PORT", phpResponse.Server.Port.ToString());

            string v_uagent = "IGKDEV - PhpServer;"+ phpResponse.GetParam("User-Agent");
            string v_host = phpResponse.GetParam("Host");

            v_info.EnvironmentVariables.Add("USER_AGENT", v_uagent);
            v_info.EnvironmentVariables.Add("HTTP_USER_AGENT", v_uagent);

            //other param
            //bind igk variable to by passing it througth HTTP_ prefixed environment variable.
            phpResponse.EnumParams((k, v) => {
                k = k.ToUpper();
                if (k.StartsWith("IGK"))
                {
                    v_info.EnvironmentVariables.Add("HTTP_" + k
                        .Replace("-", "_")
                        , v);

                }
                //else {
                //    if (!v_info.EnvironmentVariables.ContainsKey(k))
                //    {
                //        v_info.EnvironmentVariables.Add(k, v);
                //    }
                //}
            });


            //v_info.EnvironmentVariables.Add("HTTP_DATA", "INFO");



            //v_info.EnvironmentVariables.Add("REQUESTED_METHOD", phpResponse.RequestProtocol);
            v_info.EnvironmentVariables.Add("REQUEST_METHOD", phpResponse.RequestProtocol);
            
            v_info.EnvironmentVariables.Add("REFERER", phpResponse.GetParam("Referer"));
            v_info.EnvironmentVariables.Add("REMOTE_ADDR", "127.0.0.1"); //::1

            v_info.EnvironmentVariables.Add("HOST", v_host);
            v_info.EnvironmentVariables.Add("HTTP_HOST", v_host);
            if (phpResponse.ParamsContainsKey("Cookie"))
            {
                var v_cookie = phpResponse.GetParam("Cookie");
                v_info.EnvironmentVariables.Add("HTTP_COOKIE", v_cookie);
                v_info.EnvironmentVariables.Add("HTTP_CONNECTION", "keep-alive");
                v_info.EnvironmentVariables.Add("HTTP_CACHE_CONTROL", "max-age=0");
                var d = v_cookie.Split(new Char[] { ';', '=' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < d.Length; i += 2)
                {
                    var key = d[i].Trim();  
                    if ((key == "path")||( (v_info.EnvironmentVariables.ContainsKey (key)))) continue;
                    
                    v_info.EnvironmentVariables.Add(key, d[i + 1].Trim());
                }

            }
            //HTTP_ACCEPT_LANGUAGE
            //HTTP_ACCEPT
            //HTTP_USER_AGENT

            v_info.EnvironmentVariables.Add("SERVER_PROTOCOL", "HTTP/1.1");
            v_info.EnvironmentVariables.Add("GATEWAY_INTERFACE", "CGI/1.1");
            
            if (phpResponse.Server.IsInWebContext)
                v_info.EnvironmentVariables.Add("REDIRECT_STATUS", "200");
            v_info.EnvironmentVariables.Add("SCRIPT_NAME", phpResponse.GetScriptName(filename));
            v_info.EnvironmentVariables.Add("SCRIPT_FILENAME", phpResponse.GetUriPath(filename));

            v_info.EnvironmentVariables.Add("REQUEST_URI", "/"+phpResponse.RequestUri);// phpResponse.GetScriptName(filename));//"/" + phpResponse.m_requestUri);
            v_info.EnvironmentVariables.Add("QUERY_STRING", phpResponse.RequestQuery);


            switch (phpResponse.RequestProtocol) {
                case "OPTIONS":

                    break;
            }

            if (phpResponse.RequestProtocol != "GET")
            {
                v_info.EnvironmentVariables.Add("CONTENT_TYPE", phpResponse.GetParam("Content-Type"));// "application/x-www-form-urlencoded");
                v_info.EnvironmentVariables.Add("CONTENT_LENGTH", phpResponse.PostDataLength.ToString());
            }
            //for ssl setting
            //v_info.EnvironmentVariables.Add("HTTPS", "on");
            //v_info.EnvironmentVariables.Add("HTTP_UPGRADE_INSECURE_REQUESTS", "1");
            //v_info.EnvironmentVariables.Add("SSL_TLS_SNI", "local.com");
            //v_info.EnvironmentVariables.Add("REQUEST_SCHEME", "");


            //v_info.EnvironmentVariables.Add("Access-Control-Allow-Origin", "*");
            //v_info.EnvironmentVariables.Add("Access-Control-Allow-Methods", "GET, PUT, POST, DELETE, HEAD");
            //v_info.EnvironmentVariables.Add("Access-Control-Allow-Header", "Origin, X-requested-with, Content-Type, Accept");

            v_info.EnvironmentVariables.Add("HTTP_REFERER", phpResponse.GetParam("Referer") ?? phpResponse.Server.Location);
        }
    }
}
