

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: StartDocumentManagerTool.cs
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
file:StartDocumentManagerTool.cs
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
using System.Text;
using Microsoft.Win32;
namespace IGK.DrSStudio.WinLauncher.Tools
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Drawing2D.WinUI;
    using IGK.ICore.WinUI;
    using IGK.ICore.Tools;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
    using System.Diagnostics;
    using IGK.DrSStudio.Tools;
    using IGK.ICore.WinCore.Tools;
    [CoreTools("Tool.StartDocumentManager")]
    public  class StartDocumentManagerTool : DrSStudioToolBase
    {
        private static StartDocumentManagerTool sm_instance;
     
      
        public new ICoreSurfaceManagerWorkbench Workbench {
            get {
                return base.Workbench as ICoreSurfaceManagerWorkbench;
            }
        }
        private StartDocumentManagerTool()
        {
        }
        public static StartDocumentManagerTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static StartDocumentManagerTool()
        {
            sm_instance = new StartDocumentManagerTool();

           // WinCoreService.RegisterIE11WebService ();
            Application.ApplicationExit += Application_ApplicationExit;
        }
        static void Application_ApplicationExit(object sender, EventArgs e)
        {
           // WinCoreService.UnRegisterIE11WebService();
            Application.ApplicationExit -= Application_ApplicationExit;
        }
        internal void Refresh()
        {

        }
    }
}

