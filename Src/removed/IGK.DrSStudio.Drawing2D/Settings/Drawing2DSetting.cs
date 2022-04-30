

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Drawing2DSetting.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:Drawing2DSetting.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D.Settings
{
    using IGK.ICore;using IGK.DrSStudio.Settings;
    using IGK.DrSStudio.WinUI.Configuration;
    [AttributeUsage (AttributeTargets.Class , Inherited = false , AllowMultiple =false )]
    public class Core2DSettingAttribute : CoreSettingAttribute
    {
        public override int GroupIndex
        {
            get { return 0x5; }
        }
        public override string GroupName
        {
            get { return "Drawing2D"; }
        }
    }
    [Core2DSettingAttribute(Name = "Drawing2D.GeneralSetting")]
    /// <summary>
    /// .represent the drawing 2D setting
    /// </summary>
    class Drawing2DSetting : CoreSettingBase  
    {
        private static Drawing2DSetting sm_instance;
        private Drawing2DSetting()
        {
            this.Add("DefaultWidth", 400, null);
            this.Add("DefaultHeight", 300, null);
        }
        public static Drawing2DSetting Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static Drawing2DSetting()
        {
            sm_instance = new Drawing2DSetting();
        }
        void settingChanged(object sender, CoreParameterChangedEventArgs e)
        {
            OnSettingChanged(e);
        }
        public override DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            parameters =  base.GetParameters(parameters);
            IGK.DrSStudio.WinUI.ICoreParameterGroup group = parameters.AddGroup("Settings");
            enuParameterType v_type = enuParameterType.Text;
            foreach (KeyValuePair<string,ICoreApplicationSetting>  item in this)
            {
                group.AddItem(item.Key , item.Key, item.Value.Value , v_type , settingChanged);
            }
            return parameters;
        }
        public override DrSStudio.WinUI.Configuration.enuParamConfigType GetConfigType()
        {
            return DrSStudio.WinUI.Configuration.enuParamConfigType.ParameterConfig;
        }
    }
}

