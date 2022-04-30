using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Xamarin.Menu.Build
{
    [CoreMenu(XamarinConstant.MENU_BUILD+".Generate", 0x01)]
    public class XamarinGenerateMenu : XamarinMenuBase 
    {
        protected override bool PerformAction()
        {
            return base.PerformAction();
        }   
    }
}
