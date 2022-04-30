

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreCodecCollections.cs
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
file:ICoreCodecCollections.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Codec
{
    public interface ICoreCodecCollections
    {
        ICoreCodec[] GetEncoders();
        ICoreCodec[] GetDecoders();
        ICoreCodec[] GetEncoders(string category);
        ICoreCodec[] GetDecoders(string category);
        ICoreCodec[] GetEncodersByExtension(string ext);
        ICoreCodec[] GetDecodersByExtension(string ext);
        string GetDecoderTypeByExtension(string ext);
        /// <summary>
        /// reguistar extra codec
        /// </summary>
        /// <param name="extenstion"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        bool RegisterDecoder(string extenstion, string path);
        /// <summary>
        /// clear extra codec
        /// </summary>
        void ClearExtraDecoder();
    }
}

