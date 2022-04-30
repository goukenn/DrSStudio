

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
﻿using IGK.ICore.WinCore;
using IGK.DrSStudio.Presentation.WinUI;
using IGK.ICore;
using IGK.ICore.Threading;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.Presentation
{
    public static class Program
    {
        /*
         * entry point
         * expected parameter. filename
         * 
         * */
        [STAThread()]
        public static void Main(params string[] args) 
        {

#if DEBUG
            args =  new string[]{ "Presentation.gkds"};
#endif
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
             
                CoreSystem.Init();
                CoreThreadManager.InitCurrentThread();
                var frm = new PresentationForm();
                //open file
                if ((args != null) && (args.Length >= 1))
                {
                    string file = args[0];
                    if (File.Exists(file))
                    {
                        frm.PresentationDocument =
                            PresentationDocument.Open(file);
                    }
                }
                Application.Run(frm);
            }
            catch (Exception ex)
            {
                CoreMessageBox.Show(
                    PresentationConstant.ERR_APPLAUNCHFAILED.R("\n" + ex.Message + "\n" + ex.StackTrace),
                    PresentationConstant.ERR_TITLE.R()
                    );
            }
        }
        /// <summary>
        /// used in drsstudio context. to run a presentation form
        /// </summary>
        public static void InitRun()
        { 
        }
    }
}
