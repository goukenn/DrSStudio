using IGK.ICore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebGLGameEngineApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CoreSystem.InitWithEntryAssembly(typeof(Program).Assembly);
                
            Application.Run(new MainForm());
        }
    }

}
