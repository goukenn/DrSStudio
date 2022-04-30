using IGK.ICore;
using IGK.ICore.Settings;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.Settings
{
    [CoreAppSettingAttribute("DrSStudioSetting")]
    sealed class DrSStudioSetting : CoreSettingBase
    {
        private static DrSStudioSetting sm_instance;
        public override enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.NoConfig;
        }
        private DrSStudioSetting()
        {
            this.Add(nameof(Location), Vector2i.Zero, null);
            this.Add(nameof(MainFormSize), new  Size2i(800,460), null);
            this.Add(nameof(MainFormState), FormWindowState.Normal, null);
        }

        public static DrSStudioSetting Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static DrSStudioSetting()
        {
            sm_instance = new DrSStudioSetting();
        }
        [CoreSettingDefaultValue("0;0")]
        public Vector2i Location { get {
            return (Vector2i)this[nameof(Location)].Value;
        }
            set {
                this[nameof(Location)].Value  = value;
            }
        }

        [CoreSettingDefaultValue("0;0")]
        public Size2i MainFormSize
        {
            get
            {
                return (Size2i)this[nameof(MainFormSize)].Value;
            }
            set
            {
                this[nameof(MainFormSize)].Value = value;
            }
        }


        [CoreSettingDefaultValue(FormWindowState.Normal)]
        public FormWindowState MainFormState
        {
            get
            {
                return (FormWindowState)this[nameof(MainFormState)].Value;
            }
            set
            {
                this[nameof(MainFormState)].Value = value;
            }
        }
    }
}
