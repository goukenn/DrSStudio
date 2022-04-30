using IGK.ICore.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.BalafonDesigner.Settings
{

    [BalafonSetting("BalafonViewDesignerSettings")]
    class BalafonViewDesignerSettings : CoreSettingBase
    {
        private static BalafonViewDesignerSettings sm_instance;
        private BalafonViewDesignerSettings()
        {
        }

        public static BalafonViewDesignerSettings Instance
        {
            get
            {
                return sm_instance;
            }
        }

        public string DocumentRoot { get => this.GetValue("DocumentRoot", "D:\\wamp\\wwww\\igkdev") as string; }
        public int DefaultPort { get; internal set; }
        public ICoreSetting DefaulPhpSDKFolder { get; internal set; }

        static BalafonViewDesignerSettings()
        {
            sm_instance = new BalafonViewDesignerSettings();

        }
    }
}
