using IGK.ICore.WinCore;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.BalafonViewDesigner.Test
{
    static class Program
    {

        //[DllImport(@"urlmon.dll", CharSet = CharSet.Ansi)]
    //    private static extern int UrlMkSetSessionOption(
    //int dwOption,
    //string pBuffer,
    //int dwBufferLength,
    //int dwReserved);

    //    private const int UrlmonOptionUseragent = 0x10000001;

    //    //const string ua = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
    //    const string ua = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.140 Safari/537.36 Edge/17.17134";
    //    private static PrivateFontCollection pc;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            WinCoreService.RegisterIEWebService(WinCoreService.IE_EDGE);//.RegisterIE11WebService();

            //add private font
         //   UrlMkSetSessionOption(0x10000001, ua, ua.Length, 0);


            //string file = @"D:\wamp\www\igkdev\Lib\igk\Ext\ControllerModel\GoogleControllers\Styles\Fonts\KFOkCnqEu92Fr1MmgVxIIzI.ttf";
            //if (pc == null)
            //    pc = new System.Drawing.Text.PrivateFontCollection();
            //pc.AddFontFile(file);


            Application.Run(new TestForm());
        }
    }
}
