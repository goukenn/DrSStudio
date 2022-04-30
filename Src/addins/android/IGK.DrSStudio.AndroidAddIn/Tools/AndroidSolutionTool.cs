

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidSolutionTool.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Tools
{
    [CoreTools("Tool.Android.Solution")]
    public class AndroidSolutionTool : AndroidToolBase
    {
        private static AndroidSolutionTool sm_instance;
        private AndroidSolutionTool()
        {
        }

        public static AndroidSolutionTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static AndroidSolutionTool()
        {
            sm_instance = new AndroidSolutionTool();

        }
        public void BuildSolution()
        { 
        }
    }
}