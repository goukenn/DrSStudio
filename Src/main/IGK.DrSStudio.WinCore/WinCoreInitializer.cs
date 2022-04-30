using IGK.ICore.WinCore;
using IGK.DrSStudio.WinUI;
using IGK.ICore;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Registrable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IGK.ICore.WinCore.WinUI.Controls;

namespace IGK.DrSStudio
{
    [CoreAddInInitializerAttribute(true)]
    class WinCoreInitializer : ICoreInitializer
    {
        bool ICoreInitializer.Initialize(CoreApplicationContext context)
        {
           // CoreControlFactory.RegisterControl(typeof(IWebBrowserControl).Name, typeof(IGKXWebBrowserControl));
            return true;
        }

        bool ICoreInitializer.UnInitilize(CoreApplicationContext context)
        {
            return false; 
        }
    }
}
