using IGK.DrSStudio.Android.Xamarin.Settings;
using IGK.ICore;
using IGK.ICore.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Xamarin.Web
{
    public static class XamarinWebUtils
    {
        static bool sm_xamarinInstalled;

        static XamarinWebUtils() {
            Init();
        }
        public static bool IsXamarinInstalled{
            get{return sm_xamarinInstalled; }
        }
        private static void Init()
        {
            List<string> f = new List<string>();
            string v_dir = PathUtils.GetPath(XamarinSettings.Instance.XamarinMSBUILDFolder);
            if (Directory.Exists(v_dir))
            {
                CoreSystem.Instance.AssemblyLoader.AddSearchDir(v_dir);
                f.Add(Path.Combine(v_dir, "Android/Xamarin.AndroidTools.dll"));
                f.Add(Path.Combine(v_dir, "Android/Xamarin.Android.Build.Tasks.dll"));

                foreach (string item in f)
                {
                    if (File.Exists(item))
                    {
                        Assembly.LoadFile(item);
                    }
                }
                sm_xamarinInstalled = true;
            }
        }


        public static Dictionary<int,string> GetApiLevelFoFrameworkVersion()
        {
            Dictionary<int, string> d = new Dictionary<int, string>();           
            try
            {
                //foreach (var i in global::Xamarin.AndroidTools.AndroidSdk.GetInstalledPlatformVersions())
                //{
                //    d.Add(i.ApiLevel, i.OSVersion);
                //}
            }
            catch (Exception ex)
            {
                CoreLog.WriteLine(ex.Message);
            }
            return d;
        }

        /// <summary>
        /// return an option list of plateform list
        /// </summary>
        /// <param name="androidTargetInfo"></param>
        /// <returns></returns>
        public static string GetPlateformList(AndroidTargetInfo androidTargetInfo)
        {
            StringBuilder sb = new StringBuilder();
            var s = AndroidSystemManager.GetAndroidTargets();
            Dictionary<int, string>  api = GetApiLevelFoFrameworkVersion();
            if (s != null)
            {
                Array.Sort(s, (a, b) =>
                {
                    return a.TargetId.CompareTo(b.TargetId);
                });
                foreach (var item in s)
                {
                    if (item.TargetName.StartsWith("android"))
                    {
                        var v = item.GetVersion();
                        if (api.ContainsKey(item.TargetId) && (v !=null) )
                        {
                            sb.Append(string.Format("<option value=\"{0}\" {1}>{2}</option>", item.TargetName,
                                item == androidTargetInfo ? "selected=\"true\"" : "",
                                v.Name + "( " + v.Number + " )"));
                        }
                    }
                }
            }
            else
            {
                sb.Append(string.Format("<option value=\"-1\" >{0}</option>", AndroidConstant.MSG_NOPLATFORMFOUND.R()));
            }
            return sb.ToString();
        }

        public static string GetTargetFrameworVersion(AndroidTargetInfo g)
        {
            if (g == null)
                return null;
            var tg = GetApiLevelFoFrameworkVersion();
            var i = g.APILevel;
            if (tg.ContainsKey(i))
                return string.Format("v{0}", tg[i]);
            return null;
        }
    }
}
