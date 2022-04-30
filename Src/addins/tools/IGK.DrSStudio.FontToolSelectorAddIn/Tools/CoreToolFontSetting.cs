

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreToolFontSetting.cs
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
file:CoreToolFontSetting.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Tools
{
    using IGK.ICore.WinCore;
using IGK.ICore;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.Settings;
    using IGK.ICore.WinUI.Configuration;
    using System.ComponentModel;

    [CoreAppSetting(Name = "Core.FontProperties", Index=20)]
    class CoreToolFontSetting : CoreSettingBase 
    {
        public string DefaultFont {
            get { return (string)this["DefaultFont"].Value; }
            set {
                if (string.IsNullOrEmpty(value)) {
                    return;
                }
                this["DefaultFont"].Value = value;                
            }
        }
       
        [System.ComponentModel.TypeConverter(typeof(CoreArrayStringConverter))]
        public string[] DefaultFontSizes { get { return ((string[])this["DefaultFontSizes"].Value); } }
        private static readonly CoreToolFontSetting sm_instance;
        private CoreToolFontSetting()
        {
            this.Add(nameof(DefaultFont), "FontName:Arial black; Size: 12pt;", null);            
            this.Add(nameof(DefaultFontSizes), CoreConstant.FONT_SIZES.Split(';'), null);
        }
        public static CoreToolFontSetting Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static CoreToolFontSetting()
        {
            sm_instance = new CoreToolFontSetting();
        }
        protected override void InitDefaultProperty(
            System.Reflection.PropertyInfo prInfo, 
            CoreSettingDefaultValueAttribute attrib)
        {
            base.InitDefaultProperty(prInfo, attrib);
        }
        public override enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            var group = parameters.AddGroup("fontProperties");
            //(()=>{DefaultFont}).Name();

            group.AddItem(this.GetType().GetProperty("DefaultFont"));
            group.AddItem("DefaultFontSizes", "lb.DefaultFontSizes",
                string.Join(";", this.DefaultFontSizes), enuParameterType.Text, (o, e) => {
                    if (e.Value == null)
                        this["DefaultFontSizes"].Value = null;
                    else
                        this["DefaultFontSizes"].Value  = e.Value.ToString().Split(';');
                });
            return parameters;
        }
    }
 
}

