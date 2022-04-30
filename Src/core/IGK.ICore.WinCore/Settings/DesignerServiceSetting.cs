using IGK.ICore.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.WinCore.Settings
{
    [CoreAppSetting(Name ="DefaultDesignerTool")]
    class DesignerServiceSetting : CoreSettingBase
    {
        private static DesignerServiceSetting sm_instance;
        private DesignerServiceSetting()
        {
        }

        public static DesignerServiceSetting Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static DesignerServiceSetting()
        {
            sm_instance = new DesignerServiceSetting();
        }
        
        public bool AlwayShowDialog
        {
            get { return (bool)(this[nameof(AlwayShowDialog)].Value is bool t); }
            set
            {
                this[nameof(AlwayShowDialog)].Value = value;
            }
        }
    }
}
