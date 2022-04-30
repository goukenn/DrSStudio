using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.Settings;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Theme;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Settings
{
    [CoreAppSetting("DrSStudioSetting.MenuFont")]
    class DrSStudioMenuFontSetting : CoreSettingBase
    {
        const string BASEFONT = "FontName:Tahoma; Size:10pt";

        public static CoreFont MenuBaseFont { get {
                return CoreThemeManager.GetValue<CoreFont>(nameof(MenuBaseFont), BASEFONT);}
        }


        private static DrSStudioMenuFontSetting sm_instance;
        private DrSStudioMenuFontSetting()
        {
   
        }

        public static DrSStudioMenuFontSetting Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static DrSStudioMenuFontSetting()
        {
            sm_instance = new DrSStudioMenuFontSetting();
        }

        protected override void InitDefaultProperty(PropertyInfo prInfo, CoreSettingDefaultValueAttribute attrib)
        {
            base.InitDefaultProperty(prInfo, attrib);
        }
    }
}
