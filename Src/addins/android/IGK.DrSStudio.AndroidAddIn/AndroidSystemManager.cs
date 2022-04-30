using IGK.ICore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using IGK.ICore.IO;
using IGK.DrSStudio.Android.Settings;
using IGK.ICore.Threading;
using IGK.DrSStudio.Android.AndroidThemeBuilder;
using IGK.DrSStudio.Android.Entities;
using IGK.DrSStudio.Android.JAVA;

namespace IGK.DrSStudio.Android
{
    public class AndroidSystemManager
    {
        private static Dictionary<string, AndroidTargetInfo> sm_targetDics;
        private static bool sm_init;
        public static string SDK {
            get {
                return AndroidSetting.Instance.PlatformSDK;
            }
        }
  
        static AndroidSystemManager() {
            sm_init = false;
            sm_targetDics = new Dictionary<string, AndroidTargetInfo>();
        }
        internal static void SetUpEnvrionment(ProcessStartInfo v_info)
        {
            v_info.EnvironmentVariables.Clear();

            v_info.EnvironmentVariables.Add("SystemRoot", Environment.GetEnvironmentVariable("SystemRoot"));
            v_info.EnvironmentVariables.Add("JAVA_HOME", AndroidSetting.Instance.JavaSDK);
            v_info.EnvironmentVariables.Add("ANT_HOME", AndroidSetting.Instance.AntSDK);
            v_info.EnvironmentVariables.Add("ANDROID_SDK", AndroidSetting.Instance.PlatformSDK);
            v_info.EnvironmentVariables.Add("ANDROID_DEFAULT_PLATFORM", AndroidSetting.Instance.DefaultPlatform);

            v_info.EnvironmentVariables.Add("Path",
                Path.GetFullPath(AndroidSetting.Instance.JavaSDK + "/Bin;") +
                Path.GetFullPath(AndroidSetting.Instance.AntSDK + "/Bin;") +
                Path.GetFullPath(AndroidSetting.Instance.PlatformSDK));
        }
        /// <summary>
        /// return and array of installed android target info
        /// </summary>
        /// <returns></returns>
        public static AndroidTargetInfo[] GetAndroidTargets()
        {
            if ((sm_targetDics != null) && sm_init)
            {
                return sm_targetDics.Values.ToArray<AndroidTargetInfo>();
            }   
            string v_commandList = string.Format("list target");
            string v_file = Path.Combine(AndroidSetting.Instance.PlatformSDK, AndroidConstant.TOOLS_ANDROID_PATH);
            if (!File.Exists(v_file))
                return null;

            Process p = new Process();
            ProcessStartInfo v_info = new ProcessStartInfo()
            {
                WorkingDirectory = System.IO.Path.GetDirectoryName(v_file),
                FileName = v_file,
                Arguments = v_commandList,
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };
            string dir = System.IO.Path.GetFullPath(System.IO.Path.GetDirectoryName(v_file) + "/lib/x86");
            v_info.EnvironmentVariables.Add("ANDROID_SWT", dir);
            if (!v_info.EnvironmentVariables.ContainsKey("JAVA_HOME"))
                v_info.EnvironmentVariables.Add("JAVA_HOME", AndroidSetting.Instance.JavaSDK);
            else {
               var  t = v_info.EnvironmentVariables["JAVA_HOME"];
                v_info.EnvironmentVariables["JAVA_HOME"] = AndroidSetting.Instance.JavaSDK;
            }
            p.StartInfo = v_info;
            
            p.Start();
            ////wait for 10 second
            //StreamReader rs = p.StandardOutput;
            //string vh = string.Empty;
            //while (!p.HasExited)
            //{
            //    vh += rs.ReadToEnd();


            //}
            //vh += rs.ReadToEnd();
            //rs.Close();
            p.WaitForExit(10000);
            if (!p.HasExited)
            {
                p.Kill();
                CoreLog.WriteDebug("process killed ..");
                return null;
            }
          

            string h = AndroidUtils.GetString(p.StandardOutput);
            string k = AndroidUtils.GetString(p.StandardError );
            if (!string.IsNullOrEmpty(k))
            {
                CoreLog.WriteError("Error : " + k);
                return null;
            }
            if (p.ExitCode != 0)
            {
                return null;
            }
          //  string r = AndroidUtils.GetString(p.StandardInput);
            List<AndroidTargetInfo> target = new List<AndroidTargetInfo>();
            if (!string.IsNullOrEmpty(h))
            {
                //load android target info
                string[] tab = h.Split('\n');
                //load target info
                Regex rg = new Regex(@"id:\s*(?<id>([0-9]+))\s*or\s*""(?<name>(.)+)""");
                AndroidTargetInfo v_t = null;
                string v_name = string.Empty;
                for (int i = 2; i < tab.Length; i++)
                {
                    if (tab[i].StartsWith("id:"))
                    {

                        MatchCollection m = rg.Matches(tab[i]);
                        if (m.Count == 1)
                        {
                            v_t = new AndroidTargetInfo();
                            v_t.TargetId = Convert.ToInt32(m[0].Groups["id"].Value);
                            v_t.TargetName = m[0].Groups["name"].Value;
                            target.Add(v_t);
                        }

                    }
                    else
                    {
                        //add additional info
                        if (v_t != null)
                        {
                            var v_ll = tab[i].Split(':');
                            if (v_ll.Length > 1)
                            {
                                v_name = v_ll[0].Trim();
                            }
                            else
                            {
                                if (v_ll[0] == "----------")
                                {
                                    v_t = null;
                                    v_name = null;

                                }
                            }
                            if (string.IsNullOrEmpty(v_name) == false)
                                v_t[v_name] = v_t[v_name] + tab[i].Substring(tab[i].IndexOf(':', 0) + 1);
                        }
                    }
                }
            }
            // p.Kill();
            return target.ToArray();
        }

