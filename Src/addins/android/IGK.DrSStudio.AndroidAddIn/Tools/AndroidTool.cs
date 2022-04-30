

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidTool.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Android.Settings;
using IGK.ICore.IO;
using IGK.ICore.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Tools
{
    [CoreTools("Tool.Android")]
    public sealed class AndroidTool : AndroidToolBase 
    {
        public string AndroidPath {
            get {
                return Path.GetFullPath ( Path.Combine(SDK, AndroidConstant.TOOLS_ANDROID_PATH ));
            }
        }
        private static AndroidTool sm_instance;
        private AndroidTool()
        {
        }

        public static AndroidTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static AndroidTool()
        {
            sm_instance = new AndroidTool();
        }
        /// <summary>
        /// create a new android project
        /// </summary>
        /// <param name="name"></param>
        /// <param name="targetDirectory"></param>
        public string CreateProject(string name, string targetDirectory, 
            string packageName, 
            string mainActivityClass,
            AndroidTargetInfo target,
            AndroidTargetInfo mintarget)
        {
            if (target == null)
                return "ERROR: target is null";
            string v_commandList = string.Format("create project -n {0} -p {1} -t {2} {3}", name, targetDirectory, target.TargetId ,
                string.Format ("-k {0} -a {1}", packageName, mainActivityClass ));
            string v_file = this.AndroidPath;

            if (!File.Exists(v_file))
                return null;
            Process p = new Process();
            ProcessStartInfo v_info = new ProcessStartInfo();
            v_info.FileName = v_file;
            v_info.Arguments = v_commandList;
            v_info.UseShellExecute = false;
            v_info.WindowStyle = ProcessWindowStyle.Hidden;
            v_info.RedirectStandardInput = true;
            v_info.RedirectStandardOutput = true;
            v_info.RedirectStandardError = true;
            v_info.ErrorDialog = false;
            v_info.CreateNoWindow = true;
            p.StartInfo = v_info;
            p.Start();
            p.WaitForExit();
            string s =  AndroidUtils.GetString(p.StandardOutput);
            if (p.ExitCode == 0)
                return "SUCESSFULL";
            return "FAILED";
        }

        /// <summary>
        /// used to compile a project
        /// </summary>
        /// <param name="p"></param>
        public bool CompileProject(string directory, bool debug)
        {
            string v_commandList = debug ? "debug" : "release";
            if (string.IsNullOrEmpty(AndroidSetting.Instance.AntSDK))
                return false ;
            string v_file = Path.GetFullPath(Path.Combine(AndroidSetting.Instance.AntSDK, "bin/ant.bat"));

            if (!File.Exists(v_file) || !Directory.Exists (directory))
                return false ;
            Debug.WriteLine("Compiling project ... " + directory);
            Process p = new Process();
            ProcessStartInfo v_info = new ProcessStartInfo();
            v_info.FileName = v_file;
            v_info.Arguments = v_commandList;
            v_info.WorkingDirectory = directory;
            v_info.UseShellExecute = false;
            v_info.WindowStyle = ProcessWindowStyle.Hidden ;
            v_info.RedirectStandardInput = true;
            v_info.RedirectStandardOutput = true;
            v_info.RedirectStandardError = true;
            v_info.ErrorDialog = false;
            v_info.CreateNoWindow = true;

            
            AndroidSystemManager.SetUpEnvrionment(v_info);
            
            p.StartInfo = v_info;
            p.Start();
            string r = string.Empty ;
            StringBuilder sb = new StringBuilder();
            while ((r = AndroidUtils.GetString(p.StandardOutput, 4096)) != null)
            {
                Debug.WriteLine("--->"+r);
                sb.Append(r);
            }
            
            return (p.ExitCode == 0);
        }

        /// <summary>
        /// run command dir
        /// </summary>
        /// <param name="command">command directory</param>
        /// <param name="workingdir">working directory</param>
        public string  Cmd(string command, string workingdir)
        {
            string v_commandList = command.Substring(command.IndexOf(' ') + 1);
            string v_file = FindPath(command.Split(' ')[0]);

            if (File.Exists(v_file) == false )
                return string.Empty;

            Debug.WriteLine("Excecute command: " + command);
            Process p = new Process();
            ProcessStartInfo v_info = new ProcessStartInfo();
            v_info.FileName = v_file;
            v_info.Arguments = v_commandList;
            v_info.WorkingDirectory = workingdir;
            v_info.UseShellExecute = false;
            v_info.WindowStyle = ProcessWindowStyle.Hidden;
            v_info.RedirectStandardInput = true;
            v_info.RedirectStandardOutput = true;
            v_info.RedirectStandardError = true;
            v_info.ErrorDialog = false;
            v_info.CreateNoWindow = true;


            AndroidSystemManager.SetUpEnvrionment(v_info);

            p.StartInfo = v_info;
            p.Start();
            p.WaitForExit(5000);           
            string v =  AndroidUtils.GetString(p.StandardOutput);
            return v;
        }

        private string FindPath(string path)
        {
            string[] dir = new string[]{
                Path.GetFullPath (AndroidSetting.Instance.JavaSDK+"/Bin"),
                Path.GetFullPath (AndroidSetting.Instance.AntSDK + "/Bin"),
                Path.GetFullPath (AndroidSetting.Instance.PlatformSDK),
                Path.GetFullPath (AndroidSetting.Instance.PlatformSDK+"/platform-tools")
            };

            string ext = "("+ Environment.GetEnvironmentVariable ("PATHEXT").Replace (";","|").Replace(".","\\.") +")";
            if (Path.GetExtension(path) == string.Empty)
            {
                ext = path+ext;
            }
            else
                ext = path;
            foreach (string item in dir)
            {
                if (Directory.Exists(item))
                {
                    foreach (string d in Directory.GetFiles(item))
                    {
                        if (Regex.IsMatch(d, ext, RegexOptions.IgnoreCase ))
                        {
                            return Path.GetFullPath(Path.Combine(item, d));
                        }
                    }
                }
            }
            return string.Empty;
        }

      
        /// <summary>
        /// install the apk file
        /// </summary>
        /// <param name="apkFile"></param>
        /// <returns></returns>
        public bool DeployInstall(string apkFile)
        {
            string t =    Cmd("adb.exe install " +Path.GetFullPath ( apkFile), PathUtils.GetDirectoryName (apkFile ));
            System.Diagnostics.Debug.WriteLine(t);
            if (t.IndexOf("Failure") != -1)
            {
                return false;
            }
            return true ;
        }
        /// <summary>
        /// remove the 
        /// </summary>
        /// <param name="apkPagckageName"></param>
        /// <returns></returns>
        public bool DeployUnInstall(string apkPackageName)
        {
            string t = Cmd("adb.exe uninstall " + apkPackageName, Environment.CurrentDirectory);
            if (t.StartsWith ("Failure"))
                return false;
            return true;
        }

        public AndroidTargetInfo[] GetAndroidTargets()
        {
            return AndroidSystemManager.GetAndroidTargets();
        }

      


        class BackgroundWorker
        {
            private string m_argument;
            private string m_filename;
            private string m_response;
            private Thread m_thread;
            public string Response { get { return this.m_response; } }
            public BackgroundWorker(AndroidTool tool,  string filename, string argument)
            {
                this.m_filename = filename;
                this.m_argument = argument;
            }
            void DoJob()
            {
                if (File.Exists(this.m_filename) == false)
                    return;
                Process p = new Process();
                ProcessStartInfo v_info = new ProcessStartInfo();
                v_info.FileName = this.m_filename;
                v_info.Arguments = this.m_argument;
                v_info.UseShellExecute = false;
                v_info.WindowStyle = ProcessWindowStyle.Hidden;
                v_info.RedirectStandardInput = true;
                v_info.RedirectStandardOutput = true;
                v_info.RedirectStandardError = true;
                v_info.ErrorDialog = false;
                v_info.CreateNoWindow = true;
                p.StartInfo = v_info;
                p.Start();
                p.WaitForExit();

                string h = AndroidUtils.GetString(p.StandardOutput);
                this.m_response = h;
            }

            internal void Start()
            {
                this.m_thread = new Thread(DoJob);
                this.m_thread.IsBackground = true;
                this.m_thread.SetApartmentState(ApartmentState.STA);
                this.m_thread.Start();
            }

            internal void Stop()
            {
                if (this.m_thread != null)
                {
                    this.m_thread.Abort();
                    this.m_thread.Join();
                }
            }
        }





        public string Cmd(string command)
        {
            return Cmd(command, Environment.CurrentDirectory);
        }
      
    }
}
