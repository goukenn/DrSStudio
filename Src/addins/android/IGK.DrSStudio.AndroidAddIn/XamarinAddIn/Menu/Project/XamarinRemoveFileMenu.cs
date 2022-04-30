using IGK.ICore.WinCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Xamarin.Menu.Project
{
    [XamarinMenuAttributte("Project.RemoveFile", 0x13)]
    class XamarinRemoveFileMenu : XamarinMenuBase 
    {
        protected override bool PerformAction()
        {            
            this.CurrentSurface.Project.RemoveFile(this.Params as string);
            return false;
        }
    }
}
