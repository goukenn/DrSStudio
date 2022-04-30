

/*
IGKDEV @ 2008 - 2014
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
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:Program.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics ;
using System.Threading ;
namespace IGK.DrSStudio.WinLauncher
{
#pragma warning disable
    static class Program
    {
        /// <summary>
        /// represent a single instance program mutex. for single launch
        /// </summary>
        private static Mutex mutex;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(params string[] args)
        {
            try
            {
                mutex = Mutex.OpenExisting(CoreConstant.MUTEX_CONTEXT);
                //already register Mutex
                CoreSystem.RegisterClientSystem();
                if (CoreSystem.Instance.MainForm != null)
                {
                    MethodInvoker m = delegate()
                    {
                        CoreSystem.Instance.MainForm.Activate();
                        CoreSystem.Instance.MainForm.Show();
                        CoreSystem.Instance.OpenFile(args);  
                    };
                    if(CoreSystem.Instance.MainForm.InvokeRequired )
                        m.BeginInvoke (null,null);
                    else 
                        m();
                }
                return;
            }
            catch (Exception ex)
            { 
                //no mutext 
                mutex = new Mutex(true, CoreConstant.MUTEX_CONTEXT);
            }
#if !DEBUG
            try
            {
#endif
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(true);
                WinLaucherStartForm v_sfrm = new WinLaucherStartForm();
                v_sfrm.BackgroundImage = IGK.DrSStudio.Resources.R.GetSplahScreen();
                v_sfrm.BackgroundImageLayout = ImageLayout.Stretch;
                v_sfrm.Load += v_sfrm_Load;
                Application.Run(v_sfrm); 
                //IGK.DrSStudio.CoreSystem.Init();
                //IGK.DrSStudio.CoreSystem.Instance.Run(
                //    typeof(WinLaucherStartForm),
                //    typeof(WinLaucherMainForm));
#if !DEBUG
            }
            catch(Exception ex) {
                CoreLog.WriteLine(ex.Message);
                MessageBox.Show("Une Erreur est survenu lors du lancement de l'application. Nous sommes désoler de la gène occasionnée","Erreur Inattendu");
            }
#endif
        }
        static void v_sfrm_Load(object sender, EventArgs e)
        {
            Thread t = new Thread(new WinMainFormLauncher(sender as WinLaucherStartForm ).Run);
            t.SetApartmentState(ApartmentState.STA);
            t.IsBackground = false;
            t.Start();
        }
    }
}

