

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: MySQLServerSettings.cs
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
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Net.Settings
{
       [CoreNetSetting("MySQLServer")]
    public sealed class MySQLServerSettings :CoreSettingBase
    {
        private static MySQLServerSettings sm_instance;
        private MySQLServerSettings()
        {
#if DEBUG
            this.Add("TargetPlatformDir", @"c:\wamp\bin\mysql\mysql5.6.12", null);
            this.Add("User", "root", null);
            
#else            
            this.Add("TargetPlatformDir", @"%startup%\bin\mysql\", null);
            this.Add("User", "root", null);
#endif
        }

        public static MySQLServerSettings Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static MySQLServerSettings()
        {
            sm_instance = new MySQLServerSettings();

        }

        public override ICoreControl GetConfigControl()
        {
            return null;
        }
        public override enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }

        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            var g = parameters.AddGroup("settings");
            g.AddItem(GetType().GetProperty("TargetPlatformDir"));
            
            return parameters;
        }

           /// <summary>
           /// get the target platform directory
           /// </summary>
        public string TargetPlatformDir { get {
            return (string)this["TargetPlatformDir"].Value ;
        }
            set{
            this["TargetPlatformDir"].Value = value ;
        }}
        /// <summary>
        /// get the target platform directory
        /// </summary>
        public string User
        {
            get
            {
                return (string)this["User"].Value;
            }
            set
            {
                this["User"].Value = value;
            }
        }
    }
}
