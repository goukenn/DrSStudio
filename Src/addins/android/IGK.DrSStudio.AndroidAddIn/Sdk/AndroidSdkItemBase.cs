using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Sdk
{
    public class AndroidSdkItemBase : IAndroidSdkItem
    {


        public AndroidTargetInfo Target { get; set; }
        public string Name
        {
            get;
            set;
        }
        internal AndroidSdkItemBase(AndroidTargetInfo target)
        {
            this.Target = target;
        }
    }
}
