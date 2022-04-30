

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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Configuration;
using System.Configuration.Install;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Security.Permissions;
using Microsoft.Win32;
using System.Windows.Forms;
using IGK.ICore;using IGK.DrSStudio.PicturePreviewHandler;
namespace IGK.DrSStudio.PreviewInstaller
{
    class Program
    {
        public static void Main(string[] args)
        {
            if ((args != null) && (args.Length > 0))
            {
                switch (args[0])
                {
                    case "/i":
                        InstallComponent();
                        return;
                    case "/u":
                        UnInstallComponent();
                        return;
                }
            }
            Application.EnableVisualStyles();
            Application.Run(new InstallerForm());
        }
        static IDictionary sm_state = new Hashtable();
        internal static void UnInstallComponent()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            AssemblyInstaller v_asminstaller = new AssemblyInstaller(asm, new string[] {"/LogFile=prev.log" });
            try
            {
                v_asminstaller.Uninstall(sm_state);
            }
            catch (Exception ex){
                Console.WriteLine("Failed  to uninstall component : "+ex.Message );
            }
        }
        internal static void InstallComponent()
        {
            Assembly asm = Assembly.GetExecutingAssembly ();
            AssemblyInstaller v_asminstaller = new AssemblyInstaller(asm, new string[] { "/LogFile=prev.log" });
            try
            {
                v_asminstaller.Install(sm_state);
                v_asminstaller.Commit(sm_state);
            }
            catch {
                Console.WriteLine("Install state failed");
            }
        }
    }
}

