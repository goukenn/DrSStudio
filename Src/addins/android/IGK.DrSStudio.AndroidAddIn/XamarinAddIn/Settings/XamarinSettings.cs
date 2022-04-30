using IGK.ICore.Settings;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Xamarin.Settings
{
    [XamarinSettingAttribute("XamarinSettings")]
    class XamarinSettings : CoreSettingBase
    {
        private static XamarinSettings sm_instance;
        
        private XamarinSettings()
        {
            
        }

        public static XamarinSettings Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static XamarinSettings()
        {
            sm_instance = new XamarinSettings();
        }
         [CoreSettingDefaultValue("IGKApp")]
        public string AppDefaultTitle
        {
            get
            {
                return (string)this["AppDefaultTitle"].Value;
            }
            set
            {
                this["AppDefaultTitle"].Value = value;
            }
        }

        [CoreSettingDefaultValue ("IGK")]
        public string AppDefaultPrefix
        {
            get
            {
                return (string)this["AppDefaultPrefix"].Value;
            }
            set
            {
                this["AppDefaultPrefix"].Value = value;
            }
        }
        [CoreSettingDefaultValue("IGK.Android")]
        public string AppDefaultNameSpace
        {
            get
            {
                return (string)this["AppDefaultNameSpace"].Value;
            }
            set
            {
                this["AppDefaultNameSpace"].Value = value;
            }
        }

        public override enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }

         [CoreSettingDefaultValue("v4.0.3")]
        public string DefaultTargetFrameWork
        {
            get
            {
                return (string)this["DefaultTargetFrameWork"].Value;
            }
            set
            {
                this["DefaultTargetFrameWork"].Value = value;
            }
        }

        [CoreSettingDefaultValue("%progfile%/MSBuild/Xamarin", ConfigType = enuSettingConfig.PickFolder)]
         public string XamarinMSBUILDFolder
         {
             get
             {
                 return (string)this["XamarinMSBUILDFolder"].Value;
             }
             set
             {
                 this["XamarinMSBUILDFolder"].Value = value;
             }
         }
    }
}
