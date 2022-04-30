

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreZipReader.cs
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

namespace IGK.ICore.IO
{
    public interface ICoreZipReader
    {
        /// <summary>
        /// extract zip file
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="outFolder"></param>
        /// <returns></returns>
        bool ExtractZipFile(string filename, string outFolder);
        /// <summary>
        /// extract zip stream
        /// </summary>
        /// <param name="inputStream"></param>
        /// <param name="outFolder"></param>
        /// <returns></returns>
        bool ExtractZipStream(Stream inputStream, string outFolder);

        /// <summary>
        /// extract zip data
        /// </summary>
        /// <param name="inputStream"></param>
        /// <param name="outFolder"></param>
        /// <returns></returns>
        bool ExtractZipData(byte[] data, string outFolder, ICoreZipFileExtractListener callback = null);
    }
}
