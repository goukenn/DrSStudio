

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Utils.cs
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
file:Utils.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.VideoEditionTableAddIn.Utils
{
    using IGK.ICore;using IGK.AudioVideo.AVI ;
    static class Utils
    {
        public static System.Drawing.Bitmap GetPicture(IGK.AudioVideo.AVI.AVIFile.VideoStream vid, int position)
        {
            if ((position < 0) || (position > vid.Length))
                return null;
                System.Drawing.Bitmap cbip = null;
                if (vid.BeginStreaming(0, vid.Length, (int)enuVideoPlaySpeed.Normal))
                {
                    if (vid.BeginGetFrame())
                    {
                        cbip= vid.GetFrame(position);
                        vid.EndGetFrame();
                    }
                    vid.EndStreaming();
                }
                return cbip;
        }
    }
}

