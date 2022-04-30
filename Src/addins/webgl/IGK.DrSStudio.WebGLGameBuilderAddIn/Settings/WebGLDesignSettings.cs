using IGK.ICore;
using IGK.ICore.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinUI.Configuration;
using System.Reflection;

namespace IGK.DrSStudio.WebGLEngine.Settings
{
    [CoreAppSetting("WebGLGameBuilder")]
    class WebGLDesignSettings : IGK.ICore.Settings.CoreSettingBase
    {
        private static WebGLDesignSettings sm_instance;

        //.ctr
        private WebGLDesignSettings()
        {
            //initialize require setting
            this.Add(nameof(SceneBackgroundColor), Colorf.CornflowerBlue, null);
        }

        public static WebGLDesignSettings Instance
        {
            get
            {
                return sm_instance;
            }
        }
        [CoreSettingDefaultValue(nameof(Colorf.CornflowerBlue))]
        public static Colorf SceneBackgroundColor
        {
            get { return (Colorf) sm_instance[nameof(SceneBackgroundColor)]?.Value; }
            set { sm_instance[nameof(SceneBackgroundColor)].Value = value; }
        }

        static WebGLDesignSettings()
        {
            sm_instance = new WebGLDesignSettings();
        }

        protected override void LoadSettingProperty(ICoreParameterGroup g)
        {
            base.LoadSettingProperty(g);
        }
        protected override void InitDefaultProperty(PropertyInfo prInfo, CoreSettingDefaultValueAttribute attrib)
        {
            base.InitDefaultProperty(prInfo, attrib);
            
        }

        

        //-------------------------------------------------------------------------------------------------------
        //configuration
        //-------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            return base.GetParameters(parameters);
        }
        public override enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.NoConfig;
        }
    }
}
