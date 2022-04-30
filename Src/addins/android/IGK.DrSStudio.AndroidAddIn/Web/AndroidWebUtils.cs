using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Web
{
    using IGK.ICore;
    using IGK.ICore.IO;
    using IGK.ICore.Xml;
    using System.IO;
    using System.Reflection;

    public static class AndroidWebUtils
    {
        static AndroidWebUtils() {
          
        }
       
        /// <summary>
        /// return an option list of plateform list
        /// </summary>
        /// <param name="androidTargetInfo">selected android target name</param>
        /// <returns></returns>
        public  static string GetPlateformList(ref AndroidTargetInfo androidTargetInfo)
        {
            StringBuilder sb = new StringBuilder();
            var s = AndroidSystemManager.GetAndroidTargets();
        
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
                        if (androidTargetInfo == null) {
                            androidTargetInfo = item ;
                        }
                        var v = item.GetVersion();
                        if (v != null)
                        {

                            var inf = AndroidWebUtils.GetSourceProperties(v);
                            sb.Append(string.Format("<option value=\"{0}\" {1}>{2}</option>", item.TargetName,
item == androidTargetInfo ? "selected=\"true\"" : "",
v.Name + "( " + v.Number + " - "+inf?.Desc+" )"));
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


        internal static AndroidSourceProperties GetSourceProperties(AndroidVersion v) {
            string f = Path.Combine(AndroidSystemManager.SDK, "platforms/android-" + v.Number+"/source.properties");
            if (!File.Exists(f))
                return null;

            AndroidSourceProperties prop = new AndroidSourceProperties();
            foreach (string g in File.ReadAllLines(f)) {
                if (string.IsNullOrEmpty(g)) continue;
                string s = g.Trim();


                if (s[0] == '#') continue;//comment

                var t = s.Split('=');
                prop.Add(t[0], s.Substring(s.IndexOf('=') + 1));

            }
            

            return prop;
        }
        internal static string GetThemes(string plateformname)
        {
            string v_tf = string.Format ( AndroidConstant.ANDROID_SDK_THEME_FILE, Path.Combine(AndroidSystemManager.SDK, "platforms", plateformname));
            if (File.Exists(v_tf) == false)
                return string.Empty;

            var e = CoreXmlWebElement.CreateXmlNode("select");
            CoreXmlElement t = CoreXmlElement.LoadFile(v_tf);

            List<string> v_tt = new List<string>();
            foreach (var i in t.getElementsByTagName("style"))
            {
                string n = i["name"];
                v_tt.Add(n);

                
            }
            v_tt.Sort();
            foreach (string item in v_tt)
            {

                var op = e.Add("option");
                op["value"] = item;
                op.Content = item;
            }
            return e.RenderXML(null);
        }
    }
}
