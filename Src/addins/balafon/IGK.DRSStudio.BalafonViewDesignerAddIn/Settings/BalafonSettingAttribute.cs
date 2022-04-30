using IGK.ICore.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.BalafonDesigner.Settings
{

    internal class BalafonSettingAttribute : CoreAttributeSettingBase
    {
        public override string GroupName => "BalafonViewSetting";
        public override int GroupIndex => 0x1;
        ///<summary>
        ///public .ctr
        ///</summary>
        public BalafonSettingAttribute(string name):base(name)
        {

        }
    }
}
