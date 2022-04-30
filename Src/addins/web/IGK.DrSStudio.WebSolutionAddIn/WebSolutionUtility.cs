

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebSolutionUtility.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Web
{
    /// <summary>
    /// represent the web solution utility
    /// </summary>
    public class WebSolutionUtility
    {
        public static void ExtractiGKWebFrameWorkTo(string outfolder)
        {
            var zip = CoreApplicationManager.Application.ResourcesManager.GetZipReader();
            if (zip != null)
            {
                zip.ExtractZipData(Properties.Resources.iGKWeb,
                    outfolder);
            }
        }

        internal static int GetFreePort()
        {
            Socket sock = new Socket(AddressFamily.InterNetwork,
                         SocketType.Stream, ProtocolType.Tcp);
            sock.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 0)); // Pass 0 here.
            int p = ((IPEndPoint)sock.LocalEndPoint).Port;            
            sock.Dispose();
            return p;
        }

        internal static string AddController(WebSolutionSolution webSolutionSolution, string controllerName)
        {
            string v_suri = webSolutionSolution.Uri.ToString() + WebSolutionConstant.ADDCTRL_URI + "&n=" + controllerName;
            return sendRequest(webSolutionSolution, v_suri);
        }
        private static string sendRequest(WebSolutionSolution solution, string uri)
        {
            HttpWebRequest request = HttpWebRequest.Create(uri) as HttpWebRequest;
            try
            {
               
                InitRequest(solution , request);
                HttpWebResponse rep = (HttpWebResponse)request.GetResponse();
                solution.GetHeader(rep);
                StreamReader sr = new StreamReader(rep.GetResponseStream());
                string v_str = sr.ReadToEnd();
                sr.Close();
                
                return v_str;
            }
            catch (Exception ex)
            {
                CoreLog.WriteDebug("Exception : " + ex.Message);
            }
            finally
            {
                
            }
            return null;
        }

        private static void InitRequest(WebSolutionSolution solution, HttpWebRequest request)
        {
            request.UserAgent = "WebSolutionUserAgent";
            solution.SetHeader(request );
        }

        public static string SetRequest(WebSolutionSolution solution, string query)
        {
            string v_suri = solution.Uri.ToString() + query;
            return sendRequest(solution, v_suri);
        }
    }
}
