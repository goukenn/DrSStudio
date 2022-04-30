using IGK.ICore.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.Net.Settings
{
    [PhpSettingAttribute("PhpServerSetting")]
    public sealed class PhpServerSetting : CoreSettingBase
    {
        private static PhpServerSetting sm_instance;
        private PhpServerSetting()
        {
        }

        public static PhpServerSetting Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static PhpServerSetting()
        {
            sm_instance = new PhpServerSetting();

        }

#if DEBUG
       [CoreSettingDefaultValue(@"D:\wamp\bin\php\php5.4.12")]
#else
        [CoreSettingDefaultValue("%startup%/Lib/php")]
#endif
        public string PlateformSDKFolder
        {
            get { return (string)this["PlateformSDKFolder"].Value; }
            set
            {
                this["PlateformSDKFolder"].Value  = value;
            }
        }
    }
}
