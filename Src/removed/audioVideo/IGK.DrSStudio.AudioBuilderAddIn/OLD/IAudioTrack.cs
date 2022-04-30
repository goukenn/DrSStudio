

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IAudioTrack.cs
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
file:IAudioTrack.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.AudioBuilder
{
    public interface IAudioTrack : IDisposable 
    {
        string Name { get; }
        TimeSpan Duration { get; }       
         IAudioSequenceCollections Sequences
        {
            get;
        }
        void Play();
        void Stop();
        void SaveTo(string filename);
        void Export();
        void Edit();
        void CopyAll();
        void Copy(TimeSpan From);
        void Copy(TimeSpan From, TimeSpan To);
        /// <summary>
        /// get the editable stream
        /// </summary>
        /// <returns></returns>
        IGK.AudioVideo.AVI.AVIEditableStream GetEditableStream();
    }
}

