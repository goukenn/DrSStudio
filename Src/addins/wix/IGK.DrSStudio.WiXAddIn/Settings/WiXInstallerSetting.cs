

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WiXInstallerSetting.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
using IGK.ICore.IO;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:WiXInstallerSetting.cs
*/
using IGK.ICore.Settings;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.WiXAddIn.Settings
{
    [CoreAppSetting(Name="WiXInstallerSetting")]
    class WiXInstallerSetting : CoreSettingBase 
    {
        private static WiXInstallerSetting sm_instance;
        public override enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            var group = parameters.AddGroup("Default");
            Type t = this.GetType();
            group.AddItem(t.GetProperty("WiXFolder"));
            return parameters;
        }
        private WiXInstallerSetting()
        {
        }
        public static WiXInstallerSetting Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static WiXInstallerSetting()
        {
            sm_instance = new WiXInstallerSetting();
        }
        [CoreSettingDefaultValueAttribute(WiXConstant.DEFAULT_BIN_INSTALL_FOLDER )]
        /// <summary>
        /// get or set the wix folder
        /// </summary>
        public string WiXFolder
        {
            get { return (string)this["WiXFolder"].Value ; }
            set
            {
                this["WiXFolder"].Value  = value;
            }
        }
        protected override void InitDefaultProperty(System.Reflection.PropertyInfo prInfo, CoreSettingDefaultValueAttribute attrib)
        {
            switch (prInfo.Name.ToLower())
            {
                case "wixfolder":
                    prInfo.SetValue(this, PathUtils.GetPath(attrib.Value.ToString()));
                    break;
                default:
                    break;
            }
            base.InitDefaultProperty(prInfo, attrib);
        }
    }
}

