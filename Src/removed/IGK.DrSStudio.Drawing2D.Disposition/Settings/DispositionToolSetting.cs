

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DispositionToolSetting.cs
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
file:DispositionToolSetting.cs
*/
using IGK.ICore;using IGK.DrSStudio.Drawing2D.Disposition.Tools;
using IGK.DrSStudio.Settings;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D.Disposition.Settings
{
    [CoreAppSetting(Name = "Drawing2D.DispositionToolSetting")]
    class DispositionToolSetting : CoreSettingBase
    {
        private static DispositionToolSetting sm_instance;
        private DispositionToolSetting()
        {
        }
        public static DispositionToolSetting Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static DispositionToolSetting()
        {
            sm_instance = new DispositionToolSetting();
        }
        public override enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            var g = parameters.AddGroup("Disposition");
            g.AddItem("ShowDisposition", "lb.ShowDisposition.caption", DispositionTools.Instance.ShowDisposition, enuParameterType.Bool, (CoreParameterChangedEventHandler)delegate(object sender, CoreParameterChangedEventArgs e)
            {
                DispositionTools.Instance.ShowDisposition = (bool)e.Value;
            });
            return parameters;
        }
    }
}

