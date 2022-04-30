using IGK.CssPropertiesBuilder.WinUI;
using IGK.ICore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.CssPropertiesBuilder
{
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
            Application.Run(new MainForm());
        }
    }
}
