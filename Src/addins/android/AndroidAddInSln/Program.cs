using IGK.ICore.WinCore;
using IGK.DrSStudio;
using IGK.DrSStudio.Android.Settings;
using IGK.ICore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGK.ICore.WinUI;

[assembly: CoreAddIn()]

namespace AndroidAddInSln
{
    

    [CoreApplication(Title="Demo")]
    class App : IGK.ICore.WinCore.WinCoreDefaultApp 
    {
        public override ICoreWorkbench CreateNewWorkbench()
        {
            return new CoreSingleWorkbench();
        }
    }
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CoreSystem.Init();
            AndroidSetting.Instance.PlatformSDK = "d:\\Android\\sdk";
       
       //    Application.Run(new AttributeViewDemoForm());
            //Application.Run(new AndroidPlatformExplorerForm());

            Application.Run(new AndroidMenuBuilderToolForm());
        }
    }
}
