

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreZipFile.cs
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
using System.Threading.Tasks;

namespace IGK.ICore.IO
{
    public static class CoreZipFile 
    {
        public static bool ExtractZipFile(string filename, string outfolder)
        {
            ICoreZipReader reader = CoreApplicationManager.Application.ResourcesManager.GetZipReader();
            if (reader != null)
            {
                return reader.ExtractZipFile(filename, outfolder);
            }
            return false;
        }
        public static bool ExtractZipData(byte[] data, string outfolder, ICoreZipFileExtractListener callback = null)
        {
            ICoreZipReader reader = CoreApplicationManager.Application.ResourcesManager.GetZipReader();
            if (reader != null)
            {
                return reader.ExtractZipData(data, outfolder, callback);
            }
            return false;
        }
    }
}
