

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PhpServerSetting.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.Settings;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Net.Settings
{
    [CoreNetSetting("PhpServer")]
    public sealed class PhpServerSetting : CoreSettingBase
    {
        private static PhpServerSetting sm_instance;

        public override enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }

        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            parameters =  base.GetParameters(parameters);
            var group = parameters.AddGroup("Default");
            group.AddItem(this.GetType().GetProperty("TargetPath"));
            group.AddItem(this.GetType().GetProperty("DocumentRoot"));
            return parameters;
        }

        private PhpServerSetting()
        {
#if DEBUG
            this.Add("TargetPath", @"D:\wamp\bin\php\php5.4.12", null);
            this.Add("DocumentRoot", @"%startup%/www", null);
#else
            this.Add("TargetPath", @"c:\php", null);
            this.Add("DocumentRoot", @"%startup%/www", null);
#endif
        }

        protected override void InitDefaultProperty(PropertyInfo prInfo, CoreSettingDefaultValueAttribute attrib)
        {
            base.InitDefaultProperty(prInfo, attrib);

        }
        public string TargetPath
        {
            get { return (string)this["TargetPath"].Value; }
            set
            {
                this["TargetPath"].Value  = value;
            }
        }

        public string DocumentRoot
        {
            get { return (string)this["DocumentRoot"].Value; }
            set
            {
                this["DocumentRoot"].Value = value;
            }
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
    }
}
