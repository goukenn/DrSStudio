

using IGK.ICore.IO;
using IGK.ICore.WinUI;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: MySQLServer.cs
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
using System.Text;
using System.Threading.Tasks;

namespace IGK.Net.MySQL
{
    /// <summary>
    /// represent the default mysql server
    /// </summary>
    public class MySQLServer : IDisposable 
    {
        private string m_TargetPlatformDirectory;
        private DateTime m_startTime;
        private string m_User;
        private bool m_isRunning;
        public DateTime StartTime { get { return this.m_startTime;  } }

        /// <summary>
        /// .ctr
        /// </summary>
        public MySQLServer()
        {

        }

        /// <summary>
        /// get if this mysql server is running
        /// </summary>
        public static bool IsRunning
        {
            get { 
                    Process[] t = Process.GetProcessesByName("mysqld");
                    if ((t == null) || (t.Length == 0))
                    {
                        return false;
                    }
                    return true;
            }
        }
        /// <summary>
        /// Get or set the user
        /// </summary>
        public string User
        {
            get { return m_User; }
            set
            {
                if (m_User != value)
                {
                    m_User = value;
                }
            }
        }
        /// <summary>
        /// get or set the target plat form directory
        /// </summary>
        public string TargetPlatformDirectory
        {
            get { return m_TargetPlatformDirectory; }
            set
            {
                if (m_TargetPlatformDirectory != value)
                {
                    m_TargetPlatformDirectory = value;
                }
            }
        }
        public void RunServer()
        {

            Process[] t = Process.GetProcessesByName("mysqld");
            if ((t == null) || (t.Length == 0))
            {
                string sdkfolder = this.TargetPlatformDirectory ?? string.Empty ;// ==null? string.Empty: this.TargetPlatformDirectory;
                string v_file = Path.GetFullPath (Path.Combine(sdkfolder, "bin/mysqld.exe"));
                if (File.Exists(v_file))
                {//start deamon
                    //GSLog.WriteLine("fichier : existe mysqld");


                        ProcessStartInfo v_startInfo = new ProcessStartInfo();
                        v_startInfo.FileName = v_file;
                        v_startInfo.Arguments = "";
                        v_startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        v_startInfo.UseShellExecute = true;
                        v_startInfo.ErrorDialog = false;
                        v_startInfo.CreateNoWindow = true;
                        Process p = new Process();
                        p.StartInfo = v_startInfo;
                        try
                        {
                            p.Start();
                            p.WaitForExit(3000);
                            
                        }
                        catch(Exception ex) {
                            CoreMessageBox.Show(ex);
                        }
                        if (p.HasExited) {
                            CoreMessageBox.Show("mysqld s'est arrêter de manière innatendu ...\n "+
                                string.Format ("Code : {0}\n fichier :{1}",p.ExitCode ,
                                v_file ));
                        }
                        this.m_startTime = p.StartTime;
                        this.m_isRunning = true;
                    }
                }
                else {
                    Process p = t[0];
                    try
                    {
                        this.TargetPlatformDirectory = PathUtils.GetDirectoryName(p.MainModule.FileName);
                        this.m_startTime = p.StartTime;
                    }
                    catch {
                        IGK.ICore.CoreLog.WriteDebug("Can't obtain main module handle");
                    }
                }
        }
        /// <summary>
        /// stop the server
        /// </summary>
        public void  StopServer()
        {
            if (MySQLServer.IsRunning && this.m_isRunning)
            {//that meeen this application own the server
                string v_file = Path.Combine(this.TargetPlatformDirectory, "bin/mysqladmin.exe");
                if (File.Exists(v_file))
                {
                    ProcessStartInfo v_startInfo = new ProcessStartInfo();
                    v_startInfo.FileName = v_file;
                    v_startInfo.Arguments = string.Format("-u {0} shutdown", this.User);
                    v_startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    v_startInfo.UseShellExecute = true;
                    v_startInfo.ErrorDialog = false;
                    v_startInfo.CreateNoWindow = true;
                    Process p = new Process();
                    p.StartInfo = v_startInfo;
                    try
                    {
                        p.Start();
                        p.WaitForExit();
                        Debug.WriteLine("[MySQLServer] - Stopped by command...[OK]");
                        this.m_isRunning = false;
                    }
                    catch {
                        Debug.WriteLine("[MySQLServer] - can't Stop MySQLServer");
                    }

                }
            }
        }

        public void Dispose()
        {
            if (this.m_isRunning)
            {
                //this.StopServer();
            }
        }
    }
}
