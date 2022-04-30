using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Sdk
{
    public class AndroidSdkTemplateFile : AndroidSdkItemBase
    {
        public string File
        {
            get;
            set;
        }
        public AndroidSdkTemplateFile(AndroidTargetInfo target):base(target )
        {

        }
    }
}
