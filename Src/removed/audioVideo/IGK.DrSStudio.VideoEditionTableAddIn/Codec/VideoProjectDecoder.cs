

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: VideoProjectDecoder.cs
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
file:VideoProjectDecoder.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.VideoEditionTableAddIn.Codec
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.VideoEditionTableAddIn.WinUI;
    [IGK.DrSStudio.Codec.CoreCodec (VideoConstant.CODEC_NAME,VideoConstant.CODEC_MIMETYPE, VideoConstant.CODEC_EXTENSIONS) ]
    public sealed class VideoProjectDecoder : IGK.DrSStudio.Codec .CoreDecoderBase 
    {
        public override bool Open(IGK.DrSStudio.WinUI.ICoreWorkbench bench, string filename)
        {
            IVideoProject project = LoadProject(filename);
            if (project != null)
            {
                IVideoEditorSurface v_surface = CoreSystem.CreateWorkingObject(VideoConstant.VIDEOEDITOR_SURFACENAME) as IVideoEditorSurface;
                if (v_surface != null)
                {
                    v_surface.VideoProject = project;
                    bench.Surfaces.Add(v_surface);
                    return true;
                }
            }
            return false;
        }
        private IVideoProject LoadProject(string filename)
        {
            return null;
        }
    }
}

