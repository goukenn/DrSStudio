

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: MP3AudioBuilderCodec.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:MP3AudioBuilderCodec.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace IGK.DrSStudio.AudioBuilder.Codec
{
    using IGK.ICore;using IGK.DrSStudio.Codec;
    using IGK.DrSStudio.AudioBuilder.WinUI;
    [CoreCodec ("AudioBuilder", "gkds/audio-file", "mp3;wav;ogg" , Category = "AudioFile")]
    /// <summary>
    /// used only to open file
    /// </summary>
    class MP3AudioBuilderCodec :  IGK.DrSStudio.Codec.CoreDecoderBase 
    {
        public override bool Open(IGK.DrSStudio.WinUI.ICoreWorkbench bench, string filename)
        {
            if (File.Exists(filename))
            {
                XAudioBuilderSurface s = new XAudioBuilderSurface();
                s.OpenFile(filename);
                bench.Surfaces.Add(s);
            }
            return false;
        }
    }
}

