using IGK.ICore.WinUI;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.ICore.WinCore
{
    public static class WinCoreService
    {
        public const string IE_FEATURE_KEY = @"Software\Microsoft\Internet Explorer\Main\FeatureControl\";
        public const string IE_EMULATION_KEY = @"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION";
        public const string IE_MAXSTATEMENT = @"Software\Microsoft\Internet Explorer\Styles";
        public const int IE_EDGE = 0x2ee1; //for edge 
        public const int IE_BS = 0x2af8; //11000
        public const int IE_11 = 11000;//0xA000;
        public const int IE_10 = 10000;//0xA000;
        public const int IE_9 = 0x9000;
        private static string sm_startLocation;
/*
 * 
 * USED To register the current application to targered internet explorer 10 if possible.
 * 
 * */

        public static void RegisterIE11WebService()
        {
            RegisterIEWebService(IE_11);
        }
        private static void DisableScriptStatement() {
            RegistryKey rg = Registry.CurrentUser.OpenSubKey(IE_MAXSTATEMENT, //@"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION",
               RegistryKeyPermissionCheck.ReadWriteSubTree,
              System.Security.AccessControl.RegistryRights.FullControl);
            if (rg == null)
            {
                try
                {
                    rg = Registry.CurrentUser.CreateSubKey(IE_MAXSTATEMENT, RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryOptions.None);
                }
                catch
                {
                    CoreLog.WriteDebug("Can't create " + IE_MAXSTATEMENT);
                    return;
                }
            }
            rg.SetValue("MaxScriptStatements", 
                0xFFFFFFFF,
                RegistryValueKind.DWord);

            rg.Close();
        }
        /// <summary>
        /// register IE web service
        /// </summary>
        /// <param name="p"></param>
        public static void RegisterIEWebService(int p)
        {
            //return;
            var v_asm = CoreSystemEnvironment.GetEntryAssembly();
            if ((v_asm == null) || v_asm.IsDynamic)
                return;
            RegistryKey rg = Registry.CurrentUser.OpenSubKey(IE_EMULATION_KEY, //@"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION",
                 RegistryKeyPermissionCheck.ReadWriteSubTree,
                System.Security.AccessControl.RegistryRights.FullControl);
            if (rg == null)
            {
                try
                {
                    rg = Registry.CurrentUser.CreateSubKey(IE_EMULATION_KEY, RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryOptions.None);
                }
                catch {
                    CoreLog.WriteDebug("Can't create "+IE_EMULATION_KEY);
                    return;
                }
            }
            string v_location = string.Empty;
            if (rg != null) 
            {

                try
                {
                    string g = Application.StartupPath;
                    string entry = v_asm.Location;
                    //used the current process name. not entry assembly 
                    string process = Process.GetCurrentProcess().MainModule.FileName;
                    v_location = Path.GetFileName(process); // Path.GetFileName(Assembly.GetExecutingAssembly().Location);
                    rg.SetValue(v_location,
                        p, RegistryValueKind.DWord);
                    rg.Close();
                    sm_startLocation = v_location;
                }
                catch
                {
                    CoreLog.WriteLine("Unabled to write registry key");
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static void RegisterIE10WebService()
        {
            var v_asm = CoreSystemEnvironment.GetEntryAssembly();
            if ((v_asm == null) || v_asm.IsDynamic)
                return;
            RegistryKey rg = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION",
                 RegistryKeyPermissionCheck.ReadWriteSubTree,
                System.Security.AccessControl.RegistryRights.FullControl);

            string v_location = string.Empty;
            if (rg != null)
            {

                try
                {
                    string g = Application.StartupPath;
                    string entry = v_asm.Location;
                    //used the current process name. not entry assembly 
                    string process = Process.GetCurrentProcess().MainModule.FileName;
                    v_location = Path.GetFileName(process); // Path.GetFileName(Assembly.GetExecutingAssembly().Location);
                    rg.SetValue(v_location,
                        9999, RegistryValueKind.DWord);
                    rg.Close();
                    sm_startLocation = v_location;
                }
                catch
                {
                    CoreLog.WriteLine("Unabled to write registry key");
                }
            }
        }

        public static void UnRegisterIE11WebService()
        {
            UnregisterIEWebService();
        }

        private static void UnregisterIEWebService()
        {

            RegistryKey rg = null;
            rg = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", true);
            if (rg != null)
            {
                try
                {
                    rg.DeleteValue(Path.GetFileName(sm_startLocation));
                }
                catch (Exception ex)
                {
                    CoreLog.WriteDebug(ex.Message);
                }
                finally
                {
                    rg.Close();
                }
            }
        }
        public static void UnregisterIE10WebService()
        {
            UnregisterIEWebService();
        }


        public static string PickFolder(this ICoreWorkbench bench, string startFolder)
        {
            using (var s = bench.CreateCommonDialog<IGK.ICore.WinUI.Common.FolderNamePicker>())
            {
                s.SelectedFolder = startFolder;
                if (s.ShowDialog() == enuDialogResult.OK)
                {
                    return s.SelectedFolder;
                }
            }
            return string.Empty;
        }


        public static bool EnableFeature(EnuFeatureType type) {
            string feature = string.Empty;
            switch (type)
            {
                case EnuFeatureType.mimeHanlding:
                    feature = "FEATURE_MIME_HANDLING";
                    break;
                default:
                    break;
            }

            string v_gkey = IE_FEATURE_KEY + "FEATURE_MIME_HANDLING";

            RegistryKey rg = Registry.CurrentUser.OpenSubKey(v_gkey, //@"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION",
               RegistryKeyPermissionCheck.ReadWriteSubTree,
              System.Security.AccessControl.RegistryRights.FullControl);
            if (rg == null)
            {
                try
                {
                    rg = Registry.CurrentUser.CreateSubKey(v_gkey, RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryOptions.None);
                }
                catch
                {
                    CoreLog.WriteDebug("Can't create " + v_gkey);
                    return false;
                }
            }
            string v_location = string.Empty;
            var v_asm = CoreSystemEnvironment.GetEntryAssembly();
            if (rg != null)
            {

                try
                {
                    string g = Application.StartupPath;
                    string entry = v_asm.Location;
                    //used the current process name. not entry assembly 
                    string process = Process.GetCurrentProcess().MainModule.FileName;
                    v_location = Path.GetFileName(process); // Path.GetFileName(Assembly.GetExecutingAssembly().Location);
                    rg.SetValue(v_location,0, RegistryValueKind.DWord);
                    rg.Close();
                    sm_startLocation = v_location;
                }
                catch
                {
                    CoreLog.WriteLine("Unabled to write registry key");
                    return false;
                }
            }
            return true;
        }

        
    }

   
    public enum EnuFeatureType : uint {
        mimeHanlding =2
    }
}
