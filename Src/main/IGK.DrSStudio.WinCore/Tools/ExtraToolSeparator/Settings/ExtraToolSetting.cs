

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ExtraToolSetting.cs
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
file:ExtraToolSetting.cs
*/
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Tools;
using IGK.DrSStudio.WinUI;

using IGK.ICore.Settings;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Settings
{
    [CoreAppSetting(Name = "ExternalTool")]
    public class CoreExternalToolSetting : CoreSettingBase
    {
        private static CoreExternalToolSetting sm_instance;
        private CoreExternalToolSetting()
        {
            this["CS"].Value = "notepad.exe";
            this["JS"].Value = "c://programfile/notepadpp.exe";
            this["PHP"].Value = "c://programfile/notepadpp.exe";
        }
        public static CoreExternalToolSetting Instance
        {
            get
            {
                return sm_instance;
            }
        }
        protected override bool LoadDummyChildSetting(KeyValuePair<string, ICoreSettingValue> d)
        {
            return base.LoadDummyChildSetting(d);
        }
        protected  override void Load(ICoreSetting setting)
        {
            this.Clear();
            base.Load(setting);
            CoreExternalTools.RegisterExtraCodec();
        }
        static CoreExternalToolSetting()
        {
            sm_instance = new CoreExternalToolSetting();
        }
        public override enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.CustomControl;
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            return parameters;
        }
        public override  ICoreControl GetConfigControl()
        {
            UIXConfigExtraTools ctr = new UIXConfigExtraTools();
            ctr.Size = new System.Drawing.Size(300, 400);
            foreach (KeyValuePair<string, ICoreSettingValue> t in this)
            {
                ctr.LoadProperty(t.Value.Name, t.Value.Value);
            }
            return ctr as ICoreControl;
        }
    }
}

