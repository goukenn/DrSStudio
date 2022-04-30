using IGK.ICore.Menu;
using IGK.ICore.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Tools.AndroidBuildTool.Menu
{
    class AndroidViewMenuAttribute : CoreViewMenuAttribute
    {
        public AndroidViewMenuAttribute(string name, int index):base(name, index)
        {

        }
        public override string ImageKey
        {
            get
            {
                return base.ImageKey;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    value = CoreResources.GetResourceId(this.GetType().Assembly, value);
                }
                base.ImageKey = value;
            }
        }
    }
}
