

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AddInSetting.cs
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
file:AddInSetting.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.DrSStudio.WinUI.Settings
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Settings;
    
    using IGK.ICore.Settings;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinUI.Configuration;

    [CoreAppSetting(
        Name = "Core.AddInInfoSetting",        
        Index=500 )]
    sealed class AddInSetting 
        : CoreSettingBase 
    {
        private static AddInSetting sm_instance;
        private AddInSetting()
        {
        }
        public static AddInSetting Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static AddInSetting()
        {
            sm_instance = new AddInSetting();
        }
        public override enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.CustomControl;
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            return base.GetParameters(parameters);
        }
        public override  ICoreControl GetConfigControl()
        {
            return new XUIAddInConfig();
        }
    }
}

