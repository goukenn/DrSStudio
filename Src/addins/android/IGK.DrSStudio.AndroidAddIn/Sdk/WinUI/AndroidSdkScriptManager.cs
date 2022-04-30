using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace IGK.DrSStudio.Android.Sdk.WinUI
{
    [ComVisible(true)]
    public class AndroidSdkScriptManager 
    {
        private AndroidSdkManagerSurface androidSdkManagerSurface;
        private Sdk.AndroidSdkTheme theme;


        public AndroidSdkScriptManager(AndroidSdkManagerSurface androidSdkManagerSurface, Sdk.AndroidSdkTheme theme)
        {        
            this.androidSdkManagerSurface = androidSdkManagerSurface;
            this.theme = theme;
        }
        public void open_theme(string name) {
            androidSdkManagerSurface.OpenTheme(name);
        }
    }
}
