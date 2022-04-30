

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IVideoItemInfo.cs
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
file:IVideoItemInfo.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.VideoEditionTableAddIn
{
    /// <summary>
    /// represent a video item info
    /// </summary>
    public interface IVideoItemInfo
    {
        /// <summary>
        /// get if this video item has video
        /// </summary>
        bool HasVideo { get; }
        /// <summary>
        /// get if this video as Audio
        /// </summary>
        bool HasAudio { get; }
        /// <summary>
        /// get a video item duration
        /// </summary>
        TimeSpan Duration { get; }
        IGK.AudioVideo.AVI.AVIFile File{get;}
    }
}

