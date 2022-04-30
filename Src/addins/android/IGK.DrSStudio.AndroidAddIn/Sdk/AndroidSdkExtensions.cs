
using IGK.DrSStudio.Android.Settings;
using IGK.ICore.IO;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Sdk
{
    public static class AndroidSdkExtensions
    {
        public static CoreXmlElement  LoadAttributes(string platform, string name)
        {
            string path = Path.GetFullPath(Path.Combine(PathUtils.GetPath(AndroidSetting.Instance.PlatformSDK),
    "platforms/" + platform + "/data/res/values/attrs.xml"));
            if (!File.Exists(path))
                return null;
            CoreXmlElement l = CoreXmlElement.LoadFile(path);
            foreach (CoreXmlElement  s in l.getElementsByTagName(AndroidConstant.DECLARE_STYLELABLE_TAG))
            {
                if (s["name"] == name)
                    return s;
            }
            return null;

        }
        public static CoreXmlElement LoadAttributes(this AndroidTargetInfo platform, string name)
        {
            if (platform == null)
                return null ;
            return LoadAttributes(platform.TargetName, name);

        }
    }
}
