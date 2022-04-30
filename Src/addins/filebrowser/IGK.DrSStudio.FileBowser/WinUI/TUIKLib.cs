using IGK.Native;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Security.Permissions;


namespace IGK.DrSStudio.FileBrowser.WinUI
{
    class TUIKLib
    {
        public static Icon ShellGetIcon(string f)
        {
            var tab = f.Split(',');
            if (tab.Length >= 2)
            {
                string ff = tab[0];
                return ExtractIcon(ff, (int)Int32.Parse(tab[1]));
            }
            return null;
        }
        private static Icon ExtractIcon(string f, int p)
        {
            var fb = System.Environment.ExpandEnvironmentVariables(f);
            //var s = Registry.ClassesRoot.OpenSubKey (@"Applications\explorer.exe").GetValue("TaskbarGroupIcon", "test", RegistryValueOptions.DoNotExpandEnvironmentNames );
            //  string gf = CommandInvocationIntrinsics.ExpandString(f);
            //var o = new RegistryKey();
            //o.GetValue ("","", RegistryValueOptions.
            if (File.Exists(fb))
            {
                IntPtr h = Kernell32.LoadLibrary(fb);
                IntPtr hh = User32.ExtractIcon(h, fb, p);
                Kernell32.FreeLibrary(h);
                if (hh != IntPtr.Zero)
                    return Icon.FromHandle(hh);

            }
            return null;
        }
        [RegistryPermission(SecurityAction.Demand, Unrestricted = true)]
        internal static Icon GetRegIcon(string p)
        {
            using (var key = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Default))
            {
                using (var dd = key.OpenSubKey(p + "\\DefaultIcon"))
                {
                    if (dd != null)
                    {
                        var d = dd.GetValue(null);// Registry.ClassesRoot.GetValue(null, "ssfaultIcon", RegistryValueOptions.None);//p + "/DefaultIcon", true );
                        if (d != null)
                        {
                            return ShellGetIcon(d.ToString());
                        }
                    }
                }
            }
            return null;
        }

        internal static Icon GetShellExtensionIcon(string ex)
        {
            using (var key = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Default))
            {
                using (var dd = key.OpenSubKey(ex + "\\DefaultIcon"))
                {
                    if (dd != null)
                    {
                        var d = dd.GetValue(null);// Registry.ClassesRoot.GetValue(null, "ssfaultIcon", RegistryValueOptions.None);//p + "/DefaultIcon", true );
                        if (d != null)
                        {
                            return ShellGetIcon(d.ToString());
                        }
                    }
                    else {
                        if (ex.StartsWith("."))
                        {
                            var p = key.OpenSubKey(ex);
                            
                            
                            if (p != null)
                            {
                                var m =  p.GetValue(null);
                                return m != null ? GetRegIcon(m.ToString()) : null;
                            }
                            return GetRegIcon("unknown");
                        }
                    }
                }
            }
            return null; 
        }
    }
}
