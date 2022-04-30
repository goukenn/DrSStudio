

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IAudioSequence.cs
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
file:IAudioSequence.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
namespace IGK.DrSStudio.AudioBuilder
{
    public interface IAudioSequence : IComparer<IAudioSequence>, IDisposable 
    {
        int Form { get; }
        int To { get; }
        int Duration { get; }
        IAudioTrack Parent{get;}
        /// <summary>
        /// Get the sequence type
        /// </summary>
        enuAudioSequenceType SequenceType { get; }
    }
}

