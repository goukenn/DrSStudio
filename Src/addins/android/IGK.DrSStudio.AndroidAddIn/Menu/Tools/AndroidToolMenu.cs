
using IGK.ICore;using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Menu.Tools
{
    [CoreMenu("Tools.Android", 0)]
    public class AndroidToolBase : CoreApplicationMenu
    {
        protected override bool IsVisible()
        {
            return true ;
        }
        protected override bool IsEnabled()
        {
            return true;
        }
        public AndroidToolBase()
        {

        }
        protected override void InitMenu()
        {
            base.InitMenu();
        }
    }
}
