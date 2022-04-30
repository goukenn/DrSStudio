

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreWorkingApplicationSetting.cs
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
file:CoreWorkingApplicationSetting.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
namespace IGK.ICore
{
    using IGK.ICore;
    using IGK.ICore.WinUI.Configuration;
    using IGK.ICore.WinUI;
    /// <summary>
    /// represent the default class setting
    /// </summary>
    public sealed class CoreWorkingApplicationSetting : 
        ICoreWorkingConfigurableObject
    {
        private static CoreWorkingApplicationSetting sm_instance;
        private CoreWorkingApplicationSetting()
        {
        }
        public static CoreWorkingApplicationSetting Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static CoreWorkingApplicationSetting()
        {
            sm_instance = new CoreWorkingApplicationSetting();
        }
        #region ICoreWorkingConfigurableObject Membres
        public enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.CustomControl;
        }
        public ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            return null;
        }
        public  ICoreControl GetConfigControl()
        {
            Type p = CoreSystem.GetWorkingObjectType(CoreConstant.GUI_APP_SETTINGCONTROL );
            if (p == null)
            {
                throw new CoreException(CoreConstant.GUI_APP_SETTINGCONTROL +" Not found");
            }
            ConstructorInfo ctr = p.GetConstructor(new Type[] { typeof (ICoreWorkingConfigurableObject) });
            return ctr.Invoke (new object[]{this}) as ICoreControl;
        }
        #endregion
        #region ICoreIdentifier Membres
        public string Id
        {
            get { return "Setting"; }
        }
        #endregion
        //save the application setting
        public void SaveSetting()
        {
            CoreSystem.GetSettings().SaveSetting();   
        }
    }
}