        /// <summary>
        /// load environment
        /// </summary>
        /// <param name="listener"></param>
        public static void LoadEnvironment(AndroidEnvironmentLoadingingCompleteCallBack listener)
        {
            var th = CoreThreadManager.CreateThread(() => {
                _CachesSystemLib();
                listener?.Invoke();
            },
            AndroidConstant.ENVIRONMENT_THREAD);

            th.SetApartmentState(System.Threading.ApartmentState.STA);
            th.IsBackground = false;
            th.Start();
        }
        /// <summary>
        /// shortcut to AndroidTheme managemenet GetAndroidThemes
        /// </summary>
        /// <param name="androidTargetInfo"></param>
        /// <returns></returns>
        //public static AndroidTheme[] GetAndroidThemes(AndroidTargetInfo androidTargetInfo)
        //{
        //    if (androidTargetInfo!=null)
        //        return AndroidTheme.GetAndroidThemes(androidTargetInfo.TargetName);
        //    return null;
        
        //}
        /// <summary>
        /// get android target by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static AndroidTargetInfo GetAndroidTargetByName(string name)
        {
            if (sm_targetDics.ContainsKey(name))
            {
                return sm_targetDics[name];
            }
            return null;
        }
#if DEBUG
        public 
#else 
        private
#endif 
        static void _CachesSystemLib()
        {
            var t = GetAndroidTargets();
            sm_targetDics.Clear();
            if (t != null)
            {
                foreach (var item in t)
                {
                    sm_targetDics.Add(item.TargetName, item);
                }
            }
            sm_init = true;
        }


        public static AndroidAttrs[] GetAndroidProperties(AndroidTheme androidTheme)
        {
            if (androidTheme == null) return null;

            List<AndroidAttrs> c = new List<AndroidAttrs>();

            return c.ToArray();   
        }
        public static string[] GetInfo(AndroidTargetInfo target, string file)
        {
            if (target == null)
                return null;
            string v_dir = Path.GetFullPath(Path.Combine(SDK, "platforms", target.GetAPIName() + "/data"));
            string v_file = Path.Combine(v_dir, file);
            if (File.Exists(v_file))
            {
                return File.ReadAllText(v_file).Split(
                    new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            }
            return null;
        }
        public static JAVA.JAVAClass[] GetWidgets(AndroidTargetInfo androidTargetInfo)
        {
            if (androidTargetInfo == null)
                return null;
            string[] t = GetInfo(androidTargetInfo, AndroidConstant.ANDROID_WIDGETS_FILE);
            if (t != null)
            {
                List<JAVAClass> rList = new List<JAVAClass>();
                foreach (string i in t)
                {
                    string[] e = i.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (e.Length > 0)
                    {
                        string n = e[0];
                        if (char.IsUpper(n[0]))
                        {
                            n = n.Substring(1);
                        }
                        JAVAClass cl = JAVAClass.GetClass(n);
                        rList.Add(cl);
                        for (int f = 1; f < e.Length; f++)
                        {
                            cl.Parent = JAVAClass.GetClass(e[f]);
                            cl = cl.Parent;
                        }
                    }
                }
                return rList.ToArray();
            }
            return null;
        }
        [Obsolete("used get last GetLastAndroidTargets")]
        /// <summary>
        /// return the height level android target info or null
        /// </summary>
        /// <returns></returns>
        public  static AndroidTargetInfo GetHeightTargetLevel()
        {
            AndroidTargetInfo c = null;
            AndroidTargetInfo[] t = GetAndroidTargets();
            if ((t != null) && (t.Length > 0))
            {
                c = t[0];
                for (int i = 1; i < t.Length; i++)
                {
                    if (c.TargetId <= t[i].TargetId)
                        c = t[i];
                }
            }
            return c;
        }
        public static AndroidTargetInfo  GetLastAndroidTargets()
        {
            var  c = GetAndroidTargets();
            if (c == null)
                return null;
            int i = 0;
            AndroidTargetInfo g = null;
            foreach (var item in c)
            {
                if ((item.TargetId > 0) || (i < item.TargetId))
                {
                    g = item;
                    i = item.TargetId;
                }


            } return g;

        }
        public static string GetLastAndroidTargetsName()
        {
            var b = GetLastAndroidTargets();
            if (b != null)
                return b.TargetName;
            return null;
        }
        public static string[] GetManisfestSetting() {
            List<string> v = new List<string>();
            return v.ToArray();
        }
    }
}
