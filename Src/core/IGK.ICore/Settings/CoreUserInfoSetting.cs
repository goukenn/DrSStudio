using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Settings
{
    /// <summary>
    /// Represent the base application setting
    /// </summary>
    [CoreAppSetting(Name = CoreConstant.APP_USER_INFO_SETTING)]
    class CoreUserInfoSetting : CoreSettingBase 
    {
        private static CoreUserInfoSetting sm_instance;
        private CoreUserInfoSetting()
        {

            this.Add("Author", CoreConstant.AUTHOR, null);
        }

        public static CoreUserInfoSetting Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static CoreUserInfoSetting()
        {
            sm_instance = new CoreUserInfoSetting();
        }

    }
}
