

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidManifestDecoder.cs
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
    [CoreCodec("android-application-manifest", "android/application-manifest",
        "xml")]
    class AndroidManifestDecoder : CoreDecoderBase 
    {
        /// <summary>
        /// open 
        /// </summary>
        /// <param name="bench"></param>
        /// <param name="filename"></param>
        /// <param name="selectCreatedSurface"></param>
        /// <returns></returns>
        public override bool Open(ICoreWorkbench bench, string filename, bool selectCreatedSurface)
        {
                AndroidManifest c = AndroidManifest.LoadManifest(filename);
                if ((c != null) && (c.IsValid))
                {
                    var s = new AndroidManifestEditorSurface();
                    s.SetUp(c, filename);
                    bench.AddSurface(s, selectCreatedSurface );
                    return true;
                }
                return false;
        }
    }
}
