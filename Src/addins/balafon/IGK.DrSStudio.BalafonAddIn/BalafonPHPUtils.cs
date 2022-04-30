using IGK.DrSStudio.Balafon.Setttings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Balafon
{
    /// <summary>
    /// utility class for php utils
    /// </summary>
    class BalafonPHPUtils
    {
        static string PhpExeFile=>  Path.Combine (BalafonSetting.Instance.PhpInstallSDK, "php.exe");
  
        static bool SDKDir => Directory.Exists(BalafonSetting.Instance.PhpInstallSDK) && 
           File.Exists(PhpExeFile);
        internal static string  ExecScript(string script)
        {

            if (!SDKDir) {
             return string.Empty;
            }
            string k = PhpExeFile ;
           return  RunScript(k, script);
        }

        private static string RunScript(string file, string script)
        {
            ProcessStartInfo v_info = new ProcessStartInfo();
            v_info.RedirectStandardOutput = true;
            v_info.UseShellExecute = false;
            v_info.WindowStyle = ProcessWindowStyle.Hidden;
            v_info.WorkingDirectory = Path.GetDirectoryName(file);
            v_info.ErrorDialog = true;
            v_info.CreateNoWindow = true;
            v_info.FileName = file;
            v_info.Arguments = string.Format("-r \"{0}\"", script);
            Process p = new Process();
            p.StartInfo = v_info;
            p.Start();

            BinaryReader bReader = new BinaryReader(p.StandardOutput.BaseStream);
            Byte[] data = new byte[4096];
            int count = 0;

            string v_response = string.Empty;
            while ((count = bReader.Read(data, 0, data.Length)) > 0)
            {
                v_response += ASCIIEncoding.Default.GetString(data, 0, count);
            }
            return v_response;
        }
    }
}
