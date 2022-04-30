

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidSolutionBinaryFolder.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Android
{
    public sealed class AndroidSolutionBinaryFolder : AndroidSolutionFolder
    {
        public AndroidSolutionBinaryFolder(AndroidProject androidProject):base( androidProject,
            Path.Combine(androidProject.TargetLocation,"bin"))
        {
        }
        public override string ImageKey
        {
            get { return AndroidConstant.ANDROID_IMG_BINARYFOLDER; }
        }
    }
}
