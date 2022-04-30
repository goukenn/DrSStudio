

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Program.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:Program.cs
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
using System.Configuration.Install;
using System.Diagnostics;
namespace IGK.DrSStudio.PictureHandlerService
{
    class Program
    {
        static IDictionary hash = new Hashtable();
        static void Main(string[] args)
        {
            ServiceController c = Utility.GetService(PictureHandlerConstant.SERVICE_NAME);
            if (c == null)
            {
#if DEBUG
                EventLog.WriteEntry("Main", "Install Service : " + PictureHandlerConstant.SERVICE_NAME);
#endif
                //install service
                string[] v_commandLine = new string[]{"/LogFile=picHandlerSrv.Log"};
                AssemblyInstaller v_asmInstaller = new AssemblyInstaller(typeof(Program).Assembly.Location,
                    v_commandLine);
                try
                {
                    v_asmInstaller.Install(hash);
                    v_asmInstaller.Commit(hash);
                }
                catch {
                    Console.WriteLine("Error Append");
                }
                c = Utility.GetService(PictureHandlerConstant.SERVICE_NAME);
                if (c==null)
                    return;
#if DEBUG
                EventLog.WriteEntry("Main", "Start the Service : " + PictureHandlerConstant.SERVICE_NAME);
#endif
                c.Start();
                return;
            }
            if (c.Status != ServiceControllerStatus.Running)
            {
                ServiceBase.Run(new PictureHandlerSrv());
            }
            else
            {
                //uninstall the service
                //Uninstall the service
#if DEBUG
                EventLog.WriteEntry("Main", "Remove Service : " + PictureHandlerConstant.SERVICE_NAME);
#endif
                string[] v_commandLine1 = new string[] { "/LogFile=upicHandlerSrv.Log" };
                AssemblyInstaller v_asmInstaller1 = new AssemblyInstaller(typeof(Program).Assembly.Location,
                    v_commandLine1);
                v_asmInstaller1.Uninstall(hash);
            }
        }
    }
}

