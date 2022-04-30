

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Program.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.DrSStudio.WinUI;
using IGK.ICore;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:Program.cs
*/
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace IGK.DrSStudio
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
            try
            {
                if (!CoreSystemEnvironment.IsInConsoleMode)
                {
                    CoreSystem.Init(typeof(DrSStudioSplashScreen), typeof(DrSStudioMainForm));
                }
                else {
                    CoreSystem.Init();
                    CoreSystem.GetAction("Help.ShowHelp").DoAction();
                }
            }
            catch (Exception ex){
                CoreMessageBox.Show(ex);
                Environment.Exit(-1);
            }
        }
    }
}

