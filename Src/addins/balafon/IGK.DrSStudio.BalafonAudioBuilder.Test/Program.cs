using IGK.ICore;
using IGK.ICore.WinCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IGK.DRSStudio.BalafonAudioBuilder.Test
{
    [CoreApplication()]
    class DemoApp : WinCoreApplication {
        public override void Initialize()
        {
            base.Initialize();
            WinCoreService.RegisterIEWebService (WinCoreService.IE_EDGE);// RegisterIE11WebService();
        }
    }

    class Program
    {
        [STAThread()]
        static void Main(string[] args)
        {
            Console.WriteLine ("Audio Builder test ");
            CoreSystem.Init();
            var th = new Thread(()=>
            {
                Console.WriteLine(AudioBuilderManager.Components());
            });
            th.SetApartmentState (ApartmentState.STA );
            th.IsBackground = false ;
            th.Start();
            Console.ReadLine ();
        }
    }
}
