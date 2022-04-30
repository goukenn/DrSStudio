using IGK.ICore.WinCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Xamarin.Menu.Project
{
    [XamarinMenuAttributte("Project.AddFileFromFolder", 0x12)]
    class XamarinAddFileFromFolderMenu : XamarinMenuBase 
    {
        protected override bool PerformAction()
        {
            string folder = WinCoreService.PickFolder(this.Workbench, Environment.CurrentDirectory);
            if (string.IsNullOrEmpty (folder))
                    return false ;
            

            return false;
        }
    }
}
