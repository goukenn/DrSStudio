

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: FtpSettingInfo.cs
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
file:FtpSettingInfo.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.FtpManagerAddIn.Settings
{
    
using IGK.ICore;using IGK.ICore.Settings;
    /// <summary>
    /// represent the setting info
    /// </summary>
    [CoreAppSetting(Name = "FtpSettingManager")]
    sealed class FtpSettingInfo : CoreSettingBase 
    {
        private FtpSettingInfo()
        {
            this.Add("Server", "localhost" , null);
            this.Add("Login", "admin", null);
            this.Add("Password", "123566", null);
        }
        private static FtpSettingInfo sm_instance;
        public static FtpSettingInfo Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static FtpSettingInfo()
        {
            sm_instance = new FtpSettingInfo();
        }
    }
}

