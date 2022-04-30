

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IVideoFile.cs
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
file:IVideoFile.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio
{
    public interface IVideoFile : IDisposable 
    {
        string FileName { get; }
        IntPtr AudHandle{get;}
        IntPtr VidHandle { get; }
        bool HasAudio { get; }
        bool HasVideo { get; }
        int Width { get; }
        int Height { get;}
        TimeSpan Duration { get; }
        float Fps { get; }
        int Compression { get; }
        /// <summary>
        /// get the resulting bitmap
        /// </summary>
        /// <returns></returns>
        System.Drawing.Bitmap GetBitmap();
    }
}

