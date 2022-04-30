using IGK.ICore.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Xamarin.Settings
{
    public sealed class XamarinSettingAttribute :  CoreSettingAttribute
    {
        public override string GroupName
        {
            get { return XamarinConstant.SETTING_GROUP_NAME; }
        }
        public override int GroupIndex
        {
            get { return 0x30; }
        }
        public XamarinSettingAttribute(string name):base(name)
        {
            
        }

       
    }
}
