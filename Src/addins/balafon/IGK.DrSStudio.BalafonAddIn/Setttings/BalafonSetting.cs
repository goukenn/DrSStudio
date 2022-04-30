using IGK.ICore.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using IGK.ICore.WinUI.Configuration;
using IGK.ICore.WinUI;

namespace IGK.DrSStudio.Balafon.Setttings
{
    [BalafonSettingAttribute(Name = "Balafon.General")]
    class BalafonSetting : CoreSettingBase
    {
        private static BalafonSetting sm_instance;
        private BalafonSetting()
        {
        }

        public static BalafonSetting Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static BalafonSetting()
        {
            sm_instance = new BalafonSetting();
        }

        [CoreConfigurableProperty()]
        public string PhpInstallSDK
        {
            get
            {
                return (string)(this[nameof(PhpInstallSDK)].Value);
            }
            set
            {
                this[nameof(PhpInstallSDK)].Value = value;
            }
        }
        [CoreConfigurableProperty()]
        public string BalafonInstallSite       {
            get
            {
                return (string)(this[nameof(BalafonInstallSite)].Value);
            }
            set
            {
                this[nameof(BalafonInstallSite)].Value = value;
            }
        }
        protected override void InitDefaultProperty(PropertyInfo prInfo, CoreSettingDefaultValueAttribute attrib)
        {
            base.InitDefaultProperty(prInfo, attrib);
            this.PhpInstallSDK = "c:\\php";
            this.BalafonInstallSite = "c:\\wamp\\www";
        }
        public override enuParamConfigType GetConfigType()
        {
            return  enuParamConfigType.ParameterConfig;
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            var defgroup = parameters.AddGroup ("Default");
            ICoreParameterGroup group = null;
            foreach (var item in
            GetType().GetProperties( BindingFlags.Public  | BindingFlags.Instance)) {
                if ( item.CanRead && item.CanWrite && item.DeclaringType == this.GetType())
                {
                    var gg = CoreConfigurablePropertyAttribute.GetCustomAttribute< CoreConfigurablePropertyAttribute>(item);//.Get
                    if (gg != null)
                    {
                        group = parameters.AddGroup(gg.Group);
                    }
                    else {
                        group=defgroup;
                    }
                    group.AddItem(item);

                }
            }
            return base.GetParameters(parameters);
        }
    }
}
