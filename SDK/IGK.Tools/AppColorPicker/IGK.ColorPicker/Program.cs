using IGK.ICore;
using IGK.ICore.WinCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.ColorPicker
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CoreSystem.Init();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ColorPickerForm());
        }
    }
    [CoreApplication()]
    class AppProgram : IGK.DrSStudio.WinCoreApplication
    {
       
    }
}
