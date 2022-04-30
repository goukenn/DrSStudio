using IGK.ICore.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IGK.ICore
{
    /// <summary>
    /// represent a static class for environment definitions
    /// </summary>
    public static class CoreSystemEnvironment
    {
        private static Assembly sm_entryAssembly;

        /// <summary>
        /// indicate if this lib is loaded in design mode
        /// </summary>
        public static bool DesignMode {
            get { 
#if __ANDROID__
                return false;
#else
                return LicenseManager.UsageMode == LicenseUsageMode.Designtime;
#endif
            }
        }
        /// <summary>
        /// get the entry assembly
        /// </summary>
        /// <returns></returns>
        public static Assembly GetEntryAssembly() 
        {
           #if __ANDROID__
                return sm_entryAssembly;
            #else
                return sm_entryAssembly ?? Assembly.GetEntryAssembly();
            #endif
            
        }
        /// <summary>
        /// enternal set entry assembly
        /// </summary>
        /// <param name="v_entryAsm"></param>
        internal static void SetEntryAssembly(Assembly v_entryAsm)
        {
            sm_entryAssembly = v_entryAsm;
        }
        /// <summary>
        /// get if this execution context is in console mode
        /// </summary>
        public static bool IsInConsoleMode { get {
            return CoreUtils.IsInConsoleMode();
        } 
        }
        
        /// <summary>
        /// evaluate string by replacing environment variables
        /// </summary>
        /// <param name="rs"></param>
        /// <returns></returns>
        public static string EvalString(string rs)
        {
            rs = Regex.Replace(rs, "%(?<key>[a-z]+)%", (o) => {
                return GetEnvironmentValue(o.Groups["key"].Value);
                
            }, RegexOptions.IgnoreCase);

            return rs;
        }

        internal static string GetEnvironmentValue(string p)
        {
            string s= p.ToLower();
            switch (s)
            {
                case "progfile":
                    string S = string.Empty;
                    if (Environment.Is64BitProcess)
                    {
                       S = Environment.GetEnvironmentVariable("ProgramFiles");
                    }
                    else {
                        S = Environment.GetEnvironmentVariable("ProgramFiles(x86)");
                        if (string.IsNullOrEmpty (S))
                            S = Environment.GetEnvironmentVariable("ProgramFiles");                        
                    }
                    return S;
                case "coreversion":
                    return CoreConstant.VERSION;
                case "startup":
                    return CoreApplicationManager.StartupPath;
                case "sourcedir":
                    return CoreApplicationManager.SourceDir;
                case "programfile":
                    return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
                case "system":
                    return Environment.GetFolderPath(Environment.SpecialFolder.SystemX86);
                case "programfilex64":
                    return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                case "systemx64":
                    return Environment.GetFolderPath(Environment.SpecialFolder.System);  
                case "temp":
                    return PathUtils.GetPath((string)CoreSystem.GetSettings()["Core.GeneralSetting"]["TempFolder"].Value);
                default :
                    var b = CoreSystem.GetSettings()["Core.GeneralSetting"][s];
                    if (b != null)
                    {
                        string pp = (string)b.Value;
                        return PathUtils.GetPath(pp);
                    }                                    
                    if (CoreApplicationManager.Application is ICoreEnvironmentManager)
                    {
                        return (CoreApplicationManager.Application as ICoreEnvironmentManager).GetEnvironmentValue(p);
                    }
                    break;
            }
            return p;
        }
    }
}
