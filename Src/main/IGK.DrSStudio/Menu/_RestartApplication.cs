

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _RestartApplication.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:_RestartApplication.cs
*/
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Reflection;
//namespace IGK.DrSStudio.Menu
//{
//    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Menu;
//    [DrSStudioMenu("File.RestartApplication", 900)]
//    sealed class _RestartApplication : DrSStudioMenuBase
//    {
//        protected override bool PerformAction()
//        {
//            System.Windows.Forms.Application.Exit();
//            System.Threading.Thread th = new System.Threading.Thread(
//                () =>
//                {
//                    System.Diagnostics.Process v_proc = new System.Diagnostics.Process();
//                    System.Diagnostics.ProcessStartInfo v_info = new System.Diagnostics.ProcessStartInfo();
//                    v_info.FileName =
//                        Assembly.GetEntryAssembly().Location;
//                    v_proc.StartInfo = v_info;
//                    v_proc.Start();
//                }
//            );
//            th.IsBackground = true;
//            th.Start();
//            return true;
//        }
//    }
//}

