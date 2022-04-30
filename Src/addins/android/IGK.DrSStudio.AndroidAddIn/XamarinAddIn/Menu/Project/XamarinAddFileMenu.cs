using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Xamarin.Menu.Project
{
    [XamarinMenuAttributte("Project.AddFileMenu", 0x10)]
    class XamarinAddFileMenu : XamarinMenuBase 
    {
        protected override bool PerformAction()
        {
            return false;
        }
    }
}
