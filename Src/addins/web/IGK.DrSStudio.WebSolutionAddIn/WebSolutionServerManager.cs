

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebSolutionServerManager.cs
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
﻿using IGK.ICore.Settings;
using IGK.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Web
{

    /// <summary>
    /// solution web explorer
    /// </summary>
    class WebSolutionServerManager
    {
        private WebSolutionSolution m_solution;
        private Uri m_Uri;
        PhpServer m_phpServer;

        public Uri Uri { get { return this.m_Uri; } }

        public WebSolutionServerManager(WebSolutionSolution solution)
        {            
            this.m_solution = solution;
            this.m_phpServer = new WebSolutionWebServer();

            this.m_phpServer.DocumentRoot = solution.OutFolder;
            this.m_phpServer.WebContext = true;
            this.m_phpServer.Port = WebSolutionUtility.GetFreePort();
            this.m_phpServer.TargetSDKFolder = (string) CoreSettings.GetSettingValue("PhpServer.TargetPath", null);
            this.m_phpServer.StartServer();

            this.m_Uri = new Uri(Uri.UriSchemeHttp + "://localhost:" + this.m_phpServer.Port);
        }

        class WebSolutionWebServer : PhpServer
        {
            protected override IPhpResponse CreateNewReponse(System.Net.Sockets.Socket socket)
            {
                //synchronous web response
                return new WebSolutionWebAsyncResponse(socket, this);
            }
        }
        class WebSolutionWebResponse : PhpResponse 
        {
            public WebSolutionWebResponse(System.Net.Sockets.Socket socket, WebSolutionWebServer server):base(socket, server)
            {
            } 
        }
        class WebSolutionWebAsyncResponse : PhpAsyncResponse
        {
            public WebSolutionWebAsyncResponse(System.Net.Sockets.Socket socket, WebSolutionWebServer server)
                : base(socket, server)
            {
            }
        }

        internal void Close()
        {
            if (this.m_phpServer != null)
            {
                this.m_phpServer.StopServer();
                this.m_phpServer = null;
            }
        }
    }
}
