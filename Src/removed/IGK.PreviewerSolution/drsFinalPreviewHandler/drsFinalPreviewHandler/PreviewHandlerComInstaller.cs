

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PreviewHandlerComInstaller.cs
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
file:PreviewHandlerComInstaller.cs
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration.Install;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Microsoft.Win32;
namespace IGK.DrSStudio.PreviewHandler
{
    [RunInstaller(true)]
    public class PreviewHandlerComInstaller : Installer
    {
         /// <summary>
         /// install current assembly
         /// </summary>
        public  static void UninstallAssembly()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            Hashtable sm_state = new Hashtable();
            AssemblyInstaller asmI = new AssemblyInstaller(asm, new string[] { });
            try
            {
                asmI.Uninstall(sm_state);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
         /// <summary>
         /// uninsitall current assembly
         /// </summary>
        public static void InstallAssembly()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Installing this Assembly");
            Console.ResetColor();
            Assembly asm = Assembly.GetExecutingAssembly();
            Hashtable sm_state = new Hashtable();
            AssemblyInstaller asmI = new AssemblyInstaller(asm, new string[] { });
            asmI.Install(sm_state);
            asmI.Commit(sm_state);
        }
        public override void Install(IDictionary stateSaver)
        {
            try
            {
                base.Install(stateSaver);
                RegistrationServices regsrv = new RegistrationServices();
                if (!regsrv.RegisterAssembly(this.GetType().Assembly, AssemblyRegistrationFlags.SetCodeBase))
                {
                    throw new InstallException("Failed to register for COM interop.");
                }
            }
            catch (Exception exc)
            {
                Trace.WriteLine(exc.ToString());
                throw;
            }
        }
        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);
            RegistrationServices regsrv = new RegistrationServices();
            if (!regsrv.UnregisterAssembly(this.GetType().Assembly))
            {
                throw new InstallException("Failed to unregister for COM interop.");
            }
        }
        public  static void RegisterPreviewHandler(string name, string extensions, string previewerGuid, string appId)
        {
            // Create a new prevhost AppID so that this always runs in its own isolated process
            using (RegistryKey appIdsKey = Registry.ClassesRoot.OpenSubKey("AppID", true))
            using (RegistryKey appIdKey = appIdsKey.CreateSubKey(appId))
            {
                appIdKey.SetValue("DllSurrogate", @"%SystemRoot%\system32\prevhost.exe", RegistryValueKind.ExpandString);
            }
            // Add preview handler to preview handler list
            using (RegistryKey handlersKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\PreviewHandlers", true))
            {
                handlersKey.SetValue(previewerGuid, name, RegistryValueKind.String);
            }
            // Modify preview handler registration
            using (RegistryKey clsidKey = Registry.ClassesRoot.OpenSubKey("CLSID"))
            using (RegistryKey idKey = clsidKey.OpenSubKey(previewerGuid, true))
            {
                idKey.SetValue("DisplayName", name, RegistryValueKind.String);
                idKey.SetValue("AppID", appId, RegistryValueKind.String);
                idKey.SetValue("DisableLowILProcessIsolation", 1, RegistryValueKind.DWord); // optional, depending on what preview handler needs to be able to do
            }
            foreach (string extension in extensions.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
            {
                Trace.WriteLine("Registering extension '" + extension + "' with previewer '" + previewerGuid + "'");
                // Set preview handler for specific extension
                using (RegistryKey extensionKey = Registry.ClassesRoot.CreateSubKey(extension))
                {
                    extensionKey.SetValue(null, string.Empty);
                    using (RegistryKey shellexKey = extensionKey.CreateSubKey("shellex"))
                    using (RegistryKey previewKey = shellexKey.CreateSubKey("{8895b1c6-b41f-4c1c-a562-0d564250836f}"))
                    {
                        previewKey.SetValue(null, previewerGuid, RegistryValueKind.String);
                    }
                }
            }
        }
        public static void UnregisterPreviewHandler(string extensions, string previewerGuid, string appId)
        {
            foreach (string extension in extensions.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
            {
                Trace.WriteLine("Unregistering extension '" + extension + "' with previewer '" + previewerGuid + "'");
                using (RegistryKey shellexKey = Registry.ClassesRoot.OpenSubKey(extension + "\\shellex", true))
                {
                    shellexKey.DeleteSubKey("{8895b1c6-b41f-4c1c-a562-0d564250836f}", false);
                }
            }
            using (RegistryKey appIdsKey = Registry.ClassesRoot.OpenSubKey("AppID", true))
            {
                appIdsKey.DeleteSubKey(appId, false);
            }
            using (RegistryKey classesKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\PreviewHandlers", true))
            {
                classesKey.DeleteValue(previewerGuid, false);
            }
        }
    }
}

