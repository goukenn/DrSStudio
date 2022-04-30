

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PinceauSetting.cs
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
file:PinceauSetting.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Settings
{
    [CoreAppSetting(Name = "PiceauToolSetting")]
    class PinceauSetting : IGK.DrSStudio.Settings .CoreSettingBase 
    {
        [CoreSettingDefaultValue ("%startup%")]
        public string CurrentDirectory {
            get {
                return this["CurrentDirectory"].Value as string ;
            }
            set {
                this["CurrentDirectory"].Value = value;
            }
        }
        private static PinceauSetting sm_instance;
        private PinceauSetting()
        {
        }
        public static PinceauSetting Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static PinceauSetting()
        {
            sm_instance = new PinceauSetting();
        }
    }
}

