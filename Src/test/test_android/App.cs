using IGK.ICore;
using IGK.ICore.WinCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_android
{
    [CoreApplication(Name="TestApplication")]
    class App : WinCoreApplication
    {
        public override void Initialize()
        {
            base.Initialize();

            WinCoreService.RegisterIE11WebService();
        }
    }
}
