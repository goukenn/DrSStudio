using IGK.ICore.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.WebSdk.Setting
{
    [CoreAppSetting(Name="WebSdkSetting")]
    class CoreWebSdkInstallerSetting : CoreSettingBase
    {
        private static CoreWebSdkInstallerSetting sm_instance;


        [CoreSettingDefaultValue (false)]
        public bool Installed
        {
            get { return (bool)this["Installed"].Value; }
            set
            {
                this["Installed"].Value  = value;
            }
        }
        private CoreWebSdkInstallerSetting()
        {
        }

        public static CoreWebSdkInstallerSetting Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static CoreWebSdkInstallerSetting()
        {
            sm_instance = new CoreWebSdkInstallerSetting();

        }
        protected override void Load(ICoreSetting setting)
        {
            base.Load(setting);
        }
        protected override bool LoadDummyChildSetting(KeyValuePair<string, ICoreSettingValue> d)
        {
            return base.LoadDummyChildSetting(d);
        }

      
    }
}
