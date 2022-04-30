

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinCoreTool.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.ICore.WinCore.Tools
{    
    using IGK.ICore;
    using IGK.ICore.Tools;
    using IGK.ICore.WinCore;

    [CoreTools("Tool.WinCoreTool", Description="Service for windows core dev")]
    public sealed class WinCoreTool : CoreToolBase
    {
        const string IE_EMULATION_KEY = @"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION";
        private static WinCoreTool sm_instance;
        //private static string sm_startLocation;
        private WinCoreTool()
        {
        }

        public static WinCoreTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static WinCoreTool()
        {
            sm_instance = new WinCoreTool();

        }
      
        /*
      * 
      * USED To register the current application to targered internet explorer 10 if possible.
      * 
      * */
     
        /*/// <summary>
        /// register key for ie 10 service
        /// </summary>
        public static void RegisterIE10WebService()
        {
            var v_asm = CoreSystemEnvironment.GetEntryAssembly();
            if ((v_asm == null) || v_asm.IsDynamic)
                return;
            
            RegistryKey rg = Registry.CurrentUser.OpenSubKey(IE_EMULATION_KEY,
                 RegistryKeyPermissionCheck.ReadWriteSubTree,
                System.Security.AccessControl.RegistryRights.FullControl);
            if (rg == null)
            {
                //try to create the sub key
                rg = Registry.CurrentUser.CreateSubKey(IE_EMULATION_KEY,
                       RegistryKeyPermissionCheck.ReadWriteSubTree);
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
                        9999, RegistryValueKind.DWord);
                    rg.Close();
                    sm_startLocation = v_location;
                }
                catch
                {
                    CoreLog.WriteLine("Unabled to write registry key");
                }
            }
            else
            {
                CoreLog.WriteLine("Unabled to write registry key");
            }
        }




        public static void UnregisterIE10WebService()
        {
            if (string.IsNullOrEmpty(sm_startLocation))
                return;
            
            RegistryKey rg = null;
            rg = Registry.CurrentUser.OpenSubKey(IE_EMULATION_KEY, true);
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
        }*/
    }
}
