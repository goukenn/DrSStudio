

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: VideoProjectEncoder.cs
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
file:VideoProjectEncoder.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.VideoEditionTableAddIn.Codec
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.VideoEditionTableAddIn.WinUI;
    [IGK.DrSStudio.Codec.CoreCodec(VideoConstant.VIDEOEDITOR_SURFACENAME, 
        VideoConstant.CODEC_MIMETYPE ,
        VideoConstant.CODEC_EXTENSIONS)]
    public sealed class VideoProjectEncoder : IGK.DrSStudio.Codec .CoreEncoderBase
    {
        public override bool Save(IGK.DrSStudio.WinUI.ICoreWorkingSurface surface, string filename, ICoreWorkingDocument[] documents)
        {
            if ((surface is IVideoEditorSurface)==false )
            {
                return false;
            }
            return (surface as IVideoEditorSurface ).SaveProject(filename);
        }
    }
}

