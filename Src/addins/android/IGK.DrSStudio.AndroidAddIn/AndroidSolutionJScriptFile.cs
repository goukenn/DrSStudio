

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidSolutionJScriptFile.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android
{
    public class AndroidSolutionJScriptFile : AndroidSolutionFile
    {
        public AndroidSolutionJScriptFile(AndroidProject project, string filename) :base(project,filename)
        {

        }
        public override void Open(ICoreSystemWorkbench bench)
        {
            if (this.Project != null)
            {
                this.Project.Open(bench, this);
            }
            else {
                Process.Start(this.FileName);
            }
        }
        public override string ImageKey
        {
            get
            {
                return AndroidConstant.ANDROID_IMG_JSCRIPTFILE;
            }
        }
    }
}
