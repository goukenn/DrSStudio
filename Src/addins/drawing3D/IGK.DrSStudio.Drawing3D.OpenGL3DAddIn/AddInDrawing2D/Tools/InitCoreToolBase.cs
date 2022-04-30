

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: InitCoreToolBase.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
﻿using IGK.ICore.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing3D.OpenGL.AddInDrawing2D.Tools
{
    [CoreTools("Tool.InitTool")]
    class InitCoreToolBase : CoreToolBase
    {
        private static InitCoreToolBase sm_instance;
        private InitCoreToolBase()
        {
        }

        public static InitCoreToolBase Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static InitCoreToolBase()
        {
            sm_instance = new InitCoreToolBase();

        }
        protected override void RegisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            base.RegisterBenchEvent(Workbench);
            //var v = new IGKOGL2DDrawingSurface();
            //Workbench.Surfaces.Add(v);
            //Workbench.CurrentSurface = v;
        }
    }
}
