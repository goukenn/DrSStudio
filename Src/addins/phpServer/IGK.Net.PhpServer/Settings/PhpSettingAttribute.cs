using IGK.ICore.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.Net.Settings
{
    public class PhpSettingAttribute : CoreAttributeSettingBase
    {
        public override string GroupName
        {
            get { return "PHPSERVER"; }
        }
        public PhpSettingAttribute(string name):base(name)
        {
            
        }

        public override int GroupIndex
        {
            get { return 0x80;  }
        }
    }
}
