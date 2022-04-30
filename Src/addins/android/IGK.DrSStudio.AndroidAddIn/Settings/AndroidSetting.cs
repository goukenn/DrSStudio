

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidSetting.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:AndroidSetting.cs
*/

using IGK.ICore;using IGK.ICore.Settings;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Android.Settings
{
    [AndroidSetting(Name = "Android.AndroidGeneral")]
    public sealed class AndroidSetting : CoreSettingBase 
    {
     
        private static readonly AndroidSetting sm_instance;
        /// <summary>
        /// .ctr
        /// </summary>
        private AndroidSetting()
        {
            //init default setting
            this.Add(AndroidConstant.PLATFORM_SDK , AndroidConstant.DEFAULT_INSTALL_FOLDER, null);
            this.Add("AndroidWorkingDirectory", "c:\\android\\workbench", null);
        }

        /// <summary>
        /// get or set the java sdk folder
        /// </summary>
        [CoreConfigurableProperty()]
        public string JavaSDK
        {
            get { return this[nameof(JavaSDK)].Value as string; }
            set
            {
                this[nameof(JavaSDK)].Value = value;
            }
        }
        /// <summary>
        /// get or set the plateform sdk
        /// </summary>
        [CoreConfigurableProperty()]
        public string PlatformSDK
        {
            get { return this[nameof(PlatformSDK)].Value as string; }
            set
            {
                this[nameof(PlatformSDK)].Value = value;
            }
        }


        [CoreConfigurableProperty()]        
        public string DefaultPlatform
        {
            get { return this[nameof(DefaultPlatform)].Value as string;}
            set
            {
                this[nameof(DefaultPlatform)].Value = value;
            }
        }
        /// <summary>
        /// get or set the Ant sdk folder
        /// </summary>
        [CoreConfigurableProperty()]
        public string AntSDK
        {
            get { return this[nameof(AntSDK )].Value as string; }
            set
            {
                this[nameof(AntSDK)].Value = value;
            }
        }
        /// <summary>
        /// get or set the working directory
        /// </summary>
        [CoreConfigurableProperty()]
        public string AndroidWorkingDirectory
        {
            get { return this[nameof(AndroidWorkingDirectory)].Value as string; }
            set
            {
                this[nameof(AndroidWorkingDirectory)].Value = value;
            }
        }

       /// <summary>
       /// get the android setting instance
       /// </summary>
        public static AndroidSetting Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static AndroidSetting()
        {
            sm_instance = new AndroidSetting();
        }
        public override enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        /// <summary>
        /// configuration parameters
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            var group = parameters.AddGroup("Default");
            group.AddItem(this.GetType().GetProperty("PlatformSDK"));
            group.AddItem(this.GetType().GetProperty("AntSDK"));
            group.AddItem(this.GetType().GetProperty("JavaSDK"));
            group.AddItem(this.GetType().GetProperty(nameof(DefaultPlatform)));
            return parameters;
        }
    }
}

