

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: VideoDecoder.cs
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
file:VideoDecoder.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
/* 
-------------------------------------------------------------
This file is part of iGK-DEV-DrawingStudio
-------------------------------------------------------------
-------------------------------------------------------------
-------------------------------------------------------------
view license file in Documentation folder to get more info
Copyright (c) 2008-2010 
Author  : Charles A.D. BONDJE DOUE 
mail : bondje.doue@hotmail.com
-------------------------------------------------------------
-------------------------------------------------------------
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using IGK.ICore;using IGK.DrSStudio.AudioVideo.WinUI;
namespace IGK.DrSStudio.AudioVideo.Codec
{
    using IGK.DrSStudio;
    using IGK.DrSStudio.Codec;
    using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.Drawing2D.WinUI;
    using IGK.DrSStudio.AudioVideo.WinUI;
    using IGK.AudioVideo.AVI;
    using IGK.AudioVideo;
    using IGK.AudioVideo.Players;
    [CoreCodec ("AVI","VIDEO/File","avi;vob;mpeg;",
        Category = "VIDEO")]
    class VideoDecoder : CoreDecoderBase 
    {
        public override bool Open(ICoreWorkbench bench,string filename)
        {
            AVIFile v_file = null;
            try
            {
                v_file = AVIFile.Open(filename);
                if (v_file != null)
                {
                    AVIFile.VideoStream vid = v_file.GetVideoStream();
                    if (vid.Compression == 0)
                    {
                        XVideoSurfaceBase mvideo = new XVideoSurfaceEditor(
                            v_file , filename );
                        bench.Surfaces.Add(mvideo);
                        return true;
                    }
                    else
                    {
                        vid.Dispose();
                        v_file.Dispose();
                    }
                }
                VideoPlayer vidPlayer = VideoPlayer.Open(filename, Guid.NewGuid().ToString());
                if (vidPlayer != null)
                {
                    XVideoSurfaceBase vid = new AVVideoSurfacePlayer(vidPlayer, filename);
                    bench.Surfaces.Add(vid);
                }
            }
            catch (Exception ex)
            {
                CoreLog.WriteDebug( ex.Message + CoreSystem.GetString(CoreConstant.ERR_FILE_NOT_OPENED));
                return false;
            }
            finally {
                if (v_file != null)
                {
                    v_file.Dispose();
                }
            }
            return true ;
        }
    }
}

