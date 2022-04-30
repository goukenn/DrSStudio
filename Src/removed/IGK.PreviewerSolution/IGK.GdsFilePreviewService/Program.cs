

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
using System.Reflection;
using System.Runtime.InteropServices;
using System.Configuration;
using System.Configuration.Install;
using System.ServiceProcess;
namespace IGK.GdsFilePreviewService
{
    public class Program
    {
        static IDictionary sm_state = new Hashtable();
        public static void Main(string[] args)
        {
            if ((args != null) && (args.Length == 1))
            {
                switch (args[0].ToLower())
                {
                    case "/i":
                        //install and run the service
                        InstallService(Assembly.GetExecutingAssembly());
                        return;
                    case "/u":
                        UninstallService(Assembly.GetExecutingAssembly());
                        return;
                }
            }
            ServiceController ctr = GkdsFilePreviewHandler.GkdsFileConstant.GetService(GkdsFilePreviewHandler.GkdsFileConstant.SERVICE_NAME);
            if (ctr != null) 
            {
                if (ctr.Status == ServiceControllerStatus.Stopped)
                {
                    ctr.Start(new string[] { });
                    return;
                }
                if (ctr.Status != ServiceControllerStatus.Running)
                {
                    IGK.DrSStudio.CoreSystem.RegisterResources = false;
                    IGK.DrSStudio.CoreSystem.InitReg(null);
                    ServiceBase.Run(new GkdsFileService());
                }
            }
            else { 
                Console .WriteLine ("No service base {0} Installed", GkdsFilePreviewHandler .GkdsFileConstant.SERVICE_NAME );
            }
        }
        public static void UninstallService(Assembly assembly)
        {
            AssemblyInstaller v_asm = new AssemblyInstaller(assembly, new string[] { });
            try
            {
                v_asm.Uninstall(sm_state);
            }
            catch { 
            }
        }
        public static void InstallService(Assembly assembly)
        {
            AssemblyInstaller v_asm = new AssemblyInstaller(assembly, new string[] { });
            try
            {
                v_asm.Install(sm_state);
                v_asm.Commit(sm_state);
            }
            catch {
                Console.WriteLine("Installation failed");
            }
        }
    }
}

