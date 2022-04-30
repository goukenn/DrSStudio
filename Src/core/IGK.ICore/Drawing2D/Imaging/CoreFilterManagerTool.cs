

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreFilterManagerTool.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore;using IGK.ICore.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Drawing2D.Imaging
{
    [CoreTools("Tool.ImagingFilterManager")]
    class CoreFilterManagerTool : CoreToolBase
    {
        private static CoreFilterManagerTool sm_instance;
        private string[] m_filterList;
        private CoreFilterManagerTool()
        {
        }

        public static CoreFilterManagerTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static CoreFilterManagerTool()
        {
            sm_instance = new CoreFilterManagerTool();
        }
        protected override void GenerateHostedControl()
        {
            //
            try
            {
                this.m_filterList = CoreFilterManager.GetFilterList();
            }
            catch { 
                CoreLog.WriteLine ("Filter List ");
            }
        }
    }
}
