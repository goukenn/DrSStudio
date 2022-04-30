using IGK.DrSStudio.Android.Entities;
using IGK.DrSStudio.Android.Web;
using IGK.ICore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.AndroidThemeBuilder
{
    class ATBInvacationScriptManager
    {
        private AndroidTheme.AndroidThemeCollections m_themes;
        private AndroidTargetInfo m_TargetInfo; //primary target

        /// <summary>
        /// get or set the selected theme
        /// </summary>
        public string Theme { get; set; } // selected theme theme
        public string DefaultParent { get; set; }

        public string GetThemeName() {
            return this.Theme;
        }
        public string GetParentList()
        {
            this.m_themes = null;
            StringBuilder sb = new StringBuilder();
            var s = AndroidTheme.GetAndroidThemes(this.m_TargetInfo?.TargetName);
            if (s != null)
            {
                //s.Sort( (a, b) =>
                //{
                //    return a.Name.CompareTo(b.Name);
                //});
                var g = this.DefaultParent;
                foreach (AndroidTheme item in s)
                {
                    sb.Append(string.Format("<option value=\"{0}\" {1}>{0}</option>", item.Name,
                        (g == item.Name) ? "selected=\"true\" " : ""
                        ));
                }
                this.m_themes = s;
            }
            else
            {
                sb.Append(string.Format("<option value=\"-1\" >No Parent </option>"));
            }
            return sb.ToString();
        }
        public string GetPlateformList()
        {
            string v_sb = AndroidWebUtils.GetPlateformList(ref this.m_TargetInfo);
            if (string.IsNullOrEmpty(v_sb))
            {
                return "<option value='-1'>" + "android.platformlist.failed".R() + "</option>";
            }
            return v_sb;
        }

        public void SetTargetInfo(AndroidTargetInfo item)
        {
            this.m_TargetInfo = item;
        }
    }
}
