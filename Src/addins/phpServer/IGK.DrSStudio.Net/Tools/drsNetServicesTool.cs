

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: drsNetServicesTool.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Net.Tools
{
    using IGK.DrSStudio.Net.Settings;
    using IGK.ICore;
    using IGK.ICore.IO;
    using IGK.Net;
    using IGK.Net.MySQL;
    using System.Diagnostics;

    /// <summary>
    /// provide and configure php server and mysql server connnection status
    /// </summary>
    [CoreTools("Tool.DrSNetServices")]
    class drsNetServicesTool : CoreToolBase
    {
        private static drsNetServicesTool sm_instance;
        private PhpServer m_phpServer;
        private MySQLServer m_mySQLServer;
        private bool m_run;


        private drsNetServicesTool()
        {
            CoreApplicationManager.ApplicationExit += _ApplicationExit;
            Process.GetCurrentProcess().Exited += _Exited;
        }

        public static drsNetServicesTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static drsNetServicesTool()
        {
            sm_instance = new drsNetServicesTool();

        }
        protected override void GenerateHostedControl()
        {
            base.GenerateHostedControl();
            //init services

            m_phpServer = new PhpServer();
            m_mySQLServer = new MySQLServer();


            m_phpServer.WebContext = true;
            m_phpServer.TargetSDKFolder = PhpServerSetting.Instance.TargetPath;
            m_phpServer.DocumentRoot = PathUtils.GetPath(PhpServerSetting.Instance.DocumentRoot);
            m_phpServer.Port = PhpServerConstant.PORT;
            PathUtils.CreateDir(m_phpServer.DocumentRoot);


            //configure mysql server service
            m_mySQLServer.TargetPlatformDirectory = PathUtils.GetPath(MySQLServerSettings.Instance.TargetPlatformDir);
            m_mySQLServer.User = MySQLServerSettings.Instance.User;

            //run servers
            m_phpServer.StartServer();
            m_mySQLServer.RunServer();
            m_run = true;
        }

        void _Exited(object sender, EventArgs e)
        {

        }

        private void __stopServer()
        {
            if (m_run)
            {
                this.m_phpServer.StopServer();
                this.m_mySQLServer.StopServer();
                m_run = false;
            }
        }

        void _ApplicationExit(object sender, EventArgs e)
        {
            __stopServer();
        }
    }
}
