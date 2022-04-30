

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidProjectFileDecoder.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿

using IGK.ICore;using IGK.DrSStudio.Android.WinUI;
using IGK.ICore.Codec;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Codec
{
    [CoreCodec("android-application", "android-application-solution", AndroidConstant.ANDROID_SLN_FILE_EXTENSION)]
    class AndroidProjectFileDecoder : CoreDecoderBase 
    {
        public override bool Open(ICoreWorkbench bench, string filename, bool selectCreatedSurface)
        {
            AndroidProject p = null;
            p = AndroidProject.OpenProject(filename);
            if (p != null)
            {
                AndroidProjectEditorSurface editor = new AndroidProjectEditorSurface();
                editor.Project = p;
                bench.AddSurface(editor, selectCreatedSurface );
                return true;
            }
            return false;
        }
    }
}
